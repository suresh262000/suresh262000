using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection.Emit;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace HRMS
{
    public partial class payrole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            get_data();
        }

        public void get_data()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT Project_Id,Project_Name,Resource,Start_Date,End_Date,status,Created_By,isactive from ProjectMaster";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            payRollTable.DataSource = ds;
            payRollTable.DataBind();

            payRollTable.UseAccessibleHeader = true;
            if (payRollTable.Rows.Count > 0)
            {
                payRollTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                payRollTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }



        }

        protected void Button90_Click(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{


            var projectname = Projectname.Text;


            if (projectname == "")
            {
                val_Projectname.InnerText = " Please enter project name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                val_Projectname.InnerText = "";
            }

            var Noofresource = noofresource.Text;
            if (Noofresource == "")
            {
                noofresource1.InnerText = " Please enter Noofresource";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                noofresource1.InnerText = "";
            }
            var StartDate = startdate.Text;
            if (StartDate == "")
            {
                val_startDate.InnerText = " Please Select StartDate";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                val_startDate.InnerText = "";
            }
            var EndDate = enddate.Text;
            if (EndDate == "")
            {
                val_enddate.InnerText = " Please Select EndDate";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                val_enddate.InnerText = "";
            }
            var Status = status.Text;
            if (Status == "")
            {
                val_status.InnerText = " Please enter Status";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                val_status.InnerText = "";
            }
            var Createdbby = TextBox1.Text;
            if (Createdbby == "")
            {
                Label7.InnerText = " Please Enter Created By";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                Label7.InnerText = "";
            }
            var ISactive = TextBox1.Text;
            if (ISactive == "")
            {
                Label8.InnerText = " Please Enter Is Active";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                Label8.InnerText = "";
            }




            DateTime nowDateTime = DateTime.Now;
            string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProjectMaster (Project_Name,Resource,Start_Date,End_Date,status,Created_By,isactive,Created_Date) VALUES (  @ProjectName, @Resource, @StartDate, @EndDate, @status,@CreatedBy,@IsActive,@CreatedDate)"))
                {
                    //cmd.Parameters.AddWithValue("@Payroll_ID", Payroll_ID.Text);
                    //cmd.Parameters.AddWithValue("@ProjectId", SalaryAmount.Text);
                    cmd.Parameters.AddWithValue("@ProjectName", Projectname.Text);
                    cmd.Parameters.AddWithValue("@Resource", noofresource.Text);
                    cmd.Parameters.AddWithValue("@StartDate", startdate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", enddate.Text);
                    cmd.Parameters.AddWithValue("@status", status.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@IsActive", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@CreatedDate",nowDateTime );


                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                get_data();
                Response.Redirect(Request.Url.AbsoluteUri);
                //}
                //}
            }
        }
        protected void lnkdelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            //getting particular row linkbutton
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //getting userid of particular row
            int id = Convert.ToInt32(payRollTable.DataKeys[gvrow.RowIndex].Value.ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "delete ProjectMaster where Project_Id = @id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Parameters.AddWithValue("@id", id);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //cmd.ExecuteNonQuery();
            //Response.Redirect("project.aspx", true);
            var row = cmd.ExecuteNonQuery();
            con.Close();

            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Deleted!','Project has been deleted!','success')", true);
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", false);
                this.get_data();


            }
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
            string selectSQL = "SELECT Project_Id,Project_Name,Resource,Start_Date,End_Date,status,Created_By,Created_Date,Modified_By,Modified_Date,isactive from ProjectMaster where Project_Id = @id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Project_ID_EDIT.Text = dr["Project_Id"].ToString();
                ProjectName_EDIT.Text = dr["Project_Name"].ToString();
                Noofresources_EDIT.Text = dr["Resource"].ToString();
                Startdate_EDIT.Text = Convert.ToDateTime(dr["Start_Date"]).ToString("yyyy-MM-dd");

                Enddate_EDIT.Text = Convert.ToDateTime(dr["End_Date"]).ToString("yyyy-MM-dd");
                Status_EDIT.Text = dr["status"].ToString();
               Modified_EDIT.Text= dr["Modified_By"].ToString();
                IsActive_EDIT.Text= dr["isactive"].ToString();
            }
            con.Close();

        }
        protected void button_002_clk(object sender, EventArgs e)
        {
            // now you write edit function here
            //var projectid = Project_ID_EDIT.Text;
            //if (projectid == "")
            //{
            //    Label6.InnerText = " Please Reenter project name";
            //    ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
            //    return;
            //}
            //else
            //{
            //    Label6.InnerText = "";
            //}

            var projectnamee = ProjectName_EDIT.Text;


            if (projectnamee == "")
            {
                Label1.InnerText = " Please Reenter project name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label1.InnerText = "";
            }

            var noofresourcee = Noofresources_EDIT.Text;
            if (noofresourcee == "")
            {
                Label2.InnerText = " Please Reenter no of Resource name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label2.InnerText = "";
            }

            var startdatee = Startdate_EDIT.Text;
            if ( startdatee  == "")
            {
                Label3.InnerText = " Please Reenter StartDate name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label3.InnerText = "";
            }
            var enddatee = Enddate_EDIT.Text;
            if (enddatee == "")
            {
                Label4.InnerText = " Please Reenter EndDate name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label4.InnerText = "";
            }
            var statuss = Status_EDIT.Text;
            if (statuss == "")
            {
                Label5.InnerText = " Please Reenter Status name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label5.InnerText = "";
            }
            var modifiedby = Modified_EDIT.Text;
            if (modifiedby == "")
            {
                Label6.InnerText = " Please Enter ModifiedBy name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label6.InnerText = "";
            }
            var isactive = IsActive_EDIT.Text;
            if (isactive == "")
            {
                Label7.InnerText = " Please Enter IsActive name";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label7.InnerText = "";
            }
            DateTime nowDateTime = DateTime.Now;
            string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            //string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;


            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //using (SqlCommand cmd = new SqlCommand("INSERT INTO ProjectMaster (Project_Name,Resource,Start_Date,End_Date,status) VALUES (  @ProjectName, @Resource, @StartDate, @EndDate, @status)"))

            SqlCommand cmd = new SqlCommand("UPDATE ProjectMaster SET Project_Name=@ProjectName,Resource=@Resource,Start_Date=@StartDate,End_Date=@EndDate,Status=@status,Modified_By=@ModifiedBy,IsActive=@IsActive,Modified_Date=@ModifiedDate WHERE Project_Id=@Project_Id", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectName", ProjectName_EDIT.Text);
            cmd.Parameters.AddWithValue("@Resource", Noofresources_EDIT.Text);
            cmd.Parameters.AddWithValue("@StartDate", Startdate_EDIT.Text);
            cmd.Parameters.AddWithValue("@EndDate", Enddate_EDIT.Text);
            cmd.Parameters.AddWithValue("@status", Status_EDIT.Text);
            cmd.Parameters.AddWithValue("@Project_Id", Project_ID_EDIT.Text);
            cmd.Parameters.AddWithValue("@ModifiedBy", Modified_EDIT.Text);
            cmd.Parameters.AddWithValue("@IsActive", IsActive_EDIT.Text);
            cmd.Parameters.AddWithValue("@ModifiedDate", nowDateTime);

            cmd.ExecuteNonQuery();
            conn.Close();
            ClientScript.RegisterClientScriptBlock
         (this.GetType(), "K", "swal('Success!','Project update Successfully!','success')", true);
            this.get_data();





        }

        protected void excel_Click1(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Project_Id,Project_Name,Resource,Start_Date,End_Date,status,Created_Date,Modified_Date,Modified_By,Created_By,isactive from ProjectMaster"))
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
                                Response.AddHeader("content-disposition", "attachment;filename=ProjectMaster.xlsx");
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
    }
}




