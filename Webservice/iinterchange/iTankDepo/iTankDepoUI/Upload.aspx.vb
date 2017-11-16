
Partial Class U_Upload
    Inherits Framebase


#Region "Declarations"
    Private dsUpload As New UploadDataSet
    Private Const UPLOAD As String = "UPLOAD"
    Private Const EXCELDATA As String = "dtExceldata"
    Private Const BILLING_TYPE As String = "Billing_Type"
    Private Const _CD As String = "_CD"
    Private Const _DSCRPTN_VC As String = "_DSCRPTN_VC"
    Dim bln_054EnabledBit As Boolean
    Dim str_054Value As String
#End Region

#Region "Page_LoadComplete1"
    Protected Sub Page_LoadComplete1(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            Page.ClientScript.RegisterStartupScript(GetType(String), "HideData", _
                  "HideData();", True)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region
    'UIG Fix
#Region "Page_OnCallback"

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As iInterchange.Framework.UI.PageCallbackEventArgs) Handles Me.OnCallback
        Try
            Select Case e.CallbackType
                Case "InsertFileUpload"
                    pvt_InsertFileUpload(e.GetCallbackValue("SchemaID"), _
                                         e.GetCallbackValue("filename"), _
                                         CInt(e.GetCallbackValue("DepotId")), _
                                         e.GetCallbackValue("USERNAME"), _
                                         e.GetCallbackValue("TableName"), _
                                         e.GetCallbackValue("Size"), _
                                         e.GetCallbackValue("Type"), _
                                         e.GetCallbackValue("Cstmr_id"), _
                                         e.GetCallbackValue("PageType"), _
                                         e.GetCallbackValue("TariffId"))



            End Select
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "Page_PreRender"
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/UI/Upload.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsCallback AndAlso Not RetrieveData("UploadData") Is Nothing Then
                Dim dtUploadData As DataTable = CType(RetrieveData("UploadData"), DataTable)
                Dim strIdColumn As String = CType(RetrieveData("IdColumn"), String)
                Dim strTableName As String = CType(RetrieveData("tablename"), String)
                Dim strDisplayType As String = CType(RetrieveData("DisplayType"), String)
                Dim dtExcelData As DataTable = CType(RetrieveData(EXCELDATA), DataTable)
                If Not dtUploadData Is Nothing AndAlso dtUploadData.Rows.Count > 0 Then
                    pvt_BindGrid(dtExcelData, dtUploadData, strIdColumn, strTableName, strDisplayType)
                End If
            End If

            Dim strFileName As String = ""
            Dim strFileExtension As String
            Dim strActualFileName As String = ""
            Dim intLastIndex As Integer
            Dim lngFileSize As Long
            Dim strSchemaID As String = Request.QueryString("SchemaID")
            If Not Page.IsPostBack AndAlso Not strSchemaID = "" AndAlso Not Page.IsCallback Then
                Dim objcommon As New CommonData
                Dim intDepotID As Integer = objcommon.GetDepotID()
                hdnDpt_ID.Value = intDepotID
                ' hdnDpt_ID.Value = Request.QueryString("DPT_ID")
                pvt_GetUploadSchema(strSchemaID)
                pvt_GetUploadSchemaKeys(strSchemaID)
            End If

            If Request.Files.Count = 1 Then
                Dim strLocation As String = String.Concat(CommonWeb.pub_GetConfigValue("UploadPhyPath"), "ExcelUpload\")
                strFileName = filename.PostedFile.FileName
                intLastIndex = strFileName.LastIndexOf("\")
                If intLastIndex > 0 OrElse strFileName <> "" Then
                    If intLastIndex > 0 Then
                        intLastIndex += 1
                        strActualFileName = strFileName.Substring(intLastIndex, (strFileName.Length - intLastIndex))
                    Else
                        strActualFileName = strFileName
                    End If

                    strFileExtension = System.IO.Path.GetExtension(strActualFileName)

                    strFileName = System.IO.Path.GetFileNameWithoutExtension(strActualFileName)

                    If strFileExtension = ".xlsx" OrElse strFileExtension = ".xls" Then

                        Dim strGuid As String = DateTime.Now.ToFileTime().ToString

                        strFileName = String.Concat(strFileName, strGuid, strFileExtension)

                        lngFileSize = filename.PostedFile.ContentLength

                        If lngFileSize < 2097152 Then
                            If strActualFileName.Length <= 50 Then

                                Dim strClientFilePath As String = String.Concat(strLocation, strFileName)
                                filename.PostedFile.SaveAs(strClientFilePath)

                                hdnFileName.Value = String.Concat(strFileName)
                                Page.ClientScript.RegisterStartupScript(GetType(String), "error", _
                                "showInfoMessage(""File Uploaded. Press upload to process the uploaded file."");", True)

                            Else
                                hdnFileName.Value = ""
                                Page.ClientScript.RegisterStartupScript(GetType(String), "error", _
                                   "showErrorMessage(""Uploaded file name must be below 50 characters."");", True)
                            End If
                        Else
                            hdnFileName.Value = ""
                            Page.ClientScript.RegisterStartupScript(GetType(String), "error", _
                                 "showErrorMessage(""Uploaded file size must be below 2MB."");", True)
                        End If
                    Else
                        hdnFileName.Value = ""
                        Page.ClientScript.RegisterStartupScript(GetType(String), "error", _
                                       "showErrorMessage(""Only excel (.xlsx,.xls) files can be uploaded."");", True)
                    End If
                Else

                End If
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetUploadSchema"
    Private Sub pvt_GetUploadSchema(ByVal bv_strSchemaID As String)
        Try
            Dim objUpload As New Upload
            dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Clear()
            dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Merge(objUpload.pub_GetUPLOAD_SCHEMABySCHM_ID(bv_strSchemaID).Tables(UploadData._UPLOAD_SCHEMA))
            hdnUploadTblName.Value = dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(UploadData.UPLD_TBL_NAM)
            CacheData(UPLOAD, dsUpload)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GetUploadSchemaKeys"
    Private Sub pvt_GetUploadSchemaKeys(ByVal bv_strSchemaID As String)
        Try
            Dim objUpload As New Upload
            dsUpload.Tables(UploadData._UPLOAD_KEYS).Clear()
            dsUpload.Tables(UploadData._UPLOAD_KEYS).Merge(objUpload.pub_GetUPLOADKEYSBySCHMID(bv_strSchemaID).Tables(UploadData._UPLOAD_KEYS))
            CacheData(UPLOAD, dsUpload)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub

#End Region

#Region "pvt_InsertFileUpload"

    Private Sub pvt_InsertFileUpload(ByVal bv_SchemaID As String, ByVal bv_strFileName As String, _
                                       ByVal bv_intDepotID As Int32, ByVal bv_strUserName As String, _
                                       ByVal bv_strTblName As String, ByVal bv_strSize As String, ByVal bv_strType As String, _
                                       ByVal bv_strCstmr_ID As String, ByVal bv_strPageType As String, _
                                       ByVal bv_intTariffId As Int64)

        Try
            Dim objUpload As New Upload
            'UIG Fix
            Dim dtCodeMaster As New DataTable
            Dim dtCodeMasterData As New DataTable
            Dim blnCheckCode As Boolean = True
            pvt_GetUploadSchema(bv_SchemaID)
            pvt_GetUploadSchemaKeys(bv_SchemaID)
            Dim strLocation As String = String.Concat(CommonWeb.pub_GetConfigValue("UploadPhyPath"), "ExcelUpload\")

            Dim strClientFileName As String = String.Concat(strLocation, bv_strFileName)
            If bv_strTblName.ToUpper = "PRE_ADVICE" Then
                bv_strCstmr_ID = bv_SchemaID
            End If
            Dim dtExcelData As New DataTable
            dtExcelData = pvt_ProcessExcelFile(strClientFileName, bv_strTblName, bv_strSize, bv_strType, bv_strCstmr_ID)
           
            If blnCheckCode = True Then
                'End If

                Dim dtSourceData As New DataTable
                dtSourceData = pvt_ProcessSourceTable(bv_intDepotID)

                Dim blnCheck As Boolean = pvt_CompareSchema(dtSourceData, dtExcelData)
                If blnCheck Then
                    Dim strRefObjectName As String
                    Dim strUploadTableName As String

                    dsUpload = CType(RetrieveData(UPLOAD), UploadDataSet)
                   
                    dtExcelData.Columns.Add("Valid", GetType(Boolean)).DefaultValue = True
                    dtExcelData.Columns.Add("Error", GetType(String)).DefaultValue = ""

                    With dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0)
                        strRefObjectName = .Item(UploadData.RF_OBJCT_NAM)
                        strUploadTableName = .Item(UploadData.UPLD_TBL_NAM)
                    End With

                    Dim objcommonUI As New CommonUIs
                    dtCodeMaster = objcommonUI.GetTableSchema(bv_strTblName.ToUpper())
                    dtCodeMasterData = dtCodeMaster.Clone
                    dtCodeMasterData.TableName = bv_strTblName.ToUpper()
                    If (bv_strTblName = "TARIFF_CODE_DETAIL") AndAlso dtSourceData.Rows.Count > 0 Then
                        dtSourceData.Clear()
                    End If
                    If Not dsUpload.Tables.Contains(bv_strTblName.ToUpper()) Then
                        dsUpload.Tables.Add(dtCodeMasterData)
                        dsUpload.Tables(bv_strTblName.ToUpper()).Clear()
                    End If

                    dtExcelData.TableName = strUploadTableName

                    Dim dtSchema As New DataTable(UploadData._UPLOAD_SCHEMA_DETAIL)
                    dtSchema = pvt_GetSchema(strUploadTableName)


                    bv_SchemaID = bv_SchemaID.Replace("+", "")


                    'Code Validation
                    Dim drsUniqueKey As DataRow()
                    drsUniqueKey = dsUpload.Tables(UploadData._UPLOAD_KEYS).Select(String.Concat(UploadData.KY_TYP, "='UNIQUE' AND ", UploadData.SCHM_ID, "=", bv_SchemaID))

                    For Each drUniqueKey As DataRow In drsUniqueKey
                        Dim strColumnName As String = drUniqueKey.Item(UploadData.CLMN_NAM)
                        strColumnName = pvt_GetColumnCaptionName(strUploadTableName, strColumnName)
                        pvt_ValidateUniqueCode(strColumnName, dtExcelData, dtSourceData)
                    Next

                    Dim dtExcel As DataTable = dtExcelData.Copy()
                    If dtExcel.Rows.Count > 0 Then
                        CacheData(EXCELDATA, dtExcel)

                        pvt_ValidateCode(dtExcelData, dtSourceData, bv_intDepotID)


                        'Max Length Validation
                        pvt_ValidateMaxLength(dtExcelData, dtSchema, dtSourceData, strUploadTableName)
                        If (dtExcelData.TableName.ToUpper = "TARIFF_CODE_DETAIL") Then
                            dtExcelData.Columns.Add("TRFF_CD_ID", GetType(Int64))
                            For i = 0 To dtExcelData.Rows.Count - 1
                                dtExcelData.Rows(i).Item("TRFF_CD_ID") = bv_intTariffId
                            Next
                        End If
                        Dim blnInsert As Boolean = pvt_InsertBulkData(dtExcelData, strUploadTableName, bv_intDepotID, bv_strUserName, bv_strPageType)

                        If blnInsert = False Then
                            pub_SetCallbackStatus(False)
                            Exit Sub
                        End If
                    End If

                    Dim strIdCol As String = objUpload.pvt_GetIdColumn(strUploadTableName)

                    Dim dt As DataRow() = dtExcelData.Select("valid=True")

                    If dt.Length() > 0 Then
                        pub_SetCallbackReturnValue("Message", "File Uploaded Successfully.")
                        pub_SetCallbackReturnValue("Error", "No")
                        pub_SetCallbackReturnValue("Upload", "yes")
                        pub_SetCallbackReturnValue("IdCol", strIdCol)
                        pub_SetCallbackStatus(True)
                    Else
                        pub_SetCallbackReturnValue("Message", "")
                        pub_SetCallbackReturnValue("Error", "No Valid Data to Upload.")
                        pub_SetCallbackReturnValue("Upload", "yes")
                        pub_SetCallbackReturnValue("IdCol", strIdCol)
                        pub_SetCallbackStatus(True)
                    End If

                Else
                    pub_SetCallbackReturnValue("Error", "The Excel file does not match the template. Please use files from template.")
                    pub_SetCallbackReturnValue("Upload", "no")

                End If
            Else
                pub_SetCallbackReturnValue("Error", "The Excel file does not match the template. Please use files from template.")
                pub_SetCallbackReturnValue("Upload", "no")
            End If

            pub_SetCallbackStatus(True)

        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ProcessExcelFile"
    Private Function pvt_ProcessExcelFile(ByVal strFileName As String, ByVal bv_strTblName As String, ByVal bv_strSize As String, ByVal bv_strType As String, ByVal bv_strCstmr_Id As String) As DataTable
        Try
            Dim MyConnection As System.Data.OleDb.OleDbConnection
            Dim dtDestination As New DataTable("Destination")
            Dim dao As System.Data.OleDb.OleDbDataAdapter
            Dim strExcelQuery As String

            Dim strExcelCon As String = CommonWeb.pub_GetConfigValue("ExcelFileConnectionString")

            Dim strCon As String = "Data Source='" & strFileName & "';" & strExcelCon & ";"

            MyConnection = New System.Data.OleDb.OleDbConnection(strCon)

            strExcelQuery = String.Concat("SELECT * FROM [Sheet1$]")

            If MyConnection.State = Data.ConnectionState.Closed Then
                MyConnection.Open()
            End If

            dao = New System.Data.OleDb.OleDbDataAdapter(strExcelQuery, MyConnection)

            dao.Fill(dtDestination)

            pub_UseFirstRowAsHeader(dtDestination)

            MyConnection.Close()

            Dim newDataTable As New DataTable
            newDataTable = dtDestination.Clone()
            For Each dc As DataColumn In newDataTable.Columns
                dc.DataType = GetType(String)
            Next

            If bv_strTblName.ToUpper() = "TARIFF" Then
                newDataTable.Columns.Add("EQPMNT_SZ", GetType(String)).DefaultValue = ""
                newDataTable.Columns.Add("EQPMNT_TYP", GetType(String)).DefaultValue = ""
                newDataTable.Columns.Add("CSTMR_ID", GetType(Int64)).DefaultValue = 0
            End If
            If bv_strTblName.ToUpper() = "PRE_ADVICE" Then
                newDataTable.Columns.Add("EQPMNT_NO", GetType(String)).DefaultValue = ""
                newDataTable.Columns.Add("EQPMNT_TYP_ID", GetType(String)).DefaultValue = ""
                newDataTable.Columns.Add("CSTMR_ID", GetType(String)).DefaultValue = 0
                newDataTable.Columns.Add("RMRKS_VC", GetType(String)).DefaultValue = ""
                newDataTable.Columns.Add("ENTRD_DT", GetType(DateTime)).DefaultValue = Now
                dtDestination.Columns.Add("EQPMNT_NO", GetType(String)).DefaultValue = ""
                dtDestination.Columns.Add("EQPMNT_TYP_ID", GetType(String)).DefaultValue = ""
                dtDestination.Columns.Add("CSTMR_ID", GetType(String)).DefaultValue = 0
                dtDestination.Columns.Add("RMRKS_VC", GetType(String)).DefaultValue = ""
                dtDestination.Columns.Add("ENTRD_DT", GetType(DateTime)).DefaultValue = Now
                If bv_strCstmr_Id = 81 Then
                    newDataTable.Columns.Add("EQPMNT_CD_ID", GetType(String)).DefaultValue = ""
                    dtDestination.Columns.Add("EQPMNT_CD_ID", GetType(String)).DefaultValue = ""
                    newDataTable.Columns.Add("CNSGN_NM", GetType(String)).DefaultValue = ""
                    dtDestination.Columns.Add("CNSGN_NM", GetType(String)).DefaultValue = ""
                ElseIf bv_strCstmr_Id = 82 Then
                    dtDestination.Columns.Add("PRDCT_ID", GetType(String)).DefaultValue = ""
                    newDataTable.Columns.Add("PRDCT_ID", GetType(String)).DefaultValue = ""
                    dtDestination.Columns.Add("CLNNG_RFRNC_NO", GetType(String)).DefaultValue = ""
                    newDataTable.Columns.Add("CLNNG_RFRNC_NO", GetType(String)).DefaultValue = ""
                End If
            End If
            For Each drdet As DataRow In dtDestination.Rows
                Dim dr As DataRow = newDataTable.NewRow()
                For Each dc As DataColumn In newDataTable.Columns
                    If bv_strTblName.ToUpper() = "TARIFF" Then
                        If Not dc.ColumnName.ToUpper() = "EQPMNT_SZ" AndAlso Not dc.ColumnName.ToUpper() = "EQPMNT_TYP" AndAlso Not dc.ColumnName.ToUpper() = "CSTMR_ID" Then
                            If dc.ColumnName.ToLower() = "code" Then
                                dr.Item(dc.ColumnName) = drdet.Item(dc.ColumnName).ToString().ToUpper().Trim()
                            Else
                                dr.Item(dc.ColumnName) = drdet.Item(dc.ColumnName).ToString().Trim()
                            End If
                        End If
                    ElseIf bv_strTblName.ToUpper() = "PRE_ADVICE" Then
                        If Not dc.ColumnName.ToUpper() = "EQPMNT_NO" AndAlso dc.ColumnName.ToUpper = "EQPMNT_TYP_ID" AndAlso dc.ColumnName.ToUpper = "CSTMR_ID" Then
                            dr.Item(dc.ColumnName) = drdet.Item(dc.ColumnName).ToString().ToUpper().Trim()
                        ElseIf Not dc.ColumnName.ToUpper = "ENTRD_DT" Then
                            dr.Item(dc.ColumnName) = drdet.Item(dc.ColumnName).ToString().Trim()
                        End If
                    Else
                        If dc.ColumnName.ToLower() = "code" Then
                            dr.Item(dc.ColumnName) = drdet.Item(dc.ColumnName).ToString().ToUpper().Trim()
                        Else
                            dr.Item(dc.ColumnName) = drdet.Item(dc.ColumnName).ToString().Trim()
                        End If
                    End If

                    If bv_strTblName.ToUpper() = "PRE_ADVICE" Then
                        dr.Item("EQPMNT_NO") = drdet.Item("Equipment_No")
                        dr.Item("EQPMNT_TYP_ID") = drdet.Item("Equipment_Type")
                        dr.Item("CSTMR_ID") = drdet.Item("Customer_Code")
                        If bv_strCstmr_Id = 81 Then
                            dr.Item("CNSGN_NM") = drdet.Item("Consignee")
                            dr.Item("AUTH_NO") = drdet.Item("Auth_No")
                            dr.Item("EQPMNT_CD_ID") = drdet.Item("Equipment_Code")
                        ElseIf bv_strCstmr_Id = 82 Then
                            dr.Item("PRDCT_ID") = drdet.Item("Previous_Cargo")
                            dr.Item("CLNNG_RFRNC_NO") = drdet.Item("Cleaning_Reference")
                        End If
                        Dim regExString As String = "^((31(?! (FEB|APR|JUN|SEP|NOV)))|((30|29)(?! FEB))|(29(?= FEB (((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))|(0?[1-9])|1\d|2[0-8])-(JAN|FEB|MAR|MAY|APR|JUL|JUN|AUG|OCT|SEP|NOV|DEC)-((1[6-9]|[2-9]\d)\d{2})$"
                        Dim blnAlphaNumeric As Boolean = Regex.IsMatch(drdet.Item("ETA_Date").ToString().ToUpper(), regExString)
                        If blnAlphaNumeric Then
                            dr.Item("ENTRD_DT") = drdet.Item("ETA_Date")
                        End If
                        dr.Item("RMRKS_VC") = drdet.Item("Remarks")
                    End If

                    'If bv_strTblName.ToUpper() = ReserveBookingData._OUTWARD_PASS Or bv_strTblName.ToUpper() = InputRedeliveryData._INWARD_PASS Then
                    '    If dc.ColumnName.ToUpper() = ReserveBookingData.AUTH_NO Or dc.ColumnName.ToUpper() = ReserveBookingData.XL_EQPMNT_NO Or dc.ColumnName.ToUpper() = ReserveBookingData.XL_BLLNG_TYP Then

                    '        If (drdet.Item(ReserveBookingData.AUTH_NO).ToString() IsNot Nothing AndAlso Not drdet.Item(ReserveBookingData.AUTH_NO).ToString() = "") Then
                    '            dr.Item(ReserveBookingData.AUTH_NO) = drdet.Item(ReserveBookingData.AUTH_NO).ToString().ToUpper().Trim()
                    '        End If

                    '        If (drdet.Item(ReserveBookingData.XL_EQPMNT_NO).ToString() IsNot Nothing AndAlso Not drdet.Item(ReserveBookingData.XL_EQPMNT_NO).ToString() = "") Then
                    '            dr.Item(ReserveBookingData.XL_EQPMNT_NO) = drdet.Item(ReserveBookingData.XL_EQPMNT_NO).ToString().ToUpper().Trim()
                    '        End If

                    '        If (drdet.Item(ReserveBookingData.AUTH_NO).ToString() IsNot Nothing AndAlso Not drdet.Item(ReserveBookingData.XL_EQPMNT_NO).ToString() IsNot Nothing AndAlso drdet.Item(ReserveBookingData.XL_BLLNG_TYP).ToString() IsNot Nothing) Then
                    '            dr.Item(ReserveBookingData.XL_BLLNG_TYP) = drdet.Item(ReserveBookingData.XL_BLLNG_TYP).ToString().ToUpper().Trim()
                    '        End If

                    '        If (drdet.Item(ReserveBookingData.AUTH_NO).ToString() IsNot Nothing AndAlso Not drdet.Item(ReserveBookingData.XL_EQPMNT_NO).ToString() IsNot Nothing AndAlso drdet.Item(ReserveBookingData.XL_BLLNG_TYP).ToString() Is Nothing Or drdet.Item(ReserveBookingData.XL_BLLNG_TYP).ToString().Trim() = "") Then
                    '            dr.Item(ReserveBookingData.XL_BLLNG_TYP) = "NULL"
                    '        End If

                    '        If (drdet.Item(ReserveBookingData.AUTH_NO).ToString() IsNot Nothing AndAlso Not drdet.Item(ReserveBookingData.XL_EQPMNT_NO).ToString() IsNot Nothing AndAlso drdet.Item(ReserveBookingData.XL_LSS_CD).ToString() Is Nothing Or drdet.Item(ReserveBookingData.XL_LSS_CD).ToString().Trim() = "") Then
                    '            dr.Item(ReserveBookingData.XL_LSS_CD) = "NULL"
                    '        End If

                    '    End If

                    'End If


                Next

                If bv_strTblName.ToUpper() = "TARIFF" Then
                    dr.Item("EQPMNT_SZ") = bv_strSize
                    dr.Item("EQPMNT_TYP") = bv_strType
                    dr.Item("CSTMR_ID") = bv_strCstmr_Id
                End If

                newDataTable.Rows.Add(dr)
            Next


            Return newDataTable
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pub_UseFirstRowAsHeader()"

    Public Sub pub_UseFirstRowAsHeader(ByRef dt As DataTable)
        Try
            Dim row As DataRow = dt.Rows(0)

            For i As Integer = 0 To row.ItemArray.Length - 1
                If Not (dt.Columns.Contains(row.Item(i).ToString())) Then
                    dt.Columns(i).ColumnName = row.Item(i).ToString()
                Else
                    dt.Columns(i).ColumnName = String.Concat(row.Item(i).ToString(), i)
                End If
            Next
            dt.Rows.RemoveAt(0)
            dt.AcceptChanges()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_ProcessSourceTable"
    Private Function pvt_ProcessSourceTable(ByVal bv_i32Depot_ID As Int32) As DataTable
        Try
            Dim objUpload As New Upload
            Dim strSchemaQuery As String
            dsUpload = CType(RetrieveData(UPLOAD), UploadDataSet)
            With dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0)
                strSchemaQuery = .Item(UploadData.SCHM_QRY)
            End With

            Dim dtSourceTable As New DataTable(UploadData._UPLOAD_SOURCE_TABLE)

            If Not dsUpload.Tables.Contains(UploadData._UPLOAD_SOURCE_TABLE) Then
                dsUpload.Tables.Add(dtSourceTable)
            End If

            dsUpload.Tables(UploadData._UPLOAD_SOURCE_TABLE).Clear()

            dtSourceTable = objUpload.pub_GetUPLOAD_SOURCE_TABLE(strSchemaQuery, bv_i32Depot_ID).Tables(UploadData._UPLOAD_SOURCE_TABLE)

            dsUpload.Tables(UploadData._UPLOAD_SOURCE_TABLE).Merge(dtSourceTable)

            CacheData(UPLOAD, dsUpload)

            Return dtSourceTable
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_CompareSchema"
    Private Function pvt_CompareSchema(ByVal dtSource As DataTable, ByVal dtExcel As DataTable) As Boolean
        Try
            Dim bool As Boolean = True
            If dtSource.Columns.Count <> dtExcel.Columns.Count Then
                Return False
            End If

            Dim excelcolumns As DataColumnCollection = dtExcel.Columns

            For Each dc As DataColumn In dtSource.Columns
                If Not excelcolumns.Contains(dc.ColumnName) Then
                    Return False
                End If
            Next
            Return bool
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_ValidateCode"
    Private Function pvt_ValidateCode(ByRef dtExceldata As DataTable, ByVal dtSource As DataTable, ByVal bv_intDepotID As Int32) As DataTable
        'Required Field Validation
        Dim regExString As String
        Dim RegexObj As Regex = New Regex("regularexpression")
        Dim objUpload As New Upload
        Dim blnAlphaNumeric As Boolean = True
        Dim intHeadQuarterID As Integer
        Dim objCommon As New CommonData
        If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
            intHeadQuarterID = objCommon.GetHeadQuarterID()
        Else
            intHeadQuarterID = bv_intDepotID
        End If
        Try
            Dim blnValidateCode As Boolean = True

            For i = 0 To dtExceldata.Rows.Count - 1
                Dim strErrorMsg As String = ""
                For j = 0 To dtExceldata.Columns.Count - 1
                    Dim strColumnName As String = dtExceldata.Columns(j).ColumnName
                    If Not dtExceldata.TableName.ToUpper() = "PRE_ADVICE" Then
                        If strColumnName = "" OrElse strColumnName = "" Then
                            Continue For
                        End If
                    End If
                    If strColumnName = "Valid" Then
                        Exit For
                    End If
                    If Not (dtExceldata.TableName.ToUpper() = "TARIFF" OrElse dtExceldata.TableName.ToUpper() = "TARIFF_CODE_DETAIL" OrElse dtExceldata.TableName.ToUpper() = "PRE_ADVICE") Then
                        If Not pvt_GetISNull(strColumnName, dtExceldata.TableName) AndAlso (IsDBNull(dtExceldata.Rows(i).Item(strColumnName)) OrElse dtExceldata.Rows(i).Item(strColumnName) = "") Then
                            If Not IsDBNull(dtExceldata.Rows(i).Item("Error")) AndAlso dtExceldata.Rows(i).Item("Error") <> "" Then
                                strErrorMsg = String.Concat(strErrorMsg, ", ")
                            End If
                            strErrorMsg = String.Concat(strErrorMsg, strColumnName & " Required.")
                            dtExceldata.Rows(i).Item("Error") = strErrorMsg
                            dtExceldata.Rows(i).Item("Valid") = False
                            blnValidateCode = False
                            Continue For
                        Else

                            If IsDBNull(dtExceldata.Rows(i).Item("Valid")) Then
                                dtExceldata.Rows(i).Item("Valid") = True
                            End If
                        End If
                    Else     'Only for Tariff Tables
                        If dtExceldata.TableName.ToUpper() = "TARIFF_CODE_DETAIL" Then
                            If Not pvt_GetISNull(strColumnName, dtExceldata.TableName) AndAlso dtExceldata.Rows(i).Item(strColumnName) = "" Then
                                dtExceldata.Rows(i).Item("Error") = strColumnName & " Required."
                                dtExceldata.Rows(i).Item("Valid") = False
                                Continue For
                            End If
                        ElseIf dtExceldata.TableName.ToUpper() = "PRE_ADVICE" Then 'Only for Pre-advice
                            If strColumnName = "Customer_Code" OrElse strColumnName = "Equipment_Type" OrElse strColumnName = "ETA_Date" OrElse strColumnName = "Equipment_No" OrElse strColumnName = "Equipment_Code" Then
                                str_054Value = objCommon.GetConfigSetting("054", bln_054EnabledBit)
                                'implement with settings for columns prodct_id and code
                                If strColumnName = "Equipment_Code" AndAlso str_054Value.ToLower = "true" AndAlso dtExceldata.Rows(i).Item(strColumnName).ToString.Trim = "" Then
                                    dtExceldata.Rows(i).Item("Error") = strColumnName & " Required."
                                    dtExceldata.Rows(i).Item("Valid") = False
                                    Continue For
                                    'ElseIf strColumnName = "Previous_Cargo" AndAlso str_054Value.ToLower = "false" AndAlso dtExceldata.Rows(i).Item(strColumnName).ToString.Trim = "" Then
                                    '    dtExceldata.Rows(i).Item("Error") = strColumnName & " Required."
                                    '    dtExceldata.Rows(i).Item("Valid") = False
                                    '    Continue For
                                ElseIf dtExceldata.Rows(i).Item(strColumnName).ToString.Trim = "" AndAlso dtExceldata.Rows(i).Item("Valid") = True Then
                                    dtExceldata.Rows(i).Item("Error") = strColumnName & " Required."
                                    dtExceldata.Rows(i).Item("Valid") = False
                                    Continue For
                                End If
                                If strColumnName = "ETA_Date" Then
                                    regExString = "^((31(?! (FEB|APR|JUN|SEP|NOV)))|((30|29)(?! FEB))|(29(?= FEB (((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))|(0?[1-9])|1\d|2[0-8])-(JAN|FEB|MAR|MAY|APR|JUL|JUN|AUG|OCT|SEP|NOV|DEC)-((1[6-9]|[2-9]\d)\d{2})$"
                                    blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName).ToString().ToUpper(), regExString)
                                    If blnAlphaNumeric = False Then
                                        If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                                            dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & "(dd-MMM-yyyy)"
                                            dtExceldata.Rows(i).Item("Valid") = False
                                        End If
                                    ElseIf CDate(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) < Now.Date() AndAlso dtExceldata.Rows(i).Item("Valid") = True Then
                                        dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & " should be greater or equal to Current Date"
                                        dtExceldata.Rows(i).Item("Valid") = False
                                    End If
                                End If
                            Else
                                If strColumnName = "Auth_No" OrElse strColumnName = "Consignee" Then
                                    regExString = "^[a-zA-Z0-9]+$"
                                    If Not IsDBNull(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) AndAlso Not dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) = "" Then
                                        blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), regExString)
                                        If blnAlphaNumeric = False Then
                                            If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                                                dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & ". Only Alphabets and Numbers are allowed"
                                            End If
                                            dtExceldata.Rows(i).Item("Valid") = False
                                        End If
                                    End If
                                End If
                            End If
                            Continue For
                        Else
                            If Not pvt_GetISNull(strColumnName, dtExceldata.TableName) AndAlso IsDBNull(dtExceldata.Rows(i).Item(strColumnName)) AndAlso strColumnName.ToUpper <> "MAN_HOUR" AndAlso strColumnName.ToUpper <> "MATERIAL_COST" Then
                                If Not IsDBNull(dtExceldata.Rows(i).Item("Error")) AndAlso dtExceldata.Rows(i).Item("Error") <> "" Then
                                    strErrorMsg = String.Concat(strErrorMsg, ", ")
                                End If
                                strErrorMsg = String.Concat(strErrorMsg, strColumnName & " Required.")
                                dtExceldata.Rows(i).Item("Error") = strErrorMsg
                                dtExceldata.Rows(i).Item("Valid") = False
                                blnValidateCode = False
                                Continue For
                            Else
                                If IsDBNull(dtExceldata.Rows(i).Item("Valid")) Then
                                    dtExceldata.Rows(i).Item("Valid") = True
                                End If
                                If (IsDBNull(dtExceldata.Rows(i).Item(strColumnName)) OrElse dtExceldata.Rows(i).Item(strColumnName).ToString = "") AndAlso (strColumnName.ToUpper = "MAN_HOUR" OrElse strColumnName.ToUpper = "MATERIAL_COST") Then
                                    dtExceldata.Rows(i).Item(strColumnName) = 0
                                End If
                            End If
                        End If
                    End If

                    'Alpha numeric Validation
                    If Not (dtExceldata.Columns(j).ColumnName = "Valid" Or dtExceldata.Columns(j).ColumnName = "Error") Then
                        Dim StrTempColumn As String = pvt_GetColumnName(dtExceldata.TableName, dtExceldata.Columns(j).ColumnName)
                        Dim strColCaption As String = ""
                        If (dsUpload.Tables(dtExceldata.TableName).Columns.Contains(StrTempColumn)) Then
                            strColCaption = dsUpload.Tables(dtExceldata.TableName).Columns(StrTempColumn).Caption
                        End If
                        If strColCaption.Contains("Alpha_Numeric") Then
                            'Alpha Numeric Validation"
                            regExString = "^[a-zA-Z0-9]+$"
                            If Not IsDBNull(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) AndAlso Not dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) = "" Then

                                blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), regExString)
                                If blnAlphaNumeric = False Then
                                    If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                                        dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & ". Only Alphabets and Numbers are allowed"
                                    End If
                                    dtExceldata.Rows(i).Item("Valid") = False
                                End If
                            End If
                        ElseIf strColCaption.Contains("Description_Val") Then
                            'regExString = "^[a-zA-Z0-9-_.'&,\\\/\[\]\(\) ]+$"
                            'regExString = "^[^']+$"
                            'If Not IsDBNull(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) AndAlso Not dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) = "" Then
                            '    blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), regExString)
                            '    If blnAlphaNumeric = False Then
                            '        If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                            '            dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & ". Only Alphabets/Numbers and [-_.'&\/[](),] are allowed."
                            '        End If
                            '        dtExceldata.Rows(i).Item("Valid") = False
                            '    End If
                            'End If
                        ElseIf strColCaption.Contains("Numeric") Then
                            regExString = "^[0-9.,]+$"

                            If dtExceldata.Columns(j).DataType.FullName = GetType(Int64).FullName OrElse _
                                dtExceldata.Columns(j).DataType.FullName = GetType(Int32).FullName Then
                                If Not IsDBNull(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) AndAlso Not dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) = 0 Then
                                    blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), regExString)
                                    If blnAlphaNumeric = False Then
                                        If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                                            dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & ". Only Numbers are allowed."
                                        End If
                                        dtExceldata.Rows(i).Item("Valid") = False
                                    End If
                                End If
                            Else
                                If Not IsDBNull(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) AndAlso Not dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) = "" Then
                                    blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), regExString)
                                    If blnAlphaNumeric = False Then
                                        If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                                            dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & ". Only Numbers are allowed."
                                        End If
                                        dtExceldata.Rows(i).Item("Valid") = False
                                    End If
                                End If
                            End If
                            'For DateTime Validation
                        ElseIf strColCaption.Contains("DateTime") Then
                            regExString = "^((31(?! (FEB|APR|JUN|SEP|NOV)))|((30|29)(?! FEB))|(29(?= FEB (((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))|(0?[1-9])|1\d|2[0-8])-(JAN|FEB|MAR|MAY|APR|JUL|JUN|AUG|OCT|SEP|NOV|DEC)-((1[6-9]|[2-9]\d)\d{2})$"
                            blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName).ToString().ToUpper(), regExString)
                            If blnAlphaNumeric = False Then
                                If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                                    dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & "(dd-MMM-yyyy)"
                                    dtExceldata.Rows(i).Item("Valid") = False
                                End If
                            ElseIf CDate(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) > Now.Date() Then
                                dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & " should not be greater than Current Date"
                                dtExceldata.Rows(i).Item("Valid") = False
                            Else
                                'If (dtExceldata.TableName.ToUpper() = InputRedeliveryData._INWARD_PASS) Then
                                '    Dim dsActivity As DataSet
                                '    Dim blndsValid As Boolean
                                '    Dim objinputRedelivery As New InputRedelivery
                                '    blndsValid = objinputRedelivery.pub_ValidateEquipmentNoByDepotID(dtExceldata.Rows(i).Item(InputRedeliveryData.XL_EQPMNT_NO).ToString(), bv_intDepotID)
                                '    If blndsValid = False Then
                                '        dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & " Duplicate Entry Container No(Not Gateout)"
                                '        dtExceldata.Rows(i).Item("Valid") = False
                                '    Else
                                '        dsActivity = objUpload.pub_fnCheckPreviousActivity(dtExceldata.Rows(i).Item(InputRedeliveryData.XL_LSS_CD).ToString(), dtExceldata.Rows(i).Item(InputRedeliveryData.XL_EQPMNT_NO).ToString(), bv_intDepotID)
                                '        If (dsActivity.Tables(InputRedeliveryData._INWARD_PASS).Rows.Count > 0) Then
                                '            If dsActivity.Tables(InputRedeliveryData._INWARD_PASS).Rows(0).Item(InputRedeliveryData.DT_OF_ACCPTNC) > CDate(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) Then
                                '                dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & " Date of Acceptance should not be greater than Previous Date of Aceptance Date"
                                '                dtExceldata.Rows(i).Item("Valid") = False
                                '            End If
                                '        End If
                                '    End If
                                'End If
                            End If
                            'For Container no Validation
                        ElseIf strColCaption.Contains("ContainerNo") Then
                            'Dim strEqNo As String = String.Empty
                            'Dim dsContainer As New DataSet
                            'Dim blnEqpCheck As Boolean = False

                            'If (dtExceldata.TableName = ReserveBookingData._OUTWARD_PASS) Then
                            '    dsContainer = objUpload.Pub_GateIn_Check(dtExceldata.Rows(i).Item(ReserveBookingData.XL_CSTMR_CD), bv_intDepotID.ToString())
                            '    If (dsContainer.Tables(UploadData._UPLOAD_SCHEMA).Rows.Count > 0) Then
                            '        If Not IsDBNull(dsContainer.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(ReserveBookingData.EQPMNT_NO)) Then
                            '            strEqNo = dsContainer.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(ReserveBookingData.EQPMNT_NO)
                            '        End If
                            '        strEqNo = dtExceldata.Rows(i).Item(ReserveBookingData.XL_EQPMNT_NO)
                            '        For Each dr As DataRow In dsContainer.Tables(UploadData._UPLOAD_SCHEMA).Rows
                            '            If (dr(ReserveBookingData.EQPMNT_NO) = strEqNo) Then
                            '                blnEqpCheck = True
                            '            End If
                            '        Next
                            '        If (blnEqpCheck = False) Then
                            '            dtExceldata.Rows(i).Item("Valid") = False
                            '            dtExceldata.Rows(i).Item("Error") = dtExceldata.Rows(i).Item("Error") & "Equipment_No is not Valid"
                            '        End If
                            '    Else
                            '        dtExceldata.Rows(i).Item("Valid") = False
                            '        dtExceldata.Rows(i).Item("Error") = dtExceldata.Rows(i).Item("Error") & "Equipment_No is not Valid"
                            '    End If
                            'Else
                            '    Dim intCheckDigit As Integer = 0
                            '    regExString = "^[A-Za-z]{4}[0-9]{6,7}$"
                            '    If Not IsDBNull(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) AndAlso Not dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) = "" Then
                            '        blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), regExString)
                            '        If blnAlphaNumeric = False Then
                            '            If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                            '                dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & " must be 11 characters.(4 Alpha characters,6 Digit number,1 Check digit)"
                            '                dtExceldata.Rows(i).Item("Valid") = False
                            '            End If
                            '        Else
                            '            Dim blnValidDuplicate As Boolean
                            '            intCheckDigit = pvt_fnCheckDigit(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName).ToString().Substring(0, 10))
                            '            blnValidDuplicate = objUpload.pub_GetDuplicateEquipment(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), dtExceldata.Rows(i).Item(InputRedeliveryData.XL_AUTH_NO))
                            '            If (dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName).ToString().Length) = 10 Then
                            '                dtExceldata.Rows(i).Item(InputRedeliveryData.EQPMNT_NO) = String.Concat(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), intCheckDigit)
                            '            ElseIf blnValidDuplicate = False Then
                            '                dtExceldata.Rows(i).Item("Error") = "Duplicate " & dtExceldata.Columns(j).ColumnName
                            '                dtExceldata.Rows(i).Item("Valid") = False
                            '            Else
                            '                If (dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)).ToString().Substring(10, 1) <> intCheckDigit Then
                            '                    dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & "Check Digit"
                            '                    dtExceldata.Rows(i).Item("Valid") = False
                            '                End If
                            '            End If
                            '        End If
                            '    End If
                            'End If
                        Else
                            'Alphabets Validation"
                            regExString = "^[a-zA-Z]+$"
                            If Not IsDBNull(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName)) AndAlso Not dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) = "" Then
                                If (dtExceldata.Columns(j).ColumnName = BILLING_TYPE) Then
                                    Dim dtEnum As DataTable
                                    dtEnum = objCommon.GetEnumTableByTypeID(2)
                                    If dtEnum.Rows.Count > 0 Then
                                        Dim drArr As DataRow() = dtEnum.Select("ENM_CD='" & dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName) & "'")
                                        If Not drArr.Length > 0 Then
                                            dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & "Only LOCAL (or) CENTRALISED is Allowed"
                                            dtExceldata.Rows(i).Item("Valid") = False
                                        End If
                                    End If
                                End If
                                blnAlphaNumeric = Regex.IsMatch(dtExceldata.Rows(i).Item(dtExceldata.Columns(j).ColumnName), regExString)
                                If blnAlphaNumeric = False Then
                                    If IsDBNull(dtExceldata.Rows(i).Item("Error")) Then
                                        dtExceldata.Rows(i).Item("Error") = "Invalid " & dtExceldata.Columns(j).ColumnName & ". Only Alphabets are allowed."
                                    End If
                                    dtExceldata.Rows(i).Item("Valid") = False
                                End If
                            End If

                        End If
                    End If

                Next

                'For Foreign Key Validation 
                dsUpload = CType(RetrieveData(UPLOAD), UploadDataSet)

                'For Foreign Key Validation
                If dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(UploadData.RF_OBJCT_NAM) <> dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(UploadData.UPLD_TBL_NAM) Then

                    Dim drsFK() As DataRow = dsUpload.Tables(UploadData._UPLOAD_KEYS).Select(String.Concat(UploadData.KY_TYP, "='FOREIGN'"))
                    If drsFK.Length > 0 Then
                        For Each drFK As DataRow In drsFK
                            Dim strRefTblName As String = drFK.Item(UploadData.RF_TBL_NAM)
                            Dim strRefColName As String = drFK.Item(UploadData.RF_CLMN_NAM)
                            If Not dtExceldata.Columns.Contains(drFK.Item(UploadData.KY_NAM)) Then
                                dtExceldata.Columns.Add(drFK.Item(UploadData.KY_NAM), GetType(Long))
                            End If
                            Dim strTempColumn As String = pvt_GetColumnCaptionName(dtExceldata.TableName, drFK.Item(UploadData.KY_NAM))
                            Dim lngFK As Int64 = Nothing
                            If Not IsDBNull(dtExceldata.Rows(i).Item(strTempColumn)) Then 'AndAlso Not dtExceldata.Rows(i).Item(strTempColumn) = "" Then
                                If (strTempColumn = "Sub_Item_Code" And strRefTblName = "SUB_ITEM") Then
                                    Dim intItemCode As Integer = dtExceldata.Rows(i).Item(UploadData.ITM_ID)
                                    lngFK = pvt_CheckForeignkeyForSubItem(drFK.Item(UploadData.KY_NAM), strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intItemCode)
                                ElseIf dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(UploadData.UPLD_TBL_NAM) = "TARIFF_CODE_DETAIL" Then
                                    Select Case strTempColumn
                                        Case "Component"
                                            If Not IsDBNull(dtExceldata.Rows(i).Item("TRFF_CD_DTL_LCTN_ID")) Then
                                                If dtExceldata.Rows(i).Item(strTempColumn).ToString.Trim = "" Then
                                                    lngFK = 4
                                                Else
                                                    lngFK = pvt_CheckForeignkeyForSubItem("SB_ITM_ID", strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), dtExceldata.Rows(i).Item("TRFF_CD_DTL_LCTN_ID"))
                                                End If
                                            End If
                                        Case "Damage"
                                            If Not dtExceldata.Rows(i).Item(strTempColumn).ToString.Trim = "" Then
                                                lngFK = pvt_CheckForeignkey("DMG_ID", strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                            End If
                                        Case "Location"
                                            If Not dtExceldata.Rows(i).Item(strTempColumn).ToString.Trim = "" Then
                                                lngFK = pvt_CheckForeignkey("ITM_ID", strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                            End If
                                        Case "Repair"
                                            If Not dtExceldata.Rows(i).Item(strTempColumn).ToString.Trim = "" Then
                                                lngFK = pvt_CheckForeignkey("RPR_ID", strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                            End If
                                        Case "Material"
                                            If Not dtExceldata.Rows(i).Item(strTempColumn).ToString.Trim = "" Then
                                                lngFK = pvt_CheckForeignkey("MTRL_ID", strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                            Else
                                                lngFK = -1
                                            End If
                                        Case Else
                                            lngFK = pvt_CheckForeignkey(drFK.Item(UploadData.KY_NAM), strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                    End Select
                                ElseIf dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(UploadData.UPLD_TBL_NAM) = "PRE_ADVICE" Then
                                    Select Case strTempColumn
                                        Case "Equipment_Code"
                                            lngFK = pvt_CheckForeignkey("EQPMNT_TYP_ID", "EQPMNT_TYP_CD='" & dtExceldata.Rows(i).Item("Equipment_Type") & "' AND EQPMNT_CD_CD ", strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                        Case "Equipment_Type"
                                            str_054Value = objCommon.GetConfigSetting("054", bln_054EnabledBit)
                                            If str_054Value.ToLower = "true" Then
                                                lngFK = pvt_CheckForeignkey("EQPMNT_TYP_ID", "EQPMNT_TYP_CD='" & dtExceldata.Rows(i).Item("Equipment_Type") & "' AND EQPMNT_CD_CD ", strRefTblName, dtExceldata.Rows(i).Item("Equipment_Code"), intHeadQuarterID)
                                            Else
                                                lngFK = pvt_CheckForeignkey(drFK.Item(UploadData.KY_NAM), strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                            End If
                                        Case "Previous_Cargo"
                                            If IsDBNull(dtExceldata.Rows(i).Item(strTempColumn)) OrElse dtExceldata.Rows(i).Item(strTempColumn) = "" Then
                                                lngFK = 999
                                            Else
                                                lngFK = pvt_CheckForeignkey(drFK.Item(UploadData.KY_NAM), strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                            End If
                                        Case Else
                                            lngFK = pvt_CheckForeignkey(drFK.Item(UploadData.KY_NAM), strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                    End Select
                                Else

                                    lngFK = pvt_CheckForeignkey(drFK.Item(UploadData.KY_NAM), strRefColName, strRefTblName, dtExceldata.Rows(i).Item(strTempColumn), intHeadQuarterID)
                                End If

                                If lngFK <= 0 AndAlso dtExceldata.Rows(i).Item("Valid") = True Then
                                    dtExceldata.Rows(i).Item("Error") = strTempColumn & " Not Exist"
                                    dtExceldata.Rows(i).Item("Valid") = False
                                End If
                                If strTempColumn = "Previous_Cargo" AndAlso lngFK = 999 Then
                                    lngFK = -1
                                End If
                            End If

                            If lngFK > 0 Then
                                If IsDBNull(dtExceldata.Rows(i).Item("Valid")) Then
                                    dtExceldata.Rows(i).Item("Valid") = True
                                End If
                                dtExceldata.Rows(i).Item(drFK.Item(UploadData.KY_NAM)) = lngFK
                            End If
                        Next
                    End If
                End If
            Next
            'For Pre-Advice
            Dim drsUK() As DataRow = dsUpload.Tables(UploadData._UPLOAD_KEYS).Select(String.Concat(UploadData.KY_TYP, "='UNIQUE'"))
            If drsUK.Length > 0 AndAlso dsUpload.Tables(UploadData._UPLOAD_SCHEMA).Rows(0).Item(UploadData.UPLD_TBL_NAM) = "PRE_ADVICE" Then
                Dim strColumnName As String = ""
                For Each drUniqueKey As DataRow In drsUK
                    strColumnName = drUniqueKey.Item(UploadData.CLMN_NAM)
                Next
                If strColumnName = "EQPMNT_NO" Then
                    For i = 0 To dtExceldata.Rows.Count - 1
                        If dtExceldata.Rows(i).Item("Valid") = True Then
                            Dim strEquipNo = dtExceldata.Rows(i).Item("EQPMNT_NO").ToString.Trim
                            Dim blnCustChkDigtValid As Boolean
                            Dim blnContainerNoValid As Boolean
                            Dim brStrBuilder As String = ""
                            blnCustChkDigtValid = (objUpload.pub_GetCustomerByCustmrId(dtExceldata.Rows(i).Item("CSTMR_ID"), intHeadQuarterID)).Tables("Customer").Rows(0).Item("CHK_DGT_VLDTN_BT")
                            blnContainerNoValid = CommonWeb.pub_ValidateContainerNo(strEquipNo, blnCustChkDigtValid, brStrBuilder)
                            If Not blnContainerNoValid Then
                                dtExceldata.Rows(i).Item("Error") = "Invalid Equipment number"
                                dtExceldata.Rows(i).Item("Valid") = False
                                Continue For
                            Else
                                Dim objUploads As New Uploads
                                ' blnContainerNoValid = objUploads.pub_GateInCheckByEquipNo(strCodeColumnValue, objCommon.GetDepotID())
                                Dim objPreAdvice As New PreAdvice
                                blnContainerNoValid = objPreAdvice.pub_ValidateEquipmentNoByDepotID(strEquipNo, CInt(objCommon.GetDepotID()), dtExceldata.Rows(i).Item("Customer_Code"))
                                If blnContainerNoValid = True Then
                                    blnContainerNoValid = objPreAdvice.pub_ValidateStatusOfEquipment(strEquipNo, CInt(objCommon.GetDepotID()))
                                End If
                                dtExceldata.Rows(i).Item("Valid") = blnContainerNoValid
                                If blnContainerNoValid = False Then
                                    dtExceldata.Rows(i).Item("Error") = "Equipment already been Gated In"
                                    Continue For
                                End If
                            End If
                        End If
                    Next
                End If
            End If

            Return dtExceldata

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_ValidateUniqueCode"
    Private Function pvt_ValidateUniqueCode(ByVal strCodeColumnName As String, ByRef dtExceldata As DataTable, ByVal dtSource As DataTable) As DataTable
        Try
            Dim blnValidateCode As Boolean = True
            Dim i As Integer

            For i = 0 To dtExceldata.Rows.Count - 1
                Dim strCodeColumnValue As String
                Dim strDescriptionColumnValue As String
                If IsDBNull(dtExceldata.Rows(i).Item(strCodeColumnName)) Then
                    dtExceldata.Rows(i).Item("Error") = "REJRECORD"
                    dtExceldata.Rows(i).Item("Valid") = False
                    Continue For
                Else
                    If (dtExceldata.TableName.ToUpper = "PRE_ADVICE") Then
                        strCodeColumnValue = dtExceldata.Rows(i).Item(strCodeColumnName)
                    Else
                        strCodeColumnValue = dtExceldata.Rows(i).Item(strCodeColumnName)
                        strDescriptionColumnValue = dtExceldata.Rows(i).Item("Description")
                    End If
                End If
                'Dim blnContainerNoValid As Boolean
                'If strCodeColumnName.ToUpper = "EQPMNT_NO" Then
                '    Dim blnCustChkDigtValid As Boolean
                '    Dim brStrBuilder As String = ""
                '    blnContainerNoValid = CommonWeb.pub_ValidateContainerNo(strCodeColumnValue, blnCustChkDigtValid, brStrBuilder)
                '    If Not blnContainerNoValid Then
                '        dtExceldata.Rows(i).Item("Error") = "Invalid Equipment number"
                '        dtExceldata.Rows(i).Item("Valid") = False
                '        Continue For
                '    Else
                '        Dim objUploads As New Uploads
                '        Dim objCommon As New CommonData
                '        ' blnContainerNoValid = objUploads.pub_GateInCheckByEquipNo(strCodeColumnValue, objCommon.GetDepotID())
                '        Dim objPreAdvice As New PreAdvice
                '        blnContainerNoValid = objPreAdvice.pub_ValidateEquipmentNoByDepotID(strCodeColumnValue, CInt(objCommon.GetDepotID()), dtExceldata.Rows(i).Item("Customer_Code"))
                '        dtExceldata.Rows(i).Item("Valid") = blnContainerNoValid
                '        If blnContainerNoValid = False Then
                '            dtExceldata.Rows(i).Item("Error") = "Equipment already been Gated In"
                '            Continue For
                '        End If
                '    End If
                'End If
                Dim drsSource As DataRow()

                If (strCodeColumnName.ToLower() = "code" OrElse strCodeColumnName.ToLower() = "tariff_code" OrElse strCodeColumnName.ToLower() = "eqpmnt_no") AndAlso strCodeColumnValue = "" Then
                    dtExceldata.Rows(i).Item("Error") = "REJRECORD"
                    dtExceldata.Rows(i).Item("Valid") = False
                    Continue For
                End If
                'Unique Key Validation"
                drsSource = dtSource.Select(String.Concat(strCodeColumnName, "='", strCodeColumnValue, "'"), "")
                If drsSource.Length > 0 Then
                    If strCodeColumnName = "EQPMNT_NO" Then
                        dtExceldata.Rows(i).Item("Error") = "Equipment_No already exists."
                        dtExceldata.Rows(i).Item("Valid") = False
                        blnValidateCode = False
                    Else
                        dtExceldata.Rows(i).Item("Error") = strCodeColumnName & " already exists."
                        dtExceldata.Rows(i).Item("Valid") = False
                        blnValidateCode = False
                    End If
                Else
                    blnValidateCode = True
                End If
              
                'If (dtExceldata.TableName = ReserveBookingData._OUTWARD_PASS Or dtExceldata.TableName = InputRedeliveryData._INWARD_PASS) Then
                '    If strCodeColumnName.ToString() = InputRedeliveryData.XL_EQPMNT_NO AndAlso strCodeColumnValue = "" Then
                '        dtExceldata.Rows(i).Item("Error") = "REJRECORD"
                '        dtExceldata.Rows(i).Item("Valid") = False
                '        Continue For
                '    End If
                'Else

                'End If
                If Not (dtExceldata.TableName.ToUpper = "TARIFF") Then
                    'Unique Key Validation"
                    drsSource = dtSource.Select(String.Concat(strCodeColumnName, "='", strCodeColumnValue, "'"), "")
                    If drsSource.Length > 0 Then
                        If strCodeColumnName = "EQPMNT_NO" Then
                            dtExceldata.Rows(i).Item("Error") = "Equipment_No already exists."
                            dtExceldata.Rows(i).Item("Valid") = False
                            blnValidateCode = False
                        Else
                            dtExceldata.Rows(i).Item("Error") = strCodeColumnName & " already exists."
                            dtExceldata.Rows(i).Item("Valid") = False
                            blnValidateCode = False
                        End If
                    Else
                        blnValidateCode = True
                    End If

                    If blnValidateCode Then
                        If IsDBNull(dtExceldata.Rows(i).Item("Valid")) Then
                            dtExceldata.Rows(i).Item("Valid") = True
                        End If
                    End If
                Else
                    If IsDBNull(dtExceldata.Rows(i).Item("Valid")) Then
                        dtExceldata.Rows(i).Item("Valid") = True
                    End If
                End If
            Next

            Dim dtTemp As New DataTable
            'Dim drrow() As DataRow = dtUploadData.Select("Error<>'REJRECORD'")
            dtTemp = dtExceldata.Clone()
            For Each dr As DataRow In dtExceldata.Rows()
                If IsDBNull(dr.Item("Error")) Then
                    dtTemp.ImportRow(dr)
                Else
                    If Not dr.Item("Error") = "REJRECORD" Then
                        dtTemp.ImportRow(dr)
                    End If
                End If
            Next


            dtExceldata = dtTemp

            'For Checking duplicates in excel sheet itself

            Dim hTable As Hashtable = New Hashtable()

            For Each drow As DataRow In dtExceldata.Rows
                If hTable.Contains(drow.Item(strCodeColumnName)) AndAlso Not IsDBNull(drow.Item(strCodeColumnName)) AndAlso Not drow.Item(strCodeColumnName) = "" Then
                    If strCodeColumnName = "EQPMNT_NO" Then
                        drow.Item("Error") = "Equipment_No already exists in Sheet."
                        drow.Item("Valid") = False
                    Else
                        drow.Item("Error") = strCodeColumnName & " already exists in Sheet."
                        drow.Item("Valid") = False
                    End If
                Else
                    If Not IsDBNull(drow.Item(strCodeColumnName)) Then
                        If Not drow.Item(strCodeColumnName) = "" Then
                            hTable.Add(drow.Item(strCodeColumnName), String.Empty)
                        End If
                    End If
                End If
            Next

            'End

            Return dtExceldata
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_GetISNull"
    'Check Required Field Error
    Private Function pvt_GetISNull(ByVal bv_strColumnName As String, ByVal bv_strTableName As String) As Boolean
        Try

            Dim blnNull As Boolean = False

            Dim strTempColumnname As String = pvt_GetColumnName(bv_strTableName, bv_strColumnName)

            'dsUpload = CType(RetrieveData(UPLOAD), UploadDataSet)

            'Dim drsUpload As DataRow() = dsUpload.Tables(UploadData._UPLOAD_SCHEMA_DETAIL).Select(String.Concat(UploadData.CLMN_NAM, "='", strTempColumnname, "'"))


            'If drsUpload(0).Item(UploadData.IS_NLLBLE) = "YES" Then
            If dsUpload.Tables(bv_strTableName).Columns(strTempColumnname).Caption.Contains("NotNull") Then
                blnNull = False
            Else
                blnNull = True
            End If

            Return blnNull
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try

    End Function
#End Region

#Region "pvt_ValidateMaxLength"
    Public Function pvt_ValidateMaxLength(ByRef dtExceldata As DataTable, ByVal dtschema As DataTable, ByVal dtBulkData As DataTable, ByVal strTableName As String) As DataTable
        Try
            Dim intMaxlength As Integer
            Dim strExtractedTableName As String
            Dim objcommonUI As New CommonUIs
            strExtractedTableName = objcommonUI.ExtractConsontant(strTableName, False)

            For Each dc As DataColumn In dtExceldata.Columns
                If Not (dc.ColumnName = "Valid" Or dc.ColumnName = "Error") AndAlso dc.DataType Is GetType(String) Then

                    intMaxlength = pvt_GetMaxLength(dtschema, dc.ColumnName, dtExceldata.TableName, strExtractedTableName)

                    For i As Integer = 0 To dtExceldata.Rows.Count - 1
                        If intMaxlength > 0 AndAlso intMaxlength < dtExceldata.Rows(i).Item(dc).ToString.Length Then
                            If dc.ColumnName = "EQPMNT_NO" Then
                                dtExceldata.Rows(i).Item("Error") = "Equipment_No Exceed " & intMaxlength & " Characters"
                                dtExceldata.Rows(i).Item("Valid") = False
                            Else
                                dtExceldata.Rows(i).Item("Error") = dc.ColumnName & " Exceed " & intMaxlength & " Characters"
                                dtExceldata.Rows(i).Item("Valid") = False
                            End If
                        End If
                    Next
                End If
            Next

            Return dtExceldata
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_GetSchema"
    Private Function pvt_GetSchema(ByVal bv_strTablename As String) As DataTable
        Try

            Dim dtSchema As New DataTable(UploadData._UPLOAD_SCHEMA_DETAIL)
            Dim objUpload As New Upload

            dsUpload.Tables(UploadData._UPLOAD_SCHEMA_DETAIL).Clear()
            dsUpload.Tables(UploadData._UPLOAD_SCHEMA_DETAIL).Merge(objUpload.pub_GetUPLOAD_SCHEMADETAILBySCHM_NAM(bv_strTablename).Tables(UploadData._UPLOAD_SCHEMA_DETAIL))
            CacheData(UPLOAD, dsUpload)
            dtSchema = dsUpload.Tables(UploadData._UPLOAD_SCHEMA_DETAIL)

            Return dtSchema
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_GetMaxLength"
    Private Function pvt_GetMaxLength(ByVal bv_dtSchema As DataTable, ByVal bv_strColumnname As String, ByVal bv_strTableName As String, ByVal bv_strExtractName As String) As Integer
        Try
            Dim length As Integer
            Dim strTempColumnname As String = pvt_GetColumnName(bv_strTableName, bv_strColumnname)

            For Each dr As DataRow In bv_dtSchema.Rows
                If Not IsDBNull(dr.Item(UploadData.CHRCTR_MX_LNGTH)) Then
                    If dr.Item(UploadData.DT_TYP).ToString.ToLower = "varchar" Then
                        If strTempColumnname = "Code" Then
                            strTempColumnname = String.Concat(bv_strExtractName, _CD)
                        ElseIf strTempColumnname = "Description" Then
                            strTempColumnname = String.Concat(bv_strExtractName, _DSCRPTN_VC)
                        End If

                        If dr.Item(UploadData.CLMN_NAM).ToString.ToLower = strTempColumnname.ToLower() Then
                            length = dr.Item(UploadData.CHRCTR_MX_LNGTH)
                            Exit For
                        End If
                    End If
                End If
            Next
            Return length
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_GetColumnName"  'This Method is used to get the Column name; Input Caption
    Private Function pvt_GetColumnName(ByVal bv_strTablename As String, ByVal bv_strColumnName As String) As String
        Try
            Dim strTempColumnname As String = Nothing

            For Each dc As DataColumn In dsUpload.Tables(bv_strTablename).Columns
                Dim length As Integer = dc.Caption.IndexOf(";")
                Dim strTemp As String = ""
                If (length > 0) Then
                    strTemp = dc.Caption.Substring(0, length)
                ElseIf dc.Caption <> "" AndAlso length < 0 Then
                    strTemp = dc.Caption
                End If
                If (strTemp.ToUpper() = bv_strColumnName.ToUpper()) Then
                    strTempColumnname = dc.ColumnName
                    Exit For
                End If
            Next
            If strTempColumnname = "" Then
                strTempColumnname = bv_strColumnName
            End If
            Return strTempColumnname
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_GetColumnCaptionName"  'This Method is used to get the Caption; Input Column name
    Private Function pvt_GetColumnCaptionName(ByVal bv_strTablename As String, ByVal bv_strColumnName As String) As String
        Try
            Dim strTempColumnname As String = Nothing

            If dsUpload.Tables(bv_strTablename).Columns.Contains(bv_strColumnName) Then
                strTempColumnname = dsUpload.Tables(bv_strTablename).Columns(bv_strColumnName).Caption
            End If

            Dim length As Integer = strTempColumnname.IndexOf(";")

            If (length > 0) Then
                strTempColumnname = strTempColumnname.Substring(0, length)
            End If

            If strTempColumnname = "" Then
                strTempColumnname = bv_strColumnName
            End If
            Return strTempColumnname
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_InsertBulkData"
    Private Function pvt_InsertBulkData(ByVal dtExceldata As DataTable, ByVal bv_strTablename As String, _
                                        ByVal bv_intDepotID As Int32, ByVal bv_strUserName As String, _
                                        ByVal bv_strPageType As String) As Boolean

        Dim dtFKColumns As New DataTable
        Dim strinsertBulkQry, strColumnname As String
        Dim objUpload As New Upload
        Dim objCommondata As New CommonData
        Dim intHeadQuarterID As Integer
        Dim ds As New DataSet
        Try
            If objCommondata.GetMultiLocationSupportConfig.ToLower = "true" Then
                intHeadQuarterID = objCommondata.GetHeadQuarterID()
            Else
                intHeadQuarterID = bv_intDepotID
            End If
            ' Finding ID COLOUMN
            strColumnname = pvt_GetIdColumn(bv_strTablename)

            dtExceldata.Columns.Add(strColumnname, GetType(Long))
            'If Not bv_strTablename.ToUpper() = "TARIFF" AndAlso Not bv_strTablename.ToUpper() = InputRedeliveryData._INWARD_PASS AndAlso Not bv_strTablename.ToUpper() = "OUTWARD_PASS" Then
            '    dtExceldata.Columns.Add(UploadData.CRTD_BY, GetType(String))
            '    dtExceldata.Columns.Add(UploadData.CRTD_DT, GetType(Date))
            'End If

            'If Not bv_strTablename.ToUpper() = InputRedeliveryData._INWARD_PASS AndAlso Not bv_strTablename.ToUpper() = "OUTWARD_PASS" Then
            If bv_strTablename.ToUpper = "TARIFF_CODE_DETAIL" Then
                dtExceldata.Columns.Add("TRFF_CD_DTL_CRTD_BY", GetType(String))
                dtExceldata.Columns.Add("TRFF_CD_DTL_CRTD_DT", GetType(Date))
                dtExceldata.Columns.Add("TRFF_CD_DTL_MDFD_BY", GetType(String))
                dtExceldata.Columns.Add("TRFF_CD_DTL_MDFD_DT", GetType(Date))
                dtExceldata.Columns.Add("TRFF_CD_DTL_ACTV_BT", GetType(Boolean))
            Else
                dtExceldata.Columns.Add(UploadData.CRTD_BY, GetType(String))
                dtExceldata.Columns.Add(UploadData.CRTD_DT, GetType(Date))
                dtExceldata.Columns.Add(UploadData.MDFD_BY, GetType(String))
                dtExceldata.Columns.Add(UploadData.MDFD_DT, GetType(Date))
                dtExceldata.Columns.Add(UploadData.ACTV_BT, GetType(Boolean))
            End If

            'End If
            dtExceldata.Columns.Add(UploadData.DPT_ID, GetType(Integer))
            If bv_strPageType = "CodeMaster" Then
                Dim ObjDao As New DataObjects
                strinsertBulkQry = ObjDao.GetInsertQuery(bv_strTablename)
            Else
                strinsertBulkQry = pvt_BuildInsertBulkQuery(bv_strTablename)
            End If
            Dim strUserName As String = bv_strUserName
            Dim datCurrentDate As Date = objCommondata.GetCurrentDate()

            If bv_strTablename.ToUpper = "TARIFF_CODE_DETAIL" Then
                For Each dr As DataRow In dtExceldata.Rows
                    With dr
                        .Item("TRFF_CD_DTL_CRTD_BY") = strUserName
                        .Item("TRFF_CD_DTL_CRTD_DT") = datCurrentDate
                        .Item("TRFF_CD_DTL_MDFD_BY") = strUserName
                        .Item("TRFF_CD_DTL_MDFD_DT") = datCurrentDate
                        .Item("TRFF_CD_DTL_ACTV_BT") = True
                        .Item(UploadData.DPT_ID) = intHeadQuarterID
                    End With
                Next
            ElseIf bv_strTablename = "PRE_ADVICE" Then
                For Each dr As DataRow In dtExceldata.Rows
                    With dr
                        .Item(UploadData.CRTD_BY) = strUserName
                        .Item(UploadData.CRTD_DT) = datCurrentDate
                        .Item(UploadData.MDFD_BY) = strUserName
                        .Item(UploadData.MDFD_DT) = datCurrentDate
                        .Item(UploadData.ACTV_BT) = True
                        .Item(UploadData.DPT_ID) = bv_intDepotID
                    End With
                Next
            Else
                For Each dr As DataRow In dtExceldata.Rows
                    With dr
                        .Item(UploadData.CRTD_BY) = strUserName
                        .Item(UploadData.CRTD_DT) = datCurrentDate
                        .Item(UploadData.MDFD_BY) = strUserName
                        .Item(UploadData.MDFD_DT) = datCurrentDate
                        .Item(UploadData.ACTV_BT) = True
                        .Item(UploadData.DPT_ID) = intHeadQuarterID
                    End With
                Next
            End If

            dtExceldata = pvt_CopyDataTablevalue(dtExceldata, bv_strTablename, strColumnname)

            dsUpload = CType(RetrieveData(UPLOAD), UploadDataSet)
            If bv_strPageType = "CodeMaster" Then
                Dim strExtractedTableName As String
                Dim objcommonUI As New CommonUIs
                strExtractedTableName = objcommonUI.ExtractConsontant(bv_strTablename, False)
                dtExceldata.Columns.Add(String.Concat(strExtractedTableName, _CD), GetType(System.String))
                dtExceldata.Columns.Add(String.Concat(strExtractedTableName, _DSCRPTN_VC), GetType(System.String))

                For Each dr As DataRow In dtExceldata.Rows
                    dr.Item(String.Concat(strExtractedTableName, _CD)) = dr.Item("Code")
                    dr.Item(String.Concat(strExtractedTableName, _DSCRPTN_VC)) = dr.Item("Description")
                Next
            End If

            dsUpload.Tables(bv_strTablename).Clear()
            dsUpload.Tables(bv_strTablename).Merge(dtExceldata)

            Dim lngMax As Long = objUpload.pvt_CreateUpload(strinsertBulkQry, dsUpload, bv_strTablename, strColumnname, bv_strPageType)

            CacheData(UPLOAD, dsUpload)


            Return True
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
            Return False
        End Try
    End Function
#End Region

#Region "pvt_GetIdColumn"
    Public Function pvt_GetIdColumn(ByVal bv_strTablename As String) As String
        Try

            Dim stridColumnname As String
            Dim objUpload As New Upload
            stridColumnname = objUpload.pvt_GetIdColumn(bv_strTablename)
            Return stridColumnname
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_BuildInsertBulkQuery"
    Private Function pvt_BuildInsertBulkQuery(ByVal bv_strTablename As String) As String
        Try
            Dim strQuery As String = Nothing
            Dim strquery1 As String = Nothing
            Dim strquery2 As String = Nothing
            strQuery = String.Concat("Insert into ", bv_strTablename, "(")
            For Each dc As DataColumn In dsUpload.Tables(bv_strTablename).Columns
                strquery1 = String.Concat(strquery1, dc.ColumnName, ",")
            Next
            strquery1 = pvt_RemoveComma(strquery1)
            strQuery = String.Concat(strQuery, strquery1, ")values(")
            For Each dc As DataColumn In dsUpload.Tables(bv_strTablename).Columns
                strquery2 = String.Concat(strquery2, "@", dc.ColumnName, ",")
            Next
            strquery2 = pvt_RemoveComma(strquery2)
            strQuery = String.Concat(strQuery, strquery2, ")")
            Return strQuery
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_RemoveComma"
    Private Function pvt_RemoveComma(ByVal strquery As String) As String
        Try
            If strquery.EndsWith(",") Then
                Return strquery.Remove(strquery.Length - 1, 1)
            End If
            Return strquery
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_CopyDataTablevalue"
    Private Function pvt_CopyDataTablevalue(ByVal bv_dtExceldata As DataTable, ByVal bv_strTableName As String, ByVal bv_strColumnName As String) As DataTable
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Column", GetType(String))
            dt.Columns.Add("Datatype", GetType(System.Type))

            For Each dc As DataColumn In bv_dtExceldata.Columns
                Dim strTemp As String
                strTemp = pvt_GetColumnName(bv_strTableName, dc.ColumnName)
                'strFK = pvt_GetColumnCaptionName(bv_strTableName, dc.ColumnName)
                If strTemp.ToUpper() <> dc.ColumnName.ToUpper() Then
                    Dim dr As DataRow = dt.NewRow()
                    dr.Item("Column") = strTemp.ToUpper()
                    dr.Item("Datatype") = dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType   'dc.DataType
                    dt.Rows.Add(dr)
                End If
            Next

            For Each dr As DataRow In dt.Rows
                With dr
                    If Not bv_dtExceldata.Columns.Contains(.Item("Column")) Then
                        bv_dtExceldata.Columns.Add(.Item("Column"), .Item("Datatype"))
                    End If
                End With
            Next

            Dim len As Int64 = 1
            For Each dr As DataRow In bv_dtExceldata.Rows
                For Each dc As DataColumn In bv_dtExceldata.Columns
                    Dim strTemp As String = pvt_GetColumnName(bv_strTableName, dc.ColumnName)
                    Dim drs() As DataRow = dsUpload.Tables(UploadData._UPLOAD_KEYS).Select(String.Concat(UploadData.KY_TYP, "='FOREIGN' AND ", UploadData.KY_NAM, "='", strTemp, "'"))

                    If strTemp <> dc.ColumnName AndAlso drs.Length = 0 Then
                        If Not (dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(DateTime).FullName OrElse dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(Boolean).FullName) Then
                            If Not IsDBNull(dr.Item(dc.ColumnName)) Then
                                If dr.Item(dc.ColumnName) = "" AndAlso Not dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(String).FullName Then
                                    dr.Item(dc.ColumnName) = Nothing
                                End If
                                If bv_strTableName = "TARIFF_CODE_DETAIL" AndAlso dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(Decimal).FullName AndAlso dr.Item("Valid") Then
                                    If dr.Item(dc.ColumnName) > 9999999.99 Then
                                        dr.Item("Valid") = False
                                        dr.Item("Error") = "Range must be from 0.01 to 9999999.99"
                                    Else
                                        dr.Item(strTemp) = CommonWeb.iDbl(dr.Item(dc.ColumnName))
                                    End If
                                End If
                            End If
                        End If
                        If dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(Int32).FullName Then
                            If ((dr.Item(dc.ColumnName).GetType = GetType(Int32))) Then
                                dr.Item(strTemp) = CommonWeb.iInt(dr.Item(dc.ColumnName))
                            End If
                        ElseIf dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(Double).FullName Then
                            If ((dr.Item(dc.ColumnName).GetType = GetType(Double))) Then
                                dr.Item(strTemp) = CommonWeb.iDbl(dr.Item(dc.ColumnName))
                            End If
                        ElseIf dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(Decimal).FullName Then
                            If ((dr.Item(dc.ColumnName).GetType = GetType(Decimal))) Then
                                dr.Item(strTemp) = CommonWeb.iDec(dr.Item(dc.ColumnName))
                            End If
                        ElseIf dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(DateTime).FullName Then
                            'If ((dr.Item(dc.ColumnName).GetType = GetType(DateTime))) Then
                            '    dr.Item(strTemp) = CommonWeb.iDat(dr.Item(dc.ColumnName))
                            'End If
                            dr.Item(strTemp) = CDate(dr.Item(dc.ColumnName))
                        ElseIf dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(Int64).FullName Then
                            If ((dr.Item(dc.ColumnName).GetType = GetType(Int64))) Then
                                dr.Item(strTemp) = CommonWeb.iLng(dr.Item(dc.ColumnName))
                            End If
                        ElseIf dsUpload.Tables(bv_strTableName).Columns(strTemp).DataType.FullName = GetType(Boolean).FullName Then
                            'If ((dr.Item(dc.ColumnName).GetType = GetType(Boolean))) Then
                            '    'dr.Item(strTemp) = CBool(dr.Item(dc.ColumnName))
                            'End If
                            dr.Item(strTemp) = CBool(dr.Item(dc.ColumnName))
                        Else
                            dr.Item(strTemp) = dr.Item(dc.ColumnName)
                        End If
                    End If

                    If bv_strColumnName = dc.ColumnName Then
                        dr.Item(bv_strColumnName) = len
                        len = len + 1
                    End If

                Next
            Next

            Return bv_dtExceldata
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "ifgUploadStatus_ClientBind"
    Protected Sub ifgUploadStatus_ClientBind(ByVal sender As Object, ByVal e As iInterchange.WebControls.v4.Data.iFlexGridClientBindEventArgs) Handles ifgUploadStatus.ClientBind
        Try
            Dim dtUploadData As New DataTable
            dsUpload = CType(RetrieveData(UPLOAD), UploadDataSet)
            Dim strTableName As String = CStr(e.Parameters("TableName"))
            Dim strIdColumn As String = CStr(e.Parameters("IdColumn"))

            Dim strDisplayType As String = Nothing
            If Not e.Parameters("DisplayType") Is Nothing Then
                strDisplayType = CStr(e.Parameters("DisplayType"))
            End If

            If dtUploadData.Rows.Count > 0 Then
                dtUploadData.Clear()
            End If

            If Not e.Parameters("SessionChk") Is Nothing AndAlso strTableName.ToUpper = "TARIFF" Then
                If (e.Parameters("SessionChk").ToString() = "Yes") Then
                    dtUploadData = CType(RetrieveData("UploadData"), DataTable).Copy()
                Else
                    dtUploadData = dsUpload.Tables(strTableName).Copy()
                End If
            Else
                dtUploadData = dsUpload.Tables(strTableName).Copy()
            End If

            If Not dtUploadData.Rows.Count > 0 Then
                e.OutputParameters.Add("norecordsfound", "True")
            Else
                If strTableName.ToUpper = "TARIFF" Then
                    Dim drsValid = New DataView(dtUploadData)

                    If strDisplayType = "Valid" Then
                        drsValid.RowFilter = "Valid=True"
                    Else
                        drsValid.RowFilter = "Valid=False"
                    End If
                    If Not drsValid.Count > 0 Then
                        e.OutputParameters.Add("norecordsfound", "True")
                        If strDisplayType = "Valid" Then
                            e.OutputParameters.Add("lblData", "No Valid Data Found.")
                        Else
                            e.OutputParameters.Add("lblData", "No InValid Data Found.")
                        End If
                    Else
                        If strDisplayType = "Valid" Then
                            e.OutputParameters.Add("lblData", "Showing Valid Data.")
                        Else
                            e.OutputParameters.Add("lblData", "Showing InValid Data.")
                        End If
                    End If
                End If
            End If

            Dim dtExcelData As DataTable = CType(RetrieveData(EXCELDATA), DataTable)

            pvt_BindGrid(dtExcelData, dtUploadData, strIdColumn, strTableName, strDisplayType)
            CacheData("UploadData", dtUploadData)
            CacheData("IdColumn", strIdColumn)
            CacheData("tablename", strTableName)
            CacheData("DisplayType", strDisplayType)
            'Remove Added Columns
            For Each dc As DataColumn In dtExcelData.Columns
                If dsUpload.Tables(strTableName).Columns.Contains(dc.ColumnName) Then
                    If strTableName.ToUpper = "TARIFF" Then
                        If Not (dc.ColumnName.ToUpper = "EQPMNT_SZ" Or dc.ColumnName.ToUpper = "EQPMNT_TYP" Or dc.ColumnName.ToUpper = "CSTMR_ID") Then  'only For Tariff Table Due to Size and type updation
                            dsUpload.Tables(strTableName).Columns.Remove(dc.ColumnName)
                        End If
                    Else
                        dsUpload.Tables(strTableName).Columns.Remove(dc.ColumnName)
                    End If
                End If
            Next
            dsUpload.Tables(strTableName).Clear()
            CacheData(UPLOAD, dsUpload)
            'Ends
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_BindGrid"
    Private Sub pvt_BindGrid(ByRef dtExcelData As DataTable, ByRef dtUploadData As DataTable, ByVal bv_strIdColumn As String, _
                             ByVal bv_strTableName As String, ByVal bv_DisplayType As String)
        Try
            pvt_BindSchema(dtExcelData, bv_strIdColumn, bv_strTableName)
            If dtUploadData.Rows.Count > 0 Then

                If bv_DisplayType <> "" AndAlso bv_strTableName.ToUpper() = "TARIFF" Then  'For Tariff Upload to showing Valid/InValid Data

                    Dim drsValid = New DataView(dtUploadData)

                    If bv_DisplayType = "Valid" Then

                        drsValid.RowFilter = "Valid=True"
                    Else
                        drsValid.RowFilter = "Valid=False"
                    End If
                    With ifgUploadStatus
                        .DataSource = drsValid
                        .DataBind()
                    End With
                Else
                    With ifgUploadStatus
                        .DataSource = dtUploadData
                        .DataBind()
                    End With
                End If

            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
#Region "pvt_CheckForeignkeyForSubItem"
    Private Function pvt_CheckForeignkeyForSubItem(ByVal strKeyCol As String, ByVal strRefColName As String, ByVal strRefTblName As String, strExceldata As String, ByVal bv_ITM_ID As Int32) As Int64
        Try
            Dim strBuildQuery As String
            strBuildQuery = String.Concat("Select ", strKeyCol, " From ", strRefTblName, " Where ", strRefColName, "='", strExceldata, "'")
            Dim objUpload As New Upload
            Return objUpload.pvt_GetCountCheckForeignkeySubItem(strBuildQuery, bv_ITM_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_CheckForeignkeyForEquipCode"
    Private Function pvt_CheckForeignkeyForEquipCode(ByVal strKeyCol As String, ByVal strRefColName As String, ByVal strRefTblName As String, strExceldata As String, ByVal bv_Type_ID As String) As Int64
        Try
            Dim strBuildQuery As String
            strBuildQuery = String.Concat("Select ", strKeyCol, " From ", strRefTblName, " Where ", strRefColName, "='", strExceldata, "'")
            Dim objUpload As New Upload
            Return objUpload.pvt_GetCountCheckForeignkeyEquipCode(strBuildQuery, bv_Type_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_CheckForeignkey"
    Private Function pvt_CheckForeignkey(ByVal strKeyCol As String, ByVal strRefColName As String, ByVal strRefTblName As String, ByVal strExceldata As String, ByVal bv_i32DEPOT_ID As Int32) As Int64
        Try
            Dim strBuildQuery As String
            strBuildQuery = String.Concat("Select ", strKeyCol, " From ", strRefTblName, " Where ACTV_BT = 1 AND ", strRefColName, "='", strExceldata, "'")
            Dim objUpload As New Upload
            Return objUpload.pvt_GetCountCheckForeignkey(strBuildQuery, bv_i32DEPOT_ID)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Function
#End Region

#Region "pvt_BindSchema"
    ''' <summary>
    ''' This function is used to generate schema based on the type of data column
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub pvt_BindSchema(ByRef dt As DataTable, ByVal bv_strIdColumn As String, ByVal bv_strTableName As String)
        Try
            Dim I As Integer
            Dim intTariffCount As Integer = 0
            Dim intPreAdvCount As Integer = 0
            I = 0
            Dim arrTarif() As Integer = {0, 1, 4, 5, 6, 7, 8, 16, 17, 18, 19}   'skip these indices for tariff upload
            Dim arrCustTarif() As Integer = {12}   'skip these indices for tariff upload
            Dim objCommon As New CommonData
            str_054Value = objCommon.GetConfigSetting("054", bln_054EnabledBit)
            Dim arrPreAdvice() As Integer
            If str_054Value.ToLower = "true" Then
                arrPreAdvice = {8, 9, 10, 11, 12, 13, 14}
            Else
                arrPreAdvice = {7, 8, 9, 10, 11, 12, 13}
            End If

            ifgUploadStatus.AutoGenerateColumns = False
            ifgUploadStatus.Columns.Clear()
            ifgUploadStatus.DataKeyNames() = New String() {bv_strIdColumn}
            For Each dc As DataColumn In dt.Columns
                If bv_strTableName.ToUpper() = "TARIFF" Then
                    ifgUploadStatus.StaticHeaderHeight = 400
                    ifgUploadStatus.Width = 950
                    Dim arrTarifList As New List(Of Integer)(arrTarif)
                    If arrTarifList.Contains(intTariffCount) Then
                        intTariffCount = intTariffCount + 1
                        Continue For
                    End If
                    intTariffCount = intTariffCount + 1
                ElseIf bv_strTableName.ToUpper = "TARIFF_CODE_DETAIL" Then
                    Dim arrTarifList As New List(Of Integer)(arrCustTarif)
                    ifgUploadStatus.Width = 950
                    If arrTarifList.Contains(intTariffCount) Then
                        intTariffCount = intTariffCount + 1
                        Continue For
                    End If
                    intTariffCount = intTariffCount + 1
                ElseIf bv_strTableName.ToUpper = "PRE_ADVICE" Then
                    Dim arrPreAdviceList As New List(Of Integer)(arrPreAdvice)
                    ifgUploadStatus.Width = 950
                    If arrPreAdviceList.Contains(intPreAdvCount) Then
                        intPreAdvCount = intPreAdvCount + 1
                        Continue For
                    End If
                    intPreAdvCount = intPreAdvCount + 1
                End If
                If dc.DataType Is GetType(System.DateTime) Then
                    Dim dfield As New DateField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.DataFormatString = "{0:dd-MMM-yyyy}"
                    dfield.HtmlEncode = False
                    dfield.iDate.DateIcon.CssClass = "dimg"
                    dfield.iDate.DateIcon.Src = iUtil.AppPath & _
                                    iUtil.ImagesFolder & "calendar.png"
                    dfield.iDate.CssClass = "lkp"
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    dfield.iDate.MaxLength = 23
                    ifgUploadStatus.Columns.Add(dfield)
                ElseIf dc.DataType Is GetType(System.Int32) Or dc.DataType Is GetType(System.Int64) Or _
                    dc.DataType Is GetType(System.Decimal) Then
                    Dim dfield As New TextboxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.TextBox.CssClass = "lkp"
                    dfield.TextBox.iCase = TextCase.Numeric
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    dfield.TextBox.MaxLength = 255
                    ifgUploadStatus.Columns.Add(dfield)
                ElseIf dc.DataType Is GetType(System.Boolean) Then
                    Dim dfield As New iInterchange.WebControls.v4.Data.CheckBoxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = False
                    ifgUploadStatus.Columns.Add(dfield)
                Else
                    Dim dfield As New TextboxField
                    dfield.DataField = dc.ColumnName
                    dfield.SortExpression = dc.ColumnName
                    dfield.HeaderText = dc.ColumnName
                    dfield.TextBox.CssClass = "lkp"
                    ifgUploadStatus.Columns.Add(dfield)
                    dfield.HeaderStyle.Wrap = False
                    dfield.ItemStyle.Wrap = True
                End If
                ifgUploadStatus.Columns.Item(I).AllowSearch = True
                I = I + 1
            Next
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "CHECK DIGIT FOR CONATAINER"
    Private Function pvt_fnCheckDigit(ByVal bv_strDigit As String) As Integer
        Try
            Dim a() As Integer = {10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 34, 35, 36, 37, 38}
            Dim i As Integer
            Dim total As Integer = 0
            Dim product As Integer
            Dim retChkdigit As Integer
            Dim retVal As Boolean = True

            bv_strDigit = bv_strDigit.ToUpper
            If Not bv_strDigit.Trim = String.Empty Then
                For i = 0 To bv_strDigit.Trim.Length - 1
                    Dim strCharcode As Integer = Asc(bv_strDigit.Chars(i))
                    'CHECK FOR ALPHABET
                    If strCharcode >= 65 And strCharcode <= 90 Then
                        product = a(strCharcode - 65)
                        'CHECK FOR NUMERIC
                    ElseIf strCharcode >= 48 And strCharcode <= 57 Then
                        product = strCharcode - 48
                    Else
                        retVal = False
                    End If
                    total = total + (product * Math.Pow(2, i))
                Next i
                If retVal Then
                    retChkdigit = (total Mod 11) Mod 10
                    Return retChkdigit
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class

