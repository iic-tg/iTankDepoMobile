Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls


<ToolboxBitmap(GetType(ExportToExcel)), DefaultProperty("Text"), ToolboxData("<{0}:ExportToExcel runat=server></{0}:ExportToExcel>")> _
Public Class ExportToExcel
    Inherits Web.UI.WebControls.WebControl

    Private _AlternateRowBackColor As Color
    Private _ColumnHeader As DisplaySetting
    Private _ContentType As String
    Private _Data As DisplaySetting
    Private _DataBorderColor As Color
    Private _DataBorderSize As Integer
    Private _DataSuppressZero As Boolean
    Private _Delimeter As String
    Private _ds As DataSet
    Private _Footer As DisplaySetting
    Private _PopupURL As String
    Private _ShowBorder As Boolean
    Private _ShowPopupWindow As Boolean
    Private _text As String
    Private _Title As DisplaySetting

    ' Methods
    Public Sub New()
        Me._text = ""
        Me._Delimeter = ""
        Me._PopupURL = ""
        Me._ContentType = "UTF-8"
        Me._ShowPopupWindow = True
        Me._Title = New DisplaySetting
        Me._ColumnHeader = New DisplaySetting
        Me._Data = New DisplaySetting
        Me._Footer = New DisplaySetting
        Me._ShowBorder = True
        Me._DataSuppressZero = False
        Me._DataBorderSize = 1
    End Sub

    Public Function ExportToExcel(ByVal dt As DataTable) As StringWriter
        Dim sws As New StringWriter
        Try
            Dim builder1 As New StringBuilder
            Dim num1 As Integer = dt.Columns.Count

            sws.WriteLine("<HTML><HEAD>")
            If (StringType.StrCmp(Me._ContentType, "", False) <> 0) Then
                sws.WriteLine(("<meta http-equiv=""Content-Type"" content=""text/html""; charset=""" & Me._ContentType & """>"))
            End If
            'Me.WriteStyle()
            sws.WriteLine("</HEAD>")
            sws.WriteLine("<BODY>")
            If Not Me._ShowBorder Then
                Me._DataBorderSize = 0
            End If
            sws.WriteLine(("<table border='" & Me._DataBorderSize.ToString & "'"))
            If Not Me._DataBorderColor.IsEmpty Then
                Dim converter1 As New WebColorConverter
                sws.WriteLine((" bordercolor='" & converter1.ConvertToString(Me._DataBorderColor) & "'"))
            End If
            sws.WriteLine(">")
            If (StringType.StrCmp(Me._Title.Description, "", False) <> 0) Then
                sws.WriteLine(("<tr class=""title""><td colspan='" & num1.ToString & "' "))
                sws.WriteLine(">")
                sws.WriteLine(Me._Title.Description)
                sws.WriteLine("</td></tr>")
            End If
            If Me._ColumnHeader.Show Then
                sws.WriteLine("<tr valign=""middle"" class=""columnheader"">")
                Dim column1 As DataColumn
                For Each column1 In dt.Columns
                    sws.WriteLine(("<td style=""font-weight: bold; background-color:  #99CCFF; font-family: 'Times New Roman', Times, serif;border-style: solid"">" & column1.ToString & "</td>"))
                Next
                sws.WriteLine("</tr>")
            End If
            Dim flag1 As Boolean = False
            Dim row1 As DataRow
            For Each row1 In dt.Rows
                If flag1 Then
                    sws.WriteLine("<tr class=""alternaterowdata"">")
                Else
                    sws.WriteLine("<tr>")
                End If
                Dim objArray1 As Object() = row1.ItemArray
                Dim num2 As Integer = 0
                Do While (num2 < objArray1.Length)
                    Dim obj1 As Object = RuntimeHelpers.GetObjectValue(objArray1(num2))
                    sws.WriteLine("<td style=""font-family: 'Times New Roman', Times, serif""")
                    If (obj1 Is DBNull.Value) Then
                        obj1 = ""
                    End If
                    If ((StringType.StrCmp(obj1.ToString.Trim, "0", False) = 0) And Me.DataSuppressZero) Then
                        obj1 = ""
                    End If
                    Dim text2 As String = StringType.FromObject(obj1)
                    sws.WriteLine(">")
                    sws.WriteLine(text2)
                    sws.WriteLine("</td>")
                    num2 += 1
                Loop
                sws.WriteLine("</tr>")
                flag1 = Not flag1
            Next
            If (StringType.StrCmp(Me._Footer.Description, "", False) <> 0) Then
                sws.WriteLine("<tr class=""footer"">")
                sws.WriteLine(("<td colspan='" & num1.ToString & "'>"))
                sws.WriteLine(Me._Footer.Description)
                sws.WriteLine("</td></tr>")
            End If
            sws.WriteLine("</TABLE></BODY></HTML>")
        Catch exception2 As Exception
            ProjectData.SetProjectError(exception2)
            Dim exception1 As Exception = exception2
            Throw exception1
        End Try
        Return sws
    End Function

    Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
        output.Write(Me._text)
    End Sub

    'Properties

    <Category("Display"), Description("Do not display if data in a cell is numeric type and value is 0."), Bindable(True)> _
    Public Property DataSuppressZero() As Boolean
        Get
            Return Me._DataSuppressZero
        End Get
        Set(ByVal Value As Boolean)
            Me._DataSuppressZero = Value
        End Set
    End Property


    ' Nested Types
    Private Class DisplaySetting
        ' Methods
        Public Sub New()
            Me._Style = ""
            Me._Show = True
            Me._Alignment = EnumAlignment.Left
        End Sub

        Private Function CheckFontFormat(ByVal fontId As Font, ByVal propName As String) As Font
            Dim num1 As Integer = 0
            Dim font2 As Font = Nothing
            Return font2
        End Function


        ' Properties
        Public Property Alignment() As EnumAlignment
            Get
                Return Me._Alignment
            End Get
            Set(ByVal Value As EnumAlignment)
                Me._Alignment = Value
            End Set
        End Property

        Public Property BackColor() As Color
            Get
                Return Me._BackColor
            End Get
            Set(ByVal Value As Color)
                Me._BackColor = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return Me._Description
            End Get
            Set(ByVal Value As String)
                Me._Description = Value
            End Set
        End Property

        Public Property Font() As Font
            Get
                Return Me._Font
            End Get
            Set(ByVal Value As Font)
                Me._Font = Me.CheckFontFormat(Value, "Font")
            End Set
        End Property

        Public Property ForeColor() As Color
            Get
                Return Me._ForeColor
            End Get
            Set(ByVal Value As Color)
                Me._ForeColor = Value
            End Set
        End Property

        Public Property Show() As Boolean
            Get
                Return Me._Show
            End Get
            Set(ByVal Value As Boolean)
                Me._Show = Value
            End Set
        End Property

        Public Property Style() As String
            Get
                Return Me._Style
            End Get
            Set(ByVal Value As String)
                Me._Style = Value
            End Set
        End Property


        ' Fields
        Private _Alignment As EnumAlignment
        Private _BackColor As Color
        Private _Description As String
        Private _Font As Font
        Private _ForeColor As Color
        Private _Show As Boolean
        Private _Style As String
    End Class

    Public Enum EnumAlignment
        ' Fields
        Center = 1
        Left = 0
        Right = 2
    End Enum

End Class
