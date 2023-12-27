<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetlink.aspx.cs" Inherits="HRMS.resetlink" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        .style1
        {
            width: 44%;
        }
        .style2
        {
            width: 128px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div align="center">
        <table class="style1" align="center">
            <tr>
                <td class="style2">
                    New Password
                </td>
                <td>
                    <asp:TextBox ID="txtpwd" runat="server" Width="158px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter password"    
    ControlToValidate="txtpwd" ForeColor="Red" ></asp:RequiredFieldValidator> 
                    <asp:RegularExpressionValidator ID="Regex1" runat="server" ControlToValidate="txtpwd"
    ValidationExpression="^(?=.*\d)[\d]{6,}$" ErrorMessage="Password must contain: Minimum 6 digit" ForeColor="Red" style="margin-left:-31%" ></asp:RegularExpressionValidator>  
                    <%--<asp:RegularExpressionValidator ID="Regex4" runat="server" ControlToValidate="txtpwd"
    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}"
ErrorMessage="Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" ForeColor="Red" />--%>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Confirm Password
                </td>
                <td>
                    <asp:TextBox ID="txtcofrmpwd" runat="server" Width="158px"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="txtpwd"
                        ControlToValidate="txtcofrmpwd" ErrorMessage="Password does not match" Font-Names="Rockwell"
                        ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="156px" OnClick="btnsubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
