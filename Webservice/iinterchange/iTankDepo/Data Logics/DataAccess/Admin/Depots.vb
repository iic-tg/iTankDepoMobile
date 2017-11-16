#Region " Depots.vb"
'*********************************************************************************************************************
'Name :
'      Depots.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Depots.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/18/2013 9:59:06 AM
'*********************************************************************************************************************
#End Region

Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
#Region "Depots"

Public Class Depots

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const DEPOTSelectQueryByDPT_CD As String = "SELECT DPT_ID,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,VT_NO,EML_ID,PHN_NO,FX_NO,CMPNY_LG_PTH,MDFD_BY,MDFD_DT,ORGNZTN_TYP_ID,RPRTNG_TO_ID FROM DEPOT WHERE DPT_CD=@DPT_CD"
    Private Const DEPOTInsertQuery As String = "INSERT INTO DEPOT(DPT_ID,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,VT_NO,EML_ID,PHN_NO,FX_NO,CMPNY_LG_PTH,MDFD_BY,MDFD_DT,RPRTNG_TO_ID,ORGNZTN_TYP_ID)VALUES(@DPT_ID,@DPT_CD,@DPT_NAM,@CNTCT_PRSN_NAM,@ADDRSS_LN1_VC,@ADDRSS_LN2_VC,@ADDRSS_LN3_VC,@VT_NO,@EML_ID,@PHN_NO,@FX_NO,@CMPNY_LG_PTH,@MDFD_BY,@MDFD_DT,@RPRTNG_TO_ID,@ORGNZTN_TYP_ID)"
    Private Const DEPOTUpdateQuery As String = "UPDATE DEPOT SET DPT_ID=@DPT_ID, DPT_CD=@DPT_CD, DPT_NAM=@DPT_NAM, CNTCT_PRSN_NAM=@CNTCT_PRSN_NAM, ADDRSS_LN1_VC=@ADDRSS_LN1_VC, ADDRSS_LN2_VC=@ADDRSS_LN2_VC, ADDRSS_LN3_VC=@ADDRSS_LN3_VC, VT_NO=@VT_NO, EML_ID=@EML_ID, PHN_NO=@PHN_NO, FX_NO=@FX_NO,CMPNY_LG_PTH=@CMPNY_LG_PTH, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT,RPRTNG_TO_ID=@RPRTNG_TO_ID,ORGNZTN_TYP_ID=@ORGNZTN_TYP_ID WHERE DPT_ID=@DPT_ID"
    Private Const DEPOTDeleteQuery As String = "DELETE FROM DEPOT WHERE DPT_ID=@DPT_ID"
    Private Const V_BankDetailSelectQuery As String = "SELECT BNK_DTL_BIN,BNK_TYP_ID,BNK_TYP_CD,BNK_NM,BNK_ADDRSS,ACCNT_NO,IBAN_NO,SWIFT_CD,CRRNCY_ID,CRRNCY_CD,DPT_ID FROM V_BANK_DETAIL WHERE DPT_ID=@DPT_ID"
    Private Const V_BankDetailLocalCurrencySelectQuery As String = "SELECT BNK_DTL_BIN,BNK_TYP_ID,BNK_TYP_CD,BNK_NM,BNK_ADDRSS,ACCNT_NO,IBAN_NO,SWIFT_CD,CRRNCY_ID,CRRNCY_CD,DPT_ID FROM V_BANK_DETAIL WHERE DPT_ID=@DPT_ID and BNK_TYP_ID=@BNK_TYP_ID"
    Private Const BankDetailInsertQuery As String = "INSERT INTO BANK_DETAIL(BNK_DTL_BIN,BNK_TYP_ID,BNK_NM,BNK_ADDRSS,ACCNT_NO,IBAN_NO,SWIFT_CD,CRRNCY_ID,DPT_ID) VALUES(@BNK_DTL_BIN,@BNK_TYP_ID,@BNK_NM,@BNK_ADDRSS,@ACCNT_NO,@IBAN_NO,@SWIFT_CD,@CRRNCY_ID,@DPT_ID)"
    Private Const BankDetailUpdateQuery As String = "UPDATE BANK_DETAIL SET BNK_TYP_ID=@BNK_TYP_ID,BNK_NM=@BNK_NM,BNK_ADDRSS=@BNK_ADDRSS,ACCNT_NO=@ACCNT_NO,IBAN_NO=@IBAN_NO,SWIFT_CD=@SWIFT_CD,CRRNCY_ID=@CRRNCY_ID WHERE (DPT_ID=@DPT_ID AND BNK_DTL_BIN=@BNK_DTL_BIN)"
    Private Const BankDetailDeleteQuery As String = "DELETE FROM BANK_DETAIL WHERE BNK_DTL_BIN=@BNK_DTL_BIN"
    Private Const V_DepotSelectQuery As String = "SELECT DPT_CD[Depot Code],DPT_NAM[Depot Name],CNTCT_PRSN_NAM[Contact Person],MDFD_BY[Modified By],MDFD_DT[Modified Date],DPT_ID,PHN_NO,ADDRSS_LN1_VC,ADDRSS_LN2_VC,ADDRSS_LN3_VC,VT_NO,EML_ID,FX_NO,CMPNY_LG_PTH,DPT_CD,DPT_NAM,CNTCT_PRSN_NAM,MDFD_BY,MDFD_DT,ORGNZTN_TYP_ID,RPRTNG_TO_ID,RPRTNG_TO_CD,ORGNZTN_TYP_CD FROM V_DEPOT"
    Private ds As DepotDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New DepotDataSet
    End Sub

