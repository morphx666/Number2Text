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
    Public WithEvents ButtonGuess As System.Windows.Forms.Button
    Public WithEvents TextBoxNumber As System.Windows.Forms.TextBox
	Public WithEvents LabelResult As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.ButtonGuess = New System.Windows.Forms.Button()
        Me.TextBoxNumber = New System.Windows.Forms.TextBox()
        Me.LabelResult = New System.Windows.Forms.Label()
        Me.ButtonAuto = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonGuess
        '
        Me.ButtonGuess.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonGuess.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonGuess.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonGuess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonGuess.Location = New System.Drawing.Point(632, 3)
        Me.ButtonGuess.Name = "ButtonGuess"
        Me.ButtonGuess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonGuess.Size = New System.Drawing.Size(71, 23)
        Me.ButtonGuess.TabIndex = 3
        Me.ButtonGuess.Text = "Jugar..."
        Me.ButtonGuess.UseVisualStyleBackColor = True
        '
        'TextBoxNumber
        '
        Me.TextBoxNumber.AcceptsReturn = True
        Me.TextBoxNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNumber.BackColor = System.Drawing.SystemColors.Window
        Me.TextBoxNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxNumber.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxNumber.Location = New System.Drawing.Point(4, 4)
        Me.TextBoxNumber.MaxLength = 0
        Me.TextBoxNumber.Name = "TextBoxNumber"
        Me.TextBoxNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxNumber.Size = New System.Drawing.Size(541, 22)
        Me.TextBoxNumber.TabIndex = 0
        Me.TextBoxNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelResult
        '
        Me.LabelResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelResult.BackColor = System.Drawing.SystemColors.Window
        Me.LabelResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.LabelResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelResult.Location = New System.Drawing.Point(5, 33)
        Me.LabelResult.Name = "LabelResult"
        Me.LabelResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelResult.Size = New System.Drawing.Size(698, 237)
        Me.LabelResult.TabIndex = 1
        '
        'ButtonAuto
        '
        Me.ButtonAuto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAuto.Location = New System.Drawing.Point(551, 3)
        Me.ButtonAuto.Name = "ButtonAuto"
        Me.ButtonAuto.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAuto.TabIndex = 4
        Me.ButtonAuto.Text = "Auto"
        Me.ButtonAuto.UseVisualStyleBackColor = True
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(715, 278)
        Me.Controls.Add(Me.ButtonAuto)
        Me.Controls.Add(Me.ButtonGuess)
        Me.Controls.Add(Me.TextBoxNumber)
        Me.Controls.Add(Me.LabelResult)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(294, 351)
        Me.Name = "FormMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Number2Text"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonAuto As System.Windows.Forms.Button
#End Region
End Class