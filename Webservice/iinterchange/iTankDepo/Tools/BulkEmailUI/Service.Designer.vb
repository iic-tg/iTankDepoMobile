<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Service
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.llClearConsole = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tmrBulkEmail = New System.Timers.Timer()
        Me.bnStop = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rtbConsole = New System.Windows.Forms.RichTextBox()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        CType(Me.tmrBulkEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(607, 21)
        Me.txtInterval.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(75, 22)
        Me.txtInterval.TabIndex = 28
        Me.txtInterval.Text = "100"
        '
        'llClearConsole
        '
        Me.llClearConsole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.llClearConsole.AutoSize = True
        Me.llClearConsole.Location = New System.Drawing.Point(1016, 76)
        Me.llClearConsole.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.llClearConsole.Name = "llClearConsole"
        Me.llClearConsole.Size = New System.Drawing.Size(96, 17)
        Me.llClearConsole.TabIndex = 27
        Me.llClearConsole.TabStop = True
        Me.llClearConsole.Text = "Clear Console"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-1, 76)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 17)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Console"
        '
        'tmrBulkEmail
        '
        Me.tmrBulkEmail.AutoReset = False
        Me.tmrBulkEmail.SynchronizingObject = Me
        '
        'bnStop
        '
        Me.bnStop.Location = New System.Drawing.Point(171, 2)
        Me.bnStop.Margin = New System.Windows.Forms.Padding(4)
        Me.bnStop.Name = "bnStop"
        Me.bnStop.Size = New System.Drawing.Size(160, 59)
        Me.bnStop.TabIndex = 24
        Me.bnStop.Text = "Stop"
        Me.bnStop.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(367, 25)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 17)
        Me.lblStatus.TabIndex = 23
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(3, 2)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(160, 59)
        Me.btnStart.TabIndex = 22
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1111, 766)
        Me.ShapeContainer1.TabIndex = 30
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape1
        '
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 308
        Me.LineShape1.X2 = 383
        Me.LineShape1.Y1 = 258
        Me.LineShape1.Y2 = 281
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(543, 25)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 17)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Interval"
        '
        'rtbConsole
        '
        Me.rtbConsole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbConsole.Location = New System.Drawing.Point(3, 96)
        Me.rtbConsole.Margin = New System.Windows.Forms.Padding(4)
        Me.rtbConsole.Name = "rtbConsole"
        Me.rtbConsole.Size = New System.Drawing.Size(1108, 335)
        Me.rtbConsole.TabIndex = 25
        Me.rtbConsole.Text = ""
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportViewer1.Location = New System.Drawing.Point(4, 439)
        Me.ReportViewer1.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(1102, 307)
        Me.ReportViewer1.TabIndex = 31
        '
        'Service
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1111, 766)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.llClearConsole)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.rtbConsole)
        Me.Controls.Add(Me.bnStop)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Service"
        Me.Text = "Form1"
        CType(Me.tmrBulkEmail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents llClearConsole As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmrBulkEmail As System.Timers.Timer
    Friend WithEvents bnStop As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rtbConsole As System.Windows.Forms.RichTextBox
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer

End Class
