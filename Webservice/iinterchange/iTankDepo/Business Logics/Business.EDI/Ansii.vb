Imports System.Globalization
Imports iInterchange.iTankDepo.GatewayFramework
Imports iInterchange.iTankDepo.Data
Imports iInterchange.iTankDepo.Entities
Imports System.Text
Imports iInterchange.iTankDepo.DataAccess

Public Class AnsiiEngine

#Region "Declarations "

    Dim strdate As String
    Dim sno As String
    Dim currdate As Date = Now
    Dim intCount As Integer = 0

    Dim strOutFolderName As String
    Dim strTempFolderName As String
    Dim strFileExtension As String

    Dim strResult As String = "" ' 

    Dim intnoofcount As Integer
    Dim intgicount As Integer
    Dim intgocount As Integer
    Dim intwmcount As Integer

    Dim intGATEINCount As Integer
    Dim intGATOUTCount As Integer
    Dim intWESTIMCount As Integer
    Dim intWESTIMDTCount As Integer
    Dim strLsr_Owner As String
    Dim Pvt_Reconnect As Integer
    Dim Pvt_Time As Integer

    Public strStatusMsg As String = "Please Wait..."
    Public _Settings As Settings = New Settings


    Dim _GIContainerNo As StringBuilder
    Dim _GOContainerNo As StringBuilder
    Dim _WMContainerNo As StringBuilder
    Dim _WDContainerNo As StringBuilder
    Dim _strTraceExp As StringBuilder


    ' bolRetrievAfter10Mts is to check whether reconnect is activated in configuration 
    Dim bolRetrievAfter10Mts As Boolean

    ' bolSendMail variable is to check whether any retrieval record is there to send mail
    Dim bolSendMail As Boolean
    Dim bolIsReconnection As Boolean
    Dim bolIsFileExist As Boolean

    Const pvt_UNICODEREPLACEMENTVAL = " "
    Private bv_strCustomerCode As String
    Private bv_strDepotCode As String
#End Region

#Region "New()"
    Public Sub New(ByVal strCustomerCode As String, ByVal strDepotCode As String)
        bv_strCustomerCode = strCustomerCode
        bv_strDepotCode = strDepotCode
    End Sub
#End Region

#Region "Methods"


