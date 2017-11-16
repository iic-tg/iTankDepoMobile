#Region " Tracking.vb"
'*********************************************************************************************************************
'Name :
'      Tracking.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Tracking.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      6/13/2013 6:16:48 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
<ServiceContract()> _
Public Class Tracking

#Region "GetTrackingDetails() TABLE NAME:TRACKING"
    <OperationContract()> _
    Public Function GetTrackingDetails(ByVal bv_intCstmr_ID As String, ByVal bv_strContainerNo As String, _
                                  ByVal bv_strPickUpDate As String, ByVal bv_strReceivedDate As String, _
                                  ByVal bv_strTransmission As String, ByVal bv_intLss_ID As String, ByVal bv_strActvty_Nam As String, ByVal bv_strWFDATA As String) As TrackingDataSet
        Try
            Dim objTrackings As New Trackings
            Dim dsTracking As New TrackingDataSet
            Dim strDepotId As String = CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID)
            dsTracking = objTrackings.GetTrackingDetails(bv_intCstmr_ID, bv_strContainerNo, bv_strPickUpDate, bv_strReceivedDate, _
                                         bv_strTransmission, bv_intLss_ID, bv_strActvty_Nam, strDepotId)
            Return dsTracking
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateBulk_Email() TABLE NAME:BULK_EMAIL"
    <OperationContract()> _
    Public Function pub_CreateBulk_Email(ByVal bv_strFrom As String, ByVal bv_strTo As String, _
                                         ByVal bv_strSubject As String, ByVal bv_strBody As String, _
                                         ByVal bv_CreatedBy As String, ByVal bv_datCreatedDat As Date, _
                                         ByVal bv_intDepotid As Int32, ByRef br_dsTracking As TrackingDataSet) As Long
        Dim objTransaction As New Transactions()
        Try
            Dim objTrackings As New Trackings
            Dim lngCreated As Long

            lngCreated = objTrackings.CreateBulk_Email(bv_strFrom, bv_strTo, _
                                                       bv_strSubject, bv_strBody, _
                                                       CDate(bv_datCreatedDat), bv_CreatedBy, _
                                                       True, bv_intDepotid, objTransaction)

            pub_CreateBulk_Email_Detail(br_dsTracking, CInt(lngCreated), objTransaction)
            objTransaction.commit()
            Return lngCreated
        Catch ex As Exception
            objTransaction.RollBack()
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_CreateBulk_Email_Detail() TABLE NAME:BULK_EMAIL_DETAIL"
    <OperationContract()> _
    Public Function pub_CreateBulk_Email_Detail(ByRef br_dsTracking As TrackingDataSet, ByVal bv_BulkMailID As Integer, ByRef br_ObjTransactions As Transactions) As Long

        Try
            Dim objTrackings As New Trackings
            Dim lngCreated As Long
            Dim dtBulKMailDetail As DataTable = br_dsTracking.Tables(TrackingData._V_TRACKING)
            For Each drDetail As DataRow In dtBulKMailDetail.Rows
                If (drDetail.Item(TrackingData.BLK_MAIL_BT).ToString = "True") Then
                    lngCreated = objTrackings.CreateBulk_Email_Detail(bv_BulkMailID, _
                                                                                  CStr(drDetail.Item(TrackingData.ACTVTY_NAM)), _
                                                                                  CStr(drDetail.Item(TrackingData.TRNSMSSN_NO)), _
                                                                                  CStr(drDetail.Item(TrackingData.EQPMNT_NO)), CStr("1"), br_ObjTransactions)

                End If
            Next
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GetParameter() TABLE NAME:Hashtable"
    <OperationContract()> _
    Public Function pub_GetParameter(ByVal bv_strKey As String, ByVal bv_paramdata As String) As String
        Dim NMColl As Hashtable
        Dim strKeyValue As String = ""
        NMColl = ParseParameter(bv_paramdata)
        If NMColl.Contains(bv_strKey) Then
            If NMColl.Item(bv_strKey).ToString <> "" Then
                strKeyValue = NMColl.Item(bv_strKey).ToString
            End If
        End If
        Return strKeyValue
    End Function
#End Region

#Region "ParseParameter"
    <OperationContract()> _
    Public Shared Function ParseParameter(ByVal qrystr As String) As Hashtable
        Dim hstble As New Hashtable
        Dim strItems() As String
        strItems = qrystr.Split(CChar("&"))
        If strItems.Length = 0 Then
            Throw New Exception("Input parameter is not valid")
        End If
        For i As Integer = 0 To strItems.Length - 1
            If strItems(i) <> "" Then
                hstble.Add(strItems(i).Split(CChar("="))(0), strItems(i).Split(CChar("="))(1))
            End If
        Next i

        ParseParameter = hstble

        hstble = Nothing
        strItems = Nothing
    End Function
#End Region


End Class
