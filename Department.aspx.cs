
using ClosedXML.Excel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.DynamicData;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;


namespace HRMS
{
    public partial class Department : System.Web.UI.Page
    {

        // string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                
                loadData();

                clearCreateDep();
                cleareditDep();
            }


        }

        // to load the database data into gridview
        protected void loadData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
            
                SqlDataAdapter da = new SqlDataAdapter("SELECT dm.Dept_ID,dm.Dept_Name,COALESCE(em.total_employees, 0) AS Dept_TotalEmployees, dm.Dept_POC, dm.IsActive, dm.Created_By, dm.Created_Date, dm.Modified_By, dm.Modified_Date FROM DepartmentMaster dm LEFT JOIN (SELECT Dept_FKID,COUNT(Emp_Name) AS total_employees FROM EmployerMaster GROUP BY Dept_FKID) em ON dm.Dept_ID = em.Dept_FKID;", con);
                da.Fill(dt);
                dept.DataSource = dt;
                dept.DataBind();
            }


           
            if (dept.FooterRow != null)
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
                dept.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        // to clear create department modal
        protected void clearCreateDep()
        {
            depName.Text = string.Empty;
            
            poc.Text = string.Empty;
            isActive.Text = string.Empty;
            createdBy.Text = string.Empty;

        }

        // to clear edit department modal
        protected void cleareditDep()
        {
            depName.Text = string.Empty;
            poc.Text = string.Empty;
            isActive.Text = string.Empty;
            createdBy.Text = string.Empty;

        }
        // to save data into database
        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (dept.FooterRow != null)
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
                dept.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            clearcreateError();
            try
            {

                if (depName.Text == string.Empty)
                {
                    dn.Text = "Please Enter Department Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;
                }

             /*   else if (System.Text.RegularExpressions.Regex.IsMatch(totEmp.Text, @"\D"))
                {
                    te.Text = "Please Enter Numbers only";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }

                else if (totEmp.Text == string.Empty)
                {
                    te.Text = "Please Enter Total Employees";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }*/

                else if (poc.Text == string.Empty)
                {
                    pc.Text = "Please Enter Department POC";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }

               /* else if (isActive.Text == string.Empty)
                {
                    ia.Text = "Please Enter Is Active";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }


                else if (System.Text.RegularExpressions.Regex.IsMatch(isActive.Text, @"\D") || !(int.Parse(isActive.Text) <= 1))
                {
                    ia.Text = "Please Enter 0 - Inactive 1-Active only";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;
                }

                else if (createdBy.Text == string.Empty)
                {
                    cb.Text = "Please Enter Modified By";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;
                }

                else if (!System.Text.RegularExpressions.Regex.IsMatch(createdBy.Text, @"\D"))
                {
                    cb.Text = "Please Enter only Alphabets";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;
                }*/
                else
                {
                    clearcreateError();
                }

                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                DateTime nowDateTime = DateTime.Now;
                string un = Session["username"].ToString();
                string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                SqlCommand cmd = new SqlCommand("Insert into DepartmentMaster (Dept_Name,Dept_POC,IsActive,Created_By,Created_Date) Values" +
                 " ('" + depName.Text + "','" + poc.Text + "','" + 0 + "','" + un + "','" + nowDateTime + "')", con);
                int i = cmd.ExecuteNonQuery();
                loadData();
                clearCreateDep();
                con.Close();
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Created','Department Added Successfully!','success')", true);
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Creating!','error')", true);
            }

        }

        // to delete data into database
        protected void dept_RowDeleting(object sender, GridViewDeleteEventArgs e)
        
        {
            if (dept.FooterRow != null)
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
                dept.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {

            
            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from DepartmentMaster where Dept_ID=@id ", con);
            cmd.Parameters.AddWithValue("@id", dept.DataKeys[e.RowIndex].Value.ToString());


            int r = cmd.ExecuteNonQuery();
            loadData();
            con.Close();
             if (r > 0)

               {
                
                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Deleted!','Department has been deleted!','success')", true);
                
               }
            }
            catch(Exception ex)
            {
               
                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Foreign Key Constraint Violation','Cannot Delete Record.Associated Data In Use.','error')", true); 
            }
                        
        }
        // to download data into excel
        protected void btnDownload_Click(object sender, EventArgs e)
        {

            DataTable dtexcel = new DataTable();
           // GridView gv = new GridView();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                
                SqlDataAdapter da = new SqlDataAdapter("Select Dept_ID as [Department ID], Dept_Name as [Department Name],Dept_TotalEmployees as [Total Employees],Dept_POC as [POC],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date] from DepartmentMaster", con);
                da.Fill(dtexcel);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtexcel, "Department");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Department Details.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }
           /* gv.DataSource = dtexcel;
            gv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename=Department Details.xls"));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);


            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();*/
         
        }
        public override void VerifyRenderingInServerForm(Control control)
        {


        }

        // to update data into database
        private void cleareditError()
        {
            edn.Text = "";
            
            epc.Text = "";
            eia.Text = "";
            emb.Text = "";

        }
        private void clearcreateError()
        {
            dn.Text = "";
            pc.Text = "";
            ia.Text = "";
            cb.Text = "";

        }
        protected void update_Click(object sender, EventArgs e)
        {
            cleareditError();
            if (dept.FooterRow != null)
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
                dept.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {
                if (edepName.Text == string.Empty)
                {
                    edn.Text = "Please Enter Department Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;
                }

               
                else if (ePOC.Text == string.Empty)
                {
                    epc.Text = "Please Enter Department POC";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;

                }

              /*  else if (eisActive.Text == string.Empty)
                {
                    eia.Text = "Please Enter Is Active";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;

                }


                else if (System.Text.RegularExpressions.Regex.IsMatch(eisActive.Text, @"\D") || !(int.Parse(eisActive.Text) <= 1))
                {
                    eia.Text = "Please Enter 0 - Inactive 1-Active only";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;
                }

                else if (emodifiedBy.Text == string.Empty)
                {
                    emb.Text = "Please Enter Modified By";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;
                }

                else if (!System.Text.RegularExpressions.Regex.IsMatch(emodifiedBy.Text, @"\D"))
                {
                    emb.Text = "Please Enter only Alphabets";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;
                }*/
                else
                {
                    cleareditError();
                }

                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                DateTime nowDateTime = DateTime.Now;
                string un = Session["username"].ToString();
                SqlCommand cmd = new SqlCommand("Update DepartmentMaster Set Dept_Name=@depname,Dept_POC=@poc,IsActive=@isActive,Modified_By=@modifiedby,Modified_Date=@modifieddate where Dept_ID=@id ", con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dept.DataKeys[dept.EditIndex].Value));
                cmd.Parameters.AddWithValue("@depname", edepName.Text);
                cmd.Parameters.AddWithValue("@poc", ePOC.Text);
                cmd.Parameters.AddWithValue("@isActive",1);
                cmd.Parameters.AddWithValue("@modifiedby", un);
                cmd.Parameters.AddWithValue("@modifieddate", nowDateTime);
                int r = cmd.ExecuteNonQuery();
                loadData();
                cleareditDep();
                con.Close();
                if (r > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Updated!','Department has been Updated !','success')", true);
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Updating!','error')", true);
            }

        }



        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {


            string id = e.CommandArgument.ToString();

            SqlConnection con1 = new SqlConnection(connectionString);


            con1.Open();


            SqlCommand cmd1 = new SqlCommand("Select * from DepartmentMaster where Dept_ID=@id", con1);
            cmd1.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                edepName.Text = dr["Dept_Name"].ToString();
                etotEmp.Text = dr["Dept_TotalEmployees"].ToString();
                ePOC.Text = dr["Dept_POC"].ToString();
                eisActive.Text = dr["IsActive"].ToString();
                emodifiedBy.Text = dr["Modified_By"].ToString();

            }
            dr.Close();


        }

        protected void dept_RowEditing(object sender, GridViewEditEventArgs e)
        {

            string script = "$('#editmodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupedit", script, true);
            if (dept.FooterRow != null)
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
                dept.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dept.UseAccessibleHeader = true;
                dept.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
       
       
       
    }
}
