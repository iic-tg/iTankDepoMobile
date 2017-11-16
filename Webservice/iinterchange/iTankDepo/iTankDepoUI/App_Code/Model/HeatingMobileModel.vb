Imports Microsoft.VisualBasic

Public Class HeatingMobileModel

    Public Property EQPMNT_NO() As String
    Public Property CSTMR_CD() As String
    Public Property EQPMNT_TYP_CD() As String
    Public Property PRDCT_DSCRPTN_VC() As String
    Public Property GTN_DT() As String
    Public Property YRD_LCTN() As String
    Public Property GI_TRNSCTN_NO() As String
    Public Property HTNG_STRT_DT() As String
    Public Property HTNG_STRT_TM() As String
    Public Property HTNG_END_DT() As String
    Public Property HTNG_END_TM() As String
    Public Property HTNG_TMPRTR() As String
    Public Property TTL_HTN_PRD() As String
    Public Property MIN_HTNG_RT_NC() As String
    Public Property HRLY_CHRG_NC() As String
    Public Property CSTMR_CRRNCY_CD() As String
    Public Property MIN_HTNG_PRD_NC() As String
    Public Property TTL_RT_NC() As String


End Class


Public Class ReturnHeating
    Public Property HeatingArray() As ArrayList
    Public Property Status() As String
End Class


Public Class HeatingPeriod

    Public Property TotalHeatingPeriod() As String
    Public Property Status() As String

End Class


Public Class HeatingTotalRates

    Public Property TotalHeatingRate() As String
    Public Property Status() As String

End Class


Public Class HeatingUpdateStatus

    ' Public Property TotalHeatingRate() As String
    Public Property Status() As String

End Class