<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="HRMS_.Category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <style>
        .h3, h3 {
            font-size: 1.5rem;
        }
        .fa {
            padding-right: 0px;
        }
    </style>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script>
        function createModal() {
            $('#createcategorymodal').modal('show');
        }
    </script>
    
    <script>
        function editModal() {
            $('#editcategorymodal').modal('show');
        }
    </script>
    <script>
        $(document).ready(function () {

            $('#smartwizard').smartWizard({
                selected: 0,
                theme: 'dots',
                autoAdjustHeight: true,
                transitionEffect: 'fade',
                showStepURLhash: false,

            });

        });
    </script>
    <script>
        var object = { status: false, ele: null };
        function confirmdelete(ev) {


            if (object.status) { return true; };

            swal({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#e91d1d',
                cancelButtonColor: '#2a7de5',
                confirmButtonText: "Yes",
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
            <i class="fa fa-code-fork"></i>
            Category
        </h2>

    </div>
    <br />
    <asp:Button ID="addcategory" CssClass="btn button-1" OnClick="addcategory_Click" runat="server" Text="+ Add Category" />
    <asp:Button ID="downloadcategory" CssClass="pull-right btn button-1" OnClick="downloadcategory_Click" runat="server"  Text="Download " />
    <br />
    <br />
    <asp:GridView ID="categorygrid" runat="server" OnRowDeleting="categorygrid_RowDeleting" AutoGenerateColumns="False" CssClass="table table-striped  dt-responsive nowrap datatable1" width="100%" DataKeyNames="Category_ID" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowEditing="categorygrid_RowEditing" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Category Name"><ItemTemplate>
                    <asp:Label ID="categoryname" runat="server" Text='<%# Bind("Category_Name") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Category Description"><ItemTemplate>
                    <asp:Label ID="categorydesc" runat="server" Text='<%# Bind("Category_Desc") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Edit"> <ItemTemplate >
                                        <asp:LinkButton ID="cEdit" runat="server" OnCommand="cEdit_Command" CommandArgument='<%# Eval("Category_ID") %>' CommandName="Edit">
                    <i class="fa fa-edit"></i>
                    </asp:LinkButton>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Delete"> <ItemTemplate >
                                        <asp:LinkButton ID="cDelete" runat="server" CommandName="Delete"
                        OnClientClick="return confirmdelete(this);">
                    <i class="fa fa-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate></asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

       
    <!-- To Create Category Modol Popup -->


    <div class="container">
        <div class="modal fade " data-backdrop="false" id="createcategorymodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">ADD CATEGORY</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Category Name</label>
                        <asp:TextBox ID="categoryname" CssClass="form-control" placeholder="Category Name" runat="server" ></asp:TextBox>
                        <asp:Label ID="catname" runat="server"  ForeColor="Red"></asp:Label>
                        <br /> <label>Category Description</label>
                        <asp:TextBox ID="categorydesc" CssClass="form-control" placeholder="Category Description" runat="server" ></asp:TextBox>
                        <asp:Label ID="catdesc" runat="server"  ForeColor="Red"></asp:Label>
                     
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--    To Edit Category Modal Popup -->

    <div class="container">
        <div class="modal fade " data-backdrop="false" id="editcategorymodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">EDIT CATEGORY</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        
                        <label>Category Name</label>
                        <asp:TextBox ID="ecategoryname" CssClass="form-control" placeholder="Category Name" runat="server" ></asp:TextBox>
                        <asp:Label ForeColor="Red" ID="ecatname" runat="server" ></asp:Label>
                        <br /><label>Category Description</label>
                        <asp:TextBox Enabled="true" ID="ecategorydesc" CssClass="form-control" placeholder="Category Description" runat="server"  ></asp:TextBox>
                       <asp:Label ForeColor="Red" ID="ecatdesc" runat="server"  ></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="update" OnClick="update_Click1" CssClass="btn btn-primary" Text="Update" runat="server"  />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

