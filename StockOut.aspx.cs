using HRMS;
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
using DocumentFormat.OpenXml.Wordprocessing;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using ClosedXML.Excel;
using System.IO;
//using ListItem = DocumentFormat.OpenXml.Wordprocessing.ListItem;

namespace HRMS_
{
    public partial class StockOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.getdata();
        }
        public void getdata()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT  so.Selling_ID,so.Category_NAME,so.Subcategory_NAME,so.Product_NAME,so.quantity,p.Net_Qty avl_qty,so.price,so.totalprice, " +
                                "so.Status,so.pid_Fkid  Product_ID from StockOut so left join Tbl_Products p on p.Product_ID = so.pid_Fkid ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            Stockout.DataSource = ds;
            Stockout.DataBind();
            if (Stockout.FooterRow != null)
            {

                Stockout.UseAccessibleHeader = true;
                Stockout.HeaderRow.TableSection = TableRowSection.TableHeader;
                Stockout.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                Stockout.UseAccessibleHeader = true;
                Stockout.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            string comm = "select * from Tbl_Products where IsActive = 1";
            SqlDataAdapter adptr = new SqlDataAdapter(comm, connectionString);
            DataTable dtt = new DataTable();
            dtt.Columns.Add("Product_ID");
            dtt.Columns.Add("Product_Name");
            dtt.Rows.Add("0", "select");
            adptr.Fill(dtt);
            DropDownList4.DataSource = dtt;
            DropDownList4.DataBind();
            DropDownList4.DataTextField = "Product_Name";
            DropDownList4.DataValueField = "Product_Name";

            DropDownList4.DataBind();
            //DropDownList4.Items.Insert(0, new ListItem("Select Product Name", "0"));



        }

        protected void apply_Click(object sender, EventArgs e)
        {

            var pname = productname.Text;
            if (pname == "")
            {
                errorid.Text = "Select Product Name";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                return;
            }
            else
            {
                errorid.Text = "";
            }
            var pid = Product_ID.Text;
            var Categoryname = Category_name.Text;
            var subcategoryname = subcategory_name.Text;
            var quantityy = quantity.Text;

            var price1 = price.Text;
            var testimateprice1 = TextBox5.Text;



            var status = "Ordered";
            string un = Session["username"].ToString();
            DateTime nowDateTime = DateTime.Now;
            string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Insert into StockOut (Category_NAME,Subcategory_NAME,Product_NAME,Quantity,Price,Totalprice,Status,pid_Fkid,Created_By,Created_Date)" +
                "values(@Categoryname,@subcategoryname,@pname,@quantityy,@price1,@testimateprice1,@status,@pid,@Created_By,@Created_Date)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Categoryname", Categoryname);
            cmd.Parameters.AddWithValue("@subcategoryname", subcategoryname);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@quantityy", quantityy);
            cmd.Parameters.AddWithValue("@price1", price1);
            cmd.Parameters.AddWithValue("@testimateprice1", testimateprice1);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@Created_By", un);
            cmd.Parameters.AddWithValue("@Created_Date", nowDateTime);
            cmd.ExecuteNonQuery();

            string query1 = "Update Tbl_Products set Net_Qty=Net_Qty-@quantityy where Product_ID=@pid";
            SqlCommand cmd1 = new SqlCommand(query1);
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("quantityy", quantityy);
            cmd1.Parameters.AddWithValue("pid", pid);
            cmd1.ExecuteNonQuery();
            //LoadRecord();
            Product_ID.Text = "";
            testimateprice.Text = "";
            price.Text = "";
            quantity.Text = "";
            subcategory_name.Text = "";
            Category_name.Text = "";
            DropDownList4.SelectedValue = "select";

            ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Success!','Ordered Successfully!','success')", true);
            con.Close();
            this.getdata();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", false);

            Product_ID.Text = "";
            testimateprice.Text = "";
            price.Text = "";
            quantity.Text = "";
            subcategory_name.Text = "";
            Category_name.Text = "";
            DropDownList4.SelectedValue = "select";
        }

        protected void Stockout_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(Stockout.DataKeys[e.RowIndex].Value.ToString());
            //var quantityy = quantity.Text;
            //var pid = Product_ID.Text;
            int quantityy = Convert.ToInt32(Stockout.Rows[e.RowIndex].Cells[5].Text);
            int pid = Convert.ToInt32(Stockout.Rows[e.RowIndex].Cells[1].Text);

            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query1 = "Update Tbl_Products set Net_Qty=Net_Qty+@quantityy where Product_ID=@pid";
            SqlCommand cmd1 = new SqlCommand(query1);
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("quantityy", quantityy);
            cmd1.Parameters.AddWithValue("pid", pid);
            cmd1.ExecuteNonQuery();
            string query = "Delete from StockOut where Selling_ID='" + id + "'";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            var row = cmd.ExecuteNonQuery();
     
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Cancelled!','Order has been cancelled!','success')", true);
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", false);
                this.getdata();


            }
        }

        protected void Stockout_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                    string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
                    string selectSQL = "SELECT  so.Selling_ID,so.Category_NAME,so.Subcategory_NAME,so.Product_NAME,so.quantity,p.Net_Qty avl_qty,so.price,so.totalprice, " +
                                        "so.Status,so.pid_Fkid  Product_ID from StockOut so left join Tbl_Products p on p.Product_ID = so.pid_Fkid ";
                    SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                //cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    TextBox6.Text = dr["Product_NAME"].ToString();
                    TextBox1.Text = dr["Product_ID"].ToString();
                    TextBox2.Text = dr["Category_NAME"].ToString();
                    TextBox3.Text = dr["Subcategory_NAME"].ToString();
                    TextBox4.Text = dr["quantity"].ToString();
                    TextBox11.Text = dr["quantity"].ToString();
                    TextBox7.Text = dr["price"].ToString();
                    TextBox8.Text = dr["totalprice"].ToString();
                    TextBox10.Text = dr["Selling_ID"].ToString();


                }
                con.Close();

                //TextBox6.Text = pname;
                //TextBox1.Text = pid;
                //TextBox10.Text = oid;
                //TextBox2.Text = cname;
                //TextBox3.Text = scname;
                //TextBox4.Text = quantity;
                //TextBox7.Text = price;
                //TextBox8.Text = totalprice;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var orid = int.Parse(TextBox10.Text);
            var id = int.Parse(TextBox1.Text);
            var pname = TextBox6.Text;
            var category = TextBox2.Text;
            var sca = TextBox3.Text;
            // var quan = TextBox4.Text;
            var price = TextBox7.Text;
            var tp = TextBox9.Text;
            var quantity = TextBox4.Text;

            if (quantity == "")
            {
                Label1.Text = "Enter  Quantity";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                return;
            }
            else
            {
                Label1.Text = "";
            }
            var status = "Ordered";

            DateTime nowDateTime = DateTime.Now;
            string un = Session["username"].ToString();
            string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Update StockOut set Category_NAME=@cname,Subcategory_NAME=@scname,Product_NAME=@pname," +
                "quantity=@quantity,price=@price,totalprice=@totalestimate ,Status=@status,pid_Fkid=@pid,Modified_By=@Modified_By,Modified_Date=@Modified_Date" +
                " where Selling_ID=@oid";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@oid", orid);
            cmd.Parameters.AddWithValue("@pid", id);
            cmd.Parameters.AddWithValue("@cname", category);
            cmd.Parameters.AddWithValue("@scname", sca);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@totalestimate", tp);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@Modified_By", un);
            cmd.Parameters.AddWithValue("@Modified_Date", nowDateTime);

            string query1 = "Update Tbl_Products set Net_Qty=Net_Qty+@quantity where Product_ID=@pid";
            SqlCommand cmd1 = new SqlCommand(query1);
            cmd1.Connection = con;
            
            cmd1.Parameters.AddWithValue("quantity", Convert.ToInt32(TextBox11.Text)-Convert.ToInt32(quantity));
            cmd1.Parameters.AddWithValue("pid", id);
            cmd1.ExecuteNonQuery();
            //cmd.Parameters.AddWithValue("@StudentID", name);
 

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            ClientScript.RegisterClientScriptBlock
                  (this.GetType(), "K", "swal('Success!','Order updated Successfully!','success')", true);
            //   Response.Redirect("~/leave_form.aspx");
            this.getdata();
        }
