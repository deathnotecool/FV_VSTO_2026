<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class C06_物品消耗使用成本统计分析
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(C06_物品消耗使用成本统计分析))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.物品名称 = New System.Windows.Forms.ComboBox()
        Me.重置条件 = New System.Windows.Forms.Button()
        Me.关闭退出 = New System.Windows.Forms.Button()
        Me.统计分析 = New System.Windows.Forms.Button()
        Me.物品规格 = New System.Windows.Forms.ComboBox()
        Me.起始日期 = New System.Windows.Forms.DateTimePicker()
        Me.截止日期 = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择物品名称"
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(257, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "选择物品规格"
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
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(317, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "至"
        '
        '物品名称
        '
        Me.物品名称.FormattingEnabled = True
        Me.物品名称.Location = New System.Drawing.Point(128, 98)
        Me.物品名称.Name = "物品名称"
        Me.物品名称.Size = New System.Drawing.Size(123, 20)
        Me.物品名称.TabIndex = 1
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
        '关闭退出
        '
        Me.关闭退出.Location = New System.Drawing.Point(340, 157)
        Me.关闭退出.Name = "关闭退出"
        Me.关闭退出.Size = New System.Drawing.Size(75, 23)
        Me.关闭退出.TabIndex = 2
        Me.关闭退出.Text = "关闭退出"
        Me.关闭退出.UseVisualStyleBackColor = True
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
        '物品规格
        '
        Me.物品规格.FormattingEnabled = True
        Me.物品规格.Location = New System.Drawing.Point(340, 101)
        Me.物品规格.Name = "物品规格"
        Me.物品规格.Size = New System.Drawing.Size(123, 20)
        Me.物品规格.TabIndex = 1
        '
        '起始日期
        '
        Me.起始日期.Location = New System.Drawing.Point(128, 20)
        Me.起始日期.Name = "起始日期"
        Me.起始日期.Size = New System.Drawing.Size(123, 21)
        Me.起始日期.TabIndex = 3
        '
        '截止日期
        '
        Me.截止日期.Location = New System.Drawing.Point(340, 20)
        Me.截止日期.Name = "截止日期"
        Me.截止日期.Size = New System.Drawing.Size(123, 21)
        Me.截止日期.TabIndex = 3
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
        Me.GroupBox1.Controls.Add(Me.物品规格)
        Me.GroupBox1.Controls.Add(Me.物品名称)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(502, 200)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "数据分析"
        '
        'C06_物品消耗使用成本统计分析
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(524, 220)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "C06_物品消耗使用成本统计分析"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "C06_物品消耗使用成本统计分析"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents 物品名称 As Windows.Forms.ComboBox
    Friend WithEvents 重置条件 As Windows.Forms.Button
    Friend WithEvents 关闭退出 As Windows.Forms.Button
    Friend WithEvents 统计分析 As Windows.Forms.Button
    Friend WithEvents 物品规格 As Windows.Forms.ComboBox
    Friend WithEvents 起始日期 As Windows.Forms.DateTimePicker
    Friend WithEvents 截止日期 As Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
End Class
