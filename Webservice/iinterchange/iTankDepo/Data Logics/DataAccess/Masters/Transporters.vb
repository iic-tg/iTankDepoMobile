Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

Public Class Transporters

#Region "Declarartion Part.."
    Dim objData As DataObjects

    Private Const TransporterInsertQuery As String = "INSERT INTO TRANSPORTER(TRNSPRTR_ID,TRNSPRTR_CD,TRNSPRTR_DSCRPTN,CNTCT_PRSN,CNTCT_ADDRSS,ZP_CD,PHN_NO,FX_NO,EML_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID)VALUES(@TRNSPRTR_ID,@TRNSPRTR_CD,@TRNSPRTR_DSCRPTN,@CNTCT_PRSN,@CNTCT_ADDRSS,@ZP_CD,@PHN_NO,@FX_NO,@EML_ID,@ACTV_BT,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@DPT_ID)"
    Private Const TransporterUpdateQuery As String = "UPDATE Transporter SET TRNSPRTR_ID=@TRNSPRTR_ID, TRNSPRTR_CD=@TRNSPRTR_CD, TRNSPRTR_DSCRPTN=@TRNSPRTR_DSCRPTN, CNTCT_PRSN=@CNTCT_PRSN, CNTCT_ADDRSS=@CNTCT_ADDRSS, ZP_CD=@ZP_CD, PHN_NO=@PHN_NO, FX_NO=@FX_NO, EML_ID=@EML_ID, ACTV_BT=@ACTV_BT, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, DPT_ID=@DPT_ID WHERE TRNSPRTR_ID=@TRNSPRTR_ID"
    Private Const Transporter_Route_DetailInsertQuery As String = "INSERT INTO TRANSPORTER_ROUTE_DETAIL(TRNSPRTR_RT_DTL_ID,TRNSPRTR_ID,RT_ID,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,DRP_OFF_LCTN_CD,EMPTY_TRP_SPPLR_RT_NC,EMPTY_TRP_CSTMR_RT_NC,FLL_TRP_SPPLR_RT_NC,FLL_TRP_CSTMR_RT_NC)VALUES(@TRNSPRTR_RT_DTL_ID,@TRNSPRTR_ID,@RT_ID,@RT_DSCRPTN_VC,@PCK_UP_LCTN_CD,@DRP_OFF_LCTN_CD,@EMPTY_TRP_SPPLR_RT_NC,@EMPTY_TRP_CSTMR_RT_NC,@FLL_TRP_SPPLR_RT_NC,@FLL_TRP_CSTMR_RT_NC)"
    Private Const Transporter_Route_DetailUpdateQuery As String = "UPDATE TRANSPORTER_ROUTE_DETAIL SET TRNSPRTR_RT_DTL_ID=@TRNSPRTR_RT_DTL_ID, TRNSPRTR_ID=@TRNSPRTR_ID, RT_ID=@RT_ID,RT_DSCRPTN_VC=@RT_DSCRPTN_VC, PCK_UP_LCTN_CD=@PCK_UP_LCTN_CD, DRP_OFF_LCTN_CD=@DRP_OFF_LCTN_CD, EMPTY_TRP_SPPLR_RT_NC=@EMPTY_TRP_SPPLR_RT_NC, EMPTY_TRP_CSTMR_RT_NC=@EMPTY_TRP_CSTMR_RT_NC, FLL_TRP_SPPLR_RT_NC=@FLL_TRP_SPPLR_RT_NC, FLL_TRP_CSTMR_RT_NC=@FLL_TRP_CSTMR_RT_NC WHERE TRNSPRTR_RT_DTL_ID=@TRNSPRTR_RT_DTL_ID"
    Private Const Transporter_Route_DetailDeleteQuery As String = "DELETE FROM TRANSPORTER_ROUTE_DETAIL WHERE TRNSPRTR_RT_DTL_ID=@TRNSPRTR_RT_DTL_ID"
    Private Const V_TransporterCodeSelectQuery As String = "SELECT TRNSPRTR_ID,TRNSPRTR_CD,TRNSPRTR_DSCRPTN,CNTCT_PRSN,CNTCT_ADDRSS,ZP_CD,PHN_NO,FX_NO,EML_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,DPT_CD FROM V_TRANSPORTER WHERE TRNSPRTR_CD=@TRNSPRTR_CD AND DPT_ID=@DPT_ID"
    Private Const V_TRANSPORTER_ROUTE_DETAILSelectQueryByTransporterID As String = "SELECT TRNSPRTR_RT_DTL_ID,TRNSPRTR_ID,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,DRP_OFF_LCTN_CD,EMPTY_TRP_SPPLR_RT_NC,EMPTY_TRP_CSTMR_RT_NC,FLL_TRP_SPPLR_RT_NC,FLL_TRP_CSTMR_RT_NC,DPT_CRRNCY_CD FROM V_TRANSPORTER_ROUTE_DETAIL WHERE TRNSPRTR_ID=@TRNSPRTR_ID AND DPT_ID=@DPT_ID"
    Private Const V_TRANSPORTERSelectQueryDefaultByDepotId As String = "SELECT TRNSPRTR_ID,TRNSPRTR_CD,TRNSPRTR_DSCRPTN,CNTCT_PRSN,CNTCT_ADDRSS,ZP_CD,PHN_NO,FX_NO,EML_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,DPT_CD FROM V_TRANSPORTER WHERE DPT_ID=@DPT_ID"
    Private Const V_TRANSPORTER_ROUTE_DETAILSelectQueryByRouteCode As String = "SELECT TRNSPRTR_RT_DTL_ID,TRNSPRTR_ID,RT_ID,RT_CD,RT_DSCRPTN_VC,PCK_UP_LCTN_CD,DRP_OFF_LCTN_CD,EMPTY_TRP_SPPLR_RT_NC,EMPTY_TRP_CSTMR_RT_NC,FLL_TRP_SPPLR_RT_NC,FLL_TRP_CSTMR_RT_NC,DPT_ID,DPT_CRRNCY_CD FROM V_TRANSPORTER_ROUTE_DETAIL WHERE (TRNSPRTR_CD =@TRNSPRTR_CD AND DPT_ID=@DPT_ID AND RT_CD=@RT_CD)"
    Private Const Bank_Detail_SelectQueryForLocalCurrency As String = "SELECT BNK_DTL_BIN,BNK_TYP_ID,BNK_TYP_CD,BNK_NM,BNK_ADDRSS,ACCNT_NO,IBAN_NO,SWIFT_CD,CRRNCY_ID,CRRNCY_CD,DPT_ID FROM V_BANK_DETAIL WHERE DPT_ID=@DPT_ID AND BNK_TYP_ID=44"
    Private Const TransporterDetailSelectQueryByID As String = "SELECT TRNSPRTR_ID,TRNSPRTR_CD,TRNSPRTR_DSCRPTN,CNTCT_PRSN,CNTCT_ADDRSS,ZP_CD,PHN_NO,FX_NO,EML_ID,ACTV_BT,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,DPT_ID,DPT_CD FROM V_TRANSPORTER WHERE TRNSPRTR_ID=@TRNSPRTR_ID AND DPT_ID=@DPT_ID"
    Private ds As TransporterDataSet
