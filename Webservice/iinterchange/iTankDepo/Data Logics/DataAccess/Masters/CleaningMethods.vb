#Region " CleaningMethods.vb"
'*********************************************************************************************************************
'Name :
'      CleaningMethods.vb
'Purpose : The Class main purpose is to 
'           1. Defines all properties for the file(CleaningMethods.vb)
'           2. It defines Data Access of the tables.
'Created By: 
'            Business Logic Generator 
'Changes and Change Date : 
'                      10/8/2014 5:28:12 PM
'*********************************************************************************************************************
#End Region
Imports Microsoft.VisualBasic
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports iInterchange.iTankDepo.GatewayFramework

#Region "CleaningMethods"
Public Class CleaningMethods
#Region "Declaration Part.. "
    ''Queries
    Dim objData As DataObjects
    Private Const Cleaning_MethodDetailSelectQueryBy As String = "SELECT CLNNG_MTHD_DTL_ID,CLNNG_MTHD_TYP_ID,CLNNG_MTHD_TYP_CD,CLNNG_MTHD_TYP_DSCRPTN_VC,CLNNG_TYP_ID,CLNNG_TYP_CD,CLNNG_TYP_DSCRPTN_VC,CMMNTS_VC FROM V_CLEANING_METHOD_DETAIL WHERE CLNNG_MTHD_TYP_ID=@CLNNG_MTHD_TYP_ID"
    Private Const Cleaning_MethodInsertQuery As String = "INSERT INTO CLEANING_METHOD(CLNNG_MTHD_TYP_ID,CLNNG_MTHD_TYP_CD,CLNNG_MTHD_TYP_DSCRPTN_VC,CRTD_BY,CRTD_DT,MDFD_BY,MDFD_DT,ACTV_BT,DPT_ID)VALUES(@CLNNG_MTHD_TYP_ID,@CLNNG_MTHD_TYP_CD,@CLNNG_MTHD_TYP_DSCRPTN_VC,@CRTD_BY,@CRTD_DT,@MDFD_BY,@MDFD_DT,@ACTV_BT,@DPT_ID)"
    Private Const Cleaning_Method_DetailInsertQuery As String = "INSERT INTO CLEANING_METHOD_DETAIL(CLNNG_MTHD_DTL_ID,CLNNG_MTHD_TYP_ID,CLNNG_TYP_ID,CMMNTS_VC)VALUES(@CLNNG_MTHD_DTL_ID,@CLNNG_MTHD_TYP_ID,@CLNNG_TYP_ID,@CMMNTS_VC)"
    Private Const Cleaning_MethodUpdateQuery As String = "UPDATE CLEANING_METHOD SET  CLNNG_MTHD_TYP_CD=@CLNNG_MTHD_TYP_CD, CLNNG_MTHD_TYP_DSCRPTN_VC=@CLNNG_MTHD_TYP_DSCRPTN_VC, MDFD_BY=@MDFD_BY, MDFD_DT=@MDFD_DT, ACTV_BT=@ACTV_BT WHERE CLNNG_MTHD_TYP_ID=@CLNNG_MTHD_TYP_ID"
    Private Const Cleaning_Method_DetailUpdateQuery As String = "UPDATE CLEANING_METHOD_DETAIL SET  CLNNG_TYP_ID=@CLNNG_TYP_ID, CMMNTS_VC=@CMMNTS_VC WHERE CLNNG_MTHD_DTL_ID=@CLNNG_MTHD_DTL_ID"
    Private Const Cleaning_Method_DetailDeleteeQuery As String = "DELETE FROM CLEANING_METHOD_DETAIL WHERE CLNNG_MTHD_DTL_ID=@CLNNG_MTHD_DTL_ID"
    Private Const CleaningMethodTypeCodeCountQuery As String = "SELECT COUNT(CLNNG_MTHD_TYP_ID) FROM CLEANING_METHOD WHERE CLNNG_MTHD_TYP_CD=@CLNNG_MTHD_TYP_CD"
    Private ds As CleaningMethodDataSet
#End Region

