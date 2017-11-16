Imports System.Xml
Imports System.Xml.Xsl
Partial Class Operations_EstimateAttachment
    Inherits Pagebase

#Region "Page_Load"
    ''' <summary>
    ''' This method is used set max length for Description and also to set changes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonWeb.pub_AttachDescMaxlength(txtDescription, True)
        CommonWeb.pub_AttachHasChanges(txtDescription)
    End Sub
#End Region

#Region "OnCallback"
    ''' <summary>
    ''' This method is used to initialise Callback methods.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub Page_OnCallback(ByVal sender As Object, ByVal e As PageCallbackEventArgs) Handles Me.OnCallback
        Select Case e.CallbackType
            Case "LoadAttachments"
                pvt_LoadAttachments(e.GetCallbackValue("Mode"), _
                                    e.GetCallbackValue("HeaderLine"), _
                                    e.GetCallbackValue("EstimateNo"), _
                                    e.GetCallbackValue("Count"), _
                                    e.GetCallbackValue("UnreadCount"), _
                                    e.GetCallbackValue("RevisionNo"), _
                                    e.GetCallbackValue("Action"), _
                                    e.GetCallbackValue("ViewMode"))



            Case "AttachFile"
                'pvt_AttachFile(e.GetCallbackValue("Description"), e.GetCallbackValue("HeaderLine"), e.GetCallbackValue("FileName"))
            Case "RemoveAttachments"
                pvt_RemoveAttachments(e.GetCallbackValue("pathid"), e.GetCallbackValue("LineNo"))
            Case "FilterAttachments"
                pvt_FilterAttachments(e.GetCallbackValue("AttachmentType"), e.GetCallbackValue("HeaderLine"), e.GetCallbackValue("Viewmode"))

        End Select
    End Sub
#End Region

