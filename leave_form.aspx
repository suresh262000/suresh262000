<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="leave_form.aspx.cs" Inherits="HRMS.leave_form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

            var drop = document.getElementById("ContentPlaceHolder1_DropDownList3").value;
            if (drop != 0) {
                document.getElementById("ContentPlaceHolder1_Employe_Name").value = drop;
            }
            var drop1 = document.getElementById("ContentPlaceHolder1_DropDownList4").value;
            if (drop1 != 0) {
                document.getElementById("ContentPlaceHolder1_Employe_ID").value = drop1;
            }
        }
    </script>
    <style>
        .span{
            color:red;
        }
    </style>
     
 
    
    <h2 class="header">
       
        <i data-feather="check-square"></i>
           Leave Form

        

    </h2>

    <div>
        <button type="button" class="btn button-1 createmodal" data-toggle="modal" data-target="#createmodal">
            + Apply Leave Form</button>
 
           <asp:button type="button" style="margin-left:700px;" id="excel"  runat="server" class="btn button-1" OnClick="excel_Click1" Text="Download Report"></asp:button>
        
    </div>
         <div class="table-responsive">
        <br />
             
<asp:GridView ID="leaveform" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-striped  dt-responsive nowrap datatable1" width="100%" 
    DataKeyNames="LEAVE_ID" OnRowDeleting="Employee_Delete" OnRowCommand="leaveform_RowCommand1" AutoGenerateColumns ="False" >
        <Columns>
            <asp:BoundField DataField ="LEAVE_ID" HeaderText ="Leave ID" />

            <asp:BoundField DataField ="EMP_FKID" HeaderText ="EMP ID" />
            <asp:BoundField DataField ="EMP_NAME" HeaderText ="Employ Name" />
            
            <asp:BoundField DataField ="LEAVE_START" HeaderText ="Leave start" DataFormatString = "{0:dd-MM-yyyy}" />
            <asp:BoundField DataField ="LEAVE_END" HeaderText ="Leave End"  DataFormatString = "{0:dd-MM-yyyy}"/>
            <asp:BoundField DataField ="LEAVE_TYPE" HeaderText ="Leave Type" />
            <asp:BoundField DataField ="LEAVE_REASON" HeaderText ="Reason" />
                        <asp:BoundField DataField ="APPLY_STATUS" HeaderText ="Status" />
            
           <asp:TemplateField>
                <HeaderTemplate >Delete</HeaderTemplate>
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
                       Apply Leave
                    </h3>

                </div>
            
           <div class="form-group">
            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>

            <label for="Employe">Employee ID</label><span style="color:red">*</span>
               
              <asp:DropDownList class="form-control" ID="DropDownList4" onchange="valentr()" runat="server">
                            </asp:DropDownList>

            <asp:TextBox type="text" runat="server" class="form-control" id="Employe_ID" style="display:none"  ></asp:TextBox>
               <asp:Label runat="server" id="errorid" ForeColor="Red">


               </asp:Label>
        </div>
        <div class="form-group">
            <%--<input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">--%>

            <label for="SalaryAmount">Employee Name</label><span style="color:red">*</span>
            
              <asp:DropDownList class="form-control" ID="DropDownList3" onchange="valentr()" runat="server">
                            </asp:DropDownList>
            <asp:TextBox type="text" runat="server" class="form-control" style="display:none" id="Employe_Name" ></asp:TextBox>
             <asp:Label runat="server" id="errorname" ForeColor="Red"></asp:Label>
        </div>

        
        <div class="form-group">
            <label for="BonusAmount">Leave Start</label><span style="color:red">*</span>
            
           <asp:TextBox ID="from_date" runat="server" class="form-control" placeholder="From" style="width:100%" type="date" ></asp:TextBox>
             <asp:Label runat="server" id="errorstart" ForeColor="Red"></asp:Label>


            
        </div>
        <div class="form-group">
            <label for="BonusAmount">Leave End</label><span style="color:red">*</span>
           <asp:TextBox ID="end_date" runat="server" class="form-control" placeholder="From" style="width:100%" type="date" ></asp:TextBox>
             <asp:Label runat="server" id="errorend" ForeColor="Red"></asp:Label>

            
            
           
            
        </div>



        <div class="form-group">
            <label for="leavetype">Leave Type</label><span style="color:red">*</span>
               <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" style="width:100%" >  
                   <asp:ListItem Enabled="true" Text= "Please Select" Value= "0"></asp:ListItem>


             
            <asp:ListItem Value="Causal">Causal</asp:ListItem>  
            <asp:ListItem Value="Medical">Medical</asp:ListItem>  
            <asp:ListItem Value="Emergency">Emergency</asp:ListItem>  
            <asp:ListItem Value="Vacation">Vacation</asp:ListItem>  
        </asp:DropDownList>
                    <asp:Label runat="server" id="errortype" ForeColor="Red"></asp:Label>
            </div>

        <div class="form-group">
            <label for="reason">Reason</label><span style="color:red">*</span>
             <asp:TextBox type="text" runat="server" class="form-control" id="reason"></asp:TextBox>
             <asp:Label runat="server" id="errorreason" ForeColor="Red"></asp:Label>

        </div>
       
                
                <div class="modal-footer">
                    <asp:button type="button"  runat="server" class="btn button-1" OnClick="close" Text="Close"></asp:button>
                    <asp:button type="button" runat="server" OnClick="apply_Click" class="btn button-1 " Text="Apply" id="apply"></asp:button>
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
                       Edit Applied Leave
                    </h3>

                </div>
              
           <div class="form-group">
            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>

            <label for="ID">Leave ID</label>
               <asp:TextBox ID="TextBox6"  type="text" class="form-control"  runat="server" ReadOnly="True" ></asp:TextBox>
        </div>
           <div class="form-group">
            <%--@* <input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">*@--%>

            <label for="Payroll_ID">Employee ID</label>
               <asp:TextBox ID="TextBox1"  type="text" class="form-control"  runat="server" ReadOnly="True" ></asp:TextBox>
        </div>
        <div class="form-group">
            <%--<input type="text" class="form-control" style="display:none" value="@Model.Payroll_ID" id="Payroll_ID">--%>

            <label for="SalaryAmount">Employee Name</label>
            <asp:TextBox type="text" runat="server" class="form-control" id="TextBox2" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="BonusAmount">Leave Start</label><span style="color:red">*</span>
            
           <asp:TextBox ID="TextBox3" runat="server" class="form-control" placeholder="From" style="width:100%" type="date"></asp:TextBox>
            <asp:Label runat="server" id="error1" ForeColor="Red"></asp:Label>
         
            
        </div>
        <div class="form-group">
            <label for="BonusAmount">Leave End</label><span style="color:red">*</span>
           <asp:TextBox ID="TextBox4" runat="server" class="form-control" placeholder="From" style="width:100%" type="date" ></asp:TextBox>
           
             <asp:Label runat="server" id="error2" ForeColor="Red"></asp:Label>
            
           
            
        </div>



        <div class="form-group">
            <label for="OtherAllowance">Leave Type</label><span style="color:red">*</span>
               <asp:DropDownList ID="DropDownList2" runat="server" class="form-control" style="width:100%" >  
             <asp:ListItem disabled="true" Enabled="true" Text= "Please Select" Value= "0"></asp:ListItem> 
            <asp:ListItem Value="Causal">Causal</asp:ListItem>  
            <asp:ListItem Value="Medical">Medical</asp:ListItem>  
            <asp:ListItem Value="Emergency">Emergency</asp:ListItem>  
            <asp:ListItem Value="Vacation">Vacation</asp:ListItem>  
        </asp:DropDownList>
        <asp:Label runat="server" id="error3" ForeColor="Red"></asp:Label>
            </div>
        <div class="form-group">
            <label for="reason">Reason</label><span style="color:red">*</span>
             <asp:TextBox type="text" runat="server" class="form-control" id="TextBox5"></asp:TextBox>
             <asp:Label runat="server" id="error4" ForeColor="Red"></asp:Label>
        </div>
       
                
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary closeing" data-dismiss="modal">Close</button>
                    <asp:button type="button" runat="server" OnClick="update_Click" class="btn button-1 " Text="Update" id="Button1"></asp:button>
                </div>
            
        </div>
    </div>
            
        </div>
      








    


</asp:Content>

