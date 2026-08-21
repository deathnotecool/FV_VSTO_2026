<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F04_供应商索赔信息
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F04_供应商索赔信息))
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
        Me.btnReseting = New System.Windows.Forms.Button()
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.统计始日 = New System.Windows.Forms.MaskedTextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.赔偿表证明 = New System.Windows.Forms.TextBox()
        Me.赔偿表证明1 = New System.Windows.Forms.Label()
        Me.统计止日 = New System.Windows.Forms.MaskedTextBox()
        Me.补货状态 = New System.Windows.Forms.CheckBox()
        Me.供应商确认 = New System.Windows.Forms.CheckBox()
        Me.毛坯抵扣 = New System.Windows.Forms.CheckBox()
        Me.图号 = New System.Windows.Forms.ComboBox()
        Me.区分 = New System.Windows.Forms.ComboBox()
        Me.供应商 = New System.Windows.Forms.ComboBox()
        Me.lab2 = New System.Windows.Forms.Label()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.毛坯费扣 = New System.Windows.Forms.TextBox()
        Me.赔偿单号 = New System.Windows.Forms.TextBox()
        Me.加工费扣 = New System.Windows.Forms.TextBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.数量 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ID = New System.Windows.Forms.TextBox()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        '执行查询
        '
        Me.执行查询.Location = New System.Drawing.Point(215, 100)
        Me.执行查询.Name = "执行查询"
        Me.执行查询.Size = New System.Drawing.Size(38, 21)
        Me.执行查询.TabIndex = 29
        Me.执行查询.Text = "查询"
        Me.执行查询.UseVisualStyleBackColor = True
        '
        '执行排序
        '
        Me.执行排序.Location = New System.Drawing.Point(215, 33)
        Me.执行排序.Name = "执行排序"
        Me.执行排序.Size = New System.Drawing.Size(80, 21)
        Me.执行排序.TabIndex = 28
        Me.执行排序.Text = "执行排序"
        Me.执行排序.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Enabled = False
        Me.删除.Location = New System.Drawing.Point(183, 163)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(40, 22)
        Me.删除.TabIndex = 24
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(129, 163)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(40, 22)
        Me.更新.TabIndex = 23
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(75, 163)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(40, 22)
        Me.添加.TabIndex = 21
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(21, 163)
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
        Me.排序字段.Location = New System.Drawing.Point(82, 35)
        Me.排序字段.Name = "排序字段"
        Me.排序字段.Size = New System.Drawing.Size(126, 20)
        Me.排序字段.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 104)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "查询条件"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 633)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1227, 25)
        Me.ToolStrip1.TabIndex = 47
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
        Me.退出.Location = New System.Drawing.Point(237, 163)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(40, 22)
        Me.退出.TabIndex = 25
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        '查询条件
        '
        Me.查询条件.Location = New System.Drawing.Point(80, 100)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(126, 21)
        Me.查询条件.TabIndex = 15
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.查询条件, "清空后将刷新数据显示")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(19, 38)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "排序字段"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
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
        Me.GroupBox2.Location = New System.Drawing.Point(904, 389)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(311, 225)
        Me.GroupBox2.TabIndex = 48
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        'btnReseting
        '
        Me.btnReseting.Location = New System.Drawing.Point(257, 100)
        Me.btnReseting.Name = "btnReseting"
        Me.btnReseting.Size = New System.Drawing.Size(38, 21)
        Me.btnReseting.TabIndex = 31
        Me.btnReseting.Text = "重设"
        Me.btnReseting.UseVisualStyleBackColor = True
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
        Me.grdAuthorTitles.Size = New System.Drawing.Size(1195, 355)
        Me.grdAuthorTitles.TabIndex = 36
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(388, 189)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 34
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(333, 189)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 33
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(117, 188)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 30
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(172, 189)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 31
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.grdAuthorTitles)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1207, 381)
        Me.GroupBox4.TabIndex = 49
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "索赔信息列表"
        '
        '统计始日
        '
        Me.统计始日.Location = New System.Drawing.Point(219, 33)
        Me.统计始日.Name = "统计始日"
        Me.统计始日.Size = New System.Drawing.Size(110, 21)
        Me.统计始日.TabIndex = 2
        Me.统计始日.ValidatingType = GetType(Date)
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
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.btnOpenFile)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.赔偿表证明)
        Me.GroupBox1.Controls.Add(Me.赔偿表证明1)
        Me.GroupBox1.Controls.Add(Me.统计止日)
        Me.GroupBox1.Controls.Add(Me.统计始日)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.补货状态)
        Me.GroupBox1.Controls.Add(Me.供应商确认)
        Me.GroupBox1.Controls.Add(Me.毛坯抵扣)
        Me.GroupBox1.Controls.Add(Me.图号)
        Me.GroupBox1.Controls.Add(Me.区分)
        Me.GroupBox1.Controls.Add(Me.供应商)
        Me.GroupBox1.Controls.Add(Me.lab2)
        Me.GroupBox1.Controls.Add(Me.btnMoveLast)
        Me.GroupBox1.Controls.Add(Me.btnMoveNext)
        Me.GroupBox1.Controls.Add(Me.btnMoveFirst)
        Me.GroupBox1.Controls.Add(Me.btnMovePrevious)
        Me.GroupBox1.Controls.Add(Me.txtRecordPosition)
        Me.GroupBox1.Controls.Add(Me.毛坯费扣)
        Me.GroupBox1.Controls.Add(Me.赔偿单号)
        Me.GroupBox1.Controls.Add(Me.加工费扣)
        Me.GroupBox1.Controls.Add(Me.lbl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.备注)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.数量)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.ID)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 389)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 225)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "信息输入"
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(777, 113)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(61, 23)
        Me.btnOpenFile.TabIndex = 16
        Me.btnOpenFile.Text = "打开"
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(711, 113)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(61, 23)
        Me.btnImport.TabIndex = 15
        Me.btnImport.Text = "导入"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        '赔偿表证明
        '
        Me.赔偿表证明.Location = New System.Drawing.Point(591, 114)
        Me.赔偿表证明.Name = "赔偿表证明"
        Me.赔偿表证明.Size = New System.Drawing.Size(110, 21)
        Me.赔偿表证明.TabIndex = 38
        '
        '赔偿表证明1
        '
        Me.赔偿表证明1.AutoSize = True
        Me.赔偿表证明1.Location = New System.Drawing.Point(517, 118)
        Me.赔偿表证明1.Name = "赔偿表证明1"
        Me.赔偿表证明1.Size = New System.Drawing.Size(65, 12)
        Me.赔偿表证明1.TabIndex = 14
        Me.赔偿表证明1.Text = "赔偿表证明"
        '
        '统计止日
        '
        Me.统计止日.Location = New System.Drawing.Point(404, 35)
        Me.统计止日.Name = "统计止日"
        Me.统计止日.Size = New System.Drawing.Size(110, 21)
        Me.统计止日.TabIndex = 3
        Me.统计止日.ValidatingType = GetType(Date)
        '
        '补货状态
        '
        Me.补货状态.AutoSize = True
        Me.补货状态.Location = New System.Drawing.Point(404, 117)
        Me.补货状态.Name = "补货状态"
        Me.补货状态.Size = New System.Drawing.Size(72, 16)
        Me.补货状态.TabIndex = 13
        Me.补货状态.Text = "补货状态"
        Me.补货状态.UseVisualStyleBackColor = True
        '
        '供应商确认
        '
        Me.供应商确认.AutoSize = True
        Me.供应商确认.Location = New System.Drawing.Point(219, 117)
        Me.供应商确认.Name = "供应商确认"
        Me.供应商确认.Size = New System.Drawing.Size(84, 16)
        Me.供应商确认.TabIndex = 12
        Me.供应商确认.Text = "供应商确认"
        Me.供应商确认.UseVisualStyleBackColor = True
        '
        '毛坯抵扣
        '
        Me.毛坯抵扣.AutoSize = True
        Me.毛坯抵扣.Location = New System.Drawing.Point(33, 117)
        Me.毛坯抵扣.Name = "毛坯抵扣"
        Me.毛坯抵扣.Size = New System.Drawing.Size(72, 16)
        Me.毛坯抵扣.TabIndex = 11
        Me.毛坯抵扣.Text = "毛坯抵扣"
        Me.毛坯抵扣.UseVisualStyleBackColor = True
        '
        '图号
        '
        Me.图号.FormattingEnabled = True
        Me.图号.Location = New System.Drawing.Point(33, 75)
        Me.图号.Name = "图号"
        Me.图号.Size = New System.Drawing.Size(110, 20)
        Me.图号.TabIndex = 6
        '
        '区分
        '
        Me.区分.FormattingEnabled = True
        Me.区分.Location = New System.Drawing.Point(219, 76)
        Me.区分.Name = "区分"
        Me.区分.Size = New System.Drawing.Size(110, 20)
        Me.区分.TabIndex = 7
        '
        '供应商
        '
        Me.供应商.FormattingEnabled = True
        Me.供应商.Location = New System.Drawing.Point(762, 34)
        Me.供应商.Name = "供应商"
        Me.供应商.Size = New System.Drawing.Size(110, 20)
        Me.供应商.TabIndex = 5
        '
        'lab2
        '
        Me.lab2.AutoSize = True
        Me.lab2.Location = New System.Drawing.Point(520, 80)
        Me.lab2.Name = "lab2"
        Me.lab2.Size = New System.Drawing.Size(53, 12)
        Me.lab2.TabIndex = 9
        Me.lab2.Text = "加工费扣"
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(227, 189)
        Me.txtRecordPosition.Name = "txtRecordPosition"
        Me.txtRecordPosition.Size = New System.Drawing.Size(100, 21)
        Me.txtRecordPosition.TabIndex = 32
        Me.txtRecordPosition.TabStop = False
        Me.txtRecordPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '毛坯费扣
        '
        Me.毛坯费扣.Location = New System.Drawing.Point(762, 77)
        Me.毛坯费扣.Name = "毛坯费扣"
        Me.毛坯费扣.Size = New System.Drawing.Size(110, 21)
        Me.毛坯费扣.TabIndex = 10
        '
        '赔偿单号
        '
        Me.赔偿单号.Location = New System.Drawing.Point(589, 33)
        Me.赔偿单号.Name = "赔偿单号"
        Me.赔偿单号.Size = New System.Drawing.Size(110, 21)
        Me.赔偿单号.TabIndex = 4
        '
        '加工费扣
        '
        Me.加工费扣.Location = New System.Drawing.Point(591, 77)
        Me.加工费扣.Name = "加工费扣"
        Me.加工费扣.Size = New System.Drawing.Size(110, 21)
        Me.加工费扣.TabIndex = 9
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        Me.lbl1.Location = New System.Drawing.Point(6, 37)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(17, 12)
        Me.lbl1.TabIndex = 1
        Me.lbl1.Text = "ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(340, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "统计止日"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(710, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "供应商"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(354, 79)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 12)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "数量"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(171, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "区分"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(155, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "统计始日"
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(529, 163)
        Me.备注.Multiline = True
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(343, 52)
        Me.备注.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(469, 186)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(29, 12)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "备注"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(707, 80)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "毛坯费扣"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(525, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "赔偿单号"
        '
        '数量
        '
        Me.数量.Location = New System.Drawing.Point(404, 75)
        Me.数量.Name = "数量"
        Me.数量.Size = New System.Drawing.Size(110, 21)
        Me.数量.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "图号"
        '
        'ID
        '
        Me.ID.Enabled = False
        Me.ID.Location = New System.Drawing.Point(34, 34)
        Me.ID.Name = "ID"
        Me.ID.Size = New System.Drawing.Size(110, 21)
        Me.ID.TabIndex = 1
        '
        'F04_供应商索赔信息
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1227, 658)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F04_供应商索赔信息"
        Me.Text = "F04_供应商索赔信息"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

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
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents grdAuthorTitles As Windows.Forms.DataGridView
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents 统计始日 As Windows.Forms.MaskedTextBox
    Friend WithEvents CheckBox1 As Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents 统计止日 As Windows.Forms.MaskedTextBox
    Friend WithEvents 毛坯抵扣 As Windows.Forms.CheckBox
    Friend WithEvents 图号 As Windows.Forms.ComboBox
    Friend WithEvents 区分 As Windows.Forms.ComboBox
    Friend WithEvents 供应商 As Windows.Forms.ComboBox
    Friend WithEvents lab2 As Windows.Forms.Label
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents 毛坯费扣 As Windows.Forms.TextBox
    Friend WithEvents 赔偿单号 As Windows.Forms.TextBox
    Friend WithEvents 加工费扣 As Windows.Forms.TextBox
    Friend WithEvents lbl1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents 数量 As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents ID As Windows.Forms.TextBox
    Friend WithEvents 赔偿表证明1 As Windows.Forms.Label
    Friend WithEvents 补货状态 As Windows.Forms.CheckBox
    Friend WithEvents 供应商确认 As Windows.Forms.CheckBox
    Friend WithEvents 赔偿表证明 As Windows.Forms.TextBox
    Friend WithEvents btnOpenFile As Windows.Forms.Button
    Friend WithEvents btnImport As Windows.Forms.Button
    Friend WithEvents btnReseting As Windows.Forms.Button
End Class
