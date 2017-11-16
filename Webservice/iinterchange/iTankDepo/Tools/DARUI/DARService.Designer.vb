<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DARService
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
        Me.rtbConsole = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.llClearConsole = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.bnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.tmrDAR = New System.Timers.Timer()
        CType(Me.tmrDAR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rtbConsole
        '
        Me.rtbConsole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbConsole.Location = New System.Drawing.Point(5, 90)
        Me.rtbConsole.Name = "rtbConsole"
        Me.rtbConsole.Size = New System.Drawing.Size(832, 273)
        Me.rtbConsole.TabIndex = 26
        Me.rtbConsole.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Console"
        '
        'llClearConsole
        '
        Me.llClearConsole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.llClearConsole.AutoSize = True
        Me.llClearConsole.Location = New System.Drawing.Point(765, 74)
        Me.llClearConsole.Name = "llClearConsole"
        Me.llClearConsole.Size = New System.Drawing.Size(72, 13)
        Me.llClearConsole.TabIndex = 28
        Me.llClearConsole.TabStop = True
        Me.llClearConsole.Text = "Clear Console"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(410, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Interval"
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(458, 15)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(57, 20)
        Me.txtInterval.TabIndex = 32
        Me.txtInterval.Text = "100"
        '
        'bnStop
        '
        Me.bnStop.Location = New System.Drawing.Point(131, 0)
        Me.bnStop.Name = "bnStop"
        Me.bnStop.Size = New System.Drawing.Size(120, 48)
        Me.bnStop.TabIndex = 31
        Me.bnStop.Text = "Stop"
        Me.bnStop.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(5, 0)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(120, 48)
        Me.btnStart.TabIndex = 30
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'tmrDAR
        '
        Me.tmrDAR.AutoReset = False
        Me.tmrDAR.Interval = 60000.0R
        Me.tmrDAR.SynchronizingObject = Me
        '
        'DARService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 636)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.bnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.llClearConsole)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.rtbConsole)
        Me.Name = "DARService"
        Me.Text = "Form1"
        CType(Me.tmrDAR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtbConsole As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents llClearConsole As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents bnStop As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents tmrDAR As System.Timers.Timer

End Class
