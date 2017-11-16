Option Strict On
Imports Microsoft.VisualBasic

#Region "AlertData"
Public Class AlertData
#Region "Declaration Part.."
    'Data Tables..
    Public Const _ALERTSETTING As String = "ALERTSETTING"
    Public Const _ALERTTEMPLATE As String = "ALERTTEMPLATE"
    Public Const _PHALERT As String = "PHALERT"
    Public Const _PQALERT As String = "PQALERT"

    'Data Columns..

    'Table Name: AlertSetting
    Public Const ACTVTY_ID As String = "ACTVTY_ID"
    Public Const ALRT_TMPLT_ID As String = "ALRT_TMPLT_ID"
    Public Const ALRT_RF_FLD_VCR As String = "ALRT_RF_FLD_VCR"
    Public Const ALRT_QRY_VCR As String = "ALRT_QRY_VCR"
    Public Const ALRT_TP_FLD As String = "ALRT_TP_FLD"

    'Table Name: AlertTemplate
    Public Const ALRT_TMPLT_NAM As String = "ALRT_TMPLT_NAM"
    Public Const ALRT_FL_PTH As String = "ALRT_FL_PTH"

    'Table Name: PHAlert
    Public Const ALRT_BIN As String = "ALRT_BIN"
    Public Const TRNSCTN_ID As String = "TRNSCTN_ID"
    Public Const ORGNSTN_ID As String = "ORGNSTN_ID"
    Public Const ALRT_SBJCT_VCR As String = "ALRT_SBJCT_VCR"
    Public Const ALRT_CNTNT_XML As String = "ALRT_CNTNT_XML"
    Public Const ALRT_TO As String = "ALRT_TO"
    Public Const ALRT_CC As String = "ALRT_CC"
    Public Const ALRT_STTS_BT As String = "ALRT_STTS_BT"
    Public Const ALRT_LST_SND_DT As String = "ALRT_LST_SND_DT"
    Public Const CRTD_BY As String = "CRTD_BY"
    Public Const CRTD_DT As String = "CRTD_DT"
    Public Const ALRT_DE_DT As String = "ALRT_DE_DT"
    'Table Name: PQAlert
#End Region

End Class

#End Region
