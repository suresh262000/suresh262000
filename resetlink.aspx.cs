using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Drawing;

namespace HRMS
{
    public partial class resetlink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Session["email"].ToString();

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string email = Session["email"].ToString();
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            SqlCommand cmd = new SqlCommand("Update AdminMaster set Admin_Password = '" + txtpwd.Text + "',password_change_status=0 where Admin_Email= '" + email + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert ('your password has been successfully updated')</script>");
            txtpwd.Text = "";
            txtcofrmpwd.Text = "";
            //Response.Redirect("login.aspx", true);
            //Response.Redirect("login.aspx");


        }
    }

}
