<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class B03_保养设备信息
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(B03_保养设备信息))
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.grdAuthorTitles1th = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtRecordPosition = New System.Windows.Forms.TextBox()
        Me.替换件编号 = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.保养级别 = New System.Windows.Forms.ComboBox()
        Me.设备编号 = New System.Windows.Forms.ComboBox()
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveNext = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMovePrevious = New System.Windows.Forms.Button()
        Me.保养内容 = New System.Windows.Forms.TextBox()
        Me.保养时间 = New System.Windows.Forms.TextBox()
        Me.保养单号1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.工时 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.保养费用 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.保养单号 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.退出 = New System.Windows.Forms.Button()
        Me.执行查询 = New System.Windows.Forms.Button()
        Me.执行排序 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.查询条件 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.删除 = New System.Windows.Forms.Button()
        Me.更新 = New System.Windows.Forms.Button()
        Me.添加 = New System.Windows.Forms.Button()
        Me.新建 = New System.Windows.Forms.Button()
        Me.排序字段 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAuthorTitles1th, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.grdAuthorTitles)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 351)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(770, 190)
        Me.GroupBox4.TabIndex = 39
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "保养汇总记录"
        '
        'grdAuthorTitles
        '
        Me.grdAuthorTitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles.Location = New System.Drawing.Point(2, 15)
        Me.grdAuthorTitles.Name = "grdAuthorTitles"
        Me.grdAuthorTitles.RowTemplate.Height = 23
        Me.grdAuthorTitles.Size = New System.Drawing.Size(762, 169)
        Me.grdAuthorTitles.TabIndex = 29
        '
        'grdAuthorTitles1th
        '
        Me.grdAuthorTitles1th.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles1th.Location = New System.Drawing.Point(2, 11)
        Me.grdAuthorTitles1th.Name = "grdAuthorTitles1th"
        Me.grdAuthorTitles1th.RowTemplate.Height = 23
        Me.grdAuthorTitles1th.Size = New System.Drawing.Size(762, 124)
        Me.grdAuthorTitles1th.TabIndex = 33
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.grdAuthorTitles1th)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 210)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(770, 138)
        Me.GroupBox3.TabIndex = 38
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "保养履历卡"
        '
        'txtRecordPosition
        '
        Me.txtRecordPosition.Location = New System.Drawing.Point(166, 167)
        Me.txtRecordPosition.Name = "txtRecordPosition"
        Me.txtRecordPosition.Size = New System.Drawing.Size(132, 21)
        Me.txtRecordPosition.TabIndex = 26
        Me.txtRecordPosition.TabStop = False
        Me.txtRecordPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '替换件编号
        '
        Me.替换件编号.Location = New System.Drawing.Point(231, 21)
        Me.替换件编号.Name = "替换件编号"
        Me.替换件编号.Size = New System.Drawing.Size(82, 21)
        Me.替换件编号.TabIndex = 2
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 542)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(780, 25)
        Me.ToolStrip1.TabIndex = 36
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.保养级别)
        Me.GroupBox1.Controls.Add(Me.设备编号)
        Me.GroupBox1.Controls.Add(Me.btnMoveLast)
        Me.GroupBox1.Controls.Add(Me.btnMoveNext)
        Me.GroupBox1.Controls.Add(Me.btnMoveFirst)
        Me.GroupBox1.Controls.Add(Me.btnMovePrevious)
        Me.GroupBox1.Controls.Add(Me.txtRecordPosition)
        Me.GroupBox1.Controls.Add(Me.替换件编号)
        Me.GroupBox1.Controls.Add(Me.保养内容)
        Me.GroupBox1.Controls.Add(Me.保养时间)
        Me.GroupBox1.Controls.Add(Me.保养单号1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.工时)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.保养费用)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.保养单号)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(472, 202)
        Me.GroupBox1.TabIndex = 35
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "保养信息"
        '
        '保养级别
        '
        Me.保养级别.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.保养级别.FormattingEnabled = True
        Me.保养级别.Location = New System.Drawing.Point(384, 57)
        Me.保养级别.Name = "保养级别"
        Me.保养级别.Size = New System.Drawing.Size(82, 20)
        Me.保养级别.TabIndex = 31
        '
        '设备编号
        '
        Me.设备编号.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.设备编号.FormattingEnabled = True
        Me.设备编号.Location = New System.Drawing.Point(70, 59)
        Me.设备编号.Name = "设备编号"
        Me.设备编号.Size = New System.Drawing.Size(82, 20)
        Me.设备编号.TabIndex = 4
        '
        'btnMoveLast
        '
        Me.btnMoveLast.Location = New System.Drawing.Point(364, 166)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveLast.TabIndex = 30
        Me.btnMoveLast.Text = ">|"
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = True
        '
        'btnMoveNext
        '
        Me.btnMoveNext.Location = New System.Drawing.Point(309, 166)
        Me.btnMoveNext.Name = "btnMoveNext"
        Me.btnMoveNext.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveNext.TabIndex = 29
        Me.btnMoveNext.Text = ">"
        Me.ToolTip1.SetToolTip(Me.btnMoveNext, "Move Next")
        Me.btnMoveNext.UseVisualStyleBackColor = True
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.Location = New System.Drawing.Point(48, 166)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(49, 21)
        Me.btnMoveFirst.TabIndex = 28
        Me.btnMoveFirst.Text = "|<"
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Move First")
        Me.btnMoveFirst.UseVisualStyleBackColor = True
        '
        'btnMovePrevious
        '
        Me.btnMovePrevious.Location = New System.Drawing.Point(103, 167)
        Me.btnMovePrevious.Name = "btnMovePrevious"
        Me.btnMovePrevious.Size = New System.Drawing.Size(49, 21)
        Me.btnMovePrevious.TabIndex = 27
        Me.btnMovePrevious.Text = "<"
        Me.ToolTip1.SetToolTip(Me.btnMovePrevious, "Move Previous")
        Me.btnMovePrevious.UseVisualStyleBackColor = True
        '
        '保养内容
        '
        Me.保养内容.Location = New System.Drawing.Point(231, 97)
        Me.保养内容.Name = "保养内容"
        Me.保养内容.Size = New System.Drawing.Size(235, 21)
        Me.保养内容.TabIndex = 8
        '
        '保养时间
        '
        Me.保养时间.Location = New System.Drawing.Point(384, 20)
        Me.保养时间.Name = "保养时间"
        Me.保养时间.Size = New System.Drawing.Size(82, 21)
        Me.保养时间.TabIndex = 3
        '
        '保养单号1
        '
        Me.保养单号1.AutoSize = True
        Me.保养单号1.Location = New System.Drawing.Point(11, 24)
        Me.保养单号1.Name = "保养单号1"
        Me.保养单号1.Size = New System.Drawing.Size(53, 12)
        Me.保养单号1.TabIndex = 17
        Me.保养单号1.Text = "保养单号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "设备编号"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "保养费用"
        '
        '工时
        '
        Me.工时.Location = New System.Drawing.Point(231, 56)
        Me.工时.Name = "工时"
        Me.工时.Size = New System.Drawing.Size(82, 21)
        Me.工时.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(166, 59)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(53, 12)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "保养工时"
        '
        '保养费用
        '
        Me.保养费用.Location = New System.Drawing.Point(70, 97)
        Me.保养费用.Name = "保养费用"
        Me.保养费用.Size = New System.Drawing.Size(82, 21)
        Me.保养费用.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(319, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "保养级别"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(319, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "保养时间"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(166, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 12)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "替换件编号"
        '
        '保养单号
        '
        Me.保养单号.Enabled = False
        Me.保养单号.Location = New System.Drawing.Point(70, 20)
        Me.保养单号.Name = "保养单号"
        Me.保养单号.Size = New System.Drawing.Size(82, 21)
        Me.保养单号.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(166, 101)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "保养内容"
        '
        '退出
        '
        Me.退出.Location = New System.Drawing.Point(203, 150)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(75, 22)
        Me.退出.TabIndex = 24
        Me.退出.Text = "退出"
        Me.退出.UseVisualStyleBackColor = True
        '
        '执行查询
        '
        Me.执行查询.Location = New System.Drawing.Point(203, 73)
        Me.执行查询.Name = "执行查询"
        Me.执行查询.Size = New System.Drawing.Size(75, 21)
        Me.执行查询.TabIndex = 23
        Me.执行查询.Text = "执行查询"
        Me.执行查询.UseVisualStyleBackColor = True
        '
        '执行排序
        '
        Me.执行排序.Location = New System.Drawing.Point(203, 21)
        Me.执行排序.Name = "执行排序"
        Me.执行排序.Size = New System.Drawing.Size(75, 21)
        Me.执行排序.TabIndex = 22
        Me.执行排序.Text = "执行排序"
        Me.执行排序.UseVisualStyleBackColor = True
        '
        '查询条件
        '
        Me.查询条件.Location = New System.Drawing.Point(66, 73)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(131, 21)
        Me.查询条件.TabIndex = 15
        Me.查询条件.TabStop = False
        Me.查询条件.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.GroupBox2.Location = New System.Drawing.Point(481, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(292, 202)
        Me.GroupBox2.TabIndex = 37
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        '删除
        '
        Me.删除.Location = New System.Drawing.Point(107, 166)
        Me.删除.Name = "删除"
        Me.删除.Size = New System.Drawing.Size(60, 22)
        Me.删除.TabIndex = 21
        Me.删除.Text = "删除"
        Me.删除.UseVisualStyleBackColor = True
        '
        '更新
        '
        Me.更新.Location = New System.Drawing.Point(41, 166)
        Me.更新.Name = "更新"
        Me.更新.Size = New System.Drawing.Size(60, 22)
        Me.更新.TabIndex = 20
        Me.更新.Text = "更新"
        Me.更新.UseVisualStyleBackColor = True
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(107, 132)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(60, 22)
        Me.添加.TabIndex = 19
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(41, 133)
        Me.新建.Name = "新建"
        Me.新建.Size = New System.Drawing.Size(60, 22)
        Me.新建.TabIndex = 18
        Me.新建.Text = "新建"
        Me.新建.UseVisualStyleBackColor = True
        '
        '排序字段
        '
        Me.排序字段.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.排序字段.FormattingEnabled = True
        Me.排序字段.Location = New System.Drawing.Point(66, 23)
        Me.排序字段.Name = "排序字段"
        Me.排序字段.Size = New System.Drawing.Size(131, 20)
        Me.排序字段.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 77)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "查询条件"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "排序字段"
        '
        'B03_保养设备信息
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(780, 567)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "B03_保养设备信息"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "保养设备信息"
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAuthorTitles1th, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents grdAuthorTitles As Windows.Forms.DataGridView
    Friend WithEvents grdAuthorTitles1th As Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents txtRecordPosition As Windows.Forms.TextBox
    Friend WithEvents 替换件编号 As Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnMoveLast As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents btnMoveNext As Windows.Forms.Button
    Friend WithEvents btnMoveFirst As Windows.Forms.Button
    Friend WithEvents btnMovePrevious As Windows.Forms.Button
    Friend WithEvents 保养内容 As Windows.Forms.TextBox
    Friend WithEvents 保养时间 As Windows.Forms.TextBox
    Friend WithEvents 保养单号1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents 工时 As Windows.Forms.TextBox
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents 保养费用 As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents 保养单号 As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents 退出 As Windows.Forms.Button
    Friend WithEvents 执行查询 As Windows.Forms.Button
    Friend WithEvents 执行排序 As Windows.Forms.Button
    Friend WithEvents 查询条件 As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents 删除 As Windows.Forms.Button
    Friend WithEvents 更新 As Windows.Forms.Button
    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents 新建 As Windows.Forms.Button
    Friend WithEvents 排序字段 As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents 设备编号 As Windows.Forms.ComboBox
    Friend WithEvents 保养级别 As Windows.Forms.ComboBox
End Class
