﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true"  CodeBehind="project.aspx.cs" Inherits="HRMS.payrole" %>
             function openModal1() {
                 $('#createmodal').modal('show');
             }
</script>
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
