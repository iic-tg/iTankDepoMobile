Imports System.Configuration
Imports System.IO
Imports System.Text

Public Class WriteEDI

#Region "Declarations"
    Private strOutFolderName As String
    Private strTempFolderName As String
    Private strFileExtension As String
    Private bv_strCustomerCode As String
    Private strMailTempFolderName As String

#End Region

#Region "New()"
    Public Sub New(ByVal strCustomerCode As String, ByVal strDepotCode As String)
        bv_strCustomerCode = strCustomerCode
        strOutFolderName = String.Concat(pub_GetAppConfigValue("OutputFolder"), strDepotCode, "\", bv_strCustomerCode, "\")
        strMailTempFolderName = String.Concat(pub_GetAppConfigValue("OutputFolder"), strDepotCode, "\", bv_strCustomerCode, "\", "Temp", "\")
        strTempFolderName = pub_GetAppConfigValue("TempFolder")
        strFileExtension = pub_GetAppConfigValue("Extn")
    End Sub
#End Region

#Region "pub_WriteText"
    Public Sub pub_WriteText(ByVal strData As StringBuilder, ByVal bv_Extension As String)
        Try

            Dim strFileNamePrefix As String = ""

            Dim strTraceFilePath As String
            If pub_GetAppConfigValue("LogFilePath") IsNot Nothing Then
                strTraceFilePath = pub_GetAppConfigValue("LogFilePath")
            Else
                strTraceFilePath = AppDomain.CurrentDomain.BaseDirectory.ToString & "LogFiles\"
            End If

            If bv_Extension = ".txt" Then
                strFileNamePrefix = "EDI"
            End If
            Dim fi As FileStream = Nothing

            strTraceFilePath = String.Concat(strTraceFilePath, strFileNamePrefix, DateTime.Now.ToString("ddMMyyyy"), ".txt")
            If Not File.Exists(strTraceFilePath) Then
                fi = File.Create(strTraceFilePath)
                fi.Close()
            End If
            Using w As StreamWriter = File.AppendText(strTraceFilePath)
                Log(strData.ToString(), w)
                'Close the writer and underlying file.
                w.Close()
            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region

#Region "pub_GetAppConfigValue"
    Public Shared Function pub_GetAppConfigValue(ByVal bv_strConfigName As String) As String
        Try
            Return ConfigurationManager.AppSettings(bv_strConfigName)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Log"
    Public Shared Sub Log(ByVal bv_strMessage As String, ByVal w As TextWriter)
        Try
            w.Write(ControlChars.CrLf)
            w.WriteLine("{0}", _
                    bv_strMessage)
            w.Flush()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pub_CreateActivityReceivelog"
    Public Sub pub_CreateActivityReceivelog(ByVal Receivelog As String, ByVal ReceivelogHeader As String)

        Try
            If Not Directory.Exists(strOutFolderName) Then
                Directory.CreateDirectory(strOutFolderName)
            End If
            Dim swReceivelog As New StreamWriter(String.Concat(strOutFolderName, Format(Now, "MMddHHmm"), ".rcv"), True)
            If Receivelog.Trim <> String.Empty And ReceivelogHeader.Trim = String.Empty Then
                swReceivelog.Write(Receivelog)
            ElseIf ReceivelogHeader.Trim <> String.Empty Then
                swReceivelog.WriteLine(ReceivelogHeader)
            ElseIf Receivelog.Trim = String.Empty And ReceivelogHeader.Trim = String.Empty Then
                swReceivelog.WriteLine("")
            End If
            swReceivelog.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pub_WriteSpecificFile"
    Public Sub pub_WriteSpecificFile(ByVal strFileName As String, ByRef strData As StringBuilder, ByVal bv_strGenerationMode As String)
        Try

            Dim strOutputFileName As String = String.Concat(strOutFolderName, strFileName, ".tmp")
            Dim strEmailTempFileName As String = String.Concat(strMailTempFolderName, strFileName, ".tmp")
            Dim strTempFileName As String = String.Concat(strTempFolderName, strFileName, ".tmp")
            If Not Directory.Exists(strOutFolderName) Then
                Directory.CreateDirectory(strOutFolderName)
            End If
            If Not Directory.Exists(strMailTempFolderName) Then
                Directory.CreateDirectory(strMailTempFolderName)
            End If
            If Not Directory.Exists(strTempFolderName) Then
                Directory.CreateDirectory(strTempFolderName)
            End If

            RenameFilesToTemp()
            If File.Exists(strOutputFileName) Then

                Try
                    File.Move(strOutputFileName, strTempFileName)
                    WriteFile(strOutputFileName, strTempFileName, strData)
                Catch ex As Exception
                    WriteFile(strOutputFileName, strTempFileName, strData)
                    ex = Nothing
                End Try
            Else
                WriteFile(strOutputFileName, strTempFileName, strData)
            End If
            If UCase(bv_strGenerationMode) = "SERVICE" Then

                WriteFile(strEmailTempFileName, strTempFileName, strData)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pub_WriteSpecificFile"
    Public Sub pub_WriteSpecificCodecoFile(ByVal strFileName As String, ByRef strData As StringBuilder, ByVal bv_strGenerationMode As String)
        Try

            Dim strOutputFileName As String = String.Concat(strOutFolderName, strFileName, ".txt")
            Dim strEmailTempFileName As String = String.Concat(strMailTempFolderName, strFileName, ".txt")

            Dim strTempFileName As String = String.Concat(strTempFolderName, strFileName, ".txt")
            If Not Directory.Exists(strOutFolderName) Then
                Directory.CreateDirectory(strOutFolderName)
            End If

            If Not Directory.Exists(strTempFolderName) Then
                Directory.CreateDirectory(strTempFolderName)
            End If
            If Not Directory.Exists(strMailTempFolderName) Then
                Directory.CreateDirectory(strMailTempFolderName)
            End If
            If File.Exists(strOutputFileName) Then

                Try
                    File.Move(strOutputFileName, strTempFileName)
                    WriteFile(strOutputFileName, strTempFileName, strData)
                Catch ex As Exception
                    WriteFile(strOutputFileName, strTempFileName, strData)
                    ex = Nothing
                End Try
            Else
                WriteFile(strOutputFileName, strTempFileName, strData)
            End If
            If UCase(bv_strGenerationMode) = "SERVICE" Then
                WriteFile(strEmailTempFileName, strTempFileName, strData)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "WriteFile"
    Public Sub WriteFile(ByRef strOutFile As String, ByRef strTempFile As String, ByRef strData As StringBuilder)
        Try
            Using ioAObj As StreamWriter = File.AppendText(strTempFile)

                If Not strData.ToString = "" Then
                    ioAObj.WriteLine(strData)
                End If
                ioAObj.Close()
            End Using

            File.Move(strTempFile, strOutFile)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
#End Region

#Region "RenameFilesToTemp"
    Public Sub RenameFilesToTemp()
        Try
            If File.Exists(String.Concat(strOutFolderName, "Gatein", strFileExtension)) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Gatein", strFileExtension), String.Concat(strOutFolderName, "Gatein.tmp"))
            End If
            If File.Exists(String.Concat(strOutFolderName, "Gateout", strFileExtension)) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Gateout", strFileExtension), String.Concat(strOutFolderName, "Gateout.tmp"))
            End If
            If File.Exists(String.Concat(strOutFolderName & "Westim" & strFileExtension)) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Westim", strFileExtension), String.Concat(strOutFolderName, "Westim.tmp"))
            End If
            If File.Exists(String.Concat(strOutFolderName & "Westimdt" & strFileExtension)) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Westimdt", strFileExtension), String.Concat(strOutFolderName, "Westimdt.tmp"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "pub_RenameFilesToExtension"
    'Public Sub pub_RenameFilesToExtension()
    Public Function pub_RenameFilesToExtension() As String
        Try
            Dim bolTimeStampRqrd, bolDateStampRqrd, bolDateTimeStampRqrd As Boolean
            Dim strTimeStamp As String = String.Empty
            Dim strEstimateFile As String = String.Empty

            bolTimeStampRqrd = Convert.ToBoolean(pub_GetAppConfigValue("TimeStampRequired"))
            bolDateStampRqrd = Convert.ToBoolean(pub_GetAppConfigValue("DateStampRequired"))
            bolDateTimeStampRqrd = Convert.ToBoolean(pub_GetAppConfigValue("DateTimeStampRequired"))

            If bolTimeStampRqrd = True Then
                strTimeStamp = Now.ToString("HH-mm-ss-")
            ElseIf bolDateStampRqrd = True Then
                strTimeStamp = Now.ToString("yyyy-MM-dd-")
            ElseIf bolDateTimeStampRqrd = True Then
                strTimeStamp = Now.ToString("yyyy-MM-dd-HH-mm-ss-")
            End If

            If (Not File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Gatein", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Gatein.tmp")) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Gatein.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Gatein", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Gatein", strFileExtension)
            ElseIf (File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Gatein", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Gatein.tmp")) Then
                File.Move(String.Concat(strOutFolderName, strTimeStamp, "Gatein", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Gatein.exp"))
                FileSystem.Rename(String.Concat(strOutFolderName, "Gatein.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Gatein", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Gatein", strFileExtension)
            End If

            If (Not File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Gatein", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Gatein.tmp")) Then
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Gatein.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Gatein", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Gatein", strFileExtension)
            ElseIf (File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Gatein", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Gatein.tmp")) Then
                File.Move(String.Concat(strMailTempFolderName, strTimeStamp, "Gatein", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Gatein.exp"))
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Gatein.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Gatein", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Gatein", strFileExtension)
            End If

            If (Not File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Gateout", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Gateout.tmp")) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Gateout.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Gateout", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Gateout", strFileExtension)
            ElseIf (File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Gateout", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Gateout.tmp")) Then

                File.Move(String.Concat(strOutFolderName, strTimeStamp, "Gateout", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Gateout.exp"))
                FileSystem.Rename(String.Concat(strOutFolderName, "Gateout.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Gateout", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Gateout", strFileExtension)
            End If

            If (Not File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Gateout", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Gateout.tmp")) Then
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Gateout.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Gateout", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Gateout", strFileExtension)
            ElseIf (File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Gateout", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Gateout.tmp")) Then

                File.Move(String.Concat(strMailTempFolderName, strTimeStamp, "Gateout", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Gateout.exp"))
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Gateout.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Gateout", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Gateout", strFileExtension)
            End If


            If (Not File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Westim", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Westim.tmp")) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Westim.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Westim", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Westim", strFileExtension)
            ElseIf (File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Westim", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Westim.tmp")) Then

                File.Move(String.Concat(strOutFolderName, strTimeStamp, "Westim", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Westim.exp"))
                FileSystem.Rename(String.Concat(strOutFolderName, "Westim.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Westim", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Westim", strFileExtension)
            End If

            If (Not File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Westim", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Westim.tmp")) Then
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Westim.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Westim", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Westim", strFileExtension)

            ElseIf (File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Westim", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Westim.tmp")) Then

                File.Move(String.Concat(strMailTempFolderName, strTimeStamp, "Westim", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Westim.exp"))
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Westim.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Westim", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Westim", strFileExtension)
            End If


            If (Not File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Westimdt", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Westimdt.tmp")) Then
                FileSystem.Rename(String.Concat(strOutFolderName, "Westimdt.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Westimdt", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Westimdt", strFileExtension)
            ElseIf (File.Exists(String.Concat(strOutFolderName, strTimeStamp, "Westimdt", strFileExtension))) AndAlso File.Exists(String.Concat(strOutFolderName, "Westimdt.tmp")) Then
                File.Move(String.Concat(strOutFolderName, strTimeStamp, "Westimdt", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Westimdt.exp"))
                FileSystem.Rename(String.Concat(strOutFolderName, "Westimdt.tmp"), String.Concat(strOutFolderName, strTimeStamp, "Westimdt", strFileExtension))
                strEstimateFile = String.Concat(strOutFolderName, strTimeStamp, "Westimdt", strFileExtension)
            End If


            If (Not File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Westimdt", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Westimdt.tmp")) Then
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Westimdt.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Westimdt", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Westimdt", strFileExtension)
            ElseIf (File.Exists(String.Concat(strMailTempFolderName, strTimeStamp, "Westimdt", strFileExtension))) AndAlso File.Exists(String.Concat(strMailTempFolderName, "Westimdt.tmp")) Then
                File.Move(String.Concat(strMailTempFolderName, strTimeStamp, "Westimdt", strFileExtension), String.Concat(strTempFolderName, "\", Now.ToString("yyyy-MM-dd-HH-mm-ss-"), "Westimdt.exp"))
                FileSystem.Rename(String.Concat(strMailTempFolderName, "Westimdt.tmp"), String.Concat(strMailTempFolderName, strTimeStamp, "Westimdt", strFileExtension))
                strEstimateFile = String.Concat(strMailTempFolderName, strTimeStamp, "Westimdt", strFileExtension)
            End If
            If strEstimateFile = String.Empty Then
                Return strTimeStamp
            Else
                Return strEstimateFile
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Fill"
    Function Fill(ByVal bv_dppamt As Decimal, ByVal bv_size As String)
        Try
            Dim intlen As Integer
            Dim intCount As Integer
            Dim startindex As Integer
            Dim bv_value As String
            Dim str As String
            intlen = CInt(bv_size)
            bv_value = GetDpp_Amt(bv_dppamt, intlen)

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

#Region "GetDPP_Amt"
    Function GetDpp_Amt(ByVal bv_int As Decimal, ByVal bv_size As Integer)
        Try
            Dim CToString As String
            Dim intDotindex As Integer = bv_int.ToString.IndexOf(".")
            Dim intlen As Integer = bv_int.ToString.Length
            Dim intdiff As Integer = intlen - intDotindex
            Dim intFrom As Integer = intDotindex + 3
            Dim intTo As Integer = intlen - (intDotindex + 3)

            If bv_int.ToString.Length < (bv_size - 2) And bv_int.ToString.IndexOf(".") < 0 Or intdiff < 3 Then
                CToString = Format(bv_int, "0.00")

            ElseIf bv_int.ToString.Length <= bv_size And bv_int.ToString.IndexOf(".") < 0 Or intdiff < 3 Then
                CToString = bv_int
            ElseIf intdiff > 3 And intDotindex > 0 Then
                CToString = bv_int.ToString.Remove(intFrom, intTo)
            Else
                CToString = bv_int
            End If
            Return CToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region " RemoveLeadingZeros "

    Function RemoveLeadingZeros(ByVal strValue As String, ByVal decimalTrue As Boolean)
        Try
            Dim decValue As Decimal
            Dim intValue As Integer
            Dim retValue As String
            Try
                If decimalTrue Then
                    decValue = strValue
                    ' Default is 'G', to confirm use "0.00"
                    retValue = decValue.ToString("0.00")
                Else
                    intValue = strValue
                    retValue = intValue
                End If
            Catch ex As Exception
                retValue = String.Empty
            End Try
            Return retValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
