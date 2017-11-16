
''' This class helps to create dynamic master page with common schema
''' </summary>
''' <remarks></remarks>
Partial Class Masters_CodeMaster
    Inherits Pagebase

#Region "Declarations"
    Private strMSGUPDATE As String = "Updated Successfully."
    Private bln048Exists As Boolean
    Private str048KeyValue As String
#End Region

#Region "Parameters"
    Public dsCodeMaster As DataSet
    Public dtCodeMaster As DataTable
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
    Dim strActivityId As String
#End Region

#Region "Page_Load"
    ''' <summary>
    ''' This method is used to invoke page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim str_PageTitle As String = String.Empty
            ifgCodeMaster.DeleteButtonText = "Delete"
            ifgCodeMaster.RefreshButtonText = "Refresh"
            strActivityId = Request.QueryString("ifgActivityId")
            str_PageTitle = pvt_GetPageTittle()
            If str_PageTitle = "Equipment Status" Then
                ifgCodeMaster.AllowAdd = False
                ifgCodeMaster.AllowDelete = False
                ifgCodeMaster.ActionButtons.Item(0).Visible = False
            Else
                ifgCodeMaster.AllowAdd = True
                ifgCodeMaster.ActionButtons.Item(0).Visible = True
                ifgCodeMaster.AllowDelete = True
                ifgCodeMaster.AddButtonText = String.Concat("Add ", str_PageTitle)
                ifgCodeMaster.ActionButtons.Item(0).Text = "Upload"
            End If

            ifgCodeMaster.ActionButtons.Item(1).Text = "Export"
            If Not Request.QueryString("Export") Is Nothing Then
                If Request.QueryString("Export") = "True" Then

                    Dim objCommon As New CommonData
                    Dim objstream As New IO.StringWriter
                    dsCodeMaster = RetrieveData("CodeMaster" + strActivityId)
                    If dsCodeMaster.Tables.Count > 0 Then
                        objstream = objCommon.ExportToExcel(dsCodeMaster.Tables(0), str_PageTitle, "Code,Description,Active", "ID", False)
                        Response.ContentType = "application/vnd.Excel"
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + str_PageTitle + ".xls")
                        Response.Write(objstream.ToString())
                        Response.End()
                    End If
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "Page_OnCallback"
    ''' <summary>
    ''' This method is used to invoke call back method
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "ValidateCode"
                pvt_ValidatePK(e.GetCallbackValue("Code"), e.GetCallbackValue("TableName"), _
                               e.GetCallbackValue("GridIndex"), e.GetCallbackValue("RowState"), e.GetCallbackValue("WFDATA"), e.GetCallbackValue("ActivityId"))
            Case "UpdateCodeMaster"
                pvt_UpdateCode(e.GetCallbackValue("TableName"), e.GetCallbackValue("WFDATA"))
        End Select
    End Sub
#End Region

#Region "ifgCodeMaster_ClientBind"
    ''' <summary>
    ''' This method is to bind the grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ifgCodeMaster_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgCodeMaster.ClientBind
        Try
            strActivityId = Request.QueryString("ifgActivityId")
            Dim strTableName As String = e.Parameters("TableName")
            Dim strPageTitle As String = e.Parameters("PageTitle")
            Dim strWFDATA As String = e.Parameters("WFDATA")
            Dim objCommondata As New CommonData
            If Not String.IsNullOrEmpty(strTableName) Then
                Dim obj As New CodeMaster

                'Get data from corresponding table
                obj._New(strTableName)
                ifgCodeMaster.UseCachedDataSource = True
                If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                    dsCodeMaster = obj.pub_GetCodeMaster(String.Concat("DPT_ID=", objCommondata.GetHeadQuarterID().ToString))
                Else
                    dsCodeMaster = obj.pub_GetCodeMaster(strWFDATA)
                End If
                dtCodeMaster = dsCodeMaster.Tables(strTableName)

                e.DataSource = dtCodeMaster
                Dim strCaption As String = dsCodeMaster.Tables(strTableName).Columns("Code").Caption
                CacheData("tableName" + strActivityId, strTableName)
                CacheData("pagetitle" + strActivityId, strPageTitle)
                CacheData("pagename" + strActivityId, strPageTitle)
                CacheData("CodeMaster" + strActivityId, dsCodeMaster)

                DisableToolbarBasedOnSettings()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCodeMaster_RowDataBound"
    ''' <summary>
    ''' This event is executed at the time of binding code master grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ifgCodeMaster_RowDataBound(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridRowEventArgs) Handles ifgCodeMaster.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                Dim dt As DataTable = ifgCodeMaster.DataSource

                Dim strReadOnly As String = String.Empty
                Dim strValidationType As String = String.Empty
                If dt IsNot Nothing Then
                    strValidationType = dt.Columns("Code").Caption
                    strReadOnly = dt.Columns("Code").Caption
                End If

                If Not e.Row.DataItem Is Nothing Then
                    If drv.Row.RowState = DataRowState.Unchanged Then
                        CType(e.Row.Cells(0), iInterchange.WebControls.v4.Data.iFgFieldCell).ReadOnly = True
                    End If
                End If
                Dim strPageTitle As String = RetrieveData("pagetitle")
                Dim txtCode As iTextBox = CType(DirectCast(DirectCast(e.Row.Cells(0),  _
                            iFgFieldCell).ContainingField,  _
                            TextboxField).TextBox, iTextBox)
                Dim objcommon As New CommonData
                str048KeyValue = objcommon.GetConfigSetting("048", bln048Exists)
                If bln048Exists Then
                    If str048KeyValue.ToLower = "true" Then
                        txtCode.Validator.RegErrorMessage = "Only Alphabets are allowed"
                        txtCode.Validator.RegularExpression = "^[a-zA-Z]+$"
                        txtCode.Validator.RegexValidation = True
                    Else
                        txtCode.Validator.RegErrorMessage = "Only Alphabets and Numbers are allowed"
                        txtCode.Validator.RegularExpression = "^[a-zA-Z0-9]+$"
                        txtCode.Validator.RegexValidation = True
                    End If
                End If
                DisableToolbarBasedOnSettings()
                txtCode.HelpText = String.Concat("14,", dt.TableName, "_", dt.Columns("Code").Caption.Split(";")(0))
                Dim txtDescription As iTextBox = CType(DirectCast(DirectCast(e.Row.Cells(1),  _
                        iFgFieldCell).ContainingField,  _
                        TextboxField).TextBox, iTextBox)
                txtDescription.HelpText = String.Concat("15,", dt.TableName, "_", dt.Columns("Description").Caption.Split(";")(0))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCodeMaster_RowInserting"
    ''' <summary>
    ''' This event is executed at the time of inserting row in code master grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ifgCodeMaster_RowInserting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridInsertEventArgs) Handles ifgCodeMaster.RowInserting
        Try
            dtCodeMaster = ifgCodeMaster.DataSource
            Dim lngID As Long
            lngID = CommonWeb.GetNextIndex(dtCodeMaster, "ID")
            e.Values("ID") = lngID
            DisableToolbarBasedOnSettings()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region

#Region "ifgCodeMaster_RowDeleting"
    ''' <summary>
    ''' This event is executed at the time of deleting row in code master grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ifgCodeMaster_RowDeleting(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridDeleteEventArgs) Handles ifgCodeMaster.RowDeleting
        Try
            DisableToolbarBasedOnSettings()
            If CType(ifgCodeMaster.DataSource, DataTable).Select("ID=" & e.Keys(0))(0).RowState <> DataRowState.Added Then
                dtCodeMaster = ifgCodeMaster.DataSource
                e.Cancel = True
                e.OutputParamters("Delete") = String.Concat(pvt_GetPageTittle(), " : ", " Code ", dtCodeMaster.Select("ID=" & e.Keys(0))(0).Item("Code").ToString, " cannot be deleted")
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub
#End Region
    ''' <summary>
    ''' This method is to disable and enable add action button based on settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisableToolbarBasedOnSettings()
        Dim bln_047 As Boolean
        Dim str_047 As String
        Dim objCommon As New CommonData
        Dim str_049 As String
        Dim bln_049 As Boolean
        str_047 = objCommon.GetConfigSetting("047", bln_047)
        Dim strPageTitle As String = RetrieveData("pagename" + strActivityId)
        str_049 = objCommon.GetConfigSetting("049", bln_049)
        If strPageTitle = "Equipment Status" AndAlso str_049 = True Then
            ifgCodeMaster.AllowAdd = True
            ifgCodeMaster.AllowDelete = True
            ifgCodeMaster.ActionButtons.Item(0).Visible = True

            'Else
            '    ifgCodeMaster.AllowAdd = False
            '    ifgCodeMaster.AllowDelete = False
            '    ifgCodeMaster.ActionButtons.Item(0).Visible = False
            '    ifgCodeMaster.AddButtonText = String.Concat("Add ", strPageTitle)
        End If
        ifgCodeMaster.AddButtonText = String.Concat("Add ", strPageTitle)
        If bln_047 AndAlso str_047 <> "" AndAlso strPageTitle = "Activity Status" Then
            ifgCodeMaster.AllowAdd = True
            ifgCodeMaster.ActionButtons.Item(0).Visible = False
            ifgCodeMaster.ActionButtons.Item(1).Visible = False
        End If
    End Sub
#Region "pvt_ValidatePK"
    ''' <summary>
    ''' This method is to validate code for duplication
    ''' </summary>
    ''' <param name="bv_strCode"></param>
    ''' <param name="bv_strTableName"></param>
    ''' <param name="bv_intGridRowIndex"></param>
    ''' <param name="bv_strRowState"></param>
    ''' <remarks></remarks>
    Private Sub pvt_ValidatePK(ByVal bv_strCode As String, ByVal bv_strTableName As String, ByVal bv_intGridRowIndex As Integer, ByVal bv_strRowState As String, ByVal bv_strWFDATA As String, ByVal strActivityId As String)
        Try
            Dim blndsValid As Boolean
            Dim strExistCode As String = String.Empty
            dsCodeMaster = RetrieveData("CodeMaster" + strActivityId)

            Dim dtCodeMasterTemp As DataTable = dsCodeMaster.Tables(bv_strTableName).Copy
            dtCodeMasterTemp.AcceptChanges()

            ''Checking whether the entered code is available in grid
            Dim intResultIndex() As System.Data.DataRow = dtCodeMasterTemp.Select("Code" & "='" & bv_strCode & "'")

            If intResultIndex.Length > 0 Then
                blndsValid = False
            Else
                blndsValid = True
            End If

            'Checking whether the entered code is available in database
            If blndsValid = True Then
                If bv_strRowState = "Added" Then
                    Dim objCodeMaster As New CodeMaster
                    Dim objCommondata As New CommonData
                    objCodeMaster._New(bv_strTableName)
                    If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                        bv_strWFDATA = String.Concat("DPT_ID=", objCommondata.GetHeadQuarterID().ToString)
                    End If
                    blndsValid = objCodeMaster.pub_ValidatePK(bv_strCode, bv_strWFDATA)
                End If
            End If

            If blndsValid = True Then
                pub_SetCallbackReturnValue("bNotExists", "true")
            Else
                pub_SetCallbackReturnValue("bNotExists", "false")
            End If
            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

#Region "pvt_UpdateCode"
    ''' <summary>
    ''' This method is to submit the grid details, for new and edit mode
    ''' </summary>
    ''' <param name="bv_strTable"></param>
    ''' <remarks></remarks>
    Private Sub pvt_UpdateCode(ByVal bv_strTable As String, ByVal bv_strWFDATA As String)
        Try
            Dim objCodeMaster As New CodeMaster
            Dim objCommondata As New CommonData
            objCodeMaster._New(bv_strTable)
            dtCodeMaster = ifgCodeMaster.DataSource
            Dim str_PageTitle As String = String.Empty
            str_PageTitle = pvt_GetPageTittle()
            If objCommondata.GetMultiLocationSupportConfig().ToLower = "true" Then
                objCodeMaster.pub_UpdateCodeMaster(dtCodeMaster.DataSet, objCommondata.GetCurrentUserName(), objCommondata.GetCurrentDate(), String.Concat("DPT_ID=", objCommondata.GetHeadQuarterID().ToString))
            Else
                objCodeMaster.pub_UpdateCodeMaster(dtCodeMaster.DataSet, objCommondata.GetCurrentUserName(), objCommondata.GetCurrentDate(), bv_strWFDATA)
            End If
            dtCodeMaster.AcceptChanges()

            pub_SetCallbackReturnValue("Message", String.Concat(str_PageTitle, " Code(s) ", strMSGUPDATE))
            pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Masters/CodeMaster.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/MaxLength.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                  MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "getPageTittle"
    Private Function pvt_GetPageTittle() As String
        Try
            Dim str_PageTitle As String = String.Empty

            Dim strpagetitle As String = RetrieveData("pagename" + strActivityId)
            Dim strTitle() As String = strpagetitle.ToString.Split(CChar(">>"))

            If strTitle.Length > 1 Then
                str_PageTitle = Trim(strTitle(2).ToString())
            Else
                str_PageTitle = strpagetitle
            End If
            Return str_PageTitle
        Catch ex As Exception
            Return String.Empty
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                 MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

End Class