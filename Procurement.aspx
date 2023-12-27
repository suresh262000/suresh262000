﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Procurement.aspx.cs" Inherits="HRMS_.Procurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <script type="text/javascript">
      function openModal() {
          $('#createmodal1').modal('show');
      }
      function openModal1() {
          $('#createmodal').modal('show');
      }
    
  </script>
      <script src="jquery.js" type="text/javascript"></script>  
    <script type="text/javascript"  lang="js">  
      
    </script> 
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }


    </script>
    <script>
        function valentr() {

            var drop = document.getElementById("ContentPlaceHolder1_DropDownList4").value;
            if (drop != 0) {
                document.getElementById("ContentPlaceHolder1_productname").value = drop;
            }
 
        }
       
    </script>
    <script>
        function valentr2() {
            var drop = document.getElementById("ContentPlaceHolder1_DropDownList1").value;
            if (drop != 0) {
                document.getElementById("ContentPlaceHolder1_dealername").value = drop;
            }
        }
    </script>

    <script>
        function totalprice() {
           
           
            var q1 = document.getElementById("ContentPlaceHolder1_quantity").value;
            
            var p1 = document.getElementById("ContentPlaceHolder1_price").value;
            var total = (q1 * p1);
           
            document.getElementById("ContentPlaceHolder1_testimateprice").value = total;
            document.getElementById("ContentPlaceHolder1_TextBox5").value = total;
        }
    </script>
    <script>
        function totalupdateprice() {
            var q1 = document.getElementById("ContentPlaceHolder1_TextBox4").value;

            var p1 = document.getElementById("ContentPlaceHolder1_TextBox7").value;
            var total = (q1 * p1);
          
            document.getElementById("ContentPlaceHolder1_TextBox8").value = total;
            document.getElementById("ContentPlaceHolder1_TextBox9").value = total;
        }
    </script>
    <style>
        .span{
            color:red;
        }
        .fa {
            padding-right: 0px;
        }
    </style>
   
        <h2 class="header">
            <i class="fa fa-cart-plus"></i>
            Procurement
        </h2>

   
      <div>
        <button type="button" class="btn button-1 createmodal" data-toggle="modal" data-target="#createmodal">
            + Procurement</button>
            <asp:button type="button" style="margin-left:700px;" id="excel"  runat="server" class="btn button-1" OnClick="excel_Click" Text="Download Report"></asp:button>

        
    </div>
    <div class="table-responsive">
        <br />
             
