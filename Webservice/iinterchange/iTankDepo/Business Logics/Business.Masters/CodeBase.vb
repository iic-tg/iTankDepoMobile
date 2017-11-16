Option Strict On
Imports System.Data.Common

Public MustInherit Class CodeBase
    Sub New()

    End Sub

    ''' <summary>
    ''' This method used to get data all records from code master tables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As DataSet
    End Function

    ''' <summary>
    ''' This method used to check whether the Code is Unique
    ''' </summary>
    ''' <param name="bv_strCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function pub_ValidatePK(ByVal bv_strCode As String, ByVal bv_strWFDATA As String) As Boolean
    End Function
    ''' <summary>
    ''' This method used to insert record in new mode
    ''' </summary>
    ''' <param name="bv_strCode"></param>
    ''' <param name="bv_strDescription"></param>
    ''' <param name="bv_blnActivate"></param>
    ''' <param name="bv_strModified_By"></param>
    ''' <param name="bv_datModified"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function pub_CreateCodeMaster(ByVal bv_strCode As String, ByVal bv_strDescription As String, ByVal bv_blnActivate As Boolean, _
                                                     ByVal bv_strCreated_By As String, ByVal bv_datCreated As DateTime, ByVal bv_strModified_By As String, ByVal bv_datModified As DateTime, ByVal bv_strWFDATA As String) As Long
    End Function
    ''' <summary>
    ''' Name     : pub_ModifyCodeMaster
    ''' Purpose  : To update the record in edit Mode
    ''' </summary>
    ''' <param name="bv_intID"></param>
    ''' <param name="bv_strCode"></param>
    ''' <param name="bv_strDescription"></param>
    ''' <param name="bv_blnActivate"></param>
    ''' <param name="bv_strModified_By"></param>
    ''' <param name="bv_datModified"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function pub_ModifyCodeMaster(ByVal bv_intID As Int32, ByVal bv_strCode As String, ByVal bv_strDescription As String, ByVal bv_blnActivate As Boolean, _
                                                     ByVal bv_strModified_By As String, ByVal bv_datModified As DateTime, ByVal bv_strWFDATA As String) As Boolean
    End Function

 
End Class
