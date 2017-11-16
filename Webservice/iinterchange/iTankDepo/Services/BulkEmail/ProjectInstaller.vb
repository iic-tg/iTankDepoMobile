Imports System.ComponentModel
Imports System.Configuration.Install
Imports Microsoft.Win32

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Public Overrides Sub Install(ByVal stateServer As IDictionary)
        Try
            MyBase.Install(stateServer)

            Dim system, currentControlSet, services, service, config As RegistryKey

            system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System")

            currentControlSet = system.OpenSubKey("CurrentControlSet")

            services = currentControlSet.OpenSubKey("Services")

            service = services.OpenSubKey(Me.siBulkEmail.ServiceName, True)

            service.SetValue("Description", "iDepoBulkEmailService")

            config = service.CreateSubKey("iDP_BML")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub

    Public Overrides Sub Uninstall(ByVal stateServer As IDictionary)
        Dim system, currentControlSet, services, service As RegistryKey
        Try
            system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System")

            currentControlSet = system.OpenSubKey("CurrentControlSet")

            services = currentControlSet.OpenSubKey("Services")

            service = services.OpenSubKey(Me.siBulkEmail.ServiceName, True)

            service.DeleteSubKeyTree("iDP_BML")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            MyBase.Uninstall(stateServer)
        End Try
    End Sub

End Class
