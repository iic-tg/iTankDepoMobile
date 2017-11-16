#Region " ExchangeRate.vb"
'*********************************************************************************************************************
'Name :
'      ExchangeRate.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(ExchangeRate.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 4:11:58 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()>
Public Class ExchangeRate

#Region "pub_GetEXCHANGERATEByDPTID() TABLE NAME:EXCHANGE_RATE"
    <OperationContract()>
    Public Function pub_GetEXCHANGERATEByDPTID(ByVal bv_strWFDATA As String) As ExchangeRateDataSet

        Try
            Dim objEXCHANGERATE As ExchangeRateDataSet
            Dim obExchangeRates As New ExchangeRates
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            objEXCHANGERATE = obExchangeRates.GetEXCHANGE_RATEByDPT_ID(intDepotID)
            Return objEXCHANGERATE
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "pub_UpdateExchangeRate()"
    <OperationContract()> _
    Public Function pub_UpdateExchangeRate(ByRef dsExchangeRate As ExchangeRateDataSet, ByVal bv_strModifiedBy As String, ByVal bv_datModifieddate As DateTime, ByVal bv_DptId As Long) As Boolean
        Try
            Dim dtExchangeRate As DataTable
            Dim ObjExchangeRates As New ExchangeRates
            Dim bolupdatebt As Boolean
            dtExchangeRate = dsExchangeRate.Tables(ExchangeRateData._EXCHANGE_RATE)
            For Each drExchangeRate As DataRow In dtExchangeRate.Rows
                Select Case drExchangeRate.RowState
                    Case DataRowState.Added
                        Dim lngCreated As Long = ObjExchangeRates.CreateEXCHANGE_RATE(CInt(drExchangeRate.Item(ExchangeRateData.FRM_CRRNCY_ID)), _
                                                                                            CInt(drExchangeRate.Item(ExchangeRateData.TO_CRRNCY_ID)), _
                                                                                            CDec(drExchangeRate.Item(ExchangeRateData.EXCHNG_RT_PR_UNT_NC)), _
                                                                                            CDate(drExchangeRate.Item(ExchangeRateData.WTH_EFFCT_FRM_DT)), _
                                                                                             bv_strModifiedBy, bv_datModifieddate, bv_strModifiedBy, bv_datModifieddate, _
                                                                                           CBool(drExchangeRate.Item(ExchangeRateData.ACTV_BT)), CInt(bv_DptId))

                        drExchangeRate.Item(ExchangeRateData.EXCHNG_RT_ID) = lngCreated
                    Case DataRowState.Modified
                        bolupdatebt = ObjExchangeRates.UpdateEXCHANGE_RATE(CInt(drExchangeRate.Item(ExchangeRateData.EXCHNG_RT_ID)), _
                                                                                           CInt(drExchangeRate.Item(ExchangeRateData.FRM_CRRNCY_ID)), _
                                                                                            CInt(drExchangeRate.Item(ExchangeRateData.TO_CRRNCY_ID)), _
                                                                                            CDec(drExchangeRate.Item(ExchangeRateData.EXCHNG_RT_PR_UNT_NC)), _
                                                                                            CDate(drExchangeRate.Item(ExchangeRateData.WTH_EFFCT_FRM_DT)), _
                                                                                             bv_strModifiedBy, bv_datModifieddate, _
                                                                                           CBool(drExchangeRate.Item(ExchangeRateData.ACTV_BT)), CInt(bv_DptId))
                End Select
            Next
            Return bolupdatebt
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

  

End Class
