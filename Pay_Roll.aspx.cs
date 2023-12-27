using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS_
{
    public partial class Pay_Roll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            get_data();

            getdrop();
         
        }
        public void getdrop()
        {
            var drop1 = tmp_Emp_Department_MSG11.Text;
            if (drop1 != "")
            {
                DropDownList1.SelectedValue = drop1;
            }

        }
        public void get_data()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT p.Payroll_ID ,p.SalaryAmount, p.BonusAmount ,p.PFAmount ,p.OtherAllowance ,d.Dept_Name ,e.Emp_Name , p.IsActive FROM PayrollMaster as p JOIN DepartmentMaster d ON d.Dept_ID = P.Dept_FKID JOIN EmployerMaster e ON e.Emp_MasterID = p.Emp_FKID WHERE isnumeric(d.Dept_ID) = isnumeric(p.Dept_FKID) and isnumeric(e.Emp_MasterID) = isnumeric(p.Emp_FKID)";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            payRollTable.DataSource = ds;
            payRollTable.DataBind();

            if (payRollTable.FooterRow != null)
            {
                payRollTable.UseAccessibleHeader = true;
                payRollTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                payRollTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                payRollTable.UseAccessibleHeader = true;
                payRollTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            //string connectionString = ConfigurationManager.ConnectionStrings["HRMS_DB"].ConnectionString;
            string com = "Select * from DepartmentMaster";
            SqlDataAdapter adpt = new SqlDataAdapter(com, connectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "Dept_Name";
            DropDownList1.DataValueField = "Dept_ID";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Select Department", "0"));
            DropDownList3.DataSource = dt;
            DropDownList3.DataBind();
            DropDownList3.DataTextField = "Dept_Name";
            DropDownList3.DataValueField = "Dept_ID";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("Select Department", "0"));


            string comm = "Select * from EmployerMaster";
            SqlDataAdapter adptr = new SqlDataAdapter(comm, connectionString);
            DataTable dtt = new DataTable();
            adptr.Fill(dtt);
            DropDownList2.DataSource = dtt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "Emp_Name";
            DropDownList2.DataValueField = "Emp_MasterID";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("Select Employment", "0"));
            DropDownList4.DataSource = dtt;
            DropDownList4.DataBind();
            DropDownList4.DataTextField = "Emp_Name";
            DropDownList4.DataValueField = "Emp_MasterID";
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, new ListItem("Select Employment", "0"));

           
        }

        public void valueCheck()
        {
            
            
            if (SalaryAmount.Text == "" || BonusAmount.Text == "" || PFAmount.Text == "" || OtherAllowance.Text == "" || tmp_Emp_Department_MSG11.Text =="" || tmp_Emp_FKID_MSG11.Text == "")
            {
                if (SalaryAmount.Text == "")
                {
                    SalaryAmount_MSG11.Text = "please select salary amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreateModal();", true);
                    return;
                }
                if (BonusAmount.Text == "")
                {
                    BonusAmount_MSG11.Text = "please select Bonus amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreateModal();", true);
                    return;
                }
                if (PFAmount.Text == "")
                {
                    PFAmount_MSG11.Text = "please select PF amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreateModal();", true);
                    return;
                    ;
                }
                if (OtherAllowance.Text == "")
                {
                    OtherAllowance_MSG11.Text = "please select Other allowance amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreateModal();", true);
                    return;
                }

                if (tmp_Emp_Department_MSG11.Text == "")
                {
                    Emp_Department_MSG11.Text = "please select Other Employee!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreateModal();", true);
                    return;
                }
                if (tmp_Emp_FKID_MSG11.Text == "")
                {
                    Emp_FKID_MSG.Text = "please select Other Department!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreateModal();", true);
                    return;
                }
            }

        }
        public void valueCheck1()
        {

            if (SalaryAmount1.Text == "" || BonusAmount1.Text == "" || PFAmount1.Text == "" || OtherAllowancel1.Text == "" || tmp_Emp_Department_MSG2.Text == "" || tmp_Emp_FKID_MSG2.Text == "")
            {
                if (SalaryAmount1.Text == "")
                {
                    SalaryAmount_MSG1.Text = "please select salary amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    
                    //return;
                }
                if (BonusAmount1.Text == "")
                {
                    BonusAmount_MSG1.Text = "please select Bonus amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    //return;
                }
                if (PFAmount1.Text == "")
                {
                    PFAmount_MSG1.Text = "please select PF amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    /*return*/
                    ;
                }
                if (OtherAllowancel1.Text == "")
                {
                    OtherAllowance_MSG11.Text = "please select Other allowance amount!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    //return;
                }

                if (tmp_Emp_Department_MSG2.Text == "")
                {
                    Emp_Department_MSG1.Text = "please select Other Employee!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    
                  
                }
               
                if (tmp_Emp_FKID_MSG2.Text == "")
                {
                    Emp_FKID_MSG1.Text = "please select Other Department!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                   
                }
               
            }
        }
            protected void SalaryAmount_KeyPress(object sender, EventArgs e)
        {
            valueCheck();
        }
        protected void Button90_Click(object sender, EventArgs e)
        {
            var ss = tmp_Emp_Department_MSG11.Text;
            //if (!IsPostBack)
            //{
            if (SalaryAmount.Text == "" || BonusAmount.Text == "" || PFAmount.Text == "" || OtherAllowance.Text == "" || tmp_Emp_Department_MSG11.Text == "" || tmp_Emp_FKID_MSG11.Text == "")
            {
                
                valueCheck();
            }
            //    string valCk = valueCheck();
            //if (valCk == "ok")
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;


                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO PayrollMaster (SalaryAmount, BonusAmount, PFAmount, OtherAllowance, Dept_FKID, Emp_FKID , IsActive) VALUES ( @SalaryAmount, @BonusAmount, @PFAmount, @OtherAllowance, @Dept_FKID, @Emp_FKID, @IsActive)"))
                    {
                        //cmd.Parameters.AddWithValue("@Payroll_ID", Payroll_ID.Text);
                        cmd.Parameters.AddWithValue("@SalaryAmount", SalaryAmount.Text);
                        cmd.Parameters.AddWithValue("@BonusAmount", BonusAmount.Text);
                        cmd.Parameters.AddWithValue("@PFAmount", PFAmount.Text);
                        cmd.Parameters.AddWithValue("@OtherAllowance", OtherAllowance.Text);
                        cmd.Parameters.AddWithValue("@Dept_FKID", tmp_Emp_Department_MSG11.Text);
                        cmd.Parameters.AddWithValue("@Emp_FKID", tmp_Emp_FKID_MSG11.Text);
                        cmd.Parameters.AddWithValue("@IsActive", 1);


                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    get_data();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }

            }
            //}
        }

        protected void lnkdelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            //getting particular row linkbutton
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //getting userid of particular row
            int id = Convert.ToInt32(payRollTable.DataKeys[gvrow.RowIndex].Value.ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "delete PayrollMaster where Payroll_ID = @id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Parameters.AddWithValue("@id", id);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            // Response.Redirect("payroll.aspx", true);
            ClientScript.RegisterClientScriptBlock
                     (this.GetType(), "K", "swal('Deleted!','Employee has been deleted!','success')", true);
            get_data();

        }
        protected void lnkedit_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            LinkButton lnkbtn = sender as LinkButton;
            //getting particular row linkbutton
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //getting userid of particular row
            int id = Convert.ToInt32(payRollTable.DataKeys[gvrow.RowIndex].Value.ToString());

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT Payroll_ID, SalaryAmount, BonusAmount, PFAmount, OtherAllowance, Dept_FKID, Emp_FKID from PayrollMaster where Payroll_ID = @id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Payroll_ID1.Text = dr["Payroll_ID"].ToString();
                SalaryAmount1.Text = dr["SalaryAmount"].ToString();
                BonusAmount1.Text = dr["BonusAmount"].ToString();
                PFAmount1.Text = dr["PFAmount"].ToString();
                OtherAllowancel1.Text = dr["OtherAllowance"].ToString();
                DropDownList3.SelectedValue = dr["Dept_FKID"].ToString();
                tmp_Emp_Department_MSG2.Text = dr["Dept_FKID"].ToString();
                DropDownList4.SelectedValue = dr["Emp_FKID"].ToString();
                tmp_Emp_FKID_MSG2.Text = dr["Emp_FKID"].ToString();

            }
            con.Close();

        }
        protected void button_002_clk(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreateModal();", true);
            if (SalaryAmount1.Text == "" || BonusAmount1.Text == "" || PFAmount1.Text == "" || OtherAllowancel1.Text == "" || tmp_Emp_Department_MSG2.Text == "" || tmp_Emp_FKID_MSG2.Text == "")
            {
                valueCheck1();
            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;

                SqlConnection con = new SqlConnection(constr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("_sp_UpdatePayrole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Payroll_ID", Payroll_ID1.Text);
                cmd.Parameters.AddWithValue("@SalaryAmount", SalaryAmount1.Text);
                cmd.Parameters.AddWithValue("@BonusAmount", BonusAmount1.Text);

                cmd.Parameters.AddWithValue("@PFAmount", PFAmount1.Text);
                cmd.Parameters.AddWithValue("@OtherAllowance", OtherAllowancel1.Text);
                cmd.Parameters.AddWithValue("@Dept_FKID", tmp_Emp_Department_MSG2.Text);
                cmd.Parameters.AddWithValue("@Emp_FKID", tmp_Emp_FKID_MSG2.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                get_data();
                //Response.Redirect("payroll.aspx", true);
            }
        }
        protected void excel_Click1(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMS_DB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT p.Payroll_ID ,p.SalaryAmount, p.BonusAmount ,p.PFAmount ,p.OtherAllowance ,d.Dept_Name ,e.Emp_Name FROM PayrollMaster as p JOIN DepartmentMaster d ON d.Dept_ID = P.Dept_FKID JOIN EmployerMaster e ON e.Emp_MasterID = p.Emp_FKID WHERE isnumeric(d.Dept_ID) = isnumeric(p.Dept_FKID) and isnumeric(e.Emp_MasterID) = isnumeric(p.Emp_FKID)"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "Emoloyee");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=PayROll.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}