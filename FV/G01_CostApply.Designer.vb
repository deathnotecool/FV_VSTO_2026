<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class G01_CostApply
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(G01_CostApply))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.适用单位 = New System.Windows.Forms.ComboBox()
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.项目申请 = New System.Windows.Forms.TextBox()
        Me.总价 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.数量 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.申请日期 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.申请人 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.完成状态 = New System.Windows.Forms.TextBox()
        Me.用途 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
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
        Me.GroupBox1.Controls.Add(Me.适用单位)
        Me.GroupBox1.Controls.Add(Me.btnMoveLast)
        Me.GroupBox1.Controls.Add(Me.btnMoveNext)
        Me.GroupBox1.Controls.Add(Me.btnMoveFirst)
        Me.GroupBox1.Controls.Add(Me.btnMovePrevious)
        Me.GroupBox1.Controls.Add(Me.txtRecordPosition)
        Me.GroupBox1.Controls.Add(Me.项目申请)
        Me.GroupBox1.Controls.Add(Me.总价)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.备注)
        Me.GroupBox1.Controls.Add(Me.数量)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.申请日期)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.申请人)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.完成状态)
        Me.GroupBox1.Controls.Add(Me.用途)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.序列号)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 456)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(667, 160)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "单个证书信息"
        '
        '适用单位
        '
        Me.适用单位.FormattingEnabled = True
        Me.适用单位.Location = New System.Drawing.Point(67, 55)
        Me.适用单位.Name = "适用单位"
        Me.适用单位.Size = New System.Drawing.Size(70, 20)
        Me.适用单位.TabIndex = 5
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(432, 127)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 30
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(377, 127)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 29
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(161, 126)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 28
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(216, 127)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 27
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(271, 127)
        Me.txtRecordPosition.Name = "txtRecordPosition"
        Me.txtRecordPosition.Size = New System.Drawing.Size(100, 21)
        Me.txtRecordPosition.TabIndex = 26
        Me.txtRecordPosition.TabStop = False
        Me.txtRecordPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '项目申请
        '
        Me.项目申请.Location = New System.Drawing.Point(478, 13)
        Me.项目申请.Name = "项目申请"
        Me.项目申请.Size = New System.Drawing.Size(70, 21)
        Me.项目申请.TabIndex = 3
        '
        '总价
        '
        Me.总价.Location = New System.Drawing.Point(208, 55)
        Me.总价.Name = "总价"
        Me.总价.Size = New System.Drawing.Size(70, 21)
        Me.总价.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "序列号"
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(67, 91)
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(600, 21)
        Me.备注.TabIndex = 9
        '
        '数量
        '
        Me.数量.Location = New System.Drawing.Point(595, 13)
        Me.数量.Name = "数量"
        Me.数量.Size = New System.Drawing.Size(70, 21)
        Me.数量.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(287, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "申请人"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(538, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "完成状态"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(288, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "用途"
        '
        '申请日期
        '
        Me.申请日期.Location = New System.Drawing.Point(208, 13)
        Me.申请日期.Name = "申请日期"
        Me.申请日期.Size = New System.Drawing.Size(70, 21)
        Me.申请日期.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "适用单位"
        '
        '申请人
        '
        Me.申请人.Location = New System.Drawing.Point(337, 13)
        Me.申请人.Name = "申请人"
        Me.申请人.Size = New System.Drawing.Size(70, 21)
        Me.申请人.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(146, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "申请日期"
        '
        '完成状态
        '
        Me.完成状态.Location = New System.Drawing.Point(595, 55)
        Me.完成状态.Name = "完成状态"
        Me.完成状态.Size = New System.Drawing.Size(70, 21)
        Me.完成状态.TabIndex = 8
        '
        '用途
        '
        Me.用途.Location = New System.Drawing.Point(337, 55)
        Me.用途.Name = "用途"
        Me.用途.Size = New System.Drawing.Size(195, 21)
        Me.用途.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(29, 94)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "备注"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(416, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "项目申请"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(557, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "数量"
        '
        '序列号
        '
        Me.序列号.Enabled = False
        Me.序列号.Location = New System.Drawing.Point(67, 13)
        Me.序列号.Name = "序列号"
        Me.序列号.Size = New System.Drawing.Size(70, 21)
        Me.序列号.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(146, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "总价"
        '
        'grdAuthorTitles
        '
        Me.grdAuthorTitles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAuthorTitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles.Location = New System.Drawing.Point(8, 20)
        Me.grdAuthorTitles.Name = "grdAuthorTitles"
        Me.grdAuthorTitles.RowTemplate.Height = 23
        Me.grdAuthorTitles.Size = New System.Drawing.Size(1121, 417)
        Me.grdAuthorTitles.TabIndex = 36
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 619)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1160, 25)
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
        Me.查询条件.Location = New System.Drawing.Point(151, 69)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(126, 21)
        Me.查询条件.TabIndex = 15
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.查询条件, "清空后将刷新数据显示")
        '
        'GroupBox2
        '
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
        Me.GroupBox2.Location = New System.Drawing.Point(688, 456)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(454, 160)
        Me.GroupBox2.TabIndex = 44
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(311, 125)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(60, 22)
        Me.退出.TabIndex = 24
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        '执行查询
        '
        Me.执行查询.Location = New System.Drawing.Point(338, 70)
        Me.执行查询.Name = "执行查询"
        Me.执行查询.Size = New System.Drawing.Size(78, 21)
        Me.执行查询.TabIndex = 23
        Me.执行查询.Text = "执行查询"
        Me.执行查询.UseVisualStyleBackColor = True
        '
        '执行排序
        '
        Me.执行排序.Location = New System.Drawing.Point(338, 24)
        Me.执行排序.Name = "执行排序"
        Me.执行排序.Size = New System.Drawing.Size(78, 21)
        Me.执行排序.TabIndex = 22
        Me.执行排序.Text = "执行排序"
        Me.执行排序.UseVisualStyleBackColor = True
        '
        '删除
        '
        Me.删除.Location = New System.Drawing.Point(245, 125)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(60, 22)
        Me.删除.TabIndex = 21
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(179, 125)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(60, 22)
        Me.更新.TabIndex = 11
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(113, 125)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(60, 22)
        Me.添加.TabIndex = 10
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(52, 125)
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
        Me.排序字段.Location = New System.Drawing.Point(151, 25)
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
        Me.GroupBox4.Location = New System.Drawing.Point(15, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1133, 443)
        Me.GroupBox4.TabIndex = 45
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "费用管理台账"
        '
        'G01_CostApply
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1160, 644)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "G01_CostApply"
        Me.Text = "G01_CostApply"
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
    Friend WithEvents 适用单位 As Windows.Forms.ComboBox
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents 项目申请 As Windows.Forms.TextBox
    Friend WithEvents 总价 As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents 数量 As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents 申请日期 As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents 申请人 As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents 用途 As Windows.Forms.TextBox
    Friend WithEvents Label12 As Windows.Forms.Label
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
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents 完成状态 As Windows.Forms.TextBox
End Class