#End Region

#Region "Constructor.."
    Sub New()
        ds = New TransporterDataSet
    End Sub
#End Region

#Region "GetTransporterRouteDetailByTransporterID() TABLE NAME:TRANSPORTER_ROUTE_DETAIL"

    Public Function GetTransporterRouteDetailByTransporterID(ByVal bv_i64TransporterID As Int64, _
                                                             ByVal bv_intDepotId As Integer) As TransporterDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransporterData.DPT_ID, bv_intDepotId)
            hshparameters.Add(TransporterData.TRNSPRTR_ID, bv_i64TransporterID)
            objData = New DataObjects(V_TRANSPORTER_ROUTE_DETAILSelectQueryByTransporterID, hshparameters)
            objData.Fill(CType(ds, DataSet), TransporterData._V_TRANSPORTER_ROUTE_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateTransporter() TABLE NAME:Transporter"

    Public Function CreateTransporter(ByVal bv_strTransporterCD As String, _
                                      ByVal bv_strTransporterDescription As String, _
                                      ByVal bv_strContactPerson As String, _
                                      ByVal bv_strContactAddress As String, _
                                      ByVal bv_strZipCode As String, _
                                      ByVal bv_strPhoneNo As String, _
                                      ByVal bv_strFaxNo As String, _
                                      ByVal bv_strEmailID As String, _
                                      ByVal bv_blnActiveID As Boolean, _
                                      ByVal bv_strCreatedBy As String, _
                                      ByVal bv_datCreatedDate As DateTime, _
                                      ByVal bv_strModifiedBy As String, _
                                      ByVal bv_datModifiedDate As DateTime, _
                                      ByVal bv_i32DepotID As Int32, _
                                      ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TransporterData._TRANSPORTER).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TransporterData._TRANSPORTER, br_objTransaction)
                .Item(TransporterData.TRNSPRTR_ID) = intMax
                .Item(TransporterData.TRNSPRTR_CD) = bv_strTransporterCD
                .Item(TransporterData.TRNSPRTR_DSCRPTN) = bv_strTransporterDescription
                .Item(TransporterData.CNTCT_PRSN) = bv_strContactPerson
                If bv_strContactAddress <> Nothing Then
                    .Item(TransporterData.CNTCT_ADDRSS) = bv_strContactAddress
                Else
                    .Item(TransporterData.CNTCT_ADDRSS) = DBNull.Value
                End If
                If bv_strZipCode <> Nothing Then
                    .Item(TransporterData.ZP_CD) = bv_strZipCode
                Else
                    .Item(TransporterData.ZP_CD) = DBNull.Value
                End If
                If bv_strPhoneNo <> Nothing Then
                    .Item(TransporterData.PHN_NO) = bv_strPhoneNo
                Else
                    .Item(TransporterData.PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNo <> Nothing Then
                    .Item(TransporterData.FX_NO) = bv_strFaxNo
                Else
                    .Item(TransporterData.FX_NO) = DBNull.Value
                End If
                If bv_strEmailID <> Nothing Then
                    .Item(TransporterData.EML_ID) = bv_strEmailID
                Else
                    .Item(TransporterData.EML_ID) = DBNull.Value
                End If
                .Item(TransporterData.ACTV_BT) = bv_blnActiveID
                .Item(TransporterData.CRTD_BY) = bv_strCreatedBy
                .Item(TransporterData.CRTD_DT) = bv_datCreatedDate
                .Item(TransporterData.MDFD_BY) = bv_strModifiedBy
                .Item(TransporterData.MDFD_DT) = bv_datModifiedDate
                .Item(TransporterData.DPT_ID) = bv_i32DepotID
            End With
            objData.InsertRow(dr, TransporterInsertQuery, br_objTransaction)
            dr = Nothing
            CreateTransporter = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTransporter() TABLE NAME:Transporter"

    Public Function UpdateTransporter(ByVal bv_i64TransporterID As Int64, _
                                      ByVal bv_strTransporterCD As String, _
                                      ByVal bv_strTransporterDescription As String, _
                                      ByVal bv_strContactPerson As String, _
                                      ByVal bv_strContactAddress As String, _
                                      ByVal bv_strZipCode As String, _
                                      ByVal bv_strPhoneNo As String, _
                                      ByVal bv_strFaxNo As String, _
                                      ByVal bv_strEmailID As String, _
                                      ByVal bv_blnActiveID As Boolean, _
                                      ByVal bv_strModifiedBy As String, _
                                      ByVal bv_datModifiedDate As DateTime, _
                                      ByVal bv_i32DepotID As Int32, _
                                      ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TransporterData._TRANSPORTER).NewRow()
            With dr
                .Item(TransporterData.TRNSPRTR_ID) = bv_i64TransporterID
                .Item(TransporterData.TRNSPRTR_CD) = bv_strTransporterCD
                .Item(TransporterData.TRNSPRTR_DSCRPTN) = bv_strTransporterDescription
                .Item(TransporterData.CNTCT_PRSN) = bv_strContactPerson
                If bv_strContactAddress <> Nothing Then
                    .Item(TransporterData.CNTCT_ADDRSS) = bv_strContactAddress
                Else
                    .Item(TransporterData.CNTCT_ADDRSS) = DBNull.Value
                End If
                If bv_strZipCode <> Nothing Then
                    .Item(TransporterData.ZP_CD) = bv_strZipCode
                Else
                    .Item(TransporterData.ZP_CD) = DBNull.Value
                End If
                If bv_strPhoneNo <> Nothing Then
                    .Item(TransporterData.PHN_NO) = bv_strPhoneNo
                Else
                    .Item(TransporterData.PHN_NO) = DBNull.Value
                End If
                If bv_strFaxNo <> Nothing Then
                    .Item(TransporterData.FX_NO) = bv_strFaxNo
                Else
                    .Item(TransporterData.FX_NO) = DBNull.Value
                End If
                If bv_strEmailID <> Nothing Then
                    .Item(TransporterData.EML_ID) = bv_strEmailID
                Else
                    .Item(TransporterData.EML_ID) = DBNull.Value
                End If
                .Item(TransporterData.ACTV_BT) = bv_blnActiveID
                .Item(TransporterData.MDFD_BY) = bv_strModifiedBy
                .Item(TransporterData.MDFD_DT) = bv_datModifiedDate
                .Item(TransporterData.DPT_ID) = bv_i32DepotID
            End With
            UpdateTransporter = objData.UpdateRow(dr, TransporterUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateTransporterRouteDetail() TABLE NAME:Transporter_Route_Detail"

    Public Function CreateTransporterRouteDetail(ByVal bv_i64TransporterID As Int64, _
                                                 ByVal bv_i64RouteId As Int64, _
                                                 ByVal bv_strRouteDescription As String, _
                                                 ByVal bv_strPickUpLocationCode As String, _
                                                 ByVal bv_strDropOffLocationCode As String, _
                                                 ByVal bv_dblEmptyTripSupplierRate As Decimal, _
                                                 ByVal bv_dblEmptyTripCustomerRate As Decimal, _
                                                 ByVal bv_dblFullTripSupplierRate As Decimal, _
                                                 ByVal bv_dblFullTripCustomerRate As Decimal, _
                                                 ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(TransporterData._TRANSPORTER_ROUTE_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(TransporterData._TRANSPORTER_ROUTE_DETAIL, br_objTransaction)
                .Item(TransporterData.TRNSPRTR_RT_DTL_ID) = intMax
                .Item(TransporterData.TRNSPRTR_ID) = bv_i64TransporterID
                .Item(TransporterData.RT_ID) = bv_i64RouteId
                .Item(TransporterData.RT_DSCRPTN_VC) = bv_strRouteDescription
                .Item(TransporterData.PCK_UP_LCTN_CD) = bv_strPickUpLocationCode
                .Item(TransporterData.DRP_OFF_LCTN_CD) = bv_strDropOffLocationCode
                .Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC) = bv_dblEmptyTripSupplierRate
                .Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC) = bv_dblEmptyTripCustomerRate
                .Item(TransporterData.FLL_TRP_SPPLR_RT_NC) = bv_dblFullTripSupplierRate
                .Item(TransporterData.FLL_TRP_CSTMR_RT_NC) = bv_dblFullTripCustomerRate
            End With
            objData.InsertRow(dr, Transporter_Route_DetailInsertQuery, br_objTransaction)
            dr = Nothing
            CreateTransporterRouteDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateTransporterRouteDetail() TABLE NAME:Transporter_Route_Detail"

    Public Function UpdateTransporterRouteDetail(ByVal bv_i64TransporterRouteDetailID As Int64, _
                                                 ByVal bv_i64TransporterID As Int64, _
                                                 ByVal bv_i64RouteId As Int64, _
                                                 ByVal bv_strRouteDescription As String, _
                                                 ByVal bv_strPickUpLocationCode As String, _
                                                 ByVal bv_strDropOffLocationCode As String, _
                                                 ByVal bv_decEmptyTripSupplierRate As Decimal, _
                                                 ByVal bv_decEmptyTripCustomerRate As Decimal, _
                                                 ByVal bv_decFullTripSupplierRate As Decimal, _
                                                 ByVal bv_decFullTripCustomerRate As Decimal, _
                                                 ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(TransporterData._TRANSPORTER_ROUTE_DETAIL).NewRow()
            With dr
                .Item(TransporterData.TRNSPRTR_RT_DTL_ID) = bv_i64TransporterRouteDetailID
                .Item(TransporterData.TRNSPRTR_ID) = bv_i64TransporterID
                .Item(TransporterData.RT_ID) = bv_i64RouteId
                .Item(TransporterData.RT_DSCRPTN_VC) = bv_strRouteDescription
                .Item(TransporterData.PCK_UP_LCTN_CD) = bv_strPickUpLocationCode
                .Item(TransporterData.DRP_OFF_LCTN_CD) = bv_strDropOffLocationCode
                .Item(TransporterData.EMPTY_TRP_SPPLR_RT_NC) = bv_decEmptyTripSupplierRate
                .Item(TransporterData.EMPTY_TRP_CSTMR_RT_NC) = bv_decEmptyTripCustomerRate
                .Item(TransporterData.FLL_TRP_SPPLR_RT_NC) = bv_decFullTripSupplierRate
                .Item(TransporterData.FLL_TRP_CSTMR_RT_NC) = bv_decFullTripCustomerRate
            End With
            UpdateTransporterRouteDetail = objData.UpdateRow(dr, Transporter_Route_DetailUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteTransporterRouteDetail() TABLE NAME:Transporter_Route_Detail"

    Public Function DeleteTransporterRouteDetail(ByVal bv_dblTransporterRouteDetailID As Int64, _
                                                 ByRef br_objTransaction As Transactions) As Boolean

        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(TransporterData._TRANSPORTER_ROUTE_DETAIL).NewRow()
            With dr
                .Item(TransporterData.TRNSPRTR_RT_DTL_ID) = bv_dblTransporterRouteDetailID
            End With
            DeleteTransporterRouteDetail = objData.DeleteRow(dr, Transporter_Route_DetailDeleteQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetTransporterByTransporterCode() TABLE NAME:TRANSPORTER"

    Public Function GetTransporterByTransporterCode(ByVal bv_strTransporterCode As String, _
                                                    ByVal bv_intDepotId As Integer) As TransporterDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransporterData.DPT_ID, bv_intDepotId)
            hshparameters.Add(TransporterData.TRNSPRTR_CD, bv_strTransporterCode)
            objData = New DataObjects(V_TransporterCodeSelectQuery, hshparameters)
            objData.Fill(CType(ds, DataSet), TransporterData._V_TRANSPORTER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetCheckDefaultByDepotId() TABLE NAME:TRANSPORTER"

    Public Function GetCheckDefaultByDepotId(ByVal bv_intDepotId As Integer) As TransporterDataSet
        Try
            objData = New DataObjects(V_TRANSPORTERSelectQueryDefaultByDepotId, TransporterData.DPT_ID, bv_intDepotId)
            objData.Fill(CType(ds, DataSet), TransporterData._V_TRANSPORTER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetRouteCodeByRoute() TABLE NAME: TRANSPORTER_ROUTE_DETAIL"
    Public Function GetTransporterRouteCodeByRoute(ByVal bv_strRouteCode As String, _
                                                   ByVal bv_intDepotId As Int64, _
                                                   ByVal bv_strTransporterCode As String) As String
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(TransporterData.DPT_ID, bv_intDepotId)
            hshParameters.Add(TransporterData.RT_CD, bv_strRouteCode)
            hshParameters.Add(TransporterData.TRNSPRTR_CD, bv_strTransporterCode)
            objData = New DataObjects(V_TRANSPORTER_ROUTE_DETAILSelectQueryByRouteCode, hshParameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetBankDetailByDepotId() TABLE NAME: BANK_DETAIL"

    Public Function GetBankDetailByDepotId(ByVal bv_i32DepotId As Int32) As TransporterDataSet
        Try
            objData = New DataObjects(Bank_Detail_SelectQueryForLocalCurrency, CommonUIData.DPT_ID, bv_i32DepotId)
            objData.Fill(CType(ds, DataSet), TransporterData._V_BANK_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GetTransporterDetailByID() TABLE NAME:TRANSPORTER"

    Public Function GetTransporterDetailByID(ByVal bv_i64TransporterID As Int64, _
                                                    ByVal bv_intDepotId As Integer) As TransporterDataSet
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(TransporterData.DPT_ID, bv_intDepotId)
            hshparameters.Add(TransporterData.TRNSPRTR_ID, bv_i64TransporterID)
            objData = New DataObjects(TransporterDetailSelectQueryByID, hshparameters)
            objData.Fill(CType(ds, DataSet), TransporterData._TRANSPORTER)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
