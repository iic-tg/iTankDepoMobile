Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework
#Region "Audits"
Public Class Audits
#Region "Declaration Part"
    Dim objData As DataObjects
    Private Const AuditDetailsSelectQuery As String = "SELECT EQPMNT_NO,RFRNC_NO,ACTVTY_NAM,ACTN_VC,CNCLD_BY,CNCLD_DT,ADT_RMRKS,OLD_VL,NEW_VL FROM V_AUDIT WHERE DPT_ID=@DPT_ID ORDER BY CNCLD_DT DESC"

    Private ds As AuditDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New AuditDataSet
    End Sub

#End Region
#Region "pub_GetAuditDetails() "

    Public Function pub_GetAuditDetails(ByVal bv_DepotID As Int64) As AuditDataSet
        Try
            objData = New DataObjects(AuditDetailsSelectQuery, AuditData.DPT_ID, CStr(bv_DepotID))
            objData.Fill(CType(ds, DataSet), AuditData._V_AUDIT)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
#End Region
