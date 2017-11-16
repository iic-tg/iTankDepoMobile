Imports Microsoft.VisualBasic

Public Class GateInss

    Public Property GTN_ID() As String
    Public Property GTN_CD() As String
    Public Property CSTMR_ID() As String
    Public Property CSTMR_CD() As String
    'Public Property CSTMR_NAM() As Char
    Public Property EQPMNT_NO() As String
    Public Property EQPMNT_TYP_ID() As String
    Public Property EQPMNT_TYP_CD() As String
    Public Property EQPMNT_CD_ID() As String
    Public Property EQPMNT_CD_CD() As String
    Public Property EQPMNT_STTS_ID() As String
    Public Property EQPMNT_STTS_CD() As String
    'Public Property EQPMNT_STTS_DSCRPTN_VC() As Char
    Public Property YRD_LCTN() As String
    Public Property GTN_DT() As String
    Public Property GTN_TM() As String
    Public Property PRDCT_ID() As String
    Public Property PRDCT_CD() As String
    ''''''''''''''''''Public Property PRDCT_DSCRPTN_VC() As String
    Public Property EIR_NO() As String
    Public Property VHCL_NO() As String
    Public Property TRNSPRTR_CD() As String
    Public Property HTNG_BT() As String
    Public Property RMRKS_VC() As String

    Public Property GI_TRNSCTN_NO() As String
    'Public Property RNTL_RFRNC_NO() As Char
    'Public Property GTOT_BT() As Integer

    Public Property DPT_ID() As String
    Public Property CRTD_BY() As String
    Public Property CRTD_DT() As String
    Public Property MDFD_BY() As String
    Public Property MDFD_DT() As String

    Public Property FRM_PRE_ADVC_BT() As String
    Public Property EQUPMNT_STTS_DSCRPTN_VC() As String
    Public Property CHECKED() As String
    Public Property MD_OF_PYMNT() As String
    Public Property BLLNG_TYP() As String
    Public Property CSTMR_NAM() As String
    Public Property ALLOW_UPDATE() As String
    Public Property PR_ADVC_BT() As String
    Public Property HTNG_EDIT() As String
    Public Property PRDCT_DSCRPTN_VC() As String
    Public Property RNTL_BT() As String
    Public Property RNTL_RFRNC_NO() As String
    Public Property ALLOW_RENTAL() As String
    Public Property COUNT_ATTACH() As String

    Public Property RDL_ATH() As String

    Public Property GRD_ID() As String
    Public Property AGNT_ID() As String
    Public Property CNSGNE() As String
    Public Property BLL_CD() As String
    Public Property BLL_ID() As String
    Public Property AGNT_CD() As String

    Public Property GRD_CD() As String
    Public Property CSC_VLDTY() As String
    Public Property MNFCTR_DT() As String
    Public Property PR_ADVC_ID() As String
    Public Property GTN_BIN() As String
    Public Property PR_ADVC_CD() As String
    Public Property EQPMNT_SZ_ID() As String
    Public Property EQPMNT_SZ_CD() As String
    Public Property CLNNG_RFRNC_NO() As String

    Public Property attchement() As ArrayList

   
    'Public Property AGNT_CD() As String



    'Public Property GTN_DT() As DateTime?

    'Public Property CRTD_DT() As Date?
    'Public Property MDFD_DT() As DateTime?
    'Public Property AGNT_CD() As String

   
    




End Class


Public Class attchementPro
    Public Property fileName() As String
    Public Property imageUrl() As String
    Public Property attchPath() As String
    Public Property attchId() As String
End Class




Public Class ListModel

    Public Property ListGateInss() As ArrayList
    Public Property stauscode() As Integer
    Public Property statusText() As String
    Public Property status() As String


End Class

Public Class COnditionFail

    Public Property confial() As String
End Class


Public Class pvt_GIlockData

    Public Property lockDataValidation() As String
    Public Property stausCode() As Integer
    Public Property statusText() As String

End Class

Public Class Update

    Public Property equipmentUpdate() As String
    Public Property stausCode() As Integer
    Public Property statusText() As String

End Class

Public Class pvt_ValidateGateINAttachment

    Public Property validateGateINAttachment() As String
    Public Property stausCode() As Integer
    Public Property status() As String
    Public Property statusText() As String

End Class

Public Class pvt_GetEquipmentCode

    Public Property code() As String
    Public Property id() As String
    Public Property stausCode() As Integer
    Public Property status() As String
    Public Property statusText() As String

End Class


Public Class FilteredValues

    Public Property Values() As String
    

End Class


Public Class SearchValues

    Public Property SearchValues() As List(Of Values)


End Class




Public Class Values

    Public Property values As String


End Class


Public Class FileParams

    Public Property FileName() As String
    Public Property ContentLength() As String
    Public Property base64imageString() As String


End Class



Public Class ArrayOfFileParams

    Public Property ArrayOfFileParams() As List(Of FileParams)
    

End Class


Public Class DefaultValues

    Public Property EquipmentStatus() As String
    Public Property YardLocation() As String
    Public Property EquipmentType() As String
    Public Property EquipmentCode() As String
    Public Property Status() As String


End Class