#Region "GetFiles"

    Public Function pub_AsciicodeValidation(ByVal bv_strString As String) As String
        Try
            Dim lngAscii As Long, intLen As Integer, intLoopVal As Integer

            If bv_strString Is Nothing = False Then
                intLen = bv_strString.Length - 1
                For intLoopVal = 0 To intLen
                    Try
                        'AscW returns the Unicode code point for the input character. This can be 0 through 65535
                        lngAscii = AscW(bv_strString.Substring(intLoopVal, 1))
                        'If lngAscii < 28 Or lngAscii > 127 Then
                        If lngAscii < 33 Or lngAscii > 127 Then
                            bv_strString = bv_strString.Replace(bv_strString.Substring(intLoopVal, 1), pvt_UNICODEREPLACEMENTVAL)
                        End If
                    Catch ex As Exception   'the character range is above 65535 then error raised. simply replace the replacement value
                        bv_strString = bv_strString.Replace(bv_strString.Substring(intLoopVal, 1), pvt_UNICODEREPLACEMENTVAL)
                    End Try
                Next
            End If
            Return bv_strString
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region


    Public Function ReportEstimateUpdate(ByVal bv_strwestimSno As String, ByVal bv_strwestimTrNo As String) As DataTable

        Try
            Dim strWestim As String = Nothing
            Dim strWestimDt As String = Nothing
            If bv_strwestimSno <> "" Then
                strWestim = bv_strwestimSno.Substring(0, bv_strwestimSno.Length - 1)
            End If
            If bv_strwestimTrNo <> "" Then
                strWestimDt = bv_strwestimTrNo.Substring(0, bv_strwestimTrNo.Length - 1)
            End If
            Dim dtWestimDetailRet As DataTable
            If bv_strwestimTrNo <> "" Then
                Dim objEDis As New EDIs
                dtWestimDetailRet = objEDis.GetRepairEstimateDetailRetBySNO(strWestim, "U")
                Return dtWestimDetailRet
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "WriteGateinFile()"
    Function WriteGatinFile(ByVal bv_DepotCode As String, ByVal bv_strGenerationMode As String, ByVal bv_strTimeStamp As String) As String
        strResult = ""
        Dim strUpdateGatein As New StringBuilder
        Dim recGateinlog As New StringBuilder
        _GIContainerNo = New StringBuilder
        Dim strGateInTrace As New StringBuilder
        Dim strgatein As New StringBuilder
        Dim dtGatein As New DataTable
        Dim strUpdateGateinTransNo As New StringBuilder
        Dim objWriteEDI As New WriteEDI(bv_strCustomerCode, bv_DepotCode)
        '  Dim objWriteEDI As New WriteEDI(objAnsiiLessor())

        Dim intRowsCount As Integer = 0
        Try
            Dim str As String = String.Concat("GetGateinData : ", Now.ToString, " - Started")
            strGateInTrace.AppendLine(String.Concat("GetGateinData : ", Now.ToString, " - Started"))
            If bv_strTimeStamp = "AUTO" Then
                dtGatein = getGateinData(bv_DepotCode)
            Else
                Dim strActivity() As String = bv_strTimeStamp.Split(",")
                dtGatein = getGateinData(bv_DepotCode, bv_strTimeStamp)
            End If

            strGateInTrace.AppendLine(String.Concat("GetGateinData : ", Now.ToString, " - Ended"))

            intGATEINCount = 0

            If dtGatein.Rows.Count = 0 Then
                strResult = "No Gate In Records found for EDI generation"
                Return strResult
            End If

            For Each drGateIn As DataRow In dtGatein.Rows
                With drGateIn
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMPLETE, 1)), "L", "T"))
                        strGateInTrace.AppendLine(String.Concat("Gatein Unit No : ", Now.ToString, " - ", pvt_GetValue(drGateIn, EDIData.GI_UNIT_NBR, 4)))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMPLETE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_EIR_DATE, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EIR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SENT_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SENT_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(8))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_REC_EIR, 1)), "L", "T"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_REC_EIR, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_REC_DATE, 8)), 8)))

                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_REC_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(8))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_REC_ADDR, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_REC_ADDR, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_REC_TYPE, 1)), 1))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_REC_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_EXPORTED, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EXPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_EXPOR_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EXPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(8))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_IMPORTED, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_IMPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_IMPOR_DATE, 8)), 8)))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_IMPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(8))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_TRNSXN, 14)), 14))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_TRNSXN, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(14))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_ADVICE, 14)), 14))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_ADVICE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(14))
                    End Try
                    Try
                        strgatein.Append(pub_fill(Pub_Unit_Id_A(pvt_GetValue(drGateIn, EDIData.GI_UNIT_NBR, 4)), 4))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat("GI_UNIT_ID_A : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(4))
                    End Try
                    Try
                        strgatein.Append(pub_fill(Pub_Unit_Id_N(pvt_GetValue(drGateIn, EDIData.GI_UNIT_NBR, 4)), 6))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat("GI_UNIT_ID_N : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(6))
                    End Try
                    Try
                        strgatein.Append(pub_fill(Pub_Unit_Id_C(pvt_GetValue(drGateIn, EDIData.GI_UNIT_NBR, 4)), 1))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat("GI_UNIT_ID_C : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_EQUIP_TYPE, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EQUIP_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_EQUIP_DESC, 30)), 30))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EQUIP_DESC, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(30))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_EQUIP_CODE, 4)), 4))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EQUIP_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(4))
                    End Try
                    Try
                        strgatein.Append(pub_alignright_forstatus(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_CONDITION, 10)), 10))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_CONDITION, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(10))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_A, 4)), 4))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_ID_A, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(4))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_N, 6)), 6))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_ID_N, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(6))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_C, 1)), 1))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_ID_C, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_TYPE, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_DESC, 30)), 30))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_DESC, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(30))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_CODE, 4)), 4))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(4))
                    End Try
                    Try
                        strgatein.Append(RemoveZeros(pub_fill(Pub_ConvertDatetoString(.Item(EDIData.GI_EIR_DATE)), 8)))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EIR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(8))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_EIR_TIME, 5)), 5))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_EIR_TIME, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(5))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_REFERENCE, 9))), 35))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_REFERENCE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(35))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_MANU_DATE, 5)), 5))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_MANU_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(5))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_MATERIAL, 2)), 2))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(2))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_WEIGHT, 10)), 10))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_WEIGHT, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(10))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_MEASURE, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_MEASURE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_UNITS, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_UNITS, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_CSC_REEXAM, 5)), 5))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_CSC_REEXAM, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(5))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COUNTRY, 2)), 2))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COUNTRY, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(2))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LIC_STATE, 2)), 2))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LIC_STATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(2))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LIC_REG, 8)), 8))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LIC_REG, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(8))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LIC_EXPIRE, 5)), 5))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LIC_EXPIRE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(5))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LSR_OWNER, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LSR_OWNER, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_C, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_1, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_REFERENCE, 35)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SSL_LSE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_C, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_HAULIER, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_HAULIER, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_C, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_3, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_DPT_TRM, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_DPT_TRM, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEND_EDI_4, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_4, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_OTHERS_1, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_OTHERS_1, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_OTHERS_2, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_OTHERS_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_OTHERS_3, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_OTHERS_3, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_OTHERS_4, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_OTHERS_4, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_NOTE_1, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_NOTE_1, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_NOTE_2, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_NOTE_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LOAD, 1)), 1))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LOAD, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_FHWA, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_FHWA, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LAST_OH_LOC, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LAST_OH_LOC, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LAST_OH_DATE, 8)), 8)))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LAST_OH_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(8))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SENDER, 15)), 15))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SENDER, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(15))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_ATTENTION, 15)), 15))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_ATTENTION, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(15))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_REVISION, 1)), 1))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_REVISION, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEND_EDI_5, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_5, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEND_EDI_6, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_6, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEND_EDI_7, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_7, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEND_EDI_8, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEND_EDI_8, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_PARTY_1, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_PARTY_1, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_NUMBER_1, 15)), 15))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_NUMBER_1, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(15))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_PARTY_2, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_PARTY_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_NUMBER_2, 15)), 15))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_NUMBER_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(15))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_PARTY_3, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_PARTY_3, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_NUMBER_3, 15)), 15))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_NUMBER_3, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(15))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_PARTY_4, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_PARTY_4, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEAL_NUMBER_4, 15)), 15))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEAL_NUMBER_4, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(15))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_PORT_FUNC_CODE, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_PORT_FUNC_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_PORT_NAME, 24)), 24))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_PORT_NAME, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(24))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_VESSEL_NAME, 35)), 35))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_VESSEL_NAME, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(35))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_VOYAGE_NUM, 17)), 17))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_VOYAGE_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(17))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_HAZ_MAT_CODE, 10)), 10))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_HAZ_MAT_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(10))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_HAZ_MAT_DESC, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_HAZ_MAT_DESC, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_NOTE_3, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_NOTE_3, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_NOTE_4, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_NOTE_4, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_NOTE_5, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_NOTE_5, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_A_2, 4)), 4))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_ID_A_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(4))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_N_2, 6)), 6))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_ID_N_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(6))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_ID_C_2, 1)), 1))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_ID_C_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_TYPE_2, 3)), 3))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_TYPE_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(3))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_CODE_2, 4)), 4))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_CODE_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(4))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_COMP_DESC_2, 30)), 30))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_COMP_DESC_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(30))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SHIPPER, 35)), 35))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SHIPPER, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(35))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_DRAY_ORDER, 35)), 35))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_DRAY_ORDER, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(35))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_RAIL_ID, 17)), 17))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_RAIL_ID, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(17))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_RAIL_RAMP, 17)), 17))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_RAIL_RAMP, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(17))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_VESSEL_CODE, 9)), 9))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_VESSEL_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(9))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_WGHT_CERT_1, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_WGHT_CERT_1, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_WGHT_CERT_2, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_WGHT_CERT_2, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_WGHT_CERT_3, 70)), 70))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_WGHT_CERT_3, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(70))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_SEA_RAIL, 1)), 1))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_SEA_RAIL, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(1))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_LOC_IDENT, 25)), 25))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_LOC_IDENT, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(25))
                    End Try
                    Try
                        strgatein.Append(pub_fill(pub_ucase(pvt_GetValue(drGateIn, EDIData.GI_PORT_LOC_QUAL, 2)), 2))
                    Catch ex As Exception
                        strGateInTrace.AppendLine(String.Concat(EDIData.GI_PORT_LOC_QUAL, " : ", Now.ToString, " - ", ex.Message))
                        strgatein.Append(pub_space(2))
                    End Try

                    If Not intRowsCount = dtGatein.Rows.Count - 1 Then
                        strgatein.Append(vbCrLf)
                    End If
                    intRowsCount += 1
                    intGATEINCount += 1
                    intCount = intCount + 1

                    recGateinlog.Append(pub_fill(intCount, 4) & " ")
                    recGateinlog.Append(pub_fill(pvt_GetValue(drGateIn, EDIData.GI_DPT_TRM, 9), 9) & " ")
                    recGateinlog.Append(pub_fill(pvt_GetValue(drGateIn, EDIData.GI_TRNSXN, 14), 14) & " ")
                    recGateinlog.Append(pvt_GetValue(drGateIn, EDIData.GI_UNIT_NBR, 4) & " ")

                    recGateinlog.Append(.Item(EDIData.GI_TRANSMISSION_NO).ToString() & " " & "GATEIN ")
                    recGateinlog.Append(vbCrLf)
                    _GIContainerNo.Append(pvt_GetValue(drGateIn, EDIData.GI_UNIT_NBR, 4) & ";")

                    strUpdateGatein.Append(.Item(EDIData.GI_SNO).ToString())
                    strUpdateGatein.Append(",")

                    'strUpdateGateinTransNo.Append("'")
                    'strUpdateGateinTransNo.Append(.Item(EDIData.GI_TRANSMISSION_NO).ToString())
                    strUpdateGateinTransNo.Append(.Item(EDIData.GI_SNO).ToString())
                    strUpdateGateinTransNo.Append(",")
                    'strUpdateWestimtr.Append("',")

                End With
            Next

            objWriteEDI.pub_CreateActivityReceivelog(recGateinlog.ToString, "")

            If Not strgatein.ToString = "" Then
                objWriteEDI.pub_WriteSpecificFile("Gatein", strgatein, bv_strGenerationMode)
            End If

            Dim strTimestamp As String = objWriteEDI.pub_RenameFilesToExtension()
            strGateInTrace.AppendLine(String.Concat("Gatein Updation Process", " : ", Now.ToString, " - ", "Started"))
            'surya - check the flow
            ReportGateinUpdate(strUpdateGatein.ToString(), strUpdateGateinTransNo.ToString)
            strGateInTrace.AppendLine(String.Concat("Gatein Updation Process", " : ", Now.ToString, " - ", "Ended"))

            objWriteEDI.pub_WriteText(strGateInTrace, ".txt")
            strResult = String.Concat("Gate In EDI file generated successfully.", ",", strTimestamp)
            bv_strTimeStamp = strTimestamp
            Return strResult
        Catch GateinWritrex As Exception
            _strTraceExp.Clear()
            _strTraceExp.AppendLine(String.Concat("WriteGatinFile", " : ", Now.ToString, " - ", GateinWritrex.Message))
            objWriteEDI.pub_WriteText(_strTraceExp, ".txt")
            _strTraceExp.Clear()
            'CreateGateinTrace("Exception Raised from the Gatein Writing Process Of Data Retrieval", GateinWritrex.Message.ToString)
            Return "Gate In EDI file generation failed."
        End Try
    End Function

#End Region

