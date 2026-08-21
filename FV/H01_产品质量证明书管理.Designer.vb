<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class H01_产品质量证明书管理
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.报告日期 = New System.Windows.Forms.MaskedTextBox()
        Me.技术条件 = New System.Windows.Forms.ComboBox()
        Me.区分 = New System.Windows.Forms.ComboBox()
        Me.交货状态 = New System.Windows.Forms.ComboBox()
        Me.品名 = New System.Windows.Forms.ComboBox()
        Me.获取路径 = New System.Windows.Forms.Button()
        Me.内部编号 = New System.Windows.Forms.TextBox()
        Me.评审完成 = New System.Windows.Forms.CheckBox()
        Me.打开 = New System.Windows.Forms.Button()
        Me.提供来源 = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.打开路径 = New System.Windows.Forms.TextBox()
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.锻件编号 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.材料炉号 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.数量 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.序列号 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.查询条件 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnReseting = New System.Windows.Forms.Button()
        Me.退出 = New System.Windows.Forms.Button()
        Me.执行查询 = New System.Windows.Forms.Button()
        Me.执行排序 = New System.Windows.Forms.Button()
        Me.删除 = New System.Windows.Forms.Button()
        Me.更新 = New System.Windows.Forms.Button()
        Me.添加 = New System.Windows.Forms.Button()
        Me.新建 = New System.Windows.Forms.Button()
        Me.排序字段 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.报告日期)
        Me.GroupBox1.Controls.Add(Me.技术条件)
        Me.GroupBox1.Controls.Add(Me.区分)
        Me.GroupBox1.Controls.Add(Me.交货状态)
        Me.GroupBox1.Controls.Add(Me.品名)
        Me.GroupBox1.Controls.Add(Me.获取路径)
        Me.GroupBox1.Controls.Add(Me.内部编号)
        Me.GroupBox1.Controls.Add(Me.评审完成)
        Me.GroupBox1.Controls.Add(Me.打开)
        Me.GroupBox1.Controls.Add(Me.提供来源)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.打开路径)
        Me.GroupBox1.Controls.Add(Me.btnMoveLast)
        Me.GroupBox1.Controls.Add(Me.btnMoveNext)
        Me.GroupBox1.Controls.Add(Me.btnMoveFirst)
        Me.GroupBox1.Controls.Add(Me.btnMovePrevious)
        Me.GroupBox1.Controls.Add(Me.txtRecordPosition)
        Me.GroupBox1.Controls.Add(Me.锻件编号)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.备注)
        Me.GroupBox1.Controls.Add(Me.材料炉号)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.数量)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.序列号)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 352)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(708, 155)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "单个证书信息"
        '
        '报告日期
        '
        Me.报告日期.Location = New System.Drawing.Point(61, 66)
        Me.报告日期.Name = "报告日期"
        Me.报告日期.Size = New System.Drawing.Size(106, 21)
        Me.报告日期.TabIndex = 33
        '
        '技术条件
        '
        Me.技术条件.FormattingEnabled = True
        Me.技术条件.Location = New System.Drawing.Point(245, 98)
        Me.技术条件.Name = "技术条件"
        Me.技术条件.Size = New System.Drawing.Size(106, 20)
        Me.技术条件.TabIndex = 10
        '
        '区分
        '
        Me.区分.FormattingEnabled = True
        Me.区分.Location = New System.Drawing.Point(410, 65)
        Me.区分.Name = "区分"
        Me.区分.Size = New System.Drawing.Size(106, 20)
        Me.区分.TabIndex = 6
        '
        '交货状态
        '
        Me.交货状态.FormattingEnabled = True
        Me.交货状态.Location = New System.Drawing.Point(245, 65)
        Me.交货状态.Name = "交货状态"
        Me.交货状态.Size = New System.Drawing.Size(106, 20)
        Me.交货状态.TabIndex = 7
        '
        '品名
        '
        Me.品名.FormattingEnabled = True
        Me.品名.Location = New System.Drawing.Point(410, 34)
        Me.品名.Name = "品名"
        Me.品名.Size = New System.Drawing.Size(106, 20)
        Me.品名.TabIndex = 3
        '
        '获取路径
        '
        Me.获取路径.Location = New System.Drawing.Point(595, 127)
        Me.获取路径.Name = "获取路径"
        Me.获取路径.Size = New System.Drawing.Size(48, 23)
        Me.获取路径.TabIndex = 13
        Me.获取路径.Text = "..."
        Me.获取路径.UseVisualStyleBackColor = True
        '
        '内部编号
        '
        Me.内部编号.Location = New System.Drawing.Point(410, 98)
        Me.内部编号.Name = "内部编号"
        Me.内部编号.Size = New System.Drawing.Size(106, 21)
        Me.内部编号.TabIndex = 11
        '
        '评审完成
        '
        Me.评审完成.AutoSize = True
        Me.评审完成.Checked = True
        Me.评审完成.CheckState = System.Windows.Forms.CheckState.Checked
        Me.评审完成.Location = New System.Drawing.Point(61, 130)
        Me.评审完成.Name = "评审完成"
        Me.评审完成.Size = New System.Drawing.Size(72, 16)
        Me.评审完成.TabIndex = 12
        Me.评审完成.Text = "评审完成"
        Me.评审完成.UseVisualStyleBackColor = True
        '
        '打开
        '
        Me.打开.Location = New System.Drawing.Point(654, 126)
        Me.打开.Name = "打开"
        Me.打开.Size = New System.Drawing.Size(48, 23)
        Me.打开.TabIndex = 14
        Me.打开.Text = "打开"
        Me.打开.UseVisualStyleBackColor = True
        '
        '提供来源
        '
        Me.提供来源.FormattingEnabled = True
        Me.提供来源.Location = New System.Drawing.Point(245, 34)
        Me.提供来源.Name = "提供来源"
        Me.提供来源.Size = New System.Drawing.Size(106, 20)
        Me.提供来源.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(351, 101)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "内部编号"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(186, 101)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "技术条件"
        '
        '打开路径
        '
        Me.打开路径.Enabled = False
        Me.打开路径.Location = New System.Drawing.Point(475, 128)
        Me.打开路径.Name = "打开路径"
        Me.打开路径.Size = New System.Drawing.Size(115, 21)
        Me.打开路径.TabIndex = 31
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(410, 128)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 30
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(355, 128)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 29
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(139, 127)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 28
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(194, 128)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 27
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(249, 128)
        Me.txtRecordPosition.Name = "txtRecordPosition"
        Me.txtRecordPosition.Size = New System.Drawing.Size(100, 21)
        Me.txtRecordPosition.TabIndex = 26
        Me.txtRecordPosition.TabStop = False
        Me.txtRecordPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '锻件编号
        '
        Me.锻件编号.Location = New System.Drawing.Point(596, 34)
        Me.锻件编号.Name = "锻件编号"
        Me.锻件编号.Size = New System.Drawing.Size(106, 21)
        Me.锻件编号.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "序列号"
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(595, 98)
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(106, 21)
        Me.备注.TabIndex = 5
        '
        '材料炉号
        '
        Me.材料炉号.Location = New System.Drawing.Point(596, 66)
        Me.材料炉号.Name = "材料炉号"
        Me.材料炉号.Size = New System.Drawing.Size(106, 21)
        Me.材料炉号.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(0, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "报告日期"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(375, 68)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "区分"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(375, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "品名"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "数量"
        '
        '数量
        '
        Me.数量.Location = New System.Drawing.Point(61, 98)
        Me.数量.Name = "数量"
        Me.数量.Size = New System.Drawing.Size(106, 21)
        Me.数量.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(179, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "提供来源"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(536, 101)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(29, 12)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "备注"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(537, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "锻件编号"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(537, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "材料炉号"
        '
        '序列号
        '
        Me.序列号.Enabled = False
        Me.序列号.Location = New System.Drawing.Point(61, 34)
        Me.序列号.Name = "序列号"
        Me.序列号.Size = New System.Drawing.Size(106, 21)
        Me.序列号.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(179, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "交货状态"
        '
        'grdAuthorTitles
        '
        Me.grdAuthorTitles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAuthorTitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles.Location = New System.Drawing.Point(6, 20)
        Me.grdAuthorTitles.Name = "grdAuthorTitles"
        Me.grdAuthorTitles.RowTemplate.Height = 23
        Me.grdAuthorTitles.Size = New System.Drawing.Size(1057, 317)
        Me.grdAuthorTitles.TabIndex = 36
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 510)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1080, 25)
        Me.ToolStrip1.TabIndex = 43
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        '
        '查询条件
        '
        Me.查询条件.Location = New System.Drawing.Point(82, 76)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(126, 21)
        Me.查询条件.TabIndex = 16
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.查询条件, "清空后将刷新数据显示")
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnReseting)
        Me.GroupBox2.Controls.Add(Me.退出)
        Me.GroupBox2.Controls.Add(Me.执行查询)
        Me.GroupBox2.Controls.Add(Me.执行排序)
        Me.GroupBox2.Controls.Add(Me.删除)
        Me.GroupBox2.Controls.Add(Me.更新)
        Me.GroupBox2.Controls.Add(Me.添加)
        Me.GroupBox2.Controls.Add(Me.新建)
        Me.GroupBox2.Controls.Add(Me.排序字段)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.查询条件)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(718, 352)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(351, 155)
        Me.GroupBox2.TabIndex = 44
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        'btnReseting
        '
        Me.btnReseting.Location = New System.Drawing.Point(292, 75)
        Me.btnReseting.Name = "btnReseting"
        Me.btnReseting.Size = New System.Drawing.Size(53, 21)
        Me.btnReseting.TabIndex = 25
        Me.btnReseting.Text = "重设"
        Me.btnReseting.UseVisualStyleBackColor = True
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(263, 118)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(60, 22)
        Me.退出.TabIndex = 24
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        '执行查询
        '
        Me.执行查询.Location = New System.Drawing.Point(231, 75)
        Me.执行查询.Name = "执行查询"
        Me.执行查询.Size = New System.Drawing.Size(53, 21)
        Me.执行查询.TabIndex = 17
        Me.执行查询.Text = "查询"
        Me.执行查询.UseVisualStyleBackColor = True
        '
        '执行排序
        '
        Me.执行排序.Location = New System.Drawing.Point(231, 30)
        Me.执行排序.Name = "执行排序"
        Me.执行排序.Size = New System.Drawing.Size(114, 21)
        Me.执行排序.TabIndex = 22
        Me.执行排序.Text = "执行排序"
        Me.执行排序.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Location = New System.Drawing.Point(197, 118)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(60, 22)
        Me.删除.TabIndex = 21
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(131, 118)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(60, 22)
        Me.更新.TabIndex = 20
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(65, 118)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(60, 22)
        Me.添加.TabIndex = 15
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(4, 118)
        Me.新建.Name = "新建"
        Me.新建.Size = New System.Drawing.Size(51, 22)
        Me.新建.TabIndex = 18
        Me.新建.Text = "新建"
        Me.新建.UseVisualStyleBackColor = True
        '
        '排序字段
        '
        Me.排序字段.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.排序字段.FormattingEnabled = True
        Me.排序字段.Location = New System.Drawing.Point(82, 32)
        Me.排序字段.Name = "排序字段"
        Me.排序字段.Size = New System.Drawing.Size(126, 20)
        Me.排序字段.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 79)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "查询条件"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(19, 35)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "排序字段"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.grdAuthorTitles)
        Me.GroupBox4.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1067, 343)
        Me.GroupBox4.TabIndex = 45
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "证书信息"
        '
        'H01_产品质量证明书管理
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1080, 535)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "H01_产品质量证明书管理"
        Me.Text = "H01_产品质量证明书管理"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents 打开路径 As Windows.Forms.TextBox
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents 锻件编号 As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents 材料炉号 As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents 数量 As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents 序列号 As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents grdAuthorTitles As Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents 查询条件 As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents 退出 As Windows.Forms.Button
    Friend WithEvents 执行查询 As Windows.Forms.Button
    Friend WithEvents 执行排序 As Windows.Forms.Button
    Friend WithEvents 删除 As Windows.Forms.Button
    Friend WithEvents 更新 As Windows.Forms.Button
    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents 新建 As Windows.Forms.Button
    Friend WithEvents 排序字段 As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents 提供来源 As Windows.Forms.ComboBox
    Friend WithEvents 打开 As Windows.Forms.Button
    Friend WithEvents 评审完成 As Windows.Forms.CheckBox
    Friend WithEvents 内部编号 As Windows.Forms.TextBox
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents 获取路径 As Windows.Forms.Button
    Friend WithEvents 品名 As Windows.Forms.ComboBox
    Friend WithEvents 技术条件 As Windows.Forms.ComboBox
    Friend WithEvents 区分 As Windows.Forms.ComboBox
    Friend WithEvents 交货状态 As Windows.Forms.ComboBox
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents 报告日期 As Windows.Forms.MaskedTextBox
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents btnReseting As Windows.Forms.Button
End Class
