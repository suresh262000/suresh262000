<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="HRMS_.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
        .modal-title {
            font-size: 16px;
            margin-left: 100px;
            margin-top: 0;
        }

        .fa {
            padding-right: 0px;
        }

        .input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#Updatemodal').modal('show');
        }

        function opencreatModal() {
            $('#createmodal').modal('show');
        }
    </script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script>
        function reload() {
            location.reload();
        }
    </script>
    <script>
        var object = { status: false, ele: null };
        function delete_emp(ev) {


            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#e91d1d',
                cancelButtonColor: '#2a7de5',
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "Cancel",
                closeOnConfirm: true
            },
                function () {
                    object.status = true;
                    object.ele = ev;
                    object.ele.click();
                });

            return false;
        };

    </script>

    <div class="main_container">
        <h2 class="header">
            <i class="fa fa-cubes"></i>
            Products
        </h2>
    </div>
    <div>
        <button type="button" class="btn button-1 createmodal" data-toggle="modal" data-target="#createmodal">
            + Add Products
        </button>
        <asp:Button runat="server" class="pull-right btn button-1" data-toggle="modal" OnClick="Excel_download" Text="Download Report"  />
    </div>
    <%--gridview--%>
    <div class="table-responsive">
        <br />

        <asp:GridView ID="ProductTable" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-striped  dt-responsive nowrap datatable1" Width="100%" CellSpacing="0" DataKeyNames="Product_ID" OnRowDeleting="ProductTable_RowDeleting"  AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Product_ID" HeaderText="Product ID" />
                <asp:BoundField DataField="Product_Name" HeaderText="Product Name" />
                <asp:BoundField DataField="Category_Name" HeaderText="Category"  />
                <asp:BoundField DataField="SubCategory_Name" HeaderText="Sub Category"  />
                <asp:BoundField DataField="Net_Qty" HeaderText="Net Qty" />
                <asp:BoundField DataField="Net_Rate" HeaderText="Net Rate" />
               
                <%--<asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="lbseq" Value='<%#Eval("Emp_MasterID")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField>
                    <HeaderTemplate>Delete</HeaderTemplate>
                    <ItemTemplate>
                        <%--<a CommandName="Delete">
                        <i class="fa fa-trash"></i></a>--%>
                        <asp:LinkButton ID="deletebtn" runat="server" CommandName="Delete" OnClientClick="return delete_emp(this);"><i class="fa fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Edit</HeaderTemplate>
                    <ItemTemplate>
                        <%--<a href="#"> 
                        <i class="fa fas fa-edit"></i></a>--%>
                        <asp:LinkButton ID="editbtn" runat="server" OnClick="Get_product_data" ><i class="fa fa-edit"  ></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        </div>

    <%------------create product---------------%>
    <div class="container">
        <div class="modal fade " data-backdrop="false" id="createmodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Add Product</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Product Name</label>
                        <asp:TextBox ID="prodNametxt" CssClass="form-control" placeholder="Product Name" runat="server" ></asp:TextBox>
                        <asp:Label ID="PN" runat="server"  ForeColor="Red"></asp:Label>

                        <br /> <label>Category</label>
                        <asp:DropDownList ID="categorydrpdwn" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="categorydrpdwn_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:Label ID="CT" runat="server"  ForeColor="Red"></asp:Label>

                       <br /> <label>Sub Category</label>
                        <asp:DropDownList ID="subcatdrpdwn" runat="server"  CssClass="form-control">
                        </asp:DropDownList>
                        <asp:Label ID="SCT" runat="server"  ForeColor="Red"></asp:Label>

                       <%--<br /> <label>Quantity</label>
                        <asp:TextBox ID="Qtytxt" TextMode="Number" CssClass="form-control" placeholder="Quantity" runat="server" ></asp:TextBox>
                        <asp:Label ID="QTY" runat="server"  ForeColor="Red"></asp:Label>--%> 

                        <br /> <label>Rate</label>
                        <asp:TextBox ID="Ratetxt" TextMode="Number" CssClass="form-control" placeholder="Rate" runat="server" ></asp:TextBox>
                        <asp:Label ID="RT" runat="server"  ForeColor="Red"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click"  Text="Save" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%------------Update product---------------%>
    <div class="container">
        <div class="modal fade " data-backdrop="false" id="Updatemodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Edit Product</h4>
                        <button type="button" class="close" onclick="reload()" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Product Name</label>
                        <div style="display: none">
                        <asp:TextBox ID="prod_idtxt1" CssClass="form-control"  runat="server" ></asp:TextBox>
                        </div>
                        <asp:TextBox ID="prodNametxt1" CssClass="form-control" placeholder="Product Name" runat="server" ></asp:TextBox>
                        <asp:Label ID="PN1" runat="server"  ForeColor="Red"></asp:Label>

                        <br /> <label>Category</label>
                        <asp:DropDownList ID="categorydrpdwn1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="categorydrpdwn1_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:Label ID="CT1" runat="server"  ForeColor="Red"></asp:Label>

                       <br /> <label>Sub Category</label>
                        <asp:DropDownList ID="subcatdrpdwn1" runat="server"  CssClass="form-control">
                        </asp:DropDownList>
                        <asp:Label ID="SCT1" runat="server"  ForeColor="Red"></asp:Label>

                       <%--<br /> <label>Quantity</label>
                        <asp:TextBox ID="Qtytxt1" TextMode="Number" CssClass="form-control" placeholder="Quantity" runat="server" ></asp:TextBox>
                        <asp:Label ID="QTY1" runat="server"  ForeColor="Red"></asp:Label>--%> 

                        <br /> <label>Rate</label>
                        <asp:TextBox ID="Ratetxt1" TextMode="Number" CssClass="form-control" placeholder="Rate" runat="server" ></asp:TextBox>
                        <asp:Label ID="RT1" runat="server"  ForeColor="Red"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button"  class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="Btn_edit" CssClass="btn btn-primary" OnClick="update_Click"  Text="Save" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

