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

namespace HRMS_
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                get_products();
                Catedrpdown();
                Subcatedrpdown();
                
                Catedrpdown_1();
                //Subcatedrpdown_1();
            }
                
        }

        

        protected void get_products()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT PR.Product_ID, PR.Product_Name, PR.Net_Qty, PR.Net_Rate, SC.SubCategory_Name, CT.Category_Name from Category CT, SubCategory SC, Tbl_Products PR\r\nwhere PR.Category_ID = CT.Category_ID and sc.SubCategory_ID = PR.SubCategory_ID and PR.IsActive = 1 order by PR.Created_dt  DESC";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            ProductTable.DataSource = ds;
            ProductTable.DataBind();
            if (ProductTable.FooterRow != null)
            {
                ProductTable.UseAccessibleHeader = true;
                ProductTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                ProductTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                ProductTable.UseAccessibleHeader = true;
                ProductTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Catedrpdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT Category_ID, Category_Name from Category";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            categorydrpdwn.DataSource = ds;
            categorydrpdwn.DataTextField = "Category_Name";
            categorydrpdwn.DataValueField = "Category_ID";
            categorydrpdwn.DataBind();
            categorydrpdwn.Items.Insert(0, new ListItem("Select Category", "0"));
        }

        protected void Catedrpdown_1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT Category_ID, Category_Name from Category";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            categorydrpdwn1.DataSource = ds;
            categorydrpdwn1.DataTextField = "Category_Name";
            categorydrpdwn1.DataValueField = "Category_ID";
            categorydrpdwn1.DataBind();
            categorydrpdwn1.Items.Insert(0, new ListItem("Select Category", "0"));
        }

        protected void Subcatedrpdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT SubCategory_ID, SubCategory_Name from SubCategory where BaseCategory_ID ='"+categorydrpdwn.SelectedValue+"'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            subcatdrpdwn.DataSource = ds;
            subcatdrpdwn.DataTextField = "SubCategory_Name";
            subcatdrpdwn.DataValueField = "SubCategory_ID";
            subcatdrpdwn.DataBind();
            subcatdrpdwn.Items.Insert(0, new ListItem("Select Sub Category", "0"));
        }

        protected void Subcatedrpdown_1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT SubCategory_ID, SubCategory_Name from SubCategory where BaseCategory_ID ='"+ categorydrpdwn1.SelectedValue+"' ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            subcatdrpdwn1.DataSource = ds;
            subcatdrpdwn1.DataTextField = "SubCategory_Name";
            subcatdrpdwn1.DataValueField = "SubCategory_ID";
            subcatdrpdwn1.DataBind();
            subcatdrpdwn1.Items.Insert(0, new ListItem("Select Sub Category", "0"));
        }

        private void clearcreateError()
        {
            PN.Text = "";
            CT.Text = "";
            SCT.Text = "";
            //QTY.Text = "";
            RT.Text = "";

        }

        private void clearcreateError1()
        {
            PN1.Text = "";
            CT1.Text = "";
            SCT1.Text = "";
            //QTY1.Text = "";
            RT1.Text = "";

        }
        protected void clearCreateProd()
        {
            prodNametxt.Text = string.Empty;
            this.categorydrpdwn.ClearSelection();
            this.subcatdrpdwn.ClearSelection();
            
            //Qtytxt.Text = string.Empty;
            Ratetxt.Text = string.Empty;

        }
        protected void clearCreateProd1()
        {
            prodNametxt1.Text = string.Empty;
            this.categorydrpdwn1.ClearSelection();
            this.subcatdrpdwn1.ClearSelection();

            //Qtytxt1.Text = string.Empty;
            Ratetxt1.Text = string.Empty;

        }

        /* Save Products */
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ProductTable.FooterRow != null)
            {
                ProductTable.UseAccessibleHeader = true;
                ProductTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                ProductTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                ProductTable.UseAccessibleHeader = true;
                ProductTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            clearcreateError();
            try
            {

                if (prodNametxt.Text == string.Empty)
                {
                    PN.Text = "Please Enter Product Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                    return;
                }

                else if (categorydrpdwn.SelectedValue == "0")
                {
                    CT.Text = "Please Select Category";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                    return;

                }

                else if (subcatdrpdwn.SelectedValue == "0")
                {
                     SCT.Text = "Please Select Sub Category";
                     ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                     return;

                }

                //else if (Qtytxt.Text == string.Empty)
                //{
                //    QTY.Text = "Please Enter Quantity";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                //    return;
                //}

                else if (Ratetxt.Text == string.Empty)
                {
                     RT.Text = "Please Enter Rate";
                     ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
                     return;
                }
                else
                {
                    clearcreateError();
                }

                string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                DateTime nowDateTime = DateTime.Now;
                string un = Session["username"].ToString();
                string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                SqlCommand cmd = new SqlCommand("Insert into Tbl_Products (Product_Name,Category_ID,SubCategory_ID,Net_Rate,Created_dt,IsActive,Created_by) " +
                    "Values(@Product_Name,@Category_ID,@SubCategory_ID,@Net_Rate,@Created_dt,1,@Created_by)", con);
                
                cmd.Parameters.AddWithValue("@Product_Name", prodNametxt.Text);
                cmd.Parameters.AddWithValue("@Category_ID", categorydrpdwn.SelectedValue);
                cmd.Parameters.AddWithValue("@SubCategory_ID", subcatdrpdwn.SelectedValue);
                //cmd.Parameters.AddWithValue("@Net_Qty", Qtytxt.Text);
                cmd.Parameters.AddWithValue("@Net_Rate", Ratetxt.Text);
                cmd.Parameters.AddWithValue("@Created_dt", nowDateTime);
                cmd.Parameters.AddWithValue("@Created_by", un);

                int i = cmd.ExecuteNonQuery();
                get_products();
                clearCreateProd();
                con.Close();
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Created','Product Added Successfully!','success')", true);
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Creating!','error')", true);
            }
        }

        protected void ProductTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var prod_id = (int)Convert.ToInt64(ProductTable.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                try
                {
                    string deleteSQL3 = "update Tbl_Products set IsActive = 0 where Product_ID ='" + prod_id + "'";
                    SqlCommand cmd3 = new SqlCommand(deleteSQL3, con);
                    con.Open();
                    int row = cmd3.ExecuteNonQuery();
                    con.Close();

                    if (row > 0)
                    {
                        ClientScript.RegisterClientScriptBlock
                            (this.GetType(), "K", "swal('Deleted!','Product has been deleted!','success')", true);
                        get_products();

                    }
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
                catch (Exception ex)
                {

                    ClientScript.RegisterClientScriptBlock
                       (this.GetType(), "K", "swal('Foreign Key Constraint Violation','Cannot Delete Record. Associated Data In Use.','warning')", true);
                }
            }
        }

        protected void Excel_download(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT PR.Product_ID, PR.Product_Name,CT.Category_Name, SC.SubCategory_Name,  PR.Net_Qty, PR.Net_Rate, PR.IsActive from Category CT, SubCategory SC, Tbl_Products PR\r\nwhere PR.Category_ID = CT.Category_ID and sc.SubCategory_ID = PR.SubCategory_ID and PR.IsActive = 1 order by PR.Created_dt  DESC"))
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
                                wb.Worksheets.Add(dt, "Product");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Product_List.xlsx");
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

        protected void Get_product_data(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            int prod_id = Convert.ToInt32(ProductTable.DataKeys[gvrow.RowIndex].Value.ToString());

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT PR.Product_ID, PR.Product_Name,PR.Category_ID, PR.SubCategory_ID,  PR.Net_Qty, PR.Net_Rate from Category CT, SubCategory SC, Tbl_Products PR\r\nwhere PR.Category_ID = CT.Category_ID and sc.SubCategory_ID = PR.SubCategory_ID AND PR.Product_ID ='" + prod_id + "' and PR.IsActive = 1 order by PR.Created_dt DESC;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            con.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                sdr.Read();
                prod_idtxt1.Text = prod_id.ToString();
                prodNametxt1.Text = sdr["Product_Name"].ToString();
                categorydrpdwn1.Text = sdr["Category_ID"].ToString();
                Subcatedrpdown_1();
                subcatdrpdwn1.Text = sdr["SubCategory_ID"].ToString();
                //Qtytxt1.Text = sdr["Net_Qty"].ToString();
                Ratetxt1.Text = sdr["Net_Rate"].ToString();


            }
            con.Close();
        }

        protected void update_Click(object sender, EventArgs e)
        {
            clearcreateError1();
            if (ProductTable.FooterRow != null)
            {
                ProductTable.UseAccessibleHeader = true;
                ProductTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                ProductTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                ProductTable.UseAccessibleHeader = true;
                ProductTable.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {
                if (prodNametxt1.Text == string.Empty)
                {
                    PN1.Text = "Please Enter Product Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;
                }

                else if (categorydrpdwn1.SelectedValue == "0")
                {
                    CT1.Text = "Please Select Category";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;

                }

                else if (subcatdrpdwn1.SelectedValue == "0")
                {
                    SCT1.Text = "Please Select Sub Category";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;

                }

                //else if (Qtytxt1.Text == string.Empty)
                //{
                //    QTY1.Text = "Please Enter Quantity";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                //    return;
                //}

                else if (Ratetxt1.Text == string.Empty)
                {
                    RT1.Text = "Please Enter Rate";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;
                }
                else
                {
                    clearcreateError1();
                }
                string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                DateTime nowDateTime = DateTime.Now;
                int Product_id = Convert.ToInt32(prod_idtxt1.Text.ToString());
                string un = Session["username"].ToString();
                SqlCommand cmd = new SqlCommand("Update Tbl_Products Set Product_Name=@Product_Name,Category_ID=@Category_ID,SubCategory_ID=@SubCategory_ID,Net_Rate=@Net_Rate,Modified_dt = @Modified_dt,Modified_by =@Modified_by where Product_ID=@productid ", con);
                cmd.Parameters.AddWithValue("@productid", Product_id);
                cmd.Parameters.AddWithValue("@Product_Name", prodNametxt1.Text);
                cmd.Parameters.AddWithValue("@Category_ID", categorydrpdwn1.SelectedValue);
                cmd.Parameters.AddWithValue("@SubCategory_ID", subcatdrpdwn1.SelectedValue);
                //cmd.Parameters.AddWithValue("@Net_Qty", Qtytxt1.Text);
                cmd.Parameters.AddWithValue("@Net_Rate", Ratetxt1.Text);
                cmd.Parameters.AddWithValue("@Modified_dt", nowDateTime);
                cmd.Parameters.AddWithValue("@Modified_by", un);
                int r = cmd.ExecuteNonQuery();
                get_products();
                clearCreateProd1();
                con.Close();
                if (r > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Updated!','Product has been Updated !','success')", true);
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Updating!','error')", true);
            }

        }

       

        protected void categorydrpdwn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT SubCategory_ID, SubCategory_Name from SubCategory where BaseCategory_ID ='" + categorydrpdwn.SelectedValue + "'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            subcatdrpdwn.DataSource = ds;
            subcatdrpdwn.DataTextField = "SubCategory_Name";
            subcatdrpdwn.DataValueField = "SubCategory_ID";
            
            subcatdrpdwn.DataBind();
            subcatdrpdwn.Items.Insert(0, new ListItem("Select Sub Category", "0"));

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "opencreatModal();", true);
        }

        protected void categorydrpdwn1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT SubCategory_ID, SubCategory_Name from SubCategory where BaseCategory_ID ='" + categorydrpdwn1.SelectedValue + "' ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            subcatdrpdwn1.DataSource = ds;
            subcatdrpdwn1.DataTextField = "SubCategory_Name";
            subcatdrpdwn1.DataValueField = "SubCategory_ID";
            subcatdrpdwn1.DataBind();
            subcatdrpdwn1.Items.Insert(0, new ListItem("Select Sub Category", "0"));

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }

        
    }
}