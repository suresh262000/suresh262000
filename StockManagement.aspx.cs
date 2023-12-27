using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using System.Drawing;

namespace HRMS_
{
    public partial class StockManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            get_data();
          
        }
        //To get stocks
        public void get_data()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            string selectSQL = "Select * from Tbl_Products tp left join Category ct on tp.Category_ID = ct.Category_ID " +
                "left join SubCategory sct on tp.SubCategory_ID = sct.SubCategory_ID";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            StockmanagementTable.DataSource = ds;
            StockmanagementTable.DataBind();

            StockmanagementTable.UseAccessibleHeader = true;
            if (StockmanagementTable.Rows.Count > 0)
            {
                StockmanagementTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                StockmanagementTable.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        //To Excel_download
        protected void Stock_report_download(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HRMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select tp.Product_ID,tp.Product_Name,tp.Category_ID,ct.Category_Name," +
                    "sct.SubCategory_ID, sct.SubCategory_Name, tp.Net_Qty, tp.Net_Rate from Tbl_Products tp left " +
                    "join Category ct on tp.Category_ID = ct.Category_ID left join SubCategory sct on tp.SubCategory_ID = sct.SubCategory_ID"))
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
                                var ws=wb.Worksheets.Add(dt, "stock");
                                if (dt.Rows.Count > 0)
                                {
                                    // Adding HeaderRow.
                                    ws.Cell("A" + 1).Value = dt.Columns[0].ColumnName;
                                    ws.Cell("B" + 1).Value = dt.Columns[1].ColumnName;
                                    ws.Cell("C" + 1).Value = dt.Columns[2].ColumnName;
                                    ws.Cell("D" + 1).Value = dt.Columns[3].ColumnName;
                                    ws.Cell("E" + 1).Value = dt.Columns[4].ColumnName;
                                    ws.Cell("F" + 1).Value = dt.Columns[5].ColumnName;
                                    ws.Cell("G" + 1).Value = dt.Columns[6].ColumnName;

                                    // Adding DataRows.
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        ws.Cell("A" + (i + 2)).Value = dt.Rows[i][0];
                                        ws.Cell("B" + (i + 2)).Value = dt.Rows[i][1];
                                        ws.Cell("C" + (i + 2)).Value = dt.Rows[i][2];
                                        ws.Cell("D" + (i + 2)).Value = dt.Rows[i][3];
                                        ws.Cell("E" + (i + 2)).Value = dt.Rows[i][4];
                                        ws.Cell("F" + (i + 2)).Value = dt.Rows[i][5];
                                        ws.Cell("G" + (i + 2)).Value = dt.Rows[i][6];


                                        if (dt.Rows[i]["Net_Qty"].ToString() == "0")
                                        {
                                            // Changing color to Red.
                                            ws.Cells("G" + (i + 2)).Style.Fill.BackgroundColor = XLColor.Red;
                                        }
                                        else if (Convert.ToInt32( dt.Rows[i]["Net_Qty"]) > 0 && Convert.ToInt32(dt.Rows[i]["Net_Qty"]) < 10)
                                        {
                                            //Changing color to Green.
                                            ws.Cells("G" + (i + 2)).Style.Fill.BackgroundColor = XLColor.Orange;
                                        }
                                    }
                                }
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=StockManagement.xlsx");
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
        //To Highlight the color in grid view
        protected void Stockqty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int qty = Convert.ToInt32(e.Row.Cells[6].Text.ToString());
                //if (qty > 0 )
                if (qty > 0 && qty <= 5)
                {
                    e.Row.Cells[6].BackColor = Color.Orange;
                    //e.Row.BackColor = Color.Orange;
                }
                if (qty == 0)
                {
                    e.Row.Cells[6].BackColor = Color.Red;
                    //e.Row.BackColor = Color.Red;
                }
            }
        }

    }
    
}
