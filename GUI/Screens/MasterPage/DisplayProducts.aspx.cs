using SJ_Botique_System.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SJ_Botique_System.Entities;

namespace SJ_Botique_System.GUI.Screens.MasterPage
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //getting userId session variable
            string Id = (Session["userId"]?.ToString())?.Trim();
            //if Id is NULL
            if (String.IsNullOrEmpty(Id))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                ShowProducts();
            }
        }
        protected void ShowProducts()
        {
            StringBuilder query = new StringBuilder();
            //Creating List of Product objects to collect all products
            List<Product> Collection = new List<Product>();
            // Clearing the query
            query.Clear();
            //Appending the query
            query.Append($"SELECT Id, Name, Description , Price FROM Product");
            //Getting the dataTable from database through DbUtility
            var result = DbUtility.GetDataTable(query.ToString());
            //Avoiding extra columns
            GridView1.AutoGenerateColumns = false;
            //Populating data in GridView
            GridView1.DataSource = result;
            //Binding Data in GridView
            GridView1.DataBind();

            //Collecting all the Product objects in List named Collection
            foreach (DataRow row in result.Rows)
            {
                int Id = Convert.ToInt32(row["Id"]);
                string Name = row["Name"].ToString();
                double Price = double.Parse(row["Price"].ToString());
                Product W = new Product(Name, Price);
                W.SetId(Id);
                Collection.Add(W);
            }
        }

    }
}