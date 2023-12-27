using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Drawing;


namespace HRMS
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //loginclick
        protected void login_click(object sender, EventArgs e)
        {
            string uid = Admin_Name.Text;
            string pass = Admin_Password.Text;
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Select * From AdminMaster where Admin_Name='"+ uid + "' and Admin_Password='" + pass + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["username"] = Admin_Name.Text;
                Response.Redirect("timesheet.aspx");

            }
            else
            {
                Label4.Text = "UserId & Password Is not correct Try again..!!";
            }
            con.Close();

        }
        //forgotemail_process
        protected void btnsend_Click(object sender, EventArgs e)
        {
            Session["email"] = txtemail.Text;
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);


            SqlDataAdapter adp = new SqlDataAdapter("select * from AdminMaster where Admin_Email=@email", con);
            DataTable dt = new DataTable();
            con.Open();

            adp.SelectCommand.Parameters.AddWithValue("@email", txtemail.Text);

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                SqlCommand cmd = new SqlCommand("Update AdminMaster set password_change_status=1 where Admin_Email='" + txtemail.Text + "'", con);

                cmd.ExecuteNonQuery();
                SendEmail();

                lbresult.Text = "successfully sent reset link on  your mail ,please check once! Thank you.";
                con.Close();

                cmd.Dispose();

                txtemail.Text = "";

            }
            else
            {

                lbresult.Text = "Please enter vaild email ,please check once! Thank you.";

            }

        }
        //sendmail
        private void SendEmail()
        {


                StringBuilder sb = new StringBuilder();
                sb.Append("Hi,<br/> Click on below given link to Reset Your Password<br/>");
                sb.Append("<a href=https://localhost:44323/resetlink.aspx?username=" + txtemail.Text);
                sb.Append("&email=" + txtemail.Text + ">Click here to change your password</a><br/>");
                sb.Append("<b>Thanks</b>,<br> Solution <br/>");
                sb.Append("<br/><b> for more post </b> <br/>");
                //sb.Append("<br/><a href=http://neerajcodesolution.blogspot.in");
                sb.Append("thanks");

                MailMessage message = new System.Net.Mail.MailMessage("ramsguna97@gmail.com", txtemail.Text.Trim(), "Reset Your Password", sb.ToString());

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.Port = 587;

                smtp.Credentials = new System.Net.NetworkCredential("ramsguna97@gmail.com", "glcgjjwfhkhdlrkb");

                smtp.EnableSsl = true;

                message.IsBodyHtml = true;

                smtp.Send(message);



        }
    }

    }
