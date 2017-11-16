#Region " EDI.vb"
'*********************************************************************************************************************
'Name :
'      EDI.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(EDI.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      7/9/2013 4:29:19 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class EDI

#Region "pub_WriteGatinFile()"
    <OperationContract()> _
    Public Function pub_WriteGatinFile(ByVal bv_strWfdata As String, ByVal bv_strCustomerCode As String, ByVal bv_strGenerationMode As String, ByVal by_strTimeStamp As String) As String
        Try
            'Dim strCustomer As String() = bv_strCustomerCode.Split(CChar(","))
            'bv_strCustomerCode = strCustomer(0)

            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            Dim objAnsii As New AnsiiEngine(bv_strCustomerCode, strDepotCode)
            '   Dim objAnsiiLessor As New AnsiiEngine(strCustomer(1))
            Return objAnsii.WriteGatinFile(strDepotCode, bv_strGenerationMode, by_strTimeStamp)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_WriteGatinFile()"
    <OperationContract()> _
    Public Function pub_WriteGatinFile(ByVal bv_strWfdata As String, ByVal bv_strCustomerCode As String, ByVal bv_strGenerationMode As String) As String
        Try
            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            Dim objAnsii As New AnsiiEngine(bv_strCustomerCode, strDepotCode)
            Return objAnsii.WriteGatinFile(strDepotCode, bv_strGenerationMode, "AUTO")
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_WriteGateoutFile()"
    <OperationContract()> _
    Public Function pub_WriteGateoutFile(ByVal bv_strWfdata As String, ByVal bv_strCustomerCode As String, ByVal bv_strGenerationMode As String) As String
        Try
            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            Dim objAnsii As New AnsiiEngine(bv_strCustomerCode, strDepotCode)
            Return objAnsii.WriteGateoutFile(strDepotCode, bv_strGenerationMode)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_WriteWestimFile()"
    <OperationContract()> _
    Public Function pub_WriteWestimFile(ByVal bv_strWfdata As String, ByVal bv_strCustomerCode As String, ByVal bv_strGenerationMode As String) As String
        Try
            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            Dim objAnsii As New AnsiiEngine(bv_strCustomerCode, strDepotCode)
            Return objAnsii.WriteWestimFile(strDepotCode, bv_strGenerationMode)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


#Region "pub_WriteGateoutFileCodeco()"
    <OperationContract()> _
    Public Function pub_WriteCodeco(ByRef br_dsEdiDataSet As EDIDataSet, ByVal bv_strMovement As String, ByVal bv_strCustomerCode As String, ByVal bv_strGenerationMode As String, _
                                    ByVal bv_strwfData As String) As String
        Try
            Dim objCodeco As New Codeco(bv_strCustomerCode, CommonUIs.ParseWFDATA(bv_strwfData, "DPT_CD"))
            Return objCodeco.WriteCodeco(br_dsEdiDataSet, bv_strMovement, bv_strGenerationMode)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region


