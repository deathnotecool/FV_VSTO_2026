<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN231210_多图排放
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN231210_多图排放))
        Me.rbDown = New System.Windows.Forms.RadioButton()
        Me.rbRight = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.cbDisplay = New System.Windows.Forms.CheckBox()
        Me.txtCellAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbDown
        '
        Me.rbDown.AutoSize = True
        Me.rbDown.Checked = True
        Me.rbDown.Location = New System.Drawing.Point(15, 63)
        Me.rbDown.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.rbDown.Name = "rbDown"
        Me.rbDown.Size = New System.Drawing.Size(71, 16)
        Me.rbDown.TabIndex = 0
        Me.rbDown.TabStop = True
        Me.rbDown.Text = "向下排列"
        Me.rbDown.UseVisualStyleBackColor = True
        '
        'rbRight
        '
        Me.rbRight.AutoSize = True
        Me.rbRight.Location = New System.Drawing.Point(155, 63)
        Me.rbRight.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.rbRight.Name = "rbRight"
        Me.rbRight.Size = New System.Drawing.Size(71, 16)
        Me.rbRight.TabIndex = 1
        Me.rbRight.Text = "向右排列"
        Me.rbRight.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.cbDisplay)
        Me.GroupBox1.Controls.Add(Me.txtCellAddress)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.rbRight)
        Me.GroupBox1.Controls.Add(Me.rbDown)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 18)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(410, 130)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "排放规则"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("宋体", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnExit.Location = New System.Drawing.Point(316, 59)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(68, 19)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "退出"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'cbDisplay
        '
        Me.cbDisplay.AutoSize = True
        Me.cbDisplay.Location = New System.Drawing.Point(15, 102)
        Me.cbDisplay.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbDisplay.Name = "cbDisplay"
        Me.cbDisplay.Size = New System.Drawing.Size(120, 16)
        Me.cbDisplay.TabIndex = 6
        Me.cbDisplay.Text = "添加图片行列标题"
        Me.cbDisplay.UseVisualStyleBackColor = True
        '
        'txtCellAddress
        '
        Me.txtCellAddress.Location = New System.Drawing.Point(187, 29)
        Me.txtCellAddress.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtCellAddress.Name = "txtCellAddress"
        Me.txtCellAddress.Size = New System.Drawing.Size(126, 21)
        Me.txtCellAddress.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 31)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "导入图片，指定一个参考单元格"
        '
        'btnImport
        '
        Me.btnImport.Font = New System.Drawing.Font("宋体", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnImport.Location = New System.Drawing.Point(316, 29)
        Me.btnImport.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(68, 20)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "导入"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'WIN231210_多图排放
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(428, 166)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WIN231210_多图排放"
        Me.Text = "多图排放"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rbDown As Windows.Forms.RadioButton
    Friend WithEvents rbRight As Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents cbDisplay As Windows.Forms.CheckBox
    Friend WithEvents txtCellAddress As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btnImport As Windows.Forms.Button
    Friend WithEvents btnExit As Windows.Forms.Button
End Class
