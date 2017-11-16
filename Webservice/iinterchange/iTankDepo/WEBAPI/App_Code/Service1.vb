Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Business.Operations

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Service1
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Dim dsGateInData As New GateinDataSet
        Dim objGateIn As New Gatein

        dsGateInData = objGateIn.GetGateInPreAdviceDetail(1)

        Return "Hello World"
    End Function

End Class