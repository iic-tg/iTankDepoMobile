Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class GateOutMobile
    Inherits System.Web.Services.WebService


    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"
    Dim dsGateOutData As New GateOutDataSet
    Dim str_008EIRTime As String
    Dim bln_008EIRTime_Key As Boolean
    Dim bln_005EqStatus_Key As Boolean
    Dim str_005EqStatus As String
    Dim strMode As String
    Dim bln_022RentalBit_Key As Boolean
    Dim str_022RentalBit As String
    Dim str_055 As String
    Dim bln_055 As Boolean

    Dim objCommon As New CommonData
    Dim gateInMobile As New GateinMobile_C

    <WebMethod(enableSession:=True)> _
     <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function List(ByVal Mode As String, ByVal UserName As String) As GOTList


        Try
            gateInMobile.DepotID(UserName)
            Dim strFilterName As String = ""
            Dim strFilterValue As String = ""
            Dim objCommonUI As New CommonUI()
            Dim dtGateOut As DataTable
            Dim dtRental As New DataTable
            Dim objGateOut As New GateOut()
            Dim objCommon As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim str_059Gateout As String = String.Empty
            Dim strGateOutApprovalProcess As String = Nothing
            Dim GOTList As New GOTList

            str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDPT_ID)
            strGateOutApprovalProcess = objCommonConfig.pub_GetConfigSingleValue("066", intDPT_ID)


            'hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
            '

            Dim strCurrentSessionId As String = objCommon.GetSessionID()
            'objCommon.FlushLockDataByActivity(strCurrentSessionId, "Gate Out")

            ' hdnMode.Value = e.Parameters("Mode").ToString()
            Select Case Mode
                Case strNew
                    'Dim blnShowEqStatus As Boolean = False
                    Dim dsEqpStatus As New DataSet
                    dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate Out", True, intDPT_ID)

                    If str_059Gateout.Trim.ToUpper = "TRUE" Then
                        dsGateOutData = objGateOut.pub_GetGateOutDetailsGWS(intDPT_ID)
                    Else
                        dsGateOutData = objGateOut.pub_GetGateOutDetails(intDPT_ID)
                    End If



                    If strGateOutApprovalProcess <> Nothing AndAlso CBool(strGateOutApprovalProcess) = True Then
