using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Drawing;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using Microsoft.Ajax.Utilities;

namespace HRMS_
{
    public partial class PurchaseRecieve : System.Web.UI.Page
    {
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
        int qty;
        int iqty;
        int npid;
        int epid;
        int prodid;
        int originalQuantity;
        Label lb = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
                ddlsuplier();

                //ddlsuppliername.Items.Insert(0, new ListItem("Select Supplier", "0"));
                // ddlpoid.Items.Insert(0, new ListItem("Select Purchase Order", "0"));
            }

            // int lastInvoiceNumber = GetLastInvoiceNumber();

            // Increment the invoice number by 1
            //int newInvoiceNumber = lastInvoiceNumber + 1;

            // Set the new invoice number in the textbox
            // txtInvoiceNumber.Text = newInvoiceNumber.ToString();



        }

        private int GetLastInvoiceNumber()
        {
            SqlConnection connection = new SqlConnection(constring);

            string query = "SELECT TOP 1 ReceiveID FROM PurchaseReceives ORDER BY ReceiveID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            object result = command.ExecuteScalar();

            int lastInvoiceNumber = Convert.ToInt32(result);
            return lastInvoiceNumber;


        }
        protected void ddlsuplier()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetAllDealers", con)) // Assuming GetAllDealers is the name of your stored procedure
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    ddlsuppliername.DataSource = cmd.ExecuteReader();
                    ddlsuppliername.DataBind();
                    ddlsuppliername.Items.Insert(0, new ListItem("Select Supplier", "0"));
                }
                con.Close();
            }
        }
        protected void ddlporderid()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetPurchaseOrderIDs", con)) // Assuming GetPurchaseOrderIDs is the name of your stored procedure
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierID", ddlsuppliername.SelectedValue);
                    ddlpoid.DataSource = cmd.ExecuteReader();
                    ddlpoid.DataTextField = "POrderID";
                    ddlpoid.DataBind();
                    ddlpoid.Items.Insert(0, new ListItem("Select Purchase Order", "0"));
                }
                con.Close();
            }
        }

        protected void loadData()
        {
            System.Data.DataTable dt1 = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(constring))
            {

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * from PurchaseReceives", con);
                da.Fill(dt1);
                grn.DataSource = dt1;
                grn.DataBind();
            }
            if (grn.FooterRow != null)
            {
                grn.UseAccessibleHeader = true;
                grn.HeaderRow.TableSection = TableRowSection.TableHeader;
                grn.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                grn.UseAccessibleHeader = true;
                grn.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void prno_Click(object sender, EventArgs e)
        {
            string script = "$('#purchaserecieve').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupedit", script, true);
            LinkButton btn = (LinkButton)sender;
            int rowIndex = Convert.ToInt32(btn.CommandArgument);
            GridViewRow row = grn.Rows[rowIndex];
            Label idl = (Label)row.FindControl("poid");
            Label vname = (Label)row.FindControl("suppliername");
            string vn = vname.Text;
            string pid = idl.Text;
            Label daterecieve = (Label)row.FindControl("recievedate");
            string rdate = daterecieve.Text;
            Label sts = (Label)row.FindControl("status");
            string stat = sts.Text;
            recievedate.InnerText = rdate;
            PoID.InnerText = pid;
            status.InnerText = stat;
            vendorname.InnerText = vn;
            string state;

            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand command = new SqlCommand("Select Status from PurchaseReceives where PurchaseOrderId='" + pid + "' ", con);
            object statusofpo = command.ExecuteScalar();
            state = statusofpo.ToString();

            if (state == "Received")
            {
                statbtn.Text = "Mark As In Transit";
            }
            else
            {
                statbtn.Text = "Mark As Received";
            }


            System.Data.DataTable dt2 = new System.Data.DataTable();
            using (SqlConnection con1 = new SqlConnection(constring))
            {

                con1.Open();

                SqlDataAdapter da1 = new SqlDataAdapter("SELECT ItemName,ItemQuantity,Amount from PurchaseRecieveItems where PoID='" + PoID.InnerText + "'", con1);
                da1.Fill(dt2);
                recieveitem.DataSource = dt2;
                recieveitem.DataBind();
            }

        }



        protected void ddlsuppliername_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);


            con.Open();

            SqlCommand cmd = new SqlCommand("Select POrderID from PurchaseOrders where SupplierID='" + ddlsuppliername.SelectedValue + "' AND Status IS NULL ", con);

            ddlpoid.DataSource = cmd.ExecuteReader();
            ddlpoid.DataTextField = "POrderID";
            ddlpoid.DataBind();
            ddlpoid.Items.Insert(0, new ListItem("Select Purchase Order", "0"));
            con.Close();


            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "newpurchase();", true);

        }


        protected void ddlpoid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string spo = ddlpoid.SelectedItem.Text;

            LoadEditRecieveItemData(spo);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "newpurchase();", true);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (ddlsuppliername.SelectedValue == "0")
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Ple", "newpurchase();", true);
                ddlsuppliername.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','Select Supplier Name','warning')", true);

                return;

            }
            else if (ddlpoid.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Ple", "newpurchase();", true);
                ddlpoid.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','Select Purchase Order','warning')", true);

                return;
            }
            else if (rcdate.Value == String.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Ple", "newpurchase();", true);
                ddlsuppliername.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','Select Select Date','warning')", true);

                return;
            }
            else
            {


                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Done','Purchase Marked As Recieved','success')", true);

                string suppname = ddlsuppliername.SelectedItem.Text.ToString();
                int suppid = Convert.ToInt32(ddlsuppliername.SelectedValue);
                int poid = Convert.ToInt32(ddlpoid.SelectedItem.Text);
                // int recieveid = Convert.ToInt32(txtInvoiceNumber.Text);
                string dt = rcdate.Value.ToString();
                string notes = note.Value.ToString();
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();

                    // Assuming "Column1" and "Column2" are the names of the columns you want to calculate the total for
                    string query = "SELECT Quantity,TotalAmount FROM PurchaseOrderItems where PurchaseOrderID='" + poid + "'";

                    SqlCommand command = new SqlCommand(query, con);


                    SqlDataReader read = command.ExecuteReader();

                    int totalqty = 0;
                    int totalamt = 0;

                    while (read.Read())
                    {
                        // Assuming the column types are int, modify accordingly for other types
                        int column1Value = read.GetInt32(0);
                        int column2Value = read.GetInt32(1);

                        // Add the values to their respective totals
                        totalqty += column1Value;
                        totalamt += column2Value;
                    }
                    read.Close();
                    // Now 'totalColumn1' holds the sum of values in Column1, and 'totalColumn2' holds the sum of values in Column2
                    using (SqlCommand com = new SqlCommand("INSERT INTO PurchaseReceives (ReceiveDate,SupplierID,SupplierName,PurchaseOrderID,Quantity,TotalAmount,Notes,Status)  VALUES ('" + dt + "','" + suppid + "', '" + suppname + "','" + poid + "','" + totalqty + "','" + totalamt + "','" + notes + "','Recieved')", con))
                    {

                        com.ExecuteNonQuery();
                        con.Close();


                    }
                    using (SqlCommand com = new SqlCommand("INSERT INTO PurchaseRecieveItems (PoID,ItemID,ItemName,ItemQuantity,UnitPrice,Amount) SELECT PurchaseOrderID,ProductID,ItemName,Quantity,UnitPrice,TotalAmount from PurchaseOrderItems where PurchaseOrderID='" + poid + "'", con))
                    {
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                        loadData();
                        clearnewpo();

                    }
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Success','Marked As Recieved','success')", true);

                }

            }
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','Select Supplier Name','warning')", true);




        }


        protected void clearnewpo()
        {
            ddlsuppliername.ClearSelection();
            ddlpoid.ClearSelection();
            rcdate.Value = null;
            note.InnerText = String.Empty;
            editrecieveitem.DataSource = null;
            editrecieveitem.DataBind();

        }


        protected void editbtn_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editmodal();", true);
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            prodid = Convert.ToInt32(editrecieveitem.DataKeys[gvrow.RowIndex].Value.ToString());
            npid = prodid;
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "SELECT ProductID,ItemName,Quantity from PurchaseOrderItems where ProductID='" + prodid + "'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            sdr.Read();
            productid.Text = prodid.ToString();
            itemname.Text = sdr["ItemName"].ToString();
            itemquantity.Text = sdr["Quantity"].ToString();
            sdr.Close();

            SqlDataReader sdrq = cmd.ExecuteReader();

            if (sdrq.Read())
            {
                originalQuantity = Convert.ToInt32(sdrq["Quantity"].ToString());
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error','Recieve Quantity','warning')", true);

            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','" + originalQuantity + "','warning')", true);
            sdrq.Close();
            con.Close();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ddlsuppliername.SelectedValue == "0")
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Ple", "newpurchase();", true);
                ddlsuppliername.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','Select Supplier Name','warning')", true);

                return;

            }
            else if (ddlpoid.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Ple", "newpurchase();", true);
                ddlpoid.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','Select Purchase Order','warning')", true);

                return;
            }
            else if (rcdate.Value == String.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Ple", "newpurchase();", true);
                ddlsuppliername.Focus();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Please Fill','Select Select Date','warning')", true);

                return;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Done','Purchase Marked As In Transit','success')", true);

                string suppname = ddlsuppliername.SelectedItem.Text.ToString();
                int suppid = Convert.ToInt32(ddlsuppliername.SelectedValue);
                int poid = Convert.ToInt32(ddlpoid.SelectedItem.Text);
                //int recieveid = Convert.ToInt32(txtInvoiceNumber.Text);
                string dt = rcdate.Value.ToString();
                string notes = note.Value.ToString();


                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();

                    // Assuming "Column1" and "Column2" are the names of the columns you want to calculate the total for
                    string query = "SELECT Quantity,TotalAmount FROM PurchaseOrderItems where PurchaseOrderID='" + poid + "'";

                    SqlCommand command = new SqlCommand(query, con);


                    SqlDataReader read = command.ExecuteReader();

                    int totalqty = 0;
                    int totalamt = 0;

                    while (read.Read())
                    {
                        // Assuming the column types are int, modify accordingly for other types
                        int column1Value = read.GetInt32(0);
                        int column2Value = read.GetInt32(1);

                        // Add the values to their respective totals
                        totalqty += column1Value;
                        totalamt += column2Value;
                    }
                    read.Close();
                    // Now 'totalColumn1' holds the sum of values in Column1, and 'totalColumn2' holds the sum of values in Column2
                    using (SqlCommand com = new SqlCommand("INSERT INTO PurchaseReceives (ReceiveDate,SupplierID,SupplierName,PurchaseOrderID,Quantity,TotalAmount,Notes,Status)  VALUES ('" + dt + "','" + suppid + "', '" + suppname + "','" + poid + "','" + totalqty + "','" + totalamt + "','" + notes + "','In Transit')", con))
                    {

                        com.ExecuteNonQuery();
                        con.Close();


                    }
                    using (SqlCommand com = new SqlCommand("INSERT INTO PurchaseRecieveItems (PoID,ItemID,ItemName,ItemQuantity,UnitPrice,Amount) SELECT PurchaseOrderID,ProductID,ItemName,Quantity,UnitPrice,TotalAmount from PurchaseOrderItems where PurchaseOrderID='" + poid + "'", con))
                    {
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                        loadData();
                        clearnewpo();

                    }
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Success','Marked As InTransit','success')", true);





                }
            }


        }
        protected void UpdateQtyAmt(int upo)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("UpdateQtyAmt", con)) // Assuming UpdateQtyAmt is the name of your stored procedure
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PurchaseOrderID", upo);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void stat_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            string poids = PoID.InnerText;
            string state;
            // stat.Text = poids;
            con.Open();

            // Add the values to their respective totals
            if (statbtn.Text == "Mark As In Transit")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Done','Purchase Marked As In Transit','success')", true);

                using (SqlCommand com = new SqlCommand("Update PurchaseReceives Set Status='In Transit' where PurchaseOrderID='" + poids + "'", con))
                {

                    com.ExecuteNonQuery();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Success','Marked As InTransit','success')", true);


                    loadData();


                }
                using (SqlCommand com = new SqlCommand("Update PurchaseOrders Set Status='In Transit' where POrderID='" + poids + "'", con))
                {

                    com.ExecuteNonQuery();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Success','Marked As Received','success')", true);

                    loadData();


                }

            }
            if (statbtn.Text == "Mark As Received")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Done','Purchase Marked As Recieved','success')", true);

                using (SqlCommand com = new SqlCommand("Update PurchaseReceives Set Status='Received' where PurchaseOrderID='" + poids + "'", con))
                {

                    com.ExecuteNonQuery();
                    loadData();


                }
                using (SqlCommand com = new SqlCommand("Update PurchaseOrders Set Status='Received' where POrderID='" + poids + "'", con))
                {

                    com.ExecuteNonQuery();
                    loadData();


                }

            }


            //    ClientScript.RegisterClientScriptBlock
            // (this.GetType(), "K", "swal('','"+statbtn.Text+"','success')", true);

        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(constring);
            connection.Open();
            SqlCommand cmd = new SqlCommand("Delete From PurchaseReceives where PurchaseOrderID='" + PoID.InnerText + "'", connection);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("Delete From PurchaseRecieveItems where PoID='" + PoID.InnerText + "'", connection);
            cmd1.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("Update PurchaseOrders set Status=@newValue where POrderID='" + PoID.InnerText + "'", connection);
            cmd2.Parameters.AddWithValue("@newValue", DBNull.Value);
            cmd2.ExecuteNonQuery();
            connection.Close();
            loadData();
        }


        private void LoadEditRecieveItemData(string spo)
        {
            SqlConnection con5 = new SqlConnection(constring);
            // Replace this with your actual logic to fetch data from the database
            SqlDataAdapter eda = new SqlDataAdapter("SELECT ProductID,ItemName,Quantity FROM PurchaseOrderItems where PurchaseOrderID='" + spo + "'", con5);
            System.Data.DataTable eitem = new System.Data.DataTable();
            eda.Fill(eitem);
            editrecieveitem.DataSource = eitem;
            editrecieveitem.DataBind();

        }
        private void EditRecieveItemData(string spo)
        {
            SqlConnection con4 = new SqlConnection(constring);
            // Replace this with your actual logic to fetch data from the database
            SqlDataAdapter eeda = new SqlDataAdapter("SELECT ProductID,ItemName,Quantity FROM PurchaseOrderItems where PurchaseOrderID='" + spo + "'", con4);
            System.Data.DataTable eeitem = new System.Data.DataTable();
            eeda.Fill(eeitem);
            editpurchase.DataSource = eeitem;
            editpurchase.DataBind();

        }

        protected void save_Click(object sender, EventArgs e)
        {

            if (itemquantity.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error','Recieve Quantity Cant be Empty','warning')", true);

                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#newpurchaserecieve').modal('show');", true);

            }
            else if (Convert.ToInt32(itemquantity.Text) <= 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error','Recieve Quantity Shoudnt be 0','warning')", true);
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#newpurchaserecieve').modal('show');", true);

            }
            else if (Convert.ToInt32(itemquantity.Text) > originalQuantity)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error','Recieve Quantity Shoudnt Greater Than Order Quantity','warning')", true);

                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#newpurchaserecieve').modal('show');", true);

            }
            else
            {

                SqlConnection con2 = new SqlConnection(constring);
                con2.Open();
                SqlCommand cmd1 = new SqlCommand("Update PurchaseOrderItems set Quantity='" + Convert.ToInt32(itemquantity.Text) + "' where ProductID='" + Convert.ToInt32(productid.Text) + "' AND PurchaseOrderID='" + Convert.ToInt32(ddlpoid.SelectedItem.Text) + "'", con2);

                cmd1.ExecuteNonQuery();


                con2.Close();

            }
            UpdateQtyAmt(Convert.ToInt32(ddlpoid.SelectedItem.Text));
            LoadEditRecieveItemData(ddlpoid.SelectedItem.Text);
            loadData();

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "newpurchase();", true);

        }

        protected void edit_Click(object sender, EventArgs e)
        {
            string script = "$('#editpurchaserecieve').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupedit", script, true);
            Date1.Value = recievedate.InnerText;
            epoid.Text = PoID.InnerText;
            string po = epoid.Text;
            //status.InnerText;
            esname.Text = vendorname.InnerText;
            EditRecieveItemData(po);


        }

        protected void eeditbtn_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#editreceiveditem').modal('show');", true);
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            int prod_id = Convert.ToInt32(editpurchase.DataKeys[gvrow.RowIndex].Value.ToString());

            string connectionString1 = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL1 = "SELECT ProductID,ItemName,Quantity from PurchaseOrderItems where ProductID='" + prod_id + "' AND PurchaseOrderID='" + Convert.ToInt32(epoid.Text) + "'";
            SqlConnection con1 = new SqlConnection(connectionString1);
            SqlCommand cmd = new SqlCommand(selectSQL1, con1);
            con1.Open();
            SqlDataReader sdr1 = cmd.ExecuteReader();

            sdr1.Read();
            eproductid.Text = prod_id.ToString();
            eitemname.Text = sdr1["ItemName"].ToString();
            equantity.Text = sdr1["Quantity"].ToString();
            qty = Convert.ToInt32(sdr1["Quantity"].ToString());

            con1.Close();
        }

        protected void esave_Click(object sender, EventArgs e)
        {


            if (equantity.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error','Recieve Quantity Cant be Empty','warning')", true);


                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#purchaserecieve').modal('show');", true);

            }
            else if (Convert.ToInt32(equantity.Text) <= 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error','Recieve Quantity Shoudnt be 0','warning')", true);
                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#purchaserecieve').modal('show');", true);

            }

            else
            {
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand("Update PurchaseOrderItems set Quantity='" + Convert.ToInt32(equantity.Text) + "' where ProductID='" + Convert.ToInt32(eproductid.Text) + "'", con);
                SqlCommand cmdd = new SqlCommand("Update PurchaseRecieveItems set ItemQuantity='" + Convert.ToInt32(equantity.Text) + "' where ItemID='" + Convert.ToInt32(eproductid.Text) + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                cmdd.ExecuteNonQuery();

                con.Close();
            }

            UpdateQtyAmt(Convert.ToInt32(epoid.Text));
            EditRecieveItemData(epoid.Text);
            loadData();
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#editpurchaserecieve').modal('show');", true);
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            clearnewpo();
            ClientScript.RegisterStartupScript(this.GetType(), "Ple", "newpurchase();", true);

        }

        protected void editsave_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("Update PurchaseReceives set ReceiveDate='" + Date1.Value.ToString() + "',Notes='" + Textarea1.Value.ToString() + "' where PurchaseOrderID='" + Convert.ToInt32(epoid.Text) + "'", con);
            //SqlCommand cmdd = new SqlCommand("Update PurchaseRecieveItems set ItemQuantity='" + Convert.ToInt32(equantity.Text) + "' where ItemID='" + Convert.ToInt32(eproductid.Text) + "'", con);

            con.Open();
            cmd.ExecuteNonQuery();
            //cmdd.ExecuteNonQuery();
            con.Close();


            loadData();
        }

        protected void canceledit_Click(object sender, EventArgs e)
        {
            rollback(originalQuantity, npid, Convert.ToInt32(ddlpoid.SelectedItem.Text));
            clearnewpo();
        }
        protected void rollback(int originalQty, int proid, int pordid)
        {
            SqlConnection conn = new SqlConnection(constring);
            conn.Open();
            using (SqlCommand revert = new SqlCommand("Update PurchaseOrderItems set Quantity = @originalQuantity where ProductID = @npid AND PurchaseOrderID = @purchaseOrderID", conn))
            {
                // Debugging statements

                // Assuming npid is a variable containing the original ProductID
                revert.Parameters.AddWithValue("@originalQuantity", originalQty);
                revert.Parameters.AddWithValue("@npid", proid);
                revert.Parameters.AddWithValue("@purchaseOrderID", pordid);

                revert.ExecuteNonQuery();
                conn.Close();
            }
        }
        protected void new_Click(object sender, EventArgs e)
        {
            clearnewpo();
        }


        // ---------------------------------------------------------------------------------
        // *********************************************************************************

    }
}
