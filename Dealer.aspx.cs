using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRMS_
{
    public partial class Dealer : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                loadDealer();
                clearCreateDep();
                cleareditDep();
            }

           



        }


        protected void loadDealer()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Select * From DealerMaster", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dealergrid.DataSource = ds;
                dealergrid.DataBind();
            }
            if (dealergrid.FooterRow != null)
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                dealergrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


        protected void clearCreateDep()
        {
            dname.Text = string.Empty;
            dadd.Text = string.Empty;
            dcont.Text = string.Empty;
            dmail.Text = string.Empty;
           

        }



        protected void cleareditDep()
        {
            edealername.Text = string.Empty;
            edealeradd.Text = string.Empty;
            edealercont.Text = string.Empty;
            email.Text = string.Empty;
            


        }










        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (dealergrid.FooterRow != null)
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                dealergrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            clearcreateError();
            try
            {





                if (dealername.Text == string.Empty)
                {
                    dname.Text = "Please Enter Dealer Name";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }
                else if (dealeraddress.Text == string.Empty)
                {
                    dadd.Text = "Please Enter Address";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }
                else if (dcontact.Text == string.Empty)
                {
                    dcont.Text = "Please Enter  Contact ";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }
                else if (demail.Text == string.Empty)
                {
                    dmail.Text = "Please Enter  Mail ";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                    return;

                }

                //else if (dealerby.Text == string.Empty)
                //{
                //    dby.Text = "Please Enter Category Description";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "createModal();", true);
                //    return;

                //}


                else
                {
                    clearcreateError();
                }

                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                string nowDateTime = DateTime.Now.ToString("yyyy-MM-dd");
                string un = Session["username"].ToString();
                //string formattedDateTime = nowDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                SqlCommand cmd = new SqlCommand("Insert into DealerMaster (Dealer_Name, Dealer_Address ,Dealer_Contact ,Dealer_Gmail ,Created_Date ,Created_By,Is_Active) Values" +
                 " ('" + dealername.Text + "','" + dealeraddress.Text + "','" + dcontact.Text + "','" + demail.Text + "','" + nowDateTime + "','" + un + "','" + 1 + "')", con);
                int i = cmd.ExecuteNonQuery();
                loadDealer();
                clearCreateDep();
                con.Close();
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Created','Dealer Added Successfully!','success')", true);
                }
                clearCreateDep();
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Creating!','error')", true);
            }

        }

        protected void dealergrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (dealergrid.FooterRow != null)
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                dealergrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            try
            {


                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from DealerMaster where Dealer_ID=@delid ", con);
                cmd.Parameters.AddWithValue("@delid", dealergrid.DataKeys[e.RowIndex].Value.ToString());


                int r = cmd.ExecuteNonQuery();
                loadDealer();
                con.Close();
                if (r > 0)

                {

                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Deleted!','Dealer has been deleted!','success')", true);

                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Foreign Key Constraint Violation','Cannot Delete Record.Associated Data In Use.','error')", true);
            }


        }

        protected void downloaddealer_Click(object sender, EventArgs e)
        {
            DataTable dtexcel = new DataTable();
            // GridView gv = new GridView();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select Dealer_ID as [Dealer ID], Dealer_Name as [Dealer Name],Dealer_Address as [Dealer Address],Dealer_Contact as [Dealer Contact]," +
                    "Dealer_Gmail as [Dealer Gmail],Created_Date as [Created Date],Created_By as [Created By],Modified_By as [Modified By],Modified_Date as [Modified Date],Is_Active as [Is Active] from DealerMaster", con);
                da.Fill(dtexcel);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtexcel, "Dealer");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=DealerDetails.xlsx");
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
            ecatname.Text = "";
            ecatadd.Text = "";
            edealerco.Text = "";
            emill.Text = "";
        }




        private void clearcreateError()
        {
            dname.Text = "";
            dadd.Text = "";
            dcont.Text = "";
            dmail.Text = "";
           
        }

        protected void dealeredit_Command(object sender, CommandEventArgs e)
        {
            string cid = e.CommandArgument.ToString();

            SqlConnection con1 = new SqlConnection(connectionString);


            con1.Open();


            SqlCommand cmd1 = new SqlCommand("Select * from DealerMaster where Dealer_ID=@cid", con1);
            cmd1.Parameters.AddWithValue("@cid", cid);
            SqlDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {

                edealername.Text = dr["Dealer_Name"].ToString();
                edealeradd.Text = dr["Dealer_Address"].ToString();
                edealercont.Text = dr["Dealer_Contact"].ToString();
                email.Text = dr["Dealer_Gmail"].ToString();
              


            }
            dr.Close();


        }

        protected void dealergrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string script = "$('#editDealermodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupedit", script, true);
            if (dealergrid.FooterRow != null)
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                dealergrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void update_Click(object sender, EventArgs e)
        {
            if (dealergrid.FooterRow != null)
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                dealergrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //cleareditError();
            try
            {
                //  if (edealername.Text == string.Empty)
                //{
                //    ecatname.Text = "Please Enter Dealer Name";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                //    return;

                //}
                //else if (edealeradd.Text == string.Empty)
                //{
                //    ecatadd.Text = "Please Enter Address";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                //    return;

                //}
                //else if (edealercont.Text == string.Empty)
                //{
                //    edealerco.Text = "Please Enter  Contact ";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                //    return;

                //}
                //else if (email.Text == string.Empty)
                //{
                //    emill.Text = "Please Enter  Mail ";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                //    return;

                //}


                var DName = edealername.Text;


                if (DName == "")
                {
                    ecatname.Text = " Please enter Dealer name";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", "editModal();", true);
                    return;
                }
                else
                {
                    ecatname.Text = "";
                }

                var Daddress = edealeradd.Text;
                if (Daddress == "")
                {
                    ecatadd.Text = " Please enter Address";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", "editModal();", true);
                    return;
                }
                else
                {
                    ecatadd.Text = "";
                }
                var DCon = edealercont.Text;
                if (DCon == "")
                {
                    edealerco.Text = " Please Select Contact";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", "editModal();", true);
                    return;
                }
                else
                {
                    edealerco.Text = "";
                }
                var EMail = email.Text;
                if (EMail == "")
                {
                    emill.Text = " Please Select Email";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", "editModal();", true);
                    return;
                }
                else
                {
                    emill.Text = "";
                }



                //if (edealername.Text == string.Empty)
                //{
                //    ecatname.Text = "Please Enter Dealer Name";
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                //    return;
                //}

                    //else if (edealeradd.Text == string.Empty)
                    //{

                    //    ecatadd.Text = "Please Enter Dealer Address";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    //    return;

                    //}
                    //else if (edealercont.Text == string.Empty)
                    //{

                    //    edealerco.Text = "Please Enter Dealer Contact";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    //    return;

                    //}
                    //else if (email.Text == string.Empty)
                    //{

                    //    emill.Text = "Please Enter Dealer Email";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "editModal();", true);
                    //    return;

                    //}


                //else
                //{
                //    cleareditError();

                //}
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string nowDateTime = DateTime.Now.ToString("yyyy-MM-dd");
                string un = Session["username"].ToString();
                SqlCommand cmd = new SqlCommand("Update DealerMaster Set Dealer_Name=@dname,Dealer_Address=@dadd,Dealer_Contact=@dcont,Dealer_Gmail=@gmail," +
                    "Modified_By=@catmodifiedby,Modified_Date=@catmodifieddate where Dealer_ID=@id ", con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dealergrid.DataKeys[dealergrid.EditIndex].Value));
                cmd.Parameters.AddWithValue("@dname", edealername.Text);
                cmd.Parameters.AddWithValue("@dadd", edealeradd.Text);
                cmd.Parameters.AddWithValue("@dcont", edealercont.Text);
                cmd.Parameters.AddWithValue("@gmail", email.Text);
                


                cmd.Parameters.AddWithValue("@catmodifiedby", un);
                cmd.Parameters.AddWithValue("@catmodifieddate", nowDateTime);
              
                int r = cmd.ExecuteNonQuery();



                loadDealer();
                cleareditDep();
                con.Close();
                if (r > 0)
                {
                    ClientScript.RegisterClientScriptBlock
                    (this.GetType(), "K", "swal('Updated!','Dealer has been Updated !','success')", true);
                }

            }
            catch (Exception ex)
            {

                ClientScript.RegisterClientScriptBlock
                   (this.GetType(), "K", "swal('Error','Error Occured While Updating!','error')", true);
            }

        }

        protected void adddealer_Click(object sender, EventArgs e)
        {
            string script = "$('#createdealermodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popupedit", script, true);
            if (dealergrid.FooterRow != null)
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                dealergrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                dealergrid.UseAccessibleHeader = true;
                dealergrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}
    
    























    
    



