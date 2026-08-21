<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class A03_文件创建更改管理
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(A03_文件创建更改管理))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.文件号 = New System.Windows.Forms.ComboBox()
        Me.更改类别 = New System.Windows.Forms.ComboBox()
        Me.更改描述 = New System.Windows.Forms.TextBox()
        Me.版次 = New System.Windows.Forms.TextBox()
        Me.签字 = New System.Windows.Forms.TextBox()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.添加 = New System.Windows.Forms.Button()
        Me.修改 = New System.Windows.Forms.Button()
        Me.删除 = New System.Windows.Forms.Button()
        Me.查询 = New System.Windows.Forms.Button()
        Me.新记录 = New System.Windows.Forms.Button()
        Me.退出 = New System.Windows.Forms.Button()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.更新日期 = New System.Windows.Forms.MaskedTextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "文件号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "更改类别"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "更新日期"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "更改描述"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(266, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "版次"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(266, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "签字"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(266, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "备注"
        '
        '文件号
        '
        Me.文件号.FormattingEnabled = True
        Me.文件号.Location = New System.Drawing.Point(81, 2)
        Me.文件号.Name = "文件号"
        Me.文件号.Size = New System.Drawing.Size(178, 20)
        Me.文件号.TabIndex = 1
        '
        '更改类别
        '
        Me.更改类别.FormattingEnabled = True
        Me.更改类别.Location = New System.Drawing.Point(81, 28)
        Me.更改类别.Name = "更改类别"
        Me.更改类别.Size = New System.Drawing.Size(178, 20)
        Me.更改类别.TabIndex = 2
        '
        '更改描述
        '
        Me.更改描述.Location = New System.Drawing.Point(81, 88)
        Me.更改描述.Multiline = True
        Me.更改描述.Name = "更改描述"
        Me.更改描述.Size = New System.Drawing.Size(420, 47)
        Me.更改描述.TabIndex = 7
        '
        '版次
        '
        Me.版次.Location = New System.Drawing.Point(323, 0)
        Me.版次.Name = "版次"
        Me.版次.Size = New System.Drawing.Size(178, 21)
        Me.版次.TabIndex = 4
        '
        '签字
        '
        Me.签字.Location = New System.Drawing.Point(323, 28)
        Me.签字.Name = "签字"
        Me.签字.Size = New System.Drawing.Size(178, 21)
        Me.签字.TabIndex = 5
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(323, 56)
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(178, 21)
        Me.备注.TabIndex = 6
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Location = New System.Drawing.Point(26, 141)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(478, 461)
        Me.ListView1.TabIndex = 14
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(507, 141)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(75, 23)
        Me.添加.TabIndex = 10
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '修改
        '
        Me.修改.Location = New System.Drawing.Point(507, 208)
        Me.修改.Name = "修改"
        Me.修改.Size = New System.Drawing.Size(75, 23)
        Me.修改.TabIndex = 11
        Me.修改.Text = "修改"
        Me.修改.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Location = New System.Drawing.Point(507, 275)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(75, 23)
        Me.删除.TabIndex = 12
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '查询
        '
        Me.查询.Location = New System.Drawing.Point(507, 7)
        Me.查询.Name = "查询"
        Me.查询.Size = New System.Drawing.Size(75, 23)
        Me.查询.TabIndex = 8
        Me.查询.Text = "查询"
        Me.查询.UseVisualStyleBackColor = True
        '
        '新记录
        '
        Me.新记录.Location = New System.Drawing.Point(507, 74)
        Me.新记录.Name = "新记录"
        Me.新记录.Size = New System.Drawing.Size(75, 23)
        Me.新记录.TabIndex = 9
        Me.新记录.Text = "新记录"
        Me.新记录.UseVisualStyleBackColor = True
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(507, 342)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(75, 23)
        Me.退出.TabIndex = 13
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        'ListView2
        '
        Me.ListView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView2.Location = New System.Drawing.Point(592, 0)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(775, 602)
        Me.ListView2.TabIndex = 15
        Me.ListView2.UseCompatibleStateImageBehavior = False
        '
        '更新日期
        '
        Me.更新日期.Location = New System.Drawing.Point(81, 56)
        Me.更新日期.Name = "更新日期"
        Me.更新日期.Size = New System.Drawing.Size(178, 21)
        Me.更新日期.TabIndex = 3
        '
        'A03_文件创建更改管理
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1379, 635)
        Me.Controls.Add(Me.更新日期)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.退出)
        Me.Controls.Add(Me.删除)
        Me.Controls.Add(Me.新记录)
        Me.Controls.Add(Me.修改)
        Me.Controls.Add(Me.查询)
        Me.Controls.Add(Me.添加)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.备注)
        Me.Controls.Add(Me.签字)
        Me.Controls.Add(Me.版次)
        Me.Controls.Add(Me.更改描述)
        Me.Controls.Add(Me.更改类别)
        Me.Controls.Add(Me.文件号)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "A03_文件创建更改管理"
        Me.Text = "文件履历卡"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents 文件号 As Windows.Forms.ComboBox
    Friend WithEvents 更改类别 As Windows.Forms.ComboBox
    Friend WithEvents 更改描述 As Windows.Forms.TextBox
    Friend WithEvents 版次 As Windows.Forms.TextBox
    Friend WithEvents 签字 As Windows.Forms.TextBox
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents ListView1 As Windows.Forms.ListView
    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents 修改 As Windows.Forms.Button
    Friend WithEvents 删除 As Windows.Forms.Button
    Friend WithEvents 查询 As Windows.Forms.Button
    Friend WithEvents 新记录 As Windows.Forms.Button
    Friend WithEvents 退出 As Windows.Forms.Button
    Friend WithEvents ListView2 As Windows.Forms.ListView
    Friend WithEvents 更新日期 As Windows.Forms.MaskedTextBox
End Class
