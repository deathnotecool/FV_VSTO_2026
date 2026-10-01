<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class L_PChart
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblMainNo = New System.Windows.Forms.Label()
        Me.lblMainName = New System.Windows.Forms.Label()
        Me.txtMainNo = New System.Windows.Forms.TextBox()
        Me.txtMainName = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlInput = New System.Windows.Forms.Panel()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblExpectedOutput = New System.Windows.Forms.Label()
        Me.lblSystemFunction = New System.Windows.Forms.Label()
        Me.lblControlFactor = New System.Windows.Forms.Label()
        Me.lblInfoInput = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.txtNoiseFactor = New System.Windows.Forms.TextBox()
        Me.txtUnexpectedOutput = New System.Windows.Forms.TextBox()
        Me.txtExpectedOutput = New System.Windows.Forms.TextBox()
        Me.txtSystemFunction = New System.Windows.Forms.TextBox()
        Me.txtControlFactor = New System.Windows.Forms.TextBox()
        Me.txtInfoInput = New System.Windows.Forms.TextBox()
        Me.lblNoiseFactor = New System.Windows.Forms.Label()
        Me.lblUnexpectedOutput = New System.Windows.Forms.Label()
        Me.lblStatusBar = New System.Windows.Forms.Label()
        Me.btnImportPChart = New System.Windows.Forms.Button()
        Me.pnlInput.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMainNo
        '
        Me.lblMainNo.AutoSize = True
        Me.lblMainNo.Location = New System.Drawing.Point(14, 34)
        Me.lblMainNo.Name = "lblMainNo"
        Me.lblMainNo.Size = New System.Drawing.Size(82, 15)
        Me.lblMainNo.TabIndex = 0
        Me.lblMainNo.Text = "工序编号："
        '
        'lblMainName
        '
        Me.lblMainName.AutoSize = True
        Me.lblMainName.Location = New System.Drawing.Point(281, 34)
        Me.lblMainName.Name = "lblMainName"
        Me.lblMainName.Size = New System.Drawing.Size(82, 15)
        Me.lblMainName.TabIndex = 0
        Me.lblMainName.Text = "工序名称："
        '
        'txtMainNo
        '
        Me.txtMainNo.Location = New System.Drawing.Point(102, 29)
        Me.txtMainNo.Name = "txtMainNo"
        Me.txtMainNo.ReadOnly = True
        Me.txtMainNo.Size = New System.Drawing.Size(173, 25)
        Me.txtMainNo.TabIndex = 1
        '
        'txtMainName
        '
        Me.txtMainName.Location = New System.Drawing.Point(369, 29)
        Me.txtMainName.Name = "txtMainName"
        Me.txtMainName.ReadOnly = True
        Me.txtMainName.Size = New System.Drawing.Size(173, 25)
        Me.txtMainName.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(548, 22)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(57, 38)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(611, 22)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(57, 38)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'pnlInput
        '
        Me.pnlInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlInput.AutoScroll = True
        Me.pnlInput.Controls.Add(Me.lblRemark)
        Me.pnlInput.Controls.Add(Me.lblExpectedOutput)
        Me.pnlInput.Controls.Add(Me.lblSystemFunction)
        Me.pnlInput.Controls.Add(Me.lblControlFactor)
        Me.pnlInput.Controls.Add(Me.lblInfoInput)
        Me.pnlInput.Controls.Add(Me.txtRemark)
        Me.pnlInput.Controls.Add(Me.txtNoiseFactor)
        Me.pnlInput.Controls.Add(Me.txtUnexpectedOutput)
        Me.pnlInput.Controls.Add(Me.txtExpectedOutput)
        Me.pnlInput.Controls.Add(Me.txtSystemFunction)
        Me.pnlInput.Controls.Add(Me.txtControlFactor)
        Me.pnlInput.Controls.Add(Me.txtInfoInput)
        Me.pnlInput.Controls.Add(Me.lblNoiseFactor)
        Me.pnlInput.Controls.Add(Me.lblUnexpectedOutput)
        Me.pnlInput.Location = New System.Drawing.Point(12, 71)
        Me.pnlInput.Name = "pnlInput"
        Me.pnlInput.Size = New System.Drawing.Size(957, 981)
        Me.pnlInput.TabIndex = 8
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(55, 910)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(52, 15)
        Me.lblRemark.TabIndex = 22
        Me.lblRemark.Text = "备注："
        '
        'lblExpectedOutput
        '
        Me.lblExpectedOutput.AutoSize = True
        Me.lblExpectedOutput.Location = New System.Drawing.Point(25, 492)
        Me.lblExpectedOutput.Name = "lblExpectedOutput"
        Me.lblExpectedOutput.Size = New System.Drawing.Size(82, 15)
        Me.lblExpectedOutput.TabIndex = 8
        Me.lblExpectedOutput.Text = "预期输出："
        '
        'lblSystemFunction
        '
        Me.lblSystemFunction.AutoSize = True
        Me.lblSystemFunction.Location = New System.Drawing.Point(25, 343)
        Me.lblSystemFunction.Name = "lblSystemFunction"
        Me.lblSystemFunction.Size = New System.Drawing.Size(82, 15)
        Me.lblSystemFunction.TabIndex = 9
        Me.lblSystemFunction.Text = "系统功能："
        '
        'lblControlFactor
        '
        Me.lblControlFactor.AutoSize = True
        Me.lblControlFactor.Location = New System.Drawing.Point(25, 199)
        Me.lblControlFactor.Name = "lblControlFactor"
        Me.lblControlFactor.Size = New System.Drawing.Size(82, 15)
        Me.lblControlFactor.TabIndex = 10
        Me.lblControlFactor.Text = "控制因子："
        '
        'lblInfoInput
        '
        Me.lblInfoInput.AutoSize = True
        Me.lblInfoInput.Location = New System.Drawing.Point(25, 61)
        Me.lblInfoInput.Name = "lblInfoInput"
        Me.lblInfoInput.Size = New System.Drawing.Size(82, 15)
        Me.lblInfoInput.TabIndex = 11
        Me.lblInfoInput.Text = "信息输入："
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Location = New System.Drawing.Point(113, 842)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(835, 133)
        Me.txtRemark.TabIndex = 7
        '
        'txtNoiseFactor
        '
        Me.txtNoiseFactor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNoiseFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoiseFactor.Location = New System.Drawing.Point(113, 703)
        Me.txtNoiseFactor.Multiline = True
        Me.txtNoiseFactor.Name = "txtNoiseFactor"
        Me.txtNoiseFactor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNoiseFactor.Size = New System.Drawing.Size(835, 133)
        Me.txtNoiseFactor.TabIndex = 6
        '
        'txtUnexpectedOutput
        '
        Me.txtUnexpectedOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUnexpectedOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnexpectedOutput.Location = New System.Drawing.Point(113, 564)
        Me.txtUnexpectedOutput.Multiline = True
        Me.txtUnexpectedOutput.Name = "txtUnexpectedOutput"
        Me.txtUnexpectedOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUnexpectedOutput.Size = New System.Drawing.Size(835, 133)
        Me.txtUnexpectedOutput.TabIndex = 5
        '
        'txtExpectedOutput
        '
        Me.txtExpectedOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExpectedOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpectedOutput.Location = New System.Drawing.Point(113, 425)
        Me.txtExpectedOutput.Multiline = True
        Me.txtExpectedOutput.Name = "txtExpectedOutput"
        Me.txtExpectedOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtExpectedOutput.Size = New System.Drawing.Size(835, 133)
        Me.txtExpectedOutput.TabIndex = 4
        '
        'txtSystemFunction
        '
        Me.txtSystemFunction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSystemFunction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSystemFunction.Location = New System.Drawing.Point(113, 286)
        Me.txtSystemFunction.Multiline = True
        Me.txtSystemFunction.Name = "txtSystemFunction"
        Me.txtSystemFunction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSystemFunction.Size = New System.Drawing.Size(835, 133)
        Me.txtSystemFunction.TabIndex = 3
        '
        'txtControlFactor
        '
        Me.txtControlFactor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtControlFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtControlFactor.Location = New System.Drawing.Point(113, 147)
        Me.txtControlFactor.Multiline = True
        Me.txtControlFactor.Name = "txtControlFactor"
        Me.txtControlFactor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtControlFactor.Size = New System.Drawing.Size(835, 133)
        Me.txtControlFactor.TabIndex = 2
        '
        'txtInfoInput
        '
        Me.txtInfoInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInfoInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInfoInput.Location = New System.Drawing.Point(113, 8)
        Me.txtInfoInput.Multiline = True
        Me.txtInfoInput.Name = "txtInfoInput"
        Me.txtInfoInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfoInput.Size = New System.Drawing.Size(835, 133)
        Me.txtInfoInput.TabIndex = 1
        '
        'lblNoiseFactor
        '
        Me.lblNoiseFactor.AutoSize = True
        Me.lblNoiseFactor.Location = New System.Drawing.Point(25, 763)
        Me.lblNoiseFactor.Name = "lblNoiseFactor"
        Me.lblNoiseFactor.Size = New System.Drawing.Size(82, 15)
        Me.lblNoiseFactor.TabIndex = 13
        Me.lblNoiseFactor.Text = "噪音因子："
        '
        'lblUnexpectedOutput
        '
        Me.lblUnexpectedOutput.AutoSize = True
        Me.lblUnexpectedOutput.Location = New System.Drawing.Point(10, 631)
        Me.lblUnexpectedOutput.Name = "lblUnexpectedOutput"
        Me.lblUnexpectedOutput.Size = New System.Drawing.Size(97, 15)
        Me.lblUnexpectedOutput.TabIndex = 14
        Me.lblUnexpectedOutput.Text = "非预期输出："
        '
        'lblStatusBar
        '
        Me.lblStatusBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusBar.Location = New System.Drawing.Point(821, 26)
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(143, 31)
        Me.lblStatusBar.TabIndex = 22
        Me.lblStatusBar.Text = "                         "
        Me.lblStatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnImportPChart
        '
        Me.btnImportPChart.Location = New System.Drawing.Point(674, 22)
        Me.btnImportPChart.Name = "btnImportPChart"
        Me.btnImportPChart.Size = New System.Drawing.Size(141, 38)
        Me.btnImportPChart.TabIndex = 23
        Me.btnImportPChart.Text = "从Excel导入P图"
        Me.btnImportPChart.UseVisualStyleBackColor = True
        '
        'L_PChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(981, 1055)
        Me.Controls.Add(Me.btnImportPChart)
        Me.Controls.Add(Me.lblStatusBar)
        Me.Controls.Add(Me.pnlInput)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtMainName)
        Me.Controls.Add(Me.txtMainNo)
        Me.Controls.Add(Me.lblMainName)
        Me.Controls.Add(Me.lblMainNo)
        Me.Name = "L_PChart"
        Me.Text = "L_PChart"
        Me.pnlInput.ResumeLayout(False)
        Me.pnlInput.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblMainNo As Windows.Forms.Label
    Friend WithEvents lblMainName As Windows.Forms.Label
    Friend WithEvents txtMainNo As Windows.Forms.TextBox
    Friend WithEvents txtMainName As Windows.Forms.TextBox
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents pnlInput As Windows.Forms.Panel
    Friend WithEvents lblStatusBar As Windows.Forms.Label
    Friend WithEvents lblExpectedOutput As Windows.Forms.Label
    Friend WithEvents lblSystemFunction As Windows.Forms.Label
    Friend WithEvents lblControlFactor As Windows.Forms.Label
    Friend WithEvents lblInfoInput As Windows.Forms.Label
    Friend WithEvents txtRemark As Windows.Forms.TextBox
    Friend WithEvents txtNoiseFactor As Windows.Forms.TextBox
    Friend WithEvents txtUnexpectedOutput As Windows.Forms.TextBox
    Friend WithEvents txtExpectedOutput As Windows.Forms.TextBox
    Friend WithEvents txtSystemFunction As Windows.Forms.TextBox
    Friend WithEvents txtControlFactor As Windows.Forms.TextBox
    Friend WithEvents txtInfoInput As Windows.Forms.TextBox
    Friend WithEvents lblNoiseFactor As Windows.Forms.Label
    Friend WithEvents lblUnexpectedOutput As Windows.Forms.Label
    Friend WithEvents lblRemark As Windows.Forms.Label
    Friend WithEvents btnImportPChart As Windows.Forms.Button
End Class
