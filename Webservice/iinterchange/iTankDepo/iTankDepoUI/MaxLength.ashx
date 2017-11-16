<%@ WebHandler Language="VB" Class="MaxLength" %>

Imports System
Imports System.Web
Imports System.IO

Public Class MaxLength : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim sqlCon As New SqlClient.SqlConnection
            Dim strQuery As String
            Dim da As SqlClient.SqlDataAdapter
            Dim ds As New DataSet
            Dim sbrJsContent As New StringBuilder
            Dim sbrTableColumnList As New StringBuilder
            Dim sbrMaxlength As New StringBuilder
            
            sqlCon.ConnectionString = CommonWeb.pub_GetConfigValue("ConnectionString")
            sqlCon.Open()
            
            strQuery = "select (select name from sysobjects where id=syscolumns.id) as table_name, name as column_name, length as max_length,(select top(1) name from systypes where xtype=syscolumns.xtype) data_type , ISNULL(prec, '') as prec ,ISNULL(scale, '') as scale from syscolumns where id in (select id from sysobjects where type='u')"
            da = New SqlClient.SqlDataAdapter(strQuery, sqlCon)
            da.Fill(ds, "MaxLength")
            
            sqlCon.Close()
            
            sbrJsContent.Append("var TableColumnList = new Array();" & vbCrLf)
            sbrJsContent.Append("var MaxLength = new Array();" & vbCrLf)
            
            For iLoop As Integer = 0 To ds.Tables(0).Rows.Count - 1
                
                With ds.Tables(0).Rows(iLoop)
                    sbrTableColumnList.Append(String.Concat("TableColumnList[", iLoop, "]=""", .Item("table_name"), _
                                           "_", .Item("column_name"), """", vbCrLf))
                    
                    sbrMaxlength.Append(String.Concat("MaxLength[", iLoop, "]=""", "TableName:", .Item("table_name"), _
                                        ",ColumnName:", .Item("column_name"), _
                                        ",MaxLength:", .Item("max_length"), _
                                        ",DataType:", .Item("data_type"), _
                                        ",Prec:", .Item("prec"), _
                                        ",Scale:", .Item("scale"), """", vbCrLf))
                    
                End With
            
            Next

            sbrJsContent.Append("//Table Mappings" & vbCrLf)
            sbrJsContent.Append(sbrTableColumnList.ToString() & vbCrLf)
            sbrJsContent.Append("//Max Length Mappings" & vbCrLf)
            sbrJsContent.Append(sbrMaxlength.ToString() & vbCrLf)
            
            'Write JS file
            Dim FILENAME As String = context.Server.MapPath(String.Concat("Script", "/", "MaxLength.js"))

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
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class