<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class A01_文件基本信息
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(A01_文件基本信息))
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.新文件 = New System.Windows.Forms.Button()
        Me.添加 = New System.Windows.Forms.Button()
        Me.修改 = New System.Windows.Forms.Button()
        Me.删除 = New System.Windows.Forms.Button()
        Me.第一条 = New System.Windows.Forms.Button()
        Me.下一条 = New System.Windows.Forms.Button()
        Me.上一条 = New System.Windows.Forms.Button()
        Me.最末条 = New System.Windows.Forms.Button()
        Me.查询 = New System.Windows.Forms.Button()
        Me.退出 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.文件号 = New System.Windows.Forms.TextBox()
        Me.文件名称 = New System.Windows.Forms.TextBox()
        Me.版次 = New System.Windows.Forms.TextBox()
        Me.收件人 = New System.Windows.Forms.TextBox()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.存储名 = New System.Windows.Forms.TextBox()
        Me.存放位置 = New System.Windows.Forms.TextBox()
        Me.使用部门 = New System.Windows.Forms.ComboBox()
        Me.文件类别 = New System.Windows.Forms.ComboBox()
        Me.添加类别 = New System.Windows.Forms.Button()
        Me.添加部门 = New System.Windows.Forms.Button()
        Me.删除类别 = New System.Windows.Forms.Button()
        Me.删除部门 = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnGetAddress = New System.Windows.Forms.Button()
        Me.文件记录数目 = New System.Windows.Forms.Label()
        Me.发布日期 = New System.Windows.Forms.MaskedTextBox()
        Me.实施日期 = New System.Windows.Forms.MaskedTextBox()
        Me.btnDisplayUpdatingWindow = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView2
        '
        Me.ListView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ListView2.Location = New System.Drawing.Point(621, 4)
        Me.ListView2.MultiSelect = False
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(737, 593)
        Me.ListView2.TabIndex = 23
        Me.ListView2.UseCompatibleStateImageBehavior = False
        '
        '新文件
        '
        Me.新文件.Location = New System.Drawing.Point(549, 61)
        Me.新文件.Name = "新文件"
        Me.新文件.Size = New System.Drawing.Size(60, 23)
        Me.新文件.TabIndex = 24
        Me.新文件.Text = "新文件"
        Me.新文件.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(549, 117)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(60, 23)
        Me.添加.TabIndex = 25
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '修改
        '
        Me.修改.Location = New System.Drawing.Point(549, 173)
        Me.修改.Name = "修改"
        Me.修改.Size = New System.Drawing.Size(60, 23)
        Me.修改.TabIndex = 26
        Me.修改.Text = "修改"
        Me.修改.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Location = New System.Drawing.Point(549, 229)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(60, 23)
        Me.删除.TabIndex = 27
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '第一条
        '
        Me.第一条.Location = New System.Drawing.Point(549, 285)
        Me.第一条.Name = "第一条"
        Me.第一条.Size = New System.Drawing.Size(60, 23)
        Me.第一条.TabIndex = 28
        Me.第一条.Text = "第一条"
        Me.第一条.UseVisualStyleBackColor = True
        '
        '下一条
        '
        Me.下一条.Location = New System.Drawing.Point(549, 341)
        Me.下一条.Name = "下一条"
        Me.下一条.Size = New System.Drawing.Size(60, 23)
        Me.下一条.TabIndex = 29
        Me.下一条.Text = "下一条"
        Me.下一条.UseVisualStyleBackColor = True
        '
        '上一条
        '
        Me.上一条.Location = New System.Drawing.Point(549, 397)
        Me.上一条.Name = "上一条"
        Me.上一条.Size = New System.Drawing.Size(60, 23)
        Me.上一条.TabIndex = 30
        Me.上一条.Text = "上一条"
        Me.上一条.UseVisualStyleBackColor = True
        '
        '最末条
        '
        Me.最末条.Location = New System.Drawing.Point(549, 453)
        Me.最末条.Name = "最末条"
        Me.最末条.Size = New System.Drawing.Size(60, 23)
        Me.最末条.TabIndex = 31
        Me.最末条.Text = "最末条"
        Me.最末条.UseVisualStyleBackColor = True
        '
        '查询
        '
        Me.查询.Location = New System.Drawing.Point(549, 509)
        Me.查询.Name = "查询"
        Me.查询.Size = New System.Drawing.Size(60, 23)
        Me.查询.TabIndex = 32
        Me.查询.Text = "查询"
        Me.查询.UseVisualStyleBackColor = True
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(549, 565)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(60, 23)
        Me.退出.TabIndex = 33
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "文件号"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(196, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "文件名称"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(194, 119)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "文件类别"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 12)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "版  次:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(194, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 12)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "存储名"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 117)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 12)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "保管/收件"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(195, 83)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "存放位置"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 51)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "实施日期"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(193, 150)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "使用部门"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "发布日期"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 184)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "备   注:"
        '
        '文件号
        '
        Me.文件号.BackColor = System.Drawing.SystemColors.Window
        Me.文件号.Location = New System.Drawing.Point(66, 78)
        Me.文件号.Name = "文件号"
        Me.文件号.Size = New System.Drawing.Size(122, 21)
        Me.文件号.TabIndex = 2
        '
        '文件名称
        '
        Me.文件名称.Location = New System.Drawing.Point(252, 12)
        Me.文件名称.Name = "文件名称"
        Me.文件名称.Size = New System.Drawing.Size(284, 21)
        Me.文件名称.TabIndex = 6
        '
        '版次
        '
        Me.版次.Location = New System.Drawing.Point(67, 150)
        Me.版次.Name = "版次"
        Me.版次.Size = New System.Drawing.Size(122, 21)
        Me.版次.TabIndex = 4
        '
        '收件人
        '
        Me.收件人.Location = New System.Drawing.Point(66, 112)
        Me.收件人.Name = "收件人"
        Me.收件人.Size = New System.Drawing.Size(122, 21)
        Me.收件人.TabIndex = 3
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(67, 180)
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(471, 21)
        Me.备注.TabIndex = 5
        '
        '存储名
        '
        Me.存储名.Location = New System.Drawing.Point(253, 42)
        Me.存储名.Name = "存储名"
        Me.存储名.Size = New System.Drawing.Size(283, 21)
        Me.存储名.TabIndex = 7
        '
        '存放位置
        '
        Me.存放位置.Location = New System.Drawing.Point(252, 80)
        Me.存放位置.Name = "存放位置"
        Me.存放位置.Size = New System.Drawing.Size(121, 21)
        Me.存放位置.TabIndex = 8
        '
        '使用部门
        '
        Me.使用部门.FormattingEnabled = True
        Me.使用部门.Location = New System.Drawing.Point(252, 147)
        Me.使用部门.Name = "使用部门"
        Me.使用部门.Size = New System.Drawing.Size(121, 20)
        Me.使用部门.TabIndex = 10
        '
        '文件类别
        '
        Me.文件类别.FormattingEnabled = True
        Me.文件类别.Location = New System.Drawing.Point(253, 114)
        Me.文件类别.Name = "文件类别"
        Me.文件类别.Size = New System.Drawing.Size(120, 20)
        Me.文件类别.TabIndex = 9
        '
        '添加类别
        '
        Me.添加类别.Location = New System.Drawing.Point(379, 112)
        Me.添加类别.Name = "添加类别"
        Me.添加类别.Size = New System.Drawing.Size(75, 23)
        Me.添加类别.TabIndex = 11
        Me.添加类别.Text = "添加类别"
        Me.添加类别.UseVisualStyleBackColor = True
        '
        '添加部门
        '
        Me.添加部门.Location = New System.Drawing.Point(379, 146)
        Me.添加部门.Name = "添加部门"
        Me.添加部门.Size = New System.Drawing.Size(75, 23)
        Me.添加部门.TabIndex = 14
        Me.添加部门.Text = "添加部门"
        Me.添加部门.UseVisualStyleBackColor = True
        '
        '删除类别
        '
        Me.删除类别.Location = New System.Drawing.Point(461, 112)
        Me.删除类别.Name = "删除类别"
        Me.删除类别.Size = New System.Drawing.Size(75, 23)
        Me.删除类别.TabIndex = 12
        Me.删除类别.Text = "删除类别"
        Me.删除类别.UseVisualStyleBackColor = True
        '
        '删除部门
        '
        Me.删除部门.Location = New System.Drawing.Point(461, 144)
        Me.删除部门.Name = "删除部门"
        Me.删除部门.Size = New System.Drawing.Size(75, 23)
        Me.删除部门.TabIndex = 13
        Me.删除部门.Text = "删除部门"
        Me.删除部门.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Location = New System.Drawing.Point(6, 9)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(528, 383)
        Me.ListView1.TabIndex = 22
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ListView1)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 198)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(540, 398)
        Me.GroupBox1.TabIndex = 35
        Me.GroupBox1.TabStop = False
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(461, 79)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnOpen.TabIndex = 36
        Me.btnOpen.Text = "打开"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(549, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(59, 23)
        Me.Button2.TabIndex = 37
        Me.Button2.Text = "全导出"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnGetAddress
        '
        Me.btnGetAddress.Location = New System.Drawing.Point(379, 79)
        Me.btnGetAddress.Name = "btnGetAddress"
        Me.btnGetAddress.Size = New System.Drawing.Size(75, 23)
        Me.btnGetAddress.TabIndex = 38
        Me.btnGetAddress.Text = "获取路径"
        Me.btnGetAddress.UseVisualStyleBackColor = True
        '
        '文件记录数目
        '
        Me.文件记录数目.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.文件记录数目.AutoSize = True
        Me.文件记录数目.Location = New System.Drawing.Point(12, 620)
        Me.文件记录数目.Name = "文件记录数目"
        Me.文件记录数目.Size = New System.Drawing.Size(77, 12)
        Me.文件记录数目.TabIndex = 34
        Me.文件记录数目.Text = "文件记录数目"
        '
        '发布日期
        '
        Me.发布日期.Location = New System.Drawing.Point(66, 13)
        Me.发布日期.Name = "发布日期"
        Me.发布日期.Size = New System.Drawing.Size(122, 21)
        Me.发布日期.TabIndex = 39
        '
        '实施日期
        '
        Me.实施日期.Location = New System.Drawing.Point(66, 46)
        Me.实施日期.Name = "实施日期"
        Me.实施日期.Size = New System.Drawing.Size(122, 21)
        Me.实施日期.TabIndex = 40
        '
        'btnDisplayUpdatingWindow
        '
        Me.btnDisplayUpdatingWindow.Location = New System.Drawing.Point(434, 596)
        Me.btnDisplayUpdatingWindow.Name = "btnDisplayUpdatingWindow"
        Me.btnDisplayUpdatingWindow.Size = New System.Drawing.Size(101, 23)
        Me.btnDisplayUpdatingWindow.TabIndex = 41
        Me.btnDisplayUpdatingWindow.Text = "履历更新窗口"
        Me.btnDisplayUpdatingWindow.UseVisualStyleBackColor = True
        '
        'A01_文件基本信息
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 641)
        Me.Controls.Add(Me.btnDisplayUpdatingWindow)
        Me.Controls.Add(Me.实施日期)
        Me.Controls.Add(Me.发布日期)
        Me.Controls.Add(Me.文件记录数目)
        Me.Controls.Add(Me.btnGetAddress)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.文件号)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.删除部门)
        Me.Controls.Add(Me.删除类别)
        Me.Controls.Add(Me.添加部门)
        Me.Controls.Add(Me.添加类别)
        Me.Controls.Add(Me.查询)
        Me.Controls.Add(Me.文件类别)
        Me.Controls.Add(Me.最末条)
        Me.Controls.Add(Me.使用部门)
        Me.Controls.Add(Me.下一条)
        Me.Controls.Add(Me.存放位置)
        Me.Controls.Add(Me.退出)
        Me.Controls.Add(Me.存储名)
        Me.Controls.Add(Me.第一条)
        Me.Controls.Add(Me.备注)
        Me.Controls.Add(Me.上一条)
        Me.Controls.Add(Me.修改)
        Me.Controls.Add(Me.收件人)
        Me.Controls.Add(Me.删除)
        Me.Controls.Add(Me.版次)
        Me.Controls.Add(Me.添加)
        Me.Controls.Add(Me.文件名称)
        Me.Controls.Add(Me.新文件)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "A01_文件基本信息"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "文件基本信息"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView2 As Windows.Forms.ListView
    Friend WithEvents 新文件 As Windows.Forms.Button
    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents 修改 As Windows.Forms.Button
    Friend WithEvents 删除 As Windows.Forms.Button
    Friend WithEvents 第一条 As Windows.Forms.Button
    Friend WithEvents 下一条 As Windows.Forms.Button
    Friend WithEvents 上一条 As Windows.Forms.Button
    Friend WithEvents 最末条 As Windows.Forms.Button
    Friend WithEvents 查询 As Windows.Forms.Button
    Friend WithEvents 退出 As Windows.Forms.Button
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents 文件号 As Windows.Forms.TextBox
    Friend WithEvents 文件名称 As Windows.Forms.TextBox
    Friend WithEvents 版次 As Windows.Forms.TextBox
    Friend WithEvents 收件人 As Windows.Forms.TextBox
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents 存储名 As Windows.Forms.TextBox
    Friend WithEvents 存放位置 As Windows.Forms.TextBox
    Friend WithEvents 使用部门 As Windows.Forms.ComboBox
    Friend WithEvents 文件类别 As Windows.Forms.ComboBox
    Friend WithEvents 添加类别 As Windows.Forms.Button
    Friend WithEvents 添加部门 As Windows.Forms.Button
    Friend WithEvents 删除类别 As Windows.Forms.Button
    Friend WithEvents 删除部门 As Windows.Forms.Button
    Friend WithEvents ListView1 As Windows.Forms.ListView
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnOpen As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents btnGetAddress As Windows.Forms.Button
    Friend WithEvents 文件记录数目 As Windows.Forms.Label
    Friend WithEvents 发布日期 As Windows.Forms.MaskedTextBox
    Friend WithEvents 实施日期 As Windows.Forms.MaskedTextBox
    Friend WithEvents btnDisplayUpdatingWindow As Windows.Forms.Button
End Class
