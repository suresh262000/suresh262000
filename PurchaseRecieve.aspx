<%@ Page Language="C#" Title="Purchase Recieve" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="PurchaseRecieve.aspx.cs" Inherits="HRMS_.PurchaseRecieve" %>

<asp:Content ID="conten1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <!-- Add this style in the head section or in an external CSS file -->
<style>
    .modal-body-item {
        width: 100%; /* Adjust the width as needed */
        padding: 10px; /* Adjust the padding as needed */
        background-color:aquamarine; /* Set the background color */
        margin-bottom: 10px; /* Add margin between items */
    }
</style>
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


    <script>
        function newpurchase() {

            $('#newpurchaserecieve').modal('show');

        }
    </script>
    <script>
        function purchase() {

            $('#purchaserecieve').modal('show');

        }
    </script>
    <script>
        function editreceivemodal() {

            $('#editreceiveditem').modal('show');

        }
    </script>
    <script>
        function editmodal() {

            $('#edititem').modal('show');

        }
    </script>
    <script>
        function captureModalData(index) {
            // Get data from modal and store it in hidden fields or JavaScript variables
            var pono = document.getElementById('<%= recieveitem.ClientID %>').rows[index + 1].cells[1].innerHTML;


            // Store data in hidden fields
            document.getElementById('<%= hidepono.ClientID %>').value = pono;

        }
    </script>


    <div class="main_container">
        <h2 class="header">
            <i data-feather="layers"></i>
            Purchase Recieves
        </h2>
        <p class="header">
            <button type="button" class="btn btn-primary" onclick="new_Click" data-toggle="modal" data-target="#newpurchaserecieve">New +</button>
        </p>
        <asp:HiddenField ID="hidepono" runat="server" />
    </div>
    <asp:GridView ID="grn" AllowSorting="true" runat="server" ShowHeaderWhenEmpty="True" DataKeyNames="ReceiveID" CssClass="table table-striped  dt-responsive nowrap datatable1" Width="100%" EmptyDataText="No Data Available " AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="GRN No">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="prno" OnClientClick="captureModalData(<%# Container.DataItemIndex %>)" CommandName="GetPO" CommandArgument='<%# Container.DataItemIndex %>' OnClick="prno_Click" Text='<%# Bind("ReceiveID") %>'>></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Recieved Date">
                <ItemTemplate>
                    <asp:Label ID="recievedate" runat="server" Text='<%# Bind("ReceiveDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Suplier Id">
                <ItemTemplate>
                    <asp:Label ID="supplierid" runat="server" Text='<%# Bind("SupplierID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Supplier Name">
                <ItemTemplate>
                    <asp:Label ID="suppliername" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PO Id">
                <ItemTemplate>
                    <asp:Label ID="poid" runat="server" Text='<%# Bind("PurchaseOrderID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Amount">
                <ItemTemplate>
                    <asp:Label ID="totamt" runat="server" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes">
                <ItemTemplate>
                    <asp:Label ID="notes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <!--------------------------------------------------------------------------->
    <!-- To Edit Recieve Item -->
    <!--------------------------------------------------------------------------->

    <div class="container">

        <div class="modal fade " data-backdrop="false" id="edititem" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">EDIT ITEM</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <br/>
                       <label style="width:75px; height:30px;">Product ID</label><br />
                        <asp:TextBox style="width:150px; height:30px;" ID="productid" Enabled="false" runat="server"></asp:TextBox>
                        <br/><br />
                        <label style="width:75px; height:30px;">Item Name</label><br />
                        <asp:TextBox style="width:150px; height:30px;" ID="itemname" Enabled="false" runat="server"></asp:TextBox>
                        <br/><br />
                        <label style="width:75px; height:30px;">Quantity</label><br />
                        <asp:TextBox style="width:150px; height:30px;" ID="itemquantity" Enabled="true" runat="server"></asp:TextBox>

                        <br/><br />
                        <br/>
                        
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="save" CssClass="btn btn-success" runat="server" OnClick="save_Click" Text="Save" />
                    </div>
                </div>
            </div>
        </div>
    </div>

     <!--------------------------------------------------------------------------->
    <!-- To Edit Received Recieve Item -->
    <!--------------------------------------------------------------------------->

    <div class="container">

        <div class="modal fade " data-backdrop="false" id="editreceiveditem" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">EDIT ITEM</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <br/>
                       <label style="width:75px; height:30px;">Product ID</label><br />
                        <asp:TextBox style="width:150px; height:30px;" ID="eproductid" ReadOnly="true" runat="server"></asp:TextBox>
                        <br/><br />
                        <label style="width:75px; height:30px;">Item Name</label><br />
                        <asp:TextBox style="width:150px; height:30px;" ID="eitemname" ReadOnly="true" runat="server"></asp:TextBox>
                        <br/><br />
                        <label style="width:75px; height:30px;">Quantity</label><br />
                        <asp:TextBox style="width:150px; height:30px;" ID="equantity" Enabled="true" runat="server"></asp:TextBox>

                        <br/><br />
                        <br/>
                        
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="esave" CssClass="btn btn-success"  runat="server" OnClick="esave_Click" Text="Save" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- To Recieve Purchase Items --------------------------------------->

    <div class="container" style="color:darkseagreen;">

        <div class="modal fade " data-backdrop="false" id="purchaserecieve" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">PURCHASE RECIEVE</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body" style="background-color:aquamarine;">
                        
                        <label style="width=75px; height:30px; "><h4>Purchase Order ID</h4></label>
                        <h5><label style="width=150px; height:30px;" runat="server" id="PoID"></label></h5>
                       <label style="width=75px; height:30px;"><h4>Vendor Name</h4></label>
                       <h5> <label style="width=150px; height:30px;" runat="server" id="vendorname"></label></h5>
                                         
                      <label><h4>Recieved Date</h4></label>
                        <h5><label runat="server" id="recievedate"></label></h5>
                                                
                        <label><h4>Status</h4></label>
                       <h5> <label runat="server" id="status"></label></h5>
                                                                     
                        <asp:GridView ID="recieveitem" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-striped  dt-responsive nowrap datatable1" Width="100%" EmptyDataText="No Data Available">
                        </asp:GridView>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="Delete" OnClientClick="return confirmdelete(this);" CssClass="btn btn-danger" OnClick="Delete_Click" Text="Delete" runat="server" />
                        <asp:Button ID="edit" CssClass="btn btn-primary" OnClick="edit_Click" Text="Edit" runat="server" />
                        <asp:Button ID="statbtn" CssClass="btn btn-primary" OnClick="stat_Click" Text="Mark As InTransit" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- To New Purchase Items --------------------------------------->


    <br />
    <br />
    <br />
    <div class="container" style="color:darkgreen;">

        <div class="modal fade " data-backdrop="false" id="newpurchaserecieve" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">PURCHASE RECIEVE</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">

                        <div class="modal-body-item">
                        <asp:Label ID="Label1" runat="server" Text="Supplier Name"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br/>
        <asp:DropDownList ID="ddlsuppliername" CssClass="dropdown" Width="200px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddlsuppliername_SelectedIndexChanged" DataTextField="Dealer_Name" DataValueField="Dealer_ID" runat="server">
            </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Purchase Order"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <asp:DropDownList ID="ddlpoid" AutoPostBack="true" Width="200px" Height="30px" OnSelectedIndexChanged="ddlpoid_SelectedIndexChanged" DataValueField="POrderID" DataTextField="POrderID" runat="server">
        </asp:DropDownList>
                        <br />
                        <br />
                       
                        &nbsp;<asp:Label ID="Label4" runat="server" Text="Recieve date"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <input id="rcdate" runat="server" Width="200px" Height="30px" type="date" />
                            <br />
                        <br />
                            <asp:Label runat="server">Notes</asp:Label><br />
                        <textarea id="note" Width="200px" Height="30px" runat="server" name="S1"></textarea><br />

                        <br />



                        <asp:GridView ID="editrecieveitem" EnableViewState="true" AutoGenerateColumns="false" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-striped  dt-responsive nowrap datatable1" Width="100%" DataKeyNames="ProductID" EmptyDataText="No Data Available">
                            <Columns>

                                <asp:BoundField DataField="ProductID" HeaderText="Product ID" SortExpression="ProductID" ReadOnly="true" />
                                <asp:TemplateField HeaderText="ItemName">
                                    <ItemTemplate>

                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox ID="txtItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>

                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="editbtn" runat="server" CssClass="fa-edit" OnClick="editbtn_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>


                        


                        
                        <br />

                    </div>
                        </div>
                    <div class="modal-footer">

                        <asp:Button ID="cancel" CssClass="btn btn-warning" OnClick="cancel_Click" runat="server" Text="Clear" />
                        <div class="dropdown">

                            <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" OnClick="Button1_Click" Text="Mark As Recieved" />
                            <asp:Button ID="Button2" CssClass="btn btn-success" runat="server" OnClick="Button2_Click" Text="Mark As In Transit" />
                        <asp:Button ID="canceledit" UseSubmitBehavior="true" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="canceledit_Click" />
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>

        <!-- To edit Purchase recieve Items --------------------------------------->


    <br />
    <br />
    <br />
    <div class="container">

        <div class="modal fade " data-backdrop="false" id="editpurchaserecieve" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">EDIT PURCHASE RECIEVE</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">


                        <asp:Label ID="Label5" runat="server" Text="Supplier Name"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:TextBox runat="server" ID="esname" ReadOnly="true"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label6" runat="server" Text="Purchase Order"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="epoid" ReadOnly="true"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        
                        &nbsp;<asp:Label ID="Label8" runat="server" Text="Recieve date"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input id="Date1" runat="server" type="date" /><br />
                        <br />
                        <textarea id="Textarea1" runat="server" name="S1"></textarea><br />

                        <br />



                        <asp:GridView ID="editpurchase" EnableViewState="true" AutoGenerateColumns="false" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-striped  dt-responsive nowrap datatable1" Width="100%" DataKeyNames="ProductID" EmptyDataText="No Data Available">
                            <Columns>

                                <asp:BoundField DataField="ProductID" HeaderText="Product ID" SortExpression="ProductID" ReadOnly="true" />
                                <asp:TemplateField HeaderText="ItemName">
                                    <ItemTemplate>

                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox ID="txtItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>

                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="eeditbtn" runat="server" CssClass="fa-edit" OnClick="eeditbtn_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>


                        <br />

                    </div>

                    <div class="modal-footer">
                           <asp:Button ID="editsave" CssClass="btn btn-success" OnClick="editsave_Click" runat="server" Text="Save" />

                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>




    </div>

</asp:Content>
