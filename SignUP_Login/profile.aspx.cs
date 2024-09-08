using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignUP_Login
{
    public partial class profile : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["dbrole"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["u"] != null)
            {
                if (!IsPostBack)
                {
                    //Response.Write("Your Role Is : " + Session["id"].ToString() + "<br>");
                    NameText.Text = Session["name"].ToString();
                    EmailText.Text = Session["email"].ToString();
                    UserIdText.Text = Session["u"].ToString();
                    RoleText.Text = Session["role"].ToString();
                    PasswordText.Text = Session["p"].ToString();
                    ConfirmPasswordText.Text = Session["p"].ToString();
                    if (Session["gender"].ToString().Equals("Male",StringComparison.OrdinalIgnoreCase))
                    {
                        GenderDrop.Items[1].Selected = true;
                    }
                    else if (Session["gender"].ToString().Equals("Female",StringComparison.OrdinalIgnoreCase))
                    {
                        GenderDrop.Items[2].Selected = true;
                    }
                    else if (Session["gender"].ToString().Equals("Others", StringComparison.OrdinalIgnoreCase))
                    {
                        GenderDrop.Items[3].Selected = true;
                    }
                    else
                    {
                        GenderDrop.Items[0].Selected = true;
                    }
                }
            }
            else
            {
                Response.Redirect("WebForm1.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection(cs);

            string query = "UPDATE signup1 SET name=@name, email=@email, gender=@gender, userid=@userid, role=@role, password=@password WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@name", NameText.Text);
            cmd.Parameters.AddWithValue("@email", EmailText.Text);
            cmd.Parameters.AddWithValue("@gender", GenderDrop.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@userid", UserIdText.Text);
            cmd.Parameters.AddWithValue("@role", RoleText.Text);
            cmd.Parameters.AddWithValue("@password", PasswordText.Text);
            cmd.Parameters.AddWithValue("@id", Session["id"]);

            con.Open();
            int num = cmd.ExecuteNonQuery();
            if (num > 0)
            {
                Response.Redirect("WebForm2.aspx");
            }

            con.Close();

        }
    }

}