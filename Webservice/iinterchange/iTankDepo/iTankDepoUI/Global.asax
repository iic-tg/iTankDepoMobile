<%@ Application Language="VB" %>


<script runat="server">

   'Public Shared Property Current As HttpContext
    
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup  
        
        
        Try
            
            
            Dim dtRecordLock As New LockData
            Application("LockData") = dtRecordLock
            Dim arr_Sessions As New ArrayList()
            Application("arr_SessionIDs") = arr_Sessions
            
        Catch ex As Exception

        End Try
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
      
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Try
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, Server.GetLastError)
            Server.ClearError()
            If Request.ContentLength > 4194304 AndAlso Request.RawUrl.ToLower.IndexOf("PhotoUpload.aspx") >= 0 Then
                Server.ClearError()
            End If
        Catch ex As HttpException
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, Server.GetLastError)
            If Request.ContentLength > 4194304 AndAlso Request.RawUrl.ToLower.IndexOf("PhotoUpload.aspx") >= 0 Then
                Server.ClearError()
            End If
        End Try
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
        
        
        
        
        '''''''''''''''''''For mobile Dont expire session will be taken care in front end''''''''''''''''''
        'Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        'Dim con As Boolean = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("iTankMobileService")
        'If con Then
        'Else
        pvt_SessionEndSequence()
        'End If
        
    End Sub
    
    Protected Sub Application_BeginRequest(sender As Object, e As System.EventArgs)
         
        System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*")
        
        System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT,DELETE")
        System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers")
            
       
        
        'HttpContext.Current.Response.ContentType = "application/json;charset=utf-8"
        Dim newCulture As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture.Clone()
        newCulture.DateTimeFormat.ShortDatePattern = "M/d/yyyy"
        newCulture.DateTimeFormat.DateSeparator = "-"
        System.Threading.Thread.CurrentThread.CurrentCulture = newCulture
    End Sub
    
    
    'Protected Sub OnAuthorizeRequest(sender As Object, e As System.EventArgs)
        
    '    Dim application = 
    'End Sub
    
    Private Sub pvt_SessionEndSequence()
        Dim arr_SessionIDs As ArrayList
        Dim strSessionId As String = Session.SessionID.ToString()
        Try
            arr_SessionIDs = CType(Application("arr_SessionIDs"), ArrayList)
            If arr_SessionIDs.Contains(strSessionId) Then
                arr_SessionIDs.Remove(strSessionId)
                Application("arr_SessionIDs") = arr_SessionIDs
            End If
            ''Code: Written for clear the session while session end
            Dim objCommonData As New CommonData
            objCommonData.ClearLockData(strSessionId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
</script>