Imports Microsoft.VisualBasic
Imports System.Data.Common
Imports System.Configuration

Public Class Settings
    Private pvt_config As ConnectionStringSettings
    Private pvt_QueryCache As Boolean = False
    Private pvt_intCmdTimeOut As Integer
    Sub New()
        pvt_config = ConfigurationManager.ConnectionStrings(ConfigurationManager.AppSettings("ConnectionStringName"))
        pvt_QueryCache = (ConfigurationManager.AppSettings("LogDBQuery").ToLower = "yes")
        pvt_intCmdTimeOut = ConfigurationManager.AppSettings("CommandTimeOut")
    End Sub
    Public Function getdbFactory() As DbProviderFactory
        Dim provider As DbProviderFactory
        provider = DbProviderFactories.GetFactory(pvt_config.ProviderName)
        Return provider
    End Function
    Public ReadOnly Property dbConnectionString() As String
        Get
            Return pvt_config.ConnectionString
        End Get
    End Property

    Public ReadOnly Property QueryCacheEnabled() As Boolean
        Get
            Return pvt_QueryCache
        End Get
    End Property
    Public ReadOnly Property CommandTimeOut() As Integer
        Get
            Return pvt_intCmdTimeOut
        End Get
    End Property

    Public ReadOnly Property Provider() As String
        Get
            Return pvt_config.ProviderName
        End Get
    End Property

End Class
