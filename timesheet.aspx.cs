using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections;

using System.Security.Cryptography;
using System.IO;
using ClosedXML.Excel;

namespace HRMS
{
    public partial class timesheet : System.Web.UI.Page
    {
        //code written by kalai
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                get_timesheet();
                DropDownListSelectStatus_fill();
                department_list_fill();
                job_list_fill();
                employee_list_fill();

            }
        }

        protected void get_timesheet()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "select JM.Job_Title as Job_Name,DM.Dept_Name as Dept_Name,EM.Emp_Name as Emp_Name,SM.Status_Name as Status_Name,* from TimeSheetMaster TM left join JobAssignMaster JM on JM.Job_ID = TM.Job_FKID left join StatusMaster SM on SM.Status_Group_ID=TM.Status_FKID  left join DepartmentMaster DM on DM.Dept_ID = TM.Dept_FKID left join EmployerMaster EM on EM.Emp_MasterID = TM.Emp_FKID";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            Timesheet.DataSource = ds;
            Timesheet.DataBind();
            if (Timesheet.FooterRow != null)
            {
                Timesheet.UseAccessibleHeader = true;
                Timesheet.HeaderRow.TableSection = TableRowSection.TableHeader;
                Timesheet.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                Timesheet.UseAccessibleHeader = true;
                Timesheet.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Timesheet_Delete(object sender, GridViewDeleteEventArgs e)
        {

            string emp_id = Convert.ToString(Timesheet.DataKeys[e.RowIndex].Value.ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string deleteSQL = "delete from TimeSheetMaster where TS_ID ='" + emp_id + "'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(deleteSQL, con);


            con.Open();

            var row = cmd.ExecuteNonQuery();
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Deleted!','Timesheet has been deleted!','success')", true);
                this.get_timesheet();

            }

        }
        //dropdown_working_status
        public void DropDownListSelectStatus_fill()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = " select Status_Name,Status_Group_ID from StatusMaster  where Status_Type = 2 ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);


            //create_dropdown_fuc
            working_status_list.DataSource = dt;
            working_status_list.DataTextField = "Status_Name";
            working_status_list.DataValueField = "Status_Group_ID";
            working_status_list.DataBind();
            //edit_dropdown_func
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "Status_Name";
            DropDownList1.DataValueField = "Status_Group_ID";
            DropDownList1.DataBind();
        }
        //dropdown_job_list
        public void job_list_fill()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = " select Job_Description ,Job_ID from JobAssignMaster ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //create_dropdown_func
            job_list.DataSource = dt;
            job_list.DataTextField = "Job_Description";
            job_list.DataValueField = "Job_ID";
            job_list.DataBind();
            //edit_dropdown_func
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "Job_Description";
            DropDownList2.DataValueField = "Job_ID";
            DropDownList2.DataBind();

        }
        //dropdown_employee_list
        public void employee_list_fill()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = " select Emp_Name,Emp_MasterID from EmployerMaster ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //create_dropdown_func
            employeelist.DataSource = dt;
            employeelist.DataTextField = "Emp_Name";
            employeelist.DataValueField = "Emp_MasterID";
            employeelist.DataBind();
            //edit_dropdown_func
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "Emp_Name";
            DropDownList3.DataValueField = "Emp_MasterID";
            DropDownList3.DataBind();
        }
        //dropdown_department_list
        public void department_list_fill()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = " select Dept_Name,Dept_ID from DepartmentMaster ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            departmentlist.DataSource = dt;

            departmentlist.DataTextField = "Dept_Name";
            departmentlist.DataValueField = "Dept_ID";
            departmentlist.DataBind();

            DropDownList4.DataSource = dt;

            DropDownList4.DataTextField = "Dept_Name";
            DropDownList4.DataValueField = "Dept_ID";
            DropDownList4.DataBind();
        }

        protected void save_timesheet(object sender, EventArgs e)
        {

            string date = TS_Date.Value;
             
              if (date == "")
            {
                val_date.InnerText = "Select the date";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_date.InnerText = "";
            }
            string work_status = working_status_list.Text;
            if (work_status == "0" )
            {
                val_status.InnerText = "Enter the workingstatus";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_status.InnerText = "";
            }
            string Project = Project_m.Text;
            if (Project == "")
            {
                val_project.InnerText = "Enter the project";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_project.InnerText = "";
            }
            
            string job = job_list.Text;
            if (job == "")
            {
                val_job.InnerText = "Enter the job";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_job.InnerText = "";
            }
            string progress = Progress_m.Text;
            if (progress == "")
            {
                val_progress.InnerText = "Enter the progress";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_progress.InnerText = "";
            }
            string wor_hr = Working_Hour.Text;
            if (wor_hr == "")
            {
                val_wor_hr.InnerText = "Enter the workinghour";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_wor_hr.InnerText = "";
            }
            string employee = employeelist.Text;
            if (employee == "0")
            {
                val_employee.InnerText = "Enter the employee";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_employee.InnerText = "";
            }
            string dept = departmentlist.Text;
            if (dept == "0")
            {
                val_dept.InnerText = "Enter the dept";
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal1();", true);
                return;
            }
            else
            {
                val_dept.InnerText = "";
            }
            string isactive = "1";
            //string Created_By = "Admin";
            string Created_By = Session["username"].ToString();

            DateTime Created_Date = DateTime.Now;
            //$"DELETE FROM employee where _id = {_id}";
            string Query;
            Query = "insert into TimeSheetMaster (";
            Query += "TS_Date,";
            Query += "Status_FKID,";
            Query += "Project,";
            Query += "Job_FKID,";
            Query += "Progress,";
            Query += "Working_Hour,";
            Query += "Dept_FKID,";
            Query += "Emp_FKID,";
            Query += "IsActive,";
            Query += "Created_By,";
            Query += "Created_Date) ";
            Query += "Values (";
            Query += $"'{date}',";
            Query += $"'{work_status}',";
            Query += $"'{Project}',";
            Query += $"'{job}',";
            Query += $"'{progress}',";
            Query += $"'{wor_hr}',";
            Query += $"'{dept}',";
            Query += $"'{employee}',";
            Query += $"'{isactive}',";
            Query += $"'{Created_By}',";
            Query += $"'{DateTime.Now}')";

            //string emp_id = Convert.ToString(Timesheet.DataKeys[e.RowIndex].Value.ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string createSQL = $"{Query}";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(createSQL, con);

            con.Open();

            var row = cmd.ExecuteNonQuery();
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Saved!','Timesheet has been Saved!','success')", true);
                this.get_timesheet();

            }

        }
        protected void lnkedit_Click(object sender, EventArgs e)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            LinkButton lnkbtn = sender as LinkButton;
            //getting particular row linkbutton
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //getting userid of particular row
            int id = Convert.ToInt32(Timesheet.DataKeys[gvrow.RowIndex].Value.ToString());
            idname.Value = id.ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT TS_Date,Status_FKID,Project,Job_FKID,Progress,Working_Hour,Emp_FKID,Dept_FKID from TimeSheetMaster where TS_ID = @id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                Date2.Text = dr["TS_Date"].ToString().Substring(0, 10).Replace("/", "-");
                //( Convert.ToDateTime(dr["TS_Date"]).ToString("yyyy-MM-dd"));
                DropDownList1.Text = dr["Status_FKID"].ToString();
                TextBox1.Text = dr["Project"].ToString();
                DropDownList2.Text = dr["Job_FKID"].ToString();
                TextBox2.Text = dr["Progress"].ToString();
                TextBox3.Text = dr["Working_Hour"].ToString();
                DropDownList3.Text = dr["Emp_FKID"].ToString();
                DropDownList4.Text = dr["Dept_FKID"].ToString();

            }
            con.Close();
        }


        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string id = idname.Value;
            string date = Date2.Text;

            if (date == "")
            {
                val_dateedit.InnerText = " Please Select Date!";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                val_dateedit.InnerText = "";
            }
            string work_status = DropDownList1.Text;
            if (work_status == "Select Status")
            {
                val_workstatus.InnerText = " Please Select Workstatus";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                val_workstatus.InnerText = "";
            }
            string Project = TextBox1.Text;
            string job = DropDownList2.Text;
            if (job == "Select Job")
            {
                val_editjob.InnerText = " Please Select Job";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                val_editjob.InnerText = "";
            }
            string progress = TextBox2.Text;
            if (progress == "")
            {
                val_editprogress.InnerText = " Please Enter Progress";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                val_editprogress.InnerText = "";
            }
            string wor_hr = TextBox3.Text;
            if (wor_hr == "")
            {
                val_editwrkhr.InnerText = " Please Enter WorkingHour!";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                val_editwrkhr.InnerText = "";
            }
            string employee = DropDownList3.Text;
            if (employee == "Select Employee")
            {
                val_editemployee.InnerText = " Please Select Employee";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                val_editemployee.InnerText = "";
            }
            string dept = DropDownList4.Text;
            if (dept == "Select Department")
            {
                val_editdepart.InnerText = " Please Select Department";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                val_editdepart.InnerText = "";
            }
            //string modified_date = DateTime.Now();
            //string modified_date = DateTime.Now.ToString("yyyy-MM-dd");
            string modified_by = Session["username"].ToString();

            string Query;

            Query = $"Update TimeSheetMaster set TS_Date='{date}',Status_FKID='{work_status}',Project='{Project}',Job_FKID='{job}',Progress='{progress}',Working_Hour='{wor_hr}',Emp_FKID='{employee}',Dept_FKID='{dept}',Modified_Date='{DateTime.Now}',Modified_By='{modified_by}' where TS_ID={id}";


            //cmd.Parameters.AddWithValue("@date", date);
            //cmd.Parameters.AddWithValue("@status", work_status);
            //cmd.Parameters.AddWithValue("@project", Project);
            //cmd.Parameters.AddWithValue("@job", job);
            //cmd.Parameters.AddWithValue("@progress", progress);
            //cmd.Parameters.AddWithValue("@work_hr", wor_hr);
            //cmd.Parameters.AddWithValue("@employee", employee);
            //cmd.Parameters.AddWithValue("@dept", dept);,
            //cmd.Parameters.AddWithValue("@id", id);


            //string emp_id = Convert.ToString(Timesheet.DataKeys[e.RowIndex].Value.ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            SqlCommand cmd = new SqlCommand(Query, con);
            int row = cmd.ExecuteNonQuery();
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Updated!','Timesheet has been updated!','success')", true);
                this.get_timesheet();

            }
        }

        protected void excel_Click1(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM TimeSheetMaster"))
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
                                Response.AddHeader("content-disposition", "attachment;filename=TimesheetReport.xlsx");
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
