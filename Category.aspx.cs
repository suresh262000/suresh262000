using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS_
{
    public partial class Category : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCategory();
            }

        }
        protected void loadCategory()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Select * From Category", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                categorygrid.DataSource = ds;
                categorygrid.DataBind();
            }
            if (categorygrid.FooterRow != null)
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                categorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void clearCreateDep()
        {
            catname.Text = string.Empty;

            catdesc.Text = string.Empty;
           

        }

        


        protected void cleareditDep()
        {
            ecategoryname.Text = string.Empty;
            ecategorydesc.Text = string.Empty;
            

        }
        // to save data into database
        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (categorygrid.FooterRow != null)
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                categorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            clearcreateError();
            try
            {

                if (categoryname.Text == string.Empty)
                {
                    catname.Text = "Please Enter Category Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;
                }

               

                else if (categorydesc.Text == string.Empty)
                {
                    catdesc.Text = "Please Enter Category Description";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }

                
                else
                {
                    clearcreateError();
                }

                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                DateTime nowDateTime = DateTime.Now;
                string un = Session["username"].ToString();
                string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                SqlCommand cmd = new SqlCommand("Insert into Category (Category_Name,Category_Desc,IsActive,Created_By,Created_Date) Values" +
                 " ('" + categoryname.Text + "','" + categorydesc.Text + "','" + 1 + "','" + un + "','" + nowDateTime + "')", con);
                int i = cmd.ExecuteNonQuery();
                loadCategory();
                clearCreateDep();
                con.Close();
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Created','Category Added Successfully!','success')", true);
                }
                clearCreateDep();
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Creating!','error')", true);
            }

        }

        // to delete data into database
        protected void categorygrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (categorygrid.FooterRow != null)
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                categorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {


                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from Category where Category_ID=@catid ", con);
                cmd.Parameters.AddWithValue("@catid", categorygrid.DataKeys[e.RowIndex].Value.ToString());


                int r = cmd.ExecuteNonQuery();
                loadCategory();
                con.Close();
                if (r > 0)

                {

                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Deleted!','Category has been deleted!','success')", true);

                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Foreign Key Constraint Violation','Cannot Delete Record.Associated Data In Use.','error')", true);
            }

        }


        protected void downloadcategory_Click(object sender, EventArgs e)
        {
            DataTable dtexcel = new DataTable();
            // GridView gv = new GridView();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select Category_ID as [Category ID], Category_Name as [Category Name],Category_Desc as [Category Description],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date] from Category", con);
                da.Fill(dtexcel);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtexcel, "Category");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Category Details.xlsx");
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

       
        private void cleareditError()
        {
            ecatname.Text ="";
            ecatdesc.Text ="";
                                 

        }
        private void clearcreateError()
        {
           catname.Text ="";
            catdesc.Text = "";
        }





        protected void cEdit_Command(object sender, CommandEventArgs e)
        {


            string cid = e.CommandArgument.ToString();

            SqlConnection con1 = new SqlConnection(connectionString);


            con1.Open();


            SqlCommand cmd1 = new SqlCommand("Select * from Category where Category_ID=@cid", con1);
            cmd1.Parameters.AddWithValue("@cid", cid);
            SqlDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
               
                ecategoryname.Text = dr["Category_Name"].ToString();
                ecategorydesc.Text = dr["Category_Desc"].ToString();
                

            }
            dr.Close();


        }

        


        

        protected void categorygrid_RowEditing(object sender, GridViewEditEventArgs e)
        {

            string script = "$('#editcategorymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupedit", script, true);
            if (categorygrid.FooterRow != null)
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                categorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            
        }

        protected void update_Click1(object sender, EventArgs e)
        {

           
            if (categorygrid.FooterRow != null)
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                categorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {
                if (ecategoryname.Text == string.Empty)
                {
                    ecatname.Text = "Please Enter Category Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;
                }

                else if (ecategorydesc.Text == string.Empty)
                {
                    ecatname.Text=string.Empty; 
                    ecatdesc.Text = "Please Enter Category Description";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;

                }


                else
                {
                    cleareditError();
                                       
                }
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    DateTime nowDateTime = DateTime.Now;
                    string un = Session["username"].ToString();
                    SqlCommand cmd = new SqlCommand("Update Category Set Category_Name=@catname,Category_Desc=@catdesc,IsActive=@catisActive,Modified_By=@catmodifiedby,Modified_Date=@catmodifieddate where Category_ID=@id ", con);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(categorygrid.DataKeys[categorygrid.EditIndex].Value));
                    cmd.Parameters.AddWithValue("@catname", ecategoryname.Text);
                    cmd.Parameters.AddWithValue("@catdesc", ecategorydesc.Text);
                    cmd.Parameters.AddWithValue("@catisActive", 1);
                    cmd.Parameters.AddWithValue("@catmodifiedby", un);
                    cmd.Parameters.AddWithValue("@catmodifieddate", nowDateTime);
                    int r = cmd.ExecuteNonQuery();
                  

                
                loadCategory();
                    cleareditDep();
                    con.Close();
                    if (r > 0)
                    {
                        ClientScript.RegisterClientScriptBlock
                        (this.GetType(), "K", "swal('Updated!','Category has been Updated !','success')", true);
                    }
                
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Updating!','error')", true);
            }
            

        }

        protected void addcategory_Click(object sender, EventArgs e)
        {
            string script = "$('#createcategorymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupedit", script, true);
            if (categorygrid.FooterRow != null)
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                categorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                categorygrid.UseAccessibleHeader = true;
                categorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        

        
    }
}