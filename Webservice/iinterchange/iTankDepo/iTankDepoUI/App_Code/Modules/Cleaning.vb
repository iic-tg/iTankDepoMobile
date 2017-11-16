Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class CleaningMobile
    Inherits System.Web.Services.WebService

    Dim arraylist As New ArrayList
    Dim conn As New Dropdown_C
    Dim arrayOfCleaning As New ArrayOfCleaningMobileModel
    Dim objCleaning As New Cleaning
    Dim gateInMobile As New GateinMobile_C
    Dim objCommon As New CommonData
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function PendingList(ByVal UserName As String) As ArrayOfCleaningMobileModel


        Dim arraylist As New ArrayList
        Dim arrayOfCleaning As New ArrayOfCleaningMobileModel

        Try

            gateInMobile.DepotID(UserName)
            ' Dim intDepotID As Integer = objCommon.GetDepotID()

            Dim query As String = "SELECT ADDTNL_CLNNG_BT,ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ACN' AND ACTV_BT = 1"
            Dim ds As DataSet = conn.connection(query)
            'conn.connection(query).Fill(ds)
            'ds.Tables(Table)
            'ds.Tables()
            'Return ds
            Dim dtPreaAdvice As DataTable
            dtPreaAdvice = ds.Tables(0)


            'Dim cleaningzz As New List(Of CleaningMobileModel)()

            For Each dd As DataRow In dtPreaAdvice.Rows

                Dim cleaning As New CleaningMobileModel
                cleaning.EquipmentNo = dd.Item(GateinData.EQPMNT_NO)
                cleaning.Customer = dd.Item(GateinData.CSTMR_CD)
                cleaning.EquipmentStatusType = dd.Item(GateinData.EQPMNT_TYP_CD)
                cleaning.InDate = dd.Item(GateinData.GTN_DT)
                cleaning.PrevoiusCargo = dd.Item(GateinData.PRDCT_DSCRPTN_VC)
                cleaning.LastStatusDate = dd.Item(GateinData.ACTVTY_DT)
                cleaning.AdditionalCleaningBit = dd.Item("ADDTNL_CLNNG_BT")

                Dim dt As DataTable = objCleaning.pub_GetCleaningInsructionReportDetails(dd.Item(GateinData.EQPMNT_NO), objCommon.GetDepotID())
                Dim dsCleaningInstruction As New CleaningInspectionDataSet

                dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Clear()

                For Each dr As DataRow In dt.Rows

                    Dim drClean As DataRow = dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).NewRow()

                    For Each dc As DataColumn In dt.Columns

                        If dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Columns.Contains(dc.ColumnName) Then
                            drClean(dc.ColumnName) = dr(dc.ColumnName)
                        End If


                    Next

                    dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Rows.Add(drClean)
                Next


                Dim dm As DataTable = dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION)


                cleaning.CleaningMethod = dm.Rows(0).Item("CLNNG_MTHD_TYP_CD").ToString()



                arraylist.Add(cleaning)

                'custr.Code = dd.Item(GateinData.CSTMR_CD)
                'custr.Name = dd.Item(GateinData.CSTMR_NAM)
                ''custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
                'arraylist.Add(cleaning)
            Next

            arrayOfCleaning.ArrayOfCleaning = arraylist
            arrayOfCleaning.status = "Success"
            arrayOfCleaning.statusText = HttpContext.Current.Response.StatusDescription
            arrayOfCleaning.stauscode = HttpContext.Current.Response.StatusCode

            Return arrayOfCleaning

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                             MethodBase.GetCurrentMethod.Name, ex)

            ' arrayOfCleaning.ArrayOfCleaning = arraylist
            arrayOfCleaning.status = ex.Message
            arrayOfCleaning.statusText = HttpContext.Current.Response.StatusDescription
            arrayOfCleaning.stauscode = HttpContext.Current.Response.StatusCode
            Return arrayOfCleaning

        End Try



        '' Dim a As integer = dtPreaAdvice.l
        'Return arraylist
        'Return a1
    End Function



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function CreateCleaning(ByVal bv_strCleaningId As String, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_strCleaningRate As String, _
                                   ByVal bv_strOriginalCleaningDate As String, _
                                   ByVal bv_strLastCleaningDate As String, _
                                   ByVal bv_strEquipmentStatus As String, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_CustomerId As String, _
                                   ByVal bv_GateInDate As String, _
                                   ByVal bv_strGI_TRNSCTN_NO As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean, _
                                   ByVal bv_blnSlabRateFlag As Boolean,
                                   ByVal UserName As String) As UpdateStatus



        Dim status As New UpdateStatus
        Try


            Dim gateInMobile As New GateinMobile_C
            gateInMobile.DepotID(UserName)

            Dim objCommon As New CommonData
            Dim lngCreated As Long
            Dim objCleaning As New Cleaning
            Dim dsCleaning As New CleaningDataSet
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim strActivitySubmit As String = String.Empty

            dsCleaning = objCleaning.pub_GetActivityStatusDetails(bv_strEquipmentNo, intDPT_ID, 0, bv_strGI_TRNSCTN_NO)


            lngCreated = objCleaning.pub_CreateCleaning_Clean(CLng(bv_strCleaningId), _
                                                            bv_strEquipmentNo, _
                                                            bv_strChemicalName, _
                                                            bv_strCleaningRate, _
                                                            CommonWeb.iDat(bv_strOriginalCleaningDate), _
                                                            CommonWeb.iDat(bv_strLastCleaningDate), _
                                                            CommonWeb.iLng(bv_strEquipmentStatus), _
                                                            bv_strEquipmentStatusCD, _
                                                            bv_strCleaningReferenceNo, _
                                                            bv_strRemarks, _
                                                            bv_CustomerId, _
                                                            CommonWeb.iDat(bv_GateInDate), _
                                                            bv_strGI_TRNSCTN_NO, _
                                                            intDPT_ID, _
                                                            objCommon.GetCurrentUserName(), _
                                                            CDate(objCommon.GetCurrentDate()), _
                                                            dsCleaning, _
                                                            strActivitySubmit, _
                                                            CInt(bv_intActivityId), _
                                                            bv_blnAdditionalCleaningFlag, _
                                                            bv_blnSlabRateFlag)


            If lngCreated <> Nothing Then

                status.Status = "Success"
                status.StatusCode = HttpContext.Current.Response.StatusCode
                status.StatusText = HttpContext.Current.Response.StatusDescription

                Return status
                Exit Function

            End If

            'status.Status = "Success"
            'status.StatusCode = HttpContext.Current.Response.StatusCode
            'status.StatusText = HttpContext.Current.Response.StatusDescription



        Catch ex As Exception

            status.Status = ex.Message
            status.StatusCode = HttpContext.Current.Response.StatusCode
            status.StatusText = HttpContext.Current.Response.StatusDescription
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                                MethodBase.GetCurrentMethod.Name, ex)
            Return status

        End Try


    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function InsertClng(ByVal EquipmentNo As String, ByVal UserName As String) As ArrayOfCleaningMobileModel



        'Try




        '    'Dim gateInMobile As New GateinMobile_C
        '    gateInMobile.DepotID(UserName)
        '    Dim objCleaning As New Cleaning
        '    'Dim objCommon As New CommonData()
        '    Dim dt As DataTable = objCleaning.pub_GetCleaningInsructionReportDetails(EquipmentNo, objCommon.GetDepotID())
        '    Dim dsCleaningInstruction As New CleaningInspectionDataSet

        '    dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Clear()

        '    For Each dr As DataRow In dt.Rows

        '        Dim drClean As DataRow = dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).NewRow()

        '        For Each dc As DataColumn In dt.Columns

        '            If dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Columns.Contains(dc.ColumnName) Then
        '                drClean(dc.ColumnName) = dr(dc.ColumnName)
        '            End If


        '        Next

        '        dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION).Rows.Add(drClean)
        '    Next




        '    Dim dm As DataTable = dsCleaningInstruction.Tables(CleaningInspectionData._CLEANING_INSPECTION)

        '    'Dim query1 As String = "select COUNT(*) from Cleaning_Instruction where EQPMNT_NO=" + EquipmentNo
        '    Dim query1 As String = "select COUNT(*) from Cleaning_Instruction where [Equipment_No] ='" + EquipmentNo + "'"
        '    Dim ds1 As DataSet = conn.connection(query1)
        '    'conn.connection(query1).Fill(ds1)

        '    Dim dtPreaAdvice1 As DataTable
        '    dtPreaAdvice1 = ds1.Tables(0)

        '    If dtPreaAdvice1.Rows(0).Item("Column1") = 0 Then



        '        Dim InspectionReportNo As String = dm.Rows(0).Item("CLNNG_INSPCTN_NO").ToString()
        '        Dim InDate As String = dm.Rows(0).Item("GTN_DT").ToString()
        '        Dim CustomerName As String = dm.Rows(0).Item("CSTMR_ID").ToString()
        '        Dim Equipment As String = dm.Rows(0).Item("EQPMNT_NO").ToString()
        '        Dim Type As String = dm.Rows(0).Item("EQPMNT_TYP_CD").ToString()
        '        Dim NextTestType As String = dm.Rows(0).Item("NXT_TST_TYP_CD").ToString()
        '        Dim NextTestDate As String = dm.Rows(0).Item("NXT_TST_DT").ToString()
        '        Dim IMOClass As String = dm.Rows(0).Item("IMO_CLSS").ToString()
        '        Dim EIRNo As String = dm.Rows(0).Item("GI_TRNSCTN_NO").ToString()
        '        Dim UN As String = dm.Rows(0).Item("UN_NO").ToString()
        '        Dim CleaningMethod As String = dm.Rows(0).Item("CLNNG_MTHD_TYP_CD").ToString()
        '        Dim PreviousCargo As String = dm.Rows(0).Item("PRDCT_DSCRPTN_VC").ToString()
        '        Dim ProductCode As String = dm.Rows(0).Item("PRDCT_CD").ToString()


        '        If NextTestDate <> Nothing Then
        '            dm.Rows(0).Item("NXT_TST_DT") = CDate(NextTestDate)
        '        Else
        '            dm.Rows(0).Item("NXT_TST_DT") = DBNull.Value

        '        End If


        '        If InDate <> Nothing Then
        '            dm.Rows(0).Item("GTN_DT") = CDate(InDate)
        '        Else
        '            dm.Rows(0).Item("GTN_DT") = DBNull.Value
        '        End If

        '        Dim sqlInsert As String = "insert into Cleaning_Instruction" & _
        ' "(Inspection_Report_No,In_Date,Customer_Name,Equipment_No,Type,Next_Test_Type,Next_Test_Date,IMO_Class,EIR_No,UN,Cleaning_Method,Previous_Cargo,Product_Code,DPT_ID)" & _
        ' "Values(@InspectionReportNo,@InDate,@CustomerName,@Equipment,@Type,@NextTestType,@NextTestDate,@IMOClass,@EIRNo,@UN,@CleaningMethod,@PreviousCargo,@ProductCode,@DPT_ID)"

        '        Dim connection1 As SqlConnection = New SqlConnection()
        '        connection1.ConnectionString = "Data Source=B-PC\SQLEXPRESS;Initial Catalog=iTankDepoDB;User ID=sa;Password=meta@123"
        '        connection1.Open()

        '        Using sqlCom = New SqlCommand(sqlInsert, connection1)
        '            sqlCom.Connection = connection1
        '            sqlCom.Parameters.AddWithValue("@InspectionReportNo", InspectionReportNo)
        '            sqlCom.Parameters.AddWithValue("@InDate", dm.Rows(0).Item("GTN_DT"))
        '            sqlCom.Parameters.AddWithValue("@CustomerName", CustomerName)
        '            sqlCom.Parameters.AddWithValue("@Equipment", Equipment)
        '            sqlCom.Parameters.AddWithValue("@Type", Type)
        '            sqlCom.Parameters.AddWithValue("@NextTestType", NextTestType)
        '            sqlCom.Parameters.AddWithValue("@NextTestDate", dm.Rows(0).Item("NXT_TST_DT"))
        '            sqlCom.Parameters.AddWithValue("@IMOClass", IMOClass)
        '            sqlCom.Parameters.AddWithValue("@EIRNo", EIRNo)
        '            sqlCom.Parameters.AddWithValue("@UN", CInt(UN))
        '            sqlCom.Parameters.AddWithValue("@CleaningMethod", CleaningMethod)
        '            sqlCom.Parameters.AddWithValue("@PreviousCargo", PreviousCargo)
        '            sqlCom.Parameters.AddWithValue("@ProductCode", CInt(ProductCode))
        '            sqlCom.Parameters.AddWithValue("@DPT_ID", CInt(objCommon.GetDepotID()))
        '            Dim ReturnValue As Integer = sqlCom.ExecuteNonQuery


        '            If (ReturnValue = 1) Then

        '                arrayOfCleaning = Comman(EquipmentNo)

        '                Return arrayOfCleaning
        '                Exit Function

        '            End If
        '            'Return ReturnValue
        '        End Using


        '    Else
        '        arrayOfCleaning = Comman(EquipmentNo)

        '        Return arrayOfCleaning

        '        Exit Function

        '    End If


        'Catch ex As Exception

        '    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
        '                                    MethodBase.GetCurrentMethod.Name, ex)
        '    arrayOfCleaning.status = ex.Message
        '    arrayOfCleaning.statusText = HttpContext.Current.Response.StatusDescription
        '    arrayOfCleaning.stauscode = HttpContext.Current.Response.StatusCode
        '    Return arrayOfCleaning

        'End Try




    End Function



    Public Function Comman(ByVal EquipmentNo As String) As ArrayOfCleaningMobileModel


        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ACN' AND EQPMNT_NO ='" + EquipmentNo + "' AND ACTV_BT = 1"

        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        'Dim cleaningzz As New List(Of CleaningMobileModel)()

        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim cleaning As New CleaningMobileModel
            cleaning.EquipmentNo = dd.Item(GateinData.EQPMNT_NO).ToString()
            cleaning.Customer = dd.Item(GateinData.CSTMR_CD).ToString()
            cleaning.InDate = dd.Item(GateinData.GTN_DT).ToString()
            cleaning.PrevoiusCargo = dd.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
            cleaning.LastStatusDate = dd.Item(GateinData.ACTVTY_DT).ToString()
            cleaning.AdditionalCleaningBit = dd.Item("ADDTNL_CLNNG_BT").ToString()
            cleaning.CleaningRate = dd.Item("CLNNG_RT").ToString()
            cleaning.EquipmentStatus = dd.Item(GateinData.EQPMNT_TYP_ID).ToString()
            cleaning.EquipmentStatusType = dd.Item(GateinData.EQPMNT_TYP_CD).ToString()
            cleaning.CustomerId = dd.Item(GateinData.CSTMR_ID).ToString()
            cleaning.Remarks = dd.Item(GateinData.RMRKS_VC).ToString()
            cleaning.GiTransactionNo = dd.Item(GateinData.GI_TRNSCTN_NO).ToString()
            cleaning.CleaningId = dd.Item("CLNNG_ID").ToString()
            cleaning.CleaningReferenceNo = dd.Item("CLNNG_RFRNC_NO").ToString()

            'if dd.item("ADDTNL_CLNNG_BT") == 1 then
            'If dd.Item("ADDTNL_CLNNG_BT") = 1 Then

            '    Dim clngIn As String = "select [CLNNG_ID],[CLNNG_RFRNC_NO] from [CLEANING] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
            '    Dim dfghs1 As DataSet = New DataSet()
            '    conn.connection(clngIn).Fill(dfghs1)

            '    Dim dtPreaAdvicess As DataTable
            '    dtPreaAdvicess = dfghs1.Tables(0)

            '    cleaning.CleaningId = dtPreaAdvicess.Rows(0).Item("CLNNG_ID")
            '    cleaning.CleaningReferenceNo = dtPreaAdvicess.Rows(0).Item("CLNNG_RFRNC_NO")

            'Else
            '    cleaning.CleaningId = ""
            '    cleaning.CleaningReferenceNo = ""


            'End If

            Dim blnCustomerProductRate As Integer
            Dim blnProductRate As Integer
            Dim blnSlabRate As Integer
            blnCustomerProductRate = dd.Item("CLNNG_CSTMR_PRDCT_RT_BT").ToString()
            blnProductRate = dd.Item("CLNNG_PRDCT_RT_BT").ToString()

            blnSlabRate = objCleaning.pub_CheckSlabRateExists(dd.Item(CleaningData.CSTMR_ID), dd.Item(CleaningData.EQPMNT_TYP_ID))

            If (blnProductRate = 1 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                cleaning.SlabRate = 1
            ElseIf (blnProductRate = 0 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                cleaning.SlabRate = 1
            Else
                cleaning.SlabRate = 0
            End If

            Dim ClngInstr As String = "select [Cleaning_Date] from [Cleaning_Instruction] where [Equipment_No]='" + dd.Item(GateinData.EQPMNT_NO) + "'"
            Dim InstcDate As DataSet = conn.connection(ClngInstr)
            'conn.connection(ClngInstr).Fill(InstcDate)

            Dim dtPreaAdvicess1 As DataTable
            dtPreaAdvicess1 = InstcDate.Tables(0)

            cleaning.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date").ToString()






            arraylist.Add(cleaning)

            'custr.Code = dd.Item(GateinData.CSTMR_CD)
            'custr.Name = dd.Item(GateinData.CSTMR_NAM)
            ''custr.CheckDigit = dd.Item(GateinData.CHK_DGT_VLDTN_BT)
            'arraylist.Add(cleaning)
        Next

        arrayOfCleaning.ArrayOfCleaning = arraylist
        arrayOfCleaning.status = "Success"
        arrayOfCleaning.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfCleaning.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfCleaning


        Exit Function

    End Function

    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function MySubmit(ByVal UserName As String) As ArrayOfCleaningMobileModel


        gateInMobile.DepotID(UserName)
        Dim objCleaning As New Cleaning

        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1"
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        'Dim cleaningzz As New List(Of CleaningMobileModel)()

        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim cleadning As New CleaningMobileModel
            cleadning.EquipmentNo = dd.Item(GateinData.EQPMNT_NO).ToString()
            cleadning.Customer = dd.Item(GateinData.CSTMR_CD).ToString()
            cleadning.InDate = dd.Item(GateinData.GTN_DT).ToString()
            cleadning.PrevoiusCargo = dd.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
            cleadning.LastStatusDate = dd.Item(GateinData.ACTVTY_DT).ToString()
            cleadning.AdditionalCleaningBit = dd.Item("ADDTNL_CLNNG_BT").ToString()
            cleadning.CleaningRate = dd.Item("CLNNG_RT").ToString()
            cleadning.EquipmentStatus = dd.Item(GateinData.EQPMNT_TYP_ID).ToString()
            cleadning.EquipmentStatusType = dd.Item(GateinData.EQPMNT_TYP_CD).ToString()
            cleadning.CustomerId = dd.Item(GateinData.CSTMR_ID).ToString()
            cleadning.Remarks = dd.Item(GateinData.RMRKS_VC).ToString()
            cleadning.GiTransactionNo = dd.Item(GateinData.GI_TRNSCTN_NO).ToString()
            cleadning.CleaningId = dd.Item("CLNNG_ID").ToString()
            cleadning.CleaningReferenceNo = dd.Item("CLNNG_RFRNC_NO").ToString()

            'if dd.item("ADDTNL_CLNNG_BT") == 1 then
            'If dd.Item("ADDTNL_CLNNG_BT") = 1 Then

            '    Dim clngIn As String = "select [CLNNG_ID],[CLNNG_RFRNC_NO] from [CLEANING] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
            '    Dim dfghs1 As DataSet = New DataSet()
            '    conn.connection(clngIn).Fill(dfghs1)

            '    Dim dtPreaAdvicess As DataTable
            '    dtPreaAdvicess = dfghs1.Tables(0)

            '    cleaning.CleaningId = dtPreaAdvicess.Rows(0).Item("CLNNG_ID")
            '    cleaning.CleaningReferenceNo = dtPreaAdvicess.Rows(0).Item("CLNNG_RFRNC_NO")

            'Else
            '    cleaning.CleaningId = ""
            '    cleaning.CleaningReferenceNo = ""


            'End If

            Dim blnCustomerProductRate As Integer
            Dim blnProductRate As Integer
            Dim blnSlabRate As Integer
            blnCustomerProductRate = dd.Item("CLNNG_CSTMR_PRDCT_RT_BT").ToString()
            blnProductRate = dd.Item("CLNNG_PRDCT_RT_BT").ToString()

            blnSlabRate = objCleaning.pub_CheckSlabRateExists(dd.Item(CleaningData.CSTMR_ID), dd.Item(CleaningData.EQPMNT_TYP_ID))

            If (blnProductRate = 1 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                cleadning.SlabRate = 1
            ElseIf (blnProductRate = 0 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                cleadning.SlabRate = 1
            Else
                cleadning.SlabRate = 0
            End If

            'Dim ClngInstr As String = "select [Cleaning_Date] from [Cleaning_Instruction] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
            'Dim InstcDate As DataSet = New DataSet()
            'conn.connection(ClngInstr).Fill(InstcDate)

            'Dim dtPreaAdvicess1 As DataTable
            'dtPreaAdvicess1 = InstcDate.Tables(0)
            arraylist.Add(cleadning)
            'cleaning.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date")
        Next


        arrayOfCleaning.ArrayOfCleaning = arraylist
        arrayOfCleaning.status = "Success"
        arrayOfCleaning.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfCleaning.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfCleaning
    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function MySubmitDetail(ByVal EquipmentNo As String, ByVal CleaningId As String, ByVal UserName As String) As ArrayOfCleaningMobileModel


        gateInMobile.DepotID(UserName)
        Dim query As String = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1 AND EQPMNT_NO =" + EquipmentNo + "AND CLNNG_ID =" + CleaningId
        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)


        'Dim cleaningzz As New List(Of CleaningMobileModel)()

        For Each dd As DataRow In dtPreaAdvice.Rows
            Dim cleanin As New CleaningMobileModel
            cleanin.EquipmentNo = dd.Item(GateinData.EQPMNT_NO).ToString()
            cleanin.Customer = dd.Item(GateinData.CSTMR_CD).ToString()
            cleanin.InDate = dd.Item(GateinData.GTN_DT).ToString()
            cleanin.PrevoiusCargo = dd.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
            cleanin.LastStatusDate = dd.Item(GateinData.ACTVTY_DT).ToString()
            cleanin.AdditionalCleaningBit = dd.Item("ADDTNL_CLNNG_BT").ToString()
            cleanin.CleaningRate = dd.Item("CLNNG_RT").ToString()
            cleanin.EquipmentStatus = dd.Item(GateinData.EQPMNT_TYP_ID).ToString()
            cleanin.EquipmentStatusType = dd.Item(GateinData.EQPMNT_TYP_CD).ToString()
            cleanin.CustomerId = dd.Item(GateinData.CSTMR_ID).ToString()
            cleanin.Remarks = dd.Item(GateinData.RMRKS_VC).ToString()
            cleanin.GiTransactionNo = dd.Item(GateinData.GI_TRNSCTN_NO).ToString()
            cleanin.CleaningId = dd.Item("CLNNG_ID").ToString()
            cleanin.CleaningReferenceNo = dd.Item("CLNNG_RFRNC_NO").ToString()

            'if dd.item("ADDTNL_CLNNG_BT") == 1 then
            'If dd.Item("ADDTNL_CLNNG_BT") = 1 Then

            '    Dim clngIn As String = "select [CLNNG_ID],[CLNNG_RFRNC_NO] from [CLEANING] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
            '    Dim dfghs1 As DataSet = New DataSet()
            '    conn.connection(clngIn).Fill(dfghs1)

            '    Dim dtPreaAdvicess As DataTable
            '    dtPreaAdvicess = dfghs1.Tables(0)

            '    cleaning.CleaningId = dtPreaAdvicess.Rows(0).Item("CLNNG_ID")
            '    cleaning.CleaningReferenceNo = dtPreaAdvicess.Rows(0).Item("CLNNG_RFRNC_NO")

            'Else
            '    cleaning.CleaningId = ""
            '    cleaning.CleaningReferenceNo = ""


            'End If

            Dim blnCustomerProductRate As Integer
            Dim blnProductRate As Integer
            Dim blnSlabRate As Integer
            blnCustomerProductRate = dd.Item("CLNNG_CSTMR_PRDCT_RT_BT").ToString()
            blnProductRate = dd.Item("CLNNG_PRDCT_RT_BT").ToString()

            blnSlabRate = objCleaning.pub_CheckSlabRateExists(dd.Item(CleaningData.CSTMR_ID), dd.Item(CleaningData.EQPMNT_TYP_ID))

            If (blnProductRate = 1 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                cleanin.SlabRate = 1
            ElseIf (blnProductRate = 0 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                cleanin.SlabRate = 1
            Else
                cleanin.SlabRate = 0
            End If

            Dim ClngInstr As String = "select [Cleaning_Date] from [Cleaning_Instruction] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
            Dim InstcDate As DataSet = conn.connection(ClngInstr)
            'conn.connection(ClngInstr).Fill(InstcDate)

            Dim dtPreaAdvicess1 As DataTable
            dtPreaAdvicess1 = InstcDate.Tables(0)

            cleanin.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date").ToString()

            arraylist.Add(cleanin)
        Next


        arrayOfCleaning.ArrayOfCleaning = arraylist
        arrayOfCleaning.status = "Success"
        arrayOfCleaning.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfCleaning.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfCleaning
    End Function


    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function UpdateCleaning(ByVal bv_strCleaningId As String, _
                                   ByVal bv_strEquipmentNo As String, _
                                   ByVal bv_strChemicalName As String, _
                                   ByVal bv_strCleaningRate As String, _
                                   ByVal bv_strOriginalCleaningDate As String, _
                                   ByVal bv_strLastCleaningDate As String, _
                                   ByVal bv_strEquipmentStatus As String, _
                                   ByVal bv_strEquipmentStatusCD As String, _
                                   ByVal bv_strCleaningReferenceNo As String, _
                                   ByVal bv_strRemarks As String, _
                                   ByVal bv_CustomerId As String, _
                                   ByVal bv_GateInDate As String, _
                                   ByVal bv_strGI_TRNSCTN_NO As String, _
                                   ByVal bv_intActivityId As Integer, _
                                   ByVal bv_blnAdditionalCleaningFlag As Boolean,
                                   ByVal UserName As String) As UpdateStatus


        'Dim gateInMobile As New GateinMobile_C
        gateInMobile.DepotID(UserName)
        Dim dsCleaning As New CleaningDataSet
        'Dim objCommon As New CommonData
        Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
        Dim strActivitySubmit As String = String.Empty
        Dim Updae As New UpdateStatus


        Try



            Dim blnBill As Boolean = False
            blnBill = objCleaning.pub_GetCleaningChargeBilled(bv_strEquipmentNo, intDPT_ID, CLng(bv_strCleaningId), bv_strGI_TRNSCTN_NO, bv_strCleaningRate)
            If blnBill = False Then

                Updae.Status = "Cleaning Invoice has been raised, Hence cannot change the cleaning rate."

                Return Updae
                'pub_SetCallbackError("Cleaning Invoice has been raised, Hence cannot change the cleaning rate.")
                'pub_SetCallbackStatus(False)
                Exit Function
            End If

            dsCleaning = objCleaning.pub_GetActivityStatusDetails(bv_strEquipmentNo, intDPT_ID, bv_strCleaningId, bv_strGI_TRNSCTN_NO)


            objCleaning.ModifyCleaning_Clean(CommonWeb.iLng(bv_strCleaningId), _
                                           bv_strEquipmentNo, _
                                           bv_strChemicalName, _
                                           bv_strCleaningRate, _
                                           CommonWeb.iDat(bv_strOriginalCleaningDate), _
                                           CommonWeb.iDat(bv_strLastCleaningDate), _
                                           CommonWeb.iLng(bv_strEquipmentStatus), _
                                           bv_strEquipmentStatusCD, _
                                           bv_strCleaningReferenceNo, _
                                           bv_strRemarks, _
                                           bv_CustomerId, _
                                           CommonWeb.iDat(bv_GateInDate), _
                                           bv_strGI_TRNSCTN_NO, _
                                           intDPT_ID, _
                                           objCommon.GetCurrentUserName(), _
                                           CDate(objCommon.GetCurrentDate()), _
                                           dsCleaning, _
                                           strActivitySubmit, _
                                           bv_intActivityId, _
                                           bv_blnAdditionalCleaningFlag)


            Updae.Status = "Success"
            Updae.StatusCode = HttpContext.Current.Response.StatusCode
            Updae.StatusText = HttpContext.Current.Response.StatusDescription

            Return Updae


        Catch ex As Exception
            Updae.Status = ex.Message
            Updae.StatusCode = HttpContext.Current.Response.StatusCode
            Updae.StatusText = HttpContext.Current.Response.StatusDescription


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                               MethodBase.GetCurrentMethod.Name, ex)

            Return Updae

        End Try

    End Function


    <WebMethod(enableSession:=True)> _
 <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function InstructionDetails(ByVal EquipmentNo As String) As CleaningInstruction
        Dim ClngInstr As String = "select * from [Cleaning_Instruction] where [EQPMNT_NO]=" + EquipmentNo
        Dim InstcDate As DataSet = conn.connection(ClngInstr)
        'conn.connection(ClngInstr).Fill(InstcDate)

        Dim dtPreaAdvicess1 As DataTable
        dtPreaAdvicess1 = InstcDate.Tables(0)

        Dim cleaningInst As New CleaningInstruction

        cleaningInst.InspectionReportNo = dtPreaAdvicess1.Rows(0).Item("Inspection_Report_No").ToString()
        cleaningInst.InDate = dtPreaAdvicess1.Rows(0).Item("In_Date").ToString()
        cleaningInst.CustomerName = dtPreaAdvicess1.Rows(0).Item("Customer_Name").ToString()
        cleaningInst.EquipmentNo = dtPreaAdvicess1.Rows(0).Item("Equipment_No").ToString()
        cleaningInst.Type = dtPreaAdvicess1.Rows(0).Item("Type").ToString()
        cleaningInst.NextTestType = dtPreaAdvicess1.Rows(0).Item("Next_Test_Type").ToString()
        cleaningInst.NextTestDate = dtPreaAdvicess1.Rows(0).Item("Next_Test_Date").ToString()
        cleaningInst.IMOClass = dtPreaAdvicess1.Rows(0).Item("IMO_Class").ToString()
        cleaningInst.EIRNo = dtPreaAdvicess1.Rows(0).Item("EIR_No").ToString()
        cleaningInst.UN = dtPreaAdvicess1.Rows(0).Item("UN").ToString()
        cleaningInst.CleaningMethod = dtPreaAdvicess1.Rows(0).Item("Cleaning_Method").ToString()
        cleaningInst.PreviousCargo = dtPreaAdvicess1.Rows(0).Item("Previous_Cargo").ToString()
        cleaningInst.ProductCode = dtPreaAdvicess1.Rows(0).Item("Product_Code").ToString()

        cleaningInst.Cleaning_Date = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date").ToString()
        cleaningInst.Steaming_Start_Time = dtPreaAdvicess1.Rows(0).Item("Steaming_Start_Time").ToString()
        cleaningInst.Steaming_End_Time = dtPreaAdvicess1.Rows(0).Item("Steaming_End_Time").ToString()
        cleaningInst.Clng_Start_Time = dtPreaAdvicess1.Rows(0).Item("Clng_Start_Time").ToString()
        cleaningInst.Completed_Time = dtPreaAdvicess1.Rows(0).Item("Completed_Time").ToString()
        cleaningInst.Cleaned_By = dtPreaAdvicess1.Rows(0).Item("Cleaned_By").ToString()

        cleaningInst.Remarks = dtPreaAdvicess1.Rows(0).Item("Remarks").ToString()

        cleaningInst.O2 = dtPreaAdvicess1.Rows(0).Item("O2").ToString()
        cleaningInst.LEL = dtPreaAdvicess1.Rows(0).Item("LEL").ToString()
        cleaningInst.H2S = dtPreaAdvicess1.Rows(0).Item("H2S").ToString()
        cleaningInst.CO = dtPreaAdvicess1.Rows(0).Item("CO").ToString()
        cleaningInst.PID = dtPreaAdvicess1.Rows(0).Item("PID").ToString()
        cleaningInst.InspectionDate = dtPreaAdvicess1.Rows(0).Item("Inspection_Date").ToString()
        cleaningInst.Clean_UnClean = dtPreaAdvicess1.Rows(0).Item("Clean_UnClean").ToString()
        cleaningInst.SyphonTube = dtPreaAdvicess1.Rows(0).Item("Syphon_Tube").ToString()
        cleaningInst.ManlidGasket = dtPreaAdvicess1.Rows(0).Item("Manlid_Gasket").ToString()
        cleaningInst.Foot_Valve_Seat = dtPreaAdvicess1.Rows(0).Item("Foot_Valve_Seat").ToString()
        cleaningInst.FV_fortyt_O_ring = dtPreaAdvicess1.Rows(0).Item("FV_fortyt_O_ring").ToString()
        cleaningInst.Leak_Test = dtPreaAdvicess1.Rows(0).Item("Leak_Test").ToString()
        cleaningInst.Shell_D_End = dtPreaAdvicess1.Rows(0).Item("Shell_D_End").ToString()
        cleaningInst.Top_Discharge_Valve = dtPreaAdvicess1.Rows(0).Item("Top_Discharge_Valve").ToString()
        cleaningInst.Airline_Valve = dtPreaAdvicess1.Rows(0).Item("Airline_Valve").ToString()
        cleaningInst.Relief_Valve_Bursting_Disc = dtPreaAdvicess1.Rows(0).Item("Relief_Valve_Bursting_Disc").ToString()
        cleaningInst.Last_Test_Date = dtPreaAdvicess1.Rows(0).Item("Last_Test_Date").ToString()
        cleaningInst.Other_Remarks_Comments = dtPreaAdvicess1.Rows(0).Item("Other_Remarks_Comments").ToString()
        cleaningInst.Seal_Nos = dtPreaAdvicess1.Rows(0).Item("Seal_Nos").ToString()
        cleaningInst.M_L = dtPreaAdvicess1.Rows(0).Item("M_L").ToString()
        cleaningInst.AV_TD = dtPreaAdvicess1.Rows(0).Item("AV_TD").ToString()
        cleaningInst.BD = dtPreaAdvicess1.Rows(0).Item("BD").ToString()
        cleaningInst.Inspected_By = dtPreaAdvicess1.Rows(0).Item("Inspected_By").ToString()


        Return cleaningInst
    End Function







    <WebMethod(enableSession:=True)> _
 <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function InsertCleaningInstruction(ByVal InspectionReportNo As String,
                                              ByVal InDate As String,
                                              ByVal CustomerName As String,
                                              ByVal EquipmentNo As String,
                                              ByVal Type As String,
                                              ByVal NextTestType As String,
                                              ByVal NextTestDate As String,
                                              ByVal IMOClass As String,
                                              ByVal EIRNo As String,
                                              ByVal UN As String,
                                              ByVal CleaningMethod As String,
                                              ByVal PreviousCargo As String,
                                              ByVal ProductCode As String,
                                              ByVal Cleaning_Date As String,
                                              ByVal Steaming_Start_Time As String,
                                              ByVal Steaming_End_Time As String,
                                              ByVal Clng_Start_Time As String,
                                              ByVal Completed_Time As String,
                                            ByVal Cleaned_By As String,
                                              ByVal Remarks As String,
                                              ByVal O2 As String,
                                              ByVal LEL As String,
                                              ByVal H2S As String,
                                              ByVal CO As String,
                                              ByVal PID As String,
                                              ByVal InspectionDate As String,
                                              ByVal Clean_UnClean As String,
                                              ByVal SyphonTube As String,
                                              ByVal ManlidGasket As String,
                                              ByVal Foot_Valve_Seat As String,
                                              ByVal FV_fortyt_O_ring As String,
                                              ByVal Leak_Test As String,
                                                ByVal Shell_D_End As String,
                                            ByVal Top_Discharge_Valve As String,
                                              ByVal Airline_Valve As String,
                                               ByVal Relief_Valve_Bursting_Disc As String,
                                                ByVal Last_Test_Date As String,
                                                 ByVal Other_Remarks_Comments As String,
                                                  ByVal Seal_Nos As String,
                                                   ByVal M_L As String,
                                                    ByVal AV_TD As String,
                                                     ByVal BD As String,
                                                      ByVal Inspected_By As String
                                              ) As UpdateStatus



        Dim sqlInsert As String = "select * from [Cleaning_Instruction]"

        Dim InstcDate As DataSet = conn.connection(sqlInsert)
        'conn.connection(sqlInsert).Fill(InstcDate)

        Dim dtPreaAdvicess1 As DataTable
        dtPreaAdvicess1 = InstcDate.Tables(0)

        Dim dm As DataRow = dtPreaAdvicess1.NewRow()

        If NextTestDate <> Nothing Then
            dm.Item("Next_Test_Date") = CDate(NextTestDate)
        Else
            dm.Item("Next_Test_Date") = DBNull.Value
        End If

        If InDate <> Nothing Then
            dm.Item("In_Date") = CDate(InDate)
        Else
            dm.Item("In_Date") = DBNull.Value
        End If

        If Cleaning_Date <> Nothing Then
            dm.Item("Cleaning_Date") = CDate(Cleaning_Date)
        Else
            dm.Item("Cleaning_Date") = DBNull.Value
        End If


        If InspectionDate <> Nothing Then
            dm.Item("Inspection_Date") = CDate(InspectionDate)
        Else
            dm.Item("Inspection_Date") = DBNull.Value
        End If


        If Last_Test_Date <> Nothing Then
            dm.Item("Last_Test_Date") = CDate(Last_Test_Date)
        Else
            dm.Item("Last_Test_Date") = DBNull.Value
        End If
        Dim updateaa As New UpdateStatus

        'Try


        '    Dim sqlInsert1 As String = "update Cleaning_Instruction set" & _
        '    "(Inspection_Report_No,In_Date,Customer_Name,Equipment_No,Type,Next_Test_Type,Next_Test_Date,IMO_Class,EIR_No,UN,Cleaning_Method,Previous_Cargo,Product_Code,Cleaning_Date,Steaming_Start_Time,Steaming_End_Time,Clng_Start_Time,Completed_Time,Cleaned_By,Remarks,O2,LEL,H2S,CO,PID,Inspection_Date,Clean_UnClean,Syphon_Tube,Manlid_Gasket,Foot_Valve_Seat,FV_fortyt_O_ring,Leak_Test,Shell_D_End,Top_Discharge_Valve,Airline_Valve,Relief_Valve_Bursting_Disc,Last_Test_Date,Other_Remarks_Comments,Seal_Nos,M_L,AV_TD,BD,Inspected_By)" & _
        '    "Values(@InspectionReportNo,@InDate,@CustomerName,@Equipment,@Type,@NextTestType,@NextTestDate,@IMOClass,@EIRNo,@UN,@CleaningMethod,@PreviousCargo,@ProductCode,@Cleaning_Date,@Steaming_Start_Time,@Steaming_End_Time,@Clng_Start_Time,@Completed_Time,@Cleaned_By,@Remarks,@O2,@LEL,@H2S,@CO,@PID,@Inspection_Date,@Clean_UnClean,@Syphon_Tube,@Manlid_Gasket,@Foot_Valve_Seat,@FV_fortyt_O_ring,@Leak_Test,@Shell_D_End,@Top_Discharge_Valve,@Airline_Valve,@Relief_Valve_Bursting_Disc,@Last_Test_Date,@Other_Remarks_Comments,@Seal_Nos,@M_L,@AV_TD,@BD,@Inspected_By)"

        '    Dim connection1 As SqlConnection = New SqlConnection()
        '    connection1.ConnectionString = "Data Source=B-PC\SQLEXPRESS;Initial Catalog=iTankDepoDB;User ID=sa;Password=meta@123"
        '    connection1.Open()

        '    Using sqlCom = New SqlCommand(sqlInsert1, connection1)
        '        sqlCom.Connection = connection1
        '        sqlCom.Parameters.AddWithValue("@InspectionReportNo", InspectionReportNo)
        '        sqlCom.Parameters.AddWithValue("@InDate", dm.Item("In_Date"))
        '        sqlCom.Parameters.AddWithValue("@CustomerName", CustomerName)
        '        sqlCom.Parameters.AddWithValue("@Equipment", EquipmentNo)
        '        sqlCom.Parameters.AddWithValue("@Type", Type)
        '        sqlCom.Parameters.AddWithValue("@NextTestType", NextTestType)
        '        sqlCom.Parameters.AddWithValue("@NextTestDate", dm.Item("Next_Test_Date"))
        '        sqlCom.Parameters.AddWithValue("@IMOClass", IMOClass)
        '        sqlCom.Parameters.AddWithValue("@EIRNo", EIRNo)
        '        sqlCom.Parameters.AddWithValue("@UN", CInt(UN))
        '        sqlCom.Parameters.AddWithValue("@CleaningMethod", CleaningMethod)
        '        sqlCom.Parameters.AddWithValue("@PreviousCargo", PreviousCargo)
        '        sqlCom.Parameters.AddWithValue("@ProductCode", CInt(ProductCode))

        '        sqlCom.Parameters.AddWithValue("@Cleaning_Date", dm.Item("Cleaning_Date"))
        '        sqlCom.Parameters.AddWithValue("@Steaming_Start_Time", Steaming_Start_Time)
        '        sqlCom.Parameters.AddWithValue("@Steaming_End_Time", Steaming_End_Time)
        '        sqlCom.Parameters.AddWithValue("@Clng_Start_Time", Clng_Start_Time)
        '        sqlCom.Parameters.AddWithValue("@Cleaned_By", Cleaned_By)
        '        sqlCom.Parameters.AddWithValue("@Completed_Time", Completed_Time)
        '        sqlCom.Parameters.AddWithValue("@Remarks", Remarks)
        '        sqlCom.Parameters.AddWithValue("@O2", O2)
        '        sqlCom.Parameters.AddWithValue("@LEL", LEL)
        '        sqlCom.Parameters.AddWithValue("@H2S", H2S)
        '        sqlCom.Parameters.AddWithValue("@CO", CO)
        '        sqlCom.Parameters.AddWithValue("@PID", PID)
        '        sqlCom.Parameters.AddWithValue("@Inspection_Date", dm.Item("Inspection_Date"))

        '        sqlCom.Parameters.AddWithValue("@Clean_UnClean", Clean_UnClean)
        '        sqlCom.Parameters.AddWithValue("@Syphon_Tube", SyphonTube)
        '        sqlCom.Parameters.AddWithValue("@Manlid_Gasket", ManlidGasket)
        '        sqlCom.Parameters.AddWithValue("@Foot_Valve_Seat", Foot_Valve_Seat)
        '        sqlCom.Parameters.AddWithValue("@FV_fortyt_O_ring", FV_fortyt_O_ring)
        '        sqlCom.Parameters.AddWithValue("@Leak_Test", Leak_Test)
        '        sqlCom.Parameters.AddWithValue("@Shell_D_End", Shell_D_End)
        '        sqlCom.Parameters.AddWithValue("@Top_Discharge_Valve", Top_Discharge_Valve)
        '        sqlCom.Parameters.AddWithValue("@Airline_Valve", Airline_Valve)
        '        sqlCom.Parameters.AddWithValue("@Relief_Valve_Bursting_Disc", Relief_Valve_Bursting_Disc)
        '        sqlCom.Parameters.AddWithValue("@Last_Test_Date", dm.Item("Last_Test_Date"))
        '        sqlCom.Parameters.AddWithValue("@Other_Remarks_Comments", Other_Remarks_Comments)
        '        sqlCom.Parameters.AddWithValue("@Seal_Nos", Seal_Nos)

        '        sqlCom.Parameters.AddWithValue("@M_L", M_L)
        '        sqlCom.Parameters.AddWithValue("@AV_TD", AV_TD)
        '        sqlCom.Parameters.AddWithValue("@BD", BD)
        '        sqlCom.Parameters.AddWithValue("@Inspected_By", Inspected_By)
        '        sqlCom.Parameters.AddWithValue("@Seal_Nos", Seal_Nos)
        '        Dim ReturnValue As Integer = sqlCom.ExecuteNonQuery


        '        If ReturnValue = 1 Then


        '            updateaa.Status = "Success"
        '            updateaa.StatusCode = HttpContext.Current.Response.StatusCode
        '            updateaa.StatusText = HttpContext.Current.Response.StatusDescription
        '        End If

        '    End Using

        '    Return updateaa

        'Catch ex As Exception



        '    updateaa.Status = ex.Message
        '    updateaa.StatusCode = HttpContext.Current.Response.StatusCode
        '    updateaa.StatusText = HttpContext.Current.Response.StatusDescription


        '    iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
        '                                       MethodBase.GetCurrentMethod.Name, ex)

        '    Return updateaa

        'End Try

    End Function



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function filter(ByVal filterType As String, ByVal filterCondition As String, ByVal filterValue As String, ByVal UserName As String, ByVal Mode As String) As ListModel

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
        Select Case Mode
            Case strNew
                If filterCondition = "Equals" Then

                    query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ACN' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    'query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else

                    query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ACN' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"
                    'query = "SELECT distinct " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                End If

            Case strEdit

                If filterCondition = "Equals" Then

                    query = "SELECT " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1 AND " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else
                    query = "SELECT distinct " + filterType + " FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1 AND " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                End If

        End Select

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
    Public Function SearchList(ByVal UserName As String, ByVal SearchValues As SearchValues, ByVal filterType As String, ByVal Mode As String) As ArrayOfCleaningMobileModel


        gateInMobile.DepotID(UserName)
        Dim objCleaning As New Cleaning
        Dim query As String = ""

        If Mode = "new" Then
            query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'ACN' AND ACTV_BT = 1"
        Else
            query = "SELECT CLNNG_ID,[CLNNG_RFRNC_NO],[ADDTNL_CLNNG_BT],ACTVTY_STTS_ID,CSTMR_ID,CSTMR_CD,CSTMR_NAM,EQPMNT_NO,EQPMNT_TYP_ID,EQPMNT_TYP_CD,EQPMNT_CD_ID,EQPMNT_CD_CD,GTN_DT,GTOT_DT,PRDCT_ID,PRDCT_CD,PRDCT_DSCRPTN_VC,CLNNG_DT,EQPMNT_STTS_ID,EQPMNT_STTS_CD,ACTVTY_NAM,ACTVTY_DT,RMRKS_VC,GI_TRNSCTN_NO,INVC_GNRT_BT,GI_RF_NO,INSPCTN_DT,ACTV_BT,DPT_ID,DPT_CD,DPT_NAM,YRD_LCTN,FILTER_CODE,(CASE WHEN (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) IS NULL THEN 'U' ELSE (SELECT BLLNG_FLG FROM CLEANING_CHARGE WHERE GI_TRNSCTN_NO=VAS.GI_TRNSCTN_NO) END) AS BLLNG_FLG,(SELECT CHMCL_NM FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) AS CHMCL_NM,CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN (SELECT TTL_AMNT_NC FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID) ELSE (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) END AS CLNNG_RT,(CASE WHEN (SELECT COUNT(PRDCT_ID) FROM PRODUCT_CUSTOMER WHERE PRDCT_ID =VAS.PRDCT_ID AND CSTMR_ID=VAS.CSTMR_ID GROUP BY PRDCT_ID,CSTMR_ID) >= 1 THEN 1 ELSE 0 END)CLNNG_CSTMR_PRDCT_RT_BT,(CASE WHEN (SELECT CLNNG_TTL_AMNT_NC FROM PRODUCT WHERE PRDCT_ID=VAS.PRDCT_ID) IS NOT NULL THEN 1 ELSE 0 END)CLNNG_PRDCT_RT_BT,(SELECT CRRNCY_CD FROM CURRENCY WHERE CRRNCY_ID=(SELECT CRRNCY_ID FROM BANK_DETAIL WHERE DPT_ID=VAS.DPT_ID AND BNK_TYP_ID=44))CRRNCY_CD FROM V_ACTIVITY_STATUS VAS WHERE DPT_ID='" + objCommon.GetDepotID() + "' AND EQPMNT_STTS_CD = 'CLN' AND ACTV_BT = 1"
        End If


        Dim ds As DataSet = conn.connection(query)
        'conn.connection(query).Fill(ds)
        'ds.Tables(Table)
        'ds.Tables()
        'Return ds
        Dim dtPreaAdvice As DataTable
        dtPreaAdvice = ds.Tables(0)

        If Mode = "new" Then

            For Each dd1 In SearchValues.SearchValues

                ' Dim dd() As DataRow = dtPreaAdvice.Select(String.Concat(filterType, "='", dd1.values, "'"))
                For Each dr1 As DataRow In dtPreaAdvice.Select(String.Concat(filterType, "='", dd1.values, "'"))
                    Dim cleaning As New CleaningMobileModel
                    cleaning.EquipmentNo = dr1.Item(GateinData.EQPMNT_NO)
                    cleaning.Customer = dr1.Item(GateinData.CSTMR_CD)
                    cleaning.InDate = dr1.Item(GateinData.GTN_DT)
                    cleaning.PrevoiusCargo = dr1.Item(GateinData.PRDCT_DSCRPTN_VC)
                    cleaning.LastStatusDate = dr1.Item(GateinData.ACTVTY_DT)
                    cleaning.AdditionalCleaningBit = dr1.Item("ADDTNL_CLNNG_BT")

                    arraylist.Add(cleaning)

                Next
            Next


        Else


            For Each dd1 In SearchValues.SearchValues

                'Dim dd() As DataRow = ds.Tables(0).Select(String.Concat(filterType, "='", dd1.values, "'"))
                For Each dr1 As DataRow In ds.Tables(0).Select(String.Concat(filterType, "='", dd1.values, "'"))
                    Dim cleadning As New CleaningMobileModel
                    cleadning.EquipmentNo = dr1.Item(GateinData.EQPMNT_NO).ToString()
                    cleadning.Customer = dr1.Item(GateinData.CSTMR_CD).ToString()
                    cleadning.InDate = dr1.Item(GateinData.GTN_DT).ToString()
                    cleadning.PrevoiusCargo = dr1.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
                    cleadning.LastStatusDate = dr1.Item(GateinData.ACTVTY_DT).ToString()
                    cleadning.AdditionalCleaningBit = dr1.Item("ADDTNL_CLNNG_BT").ToString()
                    cleadning.CleaningRate = dr1.Item("CLNNG_RT").ToString()
                    cleadning.EquipmentStatus = dr1.Item(GateinData.EQPMNT_TYP_ID).ToString()
                    cleadning.EquipmentStatusType = dr1.Item(GateinData.EQPMNT_TYP_CD).ToString()
                    cleadning.CustomerId = dr1.Item(GateinData.CSTMR_ID).ToString()
                    cleadning.Remarks = dr1.Item(GateinData.RMRKS_VC).ToString()
                    cleadning.GiTransactionNo = dr1.Item(GateinData.GI_TRNSCTN_NO).ToString()
                    cleadning.CleaningId = dr1.Item("CLNNG_ID").ToString()
                    cleadning.CleaningReferenceNo = dr1.Item("CLNNG_RFRNC_NO").ToString()

                    'if dd.item("ADDTNL_CLNNG_BT") == 1 then
                    'If dd.Item("ADDTNL_CLNNG_BT") = 1 Then

                    '    Dim clngIn As String = "select [CLNNG_ID],[CLNNG_RFRNC_NO] from [CLEANING] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
                    '    Dim dfghs1 As DataSet = New DataSet()
                    '    conn.connection(clngIn).Fill(dfghs1)

                    '    Dim dtPreaAdvicess As DataTable
                    '    dtPreaAdvicess = dfghs1.Tables(0)

                    '    cleaning.CleaningId = dtPreaAdvicess.Rows(0).Item("CLNNG_ID")
                    '    cleaning.CleaningReferenceNo = dtPreaAdvicess.Rows(0).Item("CLNNG_RFRNC_NO")

                    'Else
                    '    cleaning.CleaningId = ""
                    '    cleaning.CleaningReferenceNo = ""


                    'End If

                    Dim blnCustomerProductRate As Integer
                    Dim blnProductRate As Integer
                    Dim blnSlabRate As Integer
                    blnCustomerProductRate = dr1.Item("CLNNG_CSTMR_PRDCT_RT_BT").ToString()
                    blnProductRate = dr1.Item("CLNNG_PRDCT_RT_BT").ToString()

                    blnSlabRate = objCleaning.pub_CheckSlabRateExists(dr1.Item(CleaningData.CSTMR_ID), dr1.Item(CleaningData.EQPMNT_TYP_ID))

                    If (blnProductRate = 1 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                        cleadning.SlabRate = 1
                    ElseIf (blnProductRate = 0 AndAlso blnCustomerProductRate = 0 AndAlso blnSlabRate = 1) Then
                        cleadning.SlabRate = 1
                    Else
                        cleadning.SlabRate = 0
                    End If

                    'Dim ClngInstr As String = "select [Cleaning_Date] from [Cleaning_Instruction] where [EQPMNT_NO]=" + dd.Item(GateinData.EQPMNT_NO)
                    'Dim InstcDate As DataSet = New DataSet()
                    'conn.connection(ClngInstr).Fill(InstcDate)

                    'Dim dtPreaAdvicess1 As DataTable
                    'dtPreaAdvicess1 = InstcDate.Tables(0)
                    arraylist.Add(cleadning)
                    'cleaning.OriginalCleaningDate = dtPreaAdvicess1.Rows(0).Item("Cleaning_Date")

                Next
            Next

            'Dim cleaningzz As New List(Of CleaningMobileModel)()

        End If

        arrayOfCleaning.ArrayOfCleaning = arraylist
        arrayOfCleaning.status = "Success"
        arrayOfCleaning.statusText = HttpContext.Current.Response.StatusDescription
        arrayOfCleaning.stauscode = HttpContext.Current.Response.StatusCode

        Return arrayOfCleaning
    End Function
End Class