#Region "WriteGateoutFile()"
    Public Function WriteGateoutFile(ByVal bv_DepotCode As String, ByVal bv_strGenerationMode As String) As String
        strResult = ""
        Dim strGateOutTrace As New StringBuilder
        Dim dtGateOut As New DataTable
        Dim strgateout As New StringBuilder
        Dim strUpdateGateout As New StringBuilder
        Dim strUpdateGateTransNo As New StringBuilder
        Dim recGateoutlog As New StringBuilder
        Dim intRowsCount As Integer = 0
        Dim objWriteEDI As New WriteEDI(bv_strCustomerCode, bv_DepotCode)

        Try
            strGateOutTrace.AppendLine(String.Concat("GetGateOutData : ", Now.ToString, " - Started"))
            dtGateOut = getGateoutData(bv_DepotCode)
            strGateOutTrace.AppendLine(String.Concat("GetGateOutData : ", Now.ToString, " - Ended"))

            _GOContainerNo = New StringBuilder
            Dim i As Integer

            intGATOUTCount = 0

            If dtGateOut.Rows.Count = 0 Then
                strResult = "No Gate out Records found for EDI generation"
                Return strResult
            End If

            strGateOutTrace.AppendLine(String.Concat("GetGateOutData : ", Now.ToString, " - Loop Starts"))

            For Each drGateOut As DataRow In dtGateOut.Rows
                With drGateOut
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMPLETE, 1)), "L", "T"))
                        strGateOutTrace.AppendLine(String.Concat("Gateout Unit No :", Now.ToString, " - ", .Item(EDIData.GO_UNIT_NBR).ToString()))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMPLETE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SENT_EIR, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SENT_EIR, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try
                    Try
                        strgateout.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SENT_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SENT_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(8))
                    End Try
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_REC_EIR, 1)), "L", "T"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_REC_EIR, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try
                    Try
                        strgateout.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_REC_DATE, 8)), 8)))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_REC_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(8))
                    End Try
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_REC_ADDR, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_REC_ADDR, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_REC_TYPE, 1)), 1))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_REC_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_EXPORTED, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_EXPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_EXPOR_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_EXPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(8))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_IMPORTED, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_IMPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try

                        strgateout.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_IMPOR_DATE, 8)), 8)))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_IMPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(8))
                    End Try
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_TRNSXN, 14)), 14))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_TRNSXN, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(14))
                    End Try
                    Try
                        strgateout.Append(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_ADVICE, 14)))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_ADVICE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(14))
                    End Try

                    Try
                        strgateout.Append(pub_fill(Pub_Unit_Id_A(.Item(EDIData.GO_UNIT_NBR).ToString), 4))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat("GO_UNIT_ID_A : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(4))
                    End Try

                    Try
                        strgateout.Append(pub_fill(Pub_Unit_Id_N(.Item(EDIData.GO_UNIT_NBR).ToString), 6))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat("GO_UNIT_ID_N : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(6))
                    End Try
                    Try
                        strgateout.Append(pub_fill(Pub_Unit_Id_C(.Item(EDIData.GO_UNIT_NBR).ToString), 1))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat("GO_UNIT_ID_C : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try
                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_EQUIP_TYPE, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_EQUIP_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_EQUIP_DESC, 30)), 30))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_EQUIP_DESC, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(30))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_EQUIP_CODE, 4)), 4))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_EQUIP_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(4))
                    End Try

                    Try
                        strgateout.Append(pub_alignright_forstatus(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_CONDITION, 10)), 10))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_CONDITION, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(10))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_ID_A, 4)), 4))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_ID_A, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(4))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_ID_N, 6)), 6))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_ID_N, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(6))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_ID_C, 1)), 1))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_ID_C, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_TYPE, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_DESC, 30)), 30))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_DESC, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(30))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_CODE, 4)), 4))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(4))
                    End Try

                    Try
                        strgateout.Append(RemoveZeros(pub_fill(Pub_ConvertDatetoString(.Item(EDIData.GO_EIR_DATE)), 8)))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_EIR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(8))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pvt_GetValue(drGateOut, EDIData.GO_EIR_TIME, 5), 5))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_EIR_TIME, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(5))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_REFERENCE, 35))), 35))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_REFERENCE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(35))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_MANU_DATE, 5)), 5))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_MANU_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(5))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_MATERIAL, 2)), 2))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(2))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_WEIGHT, 10)), 10))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_WEIGHT, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(10))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_MEASURE, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_MEASURE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_UNITS, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_UNITS, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_CSC_REEXAM, 5)), 5))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_CSC_REEXAM, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(5))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COUNTRY, 2)), 2))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COUNTRY, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(2))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LIC_STATE, 2)), 2))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LIC_STATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(2))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LIC_REG, 8)), 8))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LIC_REG, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(8))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LIC_EXPIRE, 5)), 5))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LIC_EXPIRE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(5))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LSR_OWNER, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LSR_OWNER, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_1, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_1, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SSL_LSE, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SSL_LSE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_2, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_HAULIER, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_HAULIER, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_3, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_3, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_DPT_TRM, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_DPT_TRM, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_4, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_4, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_OTHERS_1, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_OTHERS_1, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_OTHERS_2, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_OTHERS_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_OTHERS_3, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_OTHERS_3, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_OTHERS_4, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_OTHERS_4, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_NOTE_1, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_NOTE_1, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_NOTE_2, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_NOTE_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LOAD, 1)), 1))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LOAD, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_FHWA, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_FHWA, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LAST_OH_LOC, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LAST_OH_LOC, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LAST_OH_DATE, 8)), 8)))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LAST_OH_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(8))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SENDER, 15)), 15))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SENDER, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(15))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_ATTENTION, 15)), 15))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_ATTENTION, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(15))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_REVISION, 1)), 1))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_REVISION, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_5, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_5, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_6, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_6, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_7, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_7, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEND_EDI_8, 1)), "L", "F"))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEND_EDI_8, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_PARTY_1, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_PARTY_1, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_NUMBER_1, 15)), 15))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_NUMBER_1, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(15))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_PARTY_2, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_PARTY_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_NUMBER_2, 15)), 15))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_NUMBER_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(15))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_PARTY_3, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_PARTY_3, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_NUMBER_3, 15)), 15))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_NUMBER_3, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(15))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_PARTY_4, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_PARTY_4, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEAL_NUMBER_4, 15)), 15))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEAL_NUMBER_4, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(15))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_PORT_FUNC_CODE, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_PORT_FUNC_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_PORT_NAME, 24)), 24))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_PORT_NAME, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(24))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_VESSEL_NAME, 35)), 35))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_VESSEL_NAME, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(35))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_VOYAGE_NUM, 17)), 17))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_VOYAGE_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(17))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_HAZ_MAT_CODE, 10)), 10))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_HAZ_MAT_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(10))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_HAZ_MAT_DESC, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_HAZ_MAT_DESC, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_NOTE_3, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_NOTE_3, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_NOTE_4, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_NOTE_4, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_NOTE_5, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_NOTE_5, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try


                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_ID_A_2, 4)), 4))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_ID_A_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(4))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_ID_N_2, 6)), 6))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_ID_N_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(6))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_ID_C_2, 1)), 1))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_ID_C_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_TYPE_2, 3)), 3))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_TYPE_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(3))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_CODE_2, 4)), 4))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_CODE_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(4))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_COMP_DESC_2, 30)), 30))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_COMP_DESC_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(30))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SHIPPER, 35)), 35))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SHIPPER, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(35))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_DRAY_ORDER, 35)), 35))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_DRAY_ORDER, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(35))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_RAIL_ID, 17)), 17))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_RAIL_ID, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(17))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_RAIL_RAMP, 17)), 17))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_RAIL_RAMP, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(17))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_VESSEL_CODE, 9)), 9))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_VESSEL_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(9))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_WGHT_CERT_1, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_WGHT_CERT_1, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_WGHT_CERT_2, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_WGHT_CERT_2, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_WGHT_CERT_3, 70)), 70))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_WGHT_CERT_3, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(70))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_SEA_RAIL, 1)), 1))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_SEA_RAIL, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(1))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_BILL_LADING, 35)), 35))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_BILL_LADING, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(35))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_LOC_IDENT, 25)), 25))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_LOC_IDENT, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(25))
                    End Try

                    Try
                        strgateout.Append(pub_fill(pub_ucase(pvt_GetValue(drGateOut, EDIData.GO_PORT_LOC_QUAL, 2)), 2))
                    Catch ex As Exception
                        strGateOutTrace.AppendLine(String.Concat(EDIData.GO_PORT_LOC_QUAL, " : ", Now.ToString, " - ", ex.Message))
                        strgateout.Append(pub_space(2))
                    End Try

                    If Not intRowsCount = dtGateOut.Rows.Count - 1 Then
                        strgateout.Append(vbCrLf)
                    End If
                    intRowsCount += 1

                    intGATOUTCount += 1
                    intCount = intCount + 1

                    strUpdateGateout.Append(.Item(EDIData.GO_SNO).ToString)
                    strUpdateGateout.Append(",")

                    'strUpdateGateTransNo.Append(.Item(EDIData.GO_TRANSMISSION_NO).ToString)
                    strUpdateGateTransNo.Append(.Item(EDIData.GO_SNO).ToString)
                    strUpdateGateTransNo.Append(",")

                    recGateoutlog.Append(pub_fill(intCount, 4) & " ")
                    recGateoutlog.Append(pub_fill(pvt_GetValue(drGateOut, EDIData.GO_DPT_TRM, 9), 9) & " ")
                    recGateoutlog.Append(pub_fill(pvt_GetValue(drGateOut, EDIData.GO_TRNSXN, 14), 14) & " ")
                    recGateoutlog.Append(.Item(EDIData.GO_UNIT_NBR).ToString & " ")
                    recGateoutlog.Append(.Item(EDIData.GO_TRANSMISSION_NO).ToString & " " & "GATEOUT")
                    recGateoutlog.Append(vbCrLf)
                    _GOContainerNo.Append(.Item(EDIData.GO_UNIT_NBR).ToString & ";")
                End With
            Next
            strGateOutTrace.AppendLine(String.Concat("GateOutData : ", Now.ToString, " - Loop Ends"))

            objWriteEDI.pub_CreateActivityReceivelog(recGateoutlog.ToString, "")

            strGateOutTrace.AppendLine(String.Concat("GateOutData : ", Now.ToString, " - Writing Started"))

            If Not strgateout.ToString = "" Then
                objWriteEDI.pub_WriteSpecificFile("Gateout", strgateout, bv_strGenerationMode)
            End If
            'need to chnage
            Dim strTimestamp As String = objWriteEDI.pub_RenameFilesToExtension()
            strGateOutTrace.AppendLine(String.Concat("GateOutData : ", Now.ToString, " - Writing Ended"))
            strGateOutTrace.AppendLine(String.Concat("GateOutData : ", Now.ToString, " - Updation Started"))

            ReportGateoutUpdate(strUpdateGateout.ToString(), strUpdateGateTransNo.ToString())
            strGateOutTrace.AppendLine(String.Concat("GateOutData : ", Now.ToString, " - Updation Ended"))
            objWriteEDI.pub_WriteText(strGateOutTrace, ".txt")
            strResult = String.Concat("Gate Out EDI  file generated successfully.", ",", strTimestamp)
            ' strResult = "Gate Out EDI  file generated successfully."
            Return strResult
        Catch WriteGateoutex As Exception
            'CreateGateoutTrace("Exception Raised from the Gateout Writing Process of Data Retrieval", WriteGateoutex.Message.ToString)
            _strTraceExp.Clear()
            _strTraceExp.AppendLine(String.Concat("WriteGateOutFile", " : ", Now.ToString, " - ", WriteGateoutex.Message))
            objWriteEDI.pub_WriteText(_strTraceExp, ".txt")
            _strTraceExp.Clear()
            Return "Gate Out EDI  file generation failed."
        End Try
    End Function
