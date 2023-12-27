using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace HRMS
{
    public partial class leave_form : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
            from_date.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            end_date.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
           
            this.getdata();
            getdrop();

        }
        public void getdrop()
        {
            var drop1 = Employe_Name.Text;
            if (drop1 != "")
            {
                DropDownList3.SelectedValue = drop1;
            }
            var drop2 = Employe_ID.Text;
            if (drop2 != "")
            {
                DropDownList4.SelectedValue = drop2;
            }

        }
        public void getdata()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT  LEAVE_ID,EMP_FKID, EMP_NAME,LEAVE_START, LEAVE_END, LEAVE_TYPE, LEAVE_REASON, APPLY_STATUS from LEAVEFORM ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            leaveform.DataSource = ds;
            leaveform.DataBind();
            if (leaveform.FooterRow != null)
            {

                leaveform.UseAccessibleHeader = true;
                leaveform.HeaderRow.TableSection = TableRowSection.TableHeader;
                leaveform.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                leaveform.UseAccessibleHeader = true;
                leaveform.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            string comm = "Select * from EmployerMaster";
            SqlDataAdapter adptr = new SqlDataAdapter(comm, connectionString);
            DataTable dtt = new DataTable();
            adptr.Fill(dtt);
            DropDownList3.DataSource = dtt;
            DropDownList3.DataBind();
            DropDownList3.DataTextField = "Emp_Name";
            DropDownList3.DataValueField = "Emp_Name";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("Select Employment Name", "0"));

           
            DropDownList4.DataSource = dtt;
            DropDownList4.DataBind();
            DropDownList4.DataTextField = "Emp_MasterID";
            DropDownList4.DataValueField = "Emp_MasterID";
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, new ListItem("Select Employment ID", "0"));

        }


        protected void apply_Click(object sender, EventArgs e)
        {
            var eid = Employe_ID.Text;
            if (eid == "")
            {
                errorid.Text = "Enter Employee Id";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                errorid.Text = "";
            }
            var id = int.Parse(eid);
            var name = Employe_Name.Text;
            if (name == "")
            {
                errorname.Text = "Enter Employee Name";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                errorname.Text = "";
            }
           

            var date = from_date.Text;
            if (date == "")
            {
                errorstart.Text = "Enter Start Leave date";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                errorstart.Text = "";
            }
            var startdate = Convert.ToDateTime(from_date.Text).ToShortDateString();

            var date1 = end_date.Text;
            if (date1 == "")
            {
                errorend.Text = "Enter Start Leave date";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                errorend.Text = "";
            }

            var enddate = Convert.ToDateTime(end_date.Text).ToShortDateString();


            var leavetype = DropDownList1.SelectedValue;

            if (leavetype == "0")
            {
                errortype.Text = " Please select leave type!";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                errortype.Text = "";
            }
            var reasonleave = reason.Text;


            if (reasonleave == "")
            {
                errorreason.Text = " Please select reason!";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                errorreason.Text = "";
            }
            var status = "pending";
            
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Insert into LEAVEFORM values('" + name + "' ,'" + startdate + "','" + enddate + "','" + leavetype + "','" + reasonleave + "','" + status + "','" + id + "')";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            //LoadRecord();
            Employe_ID.Text = "";
            Employe_Name.Text = "";
            from_date.Text = "";
            end_date.Text = "";
            reason.Text = "";
            DropDownList1.SelectedValue = "0";
        
            ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Success!','Leave Applied Successfully!','success')", true);
            con.Close();
            this.getdata();
            // Response.Redirect("leave_form.aspx", true);







        }
        protected void Employee_Delete(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(leaveform.DataKeys[e.RowIndex].Value.ToString());
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Delete from LEAVEFORM where LEAVE_ID='" + id + "'";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            var row = cmd.ExecuteNonQuery();
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Deleted!','Employee has been deleted!','success')", true);
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", false);
                this.getdata();


            }


        }







        protected void leaveform_RowCommand1(object sender, GridViewCommandEventArgs e)
        {


            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "createmodal1", "$(document).ready(function () {$('#createmodal1').modal();});", true);
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);


                int Index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = leaveform.Rows[Index];
                
                var id = leaveform.DataKeys[row.RowIndex].Value.ToString();
                var leaveid = HttpUtility.HtmlDecode(row.Cells[0].Text);
                var id1 = HttpUtility.HtmlDecode(row.Cells[1].Text);
                var name = HttpUtility.HtmlDecode(row.Cells[2].Text);
                var start = HttpUtility.HtmlDecode(row.Cells[3].Text);
                var end = HttpUtility.HtmlDecode(row.Cells[4].Text);
                var type = HttpUtility.HtmlDecode(row.Cells[5].Text);
                var reason1 = HttpUtility.HtmlDecode(row.Cells[6].Text);
                

                string newFormat = DateTime.ParseExact(start, "dd-MM-yyyy", CultureInfo.InvariantCulture)
    .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string newFormat1 = DateTime.ParseExact(end, "dd-MM-yyyy", CultureInfo.InvariantCulture)
  .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                TextBox1.Text = id1;
                TextBox2.Text = name;
                newFormat = DateTime.Parse(newFormat).ToString("yyyy-MM-dd");
                TextBox3.Text = newFormat.ToString();
                newFormat1 = DateTime.Parse(newFormat1).ToString("yyyy-MM-dd");
                TextBox4.Text = newFormat1.ToString();
                DropDownList2.SelectedValue = type;
                TextBox5.Text = reason1;
                TextBox6.Text = leaveid;
            }
        }



        protected void update_Click(object sender, EventArgs e)
        {
            var id = int.Parse(TextBox1.Text);
            var name = TextBox2.Text;


            var date = TextBox3.Text;
            if (date == "")
            {
                error1.Text = "Enter Start Leave date";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                error1.Text = "";
            }
            var startdate = Convert.ToDateTime(TextBox3.Text).ToShortDateString();
            
            var date1 = TextBox4.Text;
            if (date1 == "")
            {
                error2.Text = "Enter End Leave date";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                error2.Text = "";
            }
            var enddate = Convert.ToDateTime(TextBox4.Text).ToShortDateString();


            var leavetype = DropDownList2.SelectedValue;

            if (leavetype == "0")
            {
                error3.Text = "Enter  Leave type";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                error3.Text = "";
            }
            var reasonleave = TextBox5.Text;

            if (reasonleave == "")
            {
                error4.Text = "Enter  Leave reason";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                error4.Text = "";
            }
            var status = "pending";
            var leaveid = TextBox6.Text;

            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Update LEAVEFORM set EMP_FKID=@empid,EMP_NAME=@employname,LEAVE_START=@start,LEAVE_END=@end,LEAVE_TYPE=@type,LEAVE_REASON=@reason,APPLY_STATUS=@status where LEAVE_ID=@leaveid";
            SqlCommand cmd = new SqlCommand(query);
            //cmd.Parameters.AddWithValue("@StudentID", name);
            cmd.Parameters.AddWithValue("@empid", id);
            cmd.Parameters.AddWithValue("@employname", name);
            cmd.Parameters.AddWithValue("@start", startdate);
            cmd.Parameters.AddWithValue("@end", enddate);
            cmd.Parameters.AddWithValue("@type", leavetype);
            cmd.Parameters.AddWithValue("@reason", reasonleave);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@leaveid", leaveid);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            ClientScript.RegisterClientScriptBlock
                  (this.GetType(), "K", "swal('Success!','Leave update Successfully!','success')", true);
         //   Response.Redirect("~/leave_form.aspx");
            this.getdata();
          
        }
        protected void close(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", false);
            Employe_ID.Text = "";
            Employe_Name.Text = "";
            from_date.Text = "";
            end_date.Text = "";
            reason.Text = "";
            DropDownList1.SelectedValue = "0";
            errorname.Text = "";
            errorid.Text = "";
            errorstart.Text = "";
            errorend.Text = "";
            errortype.Text = "";
            errorreason.Text = "";

            this.getdata();
           
        }



        protected void excel_Click1(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM LEAVEFORM"))
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
                                Response.AddHeader("content-disposition", "attachment;filename=EmployeLeaveform.xlsx");
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