#Region "pvt_LoadAttachments"
    ''' <summary>
    ''' This method is used to load Attachments from DashBoard and Estimate Page.
    ''' </summary>
    ''' <param name="bv_strMode">Denotes Page Mode</param>
    ''' <param name="bv_strHeaderLine">Determines line no filter need to be applied or not</param>
    ''' <param name="bv_strEstimateNo">Denotes Estimate No</param>
    ''' <param name="bv_strCount">Determines calling method,Attachment opened form Dashboard or Estimate</param>
    ''' <param name="bv_strUnreadCount">Denotes Unread Attachment count</param>
    ''' <param name="bv_strRevisionNo">Denotes Revision no to display Attachments based on Revision No</param>
    ''' <param name="bv_strAction">Determines Action to open Unread Attachment count or total count</param>
    ''' <param name="bv_strViewMode">Determines Attachment opened in View Mode</param>
    ''' <remarks></remarks>

    Private Sub pvt_LoadAttachments(ByVal bv_strMode As String, _
                                    ByVal bv_strHeaderLine As String, _
                                    ByVal bv_strEstimateNo As String, _
                                    ByVal bv_strCount As String, _
                                    ByVal bv_strUnreadCount As String, ByVal bv_strRevisionNo As String, _
                                    ByVal bv_strAction As String, ByVal bv_strViewMode As String)
        Try

            'Dim strinnerHTML As String = ""
            'Dim objCol2 As New Data.DataColumn
            'objCol2.ColumnName = "org_photo"
            'objCol2.DataType = GetType(String)
            'Dim objCol1 As New Data.DataColumn
            'objCol1.ColumnName = "extension"
            'objCol1.DataType = GetType(String)
            'Dim objCol3 As New Data.DataColumn
            'objCol3.ColumnName = "photo"
            'objCol3.DataType = GetType(String)
            'Dim objCol4 As New Data.DataColumn
            'objCol4.ColumnName = "pathid"
            'objCol4.DataType = GetType(String)
            'Dim intrcnt As Integer = 0
            'Dim dsEstimate As EstimateDataSet
            'Dim objEstimate As New Estimate
            'Dim strAttachmentcount As String = "0"
            'Dim objStrDateCol As New Data.DataColumn
            'objStrDateCol.ColumnName = "strdate"
            'objStrDateCol.DataType = GetType(String)
            'Dim i32ReadAttachmentCount As Int32 = 0
            'Dim strAttachmentMode As String = ""

            'If bv_strCount = "Estimate" Then
            '    strAttachmentMode = "Estimate"
            '    ''Notes opened from estimate page
            '    If Not RetrieveData("Estimate") Is Nothing Then
            '        dsEstimate = CType(RetrieveData("Estimate"), EstimateDataSet)
            '        If bv_strAction = "total" Then
            '            strAttachmentcount = (dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count).ToString
            '            pvt_LoadEstimateAttachments(dsEstimate, bv_strHeaderLine, strinnerHTML)
            '            CacheData("Estimate", dsEstimate)
            '        Else
            '            'If bv_strUnreadCount <> "0" Then
            '            i32ReadAttachmentCount = (dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count) - CInt(bv_strUnreadCount)
            '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("extension") = False Then
            '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol1)
            '            End If
            '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("org_photo") = False Then
            '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol2)
            '            End If

            '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("photo") = False Then
            '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol3)
            '            End If

            '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("pathid") = False Then
            '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol4)
            '            End If
            '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("strdate") = False Then
            '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objStrDateCol)
            '            End If

            '            For Each dr As DataRow In dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows
            '                intrcnt = intrcnt + 1
            '                dr.Item("pathid") = intrcnt.ToString

            '                dr.Item("extension") = IO.Path.GetExtension(dr.Item(EstimateData.ACTL_FL_NAM).ToString).ToLower()

            '                If dr.Item("extension") = ".jpg" OrElse dr.Item("extension") = ".png" OrElse dr.Item("extension") = ".gif" Then
            '                    dr.Item("photo") = String.Concat("../download.ashx?SKP_ERR=1&FL_NM=", dr.Item(EstimateData.FL_NAM), "_tn", dr.Item("extension"))
            '                ElseIf dr.Item("extension") = ".xls" Then
            '                    dr.Item("photo") = "../Images/xls.png"
            '                ElseIf dr.Item("extension") = ".xlsx" Then
            '                    dr.Item("photo") = "../Images/xlsx.png"
            '                ElseIf dr.Item("extension") = ".doc" Then
            '                    dr.Item("photo") = "../Images/doc.png"
            '                ElseIf dr.Item("extension") = ".docx" Then
            '                    dr.Item("photo") = "../Images/docx.png"
            '                ElseIf dr.Item("extension") = ".pdf" Then
            '                    dr.Item("photo") = "../Images/pdf.png"
            '                ElseIf dr.Item("extension") = ".txt" Then
            '                    dr.Item("photo") = "../Images/txt.png"
            '                ElseIf dr.Item("extension") = ".dcx" Then
            '                    dr.Item("photo") = "../Images/dcx.png"
            '                ElseIf dr.Item("extension") = ".d5d" Then
            '                    dr.Item("photo") = "../Images/d5d.png"
            '                End If

            '                dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))

            '                If CInt(dr.Item("RNO")) < i32ReadAttachmentCount Then
            '                    dr.Item(EstimateData.FLTR_ID) = 0
            '                Else
            '                    dr.Item(EstimateData.FLTR_ID) = 1
            '                End If

            '                dr.Item("strdate") = CDate(dr.Item(EstimateData.CRTD_DT)).ToString("dd-MMM-yyy hh:mm tt")
            '            Next
            '            strinnerHTML = pvt_GenerateXSLT(dsEstimate)
            '            ' End If

            '        End If
            '    End If
            'Else
            '    If bv_strRevisionNo = "" Then
            '        bv_strRevisionNo = "0"
            '    End If

            '    Dim dsAttachmentView As EstimateDataSet

            '    'Notes opened from dashboard
            '    If bv_strCount = "total" Then
            '        strAttachmentMode = "ViewMode"

            '        dsAttachmentView = objEstimate.pub_GetEstimateAttachmentByEstimateNoandRevNo(bv_strEstimateNo, bv_strRevisionNo)
            '        Dim arrLineNos As New ArrayList
            '        Dim strLineNos As String
            '        Dim intRowCount As Integer = 0

            '        For Each drEstimate As DataRow In dsAttachmentView.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows
            '            Dim strLineNo As String = drEstimate.Item(EstimateData.LN_NO).ToString()

            '            If Not arrLineNos.Contains(strLineNo) AndAlso strLineNo <> "0" Then
            '                arrLineNos.Add(strLineNo)
            '            End If
            '        Next

            '        Dim arrArrayLineNos As String() = arrLineNos.ToArray(GetType(System.String))

            '        strLineNos = String.Join(",", arrArrayLineNos)

            '        pub_SetCallbackReturnValue("LineNos", strLineNos)
            '        pub_SetCallbackReturnValue("AttachmentMode", strAttachmentMode)

            '    Else
            '        dsAttachmentView = objEstimate.pub_GetUnreadEstimateAttachmentByEstimateNo(bv_strEstimateNo, CInt(bv_strUnreadCount))
            '    End If

            '    pvt_LoadEstimateAttachments(dsAttachmentView, bv_strHeaderLine, strinnerHTML)
            '    strAttachmentcount = (dsAttachmentView.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count).ToString
            '    CacheData("EstimateView", dsAttachmentView)
            'End If

            'pub_SetCallbackReturnValue("AttachmentMode", strAttachmentMode)
            'pub_SetCallbackReturnValue("TotalAttachmentcount", strAttachmentcount)
            'pub_SetCallbackReturnValue("innerHTML", strinnerHTML)
            'pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_LoadEstimateAttachments"
    ''' <summary>
    ''' This method is used to load Estimate Attachment content
    ''' </summary>
    ''' <param name="dsEstimate">Denotes Estimate dataset</param>
    ''' <param name="bv_strHeaderLine">Determines line no filter need to be applied or not</param>
    ''' <param name="strinnerHTML">Denotes Attachment content to be displayed</param>
    ''' <remarks></remarks>

    'Private Sub pvt_LoadEstimateAttachments(ByRef dsEstimate As EstimateDataSet, ByVal bv_strHeaderLine As String, ByRef strinnerHTML As String)
    '    Try

    '        Dim objCol2 As New Data.DataColumn
    '        objCol2.ColumnName = "org_photo"
    '        objCol2.DataType = GetType(String)
    '        Dim objCol1 As New Data.DataColumn
    '        objCol1.ColumnName = "extension"
    '        objCol1.DataType = GetType(String)
    '        Dim objCol3 As New Data.DataColumn
    '        objCol3.ColumnName = "photo"
    '        objCol3.DataType = GetType(String)
    '        Dim objCol4 As New Data.DataColumn
    '        objCol4.ColumnName = "pathid"
    '        objCol4.DataType = GetType(String)
    '        Dim intrcnt As Integer = 0
    '        Dim objEstimate As New Estimate
    '        Dim strAttachmentcount As String = "0"
    '        Dim objStrDateCol As New Data.DataColumn
    '        objStrDateCol.ColumnName = "strdate"
    '        objStrDateCol.DataType = GetType(String)

    '        If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count > 0 Then
    '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("extension") = False Then
    '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol1)
    '            End If
    '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("org_photo") = False Then
    '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol2)
    '            End If

    '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("photo") = False Then
    '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol3)
    '            End If

    '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("pathid") = False Then
    '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol4)
    '            End If
    '            If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("strdate") = False Then
    '                dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objStrDateCol)
    '            End If


    '            For Each dr As DataRow In dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows
    '                intrcnt = intrcnt + 1
    '                dr.Item("pathid") = intrcnt.ToString
    '                Dim strExtension As String = ""
    '                strExtension = IO.Path.GetExtension(dr.Item(EstimateData.ACTL_FL_NAM).ToString())
    '                dr.Item("extension") = strExtension.ToLower()
    '                dr.Item("strdate") = CDate(dr.Item(EstimateData.CRTD_DT)).ToString("dd-MMM-yyy hh:mm tt")

    '                If dr.Item("extension") = ".jpg" OrElse dr.Item("extension") = ".png" OrElse dr.Item("extension") = ".gif" Then
    '                    dr.Item("photo") = String.Concat("../download.ashx?SKP_ERR=1&FL_NM=", dr.Item(EstimateData.FL_NAM), "_tn", dr.Item("extension"))
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".xls" Then
    '                    dr.Item("photo") = "../Images/xls.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".xlsx" Then
    '                    dr.Item("photo") = "../Images/xlsx.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".doc" Then
    '                    dr.Item("photo") = "../Images/doc.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".docx" Then
    '                    dr.Item("photo") = "../Images/docx.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".pdf" Then
    '                    dr.Item("photo") = "../Images/pdf.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".txt" Then
    '                    dr.Item("photo") = "../Images/txt.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".dcx" Then
    '                    dr.Item("photo") = "../Images/dcx.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                ElseIf dr.Item("extension") = ".d5d" Then
    '                    dr.Item("photo") = "../Images/d5d.png"
    '                    dr.Item("org_photo") = String.Concat("../download.ashx?FL_NM=", dr.Item(EstimateData.FL_NAM), dr.Item("extension"))
    '                End If

    '                ''This block helps to apply default filter while opening notes from estimate detail
    '                If bv_strHeaderLine <> "0" Then
    '                    If dr.Item(EstimateData.LN_NO).ToString = bv_strHeaderLine Then
    '                        dr.Item(EstimateData.FLTR_ID) = 1
    '                    Else
    '                        dr.Item(EstimateData.FLTR_ID) = 0
    '                    End If
    '                Else
    '                    dr.Item(EstimateData.FLTR_ID) = 1

    '                End If
    '            Next

    '            strinnerHTML = pvt_GenerateXSLT(dsEstimate)
    '            strAttachmentcount = (dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count).ToString
    '        Else
    '            strinnerHTML = ""
    '        End If

    '    Catch ex As Exception
    '        pub_SetCallbackStatus(False)
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
    '    End Try
    'End Sub

#End Region

    '#Region "pvt_AttachFile"
    ''' <summary>
    ''' This method is used to display Attached File in Estimate Attachment panel.
    ''' </summary>
    ''' <param name="bv_strDescription">Denotes Description given while attaching File</param>
    ''' <param name="strHeaderLine">Determines line no filter need to be applied or not</param>
    ''' <param name="bv_strFileName">Denotes Attached File Name</param>
    ''' <remarks></remarks>

    '    Private Sub pvt_AttachFile(ByVal bv_strDescription As String, ByVal strHeaderLine As String, ByVal bv_strFileName As String)
    '        Try
    '            Dim strinnerHTML As String = ""
    '            Dim objCol As New Data.DataColumn
    '            Dim intrcnt As Integer = 0
    '            objCol.ColumnName = "photo"
    '            objCol.DataType = GetType(String)
    '            Dim drAtt() As DataRow
    '            Dim strAttachments As String = "0"
    '            Dim objStrDateCol As New Data.DataColumn
    '            objStrDateCol.ColumnName = "strdate"
    '            objStrDateCol.DataType = GetType(String)


    '            If Not RetrieveData("Estimate") Is Nothing Then
    '                Dim dsEstimate As EstimateDataSet
    '                dsEstimate = CType(RetrieveData("Estimate"), EstimateDataSet)
    '                If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count > 0 Then
    '                    If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("strdate") = False Then
    '                        dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objStrDateCol)
    '                    End If
    '                    Dim dr() As DataRow = dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Select(String.Concat(EstimateData.FL_NAM, "=", bv_strFileName))
    '                    dr(0).Item(EstimateData.DSCRPTN_VC) = bv_strDescription
    '                    If strHeaderLine = "HEADER" Then
    '                        dr(0).Item(EstimateData.LN_NO) = 0
    '                    Else
    '                        dr(0).Item(EstimateData.LN_NO) = strHeaderLine
    '                    End If

    '                    For Each drA As DataRow In dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows
    '                        intrcnt = intrcnt + 1
    '                        drA.Item("pathid") = intrcnt.ToString
    '                        ''Filter ID field used to apply filter in estimate attachment dataset.
    '                        drA.Item(EstimateData.FLTR_ID) = 1
    '                        drA.Item("strdate") = CDate(drA.Item(EstimateData.CRTD_DT)).ToString("dd-MMM-yyy hh:mm tt")

    '                    Next
    '                    CacheData("Estimate", dsEstimate)
    '                    strinnerHTML = pvt_GenerateXSLT(dsEstimate)
    '                    strAttachments = dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count.ToString
    '                End If
    '                'This method is used to add Notes count in estimate detail dynamically while adding notes
    '                If strHeaderLine <> "0" Then
    '                    drAtt = dsEstimate.Tables(EstimateData._ESTIMATE_DETAIL).Select(String.Concat(EstimateData.LN_NO, " =", "'", strHeaderLine, "'"))
    '                    drAtt(0)(EstimateData.ATTCHMNT_CNT) = dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Select(String.Concat(EstimateData.LN_NO, " =", "'", strHeaderLine, "'")).Length
    '                End If
    '            Else

    '            End If

    '            pub_SetCallbackReturnValue("TotalAttachmentcount", strAttachments)
    '            pub_SetCallbackReturnValue("innerHTML", strinnerHTML)
    '            pub_SetCallbackStatus(True)

    '        Catch ex As Exception
    '            pub_SetCallbackStatus(False)
    '            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
    '        End Try
    '    End Sub
    '#End Region

