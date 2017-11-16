Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports iInterchange.Framework.UI.iPageBase
Imports iInterchange.Framework
Imports System.Data
Imports System.Web.UI.Page
Imports System.Globalization
Imports System.Net
Imports System.Data.SqlClient






' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebService
    Inherits System.Web.Services.WebService



    '   <WebMethod(enableSession:=True)> _
    '<ScriptMethod(UseHttpGet:=False, ResponseFormat:=ResponseFormat.Json)> _
    '   Public Function WebServiceLogin(ByVal Credentials As credentails) As Message

    '       Dim login As New Login

    '       Dim returnValue As Message = login.Login(Credentials.Username, Credentials.Password)
    '       'ByVal ParamArray Credentials As Object

    '       Return returnValue

    '   End Function






End Class