<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F03_不良品发生分析
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
        Me.截止日期 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.起始日期 = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.统计分析 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.关闭退出 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.重置条件 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.选择发现工程 = New System.Windows.Forms.ComboBox()
        Me.选择不良类型 = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        '截止日期
        '
        Me.截止日期.Location = New System.Drawing.Point(340, 20)
        Me.截止日期.Name = "截止日期"
        Me.截止日期.Size = New System.Drawing.Size(123, 21)
        Me.截止日期.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择不良类型"
        '
        '起始日期
        '
        Me.起始日期.Location = New System.Drawing.Point(128, 20)
        Me.起始日期.Name = "起始日期"
        Me.起始日期.Size = New System.Drawing.Size(123, 21)
        Me.起始日期.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "选择统计时间"
        '
        '统计分析
        '
        Me.统计分析.Location = New System.Drawing.Point(201, 157)
        Me.统计分析.Name = "统计分析"
        Me.统计分析.Size = New System.Drawing.Size(75, 23)
        Me.统计分析.TabIndex = 2
        Me.统计分析.Text = "统计分析"
        Me.统计分析.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(105, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "自"
        '
        '关闭退出
        '
        Me.关闭退出.Location = New System.Drawing.Point(340, 157)
        Me.关闭退出.Name = "关闭退出"
        Me.关闭退出.Size = New System.Drawing.Size(75, 23)
        Me.关闭退出.TabIndex = 2
        Me.关闭退出.Text = "关闭退出"
        Me.关闭退出.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(257, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "选择发现工程"
        '
        '重置条件
        '
        Me.重置条件.Location = New System.Drawing.Point(62, 157)
        Me.重置条件.Name = "重置条件"
        Me.重置条件.Size = New System.Drawing.Size(75, 23)
        Me.重置条件.TabIndex = 2
        Me.重置条件.Text = "重置条件"
        Me.重置条件.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(317, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "至"
        '
        '选择发现工程
        '
        Me.选择发现工程.FormattingEnabled = True
        Me.选择发现工程.Location = New System.Drawing.Point(340, 101)
        Me.选择发现工程.Name = "选择发现工程"
        Me.选择发现工程.Size = New System.Drawing.Size(123, 20)
        Me.选择发现工程.TabIndex = 1
        '
        '选择不良类型
        '
        Me.选择不良类型.FormattingEnabled = True
        Me.选择不良类型.Location = New System.Drawing.Point(128, 98)
        Me.选择不良类型.Name = "选择不良类型"
        Me.选择不良类型.Size = New System.Drawing.Size(123, 20)
        Me.选择不良类型.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.截止日期)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.起始日期)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.统计分析)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.关闭退出)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.重置条件)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.选择发现工程)
        Me.GroupBox1.Controls.Add(Me.选择不良类型)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(543, 246)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "数据分析"
        '
        'F03_不良品发生分析
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 273)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "F03_不良品发生分析"
        Me.Text = "F03_不良品发生分析"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents 截止日期 As Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents 起始日期 As Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents 统计分析 As Windows.Forms.Button
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents 关闭退出 As Windows.Forms.Button
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents 重置条件 As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents 选择发现工程 As Windows.Forms.ComboBox
    Friend WithEvents 选择不良类型 As Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
End Class
