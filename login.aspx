<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HRMS.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    
    <link href="css/lib/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/css/site.css" rel="stylesheet" />
    
    <script src="css/js/site.js"></script>
  
    <!-- Template CSS -->
    <link href="css/lib/remixicon/fonts/remixicon.css" rel="stylesheet" />
    <link href="css/assets/css/style.min.css" rel="stylesheet" />
    <script src="css/lib/jquery/jquery.min.js"></script>
    <!-- Template Scripts -->
    <script src="css/lib/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="css/lib/feathericons/feather.min.js"></script>
    <script src="css/lib/perfect-scrollbar/perfect-scrollbar.min.js"></script>
    <script src="css/assets/js/script.js"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
 
    <script src="css/js/bootstrap.min.js"></script>
    <link href="css/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <style type="text/css">
        .auto-style1 {
            width: 70%;
            height: 161px;
        }

    </style>
    <script>
        $('#myModal').on('shown.bs.modal', function () {
            $('#myInput').trigger('focus')
        })
    </script>

</head>
<body style="background-color:#f1eeee">

    <form id="form1" runat="server">
        
             <div class="sidebar-header ">

        <a href="#" class="sidebar-logo">    


            <img style="margin-left: 30px;margin-bottom: -563px; " src="css/img/yunic_logo.png" class="auto-style1" />
        </a>

        <%--<a href="#" class="sidebar-logo-text">HRM<span>S</span></a>--%>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-4" style="
    height: 90%;"></div>
            <div class="col-sm-4" style="margin-left: 186px;    margin-top: 46px;    border-style: groove;
    background-color: lightyellow;
    padding: 36px;
">
               <%-- <form class="form-horizontal" asp-action="Index" style="margin-top: 100px;">--%>
                    <div class="login-box" style=" margin-left: -12px;">
                        
                        <h1>Sign In</h1>
                       
                    </div>
                    <div asp-validation-summary="All" class="text-danger"></div>
                    

                      <div class="form-group">
                      
                        <div class="input-group mb-3">
                              <asp:Label ID="Label" runat="server" class="control-label">User Name</asp:Label>
  <div class="input-group-prepend">

                        <asp:TextBox id="Admin_Name" placeholder="Enter Username"  runat="server" class="form-control" type="text" style="margin-top: 10px;height: 44px;" />
          <span class="input-group-text" id="basic-addon" style="
    /* height: 44px; */
    margin-top: 3%;
"><i class="fa fa-user-circle-o" aria-hidden="true"></i></span>
  </div>
</div>
                    </div>


                    <div class="form-group">
                      
                        <div class="input-group mb-3">
                              <asp:Label ID="Label2" runat="server" class="control-label">Password</asp:Label>
  <div class="input-group-prepend">

         <asp:TextBox   id="Admin_Password" placeholder="Enter Password" class="form-control"  runat="server" aria-label="Username" aria-describedby="basic-addon1"  type="password" style="margin-top: 10px;height: 44px;"></asp:TextBox> 
          
      <span class="input-group-text" id="basic-addon1" style="
    margin-top: 10px;"><i id="togglePassword" class="fa fa-eye" aria-hidden="true"></i></span>

      <script type="text/javascript">
    var togglePassword = document.querySelector('#togglePassword');
          var password = document.querySelector('#Admin_Password');
    togglePassword.addEventListener('click', function (e) {
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);
        this.classList.toggle('fa-eye-slash');
    });
      </script>

  </div>

</div>
                    </div>
                    <div class="form-group">

                        <asp:Button ID="loginbtn" runat="server" type="submit" text="Login" OnClick="login_click" class="form-control" style="font-weight: 6px;background-color: yellowgreen;" />
                     <%--   <input asp-action="LoginSubmit" runat="server" type="submit" value="Login" OnClick="login_click" />
                    --%>

                    </div>
                <div>
                    <%--<a  id="forgotpwd" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">Forgot Password?</a>--%>
 <a id='LinkButton1' href='#' runat='server'  data-toggle="modal" data-target="#exampleModalCenter" style="
    font-size: 72%;
    margin-left: 62%;"
>FORGOT PASSWORD?</a>

                        
<!-- Button trigger modal -->
</div>
                    <div>
                     <asp:Label ID="Label4" runat="server" class="control-label" ForeColor="Red"></asp:Label>

               <%-- </form>--%>
            </div>

    </div>
        </div>
        </div>

    <!-- Modal -->
<!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Forgot password</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          Email Address:

        <asp:TextBox ID="txtemail" runat="server" Width = "250" />
<br />
<asp:Label ID="lbresult" runat="server" />
<br />
      </div>
      <div class="modal-footer">
          <asp:Button type="button" runat="server" Text="Submit" Width="156px" OnClick="btnsend_Click" />
        <%--<asp:Button type="button" class="btn btn-secondary" data-dismiss="modal">Back to login</asp:Button>
        <asp:Button type="button" class="btn btn-primary" OnClick="btnsend_Click">Send email</asp:Button>--%>
      </div>
    </div>
  </div>
</div>
            </form>

</body>
</html>