#Region "pub_GetEdiFileIdentifierByDpt_Id() TABLE NAME:"

    <OperationContract()> _
    Public Function pub_GetEdiFileIdentifierByDpt_Id(ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEdidataset As EDIDataSet
            Dim objEDIs As New EDIs
            Dim intDepotId As Integer
            intDepotId = CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID"))
            dsEdidataset = objEDIs.GetEdiFileIdentifierByDpt_Id(intDepotId)
            Return dsEdidataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region


#Region "pub_GetEdiMovementByDpt_Id() TABLE NAME:"

    <OperationContract()> _
    Public Function pub_GetEdiMovementByDpt_Id(ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEdidataset As EDIDataSet
            Dim objEDIs As New EDIs
            Dim intDepotId As Integer
            intDepotId = CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID"))
            dsEdidataset = objEDIs.GetEdiMovementByDpt_Id(intDepotId)
            Return dsEdidataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEdiSegmentDetailByDpt_Id() TABLE NAME:"

    <OperationContract()> _
    Public Function pub_GetEdiSegmentDetailByDpt_Id(ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEdidataset As EDIDataSet
            Dim objEDIs As New EDIs
            Dim intDepotId As Integer
            intDepotId = CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID"))
            dsEdidataset = objEDIs.GetEdiSegmentDetailByDpt_Id(intDepotId)
            Return dsEdidataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEdiSegmentHeaderByDpt_Id() TABLE NAME:"

    <OperationContract()> _
    Public Function pub_GetEdiSegmentHeaderByDpt_Id(ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEdidataset As EDIDataSet
            Dim objEDIs As New EDIs
            Dim intDepotId As Integer
            intDepotId = CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID"))
            dsEdidataset = objEDIs.GetEdiSegmentHeaderByDpt_Id(intDepotId)
            Return dsEdidataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEdiVersionByDpt_Id() TABLE NAME:"

    <OperationContract()> _
    Public Function pub_GetEdiVersionByDpt_Id(ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEdidataset As EDIDataSet
            Dim objEDIs As New EDIs
            Dim intDepotId As Integer
            intDepotId = CInt(CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_ID"))
            dsEdidataset = objEDIs.GetEdiVersionByDpt_Id(intDepotId)
            Return dsEdidataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetGateinRetByGI_DPT_TRMDS() TABLE NAME:GATEIN_RET"

    <OperationContract()> _
    Public Function pub_GetGateinRetByGI_DPT_TRMDS(ByVal bv_strCustomerCode As String, ByVal strStatus As String, ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            dsEDIDataSet = objEDIs.GetGateinRetByGI_DPT_TRMDS(strDepotCode, strStatus, bv_strCustomerCode)
            Return dsEDIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetGateoutRetByGO_DPT_TRMDS() TABLE NAME:GATEOUT_RET"

    <OperationContract()> _
    Public Function pub_GetGateoutRetByGO_DPT_TRMDS(ByVal bv_strCustomerCode As String, ByVal strStatus As String, ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            dsEDIDataSet = objEDIs.GetGateoutRetByGO_DPT_TRMDS(strDepotCode, strStatus, bv_strCustomerCode)
            Return dsEDIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetGateinRetNoRCByGI_DPT_TRMDS() TABLE NAME:GATEIN_RET"

    <OperationContract()> _
    Public Function pub_GetGateinRetNoRCByGI_DPT_TRMDS(ByVal bv_strCustomerCode As String, ByVal strStatus As String, ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            dsEDIDataSet = objEDIs.GetGateinRetNoRCByGI_DPT_TRMDS(strDepotCode, strStatus, bv_strCustomerCode)
            Return dsEDIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetRepairCompletenRetByGI_DPT_TRMDS() TABLE NAME:GATEIN_RET"

    <OperationContract()> _
    Public Function pub_GetRepairCompletenRetByGI_DPT_TRMDS(ByVal bv_strCustomerCode As String, ByVal strStatus As String, ByVal bv_strWfdata As String) As EDIDataSet

        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            Dim strDepotCode As String
            strDepotCode = CommonUIs.ParseWFDATA(bv_strWfdata, "DPT_CD")
            dsEDIDataSet = objEDIs.GetRepairCompleteRetNoRCByGI_DPT_TRMDS(strDepotCode, strStatus, bv_strCustomerCode, "C")
            Return dsEDIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEDI_Details() TABLE NAME:EDI"

    <OperationContract()> _
    Public Function pub_GetEDI_Details(ByVal intDepotID As Int32) As EDIDataSet
        'ByVal bv_strCustomerCode As String, ByVal strActivity As String
        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            dsEDIDataSet = objEDIs.GetEDIDetails(intDepotID)
            Return dsEDIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateEDI() TABLE NAME:EDI"
    <OperationContract()> _
    Public Function pub_CreateEDI(ByVal bv_Customer_cd As String, ByVal bv_strActivity As String, ByVal bv_strGenarated As DateTime, _
                                  ByVal bv_strRsnBit As String, ByVal bv_strAscFileName As String, ByVal bv_strFileformat As String, ByVal bv_strCreatedBy As String, _
                                  ByVal bv_strCreatedDt As DateTime, ByVal bv_lngCstmr_id As Int64, ByVal bv_strwfData As String) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objEDI As New EDIs
            Dim lngCreated As Long
            Dim strFilename As String()
            Dim strCustomer As Long
            'MultiDepo
            Dim strDepotCode As String
            Dim strDepotId As Int32
            strDepotCode = CommonUIs.ParseWFDATA(bv_strwfData, "DPT_CD")
            strDepotId = CInt(CommonUIs.ParseWFDATA(bv_strwfData, "DPT_ID"))
            strCustomer = bv_lngCstmr_id
            strFilename = bv_strAscFileName.Split(CChar(","))
            For i As Integer = 1 To strFilename.Length - 1
                lngCreated = objEDI.CreateEDI(strCustomer, bv_Customer_cd, _
                bv_strActivity, bv_strGenarated, _
                bv_strRsnBit, strFilename(i), _
                bv_strFileformat, _
                bv_strCreatedBy, bv_strCreatedDt, _
                strDepotId, strDepotCode, _
                objTransaction)
            Next

            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetDepot"
    Public Function pub_GetDepot(ByVal intDepot As Integer) As EDIDataSet
        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            dsEDIDataSet = objEDIs.GetDepoDetails(intDepot)
            Return dsEDIDataSet
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "pub_UpdateEmail_detail"

    <OperationContract()> _
    Public Function pub_EDIEmailCreate(ByVal bv_lngEdi As Int64, ByVal bv_intCustomer As Int64, ByVal bv_strFromMain As String, ByVal bv_strTomail As String, _
                                  ByVal bv_strBCCmail As String, ByVal bv_strSubject As String, ByVal bv_strEmailbody As String, ByVal bv_intDepoId As Int64, _
                                  ByVal bv_strUser As String, ByVal bv_dtCurrent As DateTime, ByVal bv_strmailMode As String, _
                                  ByVal bv_blnResend As Boolean) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objEDI As New EDIs
            Dim lngCreated As Long
            lngCreated = objEDI.CreateEDIEmail(bv_lngEdi, bv_intCustomer, bv_strFromMain, _
                bv_strTomail, bv_strBCCmail, _
                bv_strSubject, bv_strEmailbody, _
                bv_intDepoId, bv_strUser, _
                bv_dtCurrent, bv_strmailMode, bv_blnResend, _
                 objTransaction)
            objEDI.UpdateEDIEmaildetail(bv_lngEdi, bv_strUser, bv_dtCurrent, objTransaction)
            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEDI_EmailDetails() TABLE NAME:EDI"

    <OperationContract()> _
    Public Function pub_GetEDI_EmailDetails(ByVal intEdi As Int64) As EDIDataSet
        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            dsEDIDataSet = objEDIs.GetEDIEmailDetails(intEdi)
            Return dsEDIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetCustomerDetail()"
    <OperationContract()> _
    Public Function pub_GetCustomerDetail(ByVal intDepot As Integer, ByVal intCustomerID As Integer) As EDIDataSet
        Try
            Dim dsEdiDataset As EDIDataSet
            Dim objEdi As New EDIs
            '    dsEdiDataset = objEdi.pub_GetCustomerDetail(intDepot, intCustomerID)
            Return dsEdiDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEDIEmailDetail()"
    <OperationContract()> _
    Public Function pub_GetEDIEmailDetail(ByVal bv_intEdiId As Int64) As EDIDataSet
        Try
            Dim dsEdiEamilDataset As EDIDataSet
            Dim objEdiEmails As New EDIs
            dsEdiEamilDataset = objEdiEmails.GetEDIEmailDetail(bv_intEdiId)
            Return dsEdiEamilDataset
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetEDI_Settings _Details() TABLE NAME:EDI_SETTINGS"

    <OperationContract()> _
    Public Function pub_GetEDISettings_Details(ByVal bv_i32DepotID As Integer) As EDIDataSet
        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            dsEDIDataSet = objEDIs.GetEDISettingsDetails(bv_i32DepotID)
            Return dsEDIDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateEDISettingsDetails"
    <OperationContract()> _
    Public Function pub_UpdateEDISettingsDetails(ByRef br_dsEqpInfoDataset As EDIDataSet, ByVal bv_intDepotID As Integer, ByVal bv_strUserName As String) As Boolean
        Dim objTrans As New Transactions
        Try
            Dim objEDiSettings As New EDIs
            Dim lngCreated As Long
            Dim bolupdatebt As Boolean

            For Each drEqpInfo As DataRow In br_dsEqpInfoDataset.Tables(EDIData._V_EDI_SETTINGS).Rows
                Dim decTareWt As Decimal = 0
                Dim decGrossWt As Decimal = 0
                Dim decCapacity As Decimal = 0
                Dim strLastSurveyor As String = String.Empty
                Dim dtLastTestDate As DateTime = Nothing
                Dim dtNextTestDate As DateTime = Nothing
                Dim intLastTestTypeID As Integer = 0
                Dim intNextTestTypeID As Integer = 0
                Dim strValidityPeriod As String = String.Empty
                Dim strRemarks As String = String.Empty
                Dim dtGenaratedDateTime As String

                Dim dtToday As Date = Now
                Dim mi As String = CStr(dtToday.Minute)
                Dim hh As String = CStr(dtToday.Hour)
                If CDbl(mi) < 10 Then
                    mi = String.Concat("0", mi)
                End If
                If CDbl(hh) < 10 Then
                    hh = String.Concat("0", hh)
                End If
                Dim strcrnttime As String = String.Concat(hh, ":", mi)
                'For compare with genarated date and current datae
             

                Select Case drEqpInfo.RowState
                    Case DataRowState.Added

                        Dim strgnrtTime As String = CStr(drEqpInfo.Item(EDIData.GNRTN_TM))

                        If strgnrtTime.Replace(":", "") < strcrnttime.Replace(":", "") Then
                            dtGenaratedDateTime = CStr(Now.Date.AddDays(1) & " " & CStr((drEqpInfo.Item(EDIData.GNRTN_TM))))
                        Else
                            dtGenaratedDateTime = Now.Date & " " & CStr((drEqpInfo.Item(EDIData.GNRTN_TM)))
                        End If

                        lngCreated = objEDiSettings.CreateEdiSettings(CInt(drEqpInfo.Item(EDIData.CSTMR_ID)), _
                                                                      CInt(drEqpInfo.Item(EDIData.FLE_FRMT_ID)), _
                                                                      drEqpInfo.Item(EDIData.TO_EML).ToString, _
                                                                      drEqpInfo.Item(EDIData.CC_EML).ToString, _
                                                                      CDate(dtGenaratedDateTime), _
                                                                      CStr((drEqpInfo.Item(EDIData.GNRTN_TM))), _
                                                                      CStr((drEqpInfo.Item(EDIData.SBJCT_VCR))), _
                                                                      CBool(drEqpInfo.Item(EDIData.ACTV_BT)), _
                                                                      DateTime.Now, _
                                                                      bv_strUserName, _
                                                                      bv_intDepotID, _
                                                                      objTrans)
                        drEqpInfo.Item(EDIData.EDI_STTNGS_ID) = lngCreated

                    Case DataRowState.Modified

                        Dim strgnrtTime As String = CStr(drEqpInfo.Item(EDIData.GNRTN_TM))

                        If strgnrtTime.Replace(":", "") < strcrnttime.Replace(":", "") Then
                            dtGenaratedDateTime = CStr(Now.Date.AddDays(1) & " " & CStr((drEqpInfo.Item(EDIData.GNRTN_TM))))
                        Else
                            dtGenaratedDateTime = Now.Date & " " & CStr((drEqpInfo.Item(EDIData.GNRTN_TM)))
                        End If

                        bolupdatebt = objEDiSettings.UpdateEdiSettings(CInt(CommonUIs.iLng(drEqpInfo.Item(EDIData.EDI_STTNGS_ID))), _
                                                                       CInt(drEqpInfo.Item(EDIData.CSTMR_ID)), _
                                                                       CInt(drEqpInfo.Item(EDIData.FLE_FRMT_ID)), _
                                                                       drEqpInfo.Item(EDIData.TO_EML).ToString, _
                                                                       drEqpInfo.Item(EDIData.CC_EML).ToString, _
                                                                       CDate(dtGenaratedDateTime), _
                                                                         CStr((drEqpInfo.Item(EDIData.GNRTN_TM))), _
                                                                      CStr((drEqpInfo.Item(EDIData.SBJCT_VCR))), _
                                                                       CBool(drEqpInfo.Item(EDIData.ACTV_BT)), _
                                                                     objTrans)


                    Case DataRowState.Deleted

                        objEDiSettings.DeleteEDISettings(CInt(CommonUIs.iLng(drEqpInfo.Item(EDIData.EDI_STTNGS_ID, DataRowVersion.Original))), _
                                                                      objTrans)
                End Select

            Next
            objTrans.commit()
            Return True
        Catch ex As Exception
            objTrans.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        Finally
            objTrans = Nothing
        End Try
    End Function
#End Region

#Region "pub_ValidateEDI_Settings _Details() TABLE NAME:EDI_SETTINGS"

    <OperationContract()> _
    Public Function pub_ValidateEDI_Settings(ByVal bv_Customer As Int64, ByVal bv_Cstmr_CD As String, ByVal bv_i32DepotID As Integer) As Boolean
        Try
            Dim dsEDIDataSet As EDIDataSet
            Dim objEDIs As New EDIs
            'dsCommonUI = objCommonUI.GetServicePartnerByCode(bv_strCode, bv_intDepotID)

            dsEDIDataSet = objEDIs.ValidateEDISettingsDetails(bv_Customer, bv_i32DepotID)
            If dsEDIDataSet.Tables(EDIData._V_EDI_SETTINGS).Rows.Count > 0 Then
                bv_Cstmr_CD = CStr(dsEDIDataSet.Tables(EDIData._V_EDI_SETTINGS).Rows(0).Item(EDIData.CSTMR_CD))
                pub_ValidateEDI_Settings = False
            Else
                pub_ValidateEDI_Settings = True
            End If

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
    '' Dummy
    Public Function pub_WriteCodeco(ByRef br_dsEdiDataSet As EDIDataSet, ByVal bv_strMovement As String, ByVal bv_strCustomerCode As String, ByVal bv_strGenerationMode As String) As String
        Try

        Catch ex As Exception

        End Try
    End Function
End Class
