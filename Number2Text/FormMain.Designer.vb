<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FormMain
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
    Public WithEvents ButtondGuess As Button
    Public WithEvents TextBoxNumber As TextBox
    Public WithEvents LabelResult As Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.ButtondGuess = New Button()
        Me.TextBoxNumber = New TextBox()
        Me.LabelResult = New Label()
        Me.ButtonAuto = New Button()
        Me.SuspendLayout()
        '
        'ButtondGuess
        '
        Me.ButtondGuess.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        Me.ButtondGuess.BackColor = System.Drawing.SystemColors.Control
        Me.ButtondGuess.Cursor = Cursors.Default
        Me.ButtondGuess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtondGuess.Location = New System.Drawing.Point(737, 3)
        Me.ButtondGuess.Name = "ButtondGuess"
        Me.ButtondGuess.RightToLeft = RightToLeft.No
        Me.ButtondGuess.Size = New System.Drawing.Size(83, 27)
        Me.ButtondGuess.TabIndex = 3
        Me.ButtondGuess.Text = "Jugar..."
        Me.ButtondGuess.UseVisualStyleBackColor = True
        '
        'TextBoxNumber
        '
        Me.TextBoxNumber.AcceptsReturn = True
        Me.TextBoxNumber.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) _
            Or AnchorStyles.Right), AnchorStyles)
        Me.TextBoxNumber.BackColor = System.Drawing.SystemColors.Window
        Me.TextBoxNumber.Cursor = Cursors.IBeam
        Me.TextBoxNumber.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxNumber.Location = New System.Drawing.Point(5, 5)
        Me.TextBoxNumber.MaxLength = 0
        Me.TextBoxNumber.Name = "TextBoxNumber"
        Me.TextBoxNumber.RightToLeft = RightToLeft.No
        Me.TextBoxNumber.Size = New System.Drawing.Size(630, 22)
        Me.TextBoxNumber.TabIndex = 0
        Me.TextBoxNumber.TextAlign = HorizontalAlignment.Right
        '
        'LabelResult
        '
        Me.LabelResult.Anchor = CType((((AnchorStyles.Top Or AnchorStyles.Bottom) _
            Or AnchorStyles.Left) _
            Or AnchorStyles.Right), AnchorStyles)
        Me.LabelResult.BackColor = System.Drawing.SystemColors.Window
        Me.LabelResult.BorderStyle = BorderStyle.Fixed3D
        Me.LabelResult.Cursor = Cursors.Default
        Me.LabelResult.Font = New System.Drawing.Font("Consolas", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.LabelResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelResult.Location = New System.Drawing.Point(6, 38)
        Me.LabelResult.Name = "LabelResult"
        Me.LabelResult.RightToLeft = RightToLeft.No
        Me.LabelResult.Size = New System.Drawing.Size(814, 273)
        Me.LabelResult.TabIndex = 1
        '
        'ButtonAuto
        '
        Me.ButtonAuto.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Right), AnchorStyles)
        Me.ButtonAuto.Location = New System.Drawing.Point(643, 3)
        Me.ButtonAuto.Name = "ButtonAuto"
        Me.ButtonAuto.Size = New System.Drawing.Size(87, 27)
        Me.ButtonAuto.TabIndex = 4
        Me.ButtonAuto.Text = "Auto"
        Me.ButtonAuto.UseVisualStyleBackColor = True
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(834, 321)
        Me.Controls.Add(Me.ButtonAuto)
        Me.Controls.Add(Me.ButtondGuess)
        Me.Controls.Add(Me.TextBoxNumber)
        Me.Controls.Add(Me.LabelResult)
        Me.Cursor = Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(294, 351)
        Me.Name = "FormMain"
        Me.RightToLeft = RightToLeft.No
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Number2Text"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonAuto As Button
#End Region
End Class