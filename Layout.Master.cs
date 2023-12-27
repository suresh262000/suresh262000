using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                usr.Text = Session["username"].ToString();
                string s = usr.Text.Substring(0, 1);
                userfirstletter.Text = s;
            }
            else
            {
                //ClientScript.RegisterClientScriptBlock
                //       (this.GetType(), "K", "swal('Foreign Key Constraint Violation','Cannot Delete Record. Associated Data In Use.','warning')", true);
                Response.Redirect("login.aspx");
            }
        }
        
        protected void signout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Remove("username");
            Session.Remove("password");
            Response.Redirect("login.aspx");

        }

        protected void dashlink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Dashboard.aspx");
                
            }
           
        }

        protected void Departmentlink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Department.aspx");
            }
        }

        protected void Employeeslink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Employee.aspx");
            }
        }

        protected void Joblink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void Timesheetlink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Timesheet.aspx");
            }

        }

        protected void Paylink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Pay_Roll.aspx");
            }
        }

        protected void Projectslink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Project.aspx");
            }
        }

        protected void Leavelink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Leave_Form.aspx");
            }
        }

        protected void Catolink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Category.aspx");
            }
        }

        protected void SubCatolink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("SubCategory.aspx");
            }
        }

        protected void Prodlink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Products.aspx");
            }
        }

        protected void Dealerlink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Dealer.aspx");
            }
        }

        protected void StockMngntlink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("StockOut.aspx");
            }
        }

        protected void Procurementlink_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("Procurement.aspx");
            }
        }

        protected void Purchaserecieve_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            if (uname != "")
            {
                Response.Redirect("PurchaseRecieve.aspx");
            }
        }
    }
}