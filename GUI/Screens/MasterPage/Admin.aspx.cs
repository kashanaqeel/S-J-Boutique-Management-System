using SJ_Botique_System.App_Start;
using SJ_Botique_System.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJ_Botique_System.GUI.Screens.MasterPage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void ShowData()
        {
            try
            {
                StringBuilder query = new StringBuilder();                          // new stringbuilder object
                List<Role> Collection = new List<Role>();                           // list of all roles
                query.Clear();
                query.Append($"SELECT Id, Name , Description FROM [Role]");         // add query to the string
                var result = DbUtility.GetDataTable(query.ToString());              // get datatable using DbUtility
                foreach (DataRow row in result.Rows)                                
                {
                    int Id = Convert.ToInt32(row["Id"]);                            // Populate the Role class with id, name, description
                    string Name = row["Name"].ToString();
                    string Description = row["Description"].ToString();
                    Collection.Add(new Role(Id, Name, Description));                // add role details to the list
                }
                roleGridView.DataSource = result;                                   // set data source of the GridView
                roleGridView.DataBind();                                            // binds the data source to the GridView
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)                                            // first load of page
            {
                string Id = (Session["userId"]?.ToString())?.Trim();    // get userid 
                if (String.IsNullOrEmpty(Id))                           // if no user logged in
                {
                    Response.Redirect("Login.aspx");                    // Redirect user to the log in page
                }
                else
                {
                    string whichPage = Request.QueryString["from"]; // Use of Query String 
                    if (whichPage == "roleManagement")              // if admin visiting website
                    {
                        ShowData();                                 // show roles of all users
                    }
                }
            }
        }
    }
}