<asp:GridView ID="procurement" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-striped  dt-responsive nowrap datatable1" width="100%" 
    DataKeyNames="Product_ID"  AutoGenerateColumns ="False" OnRowDeleting="procurement_RowDeleting" OnRowCommand="procurement_RowCommand" >
        <Columns>
              <asp:BoundField DataField ="Product_ID" HeaderText ="Order ID" />
              <asp:BoundField DataField ="pid_Fkid" HeaderText ="Product ID" />

            <asp:BoundField DataField ="Category_NAME" HeaderText ="Category NAME" />
            <asp:BoundField DataField ="Subcategory_NAME" HeaderText ="Subcategory NAME" />
            
            <asp:BoundField DataField ="Product_NAME" HeaderText ="Product NAME"  />
            <asp:BoundField DataField ="quantity" HeaderText ="Quantity" />
            <asp:BoundField DataField ="price" HeaderText ="Price" />
            <asp:BoundField DataField ="totalprice" HeaderText ="Totalprice" />
                        <asp:BoundField DataField ="dealer_name" HeaderText ="Dealer" />
            
            
           <asp:TemplateField>
                <HeaderTemplate >Cancel</HeaderTemplate>
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" OnClientClick="return delete_emp(this);"><i class="fa fa-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
           
                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" HeaderText="Edit" SelectImageUrl="~/css/img/dept.jpeg" NewImageUrl="~/css/img/gss-logo.png"  />     
                
         
           
    
                   

            
        </Columns>
    </asp:GridView>     
      
    </div>

    <div class="modal fade" id="createmodal" tabindex="-1" role="dialog" style="z-index:10003" data-backdrop="static" data-keyboard="false" aria-labelledby="CreateTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-content" style ="padding: 6px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h3 class="header" id="CreateModalLabel">
                       Order Product
                    </h3>

                </div>
            
           <div class="form-group">
            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>
                <label for="Employe">Dealer Name</label><span style="color:red">*</span>
               
              <asp:DropDownList class="form-control" ID="DropDownList1" onchange="valentr2()"  runat="server">
                            </asp:DropDownList>

            <asp:TextBox type="text" runat="server" class="form-control" id="dealername" style="display:none"  ></asp:TextBox>
               <asp:Label runat="server" id="Label3" ForeColor="Red"></asp:Label>
              </div>

                    <div class="form-group">
            <label for="Employe">Product Name</label><span style="color:red">*</span>
               
              <asp:DropDownList class="form-control" ID="DropDownList4"  onchange="valentr()" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                <asp:TextBox type="text" runat="server" class="form-control" id="productname" style="display:none"  ></asp:TextBox>
               <asp:Label runat="server" id="errorid" ForeColor="Red">


               </asp:Label>
        </div>
        <div class="form-group">
            <%--<input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">--%>

            <label for="SalaryAmount">Product ID</label><span style="color:red">*</span>
            
            <asp:TextBox type="text" runat="server" class="form-control"  id="Product_ID"  ReadOnly="True"></asp:TextBox>
        </div>

        
        <div class="form-group">
            <label for="BonusAmount">Category Name</label><span style="color:red">*</span>
            
           <asp:TextBox ID="Category_name" runat="server" class="form-control"  style="width:100%" type="text" ReadOnly="True"></asp:TextBox>


            
        </div>
        <div class="form-group">
            <label for="BonusAmount">Sub Category</label><span style="color:red">*</span>
           <asp:TextBox ID="subcategory_name" runat="server" class="form-control"  style="width:100%" type="text" ReadOnly="True"  ></asp:TextBox>

            
            
           
            
        </div>
                


        <div class="form-group">
            <label for="leavetype">Quantity</label><span style="color:red">*</span>
                                <asp:TextBox type="number" runat="server"  onchange="totalprice()" class="form-control"   id="quantity"></asp:TextBox>
             <asp:Label runat="server" id="Label2" ForeColor="Red"></asp:Label>
            </div>

        <div class="form-group">
            <label for="reason"> Estimated Price</label><span style="color:red">*</span>
             <asp:TextBox type="Text" runat="server" class="form-control"  id="price" ReadOnly="True"></asp:TextBox>

        </div>
       <div class="form-group">
            <label for="reason">Total Estimation Price</label><span style="color:red">*</span>
             <asp:TextBox type="Text" runat="server" class="form-control" id="testimateprice" ReadOnly="True"></asp:TextBox>
                           <asp:TextBox type="text" runat="server" class="form-control" id="TextBox5" style="display:none"  ></asp:TextBox>


        </div>
                
                <div class="modal-footer">
                    <asp:button type="button"  runat="server" class="btn button-1"  OnClick="Unnamed_Click" Text="Close"></asp:button>
                    <asp:button type="button" runat="server"  class="btn button-1 " OnClick="apply_Click" Text="Order" id="apply"></asp:button>
                </div>
            
        </div>
    </div>
        </div>
    <div class="modal fade" id="createmodal1" tabindex="-1" role="dialog" style="z-index:10003" data-backdrop="static" data-keyboard="false" aria-labelledby="CreateTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-content" style ="padding: 6px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h3 class="header" id="CreateModalLabel1">
                       Edit Order Details
                    </h3>

                </div>
                <div class="form-group">
            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>

            <label for="ID">Dealer Name</label>
               <asp:TextBox ID="TextBox11"  type="text" class="form-control"  runat="server" ReadOnly="True" ></asp:TextBox>
        </div>
           <div class="form-group">
            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>

            <label for="ID">Product Name</label>
               <asp:TextBox ID="TextBox6"  type="text" class="form-control"  runat="server" ReadOnly="True" ></asp:TextBox>
        </div>
           <div class="form-group">
            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>

            <label for="Payroll_ID">Product ID</label>
               <asp:TextBox ID="TextBox1"  type="text" class="form-control"  runat="server" ReadOnly="True" ></asp:TextBox>
                                                     <asp:TextBox type="text" runat="server" class="form-control" id="TextBox10" style="display:none"  ></asp:TextBox>

        </div>
        <div class="form-group">
            <%--<input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">--%>

            <label for="SalaryAmount">Category Name</label>
            <asp:TextBox type="text" runat="server" class="form-control" id="TextBox2" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="BonusAmount">Sub Category Name</label><span style="color:red">*</span>
            
           <asp:TextBox ID="TextBox3" runat="server" class="form-control" placeholder="From" style="width:100%" type="text" ReadOnly="True"></asp:TextBox>
         
            
        </div>
        <div class="form-group">
            <label for="BonusAmount">Quantity</label><span style="color:red">*</span>
           <asp:TextBox ID="TextBox4" runat="server" class="form-control"  style="width:100%" onchange="totalupdateprice()" type="number" ></asp:TextBox>
                                                                <asp:TextBox type="text" runat="server" class="form-control" id="TextBox12" style="display:none"  ></asp:TextBox>

                           <asp:Label runat="server" id="Label1" ForeColor="Red"></asp:Label>

           
            
        </div>
                  <div class="form-group">
            <label for="BonusAmount">Price</label><span style="color:red">*</span>
           <asp:TextBox ID="TextBox7" runat="server" class="form-control" placeholder="From" style="width:100%" type="number"  ReadOnly="True"></asp:TextBox>
           
            
           
            
        </div>

                 <div class="form-group">
            <label for="BonusAmount">Total Estimated Price</label><span style="color:red">*</span>
           <asp:TextBox ID="TextBox8" runat="server" class="form-control" placeholder="From" style="width:100%" type="number"  ReadOnly="True"></asp:TextBox>
                                      <asp:TextBox type="int" runat="server" class="form-control" id="TextBox9" style="display:none"  ></asp:TextBox>

            
           
            
        </div>
      
       
                
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary closeing" data-dismiss="modal">Close</button>
                    <asp:button type="button" runat="server" OnClick="Button1_Click" class="btn button-1 " Text="Update" id="Button1"></asp:button>
                </div>
            
        </div>
    </div>
            
        </div>
</asp:Content>