#End Region


#Region "WriteWestimFile()"
    Public Function WriteWestimFile(ByVal bv_DepotCode As String, ByVal bv_strGenerationMode As String) As String
        strResult = ""

        Dim strWestimTrace As New StringBuilder
        Dim dtWestim As New DataTable
        Dim intRowsCount As Integer = 0

        Dim strWestim As New StringBuilder
        Dim strUpdateWestim As New StringBuilder
        Dim strUpdateWestimtr As New StringBuilder
        Dim recWestimlog As New StringBuilder

        _WMContainerNo = New StringBuilder
        Dim objWriteEdi As New WriteEDI(bv_strCustomerCode, bv_DepotCode)
        Dim strDetailTrace As New StringBuilder
        Dim strmsg As String()

        Try
            Try
                strWestimTrace.AppendLine(String.Concat("GetEstimateData : ", Now.ToString, " - Started"))
                dtWestim = getEstimateData(bv_DepotCode)
                strWestimTrace.AppendLine(String.Concat("GetEstimateData : ", Now.ToString, " - Ended"))

            Catch ex As Exception
                strWestimTrace.AppendLine(String.Concat("GetEstimateData FAILED- ESTIMATE NOT THRO - REASON : ", Now.ToString, " - ", ex.Message.ToString))

            End Try

            Dim i As Integer

            intWESTIMCount = 0

            If dtWestim.Rows.Count = 0 Then
                strResult = "No Estimate Records found for EDI generation"
                Return strResult
            End If
            strWestimTrace.AppendLine(String.Concat("GetEstimate : ", Now.ToString, " - Loop Started"))

            For Each drWestim As DataRow In dtWestim.Rows
                With drWestim
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_COMPLETE, 1)), "L", "T"))
                        strWestimTrace.AppendLine(String.Concat("Estimate Unit No : ", Now.ToString, " - ", pvt_GetValue(drWestim, EDIData.WM_UNIT_NBR, 4)))

                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_COMPLETE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SENT_EIR, 1)), "L", "F"))

                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SENT_EIR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try
                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SENT_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SENT_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_REC_EIR, 1)), "L", "T"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_REC_EIR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_REC_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_REC_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_REC_ADDR, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_REC_ADDR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_REC_TYPE, 1)), 1))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_REC_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_EXPORTED, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_EXPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_EXPOR_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_EXPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_IMPORTED, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_IMPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_IMPOR_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_IMPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_leftalign(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_TRNSXN, 14)), 14))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_TRNSXN, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(14))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_PTY_RSPONS, 1)), 1))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_PTY_RSPONS, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertIntegertoString(.Item(EDIData.WM_REVISION)), 1))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_REVISION, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(Pub_ConvertDatetoString(.Item(EDIData.WM_ESTIM_DATE)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_ESTIM_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_Unit_Id_A(pvt_GetValue(drWestim, EDIData.WM_UNIT_NBR, 4)), 4))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat("WM_UNIT_ID_A : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(4))
                    End Try
                    Try
                        strWestim.Append(pub_fill(Pub_Unit_Id_N(pvt_GetValue(drWestim, EDIData.WM_UNIT_NBR, 4)), 6))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat("WM_UNIT_ID_N : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(6))
                    End Try
                    Try
                        strWestim.Append(pub_fill(Pub_Unit_Id_C(pvt_GetValue(drWestim, EDIData.WM_UNIT_NBR, 4)), 1))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat("WM_UNIT_ID_C : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_REFERENCE, 35))), 35))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_REFERENCE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(35))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_EQUIP_TYPE, 3)), 3))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_EQUIP_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(3))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_EQUIP_CODE, 4)), 4))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_EQUIP_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(4))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_EQUIP_DESC, 30)), 30))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_EQUIP_DESC, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(30))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_TERM_LOCA, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_TERM_LOCA, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_TERM_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_TERM_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_TERM_TIME, 5)), 5))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_TERM_TIME, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(5))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_LAST_OH_LOC, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_LAST_OH_LOC, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try
                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_LAST_OH_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_LAST_OH_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_alignright_forstatus(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_CONDITION, 8)), 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_CONDITION, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_MANU_DATE, 5)), 5))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_MANU_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(5))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_CSC_REEXAM, 5)), 5))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_CSC_REEXAM, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(5))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_LOAD, 1)), 1))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_LOAD, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SENDER, 15)), 15))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SENDER, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(15))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_ATTENTION, 15)), 15))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_ATTENTION, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(15))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_LSR_OWNER, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_LSR_OWNER, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_1, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_1, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SSL_LSE, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SSL_LSE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_2, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_2, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_HAULIER, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_HAULIER, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_3, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_3, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_DPT_TRM, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_DPT_TRM, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_4, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_4, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_INSURER, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_INSURER, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SURVEYOR, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SURVEYOR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_OTHERS_1, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_OTHERS_1, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Format(ModifyTax_Rate(drWestim, EDIData.WM_TAX_RATE), "0.000"), 6))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_TAX_RATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(6))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_FILLER, 3)), 3))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_FILLER, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(3))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_NOTE_1, 70))), 70))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_NOTE_1, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(70))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_NOTE_2, 70))), 70))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_NOTE_2, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(70))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_NOTE_3, 70))), 70))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_NOTE_3, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(70))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_BAS_CURR, 3)), 3))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_BAS_CURR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(3))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_LABOR_RATE)), 12))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_LABOR_RATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(12))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_DPP_CURR, 3)), 3))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_DPP_CURR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(3))
                    End Try
                    Try
                        strWestim.Append(objWriteEdi.Fill(.Item(EDIData.WM_DPP_AMT), 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_DPP_AMT, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_WEIGHT, 10)), 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_WEIGHT, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_MEASURE, 3)), 3))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_MEASURE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(3))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_UNITS, 3)), 3))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_UNITS, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(3))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_MATERIAL, 2)), 2))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(2))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_U_LABOR)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_U_LABOR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try
                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_U_MATERIAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_U_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_U_HANDLING)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_U_HANDLING, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_U_TAX)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_U_TAX, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_U_TOTAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_U_TOTAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_I_LABOR)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_I_LABOR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_I_MATERIAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_I_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try
                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_I_HANDLING)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_I_HANDLING, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try
                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_I_TAX)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_I_TAX, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_I_TOTAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_I_TOTAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_O_LABOR)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_O_LABOR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_O_MATERIAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_O_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_O_HANDLING)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_O_HANDLING, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_O_TAX)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_O_TAX, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_O_TOTAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_O_TOTAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_D_LABOR)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_D_LABOR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_D_MATERIAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_D_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_D_HANDLING)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_D_HANDLING, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_D_TAX)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_D_TAX, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_D_TOTAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_D_TOTAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_S_LABOR)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_S_LABOR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_S_MATERIAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_S_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_S_HANDLING)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_S_HANDLING, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_S_TAX)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_S_TAX, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_S_TOTAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_S_TOTAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_X_LABOR)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_X_LABOR, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_X_MATERIAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_X_MATERIAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_X_HANDLING)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_X_HANDLING, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_X_TAX)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_X_TAX, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_X_TOTAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_X_TOTAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WM_EST_TOTAL)), "D", 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_EST_TOTAL, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_ADVICE, 14)), 14))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_ADVICE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(14))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_EIR_NUM, 14)), 14))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_EIR_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(14))
                    End Try

                    Try
                        strWestim.Append(pub_leftalign(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_AUTH_NUM, 14)), 14))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_AUTH_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(14))
                    End Try

                    Try
                        strWestim.Append(pub_fill(Pub_ConvertDecimaltoString(pvt_GetValue(drWestim, EDIData.WM_AUTH_AMT, 10)), 10))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_AUTH_AMT, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(10))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_AUTH_PTY, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_AUTH_PTY, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_AUTH_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_AUTH_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_O_ESTIM_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_O_ESTIM_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_OTHERS_2, 9)), 9))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_OTHERS_2, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(9))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_5, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_5, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_6, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_6, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_7, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_7, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SEND_EDI_8, 1)), "L", "F"))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SEND_EDI_8, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_NOTE_4, 70))), 70))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_NOTE_4, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(70))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_NOTE_5, 70))), 70))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_NOTE_5, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(70))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_WEIGHT_2, 7)), 7))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_WEIGHT_2, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(7))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_MEASURE_2, 3)), 3))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_MEASURE_2, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(3))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_INVOICE_TYPE, 2)), 2))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_INVOICE_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(2))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_ODOMETER_HOURS, 6)), 6))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_ODOMETER_HOURS, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(6))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_OUT_SVC_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_OUT_SVC_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_RET_SVC_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_RET_SVC_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try

                    Try
                        strWestim.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_OWN_INSP_DATE, 8)), 8)))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_OWN_INSP_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(8))
                    End Try
                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_MECHANIC_NAME, 25)), 25))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_MECHANIC_NAME, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(25))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_BILLEE_CODE, 15)), 15))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_BILLEE_CODE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(15))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_SUB_REPAIR_TYPE, 1)), 1))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_SUB_REPAIR_TYPE, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(1))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_OUT_SVC_TIME, 5)), 5))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_OUT_SVC_TIME, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(5))
                    End Try

                    Try
                        strWestim.Append(pub_fill(pub_ucase(pvt_GetValue(drWestim, EDIData.WM_RET_SVC_TIME, 3)), 5))
                    Catch ex As Exception
                        strWestimTrace.AppendLine(String.Concat(EDIData.WM_RET_SVC_TIME, " : ", Now.ToString, " - ", ex.Message))
                        strWestim.Append(pub_space(5))
                    End Try


                    If Not intRowsCount = dtWestim.Rows.Count - 1 Then
                        strWestim.Append(vbCrLf)
                    End If
                    intRowsCount += 1

                    intWESTIMCount += 1
                    intCount = intCount + 1
                    intwmcount = intCount
                    strUpdateWestim.Append(.Item(EDIData.WM_SNO).ToString)
                    strUpdateWestim.Append(",")
                    'strUpdateWestimtr.Append("'")

                    ' strUpdateWestimtr.Append(.Item(EDIData.WM_TRANSMISSION_NO).ToString)
                    strUpdateWestimtr.Append(.Item(EDIData.WM_SNO).ToString)
                    'strUpdateWestimtr.Append("',")
                    strUpdateWestimtr.Append(",")

                    recWestimlog.Append(pub_fill(intCount, 4) & " ")
                    recWestimlog.Append(pub_fill(pvt_GetValue(drWestim, EDIData.WM_DPT_TRM, 9), 9) & " ")
                    recWestimlog.Append(pub_fill(pvt_GetValue(drWestim, EDIData.WM_TRNSXN, 14), 14) & " ")
                    recWestimlog.Append(pvt_GetValue(drWestim, EDIData.WM_UNIT_NBR, 4) & " ")

                    recWestimlog.Append(.Item(EDIData.WM_TRANSMISSION_NO).ToString & " " & "WESTIM")
                    recWestimlog.Append(vbCrLf)
                    _WMContainerNo.Append(pvt_GetValue(drWestim, EDIData.WM_UNIT_NBR, 4) & ";")

                End With
            Next

            strWestimTrace.AppendLine(String.Concat("Estimate : ", Now.ToString, " - Loop Ends"))

            Dim writedt As String
            strWestimTrace.AppendLine(String.Concat("Estimate : ", Now.ToString, " - Writing Started"))

            If Not strWestim.ToString = "" Then
                Try
                    Dim dtWestimdetail As New DataTable
                    dtWestimdetail = ReportEstimateUpdate(strUpdateWestim.ToString(), strUpdateWestimtr.ToString())
                    '  If Not dtWestimdetail Is Nothing Then
                    If dtWestimdetail.Rows.Count <> 0 Then
                        strDetailTrace.AppendLine(String.Concat("Estimate Details: ", Now.ToString, " - Writing Started"))
                        writedt = WriteWestimDTFile(dtWestimdetail, strDetailTrace, bv_strGenerationMode)
                        strDetailTrace.AppendLine(String.Concat("Estimate Details: ", Now.ToString, " - Writing Ended"))
                        strmsg = writedt.Split(",")
                        'If writedt = "TRUE" Then
                        If strmsg(0) = "TRUE" Then
                            strWestimTrace.AppendLine(String.Concat("Estimate : ", Now.ToString, " - Writing Started"))
                            objWriteEdi.pub_WriteSpecificFile("Westim", strWestim, bv_strGenerationMode)
                            strWestimTrace.AppendLine(String.Concat("Estimate : ", Now.ToString, " - Writing Ended"))
                            strWestimTrace.AppendLine(String.Concat("Estimate : ", Now.ToString, " - Update Process Started"))

                            ReportWestimTrackUpdate(strUpdateWestim.ToString(), strUpdateWestimtr.ToString())
                            strWestimTrace.AppendLine(String.Concat("Estimate : ", Now.ToString, " - Update Process Ended"))

                            objWriteEdi.pub_CreateActivityReceivelog(recWestimlog.ToString, "")
                        Else
                            intwmcount = 0
                            strWestimTrace.AppendLine(String.Concat("Westim received without Westimdt : ", Now.ToString, " - So westim is not written to the file"))
                        End If
                    End If
                    'need to change
                    '*************
                    Dim strTimestamp As String = objWriteEdi.pub_RenameFilesToExtension()
                    objWriteEdi.pub_WriteText(strWestimTrace, ".txt")
                    objWriteEdi.pub_WriteText(strDetailTrace, ".txt")
                    If dtWestimdetail.Rows.Count <> 0 Then
                        strResult = String.Concat("Repair Estimate EDI file generated successfully.", ",", String.Concat(strTimestamp, ",", strmsg(1)))
                    Else
                        strResult = String.Concat("Repair Estimate EDI file generated successfully.", ",", String.Concat(strTimestamp, ",", ""))
                    End If

                    ' bv_strTimeStamp = strTimestamp
                    Return strResult
                    ' Catch GateinWritrex As Exception
                    '****************
                    'objWriteEdi.pub_RenameFilesToExtension()
                    'objWriteEdi.pub_WriteText(strWestimTrace, ".txt")
                    'objWriteEdi.pub_WriteText(strDetailTrace, ".txt")

                    ' strResult = "Repair Estimate EDI file generated successfully."
                Catch ex As Exception
                    _strTraceExp.Clear()
                    _strTraceExp.AppendLine(String.Concat("WriteWestimFile", " : ", Now.ToString, " - ", ex.Message))
                    objWriteEdi.pub_WriteText(_strTraceExp, ".txt")
                    _strTraceExp.Clear()
                    strResult = "Repair Estimate EDI file generation failed."
                End Try
            End If

            Return strResult
        Catch WriteEstimateEx As Exception
            _strTraceExp.Clear()
            _strTraceExp.AppendLine(String.Concat("WriteWestimFile", " : ", Now.ToString, " - ", WriteEstimateEx.Message))
            objWriteEdi.pub_WriteText(_strTraceExp, ".txt")
            _strTraceExp.Clear()
            Return "Repair Estimate EDI file generation failed."
        End Try

    End Function
