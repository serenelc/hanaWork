﻿Imports System.Data.SqlClient

Public Class userAnswer
    Inherits System.Web.UI.Page

    Public listContent As New List(Of String)
    'Public txtTitle = ""
    'Public txtDesc = ""
    Public xcreateDate = Date.Now
    Public xsurveyId As Integer = 0
    Public xquestionId As Integer = 0
    Public prmSubmitId As Integer = 0
    Public prmSurveyId As Integer = 0
    Public prmUserAnswerId As Integer = 0
    Public prmQuestionID As Integer = 0
    Public prmAnswerID As Integer = 0
    Public prmAnswerComment As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If
        If IsPostBack() = False Then
            Dim subId = Request.QueryString("subjectId")
            Call getSurveyContent(subId)
        End If
    End Sub

    Protected Sub getSurveyContent(xSubId)
        Dim dt As New DataTable
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Try
            con.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "SELECT a.*, b.sectionId, b.sectionName, c.questionId, c.questionName, c.questionType, 
                               d.answerId, d.answerName
                               From surveyMaster a left Join surveySection b on a.subjectId = b.subjectId
                               Left Join surveyQuestion c on b.sectionId = c.sectionId 
                               Left Join surveyAnswer d on c.questionId = d.questionId
                               where b.subjectId =" & xSubId.ToString()

            'create a DataReader and execute the SqlCommand
            Dim MyDataReader As SqlDataReader = cmd.ExecuteReader()

            'load the reader into the datatable
            dt.Load(MyDataReader)

            'clean up
            MyDataReader.Close()

            Dim xsubjectId As Integer = 0
            Dim xsubjectName As String = ""
            Dim xsubjectDetail As String = ""
            Dim xstatus As String = ""
            Dim xstatusComp As String = ""
            Dim xopenDate As Date = Now
            Dim xcloseDate As Date = Now
            Dim xcreateDate As Date = Now
            Dim xcreateBy As String = ""
            Dim xsectionId As Integer = 0
            Dim xsectionName As String = ""
            Dim xquestionId As Integer = 0
            Dim xquestionName As String = ""
            Dim xquestionType As String = ""
            Dim xanswerId As Integer = 0
            Dim xanswerName As String = ""


            Dim xTitle As Integer = 0
            Dim xSection As Integer = 0
            Dim xQuestion As Integer = 0
            If dt.Rows.Count > 0 Then
                For Each r In dt.Rows
                    xsubjectId = r("subjectId")
                    xsubjectName = r("subjectName")
                    xsubjectDetail = r("subjectDetail")
                    xstatus = r("status")
                    xstatusComp = r("statusComp")
                    xopenDate = r("openDate")
                    ' xcloseDate = r("closeDate")
                    xcreateDate = r("createDate")
                    xcreateBy = r("createBy")

                    xsectionId = r("sectionId")
                    xsectionName = r("sectionName")

                    xquestionId = r("questionId")
                    xquestionName = r("questionName")
                    xquestionType = r("questionType")
                    If IsDBNull(r("answerId")) = True Then
                        xanswerId = 0
                    Else
                        xanswerId = r("answerId")
                    End If
                    If IsDBNull(r("answerName")) = True Then
                        xanswerName = ""
                    Else
                        xanswerName = r("answerName")
                    End If

                    If xTitle = 0 Then
                        listContent.Add("surveyId=" + xsubjectId.ToString())
                        listContent.Add("txtTitle=" + xsubjectName)
                        listContent.Add("txtDesc=" + xsubjectDetail.ToString())
                        listContent.Add("status=" + xstatus)
                        listContent.Add("statusComp=" + xstatusComp.ToString())
                        'listContent.Add("openDate=" + xopenDate.ToString())
                        'listContent.Add("closeDate=" + xcloseDate.ToString())
                        'listContent.Add("createDate=" + xcreateDate.ToString())
                        listContent.Add("createBy=" + xcreateBy)
                        xTitle = 1
                    End If

                    If xSection <> xsectionId Then
                        listContent.Add("sectionId=" + xsectionId.ToString())
                        listContent.Add("sectionName=" + xsectionName)

                        xSection = xsectionId
                    End If

                    If xQuestion <> xquestionId Then
                        listContent.Add("questionId=" + xquestionId.ToString())
                        listContent.Add("questionName=" + xquestionName)
                        listContent.Add("questionType=" + xquestionType)

                        xQuestion = xquestionId
                    End If

                    listContent.Add("answerId=" + xanswerId.ToString())
                    listContent.Add("answerName=" + xanswerName)

                Next


            End If


        Catch ex As Exception
            Dim errorMsg = "Error While getting information from database"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
        Finally
            con.Close()
        End Try

        printListContent()

    End Sub


    Protected Sub printListContent()

        Dim t As String = "<p><h1 style='text-align: center' id='txtTitle' name='txtTitle'>"
        t += (listContent.Item(1)).Substring(listContent.Item(1).IndexOf("=") + 1) + "</h1></p>"

        Dim d As String = "<p  id='txtDesc' name='txtDesc'>" + (listContent.Item(2)).Substring(listContent.Item(2).IndexOf("=") + 1) + "</p>"

        Dim i As Integer
        Dim s As String = ""
        Dim rFlag As Integer = 0
        Dim gFlag As Integer = 0
        Dim rNameFlag As Integer = 0
        Dim gNumRad As Integer = 0
        Dim answerIdFlag As Integer = 0
        For i = 0 To (listContent.Count - 1)
            Dim v = listContent.Item(i)
            If (v.Contains("surveyId=")) Then
                xsurveyId = v.Substring(v.IndexOf("=") + 1)
            End If
            If (v.Contains("questionId=")) Then
                xquestionId = v.Substring(v.IndexOf("=") + 1)
                rNameFlag = v.Substring(v.IndexOf("=") + 1)
            End If
            If (v.Contains("answerId")) Then
                'gIdFlag = v.Remove(0, questionIdLength)
                answerIdFlag = v.Substring(v.IndexOf("=") + 1)
            End If
            If (v.Contains("sectionName")) Then
                s += "<br> <h3 style='text-decoration: underline' id='sectionTitle_name' name='sectionTitle_name'>" + v.Substring(v.IndexOf("=") + 1) + "</h3>"
            End If
            If (v.Contains("questionName")) Then
                Dim gT = listContent.Item(i + 1)
                Dim gI = listContent.Item(i + 2)
                If (Not gI.Contains("answerId=0") And gT.Contains("grid")) Then
                    s += "<div class='form-check-inline'><div class='col-7'><p id='questionInput_name' name='questionInput_name'>" + v.Substring(v.IndexOf("=") + 1) + "</div><div class='col-5'>"
                Else
                    s += "<div><p style='font-weight: bold' id='questionInput_name' name='questionInput_name'>" + v.Substring(v.IndexOf("=") + 1) + "</p>"
                End If
            End If
            If (v.Contains("questionType")) Then
                If (v.Contains("radio")) Then
                    rFlag = 1
                    gFlag = 0
                    s += "<p><div class='form-check-inline'>"
                End If
                If (v.Contains("shortanswer")) Then
                    rFlag = 0
                    gFlag = 0
                    s += "<p><textarea id = 'short' class = 'form-control' rows = '2' placeholder = 'Answer' name='short" + "_Q" + xquestionId.ToString() + "_A" + answerIdFlag.ToString() + "'"
                    s += "></textarea></div>"
                End If
                If (v.Contains("grid")) Then
                    Dim w = listContent.Item(i + 1)
                    If (Not w.Contains("answerId=0")) Then
                        gFlag = 1
                    End If
                    rFlag = 0
                End If
            End If
            If (v.Contains("answerName") And rFlag = 1) Then
                s += "<input type='radio' class='form-check-input' id='rad' name='rad" + "_Q" + xquestionId.ToString() + "_A" + answerIdFlag.ToString() + "'"
                s += "style ='padding-left: 30px;'>"
                s += "<label class='form-check-label' style='padding-right: 20px;'>" + v.Substring(v.IndexOf("=") + 1) + "</label>"
            End If
            If (v.Contains("answerName") And gFlag = 1) Then
                gNumRad = v.Substring(v.IndexOf("=") + 1)
                s += "<input type='radio' class='form-check-input' id='grid' name='grid" + "_Q" + xquestionId.ToString() + "_A" + answerIdFlag.ToString() + "'"
                s += "style ='padding-left: 30px;'>"
                s += "<label class='form-check-label' style='padding-right: 20px;'>" + gNumRad.ToString() + "</label>"
            End If
            If ((v.Contains("sectionId") Or v.Contains("questionId")) And rFlag = 1) Then
                s += "</div><br></div>"
                rFlag = 0
            End If
            If ((v.Contains("sectionId") Or v.Contains("questionId")) And gFlag = 1) Then
                s += "</div></p></div><br>"
                gFlag = 0
                Dim n = listContent.Item(i + 2)
                If (Not n.Contains("grid")) Then
                    s += "</div>"
                End If
            End If
        Next

        title.Text = t
        description.Text = d
        frm.Text = s
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("userSurveyList.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'If (validateData() = False) Then
        '    Exit Sub
        'End If

        Dim ClientQueryList = Request.QueryString
        Dim aa = ClientQueryList
        Dim bb = ClientQueryString

        Dim strRep = ClientQueryString.Replace("+", " ")
        Dim strArr() = strRep.Split("&")
        'Dim val As String = ""

        'Open connection database
        Dim SQLConn As New SqlConnection(My.Settings.ConnStringDatabaseSurvey)
        Dim SQLTran As SqlTransaction

        SQLConn.Open()
        SQLTran = SQLConn.BeginTransaction

        Try
            'I don't know how to get the surveyId. Currently hardcoded.
            prmSurveyId = 17
            If (SaveSurveyUserSubmit(SQLConn, SQLTran) = False) Then Throw New Exception("Save SurveyUserSubmit fail!")

            For i As Integer = 0 To strArr.Count - 1
                Dim xval As String = strArr(i).ToString()

                If (Not xval.Contains("btnSave")) Then
                    Dim indexE As Integer = xval.IndexOf("=")
                    Dim indexQ As Integer = xval.IndexOf("Q")
                    Dim indexA As Integer = xval.IndexOf("A")
                    Dim qIdLength As Integer = indexA - indexQ - 2
                    Dim aIdLength As Integer = indexE - indexA - 1

                    prmQuestionID = xval.Substring(indexQ + 1, qIdLength)
                    prmAnswerID = xval.Substring(indexA + 1, aIdLength)
                    prmAnswerComment = xval.Substring(indexE + 1)
                    replaceSymbol(prmAnswerComment)

                    'Dim updateValue As String = Val.Substring(Val.IndexOf("=") + 1)
                    'If (SaveSurveyUserAnswer(SQLConn, SQLTran, updateValue) = False) Then Throw New Exception("Save SurveyUserAnswer fail!")
                    If (SaveSurveyUserAnswer(SQLConn, SQLTran) = False) Then Throw New Exception("Save SurveyUserAnswer fail!")
                End If

            Next

            SQLTran.Commit()
            'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Save successful!')", True)
            Response.Redirect("userSurveyList.aspx")
        Catch ex As Exception
            If (SQLTran IsNot Nothing) Then SQLTran.Rollback()
            Dim errorMsg = "Error While inserting record On table..." & ex.Message & ",Insert Records"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "Save')", True)
        Finally
            SQLConn.Close()
        End Try
    End Sub
    'Save SaveSurveyUserSubmit
    Private Function SaveSurveyUserSubmit(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction) As Boolean

        Dim str As String = String.Empty
        'New

        str = "INSERT INTO SurveyUserSubmit(subjectId, submitDate, submitBy) "
        str = str + " VALUES(@subjectId, @submitDate, @submitBy); Set @submitID = SCOPE_IDENTITY() "
        Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                             .Transaction = SQLTran,
                                                             .CommandType = CommandType.Text,
                                                             .CommandText = str}
            With SQLCmd
                .Parameters.Clear()

                .Parameters.AddWithValue("@subjectId", prmSurveyId)
                .Parameters.AddWithValue("@submitDate", xcreateDate)
                .Parameters.AddWithValue("@submitBy", Session("En"))

                Dim prm_submitid As System.Data.SqlClient.SqlParameter = New SqlParameter("@submitID", SqlDbType.Int)
                prm_submitid.Direction = ParameterDirection.Output
                prm_submitid.SqlDbType = SqlDbType.Int
                .Parameters.Add(prm_submitid)
                .ExecuteNonQuery()

                prmSubmitId = prm_submitid.Value
            End With
        End Using

        Return True

    End Function

    'Save SaveSurveyUserAnswer
    Private Function SaveSurveyUserAnswer(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction) As Boolean

        Dim str As String = String.Empty
        'New

        str = "INSERT INTO SurveyUserAnswer(submitId, subjectId, questionId, answerId, answerComment) "
        str = str + " VALUES(@submitId, @subjectId, @questionId, @answerId, @answerComment); Set @userAnswerId = SCOPE_IDENTITY() "
        Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                             .Transaction = SQLTran,
                                                             .CommandType = CommandType.Text,
                                                             .CommandText = str}
            With SQLCmd
                .Parameters.Clear()

                .Parameters.AddWithValue("@submitId", prmSubmitId)
                .Parameters.AddWithValue("@subjectId", prmSurveyId)
                .Parameters.AddWithValue("@questionId", prmQuestionID)
                .Parameters.AddWithValue("@answerId", prmAnswerID)
                .Parameters.AddWithValue("@answerComment", prmAnswerComment)

                Dim prm_userAnswerId As System.Data.SqlClient.SqlParameter = New SqlParameter("@userAnswerId", SqlDbType.Int)
                prm_userAnswerId.Direction = ParameterDirection.Output
                prm_userAnswerId.SqlDbType = SqlDbType.Int
                .Parameters.Add(prm_userAnswerId)
                .ExecuteNonQuery()

                prmUserAnswerId = prm_userAnswerId.Value
            End With
        End Using

        Return True

    End Function
    Function validateData()
        Try
            'If txtTitle = "" Then Throw New Exception("Please fill Title!")
            'If txtDesc = "" Then Throw New Exception("Please fill Description!")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & ex.Message & "')", True)
            Return False
        End Try
        Return True
    End Function

    Protected Sub replaceSymbol(sym As String)
        If sym.Contains("%3f") Then
            sym.Replace("%3f", "?")
        End If
        If sym.Contains("%27") Then
            sym.Replace("%27", "'")
        End If
    End Sub

End Class
