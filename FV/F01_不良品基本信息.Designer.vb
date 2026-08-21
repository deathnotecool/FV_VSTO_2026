<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F01_不良品基本信息
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F01_不良品基本信息))
        Me.客户 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.管理编号 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPathEqual = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.发生日期 = New System.Windows.Forms.MaskedTextBox()
        Me.图片路径 = New System.Windows.Forms.TextBox()
        Me.因素确定 = New System.Windows.Forms.ComboBox()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.处置完成 = New System.Windows.Forms.CheckBox()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.产品规格 = New System.Windows.Forms.ComboBox()
        Me.类型区分 = New System.Windows.Forms.ComboBox()
        Me.发现过程 = New System.Windows.Forms.ComboBox()
        Me.不良类型 = New System.Windows.Forms.ComboBox()
        Me.供应商 = New System.Windows.Forms.ComboBox()
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.加工费用 = New System.Windows.Forms.TextBox()
        Me.材料费用 = New System.Windows.Forms.TextBox()
        Me.操作者 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.完成工序 = New System.Windows.Forms.TextBox()
        Me.不良现象及原因 = New System.Windows.Forms.TextBox()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.损失成本 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.重量 = New System.Windows.Forms.TextBox()
        Me.不良数量 = New System.Windows.Forms.TextBox()
        Me.加工设备 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.查询条件 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnDisplayingRedData = New System.Windows.Forms.Button()
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
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '客户
        '
        Me.客户.FormattingEnabled = True
        Me.客户.Location = New System.Drawing.Point(422, 34)
        Me.客户.Name = "客户"
        Me.客户.Size = New System.Drawing.Size(110, 20)
        Me.客户.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(536, 79)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 12)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "操作者"
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(392, 198)
        Me.txtRecordPosition.Name = "txtRecordPosition"
        Me.txtRecordPosition.Size = New System.Drawing.Size(100, 21)
        Me.txtRecordPosition.TabIndex = 32
        Me.txtRecordPosition.TabStop = False
        Me.txtRecordPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "管理编号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(375, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "客户"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(701, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "产品规格"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(183, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "发现过程"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(183, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "录入日期"
        Me.ToolTip1.SetToolTip(Me.Label5, "请不要写产品不良发生日期")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(536, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "供应商"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "加工设备"
        '
        '管理编号
        '
        Me.管理编号.Location = New System.Drawing.Point(60, 33)
        Me.管理编号.Name = "管理编号"
        Me.管理编号.Size = New System.Drawing.Size(110, 21)
        Me.管理编号.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1, 163)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "不良类型"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.txtPathEqual)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.发生日期)
        Me.GroupBox1.Controls.Add(Me.图片路径)
        Me.GroupBox1.Controls.Add(Me.因素确定)
        Me.GroupBox1.Controls.Add(Me.btnOpenFile)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.处置完成)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.产品规格)
        Me.GroupBox1.Controls.Add(Me.类型区分)
        Me.GroupBox1.Controls.Add(Me.发现过程)
        Me.GroupBox1.Controls.Add(Me.不良类型)
        Me.GroupBox1.Controls.Add(Me.供应商)
        Me.GroupBox1.Controls.Add(Me.客户)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.btnMoveLast)
        Me.GroupBox1.Controls.Add(Me.btnMoveNext)
        Me.GroupBox1.Controls.Add(Me.btnMoveFirst)
        Me.GroupBox1.Controls.Add(Me.btnMovePrevious)
        Me.GroupBox1.Controls.Add(Me.txtRecordPosition)
        Me.GroupBox1.Controls.Add(Me.加工费用)
        Me.GroupBox1.Controls.Add(Me.材料费用)
        Me.GroupBox1.Controls.Add(Me.操作者)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.完成工序)
        Me.GroupBox1.Controls.Add(Me.不良现象及原因)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.备注)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.损失成本)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.重量)
        Me.GroupBox1.Controls.Add(Me.不良数量)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.加工设备)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.管理编号)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 352)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 225)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "信息输入"
        '
        'txtPathEqual
        '
        Me.txtPathEqual.Location = New System.Drawing.Point(672, 182)
        Me.txtPathEqual.Name = "txtPathEqual"
        Me.txtPathEqual.Size = New System.Drawing.Size(116, 21)
        Me.txtPathEqual.TabIndex = 55
        Me.txtPathEqual.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(613, 204)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(53, 12)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "图片路径"
        '
        '发生日期
        '
        Me.发生日期.Location = New System.Drawing.Point(248, 33)
        Me.发生日期.Name = "发生日期"
        Me.发生日期.Size = New System.Drawing.Size(110, 21)
        Me.发生日期.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.发生日期, "请不要写产品不良发生日期")
        Me.发生日期.ValidatingType = GetType(Date)
        '
        '图片路径
        '
        Me.图片路径.Location = New System.Drawing.Point(672, 197)
        Me.图片路径.Name = "图片路径"
        Me.图片路径.Size = New System.Drawing.Size(116, 21)
        Me.图片路径.TabIndex = 53
        '
        '因素确定
        '
        Me.因素确定.FormattingEnabled = True
        Me.因素确定.Location = New System.Drawing.Point(60, 198)
        Me.因素确定.Name = "因素确定"
        Me.因素确定.Size = New System.Drawing.Size(145, 20)
        Me.因素确定.TabIndex = 17
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(839, 196)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(39, 23)
        Me.btnOpenFile.TabIndex = 52
        Me.btnOpenFile.Text = "打开"
        Me.btnOpenFile.UseVisualStyleBackColor = True
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
        '处置完成
        '
        Me.处置完成.AutoSize = True
        Me.处置完成.Location = New System.Drawing.Point(211, 200)
        Me.处置完成.Name = "处置完成"
        Me.处置完成.Size = New System.Drawing.Size(72, 16)
        Me.处置完成.TabIndex = 18
        Me.处置完成.Text = "处置完成"
        Me.处置完成.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(793, 196)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(39, 23)
        Me.btnImport.TabIndex = 51
        Me.btnImport.Text = "..."
        Me.btnImport.UseVisualStyleBackColor = True
        '
        '产品规格
        '
        Me.产品规格.FormattingEnabled = True
        Me.产品规格.Location = New System.Drawing.Point(758, 34)
        Me.产品规格.Name = "产品规格"
        Me.产品规格.Size = New System.Drawing.Size(120, 20)
        Me.产品规格.TabIndex = 5
        '
        '类型区分
        '
        Me.类型区分.FormattingEnabled = True
        Me.类型区分.Location = New System.Drawing.Point(758, 75)
        Me.类型区分.Name = "类型区分"
        Me.类型区分.Size = New System.Drawing.Size(120, 20)
        Me.类型区分.TabIndex = 6
        '
        '发现过程
        '
        Me.发现过程.FormattingEnabled = True
        Me.发现过程.Location = New System.Drawing.Point(248, 75)
        Me.发现过程.Name = "发现过程"
        Me.发现过程.Size = New System.Drawing.Size(110, 20)
        Me.发现过程.TabIndex = 9
        '
        '不良类型
        '
        Me.不良类型.FormattingEnabled = True
        Me.不良类型.Location = New System.Drawing.Point(60, 158)
        Me.不良类型.Name = "不良类型"
        Me.不良类型.Size = New System.Drawing.Size(145, 20)
        Me.不良类型.TabIndex = 16
        '
        '供应商
        '
        Me.供应商.FormattingEnabled = True
        Me.供应商.Location = New System.Drawing.Point(587, 35)
        Me.供应商.Name = "供应商"
        Me.供应商.Size = New System.Drawing.Size(110, 20)
        Me.供应商.TabIndex = 4
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(553, 198)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 34
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(498, 198)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 33
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(282, 197)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 30
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(337, 198)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 31
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        '加工费用
        '
        Me.加工费用.Enabled = False
        Me.加工费用.Location = New System.Drawing.Point(422, 115)
        Me.加工费用.Name = "加工费用"
        Me.加工费用.Size = New System.Drawing.Size(110, 21)
        Me.加工费用.TabIndex = 13
        '
        '材料费用
        '
        Me.材料费用.Enabled = False
        Me.材料费用.Location = New System.Drawing.Point(587, 114)
        Me.材料费用.Name = "材料费用"
        Me.材料费用.Size = New System.Drawing.Size(110, 21)
        Me.材料费用.TabIndex = 14
        '
        '操作者
        '
        Me.操作者.Location = New System.Drawing.Point(587, 71)
        Me.操作者.Name = "操作者"
        Me.操作者.Size = New System.Drawing.Size(110, 21)
        Me.操作者.TabIndex = 7
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(216, 166)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(53, 12)
        Me.Label18.TabIndex = 20
        Me.Label18.Text = "不良现象"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(375, 78)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 12)
        Me.Label20.TabIndex = 20
        Me.Label20.Text = "重量"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 119)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "不良数量"
        '
        '完成工序
        '
        Me.完成工序.Location = New System.Drawing.Point(248, 114)
        Me.完成工序.Name = "完成工序"
        Me.完成工序.Size = New System.Drawing.Size(110, 21)
        Me.完成工序.TabIndex = 12
        '
        '不良现象及原因
        '
        Me.不良现象及原因.Location = New System.Drawing.Point(275, 150)
        Me.不良现象及原因.Multiline = True
        Me.不良现象及原因.Name = "不良现象及原因"
        Me.不良现象及原因.Size = New System.Drawing.Size(343, 42)
        Me.不良现象及原因.TabIndex = 19
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(672, 150)
        Me.备注.Multiline = True
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(206, 42)
        Me.备注.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(624, 163)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(29, 12)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "备注"
        '
        '损失成本
        '
        Me.损失成本.Enabled = False
        Me.损失成本.Location = New System.Drawing.Point(758, 115)
        Me.损失成本.Name = "损失成本"
        Me.损失成本.Size = New System.Drawing.Size(120, 21)
        Me.损失成本.TabIndex = 15
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(1, 201)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(53, 12)
        Me.Label21.TabIndex = 24
        Me.Label21.Text = "因素确定"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(701, 118)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 12)
        Me.Label17.TabIndex = 24
        Me.Label17.Text = "损失成本"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(701, 79)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "类型区分"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(536, 118)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 12)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "材料单价"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(361, 118)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(53, 12)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "加工费用"
        '
        '重量
        '
        Me.重量.Enabled = False
        Me.重量.Location = New System.Drawing.Point(422, 75)
        Me.重量.Name = "重量"
        Me.重量.Size = New System.Drawing.Size(110, 21)
        Me.重量.TabIndex = 8
        '
        '不良数量
        '
        Me.不良数量.Location = New System.Drawing.Point(60, 115)
        Me.不良数量.Name = "不良数量"
        Me.不良数量.Size = New System.Drawing.Size(110, 21)
        Me.不良数量.TabIndex = 11
        '
        '加工设备
        '
        Me.加工设备.Location = New System.Drawing.Point(60, 75)
        Me.加工设备.Name = "加工设备"
        Me.加工设备.Size = New System.Drawing.Size(110, 21)
        Me.加工设备.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(183, 118)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "完成工序"
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
        Me.grdAuthorTitles.Size = New System.Drawing.Size(878, 317)
        Me.grdAuthorTitles.TabIndex = 36
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 580)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1205, 25)
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
        Me.查询条件.Location = New System.Drawing.Point(73, 99)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(106, 21)
        Me.查询条件.TabIndex = 27
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.查询条件, "清空后将刷新数据显示")
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
        Me.GroupBox2.Location = New System.Drawing.Point(904, 352)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(283, 225)
        Me.GroupBox2.TabIndex = 44
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        'btnDisplayingRedData
        '
        Me.btnDisplayingRedData.Location = New System.Drawing.Point(14, 191)
        Me.btnDisplayingRedData.Name = "btnDisplayingRedData"
        Me.btnDisplayingRedData.Size = New System.Drawing.Size(255, 25)
        Me.btnDisplayingRedData.TabIndex = 31
        Me.btnDisplayingRedData.Text = "未处理记录请点该按钮查看"
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
        Me.退出.Location = New System.Drawing.Point(229, 148)
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
        Me.删除.Location = New System.Drawing.Point(175, 148)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(40, 22)
        Me.删除.TabIndex = 24
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(121, 148)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(40, 22)
        Me.更新.TabIndex = 23
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(67, 148)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(40, 22)
        Me.添加.TabIndex = 21
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(13, 148)
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
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 42)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "排序字段"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox4.Controls.Add(Me.grdAuthorTitles)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(890, 343)
        Me.GroupBox4.TabIndex = 45
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "不良品信息列表"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.PictureBox1)
        Me.GroupBox5.Location = New System.Drawing.Point(904, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(283, 343)
        Me.GroupBox5.TabIndex = 50
        Me.GroupBox5.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 14)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(271, 323)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'F01_不良品基本信息
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1205, 605)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F01_不良品基本信息"
        Me.Text = "F01_不良品基本信息"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents 客户 As Windows.Forms.ComboBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents 管理编号 As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
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
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents 不良现象及原因 As Windows.Forms.TextBox
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents 不良类型 As Windows.Forms.ComboBox
    Friend WithEvents 操作者 As Windows.Forms.TextBox
    Friend WithEvents 加工设备 As Windows.Forms.TextBox
    Friend WithEvents 材料费用 As Windows.Forms.TextBox
    Friend WithEvents 完成工序 As Windows.Forms.TextBox
    Friend WithEvents 损失成本 As Windows.Forms.TextBox
    Friend WithEvents 不良数量 As Windows.Forms.TextBox
    Friend WithEvents 加工费用 As Windows.Forms.TextBox
    Friend WithEvents 类型区分 As Windows.Forms.ComboBox
    Friend WithEvents 产品规格 As Windows.Forms.ComboBox
    Friend WithEvents 发现过程 As Windows.Forms.ComboBox
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents 供应商 As Windows.Forms.ComboBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents 重量 As Windows.Forms.TextBox
    Friend WithEvents 处置完成 As Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As Windows.Forms.CheckBox
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents 因素确定 As Windows.Forms.ComboBox
    Friend WithEvents 发生日期 As Windows.Forms.MaskedTextBox
    Friend WithEvents btnReseting As Windows.Forms.Button
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents btnOpenFile As Windows.Forms.Button
    Friend WithEvents btnImport As Windows.Forms.Button
    Friend WithEvents 图片路径 As Windows.Forms.TextBox
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents btnDisplayingRedData As Windows.Forms.Button
    Friend WithEvents txtPathEqual As Windows.Forms.TextBox
End Class