#End Region

#Region "WriteWestimDTFile"
    Public Function WriteWestimDTFile(ByRef dtWestimDet As DataTable, ByRef strDetailTrace As StringBuilder, ByVal bv_strGenerationMode As String) As String
        Dim strdtcheck As String
        Try
            Dim strWestimDt As New StringBuilder
            Dim strUpdateWEstimDt As String = Nothing
            Dim strUpdateWEstimDtTrans As New StringBuilder
            Dim strDetailfile As String
            _WDContainerNo = New StringBuilder
            Dim intRowsCount As Integer = 0
            Dim i As Integer
            intWESTIMDTCount = 0

            Dim objWriteEDI As New WriteEDI(bv_strCustomerCode, bv_strDepotCode)

            For Each drWestimdt As DataRow In dtWestimDet.Rows
                With drWestimdt
                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_EXPORTED, 1)), "L", "F"))
                        strDetailTrace.AppendLine(String.Concat("Details Unit No :", Now.ToString, " - ", .Item(EDIData.WD_UNIT_NBR).ToString()))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_EXPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try
                    Try
                        strWestimDt.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_EXPOR_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_EXPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_IMPORTED, 1)), "L", "F"))

                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_IMPORTED, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(RemoveZeros(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_IMPOR_DATE, 8)), "N", 8)))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_IMPOR_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(pub_leftalign(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_TRNSXN, 14)), 14))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_TRNSXN, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(14))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(Pub_ConvertIntegertoString(.Item(EDIData.WD_REVISION)), 1))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_REVISION, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(RemoveZeros(pub_fill(Pub_ConvertDatetoString(.Item(EDIData.WD_ESTIM_DATE)), 8)))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_ESTIM_DATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(Pub_Unit_Id_A(pvt_GetValue(drWestimdt, EDIData.WD_UNIT_NBR, 4)), 4))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat("WM_UNIT_ID_A : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(4))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(Pub_Unit_Id_N(pvt_GetValue(drWestimdt, EDIData.WD_UNIT_NBR, 4)), 6))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat("WM_UNIT_ID_N : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(6))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(Pub_Unit_Id_C(pvt_GetValue(drWestimdt, EDIData.WD_UNIT_NBR, 4)), 1))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat("WM_UNIT_ID_C : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_AsciicodeValidation(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_REFERENCE, 49))), 49))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_REFERENCE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(49))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pvt_GetValue(drWestimdt, EDIData.WD_LABOR_RATE, 15), 15))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_LABOR_RATE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(15))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pvt_GetValue(drWestimdt, EDIData.WD_LINE, 2), 2))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_LINE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(2))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_REPAIR, 2)), 2))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_REPAIR, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(2))
                    End Try
                    Try

                        strWestimDt.Append(pub_fill(pvt_GetValue(drWestimdt, EDIData.WD_REPEATS, 3), 3))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_REPEATS, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(3))
                    End Try
                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_DAMAGE, 2)), 2))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_DAMAGE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(2))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_COMPONENT, 3)), 3))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_COMPONENT, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(3))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_COMP_MATL, 2)), 2))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_COMP_MATL, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(2))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_LOCATION, 4)), 4))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_LOCATION, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(4))
                    End Try

                    Try
                        strWestimDt.Append(objWriteEDI.Fill(.Item(EDIData.WD_LENGTH), 8))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_LENGTH, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(objWriteEDI.Fill(.Item(EDIData.WD_WIDTH), 8))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_WIDTH, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(objWriteEDI.Fill(.Item(EDIData.WD_HEIGHT), 8))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_HEIGHT, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_UNITS, 3)), 3))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_UNITS, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(3))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(Pub_ConvertDecimaltoString(.Item(EDIData.WD_HOURS)), 6))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_HOURS, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(6))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_SCALE, 2)), 2))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_SCALE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(2))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(Pub_ConvertDecimaltoString(ModifyTax_Rate(drWestimdt, EDIData.WD_MAT_COST)), 15))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_MAT_COST, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(15))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_PTY_RSPONS, 1)), 1))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_PTY_RSPONS, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_TAX_RULE, 1)), 1))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_TAX_RULE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_AAR_JOB, 4)), 4))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_AAR_JOB, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(4))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_JOBCODE, 9)), 9))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_JOBCODE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(9))
                    End Try


                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_DMGREP_DESC, 60)), 60))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_DMGREP_DESC, " : ", Now.ToString, " - ", ex.Message))

                        strWestimDt.Append(pub_space(60))
                    End Try


                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_OFF_TIRE_SIZE, 10)), 10))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_OFF_TIRE_SIZE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(10))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_OFF_BRAND, 10)), 10))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_OFF_BRAND, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(10))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_OFF_SERIAL_NUM, 15)), 15))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_OFF_SERIAL_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(15))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_OFF_LOT_NUM, 8)), 8))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_OFF_LOT_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_OFF_TREAD_DEPTH, 1)), 1))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_OFF_TREAD_DEPTH, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_ON_TIRE_SIZE, 1)), 10))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_ON_TIRE_SIZE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(10))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_ON_BRAND, 10)), 10))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_ON_BRAND, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(10))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_ON_SERIAL_NUM, 15)), 15))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_ON_SERIAL_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(15))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_ON_LOT_NUM, 8)), 8))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_ON_LOT_NUM, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_ON_TREAD_DEPTH, 1)), 1))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_ON_TREAD_DEPTH, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_SUPPLYTIRE, 1)), 1))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_SUPPLYTIRE, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(1))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_SUPPLYTIREAMT, 8)), 8))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_SUPPLYTIREAMT, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(8))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_ON_RETREAD_SER, 15)), 15))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_ON_RETREAD_SER, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(15))
                    End Try

                    Try
                        strWestimDt.Append(pub_fill(pub_ucase(pvt_GetValue(drWestimdt, EDIData.WD_OFF_RETREAD_SER, 15)), 15))
                    Catch ex As Exception
                        strDetailTrace.AppendLine(String.Concat(EDIData.WD_OFF_RETREAD_SER, " : ", Now.ToString, " - ", ex.Message))
                        strWestimDt.Append(pub_space(15))
                    End Try


                    If Not intRowsCount = dtWestimDet.Rows.Count - 1 Then
                        strWestimDt.Append(vbCrLf)
                    End If

                    intWESTIMDTCount += 1
                    intRowsCount += 1

                    strUpdateWEstimDt = strUpdateWEstimDt & .Item(EDIData.WD_SNO).ToString & ","

                    ' strUpdateWEstimDtTrans.Append(.Item(EDIData.WD_TRANSMISSION_NO).ToString)
                    'strUpdateWEstimDtTrans.Append(.Item(EDIData.WD_SNO).ToString)
                    strUpdateWEstimDtTrans.Append(.Item(EDIData.WM_SNO).ToString)
                    'strUpdateWestimtr.Append("',")
                    strUpdateWEstimDtTrans.Append(",")

                    _WDContainerNo.Append(pvt_GetValue(drWestimdt, EDIData.WD_UNIT_NBR, 4) & ";")

                End With
            Next

            If Not strWestimDt.ToString = "" Then
                objWriteEDI.pub_WriteSpecificFile("Westimdt", strWestimDt, bv_strGenerationMode)
                strdtcheck = "TRUE"
            Else
                strdtcheck = "FALSE"
            End If
            strDetailfile = objWriteEDI.pub_RenameFilesToExtension()
            ''TRNSMSSN_NO'. is not found in tracking table'
            ReportDetailsUpdate(strUpdateWEstimDt, strUpdateWEstimDtTrans.ToString())
            Return String.Concat(strdtcheck, ",", strDetailfile)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

