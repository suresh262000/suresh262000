using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;

namespace HRMS_
{
    public partial class SubCategory : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadsubCategory();
                clearCreateSubcategory();
                cleareditSubcategory();

            }


        }
        protected void loadsubCategory()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select BaseCategory_ID,Basecategory_Name,SubCategory_ID,Subcategory_Name,SubCategory_desc from SubCategory", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                subcategorygrid.DataSource = ds;
                subcategorygrid.DataBind();

            }
        }
        protected void clearCreateSubcategory()
        {
            scatname.Text = "";

            scatdesc.Text = "";


        }

        
        protected void cleareditSubcategory()
        {
            escatname.Text = "";
            escatdesc.Text = "";


        }
        protected void cleareditError()
        {
            escatname.Text = "";
            escatdesc.Text = "";
        }
        protected void clearcreateError()
        {
            scatname.Text = "";
            scatdesc.Text = "";
        }

        protected void addsubcategory_Click(object sender, EventArgs e)
        {
            string script = "$('#createsubcategorymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
            clearCreateSubcategory();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Category", con);
                basecategoryname.DataSource = cmd.ExecuteReader();
                basecategoryname.DataBind();
                con.Close();

            }
            clearCreateSubcategory();
            clearcreateError();
        }

        protected void savesubcategory_Click(object sender, EventArgs e)
        {
            string uname = Session["username"].ToString();
            DateTime dt = DateTime.Now;

            try
            {
                if (subcategoryname.Text == string.Empty)
                {
                    scatname.Text = "Please Enter Sub Category Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;
                }

                else if (subcategorydesc.Text == string.Empty)
                {

                    scatdesc.Text = "Please Enter Sub Category Description";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }


                else
                {
                    cleareditError();

                }


                    SqlConnection con = new SqlConnection(connectionString);
                
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Insert into SubCategory (BaseCategory_Name,BaseCategory_ID,SubCategory_Name,SubCategory_Desc,IsActive,Created_By,Created_Date) Values" +
                     " ('" + basecategoryname.SelectedItem + "','" + basecategoryname.SelectedValue + "','" + subcategoryname.Text + "','" + subcategorydesc.Text + "','" + 1 + "','" + uname + "','" + dt + "')", con);
                    int i = cmd.ExecuteNonQuery();
                    loadsubCategory();

                    con.Close();
                    if (i > 0)
                    {
                        ClientScript.RegisterClientScriptBlock
                       (this.GetType(), "K", "swal('Created','Sub Category Added Successfully!','success')", true);
                    }

                    else
                    {

                        ClientScript.RegisterClientScriptBlock
                           (this.GetType(), "K", "swal('Error','Error Occured While Creating!','error')", true);
                    }
                clearCreateSubcategory();
            }

            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Updating!','error')", true);
            }
            clearCreateSubcategory();
        } 

        protected void subcategorygrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (subcategorygrid.FooterRow != null)
            {
                subcategorygrid.UseAccessibleHeader = true;
                subcategorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                //subcategorydesc.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                subcategorygrid.UseAccessibleHeader = true;
                subcategorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {


                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from SubCategory where SubCategory_ID=@scatid ", con);
                cmd.Parameters.AddWithValue("@scatid", subcategorygrid.DataKeys[e.RowIndex].Value.ToString());


                int r = cmd.ExecuteNonQuery();
                loadsubCategory();
                con.Close();
                if (r > 0)

                {

                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Deleted!','SubCategory has been deleted!','success')", true);

                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Foreign Key Constraint Violation','Cannot Delete Record.Associated Data In Use.','error')", true);
            }
        }

        protected void subcategoryedit_Command(object sender, CommandEventArgs e)
        {
            string cid = e.CommandArgument.ToString();

            SqlConnection con1 = new SqlConnection(connectionString);


            con1.Open();


            SqlCommand cmd1 = new SqlCommand("Select * from SubCategory where SubCategory_ID=@cid", con1);
            cmd1.Parameters.AddWithValue("@cid", cid);
            SqlDataReader dr = cmd1.ExecuteReader();
            
            while (dr.Read())
            {
                string selectedCategoryName =  dr["BaseCategory_ID"].ToString();
                ebcategoryname.Text=selectedCategoryName;
                // Set the selected value of the dropdown list to the selected category name.
                

                // string val = dr["BaseCategory_Name"].ToString();
                //ebcategoryname.SelectedItem.Text = "Beverages";
                escategoryname.Text = dr["SubCategory_Name"].ToString();
                escategorydesc.Text = dr["SubCategory_Desc"].ToString();


            }
            
            
        }

        protected void subupdate_Click(object sender, EventArgs e)
        {
            cleareditError();
            if (subcategorygrid.FooterRow != null)
            {
                subcategorygrid.UseAccessibleHeader = true;
                subcategorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                subcategorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                subcategorygrid.UseAccessibleHeader = true;
                subcategorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {
                if (escategoryname.Text == string.Empty)
                {
                    escatname.Text = "Please Enter Sub Category Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    return;
                }


                else if (escategorydesc.Text == string.Empty)
                {
                    escatdesc.Text = "Please Enter Category Description";
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
                string bcatnameddl = ebcategoryname.SelectedItem.ToString();
                SqlCommand cmd = new SqlCommand("Update SubCategory Set BaseCategory_Name=@bcatname,BaseCategory_ID=@bcatid,SubCategory_Name=@scatname,SubCategory_Desc=@scatdesc,IsActive=@scatisActive,Modified_By=@scatmodifiedby,Modified_Date=@scatmodifieddate where SubCategory_ID=@sid ", con);
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(subcategorygrid.DataKeys[subcategorygrid.EditIndex].Value));
                cmd.Parameters.AddWithValue("@bcatname", bcatnameddl);
                cmd.Parameters.AddWithValue("@bcatid", ebcategoryname.SelectedValue);
                cmd.Parameters.AddWithValue("@scatname", escategoryname.Text);
                cmd.Parameters.AddWithValue("@scatdesc", escategorydesc.Text);
                cmd.Parameters.AddWithValue("@scatisActive", 1);
                cmd.Parameters.AddWithValue("@scatmodifiedby", un);
                cmd.Parameters.AddWithValue("@scatmodifieddate", nowDateTime);
                int r = cmd.ExecuteNonQuery();

                loadsubCategory();
                //cleareditDep();
                con.Close();
                if (r > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Updated!','Sub Category has been Updated !','success')", true);
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Updating!','error')", true);
            }
        }
        

        protected void subcategorygrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string script = "$('#editsubcategorymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupe", script, true);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Category", con);

                ebcategoryname.DataSource = cmd.ExecuteReader();
                ebcategoryname.DataBind();
                con.Close();
            }
            cleareditError();
            
            if (subcategorygrid.FooterRow != null)
            {
                subcategorygrid.UseAccessibleHeader = true;
                subcategorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                subcategorygrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                subcategorygrid.UseAccessibleHeader = true;
                subcategorygrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void downloadsubcategory_Click(object sender, EventArgs e)
        {
            DataTable dtexcel = new DataTable();
            // GridView gv = new GridView();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select BaseCategory_Name as [Category Name] ,SubCategory_ID as [Sub Category ID], SubCategory_Name as [Sub Category Name],SubCategory_Desc as [Sub Category Description],IsActive as [Is Active],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date] from SubCategory", con);
                da.Fill(dtexcel);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtexcel, "Sub Category");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Sub Category Details.xlsx");
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
    






        

        
    


