Option Strict On

Imports System.Text

Partial Class UserControls_iTreeView
    Inherits System.Web.UI.UserControl


    Private NodeCollection As New Collection
    Private NodeCount As Integer = 0
    Private NodeCountDiv As Integer = 0
    Private _Merror As String
    Private _Messages As String
    Private TreeCodeText As StringBuilder
    Private _PrevParentKey As Integer
    Private _NodeLeftSpaces As Integer
    Private curParKey As String

    Private _Width As Integer = 600

    Public Enum mrNodeType
        mrParent = 1
        mrChild = 2
    End Enum

    Private Class clsNodeData
        Public NodeValue As String
        Public NodeType As Integer
        Public NodeKey As String
        Public NodeParentKey As String
        Public NodeFullPath As String
        Public NodeLeft As Integer
        Public NodeTop As Integer
        Public NodeChildCount As Integer
        Public NodeNumber As Integer
        Public NodePrefixSpaces As Integer
        Public NodeWritten As Boolean
        Public NodeLevel As Integer
        Public NodeRootParentKey As String
        Public Addopt As Boolean
        Public Editopt As Boolean
        Public Viewopt As Boolean
        Public Alertopt As Boolean
        Public Reminderopt As Boolean
        Public Printopt As Boolean
        Public AlertEn As Boolean
        Public ReminderEn As Boolean
        Public PrintEn As Boolean
        Public intCountAddopt As Integer
        Public intCountEditopt As Integer
        Public intCountViewopt As Integer
        Public intCountAlertopt As Integer
        Public intCountReminderopt As Integer
        Public intCountPrintopt As Integer

        Public intEditRight As Integer
        Public intCreateRight As Integer
        Public intCancelRight As Integer

        Public intCountCancelopt As Integer
        Public Cancelopt As Boolean
    End Class

    Public mrTree1 As UserControls_iTreeView

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Property RWidth() As Integer
        Get
            RWidth = _Width
        End Get
        Set(ByVal Value As Integer)
            _Width = Value
        End Set
    End Property

    Public Property Errors() As String
        Get
            Errors = _Merror
        End Get
        Set(ByVal Value As String)
            _Merror = Value
        End Set
    End Property

    Public Property Messages() As String
        Get
            Messages = _Messages
        End Get
        Set(ByVal Value As String)
            _Messages = Value
        End Set
    End Property

    Private Property PrevParentKey() As Integer
        Get
            PrevParentKey = _PrevParentKey
        End Get
        Set(ByVal Value As Integer)
            _PrevParentKey = Value
        End Set
    End Property

    Private Function CheckIfKeyExists(ByVal nodeK As String) As Boolean
        Dim clNode As clsNodeData
        For Each clNode In NodeCollection
            If UCase(clNode.NodeKey) = UCase(nodeK) Then
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function

    Public Sub AddNodeDiv(ByVal NodeText As String, ByVal NodeType As mrNodeType, _
                        ByVal NodeKey As String, ByVal optAdd As Boolean, _
                        ByVal optEdit As Boolean, ByVal optView As Boolean, ByVal optCancel As Boolean, _
                        Optional ByVal ParentKey As String = "", _
                        Optional ByVal bv_intCheckedAddRightCount As Integer = 0, _
                        Optional ByVal bv_intCheckedEditRight As Integer = 0, _
                        Optional ByVal bv_intCheckedViewRight As Integer = 0, Optional ByVal bv_intCheckedCancelRight As Integer = 0, Optional ByVal intCreateRight As Integer = 1, Optional ByVal intEditRight As Integer = 1, Optional ByVal intCancelRight As Integer = 1)

        Try
            Dim NodeData As New clsNodeData

            If ParentKey = "" Then
                Me.NodeLeftSpaces = 2
                NodeData.NodeLevel = 1
                curParKey = NodeKey
            End If

            If CheckIfKeyExists(NodeKey) And ParentKey <> "" Then
                Me.Errors = "Key Already Exists"
                Exit Sub
            End If

            If NodeType = mrNodeType.mrChild Then
                setNodeSpaces(ParentKey)
                NodeData.intEditRight = intEditRight
                NodeData.intCreateRight = intCreateRight
                NodeData.intCancelRight = intCancelRight
                NodeData.NodeLevel = SetLevel(ParentKey) + 1
            Else
                NodeData.intCountAddopt = bv_intCheckedAddRightCount
                NodeData.intCountEditopt = bv_intCheckedEditRight
                NodeData.intCountViewopt = bv_intCheckedViewRight
                NodeData.intCountCancelopt = bv_intCheckedCancelRight
                NodeData.intEditRight = intEditRight
                NodeData.intCreateRight = intCreateRight
                NodeData.intCancelRight = intCancelRight
                setNodeSpaces(ParentKey)
            End If

            NodeCountDiv += 1

            NodeData.Addopt = optAdd
            NodeData.Editopt = optEdit
            NodeData.Viewopt = optView
            NodeData.Cancelopt = optCancel
            NodeData.NodeValue = NodeText
            NodeData.NodeType = NodeType
            NodeData.NodeKey = NodeKey.Replace(" ", "___")
            NodeData.NodeParentKey = ParentKey
            NodeData.NodeNumber = NodeCountDiv
            NodeData.NodeRootParentKey = ParentKey
            NodeData.NodePrefixSpaces = Me.NodeLeftSpaces
            NodeCollection.Add(NodeData, CStr(NodeCountDiv))

        Catch Err1 As Exception
            Me.Errors = Err1.Message
        End Try

    End Sub

    Public Function BuildTree() As String

        TreeCodeText = New StringBuilder
        Dim sbrScript As New StringBuilder
        Dim sbrSubScript As StringBuilder
        Dim sbrHideScript As New StringBuilder
        Dim sbrSubHideScript As StringBuilder
        Dim sbrNameID As StringBuilder
        Dim sbrAttributes As StringBuilder

        Dim itrNode As clsNodeData
        Dim curParentKey As String = String.Empty

        Dim prevLevl As Integer

        sbrHideScript.Append("<script> fnHAN('")
        TreeCodeText.Append("<div id=""divTree"" class=""rolerightgrid"" style='height:190px;'>")
        TreeCodeText.Append("<table Id=""tblTree"" align=""left"" cellpadding=""1"" cellspacing=""1"">")
        TreeCodeText.Append("<tr class=""ghdr"" style='position:relative;TOP:expression(this.offsetParent.scrollTop-2);height:35px'>")
        TreeCodeText.Append("<td Id=""tdMenuName"" align=""center"" >Menu Name</td>")
        TreeCodeText.Append("<td Id=""tdAdd"" align=center onclick=""fnSA();"">Add</td>")
        TreeCodeText.Append("<td Id=""tdEdit"" align=center onclick=""fnSA();"">Edit</td>")
        TreeCodeText.Append("<td Id=""tdView"" align=center onclick=""fnSA();"">View</td>")
        TreeCodeText.Append("<td Id=""tdCancel"" align=center onclick=""fnSA();"">Cancel</td>")
        TreeCodeText.Append(vbCrLf)

        Dim i As Integer = 0
        For Each itrNode In NodeCollection
            If i = 1 Then

            End If
            TreeCodeText.Append(vbCrLf)
            If itrNode.NodeType = mrNodeType.mrParent And itrNode.NodeParentKey = "" Then
                sbrSubScript = New StringBuilder

                If sbrScript.Length > 0 Then
                    TreeCodeText.Append("<script>")
                    TreeCodeText.Append(vbCrLf)
                    TreeCodeText.Append(sbrScript.ToString)
                    TreeCodeText.Append("';")
                    TreeCodeText.Append(vbCrLf)
                    TreeCodeText.Append("</script>")
                    TreeCodeText.Append(vbCrLf)
                    sbrScript.Remove(0, sbrScript.Length)
                End If
                If (itrNode.NodeKey.Substring(0, 2) = "PN") Then
                    TreeCodeText.Append("<tr id='ro" & itrNode.NodeKey & "' class=""gitem""><td width=""300px""")
                    TreeCodeText.Append(" onclick=")
                    TreeCodeText.Append("""fnCN('")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append("','")
                    TreeCodeText.Append(AppendParentKeys(itrNode.NodeKey))
                    TreeCodeText.Append("');""")
                    TreeCodeText.Append(">")
                    'If ChildExists(itrNode.NodeKey) Then
                    TreeCodeText.Append("&nbsp;")

                    TreeCodeText.Append("<img class=""gitem"" src = ""../Images/minus.jpg"" id = ""i")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append(""" class=""gitem""")
                    TreeCodeText.Append(" onclick=")
                    TreeCodeText.Append("""fnNC('")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append("','P');""")
                    TreeCodeText.Append(">")
                    'End If
                End If
                If (itrNode.NodeKey.Substring(0, 3) = "CPN") Then

                    TreeCodeText.Append("<tr id='ro" & itrNode.NodeKey & "' class=""gitem""><td width=""300px""")
                    TreeCodeText.Append(" onclick=")
                    TreeCodeText.Append("""fnCN('")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append("','")
                    TreeCodeText.Append(AppendParentKeys(itrNode.NodeKey))
                    TreeCodeText.Append("');""")
                    TreeCodeText.Append(">")
                    'If ChildExists(itrNode.NodeKey) Then
                    TreeCodeText.Append("&nbsp;&nbsp;&nbsp;")
                    TreeCodeText.Append("<img class=""gitem"" src = ""../Images/minus.jpg"" id = ""i")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append(""" class=""gitem""")
                    TreeCodeText.Append(" onclick=")
                    TreeCodeText.Append("""fnNC('")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append("','CP');""")
                    TreeCodeText.Append(">")
                    'End If
                End If


                TreeCodeText.Append("&nbsp;&nbsp;&nbsp;")
                TreeCodeText.Append(itrNode.NodeValue)
                TreeCodeText.Append("</td>")

                'Add Add Right
                sbrNameID = New StringBuilder
                sbrNameID.Append("_id")
                sbrNameID.Append(itrNode.NodeKey)
                sbrNameID.Append("_a")
                sbrAttributes = New StringBuilder
                sbrAttributes.Append("onclick=""fnCAN(this,'a','")
                sbrAttributes.Append(itrNode.NodeKey)
                sbrAttributes.Append("');""")

                If itrNode.intCreateRight = 0 Then
                    TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Addopt, sbrAttributes.ToString, False))
                Else
                    TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Addopt, sbrAttributes.ToString))
                End If


                'Add Edit Right
                sbrNameID = New StringBuilder
                sbrNameID.Append("_id")
                sbrNameID.Append(itrNode.NodeKey)
                sbrNameID.Append("_e")
                sbrAttributes = New StringBuilder
                'sbrAttributes = New StringBuilder

                sbrAttributes.Append("onclick=""fnCAN(this,'e','")
                sbrAttributes.Append(itrNode.NodeKey)
                sbrAttributes.Append("');""")
                If itrNode.intEditRight = 0 Then
                    TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Editopt, sbrAttributes.ToString, False))
                Else
                    TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Editopt, sbrAttributes.ToString))
                End If
                'Add View Right
                sbrNameID = New StringBuilder
                sbrNameID.Append("_id")
                sbrNameID.Append(itrNode.NodeKey)
                sbrNameID.Append("_v")
                sbrAttributes = New StringBuilder
                sbrAttributes.Append(" onclick=""fnCAN(this,'v','")
                sbrAttributes.Append(itrNode.NodeKey)
                sbrAttributes.Append("',true);""")
                TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Viewopt, sbrAttributes.ToString))

                'Add Cancel Right
                sbrNameID = New StringBuilder
                sbrNameID.Append("_id")
                sbrNameID.Append(itrNode.NodeKey)
                sbrNameID.Append("_c")
                sbrAttributes = New StringBuilder
                sbrAttributes.Append(" onclick=""fnCAN(this,'c','")
                sbrAttributes.Append(itrNode.NodeKey)
                sbrAttributes.Append("',true);""")
                'TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Cancelopt, sbrAttributes.ToString))
                If itrNode.intCancelRight = 0 Then
                    TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Cancelopt, sbrAttributes.ToString, False))
                Else
                    TreeCodeText.Append(CreateCheckboxTD(sbrNameID.ToString, sbrNameID.ToString, itrNode.Cancelopt, sbrAttributes.ToString))
                End If

                'Setting dynamic checked max count
                sbrScript.Append("var mc")
                sbrScript.Append(itrNode.NodeKey)
                sbrScript.Append("_a=")
                sbrScript.Append(itrNode.intCountAddopt)
                sbrScript.Append(";")

                sbrScript.Append("var mc")
                sbrScript.Append(itrNode.NodeKey)
                sbrScript.Append("_e=")
                sbrScript.Append(itrNode.intCountEditopt)
                sbrScript.Append(";")

                sbrScript.Append("var mc")
                sbrScript.Append(itrNode.NodeKey)
                sbrScript.Append("_v=")
                sbrScript.Append(itrNode.intCountViewopt)
                sbrScript.Append(";")

                'Cancel
                sbrScript.Append("var mc")
                sbrScript.Append(itrNode.NodeKey)
                sbrScript.Append("_c=")
                sbrScript.Append(itrNode.intCountViewopt)
                sbrScript.Append(";")

                sbrScript.Append("var a")
                sbrScript.Append(itrNode.NodeKey)
                sbrScript.Append("='")
                sbrHideScript.Append(itrNode.NodeKey)
                sbrHideScript.Append(",")

                curParentKey = itrNode.NodeKey

                sbrSubScript.Append("var a")
                sbrSubScript.Append(itrNode.NodeParentKey)
                sbrSubScript.Append("='")
            End If

            TreeCodeText.Append("</tr>")
            If itrNode.NodeType = mrNodeType.mrChild Then
                sbrSubHideScript = New StringBuilder

                If ChildExists(itrNode.NodeKey) Then
                Else
                    itrNode.NodeWritten = True
                    Dim iSpc As Integer
                    Dim spcStr As StringBuilder
                    spcStr = New StringBuilder
                    For iSpc = 0 To itrNode.NodePrefixSpaces - 1
                        spcStr.Append("&nbsp;")
                    Next

                    TreeCodeText.Append("<tr class=""gitem""")
                    TreeCodeText.Append(" id='ro")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append("'><td")
                    ' If itrNode.intCreateRight = 1 Then
                    TreeCodeText.Append(" onclick=")
                    TreeCodeText.Append("""fnCN('")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append("','")
                    TreeCodeText.Append(AppendParentKeys(itrNode.NodeKey))
                    TreeCodeText.Append("');""")
                    'End If
                    TreeCodeText.Append(" id='a")
                    TreeCodeText.Append(itrNode.NodeKey)
                    TreeCodeText.Append("'>")
                    TreeCodeText.Append(spcStr.ToString)
                    If (itrNode.NodeKey).Substring(0, 2) = "CN" Then
                        TreeCodeText.Append(spcStr.ToString)
                    End If
                    TreeCodeText.Append(itrNode.NodeValue)
                    TreeCodeText.Append("</td>")

                    sbrNameID = New StringBuilder
                    sbrNameID.Append("_id")
                    sbrNameID.Append(itrNode.NodeKey)
                    sbrNameID.Append("_a")
                    sbrAttributes = New StringBuilder

                    If itrNode.intCreateRight = 0 Then
                        TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Addopt, sbrAttributes.ToString, False))
                    Else
                        TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Addopt, sbrAttributes.ToString))
                    End If
                    sbrNameID = New StringBuilder
                    sbrNameID.Append("_id")
                    sbrNameID.Append(itrNode.NodeKey)
                    sbrNameID.Append("_e")
                    sbrAttributes = New StringBuilder
                    If itrNode.intEditRight = 0 Then
                        TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Editopt, sbrAttributes.ToString, False))
                    Else
                        TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Editopt, sbrAttributes.ToString))
                    End If


                    sbrNameID = New StringBuilder
                    sbrNameID.Append("_id")
                    sbrNameID.Append(itrNode.NodeKey)
                    sbrNameID.Append("_v")
                    sbrAttributes = New StringBuilder
                    TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Viewopt, sbrAttributes.ToString))

                    'Cancel Right
                    sbrNameID = New StringBuilder
                    sbrNameID.Append("_id")
                    sbrNameID.Append(itrNode.NodeKey)
                    sbrNameID.Append("_c")
                    sbrAttributes = New StringBuilder
                    'TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Cancelopt, sbrAttributes.ToString))

                    If itrNode.intCancelRight = 0 Then
                        TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Cancelopt, sbrAttributes.ToString, False))
                    Else
                        TreeCodeText.Append(CreateCheckboxTD(itrNode.NodeKey, sbrNameID.ToString, sbrNameID.ToString, itrNode.Cancelopt, sbrAttributes.ToString))
                    End If

                    sbrScript.Append(itrNode.NodeKey)
                    sbrScript.Append(",")

                    prevLevl = itrNode.NodeLevel


                    sbrSubHideScript.Append(itrNode.NodeKey)
                    sbrSubHideScript.Append(",")
                    TreeCodeText.Append("</tr>")
                End If

            End If
            i = i + 1

        Next
        If sbrScript.Length > 0 Then
            TreeCodeText.Append("<script>")
            TreeCodeText.Append(vbCrLf)
            TreeCodeText.Append(sbrScript.ToString)
            TreeCodeText.Append("';")
            TreeCodeText.Append(vbCrLf)
            TreeCodeText.Append("</script>")
            sbrScript.Remove(0, sbrScript.Length)
        End If

        TreeCodeText.Append("</td>")
        TreeCodeText.Append("</table>")
        TreeCodeText.Append("</div>")
        sbrHideScript.Append("');")
        sbrHideScript.Append(vbCrLf)
        sbrHideScript.Append("</script>")

        TreeCodeText.Append(vbCrLf)

        TreeCodeText.Append(sbrHideScript.ToString())

        BuildTree = TreeCodeText.ToString

    End Function

    Private Function CreateCheckboxTD(ByVal strName As String, ByVal strID As String, ByVal bolIsChecked As Boolean, Optional ByVal strOtherAttributes As String = "", Optional ByVal bolIsEnabled As Boolean = True) As String
        Dim sbtTemp As New StringBuilder
        sbtTemp.Append("<td width=""78px"" align=""center""><input type=""checkbox""")
        sbtTemp.Append(" name=""")
        sbtTemp.Append(strName)
        sbtTemp.Append(""" id='")
        sbtTemp.Append(strID)
        sbtTemp.Append("' ")
        If bolIsChecked Then
            sbtTemp.Append("checked = ""true""")
        End If
        If Not bolIsEnabled Then
            sbtTemp.Append("disabled=""yes""")
        End If
        If strOtherAttributes <> "" Then
            sbtTemp.Append(" ")
            sbtTemp.Append(strOtherAttributes)
            sbtTemp.Append(" ")
        End If
        sbtTemp.Append("></td>")
        CreateCheckboxTD = sbtTemp.ToString
    End Function

    Private Function CreateCheckboxTD(ByVal strPKey As String, ByVal strName As String, _
                                      ByVal strID As String, ByVal bolIsChecked As Boolean, _
                                      Optional ByVal strOtherAttributes As String = "", _
                                      Optional ByVal bolIsEnabled As Boolean = True) As String
        Dim sbtTemp As New StringBuilder
        sbtTemp.Append("<td ")
        If bolIsEnabled = False Then
            sbtTemp.Append("class=""dcbox""")
        End If
        sbtTemp.Append(" width=""78px"" align=""center""><input type=""checkbox""")
        sbtTemp.Append(" name=""")
        sbtTemp.Append(strName)
        sbtTemp.Append(""" id='")
        sbtTemp.Append(strID)
        sbtTemp.Append("' ")
        sbtTemp.Append("onclick=""fnCP(this,'")
        sbtTemp.Append(strID)
        ' sbtTemp.Append(strID.Substring(strID.LastIndexOf("_") + 1, 1))
        sbtTemp.Append("','")
        sbtTemp.Append(strPKey)
        sbtTemp.Append("');"" ")

        If bolIsChecked Then
            sbtTemp.Append("checked = ""true""")
        End If

        If Not bolIsEnabled Then
            sbtTemp.Append("disabled=""yes""")
        End If

        If strOtherAttributes <> "" Then
            sbtTemp.Append(" ")
            sbtTemp.Append(strOtherAttributes)
            sbtTemp.Append(" ")
        End If
        sbtTemp.Append("></td>")
        CreateCheckboxTD = sbtTemp.ToString
    End Function

    Private Function AppendParentKeys(ByVal srcKey As String) As String
        Dim clNode As clsNodeData
        Dim sbrTemp As New StringBuilder
        Do
            clNode = GetNodeByKey(srcKey)
            If clNode.NodeParentKey <> "" Then
                sbrTemp.Append(clNode.NodeParentKey)
                sbrTemp.Append(",")
                srcKey = clNode.NodeParentKey
            End If
        Loop Until clNode.NodeParentKey = ""
        AppendParentKeys = sbrTemp.ToString
    End Function

    Private Function AppendChildKeys(ByVal srcKey As String) As String
        Dim clNode As clsNodeData
        Dim sbrTemp As New StringBuilder
        For Each clNode In NodeCollection
            If srcKey = clNode.NodeRootParentKey Then
                sbrTemp.Append(clNode.NodeKey)
                sbrTemp.Append(",")
            End If
        Next
        AppendChildKeys = sbrTemp.ToString
    End Function

    Private Function GetNodeByKey(ByVal srcKey As String) As clsNodeData
        Dim clNode As clsNodeData
        For Each clNode In NodeCollection
            If UCase(clNode.NodeKey) = UCase(srcKey) Then
                Return clNode
                Exit Function
            End If
        Next
    End Function

    Private Function ChildExists(ByVal srcKey As String) As Boolean
        Dim clNode As clsNodeData
        For Each clNode In NodeCollection
            If UCase(clNode.NodeParentKey) = UCase(srcKey) Then
                Return True
                Exit Function
            End If
        Next
    End Function

    Private Function SetLevel(ByVal parentKey As String) As Integer
        Dim clNode As clsNodeData
        For Each clNode In NodeCollection
            If UCase(clNode.NodeKey) = UCase(parentKey) Then
                Return clNode.NodeLevel
            End If
        Next
    End Function

    Private Function setNodeSpaces(ByVal pkey As String) As Boolean
        Dim clNode As clsNodeData
        For Each clNode In NodeCollection
            If UCase(clNode.NodeKey) = UCase(pkey) Then
                Me.NodeLeftSpaces = clNode.NodePrefixSpaces + 5
                Return True
                Exit Function
            End If
        Next
    End Function

    Private Property NodeLeftSpaces() As Integer
        Get
            NodeLeftSpaces = _NodeLeftSpaces
        End Get
        Set(ByVal Value As Integer)
            _NodeLeftSpaces = Value
        End Set
    End Property
End Class
