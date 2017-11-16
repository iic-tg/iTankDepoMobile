Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

Public Class EDISettings

#Region "Declaration Part.. "

    Dim objData As DataObjects

    Private Const selectEDISettingQuery As String = "SELECT EDI_STTNGS_ID ,CSTMR_CD ,CSTMR_ID ,TO_EML ,CC_EML ,NXT_RN_DT_TM ,FLE_FRMT_CD ,FLE_FRMT_ID ,ACTV_BT ,CRTD_BY ,CRTD_DT,SBJCT_VCR,GNRTN_TM,DPT_ID,DPT_CD FROM V_EDI_SETTINGS WHERE ACTV_BT=1"
    ' Private Const selectEDISettingQuery As String = "SELECT CSTMR_ID,CSTMR_CD,EDI_FRMT,EDI_FRMT_CD,TO_EML_ID,GNRTN_FRMT,GNRTN_FRMT_CD,GNRTN_TM,DPT_ID,DPT_CD,LST_RN FROM V_EDI_SETTING_UI WHERE ACTV_BT=1"
    'Private Const UpdateCustomerEDISettingQuery As String = "UPDATE CUSTOMER_EDI_SETTING SET LST_RN=@LST_RN WHERE CSTMR_ID=@CSTMR_ID"
    Private Const UpdateCustomerEDISettingQuery As String = "UPDATE CUSTOMER_EDI_SETTING SET LST_RN=@LST_RN WHERE CSTMR_ID=@CSTMR_ID"
    Private Const UpdateGenaratedTimeEDISettingQuery As String = "UPDATE EDI_SETTINGS SET NXT_RN_DT_TM=@NXT_RN_DT_TM WHERE CSTMR_ID=@CSTMR_ID"
    Private ds As EDIDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New EDIDataSet
    End Sub

#End Region

#Region "GetCustomerEmailFormat"
    Public Function GetCustomerEmailFormat() As EDIDataSet
        Try
            objData = New DataObjects(selectEDISettingQuery)
            objData.Fill(CType(ds, DataSet), EDIData._V_EDI_SETTINGS)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateNextRunTime() TABLE NAME:CUSTOMER_EDI_SETTING"

    Public Function UpdateNextRunTime(ByVal bv_intCSMR_ID As Integer, _
                                      ByVal bv_dtLST_RN As DateTime, _
                                      ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(EDIData._EDI_SETTINGS).NewRow()
            With dr
                .Item(EDIData.CSTMR_ID) = bv_intCSMR_ID
                '  .Item(EDISettingData.LST_RN) = bv_dtLST_RN
                .Item(EDIData.NXT_RN_DT_TM) = bv_dtLST_RN
            End With
            'UpdateNextRunTime = objData.UpdateRow(dr, UpdateCustomerEDISettingQuery)
            UpdateNextRunTime = objData.UpdateRow(dr, UpdateGenaratedTimeEDISettingQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class