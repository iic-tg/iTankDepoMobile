Imports System.Data
Imports System.Security.Cryptography

Partial Class Operations_PhotoViewer
    Inherits System.Web.UI.Page

#Region "Declarations"
    Dim pdsPhotos As PagedDataSource
    Dim intCurrentPosition As Integer
    Dim intPageSize As Integer
    Dim intPageIndex As Integer
    Private Const CURRENT_POSITION As String = "CURRENT_POSITION"
    Private Const PHOTO_SESSION As String = "PHOTO_SESSION"
    Dim intDepotID As Integer = ConfigurationManager.AppSettings("DepotID")
    Dim dtPhotoUpload As New DataTable
    Dim dtPhotos As DataTable
#End Region

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Not Request.QueryString("Estimation_No") Is Nothing Then
                    Dim photos As New iTankDepoService.IserviceClient
                    Dim strAppPath As String = String.Empty
                    Dim strRepairEstimateNo As String = String.Empty
                    Dim strRepairEstimateId As String = String.Empty
                    Dim lngRepairEstimateId As Long
                    Dim strNoImagePath As String = String.Empty
                    Dim strImageFormatSplit() As String = Nothing
                    Dim strFileFormatSplit() As String = Nothing
                    Dim strUploadFilter As String = ConfigurationManager.AppSettings("UploadFilter").ToString
                    Dim strImageFormat As String = ConfigurationManager.AppSettings("ImageFormat").ToString
                    Dim strFilesFormat As String = ConfigurationManager.AppSettings("FileFormat").ToString
                    Dim strAppDomainAppPath As String = System.Web.HttpRuntime.AppDomainAppPath()
                    Session("Estimate_No") = Nothing
                    strAppPath = ConfigurationManager.AppSettings("UploadAttachPath").ToString
                    strNoImagePath = ConfigurationManager.AppSettings("NoImagePath").ToString

                    strRepairEstimateNo = DecryptString(Request.QueryString("Estimation_No").ToString.Replace("IIC", "+"))
                    Session("Estimate_No") = strRepairEstimateNo

                    If Not (Request.QueryString("Repair_Estimate_Id")) Is Nothing Then
                        strRepairEstimateId = DecryptString(Request.QueryString("Repair_Estimate_Id").ToString.Replace("IIC", "+"))
                        lngRepairEstimateId = CLng(strRepairEstimateId)
                    End If
                    dtPhotoUpload = photos.GetAttachmentsByRepairEstimateNo(intDepotID, strRepairEstimateNo, lngRepairEstimateId, strUploadFilter).Tables("V_ATTACHMENT")
                    For Each drAttchment As DataRow In dtPhotoUpload.Rows
                        Dim strSplit() As String = Nothing
                        strSplit = drAttchment.Item("IMG_ATTCHMNT_PTH").ToString.Split(".")
                        If strSplit.Length > 1 Then
                            strImageFormatSplit = strImageFormat.Split(",")
                            strFileFormatSplit = strFilesFormat.Split(",")
                            For Each strImage As String In strImageFormatSplit
                                If drAttchment.Item("IMG_ATTCHMNT_PTH").ToString.EndsWith(strImage) Then
                                    drAttchment.Item("IMG_ATTCHMNT_PTH") = String.Concat(strAppPath, drAttchment.Item("ATTCHMNT_PTH"))
                                    drAttchment.Item("ATTCHMNT_PTH") = String.Concat(strAppPath, drAttchment.Item("ATTCHMNT_PTH"))
                                End If
                            Next
                            For Each strFiles As String In strFileFormatSplit
                                If drAttchment.Item("IMG_ATTCHMNT_PTH").ToString.EndsWith(strFiles) Then
                                    drAttchment.Item("IMG_ATTCHMNT_PTH") = String.Concat(strAppPath, strNoImagePath)
                                    drAttchment.Item("ATTCHMNT_PTH") = String.Concat(strAppPath, drAttchment.Item("ATTCHMNT_PTH"))
                                End If
                            Next
                        End If
                    Next

                    If Request.QueryString("Activity_Name") = "Repair Completion" Then
                        ddlActivityName.Items.Add("Repair Completion")
                        ddlActivityName.Items.Add("Repair Estimate")
                        ddlActivityName.Items.Add("Repair Approval")
                        ddlActivityName.Items.Add("All")
                    ElseIf Request.QueryString("Activity_Name") = "Repair Approval" Then
                        ddlActivityName.Items.Add("Repair Approval")
                        ddlActivityName.Items.Add("Repair Estimate")
                        ddlActivityName.Items.Add("All")
                    ElseIf Request.QueryString("Activity_Name") = "Survey Completion" Then
                        ddlActivityName.Items.Add("Survey Completion")
                        ddlActivityName.Items.Add("Repair Estimate")
                        ddlActivityName.Items.Add("Repair Approval")
                        ddlActivityName.Items.Add("Repair Completion")
                        ddlActivityName.Items.Add("All")
                    Else
                        ddlActivityName.Items.Add("Repair Estimate")
                    End If
                    Session("Activity_Name") = Request.QueryString("Activity_Name")
                    tdEstimationNo.InnerText = String.Concat("Estimation No : ", strRepairEstimateNo)
                    If dtPhotoUpload.Rows.Count > 0 Then
                        divnoRecords.Visible = False
                        If Not Page.IsPostBack Then
                            Session(CURRENT_POSITION) = 0
                            intCurrentPosition = 0
                            intPageIndex = Session(CURRENT_POSITION)
                        End If
                        Session(PHOTO_SESSION) = dtPhotoUpload
                        pvt_DataBind(dtPhotoUpload)
                        divPhotos.Visible = True

                    Else
                        If Request.QueryString("Activity_Name") = "Repair Estimate" Then
                            divPhotos.Visible = True
                            'divnoRecords.InnerText = String.Concat(divnoRecords.InnerText, " for the Estimation No: ", strRepairEstimateNo)
                            divnoRecords.InnerText = divnoRecords.InnerText
                            divnoRecords.Visible = True
                            divFileNavigation.Visible = False
                        Else
                            divPhotos.Visible = True
                            ' divnoRecords.InnerText = String.Concat(divnoRecords.InnerText, " for the Estimation No: ", strRepairEstimateNo)
                            divnoRecords.InnerText = divnoRecords.InnerText
                            divnoRecords.Visible = True
                            divFileNavigation.Visible = False
                        End If
                       
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


