﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Survey.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admin Page</title>
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>
<body style="background-color: #B4E0E3">
    <form id="form1" runat="server">
        <head class="masthead">
            <div class="container d-flex h-100 align-items-center">
                <div class="mx-auto text-center">
                    <img src="../images/header.jpg" style="width: 70%">
                    <h1 class="mx-auto text-uppercase">Employee Satisfaction Surveys</h1>
                    <asp:button runat="server" id="btnLogin" type="button" class="btn btn-secondary"
                    text="Login" Height="47px" Width="92px"/>
                </div>
            </div>
        </head>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
   </form>
</body>
</html>