protected void StockOut_report_download(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT  so.Selling_ID SellingId,so.pid_Fkid  Product_ID,so.Category_NAME CategoryName,so.Subcategory_NAME SubcategoryName,so.Product_NAME ProductName,so.quantity Qty,p.Net_Qty Avl_Qty,so.price Price,so.totalprice TotalPrice,so.Created_By,so.Created_Date,so.Modified_By,so.Modified_Date " +
                    "from StockOut so left join Tbl_Products p on p.Product_ID = so.pid_Fkid "))
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
                                wb.Worksheets.Add(dt, "stock");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=StockOut.xlsx");
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
        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pname = productname.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
            DropDownList4.SelectedValue = pname;
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            // string query = "Select ct.Category_Name,sct.SubCategory_Name,tp.Product_ID from Tbl_Products tp left join Category ct on tp.Category_ID = ct.Category_ID left join SubCategory sct on tp.SubCategory_ID = sct.SubCategory_ID where  tp.Product_Name=pname";
            // SqlCommand cmd = new SqlCommand(query);
            string comm = "Select ct.Category_Name,sct.SubCategory_Name,tp.Product_ID,tp.Net_Rate from Tbl_Products tp " +
                "left join Category ct on tp.Category_ID = ct.Category_ID left join " +
                "SubCategory sct on tp.SubCategory_ID = sct.SubCategory_ID where  tp.Product_Name = '" + pname + "'";
            SqlDataAdapter adptr = new SqlDataAdapter(comm, constr);
            DataTable dtt = new DataTable();
            adptr.Fill(dtt);


            // string currenciesQuery = "SELECT * FROM Currencies";
            // currencyDataSet1 = new DataSets.CurrencyDataSet();
            // DataTable dataTable = currencyDataSet1.Tables.Add("Currencies");
            //  string connectionString = System.Configuration.ConfigurationSettings.AppSettings["SQLConnectionString"].ToString();

            //using (SqlConnection connection = new SqlConnection(constr))
            //{
            //    SqlDataAdapter dataAdapter = new SqlDataAdapter(comm, connection);

            //    dataAdapter.Fill(dtt);
            //}

            int datacnt = dtt.Rows.Count;
            for (int i = 0; i < dtt.Rows.Count; i++)
            {

                Product_ID.Text = dtt.Rows[i]["Product_Id"].ToString();
                Category_name.Text = dtt.Rows[i]["Category_Name"].ToString();
                subcategory_name.Text = dtt.Rows[i]["SubCategory_Name"].ToString();
                price.Text = dtt.Rows[i]["Net_Rate"].ToString();
            }

            //Product_ID.("Int", dtt, "CurrencyId");
            //Category_name.("Text", dtt, "CurrencyCode");
            //subcategory_name.TextMode("Text", dtt, "CurrencyName");
            //Category_name.DataBindings.Add("Decimal", dtt, "CurrencyRate");


        }


    }
}