#Region "DecryptString()"
    Public Function DecryptString(Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        Dim pvt_strKeyPhrase As String
        Try
            pvt_strKeyPhrase = "IIC"
            Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(pvt_strKeyPhrase))
            TDESAlgorithm.Key = TDESKey
            TDESAlgorithm.Mode = CipherMode.ECB
            TDESAlgorithm.Padding = PaddingMode.PKCS7
            Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)
            Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        Catch ex As Exception
            Throw ex
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return UTF8.GetString(Results)
    End Function
#End Region

#Region "pvt_DataBind"
    Private Sub pvt_DataBind(ByVal bv_dtPhotos As DataTable)
        Try
            pdsPhotos = New PagedDataSource()
            pdsPhotos.DataSource = bv_dtPhotos.DefaultView
            pdsPhotos.PageSize = 5
            pdsPhotos.AllowPaging = True
            pdsPhotos.CurrentPageIndex = intCurrentPosition
            btnfirst.Enabled = Not pdsPhotos.IsFirstPage
            btnprevious.Enabled = Not pdsPhotos.IsFirstPage
            btnlast.Enabled = Not pdsPhotos.IsLastPage
            btnnext.Enabled = Not pdsPhotos.IsLastPage
            dlPhotos.DataSource = pdsPhotos
            dlPhotos.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Button_Click (First,Next,Last,Previous)"
    Protected Sub btnfirst_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            intCurrentPosition = 0
            Session(CURRENT_POSITION) = intCurrentPosition
            dtPhotos = Session(PHOTO_SESSION)
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnlast_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dtPhotos = Session(PHOTO_SESSION)
            If pdsPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
            intCurrentPosition = pdsPhotos.PageCount - 1
            Session(CURRENT_POSITION) = intCurrentPosition
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
            '  lblPageCount.Text = String.Concat(intCurrentPosition + 1, " of ", intPageSize)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnnext_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As New DataTable
            dt = Session(PHOTO_SESSION)
            If pdsPhotos Is Nothing Then
                pvt_DataBind(dt)
            End If
            Dim intPageSize = pdsPhotos.PageSize
            Dim intRowCount As Integer = dt.Rows.Count
            intPageSize = intRowCount / intPageSize
            intCurrentPosition = CInt(Session(CURRENT_POSITION))
            If intPageSize > intCurrentPosition Then
                intCurrentPosition += 1
            End If
            Session(CURRENT_POSITION) = intCurrentPosition
            dtPhotos = Session(PHOTO_SESSION)
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            intCurrentPosition = CInt(Session(CURRENT_POSITION))
            If intCurrentPosition > 0 Then
                intCurrentPosition -= 1
            End If
            Session(CURRENT_POSITION) = intCurrentPosition
            intPageIndex = Session(CURRENT_POSITION)
            dtPhotos = Session(PHOTO_SESSION)
            If Not dtPhotos Is Nothing Then
                pvt_DataBind(dtPhotos)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "ddlActivityName_SelectedIndexChanged"
    Protected Sub ddlActivityName_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            '   Dim dtPhoto As New DataTable
            Dim strActivityName As String = String.Empty
            Dim strEstimateNo As String = String.Empty
            Dim strAppPath As String = String.Empty
            Dim strImageFormatSplit() As String = Nothing
            Dim strFileFormatSplit() As String = Nothing
            Dim strAppDomainAppPath As String = System.Web.HttpRuntime.AppDomainAppPath()
            strAppPath = ConfigurationManager.AppSettings("UploadAttachPath").ToString
            Dim strRepair As String = ConfigurationManager.AppSettings("RepairActivity")
            Dim strUploadFilter As String = ConfigurationManager.AppSettings("UploadFilter").ToString
            Dim strNoImagePath As String = ConfigurationManager.AppSettings("NoImagePath").ToString
            Dim photos As New iTankDepoService.IserviceClient
            Dim strImageFormat As String = ConfigurationManager.AppSettings("ImageFormat").ToString
            Dim strFilesFormat As String = ConfigurationManager.AppSettings("FileFormat").ToString
            strEstimateNo = Session("Estimate_No")
            If Session("Activity_Name") = "Repair Approval" Then
                If ddlActivityName.Text = "All" Then
                    strActivityName = strRepair
                Else
                    strActivityName = ddlActivityName.Text
                End If
            ElseIf Session("Activity_Name") = "Repair Completion" Then
                If ddlActivityName.Text = "All" Then
                    strActivityName = strRepair
                Else
                    strActivityName = ddlActivityName.Text
                End If
            ElseIf Session("Activity_Name") = "Survey Completion" Then
                If ddlActivityName.Text = "All" Then
                    strActivityName = strRepair
                Else
                    strActivityName = ddlActivityName.Text
                End If
            Else
                strActivityName = ddlActivityName.Text
            End If
            dtPhotoUpload = photos.GetAttachmentsByActivityName(intDepotID, strEstimateNo, strActivityName, strUploadFilter).Tables("V_ATTACHMENT")
            For Each drAttchment As DataRow In dtPhotoUpload.Rows
                Dim strSplit() As String = Nothing
                strSplit = drAttchment.Item("IMG_ATTCHMNT_PTH").ToString.Split(".")
                If strSplit.Length > 1 Then
                    strImageFormatSplit = strImageFormat.Split(",")
                    strFileFormatSplit = strFilesFormat.Split(",")
                    For Each strImage As String In strImageFormatSplit
                        If drAttchment.Item("IMG_ATTCHMNT_PTH").ToString.EndsWith(strImage) Then
                            drAttchment.Item("IMG_ATTCHMNT_PTH") = String.Concat(strAppPath, drAttchment.Item("ATTCHMNT_PTH"))
                            drAttchment.Item("ATTCHMNT_PTH") = String.Concat(strAppPath, drAttchment.Item("ATTCHMNT_PTH"))
                        End If
                    Next
                    For Each strFiles As String In strFileFormatSplit
                        If drAttchment.Item("IMG_ATTCHMNT_PTH").ToString.EndsWith(strFiles) Then
                            drAttchment.Item("IMG_ATTCHMNT_PTH") = String.Concat(strAppPath, strNoImagePath)
                            drAttchment.Item("ATTCHMNT_PTH") = String.Concat(strAppPath, drAttchment.Item("ATTCHMNT_PTH"))
                        End If
                    Next
                End If
              
            Next
            If dtPhotoUpload.Rows.Count <= 0 Then
                divPhotos.Visible = True
                '  divnoRecords.InnerText = String.Concat("No Files Found for the Estimation No: ", strEstimateNo)
                divnoRecords.InnerText = divnoRecords.InnerText
                divnoRecords.Visible = True
                divFileNavigation.Visible = False
            Else
                divPhotos.Visible = True
                divFileNavigation.Visible = True
                divnoRecords.Visible = False
            End If
            Session(PHOTO_SESSION) = dtPhotoUpload
            Session(CURRENT_POSITION) = 0
            pvt_DataBind(dtPhotoUpload)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region



End Class
