Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Globalization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class GateinMobile
    Inherits System.Web.Services.WebService

    Dim gateInMobile As New GateinMobile_C
    Dim dsGateInData As New GateinDataSet
    Dim dsGateInAttchmentData As New GateinDataSet
    Dim dtGateinData As DataTable
    Private Const strNew As String = "new"
    Private Const strEdit As String = "edit"


    'Classs Model'''''''''''
    Dim validationEquipment As New GateinMobileModel
    Dim validationSuccess As New EquipmentValidateSuccess

    Dim h1 As New ListModel


    Dim hlist As New ArrayList

    Dim validateAttachment As New pvt_ValidateGateINAttachment
    Dim equipmentCode As New pvt_GetEquipmentCode
    Dim conn As New Dropdown_C



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function PreAdviceList(ByVal UserName As String, ByVal Mode As String) As ListModel




        Try



            Dim ouput As GateinDataSet
            Dim objCommon As New CommonData
            'Dim ouput1 As DataRow

            ouput = gateInMobile.PreAdvice(UserName, Mode)
            Dim intDepotID As Integer = objCommon.GetDepotID()





            'var milli = "/Date(1245398693390)/".replace(/\/Date\((-?\d+)\)\//, '$1');
            'var d = new Date(parseInt(milli));

            For Each f As DataRow In ouput.Tables(GateinData._V_GATEIN).Rows

                'h.AGNT_CD = f.Item(GateinData.AGNT_CD).ToString()
                'h.GTN_DT = f.Item(GateinData.GTN_DT)
                Dim h As New GateInss
                h.GTN_ID = f.Item(GateinData.GTN_ID).ToString()
                h.GTN_CD = f.Item(GateinData.GTN_CD).ToString()
                h.CSTMR_ID = f.Item(GateinData.CSTMR_ID).ToString()
                h.CSTMR_CD = f.Item(GateinData.CSTMR_CD).ToString()
                h.EQPMNT_NO = f.Item(GateinData.EQPMNT_NO).ToString()
                h.EQPMNT_TYP_ID = f.Item(GateinData.EQPMNT_TYP_ID).ToString()
                h.EQPMNT_TYP_CD = f.Item(GateinData.EQPMNT_TYP_CD).ToString()
                h.EQPMNT_CD_ID = f.Item(GateinData.EQPMNT_CD_ID).ToString()
                h.EQPMNT_CD_CD = f.Item(GateinData.EQPMNT_CD_CD).ToString()
                h.EQPMNT_STTS_ID = f.Item(GateinData.EQPMNT_STTS_ID).ToString()
                h.EQPMNT_STTS_CD = f.Item(GateinData.EQPMNT_STTS_CD).ToString()
                h.YRD_LCTN = f.Item(GateinData.YRD_LCTN).ToString()
                h.GTN_DT = f.Item(GateinData.GTN_DT).ToString()
                h.GTN_TM = f.Item(GateinData.GTN_TM).ToString()
                h.PRDCT_ID = f.Item(GateinData.PRDCT_ID).ToString()
                h.PRDCT_CD = f.Item(GateinData.PRDCT_CD).ToString()
                h.EIR_NO = f.Item(GateinData.EIR_NO).ToString()
                h.VHCL_NO = f.Item(GateinData.VHCL_NO).ToString()
                h.TRNSPRTR_CD = f.Item(GateinData.TRNSPRTR_CD).ToString()
                h.HTNG_BT = "False"
                h.RMRKS_VC = f.Item(GateinData.RMRKS_VC).ToString()
                h.GI_TRNSCTN_NO = f.Item(GateinData.GI_TRNSCTN_NO).ToString()
                h.DPT_ID = f.Item(GateinData.DPT_ID).ToString()
                h.CRTD_BY = f.Item(GateinData.CRTD_BY).ToString()
                h.CRTD_DT = f.Item(GateinData.CRTD_DT).ToString()
                h.MDFD_BY = f.Item(GateinData.MDFD_BY).ToString()
                h.MDFD_DT = f.Item(GateinData.MDFD_DT).ToString()
                'h.FRM_PRE_ADVC_BT).ToString() = FRM_PRE_ADVC_BT
                'h.EQUPMNT_STTS_DSCRPTN_VC).ToString() = EQUPMNT_STTS_DSCRPTN_VC
                h.CHECKED = f.Item(GateinData.CHECKED).ToString()
                'h.MD_OF_PYMNT).ToString() = MD_OF_PYMNT
                h.BLLNG_TYP = f.Item(GateinData.BLLNG_TYP).ToString()
                h.CSTMR_NAM = f.Item(GateinData.CSTMR_NAM).ToString()
                h.ALLOW_UPDATE = f.Item(GateinData.ALLOW_UPDATE).ToString()
                h.PR_ADVC_BT = f.Item(GateinData.PR_ADVC_BT).ToString()
                h.HTNG_EDIT = f.Item(GateinData.HTNG_EDIT).ToString()
                h.PRDCT_DSCRPTN_VC = f.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
                h.RNTL_BT = "False"
                h.RNTL_RFRNC_NO = f.Item(GateinData.RNTL_RFRNC_NO).ToString()
                h.ALLOW_RENTAL = f.Item(GateinData.ALLOW_RENTAL).ToString()
                h.COUNT_ATTACH = f.Item(GateinData.COUNT_ATTACH).ToString()
                h.RDL_ATH = f.Item(GateinData.RDL_ATH).ToString()
                h.GRD_ID = f.Item(GateinData.GRD_ID).ToString()
                h.AGNT_ID = f.Item(GateinData.AGNT_ID).ToString()
                h.CNSGNE = f.Item(GateinData.CNSGNE).ToString()
                h.BLL_CD = f.Item(GateinData.BLL_CD).ToString()
                h.BLL_ID = f.Item(GateinData.BLL_ID).ToString()
                h.AGNT_CD = f.Item(GateinData.AGNT_CD).ToString()
                h.GRD_CD = f.Item(GateinData.GRD_CD).ToString()
                h.CSC_VLDTY = f.Item(GateinData.CSC_VLDTY).ToString()
                h.MNFCTR_DT = f.Item(GateinData.MNFCTR_DT).ToString()
                h.PR_ADVC_ID = f.Item(GateinData.PR_ADVC_ID).ToString()
                h.GTN_BIN = f.Item(GateinData.GTN_BIN).ToString()
                h.PR_ADVC_CD = f.Item(GateinData.PR_ADVC_CD).ToString()
                h.EQPMNT_SZ_ID = f.Item(GateinData.EQPMNT_SZ_ID).ToString()
                h.EQPMNT_SZ_CD = f.Item(GateinData.EQPMNT_SZ_CD).ToString()
                h.CLNNG_RFRNC_NO = f.Item(GateinData.CLNNG_RFRNC_NO).ToString()

                Dim dtAttachment As New DataTable
                Dim objGatein As New GateIns
                Dim objTrans As New Transactions

                ouput.Tables(GateinData._ATTACHMENT).Clear()
                dtAttachment = ouput.Tables(GateinData._ATTACHMENT).Clone()
                If ouput.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                    ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                    dtAttachment = objGatein.GetAttachmentByRepairEstimateId(intDepotID, "Pre-Advice", f.Item(GateinData.PR_ADVC_CD), objTrans).Tables(GateinData._ATTACHMENT)
                    ouput.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                    'End If
                End If
                'Dim attcj1 As New attchementPro
                'attcj1.attchPath = ""
                'attcj1.fileName = ""

                'attcj1.imageUrl = ""

                Dim attch1 As New ArrayList
                Dim attch As New ArrayList
                'attch1.Add(attcj1)

                For Each dr As DataRow In ouput.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", f.Item(GateinData.PR_ADVC_CD), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))
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
                    h.attchement = attch
                Else
                    h.attchement = attch1
                End If

                hlist.Add(h)
            Next

            h1.status = "Success"
            h1.stauscode = HttpContext.Current.Response.StatusCode
            h1.statusText = HttpContext.Current.Response.StatusDescription
            h1.ListGateInss = hlist






            'h.AGNT_CD = ouput1.i
            'h.age = 26

            Return h1

            'End If

            'Dim conditionfial As New COnditionFail

            'conditionfial.confial = "Session Expired"
            'Return New JavaScriptSerializer().Serialize(conditionfial)

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            h1.stauscode = HttpContext.Current.Response.StatusCode
            h1.statusText = HttpContext.Current.Response.StatusDescription
            h1.status = ex.Message
            Return h1

        End Try

        'Return "Hello World"
    End Function



    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function OnCreateNew(ByVal UserName As String) As DefaultValues



        Try


            gateInMobile.DepotID(UserName)
            Dim dtSettings As DataTable
            Dim dsSettings As New ConfigDataSet
            Dim strSelectOrderBy As String = String.Concat(CommonUIData.CNFG_TMPLT_ID, " ASC")
            Dim objCommon As New CommonUI
            Dim objCommonData As New CommonData

            Dim strFilter As String = String.Concat(CommonUIData.DPT_ID, "='", objCommonData.GetDepotID().ToString, "'")
            dtSettings = objCommon.pub_GetConfigTemplate(CInt(objCommonData.GetDepotID())).Tables(CommonUIData._CONFIG)
            For Each drSettings As DataRow In dtSettings.Select(String.Empty, strSelectOrderBy)
                Dim drArr As DataRow() = dsSettings.Tables(CommonUIData._CONFIG).Select(CommonUIData.CNFG_TMPLT_ID & "='" & drSettings.Item(CommonUIData.CNFG_TMPLT_ID).ToString & "'")
                If Not drArr.Length > 0 Then
                    Dim drNew As DataRow = dsSettings.Tables(CommonUIData._CONFIG).NewRow()
                    drNew.Item("TMP_CNFG_ID") = CommonWeb.GetNextIndex(dsSettings.Tables(CommonUIData._CONFIG), "TMP_CNFG_ID")
                    drNew.Item(CommonUIData.CNFG_ID) = drSettings.Item(CommonUIData.CNFG_ID)
                    drNew.Item(CommonUIData.CNFG_TMPLT_ID) = drSettings.Item(CommonUIData.CNFG_TMPLT_ID)
                    drNew.Item(CommonUIData.KY_NAM) = drSettings.Item(CommonUIData.KY_NAM)
                    drNew.Item(CommonUIData.KY_DSCRPTION) = drSettings.Item(CommonUIData.KY_DSCRPTION)
                    drNew.Item(CommonUIData.CNFG_TYP) = drSettings.Item(CommonUIData.CNFG_TYP)
                    drNew.Item(CommonUIData.ENBLD_BT) = drSettings.Item(CommonUIData.ENBLD_BT)
                    drNew.Item(CommonUIData.ACTV_BT) = drSettings.Item(CommonUIData.ACTV_BT)
                    drNew.Item(CommonUIData.DPT_ID) = drSettings.Item(CommonUIData.DPT_ID)
                    If Not IsDBNull(drSettings.Item(CommonUIData.KY_VL)) Then
                        If drSettings.Item(CommonUIData.KY_VL).ToString <> "" Then
                            drNew.Item(CommonUIData.KY_VL) = CommonWeb.DecryptString(drSettings.Item(CommonUIData.KY_VL).ToString)
                        End If
                    End If
                    dsSettings.Tables(CommonUIData._CONFIG).Rows.Add(drNew)
                End If
            Next


            Dim GateInDefaultEquipmentStatus As String = ""
            Dim GateInDefaultYardLocation As String = ""
            Dim GateInDefaultEquipmentType As String = ""
            Dim GateInDefaultEquipmentCode As String = ""
            Dim DefaultValues As New DefaultValues

            Dim dtES() As DataRow = dsSettings.Tables(CommonUIData._CONFIG).Select(String.Concat(CommonUIData.KY_NAM, "=", "005"))

            GateInDefaultEquipmentStatus = dtES(0).Item(CommonUIData.KY_VL).ToString()

            Dim dtYL() As DataRow = dsSettings.Tables(CommonUIData._CONFIG).Select(String.Concat(CommonUIData.KY_NAM, "=", "006"))

            GateInDefaultYardLocation = dtYL(0).Item(CommonUIData.KY_VL).ToString()

            Dim dtET() As DataRow = dsSettings.Tables(CommonUIData._CONFIG).Select(String.Concat(CommonUIData.KY_NAM, "=", "009"))

            GateInDefaultEquipmentType = dtET(0).Item(CommonUIData.KY_VL).ToString()

            Dim dtEC() As DataRow = dsSettings.Tables(CommonUIData._CONFIG).Select(String.Concat(CommonUIData.KY_NAM, "=", "010"))

            GateInDefaultEquipmentCode = dtEC(0).Item(CommonUIData.KY_VL).ToString()




            DefaultValues.EquipmentStatus = GateInDefaultEquipmentStatus.ToString()
            DefaultValues.YardLocation = GateInDefaultYardLocation.ToString()
            DefaultValues.EquipmentType = GateInDefaultEquipmentType.ToString()
            DefaultValues.EquipmentCode = GateInDefaultEquipmentCode.ToString()
            DefaultValues.Status = "Success"


            Return DefaultValues


        Catch ex As Exception
            Dim DefaultValues As New DefaultValues
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            DefaultValues.Status = ex.Message

            Return DefaultValues

        End Try

    End Function




    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function Mysubmit(ByVal UserName As String, ByVal Mode As String) As ListModel



        Try

            'Dim srtu As String = CommonWeb.GetNextIndex(dsGateInData.Tables(GateinData._V_GATEIN), GateinData.GTN_ID)

            Dim dsGateInData As New GateinDataSet
            Dim objGateIn As New Gatein
            Dim objCommon As New CommonData

            gateInMobile.DepotID(UserName)
            Dim intDepotID As Integer = objCommon.GetDepotID()

            'dsGateInData = gateInMobile.PreAdvice(UserName, Mode)

            dsGateInData = gateInMobile.PreAdvice(UserName, Mode)





            'var milli = "/Date(1245398693390)/".replace(/\/Date\((-?\d+)\)\//, '$1');
            'var d = new Date(parseInt(milli));

            For Each f As DataRow In dsGateInData.Tables(GateinData._V_GATEIN).Rows

                'h.AGNT_CD = f.Item(GateinData.AGNT_CD).ToString()
                'h.GTN_DT = f.Item(GateinData.GTN_DT)
                Dim h As New GateInss
                h.GTN_ID = f.Item(GateinData.GTN_ID).ToString()
                h.GTN_CD = f.Item(GateinData.GTN_CD).ToString()
                h.CSTMR_ID = f.Item(GateinData.CSTMR_ID).ToString()
                h.CSTMR_CD = f.Item(GateinData.CSTMR_CD).ToString()
                h.EQPMNT_NO = f.Item(GateinData.EQPMNT_NO).ToString()
                h.EQPMNT_TYP_ID = f.Item(GateinData.EQPMNT_TYP_ID).ToString()
                h.EQPMNT_TYP_CD = f.Item(GateinData.EQPMNT_TYP_CD).ToString()
                h.EQPMNT_CD_ID = f.Item(GateinData.EQPMNT_CD_ID).ToString()
                h.EQPMNT_CD_CD = f.Item(GateinData.EQPMNT_CD_CD).ToString()
                h.EQPMNT_STTS_ID = f.Item(GateinData.EQPMNT_STTS_ID).ToString()
                h.EQPMNT_STTS_CD = f.Item(GateinData.EQPMNT_STTS_CD).ToString()
                h.YRD_LCTN = f.Item(GateinData.YRD_LCTN).ToString()
                h.GTN_DT = f.Item(GateinData.GTN_DT).ToString()
                h.GTN_TM = f.Item(GateinData.GTN_TM).ToString()
                h.PRDCT_ID = f.Item(GateinData.PRDCT_ID).ToString()
                h.PRDCT_CD = f.Item(GateinData.PRDCT_CD).ToString()
                h.EIR_NO = f.Item(GateinData.EIR_NO).ToString()
                h.VHCL_NO = f.Item(GateinData.VHCL_NO).ToString()
                h.TRNSPRTR_CD = f.Item(GateinData.TRNSPRTR_CD).ToString()
                h.HTNG_BT = f.Item(GateinData.HTNG_BT).ToString()
                h.RMRKS_VC = f.Item(GateinData.RMRKS_VC).ToString()
                h.GI_TRNSCTN_NO = f.Item(GateinData.GI_TRNSCTN_NO).ToString()
                h.DPT_ID = f.Item(GateinData.DPT_ID).ToString()
                h.CRTD_BY = f.Item(GateinData.CRTD_BY).ToString()
                h.CRTD_DT = f.Item(GateinData.CRTD_DT).ToString()
                h.MDFD_BY = f.Item(GateinData.MDFD_BY).ToString()
                h.MDFD_DT = f.Item(GateinData.MDFD_DT).ToString()
                'h.FRM_PRE_ADVC_BT).ToString() = FRM_PRE_ADVC_BT
                'h.EQUPMNT_STTS_DSCRPTN_VC).ToString() = EQUPMNT_STTS_DSCRPTN_VC
                h.CHECKED = f.Item(GateinData.CHECKED).ToString()
                'h.MD_OF_PYMNT).ToString() = MD_OF_PYMNT
                h.BLLNG_TYP = f.Item(GateinData.BLLNG_TYP).ToString()
                h.CSTMR_NAM = f.Item(GateinData.CSTMR_NAM).ToString()
                h.ALLOW_UPDATE = f.Item(GateinData.ALLOW_UPDATE).ToString()
                h.PR_ADVC_BT = f.Item(GateinData.PR_ADVC_BT).ToString()
                h.HTNG_EDIT = f.Item(GateinData.HTNG_EDIT).ToString()
                h.PRDCT_DSCRPTN_VC = f.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
                h.RNTL_BT = f.Item(GateinData.RNTL_BT).ToString()
                h.RNTL_RFRNC_NO = f.Item(GateinData.RNTL_RFRNC_NO).ToString()
                h.ALLOW_RENTAL = f.Item(GateinData.ALLOW_RENTAL).ToString()
                h.COUNT_ATTACH = f.Item(GateinData.COUNT_ATTACH).ToString()
                h.RDL_ATH = f.Item(GateinData.RDL_ATH).ToString()
                h.GRD_ID = f.Item(GateinData.GRD_ID).ToString()
                h.AGNT_ID = f.Item(GateinData.AGNT_ID).ToString()
                h.CNSGNE = f.Item(GateinData.CNSGNE).ToString()
                h.BLL_CD = f.Item(GateinData.BLL_CD).ToString()
                h.BLL_ID = f.Item(GateinData.BLL_ID).ToString()
                h.AGNT_CD = f.Item(GateinData.AGNT_CD).ToString()
                h.GRD_CD = f.Item(GateinData.GRD_CD).ToString()
                h.CSC_VLDTY = f.Item(GateinData.CSC_VLDTY).ToString()
                h.MNFCTR_DT = f.Item(GateinData.MNFCTR_DT).ToString()
                h.PR_ADVC_ID = f.Item(GateinData.PR_ADVC_ID).ToString()


                Dim dtAttachment As New DataTable
                Dim objGatein1 As New GateIns
                Dim objTrans As New Transactions

                dsGateInData.Tables(GateinData._ATTACHMENT).Clear()
                dtAttachment = dsGateInData.Tables(GateinData._ATTACHMENT).Clone()
                If dsGateInData.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                    ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                    dtAttachment = objGatein1.GetAttachmentByRepairEstimateId(intDepotID, "GateIn", f.Item(GateinData.GTN_ID), objTrans).Tables(GateinData._ATTACHMENT)
                    dsGateInData.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                    'End If
                End If
                'h.GTN_BIN = f.Item(GateinData.GTN_BIN).ToString()
                'h.PR_ADVC_CD = f.Item(GateinData.PR_ADVC_CD).ToString()
                'h.EQPMNT_SZ_ID = f.Item(GateinData.EQPMNT_SZ_ID).ToString()
                'h.EQPMNT_SZ_CD = f.Item(GateinData.EQPMNT_SZ_CD).ToString()
                'h.CLNNG_RFRNC_NO = f.Item(GateinData.CLNNG_RFRNC_NO).ToString()
                Dim attch1 As New ArrayList
                Dim attch As New ArrayList

                For Each dr As DataRow In dsGateInData.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", f.Item(GateinData.GTN_ID), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))

                    Dim attcj As New attchementPro
                    attcj.attchPath = dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString
                    attcj.fileName = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                    attcj.attchId = dr.Item(RepairEstimateData.ATTCHMNT_ID).ToString
                    Dim currentUrl As String = HttpContext.Current.Request.Url.Host
                    'attcj.imageUrl = "http://192.18.0.12/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                    Dim Attachment_URL As String = Config.pub_GetAppConfigValue("AttachmentURL")
                    'attcj.imageUrl = dr.Item(RepairEstimateData.ACTL_FL_NM).ToString
                    attcj.imageUrl = Attachment_URL + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                    'h.imageUrl = "http://"+ currentUrl +"/iTankDepoUI/Download.ashx?FL_NM=" + dr.Item(RepairEstimateData.ATTCHMNT_PTH).ToString + ""
                    'sbAttachment.Append("<a title=""Delete Image"" onclick=""deletePhoto('" + CStr(dr.Item(RepairEstimateData.ATTCHMNT_ID)) + "' ); return false;"" href=""#"" ><i class='icon-trash' style=""color:red;""></i></a>&nbsp;&nbsp;")
                    'sbAttachment.Append(String.Concat("<a target='_blank' href='../download.ashx?FL_NM=", strFileName, "'  title='Click to download error file' >", strFileName, "</a>"))
                    'sbAttachment.Append(String.Concat("<a target='_blank' href='//", strPath, "?FL_NM=", strPhysicalpath, "'  title='Click to download file' >", strFileName, "</a>"))
                    'sbAttachment.Append("<br />")
                    attch.Add(attcj)
                Next

                If attch.Count > 0 Then
                    h.attchement = attch
                Else
                    h.attchement = attch1
                End If

                hlist.Add(h)
            Next

            h1.status = "Success"
            h1.stauscode = HttpContext.Current.Response.StatusCode
            h1.statusText = HttpContext.Current.Response.StatusDescription
            h1.ListGateInss = hlist


            Return h1

        Catch ex As Exception


            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            h1.stauscode = HttpContext.Current.Response.StatusCode
            h1.statusText = HttpContext.Current.Response.StatusDescription
            h1.status = ex.Message
            Return h1

        End Try


    End Function







    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function DeleteAttachment(ByVal EquipmentNo As String, ByVal bv_strAttachmentId As String, ByVal bv_strRepairEstimateId As String,
                                      ByVal UserName As String, ByVal Mode As String) As DateValidate


        Try



            gateInMobile.DepotID(UserName)


            Dim objCommon As New CommonData


            Dim intDepotID As Integer = objCommon.GetDepotID()
            'dsGateInData = gateInMobile.PreAdvice(UserName, Mode)

            dsGateInData = gateInMobile.PreAdvice(UserName, Mode)
            Dim PageName As String
            Dim Id As Long




            Dim dtAttachment As New DataTable
            Dim objGatein1 As New GateIns
            Dim objTrans As New Transactions

            dsGateInData.Tables(GateinData._ATTACHMENT).Clear()
            dtAttachment = dsGateInData.Tables(GateinData._ATTACHMENT).Clone()
            Dim dr1() As DataRow = dsGateInData.Tables(GateinData._V_GATEIN).Select(String.Concat(GateinData.EQPMNT_NO, "='", EquipmentNo, "'"))

            If Mode = "new" Then
                PageName = "Pre-Advice"
                Id = dr1(0).Item(GateinData.PR_ADVC_ID)
            Else
                PageName = "GateIn"
                Id = dr1(0).Item(GateinData.GTN_ID)
            End If
            If dsGateInData.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                dtAttachment = objGatein1.GetAttachmentByRepairEstimateId(intDepotID, PageName, Id, objTrans).Tables(GateinData._ATTACHMENT)
                dsGateInData.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                'End If
            End If
            Dim dr() As DataRow
            Dim DleAtt As New DateValidate

            If Not IsNothing(dsGateInData) Then
                dr = dsGateInData.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.ATTCHMNT_ID, "= ", bv_strAttachmentId))
                If dr.Length > 0 Then
                    dr(0).Delete()
                    'Know Delete operation occur
                    DleAtt.statusText = "Deleted"

                    Return DleAtt

                Else
                    DleAtt.statusText = "Not Deleted"

                    Return DleAtt

                End If
                'ds.AcceptChanges()


            End If

        Catch ex As Exception

            Dim DleAtt As New DateValidate
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            DleAtt.statusText = ex.Message
            Return DleAtt

        End Try


    End Function



    'pvt_ValidateGateINAttachment

    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function pvt_ValidateGateINAttachment(ByVal bv_strGateINPreAdviceID As String, ByVal UserName As String) As pvt_ValidateGateINAttachment


        Try
            gateInMobile.DepotID(UserName)
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim dsGateOutAttchemnt As GateinDataSet
            dsGateOutAttchemnt = objGateIn.pub_GetAttchemntbyGateIN(CInt(bv_strGateINPreAdviceID), "Pre-Advice")
            If CInt(dsGateOutAttchemnt.Tables(GateinData._V_GATEIN).Rows(0).Item("COUNT_ATTACH")) > 0 Then
                validateAttachment.validateGateINAttachment = "Yes"
            Else
                validateAttachment.validateGateINAttachment = "No"
            End If

            validateAttachment.status = "Success"
            validateAttachment.statusText = HttpContext.Current.Response.StatusDescription
            validateAttachment.stausCode = HttpContext.Current.Response.StatusCode

            Return validateAttachment
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
            validateAttachment.validateGateINAttachment = ex.Message
            validateAttachment.statusText = HttpContext.Current.Response.StatusDescription
            validateAttachment.stausCode = HttpContext.Current.Response.StatusCode
            Return validateAttachment

        End Try

    End Function


    <WebMethod(enableSession:=True)> _
   <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function pvt_GetEquipmentCode(ByVal bv_strType As String, ByVal bv_userName As String) As pvt_GetEquipmentCode


        Try
            gateInMobile.DepotID(bv_userName)
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData

            Dim intDepotID As Integer
            Dim dt As New DataTable
            If objCommon.GetMultiLocationSupportConfig.ToLower = "true" Then
                intDepotID = CInt(objCommon.GetHeadQuarterID())
            Else
                intDepotID = CInt(objCommon.GetDepotID())
            End If
            dt = objCommonUI.GetEquipmentCodeByType(bv_strType, intDepotID)

            If dt.Rows.Count > 0 Then

                equipmentCode.code = dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString()
                equipmentCode.id = dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString()
                equipmentCode.statusText = HttpContext.Current.Response.StatusDescription
                equipmentCode.stausCode = HttpContext.Current.Response.StatusCode
                equipmentCode.status = "Success"

                Return equipmentCode
                'pub_SetCallbackReturnValue("Code", dt.Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString())
                'pub_SetCallbackReturnValue("ID", dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString())
                'pub_SetCallbackStatus(True)
            Else
                'pub_SetCallbackStatus(False)
            End If

        Catch ex As Exception
            'pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                         MethodBase.GetCurrentMethod.Name, ex)
            equipmentCode.status = ex.Message
            'equipmentCode.id = dt.Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString()
            equipmentCode.statusText = HttpContext.Current.Response.StatusDescription
            equipmentCode.stausCode = HttpContext.Current.Response.StatusCode
            Return equipmentCode
        End Try

    End Function



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function ValidateEquipment(ByVal CustomerName As String, ByVal bv_strEquipmentNo As String, ByVal bv_userName As String, ByVal bv_intGridIndex As Integer) As GateinMobileModel
        Try
            gateInMobile.DepotID(bv_userName)
            Dim retreiveData As New RetreiveData
            Dim objGateIn As New Gatein
            Dim objCommon As New CommonData
            Dim strCustomer As String = String.Empty
            Dim blndsValid As Boolean
            Dim blnRental As Boolean
            Dim blnRentalEntry As Boolean
            Dim blnDuplicateEquipment As Boolean = True
            Dim blnPreadviceEquipment As Boolean = True
            Dim blnGTOT_BT As Boolean
            Dim bv_intDepotID As Integer = objCommon.GetDepotID()
            dsGateInData = objGateIn.GetGateInPreAdviceDetail(bv_intDepotID)
            dtGateinData = dsGateInData.Tables(GateinData._V_GATEIN)
            Dim intResultIndex() As System.Data.DataRow = dtGateinData.Select(String.Concat(GateinData.EQPMNT_NO, "='", bv_strEquipmentNo, "' "))
            Dim strExistEquipment As String = ""
            If intResultIndex.Length > 0 Then
                If dtGateinData.Rows.Count > bv_intGridIndex Then
                    If dtGateinData.Rows(bv_intGridIndex).RowState = Data.DataRowState.Deleted Then
                        strExistEquipment = String.Empty
                    ElseIf dtGateinData.Rows(bv_intGridIndex).RowState <> Data.DataRowState.Deleted Then
                        strExistEquipment = dtGateinData.Rows(bv_intGridIndex)(GateinData.EQPMNT_NO).ToString
                    End If
                End If

                If bv_strEquipmentNo = strExistEquipment Then
                    blndsValid = True
                    'pub_SetCallbackReturnValue("bNotExists", "false")
                    'pub_SetCallbackReturnValue("Customer", dtGateinData.Rows(bv_intGridIndex)(GateinData.CSTMR_ID).ToString)
                    'pub_SetCallbackStatus(True)
                    'Exit Sub
                Else
                    blndsValid = False
                End If
            Else
                blndsValid = True
            End If

            If blndsValid = False Then
                For Each drGateIn As DataRow In dtGateinData.Select(String.Concat(GateinData.EQPMNT_NO, "='", bv_strEquipmentNo, "'"))
                    strCustomer = CStr(drGateIn.Item(GateinData.CSTMR_CD))
                Next
            End If

            'Checking whether the entered code is available in database
            If blndsValid = True Then

                'blndsValid = objGateIn.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, retreiveData.GetDepotID(bv_userName), CInt(objCommon.GetDepotID()), strCustomer)
                blndsValid = objGateIn.pub_ValidateEquipmentNoByDepotID(bv_strEquipmentNo, bv_intDepotID, strCustomer)
            End If
            blnRental = objGateIn.GetRentalDetails(bv_strEquipmentNo, bv_intDepotID, strCustomer)
            blnRentalEntry = objGateIn.pub_GetRentalEntry(bv_strEquipmentNo, bv_intDepotID, blnGTOT_BT)

            'Code here to check whether this Equpt no is used for Gate In in any other Depot start here---
            ''   


            If blndsValid = True Then
                blnPreadviceEquipment = objGateIn.pub_ValidateEquipmentNoInPreAdvice(bv_strEquipmentNo, bv_intDepotID)
                If blnPreadviceEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot  

                    validationEquipment.validationStatus = "This Equipment " + bv_strEquipmentNo + " already exists for Pre-Advice in some other Depot."

                    Return validationEquipment
                    Exit Function
                    'pub_SetCallbackReturnValue("EquipmentNoInAnotherDepot", "false")
                End If

                blnDuplicateEquipment = objGateIn.pub_ValidateStatusOfEquipment(bv_strEquipmentNo, bv_intDepotID)
                If blnDuplicateEquipment = False Then
                    'Newly added if Equiment No already available in Preadvice in some other depot  

                    validationEquipment.validationStatus = "This Equipment " + bv_strEquipmentNo + " already is in Active State in some other Depot."
                    Return validationEquipment
                    Exit Function
                    'pub_SetCallbackReturnValue("StatusOfEquipment", "false")
                End If
            End If
            ''--Ends here



            If blndsValid = True Then

                If blnRental = False Then


                    If strCustomer <> CustomerName And strCustomer <> "" Then
                        validationEquipment.validationStatus = "This Equipment " + bv_strEquipmentNo + " already exists for Customer " + strCustomer + " in Rental"
                        Return validationEquipment
                        Exit Function
                    Else
                        Dim validationSuccessss As GateinMobileModel = GetSupplierDetails(bv_strEquipmentNo, bv_intDepotID)

                        Return validationSuccessss

                        Exit Function
                    End If


                Else

                    Dim validationSuccessss As GateinMobileModel = GetSupplierDetails(bv_strEquipmentNo, bv_intDepotID)

                    Return validationSuccessss

                    Exit Function


                End If

            Else
                If blnRental = True Then

                    If CustomerName <> "" And CustomerName IsNot Nothing Then


                        validationEquipment.validationStatus = "This Equipment " + bv_strEquipmentNo + " already exists for Customer " + strCustomer + ""
                        Return validationEquipment
                        Exit Function

                    Else
                        Dim validationSuccessss As GateinMobileModel = GetSupplierDetails(bv_strEquipmentNo, bv_intDepotID)

                        Return validationSuccessss

                        Exit Function

                    End If

                Else

                    If strCustomer <> CustomerName And strCustomer <> "" Then
                        validationEquipment.validationStatus = "This Equipment " + bv_strEquipmentNo + " already exists for Customer " + strCustomer + " in Rental"
                        Return validationEquipment
                    Else
                        Dim validationSuccessss As GateinMobileModel = GetSupplierDetails(bv_strEquipmentNo, bv_intDepotID)

                        Return validationSuccessss

                        Exit Function
                    End If

                End If

            End If
            'pub_SetCallbackStatus(True)
        Catch ex As Exception
            Dim validationEquipment As New GateinMobileModel
            'pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            validationEquipment.validationStatus = ex.Message
            validationEquipment.statusText = HttpContext.Current.Response.StatusDescription
            validationEquipment.stauscode = HttpContext.Current.Response.StatusCode
            Return validationEquipment
        End Try
    End Function



    Public Function GetSupplierDetails(ByVal bv_strEquipmentNo As String, ByVal intDepotId As String) As GateinMobileModel

        ' Dim validationEquipment As New GateinMobileModel
        Dim objCommonData As New CommonData
        Dim dsSupplierDetails As New GateinDataSet
        Dim objGatein As New Gatein
        'Dim intDepotId As Integer = CInt(objCommonData.GetDepotID())
        dsSupplierDetails = objGatein.pub_GetSupplierDetails(bv_strEquipmentNo, intDepotId)
        If dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows.Count > 0 Then

            validationEquipment.EquipmentTypeId = dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString
            validationEquipment.EquipmentTypeCode = dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_TYP_CD).ToString
            validationEquipment.EquipmentCodeId = dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_TYP_ID).ToString
            validationEquipment.EquipmentCode = dsSupplierDetails.Tables(GateinData._V_GATEOUT).Rows(0).Item(GateinData.EQPMNT_CD_CD).ToString
        Else
            validationEquipment.EquipmentTypeId = "".ToString()
            validationEquipment.EquipmentTypeCode = "".ToString()
            validationEquipment.EquipmentCodeId = "".ToString()
            validationEquipment.EquipmentCode = "".ToString()
        End If

        validationEquipment.validationStatus = "Success"

        Return validationEquipment

    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)> _
    Public Function pvt_ValidatePreviousActivityDate(ByVal bv_strEquipmentNo As String, ByVal bv_strEventDate As String, ByVal bv_userName As String) As String
        Try


            gateInMobile.DepotID(bv_userName)
            Dim blnDateValid As Boolean = False
            Dim dtPreviousDate As DateTime = Nothing
            Dim objGateIn As New Gatein
            Dim objCommonUI As New CommonUI
            Dim objCommon As New CommonData
            Dim dsCommonUI As New CommonUIDataSet

            If bv_strEquipmentNo <> Nothing And bv_strEventDate <> Nothing Then
                blnDateValid = objCommonUI.pub_GetEquipmentPreviousActivityDate(bv_strEquipmentNo, _
                                                                         CDate(bv_strEventDate), _
                                                                         dtPreviousDate, _
                                                                         "Gate In", _
                                                                         CInt(objCommon.GetDepotID()))
                If blnDateValid = True Then

                    validationEquipment.validationStatus = "Equipment's Activity Date Should be greater than or equal to Previous Activity Date" + dtPreviousDate + ""
                    Return New JavaScriptSerializer().Serialize(validationEquipment)
                    Exit Function
                    'pub_SetCallbackReturnValue("Error", String.Concat("Equipment's Activity Date Should be greater than or equal to Previous Activity Date (", dtPreviousDate.ToString("dd-MMM-yyyy"), ")"))
                End If
            End If

            validationEquipment.validationStatus = "Success"
            Return New JavaScriptSerializer().Serialize(validationEquipment)
            Exit Function
            'pub_SetCallbackStatus(True)
            'pub_SetCallbackReturnValue("Error", "") 'Added By Sakthivel on 20 OCT 2014 for Date Issue in UIG
        Catch ex As Exception
            'pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            validationEquipment.validationStatus = ex.Message
            validationEquipment.statusText = HttpContext.Current.Response.StatusDescription
            validationEquipment.stauscode = HttpContext.Current.Response.StatusCode

            Return New JavaScriptSerializer().Serialize(validationEquipment)
            Exit Function
        End Try
    End Function





    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function Update(ByVal GTN_ID As String, ByVal CSTMR_ID As String, ByVal CSTMR_CD As String, ByVal EQPMNT_NO As String, ByVal EQPMNT_TYP_ID As String,
                           ByVal EQPMNT_TYP_CD As String, ByVal EQPMNT_CD_ID As String, ByVal EQPMNT_CD_CD As String, ByVal YRD_LCTN As String,
                           ByVal GTN_DT As String, ByVal GTN_TM As String, ByVal PRDCT_ID As String, ByVal PRDCT_CD As String, ByVal EIR_NO As String, ByVal VHCL_NO As String,
                           ByVal TRNSPRTR_CD As String, ByVal HTNG_BT As String, ByVal RMRKS_VC As String, ByVal CHECKED As String, ByVal PRDCT_DSCRPTN_VC As String,
                           ByVal RNTL_BT As String, ByVal hfc As ArrayOfFileParams, ByVal RepairEstimateId As String,
                           ByVal IfAttchment As String, ByVal UserName As String, ByVal Mode As String,
                           ByVal EIMNFCTR_DT As String, ByVal EITR_WGHT_NC As String,
                                        ByVal EIGRSS_WGHT_NC As String, ByVal EICPCTY_NC As String, ByVal EILST_SRVYR_NM As String, ByVal EILST_TST_DT As String,
                                        ByVal EILST_TST_TYP_ID As String, ByVal EINXT_TST_DT As String, ByVal EINXT_TST_TYP_ID As String, ByVal EIRMRKS_VC As String,
                                        ByVal EIACTV_BT As String, ByVal EIRNTL_BT As String, ByVal EIAttachment As String, ByVal EIHasChanges As String, ByVal PageName As String, ByVal GateinTransactionNo As String) As Update

        Dim updte As New Update
        Try

            Dim strLockingRecords As String = String.Empty
            Dim gateInMobile As New GateinMobile_C


            Dim retn As String = gateInMobile.Update(GTN_ID, CSTMR_ID, CSTMR_CD, EQPMNT_NO, EQPMNT_TYP_ID,
                            EQPMNT_TYP_CD, EQPMNT_CD_ID, EQPMNT_CD_CD, YRD_LCTN,
                            GTN_DT, GTN_TM, PRDCT_ID, PRDCT_CD, EIR_NO, VHCL_NO,
                            TRNSPRTR_CD, HTNG_BT, RMRKS_VC, CHECKED,
                            PRDCT_DSCRPTN_VC, RNTL_BT, hfc, RepairEstimateId, IfAttchment, UserName, Mode,
                            EIMNFCTR_DT, EITR_WGHT_NC, EIGRSS_WGHT_NC, EICPCTY_NC, EILST_SRVYR_NM, EILST_TST_DT,
                            EILST_TST_TYP_ID, EINXT_TST_DT, EINXT_TST_TYP_ID, EIRMRKS_VC,
                            EIACTV_BT, EIRNTL_BT, EIAttachment, EIHasChanges, PageName, GateinTransactionNo)


            'If strLockingRecords <> Nothing Then
            '    Dim intCheckedRows As Integer = 0
            '    Dim strSplitLockingRecords() As String = Nothing
            '    Dim intSplitActivity As Integer = 0
            '    intCheckedRows = CInt(dsGateInData.Tables(GateinData._V_GATEIN).Compute(String.Concat("COUNT(", GateinData.GTN_ID, ")"), String.Concat(GateinData.CHECKED, "= 'True'")))
            '    strSplitLockingRecords = strLockingRecords.Split(CChar(","))
            '    intSplitActivity = strSplitLockingRecords.Length

            '    If intCheckedRows = intSplitActivity Then
            '        If strLockingRecords <> "" And strLockingRecords <> Nothing Then

            '            updte.equipmentUpdate = "The following equipment(s) are already submitted. " + strLockingRecords + ""
            '            Return New JavaScriptSerializer().Serialize(updte)
            '            Exit Function
            '            'The following equipment(s) are already submitted. " + strLockingRecords
            '        End If

            '        'pub_SetCallbackReturnValue("LockRecordBit", "true")
            '    Else

            '        'Gate In Creation : Equipment(s) Updated Successfully.
            '    End If
            '    'pub_SetCallbackReturnValue("LockRecord", strLockingRecords)



            'End If

            If retn = "Success" Then


                updte.equipmentUpdate = "Success"
                updte.statusText = HttpContext.Current.Response.StatusDescription
                updte.stausCode = HttpContext.Current.Response.SubStatusCode

                Return updte
                Exit Function

            ElseIf retn = "GateIn Not Updated" Then


                updte.equipmentUpdate = "GateIn Not Updated"
                updte.statusText = HttpContext.Current.Response.StatusDescription
                updte.stausCode = HttpContext.Current.Response.SubStatusCode

                Return updte
                Exit Function

            ElseIf retn = "EINotUpdated" Then


                updte.equipmentUpdate = "EINotUpdated"
                updte.statusText = HttpContext.Current.Response.StatusDescription
                updte.stausCode = HttpContext.Current.Response.SubStatusCode

                Return updte
                Exit Function

            End If

        Catch ex As Exception
            'pub_SetCallbackStatus(False)
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            updte.equipmentUpdate = ex.Message
            updte.statusText = HttpContext.Current.Response.StatusDescription
            updte.stausCode = HttpContext.Current.Response.StatusCode

            Return updte
            Exit Function
        End Try
    End Function



    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function pvt_GIlockData(ByVal bv_strEquipmentNo As String, ByVal strMode As String, ByVal bv_userName As String) As pvt_GIlockData

        gateInMobile.DepotID(bv_userName)
        Dim objCommonData As New CommonData
        Dim objCommonUI As New CommonUI
        Dim strEquipmentStatus As String = ""
        Dim strCurrentEquipmentStatus As String = ""
        Dim strCurrentSessionId As String = String.Empty
        Dim strCurrentUserName As String = String.Empty
        Dim strCurrentIpAddress As String = String.Empty
        Dim strSessionId As String = String.Empty
        Dim strUserName As String = String.Empty
        Dim strIpAddress As String = String.Empty
        Dim blnLockData As Boolean = False
        Dim intDepotID As Integer = CInt(objCommonData.GetDepotID())
        Dim strErrorMessage As String = ""
        Dim strActivity As String = ""
        Dim objCommonConfig As New ConfigSetting()
        'strCurrentSessionId = objCommonData.GetSessionID()
        'strCurrentUserName = objCommonData.GetCurrentUserName()
        ' strCurrentIpAddress = GetClientIPAddress()
        ' strMode = CType(RetrieveData(GateInMode), String)
        Dim str_051GWSBit As String
        Dim bln_051GWSBit_Key As Boolean
        str_051GWSBit = objCommonConfig.pub_GetConfigSingleValue("051", intDepotID)
        bln_051GWSBit_Key = objCommonConfig.IsKeyExists

        If strMode = "edit" Then
            strEquipmentStatus = "IND"
        Else
            strEquipmentStatus = "OUT"
        End If
        If bln_051GWSBit_Key Then
            If str_051GWSBit.ToLower = "true" Then
                strEquipmentStatus = "INS/STR"
            End If
        End If

        Dim bv_strCheckBitFlag As String = "True"

        Dim pvtGIlockData As New pvt_GIlockData

        If bv_strCheckBitFlag.ToUpper = "TRUE" Then
            strCurrentEquipmentStatus = objCommonUI.pub_GetEquipmentStaus(bv_strEquipmentNo, intDepotID)
            'blnLockData = objCommonData.StoreLockData(GateinData.EQPMNT_NO, bv_strEquipmentNo, strCurrentUserName, strCurrentSessionId, "Gate In", strCurrentIpAddress, True)
            If strCurrentEquipmentStatus <> Nothing AndAlso Not strEquipmentStatus.Contains(strCurrentEquipmentStatus) Then
                strErrorMessage = "GateIn already done for this equipment."
                pvtGIlockData.lockDataValidation = strErrorMessage
                Return pvtGIlockData

                Exit Function
            Else


            End If
        End If

        pvtGIlockData.lockDataValidation = "Success"
        pvtGIlockData.statusText = HttpContext.Current.Response.StatusDescription
        pvtGIlockData.stausCode = HttpContext.Current.Response.StatusCode


        Return pvtGIlockData

    End Function


    <WebMethod(enableSession:=True)> _
    <ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    Public Function pvt_validateRentalEntry(ByVal EquipmentNo As String, ByVal UserName As String) As DateValidate
        Dim validateRental As New DateValidate
        Try
            gateInMobile.DepotID(UserName)
            Dim objGatein As New Gatein

            Dim objCommonData As New CommonData
            Dim intDepotId As Integer = CInt(objCommonData.GetDepotID())
            Dim blnRentalEntry As Boolean = False
            Dim blnGTOT_BT As Boolean = False
            blnRentalEntry = objGatein.pub_GetRentalEntry(EquipmentNo, intDepotId, blnGTOT_BT)
            If blnRentalEntry = False Or blnGTOT_BT = False Then
                validateRental.statusText = "Equipment " + EquipmentNo + " cannot be marked for Rental Gate In, as there is no Rental Entry / Rental Gate Out created"

            Else
                validateRental.statusText = "Success"
            End If

            Return validateRental

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            validateRental.statusText = ex.Message
            Return validateRental
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

        ds = New DataSet()
        Select Case Mode
            Case strNew
                If filterCondition = "Equals" Then

                    query = "SELECT " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else
                    query = "SELECT distinct " + filterType + " FROM V_PRE_ADVICE V WHERE DPT_ID='" + objCommonData.GetDepotID() + "' AND GI_TRNSCTN_NO IS NULL and " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                End If

            Case strEdit

                If filterCondition = "Equals" Then

                    query = "SELECT " + filterType + " FROM V_GATEIN AS VG WHERE GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Gate In' AND DPT_ID='" + objCommonData.GetDepotID() + "')) AND DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_NO NOT IN(SELECT EQPMNT_NO FROM RENTAL_CHARGE WHERE OFF_HR_BLLNG_FLG='Y' AND RNTL_RFRNC_NO=VG.RNTL_RFRNC_NO) and " + filterType + "='" + filterValue + "'"

                    ds = conn.connection(query)
                    'ds.Tables(Table)
                    'ds.Tables()
                    'Return ds


                Else
                    query = "SELECT distinct " + filterType + " FROM V_GATEIN AS VG WHERE GI_TRNSCTN_NO IN (SELECT GI_TRNSCTN_NO FROM ACTIVITY_STATUS WHERE EQPMNT_STTS_ID IN (SELECT DEFAULT_STATUS_ID FROM WF_ACTIVITY WHERE WF_ACTIVITY_NAME ='Gate In' AND DPT_ID='" + objCommonData.GetDepotID() + "')) AND DPT_ID='" + objCommonData.GetDepotID() + "' AND EQPMNT_NO NOT IN(SELECT EQPMNT_NO FROM RENTAL_CHARGE WHERE OFF_HR_BLLNG_FLG='Y' AND RNTL_RFRNC_NO=VG.RNTL_RFRNC_NO) and " + filterType + " " + filterNewConditon + " '%" + filterValue + "%'"

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
    Public Function SearchList(ByVal UserName As String, ByVal SearchValues As SearchValues, ByVal filterType As String, ByVal Mode As String) As ListModel


        Try



            Dim ouput As GateinDataSet
            'Dim ouput1 As DataRow

            ouput = gateInMobile.PreAdvice(UserName, Mode)

            Dim objCommon As New CommonData
            'Dim ouput1 As DataRow


            Dim intDepotID As Integer = objCommon.GetDepotID()





            For Each dd In SearchValues.SearchValues
                '  Dim dr1() As DataRow = ouput.Tables(GateinData._V_GATEIN).Select(String.Concat(filterType, "='", dd.values, "'"))
                For Each dr1 As DataRow In ouput.Tables(GateinData._V_GATEIN).Select(String.Concat(filterType, "='", dd.values, "'"))
                    ' For Each f As DataRow In dsGateInData.Tables(GateinData._V_GATEIN).Rows

                    'var milli = "/Date(1245398693390)/".replace(/\/Date\((-?\d+)\)\//, '$1');
                    'var d = new Date(parseInt(milli));



                    'h.AGNT_CD = f.Item(GateinData.AGNT_CD).ToString()
                    'h.GTN_DT = f.Item(GateinData.GTN_DT)
                    Dim h As New GateInss

                    If Mode = "new" Then


                        h.GTN_ID = dr1.Item(GateinData.GTN_ID).ToString()
                        h.GTN_CD = dr1.Item(GateinData.GTN_CD).ToString()
                        h.CSTMR_ID = dr1.Item(GateinData.CSTMR_ID).ToString()
                        h.CSTMR_CD = dr1.Item(GateinData.CSTMR_CD).ToString()
                        h.EQPMNT_NO = dr1.Item(GateinData.EQPMNT_NO).ToString()
                        h.EQPMNT_TYP_ID = dr1.Item(GateinData.EQPMNT_TYP_ID).ToString()
                        h.EQPMNT_TYP_CD = dr1.Item(GateinData.EQPMNT_TYP_CD).ToString()
                        h.EQPMNT_CD_ID = dr1.Item(GateinData.EQPMNT_CD_ID).ToString()
                        h.EQPMNT_CD_CD = dr1.Item(GateinData.EQPMNT_CD_CD).ToString()
                        h.EQPMNT_STTS_ID = dr1.Item(GateinData.EQPMNT_STTS_ID).ToString()
                        h.EQPMNT_STTS_CD = dr1.Item(GateinData.EQPMNT_STTS_CD).ToString()
                        h.YRD_LCTN = dr1.Item(GateinData.YRD_LCTN).ToString()
                        h.GTN_DT = dr1.Item(GateinData.GTN_DT).ToString()
                        h.GTN_TM = dr1.Item(GateinData.GTN_TM).ToString()
                        h.PRDCT_ID = dr1.Item(GateinData.PRDCT_ID).ToString()
                        h.PRDCT_CD = dr1.Item(GateinData.PRDCT_CD).ToString()
                        h.EIR_NO = dr1.Item(GateinData.EIR_NO).ToString()
                        h.VHCL_NO = dr1.Item(GateinData.VHCL_NO).ToString()
                        h.TRNSPRTR_CD = dr1.Item(GateinData.TRNSPRTR_CD).ToString()
                        h.HTNG_BT = dr1.Item(GateinData.HTNG_BT).ToString()
                        h.RMRKS_VC = dr1.Item(GateinData.RMRKS_VC).ToString()
                        h.GI_TRNSCTN_NO = dr1.Item(GateinData.GI_TRNSCTN_NO).ToString()
                        h.DPT_ID = dr1.Item(GateinData.DPT_ID).ToString()
                        h.CRTD_BY = dr1.Item(GateinData.CRTD_BY).ToString()
                        h.CRTD_DT = dr1.Item(GateinData.CRTD_DT).ToString()
                        h.MDFD_BY = dr1.Item(GateinData.MDFD_BY).ToString()
                        h.MDFD_DT = dr1.Item(GateinData.MDFD_DT).ToString()
                        'h.FRM_PRE_ADVC_BT).ToString() = FRM_PRE_ADVC_BT
                        'h.EQUPMNT_STTS_DSCRPTN_VC).ToString() = EQUPMNT_STTS_DSCRPTN_VC
                        h.CHECKED = dr1.Item(GateinData.CHECKED).ToString()
                        'h.MD_OF_PYMNT).ToString() = MD_OF_PYMNT
                        h.BLLNG_TYP = dr1.Item(GateinData.BLLNG_TYP).ToString()
                        h.CSTMR_NAM = dr1.Item(GateinData.CSTMR_NAM).ToString()
                        h.ALLOW_UPDATE = dr1.Item(GateinData.ALLOW_UPDATE).ToString()
                        h.PR_ADVC_BT = dr1.Item(GateinData.PR_ADVC_BT).ToString()
                        h.HTNG_EDIT = dr1.Item(GateinData.HTNG_EDIT).ToString()
                        h.PRDCT_DSCRPTN_VC = dr1.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
                        h.RNTL_BT = dr1.Item(GateinData.RNTL_BT).ToString()
                        h.RNTL_RFRNC_NO = dr1.Item(GateinData.RNTL_RFRNC_NO).ToString()
                        h.ALLOW_RENTAL = dr1.Item(GateinData.ALLOW_RENTAL).ToString()
                        h.COUNT_ATTACH = dr1.Item(GateinData.COUNT_ATTACH).ToString()
                        h.RDL_ATH = dr1.Item(GateinData.RDL_ATH).ToString()
                        h.GRD_ID = dr1.Item(GateinData.GRD_ID).ToString()
                        h.AGNT_ID = dr1.Item(GateinData.AGNT_ID).ToString()
                        h.CNSGNE = dr1.Item(GateinData.CNSGNE).ToString()
                        h.BLL_CD = dr1.Item(GateinData.BLL_CD).ToString()
                        h.BLL_ID = dr1.Item(GateinData.BLL_ID).ToString()
                        h.AGNT_CD = dr1.Item(GateinData.AGNT_CD).ToString()
                        h.GRD_CD = dr1.Item(GateinData.GRD_CD).ToString()
                        h.CSC_VLDTY = dr1.Item(GateinData.CSC_VLDTY).ToString()
                        h.MNFCTR_DT = dr1.Item(GateinData.MNFCTR_DT).ToString()
                        h.PR_ADVC_ID = dr1.Item(GateinData.PR_ADVC_ID).ToString()
                        h.GTN_BIN = dr1.Item(GateinData.GTN_BIN).ToString()
                        h.PR_ADVC_CD = dr1.Item(GateinData.PR_ADVC_CD).ToString()
                        h.EQPMNT_SZ_ID = dr1.Item(GateinData.EQPMNT_SZ_ID).ToString()
                        h.EQPMNT_SZ_CD = dr1.Item(GateinData.EQPMNT_SZ_CD).ToString()
                        h.CLNNG_RFRNC_NO = dr1.Item(GateinData.CLNNG_RFRNC_NO).ToString()


                    ElseIf Mode = "edit" Then




                        h.GTN_ID = dr1.Item(GateinData.GTN_ID).ToString()
                        h.GTN_CD = dr1.Item(GateinData.GTN_CD).ToString()
                        h.CSTMR_ID = dr1.Item(GateinData.CSTMR_ID).ToString()
                        h.CSTMR_CD = dr1.Item(GateinData.CSTMR_CD).ToString()
                        h.EQPMNT_NO = dr1.Item(GateinData.EQPMNT_NO).ToString()
                        h.EQPMNT_TYP_ID = dr1.Item(GateinData.EQPMNT_TYP_ID).ToString()
                        h.EQPMNT_TYP_CD = dr1.Item(GateinData.EQPMNT_TYP_CD).ToString()
                        h.EQPMNT_CD_ID = dr1.Item(GateinData.EQPMNT_CD_ID).ToString()
                        h.EQPMNT_CD_CD = dr1.Item(GateinData.EQPMNT_CD_CD).ToString()
                        h.EQPMNT_STTS_ID = dr1.Item(GateinData.EQPMNT_STTS_ID).ToString()
                        h.EQPMNT_STTS_CD = dr1.Item(GateinData.EQPMNT_STTS_CD).ToString()
                        h.YRD_LCTN = dr1.Item(GateinData.YRD_LCTN).ToString()
                        h.GTN_DT = dr1.Item(GateinData.GTN_DT).ToString()
                        h.GTN_TM = dr1.Item(GateinData.GTN_TM).ToString()
                        h.PRDCT_ID = dr1.Item(GateinData.PRDCT_ID).ToString()
                        h.PRDCT_CD = dr1.Item(GateinData.PRDCT_CD).ToString()
                        h.EIR_NO = dr1.Item(GateinData.EIR_NO).ToString()
                        h.VHCL_NO = dr1.Item(GateinData.VHCL_NO).ToString()
                        h.TRNSPRTR_CD = dr1.Item(GateinData.TRNSPRTR_CD).ToString()
                        h.HTNG_BT = dr1.Item(GateinData.HTNG_BT).ToString()
                        h.RMRKS_VC = dr1.Item(GateinData.RMRKS_VC).ToString()
                        h.GI_TRNSCTN_NO = dr1.Item(GateinData.GI_TRNSCTN_NO).ToString()
                        h.DPT_ID = dr1.Item(GateinData.DPT_ID).ToString()
                        h.CRTD_BY = dr1.Item(GateinData.CRTD_BY).ToString()
                        h.CRTD_DT = dr1.Item(GateinData.CRTD_DT).ToString()
                        h.MDFD_BY = dr1.Item(GateinData.MDFD_BY).ToString()
                        h.MDFD_DT = dr1.Item(GateinData.MDFD_DT).ToString()
                        'h.FRM_PRE_ADVC_BT).ToString() = FRM_PRE_ADVC_BT
                        'h.EQUPMNT_STTS_DSCRPTN_VC).ToString() = EQUPMNT_STTS_DSCRPTN_VC
                        h.CHECKED = dr1.Item(GateinData.CHECKED).ToString()
                        'h.MD_OF_PYMNT).ToString() = MD_OF_PYMNT
                        h.BLLNG_TYP = dr1.Item(GateinData.BLLNG_TYP).ToString()
                        h.CSTMR_NAM = dr1.Item(GateinData.CSTMR_NAM).ToString()
                        h.ALLOW_UPDATE = dr1.Item(GateinData.ALLOW_UPDATE).ToString()
                        h.PR_ADVC_BT = dr1.Item(GateinData.PR_ADVC_BT).ToString()
                        h.HTNG_EDIT = dr1.Item(GateinData.HTNG_EDIT).ToString()
                        h.PRDCT_DSCRPTN_VC = dr1.Item(GateinData.PRDCT_DSCRPTN_VC).ToString()
                        h.RNTL_BT = dr1.Item(GateinData.RNTL_BT).ToString()
                        h.RNTL_RFRNC_NO = dr1.Item(GateinData.RNTL_RFRNC_NO).ToString()
                        h.ALLOW_RENTAL = dr1.Item(GateinData.ALLOW_RENTAL).ToString()
                        h.COUNT_ATTACH = dr1.Item(GateinData.COUNT_ATTACH).ToString()
                        h.RDL_ATH = dr1.Item(GateinData.RDL_ATH).ToString()
                        h.GRD_ID = dr1.Item(GateinData.GRD_ID).ToString()
                        h.AGNT_ID = dr1.Item(GateinData.AGNT_ID).ToString()
                        h.CNSGNE = dr1.Item(GateinData.CNSGNE).ToString()
                        h.BLL_CD = dr1.Item(GateinData.BLL_CD).ToString()
                        h.BLL_ID = dr1.Item(GateinData.BLL_ID).ToString()
                        h.AGNT_CD = dr1.Item(GateinData.AGNT_CD).ToString()
                        h.GRD_CD = dr1.Item(GateinData.GRD_CD).ToString()
                        h.CSC_VLDTY = dr1.Item(GateinData.CSC_VLDTY).ToString()
                        h.MNFCTR_DT = dr1.Item(GateinData.MNFCTR_DT).ToString()
                        h.PR_ADVC_ID = dr1.Item(GateinData.PR_ADVC_ID).ToString()

                    End If

                    Dim dtAttachment As New DataTable
                    Dim objGatein1 As New GateIns
                    Dim objTrans As New Transactions

                    dsGateInData.Tables(GateinData._ATTACHMENT).Clear()
                    dtAttachment = dsGateInData.Tables(GateinData._ATTACHMENT).Clone()
                    Dim attch1 As New ArrayList
                    Dim attch As New ArrayList

                    Select Case Mode
                        Case strNew

                            If dsGateInData.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                                ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                                dtAttachment = objGatein1.GetAttachmentByRepairEstimateId(intDepotID, "Pre-Advice", dr1.Item(GateinData.PR_ADVC_CD), objTrans).Tables(GateinData._ATTACHMENT)
                                dsGateInData.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                                'End If
                            End If
                            'h.GTN_BIN = f.Item(GateinData.GTN_BIN).ToString()
                            'h.PR_ADVC_CD = f.Item(GateinData.PR_ADVC_CD).ToString()
                            'h.EQPMNT_SZ_ID = f.Item(GateinData.EQPMNT_SZ_ID).ToString()
                            'h.EQPMNT_SZ_CD = f.Item(GateinData.EQPMNT_SZ_CD).ToString()
                            'h.CLNNG_RFRNC_NO = f.Item(GateinData.CLNNG_RFRNC_NO).ToString()


                            For Each dr As DataRow In ouput.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", dr1.Item(GateinData.PR_ADVC_CD), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))

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


                        Case strEdit

                            If dsGateInData.Tables(GateinData._ATTACHMENT).Rows.Count = 0 Then
                                ' If bv_strEstimationId <> Nothing AndAlso bv_strEstimationId <> "" Then
                                dtAttachment = objGatein1.GetAttachmentByRepairEstimateId(intDepotID, "GateIn", dr1.Item(GateinData.GTN_ID), objTrans).Tables(GateinData._ATTACHMENT)
                                dsGateInData.Tables(GateinData._ATTACHMENT).Merge(dtAttachment)
                                'End If
                            End If
                            'h.GTN_BIN = f.Item(GateinData.GTN_BIN).ToString()
                            'h.PR_ADVC_CD = f.Item(GateinData.PR_ADVC_CD).ToString()
                            'h.EQPMNT_SZ_ID = f.Item(GateinData.EQPMNT_SZ_ID).ToString()
                            'h.EQPMNT_SZ_CD = f.Item(GateinData.EQPMNT_SZ_CD).ToString()
                            'h.CLNNG_RFRNC_NO = f.Item(GateinData.CLNNG_RFRNC_NO).ToString()


                            For Each dr As DataRow In ouput.Tables(RepairEstimateData._ATTACHMENT).Select(String.Concat(RepairEstimateData.RPR_ESTMT_ID, "=", dr1.Item(GateinData.GTN_ID), " AND ", RepairEstimateData.ERR_FLG, " <> ", "True"))

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

                    End Select


                    If attch.Count > 0 Then
                        h.attchement = attch
                    Else
                        h.attchement = attch1
                    End If

                    hlist.Add(h)

                Next
            Next



            h1.status = "Success"
            h1.stauscode = HttpContext.Current.Response.StatusCode
            h1.statusText = HttpContext.Current.Response.StatusDescription
            h1.ListGateInss = hlist






            'h.AGNT_CD = ouput1.i
            'h.age = 26

            Return h1

            'End If

            'Dim conditionfial As New COnditionFail

            'conditionfial.confial = "Session Expired"
            'Return New JavaScriptSerializer().Serialize(conditionfial)

        Catch ex As Exception

            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
            h1.stauscode = HttpContext.Current.Response.StatusCode
            h1.statusText = HttpContext.Current.Response.StatusDescription
            h1.status = ex.Message
            Return h1

        End Try

    End Function











End Class