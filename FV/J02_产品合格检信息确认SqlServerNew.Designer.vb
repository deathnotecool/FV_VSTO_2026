<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class J02_产品合格检信息确认SqlServerNew
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
        Me.退出 = New System.Windows.Forms.Button()
        Me.执行排序 = New System.Windows.Forms.Button()
        Me.检验员 = New System.Windows.Forms.ComboBox()
        Me.btnImportFile = New System.Windows.Forms.Button()
        Me.添加 = New System.Windows.Forms.Button()
        Me.新建 = New System.Windows.Forms.Button()
        Me.btnClearPath = New System.Windows.Forms.Button()
        Me.类型区分 = New System.Windows.Forms.TextBox()
        Me.客户 = New System.Windows.Forms.TextBox()
        Me.型号 = New System.Windows.Forms.TextBox()
        Me.产品编号 = New System.Windows.Forms.MaskedTextBox()
        Me.检查区域 = New System.Windows.Forms.TextBox()
        Me.btnExportFile = New System.Windows.Forms.Button()
        Me.更新 = New System.Windows.Forms.Button()
        Me.排序字段 = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.工序ID = New System.Windows.Forms.ComboBox()
        Me.确认判定 = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.检查日期 = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdAuthorTitles1 = New System.Windows.Forms.DataGridView()
        Me.执行查询 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.项目 = New System.Windows.Forms.TextBox()
        Me.btnKuoZhan = New System.Windows.Forms.Button()
        Me.检查ID = New System.Windows.Forms.TextBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.路径 = New System.Windows.Forms.TextBox()
        Me.赔偿表证明1 = New System.Windows.Forms.Label()
        Me.lab2 = New System.Windows.Forms.Label()
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.btnConnectProcess = New System.Windows.Forms.Button()
        Me.btnReseting = New System.Windows.Forms.Button()
        Me.删除 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnOpenRev = New System.Windows.Forms.Button()
        Me.ckId查看 = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.查询条件 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.备注说明 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtChangeDisplay = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAuthorTitles1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(197, 148)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(40, 22)
        Me.退出.TabIndex = 40
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        '执行排序
        '
        Me.执行排序.Location = New System.Drawing.Point(175, 28)
        Me.执行排序.Name = "执行排序"
        Me.执行排序.Size = New System.Drawing.Size(78, 21)
        Me.执行排序.TabIndex = 41
        Me.执行排序.Text = "执行排序"
        Me.执行排序.UseVisualStyleBackColor = True
        '
        '检验员
        '
        Me.检验员.FormattingEnabled = True
        Me.检验员.Location = New System.Drawing.Point(334, 121)
        Me.检验员.Name = "检验员"
        Me.检验员.Size = New System.Drawing.Size(171, 20)
        Me.检验员.TabIndex = 51
        '
        'btnImportFile
        '
        Me.btnImportFile.Location = New System.Drawing.Point(511, 170)
        Me.btnImportFile.Name = "btnImportFile"
        Me.btnImportFile.Size = New System.Drawing.Size(69, 23)
        Me.btnImportFile.TabIndex = 50
        Me.btnImportFile.Text = "导入文件"
        Me.btnImportFile.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(59, 148)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(40, 22)
        Me.添加.TabIndex = 36
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(13, 148)
        Me.新建.Name = "新建"
        Me.新建.Size = New System.Drawing.Size(40, 22)
        Me.新建.TabIndex = 37
        Me.新建.Text = "新建"
        Me.新建.UseVisualStyleBackColor = True
        '
        'btnClearPath
        '
        Me.btnClearPath.Location = New System.Drawing.Point(436, 199)
        Me.btnClearPath.Name = "btnClearPath"
        Me.btnClearPath.Size = New System.Drawing.Size(69, 23)
        Me.btnClearPath.TabIndex = 58
        Me.btnClearPath.Text = "清除文件"
        Me.btnClearPath.UseVisualStyleBackColor = True
        '
        '类型区分
        '
        Me.类型区分.Location = New System.Drawing.Point(68, 76)
        Me.类型区分.Name = "类型区分"
        Me.类型区分.Size = New System.Drawing.Size(165, 21)
        Me.类型区分.TabIndex = 57
        '
        '客户
        '
        Me.客户.Location = New System.Drawing.Point(334, 27)
        Me.客户.Name = "客户"
        Me.客户.Size = New System.Drawing.Size(171, 21)
        Me.客户.TabIndex = 56
        '
        '型号
        '
        Me.型号.Location = New System.Drawing.Point(902, 27)
        Me.型号.Name = "型号"
        Me.型号.Size = New System.Drawing.Size(171, 21)
        Me.型号.TabIndex = 55
        '
        '产品编号
        '
        Me.产品编号.Location = New System.Drawing.Point(630, 27)
        Me.产品编号.Name = "产品编号"
        Me.产品编号.Size = New System.Drawing.Size(171, 21)
        Me.产品编号.TabIndex = 54
        '
        '检查区域
        '
        Me.检查区域.Location = New System.Drawing.Point(902, 76)
        Me.检查区域.Name = "检查区域"
        Me.检查区域.Size = New System.Drawing.Size(171, 21)
        Me.检查区域.TabIndex = 53
        '
        'btnExportFile
        '
        Me.btnExportFile.Location = New System.Drawing.Point(335, 199)
        Me.btnExportFile.Name = "btnExportFile"
        Me.btnExportFile.Size = New System.Drawing.Size(69, 23)
        Me.btnExportFile.TabIndex = 52
        Me.btnExportFile.Text = "打开文件"
        Me.btnExportFile.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(105, 148)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(40, 22)
        Me.更新.TabIndex = 38
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '排序字段
        '
        Me.排序字段.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.排序字段.FormattingEnabled = True
        Me.排序字段.Location = New System.Drawing.Point(63, 29)
        Me.排序字段.Name = "排序字段"
        Me.排序字段.Size = New System.Drawing.Size(98, 20)
        Me.排序字段.TabIndex = 32
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.PictureBox1)
        Me.GroupBox5.Location = New System.Drawing.Point(631, 107)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(158, 166)
        Me.GroupBox5.TabIndex = 49
        Me.GroupBox5.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(6, 14)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(143, 143)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        '工序ID
        '
        Me.工序ID.FormattingEnabled = True
        Me.工序ID.Location = New System.Drawing.Point(334, 76)
        Me.工序ID.Name = "工序ID"
        Me.工序ID.Size = New System.Drawing.Size(171, 20)
        Me.工序ID.TabIndex = 46
        '
        '确认判定
        '
        Me.确认判定.AutoSize = True
        Me.确认判定.Location = New System.Drawing.Point(68, 158)
        Me.确认判定.Name = "确认判定"
        Me.确认判定.Size = New System.Drawing.Size(72, 16)
        Me.确认判定.TabIndex = 45
        Me.确认判定.Text = "确认判定"
        Me.确认判定.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(299, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "客户"
        '
        '检查日期
        '
        Me.检查日期.Location = New System.Drawing.Point(68, 121)
        Me.检查日期.Name = "检查日期"
        Me.检查日期.Size = New System.Drawing.Size(165, 21)
        Me.检查日期.TabIndex = 42
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "检查ID"
        '
        'grdAuthorTitles1
        '
        Me.grdAuthorTitles1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAuthorTitles1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles1.Location = New System.Drawing.Point(6, 15)
        Me.grdAuthorTitles1.Name = "grdAuthorTitles1"
        Me.grdAuthorTitles1.RowTemplate.Height = 23
        Me.grdAuthorTitles1.Size = New System.Drawing.Size(258, 361)
        Me.grdAuthorTitles1.TabIndex = 36
        '
        '执行查询
        '
        Me.执行查询.Location = New System.Drawing.Point(175, 93)
        Me.执行查询.Name = "执行查询"
        Me.执行查询.Size = New System.Drawing.Size(38, 21)
        Me.执行查询.TabIndex = 42
        Me.执行查询.Text = "查询"
        Me.执行查询.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.grdAuthorTitles1)
        Me.GroupBox3.Location = New System.Drawing.Point(1161, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(271, 382)
        Me.GroupBox3.TabIndex = 69
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "检查工序信息"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 737)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1444, 25)
        Me.ToolStrip1.TabIndex = 66
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        '项目
        '
        Me.项目.Location = New System.Drawing.Point(630, 76)
        Me.项目.Name = "项目"
        Me.项目.Size = New System.Drawing.Size(171, 21)
        Me.项目.TabIndex = 47
        '
        'btnKuoZhan
        '
        Me.btnKuoZhan.Location = New System.Drawing.Point(59, 168)
        Me.btnKuoZhan.Name = "btnKuoZhan"
        Me.btnKuoZhan.Size = New System.Drawing.Size(40, 23)
        Me.btnKuoZhan.TabIndex = 61
        Me.btnKuoZhan.Text = "..."
        Me.ToolTip1.SetToolTip(Me.btnKuoZhan, "框选数据")
        Me.btnKuoZhan.UseVisualStyleBackColor = True
        '
        '检查ID
        '
        Me.检查ID.Enabled = False
        Me.检查ID.Location = New System.Drawing.Point(68, 27)
        Me.检查ID.Name = "检查ID"
        Me.检查ID.Size = New System.Drawing.Size(165, 21)
        Me.检查ID.TabIndex = 40
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(722, 279)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(67, 23)
        Me.btnOpenFile.TabIndex = 16
        Me.btnOpenFile.Text = "导出"
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(631, 279)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(67, 23)
        Me.btnImport.TabIndex = 15
        Me.btnImport.Text = "生成/显示"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        '路径
        '
        Me.路径.Enabled = False
        Me.路径.Location = New System.Drawing.Point(335, 172)
        Me.路径.Name = "路径"
        Me.路径.Size = New System.Drawing.Size(170, 21)
        Me.路径.TabIndex = 38
        '
        '赔偿表证明1
        '
        Me.赔偿表证明1.AutoSize = True
        Me.赔偿表证明1.Location = New System.Drawing.Point(571, 199)
        Me.赔偿表证明1.Name = "赔偿表证明1"
        Me.赔偿表证明1.Size = New System.Drawing.Size(41, 12)
        Me.赔偿表证明1.TabIndex = 14
        Me.赔偿表证明1.Text = "二维码"
        '
        'lab2
        '
        Me.lab2.AutoSize = True
        Me.lab2.Location = New System.Drawing.Point(269, 125)
        Me.lab2.Name = "lab2"
        Me.lab2.Size = New System.Drawing.Size(59, 12)
        Me.lab2.TabIndex = 9
        Me.lab2.Text = "确认/检验"
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(378, 238)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 34
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(107, 237)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 30
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(162, 238)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 31
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.grdAuthorTitles)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1143, 382)
        Me.GroupBox4.TabIndex = 68
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "合格确认信息"
        '
        'grdAuthorTitles
        '
        Me.grdAuthorTitles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAuthorTitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles.Location = New System.Drawing.Point(4, 15)
        Me.grdAuthorTitles.Name = "grdAuthorTitles"
        Me.grdAuthorTitles.RowTemplate.Height = 23
        Me.grdAuthorTitles.Size = New System.Drawing.Size(1133, 361)
        Me.grdAuthorTitles.TabIndex = 36
        '
        'btnConnectProcess
        '
        Me.btnConnectProcess.Location = New System.Drawing.Point(13, 265)
        Me.btnConnectProcess.Name = "btnConnectProcess"
        Me.btnConnectProcess.Size = New System.Drawing.Size(240, 22)
        Me.btnConnectProcess.TabIndex = 44
        Me.btnConnectProcess.Text = "增加所有关联工序--请慎用"
        Me.btnConnectProcess.UseVisualStyleBackColor = True
        '
        'btnReseting
        '
        Me.btnReseting.Location = New System.Drawing.Point(215, 93)
        Me.btnReseting.Name = "btnReseting"
        Me.btnReseting.Size = New System.Drawing.Size(38, 21)
        Me.btnReseting.TabIndex = 43
        Me.btnReseting.Text = "重设"
        Me.btnReseting.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Enabled = False
        Me.删除.Location = New System.Drawing.Point(151, 148)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(40, 22)
        Me.删除.TabIndex = 39
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox2.Controls.Add(Me.btnKuoZhan)
        Me.GroupBox2.Controls.Add(Me.btnOpenRev)
        Me.GroupBox2.Controls.Add(Me.ckId查看)
        Me.GroupBox2.Controls.Add(Me.btnConnectProcess)
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
        Me.GroupBox2.Location = New System.Drawing.Point(1161, 394)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(271, 334)
        Me.GroupBox2.TabIndex = 67
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        'btnOpenRev
        '
        Me.btnOpenRev.Location = New System.Drawing.Point(116, 195)
        Me.btnOpenRev.Name = "btnOpenRev"
        Me.btnOpenRev.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenRev.TabIndex = 46
        Me.btnOpenRev.Text = "打开版次"
        Me.btnOpenRev.UseVisualStyleBackColor = True
        '
        'ckId查看
        '
        Me.ckId查看.AutoSize = True
        Me.ckId查看.Location = New System.Drawing.Point(13, 195)
        Me.ckId查看.Name = "ckId查看"
        Me.ckId查看.Size = New System.Drawing.Size(60, 16)
        Me.ckId查看.TabIndex = 45
        Me.ckId查看.Text = "ID查看"
        Me.ckId查看.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 98)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "查询条件"
        '
        '查询条件
        '
        Me.查询条件.Location = New System.Drawing.Point(63, 94)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(98, 21)
        Me.查询条件.TabIndex = 34
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.查询条件, "清空后将刷新数据显示")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 33
        Me.Label10.Text = "排序字段"
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(323, 238)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 33
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(217, 238)
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
        Me.GroupBox1.Controls.Add(Me.btnClearPath)
        Me.GroupBox1.Controls.Add(Me.类型区分)
        Me.GroupBox1.Controls.Add(Me.客户)
        Me.GroupBox1.Controls.Add(Me.型号)
        Me.GroupBox1.Controls.Add(Me.产品编号)
        Me.GroupBox1.Controls.Add(Me.检查区域)
        Me.GroupBox1.Controls.Add(Me.btnExportFile)
        Me.GroupBox1.Controls.Add(Me.检验员)
        Me.GroupBox1.Controls.Add(Me.btnImportFile)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.项目)
        Me.GroupBox1.Controls.Add(Me.工序ID)
        Me.GroupBox1.Controls.Add(Me.确认判定)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.检查日期)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.检查ID)
        Me.GroupBox1.Controls.Add(Me.btnOpenFile)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.路径)
        Me.GroupBox1.Controls.Add(Me.赔偿表证明1)
        Me.GroupBox1.Controls.Add(Me.lab2)
        Me.GroupBox1.Controls.Add(Me.btnMoveLast)
        Me.GroupBox1.Controls.Add(Me.btnMoveNext)
        Me.GroupBox1.Controls.Add(Me.btnMoveFirst)
        Me.GroupBox1.Controls.Add(Me.btnMovePrevious)
        Me.GroupBox1.Controls.Add(Me.txtRecordPosition)
        Me.GroupBox1.Controls.Add(Me.lbl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.备注说明)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtChangeDisplay)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 394)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1143, 334)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "信息输入"
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        Me.lbl1.Location = New System.Drawing.Point(571, 31)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(53, 12)
        Me.lbl1.TabIndex = 1
        Me.lbl1.Text = "产品编号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "类型区分"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(595, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "项目"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 125)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 12)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "检查日期"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(867, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "型号"
        '
        '备注说明
        '
        Me.备注说明.Location = New System.Drawing.Point(902, 170)
        Me.备注说明.Multiline = True
        Me.备注说明.Name = "备注说明"
        Me.备注说明.Size = New System.Drawing.Size(177, 94)
        Me.备注说明.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(801, 214)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(53, 12)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "备注说明"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(287, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "工序ID"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(843, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "检查区域"
        '
        'txtChangeDisplay
        '
        Me.txtChangeDisplay.Location = New System.Drawing.Point(630, 49)
        Me.txtChangeDisplay.Name = "txtChangeDisplay"
        Me.txtChangeDisplay.Size = New System.Drawing.Size(171, 21)
        Me.txtChangeDisplay.TabIndex = 59
        Me.txtChangeDisplay.Visible = False
        '
        'J02_产品合格检信息确认SqlServerNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1444, 762)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "J02_产品合格检信息确认SqlServerNew"
        Me.Text = "J02_产品合格检信息确认SqlServerNew"
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAuthorTitles1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents 退出 As Windows.Forms.Button
    Friend WithEvents 执行排序 As Windows.Forms.Button
    Friend WithEvents 检验员 As Windows.Forms.ComboBox
    Friend WithEvents btnImportFile As Windows.Forms.Button
    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents 新建 As Windows.Forms.Button
    Friend WithEvents btnClearPath As Windows.Forms.Button
    Friend WithEvents 类型区分 As Windows.Forms.TextBox
    Friend WithEvents 客户 As Windows.Forms.TextBox
    Friend WithEvents 型号 As Windows.Forms.TextBox
    Friend WithEvents 产品编号 As Windows.Forms.MaskedTextBox
    Friend WithEvents 检查区域 As Windows.Forms.TextBox
    Friend WithEvents btnExportFile As Windows.Forms.Button
    Friend WithEvents 更新 As Windows.Forms.Button
    Friend WithEvents 排序字段 As Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents 工序ID As Windows.Forms.ComboBox
    Friend WithEvents 确认判定 As Windows.Forms.CheckBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents 检查日期 As Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents grdAuthorTitles1 As Windows.Forms.DataGridView
    Friend WithEvents 执行查询 As Windows.Forms.Button
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents 项目 As Windows.Forms.TextBox
    Friend WithEvents btnKuoZhan As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents 检查ID As Windows.Forms.TextBox
    Friend WithEvents btnOpenFile As Windows.Forms.Button
    Friend WithEvents btnImport As Windows.Forms.Button
    Friend WithEvents 路径 As Windows.Forms.TextBox
    Friend WithEvents 赔偿表证明1 As Windows.Forms.Label
    Friend WithEvents lab2 As Windows.Forms.Label
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents grdAuthorTitles As Windows.Forms.DataGridView
    Friend WithEvents btnConnectProcess As Windows.Forms.Button
    Friend WithEvents btnReseting As Windows.Forms.Button
    Friend WithEvents 删除 As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents btnOpenRev As Windows.Forms.Button
    Friend WithEvents ckId查看 As Windows.Forms.CheckBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents 查询条件 As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents lbl1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents 备注说明 As Windows.Forms.TextBox
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents txtChangeDisplay As Windows.Forms.TextBox
End Class
