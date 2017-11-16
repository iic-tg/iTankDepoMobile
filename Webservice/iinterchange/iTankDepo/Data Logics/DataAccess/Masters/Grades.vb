#Region " Grades.vb"
'*********************************************************************************************************************
'Name :
'      Grades.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Grades.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 9:53:16 AM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Grades"

Public Class Grades

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const GradeSelectQueryByGRD_CD As String = "SELECT GRD_ID,GRD_CD,GRD_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Grade WHERE GRD_CD=@GRD_CD"
    Private Const GradeSelectQueryByDPT_ID As String = "SELECT GRD_ID,GRD_CD,GRD_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM Grade WHERE DPT_ID=@DPT_ID"
    Private Const GradeInsertQuery As String = "INSERT INTO Grade(GRD_ID,GRD_CD,GRD_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@GRD_ID,@GRD_CD,@GRD_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const GradeUpdateQuery As String = "UPDATE Grade SET GRD_ID=@GRD_ID, GRD_CD=@GRD_CD, GRD_DSCRPTN_VC=@GRD_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE GRD_ID=@GRD_ID"
    Private ds As GradeDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New GradeDataSet
    End Sub

#End Region

#Region "GetGrade() TABLE NAME:Grade"

    Public Function GetGrade(ByVal bv_intDepotID As Integer) As GradeDataSet
        Try
            objData = New DataObjects(GradeSelectQueryByDPT_ID, GradeData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), GradeData._Grade)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetGradeByGradeCode() TABLE NAME:Grade"

    Public Function GetGradeByGradeCode(ByVal bv_strGradeCode As String, ByVal bv_intDepotID As Integer) As GradeDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(GradeData.DPT_ID, bv_intDepotID)
            hshParameters.Add(GradeData.GRD_CD, bv_strGradeCode)
            objData = New DataObjects(GradeSelectQueryByGRD_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), GradeData._Grade)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateGrade() TABLE NAME:Grade"

    Public Function CreateGrade(ByVal bv_strGradeCode As String, _
        ByVal bv_strGradeDescription As String, _
        ByVal bv_strCreatedBy As String, _
        ByVal bv_datCreatedDate As DateTime, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(GradeData._Grade).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(GradeData._Grade)
                .Item(GradeData.GRD_ID) = intMax
                .Item(GradeData.GRD_CD) = bv_strGradeCode
                If bv_strGradeDescription <> Nothing Then
                    .Item(GradeData.GRD_DSCRPTN_VC) = bv_strGradeDescription
                Else
                    .Item(GradeData.GRD_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(GradeData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(GradeData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(GradeData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(GradeData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(GradeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(GradeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(GradeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(GradeData.MDFD_DT) = DBNull.Value
                End If
                .Item(GradeData.ACTV_BT) = bv_blnActiveBit
                .Item(GradeData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, GradeInsertQuery)
            dr = Nothing
            CreateGrade = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateGrade() TABLE NAME:Grade"

    Public Function UpdateGrade(ByVal bv_i64GradeId As Int64, _
        ByVal bv_strGradeCode As String, _
        ByVal bv_strGradeDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(GradeData._Grade).NewRow()
            With dr
                .Item(GradeData.GRD_ID) = bv_i64GradeId
                .Item(GradeData.GRD_CD) = bv_strGradeCode
                If bv_strGradeDescription <> Nothing Then
                    .Item(GradeData.GRD_DSCRPTN_VC) = bv_strGradeDescription
                Else
                    .Item(GradeData.GRD_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(GradeData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(GradeData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(GradeData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(GradeData.MDFD_DT) = DBNull.Value
                End If
                .Item(GradeData.ACTV_BT) = bv_blnActiveBit
                .Item(GradeData.DPT_ID) = bv_intDepotID
            End With
            UpdateGrade = objData.UpdateRow(dr, GradeUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class

#End Region
