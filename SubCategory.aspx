<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SubCategory.aspx.cs" Inherits="HRMS_.SubCategory" %>
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
            $('#createsubcategorymodal').modal('show');
        }
    </script>
    
    <script>
        function editModal() {
            $('#editsubcategorymodal').modal('show');
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
            <i class="fa fa-sitemap"></i>
            Sub Category
        </h2>

    </div>
    
        <asp:Button ID="addsubcategory" OnClick="addsubcategory_Click" Text="+ Add SubCategory" runat="server" CssClass="btn button-1" />
        <asp:Button ID="downloadsubcategory" OnClick="downloadsubcategory_Click" Text="Download" runat="server" CssClass="pull-right btn button-1" />
        <br />
        <br />
        <asp:GridView ID="subcategorygrid" OnRowEditing="subcategorygrid_RowEditing" OnRowDeleting="subcategorygrid_RowDeleting" runat="server" AutoGenerateColumns="False" DataKeyNames="SubCategory_ID" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Available" Width="100%" CssClass="table table-striped  dt-responsive nowrap datatable1">
            <Columns>
                <asp:TemplateField HeaderText="Category Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="basecategoryname" Text='<%# Bind("BaseCategory_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub category Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="subcategoryname" Text='<%# Bind("SubCategory_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub category Desc">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="subcategorydesc" Text='<%# Bind("SubCategory_Desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" OnCommand="subcategoryedit_Command" CommandArgument='<%# Eval("SubCategory_ID") %>' CssClass="fa fa-edit" ID="subcategoryedit" CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton runat="server"  OnClientClick="return confirmdelete(this);" CssClass="fa fa-trash" ID="subcategorydelete" CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>




     <!-- To Create Sub Category Modol Popup -->


    <div class="container">
        <div class="modal fade " data-backdrop="false" id="createsubcategorymodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">ADD SUB CATEGORY</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Category Name</label><br />
                        <asp:DropDownList CssClass="well-sm" Width="100%" DataTextField="Category_Name" DataValueField="Category_ID" ID="basecategoryname" runat="server"></asp:DropDownList>
                       <br /> <asp:Label ForeColor="Red" ID="bcatname" runat="server" ></asp:Label>
                         <br /><label>Sub Category Name</label>
                        <asp:TextBox ID="subcategoryname" CssClass="form-control" placeholder="Sub Category Name" runat="server" ></asp:TextBox>
                        <asp:Label ForeColor="Red" ID="scatname" runat="server" ></asp:Label>
                       <br /> <label>Sub Category Description</label>
                        <asp:TextBox ID="subcategorydesc" CssClass="form-control" placeholder="Sub Category Desc" runat="server" ></asp:TextBox>
                        <asp:Label ID="scatdesc" runat="server"  ForeColor="Red"></asp:Label>
                       
                       
                     
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="savesubcategory" OnClick="savesubcategory_Click" CssClass="btn btn-primary" Text="Save" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    <!--    To Edit Sub Category Modal Popup -->

    <div class="container">
        <div class="modal fade " data-backdrop="false" id="editsubcategorymodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">EDIT SUB CATEGORY</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                         <label>Base Category Name</label><br />
                        <asp:DropDownList Width="100%" DataTextField="Category_Name" CssClass="well-sm" DataValueField="Category_ID"  ID="ebcategoryname" runat="server"></asp:DropDownList>
                        <br /><asp:Label ForeColor="Red" ID="ebcatname" runat="server" ></asp:Label>
                        <br /><label>Sub Category Name</label>
                        <asp:TextBox ID="escategoryname" Enabled="true" CssClass="form-control" placeholder="" runat="server" ></asp:TextBox>
                        <asp:Label  ForeColor="Red" ID="escatname" runat="server" ></asp:Label>
                        <br /><label>Sub Category Description</label>
                        <asp:TextBox ID="escategorydesc" Enabled="true" CssClass="form-control" placeholder="" runat="server" ></asp:TextBox>
                        <asp:Label  ForeColor="Red" ID="escatdesc" runat="server" ></asp:Label>
                       
                        
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="subupdate" OnClick="subupdate_Click"  CssClass="btn btn-primary" Text="Update" runat="server"  />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
