﻿Imports System.Data.SqlClient

Public Class admin
    Inherits System.Web.UI.Page

    Public divSecTitle_val = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write(Session("En"))
        Response.Write(Session("Name"))
        Response.Write(Session("UserType"))

    End Sub
    Function ConnectDBUpdate(ConnectionString As String, Sql As String)
        'Try
        '    'make a SqlConnection using the supplied ConnectionString 
        '    Dim MySqlConnection As New SqlConnection(ConnectionString)
        '    Using MySqlConnection
        '        'make a query using the supplied Sql
        '        Dim MySqlCommand As SqlCommand = New SqlCommand(Sql, MySqlConnection)

        '        'open the connection
        '        MySqlConnection.Open()

        '        'create a DataReader and execute the SqlCommand
        '        MySqlCommand.ExecuteNonQuery()

        '        MySqlConnection.Close()
        '    End Using
        '    Return True
        'Catch ex As Exception
        '    Return False
        'End Try

    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        txtTitle.Value = Request.QueryString("txtTitle")
        txtDesc.Value = Request.QueryString("txtDesc")
        'divSecTitle_val = Request.QueryString("divSecTitle_id0")
        Dim xcreateBy = Session("En")
        Dim xcreateDate = Date.Now

        Dim something = Request.QueryString("divSecTitle_name0")
        Dim another = Request.QueryString("divSecTitle_name1")

        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Try
            con.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "INSERT INTO surveyMaster(subjectName,subjectDetail,status,createBy,createDate) _
                                VALUES(@subjectName, @subjectDetail, @status, @createBy,@createDate)"
            With cmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@subjectName", txtTitle.Value)
                .Parameters.AddWithValue("@subjectDetail", txtDesc.Value)
                .Parameters.AddWithValue("@status", "OPEN")
                .Parameters.AddWithValue("@createBy", xcreateBy)
                .Parameters.AddWithValue("@createDate", xcreateDate)

                .ExecuteNonQuery()

            End With

            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Save successful!')", True)
            Response.Redirect("adminHome.aspx")
        Catch ex As Exception
            Dim errorMsg = "Error While inserting record On table..." & ex.Message & ",Insert Records"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "Save')", True)
        Finally
            con.Close()
        End Try

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("adminHome.aspx")
    End Sub
End Class