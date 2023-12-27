<%@ Page Title="Employee" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="HRMS.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .h3, h3 {
            font-size: 1.5rem;
        }

        /*    .form-control:focus {
        color: #495057;
        background-color: #fff;
        border-color: #80bdff;
        outline: 0;
        box-shadow: 0 0 0 0rem rgba(0,123,255,.25)
    }*/

        .btn-secondary:focus {
            box-shadow: 0 0 0 0rem rgba(108,117,125,.5)
        }

        .close:focus {
            box-shadow: 0 0 0 0rem rgba(108,117,125,.5)
        }

        .mt-200 {
            margin-top: 200px
        }

        .modal-title {
            font-size: 16px;
            margin-left: 200px;
            margin-top: 0;
        }

        .close {
            margin-left: 61px;
        }

        .lbll {
            padding-right: 30px;
        }

        .rdobtn1 {
            padding-right: 25px;
        }

        .rdobtn2 {
            padding-right: 25px;
        }
        /*.skin-base .content-body label{
        color: #70728d;
        margin-left: 5px;
    }*/
        .tabtitle {
            text-align: center;
            color: #1724d1;
        }

        .assetitle {
            color: #1724d1;
            margin-top: 0px;
            margin-bottom: 5px;
            text-align: center;
            padding-right: 56px;
            text-decoration: underline;
        }

        .vertical-center {
            margin: 0;
            position: absolute;
            top: 50%;
            -ms-transform: translateY(-50%);
            transform: translateY(-50%);
        }

        .sumit {
            width: 100px;
            margin-left: 350px;
        }

        .input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#editmodal').modal('show');
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
    <script>
        $(document).ready(function () {

            $('#smartwizard').smartWizard({
                selected: 0,
                theme: 'dots',
                autoAdjustHeight: true,
                transitionEffect: 'fade',
                showStepURLhash: false,

            });
            $('#smartwizard1').smartWizard({
                selected: 0,
                theme: 'dots',
                autoAdjustHeight: true,
                transitionEffect: 'fade',
                showStepURLhash: false,

            });
        });
    </script>
    <script>
        function success() {
            swal("Good job!", "You clicked the button!", "success").then(function () {
                location.reload();
            });
        }
    </script>



    <div class="main_container">
        <h2 class="header">
            <i data-feather="users"></i>
            Employee
        </h2>

    </div>
    <div>
        <button type="button" class="btn button-1 createmodal" data-toggle="modal" data-target="#createmodal">
            + Create Employee
        </button>
        <asp:Button runat="server" class="pull-right btn button-1" data-toggle="modal" Text="Download Report" OnClick="Excel_download" />
    </div>
    <div class="table-responsive">
        <br />

        <asp:GridView ID="EmployeeTable" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-striped  dt-responsive nowrap datatable1" Width="100%" CellSpacing="0" DataKeyNames="Emp_MasterID" OnRowDeleting="Employee_Delete" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Emp_ID" HeaderText="Emp ID" />
                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                <asp:BoundField DataField="Emp_DOB" HeaderText="DOB" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Emp_JoinDate" HeaderText="Joining Date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Emp_Contact" HeaderText="Contact" />
                <asp:BoundField DataField="Emp_Email" HeaderText="Email" />
                <asp:BoundField DataField="Emp_Designation" HeaderText="Designation" />
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="lbseq" Value='<%#Eval("Emp_MasterID")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Delete</HeaderTemplate>
                    <ItemTemplate>
                        <%--<a CommandName="Delete">
                        <i class="fa fa-trash"></i></a>--%>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" OnClientClick="return delete_emp(this);"><i class="fa fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Edit</HeaderTemplate>
                    <ItemTemplate>
                        <%--<a href="#"> 
                        <i class="fa fas fa-edit"></i></a>--%>
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><i class="fa fa-edit" ></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <%-- Create Emp model --%>
        <div class="modal fade bd-example-modal-lg" id="createmodal" tabindex="-1" style="z-index: 10003" role="dialog" backdrop="static" data-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg " role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3 class="modal-title header" id="exampleModalLabel">Create New Employee</h3>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>

                        </button>

                    </div>
                    <div class="modal-body CreateEmp">
                        <div id="smartwizard">
                            <ul>
                                <li><a href="#step-1">Step 1<br />
                                    <small>Personal Info</small></a></li>
                                <li><a href="#step-2">Step 2<br />
                                    <small>Edu & Experience</small></a></li>
                                <li><a href="#step-3">Step 3<br />
                                    <small>Bank Details</small></a></li>
                                <li><a href="#step-4">Step 4<br />
                                    <small>Assets</small></a></li>
                                <%--<li><a href="#step-5">Step 5<br /><small>Confirm details</small></a></li>--%>
                            </ul>
                            <div>
                                <div id="step-1">
                                    <div class="row">
                                        <h3 class="tabtitle">Personal Informations</h3>
                                        <div class="col-md-6">

                                            <label for="empid" class="lbll">Employee ID <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empid" runat="server" class="form-control" oninput=""></asp:TextBox>
                                            <%--<label id="idlbl" class="lbll" style="font-size:11px; color:red;"></label>--%>
                                            <asp:Label ID="idlbl" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empname" class="lbll">Employee Name <span style="color: red">*</span></label>
                                            <asp:TextBox runat="server" ID="empname" class="form-control" oninput=""> </asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6" style="padding-top: 4px;">
                                            <label for="" class="lbll">Gender: <span style="color: red">*</span></label>
                                            <asp:RadioButton ID="RadioButton1" CssClass="rdobtn1" runat="server" value="Male" Text="Male" GroupName="gender" />
                                            <asp:RadioButton ID="RadioButton2" CssClass="rdobtn2" runat="server" value="Female" Text="Female" GroupName="gender" />
                                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6" style="padding-top: 4px;">
                                            <label for="" class="lbll">Marital Status: <span style="color: red">*</span></label>
                                            <asp:RadioButton ID="RadioButton3" CssClass="rdobtn1" runat="server" value="Married" Text="Married" GroupName="married" />
                                            <asp:RadioButton ID="RadioButton4" CssClass="rdobtn2" runat="server" value="Unmarried" Text="Unmarried" GroupName="married" />
                                            <asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empdob" class="lbll">Date of Birth <span style="color: red">*</span></label>
                                            <asp:TextBox type="date" ID="empdob" runat="server" Style="color: #9f9a9a;" oninput="" class="form-control"></asp:TextBox>
                                            <asp:Label ID="Label4" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empjoindt" class="lbll">Joining Date <span style="color: red">*</span></label>
                                            <asp:TextBox type="date" ID="empjoindt" runat="server" Style="color: #9f9a9a;" class="form-control"></asp:TextBox>
                                            <asp:Label ID="Label5" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empcon" class="lbll">Contact No <span style="color: red">*</span></label>
                                            <asp:TextBox TextMode="Number" ID="empcon" onkeypress="return this.value.length<=9" runat="server" class="form-control" placeholder=""> </asp:TextBox>
                                            <asp:Label ID="Label6" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>

                                        </div>
                                        <div class="col-md-6">
                                            <label for="empaltercon" class="lbll">Alternative Contact No</label>
                                            <asp:TextBox TextMode="Number" ID="empaltercon" onkeypress="return this.value.length<=9" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label7" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>

                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empmail" class="lbll">Email <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empmail" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label8" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>

                                        </div>
                                        <div class="col-md-6">
                                            <label for="deptdropdown" class="lbll">Department <span style="color: red">*</span></label>
                                            <asp:DropDownList ID="deptdropdown" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label9" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                            <%--<input type="text" class="form-control" placeholder="Department" required>  --%>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <%--<input type="text" class="form-control" placeholder="Designation" required>--%>
                                            <label for="emppwd" class="lbll">Password <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="emppwd" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label10" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empbloodgrp" class="lbll">Blood Group <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empbloodgrp" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label11" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empcrntadd" class="lbll">Current Address <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empcrntadd" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label12" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="permntadd" class="lbll">Permanent Address <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="permntadd" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label13" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <%--<input type="text" class="form-control" placeholder="Permanent Address" required>--%>
                                        </div>
                                        <div class="col-md-6">
                                        </div>

                                    </div>

                                </div>
                                <div id="step-2">
                                    <div class="row">
                                        <h3 class="tabtitle">Education & Experience</h3>
                                        <div class="col-md-6" style="padding-top: 4px;">
                                            <label for="" class="lbll">Qualification: <span style="color: red">*</span></label>
                                            <asp:RadioButton ID="RadioButton5" CssClass="rdobtn1" runat="server" value="UG" Text="UG" GroupName="Qualification" />
                                            <asp:RadioButton ID="RadioButton6" CssClass="rdobtn2" runat="server" value="PG" Text="PG" GroupName="Qualification" />
                                            <asp:RadioButton ID="RadioButton7" CssClass="rdobtn2" runat="server" value="Others" Text="Others" GroupName="Qualification" />
                                            <asp:Label ID="Label14" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empdegree" class="lbll">Degree <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="empdegree" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label15" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empcertificate" class="lbll">Additional Certificate </label>
                                            <asp:TextBox type="text" ID="empcertificate" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label16" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empdesigntion" class="lbll">Designation <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empdesigntion" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label17" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="ttlexp" class="lbll">Total Experience <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="ttlexp" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label18" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="relevexp" class="lbll">Relevant Experience</label>
                                            <asp:TextBox type="text" runat="server" ID="relevexp" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label19" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                        </div>
                                        <div class="col-md-6">
                                        </div>
                                    </div>
                                </div>
                                <div id="step-3" class="">
                                    <div class="row">
                                        <h3 class="tabtitle">Bank Details</h3>
                                        <div class="col-md-6">
                                            <label for="nameasperbank" class="lbll">Name As Per Bank <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="nameasperbank" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label20" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="Bankname" class="lbll">Bank <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="Bankname" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label21" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="accno" class="lbll">Account No <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="accno" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label22" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="ifsc" class="lbll">IFSC Code <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="ifsc" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label23" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="acctypes" class="lbll">Account Type <span style="color: red">*</span></label>
                                            <asp:DropDownList ID="acctypes" class="form-control" runat="server">
                                                <asp:ListItem Value="">Select Account Type</asp:ListItem>
                                                <asp:ListItem Value="Savings">Savings</asp:ListItem>
                                                <asp:ListItem Value="Current">Current</asp:ListItem>
                                                <asp:ListItem Value="Salary">Salary</asp:ListItem>

                                            </asp:DropDownList>
                                            <asp:Label ID="Label24" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                            <%--<asp:TextBox type="text" runat="server" id="acctype" class="form-control" placeholder="Account Type" ></asp:TextBox> --%>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="panno" class="lbll">PAN No <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="panno" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label25" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="aadharno" class="lbll">Aadhar No <span style="color: red">*</span></label>
                                            <asp:TextBox  TextMode="Number" ID="aadharno" onkeypress="return this.value.length<=11" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label26" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="branchadd" class="lbll">Branch & Address <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="branchadd" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label27" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                        </div>
                                        <div class="col-md-6">
                                        </div>
                                    </div>

                                </div>
                                <div id="step-4" class="">
                                    <div class="row mt-3">
                                        <h3 class="tabtitle">Assets</h3>
                                        <div class="col-md-6">
                                            <label for="mngrname" class="lbll">Reporting Manager Name <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="mngrname" class="form-control" placeholder=""> </asp:TextBox>
                                            <asp:Label ID="Label28" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="assetcount" class="lbll">Assets Count <span style="color: red">*</span></label>
                                            <asp:TextBox type="number" runat="server" ID="assetcount" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label29" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="dtofissue" class="lbll">Date of Issue <span style="color: red">*</span></label>
                                            <asp:TextBox type="date" runat="server" ID="dtofissue" Style="color: #9f9a9a;" class="form-control"></asp:TextBox>
                                            <asp:Label ID="Label30" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="dtofsurrend" class="lbll">Date of Surrender</label>
                                            <asp:TextBox type="date" runat="server" ID="dtofsurrend" Style="color: #9f9a9a;" class="form-control"></asp:TextBox>
                                            <asp:Label ID="Label31" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="remarks" class="lbll">Remarks</label>
                                            <asp:TextBox type="text" runat="server" ID="remarks" class="form-control" placeholder=""></asp:TextBox>
                                            <asp:Label ID="Label32" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>

                                        </div>
                                        <div class="col-md-6">
                                            <h3 class="assetitle">Asset Details <span style="color: red">*</span></h3>


                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="300px">
                                                <asp:ListItem Value="Laptop" style="margin-right: 98px">Laptop</asp:ListItem>
                                                <asp:ListItem Value="Laptop Bag" style="margin-right: 20px">Laptop Bag</asp:ListItem>
                                                <asp:ListItem Value="Laptop Charger" style="margin-right: 40px">Laptop Charger</asp:ListItem>
                                                <asp:ListItem Value="Laptop Mouse" style="margin-right: 20px">Laptop Mouse</asp:ListItem>
                                                <asp:ListItem Value="Tablet" style="margin-right: 104px">Tablet</asp:ListItem>
                                                <asp:ListItem Value="Tablet Charger" style="margin-right: 20px">Tablet Charger</asp:ListItem>
                                                <asp:ListItem Value="Access Card" style="margin-right: 60px">Access Card</asp:ListItem>
                                                <asp:ListItem Value="ID Card" style="margin-right: 20px">ID Card</asp:ListItem>
                                            </asp:CheckBoxList>
                                            <asp:Label ID="Label33" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>



                                        </div>
                                    </div>
                                    <div class="row mt-3">

                                        <asp:Button runat="server" class="btn button-1 createmodal sumit" data-toggle="modal" Text="Submit" OnClick="Createemployee" />

                                    </div>

                                </div>
                                <%--<div id="step-5" class=""> 
                                <div class="row"> 
                                    <h3 class="tabtitle">Confirm Details</h3>
                                    
                                    <div class="row mt-3"> 
                                    <div class="col-md-6"> 
                                        <asp:Label ID="Label4" runat="server" Text="<%# empname.Text %>"></asp:Label>
                                    </div> 
                                    <div class="col-md-6"> 
                                        <asp:Button ID="submit" runat="server" Text="Submit" class="btn button-1 createmodal" data-toggle="modal" OnClick="submit_Click" />
                                            
                                        
                                    </div> 
                                </div> 

                                </div> 

                            </div>--%>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>
        <%-- EDIT MODEL POPUP --%>
        <div class="modal fade bd-example-modal-lg" id="editmodal" tabindex="-1" style="z-index: 10003" role="dialog" backdrop="static" data-keyboard="false" aria-labelledby="exampleModalLabel1" aria-hidden="true">
            <div class="modal-dialog modal-lg " role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3 class="modal-title header" id="exampleModalLabel1">Edit Employee Details</h3>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="reload()">
                            <span aria-hidden="true">&times;</span>

                        </button>

                    </div>
                    <div class="modal-body CreateEmp">
                        <div id="smartwizard1">
                            <ul>
                                <li><a href="#stepno-1">Step 1<br />
                                    <small>Personal Info</small></a></li>
                                <li><a href="#stepno-2">Step 2<br />
                                    <small>Edu & Experience</small></a></li>
                                <li><a href="#stepno-3">Step 3<br />
                                    <small>Bank Details</small></a></li>
                                <li><a href="#stepno-4">Step 4<br />
                                    <small>Assets</small></a></li>
                                <%--<li><a href="#step-5">Step 5<br /><small>Confirm details</small></a></li>--%>
                            </ul>
                            <div>
                                <div id="stepno-1">
                                    <div class="row">
                                        <h3 class="tabtitle">Personal Informations</h3>
                                        <div class="col-md-6">
                                            <label for="empid1" class="lbll">Employee ID <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empid1" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                            <div style="display: none">
                                                <asp:TextBox type="text" ID="Emp_Masterid" runat="server" class="form-control" placeholder="EmpmasterID"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empname1" class="lbll">Employee Name <span style="color: red">*</span></label>
                                            <asp:TextBox runat="server" ID="empname1" class="form-control" placeholder=""> </asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6" style="padding-top: 4px;">
                                            <label for="" class="lbll">Gender: <span style="color: red">*</span></label>
                                            <asp:RadioButton ID="RadioButton8" CssClass="rdobtn1" runat="server" value="Male" Text="Male" GroupName="gender" />
                                            <asp:RadioButton ID="RadioButton9" CssClass="rdobtn2" runat="server" value="Female" Text="Female" GroupName="gender" />
                                        </div>
                                        <div class="col-md-6" style="padding-top: 4px;">
                                            <label for="" class="lbll">Marital Status: <span style="color: red">*</span></label>
                                            <asp:RadioButton ID="RadioButton10" CssClass="rdobtn1" runat="server" value="Married" Text="Married" GroupName="married" />
                                            <asp:RadioButton ID="RadioButton11" CssClass="rdobtn2" runat="server" value="Unmarried" Text="Unmarried" GroupName="married" />
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empdob1" class="lbll">Date of Birth <span style="color: red">*</span></label>
                                            <asp:TextBox type="date" ID="empdob1" runat="server" Style="color: #9f9a9a;" class="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-6">
                                            <label for="empjoindt1" class="lbll">Joining Date <span style="color: red">*</span></label>
                                            <asp:TextBox type="date" ID="empjoindt1" runat="server" Style="color: #9f9a9a;" class="form-control"></asp:TextBox>



                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empcon1" class="lbll">Contact No <span style="color: red">*</span></label>
                                            <asp:TextBox type="number" ID="empcon1" TextMode="Number" onkeypress="return this.value.length<=9" runat="server" class="form-control" placeholder=""> </asp:TextBox>


                                        </div>
                                        <div class="col-md-6">
                                            <label for="empaltercon1" class="lbll">Alternative Contact No</label>
                                            <asp:TextBox type="number" ID="empaltercon1" TextMode="Number" onkeypress="return this.value.length<=9" runat="server" class="form-control" placeholder=""></asp:TextBox>


                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empmail1" class="lbll">Email <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empmail1" runat="server" class="form-control" placeholder="Email"></asp:TextBox>


                                        </div>
                                        <div class="col-md-6">
                                            <label for="deptdropdown_1" class="lbll">Department <span style="color: red">*</span></label>
                                            <asp:DropDownList ID="deptdropdown_1" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <%--<input type="text" class="form-control" placeholder="Department" required>  --%>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="emppwd1" class="lbll">Password <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="emppwd1" runat="server" class="form-control" placeholder=""></asp:TextBox>

                                        </div>
                                        <div class="col-md-6">
                                            <label for="empbloodgrp1" class="lbll">Blood Group <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empbloodgrp1" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empcrntadd1" class="lbll">Current Address <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empcrntadd1" runat="server" class="form-control" placeholder=""></asp:TextBox>

                                        </div>
                                        <div class="col-md-6">
                                            <label for="permntadd1" class="lbll">Permanent Address <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="permntadd1" runat="server" class="form-control" placeholder=""></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <%--<input type="text" class="form-control" placeholder="Permanent Address" required>--%>
                                        </div>
                                        <div class="col-md-6">
                                        </div>

                                    </div>

                                </div>
                                <div id="stepno-2">
                                    <div class="row">
                                        <h3 class="tabtitle">Education & Experience</h3>
                                        <div class="col-md-6" style="padding-top: 4px;">
                                            <label for="" class="lbll">Qualification: <span style="color: red">*</span></label>
                                            <asp:RadioButton ID="RadioButton12" CssClass="rdobtn1" runat="server" value="UG" Text="UG" GroupName="Qualification" />
                                            <asp:RadioButton ID="RadioButton13" CssClass="rdobtn2" runat="server" value="PG" Text="PG" GroupName="Qualification" />
                                            <asp:RadioButton ID="RadioButton14" CssClass="rdobtn2" runat="server" value="Others" Text="Others" GroupName="Qualification" />
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empdegree1" class="lbll">Degree <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="empdegree1" class="form-control" placeholder="Degree"></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="empcertificate1" class="lbll">Additional Certificate</label>
                                            <asp:TextBox type="text" ID="empcertificate1" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="empdesigntion1" class="lbll">Designation <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="empdesigntion1" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="ttlexp1" class="lbll">Total Experience <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="ttlexp1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="relevexp1" class="lbll">Relevant Experience</label>
                                            <asp:TextBox type="text" runat="server" ID="relevexp1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                        </div>
                                        <div class="col-md-6">
                                        </div>
                                    </div>
                                </div>
                                <div id="stepno-3" class="">
                                    <div class="row">
                                        <h3 class="tabtitle">Bank Details</h3>
                                        <div class="col-md-6">
                                            <label for="nameasperbank1" class="lbll">Name As Per Bank <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="nameasperbank1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="Bankname1" class="lbll">Bank <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="Bankname1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="accno1" class="lbll">Account No <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="accno1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="ifsc1" class="lbll">IFSC Code <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" ID="ifsc1" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="acctypes1" class="lbll">Account Type <span style="color: red">*</span></label>
                                            <asp:DropDownList ID="acctypes1" class="form-control" runat="server">
                                                <asp:ListItem Value="">Select Account Type</asp:ListItem>
                                                <asp:ListItem Value="Savings">Savings</asp:ListItem>
                                                <asp:ListItem Value="Current">Current</asp:ListItem>
                                                <asp:ListItem Value="Salary">Salary</asp:ListItem>

                                            </asp:DropDownList>
                                            <%--<asp:TextBox type="text" runat="server" id="acctype" class="form-control" placeholder="Account Type" ></asp:TextBox> --%>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="panno1" class="lbll">PAN No <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="panno1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="aadharno1" class="lbll">Aadhar No <span style="color: red">*</span></label>
                                            <asp:TextBox type="number" ID="aadharno1" TextMode="Number" onkeypress="return this.value.length<=11" runat="server" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="branchadd1" class="lbll">Branch & Address <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="branchadd1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                        </div>
                                        <div class="col-md-6">
                                        </div>
                                    </div>

                                </div>
                                <div id="stepno-4" class="">
                                    <div class="row mt-3">
                                        <h3 class="tabtitle">Assets</h3>
                                        <div class="col-md-6">
                                            <label for="mngrname1" class="lbll">Reporting Manager Name <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="mngrname1" class="form-control" placeholder=""> </asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="assetcount1" class="lbll">Assets Count <span style="color: red">*</span></label>
                                            <asp:TextBox type="number" runat="server" ID="assetcount1" class="form-control" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="dtofissue1" class="lbll">Date of Issue <span style="color: red">*</span></label>
                                            <asp:TextBox type="text" runat="server" ID="dtofissue1" Style="color: #9f9a9a;" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="dtofsurrend1" class="lbll">Date of Surrender</label>
                                            <asp:TextBox type="date" runat="server" ID="dtofsurrend1" Style="color: #9f9a9a;" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <label for="remarks1" class="lbll">Remarks</label>
                                            <asp:TextBox type="text" runat="server" ID="remarks1" class="form-control" placeholder=""></asp:TextBox>

                                        </div>
                                        <div class="col-md-6">
                                            <h3 class="assetitle">Asset Details <span style="color: red">*</span></h3>


                                            <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="300px">
                                                <asp:ListItem Value="Laptop" style="margin-right: 98px">Laptop</asp:ListItem>
                                                <asp:ListItem Value="Laptop Bag" style="margin-right: 20px">Laptop Bag</asp:ListItem>
                                                <asp:ListItem Value="Laptop Charger" style="margin-right: 40px">Laptop Charger</asp:ListItem>
                                                <asp:ListItem Value="Laptop Mouse" style="margin-right: 20px">Laptop Mouse</asp:ListItem>
                                                <asp:ListItem Value="Tablet" style="margin-right: 104px">Tablet</asp:ListItem>
                                                <asp:ListItem Value="Tablet Charger" style="margin-right: 20px">Tablet Charger</asp:ListItem>
                                                <asp:ListItem Value="Access Card" style="margin-right: 60px">Access Card</asp:ListItem>
                                                <asp:ListItem Value="ID Card" style="margin-right: 20px">ID Card</asp:ListItem>
                                            </asp:CheckBoxList>



                                        </div>
                                    </div>
                                    <div class="row mt-3">

                                        <asp:Button runat="server" class="btn button-1 createmodal sumit" data-toggle="modal" Text="Save" OnClick="EditEmp" />

                                    </div>

                                </div>
                                <%--<div id="stepno-5" class=""> 
                                <div class="row"> 
                                    <h3 class="tabtitle">Confirm Details</h3>
                                    
                                    <div class="row mt-3"> 
                                    <div class="col-md-6"> 
                                        <asp:Label ID="Label4" runat="server" Text="<%# empname.Text %>"></asp:Label>
                                    </div> 
                                    <div class="col-md-6"> 
                                        <asp:Button ID="submit" runat="server" Text="Submit" class="btn button-1 createmodal" data-toggle="modal" OnClick="submit_Click" />
                                            
                                        
                                    </div> 
                                </div> 

                                </div> 

                            </div>--%>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>



    </div>
</asp:Content>