7
                        dsGateOutData = New GateOutDataSet
                        dsGateOutData = objGateOut.pub_GetGateOutDetailsGWSWithApproval(intDPT_ID)

                    End If



                    dtGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT)
                    str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDPT_ID)
                    bln_008EIRTime_Key = objCommonConfig.IsKeyExists
                    Dim str_006YardLocation As String
                    str_006YardLocation = objCommon.GetYardLocation()
                    'If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                    '    blnShowEqStatus = True
                    'End If
                    For Each drGateOut As DataRow In dtGateOut.Rows
                        drGateOut.Item(GateOutData.GTOT_ID) = CommonWeb.GetNextIndex(dsGateOutData.Tables(GateOutData._V_GATEOUT), GateOutData.GTOT_ID)
                        'If blnShowEqStatus Then
                        '    drGateOut.Item(GateOutData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.PENDING_STATUS)
                        '    drGateOut.Item(GateOutData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                        'End If
                        If bln_008EIRTime_Key Then
                            drGateOut.Item(GateOutData.GTOT_TM) = str_008EIRTime
                        End If
                        If str_059Gateout.Trim.ToUpper = "TRUE" Then
                            drGateOut.Item(GateOutData.GTOT_TM) = DateTime.Now.ToString("H:mm").ToUpper
                        End If
                        drGateOut.Item(GateOutData.GTOT_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                        drGateOut.Item(GateOutData.YRD_LCTN) = str_006YardLocation
                    Next
                    'For Each drGateOut As DataRow In dtGateOut.Rows



                Case strEdit
                    dsGateOutData = objGateOut.pub_GetGateOutMySubmitDetails(intDPT_ID)
            End Select

            If dsGateOutData.Tables(GateOutData._V_GATEOUT).Rows.Count = 0 Then
                'e.OutputParameters.Add("norecordsfound", True)
                '' hdnNoRecordFound.Value = "True"        



                'GOTList.GOTList = arrayList
                GOTList.Status = "norecordsfound"


                Return GOTList

                Exit Function
            End If
            '
            Dim dsMoreInfo As DataSet
            Dim dsdepot As New DataSet
            dsMoreInfo = objGateOut.pub_V_GateinDetail(intDPT_ID)

            dsGateOutData.Merge(dsMoreInfo.Tables(GateinData._V_GATEIN_DETAIL))
            dsdepot = objCommonUI.pub_GetDepoDetail(intDPT_ID)
            dsGateOutData.Merge(dsdepot.Tables(CommonUIData._DEPOT))

            Dim arrayList As New ArrayList


            For Each dt As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Rows

                Dim GateOutMobileModel As New GateOutMobileModel
                GateOutMobileModel.GTN_ID = String.Empty

                GateOutMobileModel.GTOT_ID = dt.Item("GTOT_ID").ToString()
                GateOutMobileModel.CSTMR_ID = dt.Item("CSTMR_ID").ToString()
                GateOutMobileModel.CSTMR_CD = dt.Item("CSTMR_CD").ToString()
                GateOutMobileModel.EQPMNT_NO = dt.Item("EQPMNT_NO").ToString()
                GateOutMobileModel.EQPMNT_TYP_ID = dt.Item("EQPMNT_TYP_ID").ToString()
                GateOutMobileModel.EQPMNT_TYP_CD = dt.Item("EQPMNT_TYP_CD").ToString()
                GateOutMobileModel.EQPMNT_CD_ID = dt.Item("EQPMNT_CD_ID").ToString()
                GateOutMobileModel.EQPMNT_CD_CD = dt.Item("EQPMNT_CD_CD").ToString()
                GateOutMobileModel.EQPMNT_STTS_ID = dt.Item("EQPMNT_STTS_ID").ToString()
                GateOutMobileModel.EQPMNT_STTS_CD = dt.Item("EQPMNT_STTS_CD").ToString()
                GateOutMobileModel.YRD_LCTN = dt.Item("YRD_LCTN").ToString()
                GateOutMobileModel.GTOT_DT = dt.Item("GTOT_DT").ToString()
                GateOutMobileModel.GTOT_TM = dt.Item("GTOT_TM").ToString()
                GateOutMobileModel.EIR_NO = dt.Item("EIR_NO").ToString()
                GateOutMobileModel.VHCL_NO = dt.Item("VHCL_NO").ToString()
                GateOutMobileModel.TRNSPRTR_CD = dt.Item("TRNSPRTR_CD").ToString()
                GateOutMobileModel.GI_TRNSCTN_NO = dt.Item("GI_TRNSCTN_NO").ToString()
                GateOutMobileModel.DPT_ID = dt.Item("DPT_ID").ToString()
                GateOutMobileModel.PRDCT_CD = dt.Item("PRDCT_CD").ToString()
                GateOutMobileModel.PRDCT_ID = dt.Item("PRDCT_ID").ToString()
                GateOutMobileModel.GTN_DT = dt.Item("GTN_DT").ToString()
                GateOutMobileModel.RNTL_BT = dt.Item("RNTL_BT").ToString()
                GateOutMobileModel.GTN_ID = dt.Item("GTN_ID").ToString()
                GateOutMobileModel.RMRKS_VC = dt.Item("RMRKS_VC").ToString()

                GateOutMobileModel.ORGNL_CSTMR_ID = dt.Item("ORGNL_CSTMR_ID").ToString()
                GateOutMobileModel.ORGNL_CSTMR_CD = dt.Item("ORGNL_CSTMR_CD").ToString()
                GateOutMobileModel.GTOT_CNDTN = dt.Item("GTOT_CNDTN").ToString()


                Select Mode
                    Case strNew

                        Dim dtAttachment As New DataTable
                        Dim objGatein1 As New GateIns
                        Dim objTrans As New Transactions

                        dsGateOutData.Tables(GateinData._ATTACHMENT).Clear()


                        dtAttachment = dsGateOutData.Tables(GateinData._ATTACHMENT).Clone()
                        If dsGateOutData.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                            If GateOutMobileModel.GTN_ID = Nothing AndAlso GateOutMobileModel.GTN_ID = "" Then
                                GateOutMobileModel.GTN_ID = 0
                            End If

                            dtAttachment = objGatein1.GetAttachmentByRepairEstimateId(intDPT_ID, "GateIn", GateOutMobileModel.GTN_ID, objTrans).Tables(GateinData._ATTACHMENT)
                            dsGateOutData.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                        End If
                        'h.GTN_BIN = f.Item(GateinData.GTN_BIN).ToString()
                        'h.PR_ADVC_CD = f.Item(GateinData.PR_ADVC_CD).ToString()
                        'h.EQPMNT_SZ_ID = f.Item(GateinData.EQPMNT_SZ_ID).ToString()
                        'h.EQPMNT_SZ_CD = f.Item(GateinData.EQPMNT_SZ_CD).ToString()
                        'h.CLNNG_RFRNC_NO = f.Item(GateinData.CLNNG_RFRNC_NO).ToString()
                        Dim attch1 As New ArrayList
                        Dim attch As New ArrayList

                        For Each dr As DataRow In dsGateOutData.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", GateOutMobileModel.GTN_ID, " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))

                            Dim attcj As New attchementPro
                            attcj.attchPath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                            attcj.fileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                            attcj.attchId = dr.Item(RepairEstimateData.ATTCHMNT_ID).ToString
                            Dim currentUrl As String = HttpContext.Current.Request.Url.Host
                            'attcj.imageUrl = "http://192.18.0.12/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            Dim Attachment_URL As String = Config.pub_GetAppConfigValue("AttachmentURL")
                            attcj.imageUrl = Attachment_URL + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            'h.imageUrl = "http://"+ currentUrl +"/iTankDepoUI/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            'sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                            'sbAttachment.Append("<br />")
                            attch.Add(attcj)
                        Next


                        If attch.Count > 0 Then
                            GateOutMobileModel.attchement = attch
                        Else
                            GateOutMobileModel.attchement = attch1
                        End If

                        arrayList.Add(GateOutMobileModel)


                    Case strEdit


                        Dim dtAttachment As New DataTable
                        Dim objGatein1 As New GateIns
                        Dim objTrans As New Transactions

                        dsGateOutData.Tables(GateinData._ATTACHMENT).Clear()
                        dtAttachment = dsGateOutData.Tables(GateinData._ATTACHMENT).Clone()
                        If dsGateOutData.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                            ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                            dtAttachment = objGatein1.GetAttachmentByRepairEstimateId(intDPT_ID, "GateOut", dt.Item("GTOT_ID"), objTrans).Tables(GateinData._ATTACHMENT)
                            dsGateOutData.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                            'End If
                        End If
                        'h.GTN_BIN = f.Item(GateinData.GTN_BIN).ToString()
                        'h.PR_ADVC_CD = f.Item(GateinData.PR_ADVC_CD).ToString()
                        'h.EQPMNT_SZ_ID = f.Item(GateinData.EQPMNT_SZ_ID).ToString()
                        'h.EQPMNT_SZ_CD = f.Item(GateinData.EQPMNT_SZ_CD).ToString()
                        'h.CLNNG_RFRNC_NO = f.Item(GateinData.CLNNG_RFRNC_NO).ToString()
                        Dim attch1 As New ArrayList
                        Dim attch As New ArrayList

                        For Each dr As DataRow In dsGateOutData.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", dt.Item("GTOT_ID"), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))

                            Dim attcj As New attchementPro
                            attcj.attchPath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                            attcj.fileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                            attcj.attchId = dr.Item(RepairEstimateData.ATTCHMNT_ID).ToString
                            Dim currentUrl As String = HttpContext.Current.Request.Url.Host
                            'attcj.imageUrl = "http://192.18.0.12/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            Dim Attachment_URL As String = Config.pub_GetAppConfigValue("AttachmentURL")
                            attcj.imageUrl = Attachment_URL + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            'h.imageUrl = "http://"+ currentUrl +"/iTankDepoUI/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                            'sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                            'sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                            'sbAttachment.Append("<br />")
                            attch.Add(attcj)
                        Next


                        If attch.Count > 0 Then
                            GateOutMobileModel.attchement = attch
                        Else
                            GateOutMobileModel.attchement = attch1
                        End If

                        arrayList.Add(GateOutMobileModel)


                End Select





               

              

            Next


            GOTList.Status = "Success"
            GOTList.GOTList = arrayList



            Return GOTList

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
            Dim GOTList As New GOTList
            GOTList.Status = ex.Message
            Return GOTList
        End Try
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

        'gateInMobile.DepotID(UserName)
        Dim strFilterName As String = ""
        Dim strFilterValue As String = ""
        Dim objCommonUI As New CommonUI()
        Dim dtGateOut As DataTable
        Dim dtRental As New DataTable
        Dim objGateOut As New GateOut()
        'Dim objCommon As New CommonData
        Dim objCommonConfig As New ConfigSetting()
        Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
        Dim str_059Gateout As String = String.Empty
        Dim strGateOutApprovalProcess As String = Nothing
        Dim GOTList As New GOTList

        str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDPT_ID)
        strGateOutApprovalProcess = objCommonConfig.pub_GetConfigSingleValue("066", intDPT_ID)


        'hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
        '

        Dim strCurrentSessionId As String = objCommon.GetSessionID()



      

        Select Case Mode
            Case strNew

                Dim dsEqpStatus As New DataSet
                dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate Out", True, intDPT_ID)

                If str_059Gateout.Trim.ToUpper = "TRUE" Then
                    dsGateOutData = objGateOut.pub_GetGateOutDetailsGWS(intDPT_ID)
                Else
                    dsGateOutData = objGateOut.pub_GetGateOutDetails(intDPT_ID)
                End If



                If strGateOutApprovalProcess <> Nothing AndAlso CBool(strGateOutApprovalProcess) = True Then

                    dsGateOutData = New GateOutDataSet
                    dsGateOutData = objGateOut.pub_GetGateOutDetailsGWSWithApproval(intDPT_ID)

                End If



                dtGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT)
                str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDPT_ID)
                bln_008EIRTime_Key = objCommonConfig.IsKeyExists
                Dim str_006YardLocation As String
                str_006YardLocation = objCommon.GetYardLocation()
                'If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                '    blnShowEqStatus = True
                'End If
                For Each drGateOut As DataRow In dtGateOut.Rows
                    drGateOut.Item(GateOutData.GTOT_ID) = CommonWeb.GetNextIndex(dsGateOutData.Tables(GateOutData._V_GATEOUT), GateOutData.GTOT_ID)
                    'If blnShowEqStatus Then
                    '    drGateOut.Item(GateOutData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.PENDING_STATUS)
                    '    drGateOut.Item(GateOutData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                    'End If
                    If bln_008EIRTime_Key Then
                        drGateOut.Item(GateOutData.GTOT_TM) = str_008EIRTime
                    End If
                    If str_059Gateout.Trim.ToUpper = "TRUE" Then
                        drGateOut.Item(GateOutData.GTOT_TM) = DateTime.Now.ToString("H:mm").ToUpper
                    End If
                    drGateOut.Item(GateOutData.GTOT_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                    drGateOut.Item(GateOutData.YRD_LCTN) = str_006YardLocation
                Next
                'For Each drGateOut As DataRow In dtGateOut.Rows


                If filterCondition = "Equals" Then



                    For Each dt As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(filterType, "='", filterValue, "'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)

                    Next

                Else


                    For Each dt As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(filterType, " ", filterNewConditon, " '%", filterValue, "%'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)

                    Next


                End If

            Case strEdit

                dsGateOutData = objGateOut.pub_GetGateOutMySubmitDetails(intDPT_ID)

                If filterCondition = "Equals" Then


                   


                    For Each dt As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(filterType, "='", filterValue, "'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)

                    Next

                Else


                    For Each dt As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(filterType, " ", filterNewConditon, " '%", filterValue, "%'"))

                        Dim filteredvalues As New FilteredValues
                        filteredvalues.Values = dt.Item(filterType).ToString()

                        arraylist.Add(filteredvalues)
                    Next


                End If

        End Select

        listVlaues.ListGateInss = arraylist
        listVlaues.status = "Success"

        Return listVlaues

    End Function



    <WebMethod(enableSession:=True)> _
  <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function SearchList(ByVal UserName As String, ByVal SearchValues As SearchValues, ByVal filterType As String, ByVal Mode As String) As GOTList

        Try
            gateInMobile.DepotID(UserName)
            Dim strFilterName As String = ""
            Dim strFilterValue As String = ""
            Dim objCommonUI As New CommonUI()
            Dim dtGateOut As DataTable
            Dim dtRental As New DataTable
            Dim objGateOut As New GateOut()
            Dim objCommon As New CommonData
            Dim objCommonConfig As New ConfigSetting()
            Dim intDPT_ID As Integer = CommonWeb.iInt(objCommon.GetDepotID())
            Dim str_059Gateout As String = String.Empty
            Dim strGateOutApprovalProcess As String = Nothing
            Dim GOTList As New GOTList

            str_059Gateout = objCommonConfig.pub_GetConfigSingleValue("059", intDPT_ID)
            strGateOutApprovalProcess = objCommonConfig.pub_GetConfigSingleValue("066", intDPT_ID)


            'hdnCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy")
            '

            Dim strCurrentSessionId As String = objCommon.GetSessionID()
            'objCommon.FlushLockDataByActivity(strCurrentSessionId, "Gate Out")

            ' hdnMode.Value = e.Parameters("Mode").ToString()
            Select Case Mode
                Case strNew
                    'Dim blnShowEqStatus As Boolean = False
                    Dim dsEqpStatus As New DataSet
                    dsEqpStatus = objCommonUI.pub_GetWorkFlowActivity("Gate Out", True, intDPT_ID)

                    If str_059Gateout.Trim.ToUpper = "TRUE" Then
                        dsGateOutData = objGateOut.pub_GetGateOutDetailsGWS(intDPT_ID)
                    Else
                        dsGateOutData = objGateOut.pub_GetGateOutDetails(intDPT_ID)
                    End If



                    If strGateOutApprovalProcess <> Nothing AndAlso CBool(strGateOutApprovalProcess) = True Then

                        dsGateOutData = New GateOutDataSet
                        dsGateOutData = objGateOut.pub_GetGateOutDetailsGWSWithApproval(intDPT_ID)

                    End If



                    dtGateOut = dsGateOutData.Tables(GateOutData._V_GATEOUT)
                    str_008EIRTime = objCommonConfig.pub_GetConfigSingleValue("008", intDPT_ID)
                    bln_008EIRTime_Key = objCommonConfig.IsKeyExists
                    Dim str_006YardLocation As String
                    str_006YardLocation = objCommon.GetYardLocation()
                    'If dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows.Count > 0 Then
                    '    blnShowEqStatus = True
                    'End If
                    For Each drGateOut As DataRow In dtGateOut.Rows
                        drGateOut.Item(GateOutData.GTOT_ID) = CommonWeb.GetNextIndex(dsGateOutData.Tables(GateOutData._V_GATEOUT), GateOutData.GTOT_ID)
                        'If blnShowEqStatus Then
                        '    drGateOut.Item(GateOutData.EQPMNT_STTS_CD) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.PENDING_STATUS)
                        '    drGateOut.Item(GateOutData.EQPMNT_STTS_ID) = dsEqpStatus.Tables(CommonUIData._WF_ACTIVITY).Rows(0).Item(CommonUIData.DEFAULT_STATUS_ID)
                        'End If
                        If bln_008EIRTime_Key Then
                            drGateOut.Item(GateOutData.GTOT_TM) = str_008EIRTime
                        End If
                        If str_059Gateout.Trim.ToUpper = "TRUE" Then
                            drGateOut.Item(GateOutData.GTOT_TM) = DateTime.Now.ToString("H:mm").ToUpper
                        End If
                        drGateOut.Item(GateOutData.GTOT_DT) = DateTime.Now.ToString("dd-MMM-yyyy")
                        drGateOut.Item(GateOutData.YRD_LCTN) = str_006YardLocation
                    Next
                    'For Each drGateOut As DataRow In dtGateOut.Rows



                Case strEdit
                    dsGateOutData = objGateOut.pub_GetGateOutMySubmitDetails(intDPT_ID)
            End Select

            If dsGateOutData.Tables(GateOutData._V_GATEOUT).Rows.Count = 0 Then
                'e.OutputParameters.Add("norecordsfound", True)
                '' hdnNoRecordFound.Value = "True"        



                'GOTList.GOTList = arrayList
                GOTList.Status = "norecordsfound"


                Return GOTList

                Exit Function
            End If
            '
            Dim dsMoreInfo As DataSet
            Dim dsdepot As New DataSet
            dsMoreInfo = objGateOut.pub_V_GateinDetail(intDPT_ID)

            dsGateOutData.Merge(dsMoreInfo.Tables(GateinData._V_GATEIN_DETAIL))
            dsdepot = objCommonUI.pub_GetDepoDetail(intDPT_ID)
            dsGateOutData.Merge(dsdepot.Tables(CommonUIData._DEPOT))

            Dim arrayList As New ArrayList

            For Each dd In SearchValues.SearchValues


                ' Dim dt() As DataRow = dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(filterType, "='", dd.values, "'"))
                For Each dr1 As DataRow In dsGateOutData.Tables(GateOutData._V_GATEOUT).Select(String.Concat(filterType, "='", dd.values, "'"))

                    Dim GateOutMobileModel As New GateOutMobileModel


                    GateOutMobileModel.GTOT_ID = dr1.Item("GTOT_ID").ToString()
                    GateOutMobileModel.CSTMR_ID = dr1.Item("CSTMR_ID").ToString()
                    GateOutMobileModel.CSTMR_CD = dr1.Item("CSTMR_CD").ToString()
                    GateOutMobileModel.EQPMNT_NO = dr1.Item("EQPMNT_NO").ToString()
                    GateOutMobileModel.EQPMNT_TYP_ID = dr1.Item("EQPMNT_TYP_ID").ToString()
                    GateOutMobileModel.EQPMNT_TYP_CD = dr1.Item("EQPMNT_TYP_CD").ToString()
                    GateOutMobileModel.EQPMNT_CD_ID = dr1.Item("EQPMNT_CD_ID").ToString()
                    GateOutMobileModel.EQPMNT_CD_CD = dr1.Item("EQPMNT_CD_CD").ToString()
                    GateOutMobileModel.EQPMNT_STTS_ID = dr1.Item("EQPMNT_STTS_ID").ToString()
                    GateOutMobileModel.EQPMNT_STTS_CD = dr1.Item("EQPMNT_STTS_CD").ToString()
                    GateOutMobileModel.YRD_LCTN = dr1.Item("YRD_LCTN").ToString()
                    GateOutMobileModel.GTOT_DT = dr1.Item("GTOT_DT").ToString()
                    GateOutMobileModel.GTOT_TM = dr1.Item("GTOT_TM").ToString()
                    GateOutMobileModel.EIR_NO = dr1.Item("EIR_NO").ToString()
                    GateOutMobileModel.VHCL_NO = dr1.Item("VHCL_NO").ToString()
                    GateOutMobileModel.TRNSPRTR_CD = dr1.Item("TRNSPRTR_CD").ToString()
                    GateOutMobileModel.GI_TRNSCTN_NO = dr1.Item("GI_TRNSCTN_NO").ToString()
                    GateOutMobileModel.DPT_ID = dr1.Item("DPT_ID").ToString()
                    GateOutMobileModel.PRDCT_CD = dr1.Item("PRDCT_CD").ToString()
                    GateOutMobileModel.PRDCT_ID = dr1.Item("PRDCT_ID").ToString()
                    GateOutMobileModel.GTN_DT = dr1.Item("GTN_DT").ToString()
                    GateOutMobileModel.RNTL_BT = dr1.Item("RNTL_BT").ToString()
                    GateOutMobileModel.GTN_ID = dr1.Item("GTN_ID").ToString()
                    GateOutMobileModel.ORGNL_CSTMR_ID = dr1.Item("ORGNL_CSTMR_ID").ToString()
                    GateOutMobileModel.ORGNL_CSTMR_CD = dr1.Item("ORGNL_CSTMR_CD").ToString()
                    GateOutMobileModel.GTOT_CNDTN = dr1.Item("GTOT_CNDTN").ToString()
                    GateOutMobileModel.RMRKS_VC = dr1.Item("RMRKS_VC").ToString()




                    Dim dtAttachment As New DataTable
                    Dim objGatein1 As New GateIns
                    Dim objTrans As New Transactions

                    dsGateOutData.Tables(GateinData._ATTACHMENT).Clear()
                    dtAttachment = dsGateOutData.Tables(GateinData._ATTACHMENT).Clone()
                    If dsGateOutData.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                        ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                        dtAttachment = objGatein1.GetAttachmentByRepairEstimateId(intDPT_ID, "GateIn", dr1.Item("GTN_ID"), objTrans).Tables(GateinData._ATTACHMENT)
                        dsGateOutData.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                        'End If
                    End If
                    'h.GTN_BIN = f.Item(GateinData.GTN_BIN).ToString()
                    'h.PR_ADVC_CD = f.Item(GateinData.PR_ADVC_CD).ToString()
                    'h.EQPMNT_SZ_ID = f.Item(GateinData.EQPMNT_SZ_ID).ToString()
                    'h.EQPMNT_SZ_CD = f.Item(GateinData.EQPMNT_SZ_CD).ToString()
                    'h.CLNNG_RFRNC_NO = f.Item(GateinData.CLNNG_RFRNC_NO).ToString()
                    Dim attch1 As New ArrayList
                    Dim attch As New ArrayList

                    For Each dr As DataRow In dsGateOutData.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", dr1.Item("GTN_ID"), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))

                        Dim attcj As New attchementPro
                        attcj.attchPath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                        attcj.fileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                        Dim currentUrl As String = HttpContext.Current.Request.Url.Host
                        'attcj.imageUrl = "http://192.18.0.12/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        Dim Attachment_URL As String = Config.pub_GetAppConfigValue("AttachmentURL")
                        attcj.imageUrl = Attachment_URL + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        'h.imageUrl = "http://"+ currentUrl +"/iTankDepoUI/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                        'sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                        'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                        'sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                        'sbAttachment.Append("<br />")
                        attch.Add(attcj)
                    Next

                    If attch.Count > 0 Then
                        GateOutMobileModel.attchement = attch
                    Else
                        GateOutMobileModel.attchement = attch1
                    End If

                    arrayList.Add(GateOutMobileModel)

                Next

            Next
            GOTList.Status = "Success"
            GOTList.GOTList = arrayList



            Return GOTList

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
            Dim GOTList As New GOTList
            GOTList.Status = ex.Message
            Return GOTList
        End Try
    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function Update(ByVal YardLocation As String, ByVal EquipmentNo As String, ByVal OutDate As String, ByVal Time As String, ByVal EIRNo As String,
                           ByVal VehicalNo As String, ByVal TransPorter As String, ByVal Rental As String, ByVal UserName As String, ByVal Mode As String,
                           ByVal RepairEstimateId As String, ByVal Remarks As String,
                           ByVal IfAttchment As String, ByVal hfc As ArrayOfFileParams) As GOTList



        Try


            Dim GateOutMobile_C As New GateOutMobile_C

            Dim result As Boolean = GateOutMobile_C.Update(YardLocation, EquipmentNo, OutDate, Time, EIRNo, VehicalNo, TransPorter, Rental, UserName, Mode, RepairEstimateId, Remarks, IfAttchment, hfc)
            Dim GOTList As New GOTList
            If result Then

                GOTList.Status = "Updated"


            End If

            Return GOTList
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                           MethodBase.GetCurrentMethod.Name, ex)
            Dim GOTList As New GOTList
            GOTList.Status = ex.Message
            Return GOTList
        End Try

    End Function



    

End Class