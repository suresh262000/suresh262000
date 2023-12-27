<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Dealer.aspx.cs" Inherits="HRMS_.Dealer" %>
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
            $('#createdealermodal').modal('show');
        }
    </script>
    
    <script>
        function editModal() {
            $('#editDealermodal').modal('show');
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
            <i class="fa fa-handshake-o"></i>
            Dealer
        </h2>

    </div>


    
        <asp:Button ID="adddealer" onclick="adddealer_Click" Text="+ Add Dealer" runat="server" CssClass="btn button-1" />

        <asp:Button ID="downloaddealer" onclick="downloaddealer_Click" Text="Download Report" runat="server" CssClass="pull-right btn button-1" />
        <br />
        <br />



        <asp:GridView ID="dealergrid" OnRowEditing="dealergrid_RowEditing" OnRowDeleting="dealergrid_RowDeleting" runat="server"
           AutoGenerateColumns="False" DataKeyNames="Dealer_ID" ShowHeaderWhenEmpty="true"  EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-VerticalAlign="Middle"
            EmptyDataText="No Data Available" 
            Width="100%" CssClass="table table-striped  dt-responsive nowrap datatable1"  AllowCustomPaging="True">

            <Columns>

                <%--<asp:TemplateField HeaderText="Dealer ID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="basedealer" Text='<%# Bind("BaseCategory_Name") %>'></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Dealer Name">
                    <ItemTemplate>
                        <asp:Label runat="server"  ID="baseDealer" Text='<%# Bind("Dealer_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>




                <asp:TemplateField HeaderText="Dealer address">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="basedealeradd" Text='<%# Bind("Dealer_Address") %>'>></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>



                  <asp:TemplateField HeaderText="Contact">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="dcontact" Text='<%# Bind("Dealer_Contact") %>'>></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>



                  <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="demail" Text='<%# Bind("Dealer_Gmail") %>'>></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>



                <%--  <asp:TemplateField HeaderText="Created Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="subcategorydesc" Text='<%# Bind("SubCategory_Desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <%--  <asp:TemplateField HeaderText="Created By">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="dealerby" Text='<%# Bind("SubCategory_Desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

               <%--  <asp:TemplateField HeaderText="Gmail">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="demail" Text='<%# Bind("SubCategory_Desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" oncommand="dealeredit_Command" CommandArgument='<%# Eval("Dealer_ID") %>' CssClass="fa fa-edit" ID="dealeredit" CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>



                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton runat="server"  OnClientClick="return confirmdelete(this);" CssClass="fa fa-trash" ID="subcategorydelete" CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>



             <EditRowStyle BackColor="#F7F6F3" />
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

    <asp:ScriptManager ID ="script1" runat ="server"></asp:ScriptManager>

         <!-- To Create Dealer Modol Popup -->

    <div class="container">
        <div class="modal fade " data-backdrop="false" id="createdealermodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Add Dealer</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">

                        <label>Dealer Name</label>
                        <asp:TextBox ID="dealername" CssClass="form-control" placeholder="Dealer Name" runat="server" ></asp:TextBox>
                     
                        <asp:Label ID="dname" runat="server"  ForeColor="Red"></asp:Label>
                        <br />

                        <label>Address</label>
                        <asp:TextBox ID="dealeraddress" CssClass="form-control" placeholder="Dealer Address" runat="server" ></asp:TextBox>
                        <asp:Label ID="dadd" runat="server"  ForeColor="Red"></asp:Label>
                         <br />

                         <label>Contact</label>
                        <asp:TextBox  TextMode="Number" ID="dcontact" onkeypress="return this.value.length<=9" CssClass="form-control" placeholder="Dealer Contact" runat="server" ></asp:TextBox>
                        <asp:Label ID="dcont" runat="server"  ForeColor="Red"></asp:Label>
                         <br />

                          <label>Email</label>
                        <asp:TextBox ID="demail" CssClass="form-control" placeholder="Dealer Email" runat="server" ></asp:TextBox>
                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Id" ControlToValidate="demail" ValidationExpression='"^\S+@\S+$"'></asp:RegularExpressionValidator>--%>
                        <asp:Label ID="dmail" runat="server"  ForeColor="Red"></asp:Label>




                     
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

          <!--    To Edit Dealer Modal Popup -->

    <div class="container">
        <div class="modal fade " data-backdrop="false" id="editDealermodal" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Edit Dealer</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        
                        <label>Dealer Name</label>
                        <asp:TextBox ID="edealername" CssClass="form-control" placeholder="Dealer Name" runat="server" ></asp:TextBox>
                        <asp:Label ForeColor="Red" ID="ecatname" runat="server" ></asp:Label>

                        <br />
                        <label>Address</label>
                        <asp:TextBox  Enabled="true" ID="edealeradd" CssClass="form-control" placeholder="Dealer Address" runat="server"  ></asp:TextBox>
                       <asp:Label ForeColor="Red" ID="ecatadd" runat="server"  ></asp:Label>

                          <br />
                        <label>Contact</label>
                        <asp:TextBox  onkeypress="return this.value.length<=9" TextMode="Number"  Enabled="true" ID="edealercont" CssClass="form-control" placeholder="Dealer Contact" runat="server"  ></asp:TextBox>
                       <asp:Label ForeColor="Red" ID="edealerco" runat="server"  ></asp:Label>

                          <br />
                        <label>Email</label>
                        <asp:TextBox Enabled="true" ID="email" CssClass="form-control" placeholder="Dealer Email" runat="server"  ></asp:TextBox>
                       <asp:Label ForeColor="Red" ID="emill" runat="server"  ></asp:Label>



                    </div>

                    <div class="modal-footer">
                        <button class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="update" onclick="update_Click" CssClass="btn btn-primary" Text="Update" runat="server"  />
                    </div>
                </div>
            </div>
        </div>
    </div>







</asp:Content>

