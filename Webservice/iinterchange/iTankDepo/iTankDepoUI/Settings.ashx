<%@ WebHandler Language="VB" Class="Settings" %>

Imports System
Imports System.Web
Imports System.IO

Public Class Settings : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim sqlCon As New SqlClient.SqlConnection
            Dim strQuery As String
            Dim da As SqlClient.SqlDataAdapter
            Dim ds As New DataSet
            Dim sbrJsContent As New StringBuilder
            Dim strDepotId As String = HttpContext.Current.Request.QueryString("DepotId")
            sqlCon.ConnectionString = CommonWeb.pub_GetConfigValue("ConnectionString")
            sqlCon.Open()
            strQuery = "SELECT ACTVTY_ID,ACTVTY_NAM,PRCSS_ID,LST_QRY,LST_URL,LST_TTL,LST_CLCNT,PG_URL,PG_TTL,TBL_NAM,ORDR_NO,MNU_TXT,CRT_RGHT_BT,EDT_RGHT_BT,ACTV_BT,ACTVTY_RL FROM ACTIVITY"
            da = New SqlClient.SqlDataAdapter(strQuery, sqlCon)
            da.Fill(ds, "ACTIVITY")
            
            sqlCon.Close()
            sbrJsContent.Append("var DisplayMode = ""Fixed"";" & vbCrLf)
            sbrJsContent.Append("var DisplayWidth = ""1000"";" & vbCrLf)
            sbrJsContent.Append("var DisplayHeight = ""700"";" & vbCrLf)
            sbrJsContent.Append("var Animate = false;" & vbCrLf)
            sbrJsContent.Append("var Activities = new Array();" & vbCrLf)
            If ds.Tables(0).Rows.Count > 0 Then
                For iLoop As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim intcount As Integer = 0
                    If Not ds.Tables("ACTIVITY").Rows(iLoop).Item("ACTVTY_RL").ToString = "" Then
                        Dim strItems() As String = ds.Tables("ACTIVITY").Rows(iLoop).Item("ACTVTY_RL").ToString.Split(CChar(","))
                        If strItems.Length > 0 Then
                            For i As Integer = 0 To strItems.Length - 1
                                If strItems(i).Substring(strItems(i).IndexOf("<") + 1, strItems(i).IndexOf(":", strItems(i).IndexOf("<")) - strItems(i).IndexOf("<") - 1) = "M" Then
                                    Dim result As String
                                    result = strItems(i).Substring(strItems(i).IndexOf(":") + 1, strItems(i).IndexOf(">", strItems(i).IndexOf(":")) - strItems(i).IndexOf(":") - 1)
                                    Dim objCommonData As New CommonUI
                                    Dim bln_KeyExist As Boolean
                                    Dim strKeyValue As String = objCommonData.GetSettings(result, strDepotId, bln_KeyExist)
                                    If bln_KeyExist Then
                                        Dim strMenu() As String = ds.Tables("ACTIVITY").Rows(iLoop).Item("MNU_TXT").ToString.Split(CChar(";"))
                                        If strMenu.Length > 0 Then
                                            sbrJsContent.Append(String.Concat("Activities[", ds.Tables("ACTIVITY").Rows(iLoop).Item("ACTVTY_ID").ToString, "]=""text=", strKeyValue, ";"))
                                            Dim strMenuItems() As String
                                            strMenuItems = strMenu(1).ToString.Split(CChar("&"))
                                            If strMenuItems.Length > 0 Then
                                            For j As Integer = 0 To strMenuItems.Length - 1
                                                Dim strTitle() As String
                                                strTitle = strMenuItems(j).ToString.Split(CChar("="))
                                                If strTitle(0).ToString.Contains("listpanetitle") Or strTitle(0).ToString.Contains("pagetitle") Or strTitle(0).ToString.Contains("qrytype") Or strTitle(0).ToString().Contains("activityname") Then
                                                        If strTitle(0).ToString.Contains("listpanetitle") Then
                                                            Dim strPage() As String = strTitle(1).ToString.Split(CChar(">>"))
                                                            sbrJsContent.Append(String.Concat(strTitle(0).ToString, "=", strPage(0).ToString, ">> ", strKeyValue, "&"))
                                                        ElseIf strTitle(0).ToString.Contains("pagetitle") Then
                                                            sbrJsContent.Append(String.Concat(strTitle(0).ToString, "=", strKeyValue, "&"))
                                                        ElseIf strTitle(0).ToString.Contains("qrytype") Then
                                                            sbrJsContent.Append(String.Concat(strMenuItems(j).ToString, ";"))
                                                        ElseIf strTitle(0).ToString().Contains("activityname") Then
                                                            sbrJsContent.Append(String.Concat("activityname", "=", strKeyValue, "&"))
                                                        End If
                                                    Else
                                                        sbrJsContent.Append(String.Concat(strMenuItems(j).ToString, "&"))
                                                    End If
                                                Next
                                            End If
                                            sbrJsContent.Append(String.Concat("status", "=", strKeyValue, """;"))
                                            intcount = 1
                                        End If
                                    End If
                                    sbrJsContent.Append(vbCrLf)
                                End If
                            Next
                        End If
                    End If
                    If intcount = 0 Then
                        sbrJsContent.Append(String.Concat("Activities[", ds.Tables("ACTIVITY").Rows(iLoop).Item("ACTVTY_ID").ToString, "]=""", ds.Tables("ACTIVITY").Rows(iLoop).Item("MNU_TXT"), """;", vbCrLf))
                    End If
                Next
                                
            End If

            'Write JS file
            Dim FILENAME As String = context.Server.MapPath(String.Concat("Script/UI", "/", "Settings.js"))

            File.WriteAllText(FILENAME, sbrJsContent.ToString())
                                                
            'Get a StreamReader class that can be used to read the file
            Dim objStreamReader As StreamReader
            objStreamReader = File.OpenText(FILENAME)

            'Now, read the entire file into a string
            Dim contents As String = objStreamReader.ReadToEnd()

            objStreamReader.Close()
            context.Response.Write(contents)
            'Set the text of the file to a Web control
            context.Response.ContentType = "text/javascript"
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#Region "IsReusable()"
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
#End Region
    
End Class