#Region "pvt_RemoveAttachments"
    ''' <summary>
    ''' This method is used to remove Attached File from Attachment panel
    ''' </summary>
    ''' <param name="bv_strpathid">Denotes Path ID of attached File</param>
    ''' <param name="bv_strLineNo">Denotes Line No</param>
    ''' <remarks></remarks>
    Private Sub pvt_RemoveAttachments(ByVal bv_strpathid As String, ByVal bv_strLineNo As String)
        Try

            'Dim strinnerHTML As String = ""
            'Dim objCol As New Data.DataColumn
            'objCol.ColumnName = "photo"
            'objCol.DataType = GetType(String)
            'Dim dsEstimate As EstimateDataSet
            'Dim drAtt() As DataRow
            'If Not RetrieveData("Estimate") Is Nothing Then

            '    dsEstimate = CType(RetrieveData("Estimate"), EstimateDataSet)
            '    If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count > 0 Then
            '        Dim dr() As DataRow = dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Select(String.Concat("pathid='", bv_strpathid, "'"))
            '        dr(0).Delete()
            '        CacheData("Estimate", dsEstimate)
            '        If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count > 0 Then
            '            strinnerHTML = pvt_GenerateXSLT(dsEstimate)
            '        Else
            '            strinnerHTML = ""
            '        End If
            '    Else
            '        strinnerHTML = ""
            '    End If
            '    'This method is used to add Notes count in estimate detail dynamically while adding notes
            '    If bv_strLineNo <> "0" Then
            '        drAtt = dsEstimate.Tables(EstimateData._ESTIMATE_DETAIL).Select(String.Concat(EstimateData.LN_NO, " =", "'", bv_strLineNo, "'"))
            '        drAtt(0)(EstimateData.ATTCHMNT_CNT) = dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Select(String.Concat(EstimateData.LN_NO, " =", "'", bv_strLineNo, "'")).Length
            '    End If
            '    pub_SetCallbackReturnValue("TotalAttachmentcount", (dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows.Count).ToString)
            'Else
            '    strinnerHTML = ""
            '    pub_SetCallbackReturnValue("TotalAttachmentcount", "")
            'End If
            'CacheData("EstimateFilecount", CType(RetrieveData("EstimateFilecount"), Integer) - 1)
            'pub_SetCallbackReturnValue("innerHTML", strinnerHTML)
            'pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_FilterAttachments"
    ''' <summary>
    ''' This method is used to filter Attached Files display
    ''' </summary>
    ''' <param name="bv_strAttachmentType">Determines Type of Attached Files </param>
    ''' <param name="strHeaderLine">Determines line no filter need to be applied or not</param>
    ''' <param name="bv_strVieMode">Determines Estimate Attachment opened in View Mode or Not</param>
    ''' <remarks></remarks>

    Private Sub pvt_FilterAttachments(ByVal bv_strAttachmentType As String, ByVal strHeaderLine As String, ByVal bv_strVieMode As String)
        Try

            'Dim dsEstimate As RepairEstimateDataSet
            'Dim strinnerHTML As String
            'Dim i32LineNo As Int32
            'Dim objCol1 As New Data.DataColumn
            'objCol1.ColumnName = "extension"
            'objCol1.DataType = GetType(String)

            'If Not RetrieveData("Estimate") Is Nothing Or Not RetrieveData("EstimateView") Is Nothing Then
            '    dsEstimate = RetrieveData("Estimate")
            '    If strHeaderLine = "ALL" Then
            '        strHeaderLine = ""
            '    ElseIf strHeaderLine = "HEADER" Then
            '        i32LineNo = 0
            '    Else
            '        i32LineNo = CInt(strHeaderLine)
            '    End If

            '    If bv_strAttachmentType = "ALL" Then
            '        bv_strAttachmentType = ""
            '    ElseIf bv_strAttachmentType = "DOCUMENTS" Then
            '        bv_strAttachmentType = "DOCUMENTS"
            '    ElseIf bv_strAttachmentType = "PHOTOS" Then
            '        bv_strAttachmentType = "PHOTOS"
            '    End If

            '    If bv_strVieMode = "True" Then
            '        dsEstimate = RetrieveData("EstimateView")
            '    Else
            '        dsEstimate = RetrieveData("Estimate")
            '    End If

            '    If dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Contains("extension") = False Then
            '        dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Columns.Add(objCol1)
            '    End If

            '    For Each dr As DataRow In dsEstimate.Tables(EstimateData._ESTIMATE_ATTACHMENT).Rows
            '        If dr.Item("extension").ToString = ".jpg" OrElse dr.Item("extension").ToString = ".png" OrElse dr.Item("extension").ToString = ".gif" Then
            '            dr.Item(EstimateData.ATTCHMNT_TYP) = "PHOTOS"
            '        Else
            '            dr.Item(EstimateData.ATTCHMNT_TYP) = "DOCUMENTS"
            '        End If

            '        If strHeaderLine <> "" AndAlso bv_strAttachmentType <> "" Then
            '            If dr.Item(EstimateData.LN_NO).ToString = i32LineNo AndAlso dr.Item(EstimateData.ATTCHMNT_TYP).ToString = bv_strAttachmentType Then
            '                dr.Item(EstimateData.FLTR_ID) = 1
            '            Else
            '                dr.Item(EstimateData.FLTR_ID) = 0
            '            End If
            '        ElseIf strHeaderLine = "" AndAlso bv_strAttachmentType <> "" Then

            '            If dr.Item(EstimateData.ATTCHMNT_TYP).ToString = bv_strAttachmentType Then
            '                dr.Item(EstimateData.FLTR_ID) = 1
            '            Else
            '                dr.Item(EstimateData.FLTR_ID) = 0
            '            End If
            '        ElseIf strHeaderLine <> "" AndAlso bv_strAttachmentType = "" Then
            '            If dr.Item(EstimateData.LN_NO).ToString = i32LineNo Then
            '                dr.Item(EstimateData.FLTR_ID) = 1
            '            Else
            '                dr.Item(EstimateData.FLTR_ID) = 0
            '            End If
            '        ElseIf strHeaderLine = "" AndAlso bv_strAttachmentType = "" Then
            '            dr.Item(EstimateData.FLTR_ID) = 1
            '        End If

            '    Next
            '    strinnerHTML = pvt_GenerateXSLT(dsEstimate)
            'Else
            '    strinnerHTML = ""
            'End If

            'pub_SetCallbackReturnValue("innerHTML", strinnerHTML)
            'pub_SetCallbackStatus(True)
        Catch ex As Exception
            pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_GenerateXSLT"
    ''' <summary>
    ''' This method is used generate XSLT by passing dataset
    ''' </summary>
    ''' <param name="bv_dsAttachment">Denotes Estimate dataset</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function pvt_GenerateXSLT(ByVal bv_dsAttachment As EstimateDataSet) As String
    '    Dim strinnerHTML As String = ""
    '    Dim objMemoryStream As New System.IO.MemoryStream
    '    Dim objhtmlMemoryStream As New System.IO.MemoryStream
    '    Try

    '        bv_dsAttachment.WriteXml(objMemoryStream, XmlWriteMode.DiffGram)

    '        Dim doc As New XmlDocument()

    '        objMemoryStream.Position = 0
    '        doc.Load(objMemoryStream)

    '        Dim objXmlWriterSetting As New XmlWriterSettings
    '        objXmlWriterSetting.ConformanceLevel = ConformanceLevel.Fragment

    '        Dim objwriter As XmlWriter = XmlWriter.Create(objhtmlMemoryStream, objXmlWriterSetting)
    '        Dim transform As New XslCompiledTransform()

    '        transform.Load(Server.MapPath("../Templates/EstimateAttachment.xslt"))

    '        transform.Transform(doc.CreateNavigator(), Nothing, objwriter)
    '        objwriter.Close()

    '        objhtmlMemoryStream.Flush()
    '        objhtmlMemoryStream.Position = 0
    '        Dim strStream As New IO.StreamReader(objhtmlMemoryStream)

    '        strinnerHTML = strStream.ReadToEnd()

    '    Catch ex As Exception
    '        pub_SetCallbackStatus(False)
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
    '    Finally
    '        objMemoryStream = Nothing
    '        objhtmlMemoryStream = Nothing
    '    End Try
    '    Return strinnerHTML
    'End Function
#End Region

#Region "Page_PreRender1"
    ''' <summary>
    ''' This method is used to render the script with version
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_PreRender1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            CommonWeb.IncludeScript("Script/Common.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/Operations/Estimate.js", MyBase.Page)
            CommonWeb.IncludeScript("Script/UI/Quicklinks.js", MyBase.Page)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region
End Class
