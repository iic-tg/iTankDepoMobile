#Region " Measures.vb"
'*********************************************************************************************************************
'Name :
'      Measures.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(Measures.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      5/9/2013 9:48:47 AM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Entities
#Region "Measures"

Public Class Measures

#Region "Declaration Part.. "

    Dim objData As DataObjects
    Private Const MeasureSelectQueryByMSR_CD As String = "SELECT MSR_ID,MSR_CD,MSR_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM MEASURE WHERE MSR_CD=@MSR_CD"
    Private Const MeasureSelectQueryByDPT_ID As String = "SELECT MSR_ID,MSR_CD,MSR_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID FROM MEASURE WHERE DPT_ID=@DPT_ID"
    Private Const MeasureInsertQuery As String = "INSERT INTO MEASURE(MSR_ID,MSR_CD,MSR_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@MSR_ID,@MSR_CD,@MSR_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const MeasureUpdateQuery As String = "UPDATE MEASURE SET MSR_ID=@MSR_ID, MSR_CD=@MSR_CD, MSR_DSCRPTN_VC=@MSR_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT, DPT_ID=@DPT_ID WHERE MSR_ID=@MSR_ID"
    Private ds As MeasureDataSet
#End Region

#Region "Constructor.."

    Sub New()
        ds = New MeasureDataSet
    End Sub

#End Region

#Region "GetMeasure() TABLE NAME:Measure"

    Public Function GetMeasure(ByVal bv_intDepotID As Integer) As MeasureDataSet
        Try
            objData = New DataObjects(MeasureSelectQueryByDPT_ID, MeasureData.DPT_ID, bv_intDepotID)
            objData.Fill(CType(ds, DataSet), MeasureData._MEASURE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetMeasureByMeasureCode() TABLE NAME:Measure"

    Public Function GetMeasureByMeasureCode(ByVal bv_strMeasureCode As String, ByVal bv_intDepotID As Integer) As MeasureDataSet
        Try
            Dim hshParameters As New Hashtable()
            hshParameters.Add(MeasureData.DPT_ID, bv_intDepotID)
            hshParameters.Add(MeasureData.MSR_CD, bv_strMeasureCode)
            objData = New DataObjects(MeasureSelectQueryByMSR_CD, hshParameters)
            objData.Fill(CType(ds, DataSet), MeasureData._MEASURE)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "CreateMeasure() TABLE NAME:Measure"

    Public Function CreateMeasure(ByVal bv_strMeasureCode As String, _
        ByVal bv_strMeasureDescription As String, _
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
            dr = ds.Tables(MeasureData._MEASURE).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(MeasureData._MEASURE)
                .Item(MeasureData.MSR_ID) = intMax
                .Item(MeasureData.MSR_CD) = bv_strMeasureCode
                If bv_strMeasureDescription <> Nothing Then
                    .Item(MeasureData.MSR_DSCRPTN_VC) = bv_strMeasureDescription
                Else
                    .Item(MeasureData.MSR_DSCRPTN_VC) = DBNull.Value
                End If

                If bv_strCreatedBy <> Nothing Then
                    .Item(MeasureData.CRTD_BY) = bv_strCreatedBy
                Else
                    .Item(MeasureData.CRTD_BY) = DBNull.Value
                End If
                If bv_datCreatedDate <> Nothing Then
                    .Item(MeasureData.CRTD_DT) = bv_datCreatedDate
                Else
                    .Item(MeasureData.CRTD_DT) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(MeasureData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(MeasureData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(MeasureData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(MeasureData.MDFD_DT) = DBNull.Value
                End If
                .Item(MeasureData.ACTV_BT) = bv_blnActiveBit
                .Item(MeasureData.DPT_ID) = bv_intDepotID
            End With
            objData.InsertRow(dr, MeasureInsertQuery)
            dr = Nothing
            CreateMeasure = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "UpdateMeasure() TABLE NAME:Measure"

    Public Function UpdateMeasure(ByVal bv_i64MeasureId As Int64, _
        ByVal bv_strMeasureCode As String, _
        ByVal bv_strMeasureDescription As String, _
        ByVal bv_strModifiedBy As String, _
        ByVal bv_datModifiedDate As DateTime, _
        ByVal bv_blnActiveBit As Boolean, _
        ByVal bv_intDepotID As Integer) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(MeasureData._MEASURE).NewRow()
            With dr
                .Item(MeasureData.MSR_ID) = bv_i64MeasureId
                .Item(MeasureData.MSR_CD) = bv_strMeasureCode
                If bv_strMeasureDescription <> Nothing Then
                    .Item(MeasureData.MSR_DSCRPTN_VC) = bv_strMeasureDescription
                Else
                    .Item(MeasureData.MSR_DSCRPTN_VC) = DBNull.Value
                End If
                If bv_strModifiedBy <> Nothing Then
                    .Item(MeasureData.MDFD_BY) = bv_strModifiedBy
                Else
                    .Item(MeasureData.MDFD_BY) = DBNull.Value
                End If
                If bv_datModifiedDate <> Nothing Then
                    .Item(MeasureData.MDFD_DT) = bv_datModifiedDate
                Else
                    .Item(MeasureData.MDFD_DT) = DBNull.Value
                End If
                .Item(MeasureData.ACTV_BT) = bv_blnActiveBit
                .Item(MeasureData.DPT_ID) = bv_intDepotID
            End With
            UpdateMeasure = objData.UpdateRow(dr, MeasureUpdateQuery)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



End Class

#End Region
