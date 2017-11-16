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
Public Class User
#Region "Login"

#Region "Password Encryption Method"
#Region "pub_EncryptPassword()"
    ''' <summary>
    ''' This method used to encrypt the password
    ''' </summary>
    ''' <param name="bv_strPassword"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_EncryptPassword(ByVal bv_strPassword As String) As String
        Dim byteRepresentation As Byte() = System.Text.UnicodeEncoding.UTF8.GetBytes(bv_strPassword)
        Dim hashedTextInBytes As Byte() = Nothing
        Dim myMD5 As System.Security.Cryptography.MD5CryptoServiceProvider = New System.Security.Cryptography.MD5CryptoServiceProvider
        Try
            hashedTextInBytes = myMD5.ComputeHash(byteRepresentation)
            bv_strPassword = Convert.ToBase64String(hashedTextInBytes)

            'Getting Salt
            Dim mac3des As New System.Security.Cryptography.MACTripleDES()
            Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("iic"))
            Dim strSalt As String = Convert.ToBase64String(mac3des.ComputeHash(mac3des.Key))

            bv_strPassword = String.Concat(bv_strPassword.Substring(0, bv_strPassword.Length - 2), "-", strSalt)
            Return bv_strPassword
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#End Region

#Region "pub_CreateUserLog"
    ''' <summary>
    ''' This method used to store client access details for monitoring
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <param name="bv_strIPAddress"></param>
    ''' <param name="bv_strSessionID"></param>
    ''' <param name="bv_strUserAgent"></param>
    ''' <param name="bv_strBrowserVersion"></param>
    ''' <param name="bv_strScreenSize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_CreateUserLog(ByVal bv_strUserName As String, _
                                      ByVal bv_dtLGN As DateTime, _
                                      ByVal bv_dtLGT As DateTime, _
                                      ByVal bv_strIPAddress As String, _
                                      ByVal bv_strSessionID As String, _
                                      ByVal bv_strUserAgent As String, _
                                      ByVal bv_strBrowserVersion As String, _
                                      ByVal bv_strScreenSize As String) As Long
        Try
            Dim objUser As New Users
            Dim lngCreated As Long

            lngCreated = objUser.CreateUSER_LOG(bv_strUserName, bv_dtLGN, bv_dtLGT, bv_strIPAddress, bv_strSessionID, bv_strUserAgent, bv_strBrowserVersion, bv_strScreenSize)

            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_UpdateUserLog"
    ''' <summary>
    ''' This method used to update logout date and time in USER_LOG table and last login information in USER_DETAIL table.
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <param name="bv_strIPAddress"></param>
    ''' <param name="bv_strSessionID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_UpdateUserLog(ByVal bv_strUserName As String, _
                                      ByVal bv_datLgtDat As DateTime, _
                                      ByVal bv_strIPAddress As String, _
                                      ByVal bv_strSessionID As String) As Boolean
        Try
            Dim objUser As New Users
            pub_UpdateUserLog = objUser.UpdateUSER_LOG(bv_strUserName, bv_datLgtDat, bv_strIPAddress, bv_strSessionID)

        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#End Region

#Region "pub_CreateUserDetail() TABLE NAME:USER_DETAIL"
    <OperationContract()> _
    Public Function pub_CreateUserDetail(ByVal bv_strUSR_NAM As String, _
        ByVal bv_strPSSWRD As String, _
        ByVal bv_strFRST_NAM As String, _
        ByVal bv_strLST_NAM As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_i32RL_ID As Int32, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_intDPT_ID As Integer, _
        ByVal bv_strPHT_PTH As String, _
        ByVal bv_strTHM_NAM As String, _
        ByVal bv_strWfData As String) As Long

        Try
            Dim objUsers As New Users
            Dim lngCreated As Long
            lngCreated = objUsers.CreateUserDetail(bv_strUSR_NAM, _
                  bv_strPSSWRD, bv_strFRST_NAM, _
                  bv_strLST_NAM, bv_strEML_ID, _
                  bv_i32RL_ID, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnACTV_BT, _
                  bv_strPHT_PTH, bv_strTHM_NAM, _
                  bv_intDPT_ID)
            Return lngCreated
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "pub_ModifyUserDetail() TABLE NAME:USER_DETAIL"
    <OperationContract()> _
    Public Function pub_ModifyUserDetail(ByVal bv_i32USR_ID As Int32, _
        ByVal bv_strUSR_NAM As String, _
        ByVal bv_strPSSWRD As String, _
        ByVal bv_strFRST_NAM As String, _
        ByVal bv_strLST_NAM As String, _
        ByVal bv_strEML_ID As String, _
        ByVal bv_i32RL_ID As Int32, _
        ByVal bv_strMDFD_BY As String, _
        ByVal bv_datMDFD_DT As DateTime, _
        ByVal bv_blnACTV_BT As Boolean, _
        ByVal bv_intDPT_ID As Integer, _
          ByVal bv_strPHT_PTH As String, _
        ByVal bv_strTHM_NAM As String, _
        ByVal bv_strWfData As String) As Boolean

        Try
            Dim objUsers As New Users
            Dim blnUpdated As Boolean
            blnUpdated = objUsers.UpdateUserDetail(bv_i32USR_ID, _
                  bv_strUSR_NAM, bv_strPSSWRD, _
                  bv_strFRST_NAM, bv_strLST_NAM, _
                  bv_strEML_ID, _
                   bv_i32RL_ID, _
                  bv_strMDFD_BY, bv_datMDFD_DT, _
                  bv_blnACTV_BT, bv_strPHT_PTH, _
                  bv_strTHM_NAM, _
                  bv_intDPT_ID)
            Return blnUpdated
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GET: pub_GetUserDetailByUserName() TABLE NAME:USER_DETAIL"

    <OperationContract()> _
    Public Function pub_GetUserDetailByUserName(ByVal bv_strUserName As String) As UserDataSet
        Try
            Dim dsUserDataSet As UserDataSet
            Dim objUsers As New Users
            objUsers.GetAPP_Version()
            objUsers.GetUserDetailByUserName(bv_strUserName)
            objUsers.GetEnumTable()
            dsUserDataSet = objUsers.GetActivities()
            Return dsUserDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET: pub_GetUserDetailByUserID() TABLE NAME:USER_DETAIL"

    <OperationContract()> _
    Public Function pub_GetUserDetailByUserID(ByVal bv_intUserID As Integer) As UserDataSet
        Try
            Dim dsUserDataSet As UserDataSet
            Dim objUsers As New Users
            dsUserDataSet = objUsers.GetUserDetailByUserId(bv_intUserID)
            Return dsUserDataSet
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#Region "VALIDATE : pub_ValidatePKUser() TABLE NAME:USER_DETAIL"
    ''' <summary>
    ''' This method is used to validate the User Name
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_ValidatePKUser(ByVal bv_strUserName As String) As Boolean
        Dim dsUserDataSet As UserDataSet
        Dim objUsers As New Users
        Try
            dsUserDataSet = objUsers.GetUserDetailByUserName(bv_strUserName)
            If dsUserDataSet.Tables(UserData._V_USER_DETAIL).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "VALIDATE : pub_ValidatePKUserWithDepotID() TABLE NAME:USER_DETAIL"
    ''' <summary>
    ''' This method is used to validate the User Name
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_ValidatePKUserWithDepotID(ByVal bv_strUserName As String, ByVal bv_i32DepotId As Int32) As Boolean
        Dim dsUserDataSet As UserDataSet
        Dim objUsers As New Users
        Try
            dsUserDataSet = objUsers.GetUserDetailByUserNameAndDepotId(bv_strUserName, bv_i32DepotId)
            If dsUserDataSet.Tables(UserData._V_USER_DETAIL).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "VALIDATE : Change Password"
#Region "Validating User Password"
    <OperationContract()> _
    Public Function pub_ValidateUserPassword(ByVal bv_strOldPassword As String, ByVal bv_strUserName As String) As Boolean
        Try
            Dim dsUserDataSet As UserDataSet
            Dim objUsers As New Users
            dsUserDataSet = objUsers.GetUserByUserNameAndPassword(bv_strOldPassword, bv_strUserName)

            If dsUserDataSet.Tables(UserData._V_USER_DETAIL).Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try

    End Function
#End Region

#Region "Modify Password"
    <OperationContract()> _
    Public Function pub_ModifyUserPassword(ByVal bv_usr_nam As String, ByVal bv_psswrd As String) As Boolean
        Try
            Dim objUser As New Users
            Dim blnUpdate As Boolean
            blnUpdate = objUser.UpdatePassword(bv_usr_nam, bv_psswrd)
            Return blnUpdate
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region
#End Region

#Region "VALIDATE : pub_ValidateEmailID() TABLE NAME:USER_DETAIL"
    <OperationContract()> _
    Public Function pub_ValidateEmailID(ByVal bv_strEmailID As String) As Boolean
        Dim dsUserDataSet As UserDataSet
        Dim objUsers As New Users
        Try
            dsUserDataSet = objUsers.GetUserDetailByEmailID(bv_strEmailID)
            If dsUserDataSet.Tables(UserData._V_USER_DETAIL).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "VALIDATE : pub_ValidateUserIDAndEmailID() TABLE NAME:USER_DETAIL"
    <OperationContract()> _
    Public Function pub_ValidateUserIDAndEmailID(ByVal bv_strUserName As String, ByVal bv_strEmailID As String) As Boolean
        Dim dsUserDataSet As UserDataSet
        Dim objUsers As New Users
        Try
            dsUserDataSet = objUsers.GetUserDetailByUserNameAndEmailID(bv_strUserName, bv_strEmailID)
            If dsUserDataSet.Tables(UserData._V_USER_DETAIL).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "GET: pub_GetAPP_Version() TABLE NAME:APP_VERSION"

    <OperationContract()> _
    Public Function pub_GetAPP_Version() As UserDataSet
        Try
            Dim dsUserDataSet As UserDataSet
            Dim objUsers As New Users
            dsUserDataSet = objUsers.GetAPP_Version()
            Return (dsUserDataSet)
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "VALIDATE : pub_ValidatePKUserWithDepotID() TABLE NAME:USER_DETAIL"
    ''' <summary>
    ''' This method is used to validate the User Name
    ''' </summary>
    ''' <param name="bv_strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Public Function pub_ValidatePKUserWithDepotID(ByVal bv_strUserName As String) As Boolean
        Dim dsUserDataSet As UserDataSet
        Dim objUsers As New Users
        Try
            dsUserDataSet = objUsers.GetUserDetailByUserNameAndDepotId(bv_strUserName)
            If dsUserDataSet.Tables(UserData._V_USER_DETAIL).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

End Class
