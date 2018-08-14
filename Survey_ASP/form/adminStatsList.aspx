﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminStatsList.aspx.vb" Inherits="Survey.adminStatsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Statistics</title>
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>
<body style="background-color: #a9c5f2;">

    <form id="form1" runat="server"   method="get">

        <div style="background-color:#619af4; height: 100px" >
        </div>
        
        <div id ="main" class="container" style="padding: 20px; background-color:white; min-height: 100%;">
            
            <div>
                <h1 style="text-align: center;"> Statistics </h1>
                <br>
            </div>

            <div>
                <p> 
                    This page will hold a list of all the surveys. When you click onto each survey 
                    it will lead you to another page with the statistics for that survey.
                            
                </p>
            </div>

            <div id = "footer" style="background-color: white; height: 70px;">
                    <br>
                <%--Button does not currently work--%>
                    <asp:button runat="server" id="btnLogout" type="button" class="btn btn-danger"
                    style="float: right" Text="Logout" onclick="btnLogout_Click" OnClientClick="return confirmLogout()"/>
                  <asp:button runat="server" id="btnBack" type="button" class="btn btn-warning"
                    style="float: left" Text="Back"/>
                </div>
        
        </div>

          <script>
            function confirmLogout() {
                return confirm("Are you sure you would like to logout?");
            }
        </script>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

</form>

</body>
</html>