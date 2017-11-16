Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Security.Cryptography
Imports iInterchange.WebControls.v4.Input
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Web.UI
Imports iInterchange.Framework.Common
Imports iInterchange.iTankDepo.Business.Common


''' <summary>
''' This class is common class for all pages which has repeatly used functions in entire application
''' </summary>
''' <remarks></remarks>
Public Class CommonWeb
#Region "pub_AttachHasChanges"
    ''' <summary>
    ''' This method is used to get changes of html controls
    ''' </summary>
    ''' <param name="br_objHTML">Denotes Html Object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_objHTML As HtmlGenericControl)
        br_objHTML.Attributes.Add("onchange", "setHasChanges();")
    End Sub

    ''' <summary>
    ''' This method is used to get changes of textbox
    ''' </summary>
    ''' <param name="br_objTextBox">Denotes Textbox Object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_objTextBox As iTextBox)
        If String.IsNullOrEmpty(br_objTextBox.OnClientTextChange) Then
            br_objTextBox.OnClientTextChange = "setHasChanges"
        Else
            br_objTextBox.OnClientTextChange = String.Concat("if(this.isvalid){", br_objTextBox.OnClientTextChange, "(this);}", "setHasChanges")
        End If
    End Sub

    ''' <summary>
    ''' This method is used to get changes of textbox for repair company page
    ''' </summary>
    ''' <param name="br_objTextBox">Denotes Textbox Object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachRCHasChanges(ByRef br_objTextBox As iTextBox)
        If String.IsNullOrEmpty(br_objTextBox.OnClientTextChange) Then
            br_objTextBox.OnClientTextChange = "setRCHasChanges"
        Else
            br_objTextBox.OnClientTextChange = String.Concat("if(this.isvalid){", br_objTextBox.OnClientTextChange, "(this);}", "setRCHasChanges")
        End If
    End Sub

    ''' <summary>
    ''' This method is used to get changes of radio buttons
    ''' </summary>
    ''' <param name="br_objListItem">Denotes List Item</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_objListItem As ListItem)
        br_objListItem.Attributes.Add("onchange", "setHasChanges();")
    End Sub

    ''' <summary>
    ''' This method is used to get changes of lookup
    ''' </summary>
    ''' <param name="br_objLookup"></param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_objLookup As iLookup)
        If String.IsNullOrEmpty(br_objLookup.OnClientTextChange) Then
            br_objLookup.OnClientTextChange = "setHasChanges;"
        Else
            br_objLookup.OnClientTextChange = String.Concat(br_objLookup.OnClientTextChange, "(this);", "setHasChanges;")
        End If
    End Sub

    ''' <summary>
    ''' This method is used to get changes of lookup  for repair company page
    ''' </summary>
    ''' <param name="br_Lookup">Denotes lookup object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachRCHasChanges(ByRef br_Lookup As iLookup)
        If String.IsNullOrEmpty(br_Lookup.OnClientTextChange) Then
            br_Lookup.OnClientTextChange = "setRCHasChanges"
        Else
            br_Lookup.OnClientTextChange = String.Concat(br_Lookup.OnClientTextChange, "(this);", "setRCHasChanges")
        End If
    End Sub

    ''' <summary>
    ''' This method is used to get changes of date field
    ''' </summary>
    ''' <param name="br_Date">Denotes date object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_Date As iDate)
        br_Date.Attributes.Add("onchange", "setHasChanges();")
    End Sub

    ''' <summary>
    ''' This method is used to get changes of date field for repair company page
    ''' </summary>
    ''' <param name="br_Date">Denotes date object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachRCHasChanges(ByRef br_Date As iDate)
        br_Date.Attributes.Add("onchange", "setHasChanges();")
    End Sub

    ''' <summary>
    ''' This method is used to get changes of checkbox 
    ''' </summary>
    ''' <param name="br_Checkbox">Denotes date object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_Checkbox As CheckBox)
        br_Checkbox.Attributes.Add("onclick", "setCBHasChanges();")
    End Sub

    ''' <summary>
    ''' This method is used to get changes of Radio Button 
    ''' </summary>
    ''' <param name="br_HtmlControl">Denotes Radio Button object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_HtmlControl As HtmlControl)
        br_HtmlControl.Attributes.Add("onclick", "setCBHasChanges();")
    End Sub

    '' <summary>
    ''' This method is used to get changes of checkbox for repair company
    ''' </summary>
    ''' <param name="br_Checkbox">Denotes check box object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachRCHasChanges(ByRef br_Checkbox As CheckBox)
        br_Checkbox.Attributes.Add("onclick", "setRCCBHasChanges();")
    End Sub

    '' <summary>
    ''' This method is used to get changes of checkbox for repair company
    ''' </summary>
    ''' <param name="br_Inputfile">Denotes input file Used</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachHasChanges(ByRef br_Inputfile As HtmlInputFile)
        br_Inputfile.Attributes.Add("onclick", "setCBHasChanges();")
    End Sub

    ''' <summary>
    ''' This method is used to get changes muli line textbox
    ''' </summary>
    ''' <param name="br_objTextBox">Denotes Textbox Object</param>
    ''' <param name="bv_blnHideIcon">Denotes hide icon option whether to hide the more info icon</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachDescMaxlength(ByRef br_objTextBox As iTextBox, Optional ByVal bv_blnHideIcon As Boolean = False)
        br_objTextBox.Attributes.Add("onkeypress", String.Concat("return imposeMaxLength(this,", br_objTextBox.MaxLength - 1, ");"))
        br_objTextBox.Attributes.Add("onpaste", String.Concat("OnPasteCheckMaxLength(this,", br_objTextBox.MaxLength, ");return false;"))
        br_objTextBox.Attributes.Add("maxLength", br_objTextBox.MaxLength)
        br_objTextBox.Attributes.Add("onchange", "setHasChanges();")
        br_objTextBox.Validator.CustomValidation = True
        br_objTextBox.Validator.CustomValidationFunction = "validateDesc"
        br_objTextBox.Validator.CsvErrorMessage = String.Concat("Only ", br_objTextBox.MaxLength + 1, " Characters allowed")
        If bv_blnHideIcon = False Then
            pvt_AttachImage(br_objTextBox)
        End If
    End Sub

    ''' <summary>
    ''' This method is used to get changes muli line textbox
    ''' </summary>
    ''' <param name="br_txtDiv">Denotes Text box oject</param>
    ''' <param name="intMaxLength">Denotes max length if textbox</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_AttachDescMaxlength(ByRef br_txtDiv As HtmlGenericControl, ByVal intMaxLength As Integer)
        br_txtDiv.Attributes.Add("maxlength", intMaxLength)
        br_txtDiv.Attributes.Add("onkeypress", String.Concat("return imposeMaxLengthDiv(this,", intMaxLength - 1, ");"))
        br_txtDiv.Attributes.Add("onpaste", String.Concat("OnPasteDivCheckMaxLength(this,", intMaxLength, ");"))
    End Sub

    ''' <summary>
    ''' This method is used to add more info icon in multiline textbox
    ''' </summary>
    ''' <param name="br_objTextBox">Denotes Text box oject</param>
    ''' <remarks></remarks>
    Private Shared Sub pvt_AttachImage(ByRef br_objTextBox As iTextBox)
        Dim lkpImg As New Image
        Dim strRUrl As String = HttpContext.Current.Request.ApplicationPath & "/MoreInfo.aspx"
        lkpImg.ImageUrl = "../Images/info.gif"
        lkpImg.Attributes.Add("style", "cursor:hand")
        Dim sbrBuildMethod As New StringBuilder
        sbrBuildMethod.Append("OpenMultiLinePopup('")
        sbrBuildMethod.Append(strRUrl)
        sbrBuildMethod.Append("','")
        sbrBuildMethod.Append(br_objTextBox.ClientID)
        sbrBuildMethod.Append("','")
        sbrBuildMethod.Append(br_objTextBox.MaxLength)
        sbrBuildMethod.Append("');")
        lkpImg.Attributes.Add("onclick", sbrBuildMethod.ToString())

        br_objTextBox.Controls.Add(lkpImg)
    End Sub
#End Region

#Region "GetNextIndex"
    ''' <summary>
    ''' This method is used to get next index from data table
    ''' </summary>
    ''' <param name="br_table">Denotes Datatable box oject</param>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Shared Function GetNextIndex(ByRef br_table As System.Data.DataTable, ByVal bv_colname As String) As Long
        Dim maxcount As Long
        Dim objvalue As Object

        objvalue = br_table.Compute(String.Concat("max(", bv_colname, ")"), "")
        If objvalue Is DBNull.Value Then
            maxcount = 0
        Else
            maxcount = CType(objvalue, Integer)
        End If
        maxcount = maxcount + 1

        objvalue = Nothing
        Return maxcount
    End Function
#End Region

#Region "pub_IsRowAlreadyExists"
    Public Shared Function pub_IsRowAlreadyExists(ByVal bv_dtTable As Data.DataTable, _
                                                  ByVal bv_drValues As System.Collections.Specialized.OrderedDictionary, _
                                                  ByVal bv_strArrColumns As String(), _
                                                  ByVal bv_strMode As String, _
                                                  ByVal bv_strPKColumnName As String, _
                                                  ByVal bv_intPKColumnValue As Integer) As Boolean
        Dim strfltr As String
        Dim drs As Data.DataRow()
        Dim dtfltr As New DataTable
        If bv_strMode = "edit" Then
            dtfltr = bv_dtTable.GetChanges()
        Else
            dtfltr = bv_dtTable
        End If
        If dtfltr Is Nothing Then
            Return False
        End If
        strfltr = pvt_BuildFilter(bv_drValues, bv_strArrColumns, bv_strMode, bv_strPKColumnName, bv_intPKColumnValue)
        drs = dtfltr.Select(strfltr, "", DataViewRowState.CurrentRows)
        If drs.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "pvt_BuildFilter"
    Private Shared Function pvt_BuildFilter(ByVal bv_drValues As System.Collections.Specialized.OrderedDictionary, _
                                            ByVal cols As String(), _
                                            ByVal bv_strMode As String, _
                                            ByVal bv_strPKColumnName As String, _
                                            ByVal bv_intPKColumnValue As Integer) As String
        Dim strBldr As New System.Text.StringBuilder
        Dim strRes As String = String.Empty

        If cols.Length > 0 Then
            For Each strColumn As String In cols
                strBldr.Append(strColumn)
                If bv_drValues.Item(strColumn) Is Nothing Or CStr(bv_drValues.Item(strColumn)) = "" Then
                    strBldr.Append(" is null ")
                    strBldr.Append(" and ")
                Else
                    strBldr.Append("='")
                    strBldr.Append(bv_drValues.Item(strColumn))
                    strBldr.Append("' and ")
                End If
            Next
            If bv_intPKColumnValue <> 0 Then
                strBldr.Append(String.Concat(bv_strPKColumnName, "<> '", bv_intPKColumnValue, "'"))
                strRes = strBldr.ToString()
            Else
                strRes = strBldr.ToString.Substring(0, strBldr.ToString.LastIndexOf(" and "))
            End If
        End If
        Return strRes
    End Function
#End Region

#Region "pub_Formatdate"
    ''' <summary>
    ''' This method used to format the date in "dd-MMM-yyyy" format 
    ''' </summary>
    ''' <param name="bv_datValue">Denotes Date Parameter</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function pub_Formatdate(ByVal bv_datValue As Date) As String
        pub_Formatdate = bv_datValue.ToString("dd-MMM-yyyy").ToUpper
    End Function
#End Region

#Region "pub_CheckAccess"
    ''' <summary>
    ''' This method is used for code protection
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub pub_CheckAccess()
        If Not HttpContext.Current.Request.ServerVariables("SERVER_NAME").ToLower = "iic80" Then
            HttpContext.Current.Response.Status = "403 Access Forbidden"
            HttpContext.Current.Session.Abandon()
            HttpContext.Current.Response.End()
        End If
    End Sub
#End Region

#Region "pub_SessionExpired"
    ''' <summary>
    ''' This method used to navigate the URL when the session is expired
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub pub_SessionExpired()
        If HttpContext.Current.Session("username") Is Nothing Or HttpContext.Current.Session("username") Is "" Then
            HttpContext.Current.Response.Redirect("~\Alerts.aspx?se=1")
        End If
    End Sub
#End Region

#Region "ColToCSVstring"
    ''' <summary>
    ''' This method used to get the column values as CSV string
    ''' </summary>
    ''' <param name="br_dtData">Denotes the data to be converted</param>
    ''' <param name="bv_strColumnName">Denotes column name to be converted</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function ColToCSVstring(ByRef br_dtData As DataTable, ByVal bv_strColumnName As String) As String
        Dim strArray As Array
        Dim strResult As String = ""
        Dim alCol As New ArrayList

        If Not br_dtData Is Nothing Then
            For Each dr As DataRow In br_dtData.Rows
                'alCol.Add(CStr(dr.Item(bv_strColumnName)))
                alCol.Add(dr.Item(bv_strColumnName))
            Next
        End If
        'strArray = alCol.ToArray(GetType(System.String))
        'strResult = String.Concat("'", String.Join("','", strArray), "'")
        strResult = String.Join(",", alCol.ToArray())
        If strResult = "" Then
            strResult = "null"
        End If
        Return strResult
    End Function
