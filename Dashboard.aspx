<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HRMS_.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_container">
        <h2 class="header">
            <i data-feather="users"></i>
            Human Resources
        </h2>
        <h2>Dashboard is working in progress!</h2>

        <%--Sub menu's--%>
        <div class="row">
            <div class="col-md-4 wow fadeInUp" data-wow-duration="0.7s" data-wow-delay="0.1s" data-wow-offset="0">
                <h3 class="header">
                    <img src="css/img/administration.png" />
                    Departments
                </h3>
            </div>
            <div class="col-md-4 wow fadeInUp" data-wow-duration="0.7s" data-wow-delay="0.1s" data-wow-offset="0">
                <h3 class="header">
                    <img src="css/img/reward.png" />
                    Employees
                </h3>
            </div>
            <div class="col-md-4 wow fadeInUp" data-wow-duration="0.7s" data-wow-delay="0.1s" data-wow-offset="0">
                <h3 class="header">
                    <img src="css/img/time-management.png" />
                    Time Sheet
                </h3>
            </div>
            <div class="col-md-4 wow fadeInUp" data-wow-duration="0.7s" data-wow-delay="0.1s" data-wow-offset="0">
                <h3 class="header">
                    <img src="css/img/wage.png" />
                    Payroll
                </h3>
            </div>
            <div class="col-md-4 wow fadeInUp" data-wow-duration="0.7s" data-wow-delay="0.1s" data-wow-offset="0">
                <h3 class="header">
                    <img src="css/img/project.png" />
                    Projects
                </h3>
            </div>
            <div class="col-md-4 wow fadeInUp" data-wow-duration="0.7s" data-wow-delay="0.1s" data-wow-offset="0">
                <h3 class="header">
                    <img src="css/img/mail.png" />
                    Leave Apply
                </h3>
            </div>
        </div>
    </div>


</asp:Content>

