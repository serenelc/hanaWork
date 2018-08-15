﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userAnswer.aspx.vb" Inherits="Survey.userAnswer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet">
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>
<body style="background: linear-gradient(#a9c5f2, #619af4);">

    <form id="form1" runat="server" method="get">

        <div style="background-color:inherit; height: 100px" >
            <div id="userInfo" class="sidenav">
                <label id= "info"><%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%></label> 
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float:left;" />
        </div>
        
        <div id ="main" class="container" style="padding: 20px; background-color:white; min-height: 100%; width: 70%;">
            
            <div>
                <h1 style="text-align: center;"> Survey Title (get info from database) </h1>
                <br>
            </div>

            <div>
                <p> 
                    Please fill out the following survey.
                </p>
            </div>

            <div id = "footer" style="background-color: white; height: 70px;">
                <br>
                <asp:button runat="server" id="btnSave" type="button" class="btn btn-success"
                    style="float: right" Text="Save"/>
                <asp:button runat="server" id="btnBack" type="button" class="btn btn-warning"
                    style="float: left;" Text="Back"/>
             </div>
        
        </div>

        <%--Script to confirm logout--%>
        <script>
            function confirmLogout() {
                return confirm("Are you sure you would like to logout?");
            }
        </script>

        <%--Script to check if user is admin--%>
        <script>
            function isAdmin() {
                <%--if (<%=Session("UserType")%> = "ADMIN") {
                    console.log("user is admin");
                }
                else {
                    console.log("user is not admin");
                }--%>
                console.log(<%=Session("UserType")%>);
            }
            isAdmin();
        </script>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>
 </body>
</html>