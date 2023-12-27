using HRMS;
using System;
using ClosedXML.Excel;
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
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.Excel;
using DataTable = System.Data.DataTable;

//using ListItem = DocumentFormat.OpenXml.Wordprocessing.ListItem;

namespace HRMS_
{
    public partial class Procurement : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.getdata();

        }
        public void getdata()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT  Product_ID,Category_NAME, Subcategory_NAME,Product_NAME, quantity, price, totalprice,pid_Fkid,dealer_name from procurement ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            procurement.DataSource = ds;
            procurement.DataBind();
            if (procurement.FooterRow != null)
            {

                procurement.UseAccessibleHeader = true;
                procurement.HeaderRow.TableSection = TableRowSection.TableHeader;
                procurement.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                procurement.UseAccessibleHeader = true;
                procurement.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            string comm = "select * from Tbl_Products";
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
            string comm1 = "select * from DealerMaster";
            SqlDataAdapter adptr1 = new SqlDataAdapter(comm1, connectionString);
            DataTable dtt1 = new DataTable();
            dtt1.Columns.Add("Dealer_ID");
            dtt1.Columns.Add("Dealer_Name");
            dtt1.Rows.Add("0", "select");
            adptr1.Fill(dtt1);
            DropDownList1.DataSource = dtt1;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "Dealer_Name";
            DropDownList1.DataValueField = "Dealer_Name";

            DropDownList1.DataBind();



        }

        protected void apply_Click(object sender, EventArgs e)
        {
            var dealer = dealername.Text;
            if (dealer == "")
            {
                Label3.Text = "Select Dealer Name";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
               
                return;
            }
            else
            {
                Label3.Text = "";
            }

            var pname = productname.Text;
            if (pname == "")
            {
                errorid.Text = "Select Product Name";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                DropDownList1.SelectedValue = dealername.Text;
                return;
            }
            else
            {
                errorid.Text = "";
            }
            
            var pid = Product_ID.Text;
            var Categoryname= Category_name.Text;
            var subcategoryname = subcategory_name.Text;
            var quantityy = quantity.Text;
            var quantityy1 = Convert.ToInt32(quantity.Text);
            if (quantityy == "")
            {
                Label2.Text = "Enter Quantity ";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                DropDownList4.SelectedValue = productname.Text;
                DropDownList1.SelectedValue = dealername.Text;
                return;
            }
            if (quantityy == "0") {

                Label2.Text = "Enter Quantity Greater than 0 ";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal1();", true);
                DropDownList4.SelectedValue = productname.Text;
                return;
            }
            else
            {
                Label2.Text = "";
            }
            var price1= price.Text;
            var testimateprice1 = TextBox5.Text;
            


           // var status = "Ordered";


            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();

          //  select Net_Qty from Tbl_Products where Product_ID = 2

            string query = "Insert into procurement values('" + Categoryname + "','" + subcategoryname + "','" + pname + "','" + quantityy + "','" + price1 + "','" + testimateprice1 + "','" + pid + "','" + dealer + "')";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
             string query1 = "Update Tbl_Products set Net_Qty=Net_Qty+@quantityy where Product_ID=@pid";
            SqlCommand cmd1 = new SqlCommand(query1);
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("@quantityy", quantityy1);
            cmd1.Parameters.AddWithValue("@pid", pid);
            cmd1.ExecuteNonQuery();

            //LoadRecord();
            Product_ID.Text = "";
            testimateprice.Text = "";
            price.Text = "";
            quantity.Text = "";
            subcategory_name.Text = "";
            Category_name.Text = "";
            DropDownList4.SelectedValue = "select";
            DropDownList1.SelectedValue = "select";
            ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Success!','Order Applied Successfully!','success')", true);
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
            DropDownList1.SelectedValue = "select";
        }

        protected void procurement_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(procurement.DataKeys[e.RowIndex].Value.ToString());
            int quantityy = Convert.ToInt32(procurement.Rows[e.RowIndex].Cells[5].Text);
            int pid = Convert.ToInt32(procurement.Rows[e.RowIndex].Cells[1].Text);



            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Delete from procurement where Product_ID='" + id + "'";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            var row = cmd.ExecuteNonQuery();
            string query1 = "Update Tbl_Products set Net_Qty=Net_Qty-@quantityy where Product_ID=@pid";
            SqlCommand cmd1 = new SqlCommand(query1);
            cmd1.Connection = con;
            cmd1.Parameters.AddWithValue("@quantityy", quantityy);
            cmd1.Parameters.AddWithValue("@pid", pid);
            cmd1.ExecuteNonQuery();
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Cancelled!','Order has been cancelled!','success')", true);
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", false);
                this.getdata();


            }
        }

        protected void procurement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);


                int Index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = procurement.Rows[Index];

                var id = procurement.DataKeys[row.RowIndex].Value.ToString();
                var oid = HttpUtility.HtmlDecode(row.Cells[0].Text);
                var pid = HttpUtility.HtmlDecode(row.Cells[1].Text);
                var cname = HttpUtility.HtmlDecode(row.Cells[2].Text);
                var scname = HttpUtility.HtmlDecode(row.Cells[3].Text);
                var pname = HttpUtility.HtmlDecode(row.Cells[4].Text);
                var quantity = HttpUtility.HtmlDecode(row.Cells[5].Text);
                var price = HttpUtility.HtmlDecode(row.Cells[6].Text);
                var totalprice = HttpUtility.HtmlDecode(row.Cells[7].Text);
                var dealer = HttpUtility.HtmlDecode(row.Cells[8].Text);

                TextBox6.Text = pname;
                TextBox1.Text = pid;
                TextBox10.Text = oid;

                TextBox11.Text = dealer;
                TextBox2.Text = cname;
                TextBox3.Text = scname;
                TextBox4.Text = quantity;
                TextBox7.Text = price;
                TextBox8.Text = totalprice;
                TextBox12.Text = quantity;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var orid= int.Parse(TextBox10.Text);
            var id = int.Parse(TextBox1.Text);
            var pname = TextBox6.Text;
            var category = TextBox2.Text;
            var sca = TextBox3.Text;
           // var quan = TextBox4.Text;
            var price = TextBox7.Text;
            var tp = TextBox9.Text;
            var dealer = TextBox11.Text;








            var quantity = TextBox4.Text;
            int quans = Convert.ToInt32(TextBox4.Text);
            int qty = Convert.ToInt32(TextBox12.Text);
            if (quantity == "0" )
            {
                Label1.Text = "Enter  Quantity";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);
                

                return;
            }
            if ( quantity == "0")
            {
                Label1.Text = "Enter  Quantity Greater than 0";

                ClientScript.RegisterStartupScript(this.GetType(), "pop", "openModal();", true);


                return;
            }
            else
            {
                Label1.Text = "";
            }
           // var status = "Ordered";
          

            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Update procurement set Category_NAME=@cname,Subcategory_NAME=@scname,Product_NAME=@pname,quantity=@quantity,price=@price,totalprice=@totalestimate ,pid_Fkid=@pid,dealer_name=@dealer where Product_ID=@oid";
            SqlCommand cmd = new SqlCommand(query);
            //cmd.Parameters.AddWithValue("@StudentID", name);
            cmd.Parameters.AddWithValue("@oid", orid);
            cmd.Parameters.AddWithValue("@pid", id);
            cmd.Parameters.AddWithValue("@cname", category);
            cmd.Parameters.AddWithValue("@scname", sca);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@totalestimate", tp);
            cmd.Parameters.AddWithValue("@dealer", dealer);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
           
          
            if (quans >  qty)
            {
                int t_q = quans - qty;
                string query1 = "Update Tbl_Products set Net_Qty=Net_Qty+@t_q where Product_ID=@pid";
                SqlCommand cmd1 = new SqlCommand(query1);
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("@t_q", t_q);
                cmd1.Parameters.AddWithValue("@pid", id);
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            if (quans < qty)
            {
                int t_q =  qty - quans ;
                string query1 = "Update Tbl_Products set Net_Qty=Net_Qty-@t_q where Product_ID=@pid";
                SqlCommand cmd1 = new SqlCommand(query1);
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("@t_q", t_q);
                cmd1.Parameters.AddWithValue("@pid", id);
                cmd1.ExecuteNonQuery();
                con.Close();
            }

           
            ClientScript.RegisterClientScriptBlock
                  (this.GetType(), "K", "swal('Success!','order update Successfully!','success')", true);
            //   Response.Redirect("~/leave_form.aspx");
            this.getdata();
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
            string comm = "Select ct.Category_Name,sct.SubCategory_Name,tp.Product_ID,tp.Net_Rate from Tbl_Products tp left join Category ct on tp.Category_ID = ct.Category_ID left join SubCategory sct on tp.SubCategory_ID = sct.SubCategory_ID where  tp.Product_Name = '" + pname+"'";
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
            for (int i= 0; i < dtt.Rows.Count; i++ ){

                Product_ID.Text =dtt.Rows[i]["Product_Id"].ToString();
                Category_name.Text=dtt.Rows[i]["Category_Name"].ToString();
                subcategory_name.Text= dtt.Rows[i]["SubCategory_Name"].ToString();
                price.Text=dtt.Rows[i]["Net_Rate"].ToString();
            }

            //Product_ID.("Int", dtt, "CurrencyId");
            //Category_name.("Text", dtt, "CurrencyCode");
            //subcategory_name.TextMode("Text", dtt, "CurrencyName");
            //Category_name.DataBindings.Add("Decimal", dtt, "CurrencyRate");


        }

        protected void excel_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM procurement"))
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
                                wb.Worksheets.Add(dt, "procurement");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Procurement.xlsx");
                             
                            }
                        }
                    }
                }
            }
        }
    }
    }
