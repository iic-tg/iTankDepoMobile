
Partial Class Operations_EquipmentInspectionExport
    Inherits Framebase

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Request.QueryString("Export") Is Nothing Then
                If Request.QueryString("Export") = "True" Then
                    Dim objCommon As New CommonData
                    Dim objstream As New IO.StringWriter
                    Dim dtEquipmentInspection As DataTable = Nothing

                    'pageModeForExport parameter is passed from exportToExcelWF method in EquipmentInspection.js
                    If Request.QueryString("pageModeForExport") = "pending" Then
                        dtEquipmentInspection = RetrieveData("EQUIP_INSPECTION").Tables(EquipmentInspectionData._V_EQUIPMENT_INSPECTION)
                        objstream = objCommon.ExportToExcelByPassingDataTable(dtEquipmentInspection, "Inspection", "CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_CD,EQPMNT_CD_CD,GTN_DT,AGNT_CD,EIR_NO,BLL_ID,GRD_CD,EQPMNT_STTS_CD,YRD_LCTN,INSPCTD_BY,INSPCTD_DT,RMRKS_VC", "EQPMNT_INSPCTN_ID", True)
                    ElseIf Request.QueryString("pageModeForExport") = "mysubmits" Then
                        dtEquipmentInspection = RetrieveData("EQUIP_INSPECTION").Tables(EquipmentInspectionData._EQUIPMENT_INSPECTION)
                        objstream = objCommon.ExportToExcelByPassingDataTable(dtEquipmentInspection, "Inspection", "CSTMR_CD,EQPMNT_NO,EQPMNT_TYP_CD,EQPMNT_CD_CD,GTN_DT,AGNT_CD,EIR_NO,BLL_ID,GRD_CD,EQPMNT_STTS_CD,YRD_LCTN,INSPCTD_BY,INSPCTD_DT,RMRKS_VC", "EQPMNT_INSPCTN_ID DESC", True)
                    End If

                    Response.ContentType = "application/vnd.Excel"
                    Response.AppendHeader("Content-Disposition", "attachment; filename=Inspection.xls")
                    Response.Write(objstream.ToString())
                    Response.End()
                End If
            End If
        Catch ex As Exception
            iErrorHandler.pub_WriteErrorLog(Me.GetType.Name, Reflection.
                                          MethodBase.GetCurrentMethod.Name, ex)
        End Try

    End Sub
End Class
