Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class EquipmentHistoryMobile
    Inherits System.Web.Services.WebService


    Dim arraylist As New ArrayList
    Dim gateInMobile As New GateinMobile_C
    Dim dsEquipmentHistory As New EquipmentHistoryDataSet
    Dim EHEqupValidate As New EHEqupValidate

    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function EquipmentHistoryList(ByVal equipmentNo As String, ByVal UserName As String) As EqupmHisList

        gateInMobile.DepotID(UserName)

        Dim objCommonConfig As New ConfigSetting()
        Dim objcommon As New CommonData()
        Dim strEqType As String = String.Empty
        Dim strEqCode As String = String.Empty
        dsEquipmentHistory.Tables.Clear()
        Dim objcommonData As New CommonData
        Dim objEquipmentHistory As New EquipmentHistory
        Dim intDepotID As Integer = CInt(objcommonData.GetDepotID())
        Dim equpmHisList As New EqupmHisList
        Try


            dsEquipmentHistory = objEquipmentHistory.pub_GetEquipmentHistory(equipmentNo, intDepotID)





            If dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows.Count > 0 Then


                With dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0)
                    If Not IsDBNull(.Item(EquipmentHistoryData.EQPMNT_TYP_CD)) Then
                        equpmHisList.strEqType = CStr(.Item(EquipmentHistoryData.EQPMNT_TYP_CD))
                    Else
                        equpmHisList.strEqType = "NA"
                    End If
                    If Not IsDBNull(.Item(EquipmentHistoryData.EQPMNT_CD_CD)) Then
                        equpmHisList.strEqCode = CStr(.Item(EquipmentHistoryData.EQPMNT_CD_CD))
                    Else
                        equpmHisList.strEqCode = "NA"
                    End If


                End With


                Dim dtEquipmentHistory As DataTable = dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY)



                For Each dt As DataRow In dtEquipmentHistory.Rows

                    Dim EquipmentHistoryModel As New EquipmentHistoryModel

                    EquipmentHistoryModel.TRCKNG_ID = dt.Item("TRCKNG_ID").ToString()
                    EquipmentHistoryModel.CSTMR_ID = dt.Item("CSTMR_ID").ToString()
                    EquipmentHistoryModel.CSTMR_CD = dt.Item("CSTMR_CD").ToString()
                    EquipmentHistoryModel.EQPMNT_NO = dt.Item("EQPMNT_NO").ToString()
                    EquipmentHistoryModel.ACTVTY_NAM = dt.Item("ACTVTY_NAM").ToString()
                    EquipmentHistoryModel.EQPMNT_STTS_ID = dt.Item("EQPMNT_STTS_ID").ToString()
                    EquipmentHistoryModel.EQPMNT_STTS_CD = dt.Item("EQPMNT_STTS_CD").ToString()
                    EquipmentHistoryModel.ACTVTY_NO = dt.Item("ACTVTY_NO").ToString()
                    EquipmentHistoryModel.ACTVTY_DT = dt.Item("ACTVTY_DT").ToString()
                    EquipmentHistoryModel.ACTVTY_RMRKS = dt.Item("ACTVTY_RMRKS").ToString()
                    EquipmentHistoryModel.GI_TRNSCTN_NO = dt.Item("GI_TRNSCTN_NO").ToString()
                    EquipmentHistoryModel.INVCNG_PRTY_ID = dt.Item("INVCNG_PRTY_ID").ToString()
                    EquipmentHistoryModel.INVCNG_PRTY_CD = dt.Item("INVCNG_PRTY_CD").ToString()
                    EquipmentHistoryModel.PRDCT_ID = dt.Item("PRDCT_ID").ToString()
                    EquipmentHistoryModel.EIR_NO = dt.Item("EIR_NO").ToString()
                    EquipmentHistoryModel.RF_NO = dt.Item("RF_NO").ToString()
                    EquipmentHistoryModel.PRDCT_DSCRPTN_VC = dt.Item("PRDCT_DSCRPTN_VC").ToString()
                    EquipmentHistoryModel.INVC_GNRTN = dt.Item("INVC_GNRTN").ToString()
                    EquipmentHistoryModel.CRTD_BY = dt.Item("CRTD_BY").ToString()
                    EquipmentHistoryModel.CRTD_DT = dt.Item("CRTD_DT").ToString()
                    EquipmentHistoryModel.CNCLD_BY = dt.Item("CNCLD_BY").ToString()
                    EquipmentHistoryModel.CNCLD_DT = dt.Item("CNCLD_DT").ToString()
                    EquipmentHistoryModel.ADT_RMRKS = dt.Item("ADT_RMRKS").ToString()
                    EquipmentHistoryModel.DPT_ID = dt.Item("DPT_ID").ToString()
                    EquipmentHistoryModel.YRD_LCTN = dt.Item("YRD_LCTN").ToString()
                    EquipmentHistoryModel.EQPMNT_TYP_ID = dt.Item("EQPMNT_TYP_ID").ToString()
                    EquipmentHistoryModel.EQPMNT_TYP_CD = dt.Item("EQPMNT_TYP_CD").ToString()
                    EquipmentHistoryModel.EQPMNT_CD_ID = dt.Item("EQPMNT_CD_ID").ToString()
                    EquipmentHistoryModel.EQPMNT_CD_CD = dt.Item("EQPMNT_CD_CD").ToString()
                    EquipmentHistoryModel.RMRKS_VC = dt.Item("RMRKS_VC").ToString()
                    EquipmentHistoryModel.PRDCT_CD = dt.Item("PRDCT_CD").ToString()
                    EquipmentHistoryModel.RNTL_CSTMR_ID = dt.Item("RNTL_CSTMR_ID").ToString()
                    EquipmentHistoryModel.RNTL_RFRNC_NO = dt.Item("RNTL_RFRNC_NO").ToString()
                    EquipmentHistoryModel.AGNT_CD = dt.Item("AGNT_CD").ToString()
                    'EquipmentHistoryModel.STTS_ID = dt.Item("STTS_ID").ToString()
                    EquipmentHistoryModel.CSTMR_NAM = dt.Item("CSTMR_NAM").ToString()
                    EquipmentHistoryModel.ADDTNL_CLNNG_BT = dt.Item("ADDTNL_CLNNG_BT").ToString()
                    EquipmentHistoryModel.DPT_CD = dt.Item("DPT_CD").ToString()


                    arraylist.Add(EquipmentHistoryModel)

                Next



               



            End If

            equpmHisList.List = arraylist
            equpmHisList.status = "Success"


            Return equpmHisList

        Catch ex As Exception
            ' Dim equpmHisList As New EqupmHisList
            equpmHisList.status = ex.Message

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                   MethodBase.GetCurrentMethod.Name, ex.Message)

            Return equpmHisList

        End Try







    End Function


    'Equipment is mapped to another customer (or) Equipment is already deleted  from this activity.


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function validateEquipmentDelete(ByVal bv_strEquipmentNO As String, ByVal bv_strGI_Trnsctn_NO As String, ByVal UserName As String) As EHEqupValidate


        Try
            gateInMobile.DepotID(UserName)
            Dim objEqUpdates As New EquipmentUpdate
            Dim strTrackingId As String
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())


            strTrackingId = objEqUpdates.Validate_Histroy_Delete(bv_strEquipmentNO, bv_strGI_Trnsctn_NO, intDepotID)

            If (Not String.IsNullOrEmpty(strTrackingId) AndAlso Not String.IsNullOrWhiteSpace(strTrackingId)) Then

                EHEqupValidate.status = "Success"
            Else
                EHEqupValidate.status = "Equipment is mapped to another customer (or) Equipment is already deleted  from this activity."
            End If

            Return EHEqupValidate

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)

            EHEqupValidate.status = ex.Message

            Return EHEqupValidate

        End Try

    End Function



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function DeleteActivity(ByVal bv_strTrackingID As String, _
                                           ByVal bv_strActivityName As String, _
                                           ByVal bv_strRemarks As String, ByVal equipmentNo As String, ByVal UserName As String) As EHEqupValidate


        Try
            gateInMobile.DepotID(UserName)
            Dim objCommondata As New CommonData
            Dim intDepotID As Integer = CInt(objCommondata.GetDepotID())
            Dim objEquipmentHistory As New EquipmentHistory
            Dim strCode As String = String.Empty

            dsEquipmentHistory = objEquipmentHistory.pub_GetEquipmentHistory(equipmentNo, intDepotID)
            'dsEquipmentHistory = CType(RetrieveData(EQUIPMENT_HISTORY), EquipmentHistoryDataSet)

            If Trim(bv_strRemarks).Length > 0 Then
                objEquipmentHistory.pub_DeleteEquipmentActivity(dsEquipmentHistory, bv_strTrackingID, bv_strActivityName, bv_strRemarks, _
                                                                objCommondata.GetCurrentUserName(), CDate(objCommondata.GetCurrentDate()), _
                                                                intDepotID)

                If dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows.Count > 0 Then
                    If Not IsDBNull(dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.EQPMNT_STTS_CD)) Then
                        strCode = String.Concat("Activity : ", dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.EQPMNT_STTS_CD))
                    Else
                        strCode = String.Concat("Activity Name : ", dsEquipmentHistory.Tables(EquipmentHistoryData._V_EQUIPMENT_HISTORY).Rows(0).Item(EquipmentHistoryData.ACTVTY_NAM))
                    End If
                    Dim strMsg As String = String.Concat(strCode, " has been deleted from Equipment History")
                    EHEqupValidate.status = strMsg

                    Return EHEqupValidate

                    Exit Function
                End If
            Else
                EHEqupValidate.status = "Provide Remarks"
            End If


            Return EHEqupValidate

        Catch ex As Exception
            EHEqupValidate.status = ex.Message
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)

            Return EHEqupValidate
        End Try

    End Function



End Class