<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Pay_Roll.aspx.cs" Inherits="HRMS_.Pay_Roll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openModal() {
           // document.getElementById("ContentPlaceHolder1_DropDownList3").value = document.getElementById("ContentPlaceHolder1_tmp_Emp_Department_MSG2").text
            $('#EditModal').modal('show');
        }
        function opencreateModal() {
            $('#createmodal').modal('show');
        }
        //$(function () {
        //    $("#DropDownList1").select2();
        //});
        //$(function () {
        //    $("#DropDownList2").select2();
        //});
    </script>
      <script>
          function valentr() {
              //$('#SalaryAmount_MSG11').innerHtml = "";
              //alert("ok");
              var sal = document.getElementById("ContentPlaceHolder1_SalaryAmount").value

              if (sal != "") {
                  document.getElementById("ContentPlaceHolder1_SalaryAmount_MSG11").innerHTML = ""
              }
              if (sal == "") {
                  document.getElementById("ContentPlaceHolder1_SalaryAmount_MSG11").innerHTML = "please select salary amount!"
              }

              var bon = document.getElementById("ContentPlaceHolder1_BonusAmount").value
              if (bon != "") {
                  document.getElementById("ContentPlaceHolder1_BonusAmount_MSG11").innerHTML = ""
              }
              if (bon == "") {
                  document.getElementById("ContentPlaceHolder1_BonusAmount_MSG11").innerHTML = "please select Bonus amount!"
              }

              var pf = document.getElementById("ContentPlaceHolder1_PFAmount").value
              if (pf != "") {
                  document.getElementById("ContentPlaceHolder1_PFAmount_MSG11").innerHTML = ""
              }
              if (pf == "") {
                  document.getElementById("ContentPlaceHolder1_PFAmount_MSG11").innerHTML = "please select PF amount!"
              }

              var OtherA = document.getElementById("ContentPlaceHolder1_OtherAllowance").value
              if (OtherA != "") {
                  document.getElementById("ContentPlaceHolder1_OtherAllowance_MSG11").innerHTML = ""
              }
              if (OtherA == "") {
                  document.getElementById("ContentPlaceHolder1_OtherAllowance_MSG11").innerHTML = "please select Other allowance amount!"
              }
              var dropv = document.getElementById("ContentPlaceHolder1_DropDownList1").value
              if (dropv != 0) {
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_Department_MSG11").value = dropv;
                  document.getElementById("ContentPlaceHolder1_Emp_Department_MSG11").innerHTML = "";

              }
              else {
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_Department_MSG11").value = "";
                  document.getElementById("ContentPlaceHolder1_Emp_Department_MSG11").innerHTML = "Please Select Department!";
              }
              var dropv1 = document.getElementById("ContentPlaceHolder1_DropDownList2").value
              if (dropv1 != 0) {
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_FKID_MSG11").value = dropv1;
                  document.getElementById("ContentPlaceHolder1_Emp_FKID_MSG").innerHTML = "";

              }
              else {
                  document.getElementById("ContentPlaceHolder1_Emp_FKID_MSG").innerHTML = "Please Select Employee";
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_FKID_MSG11").value = "";

              }
          }
          function valentrID() {
              var sal = document.getElementById("ContentPlaceHolder1_SalaryAmount1").value

              if (sal != "") {
                  document.getElementById("ContentPlaceHolder1_SalaryAmount_MSG1").innerHTML = ""
              }
              if (sal == "") {
                  document.getElementById("ContentPlaceHolder1_SalaryAmount_MSG1").innerHTML = "please select salary amount!"
              }

              var bon = document.getElementById("ContentPlaceHolder1_BonusAmount1").value
              if (bon != "") {
                  document.getElementById("ContentPlaceHolder1_BonusAmount_MSG1").innerHTML = ""
              }
              if (bon == "") {
                  document.getElementById("ContentPlaceHolder1_BonusAmount_MSG1").innerHTML = "please select Bonus amount!"
              }

              var pf = document.getElementById("ContentPlaceHolder1_PFAmount1").value
              if (pf != "") {
                  document.getElementById("ContentPlaceHolder1_PFAmount_MSG1").innerHTML = ""
              }
              if (pf == "") {
                  document.getElementById("ContentPlaceHolder1_PFAmount_MSG1").innerHTML = "please select PF amount!"
                  //document.getElementById("ContentPlaceHolder1_OtherAllowance_MSG1").innerHTML = "Please Select ther allowance amount!"

              }

              var OtherA = document.getElementById("ContentPlaceHolder1_OtherAllowancel1").value
              if (OtherA != "") {
                  document.getElementById("ContentPlaceHolder1_OtherAllowance_MSG1").innerHTML = ""
              }
              if (OtherA == "") {
                  document.getElementById("ContentPlaceHolder1_OtherAllowance_MSG1").innerHTML = "Please Select Other Allowance Amount!"
              }

              var dropv2 = document.getElementById("ContentPlaceHolder1_DropDownList3").value
              
              if (dropv2 != 0) {
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_Department_MSG2").value = dropv2;
                  document.getElementById("ContentPlaceHolder1_Emp_Department_MSG1").innerHTML = "";

              }
              else {
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_Department_MSG2").text = "";
                  document.getElementById("ContentPlaceHolder1_Emp_Department_MSG1").innerHTML = "Please Select Department!";

              }
              var dropv3 = document.getElementById("ContentPlaceHolder1_DropDownList4").value
              if (dropv3 != 0) {
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_FKID_MSG2").value = dropv3;
                  document.getElementById("ContentPlaceHolder1_Emp_FKID_MSG1").innerHTML = "";

              }
              else {
                  document.getElementById("ContentPlaceHolder1_tmp_Emp_FKID_MSG2").value = "";
                  document.getElementById("ContentPlaceHolder1_Emp_FKID_MSG1").innerHTML = "Please Select Employee";

              }
          }
      </script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
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
        <i data-feather="check-square"></i>
        Pay Roll
        </h2>

    </div>
    <div>
            <button type="button" class="btn button-1 createmodal" data-toggle="modal" data-target="#createmodal">
                + Create Pay Roll
       
            </button>
            <%--<a class="pull-right btn button-1" runat="server" onclick="excel_Click1"></a>--%>
            <asp:LinkButton class="pull-right btn button-1" OnClick="excel_Click1" ID="LinkButton2" runat="server">Download Report</asp:LinkButton>
        </div>
    <div class="table-responsive">
            <br />
            <asp:GridView ID="payRollTable" ShowHeaderWhenEmpty="true" runat="server" CssClass="table table-striped  dt-responsive nowrap datatable1" DataKeyNames="Payroll_ID" Width="100%" CellSpacing="0" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Payroll_ID" HeaderText="Payroll ID" />
                    <%--<asp:HiddenField ID="Payroll_IDD" runat="server" Value='<%# Bind("Payroll_IDD") %>'/>--%>
                    <asp:BoundField DataField="SalaryAmount" HeaderText="Salary Amount" />
                    <asp:BoundField DataField="BonusAmount" HeaderText="Bonus Amount" />
                    <asp:BoundField DataField="PFAmount" HeaderText="PF Amount" />
                    <asp:BoundField DataField="OtherAllowance" HeaderText="Other Allowance" />
                    <asp:BoundField DataField="Dept_Name" HeaderText="Department" />
                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee" />
                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" />
                    <%--    <asp:commandfield ShowDeleteButton="true" DeleteText ="Delete" />
            <asp:CommandField ShowSelectButton="True" SelectText ="Edit" />--%>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" class="fa fa-trash" runat="server" OnClick="lnkdelete_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" class="fa fas fa-edit" runat="server" OnClick="lnkedit_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="Image/delete.png" HeaderText="Delete" />--%>
                    <%--<asp:TemplateField>
                <HeaderTemplate>Delete</HeaderTemplate>
                <ItemTemplate>
                    <a href="#"><i class="fa fa-trash"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Edit</HeaderTemplate>
                <ItemTemplate>
                   <%-- <a href="#"> <i class="fa fas fa-edit"></i></a>
                    
                  <a href="#"> <i class="fa fas fa-edit EditModal" ID = "EDIT" OnClick ="Edit" data-toggle="modal" data-target="#EditModal"> </i></a>
                    <asp:Button id="LinkButton1" Text="Create" OnClick="LinkButton1_Click" runat="server"/>
                    <asp:LinkButton class="fa fas fa-edit EditModal" ID="LinkButton1" CommandName="Edit" data-toggle="modal" data-target="#EditModal" runat="server"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>

            <%--<asp:ScriptManager ID="script1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
            <%--<asp:UpdatePanel ID="Panle1" runat="server">--%>
            <%-- <contenttemplate>--%>
      <div class="modal fade" id="createmodal" tabindex="-1" role="dialog" style="z-index: 10003" data-backdrop="static" data-keyboard="false" aria-labelledby="CreateTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="padding: 6px">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h3 class="header" id="CreateModalLabel">Create Pay Roll
                            </h3>
                        </div>
                         
                        <div class="form-group">
                            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>
                            <label for="Payroll_ID">Payroll ID</label>
                            <asp:TextBox ID="Payroll_ID" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>

                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>--%>
                            <%--  <p id="Payroll_ID_MSG" style="color: red;" runat="server"></p>--%>
                            <asp:Label ID="Payroll_ID_MSG11" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <%--<input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">--%>
                            <label for="SalaryAmount">Salary Amount</label>
                            <asp:TextBox ID="SalaryAmount" oninput="valentr()" class="form-control" runat="server"></asp:TextBox>

                            <asp:Label ID="SalaryAmount_MSG11" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <label for="BonusAmount">Bonus Amount</label>
                            <asp:TextBox ID="BonusAmount" oninput="valentr()" class="form-control" runat="server"></asp:TextBox>
                            <%-- <p id="BonusAmount_MSG" style="color: red;"></p>--%>
                            <asp:Label ID="BonusAmount_MSG11" ForeColor="Red" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label for="PFAmount">PF Amount</label>
                            <asp:TextBox ID="PFAmount" oninput="valentr()" class="form-control" runat="server"></asp:TextBox>
                            <%--<p id="PFAmount_MSG" style="color: red;"></p>--%>
                            <asp:Label ID="PFAmount_MSG11" ForeColor="Red" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label for="OtherAllowance">Other Allowance</label>
                            <asp:TextBox ID="OtherAllowance" oninput="valentr()" class="form-control" runat="server"></asp:TextBox>
                            <%--<p id="OtherAllowance_MSG" style="color: red;"></p>--%>
                            <asp:Label ID="OtherAllowance_MSG11" ForeColor="Red" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                           <label for="DropDownList1">Department</label>
                            <%--<asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                            </asp:DropDownList>--%>
                            <asp:DropDownList ID="DropDownList1" onchange="valentr()" runat="server" class="form-control">
                            </asp:DropDownList>

                            
                            <%-- @Html.DropDownList("listItems2", null, "Select Department", htmlAttributes: new { @class = "form-control", @id = "Dept_FKID" })--%>
                            <%--<p id="Emp_Department_MSG" style="color: red;"></p>--%>
                            <asp:Label ID="Emp_Department_MSG11" ForeColor="Red" runat="server"></asp:Label>
                            <%--<asp:Label ID="tmp_Emp_Department_MSG11" Style="display:none" ForeColor="Red" runat="server"></asp:Label>--%>
                            <asp:TextBox ID="tmp_Emp_Department_MSG11" Style="display:none" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="Emp_MasterID">Employee</label>
                            <asp:DropDownList class="form-control" ID="DropDownList2" onchange="valentr()" runat="server">
                            </asp:DropDownList>

                            <%-- @Html.DropDownList("listItems", null, "Select Employee", htmlAttributes: new { @class = "form-control", @id = "Emp_FKID" })--%>
                            <%--<p id="Emp_FKID_MSG" style="color: red;"></p>--%>
                            <asp:Label ID="Emp_FKID_MSG" ForeColor="Red" runat="server"></asp:Label>
                           
                            <asp:TextBox ID="tmp_Emp_FKID_MSG11" Style="display:none" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary closeing" data-dismiss="modal">Close</button>
                            <%--<button type="button" class="btn button-1 CreateData" OnClick="Insert" >Create</button>--%>
                            <asp:Button ID="Button90" class="btn btn-secondary closeing" Text="Create" OnClick="Button90_Click" runat="server" />

                            <%-- <asp:Button ID="button" class="btn button-1 CreateData" runat="server" Text="Add" OnClick="Insert" />--%>
                        </div>
                    </div>
                </div>
            </div>
            <!--end-->
            <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" style="z-index: 10003" data-backdrop="static" data-keyboard="false" aria-labelledby="EditTitle" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="padding: 6px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h3 class="header" id="EditpayModalLabel">Edit payRoll
                            </h3>
                        </div>
                        <div class="form-group">
                            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>
                            <label for="Payroll_ID1">Payroll ID</label>
                            <asp:TextBox ID="Payroll_ID1" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                            <%--<p id="Payroll_ID_MSG1" style="color: red;"></p>--%>
                            <asp:Label ID="Payroll_ID_MSG1" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <%--<input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">--%>
                            <label for="SalaryAmount1">Salary Amount</label>
                            <asp:TextBox ID="SalaryAmount1" oninput="valentrID()" class="form-control" runat="server"></asp:TextBox>
                            <%--<p id="SalaryAmount_MSG1" style="color: red;"></p>--%>
                            <asp:Label ID="SalaryAmount_MSG1" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <label for="BonusAmount1">Bonus Amount</label>
                            <asp:TextBox ID="BonusAmount1" oninput="valentrID()" class="form-control" runat="server"></asp:TextBox>
                            <%--<p id="BonusAmount_MSG1" style="color: red;"></p>--%>
                            <asp:Label ID="BonusAmount_MSG1" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <label for="PFAmount1">PF Amount</label>
                            <asp:TextBox ID="PFAmount1" oninput="valentrID()" class="form-control" runat="server"></asp:TextBox>
                            <%--<p id="PFAmount_MSG1" style="color: red;"></p>--%>
                            <asp:Label ID="PFAmount_MSG1" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <label for="OtherAllowancel1">Other Allowance</label>
                            <asp:TextBox ID="OtherAllowancel1" oninput="valentrID()" class="form-control" runat="server"></asp:TextBox>
                            <%--<p id="OtherAllowance_MSG1" style="color: red;"></p>--%>
                            <asp:Label ID="OtherAllowance_MSG1" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                        <div class="form-group">
                            <label for="Emp_Department1">Department</label>
                            <asp:DropDownList class="form-control" oninput="valentrID()" ID="DropDownList3" runat="server">
                            </asp:DropDownList>
                            <%-- @Html.DropDownList("listItems2", null, "Select Department", htmlAttributes: new { @class = "form-control", @id = "Dept_FKID" })--%>
                            <%-- <p id="Emp_Department_MSG1" style="color: red;"></p>--%>
                            <asp:Label ID="Emp_Department_MSG1" ForeColor="Red" runat="server"></asp:Label>
                            <%--<asp:Label ID="tmp_Emp_Department_MSG1" ForeColor="Red" Style="display:none" runat="server"></asp:Label>--%>
                            <asp:TextBox ID="tmp_Emp_Department_MSG2" Style="display:none" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="Emp_MasterID1">Employee</label>
                            <asp:DropDownList class="form-control"  oninput="valentrID()" ID="DropDownList4" runat="server">
                            </asp:DropDownList>
                            <%-- @Html.DropDownList("listItems", null, "Select Employee", htmlAttributes: new { @class = "form-control", @id = "Emp_FKID" })--%>
                            <%--<p id="Emp_FKID_MSG1" style="color: red;"></p>--%>
                            <asp:Label ID="Emp_FKID_MSG1" ForeColor="Red" runat="server"></asp:Label>
                            <%--<asp:Label ID="tmp_Emp_FKID_MSG1" ForeColor="Red" Style="display:none" runat="server"></asp:Label>--%>
                            <asp:TextBox ID="tmp_Emp_FKID_MSG2" Style="display:none" runat="server"></asp:TextBox>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary closeing " data-dismiss="modal">Close</button>
                            <%--<button type="button" id ="button_002" OnClick class="btn btn-primary button-1 EditData">Save</button>--%>
                            <asp:Button ID="button_002" class="btn btn-secondary closeing" Text="Create" OnClick="button_002_clk" runat="server" />
                        </div>
                    </div>
                </div>
            </div>


            

            <%--</ContentTemplate>--%>
            <%-- </asp:UpdatePanel>--%>
        </div>
</asp:Content>

