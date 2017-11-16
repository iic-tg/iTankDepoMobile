Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class EquipmentInfoMobile
    Inherits System.Web.Services.WebService
    Dim gateInMobile As New GateinMobile_C

    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function EIList(ByVal EquipmentNo As String, ByVal PageName As String, ByVal GateinTransactionNo As String, ByVal Attachment As String, ByVal UserName As String) As EquipmentInfoMobileModel
        Try

            gateInMobile.DepotID(UserName)
            Dim EquipmentInfoMobile_C As New EquipmentInfoMobile_C
            Dim dsEquipmentInformationData As EquipmentInformationDataSet = EquipmentInfoMobile_C.EquipmentInfoList(EquipmentNo, PageName, GateinTransactionNo, Attachment)







            Dim dt() As DataRow = dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, "='", EquipmentNo, "'"))


            Dim EquipmentInfoMobileModel As New EquipmentInfoMobileModel

            EquipmentInfoMobileModel.EIEQPMNT_NO = dt(0).Item("EQPMNT_NO").ToString()
            EquipmentInfoMobileModel.EIEQPMNT_TYP_CD = dt(0).Item("EQPMNT_TYP_CD").ToString()
            EquipmentInfoMobileModel.EIMNFCTR_DT = dt(0).Item("MNFCTR_DT").ToString()
            EquipmentInfoMobileModel.EITR_WGHT_NC = dt(0).Item("TR_WGHT_NC").ToString()
            EquipmentInfoMobileModel.EIGRSS_WGHT_NC = dt(0).Item("GRSS_WGHT_NC").ToString()
            EquipmentInfoMobileModel.EICPCTY_NC = dt(0).Item("CPCTY_NC").ToString()
            EquipmentInfoMobileModel.EILST_SRVYR_NM = dt(0).Item("LST_SRVYR_NM").ToString()
            EquipmentInfoMobileModel.EILST_TST_DT = dt(0).Item("LST_TST_DT").ToString()
            EquipmentInfoMobileModel.EILST_TST_TYP_ID = dt(0).Item("LST_TST_TYP_ID").ToString()
            EquipmentInfoMobileModel.EINXT_TST_DT = dt(0).Item("NXT_TST_DT").ToString()
            EquipmentInfoMobileModel.EINXT_TST_TYP_ID = dt(0).Item("NXT_TST_TYP_ID").ToString()
            EquipmentInfoMobileModel.EIRMRKS_VC = dt(0).Item("RMRKS_VC").ToString()
            EquipmentInfoMobileModel.EIACTV_BT = dt(0).Item("ACTV_BT").ToString()
            EquipmentInfoMobileModel.EIRNTL_BT = dt(0).Item("RNTL_BT").ToString()
            EquipmentInfoMobileModel.EIStatus = "Success"

            Return EquipmentInfoMobileModel



        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
            Dim EquipmentInfoMobileModel As New EquipmentInfoMobileModel
            EquipmentInfoMobileModel.EIStatus = ex.Message
            Return EquipmentInfoMobileModel
        End Try
    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function UpdateEquipmentInfo(ByVal EQPMNT_NO As String, ByVal EQPMNT_TYP_CD As String, ByVal MNFCTR_DT As String, ByVal TR_WGHT_NC As String,
                                        ByVal GRSS_WGHT_NC As String, ByVal CPCTY_NC As String, ByVal LST_SRVYR_NM As String, ByVal LST_TST_DT As String,
                                        ByVal LST_TST_TYP_ID As String, ByVal NXT_TST_DT As String, ByVal NXT_TST_TYP_ID As String, ByVal RMRKS_VC As String,
                                        ByVal ACTV_BT As String, ByVal RNTL_BT As String, ByVal PageName As String, ByVal GateinTransactionNo As String,
                                        ByVal Attachment As String, ByVal UserName As String) As EIResult

        Try

            gateInMobile.DepotID(UserName)
            Dim EquipmentInfoMobile_C As New EquipmentInfoMobile_C
            Dim dsEquipmentInformationData As EquipmentInformationDataSet = EquipmentInfoMobile_C.EquipmentInfoList(EQPMNT_NO, PageName, GateinTransactionNo, Attachment)



            Dim dsEquipmentInformationData1 As EquipmentInformationDataSet = EquipmentInfoMobile_C.pvt_HasChangeEquipmentInformation(dsEquipmentInformationData)
            Dim drAEquipmentInformation As DataRow()
            Dim objCommon As New CommonData

            drAEquipmentInformation = dsEquipmentInformationData1.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(EquipmentInformationData.ACTV_BT & "='True'")

            Dim dt() As DataRow = dsEquipmentInformationData1.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Select(String.Concat(EquipmentInformationData.EQPMNT_NO, "='", EQPMNT_NO, "'"))


            dt(0).Item("EQPMNT_NO") = EQPMNT_NO
            dt(0).Item("EQPMNT_TYP_CD") = EQPMNT_TYP_CD

            If MNFCTR_DT <> Nothing Then
                dt(0).Item("MNFCTR_DT") = CDate(MNFCTR_DT)
            Else
                dt(0).Item("MNFCTR_DT") = DBNull.Value
            End If

            If LST_TST_DT <> Nothing Then
                dt(0).Item("LST_TST_DT") = CDate(LST_TST_DT)
            Else
                dt(0).Item("LST_TST_DT") = DBNull.Value
            End If

            If NXT_TST_DT <> Nothing Then
                dt(0).Item("NXT_TST_DT") = CDate(NXT_TST_DT)
            Else
                dt(0).Item("NXT_TST_DT") = DBNull.Value
            End If

            If TR_WGHT_NC <> Nothing Then
                dt(0).Item("TR_WGHT_NC") = CDec(TR_WGHT_NC)
            Else
                dt(0).Item("TR_WGHT_NC") = DBNull.Value
            End If

            If GRSS_WGHT_NC <> Nothing Then
                dt(0).Item("GRSS_WGHT_NC") = CDec(GRSS_WGHT_NC)
            Else
                dt(0).Item("GRSS_WGHT_NC") = DBNull.Value
            End If

            If CPCTY_NC <> Nothing Then
                dt(0).Item("CPCTY_NC") = CDec(CPCTY_NC)
            Else
                dt(0).Item("CPCTY_NC") = DBNull.Value
            End If

            If LST_TST_TYP_ID <> Nothing Then
                dt(0).Item("LST_TST_TYP_ID") = CInt(LST_TST_TYP_ID)
            Else
                dt(0).Item("LST_TST_TYP_ID") = DBNull.Value
            End If

            If NXT_TST_TYP_ID <> Nothing Then
                dt(0).Item("NXT_TST_TYP_ID") = CInt(NXT_TST_TYP_ID)
            Else
                dt(0).Item("NXT_TST_TYP_ID") = DBNull.Value
            End If

            

            dt(0).Item("LST_SRVYR_NM") = LST_SRVYR_NM




            dt(0).Item("RMRKS_VC") = RMRKS_VC
            dt(0).Item("ACTV_BT") = ACTV_BT
            dt(0).Item("RNTL_BT") = RNTL_BT


            dsEquipmentInformationData1.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Clone()
            Dim objEquipmentInformation As New EquipmentInformation
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            'If Not dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL) Is Nothing AndAlso dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Rows.Count = 0 Then
            '    dtAttachmentDetail = objEquipmentInformation.pub_GetEquipmentInformationDetail().Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL)
            '    dsEquipmentInformationData.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION_DETAIL).Merge(dtAttachmentDetail)
            'End If
            Dim result As Boolean = objEquipmentInformation.pub_UpdateEquipmentInformation(dsEquipmentInformationData1, intDPT_ID, objCommon.GetCurrentUserName())
            dsEquipmentInformationData1.AcceptChanges()

            Dim strEquipno As String = EQPMNT_NO

            If strEquipno <> Nothing AndAlso strEquipno <> String.Empty Then
                Dim strRemarks As String = dsEquipmentInformationData1.Tables(EquipmentInformationData._V_EQUIPMENT_INFORMATION).Rows(0).Item(EquipmentInformationData.RMRKS_VC).ToString()

            End If

            Dim EIResult As New EIResult
            If result Then


                EIResult.Status = "EIUpdated"

            Else



                EIResult.Status = "EIError"

            End If


            Return EIResult


        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)

            Dim EIResult As New EIResult

            EIResult.Status = ex.Message

            Return EIResult
        End Try

    End Function

End Class