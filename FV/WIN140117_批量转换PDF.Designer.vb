<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN140117_批量转换PDF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN140117_批量转换PDF))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.rbExcel = New System.Windows.Forms.RadioButton()
        Me.rbWord = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnStart)
        Me.GroupBox1.Controls.Add(Me.rbExcel)
        Me.GroupBox1.Controls.Add(Me.rbWord)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 11)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(348, 77)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "选择类型"
        '
        'btnStart
        '
        Me.btnStart.Font = New System.Drawing.Font("宋体", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnStart.Location = New System.Drawing.Point(261, 31)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(2)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(68, 20)
        Me.btnStart.TabIndex = 3
        Me.btnStart.Text = "开始"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'rbExcel
        '
        Me.rbExcel.AutoSize = True
        Me.rbExcel.Location = New System.Drawing.Point(127, 33)
        Me.rbExcel.Margin = New System.Windows.Forms.Padding(2)
        Me.rbExcel.Name = "rbExcel"
        Me.rbExcel.Size = New System.Drawing.Size(89, 16)
        Me.rbExcel.TabIndex = 1
        Me.rbExcel.Text = "EXCEL 转PDF"
        Me.rbExcel.UseVisualStyleBackColor = True
        '
        'rbWord
        '
        Me.rbWord.AutoSize = True
        Me.rbWord.Checked = True
        Me.rbWord.Location = New System.Drawing.Point(29, 33)
        Me.rbWord.Margin = New System.Windows.Forms.Padding(2)
        Me.rbWord.Name = "rbWord"
        Me.rbWord.Size = New System.Drawing.Size(83, 16)
        Me.rbWord.TabIndex = 0
        Me.rbWord.TabStop = True
        Me.rbWord.Text = "WORD 转PDF"
        Me.rbWord.UseVisualStyleBackColor = True
        '
        'WIN140117_批量转换PDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(372, 110)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WIN140117_批量转换PDF"
        Me.Text = "EXCEL & WORD 批量转换PDF"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnStart As Windows.Forms.Button
    Friend WithEvents rbExcel As Windows.Forms.RadioButton
    Friend WithEvents rbWord As Windows.Forms.RadioButton
End Class
