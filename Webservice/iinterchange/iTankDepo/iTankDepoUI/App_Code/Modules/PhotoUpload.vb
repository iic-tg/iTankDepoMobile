Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class PhotoUpload
    Inherits System.Web.Services.WebService

    Private strSize As String = ConfigurationSettings.AppSettings("UploadPhotoSize")
    Private strPhotoLength As String = ConfigurationSettings.AppSettings("UploadPhotoFileLength")
    Dim ds As DataSet

    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function PhotoUpload(ByVal hfc As HttpFileCollection, _
                               ByVal bv_strPageName As String, ByVal RepairEstimateId As Integer) As String
        Try

            'Dim dtPreaAdvice As DataTable
            Dim objGateIn As New Gatein
            Dim dsGateInData As New GateinDataSet



            Dim objCommon As New CommonUI
            Dim objCommonData As New CommonData
            Dim objRepairEstimate As New RepairEstimate
            Dim intDepotId As Integer
            Dim strModifiedBy As String
            Dim strVirtualPath As String = ""
            Dim uploadFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString
            Dim drAttachment As DataRow
            Dim strFilename As String = ""
            Dim strExtension As String = ""
            Dim strClientFileName As String = ""
            intDepotId = objCommonData.GetDepotID()
            strModifiedBy = objCommonData.GetCurrentUserName()
            'Dim strRepairEstimateId As String = RetrieveData("RepairEstimateId").ToString
            Dim strRepairEstimateId As String = RepairEstimateId.ToString
            Dim lngMaxSize As Long = CLng(strSize)
            lngMaxSize = lngMaxSize / 1024000

            If bv_strPageName = "Repair Completion" Then
                'ds = CType(RetrieveData(REPAIR_COMPLETION), RepairCompletionDataSet)
            ElseIf bv_strPageName = "Pre-Advice" Then
                ' ds = CType(RetrieveData(PreAdvice), PreAdviceDataSet)
            ElseIf bv_strPageName = "GateIn" Then
                ds = objGateIn.GetGateInPreAdviceDetail(1)
                'ds = dsGateInData.Tables(GateinData._V_GATEIN)
                'ds = CType(RetrieveData(GATE_IN), GateinDataSet)
            ElseIf bv_strPageName = "GateOut" Then
                'ds = CType(RetrieveData(GATE_OUT), GateOutDataSet)
            Else
                'ds = CType(RetrieveData(REPAIR_ESTIMATE), RepairEstimateDataSet)
            End If

            For i As Integer = 0 To hfc.Count - 1
                Dim hpf As HttpPostedFile = hfc(i)
                If hpf.ContentLength > 0 Then
                    Dim lngFileSize As Long
                    Dim sbPath As New StringBuilder
                    strClientFileName = System.IO.Path.GetFileName(hpf.FileName)
                    ' strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), Now.ToFileTime)
                    strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                    lngFileSize = hpf.ContentLength

                    drAttachment = ds.Tables(RepairEstimateData._ATTACHMENT).NewRow()
                    drAttachment(RepairEstimateData.ATTCHMNT_ID) = CommonWeb.GetNextIndex(ds.Tables(RepairEstimateData._ATTACHMENT), RepairEstimateData.ATTCHMNT_ID)
                    If Not IsDBNull(strRepairEstimateId) AndAlso strRepairEstimateId <> String.Empty AndAlso strRepairEstimateId <> " " Then
                        drAttachment(RepairEstimateData.RPR_ESTMT_ID) = strRepairEstimateId
                    End If

                    Dim myMatch As Match = System.Text.RegularExpressions.Regex.Match(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName), "^[a-zA-Z0-9 _.-]*$")
                    If myMatch.Success Then
                        strFilename = String.Concat(System.IO.Path.GetFileNameWithoutExtension(hpf.FileName).Replace("+", ""), Now.ToFileTime)
                        strFilename = String.Concat(strFilename, strExtension)
                        strVirtualPath = String.Concat(Config.pub_GetAppConfigValue("UploadAttachPath"), strFilename)
                        strExtension = LCase(System.IO.Path.GetExtension(hpf.FileName))
                        lngFileSize = hpf.ContentLength
                        If strClientFileName.Length < strPhotoLength Then
                            If Not System.IO.Directory.Exists(String.Concat(uploadFolder, "\UpLoad\Attachment")) Then
                                System.IO.Directory.CreateDirectory(String.Concat(uploadFolder, "\UpLoad\Attachment"))
                            End If
                            hpf.SaveAs(strVirtualPath)
                            drAttachment(RepairEstimateData.ATTCHMNT_PTH) = strFilename
                            drAttachment(RepairEstimateData.ACTL_FL_NM) = strClientFileName
                            drAttachment(RepairEstimateData.MDFD_BY) = strModifiedBy
                            drAttachment(RepairEstimateData.MDFD_DT) = CDate(Now)
                            drAttachment(RepairEstimateData.DPT_ID) = intDepotId
                            drAttachment(RepairEstimateData.ERR_FLG) = False
                        Else
                            drAttachment(RepairEstimateData.ERR_FLG) = True
                            drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("Attachment File Name (", strClientFileName, " must be below ", strPhotoLength, "Characters.")
                        End If
                    Else
                        drAttachment(RepairEstimateData.ERR_FLG) = True
                        drAttachment(RepairEstimateData.ERR_SMMRY) = String.Concat("File Name (", strClientFileName, ") with special characters are not allowed. ")
                    End If
                    ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Add(drAttachment)
                    If ds.Tables(RepairEstimateData._ATTACHMENT).Rows.Count = 0 Then
                        'Page.ClientScript.RegisterStartupScript(GetType(String), "", " el('divNoImageUpload').style.display = 'block';", True)
                    End If
                    If bv_strPageName = "GateIn" Then
                        'CacheData(GATE_IN, ds)
                    End If

                End If
            Next
        Catch ex As Exception

        End Try
    End Function

End Class