#Region "function getData()"

#Region "function getGateinData()"
    Public Function getGateinData(ByVal bv_DepotCode As String, ByVal bv_strGICondition As String) As DataTable
        Try
            Dim dtGateinRet As New DataTable
            Dim objEDIs As New EDIs
            Dim strCondition() As String = bv_strGICondition.Split(",")
            If strCondition(0) <> "" And strCondition(1) <> "" Then
                dtGateinRet = objEDIs.GetGateinRetByGI_DPT_TRM(bv_DepotCode.Trim(), "U", bv_strCustomerCode)
            Else
                If strCondition(0) <> "" Then
                    dtGateinRet = objEDIs.GetGateinRetByGI_DPT_TRM(bv_DepotCode.Trim(), "U", bv_strCustomerCode, strCondition(0))
                Else
                    dtGateinRet = objEDIs.GetGateinRetByGI_DPT_TRM(bv_DepotCode.Trim(), "U", bv_strCustomerCode, strCondition(1))

                End If

            End If

            If Not dtGateinRet Is Nothing Then
                ''To reconnect if there is no data
                If bolIsReconnection = False Then
                    If dtGateinRet.Rows.Count > 0 Then
                        bolSendMail = True
                    Else
                        Reconnect()
                    End If
                Else
                    If dtGateinRet.Rows.Count > 0 Then
                        bolIsFileExist = True
                    End If
                End If
                Return dtGateinRet
            Else
                ''To reconnect if there is  any authenticaion error
                Reconnect()
                Throw New Exception("UserName or Password was Wrong")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "function getGateinData()"
    Public Function getGateinData(ByVal bv_DepotCode As String) As DataTable
        Try
            Dim dtGateinRet As New DataTable
            Dim objEDIs As New EDIs
            dtGateinRet = objEDIs.GetGateinRetByGI_DPT_TRM(bv_DepotCode.Trim(), "U", bv_strCustomerCode)
            If Not dtGateinRet Is Nothing Then
                ''To reconnect if there is no data
                If bolIsReconnection = False Then
                    If dtGateinRet.Rows.Count > 0 Then
                        bolSendMail = True
                    Else
                        Reconnect()
                    End If
                Else
                    If dtGateinRet.Rows.Count > 0 Then
                        bolIsFileExist = True
                    End If
                End If
                Return dtGateinRet
            Else
                ''To reconnect if there is  any authenticaion error
                Reconnect()
                Throw New Exception("UserName or Password was Wrong")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "function getGateoutData()"

    Public Function getGateoutData(ByVal bv_DepotCode As String) As DataTable
        Try
            Dim dtGateOutRet As New DataTable
            Dim objEDIs As New EDIs
            dtGateOutRet = objEDIs.GetGateoutRetByGO_DPT_TRM(bv_DepotCode.Trim(), "U", bv_strCustomerCode)

            If Not dtGateOutRet Is Nothing Then

                'To reconnect if there is no data
                If bolIsReconnection = False Then
                    If dtGateOutRet.Rows.Count > 0 Then
                        bolSendMail = True
                    Else
                        Reconnect()
                    End If
                Else
                    If dtGateOutRet.Rows.Count > 0 Then
                        bolIsFileExist = True
                    End If
                End If
                Return dtGateOutRet
            Else
                ''To reconnect if there is  any authenticaion error
                '  Reconnect()
                Throw New Exception("UserName or Password was Wrong")
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "function getEstimateData()"

    Public Function getEstimateData(ByVal bv_DepotCode As String) As DataTable
        Try
            Dim dtEstimateRet As New DataTable
            Dim objEDIs As New EDIs

            dtEstimateRet = objEDIs.GetRepairEstimateRetByDPT_TRM(bv_DepotCode.Trim(), "U", bv_strCustomerCode)

            If Not dtEstimateRet Is Nothing Then

                If bolIsReconnection = False Then
                    If dtEstimateRet.Rows.Count > 0 Then
                        bolSendMail = True
                    Else
                        '    If Not ds.Tables("westim_Ret_" & bv_lessorcode.Trim.ToString).Rows.Count > 0 Or IsDBNull(ds.Tables("westim_Ret_" & bv_lessorcode.Trim.ToString).Rows.Count) = True Then
                        Reconnect()
                    End If
                Else
                    If dtEstimateRet.Rows.Count > 0 Then
                        bolIsFileExist = True
                    End If
                End If
                If bolIsReconnection = False Then
                    'If Not westimdt.pub_fdtWestimDt Is Nothing AndAlso westimdt.pub_fdtWestimDt.Rows.Count > 0 Then
                    'Else
                    'If Not ds.Tables("westim_Ret_" & bv_lessorcode.Trim.ToString).Rows.Count > 0 Or IsDBNull(ds.Tables("westim_Ret_" & bv_lessorcode.Trim.ToString).Rows.Count) = True Then
                    '   Reconnect()
                End If
                Return dtEstimateRet
            Else
                ''To reconnect if there is  any authenticaion error
                Reconnect()
                Throw New Exception("UserName or Password was Wrong")
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#End Region

