<%@ Page Title="Timesheet" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="timesheet.aspx.cs" Inherits="HRMS.timesheet" %>

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
    .modal-title{
        font-size:16px;
        margin-left: 110px;
        margin-top: 0;
    }
    .close{
        margin-left: 61px;
    }
    .lbll{
        padding-left: 10px;
        padding-right: 30px;
        color:#9f9a9a;
    }
    .rdobtn1{
        padding-right: 25px;
        
    }
    .rdobtn2{
        padding-right: 25px;
        
    }
    .skin-base .content-body label{
        color: #70728d;
    }
</style>
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

        });
    </script>
     <script>
         function openModal1() {
             $('#createmodal').modal('show');
         }
     </script>
    
    <script>
        function openModal() {
            $('#EditModal').modal('show');
        }
    </script>


    <div class="main_container">
        <h2 class="header">
        <i data-feather="users"></i>
        Time Sheet
        </h2>


    </div>
    <div>
    <div style="margin-bottom:20px">
    
        <button type="button" class="btn button-1 createmodal" data-toggle="modal" data-target="#createmodal">
            + Add Entry
        </button>
 <asp:LinkButton class="pull-right btn button-1" OnClick="excel_Click1" ID="LinkButton2" runat="server">Download Report</asp:LinkButton>
    </div>
    <br />
    <div class="table-responsive">
        

        <asp:GridView id="Timesheet" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-striped  dt-responsive nowrap datatable1" width="100%" cellspacing="0" DataKeyNames="TS_ID" onrowdeleting="Timesheet_Delete" AutoGenerateColumns ="false">
        <Columns>
            <asp:BoundField DataField ="TS_ID" HeaderText ="Id" />
            <asp:BoundField DataField ="TS_Date" HeaderText ="Date" DataFormatString = "{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField ="Status_Name" HeaderText ="Working Status" />
            <asp:BoundField DataField ="Project" HeaderText ="Project" />
            <asp:BoundField DataField ="Job_Description" HeaderText ="Task" />
            <asp:BoundField DataField ="Progress" HeaderText ="Progress" />
            <asp:BoundField DataField ="Working_Hour" HeaderText ="Working Hour" />
            <asp:BoundField DataField ="Emp_Name" HeaderText ="Employee" />
          
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
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnkedit_Click" class="fa fa-edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
         
        </Columns>
    </asp:GridView> 
    </div>

</div>
            <asp:ScriptManager ID ="script1" runat ="server"></asp:ScriptManager>
        <%--<asp:UpdatePanel ID ="Panle1" runat ="server"><ContentTemplate>--%>
<div class="modal fade" id="createmodal" tabindex="-1" role="dialog" style="z-index:10003" data-backdrop="static" data-keyboard="false" aria-labelledby="CreateTitle" aria-hidden="true">
    
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            
            <div class="modal-header">
                 <div class="modal-tittle">
        <h2 class="header">
       
        Enter Working details
        </h2></div>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                
        
            </div>
              
            <div class="modal-body CreateJob">
                






        <div class="form-group">

            <label for="Date">Date</label>
            <input type="date" runat="server" class="form-control"  id="TS_Date">
                
                   <label id="val_date" runat="server"  style="color:red; font-size:11px;"></label>
<%--            <asp:Label ID="val_date" runat="server" ForeColor="Red"> </asp:Label>--%>

           
            <p id="Date" style="color:red;"></p>


        </div>
        <div class="form-group">
            <label for="Status">Working Status</label>
<%--            @Html.DropDownList("listItems", null, "Select Status", htmlAttributes: new { @class = "form-control", @id = "Status_FKID" })--%>
             <%--<asp:DropDownList ID="working_status_list" class="form-select" runat="server" AutoPostBack="true"> </asp:DropDownList>--%>
            
             <asp:DropDownList ID="working_status_list" class="form-control" runat="server" AppendDataBoundItems="true">
                 <asp:ListItem Value="0" Text="Select Status"></asp:ListItem>  
        </asp:DropDownList>  
             <label id="val_status" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Status_MSG" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Project_m">Project</label>
<%--            <input type="text" class="form-control" value="" id="Project_m">--%>
            
            <asp:TextBox runat="server"  class="form-control" id="Project_m"></asp:TextBox>
                         <label id="val_project" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Project_p" style="color:red;"></p>
        </div>

        <div class="form-group">
            <label for="Job_FKID">Job Assigned</label>
<%--            @Html.DropDownList("listItems4", null, "Select Job", htmlAttributes: new {  @class = "form-control", @id = "Job_FKID" })--%>
             <asp:DropDownList ID="job_list" class="form-control" runat="server" AppendDataBoundItems="true">  <asp:ListItem> </asp:ListItem>  
        </asp:DropDownList>  
  <label id="val_job" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Job_FKID" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Progress_m">Progress</label>

            <asp:TextBox runat="server"  class="form-control" id="Progress_m"></asp:TextBox>
              <label id="val_progress" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Progress_p" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Hour">Working Hour</label>
<%--            <input type="text" class="form-control" id="Working_Ho ur">--%>
            <asp:TextBox runat="server"  class="form-control" id="Working_Hour"></asp:TextBox>
                          <label id="val_wor_hr" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Hour" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Employee">Employee</label>
<%--            @Html.DropDownList("listItems2", null, "Select Employee", htmlAttributes: new {  @class = "form-control", @id = "Emp_FKID" })--%>
              <asp:DropDownList ID="employeelist" class="form-control" runat="server" AppendDataBoundItems="true">  
