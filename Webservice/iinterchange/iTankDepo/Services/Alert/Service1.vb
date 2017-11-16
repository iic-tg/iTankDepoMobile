Imports System.ServiceProcess
Imports iInterchange.iTankDepo.Business.Services
Imports iInterchange.iTankDepo.Business.Common
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.Data
Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Web

Public Class Service1

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
    End Sub

    Private Sub tmrAlert_Elapsed(sender As System.Object, e As System.Timers.ElapsedEventArgs) Handles tmrAlert.Elapsed
        Try
            SyncLock tmrAlert
                Dim objAlert As New Alert
                objAlert.pub_SendAlert()
            End SyncLock
        Catch ex As Exception

        End Try

    End Sub
End Class
