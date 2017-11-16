#Region " Grade.vb"
'*********************************************************************************************************************
'Name :
'      Grade.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Grade.vb)
'           2. It defines Business Logic of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/7/2013 12:25:39 PM
'*********************************************************************************************************************
#End Region
Option Strict On
Imports System.ServiceModel
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.DataAccess
Imports iInterchange.iTankDepo.Entities
Imports System.Runtime.Serialization
<ServiceContract()> _
Public Class Grade
    Inherits CodeBase
#Region "pub_GradeGetGrade() TABLE NAME:Grade"

    <OperationContract()> _
    Public Overrides Function pub_GetCodeMaster(ByVal bv_strWFDATA As String) As System.Data.DataSet
        Try
            Dim dsGradeData As GradeDataSet
            Dim objGrades As New Grades
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsGradeData = objGrades.GetGrade(intDepotID)
            Return dsGradeData
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GradeGetGradeByGradeCode() TABLE NAME:Grade"

    <OperationContract()> _
    Public Overrides Function pub_ValidatePK(ByVal bv_strGradeCode As String, ByVal bv_strWFDATA As String) As Boolean

        Try
            Dim dsGradeData As GradeDataSet
            Dim objGrades As New Grades
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWFDATA, CommonUIData.DPT_ID))
            dsGradeData = objGrades.GetGradeByGradeCode(bv_strGradeCode, intDepotID)
            If dsGradeData.Tables(GradeData._Grade).Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function
#End Region

#Region "pub_GradeCreateGrade() TABLE NAME:Grade"

    <OperationContract()> _
    Public Overrides Function pub_CreateCodeMaster(ByVal bv_strGradeCode As String, _
     ByVal bv_strGradeDescription As String, _
      ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strCRTD_BY As String, _
     ByVal bv_datCRTD_DT As DateTime, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Long

        Try
            Dim objGrade As New Grades
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim lngCreated As Long
            lngCreated = objGrade.CreateGrade(bv_strGradeCode, _
                  bv_strGradeDescription, bv_strCRTD_BY, _
                  bv_datCRTD_DT, bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return lngCreated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

#Region "pub_GradeModifyGradeGrade() TABLE NAME:Grade"

    <OperationContract()> _
    Public Overrides Function pub_ModifyCodeMaster(ByVal bv_i64GradeId As Int32, _
     ByVal bv_strGradeCode As String, _
     ByVal bv_strGradeDescription As String, _
     ByVal bv_blnActiveBit As Boolean, _
     ByVal bv_strMDFD_BY As String, _
     ByVal bv_datMDFD_DT As DateTime, _
     ByVal bv_strWfData As String) As Boolean

        Try
            Dim objGrade As New Grades
            Dim intDepotID As Integer = CInt(CommonUIs.ParseWFDATA(bv_strWfData, CommonUIData.DPT_ID))
            Dim blnUpdated As Boolean
            blnUpdated = objGrade.UpdateGrade(bv_i64GradeId, _
                bv_strGradeCode, bv_strGradeDescription, _
                 bv_strMDFD_BY, _
                  bv_datMDFD_DT, bv_blnActiveBit, intDepotID)
            Return blnUpdated
        Catch ex As Exception
            Throw New FaultException(New FaultReason(ex.Message))
        End Try
    End Function

#End Region

End Class
