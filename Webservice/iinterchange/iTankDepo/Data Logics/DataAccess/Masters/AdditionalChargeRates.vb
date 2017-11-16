Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
Public Class AdditionalChargeRates
#Region "Declaration Part.. "

    Dim objData As DataObjects
    
    Private Const V_ADDITIONAL_CHARGE_RATESelectQueryByDepotId As String = "SELECT ADDTNL_CHRG_RT_ID,OPRTN_TYP_ID,OPRTN_TYP_CD,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC,DFLT_BT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ADDITIONAL_CHARGE_RATE WHERE DPT_ID=@DPT_ID"
    Private Const Additional_Charge_RateInsertQuery As String = "INSERT INTO ADDITIONAL_CHARGE_RATE(ADDTNL_CHRG_RT_ID,OPRTN_TYP_ID,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC,DFLT_BT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID)VALUES(@ADDTNL_CHRG_RT_ID,@OPRTN_TYP_ID,@ADDTNL_CHRG_RT_CD,@ADDTNL_CHRG_RT_DSCRPTN_VC,@RT_NC,@DFLT_BT,@ACTV_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID)"
    Private Const Additional_Charge_RateUpdateQuery As String = "UPDATE ADDITIONAL_CHARGE_RATE SET OPRTN_TYP_ID=@OPRTN_TYP_ID, ADDTNL_CHRG_RT_CD=@ADDTNL_CHRG_RT_CD, ADDTNL_CHRG_RT_DSCRPTN_VC=@ADDTNL_CHRG_RT_DSCRPTN_VC, RT_NC=@RT_NC, DFLT_BT=@DFLT_BT, ACTV_BT=@ACTV_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT WHERE ADDTNL_CHRG_RT_ID=@ADDTNL_CHRG_RT_ID AND  DPT_ID=@DPT_ID"
    Private Const GetChargeCodeByChargeCodeSelectQuery As String = "SELECT ADDTNL_CHRG_RT_ID,OPRTN_TYP_ID,OPRTN_TYP_CD,ADDTNL_CHRG_RT_CD,ADDTNL_CHRG_RT_DSCRPTN_VC,RT_NC,DFLT_BT,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID FROM V_ADDITIONAL_CHARGE_RATE WHERE (DPT_ID=@DPT_ID AND ADDTNL_CHRG_RT_CD=@ADDTNL_CHRG_RT_CD)"
    Private ds As AdditionalChargeRateDataSet
#End Region

#Region "Constructor.."
    Sub New()
        ds = New AdditionalChargeRateDataSet
    End Sub
#End Region

#Region "GetChargeCodeByChargeCode()"

    Public Function GetChargeCodeByChargeCode(ByVal bv_strChargeCode As String, ByVal bv_intDepotId As Int64) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(AdditionalChargeRateData.DPT_ID, bv_intDepotId)
            hshParameters.Add(AdditionalChargeRateData.ADDTNL_CHRG_RT_CD, bv_strChargeCode)
            objData = New DataObjects(GetChargeCodeByChargeCodeSelectQuery, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetVAdditionalChargeRateBy() TABLE NAME:V_ADDITIONAL_CHARGE_RATE"

    Public Function pub_GetAdditionlChargesByDepotId(ByVal bv_intDepotId As Integer) As AdditionalChargeRateDataSet
        Try
            objData = New DataObjects(V_ADDITIONAL_CHARGE_RATESelectQueryByDepotId, AdditionalChargeRateData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), AdditionalChargeRateData._V_ADDITIONAL_CHARGE_RATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateAddtionalCharge() TABLE NAME:Additional_Charge_Rate"

    Public Function CreateAddtionalCharge(ByVal bv_i64OperationId As Int64, _
        ByVal bv_strAdditionalChargeRateCode As String, _
        ByVal bv_strAdditionalChargeRateDescription As String, _
        ByVal bv_dblRate As Double, _
        ByVal bv_blnDefaultBit As Boolean, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotId As Int32, _
        ByRef br_objTrans As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(AdditionalChargeRateData._ADDITIONAL_CHARGE_RATE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(AdditionalChargeRateData._ADDITIONAL_CHARGE_RATE, br_objTrans)
                .Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_ID) = intMax
                .Item(AdditionalChargeRateData.OPRTN_TYP_ID) = bv_i64OperationId
                .Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_CD) = bv_strAdditionalChargeRateCode
                .Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_DSCRPTN_VC) = bv_strAdditionalChargeRateDescription
                .Item(AdditionalChargeRateData.RT_NC) = bv_dblRate
                .Item(AdditionalChargeRateData.DFLT_BT) = bv_blnDefaultBit
                .Item(AdditionalChargeRateData.ACTV_BT) = bv_blnActiveBit
                .Item(AdditionalChargeRateData.CRTD_BY) = bv_strCreatedBy
                .Item(AdditionalChargeRateData.CRTD_DT) = bv_datCreatedDate
                .Item(AdditionalChargeRateData.MDFD_BY) = bv_strModifiedBy
                .Item(AdditionalChargeRateData.MDFD_DT) = bv_datModifiedDate
                .Item(AdditionalChargeRateData.DPT_ID) = bv_i32DepotId
            End With
            objData.InsertRow(dr, Additional_Charge_RateInsertQuery, br_objTrans)
            dr = Nothing
            CreateAddtionalCharge = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateAdditionalChargeRate() TABLE NAME:Additional_Charge_Rate"

    Public Function UpdateAdditionalChargeRate(ByVal bv_i64AdditionalChargeRateId As Int64, _
        ByVal bv_i64OperationId As Int64, _
        ByVal bv_strAdditionalChargeRateCode As String, _
        ByVal bv_strAdditionalChargeRateDescription As String, _
        ByVal bv_dblRate As Double, _
        ByVal bv_blnDefaultBit As Boolean, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_i32DepotId As Int32, _
        ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(AdditionalChargeRateData._ADDITIONAL_CHARGE_RATE).NewRow()
            With dr
                .Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_ID) = bv_i64AdditionalChargeRateId
                .Item(AdditionalChargeRateData.OPRTN_TYP_ID) = bv_i64OperationId
                .Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_CD) = bv_strAdditionalChargeRateCode
                .Item(AdditionalChargeRateData.ADDTNL_CHRG_RT_DSCRPTN_VC) = bv_strAdditionalChargeRateDescription
                .Item(AdditionalChargeRateData.RT_NC) = bv_dblRate
                .Item(AdditionalChargeRateData.DFLT_BT) = bv_blnDefaultBit
                .Item(AdditionalChargeRateData.ACTV_BT) = bv_blnActiveBit
                .Item(AdditionalChargeRateData.MDFD_BY) = bv_strModifiedBy
                .Item(AdditionalChargeRateData.MDFD_DT) = bv_datModifiedDate
                .Item(AdditionalChargeRateData.DPT_ID) = bv_i32DepotId
            End With
            UpdateAdditionalChargeRate = objData.UpdateRow(dr, Additional_Charge_RateUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
