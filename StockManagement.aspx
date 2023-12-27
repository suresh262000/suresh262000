<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="StockManagement.aspx.cs" Inherits="HRMS_.StockManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_container">
        <h2 class="header">
            <i data-feather="users"></i>
            Stock Management
        </h2>
<%--        <div>--%>
<%--                Select Category
    <asp:DropDownList ID="ddlCustomers" runat="server" AutoPostBack = "true" >
    </asp:DropDownList>       
    Select SubCategory
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack = "true">
    </asp:DropDownList>--%> 
     <%--<a class="pull-right btn button-1" OnClick="excel_Click1" ID="LinkButton3" runat="server">Download Report</a>--%>
    
<%--<asp:LinkButton ID="LinkButton2" class="pull-right btn button-1"  runat="server" OnClick="excel_Click1">Download Report</asp:LinkButton>--%>

<%--    </div>   --%>
   
         <div class="table-responsive">
                     <asp:LinkButton class="pull-right btn button-1" OnClick="Stock_report_download" ID="Stock_report" runat="server" ToolTip="Click">Download Report</asp:LinkButton>

        <br />
        <br />

     <asp:GridView ID="StockmanagementTable" EmptyDataText="No Data Available" OnRowDataBound="Stockqty_RowDataBound" runat="server" CssClass="table table-striped  dt-responsive nowrap datatable1"  width="100%" cellspacing="0" AutoGenerateColumns ="false" >
           <%-- Theme Properties --%>

         <Columns>
             <asp:BoundField DataField ="Product_ID" HeaderText ="Product Id" />
            <asp:BoundField DataField ="Product_Name" HeaderText ="Product Name" />
            <asp:BoundField DataField ="Category_Name" HeaderText ="Category Name" />
            <asp:BoundField DataField ="SubCategory_Name" HeaderText ="SubCategory Name" />
            <asp:BoundField DataField ="Category_ID" HeaderText ="Category Id" />
            <asp:BoundField DataField ="SubCategory_ID" HeaderText ="SubCategory Id"  />
            <asp:BoundField DataField ="Net_Qty" HeaderText ="Qty" />
            <asp:BoundField DataField ="Net_Rate" HeaderText ="Rate" />

<%--             <asp:TemplateField HeaderText="Delete">--%>
<%--                                    <ItemTemplate>
<asp:LinkButton ID="lnkdelete" class="fa fa-trash" OnClientClick="return delete_emp(this);" runat="server" ></asp:LinkButton>
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
        
                            <ItemTemplate>
<asp:LinkButton ID="LinkButton1" class="fa fas fa-edit" runat="server" ></asp:LinkButton>
</ItemTemplate>--%>

<%--</asp:TemplateField>--%>
            
        </Columns>
    </asp:GridView>  
    </div>    

    </div>

</asp:Content>

