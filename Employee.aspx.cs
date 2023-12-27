using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using System.IO;
using ClosedXML.Excel;
using System.Drawing;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;

namespace HRMS
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                get_employees();
                deptdrpdown();
                deptdrpdown1();


            }
            
        }

        protected void get_employees()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT EM.Emp_MasterID, EM.Emp_ID, EM.Emp_Name, EM.Emp_DOB, EM.Emp_JoinDate, EM.Emp_Contact,EM.Emp_Email, EM.Emp_Designation\r\nfrom EmployerMaster EM,EmployerAccount EA, EmployerAssets EAS \r\nwhere EM.Emp_MasterID=EA.Emp_ID AND EM.Emp_MasterID = EAS.Emp_ID order by EM.Emp_JoinDate DESC";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            EmployeeTable.DataSource = ds;
            EmployeeTable.DataBind();
            if (EmployeeTable.FooterRow != null) 
            {
                EmployeeTable.UseAccessibleHeader = true;
                EmployeeTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                EmployeeTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                EmployeeTable.UseAccessibleHeader = true;
                EmployeeTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Employee_Delete(object sender, GridViewDeleteEventArgs e)
        {
            
            var emp_id = (int)Convert.ToInt64(EmployeeTable.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;            
            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                string deleteSQL1 = "delete from EmployerAccount where Emp_ID ='" + emp_id + "'";
                SqlCommand cmd1 = new SqlCommand(deleteSQL1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                string deleteSQL2 = "delete from EmployerAssets where Emp_ID ='" + emp_id + "'";
                SqlCommand cmd2 = new SqlCommand(deleteSQL2, con);
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();

                string deleteSQL3 = "delete from EmployerMaster where Emp_MasterID ='" + emp_id + "'";
                SqlCommand cmd3 = new SqlCommand(deleteSQL3, con);
                con.Open();
                int row = cmd3.ExecuteNonQuery();
                con.Close();

                if (row > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Deleted!','Employee has been deleted!','success')", true);
                    get_employees();
                    
                }
                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            
        }
        protected void deptdrpdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT Dept_ID, Dept_Name from DepartmentMaster";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL,con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            deptdropdown.DataSource = ds;
            deptdropdown.DataTextField = "Dept_Name";
            deptdropdown.DataValueField = "Dept_ID";
            deptdropdown.DataBind();
            deptdropdown.Items.Insert(0, new ListItem("Select Department", "0"));
        }
        protected void deptdrpdown1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT Dept_ID, Dept_Name from DepartmentMaster";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            deptdropdown_1.DataSource = ds;
            deptdropdown_1.DataTextField = "Dept_Name";
            deptdropdown_1.DataValueField = "Dept_ID";
            deptdropdown_1.DataBind();
            deptdropdown_1.Items.Insert(0, new ListItem("Select Department", "0"));
        }

        //protected void Empidvalidate(object sender, EventArgs e)
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmdd = new SqlCommand("SELECT Emp_ID from EmployerMaster WHERE Emp_ID LIKE '" + empid + "'", con);
        //        con.Open();
        //        string emp_id1 = Convert.ToString(cmdd.ExecuteScalar());
        //        con.Close();
        //        if (emp_id1 == "")
        //        {
        //            idlbl.Text = "Employee ID already exist";
        //        }
        //    }
        //}



        protected void Createemployee(object sender, EventArgs e)
        {
            string gen = "";
            string status = "";
            string qualifications = "";
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmdd = new SqlCommand("SELECT Emp_ID from EmployerMaster WHERE Emp_ID LIKE '" + empid.Text + "'", con);
                con.Open();
                string emp_id1 = Convert.ToString(cmdd.ExecuteScalar());
                con.Close();
                string email = empmail.Text;
                bool isValid = IsValidEmail(email);
                if (empid.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                    ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Enter Employee ID','warning')", true);
                }
                else if (emp_id1 == empid.Text)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                    ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Employee ID Already Exist!','warning')", true);
                }
                else if(empbloodgrp.Text == "" || empid.Text == "" || empname.Text == "" || empdob.Text == "" || empjoindt.Text == string.Empty || empcon.Text == string.Empty || empcon.Text.Length <=9 || empmail.Text == string.Empty || isValid == false || emppwd.Text == "" || deptdropdown.SelectedValue == "0" || empdesigntion.Text == "" || empcrntadd.Text == "" || permntadd.Text == "" || empdegree.Text == "" || ttlexp.Text == "" || nameasperbank.Text == "" || Bankname.Text == "" || accno.Text == "" || ifsc.Text == "" || acctypes.Text == "" || panno.Text == "" || aadharno.Text == "" || aadharno.Text.Length <= 11 || branchadd.Text == "" || mngrname.Text == "" || assetcount.Text == "" || dtofissue.Text == "" || (RadioButton1.Checked == false && RadioButton2.Checked == false) || (RadioButton3.Checked == false && RadioButton4.Checked == false) || (RadioButton5.Checked == false && RadioButton6.Checked == false && RadioButton7.Checked == false) || CheckBoxList1.SelectedValue == "")  
                {
                    Validations();
                }
                else
                {
                    
                        //for gender
                        if (RadioButton1.Checked)
                        {
                            gen = "Male";
                        }
                        else if (RadioButton2.Checked)
                        {
                            gen = "Female";
                        }

                        //for marital status
                        if (RadioButton3.Checked)
                        {
                            status = "Married";
                        }
                        else if (RadioButton4.Checked)
                        {
                            status = "Unmarried";
                        }

                        //for qualification
                        if (RadioButton5.Checked)
                        {
                            qualifications = "UG";
                        }
                        else if (RadioButton6.Checked)
                        {
                            qualifications = "PG";
                        }
                        else if (RadioButton7.Checked)
                        {
                            qualifications = "Others";
                        }


                        DateTime now = DateTime.Now;
                        //inserting in EmployerMaster Table
                        string insertSQL = "INSERT INTO EmployerMaster(Emp_ID, Emp_Name, Emp_DOB, Emp_JoinDate, Emp_Contact, Emp_Email,Emp_Password, Emp_Designation,Dept_FKID, IsActive, Created_Date, Alternative_contactNO, CurrentAddress, PermanentAddress, bloodgroup,Emp_Gender,Marital_Staus,Qualification,Degree,Additional_Certificates,Total_Experience,Relevant_experience) " +
                            "VALUES(@Emp_ID, @Emp_Name, @Emp_DOB, @Emp_JoinDate, @Emp_Contact, @Emp_Email,@Emp_Password, @Emp_Designation,@Dept_FKID,'1', @Created_Date, @Alternative_contactNO, @CurrentAddress," +
                            " @PermanentAddress, @bloodgroup,@Emp_Gender,@Marital_Staus,@Qualification,@Degree,@Additional_Certificates,@Total_Experience,@Relevant_experience)";
                        SqlCommand cmd = new SqlCommand(insertSQL, con);
                        cmd.Parameters.AddWithValue("@Emp_ID", empid.Text);
                        cmd.Parameters.AddWithValue("@Emp_Name", empname.Text);
                        cmd.Parameters.AddWithValue("@Emp_DOB", empdob.Text);
                        cmd.Parameters.AddWithValue("@Emp_JoinDate", empjoindt.Text);
                        cmd.Parameters.AddWithValue("@Emp_Contact", empcon.Text);
                        cmd.Parameters.AddWithValue("@Emp_Email", empmail.Text);
                        cmd.Parameters.AddWithValue("@Emp_Password", emppwd.Text);
                        cmd.Parameters.AddWithValue("@Emp_Designation", empdesigntion.Text);
                        cmd.Parameters.AddWithValue("@Dept_FKID", deptdropdown.SelectedValue);
                        //cmd.Parameters.AddWithValue("@Department", deptdropdown.Text );
                        cmd.Parameters.AddWithValue("@Created_Date", now);
                        cmd.Parameters.AddWithValue("@Alternative_contactNO", empaltercon.Text);
                        cmd.Parameters.AddWithValue("@CurrentAddress", empcrntadd.Text);
                        cmd.Parameters.AddWithValue("@PermanentAddress", permntadd.Text);
                        cmd.Parameters.AddWithValue("@bloodgroup", empbloodgrp.Text);
                        cmd.Parameters.AddWithValue("@Emp_Gender", gen);
                        cmd.Parameters.AddWithValue("@Marital_Staus", status);
                        cmd.Parameters.AddWithValue("@Qualification", qualifications);
                        cmd.Parameters.AddWithValue("@Degree", empdegree.Text);
                        cmd.Parameters.AddWithValue("@Additional_Certificates", empcertificate.Text);
                        cmd.Parameters.AddWithValue("@Total_Experience", ttlexp.Text);
                        cmd.Parameters.AddWithValue("@Relevant_experience", relevexp.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        cmd = new SqlCommand("SELECT MAX(Emp_MasterID) ID FROM EmployerMaster ", con);
                        con.Open();
                        int emp_id = Convert.ToInt16(cmd.ExecuteScalar());
                        con.Close();

                        //inserting in EmployerAccount Table
                        string insert_acc = "INSERT INTO EmployerAccount(Emp_ID, Emp_Name, Bank_Name, Account_No, IFSC_Code, Account_Type,PAN_No, Adhaar_No, Branch_Address) " +
                            "VALUES(@Emp_ID, @Emp_Name, @Bank_Name, @Account_No, @IFSC_Code, @Account_Type,@PAN_No, @Adhaar_No, @Branch_Address)";
                        SqlCommand inscmd = new SqlCommand(insert_acc, con);
                        inscmd.Parameters.AddWithValue("@Emp_ID", emp_id);
                        inscmd.Parameters.AddWithValue("@Emp_Name", nameasperbank.Text);
                        inscmd.Parameters.AddWithValue("@Bank_Name", Bankname.Text);
                        inscmd.Parameters.AddWithValue("@Account_No", accno.Text);
                        inscmd.Parameters.AddWithValue("@IFSC_Code", ifsc.Text);
                        inscmd.Parameters.AddWithValue("@Account_Type", acctypes.Text);
                        inscmd.Parameters.AddWithValue("@PAN_No", panno.Text);
                        inscmd.Parameters.AddWithValue("@Adhaar_No", aadharno.Text);
                        inscmd.Parameters.AddWithValue("@Branch_Address", branchadd.Text);

                        con.Open();
                        inscmd.ExecuteNonQuery();
                        con.Close();

                        String asset = "";
                        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                        {
                            if (CheckBoxList1.Items[i].Selected)
                            {
                                if (asset == "")
                                {
                                    asset = CheckBoxList1.Items[i].Text;
                                }
                                else
                                {
                                    asset += "," + CheckBoxList1.Items[i].Text;
                                }
                            }
                        }


                        //inserting in EmployerAssets Table
                        string insert_asset = "INSERT INTO EmployerAssets(Emp_ID, Report_Mngr_Name, Assets_Count, Date_of_Issue, Date_of_Surrender, Remarks,Asset_Details) " +
                            "VALUES(@Emp_ID, @Report_Mngr_Name, @Assets_Count, @Date_of_Issue, @Date_of_Surrender, @Remarks,@Asset_Details)";
                        SqlCommand asscmd = new SqlCommand(insert_asset, con);
                        asscmd.Parameters.AddWithValue("@Emp_ID", emp_id);
                        asscmd.Parameters.AddWithValue("@Report_Mngr_Name", mngrname.Text);
                        asscmd.Parameters.AddWithValue("@Assets_Count", assetcount.Text);
                        asscmd.Parameters.AddWithValue("@Date_of_Issue", dtofissue.Text);
                        asscmd.Parameters.AddWithValue("@Date_of_Surrender", dtofsurrend.Text);
                        asscmd.Parameters.AddWithValue("@Remarks", remarks.Text);
                        asscmd.Parameters.AddWithValue("@Asset_Details", asset);


                        con.Open();
                        int row3 = asscmd.ExecuteNonQuery();
                        con.Close();


                        if (row3 > 0)
                        {
                            ClientScript.RegisterClientScriptBlock
                                 (this.GetType(), "K", "swal('Success!','Employee Created Successfully!','success')", true);
                            get_employees();
                            //Response.Redirect("Employee.aspx");
                            //ClientScript.RegisterStartupScript
                            //    (GetType(), Guid.NewGuid().ToString(), "success();", true);
                        }

                        Make_empty();
                    
                }
                
            }
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(email))
                return false;

            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }

        protected void Validations()
        {
            string email = empmail.Text;
            bool isValid = IsValidEmail(email);

            if (empname.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Employee Name','warning')", true);
            }

            //if(empid.Text ==  )
            else if(empbloodgrp.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Blood Group','warning')", true);
            }
            else if (empdob.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Select Date of Birth','warning')", true);
            }
            else if (empjoindt.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Select Joining Date','warning')", true);
            }
            else if (empcon.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Contact No','warning')", true);
            }
            else if (empcon.Text.Length <=9 )
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter a Valid Contact No','warning')", true);
            }
            else if (empmail.Text =="")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Email','warning')", true);
            }
            
            else if(isValid == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter a Appropriate Email Format','warning')", true);
            }
            else if (emppwd.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Password','warning')", true);
            }

            else if (deptdropdown.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Select Department','warning')", true);
            }
            else if (empdesigntion.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Designation','warning')", true);
            }

            else if (empcrntadd.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Current Address','warning')", true);
            }
            else if (permntadd.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Permanent Address','warning')", true);
            }
            else if (empdegree.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Degree','warning')", true);
            }

            else if (ttlexp.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Total Experience','warning')", true);
            }

            else if (nameasperbank.Text =="")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Name as per Bank','warning')", true);
            }
            else if (Bankname.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Bank Name','warning')", true);
            }
            else if (accno.Text =="")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Account No','warning')", true);
            }
            else if (ifsc.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter IFSC Code','warning')", true);
            }
            else if (acctypes.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Account Type','warning')", true);
            }
            //acctypes.Text = string.Empty;
            else if (panno.Text =="")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter PAN No','warning')", true);
            }
            else if (aadharno.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Adhaar No','warning')", true);
            }
            else if (aadharno.Text.Length <=11)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter a Valid Adhaar No','warning')", true);
            }
            else if (branchadd.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Branch Address','warning')", true);
            }
            else if (mngrname.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Reporting manager name','warning')", true);
            }
            else if (assetcount.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Assets Count','warning')", true);
            }
            else if (dtofissue.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Date of issue','warning')", true);
            }

            else if (RadioButton1.Checked == false && RadioButton2.Checked == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Please Select Gender','warning')", true);
            }

            else if (RadioButton3.Checked ==false && RadioButton4.Checked == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Marital Status','warning')", true);
            }

            else if (RadioButton5.Checked ==false && RadioButton6.Checked ==false && RadioButton7.Checked == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Qualification','warning')", true);
            }

            else if (CheckBoxList1.SelectedValue == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Asset Details','warning')", true);
            }
        }

        protected void Validations_Edit()
        {
            string email = empmail1.Text;
            bool isValid = IsValidEmail(email);

            if (empname1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Employee Name','warning')", true);
            }

            //if(empid.Text ==  )
            else if (empbloodgrp1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal()();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Blood Group','warning')", true);
            }
            else if (empdob1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Select Date of Birth','warning')", true);
            }
            else if (empjoindt1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Select Joining Date','warning')", true);
            }
            else if (empcon1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Contact No','warning')", true);
            }
            else if (empcon1.Text.Length <=9)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter a Valid Contact No','warning')", true);
            }
            else if (empmail1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Email','warning')", true);
            }
            else if (isValid == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please a Enter Appropriate Email','warning')", true);
            }
            else if (emppwd1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Password','warning')", true);
            }

            else if (deptdropdown_1.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Select Department','warning')", true);
            }
            else if (empdesigntion1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                         (this.GetType(), "K", "swal('Warning!','Please Enter Designation','warning')", true);
            }

            else if (empcrntadd1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Current Address','warning')", true);
            }
            else if (permntadd1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Permanent Address','warning')", true);
            }
            else if (empdegree1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Degree','warning')", true);
            }

            else if (ttlexp1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Total Experience','warning')", true);
            }

            else if (nameasperbank1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Name as per Bank','warning')", true);
            }
            else if (Bankname1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Bank Name','warning')", true);
            }
            else if (accno1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Account No','warning')", true);
            }
            else if (ifsc1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter IFSC Code','warning')", true);
            }
            else if (acctypes1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Account Type','warning')", true);
            }
            //acctypes.Text = string.Empty;
            else if (panno1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter PAN No','warning')", true);
            }
            else if (aadharno1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Adhaar No','warning')", true);
            }
            else if (aadharno1.Text.Length <=11)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter a Valid Adhaar No','warning')", true);
            }
            else if (branchadd1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Branch Address','warning')", true);
            }
            else if (mngrname1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Reporting manager name','warning')", true);
            }
            else if (assetcount1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Enter Assets Count','warning')", true);
            }
            else if (dtofissue1.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Date of issue','warning')", true);
            }

            else if (RadioButton8.Checked == false && RadioButton9.Checked == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Please Select Gender','warning')", true);
            }

            else if (RadioButton10.Checked == false && RadioButton11.Checked == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Marital Status','warning')", true);
            }

            else if (RadioButton12.Checked == false && RadioButton13.Checked == false && RadioButton14.Checked == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Qualification','warning')", true);
            }

            else if (CheckBoxList2.SelectedValue == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Warning!','Please Select Asset Details','warning')", true);
            }
        }

        protected void Make_empty()
        {
            empbloodgrp.Text = string.Empty;
            empid.Text = string.Empty;
            empname.Text = string.Empty;
            empdob.Text = string.Empty;
            empjoindt.Text = string.Empty;
            empcon.Text = string.Empty;
            empmail.Text = string.Empty;
            emppwd.Text = string.Empty;

            this.deptdropdown.ClearSelection();
            empdesigntion.Text = string.Empty;
            empaltercon.Text = string.Empty;
            empcrntadd.Text = string.Empty;
            permntadd.Text = string.Empty;
            empdegree.Text = string.Empty;
            empcertificate.Text = string.Empty;
            ttlexp.Text = string.Empty;
            relevexp.Text = string.Empty;
            nameasperbank.Text = string.Empty;
            Bankname.Text = string.Empty;
            accno.Text = string.Empty;
            ifsc.Text = string.Empty;
            this.acctypes.ClearSelection();
            //acctypes.Text = string.Empty;
            panno.Text = string.Empty;
            aadharno.Text = string.Empty;
            branchadd.Text = string.Empty;
            mngrname.Text = string.Empty;
            assetcount.Text = string.Empty;
            dtofissue.Text = string.Empty;
            dtofsurrend.Text = string.Empty;
            remarks.Text = string.Empty;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;
            RadioButton4.Checked = false;
            RadioButton5.Checked = false;
            RadioButton6.Checked = false;
            RadioButton7.Checked = false;
            CheckBoxList1.ClearSelection();
        }
        //RETRIVE EMPLOYEE DETAILS
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            int emp_id = Convert.ToInt32(EmployeeTable.DataKeys[gvrow.RowIndex].Value.ToString());

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT EM.Emp_MasterID, EM.Emp_ID, EM.Emp_Name, EM.Emp_DOB, EM.Emp_JoinDate, EM.Emp_Contact,EM.Emp_Email,\r\nEM.Emp_Designation,EM.Alternative_contactNO,EM.bloodgroup,EM.CurrentAddress,EM.PermanentAddress,EM.Degree,EM.Dept_FKID,EM.Emp_Gender,\r\nEM.Emp_Password,EM.Additional_Certificates,EM.Qualification,EM.Marital_Staus,EM.Total_Experience,EM.Relevant_experience,\r\nEA.Account_No,EA.Account_Type,EA.Adhaar_No,EA.Bank_Name,EA.Branch_Address,EA.Emp_Name,EA.IFSC_Code,EA.PAN_No,EAS.Asset_Details,\r\nEAS.Assets_Count,EAS.Date_of_Issue,EAS.Date_of_Surrender,EAS.Remarks,EAS.Report_Mngr_Name\r\nfrom EmployerMaster EM,EmployerAccount EA, EmployerAssets EAS \r\nwhere EM.Emp_MasterID=EA.Emp_ID AND EM.Emp_MasterID = EAS.Emp_ID AND EM.Emp_MasterID ='" + emp_id + "' order by EM.Emp_JoinDate DESC;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            con.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                sdr.Read();
                Emp_Masterid.Text = sdr["Emp_MasterID"].ToString();
                empname1.Text = sdr["Emp_Name"].ToString();
                empid1.Text = sdr["Emp_ID"].ToString();
                empbloodgrp1.Text = sdr["bloodgroup"].ToString();
                empdob1.Text = Convert.ToDateTime(sdr["Emp_DOB"]).ToString("yyyy-MM-dd");
                empjoindt1.Text = Convert.ToDateTime(sdr["Emp_JoinDate"]).ToString("yyyy-MM-dd");
                empcon1.Text = sdr["Emp_Contact"].ToString();
                empmail1.Text = sdr["Emp_Email"].ToString();
                emppwd1.Text = sdr["Emp_Password"].ToString();
                deptdropdown_1.Text = sdr["Dept_FKID"].ToString();
                
                empdesigntion1.Text = sdr["Emp_Designation"].ToString();
                empaltercon1.Text = sdr["Alternative_contactNO"].ToString();
                empcrntadd1.Text = sdr["CurrentAddress"].ToString();
                permntadd1.Text = sdr["PermanentAddress"].ToString();
                empdegree1.Text = sdr["Degree"].ToString();
                empcertificate1.Text = sdr["Additional_Certificates"].ToString();
                ttlexp1.Text = sdr["Total_Experience"].ToString();
                relevexp1.Text = sdr["Relevant_experience"].ToString();
                nameasperbank1.Text = sdr["Emp_Name"].ToString();
                Bankname1.Text = sdr["Bank_Name"].ToString();
                accno1.Text = sdr["Account_No"].ToString();
                ifsc1.Text = sdr["IFSC_Code"].ToString();
                acctypes1.Text = sdr["Account_Type"].ToString();
                panno1.Text = sdr["PAN_No"].ToString();
                aadharno1.Text = sdr["Adhaar_No"].ToString();
                branchadd1.Text = sdr["Branch_Address"].ToString();
                assetcount1.Text = sdr["Assets_Count"].ToString();
                mngrname1.Text = sdr["Report_Mngr_Name"].ToString();
                dtofissue1.Text = Convert.ToDateTime(sdr["Date_of_Issue"]).ToString("yyyy-MM-dd");
                //dtofsurrend1.Text = Convert.ToDateTime(sdr["Date_of_Surrender"]).ToString("yyyy-MM-dd");
                remarks1.Text = sdr["Remarks"].ToString();

                
                if (Convert.ToDateTime(sdr["Date_of_Surrender"]).ToString("yyyy-MM-dd") == "1900-01-01")
                {
                    dtofsurrend1.Text = "Date of Surrender";
                }
                else
                {
                    dtofsurrend1.Text = Convert.ToDateTime(sdr["Date_of_Surrender"]).ToString("yyyy-MM-dd");
                }

                if (sdr["Emp_Gender"].ToString() == "Male")
                {
                    RadioButton8.Checked = true;
                }else if(sdr["Emp_Gender"].ToString() == "Female")
                {
                    RadioButton9.Checked = true;
                }

                if(sdr["Marital_Staus"].ToString() == "Married")
                {
                    RadioButton10.Checked = true;
                }else if (sdr["Marital_Staus"].ToString() == "Unmarried")
                {
                    RadioButton11.Checked = true; 
                }

                if(sdr["Qualification"].ToString() == "UG")
                {
                    RadioButton12.Checked = true;
                }else if (sdr["Qualification"].ToString() == "PG")
                {
                    RadioButton13.Checked = true;
                }else if (sdr["Qualification"].ToString() == "Others")
                {
                    RadioButton14.Checked = true;
                }

                var s = sdr["Asset_Details"].ToString().Split(',');
                    int length = s.Length;
                for (int i = 0; i <= s.Length - 1; i++)
                {
                    string assets = s[i];
                    for (int j = 0; j <= CheckBoxList2.Items.Count - 1; j++)
                    {
                        if (CheckBoxList2.Items[j].Text == s[i])
                        {
                            CheckBoxList2.Items[j].Selected = true;
                            break;
                        }
                    }
                }
            }
            con.Close();
        }

        protected void EditEmp(object sender, EventArgs e)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            int emp_masterid = Convert.ToInt32(Emp_Masterid.Text.ToString());
            string gen = "";
            string status = "";
            string qualifications = "";
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string email = empmail1.Text;
                bool isValid = IsValidEmail(email);
                if (empbloodgrp1.Text == "" || empid1.Text == "" || empname1.Text == "" || empdob1.Text == "" || empjoindt1.Text == string.Empty || empcon1.Text == string.Empty || empcon1.Text.Length <= 9 || empmail1.Text == string.Empty || isValid == false || emppwd1.Text == "" || deptdropdown_1.SelectedValue == "0" || empdesigntion1.Text == "" || empcrntadd1.Text == "" || permntadd1.Text == "" || empdegree1.Text == "" || ttlexp1.Text == "" || nameasperbank1.Text == "" || Bankname1.Text == "" || accno1.Text == "" || ifsc1.Text == "" || acctypes1.Text == "" || panno1.Text == "" || aadharno1.Text == "" || aadharno1.Text.Length <= 11 || branchadd1.Text == "" || mngrname1.Text == "" || assetcount1.Text == "" || dtofissue1.Text == "" || (RadioButton8.Checked == false && RadioButton9.Checked == false) || (RadioButton10.Checked == false && RadioButton11.Checked == false) || (RadioButton12.Checked == false && RadioButton13.Checked == false && RadioButton14.Checked == false) || CheckBoxList2.SelectedValue == "")
                {
                    Validations_Edit();
                }
                else
                {


                    //for gender
                    if (RadioButton8.Checked)
                    {
                        gen = "Male";
                    }
                    else if (RadioButton9.Checked)
                    {
                        gen = "Female";
                    }

                    //for marital status
                    if (RadioButton10.Checked)
                    {
                        status = "Married";
                    }
                    else if (RadioButton11.Checked)
                    {
                        status = "Unmarried";
                    }

                    //for qualification
                    if (RadioButton12.Checked)
                    {
                        qualifications = "UG";
                    }
                    else if (RadioButton13.Checked)
                    {
                        qualifications = "PG";
                    }
                    else if (RadioButton14.Checked)
                    {
                        qualifications = "Others";
                    }


                    DateTime now = DateTime.Now;
                    //inserting in EmployerMaster Table
                    string updateSQL = "UPDATE EmployerMaster set Emp_ID = @Emp_ID, Emp_Name = @Emp_Name, Emp_DOB = @Emp_DOB, Emp_JoinDate = @Emp_JoinDate, Emp_Contact =@Emp_Contact, Emp_Email = @Emp_Email,Emp_Password = @Emp_Password, Emp_Designation = @Emp_Designation, Dept_FKID = @Dept_FKID, Alternative_contactNO = @Alternative_contactNO, CurrentAddress = @CurrentAddress," +
                        " PermanentAddress =@PermanentAddress , bloodgroup =@bloodgroup ,Emp_Gender =@Emp_Gender,Marital_Staus =@Marital_Staus ,Qualification =@Qualification ,Degree =@Degree ," +
                        "Additional_Certificates =@Additional_Certificates ,Total_Experience=@Total_Experience ,Relevant_experience =@Relevant_experience ,Modified_Date = @Modified_Date WHERE Emp_MasterID = @Emp_MasterID ";
                    SqlCommand cmd = new SqlCommand(updateSQL, con);
                    cmd.Parameters.AddWithValue("@Emp_MasterID", emp_masterid);
                    cmd.Parameters.AddWithValue("@Emp_ID", empid1.Text);
                    cmd.Parameters.AddWithValue("@Emp_Name", empname1.Text);
                    cmd.Parameters.AddWithValue("@Emp_DOB", empdob1.Text);
                    cmd.Parameters.AddWithValue("@Emp_JoinDate", empjoindt1.Text);
                    cmd.Parameters.AddWithValue("@Emp_Contact", empcon1.Text);
                    cmd.Parameters.AddWithValue("@Emp_Email", empmail1.Text);
                    cmd.Parameters.AddWithValue("@Emp_Password", emppwd1.Text);
                    cmd.Parameters.AddWithValue("@Emp_Designation", empdesigntion1.Text);
                    cmd.Parameters.AddWithValue("@Dept_FKID", deptdropdown_1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Modified_Date", now);
                    cmd.Parameters.AddWithValue("@Alternative_contactNO", empaltercon1.Text);
                    cmd.Parameters.AddWithValue("@CurrentAddress", empcrntadd1.Text);
                    cmd.Parameters.AddWithValue("@PermanentAddress", permntadd1.Text);
                    cmd.Parameters.AddWithValue("@bloodgroup", empbloodgrp1.Text);
                    cmd.Parameters.AddWithValue("@Emp_Gender", gen);
                    cmd.Parameters.AddWithValue("@Marital_Staus", status);
                    cmd.Parameters.AddWithValue("@Qualification", qualifications);
                    cmd.Parameters.AddWithValue("@Degree", empdegree1.Text);
                    cmd.Parameters.AddWithValue("@Additional_Certificates", empcertificate1.Text);
                    cmd.Parameters.AddWithValue("@Total_Experience", ttlexp1.Text);
                    cmd.Parameters.AddWithValue("@Relevant_experience", relevexp1.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();

                    //cmd = new SqlCommand("SELECT MAX(Emp_MasterID) ID FROM EmployerMaster ", con);
                    //con.Open();
                    ////int emp_id = Convert.ToInt16(cmd.ExecuteScalar());
                    //con.Close();

                    //inserting in EmployerAccount Table
                    string insert_acc = "UPDATE EmployerAccount SET Emp_Name =@Emp_Name, Bank_Name=@Bank_Name , Account_No=@Account_No ," +
                        " IFSC_Code=@IFSC_Code , Account_Type=@Account_Type ,PAN_No=@PAN_No , Adhaar_No =@Adhaar_No, Branch_Address=@Branch_Address " +
                        " WHERE Emp_ID = @Emp_ID";
                    SqlCommand inscmd = new SqlCommand(insert_acc, con);

                    inscmd.Parameters.AddWithValue("@Emp_ID", emp_masterid);
                    inscmd.Parameters.AddWithValue("@Emp_Name", nameasperbank1.Text);
                    inscmd.Parameters.AddWithValue("@Bank_Name", Bankname1.Text);
                    inscmd.Parameters.AddWithValue("@Account_No", accno1.Text);
                    inscmd.Parameters.AddWithValue("@IFSC_Code", ifsc1.Text);
                    inscmd.Parameters.AddWithValue("@Account_Type", acctypes1.Text);
                    inscmd.Parameters.AddWithValue("@PAN_No", panno1.Text);
                    inscmd.Parameters.AddWithValue("@Adhaar_No", aadharno1.Text);
                    inscmd.Parameters.AddWithValue("@Branch_Address", branchadd1.Text);

                    con.Open();
                    inscmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();

                    String asset = "";
                    for (int i = 0; i <= CheckBoxList2.Items.Count - 1; i++)
                    {
                        if (CheckBoxList2.Items[i].Selected)
                        {
                            if (asset == "")
                            {
                                asset = CheckBoxList2.Items[i].Text;
                            }
                            else
                            {
                                asset += "," + CheckBoxList2.Items[i].Text;
                            }
                        }
                    }


                    //inserting in EmployerAssets Table
                    string update_asset = "UPDATE EmployerAssets SET Report_Mngr_Name =@Report_Mngr_Name , Assets_Count=@Assets_Count, Date_of_Issue=@Date_of_Issue, Date_of_Surrender=@Date_of_Surrender, Remarks=@Remarks,Asset_Details=@Asset_Details " +
                        "WHERE Emp_ID= @Emp_ID ";
                    SqlCommand asscmd = new SqlCommand(update_asset, con);
                    asscmd.Parameters.AddWithValue("@Emp_ID", emp_masterid);
                    asscmd.Parameters.AddWithValue("@Report_Mngr_Name", mngrname1.Text);
                    asscmd.Parameters.AddWithValue("@Assets_Count", assetcount1.Text);
                    asscmd.Parameters.AddWithValue("@Date_of_Issue", dtofissue1.Text);
                    asscmd.Parameters.AddWithValue("@Date_of_Surrender", dtofsurrend1.Text);
                    asscmd.Parameters.AddWithValue("@Remarks", remarks1.Text);
                    asscmd.Parameters.AddWithValue("@Asset_Details", asset);

                    con.Open();
                    int row3 = asscmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();


                    if (row3 > 0)
                    {
                        ClientScript.RegisterClientScriptBlock
                             (this.GetType(), "K", "swal('Success!','Employee Details Updated Successfully!','success')", true);
                        get_employees();
                        //Response.Redirect("Employee.aspx");
                        //ClientScript.RegisterStartupScript
                        //    (GetType(), Guid.NewGuid().ToString(), "success();", true);
                    }

                }
            }
            Make_empty();
        }

        protected void Excel_download(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT EM.Emp_MasterID, EM.Emp_ID, EM.Emp_Name, EM.Emp_DOB, EM.Emp_JoinDate, EM.Emp_Contact,EM.Emp_Email,\r\nEM.Emp_Designation,EM.Alternative_contactNO,EM.bloodgroup,EM.CurrentAddress,EM.PermanentAddress,EM.Degree,EM.Emp_Gender,\r\nEM.Emp_Password,EM.Additional_Certificates,EM.Qualification,EM.Marital_Staus,EM.Total_Experience,EM.Relevant_experience,\r\nEA.Account_No,EA.Account_Type,EA.Adhaar_No,EA.Bank_Name,EA.Branch_Address,EA.Emp_Name,EA.IFSC_Code,EA.PAN_No,EAS.Asset_Details,\r\nEAS.Assets_Count,EAS.Date_of_Issue,EAS.Date_of_Surrender,EAS.Remarks,EAS.Report_Mngr_Name\r\nfrom EmployerMaster EM,EmployerAccount EA, EmployerAssets EAS \r\nwhere EM.Emp_MasterID=EA.Emp_ID AND EM.Emp_MasterID = EAS.Emp_ID order by EM.Emp_JoinDate DESC"))
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
                                Response.AddHeader("content-disposition", "attachment;filename=Employee_List.xlsx");
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
    //partial end
    }
}
