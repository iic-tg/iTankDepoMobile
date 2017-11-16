' NOTE: You can use the "Rename" command on the context menu to change the class name "service" in code, svc and config file together.
Public Class service
    Implements Iservice

    Public Function GetAttachmentsByRepairEstimateNo(depotId As Long, estimateNo As String, estimateID As Long, filterUpload As String) As System.Data.DataSet Implements Iservice.GetAttachmentsByRepairEstimateNo
        Dim ds As New DataSet
        Dim dtAttachment As New DataTable
        Try
            Dim objCommon As New CommonData
            Dim objRepairEstimate As New RepairEstimate
            Dim dt As New DataTable
            dt = objRepairEstimate.Pub_GetAttachmentByRepairEstimateNo(depotId, estimateNo, estimateID, filterUpload).Tables("V_ATTACHMENT")
            dtAttachment = dt.Clone
            dtAttachment.Merge(dt)
            ds.Tables.Add(dtAttachment)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
        Return ds
    End Function

    Public Function GetAttachmentsByActivityName(ByVal depotId As Long, ByVal estimateNo As String, ByVal activityName As String, ByVal filterUpload As String) As System.Data.DataSet Implements Iservice.GetAttachmentsByActivityName
        Dim ds As New DataSet
        Dim dtAttachment As New DataTable
        Try
            Dim objCommon As New CommonData
            Dim objRepairEstimate As New RepairEstimate
            Dim dt As New DataTable
            dt = objRepairEstimate.Pub_GetAttachmentByActivityName(depotId, estimateNo, activityName, filterUpload).Tables("V_ATTACHMENT")
            'uploadFilter
            dtAttachment = dt.Clone
            dtAttachment.Merge(dt)
            ds.Tables.Add(dtAttachment)
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex)
        End Try
        Return ds
    End Function

End Class