#End Region

#Region "ColToArray"
    ''' <summary>
    ''' This method used to get the column values as array
    ''' </summary>
    ''' <param name="br_dtData">Denotes the data to be converted</param>
    ''' <param name="bv_strColumnName">Denotes column name to be converted</param>
    ''' <returns>Array</returns>
    ''' <remarks></remarks>
    Public Shared Function ColToArray(ByRef br_dtData As DataTable, ByVal bv_strColumnName As String) As Array
        Dim strArray As Array
        Dim alCol As New ArrayList

        If Not br_dtData Is Nothing Then
            For Each dr As DataRow In br_dtData.Rows
                alCol.Add(CStr(dr.Item(bv_strColumnName)))
            Next
        End If

        If alCol.Count = 0 Then
            alCol.Add("0")
        End If
        strArray = alCol.ToArray(GetType(System.String))

        Return strArray
    End Function
#End Region

#Region "SetTextBoxMaxLengthJSO"
    ''' <summary>
    ''' This method used to set the max length of text box
    ''' </summary>
    ''' <param name="br_objTextBox">Denotes Textbox object</param>
    ''' <param name="bv_MaxLength">Denotes Max length of textbox</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function SetTextBoxMaxLengthJSO(ByRef br_objTextBox As iTextBox, ByVal bv_MaxLength As Int32) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objTextBox.ID)
        sbrJsStatement.Append("').maxLength=")
        sbrJsStatement.Append(bv_MaxLength.ToString())
        sbrJsStatement.Append(";")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetTextValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for textbox to assign value in client side
    ''' </summary>
    ''' <param name="br_objTextBox">Denotes Textbox object</param>
    ''' <param name="bv_objControlValue">Denotes value of textbox</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetTextValuesJSO(ByRef br_objTextBox As iTextBox, ByVal bv_objControlValue As Object) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objTextBox.ID)
        sbrJsStatement.Append("').value=unescape('")
        If Not IsDBNull(bv_objControlValue) Then
            sbrJsStatement.Append(EncodeString(bv_objControlValue.ToString()))
        Else
            sbrJsStatement.Append("")
        End If
        sbrJsStatement.Append("');")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetLabelValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for label to assign value in client side
    ''' </summary>
    ''' <param name="br_objLabel">Denotes Label object</param>
    ''' <param name="bv_objControlValue">Denotes value of Label</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLabelValuesJSO(ByRef br_objLabel As iLabel, ByVal bv_objControlValue As Object) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objLabel.ID)
        sbrJsStatement.Append("').innerText=unescape(""")
        If Not IsDBNull(bv_objControlValue) Then
            sbrJsStatement.Append(bv_objControlValue.ToString())
        End If
        sbrJsStatement.Append(""");")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetLabelValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for html image to assign value in client side
    ''' </summary>
    ''' <param name="br_objHtmlImage">Denotes Html image object</param>
    ''' <param name="bv_objControlValue">Denotes source path of html image control</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetImageValuesJSO(ByRef br_objHtmlImage As HtmlImage, ByVal bv_objControlValue As Object) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objHtmlImage.ID)
        sbrJsStatement.Append("').src=unescape('")
        If Not IsDBNull(bv_objControlValue) Then
            sbrJsStatement.Append(bv_objControlValue.ToString())
        End If
        sbrJsStatement.Append("');")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetUploadLinkValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for anchor to assign value in client side
    ''' </summary>
    ''' <param name="br_objHtmlAnchor">Denotes html anchor object</param>
    ''' <param name="bv_objControlValue">Denotes navigate URL of html anchor</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetUploadLinkValuesJSO(ByRef br_objHtmlAnchor As HtmlAnchor, ByVal bv_objControlValue As Object) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objHtmlAnchor.ID)
        sbrJsStatement.Append("').href=unescape('")
        If Not IsDBNull(bv_objControlValue) Then
            sbrJsStatement.Append(bv_objControlValue.ToString())
        End If
        sbrJsStatement.Append("');")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetUploadPathValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for input file to assign value in client side
    ''' </summary>
    ''' <param name="br_objHtmlInputFile">Denotes html input file object</param>
    ''' <param name="bv_objControlValue">Denotes value of input file</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetUploadPathValuesJSO(ByRef br_objHtmlInputFile As HtmlInputFile, ByVal bv_objControlValue As Object) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objHtmlInputFile.ID)
        sbrJsStatement.Append("').value='")
        If Not IsDBNull(bv_objControlValue) Then
            sbrJsStatement.Append(bv_objControlValue.ToString())
        End If
        sbrJsStatement.Append("';")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetLookupValuesJSO2"
    ''' <summary>
    ''' This method used to get the javascript statement for lookup to assign value in client side use of array
    ''' </summary>
    ''' <param name="bv_objLookup">Denotes Lookup object</param>
    ''' <param name="bv_strControlValue">Denotes array value of lookup. Ex: "jpg", "png", "xls", "xlsx", "doc", "pdf", "docx"</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLookupValuesJSO2(ByVal bv_objLookup As iLookup, ByVal bv_strControlValue As String) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("SetLkpContrlValue(""")
        sbrJsStatement.Append(bv_objLookup.ID)
        sbrJsStatement.Append(""",")
        sbrJsStatement.Append("new Array(")
        sbrJsStatement.Append(bv_strControlValue.Replace("'", """"))
        sbrJsStatement.Append("));")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetLookupValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for lookup to assign value in client side use of array and initialize lookup object with ID column value
    ''' </summary>
    ''' <param name="br_objLookup">Denotes Lookup object</param>
    ''' <param name="bv_objControlIDValue">Denotes ID column value. Ex: 1</param>
    ''' <param name="bv_objControlValue">Denotes code column of lookup. Ex: "jpg"</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLookupValuesJSO(ByRef br_objLookup As iLookup, ByVal bv_objControlIDValue As Object, ByVal bv_objControlValue As Object) As String
        Dim objStrValuePassed As New StringBuilder
        objStrValuePassed.Append("SetLookupValue('")
        objStrValuePassed.Append(br_objLookup.ID)
        objStrValuePassed.Append("','")
        If Not IsDBNull(bv_objControlValue) AndAlso Not IsDBNull(bv_objControlIDValue) Then
            objStrValuePassed.Append(bv_objControlValue.ToString())
            objStrValuePassed.Append("',new Array(")
            objStrValuePassed.Append(String.Concat("""", bv_objControlIDValue.ToString(), """, """, bv_objControlValue.ToString(), """"))
            objStrValuePassed.Append("));")
        Else
            objStrValuePassed.Append("');")
        End If
        Return objStrValuePassed.ToString
    End Function
#End Region

#Region "GetLookupValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for lookup to assign value in client side use of array 
    ''' and initialize lookup object with ID column value
    ''' </summary>
    ''' <param name="br_objLookup">Denotes Lookup object</param>
    ''' <param name="bv_objControlIDValue">Denotes ID column value. Ex: 1</param>
    ''' <param name="bv_objControlValue1">Denotes code column of lookup. Ex: "jpg"</param>
    ''' <param name="bv_objControlValue2">Denotes decription column of lookup. Ex: "Jpeg Image"</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLookupValuesJSO(ByRef br_objLookup As iLookup, ByVal bv_objControlIDValue As Object, ByVal bv_objControlValue1 As Object, ByVal bv_objControlValue2 As Object) As String
        Dim objStrValuePassed As New StringBuilder
        objStrValuePassed.Append("SetLookupValue('")
        objStrValuePassed.Append(br_objLookup.ID)
        objStrValuePassed.Append("','")
        If Not IsDBNull(bv_objControlValue1) AndAlso Not IsDBNull(bv_objControlValue2) AndAlso Not IsDBNull(bv_objControlIDValue) Then
            objStrValuePassed.Append(bv_objControlValue1.ToString())
            objStrValuePassed.Append("',new Array(")
            objStrValuePassed.Append(String.Concat("""", bv_objControlIDValue.ToString(), """, """, bv_objControlValue1.ToString(), """, """, bv_objControlValue2.ToString(), """"))
            objStrValuePassed.Append("));")
        Else
            objStrValuePassed.Append("');")
        End If
        Return objStrValuePassed.ToString
    End Function
#End Region

#Region "GetLookupValuesCodeJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for lookup to assign value in client side use of array 
    ''' and initialize lookup object with the order Description and Code
    ''' </summary>
    ''' <param name="br_objLookup">Denotes Lookup object</param>
    ''' <param name="bv_objControlCodeValue">Denotes Lookup Code Value</param>
    ''' <param name="bv_objControlDescriptionValue">Denotes Lokup Description Value</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLookupValuesCodeJSO(ByRef br_objLookup As iLookup, ByVal bv_objControlCodeValue As Object, ByVal bv_objControlDescriptionValue As Object) As String
        Dim objStrValuePassed As New StringBuilder
        objStrValuePassed.Append("SetLookupValue('")
        objStrValuePassed.Append(br_objLookup.ID)
        objStrValuePassed.Append("','")
        If Not IsDBNull(bv_objControlCodeValue) Then
            objStrValuePassed.Append(bv_objControlCodeValue.ToString())
            objStrValuePassed.Append("',new Array(")
            objStrValuePassed.Append(String.Concat("""", bv_objControlCodeValue.ToString(), """, """, bv_objControlDescriptionValue.ToString(), """"))
            objStrValuePassed.Append("));")
        Else
            objStrValuePassed.Append("');")
        End If
        Return objStrValuePassed.ToString
    End Function

#End Region

#Region "GetLookupValuesArrayJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for lookup to assign value in client side use of array 
    ''' and initialize lookup object with ID and code column value
    ''' </summary>
    ''' <param name="br_objLookup">Denotes Lookup object</param>
    ''' <param name="bv_objControlIDValue">Denotes ID column value Ex: 1</param>
    ''' <param name="bv_objControlCodeValue">Denotes Code column value Ex: jpg</param>
    ''' <param name="bv_objControlOtherValues">Denotes Other column values  Ex: "jpeg","image"</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLookupValuesArrayJSO(ByRef br_objLookup As iLookup, ByVal bv_objControlIDValue As Object, _
                                                   ByVal bv_objControlCodeValue As Object, ByVal bv_objControlOtherValues() As Object) As String
        Dim objStrValuePassed As New StringBuilder
        objStrValuePassed.Append("SetLookupValue('")
        objStrValuePassed.Append(br_objLookup.ID)
        objStrValuePassed.Append("','")
        If Not IsDBNull(bv_objControlCodeValue) Then
            objStrValuePassed.Append(bv_objControlCodeValue.ToString())
            objStrValuePassed.Append("',new Array(")
            objStrValuePassed.Append(String.Concat("""", bv_objControlIDValue.ToString(), """,""", bv_objControlCodeValue.ToString()))
            For Each strOtherValues As String In bv_objControlOtherValues
                objStrValuePassed.Append(String.Concat(""",""", strOtherValues, "", ""))
            Next
            objStrValuePassed.Append("""));")
        Else
            objStrValuePassed.Append("');")
        End If
        Return objStrValuePassed.ToString
    End Function
#End Region

#Region "GetLookupValuesArrayJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for lookup to assign value in client side use of array 
    ''' and initialize lookup object with ID, Code, Name column values
    ''' </summary>
    ''' <param name="br_objLookup">Denotes Lookup object</param>
    ''' <param name="bv_objControlIDValue">Denotes ID column value Ex: 1</param>
    ''' <param name="bv_objControlCodeValue">Denotes Code column value Ex: jpg</param>
    ''' <param name="bv_objControlDescriptionValue">Denotes Description column value  Ex: jpeg image</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLookupValuesNameJSO(ByRef br_objLookup As iLookup, ByVal bv_objControlIDValue As Object, _
                                                  ByVal bv_objControlCodeValue As Object, ByVal bv_objControlDescriptionValue As Object) As String
        Dim objStrValuePassed As New StringBuilder
        objStrValuePassed.Append("SetLookupValue('")
        objStrValuePassed.Append(br_objLookup.ID)
        objStrValuePassed.Append("','")
        If Not IsDBNull(bv_objControlCodeValue) Then
            objStrValuePassed.Append(bv_objControlCodeValue.ToString())
            objStrValuePassed.Append("',new Array(")
            objStrValuePassed.Append(String.Concat("""", bv_objControlIDValue.ToString(), _
                                                """,""", bv_objControlCodeValue.ToString(), """,""", _
                                                bv_objControlDescriptionValue, """"))
            objStrValuePassed.Append("));")
        Else
            objStrValuePassed.Append("');")
        End If
        Return objStrValuePassed.ToString
    End Function
#End Region

#Region "GetTextDateValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for date control
    ''' </summary>
    ''' <param name="br_objDate">Denotes Date object</param>
    ''' <param name="bv_strControlValue">Denotes value of date control</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetTextDateValuesJSO(ByRef br_objDate As iDate, ByVal bv_strControlValue As String) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objDate.ID)
        sbrJsStatement.Append("').value='")
        sbrJsStatement.Append(bv_strControlValue)
        sbrJsStatement.Append("';")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetCheckboxValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for checkbox control
    ''' </summary>
    ''' <param name="br_objCheckBox">Denotes Checkbox object</param>
    ''' <param name="bv_strControlValue">Denotes value of checkbox</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetCheckboxValuesJSO(ByRef br_objCheckBox As CheckBox, ByVal bv_strControlValue As Boolean) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objCheckBox.ID)
        sbrJsStatement.Append("').checked=")
        sbrJsStatement.Append(LCase(bv_strControlValue))
        sbrJsStatement.Append(";")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetHiddenTextValuesJSO"
    ''' <summary>
    '''  This method used to get the javascript statement for hidden control
    ''' </summary>
    ''' <param name="br_objHiddenField">Denotes Hidden field object</param>
    ''' <param name="bv_strControlValue">Denotes value of hidden field</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetHiddenTextValuesJSO(ByRef br_objHiddenField As WebControls.HiddenField, ByVal bv_strControlValue As String) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objHiddenField.ID)
        sbrJsStatement.Append("').value='")
        sbrJsStatement.Append(bv_strControlValue)
        sbrJsStatement.Append("';")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetHiddenTextValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for hidden control
    ''' </summary>
    ''' <param name="br_objHiddenField">Denotes Hidden field object</param>
    ''' <param name="bv_strControlValue">Denotes value of hidden field</param>
    ''' <param name="bv_blnEncodeBit">Denotes whether the value is encoded of not</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetHiddenTextValuesJSO(ByRef br_objHiddenField As WebControls.HiddenField, ByVal bv_strControlValue As String, ByVal bv_blnEncodeBit As Boolean) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objHiddenField.ID)
        sbrJsStatement.Append("').value=unescape('")
        If bv_strControlValue <> "" Then
            sbrJsStatement.Append(EncodeString(bv_strControlValue))
        Else
            sbrJsStatement.Append("")
        End If
        sbrJsStatement.Append("');")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetLabelValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for label
    ''' </summary>
    ''' <param name="br_objLabel">Denotes label object</param>
    ''' <param name="bv_strControlValue">Denotes value of label</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLabelValuesJSO(ByRef br_objLabel As iLabel, ByVal bv_strControlValue As String) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("setText(el('")
        sbrJsStatement.Append(br_objLabel.ID)
        sbrJsStatement.Append("'),'")
        sbrJsStatement.Append(bv_strControlValue)
        sbrJsStatement.Append("');")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "GetTextEncodeValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for textbox with encoded
    ''' </summary>
    ''' <param name="br_objTextBox">Denotes textbox object</param>
    ''' <param name="bv_objControlValue">Denotes value of textbox</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetTextEncodeValuesJSO(ByRef br_objTextBox As iTextBox, ByVal bv_objControlValue As Object) As String
        Dim objStrValuePassed As New StringBuilder
        objStrValuePassed.Append("el('")
        objStrValuePassed.Append(br_objTextBox.ID)
        objStrValuePassed.Append("').value=unescape('")
        If Not IsDBNull(bv_objControlValue) Then
            objStrValuePassed.Append(EncodeString(bv_objControlValue.ToString()))
        Else
            objStrValuePassed.Append("")
        End If
        objStrValuePassed.Append("');")
        Return objStrValuePassed.ToString
    End Function
#End Region

#Region "GetHtmlControlValuesJSO"
    ''' <summary>
    ''' This method used to get the javascript statement for checkbox control
    ''' </summary>
    ''' <param name="br_objHtmlControl">Denotes Checkbox object</param>
    ''' <param name="bv_strControlValue">Denotes value of checkbox</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetHtmlControlValuesJSO(ByRef br_objHtmlControl As HtmlControl, ByVal bv_strControlValue As Boolean) As String
        Dim sbrJsStatement As New StringBuilder
        sbrJsStatement.Append("el('")
        sbrJsStatement.Append(br_objHtmlControl.ID)
        sbrJsStatement.Append("').checked=")
        sbrJsStatement.Append(LCase(bv_strControlValue))
        sbrJsStatement.Append(";")
        Return sbrJsStatement.ToString
    End Function
#End Region

#Region "EncodeString"
    ''' <summary>
    ''' This method used to encode the string
    ''' </summary>
    ''' <param name="bv_strEncodeText">Denotes value of encode text</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function EncodeString(ByVal bv_strEncodeText As String) As String
        bv_strEncodeText = bv_strEncodeText.Replace("\", "\\")
        bv_strEncodeText = bv_strEncodeText.Replace("'", "\'")
        bv_strEncodeText = bv_strEncodeText.Replace(vbCrLf, "\n")
        Return System.Web.HttpUtility.UrlEncode(bv_strEncodeText).Replace("+", "%20").Replace("&", "%26").Replace("\\", "%3F").Replace("/", "%2B").Replace("%0a", "\n")
    End Function
#End Region

#Region "EncodeStringHtml"
    ''' <summary>
    ''' This method used to encode the html string
    ''' </summary>
    ''' <param name="bv_strEncodeText">Denotes value of encode text</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function EncodeStringHtml(ByVal bv_strEncodeText As String) As String
        Return System.Web.HttpUtility.UrlEncode(bv_strEncodeText).Replace("+", "%20").Replace("&", "%26").Replace("\\", "%3F").Replace("/", "%2B")
    End Function
#End Region

#Region "Data Type Conversions"
    ''' <summary>
    ''' This method used to convert value from string to integer
    ''' </summary>
    ''' <param name="strValue"></param>
    ''' <returns>Int32</returns>
    ''' <remarks></remarks>
    Public Shared Function CI32(ByVal strValue As String) As Int32
        Dim i32 As Int32 = 0
        If strValue <> "" Then
            i32 = CInt(strValue)
        End If
        Return i32
    End Function

    ''' <summary>
    ''' This method used to convert value from string to big integer
    ''' </summary>
    ''' <param name="strValue"></param>
    ''' <returns>Int64</returns>
    ''' <remarks></remarks>
    Public Shared Function CI64(ByVal strValue As String) As Int64
        Dim i64 As Int64 = 0
        If strValue <> "" Then
            i64 = CLng(strValue)
        End If
        Return i64
    End Function

    ''' <summary>
    ''' This method used to convert value from string to boolean
    ''' </summary>
    ''' <param name="strValue"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function CBln(ByVal strValue As String) As Boolean
        Dim iBln As Boolean = False
        If strValue <> "" AndAlso strValue = "" Then
            iBln = CBln(strValue)
        End If
        Return iBln
    End Function

    ''' <summary>
    ''' This method used to convert value from string to date
    ''' </summary>
    ''' <param name="strValue">Denotes value of the string</param>
    ''' <returns>Date</returns>
    ''' <remarks></remarks>
    Public Shared Function CDat(ByVal strValue As String) As Date
        Dim iDat As Date = Nothing
        If strValue <> "" AndAlso strValue = "" Then
            iDat = CDate(strValue)
        End If
        Return iDat
    End Function
#End Region

#Region "pub_GetConfigValue"
    ''' <summary>
    ''' This method used to get the config value 
    ''' </summary>
    ''' <param name="bv_strConfigName">Denotes config key name</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function pub_GetConfigValue(ByVal bv_strConfigName As String) As String
        Return Config.pub_GetAppConfigValue(bv_strConfigName)
    End Function
#End Region

#Region "pub_IsNullString"
    ''' <summary>
    ''' This method is used to get the empty string value if it is null 
    ''' </summary>
    ''' <param name="bv_objDBValue">Denotes string value</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function pub_IsNullString(ByVal bv_objDBValue As Object) As String
        If IsDBNull(bv_objDBValue) = True Then
            Return Nothing
        ElseIf String.IsNullOrEmpty(CStr(bv_objDBValue)) = True Then
            Return Nothing
        Else
            Return CStr(bv_objDBValue)
        End If
    End Function
#End Region

#Region "toJSON"
    ''' <summary>
    ''' This method is used to get the json object by passing data table
    ''' </summary>
    ''' <param name="bv_objDataTable">Denotes data table object</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function toJSON(ByVal bv_objDataTable As DataTable) As String
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In bv_objDataTable.Rows
                row = New Dictionary(Of String, Object)
                For Each col As DataColumn In bv_objDataTable.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            Return serializer.Serialize(rows)
        Catch ex As Exception
            iInterchange.Framework.Common.iErrorHandler.pub_WriteErrorLog("CommonWeb", Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "toQueryString"
    ''' <summary>
    ''' This method is used to get the json object by passing data table
    ''' </summary>
    ''' <param name="bv_objDataTable">Denotes data table object</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function toQueryString(ByVal bv_objDataTable As DataTable) As String
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))
            Dim sbrQs As New StringBuilder()
            For Each dc As DataColumn In bv_objDataTable.Columns
                If sbrQs.ToString() <> "" Then
                    sbrQs.Append("&")
                End If
                sbrQs.Append(String.Concat(dc.ColumnName, "="))
                Dim i As Integer = 1
                For Each dr As DataRow In bv_objDataTable.Rows
                    If Not IsDBNull(dr(dc.ColumnName)) Then
                        sbrQs.Append(Trim(dr(dc.ColumnName)))
                    Else
                        sbrQs.Append("null")
                    End If
                    If i < bv_objDataTable.Rows.Count Then
                        sbrQs.Append("|")
                    End If
                    i = i + 1
                Next
            Next
            Return sbrQs.ToString()
        Catch ex As Exception
            iInterchange.Framework.Common.iErrorHandler.pub_WriteErrorLog("CommonWeb", Reflection. _
                                            MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "Set Control Style on View Mode or Readonly Mode"
    ''' <summary>
    ''' This method used to change the style of textbox in view mode or readonly mode
    ''' </summary>
    ''' <param name="br_iTextBox">Denotes textbox object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_SetViewModeTextBoxStyle(ByRef br_iTextBox As iTextBox)
        br_iTextBox.ReadOnly = True
        br_iTextBox.CssClass = "txtd"
    End Sub

    ''' <summary>
    ''' This method used to change the style of lookup in view mode or readonly mode
    ''' </summary>
    ''' <param name="br_iLookup">Denotes lookup object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_SetViewModeLookupStyle(ByRef br_iLookup As iLookup)
        br_iLookup.ReadOnly = True
        br_iLookup.CssClass = "lkpd"
    End Sub

    ''' <summary>
    ''' This method used to change the style of date in view mode or readonly mode
    ''' </summary>
    ''' <param name="br_iDate">Denotes date object</param>
    ''' <remarks></remarks>
    Public Shared Sub pub_SetViewModeDateStyle(ByRef br_iDate As iDate)
        br_iDate.ReadOnly = True
        br_iDate.CssClass = "datd"
    End Sub
#End Region

#Region "RenderCommonScripts"
    ''' <summary>
    ''' This method used to render common scripts
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RenderCommonScripts() As String
        Dim sbrClientScript As New StringBuilder
        sbrClientScript.Append(vbCrLf)
        Dim versionNo As String
        Dim strScriptPath = String.Concat(iInterchange.WebControls.v4.Utilities.iUtil.AppPath(), "/Script/")
        sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""")
        sbrClientScript.Append(strScriptPath)
        versionNo = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath("Script/HelpTip.js")).ToString("yyMMddHHHmmss")
        sbrClientScript.Append("HelpTip.js?" & versionNo & """></script>")
        sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""")
        sbrClientScript.Append(strScriptPath)
        versionNo = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath("Callback.js")).ToString("yyMMddHHHmmss")
        sbrClientScript.Append("Callback.js?" & versionNo & """></script>")
        sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""")
        sbrClientScript.Append(strScriptPath)
        versionNo = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath("MaxLength.js")).ToString("yyMMddHHHmmss")
        sbrClientScript.Append("MaxLength.js?" & versionNo & """></script>")
        sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""")
        sbrClientScript.Append(strScriptPath)
        versionNo = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath("Common.js")).ToString("yyMMddHHHmmss")
        sbrClientScript.Append("Common.js?" & versionNo & """></script>")
        sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""")
        sbrClientScript.Append(strScriptPath)
        versionNo = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath("ui/jquery.js")).ToString("yyMMddHHHmmss")
        sbrClientScript.Append("ui/jquery.js?" & versionNo & """></script>")
        sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""")
        sbrClientScript.Append(strScriptPath)
        versionNo = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath("ui/jquery.corner.js")).ToString("yyMMddHHHmmss")
        sbrClientScript.Append("ui/jquery.corner.js?" & versionNo & """></script>")
        sbrClientScript.Append("<script type=""text/javascript"" language=""javascript"" src=""")
        sbrClientScript.Append(strScriptPath)
        versionNo = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath("ui/jquery.ui.js")).ToString("yyMMddHHHmmss")
        sbrClientScript.Append("ui/jquery.ui.js?" & versionNo & """></script>")
        Return sbrClientScript.ToString
    End Function
#End Region

#Region "IncludeScript"
    Public Shared Sub IncludeScript(ByVal scriptSrc As String, ByVal m As Page)
        Try
            Dim link As New Web.UI.WebControls.Literal()
            Dim versionNo As String = IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath(scriptSrc)).ToString("yyMMddHHHmmss")
            link.Text = String.Concat("<script language=""javascript"" type=""text/javascript"" src=""", iInterchange.WebControls.v4.Utilities.iUtil.AppPath(), "/", scriptSrc, "?", versionNo, """> </script>")
            m.Page.Header.Controls.Add(link)
        Catch ex As Exception
            iInterchange.Framework.Common.iErrorHandler.pub_WriteErrorLog("CommonWeb", Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "iDbl"
    Public Shared Function iDbl(ByVal bv_objObject As Object) As Double
        If IsDBNull(bv_objObject) Then
            Return Nothing
        Else
            Return CDbl(bv_objObject)
        End If
    End Function
#End Region

#Region "iDec"
    Public Shared Function iDec(ByVal bv_objObject As Object) As Decimal
        If IsDBNull(bv_objObject) Then
            Return Nothing
        Else
            Return CDec(bv_objObject)
        End If
    End Function
#End Region

#Region "iDat"
    Public Shared Function iDat(ByVal bv_objObject As Object) As Date
        Dim sqldatenull As SqlDateTime
        If bv_objObject Is Nothing Or bv_objObject = "" Then
            Return sqldatenull
        Else
            Return CDate(bv_objObject)
        End If
    End Function
#End Region

#Region "iInt"
    Public Shared Function iInt(ByVal bv_objObject As Object) As Integer
        If IsDBNull(bv_objObject) Then
            Return Nothing
        Else
            Return CInt(bv_objObject)
        End If
    End Function
#End Region

#Region "iLng"
    Public Shared Function iLng(ByVal bv_objObject As Object) As Long
        If IsDBNull(bv_objObject) Then
            Return Nothing
        Else
            Return CLng(bv_objObject)
        End If
    End Function
#End Region

    Public Shared Function EncryptData(Message As String) As String
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim Results As Byte()
        Dim pvt_strKeyPhrase As String
        Try
            ' pvt_strKeyPhrase = IO.File.GetLastWriteTime(String.Concat(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings("KeyFileName"))).ToString("yyMMddHHHmmss")
            pvt_strKeyPhrase = "IIC"
            Dim UTF8 As New System.Text.UTF8Encoding()
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToEncrypt As Byte() = UTF8.GetBytes(Message)
            Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        Catch ex As Exception
            iInterchange.Framework.Common.iErrorHandler.pub_WriteErrorLog("Config.EncryptData", Message, ex.ToString())
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return Convert.ToBase64String(Results)
    End Function

    Public Shared Function DecryptString(Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim pvt_strKeyPhrase As String
        Try
            'pvt_strKeyPhrase = IO.File.GetLastWriteTime(String.Concat(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings("KeyFileName"))).ToString("yyMMddHHHmmss")
            pvt_strKeyPhrase = "IIC"
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)
            Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        Catch ex As Exception
            iInterchange.Framework.Common.iErrorHandler.pub_WriteErrorLog("Config.DecryptString", Message, ex.ToString())
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return UTF8.GetString(Results)
    End Function
    Public Shared Function IsLookupValidation(page As Page) As Boolean
        Dim dt As New DataTable
        If Not iInterchange.WebControls.v4.Utilities.iUtil.GetQueryString(page, "m") Is Nothing Then
            If iInterchange.WebControls.v4.Utilities.iUtil.GetQueryString(page, "m") = "pk" Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

#Region "CHECK : pub_ValidateContainerNo"
    Public Shared Function pub_ValidateContainerNo(ByVal bv_strContainerNo As String, ByVal bv_blnIncludeChkDigtValidation As Boolean, ByRef br_strErrorBuilder As String) As Boolean
        Try
            Dim strPrefixContnrNo As String
            Dim strContrdigit As String
            Dim intContrnoLtdt As Integer
            Dim intCountPrefixContainerNo As Integer
            Dim intCountContainerNo As Integer
            If bv_strContainerNo.Length = 11 Then

                bv_strContainerNo = bv_strContainerNo.ToUpper
                strPrefixContnrNo = Left(bv_strContainerNo, 4)

                strContrdigit = Right(bv_strContainerNo, 7)
                intContrnoLtdt = CInt(Right(bv_strContainerNo, 1))

                For intCountPrefixContainerNo = 0 To strPrefixContnrNo.Trim.Length - 1
                    Dim strCharcode As Integer = Asc(strPrefixContnrNo.Chars(intCountPrefixContainerNo))
                    If strCharcode >= 65 And strCharcode <= 90 Then
                        '
                    Else
                        If br_strErrorBuilder <> String.Empty Then
                            br_strErrorBuilder = String.Concat(br_strErrorBuilder, "MANDATORY FIELD : Container No. First 4 digit should be Alpha characters.")
                        Else
                            br_strErrorBuilder = String.Concat("MANDATORY FIELD : Container No. First 4 digit should be Alpha characters.")
                        End If
                        Return False
                        Exit For
                    End If
                Next

                For intCountContainerNo = 0 To strContrdigit.ToString.Length - 1
                    Dim strCharcode As Integer = Asc(strContrdigit.Chars(intCountContainerNo))
                    If strCharcode >= 48 And strCharcode <= 57 Then
                        '
                    Else
                        If br_strErrorBuilder <> String.Empty Then
                            br_strErrorBuilder = String.Concat(br_strErrorBuilder, "MANDATORY FIELD : Container No. Invalid 6 digit numeric in 11 digit container number.")
                        Else
                            br_strErrorBuilder = String.Concat("MANDATORY FIELD : Container No. Invalid 6 digit numeric in 11 digit container number.")
                        End If
                        Return False
                        Exit For
                    End If
                Next
                If bv_blnIncludeChkDigtValidation Then
                    Dim intlastdig As Integer
                    Dim checkdigit As String = Left(bv_strContainerNo, 10)
                    intlastdig = pub_GetCheckDigit(checkdigit)
                    If intContrnoLtdt = intlastdig Then
                        Return True
                    Else
                        If br_strErrorBuilder <> String.Empty Then
                            br_strErrorBuilder = String.Concat(br_strErrorBuilder, "MANDATORY FIELD : Container No. Invalid Check Digit. Check Digit must be ", intlastdig, ".")
                        Else
                            br_strErrorBuilder = String.Concat("MANDATORY FIELD : Container No. Invalid Check Digit. Check Digit must be ", intlastdig, ".")
                        End If
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                If br_strErrorBuilder <> String.Empty Then
                    br_strErrorBuilder = String.Concat(br_strErrorBuilder, "MANDATORY FIELD : Invalid Container No.")
                Else
                    br_strErrorBuilder = String.Concat("MANDATORY FIELD : Invalid Container No.")
                End If
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_GetCheckDigit"
    Public Shared Function pub_GetCheckDigit(ByVal bv_strCheckDigit As String) As Integer
        Try
            Dim a() As Integer = {10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 34, 35, 36, 37, 38}
            Dim intCount As Integer
            Dim total As Integer = 0
            Dim product As Integer

            bv_strCheckDigit = bv_strCheckDigit.ToUpper

            If Not bv_strCheckDigit.Trim = String.Empty Then
                Dim retVal As Boolean = True
                For intCount = 0 To bv_strCheckDigit.Trim.Length - 1
                    Dim strCharcode As Integer = Asc(bv_strCheckDigit.Chars(intCount))
                    'CHECK FOR ALPHABET
                    If strCharcode >= 65 And strCharcode <= 90 Then
                        product = a(strCharcode - 65)
                        'CHECK FOR NUMERIC
                    ElseIf strCharcode >= 48 And strCharcode <= 57 Then
                        product = strCharcode - 48
                    Else
                        retVal = False
                    End If
                    total = CInt(total + (product * Math.Pow(2, intCount)))
                Next intCount

                If retVal Then
                    Dim retChkdigit As Integer
                    retChkdigit = (total Mod 11) Mod 10
                    Return retChkdigit
                End If
            End If
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class
