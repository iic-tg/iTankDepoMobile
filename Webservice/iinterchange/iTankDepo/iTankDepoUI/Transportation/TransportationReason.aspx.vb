﻿
Partial Class Transportation_TransportationReason
    Inherits Pagebase

#Region "Page_Load1"
    Protected Sub Page_Load1(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack And Not Page.IsCallback Then
                SetFocus(txtReason)
                pvt_SetChangesMade()
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region

#Region "pvt_SetChangesMade"
    Private Sub pvt_SetChangesMade()
        Try
            CommonWeb.pub_AttachHasChanges(txtReason)
            CommonWeb.pub_AttachDescMaxlength(txtReason, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Page_PreRender1"
    Protected Sub Page_PreRender1(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try

        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection. _
                                      MethodBase.GetCurrentMethod.Name, ex.Message)
        End Try
    End Sub
#End Region


End Class
