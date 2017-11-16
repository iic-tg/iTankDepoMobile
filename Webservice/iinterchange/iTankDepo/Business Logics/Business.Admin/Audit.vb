Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.GatewayFramework
Imports System.Xml
Imports System.Configuration
Imports System.IO

<ServiceContract()> _
Public Class Audit
#Region "pub_GetAuditDetails()"

    <OperationContract()> _
    Public Function pub_GetAuditDetails(ByVal intDepotID As Integer) As AuditDataSet

        Try
            Dim dsAuditData As AuditDataSet
            Dim objAudit As New Audits
            'Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, AuditData.DPT_ID))
            dsAuditData = objAudit.pub_GetAuditDetails(intDepotID)
            Return dsAuditData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
End Class
