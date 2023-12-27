 <%@ Page Title="Department" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="HRMS.Department"  %>

<asp:Content ID="conten1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   
    <style>
        .h3, h3 {
            font-size: 1.5rem;
        }
    </style>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script>
        function createModal() {
            $('#createmodal').modal('show');
        }
    </script>
    
    <script>
        function editModal() {
            $('#editmodal').modal('show');
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
            <i data-feather="layers"></i>
            Department
        </h2>

    </div>

  

    <button type="button" data-toggle="modal" data-target="#createmodal" class="btn button-1 createmodal" runat="server" height="29px">+ Create Department</button>

    <asp:Button ID="btnDownload" CssClass="pull-right btn button-1" runat="server" UseSubmitBehavior="false" Text="Download Report" OnClick="btnDownload_Click" CausesValidation="False" />

    <br />
    <br />


   <asp:GridView ID="dept" OnRowEditing="dept_RowEditing" runat="server" AutoGenerateColumns="false" OnRowDeleting="dept_RowDeleting" DataKeyNames="Dept_ID"  ShowHeaderWhenEmpty="true" CssClass="table table-striped  dt-responsive nowrap datatable1" width="100%" EmptyDataText="No Data Available " >
        <Columns>
            <asp:TemplateField HeaderText="Department Name">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Dept_Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Employees">

                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Dept_TotalEmployees") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="POC">

                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Dept_POC") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Delete" >
                <ItemTemplate >
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete"
                        OnClientClick="return confirmdelete(this);">
                    <i class="fa fa-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="editbutton" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("Dept_ID") %>' CommandName="Edit">
                    <i class="fa fa-edit"></i>
                    </asp:LinkButton>
                </ItemTemplate>

            </asp:TemplateField>

        </Columns>
       
    </asp:GridView>

    
       
    <!-- To Create Department Modol Popup -->


    <div class="container">
        <div class="modal fade " data-backdrop="false" id="createmodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">ADD DEPARTMENT</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Department Name</label>
                        <asp:TextBox ID="depName" CssClass="form-control" placeholder="Department Name" runat="server" ></asp:TextBox>
                        <asp:Label ID="dn" runat="server"  ForeColor="Red"></asp:Label>
                        <br /> <label>POC</label>
                        <asp:TextBox ID="poc" CssClass="form-control" placeholder="POC" runat="server" ></asp:TextBox>
                        <asp:Label ID="pc" runat="server"  ForeColor="Red"></asp:Label>
                      <!-- <br /> <label>Is Active</label>
                        <asp:TextBox ID="isActive" CssClass="form-control" placeholder="Is Active" runat="server" ></asp:TextBox>
                        <asp:Label ID="ia" runat="server"  ForeColor="Red"></asp:Label>
                       <br /> <label>Created By</label>
                        <asp:TextBox ID="createdBy" CssClass="form-control" placeholder="Created By" runat="server" ></asp:TextBox>
                        <asp:Label ID="cb" runat="server"  ForeColor="Red"></asp:Label> -->
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--    To Edit Department Modal Popup -->

    <div class="container">
        <div class="modal fade " data-backdrop="false" id="editmodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">EDIT DEPARTMENT</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Department Name</label>
                        <asp:TextBox ID="edepName" CssClass="form-control" placeholder="Department Name" runat="server" ></asp:TextBox>
                        <asp:Label ForeColor="Red" ID="edn" runat="server" ></asp:Label>
                        <br /><label>Total Employees</label>
                        <asp:TextBox Enabled="false" ID="etotEmp" CssClass="form-control" placeholder="Total Employees" runat="server"  ></asp:TextBox>
                        <br /><label>POC</label>
                        <asp:TextBox ID="ePOC" CssClass="form-control" placeholder="POC" runat="server" ></asp:TextBox>
                        <asp:Label ForeColor="Red" ID="epc" runat="server" ></asp:Label>
                        <!-- <br /><label>Is Active</label>
                        <asp:TextBox ID="eisActive" CssClass="form-control" placeholder="Is Active" runat="server"  ></asp:TextBox>
                        <asp:Label ForeColor="Red" ID="eia" runat="server" ></asp:Label>
                        <br /><label>Modified By</label>
                        <asp:TextBox ID="emodifiedBy" CssClass="form-control" placeholder="Modified By" runat="server" ></asp:TextBox>
                        <asp:Label ForeColor="Red" ID="emb" runat="server" ></asp:Label> -->
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="update" OnClick="update_Click" CssClass="btn btn-primary" Text="Update" runat="server"  />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