<%--                  <asp:ListItem>Select Employee</asp:ListItem>--%>
                                   <asp:ListItem Value="0" Text="Select Employee"></asp:ListItem>  

        </asp:DropDownList>
            <label id="val_employee" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Employee_MSG" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Department">Department</label>
<%--            @Html.DropDownList("listItems3", null, "Select Department", htmlAttributes: new { @class = "form-control", @id = "Dept_FKID" })--%>
              <asp:DropDownList ID="departmentlist" class="form-control" runat="server" AppendDataBoundItems="true">
                  <%--<asp:ListItem>Select Department</asp:ListItem>--%>
                 <asp:ListItem Value="0" Text="Select Department"></asp:ListItem>  

        </asp:DropDownList>
                        <label id="val_dept" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Department_MSG" style="color:red;"></p>
        </div>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary closeing" data-dismiss="modal">Close</button>
<%--                <button type="button" class="btn button-1 CreateData">Create</button>--%>
                <asp:Button runat="server" class="btn button-1 CreateData" id="btnLogin" Text="Create" OnClick="save_timesheet" />
            </div>
        </div>
    </div>
</div>
   <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" style="z-index:10003" data-backdrop="static" data-keyboard="false" aria-labelledby="EditTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-content" style="padding: 6px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h3 class="header" id="EditpayModalLabel">
                        Edit Timesheet
                    </h3>

                </div>
                <div class="form-group">

            <label for="Date">ID</label>
                            <input type="Text" runat="server" class="form-control"  id="idname" disabled="">

       
           
            <p id="Date_edit1" style="color:red;"></p>


        </div>

                <div class="form-group">

            <label for="Date">Date</label>
<%--            <input type="date" runat="server"  id="Date1">--%>
            <asp:Textbox runat="server" ID="Date2"  class="form-control" TextMode="Date"></asp:Textbox>
                   <label id="val_dateedit" runat="server"  style="color:red; font-size:11px;"></label>

           
            <p id="Date_edit" style="color:red;"></p>


        </div>
        <div class="form-group">
            <label for="Status">Working Status</label>
<%--            @Html.DropDownList("listItems", null, "Select Status", htmlAttributes: new { @class = "form-control", @id = "Status_FKID" })--%>
             <%--<asp:DropDownList ID="working_status_list" class="form-select" runat="server" AutoPostBack="true"> </asp:DropDownList>--%>
            
             <asp:DropDownList ID="DropDownList1" class="form-control" runat="server"  AppendDataBoundItems="true">  <asp:ListItem>Select Status</asp:ListItem>  
        </asp:DropDownList>  
              <label id="val_workstatus" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Status_Edit" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Project_m">Project</label>
<%--            <input type="text" class="form-control" value="" id="Project_m">--%>
            
            <asp:TextBox runat="server"  class="form-control" id="TextBox1" readonly="true"></asp:TextBox>

            <p id="Project_edit" style="color:red;"></p>
        </div>

        <div class="form-group">
            <label for="Job_FKID">Job Assigned</label>
<%--            @Html.DropDownList("listItems4", null, "Select Job", htmlAttributes: new {  @class = "form-control", @id = "Job_FKID" })--%>
             <asp:DropDownList ID="DropDownList2" class="form-control" runat="server" AppendDataBoundItems="true">  <asp:ListItem>Select Job</asp:ListItem>  
        </asp:DropDownList> 
                         <label id="val_editjob" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Job_edit" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Progress_m">Progress</label>

            <asp:TextBox runat="server"  class="form-control" id="TextBox2"></asp:TextBox>
     <label id="val_editprogress" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Progress_edit" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Hour">Working Hour</label>
<%--            <input type="text" class="form-control" id="Working_Ho ur">--%>
            <asp:TextBox runat="server"  class="form-control" id="TextBox3"></asp:TextBox>
                 <label id="val_editwrkhr" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Hour_edit" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Employee">Employee</label>
<%--            @Html.DropDownList("listItems2", null, "Select Employee", htmlAttributes: new {  @class = "form-control", @id = "Emp_FKID" })--%>
              <asp:DropDownList ID="DropDownList3" class="form-control" runat="server" AppendDataBoundItems="true">  <asp:ListItem>Select Employee</asp:ListItem>
        </asp:DropDownList>
                             <label id="val_editemployee" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Employee_MSG_edit" style="color:red;"></p>
        </div>
        <div class="form-group">
            <label for="Department">Department</label>
<%--            @Html.DropDownList("listItems3", null, "Select Department", htmlAttributes: new { @class = "form-control", @id = "Dept_FKID" })--%>
              <asp:DropDownList ID="DropDownList4" class="form-control" runat="server" AppendDataBoundItems="true">  <asp:ListItem>Select Department</asp:ListItem>
        </asp:DropDownList>
 <label id="val_editdepart" runat="server"  style="color:red; font-size:11px;"></label>

            <p id="Department_edit" style="color:red;"></p>
        </div>


                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary closeing " data-dismiss="modal">Close</button>
                    <%--<button type="button" id ="button_002" OnClick class="btn btn-primary button-1 EditData">Save</button>--%>
                     <asp:Button id="button_002" class="btn button-1 CreateData" Text="Update" OnClick="LinkButton2_Click" runat="server"/>
                </div>
            </div>
        </div>
    </div>
            

<%--            </ContentTemplate>
            </asp:UpdatePanel>--%>
</asp:Content>