#End Region

#Region "GetDEPOTByDPT_CD() TABLE NAME:DEPOT"

    Public Function GetDEPOTByDPT_CD(ByVal bv_strDPT_CD As String) As DepotDataSet
        Try
            objData = New DataObjects(DEPOTSelectQueryByDPT_CD, DepotData.DPT_CD, bv_strDPT_CD)
            objData.Fill(CType(ds, DataSet), DepotData._DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateDEPOT() TABLE NAME:DEPOT"

    Public Function CreateDEPOT(ByVal bv_strDPT_CD As String, _
                                ByVal bv_strDPT_NAM As String, _
                                ByVal bv_strCNTCT_PRSN_NAM As String, _
                                ByVal bv_strADDRSS_LN1_VC As String, _
                                ByVal bv_strADDRSS_LN2_VC As String, _
                                ByVal bv_strADDRSS_LN3_VC As String, _
                                ByVal bv_strVT_NO As String, _
                                ByVal bv_strEML_ID As String, _
                                ByVal bv_strPHN_NO As String, _
                                ByVal bv_strFX_NO As String, _
                                ByVal bv_strCMPNY_LG_PTH As String, _
                                ByVal bv_strMDFD_BY As String, _
                                ByVal bv_datMDFD_DT As DateTime, _
                                ByVal bv_intOrganizationType As Int32, _
                                ByVal bv_intReportingTo As Int32, _
                                ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(DepotData._DEPOT).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(DepotData._DEPOT, br_objTransaction)
                .Item(DepotData.DPT_ID) = intMax
                .Item(DepotData.DPT_CD) = bv_strDPT_CD
                If bv_strDPT_NAM <> Nothing Then
                    .Item(DepotData.DPT_NAM) = bv_strDPT_NAM
                Else
                    .Item(DepotData.DPT_NAM) = DBNull.Value
                End If
                .Item(DepotData.CNTCT_PRSN_NAM) = bv_strCNTCT_PRSN_NAM
                .Item(DepotData.ADDRSS_LN1_VC) = bv_strADDRSS_LN1_VC
                If bv_strADDRSS_LN2_VC <> Nothing Then
                    .Item(DepotData.ADDRSS_LN2_VC) = bv_strADDRSS_LN2_VC
                Else
                    .Item(DepotData.ADDRSS_LN2_VC) = DBNull.Value
                End If
                If bv_strADDRSS_LN3_VC <> Nothing Then
                    .Item(DepotData.ADDRSS_LN3_VC) = bv_strADDRSS_LN3_VC
                Else
                    .Item(DepotData.ADDRSS_LN3_VC) = DBNull.Value
                End If
                .Item(DepotData.VT_NO) = bv_strVT_NO
                If bv_strEML_ID <> Nothing Then
                    .Item(DepotData.EML_ID) = bv_strEML_ID
                Else
                    .Item(DepotData.EML_ID) = DBNull.Value
                End If
                If bv_strPHN_NO <> Nothing Then
                    .Item(DepotData.PHN_NO) = bv_strPHN_NO
                Else
                    .Item(DepotData.PHN_NO) = DBNull.Value
                End If
                If bv_strFX_NO <> Nothing Then
                    .Item(DepotData.FX_NO) = bv_strFX_NO
                Else
                    .Item(DepotData.FX_NO) = DBNull.Value
                End If
                If bv_strCMPNY_LG_PTH <> Nothing Then
                    .Item(DepotData.CMPNY_LG_PTH) = bv_strCMPNY_LG_PTH
                Else
                    .Item(DepotData.CMPNY_LG_PTH) = DBNull.Value
                End If
                If bv_intOrganizationType <> Nothing Then
                    .Item(DepotData.ORGNZTN_TYP_ID) = bv_intOrganizationType
                Else
                    .Item(DepotData.ORGNZTN_TYP_ID) = DBNull.Value
                End If
                If bv_intReportingTo <> Nothing Then
                    .Item(DepotData.RPRTNG_TO_ID) = bv_intReportingTo
                Else
                    .Item(DepotData.RPRTNG_TO_ID) = DBNull.Value
                End If

                .Item(DepotData.MDFD_BY) = bv_strMDFD_BY
                .Item(DepotData.MDFD_DT) = bv_datMDFD_DT
            End With
            objData.InsertRow(dr, DEPOTInsertQuery, br_objTransaction)
            dr = Nothing
            CreateDEPOT = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateDEPOT() TABLE NAME:DEPOT"

    Public Function UpdateDEPOT(ByVal bv_i32DPT_ID As Int32, _
        ByVal bv_strDPT_CD As String, _
        ByVal bv_strDPT_NAM As String, _
        ByVal bv_strCNTCT_PRSN_NAM As String, _
        ByVal bv_strADDRSS_LN1_VC As String, _
        ByVal bv_strADDRSS_LN2_VC As String, _
        ByVal bv_strADDRSS_LN3_VC As String, _
        ByVal bv_strVT_NO As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_strPHN_NO As String, _
        ByVal bv_strFX_NO As String, _
        ByVal bv_strCMPNY_LG_PTH As String, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_intOrganizationType As Int32, _
        ByVal bv_intReportingTo As Int32, _
        ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(DepotData._DEPOT).NewRow()
            With dr
                .Item(DepotData.DPT_ID) = bv_i32DPT_ID
                .Item(DepotData.DPT_CD) = bv_strDPT_CD
                If bv_strDPT_NAM <> Nothing Then
                    .Item(DepotData.DPT_NAM) = bv_strDPT_NAM
                Else
                    .Item(DepotData.DPT_NAM) = DBNull.Value
                End If
                .Item(DepotData.CNTCT_PRSN_NAM) = bv_strCNTCT_PRSN_NAM
                .Item(DepotData.ADDRSS_LN1_VC) = bv_strADDRSS_LN1_VC
                If bv_strADDRSS_LN2_VC <> Nothing Then
                    .Item(DepotData.ADDRSS_LN2_VC) = bv_strADDRSS_LN2_VC
                Else
                    .Item(DepotData.ADDRSS_LN2_VC) = DBNull.Value
                End If
                If bv_strADDRSS_LN3_VC <> Nothing Then
                    .Item(DepotData.ADDRSS_LN3_VC) = bv_strADDRSS_LN3_VC
                Else
                    .Item(DepotData.ADDRSS_LN3_VC) = DBNull.Value
                End If
                .Item(DepotData.VT_NO) = bv_strVT_NO
                If bv_strEML_ID <> Nothing Then
                    .Item(DepotData.EML_ID) = bv_strEML_ID
                Else
                    .Item(DepotData.EML_ID) = DBNull.Value
                End If
                If bv_strPHN_NO <> Nothing Then
                    .Item(DepotData.PHN_NO) = bv_strPHN_NO
                Else
                    .Item(DepotData.PHN_NO) = DBNull.Value
                End If
                If bv_strFX_NO <> Nothing Then
                    .Item(DepotData.FX_NO) = bv_strFX_NO
                Else
                    .Item(DepotData.FX_NO) = DBNull.Value
                End If
                If bv_strCMPNY_LG_PTH <> Nothing Then
                    .Item(DepotData.CMPNY_LG_PTH) = bv_strCMPNY_LG_PTH
                Else
                    .Item(DepotData.CMPNY_LG_PTH) = DBNull.Value
                End If

                If bv_intOrganizationType <> Nothing Then
                    .Item(DepotData.ORGNZTN_TYP_ID) = bv_intOrganizationType
                Else
                    .Item(DepotData.ORGNZTN_TYP_ID) = DBNull.Value
                End If
                If bv_intReportingTo <> Nothing Then
                    .Item(DepotData.RPRTNG_TO_ID) = bv_intReportingTo
                Else
                    .Item(DepotData.RPRTNG_TO_ID) = DBNull.Value
                End If

                .Item(DepotData.MDFD_BY) = bv_strMDFD_BY
                .Item(DepotData.MDFD_DT) = bv_datMDFD_DT
            End With
            UpdateDEPOT = objData.UpdateRow(dr, DEPOTUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteDEPOT() TABLE NAME:DEPOT"

    Public Function DeleteDEPOT(ByVal bv_DPT_ID As Int32) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(DepotData._DEPOT).NewRow()
            With dr
                .Item(DepotData.DPT_ID) = bv_DPT_ID
            End With
            DeleteDEPOT = objData.DeleteRow(dr, DEPOTDeleteQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetBankDetail()"

    Public Function GetBankDetail(ByVal bv_DPT_ID As Int32) As DepotDataSet
        Try
            objData = New DataObjects(V_BankDetailSelectQuery, DepotData.DPT_ID, bv_DPT_ID)
            objData.Fill(CType(ds, DataSet), DepotData._V_BANK_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetBankDetailLocalCurrency()"

    Public Function GetBankDetailLocalCurrency(ByVal bv_DPT_ID As Int32, ByVal bv_BNK_TYP_ID As Int32) As DepotDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(DepotData.DPT_ID, bv_DPT_ID)
            hshParameters.Add(DepotData.BNK_TYP_ID, bv_BNK_TYP_ID)
            objData = New DataObjects(V_BankDetailLocalCurrencySelectQuery, hshParameters)
            objData.Fill(CType(ds, DataSet), DepotData._V_BANK_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateBankDetail()"

    Public Function CreateBankDetail(ByVal bv_i64DPT_ID As Int64, _
                                     ByVal bv_BankTypeId As Integer, _
                                     ByVal bv_BankName As String, _
                                     ByVal bv_BankAddress As String, _
                                     ByVal bv_AccountNo As String, _
                                     ByVal bv_IBANNo As String, _
                                     ByVal bv_SWIFTCode As String, _
                                     ByVal bv_CurrencyID As Integer, _
                                     ByRef br_ObjTransactions As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(DepotData._BANK_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(DepotData._BANK_DETAIL, br_ObjTransactions)
                .Item(DepotData.BNK_DTL_BIN) = intMax
                .Item(DepotData.BNK_TYP_ID) = bv_BankTypeId
                .Item(DepotData.BNK_NM) = bv_BankName
                .Item(DepotData.BNK_ADDRSS) = bv_BankAddress
                .Item(DepotData.ACCNT_NO) = bv_AccountNo
                .Item(DepotData.IBAN_NO) = bv_IBANNo
                .Item(DepotData.SWIFT_CD) = bv_SWIFTCode
                .Item(DepotData.CRRNCY_ID) = bv_CurrencyID
                .Item(DepotData.DPT_ID) = bv_i64DPT_ID
            End With
            objData.InsertRow(dr, BankDetailInsertQuery, br_ObjTransactions)
            dr = Nothing
            CreateBankDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateBankDetail()"

    Public Function UpdateBankDetail(ByVal bv_i64DPT_ID As Int64, _
                                     ByVal bv_BankDetailBin As Integer, _
                                     ByVal bv_BankTypeId As Integer, _
                                     ByVal bv_BankName As String, _
                                     ByVal bv_BankAddress As String, _
                                     ByVal bv_AccountNo As String, _
                                     ByVal bv_IBANNo As String, _
                                     ByVal bv_SWIFTCode As String, _
                                     ByVal bv_CurrencyID As Integer, _
                                     ByRef br_ObjTransactions As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(DepotData._BANK_DETAIL).NewRow()
            With dr
                .Item(DepotData.BNK_DTL_BIN) = bv_BankDetailBin
                .Item(DepotData.BNK_TYP_ID) = bv_BankTypeId
                .Item(DepotData.BNK_NM) = bv_BankName
                .Item(DepotData.BNK_ADDRSS) = bv_BankAddress
                .Item(DepotData.ACCNT_NO) = bv_AccountNo
                .Item(DepotData.IBAN_NO) = bv_IBANNo
                .Item(DepotData.SWIFT_CD) = bv_SWIFTCode
                .Item(DepotData.CRRNCY_ID) = bv_CurrencyID
                .Item(DepotData.DPT_ID) = bv_i64DPT_ID
            End With
            UpdateBankDetail = objData.UpdateRow(dr, BankDetailUpdateQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "DeleteBankDetail()"

    Public Function DeleteBankDetail(ByVal BankDetailBin As Int64, ByRef br_ObjTransactions As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(DepotData._BANK_DETAIL).NewRow()
            With dr
                .Item(DepotData.BNK_DTL_BIN) = BankDetailBin
            End With
            DeleteBankDetail = objData.DeleteRow(dr, BankDetailDeleteQuery, br_ObjTransactions)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetAllDepot()"

    Public Function GetAllDepot() As DepotDataSet
        Try
            objData = New DataObjects(V_DepotSelectQuery)
            objData.Fill(CType(ds, DataSet), DepotData._V_DEPOT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region