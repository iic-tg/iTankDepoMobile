Imports Microsoft.VisualBasic

Public Class RetreiveData

    Public Function GetDepotID(ByVal Username As String) As String
        Dim strDepotID As String
        Dim dsUser As New DataSet
        Dim objUser As New User
        dsUser = objUser.pub_GetUserDetailByUserName(Username)
        Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
        If dtUser.Rows.Count > 0 Then
            strDepotID = dtUser.Rows(0).Item(UserData.DPT_ID)
        End If
        Return strDepotID
    End Function


    ' Try
    'Dim strDepotID As String
    'Dim dsUser As DataSet = RetrieveData("UserData")
    'Dim dtUser As DataTable = dsUser.Tables(UserData._V_USER_DETAIL)
    '        If dtUser.Rows.Count > 0 Then
    '            strDepotID = dtUser.Rows(0).Item(UserData.DPT_ID)
    '        End If
    '        Return strDepotID
    '    Catch ex As Exception
    '        Return Nothing
    '        iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
    '    End Try

End Class