#Region "Reconnect"
    Function Reconnect()
        Try
            Pvt_Reconnect = Pvt_Reconnect + 1
            If Pvt_Reconnect = 1 Then
                If Pvt_Time = 0 Then
                    Pvt_Time = Now.Minute
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "SPlit Unit_Nbr"
    Public Function Pub_Unit_Id_A(ByVal bv_Unit_Nbr As String)
        Try
            Dim UnitA As String
            UnitA = bv_Unit_Nbr.Substring(0, 4)
            Return UnitA
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Pub_Unit_Id_N(ByVal bv_Unit_Nbr As String)
        Try
            Dim UnitN As String
            UnitN = bv_Unit_Nbr.Substring(4, 6)
            Return UnitN
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Pub_Unit_Id_C(ByVal bv_Unit_Nbr As String)
        Try
            Dim UnitC As String
            UnitC = bv_Unit_Nbr.Substring(10, 1)
            Return UnitC
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Left Align"
    Public Function pub_leftalign(ByVal bv_value As String, ByVal bv_length As Integer) As String
        Try
            Dim intlength As Integer = CInt(bv_length)
            Dim intcount As Integer = 0
            Dim strvalue As String = bv_value
            Dim startindex As Integer
            strvalue = bv_value
            startindex = strvalue.Length

            If IsNumeric(strvalue) Then
                startindex = Trim(strvalue).Length()
                strvalue = Trim(strvalue)
                For intcount = startindex To intlength - 1
                    strvalue = strvalue + " "
                Next
            Else
                startindex = Trim(strvalue).Length()
                strvalue = Trim(strvalue)

                For intcount = startindex To intlength - 1
                    strvalue = strvalue + " "
                Next
            End If

            Return strvalue
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Remove Zeros"
    Public Function RemoveZeros(ByVal Datevalue As String) As String
        Try
            If Not Datevalue.Trim = String.Empty Then
                If Datevalue.Trim = 0 Then
                    Return pub_space(8)
                Else
                    Return Datevalue
                End If
            Else
                Return pub_space(8)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region " Pub_Space "
    Public Function pub_space(ByVal bv_size As String)
        Try
            Dim intlen As Integer
            Dim intCount As Integer
            Dim str As String

            intlen = CInt(bv_size)

            For intCount = 1 To intlen
                str = str + " "
            Next
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Pub_convert"
    Public Function Pub_ConvertDatetoString(ByVal bv_date As DateTime)
        Try
            Dim dt As System.DateTime = bv_date
            '**comment by sudha to remove GMT offset

            ''Add the Current Time Zone GMT Offset here
            'Dim tz As TimeZone = System.TimeZone.CurrentTimeZone()
            'Dim ts As New TimeSpan
            'ts = tz.GetUtcOffset(Date.Now)
            ''tz.IsDaylightSavingTime()
            ''Add the Server's GMT Offset here
            'Dim ts1 As New TimeSpan(0, 0, 0)
            'ts1 = ts1.Subtract(ts)

            ''Add the 2 for Daylight Saving
            'Dim ts2 As New TimeSpan(2, 0, 0)
            'ts2 = ts2.Add(ts1)

            'dt = dt.Add(ts2)

            '**ended

            Dim CToString As String
            Dim Cyear As String
            Dim Cmonth As String
            Dim Cday As String
            Cyear = dt.Year
            Cmonth = dt.Month
            If Cmonth.Trim.Length = 1 Then
                Cmonth = "0" + Cmonth
            Else
                Cmonth = Cmonth
            End If

            Cday = dt.Day
            If Cday.Trim.Length = 1 Then
                Cday = "0" + Cday
            Else
                Cday = Cday

            End If
            CToString = Cyear + Cmonth + Cday
            Return CToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Pub_ConvertDecimaltoString(ByVal bv_int As Decimal) As String
        Try
            Dim CToString As String
            CToString = Format(bv_int, "fixed")
            Return CToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Pub_ConvertIntegertoString(ByVal bv_int As Integer)
        Try
            Dim CToString As String
            CToString = bv_int.ToString
            Return CToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#Region " UCase"
    Public Function pub_ucase(ByVal bv_value As String)
        Try
            Return (bv_value.ToUpper)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#End Region

