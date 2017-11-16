Imports Microsoft.VisualBasic

Public Class EquipmentHistoryModel


    Public Property TRCKNG_ID() As String
    Public Property CSTMR_ID() As String
    Public Property CSTMR_CD() As String
    Public Property EQPMNT_NO() As String
    Public Property ACTVTY_NAM() As String
    Public Property EQPMNT_STTS_ID() As String
    Public Property EQPMNT_STTS_CD() As String
    Public Property ACTVTY_NO() As String
    Public Property ACTVTY_DT() As String
    Public Property ACTVTY_RMRKS() As String
    Public Property GI_TRNSCTN_NO() As String
    Public Property INVCNG_PRTY_ID() As String
    Public Property INVCNG_PRTY_CD() As String
    Public Property PRDCT_ID() As String
    Public Property EIR_NO() As String
    Public Property RF_NO() As String
    Public Property PRDCT_DSCRPTN_VC() As String
    Public Property INVC_GNRTN() As String
    Public Property CRTD_BY() As String
    Public Property CRTD_DT() As String
    Public Property CNCLD_BY() As String
    Public Property CNCLD_DT() As String
    Public Property ADT_RMRKS() As String
    Public Property DPT_ID() As String
    Public Property YRD_LCTN() As String
    Public Property EQPMNT_TYP_ID() As String
    Public Property EQPMNT_TYP_CD() As String
    Public Property EQPMNT_CD_ID() As String
    Public Property EQPMNT_CD_CD() As String
    Public Property RMRKS_VC() As String
    Public Property PRDCT_CD() As String
    Public Property RNTL_CSTMR_ID() As String
    Public Property RNTL_RFRNC_NO() As String
    Public Property AGNT_CD() As String
    Public Property STTS_ID() As String
    Public Property CSTMR_NAM() As String
    Public Property ADDTNL_CLNNG_BT() As String
    Public Property DPT_CD() As String

    
   

End Class



Public Class EqupmHisList

    Public Property List() As ArrayList
    Public Property strEqType() As String
    Public Property strEqCode() As String
    Public Property status() As String

End Class


Public Class EHEqupValidate


    Public Property status() As String
End Class


Public Class EHDelete


    Public Property status() As String
End Class
