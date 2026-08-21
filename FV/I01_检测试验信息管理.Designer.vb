<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class I01_检测试验信息管理
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(I01_检测试验信息管理))
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.报告日期 = New System.Windows.Forms.MaskedTextBox()
        Me.报告单号 = New System.Windows.Forms.MaskedTextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.合格确认 = New System.Windows.Forms.CheckBox()
        Me.品名 = New System.Windows.Forms.ComboBox()
        Me.目的区分 = New System.Windows.Forms.ComboBox()
        Me.供应商 = New System.Windows.Forms.ComboBox()
        Me.lab2 = New System.Windows.Forms.Label()
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ID = New System.Windows.Forms.TextBox()
        Me.执行查询 = New System.Windows.Forms.Button()
        Me.执行排序 = New System.Windows.Forms.Button()
        Me.删除 = New System.Windows.Forms.Button()
        Me.更新 = New System.Windows.Forms.Button()
        Me.添加 = New System.Windows.Forms.Button()
        Me.新建 = New System.Windows.Forms.Button()
        Me.排序字段 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.退出 = New System.Windows.Forms.Button()
        Me.查询条件 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnResetting = New System.Windows.Forms.Button()
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.材质 = New System.Windows.Forms.ComboBox()
        Me.检验项目 = New System.Windows.Forms.ComboBox()
        Me.检测依据 = New System.Windows.Forms.ComboBox()
        Me.客户 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.报告来源 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.文件资料 = New System.Windows.Forms.TextBox()
        Me.rtxtSpec = New System.Windows.Forms.RichTextBox()
        Me.picbDisplayPicture = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picbDisplayPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(915, 109)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(61, 23)
        Me.btnOpenFile.TabIndex = 15
        Me.btnOpenFile.Text = "打开"
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(848, 109)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(61, 23)
        Me.btnImport.TabIndex = 14
        Me.btnImport.Text = "导入"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        '报告日期
        '
        Me.报告日期.Location = New System.Drawing.Point(389, 16)
        Me.报告日期.Name = "报告日期"
        Me.报告日期.Size = New System.Drawing.Size(110, 21)
        Me.报告日期.TabIndex = 3
        Me.报告日期.ValidatingType = GetType(Date)
        '
        '报告单号
        '
        Me.报告单号.Location = New System.Drawing.Point(210, 16)
        Me.报告单号.Name = "报告单号"
        Me.报告单号.Size = New System.Drawing.Size(110, 21)
        Me.报告单号.TabIndex = 2
        Me.报告单号.ValidatingType = GetType(Date)
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(60, 308)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(72, 16)
        Me.CheckBox1.TabIndex = 33
        Me.CheckBox1.Text = "处置完成"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        '合格确认
        '
        Me.合格确认.AutoSize = True
        Me.合格确认.Location = New System.Drawing.Point(210, 115)
        Me.合格确认.Name = "合格确认"
        Me.合格确认.Size = New System.Drawing.Size(72, 16)
        Me.合格确认.TabIndex = 12
        Me.合格确认.Text = "合格确认"
        Me.合格确认.UseVisualStyleBackColor = True
        '
        '品名
        '
        Me.品名.FormattingEnabled = True
        Me.品名.Location = New System.Drawing.Point(866, 16)
        Me.品名.Name = "品名"
        Me.品名.Size = New System.Drawing.Size(110, 20)
        Me.品名.TabIndex = 6
        '
        '目的区分
        '
        Me.目的区分.FormattingEnabled = True
        Me.目的区分.Location = New System.Drawing.Point(866, 63)
        Me.目的区分.Name = "目的区分"
        Me.目的区分.Size = New System.Drawing.Size(110, 20)
        Me.目的区分.TabIndex = 10
        '
        '供应商
        '
        Me.供应商.FormattingEnabled = True
        Me.供应商.Location = New System.Drawing.Point(711, 16)
        Me.供应商.Name = "供应商"
        Me.供应商.Size = New System.Drawing.Size(110, 20)
        Me.供应商.TabIndex = 5
        '
        'lab2
        '
        Me.lab2.AutoSize = True
        Me.lab2.Location = New System.Drawing.Point(151, 67)
        Me.lab2.Name = "lab2"
        Me.lab2.Size = New System.Drawing.Size(53, 12)
        Me.lab2.TabIndex = 8
        Me.lab2.Text = "检验项目"
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(281, 168)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 19
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(226, 168)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 18
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(10, 167)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 16
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(65, 168)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 16
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(120, 168)
        Me.txtRecordPosition.Name = "txtRecordPosition"
        Me.txtRecordPosition.Size = New System.Drawing.Size(100, 21)
        Me.txtRecordPosition.TabIndex = 17
        Me.txtRecordPosition.TabStop = False
        Me.txtRecordPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        Me.lbl1.Location = New System.Drawing.Point(6, 20)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(17, 12)
        Me.lbl1.TabIndex = 1
        Me.lbl1.Text = "ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(328, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "报告日期"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(662, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "供应商"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(4, 67)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 12)
        Me.Label20.TabIndex = 7
        Me.Label20.Text = "材质"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(149, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "报告单号"
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(404, 138)
        Me.备注.Multiline = True
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(572, 77)
        Me.备注.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(364, 172)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(29, 12)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "备注"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(827, 67)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "目的"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(507, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "客户"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(829, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "品名"
        '
        'ID
        '
        Me.ID.Enabled = False
        Me.ID.Location = New System.Drawing.Point(31, 16)
        Me.ID.Name = "ID"
        Me.ID.Size = New System.Drawing.Size(110, 21)
        Me.ID.TabIndex = 1
        '
        '执行查询
        '
        Me.执行查询.Location = New System.Drawing.Point(204, 99)
        Me.执行查询.Name = "执行查询"
        Me.执行查询.Size = New System.Drawing.Size(39, 21)
        Me.执行查询.TabIndex = 26
        Me.执行查询.Text = "查询"
        Me.执行查询.UseVisualStyleBackColor = True
        '
        '执行排序
        '
        Me.执行排序.Location = New System.Drawing.Point(206, 32)
        Me.执行排序.Name = "执行排序"
        Me.执行排序.Size = New System.Drawing.Size(82, 21)
        Me.执行排序.TabIndex = 23
        Me.执行排序.Text = "执行排序"
        Me.执行排序.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Enabled = False
        Me.删除.Location = New System.Drawing.Point(174, 162)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(40, 22)
        Me.删除.TabIndex = 30
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(120, 162)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(40, 22)
        Me.更新.TabIndex = 29
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(66, 162)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(40, 22)
        Me.添加.TabIndex = 28
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(12, 162)
        Me.新建.Name = "新建"
        Me.新建.Size = New System.Drawing.Size(40, 22)
        Me.新建.TabIndex = 27
        Me.新建.Text = "新建"
        Me.新建.UseVisualStyleBackColor = True
        '
        '排序字段
        '
        Me.排序字段.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.排序字段.FormattingEnabled = True
        Me.排序字段.Location = New System.Drawing.Point(73, 34)
        Me.排序字段.Name = "排序字段"
        Me.排序字段.Size = New System.Drawing.Size(126, 20)
        Me.排序字段.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 102)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "查询条件"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 591)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1314, 25)
        Me.ToolStrip1.TabIndex = 51
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(228, 162)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(40, 22)
        Me.退出.TabIndex = 31
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        '查询条件
        '
        Me.查询条件.Location = New System.Drawing.Point(71, 99)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(126, 21)
        Me.查询条件.TabIndex = 25
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.查询条件, "清空后将刷新数据显示")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(10, 37)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "排序字段"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox2.Controls.Add(Me.btnResetting)
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
        Me.GroupBox2.Location = New System.Drawing.Point(997, 363)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(299, 225)
        Me.GroupBox2.TabIndex = 52
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        'btnResetting
        '
        Me.btnResetting.Location = New System.Drawing.Point(249, 98)
        Me.btnResetting.Name = "btnResetting"
        Me.btnResetting.Size = New System.Drawing.Size(39, 21)
        Me.btnResetting.TabIndex = 32
        Me.btnResetting.Text = "重设"
        Me.btnResetting.UseVisualStyleBackColor = True
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
        Me.grdAuthorTitles.Size = New System.Drawing.Size(744, 330)
        Me.grdAuthorTitles.TabIndex = 36
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.grdAuthorTitles)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 1)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(756, 356)
        Me.GroupBox4.TabIndex = 53
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "索赔信息列表"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.材质)
        Me.GroupBox1.Controls.Add(Me.检验项目)
        Me.GroupBox1.Controls.Add(Me.检测依据)
        Me.GroupBox1.Controls.Add(Me.客户)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.报告来源)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnOpenFile)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.报告日期)
        Me.GroupBox1.Controls.Add(Me.报告单号)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.合格确认)
        Me.GroupBox1.Controls.Add(Me.品名)
        Me.GroupBox1.Controls.Add(Me.目的区分)
        Me.GroupBox1.Controls.Add(Me.供应商)
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
        Me.GroupBox1.Controls.Add(Me.备注)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.ID)
        Me.GroupBox1.Controls.Add(Me.文件资料)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 363)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(983, 225)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "信息输入"
        '
        '材质
        '
        Me.材质.FormattingEnabled = True
        Me.材质.Location = New System.Drawing.Point(31, 63)
        Me.材质.Name = "材质"
        Me.材质.Size = New System.Drawing.Size(110, 20)
        Me.材质.TabIndex = 7
        '
        '检验项目
        '
        Me.检验项目.FormattingEnabled = True
        Me.检验项目.Location = New System.Drawing.Point(210, 63)
        Me.检验项目.Name = "检验项目"
        Me.检验项目.Size = New System.Drawing.Size(295, 20)
        Me.检验项目.TabIndex = 8
        '
        '检测依据
        '
        Me.检测依据.FormattingEnabled = True
        Me.检测依据.Location = New System.Drawing.Point(544, 63)
        Me.检测依据.Name = "检测依据"
        Me.检测依据.Size = New System.Drawing.Size(277, 20)
        Me.检测依据.TabIndex = 9
        '
        '客户
        '
        Me.客户.FormattingEnabled = True
        Me.客户.Location = New System.Drawing.Point(544, 16)
        Me.客户.Name = "客户"
        Me.客户.Size = New System.Drawing.Size(110, 20)
        Me.客户.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(483, 114)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "文件资料"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(509, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "依据"
        '
        '报告来源
        '
        Me.报告来源.Location = New System.Drawing.Point(31, 112)
        Me.报告来源.Name = "报告来源"
        Me.报告来源.Size = New System.Drawing.Size(110, 21)
        Me.报告来源.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "来源"
        '
        '文件资料
        '
        Me.文件资料.Location = New System.Drawing.Point(544, 109)
        Me.文件资料.Name = "文件资料"
        Me.文件资料.Size = New System.Drawing.Size(277, 21)
        Me.文件资料.TabIndex = 13
        '
        'rtxtSpec
        '
        Me.rtxtSpec.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtxtSpec.Location = New System.Drawing.Point(6, 20)
        Me.rtxtSpec.Name = "rtxtSpec"
        Me.rtxtSpec.Size = New System.Drawing.Size(345, 330)
        Me.rtxtSpec.TabIndex = 54
        Me.rtxtSpec.Text = ""
        '
        'picbDisplayPicture
        '
        Me.picbDisplayPicture.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picbDisplayPicture.Location = New System.Drawing.Point(6, 20)
        Me.picbDisplayPicture.Name = "picbDisplayPicture"
        Me.picbDisplayPicture.Size = New System.Drawing.Size(152, 330)
        Me.picbDisplayPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picbDisplayPicture.TabIndex = 55
        Me.picbDisplayPicture.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.rtxtSpec)
        Me.GroupBox3.Location = New System.Drawing.Point(770, 1)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(357, 356)
        Me.GroupBox3.TabIndex = 56
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Spec"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.picbDisplayPicture)
        Me.GroupBox5.Location = New System.Drawing.Point(1132, 1)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(164, 356)
        Me.GroupBox5.TabIndex = 57
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "图片"
        '
        'I01_检测试验信息管理
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1314, 616)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "I01_检测试验信息管理"
        Me.Text = "I01_检测试验信息管理"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picbDisplayPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOpenFile As Windows.Forms.Button
    Friend WithEvents btnImport As Windows.Forms.Button
    Friend WithEvents 报告日期 As Windows.Forms.MaskedTextBox
    Friend WithEvents 报告单号 As Windows.Forms.MaskedTextBox
    Friend WithEvents CheckBox1 As Windows.Forms.CheckBox
    Friend WithEvents 合格确认 As Windows.Forms.CheckBox
    Friend WithEvents 品名 As Windows.Forms.ComboBox
    Friend WithEvents 目的区分 As Windows.Forms.ComboBox
    Friend WithEvents 供应商 As Windows.Forms.ComboBox
    Friend WithEvents lab2 As Windows.Forms.Label
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents lbl1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents ID As Windows.Forms.TextBox
    Friend WithEvents 执行查询 As Windows.Forms.Button
    Friend WithEvents 执行排序 As Windows.Forms.Button
    Friend WithEvents 删除 As Windows.Forms.Button
    Friend WithEvents 更新 As Windows.Forms.Button
    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents 新建 As Windows.Forms.Button
    Friend WithEvents 排序字段 As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents 退出 As Windows.Forms.Button
    Friend WithEvents 查询条件 As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents grdAuthorTitles As Windows.Forms.DataGridView
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents 文件资料 As Windows.Forms.TextBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents 报告来源 As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents 客户 As Windows.Forms.ComboBox
    Friend WithEvents 检测依据 As Windows.Forms.ComboBox
    Friend WithEvents 检验项目 As Windows.Forms.ComboBox
    Friend WithEvents 材质 As Windows.Forms.ComboBox
    Friend WithEvents rtxtSpec As Windows.Forms.RichTextBox
    Friend WithEvents picbDisplayPicture As Windows.Forms.PictureBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents btnResetting As Windows.Forms.Button
End Class
