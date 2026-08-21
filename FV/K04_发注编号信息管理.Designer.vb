<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class K04_发注编号信息管理
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
        Me.components = New System.ComponentModel.Container()
        Me.添加 = New System.Windows.Forms.Button()
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtPosition1 = New System.Windows.Forms.TextBox()
        Me.图号 = New System.Windows.Forms.ComboBox()
        Me.净重 = New System.Windows.Forms.TextBox()
        Me.型号 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnDisplayingRedData = New System.Windows.Forms.Button()
        Me.btnReseting = New System.Windows.Forms.Button()
        Me.退出 = New System.Windows.Forms.Button()
        Me.执行查询 = New System.Windows.Forms.Button()
        Me.执行排序 = New System.Windows.Forms.Button()
        Me.删除 = New System.Windows.Forms.Button()
        Me.更新 = New System.Windows.Forms.Button()
        Me.新建 = New System.Windows.Forms.Button()
        Me.排序字段 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.查询条件 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.发货日期 = New System.Windows.Forms.MaskedTextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.入库完成 = New System.Windows.Forms.CheckBox()
        Me.区分 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.锻件编号 = New System.Windows.Forms.TextBox()
        Me.采购编码 = New System.Windows.Forms.TextBox()
        Me.数量 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.热处理号 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.备注说明 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.客户编号 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.材质 = New System.Windows.Forms.TextBox()
        Me.炉批号 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.规格 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.订单号 = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.grdAuthorTitles1 = New System.Windows.Forms.DataGridView()
        Me.供应商 = New System.Windows.Forms.TextBox()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdAuthorTitles1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(68, 137)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(40, 22)
        Me.添加.TabIndex = 21
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
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
        Me.grdAuthorTitles.Size = New System.Drawing.Size(915, 297)
        Me.grdAuthorTitles.TabIndex = 36
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(60, 190)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 22)
        Me.Button1.TabIndex = 39
        Me.Button1.Text = "准备扫码"
        Me.ToolTip1.SetToolTip(Me.Button1, "扫描前点击该按钮")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtPosition1
        '
        Me.txtPosition1.Location = New System.Drawing.Point(81, 191)
        Me.txtPosition1.Name = "txtPosition1"
        Me.txtPosition1.Size = New System.Drawing.Size(51, 21)
        Me.txtPosition1.TabIndex = 38
        '
        '图号
        '
        Me.图号.FormattingEnabled = True
        Me.图号.Location = New System.Drawing.Point(591, 33)
        Me.图号.Name = "图号"
        Me.图号.Size = New System.Drawing.Size(110, 20)
        Me.图号.TabIndex = 37
        '
        '净重
        '
        Me.净重.Location = New System.Drawing.Point(768, 69)
        Me.净重.Name = "净重"
        Me.净重.Size = New System.Drawing.Size(120, 21)
        Me.净重.TabIndex = 36
        '
        '型号
        '
        Me.型号.Location = New System.Drawing.Point(768, 29)
        Me.型号.Name = "型号"
        Me.型号.Size = New System.Drawing.Size(120, 21)
        Me.型号.TabIndex = 36
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox2.Controls.Add(Me.btnDisplayingRedData)
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
        Me.GroupBox2.Location = New System.Drawing.Point(939, 329)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(306, 225)
        Me.GroupBox2.TabIndex = 83
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        'btnDisplayingRedData
        '
        Me.btnDisplayingRedData.Location = New System.Drawing.Point(14, 187)
        Me.btnDisplayingRedData.Name = "btnDisplayingRedData"
        Me.btnDisplayingRedData.Size = New System.Drawing.Size(256, 25)
        Me.btnDisplayingRedData.TabIndex = 32
        Me.btnDisplayingRedData.Text = "未入库完成记录标识红色-请点该按钮查看"
        Me.btnDisplayingRedData.UseVisualStyleBackColor = True
        '
        'btnReseting
        '
        Me.btnReseting.Location = New System.Drawing.Point(230, 99)
        Me.btnReseting.Name = "btnReseting"
        Me.btnReseting.Size = New System.Drawing.Size(39, 21)
        Me.btnReseting.TabIndex = 30
        Me.btnReseting.Text = "重设"
        Me.btnReseting.UseVisualStyleBackColor = True
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(230, 137)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(40, 22)
        Me.退出.TabIndex = 25
        Me.退出.Text = "&Exit"
        Me.退出.UseVisualStyleBackColor = True
        '
        '执行查询
        '
        Me.执行查询.Location = New System.Drawing.Point(185, 99)
        Me.执行查询.Name = "执行查询"
        Me.执行查询.Size = New System.Drawing.Size(39, 21)
        Me.执行查询.TabIndex = 29
        Me.执行查询.Text = "查询"
        Me.执行查询.UseVisualStyleBackColor = True
        '
        '执行排序
        '
        Me.执行排序.Location = New System.Drawing.Point(185, 38)
        Me.执行排序.Name = "执行排序"
        Me.执行排序.Size = New System.Drawing.Size(84, 21)
        Me.执行排序.TabIndex = 28
        Me.执行排序.Text = "执行排序"
        Me.执行排序.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Enabled = False
        Me.删除.Location = New System.Drawing.Point(176, 137)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(40, 22)
        Me.删除.TabIndex = 24
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(122, 137)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(40, 22)
        Me.更新.TabIndex = 23
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(14, 137)
        Me.新建.Name = "新建"
        Me.新建.Size = New System.Drawing.Size(40, 22)
        Me.新建.TabIndex = 22
        Me.新建.Text = "新建"
        Me.新建.UseVisualStyleBackColor = True
        '
        '排序字段
        '
        Me.排序字段.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.排序字段.FormattingEnabled = True
        Me.排序字段.Location = New System.Drawing.Point(75, 38)
        Me.排序字段.Name = "排序字段"
        Me.排序字段.Size = New System.Drawing.Size(104, 20)
        Me.排序字段.TabIndex = 26
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(10, 103)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "查询条件"
        '
        '查询条件
        '
        Me.查询条件.Location = New System.Drawing.Point(73, 99)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(106, 21)
        Me.查询条件.TabIndex = 27
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.查询条件, "清空后将刷新数据显示")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 42)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "排序字段"
        '
        '发货日期
        '
        Me.发货日期.Location = New System.Drawing.Point(422, 35)
        Me.发货日期.Name = "发货日期"
        Me.发货日期.Size = New System.Drawing.Size(110, 21)
        Me.发货日期.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.发货日期, "请不要写产品不良发生日期")
        Me.发货日期.ValidatingType = GetType(Date)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 611)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1263, 25)
        Me.ToolStrip1.TabIndex = 82
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.grdAuthorTitles)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(927, 323)
        Me.GroupBox4.TabIndex = 84
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "新罗入库信息列表"
        '
        '入库完成
        '
        Me.入库完成.AutoSize = True
        Me.入库完成.Location = New System.Drawing.Point(60, 154)
        Me.入库完成.Name = "入库完成"
        Me.入库完成.Size = New System.Drawing.Size(72, 16)
        Me.入库完成.TabIndex = 18
        Me.入库完成.Text = "入库完成"
        Me.入库完成.UseVisualStyleBackColor = True
        '
        '区分
        '
        Me.区分.FormattingEnabled = True
        Me.区分.Location = New System.Drawing.Point(248, 75)
        Me.区分.Name = "区分"
        Me.区分.Size = New System.Drawing.Size(110, 20)
        Me.区分.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(552, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(29, 12)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "数量"
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(829, 164)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 34
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(774, 164)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 33
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(558, 163)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 30
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(613, 164)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 31
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(183, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 12)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "供应商"
        Me.ToolTip1.SetToolTip(Me.Label5, "请不要写产品不良发生日期")
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(668, 164)
        Me.txtRecordPosition.Name = "txtRecordPosition"
        Me.txtRecordPosition.Size = New System.Drawing.Size(100, 21)
        Me.txtRecordPosition.TabIndex = 32
        Me.txtRecordPosition.TabStop = False
        Me.txtRecordPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.供应商)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txtPosition1)
        Me.GroupBox1.Controls.Add(Me.图号)
        Me.GroupBox1.Controls.Add(Me.净重)
        Me.GroupBox1.Controls.Add(Me.型号)
        Me.GroupBox1.Controls.Add(Me.发货日期)
        Me.GroupBox1.Controls.Add(Me.入库完成)
        Me.GroupBox1.Controls.Add(Me.区分)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.btnMoveLast)
        Me.GroupBox1.Controls.Add(Me.btnMoveNext)
        Me.GroupBox1.Controls.Add(Me.btnMoveFirst)
        Me.GroupBox1.Controls.Add(Me.btnMovePrevious)
        Me.GroupBox1.Controls.Add(Me.txtRecordPosition)
        Me.GroupBox1.Controls.Add(Me.锻件编号)
        Me.GroupBox1.Controls.Add(Me.采购编码)
        Me.GroupBox1.Controls.Add(Me.数量)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.热处理号)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.备注说明)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.客户编号)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.材质)
        Me.GroupBox1.Controls.Add(Me.炉批号)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.规格)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.订单号)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 329)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(927, 225)
        Me.GroupBox1.TabIndex = 81
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "信息输入"
        '
        '锻件编号
        '
        Me.锻件编号.Location = New System.Drawing.Point(422, 115)
        Me.锻件编号.Name = "锻件编号"
        Me.锻件编号.Size = New System.Drawing.Size(110, 21)
        Me.锻件编号.TabIndex = 13
        '
        '采购编码
        '
        Me.采购编码.Location = New System.Drawing.Point(591, 114)
        Me.采购编码.Name = "采购编码"
        Me.采购编码.Size = New System.Drawing.Size(110, 21)
        Me.采购编码.TabIndex = 14
        '
        '数量
        '
        Me.数量.Location = New System.Drawing.Point(591, 71)
        Me.数量.Name = "数量"
        Me.数量.Size = New System.Drawing.Size(110, 21)
        Me.数量.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "订单号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(363, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "发货日期"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(715, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "型号"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(375, 78)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 12)
        Me.Label20.TabIndex = 20
        Me.Label20.Text = "材质"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 119)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 12)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "炉批号"
        '
        '热处理号
        '
        Me.热处理号.Location = New System.Drawing.Point(248, 114)
        Me.热处理号.Name = "热处理号"
        Me.热处理号.Size = New System.Drawing.Size(110, 21)
        Me.热处理号.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(183, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "区分"
        '
        '备注说明
        '
        Me.备注说明.Location = New System.Drawing.Point(248, 152)
        Me.备注说明.Multiline = True
        Me.备注说明.Name = "备注说明"
        Me.备注说明.Size = New System.Drawing.Size(284, 42)
        Me.备注说明.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(189, 165)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(53, 12)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "备注说明"
        '
        '客户编号
        '
        Me.客户编号.Location = New System.Drawing.Point(768, 115)
        Me.客户编号.Name = "客户编号"
        Me.客户编号.Size = New System.Drawing.Size(120, 21)
        Me.客户编号.TabIndex = 15
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(715, 118)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 12)
        Me.Label17.TabIndex = 24
        Me.Label17.Text = "客户编号"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(715, 79)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "净重"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(536, 118)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 12)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "采购编码"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(556, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "图号"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(361, 118)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 12)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Label15"
        '
        '材质
        '
        Me.材质.Location = New System.Drawing.Point(422, 75)
        Me.材质.Name = "材质"
        Me.材质.Size = New System.Drawing.Size(110, 21)
        Me.材质.TabIndex = 8
        '
        '炉批号
        '
        Me.炉批号.Location = New System.Drawing.Point(60, 115)
        Me.炉批号.Name = "炉批号"
        Me.炉批号.Size = New System.Drawing.Size(110, 21)
        Me.炉批号.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "规格"
        '
        '规格
        '
        Me.规格.Location = New System.Drawing.Point(60, 75)
        Me.规格.Name = "规格"
        Me.规格.Size = New System.Drawing.Size(110, 21)
        Me.规格.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(183, 118)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "热处理号"
        '
        '订单号
        '
        Me.订单号.Location = New System.Drawing.Point(60, 33)
        Me.订单号.Name = "订单号"
        Me.订单号.Size = New System.Drawing.Size(110, 21)
        Me.订单号.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.grdAuthorTitles1)
        Me.GroupBox3.Location = New System.Drawing.Point(939, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(306, 323)
        Me.GroupBox3.TabIndex = 85
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "图号选择"
        '
        'grdAuthorTitles1
        '
        Me.grdAuthorTitles1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAuthorTitles1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles1.Location = New System.Drawing.Point(6, 20)
        Me.grdAuthorTitles1.Name = "grdAuthorTitles1"
        Me.grdAuthorTitles1.RowTemplate.Height = 23
        Me.grdAuthorTitles1.Size = New System.Drawing.Size(293, 297)
        Me.grdAuthorTitles1.TabIndex = 36
        '
        '供应商
        '
        Me.供应商.Location = New System.Drawing.Point(248, 33)
        Me.供应商.Name = "供应商"
        Me.供应商.Size = New System.Drawing.Size(110, 21)
        Me.供应商.TabIndex = 40
        '
        'K04_发注编号信息管理
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1263, 636)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "K04_发注编号信息管理"
        Me.Text = "K04_发注编号信息管理"
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.grdAuthorTitles1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents grdAuthorTitles As Windows.Forms.DataGridView
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents txtPosition1 As Windows.Forms.TextBox
    Friend WithEvents 图号 As Windows.Forms.ComboBox
    Friend WithEvents 净重 As Windows.Forms.TextBox
    Friend WithEvents 型号 As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents btnDisplayingRedData As Windows.Forms.Button
    Friend WithEvents btnReseting As Windows.Forms.Button
    Friend WithEvents 退出 As Windows.Forms.Button
    Friend WithEvents 执行查询 As Windows.Forms.Button
    Friend WithEvents 执行排序 As Windows.Forms.Button
    Friend WithEvents 删除 As Windows.Forms.Button
    Friend WithEvents 更新 As Windows.Forms.Button
    Friend WithEvents 新建 As Windows.Forms.Button
    Friend WithEvents 排序字段 As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents 查询条件 As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents 发货日期 As Windows.Forms.MaskedTextBox
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents 入库完成 As Windows.Forms.CheckBox
    Friend WithEvents 区分 As Windows.Forms.ComboBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents 锻件编号 As Windows.Forms.TextBox
    Friend WithEvents 采购编码 As Windows.Forms.TextBox
    Friend WithEvents 数量 As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents 热处理号 As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents 备注说明 As Windows.Forms.TextBox
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents 客户编号 As Windows.Forms.TextBox
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents 材质 As Windows.Forms.TextBox
    Friend WithEvents 炉批号 As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents 规格 As Windows.Forms.TextBox
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents 订单号 As Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents grdAuthorTitles1 As Windows.Forms.DataGridView
    Friend WithEvents 供应商 As Windows.Forms.TextBox
End Class
