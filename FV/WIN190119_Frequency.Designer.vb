<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WIN190119_Frequency
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN190119_Frequency))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAnalysisArea = New System.Windows.Forms.TextBox()
        Me.txtStartValue = New System.Windows.Forms.TextBox()
        Me.txtEndValue = New System.Windows.Forms.TextBox()
        Me.txtStepValue = New System.Windows.Forms.TextBox()
        Me.btnDetermineArea = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(43, 304)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(207, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Area of the analysis data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(43, 41)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "The start value"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(43, 129)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "The end value"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(43, 216)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 15)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Step length value"
        '
        'txtAnalysisArea
        '
        Me.txtAnalysisArea.Enabled = False
        Me.txtAnalysisArea.Location = New System.Drawing.Point(272, 300)
        Me.txtAnalysisArea.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAnalysisArea.Name = "txtAnalysisArea"
        Me.txtAnalysisArea.Size = New System.Drawing.Size(155, 25)
        Me.txtAnalysisArea.TabIndex = 4
        '
        'txtStartValue
        '
        Me.txtStartValue.Location = New System.Drawing.Point(272, 38)
        Me.txtStartValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStartValue.Name = "txtStartValue"
        Me.txtStartValue.Size = New System.Drawing.Size(155, 25)
        Me.txtStartValue.TabIndex = 1
        '
        'txtEndValue
        '
        Me.txtEndValue.Location = New System.Drawing.Point(272, 125)
        Me.txtEndValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEndValue.Name = "txtEndValue"
        Me.txtEndValue.Size = New System.Drawing.Size(155, 25)
        Me.txtEndValue.TabIndex = 2
        '
        'txtStepValue
        '
        Me.txtStepValue.Location = New System.Drawing.Point(272, 212)
        Me.txtStepValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStepValue.Name = "txtStepValue"
        Me.txtStepValue.Size = New System.Drawing.Size(155, 25)
        Me.txtStepValue.TabIndex = 3
        '
        'btnDetermineArea
        '
        Me.btnDetermineArea.Location = New System.Drawing.Point(436, 298)
        Me.btnDetermineArea.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDetermineArea.Name = "btnDetermineArea"
        Me.btnDetermineArea.Size = New System.Drawing.Size(55, 29)
        Me.btnDetermineArea.TabIndex = 4
        Me.btnDetermineArea.Text = "..."
        Me.btnDetermineArea.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(436, 334)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(55, 29)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "&Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'WIN190119_Frequency
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 368)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDetermineArea)
        Me.Controls.Add(Me.txtStepValue)
        Me.Controls.Add(Me.txtEndValue)
        Me.Controls.Add(Me.txtStartValue)
        Me.Controls.Add(Me.txtAnalysisArea)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "WIN190119_Frequency"
        Me.Text = "FrequencyAnalysis"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents txtAnalysisArea As Windows.Forms.TextBox
    Friend WithEvents txtStartValue As Windows.Forms.TextBox
    Friend WithEvents txtEndValue As Windows.Forms.TextBox
    Friend WithEvents txtStepValue As Windows.Forms.TextBox
    Friend WithEvents btnDetermineArea As Windows.Forms.Button
    Friend WithEvents btnExit As Windows.Forms.Button
End Class
