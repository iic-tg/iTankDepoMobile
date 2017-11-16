#Region " CustomerTariffCodes.vb"
'*********************************************************************************************************************
'Name :
'      CustomerTariffCodes.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(CustomerTariffCodes.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      10/10/2013 2:02:54 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "CustomerTariffCodes"

Public Class CustomerTariffCodes

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const CustomerTariffCodeselectQueryByTariffCode As String = "SELECT TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,ITM_ID,(SELECT ITM_CD FROM ITEM WHERE ITM_ID=TC.ITM_ID)AS ITM_CD,SB_ITM_ID,(SELECT SB_ITM_CD FROM SUB_ITEM WHERE SB_ITM_ID=TC.SB_ITM_ID)AS SB_ITM_CD,DMG_ID,(SELECT DMG_CD FROM DAMAGE WHERE DMG_ID=TC.DMG_ID)AS DMG_CD,RPR_ID,(SELECT RPR_CD FROM REPAIR WHERE RPR_ID=TC.RPR_ID)AS RPR_CD,MN_HR,MTRL_CST,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM TARIFF_CODE AS TC WHERE ( DPT_ID=@DPT_ID AND TRFF_CD_CD=@TRFF_CD_CD)"
    Private Const Tariff_CodeSelectQueryPk As String = "SELECT TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,ITM_ID,(SELECT ITM_CD FROM ITEM WHERE ITM_ID=TC.ITM_ID)AS ITM_CD,SB_ITM_ID,(SELECT SB_ITM_CD FROM SUB_ITEM WHERE SB_ITM_ID=TC.SB_ITM_ID)AS SB_ITM_CD,DMG_ID,(SELECT DMG_CD FROM DAMAGE WHERE DMG_ID=TC.DMG_ID)AS DMG_CD,RPR_ID,(SELECT RPR_CD FROM REPAIR WHERE RPR_ID=TC.RPR_ID)AS RPR_CD,MN_HR,MTRL_CST,RMRKS_VC,ACTV_BT,DPT_ID FROM TARIFF_CODE AS TC WHERE DPT_ID=@DPT_ID"
    Private Const Tariff_CodeInsertQuery As String = "INSERT INTO TARIFF_CODE(TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,ITM_ID,SB_ITM_ID,DMG_ID,RPR_ID,MN_HR,MTRL_CST,RMRKS_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@TRFF_CD_ID,@TRFF_CD_CD,@TRFF_CD_DESCRPTN_VC,@ITM_ID,@SB_ITM_ID,@DMG_ID,@RPR_ID,@MN_HR,@MTRL_CST,@RMRKS_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const CustomerTariff_CodeInsertQuery As String = "INSERT INTO TARIFF_CODE_HEADER (TRFF_CD_ID,TRFF_CD_TYP,TRFF_CD_EQP_TYP_ID,TRFF_CD_CSTMR_ID,TRFF_CD_AGNT_ID,TRFF_CD_CRTD_BY,TRFF_CD_CRTD_DT,TRFF_CD_MDFD_BY,TRFF_CD_MDFD_DT,ACTV_BT,DPT_ID) VALUES (@TRFF_CD_ID,@TRFF_CD_TYP,@TRFF_CD_EQP_TYP_ID,@TRFF_CD_CSTMR_ID,@TRFF_CD_AGNT_ID,@TRFF_CD_CRTD_BY,@TRFF_CD_CRTD_DT,@TRFF_CD_MDFD_BY,@TRFF_CD_MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const Tariff_CodeUpdateQuery As String = "UPDATE TARIFF_CODE SET TRFF_CD_CD=@TRFF_CD_CD, TRFF_CD_DESCRPTN_VC=@TRFF_CD_DESCRPTN_VC, ITM_ID=@ITM_ID, SB_ITM_ID=@SB_ITM_ID, DMG_ID=@DMG_ID, RPR_ID=@RPR_ID, MN_HR=@MN_HR, MTRL_CST=@MTRL_CST, RMRKS_VC=@RMRKS_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE TRFF_CD_ID=@TRFF_CD_ID"
    Private Const Tariff_CodeDeleteQuery As String = "DELETE FROM TARIFF_CODE WHERE TRFF_CD_ID=@TRFF_CD_ID"
    Private Const Tariff_GroupSelectQueryByTariffCode As String = "SELECT TRFF_GRP_DTL_ID,TRFF_CD_ID,TRFF_CD_CD,TRFF_CD_DESCRPTN_VC,TRFF_GRP_ID,TRFF_GRP_CD,TRFF_GRP_DESCRPTN_VC,RMRKS_VC,ACTV_BT FROM V_TARIFF_GROUP_DETAIL WHERE  ACTV_BT =1"
    Private Const Tariff_GroupDetailDeleteQuery As String = "DELETE FROM TARIFF_GROUP_DETAIL WHERE TRFF_CD_ID=@TRFF_CD_ID AND ACTV_BT = 0"
    Private Const Tariff_CodeSelectQueryByTariffCode As String = "SELECT  [TRFF_CD_DTL_ID] ,[TRFF_CD_ID] ,[TRFF_CD_DTL_CD] ,[TRFF_CD_DTL_DSC],[TRFF_CD_DTL_LCTN_CD] ,[TRFF_CD_DTL_LCTN_ID],[TRFF_CD_DTL_CMPNT_CD],[TRFF_CD_DTL_CMPNT_ID],[TRFF_CD_DTL_DMG_CD] ,[TRFF_CD_DTL_DMG_ID] ,[TRFF_CD_DTL_RPR_CD] ,[TRFF_CD_DTL_RPR_ID],[TRFF_CD_DTL_MTRL_CD],	[TRFF_CD_DTL_MTRL_ID],[TRFF_CD_DTL_MNHR] ,[TRFF_CD_DTL_MTRL_CST] ,[TRFF_CD_DTL_RMRKS] ,	[TRFF_CD_DTL_ACTV_BT] ,	[TRFF_CD_DTL_CRTD_BY],	[TRFF_CD_DTL_CRTD_DT],[TRFF_CD_DTL_MDFD_BY],[TRFF_CD_DTL_MDFD_DT],[DPT_ID] FROM V_TARIFF_CODE_DETAIL where TRFF_CD_ID=@TRFF_CD_ID"


    Private Const CustomerTariff_CodeUpdateQuery As String = "UPDATE TARIFF_CODE_HEADER SET  ACTV_BT=@ACTV_BT WHERE TRFF_CD_ID=@TRFF_CD_ID"
    Private Const CustomerTariffCodeDetailDeleteQuery As String = "DELETE FROM TARIFF_CODE_DETAIL WHERE TRFF_CD_ID=@TRFF_CD_ID"
    Private Const CustomerTariffCodeDetailnsertQuery As String = "INSERT INTO TARIFF_CODE_DETAIL (TRFF_CD_DTL_ID,TRFF_CD_ID,TRFF_CD_DTL_CD,TRFF_CD_DTL_DSC,TRFF_CD_DTL_LCTN_ID,TRFF_CD_DTL_CMPNT_ID,TRFF_CD_DTL_DMG_ID,TRFF_CD_DTL_RPR_ID,TRFF_CD_DTL_MTRL_ID,TRFF_CD_DTL_MNHR,TRFF_CD_DTL_MTRL_CST,TRFF_CD_DTL_RMRKS,TRFF_CD_DTL_ACTV_BT,TRFF_CD_DTL_CRTD_BY ,TRFF_CD_DTL_CRTD_DT,TRFF_CD_DTL_MDFD_BY,TRFF_CD_DTL_MDFD_DT,DPT_ID) VALUES (@TRFF_CD_DTL_ID,@TRFF_CD_ID,@TRFF_CD_DTL_CD,@TRFF_CD_DTL_DSC,@TRFF_CD_DTL_LCTN_ID,@TRFF_CD_DTL_CMPNT_ID,@TRFF_CD_DTL_DMG_ID,@TRFF_CD_DTL_RPR_ID,@TRFF_CD_DTL_MTRL_ID,@TRFF_CD_DTL_MNHR,@TRFF_CD_DTL_MTRL_CST,@TRFF_CD_DTL_RMRKS,@TRFF_CD_DTL_ACTV_BT,@TRFF_CD_DTL_CRTD_BY,@TRFF_CD_DTL_CRTD_DT,@TRFF_CD_DTL_MDFD_BY,@TRFF_CD_DTL_MDFD_DT,@DPT_ID)"
    Private Const Tariff_CodeSelectQueryByAllCode As String = "select * from Tariff_code_header where TRFF_CD_TYP=@TRFF_CD_TYP and TRFF_CD_EQP_TYP_ID=@TRFF_CD_EQP_TYP_ID and TRFF_CD_CSTMR_ID=@TRFF_CD_CSTMR_ID and TRFF_CD_AGNT_ID=@TRFF_CD_AGNT_ID"

    Private ds As CustomerTariffCodeDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New CustomerTariffCodeDataSet
    End Sub

#End Region

    '#Region "GetTariffCodeBy() TABLE NAME:Tariff_Code"

    '    Public Function GetTariffCodeByDepotID(ByVal bv_DepotID As Int64) As TariffCodeDataSet
    '        Try
    '            objData = New DataObjects(Tariff_CodeSelectQueryPk, TariffCodeData.DPT_ID, CStr(bv_DepotID))
    '            objData.Fill(CType(ds, DataSet), TariffCodeData._TARIFF_CODE)
    '            Return ds
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function
    '#End Region

#Region "GetTariffCodeByTariffCodeCode() TABLE NAME:Sub_Item"

    Public Function GetTariffCodeByTariffCodeCode(ByVal bv_strTariffCodeCode As String, ByVal bv_intDepotId As Int64) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(TariffCodeData.DPT_ID, bv_intDepotId)
            hshParameters.Add(TariffCodeData.TRFF_CD_CD, bv_strTariffCodeCode)
            objData = New DataObjects(CustomerTariffCodeselectQueryByTariffCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateCustomerTariffCode() TABLE NAME:TARIFF_CODE_HEADER"

    Public Function CreateCustomerTariffCode(ByVal bv_i64TRFF_CD_TYP As String, _
                                    ByVal bv_i64TRFF_CD_EQP_TYP_ID As Int64, _
                                    ByVal bv_i64GTRFF_CD_CSTMR_ID As Int64, _
                                    ByVal bv_i64GTRFF_CD_AGNT_ID As Int64, _
                                    ByVal bv_blnACTV_BT As Boolean, _
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
            dr = ds.Tables(CustomerTariffCodeData._TARIFF_CODE_HEADER).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerTariffCodeData._TARIFF_CODE_HEADER, br_objTrans) + 1

                .Item(CustomerTariffCodeData.TRFF_CD_ID) = intMax
                .Item(CustomerTariffCodeData.TRFF_CD_TYP) = bv_i64TRFF_CD_TYP
                .Item(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID) = bv_i64TRFF_CD_EQP_TYP_ID
                .Item(CustomerTariffCodeData.TRFF_CD_CSTMR_ID) = bv_i64GTRFF_CD_CSTMR_ID
                .Item(CustomerTariffCodeData.ACTV_BT) = bv_blnACTV_BT
                .Item(CustomerTariffCodeData.TRFF_CD_AGNT_ID) = bv_i64GTRFF_CD_AGNT_ID
                .Item(CustomerTariffCodeData.TRFF_CD_CRTD_BY) = bv_strCreatedBy
                .Item(CustomerTariffCodeData.TRFF_CD_CRTD_DT) = bv_datCreatedDate
                .Item(CustomerTariffCodeData.TRFF_CD_MDFD_BY) = bv_strModifiedBy
                .Item(CustomerTariffCodeData.TRFF_CD_MDFD_DT) = bv_datModifiedDate
                .Item(CustomerTariffCodeData.DPT_ID) = bv_i32DepotId
            End With
            objData.InsertRow(dr, CustomerTariff_CodeInsertQuery, br_objTrans)
            dr = Nothing
            CreateCustomerTariffCode = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    '#Region "GetTariffGroupCodeByDepot() TABLE NAME:Tariff_Code"

    '    Public Function GetTariffGroupCodeByDepot(ByVal bv_intTariffCodeID As Integer, ByVal bv_blnActive As Boolean) As TariffCodeDataSet
    '        Try
    '            Dim hshConfiguration As New Hashtable()
    '            hshConfiguration.Add(TariffCodeData.TRFF_CD_ID, bv_intTariffCodeID)
    '            hshConfiguration.Add(TariffCodeData.ACTV_BT, bv_blnActive)
    '            objData = New DataObjects(Tariff_GroupSelectQueryByTariffCode, hshConfiguration)
    '            objData.Fill(CType(ds, DataSet), TariffCodeData._V_TARIFF_GROUP_DETAIL)
    '            Return ds
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '#End Region

#Region "DeleteTariffCodeByTariffId() Table Name: TARIFF_CODE_DETAIL"

    Public Function DeleteTariffCodeByTariffId(ByVal bv_lngProductId As Long, _
                                                             ByRef br_objTransaction As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CustomerTariffCodeData._TARIFF_CODE_DETAIL).NewRow()
            With dr
                .Item(CustomerTariffCodeData.TRFF_CD_ID) = bv_lngProductId
            End With
            DeleteTariffCodeByTariffId = objData.DeleteRow(dr, CustomerTariffCodeDetailDeleteQuery, br_objTransaction)
            dr = Nothing
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "CreateCustomerTariffCodeDetail() TABLE NAME: TARIFF_CODE_DETAIL"
    Public Function CreateCustomerTariffCodeDetail(ByVal bv_lngTRFF_CD_ID As Long, _
                                                     ByVal TRFF_CD_DTL_CD As String,
                                                     ByVal TRFF_CD_DTL_DSC As String,
                                                     ByVal TRFF_CD_DTL_LCTN_ID As Int64,
                                                     ByVal TRFF_CD_DTL_CMPNT_ID As Int64,
                                                     ByVal TRFF_CD_DTL_DMG_ID As Int64,
                                                     ByVal TRFF_CD_DTL_RPR_ID As Int64,
                                                     ByVal TRFF_CD_DTL_MTRL_ID As Int64,
                                                     ByVal TRFF_CD_DTL_MNHR As Decimal,
                                                     ByVal TRFF_CD_DTL_MTRL_CST As Decimal,
                                                     ByVal TRFF_CD_DTL_RMRKS As String,
                                                     ByVal TRFF_CD_DTL_ACTV_BT As Boolean,
                                                     ByVal TRFF_CD_DTL_CRTD_BY As String,
                                                     ByVal TRFF_CD_DTL_CRTD_DT As DateTime,
                                                     ByVal TRFF_CD_DTL_MDFD_BY As String,
                                                     ByVal TRFF_CD_DTL_MDFD_DT As DateTime,
                                                     ByVal DPT_ID As Int64,
                                                     ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CustomerTariffCodeData._TARIFF_CODE_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CustomerTariffCodeData._TARIFF_CODE_DETAIL, br_objTransaction) '+ 1
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_ID) = intMax
                .Item(CustomerTariffCodeData.TRFF_CD_ID) = bv_lngTRFF_CD_ID
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_CD) = TRFF_CD_DTL_CD
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_DSC) = TRFF_CD_DTL_DSC
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_LCTN_ID) = TRFF_CD_DTL_LCTN_ID

                If TRFF_CD_DTL_CMPNT_ID <> Nothing Then
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID) = TRFF_CD_DTL_CMPNT_ID
                Else
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_CMPNT_ID) = DBNull.Value
                End If


                .Item(CustomerTariffCodeData.TRFF_CD_DTL_DMG_ID) = TRFF_CD_DTL_DMG_ID
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_RPR_ID) = TRFF_CD_DTL_RPR_ID
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_ID) = TRFF_CD_DTL_MTRL_ID
                If Not IsDBNull(TRFF_CD_DTL_MNHR) Then
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_MNHR) = TRFF_CD_DTL_MNHR
                Else
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_MNHR) = DBNull.Value
                End If
                If Not IsDBNull(TRFF_CD_DTL_MTRL_CST) Then
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST) = TRFF_CD_DTL_MTRL_CST
                Else
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_MTRL_CST) = DBNull.Value
                End If
                If Not IsDBNull(TRFF_CD_DTL_RMRKS) Then
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS) = TRFF_CD_DTL_RMRKS
                Else
                    .Item(CustomerTariffCodeData.TRFF_CD_DTL_RMRKS) = DBNull.Value
                End If
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_ACTV_BT) = TRFF_CD_DTL_ACTV_BT
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_CRTD_BY) = TRFF_CD_DTL_CRTD_BY
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_CRTD_DT) = TRFF_CD_DTL_CRTD_DT
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_MDFD_BY) = TRFF_CD_DTL_MDFD_BY
                .Item(CustomerTariffCodeData.TRFF_CD_DTL_MDFD_DT) = TRFF_CD_DTL_MDFD_DT
                .Item(CustomerTariffCodeData.DPT_ID) = DPT_ID
            End With
            objData.InsertRow(dr, CustomerTariffCodeDetailnsertQuery, br_objTransaction)
            dr = Nothing
            CreateCustomerTariffCodeDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "UpdateCustomerTariffCode() TABLE NAME:Tariff_Code_Header"

    Public Function UpdateCustomerTariffCode(ByVal bv_i64TariffID As Int64, _
                                            ByVal bv_blnActive As Boolean, _
                                            ByVal bv_i32DepotId As Int32, _
                                            ByRef br_objTrans As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CustomerTariffCodeData._TARIFF_CODE_HEADER).NewRow()
            With dr
                .Item(CustomerTariffCodeData.TRFF_CD_ID) = bv_i64TariffID
                .Item(CustomerTariffCodeData.ACTV_BT) = bv_blnActive
                .Item(CustomerTariffCodeData.DPT_ID) = bv_i32DepotId
            End With
            UpdateCustomerTariffCode = objData.UpdateRow(dr, CustomerTariff_CodeUpdateQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "DeleteTariff_Code() TABLE NAME:Tariff_Code"

    Public Function DeleteTariff_Code(ByVal bv_intTariffCodeID As Int64, ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TariffCodeData._TARIFF_CODE).NewRow()
            With dr
                .Item(TariffCodeData.TRFF_CD_ID) = bv_intTariffCodeID
            End With
            DeleteTariff_Code = objData.DeleteRow(dr, Tariff_CodeDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTariff_Group_Detail() TABLE NAME:Tariff_Group_Detail"

    Public Function DeleteTariff_Group_Detail(ByVal bv_intTariffGroupCodeID As Int64, ByRef br_objTrans As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TariffCodeData._TARIFF_CODE).NewRow()
            With dr
                .Item(TariffCodeData.TRFF_CD_ID) = bv_intTariffGroupCodeID
            End With
            DeleteTariff_Group_Detail = objData.DeleteRow(dr, Tariff_GroupDetailDeleteQuery, br_objTrans)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    '#Region "GetTariffGroupCodeByDepot() TABLE NAME:Tariff_Code"

    '    Public Function GetTariffGroupCodeByDepot(ByVal bv_intTariffCodeID As Integer, ByVal bv_blnActive As Boolean, ByRef br_objTrans As Transactions) As TariffCodeDataSet
    '        Try
    '            Dim hshConfiguration As New Hashtable()
    '            hshConfiguration.Add(TariffCodeData.TRFF_CD_ID, bv_intTariffCodeID)
    '            hshConfiguration.Add(TariffCodeData.ACTV_BT, bv_blnActive)
    '            objData = New DataObjects(Tariff_GroupSelectQueryByTariffCode, hshConfiguration)
    '            objData.Fill(CType(ds, DataSet), TariffCodeData._V_TARIFF_GROUP_DETAIL, br_objTrans)
    '            Return ds
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function


    '#End Region
#Region "GetCustomerTariffCodeDetailByDepotID() TABLE NAME:Tariff_Code"


    Public Function GetCustomerTariffCodeDetailByDepotID(ByVal bv_intDepotID As Integer) As CustomerTariffCodeDataSet
        Try
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(CustomerTariffCodeData.TRFF_CD_ID, bv_intDepotID)
            objData = New DataObjects(Tariff_GroupSelectQueryByTariffCode, hshConfiguration)
            objData.Fill(CType(ds, DataSet), CustomerTariffCodeData._V_TARIFF_CODE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCustomerTariffCodeDetailByTariffID() TABLE NAME:Tariff_Code_Header,Tariff_Code_Detail"


    Public Function GetCustomerTariffCodeDetailByTariffID(ByVal bv_intTariffID As Integer) As CustomerTariffCodeDataSet
        Try
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(CustomerTariffCodeData.TRFF_CD_ID, bv_intTariffID)
            objData = New DataObjects(Tariff_CodeSelectQueryByTariffCode, hshConfiguration)
            objData.Fill(CType(ds, DataSet), CustomerTariffCodeData._V_TARIFF_CODE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCustomerTariffCodeHeaderByTariffIDEqpmntId() TABLE NAME:Tariff_Code_Header,Tariff_Code_Detail"


    Public Function GetCustomerTariffCodeHeaderByTariffIDEqpmntId(ByVal bv_i64TRFF_CD_TYP As String, _
                                      ByVal bv_i64TRFF_CD_EQP_TYP_ID As Long, _
                                      ByVal bv_i64GTRFF_CD_CSTMR_ID As Long, _
                                      ByVal bv_i64GTRFF_CD_AGNT_ID As Long) As CustomerTariffCodeDataSet
        Try
            Dim hshConfiguration As New Hashtable()
            hshConfiguration.Add(CustomerTariffCodeData.TRFF_CD_TYP, bv_i64TRFF_CD_TYP)
            hshConfiguration.Add(CustomerTariffCodeData.TRFF_CD_EQP_TYP_ID, bv_i64TRFF_CD_EQP_TYP_ID)
            hshConfiguration.Add(CustomerTariffCodeData.TRFF_CD_CSTMR_ID, bv_i64GTRFF_CD_CSTMR_ID)
            hshConfiguration.Add(CustomerTariffCodeData.TRFF_CD_AGNT_ID, bv_i64GTRFF_CD_AGNT_ID)
            objData = New DataObjects(Tariff_CodeSelectQueryByAllCode, hshConfiguration)
            objData.Fill(CType(ds, DataSet), CustomerTariffCodeData._TARIFF_CODE_HEADER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class

#End Region

