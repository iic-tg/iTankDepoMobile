﻿<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.spiDAR = New System.ServiceProcess.ServiceProcessInstaller()
        Me.siDAR = New System.ServiceProcess.ServiceInstaller()
        '
        'spiDAR
        '
        Me.spiDAR.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.spiDAR.Password = Nothing
        Me.spiDAR.Username = Nothing
        '
        'siDAR
        '
        Me.siDAR.ServiceName = "iDepoDARService"
        Me.siDAR.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.spiDAR, Me.siDAR})

    End Sub
    Friend WithEvents spiDAR As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents siDAR As System.ServiceProcess.ServiceInstaller

End Class
