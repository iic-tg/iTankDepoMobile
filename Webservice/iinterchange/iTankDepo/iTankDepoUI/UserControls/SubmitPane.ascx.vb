Imports System.Data
Imports System.Configuration

Partial Class UserControls_SubmitPane
    Inherits System.Web.UI.UserControl

#Region "Declarations"
    Public onClientSubmit As String
    Public onClientPrint As String
    Public TabIndex As String = 0
    Private pvt_lngItemNo As Long
    Private objHasTbl As Hashtable
    Private pvt_intActivityId As Integer
    Private Const ITEMNO As String = "itemno"
    Dim objCommonData As New CommonData
#End Region

#Region "Event Submit Pane Load"
    ''' <summary>
    ''' This event will be invoked on every submit pane load to initialize the workspace in postback mode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event SubmitPaneLoad(ByVal sender As Object, ByVal e As System.EventArgs)
#End Region

#Region "Get Page Attribute"
    ''' <summary>
    ''' This method will be used for getting data from list grid
    ''' </summary>
    ''' <param name="bv_KEY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pub_GetPageAttribute(ByVal bv_KEY As String) As String
        Try
            If Not objHasTbl Is Nothing AndAlso objHasTbl.ContainsKey(bv_KEY) Then
                If Not IsDBNull(objHasTbl.Item(bv_KEY)) Then
                    pub_GetPageAttribute = objHasTbl.Item(bv_KEY)
                Else
                    pub_GetPageAttribute = Nothing
                End If
            Else
                pub_GetPageAttribute = Nothing
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "pub_GetPageMode"
    ''' <summary>
    ''' This method will be used for getting page mode
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pub_GetPageMode() As String
        Try
            Dim objCommonData As New CommonData()
            objCommonData.pvt_strActivityId = pvt_intActivityId
            Return objCommonData.GetCurrentPageMode()
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Function
#End Region

#Region "Page Load"
    ''' <summary>
    ''' This event will be fired on everytime when submit pane loads.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.QueryString("mode") = "view" Then
                pnlSubmitButton.Visible = False
            End If

            If Request.QueryString(ITEMNO) <> "" Then
                pvt_lngItemNo = Request.QueryString(ITEMNO)
            End If
            If Request.QueryString("activityid") <> "" Then
                pvt_intActivityId = Request.QueryString("activityid")
            End If
            If pvt_lngItemNo >= 0 AndAlso Request.QueryString(ITEMNO) <> "" Then
                objCommonData.pvt_strActivityId = pvt_intActivityId
                objHasTbl = objCommonData.GetSelectedRow(pvt_lngItemNo)
            End If

            If objHasTbl Is Nothing Then
                objHasTbl = New Hashtable
            End If

            objHasTbl.Add("tablename", Request.QueryString("tablename"))
            objHasTbl.Add("pagetitle", Request.QueryString("pagetitle"))

            If Request.QueryString("tablename") IsNot Nothing Then
                hdnTableName.Value = Request.QueryString("tablename")
            End If
            If Request.QueryString("mode") IsNot Nothing Then
                hdnPageMode.Value = Request.QueryString("mode")
                Session("pagemode" + CStr(pvt_intActivityId)) = hdnPageMode.Value
            End If

            RaiseEvent SubmitPaneLoad(sender, e)

            'Generating WFDATA
            If pvt_intActivityId > 0 Then
                Page.ClientScript.RegisterHiddenField("WFDATA", objCommonData.GenerateWFData(pvt_intActivityId))
            Else
                Page.ClientScript.RegisterHiddenField("WFDATA", objCommonData.GenerateWFData())
            End If

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region

End Class
