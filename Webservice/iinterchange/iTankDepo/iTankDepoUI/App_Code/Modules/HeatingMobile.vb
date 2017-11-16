Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class HeatingMobile
    Inherits System.Web.Services.WebService

    Dim gateInMobile As New GateinMobile_C
    Dim dsHeating As HeatingDataSet
    Dim strDepotCurrency As String = String.Empty
    Dim intDepotId As Integer
    Private pvt_strTableKey As String
    Private pvt_strTableName As String
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"

    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function HeatingList(ByVal UserName As String) As ReturnHeating



        gateInMobile.DepotID(UserName)

        Try



            Dim objHeating As New Heating
            Dim objCommon As New CommonData
            Dim drHeating As DataRow
            Dim dtHeating As DataTable
            Dim strWfData As String = objCommon.GenerateWFData(90)
            intDepotId = CommonWeb.iInt(objCommon.GetDepotID())
            'ifgHeating.UseCachedDataSource = True
            dsHeating = objHeating.pub_GetHeatingFromGateIn(intDepotId)
            Dim dsHeatingCustomer As New HeatingDataSet
            Dim dsHeatingCharge As New HeatingDataSet
            dtHeating = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clone()
            dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Rows.Clear()
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsHeatingCustomer = objHeating.pub_GetCustomerDetail(objCommon.GetHeadQuarterID())
            Else
                dsHeatingCustomer = objHeating.pub_GetCustomerDetail(intDepotId)
            End If

            dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._CUSTOMER))
            dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE).Rows.Clear()
            dsHeatingCharge = objHeating.pub_VHeatingChargeByDepot(intDepotId)
            dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE))

            For Each dr As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Rows
                drHeating = dtHeating.NewRow()
                drHeating.Item(HeatingData.HTNG_ID) = CommonWeb.GetNextIndex(dtHeating, HeatingData.HTNG_ID)
                drHeating.Item(HeatingData.CSTMR_ID) = dr.Item(HeatingData.CSTMR_ID)
                drHeating.Item(HeatingData.CSTMR_CD) = dr.Item(HeatingData.CSTMR_CD)
                drHeating.Item(HeatingData.EQPMNT_NO) = dr.Item(HeatingData.EQPMNT_NO)
                drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
                drHeating.Item(HeatingData.EQPMNT_TYP_CD) = dr.Item(HeatingData.EQPMNT_TYP_CD)
                drHeating.Item(HeatingData.EQPMNT_CD_CD) = dr.Item(HeatingData.EQPMNT_CD_CD)
                drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
                drHeating.Item(HeatingData.EQPMNT_CD_ID) = dr.Item(HeatingData.EQPMNT_CD_ID)
                drHeating.Item(HeatingData.PRDCT_ID) = dr.Item(HeatingData.PRDCT_ID)
                drHeating.Item(HeatingData.PRDCT_DSCRPTN_VC) = dr.Item(HeatingData.PRDCT_DSCRPTN_VC)
                drHeating.Item(HeatingData.GTN_DT) = CDate(dr.Item(HeatingData.GTN_DT)).ToString("dd-MMM-yyyy")
                drHeating.Item(HeatingData.GI_TRNSCTN_NO) = dr.Item(HeatingData.GI_TRNSCTN_NO)
                drHeating.Item(HeatingData.YRD_LCTN) = dr.Item(HeatingData.YRD_LCTN)

                Dim drHeatingcharge As DataRow()
                drHeatingcharge = dsHeatingCharge.Tables(HeatingData._HEATING_CHARGE).Select(String.Concat(HeatingData.GI_TRNSCTN_NO, "='", dr.Item(HeatingData.GI_TRNSCTN_NO), "'"))
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_CD) = drHeatingcharge(0).Item(HeatingData.HTNG_CD)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_STRT_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_DT)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_STRT_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_TM)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_END_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_END_DT)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_END_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_END_TM)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HTNG_TMPRTR) = drHeatingcharge(0).Item(HeatingData.HTNG_TMPRTR)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.TTL_HTN_PRD) = drHeatingcharge(0).Item(HeatingData.TTL_HTN_PRD)
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drHeatingcharge(0).Item(HeatingData.MIN_HTNG_RT_NC)
                Else
                    For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                        drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_RT_NC)
                        Exit For
                    Next
                End If
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.HRLY_CHRG_NC) = drHeatingcharge(0).Item(HeatingData.HRLY_CHRG_NC)
                Else
                    For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                        drHeating.Item(HeatingData.HRLY_CHRG_NC) = drCstmrDetail.Item(HeatingData.HRLY_CHRG_NC)
                        Exit For
                    Next
                End If
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                    strDepotCurrency = drCstmrDetail.Item(CommonUIData.CRRNCY_CD)
                    drHeating.Item(HeatingData.CSTMR_CRRNCY_CD) = drCstmrDetail.Item(HeatingData.CSTMR_CRRNCY_CD)
                    Exit For
                Next
                If drHeatingcharge.Length > 0 Then
                    drHeating.Item(HeatingData.TTL_RT_NC) = drHeatingcharge(0).Item(HeatingData.TTL_RT_NC)
                End If
                If Not (dr.Item(HeatingData.MIN_HTNG_PRD_NC) Is DBNull.Value) Then
                    drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = dr.Item(HeatingData.MIN_HTNG_PRD_NC)
                Else
                    For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                dr.Item(GateinData.CSTMR_CD), "'"))
                        drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_PRD_NC)
                        Exit For
                    Next
                End If
                dtHeating.Rows.Add(drHeating)
            Next

            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clear()
            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Merge(dtHeating)

            Dim arrayList As New ArrayList

            For Each dt As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Rows

                Dim HeatingMobileModel As New HeatingMobileModel

                HeatingMobileModel.EQPMNT_NO = dt.Item("EQPMNT_NO").ToString()
                HeatingMobileModel.CSTMR_CD = dt.Item("CSTMR_CD").ToString()
                HeatingMobileModel.EQPMNT_TYP_CD = dt.Item("EQPMNT_TYP_CD").ToString()
                HeatingMobileModel.PRDCT_DSCRPTN_VC = dt.Item("PRDCT_DSCRPTN_VC").ToString()
                HeatingMobileModel.GTN_DT = dt.Item("GTN_DT").ToString()
                HeatingMobileModel.YRD_LCTN = dt.Item("YRD_LCTN").ToString()
                HeatingMobileModel.GI_TRNSCTN_NO = dt.Item("GI_TRNSCTN_NO").ToString()
                HeatingMobileModel.HTNG_STRT_DT = dt.Item("HTNG_STRT_DT").ToString()
                HeatingMobileModel.HTNG_STRT_TM = dt.Item("HTNG_STRT_TM").ToString()
                HeatingMobileModel.HTNG_END_DT = dt.Item("HTNG_END_DT").ToString()
                HeatingMobileModel.HTNG_END_TM = dt.Item("HTNG_END_TM").ToString()
                HeatingMobileModel.HTNG_TMPRTR = dt.Item("HTNG_TMPRTR").ToString()
                HeatingMobileModel.TTL_HTN_PRD = dt.Item("TTL_HTN_PRD").ToString()
                HeatingMobileModel.MIN_HTNG_RT_NC = dt.Item("MIN_HTNG_RT_NC").ToString()
                HeatingMobileModel.HRLY_CHRG_NC = dt.Item("HRLY_CHRG_NC").ToString()
                HeatingMobileModel.CSTMR_CRRNCY_CD = dt.Item("CSTMR_CRRNCY_CD").ToString()
                HeatingMobileModel.MIN_HTNG_PRD_NC = dt.Item("MIN_HTNG_PRD_NC").ToString()
                HeatingMobileModel.TTL_RT_NC = dt.Item("TTL_RT_NC").ToString()

                arrayList.Add(HeatingMobileModel)

            Next


            Dim ReturnHeating As New ReturnHeating

            ReturnHeating.Status = "Success"
            ReturnHeating.HeatingArray = arrayList


            Return ReturnHeating
        Catch ex As Exception

            Dim ReturnHeating As New ReturnHeating
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

            ReturnHeating.Status = ex.Message
            Return ReturnHeating


        End Try

    End Function


  

    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function CalculateHeatingPeriod(ByVal str_HeatingStartDate As String, _
                                     ByVal str_HeatingStartTime As String, _
                                     ByVal str_HeatingEndDate As String, _
                                     ByVal str_HeatingEndTime As String) As HeatingPeriod
        Try
            Dim startDate As Date = str_HeatingStartDate
            Dim startTime As DateTime = str_HeatingStartTime
            Dim endDate As Date = str_HeatingEndDate
            Dim endTime As DateTime = str_HeatingEndTime 'add 3 days to startDate
            Dim timeSpan As TimeSpan = endDate.Subtract(startDate)
            Dim hour As Double = (endTime.Subtract(startTime)).TotalHours
            Dim difHr As Double = (timeSpan.TotalHours) 'get 0 hours although 3 days difference

            'Dim TotalHours As String = difHr + hour

            Dim TotalHours As Double = difHr + hour
            Dim HRS As String = TotalHours.ToString("0.00")


            Dim HeatingPeriod As New HeatingPeriod
            HeatingPeriod.TotalHeatingPeriod = HRS

            HeatingPeriod.Status = "Success"

            Return HeatingPeriod

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)
            Dim HeatingPeriod As New HeatingPeriod
            HeatingPeriod.Status = ex.Message

            Return HeatingPeriod

        End Try
    End Function


    <WebMethod(enableSession:=True)> _
 <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function CalculateTotalRate(ByVal str_MinRate As String, _
                                     ByVal str_TotaPeriod As String, _
                                     ByVal str_MinHeatingPerod As String, _
                                     ByVal str_HourlyRate As String) As HeatingTotalRates
        Try
            Dim MinRate As Decimal = str_MinRate
            Dim TotaPeriod As Decimal = str_TotaPeriod
            Dim MinHeatingPerod As Decimal = str_MinHeatingPerod
            Dim HourlyRate As Decimal = str_HourlyRate
            Dim TotalRate As Decimal
            If CDbl(str_MinHeatingPerod) <= CDbl(str_TotaPeriod) Then
                TotalRate = str_MinRate + (str_TotaPeriod - str_MinHeatingPerod) * str_HourlyRate
            ElseIf CDbl(str_MinHeatingPerod) > CDbl(str_TotaPeriod) Then
                TotalRate = str_MinRate
            End If

            TotalRate = FormatNumber(CDbl(TotalRate), 2)

            Dim HeatingRate As New HeatingTotalRates
            HeatingRate.TotalHeatingRate = TotalRate
            HeatingRate.Status = "Success"

            Return HeatingRate

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                MethodBase.GetCurrentMethod.Name, ex.Message)

            Dim HeatingRate As New HeatingTotalRates

            HeatingRate.Status = ex.Message

            Return HeatingRate

        End Try
    End Function



    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function filter(ByVal filterType As String, ByVal filterCondition As String, ByVal filterValue As String, ByVal UserName As String, ByVal Mode As String) As ListModel

        Dim conn As New Dropdown_C
        Dim objCommonData As New CommonData
        gateInMobile.DepotID(UserName)
        Dim listVlaues As New ListModel
        Dim arraylist As New ArrayList
        Dim filterNewConditon As String
        Dim ds As DataSet
        Dim dtPreaAdvice As DataTable
        Dim query As String
        If filterCondition = "Similar" Or filterCondition = "Contains" Then
            filterNewConditon = "LIKE"
        Else
            filterNewConditon = "Not LIKE"
        End If

        ds = New DataSet()
       
                If filterCondition = "Equals" Then

            ' query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"
            query = "SELECT CSTMR_ID,CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,YRD_LCTN,GTN_DT,PRDCT_ID,PRDCT_CD,GI_TRNSCTN_NO,PRDCT_DSCRPTN_VC FROM V_GATEIN WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND HTNG_BT=1 AND " + filterType + "='" + filterValue + "' AND GI_TRNSCTN_NO NOT IN (SELECT GI_TRNSCTN_NO FROM HEATING_CHARGE WHERE BLLNG_FLG = 'B' AND DPT_ID='" + objCommonData.GetDepotID() + "') ORDER BY GTN_DT DESC"
            ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else
            ' query = "SELECT distinct " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"
            query = "SELECT distinct CSTMR_ID,CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,YRD_LCTN,GTN_DT,PRDCT_ID,PRDCT_CD,GI_TRNSCTN_NO,PRDCT_DSCRPTN_VC FROM V_GATEIN WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND HTNG_BT=1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%' AND GI_TRNSCTN_NO NOT IN (SELECT GI_TRNSCTN_NO FROM HEATING_CHARGE WHERE BLLNG_FLG = 'B' AND DPT_ID='" + objCommonData.GetDepotID() + "') ORDER BY GTN_DT DESC"
            ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                End If

           


        dtPreaAdvice = ds.Tables(0)

        For Each dd As DataRow In dtPreaAdvice.Rows

            Dim filteredvalues As New FilteredValues
            filteredvalues.Values = dd.Item(filterType).ToString()

            arraylist.Add(filteredvalues)
        Next


        listVlaues.ListGateInss = arraylist

        Return listVlaues

    End Function




    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function SearchList(ByVal UserName As String, ByVal SearchValues As SearchValues, ByVal filterType As String, ByVal Mode As String) As ReturnHeating

        gateInMobile.DepotID(UserName)

        Try



            Dim objHeating As New Heating
            Dim objCommon As New CommonData
            Dim drHeating As DataRow
            Dim dtHeating As DataTable
            Dim strWfData As String = objCommon.GenerateWFData(90)
            intDepotId = CommonWeb.iInt(objCommon.GetDepotID())
            'ifgHeating.UseCachedDataSource = True
            dsHeating = objHeating.pub_GetHeatingFromGateIn(intDepotId)
            Dim dsHeatingCustomer As New HeatingDataSet
            Dim dsHeatingCharge As New HeatingDataSet
            dtHeating = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clone()
            dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Rows.Clear()
            If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
                dsHeatingCustomer = objHeating.pub_GetCustomerDetail(objCommon.GetHeadQuarterID())
            Else
                dsHeatingCustomer = objHeating.pub_GetCustomerDetail(intDepotId)
            End If

            dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._CUSTOMER))
            dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE).Rows.Clear()
            dsHeatingCharge = objHeating.pub_VHeatingChargeByDepot(intDepotId)
            dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE))

            For Each dd In SearchValues.SearchValues


                For Each dr As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Select(String.Concat(filterType, "='", dd.values, "'"))

                    drHeating = dtHeating.NewRow()
                    drHeating.Item(HeatingData.HTNG_ID) = CommonWeb.GetNextIndex(dtHeating, HeatingData.HTNG_ID)
                    drHeating.Item(HeatingData.CSTMR_ID) = dr.Item(HeatingData.CSTMR_ID)
                    drHeating.Item(HeatingData.CSTMR_CD) = dr.Item(HeatingData.CSTMR_CD)
                    drHeating.Item(HeatingData.EQPMNT_NO) = dr.Item(HeatingData.EQPMNT_NO)
                    drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
                    drHeating.Item(HeatingData.EQPMNT_TYP_CD) = dr.Item(HeatingData.EQPMNT_TYP_CD)
                    drHeating.Item(HeatingData.EQPMNT_CD_CD) = dr.Item(HeatingData.EQPMNT_CD_CD)
                    drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
                    drHeating.Item(HeatingData.EQPMNT_CD_ID) = dr.Item(HeatingData.EQPMNT_CD_ID)
                    drHeating.Item(HeatingData.PRDCT_ID) = dr.Item(HeatingData.PRDCT_ID)
                    drHeating.Item(HeatingData.PRDCT_DSCRPTN_VC) = dr.Item(HeatingData.PRDCT_DSCRPTN_VC)
                    drHeating.Item(HeatingData.GTN_DT) = CDate(dr.Item(HeatingData.GTN_DT)).ToString("dd-MMM-yyyy")
                    drHeating.Item(HeatingData.GI_TRNSCTN_NO) = dr.Item(HeatingData.GI_TRNSCTN_NO)
                    drHeating.Item(HeatingData.YRD_LCTN) = dr.Item(HeatingData.YRD_LCTN)

                    Dim drHeatingcharge As DataRow()
                    drHeatingcharge = dsHeatingCharge.Tables(HeatingData._HEATING_CHARGE).Select(String.Concat(HeatingData.GI_TRNSCTN_NO, "='", dr.Item(HeatingData.GI_TRNSCTN_NO), "'"))
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.HTNG_CD) = drHeatingcharge(0).Item(HeatingData.HTNG_CD)
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.HTNG_STRT_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_DT)
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.HTNG_STRT_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_TM)
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.HTNG_END_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_END_DT)
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.HTNG_END_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_END_TM)
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.HTNG_TMPRTR) = drHeatingcharge(0).Item(HeatingData.HTNG_TMPRTR)
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.TTL_HTN_PRD) = drHeatingcharge(0).Item(HeatingData.TTL_HTN_PRD)
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drHeatingcharge(0).Item(HeatingData.MIN_HTNG_RT_NC)
                    Else
                        For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                    dr.Item(GateinData.CSTMR_CD), "'"))
                            drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_RT_NC)
                            Exit For
                        Next
                    End If
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.HRLY_CHRG_NC) = drHeatingcharge(0).Item(HeatingData.HRLY_CHRG_NC)
                    Else
                        For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                    dr.Item(GateinData.CSTMR_CD), "'"))
                            drHeating.Item(HeatingData.HRLY_CHRG_NC) = drCstmrDetail.Item(HeatingData.HRLY_CHRG_NC)
                            Exit For
                        Next
                    End If
                    For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                    dr.Item(GateinData.CSTMR_CD), "'"))
                        strDepotCurrency = drCstmrDetail.Item(CommonUIData.CRRNCY_CD)
                        drHeating.Item(HeatingData.CSTMR_CRRNCY_CD) = drCstmrDetail.Item(HeatingData.CSTMR_CRRNCY_CD)
                        Exit For
                    Next
                    If drHeatingcharge.Length > 0 Then
                        drHeating.Item(HeatingData.TTL_RT_NC) = drHeatingcharge(0).Item(HeatingData.TTL_RT_NC)
                    End If
                    If Not (dr.Item(HeatingData.MIN_HTNG_PRD_NC) Is DBNull.Value) Then
                        drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = dr.Item(HeatingData.MIN_HTNG_PRD_NC)
                    Else
                        For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                                    dr.Item(GateinData.CSTMR_CD), "'"))
                            drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_PRD_NC)
                            Exit For
                        Next
                    End If
                    dtHeating.Rows.Add(drHeating)
                Next

            Next

            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clear()
            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Merge(dtHeating)

            Dim arrayList As New ArrayList

            For Each dt As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Rows

                Dim HeatingMobileModel As New HeatingMobileModel

                HeatingMobileModel.EQPMNT_NO = dt.Item("EQPMNT_NO").ToString()
                HeatingMobileModel.CSTMR_CD = dt.Item("CSTMR_CD").ToString()
                HeatingMobileModel.EQPMNT_TYP_CD = dt.Item("EQPMNT_TYP_CD").ToString()
                HeatingMobileModel.PRDCT_DSCRPTN_VC = dt.Item("PRDCT_DSCRPTN_VC").ToString()
                HeatingMobileModel.GTN_DT = dt.Item("GTN_DT").ToString()
                HeatingMobileModel.YRD_LCTN = dt.Item("YRD_LCTN").ToString()
                HeatingMobileModel.GI_TRNSCTN_NO = dt.Item("GI_TRNSCTN_NO").ToString()
                HeatingMobileModel.HTNG_STRT_DT = dt.Item("HTNG_STRT_DT").ToString()
                HeatingMobileModel.HTNG_STRT_TM = dt.Item("HTNG_STRT_TM").ToString()
                HeatingMobileModel.HTNG_END_DT = dt.Item("HTNG_END_DT").ToString()
                HeatingMobileModel.HTNG_END_TM = dt.Item("HTNG_END_TM").ToString()
                HeatingMobileModel.HTNG_TMPRTR = dt.Item("HTNG_TMPRTR").ToString()
                HeatingMobileModel.TTL_HTN_PRD = dt.Item("TTL_HTN_PRD").ToString()
                HeatingMobileModel.MIN_HTNG_RT_NC = dt.Item("MIN_HTNG_RT_NC").ToString()
                HeatingMobileModel.HRLY_CHRG_NC = dt.Item("HRLY_CHRG_NC").ToString()
                HeatingMobileModel.CSTMR_CRRNCY_CD = dt.Item("CSTMR_CRRNCY_CD").ToString()
                HeatingMobileModel.MIN_HTNG_PRD_NC = dt.Item("MIN_HTNG_PRD_NC").ToString()
                HeatingMobileModel.TTL_RT_NC = dt.Item("TTL_RT_NC").ToString()

                arrayList.Add(HeatingMobileModel)

            Next


            Dim ReturnHeating As New ReturnHeating

            ReturnHeating.Status = "Success"
            ReturnHeating.HeatingArray = arrayList


            Return ReturnHeating
        Catch ex As Exception

            Dim ReturnHeating As New ReturnHeating
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)

            ReturnHeating.Status = ex.Message
            Return ReturnHeating


        End Try

    End Function


    <WebMethod(enableSession:=True)> _
 <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function Update(ByVal UserName As String, ByVal EquipmentNo As String, ByVal StartDate As String,
                           ByVal StartTime As String, ByVal EndDate As String, ByVal EndTime As String,
                           ByVal Temp As String,
                           ByVal TotalPeriod As String, ByVal MinRate As String, ByVal HourlyRate As String,
                           ByVal TotalRate As String) As HeatingUpdateStatus

        Try

        

        gateInMobile.DepotID(UserName)
        Dim objHeating As New Heating
        Dim objCommon As New CommonData
        Dim drHeating As DataRow
        Dim dtHeating As DataTable
        Dim strWfData As String = objCommon.GenerateWFData(90)
        intDepotId = CommonWeb.iInt(objCommon.GetDepotID())
        'ifgHeating.UseCachedDataSource = True
        dsHeating = objHeating.pub_GetHeatingFromGateIn(intDepotId)
        Dim dsHeatingCustomer As New HeatingDataSet
        Dim dsHeatingCharge As New HeatingDataSet
        dtHeating = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clone()
        dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Rows.Clear()
        If objCommon.GetMultiLocationSupportConfig().ToLower = "true" Then
            dsHeatingCustomer = objHeating.pub_GetCustomerDetail(objCommon.GetHeadQuarterID())
        Else
            dsHeatingCustomer = objHeating.pub_GetCustomerDetail(intDepotId)
        End If

        dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._CUSTOMER))
        dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE).Rows.Clear()
        dsHeatingCharge = objHeating.pub_VHeatingChargeByDepot(intDepotId)
        dsHeating.Merge(dsHeatingCustomer.Tables(HeatingData._HEATING_CHARGE))

        For Each dr As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Rows
            drHeating = dtHeating.NewRow()
            drHeating.Item(HeatingData.HTNG_ID) = CommonWeb.GetNextIndex(dtHeating, HeatingData.HTNG_ID)
            drHeating.Item(HeatingData.CSTMR_ID) = dr.Item(HeatingData.CSTMR_ID)
            drHeating.Item(HeatingData.CSTMR_CD) = dr.Item(HeatingData.CSTMR_CD)
            drHeating.Item(HeatingData.EQPMNT_NO) = dr.Item(HeatingData.EQPMNT_NO)
            drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
            drHeating.Item(HeatingData.EQPMNT_TYP_CD) = dr.Item(HeatingData.EQPMNT_TYP_CD)
            drHeating.Item(HeatingData.EQPMNT_CD_CD) = dr.Item(HeatingData.EQPMNT_CD_CD)
            drHeating.Item(HeatingData.EQPMNT_TYP_ID) = dr.Item(HeatingData.EQPMNT_TYP_ID)
            drHeating.Item(HeatingData.EQPMNT_CD_ID) = dr.Item(HeatingData.EQPMNT_CD_ID)
            drHeating.Item(HeatingData.PRDCT_ID) = dr.Item(HeatingData.PRDCT_ID)
            drHeating.Item(HeatingData.PRDCT_DSCRPTN_VC) = dr.Item(HeatingData.PRDCT_DSCRPTN_VC)
            drHeating.Item(HeatingData.GTN_DT) = CDate(dr.Item(HeatingData.GTN_DT)).ToString("dd-MMM-yyyy")
            drHeating.Item(HeatingData.GI_TRNSCTN_NO) = dr.Item(HeatingData.GI_TRNSCTN_NO)
            drHeating.Item(HeatingData.YRD_LCTN) = dr.Item(HeatingData.YRD_LCTN)

            Dim drHeatingcharge As DataRow()
            drHeatingcharge = dsHeatingCharge.Tables(HeatingData._HEATING_CHARGE).Select(String.Concat(HeatingData.GI_TRNSCTN_NO, "='", dr.Item(HeatingData.GI_TRNSCTN_NO), "'"))
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_CD) = drHeatingcharge(0).Item(HeatingData.HTNG_CD)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_STRT_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_DT)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_STRT_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_STRT_TM)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_END_DT) = drHeatingcharge(0).Item(HeatingData.HTNG_END_DT)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_END_TM) = drHeatingcharge(0).Item(HeatingData.HTNG_END_TM)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HTNG_TMPRTR) = drHeatingcharge(0).Item(HeatingData.HTNG_TMPRTR)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.TTL_HTN_PRD) = drHeatingcharge(0).Item(HeatingData.TTL_HTN_PRD)
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drHeatingcharge(0).Item(HeatingData.MIN_HTNG_RT_NC)
            Else
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                    drHeating.Item(HeatingData.MIN_HTNG_RT_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_RT_NC)
                    Exit For
                Next
            End If
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.HRLY_CHRG_NC) = drHeatingcharge(0).Item(HeatingData.HRLY_CHRG_NC)
            Else
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                    drHeating.Item(HeatingData.HRLY_CHRG_NC) = drCstmrDetail.Item(HeatingData.HRLY_CHRG_NC)
                    Exit For
                Next
            End If
            For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                strDepotCurrency = drCstmrDetail.Item(CommonUIData.CRRNCY_CD)
                drHeating.Item(HeatingData.CSTMR_CRRNCY_CD) = drCstmrDetail.Item(HeatingData.CSTMR_CRRNCY_CD)
                Exit For
            Next
            If drHeatingcharge.Length > 0 Then
                drHeating.Item(HeatingData.TTL_RT_NC) = drHeatingcharge(0).Item(HeatingData.TTL_RT_NC)
            End If
            If Not (dr.Item(HeatingData.MIN_HTNG_PRD_NC) Is DBNull.Value) Then
                drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = dr.Item(HeatingData.MIN_HTNG_PRD_NC)
            Else
                For Each drCstmrDetail As DataRow In dsHeatingCustomer.Tables(HeatingData._CUSTOMER).Select(String.Concat(HeatingData.CSTMR_CD, "='", _
                                                                                            dr.Item(GateinData.CSTMR_CD), "'"))
                    drHeating.Item(HeatingData.MIN_HTNG_PRD_NC) = drCstmrDetail.Item(HeatingData.MIN_HTNG_PRD_NC)
                    Exit For
                Next
            End If
            dtHeating.Rows.Add(drHeating)
            Next


            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Clear()
            dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Merge(dtHeating)

        Dim strActivitySubmit As String = String.Empty



        Dim dt() As DataRow = dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Select(String.Concat(HeatingData.EQPMNT_NO, "='", EquipmentNo, "'"))


        dt(0).Item("HTNG_STRT_DT") = StartDate
        dt(0).Item("HTNG_STRT_TM") = StartTime
        dt(0).Item("HTNG_END_DT") = EndDate
        dt(0).Item("HTNG_END_TM") = EndTime
        dt(0).Item("HTNG_TMPRTR") = Temp
        dt(0).Item("TTL_HTN_PRD") = TotalPeriod
        dt(0).Item("MIN_HTNG_RT_NC") = MinRate
        dt(0).Item("HRLY_CHRG_NC") = HourlyRate
        dt(0).Item("TTL_RT_NC") = HourlyRate
        dt(0).Item("CHECKED") = True



        Dim strHeatingCodes As String = ""
        Dim strErrorMessage As String = ""
        For Each drHeating1 As DataRow In dsHeating.Tables(HeatingData._V_HEATING_CHARGE).Select(HeatingData.CHECKED & "='True'")
            If drHeating1.Item(HeatingData.HTNG_ID).ToString <> "" Then
                If strHeatingCodes <> String.Empty Then
                    strHeatingCodes = String.Concat(strHeatingCodes, ",'", drHeating1.Item(HeatingData.HTNG_CD), "'")
                Else
                    strHeatingCodes = String.Concat("'", drHeating1.Item(HeatingData.HTNG_CD), "'")
                End If
            End If
        Next
        Dim objCommonUI As New CommonUI
        Dim HeatingUpdateStatus As New HeatingUpdateStatus

        objCommonUI.pub_validateFinalizedInvoiceActivityWise(CommonUIData._HEATING_CHARGE, CInt(objCommon.GetDepotID()), strHeatingCodes, "", "", strErrorMessage, dsHeating)

        If strErrorMessage <> "" Then

            HeatingUpdateStatus.Status = "This action will not be allowed since one of the equipment(s) heating invoice is already finalized."

            Return HeatingUpdateStatus
            'pub_SetCallbackError("This action will not be allowed since one of the equipment(s) heating invoice is already finalized.")
            'pub_SetCallbackStatus(False)
            Exit Function
        End If


        Dim output As Boolean = objHeating.pub_UpdateHeating(dsHeating, _
                                         objCommon.GetCurrentUserName(), _
                                         CDate(objCommon.GetCurrentDate()), _
                                         CInt(objCommon.GetDepotID()), _
                                         strActivitySubmit, _
                                         90)
        'Dim HeatingUpdateStatus As New HeatingUpdateStatus

        If output Then

            HeatingUpdateStatus.Status = "Heating Updated"

        Else

            HeatingUpdateStatus.Status = "Heating Not Updated"

            End If


            Return HeatingUpdateStatus

        Catch ex As Exception

            Dim HeatingUpdateStatus As New HeatingUpdateStatus
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            HeatingUpdateStatus.Status = ex.Message

            Return HeatingUpdateStatus
        End Try
    End Function



    Public Function LockData() As String

        'Try
        '    Dim objCommonData As New CommonData
        '    Dim strErrorMessage As String = String.Empty
        '    Dim blnLockData As Boolean = False
        '    Dim strIpAddress As String = GetClientIPAddress()
        '    Dim strCurrentIpAddress As String = String.Empty
        '    Dim strUserName As String = String.Empty
        '    blnLockData = objCommonData.pub_GetLockData(bv_blnCheckBit, bv_strEquipmentNo, strUserName, bv_strActivityName, strIpAddress, False, HeatingData.EQPMNT_NO)
        '    If blnLockData Then
        '        strCurrentIpAddress = GetClientIPAddress()
        '        If strCurrentIpAddress = strIpAddress Then
        '            pub_SetCallbackReturnValue("IPError", "true")
        '        Else
        '            pub_SetCallbackReturnValue("IPError", "false")
        '        End If
        '    End If
        '    pub_SetCallbackReturnValue("UserName", strUserName)
        '    pub_SetCallbackReturnValue("ActivityName", bv_strActivityName)
        '    pub_SetCallbackStatus(True)
        'Catch ex As Exception
        '    pub_SetCallbackStatus(False)
        '    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
        '                        MethodBase.GetCurrentMethod.Name, ex.Message)
        'End Try

    End Function

End Class