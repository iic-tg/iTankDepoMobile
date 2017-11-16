
Partial Class UserControls_ActionPane
    Inherits System.Web.UI.UserControl

#Region "Declarations"
    Public onClientAction As String
    Public TabIndex As String = 0
    Private pvt_lngItemNo As Long
    Private objHasTbl As Hashtable
    Private pvt_intActivityId As Integer
    Private Const ITEMNO As String = "itemno"

    Private objCommonData As New CommonData
#End Region

#Region "Event Action Pane Load"
    ''' <summary>
    ''' This event will be invoked on every action pane load to initialize the workspace in postback mode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ActionPaneLoad(ByVal sender As Object, ByVal e As System.EventArgs)
#End Region

#Region "Get Page Attribute"
    ''' <summary>
    ''' This method will be used for getting data from list grid
    ''' </summary>
    ''' <param name="bv_KEY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pub_GetPageAttribute(ByVal bv_KEY As String) As String
        If Not objHasTbl Is Nothing AndAlso objHasTbl.ContainsKey(bv_KEY) Then
            If Not IsDBNull(objHasTbl.Item(bv_KEY)) Then
                pub_GetPageAttribute = objHasTbl.Item(bv_KEY)
            Else
                pub_GetPageAttribute = Nothing
            End If
        Else
            pub_GetPageAttribute = Nothing
        End If
    End Function
#End Region

#Region "Page Load"
    ''' <summary>
    ''' This event will be fired on everytime when action pane loads.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            datActionDate.Validator.ValueToCompare = Now.ToString("dd-MMM-yyyy").ToUpper
            If Not Page.IsPostBack Then
                If Request.QueryString(ITEMNO) <> "" Then
                    pvt_lngItemNo = Request.QueryString(ITEMNO)
                End If
                If Request.QueryString("activityid") <> "" Then
                    pvt_intActivityId = Request.QueryString("activityid")
                End If
                If pvt_lngItemNo >= 0 AndAlso Request.QueryString(ITEMNO) <> "" Then
                    objHasTbl = objCommonData.GetSelectedRow(pvt_lngItemNo)
                End If

                If objHasTbl Is Nothing Then
                    objHasTbl = New Hashtable
                End If

                hdnPageMode.Value = Request.QueryString("mode")
                objHasTbl.Add("pagetitle", Request.QueryString("pagetitle"))
                datActionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()
                hdnActionDate.value = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()
                RaiseEvent ActionPaneLoad(sender, e)

                'Generating WFDATA
                Page.ClientScript.RegisterHiddenField("WFDATA", objCommonData.GenerateWFData(pvt_intActivityId))
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, ex)
        End Try
    End Sub

#End Region


End Class
