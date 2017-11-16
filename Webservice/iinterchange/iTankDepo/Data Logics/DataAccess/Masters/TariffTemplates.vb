#Region " TariffTemplates.vb"
'*********************************************************************************************************************
'Name :
'      TariffTemplates.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(TariffTemplates.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      7/18/2013 4:02:32 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities

#Region "TariffTemplates"

Public Class TariffTemplates

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const Tariff_TemplateSelectQueryPk As String = "SELECT TRFF_TMPLT_ID,CSTMR_ID,CSTMR_CD,EQPMNT_SZ,EQPMNT_TYP,ACTV_BT,DPT_ID FROM V_Tariff_Template WHERE DPT_ID=@DPT_ID"
    Private Const Tariff_TemplateInsertQuery As String = "INSERT INTO TARIFF_TEMPLATE(TRFF_TMPLT_ID,EQPMNT_SZ,EQPMNT_TYP,CSTMR_ID,ACTV_BT,DPT_ID)VALUES(@TRFF_TMPLT_ID,@EQPMNT_SZ,@EQPMNT_TYP,@CSTMR_ID,@ACTV_BT,@DPT_ID)"
    Private Const Tariff_TemplateUpdateQuery As String = "UPDATE Tariff_Template SET CSTMR_ID=@CSTMR_ID,TRFF_TMPLT_ID=@TRFF_TMPLT_ID, EQPMNT_SZ=@EQPMNT_SZ, EQPMNT_TYP=@EQPMNT_TYP, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE TRFF_TMPLT_ID=@TRFF_TMPLT_ID AND DPT_ID=@DPT_ID"
    Private Const Tariff_TemplateDeleteQuery As String = "DELETE FROM Tariff_Template WHERE TRFF_TMPLT_ID=@TRFF_TMPLT_ID"
    Private Const TariffSelectQueryPk As String = "SELECT TRFF_ID,TRFF_CD,TRFF_DSCRPTN_VC,EQPMNT_SZ,EQPMNT_TYP,CMP_CD,CMP_DSCRPTN_VC,RPR_CD,RPR_DSCRPTN_VC,LCTN_CD_1,LCTN_CD_2,LCTN_CD_3,LCTN_DSCRPTN_VC,DMG_CD,DMG_DSCRPTN_VC,SLB,DMNSN,AR_SQFT,MN_HR_NC,MTRL_CST_NC,AR_SQMTRS,MDFD_BY,MDFD_DT,CSTMR_ID,ACTV_BT,DPT_ID FROM TARIFF WHERE CSTMR_ID=@CSTMR_ID AND EQPMNT_SZ=@EQPMNT_SZ AND EQPMNT_TYP=@EQPMNT_TYP AND DPT_ID=@DPT_ID"
    Private ds As TariffTemplateDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New TariffTemplateDataSet
    End Sub

#End Region

#Region "GetTariffTemplateBy() TABLE NAME:Tariff_Template"  '

    Public Function GetTariffTemplateByDepotID(ByVal bv_i32DepotID As Int32) As TariffTemplateDataSet
        Try
            objData = New DataObjects(Tariff_TemplateSelectQueryPk, TariffTemplateData.DPT_ID, CStr(bv_i32DepotID))
            objData.Fill(CType(ds, DataSet), TariffTemplateData._TARIFF_TEMPLATE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTariffTemplate() TABLE NAME:Tariff_Template"

    Public Function CreateTariffTemplate(ByVal bv_strEQPMNT_SZ As String, _
        ByVal bv_strEQPMNT_TYP As String, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_i32DPT_ID As Int32,
        ByVal bv_i32CSTMR_ID As Int64) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TariffTemplateData._TARIFF_TEMPLATE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TariffTemplateData._TARIFF_TEMPLATE)
                .Item(TariffTemplateData.TRFF_TMPLT_ID) = intMax
                .Item(TariffTemplateData.EQPMNT_SZ) = bv_strEQPMNT_SZ
                .Item(TariffTemplateData.EQPMNT_TYP) = bv_strEQPMNT_TYP
                .Item(TariffTemplateData.CSTMR_ID) = bv_i32CSTMR_ID
                If bv_blnACTV_BT <> Nothing Then
                    .Item(TariffTemplateData.ACTV_BT) = bv_blnACTV_BT
                Else
                    .Item(TariffTemplateData.ACTV_BT) = DBNull.Value
                End If
                If bv_i32DPT_ID <> 0 Then
                    .Item(TariffTemplateData.DPT_ID) = bv_i32DPT_ID
                Else
                    .Item(TariffTemplateData.DPT_ID) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Tariff_TemplateInsertQuery)
            dr = Nothing
            CreateTariffTemplate = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTariffTemplate() TABLE NAME:Tariff_Template"

    Public Function UpdateTariffTemplate(ByVal bv_i32TRFF_TMPLT_ID As Int32, _
        ByVal bv_strEQPMNT_SZ As String, _
        ByVal bv_strEQPMNT_TYP As String, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_i32DPT_ID As Int32,
        ByVal bv_i32CSTMR_ID As Int64) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TariffTemplateData._TARIFF_TEMPLATE).NewRow()
            With dr
                .Item(TariffTemplateData.TRFF_TMPLT_ID) = bv_i32TRFF_TMPLT_ID
                .Item(TariffTemplateData.EQPMNT_SZ) = bv_strEQPMNT_SZ
                .Item(TariffTemplateData.EQPMNT_TYP) = bv_strEQPMNT_TYP
                If bv_blnACTV_BT <> Nothing Then
                    .Item(TariffTemplateData.ACTV_BT) = bv_blnACTV_BT
                Else
                    .Item(TariffTemplateData.ACTV_BT) = DBNull.Value
                End If
                .Item(TariffTemplateData.CSTMR_ID) = bv_i32CSTMR_ID
                If bv_i32DPT_ID <> 0 Then
                    .Item(TariffTemplateData.DPT_ID) = bv_i32DPT_ID
                Else
                    .Item(TariffTemplateData.DPT_ID) = DBNull.Value
                End If
            End With
            UpdateTariffTemplate = objData.UpdateRow(dr, Tariff_TemplateUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTariffTemplate() TABLE NAME:Tariff_Template"

    Public Function DeleteTariffTemplate(ByVal bv_i32TRFF_TMPLT_ID As Int32) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TariffTemplateData._TARIFF_TEMPLATE).NewRow()
            With dr
                .Item(TariffTemplateData.TRFF_TMPLT_ID) = bv_i32TRFF_TMPLT_ID
            End With
            DeleteTariffTemplate = objData.DeleteRow(dr, Tariff_TemplateDeleteQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "GetTariffByCstmrID() TABLE NAME:Tariff"

    Public Function GetTariffByCstmrID(ByVal bv_strEqpmnt_type As String, ByVal bv_strEqpmnt_Size As String, ByVal bv_i64CstmrID As Int64, ByVal bv_i32DepotID As Int32) As TariffTemplateDataSet

        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(TariffTemplateData.EQPMNT_TYP, bv_strEqpmnt_type)
            hshParameters.Add(TariffTemplateData.EQPMNT_SZ, bv_strEqpmnt_Size)
            hshParameters.Add(TariffTemplateData.CSTMR_ID, bv_i64CstmrID)
            hshParameters.Add(TariffTemplateData.DPT_ID, bv_i32DepotID)
            objData = New DataObjects(TariffSelectQueryPk, hshParameters)
            objData.Fill(CType(ds, DataSet), TariffTemplateData._TARIFF)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