#Region "Constructor.."

	Sub New()
        ds = New CleaningMethodDataSet
	End Sub

#End Region

#Region "GetCleaningMethodBy() TABLE NAME:Cleaning_Method"
    ''' <summary>
    ''' This method is to get CleaningMethod
    ''' </summary>
    ''' <returns>CleaningMethodDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetCleaningMethodDetailByCleaningMethodTypeID(ByVal bv_i64CleaningMethodTypeID As Int64) As CleaningMethodDataSet
        Try
            objData = New DataObjects(Cleaning_MethodDetailSelectQueryBy, CleaningMethodData.CLNNG_MTHD_TYP_ID, CStr(bv_i64CleaningMethodTypeID))
            objData.Fill(CType(ds, DataSet), CleaningMethodData._V_CLEANING_METHOD_DETAIL)
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateCleaningMethod() TABLE NAME:Cleaning_Method"
    ''' <summary>
    ''' This method is to create CleaningMethod
    ''' </summary>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Function CreateCleaningMethod(ByVal bv_strCleaningMethodTypeCode As String, _
                                         ByVal bv_strCleaningMethodTypeDescription As String, _
                                         ByVal bv_blnActiveID As Boolean, _
                                         ByVal bv_strCreatedBy As String, _
                                         ByVal bv_datCreatedDate As DateTime, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByVal bv_i32DepotID As Int32, _
                                         ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningMethodData._CLEANING_METHOD).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CleaningMethodData._CLEANING_METHOD, br_objTransaction)
                .Item(CleaningMethodData.CLNNG_MTHD_TYP_ID) = intMax
                .Item(CleaningMethodData.CLNNG_MTHD_TYP_CD) = bv_strCleaningMethodTypeCode
                .Item(CleaningMethodData.CLNNG_MTHD_TYP_DSCRPTN_VC) = bv_strCleaningMethodTypeDescription
                .Item(CleaningMethodData.ACTV_BT) = bv_blnActiveID
                .Item(CleaningMethodData.CRTD_BY) = bv_strCreatedBy
                .Item(CleaningMethodData.CRTD_DT) = bv_datCreatedDate
                .Item(CleaningMethodData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningMethodData.MDFD_DT) = bv_datModifiedDate
                .Item(CleaningMethodData.DPT_ID) = bv_i32DepotID
            End With
            objData.InsertRow(dr, Cleaning_MethodInsertQuery, br_objTransaction)
            dr = Nothing
            CreateCleaningMethod = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCleaningMethod() TABLE NAME:Cleaning_Method"
    ''' <summary>
    ''' This method is to update CleaningMethod
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function UpdateCleaningMethod(ByVal bv_i64CleaningMethodTypeID As Int64, _
                                         ByVal bv_strCleaningMethodTypeCode As String, _
                                         ByVal bv_strCleaningMethodTypeDescription As String, _
                                         ByVal bv_blnActiveID As Boolean, _
                                         ByVal bv_strModifiedBy As String, _
                                         ByVal bv_datModifiedDate As DateTime, _
                                         ByRef br_objTransaction As Transactions) As Boolean
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningMethodData._CLEANING_METHOD).NewRow()
            With dr
                .Item(CleaningMethodData.CLNNG_MTHD_TYP_ID) = bv_i64CleaningMethodTypeID
                .Item(CleaningMethodData.CLNNG_MTHD_TYP_CD) = bv_strCleaningMethodTypeCode
                .Item(CleaningMethodData.CLNNG_MTHD_TYP_DSCRPTN_VC) = bv_strCleaningMethodTypeDescription
                .Item(CleaningMethodData.ACTV_BT) = bv_blnActiveID
                .Item(CleaningMethodData.MDFD_BY) = bv_strModifiedBy
                .Item(CleaningMethodData.MDFD_DT) = bv_datModifiedDate
            End With
            UpdateCleaningMethod = objData.UpdateRow(dr, Cleaning_MethodUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "CreateCleaningMethodDetail() TABLE NAME:Cleaning_Method_Detail"
    ''' <summary>
    ''' This method is to create cleaning Method detail
    ''' </summary>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Function CreateCleaningMethodDetail(ByVal bv_i64CleaningMethodTypeID As Int64, _
                                               ByVal bv_i64CleaningTypeID As Int64, _
                                               ByVal bv_strComments As String, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            Dim intMax As Long
            objData = New DataObjects()
            dr = ds.Tables(CleaningMethodData._CLEANING_METHOD_DETAIL).NewRow()
            With dr
                intMax = CommonUIs.GetIdentityValue(CleaningMethodData._CLEANING_METHOD_DETAIL, br_objTransaction)
                .Item(CleaningMethodData.CLNNG_MTHD_DTL_ID) = intMax
                .Item(CleaningMethodData.CLNNG_MTHD_TYP_ID) = bv_i64CleaningMethodTypeID
                .Item(CleaningMethodData.CLNNG_TYP_ID) = bv_i64CleaningTypeID
                If bv_strComments <> Nothing Then
                    .Item(CleaningMethodData.CMMNTS_VC) = bv_strComments
                Else
                    .Item(CleaningMethodData.CMMNTS_VC) = DBNull.Value
                End If
            End With
            objData.InsertRow(dr, Cleaning_Method_DetailInsertQuery, br_objTransaction)
            dr = Nothing
            CreateCleaningMethodDetail = intMax
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "UpdateCleaningMethodDetail() TABLE NAME:Cleaning_Method_Detail"
    ''' <summary>
    ''' This method is to update cleaning Method detail
    ''' </summary>
    ''' <returns>Long</returns>
    ''' <remarks></remarks>
    Public Function UpdateCleaningMethodDetail(ByVal bv_i64CleaningMethodDetailID As Int64, _
                                               ByVal bv_i64CleaningMethodTypeID As Int64, _
                                               ByVal bv_i64CleaningTypeID As Int64, _
                                               ByVal bv_strComments As String, ByRef br_objTransaction As Transactions) As Long
        Try
            Dim dr As DataRow
            objData = New DataObjects()
            dr = ds.Tables(CleaningMethodData._CLEANING_METHOD_DETAIL).NewRow()
            With dr
                .Item(CleaningMethodData.CLNNG_MTHD_DTL_ID) = bv_i64CleaningMethodDetailID
                .Item(CleaningMethodData.CLNNG_TYP_ID) = bv_i64CleaningTypeID
                .Item(CleaningMethodData.CMMNTS_VC) = bv_strComments
            End With
            UpdateCleaningMethodDetail = objData.UpdateRow(dr, Cleaning_Method_DetailUpdateQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "DeleteCleaningMethodDetail TABLE NAME:Cleaning_Method_Detail"
    ''' <summary>
    ''' This method is to delete cleaning Method detail
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteCleaningMethodDetail(ByVal bv_i64CleaningMethodDetailID As Int64, _
                                                 ByRef br_objTransaction As Transactions) As Boolean
        Dim dr As DataRow
        objData = New DataObjects()
        Try
            dr = ds.Tables(CleaningMethodData._CLEANING_METHOD_DETAIL).NewRow()
            With dr
                .Item(CleaningMethodData.CLNNG_MTHD_DTL_ID) = bv_i64CleaningMethodDetailID
            End With
            DeleteCleaningMethodDetail = objData.DeleteRow(dr, Cleaning_Method_DetailDeleteeQuery, br_objTransaction)
            dr = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "GetCleaningMethodTypeCodeCount"
    ''' <summary>
    ''' This method is to get cleaning Method Types
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function GetCleaningMethodTypeCodeCount(ByVal bv_strCleaningMethodTypeCode As String, ByVal bv_intDepotId As Int32) As Int32
        Try
            Dim hshparameters As New Hashtable
            hshparameters.Add(CleaningMethodData.CLNNG_MTHD_TYP_CD, bv_strCleaningMethodTypeCode)
            hshparameters.Add(CleaningMethodData.DPT_ID, bv_intDepotId)
            objData = New DataObjects(CleaningMethodTypeCodeCountQuery, hshparameters)
            Return objData.ExecuteScalar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
#End Region
