﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace SignUP_Login
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["dbrole"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            //Response.Write("Your User Id Is:  " + Session["u"].ToString() + "<br>");
            //Response.Write("Your password  Is : " + Session["p"].ToString() + "<br>");
            //Response.Write("Your Role Is : " + Session["role"].ToString() + "<br>");
            //Response.Write("Your Role Is : " + Session["id"].ToString() + "<br>");


            if (Session["u"] != null)
            {
                string userRole = Session["role"].ToString(); // "admin"  or "user"

                if (userRole.Equals("user", StringComparison.OrdinalIgnoreCase))
                {
                    SqlConnection con = new SqlConnection(cs);
                    string query = "select * from signup1 where userid=@userId";
                    SqlDataAdapter sql = new SqlDataAdapter(query, con);
                    sql.SelectCommand.Parameters.AddWithValue("@userId", Session["u"].ToString());
                    DataTable table = new DataTable();
                    sql.Fill(table); // My "table" is cosnsisting of all the data from my database (only one row)


                    GridView1.DataSourceID = null; // break up with my sqldatasource1
                    GridView1.DataSource = table; // "table" is the replacement
                    GridView1.AutoGenerateEditButton = false;
                    GridView1.AutoGenerateDeleteButton = false;
                    GridView1.DataBind(); // it will be binding my new datasource with the grid

                }
            }
            else
            {
                Response.Redirect("WebForm1.aspx");
            }
        }

        


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    // Style the Update and Cancel buttons
                    LinkButton updateButton = (LinkButton)e.Row.Cells[0].Controls[0]; // Assuming Update button is in the first cell
                    updateButton.CssClass = "update-button";

                    LinkButton cancelButton = (LinkButton)e.Row.Cells[0].Controls[2]; // Assuming Cancel button is in the first cell
                    cancelButton.CssClass = "cancel-button";
                }
                else
                {
                    // Style the Edit and Delete buttons
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        foreach (Control control in cell.Controls)
                        {
                            if (control is LinkButton btn)
                            {
                                if (btn.CommandName == "Edit")
                                {
                                    btn.CssClass = "edit-button";
                                }
                                else if (btn.CommandName == "Delete")
                                {
                                    btn.CssClass = "delete-button";
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void sessison_back(object sender, EventArgs e)
        {

            Session.Clear();
            Session.Abandon();
            Response.Redirect("WebForm1.aspx");

        }
    }
}