#Region "pvt_GetValue"
    Private Function pvt_GetValue(ByVal drRow As DataRow, ByVal strItemName As String, ByVal IntSpace As Integer) As String
        Try
            Dim objWriteEDi As New WriteEDI(bv_strCustomerCode, bv_strDepotCode)
            Dim strValue As String = Nothing

            If strItemName.ToUpper() = EDIData.GI_REC_DATE OrElse strItemName.ToUpper() = EDIData.GO_REC_DATE OrElse strItemName.ToUpper() = EDIData.WM_REC_DATE Then
                strValue = Format(Now, "yyyyMMdd")
            ElseIf strItemName.ToUpper() = EDIData.GI_MANU_DATE OrElse strItemName.ToUpper() = EDIData.GO_MANU_DATE OrElse strItemName.ToUpper() = EDIData.GI_LIC_EXPIRE OrElse strItemName.ToUpper() = EDIData.GO_LIC_EXPIRE OrElse strItemName.ToUpper() = EDIData.WM_MANU_DATE Then
                If Convert.IsDBNull(drRow(strItemName)) Then
                    strValue = "  /  "
                ElseIf drRow(strItemName).trim = "/" Or drRow(strItemName) = "" Then
                    strValue = "  /  "
                Else
                    strValue = drRow(strItemName)
                End If
            ElseIf strItemName.ToUpper() = EDIData.WM_TERM_TIME AndAlso drRow(strItemName).trim = "" Then
                strValue = "00:00"
            ElseIf strItemName.ToUpper() = EDIData.WM_WEIGHT AndAlso drRow(strItemName).trim = "" Then
                strValue = pub_space(9) & "0"
            ElseIf strItemName.ToUpper() = EDIData.WM_AUTH_DATE AndAlso drRow(strItemName).trim = "00010101" Then
                strValue = pub_space(IntSpace)
            ElseIf strItemName.ToUpper() = EDIData.WD_REPEATS Then
                strValue = objWriteEDi.RemoveLeadingZeros(drRow(strItemName).ToString, False)
            ElseIf strItemName.ToUpper() = EDIData.WD_DMGREP_DESC Then
                strValue = pub_space(IntSpace)
            Else
                If Convert.IsDBNull(drRow(strItemName)) Then
                    strValue = ""
                Else
                    strValue = drRow(strItemName)
                End If
            End If


            If strValue.Trim = "" OrElse strItemName = "GO_REC_TYPE" Then
                strValue = pub_space(IntSpace)
            End If

            Return strValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ModifyTaxRate"
    Private Function ModifyTax_Rate(ByVal drRow As DataRow, ByVal strItemName As String) As System.Decimal
        Try
            If strItemName.ToUpper() = EDIData.WD_MAT_COST AndAlso drRow(strItemName) = 0.0 Then
                Return "0.00"
            ElseIf drRow(strItemName) = 0.0 Then
                Return "0.000"
            Else
                Return drRow(strItemName)
            End If
        Catch ex As Exception
        End Try
    End Function
#End Region

#Region "function pub_fill()"

    Public Function pub_fill(ByVal bv_value As String, ByVal bv_Type As String, ByVal bv_char As String)
        Try
            Dim intlen As Integer
            Dim intCount As Integer
            Dim startindex As Integer
            Dim str As String


            If bv_Type = "L" Then
                str = bv_char
                Return str
            End If


            If bv_Type = "N" Then
                intlen = CInt(bv_char)
                str = ""
                For intCount = 0 To intlen - 1
                    str = str + " "
                Next
                Return (str)
            End If


            If bv_Type = "D" Then
                intlen = CInt(bv_char)

                str = Trim(bv_value)
                If str = "0" Or str = "" Then
                    str = "0.00"
                End If
                startindex = Trim(str).Length()
                For intCount = startindex To intlen - 1
                    str = " " + str
                Next

                Return str
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "function pub_fill()"
    Public Function pub_fill(ByVal bv_value As String, ByVal bv_size As String)
        Try
            Dim intlen As Integer
            Dim intCount As Integer
            Dim startindex As Integer
            Dim str As String

            intlen = CInt(bv_size)
            str = bv_value
            startindex = str.Length



            If IsNumeric(str) Then
                startindex = Trim(str).Length()
                str = Trim(str)
                For intCount = startindex To intlen - 1
                    str = " " + str
                Next
            Else
                startindex = Trim(str).Length()
                str = Trim(str)
                For intCount = startindex To intlen - 1
                    str = str + " "
                Next
            End If

            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "function pub_alignright_forstatus()"

    Public Function pub_alignright_forstatus(ByVal bv_value As String, ByVal bv_size As String)
        Try
            Dim intlen As Integer
            Dim intCount As Integer
            Dim startindex As Integer
            Dim str As String

            intlen = CInt(bv_size)
            str = bv_value
            startindex = str.Length

            startindex = Trim(str).Length()
            str = Trim(str)
            For intCount = startindex To intlen - 1
                str = str + " "
            Next
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


#Region "Updates"
#Region "ReportGateinUpdate"
    Public Sub ReportGateinUpdate(ByVal bv_strGatein As String, ByVal bv_strGateTransNo As String)

        Dim objtrans As New Transactions()
        Try
            Dim objEDIs As New EDIs
            Dim strGatein As String = Nothing
            Dim strGateTransNo As String = Nothing
            If bv_strGatein <> "" Then
                strGatein = bv_strGatein.Substring(0, bv_strGatein.Length - 1)
                For Each strGI_SNO As String In strGatein.Split(",")
                    If Not strGI_SNO.Trim = "" Then
                        objEDIs.DeleteGateinRet(CommonUIs.iLng(strGI_SNO), objtrans)
                    End If
                Next
            End If

            If bv_strGateTransNo <> "" Then
                strGateTransNo = bv_strGateTransNo.Substring(0, bv_strGateTransNo.Length - 1)
                For Each strGI_TNO As String In strGateTransNo.Split(",")
                    If Not strGI_TNO.Trim = "" Then
                        objEDIs.UpdateTracking("P", Now, strGI_TNO.Trim(), objtrans)
                    End If
                Next
            End If

            objtrans.commit()

        Catch ex As Exception
            objtrans.RollBack()
            Throw ex
        End Try
    End Sub
#End Region

#Region "ReportGateoutUpdate"
    Public Sub ReportGateoutUpdate(ByVal bv_strGateout As String, ByVal bv_strGateTransNo As String)
        Dim objtrans As New Transactions()
        Try

            Dim objEDIs As New EDIs
            Dim strGateTransNo As String = Nothing
            Dim strGateout As String
            If bv_strGateout <> "" Then
                strGateout = bv_strGateout.Substring(0, bv_strGateout.Length - 1)
                For Each strGO_SNO As String In strGateout.Split(",")
                    If Not strGO_SNO.Trim = "" Then
                        objEDIs.DeleteGateoutRet(CommonUIs.iLng(strGO_SNO), objtrans)
                    End If
                Next
            End If

            If bv_strGateTransNo <> "" Then
                strGateTransNo = bv_strGateTransNo.Substring(0, bv_strGateTransNo.Length - 1)
                For Each strGI_TNO As String In strGateTransNo.Split(",")
                    If Not strGI_TNO.Trim = "" Then
                        objEDIs.UpdateTracking("P", Now, strGI_TNO.Trim(), objtrans)
                    End If
                Next
            End If

            objtrans.commit()

        Catch ex As Exception
            objtrans.RollBack()
            Throw ex
        End Try
    End Sub
#End Region

#Region "ReportWestimTrackUpdate"
    Public Function ReportWestimTrackUpdate(ByVal bv_strwestimSno As String, ByVal bv_strTransNo As String)
        Dim objtrans As New Transactions()
        Try

            Dim objEDIs As New EDIs
            Dim strTransNo As String = Nothing

            Dim strWestimSno As String
            If bv_strwestimSno <> "" Then
                strWestimSno = bv_strwestimSno.Substring(0, bv_strwestimSno.Length - 1)
                For Each strSno As String In strWestimSno.Split(",")
                    If Not strSno.Trim = "" Then
                        objEDIs.DeleteRepairEstimateRet(CommonUIs.iLng(strSno), objtrans)
                    End If
                Next
            End If

            If bv_strTransNo <> "" Then
                strTransNo = bv_strTransNo.Substring(0, bv_strTransNo.Length - 1)
                For Each strGI_TNO As String In strTransNo.Split(",")
                    If Not strGI_TNO.Trim = "" Then
                        ' 'TRNSMSSN_NO'. not in tracking table 
                        objEDIs.UpdateTracking("P", Now, strGI_TNO.Trim(), objtrans)
                    End If
                Next
            End If

            objtrans.commit()
        Catch ex As Exception
            objtrans.RollBack()
            Throw ex
        End Try
    End Function
#End Region

#Region "ReportDetailsUpdate"
    Public Function ReportDetailsUpdate(ByVal bv_strwestimdtSno As String, ByVal bv_strTransNo As String)
        Dim objtrans As New Transactions()
        Try

            Dim objEDIs As New EDIs
            Dim strTransNo As String = Nothing

            Dim strWestimDtSno As String

            If bv_strwestimdtSno <> "" Then
                strWestimDtSno = bv_strwestimdtSno.Substring(0, bv_strwestimdtSno.Length - 1)
                For Each strSno As String In strWestimDtSno.Split(",")
                    If Not strSno.Trim = "" Then
                        objEDIs.DeleteRepairEstimateDetailRet(CommonUIs.iLng(strSno), objtrans)
                    End If
                Next
            End If


            If bv_strTransNo <> "" Then
                strTransNo = bv_strTransNo.Substring(0, bv_strTransNo.Length - 1)
                For Each strGI_TNO As String In strTransNo.Split(",")
                    If Not strGI_TNO.Trim = "" Then
                        ''TRNSMSSN_NO'. not in tracking
                        objEDIs.UpdateTracking("P", Now, strGI_TNO.Trim(), objtrans)
                    End If
                Next
            End If

            objtrans.commit()
        Catch ex As Exception
            objtrans.RollBack()
            Throw ex
        End Try
    End Function
#End Region
#End Region




End Class
