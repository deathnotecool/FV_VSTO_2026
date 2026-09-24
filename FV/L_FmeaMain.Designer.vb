<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class L_FmeaMain
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
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblProcessNo = New System.Windows.Forms.Label()
        Me.txtProcessNo = New System.Windows.Forms.TextBox()
        Me.lblSection = New System.Windows.Forms.Label()
        Me.lblProcessOrder = New System.Windows.Forms.Label()
        Me.txtProcessOrder = New System.Windows.Forms.TextBox()
        Me.lblDeptName = New System.Windows.Forms.Label()
        Me.txtDeptName = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblFunction = New System.Windows.Forms.Label()
        Me.txtFunction = New System.Windows.Forms.TextBox()
        Me.lblProcessName = New System.Windows.Forms.Label()
        Me.txtProcessName = New System.Windows.Forms.TextBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.lblOwner = New System.Windows.Forms.Label()
        Me.txtOwner = New System.Windows.Forms.TextBox()
        Me.lblFmeaTeam = New System.Windows.Forms.Label()
        Me.txtFmeaTeam = New System.Windows.Forms.TextBox()
        Me.lblCreateTime = New System.Windows.Forms.Label()
        Me.lblFunctionReq = New System.Windows.Forms.Label()
        Me.txtFunctionReq = New System.Windows.Forms.TextBox()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.lblStatusBar = New System.Windows.Forms.Label()
        Me.lblUpdateTime = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboProcessType = New System.Windows.Forms.ComboBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.dtpUpdateTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpCreateTime = New System.Windows.Forms.DateTimePicker()
        Me.lblProcessType = New System.Windows.Forms.Label()
        Me.cboSection = New System.Windows.Forms.ComboBox()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(57, 12)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "新增"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(165, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(264, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "删除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(367, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "刷新"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(461, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblProcessNo
        '
        Me.lblProcessNo.AutoSize = True
        Me.lblProcessNo.Location = New System.Drawing.Point(54, 98)
        Me.lblProcessNo.Name = "lblProcessNo"
        Me.lblProcessNo.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessNo.TabIndex = 6
        Me.lblProcessNo.Text = "工序编号:"
        '
        'txtProcessNo
        '
        Me.txtProcessNo.Location = New System.Drawing.Point(140, 95)
        Me.txtProcessNo.Name = "txtProcessNo"
        Me.txtProcessNo.Size = New System.Drawing.Size(100, 25)
        Me.txtProcessNo.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtProcessNo, "录 CP 编号，如 O-01")
        '
        'lblSection
        '
        Me.lblSection.AutoSize = True
        Me.lblSection.Location = New System.Drawing.Point(54, 182)
        Me.lblSection.Name = "lblSection"
        Me.lblSection.Size = New System.Drawing.Size(60, 15)
        Me.lblSection.TabIndex = 6
        Me.lblSection.Text = "工序段:"
        '
        'lblProcessOrder
        '
        Me.lblProcessOrder.AutoSize = True
        Me.lblProcessOrder.Location = New System.Drawing.Point(54, 244)
        Me.lblProcessOrder.Name = "lblProcessOrder"
        Me.lblProcessOrder.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessOrder.TabIndex = 6
        Me.lblProcessOrder.Text = "工序顺序:"
        '
        'txtProcessOrder
        '
        Me.txtProcessOrder.Location = New System.Drawing.Point(140, 234)
        Me.txtProcessOrder.Name = "txtProcessOrder"
        Me.txtProcessOrder.Size = New System.Drawing.Size(100, 25)
        Me.txtProcessOrder.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtProcessOrder, "录排序号，如 10、20、30")
        '
        'lblDeptName
        '
        Me.lblDeptName.AutoSize = True
        Me.lblDeptName.Location = New System.Drawing.Point(54, 321)
        Me.lblDeptName.Name = "lblDeptName"
        Me.lblDeptName.Size = New System.Drawing.Size(75, 15)
        Me.lblDeptName.TabIndex = 6
        Me.lblDeptName.Text = "责任部门:"
        '
        'txtDeptName
        '
        Me.txtDeptName.Location = New System.Drawing.Point(140, 318)
        Me.txtDeptName.Name = "txtDeptName"
        Me.txtDeptName.Size = New System.Drawing.Size(100, 25)
        Me.txtDeptName.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtDeptName, "录部门名")
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(54, 374)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(45, 15)
        Me.lblStatus.TabIndex = 6
        Me.lblStatus.Text = "状态:"
        '
        'lblFunction
        '
        Me.lblFunction.AutoSize = True
        Me.lblFunction.Location = New System.Drawing.Point(54, 458)
        Me.lblFunction.Name = "lblFunction"
        Me.lblFunction.Size = New System.Drawing.Size(75, 15)
        Me.lblFunction.TabIndex = 6
        Me.lblFunction.Text = "工序功能:"
        '
        'txtFunction
        '
        Me.txtFunction.Location = New System.Drawing.Point(140, 455)
        Me.txtFunction.Multiline = True
        Me.txtFunction.Name = "txtFunction"
        Me.txtFunction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFunction.Size = New System.Drawing.Size(449, 49)
        Me.txtFunction.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtFunction, "工序功能描述，可多行")
        '
        'lblProcessName
        '
        Me.lblProcessName.AutoSize = True
        Me.lblProcessName.Location = New System.Drawing.Point(396, 91)
        Me.lblProcessName.Name = "lblProcessName"
        Me.lblProcessName.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessName.TabIndex = 6
        Me.lblProcessName.Text = "工序名称:"
        '
        'txtProcessName
        '
        Me.txtProcessName.Location = New System.Drawing.Point(482, 88)
        Me.txtProcessName.Name = "txtProcessName"
        Me.txtProcessName.Size = New System.Drawing.Size(100, 25)
        Me.txtProcessName.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtProcessName, "录工序名，如 入库检")
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(396, 230)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(60, 15)
        Me.lblVersion.TabIndex = 6
        Me.lblVersion.Text = "版本号:"
        '
        'txtVersion
        '
        Me.txtVersion.Location = New System.Drawing.Point(482, 227)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(100, 25)
        Me.txtVersion.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtVersion, "人工填，如 V1.0")
        '
        'lblOwner
        '
        Me.lblOwner.AutoSize = True
        Me.lblOwner.Location = New System.Drawing.Point(396, 314)
        Me.lblOwner.Name = "lblOwner"
        Me.lblOwner.Size = New System.Drawing.Size(60, 15)
        Me.lblOwner.TabIndex = 6
        Me.lblOwner.Text = "负责人:"
        '
        'txtOwner
        '
        Me.txtOwner.Location = New System.Drawing.Point(482, 311)
        Me.txtOwner.Name = "txtOwner"
        Me.txtOwner.Size = New System.Drawing.Size(100, 25)
        Me.txtOwner.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtOwner, "录负责人姓名")
        '
        'lblFmeaTeam
        '
        Me.lblFmeaTeam.AutoSize = True
        Me.lblFmeaTeam.Location = New System.Drawing.Point(396, 377)
        Me.lblFmeaTeam.Name = "lblFmeaTeam"
        Me.lblFmeaTeam.Size = New System.Drawing.Size(77, 15)
        Me.lblFmeaTeam.TabIndex = 6
        Me.lblFmeaTeam.Text = "FMEA小组:"
        '
        'txtFmeaTeam
        '
        Me.txtFmeaTeam.Location = New System.Drawing.Point(482, 374)
        Me.txtFmeaTeam.Name = "txtFmeaTeam"
        Me.txtFmeaTeam.Size = New System.Drawing.Size(100, 25)
        Me.txtFmeaTeam.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtFmeaTeam, "多人用 ; 分隔，如 张三;李四;王五")
        '
        'lblCreateTime
        '
        Me.lblCreateTime.AutoSize = True
        Me.lblCreateTime.Location = New System.Drawing.Point(35, 712)
        Me.lblCreateTime.Name = "lblCreateTime"
        Me.lblCreateTime.Size = New System.Drawing.Size(75, 15)
        Me.lblCreateTime.TabIndex = 6
        Me.lblCreateTime.Text = "创建时间:"
        '
        'lblFunctionReq
        '
        Me.lblFunctionReq.AutoSize = True
        Me.lblFunctionReq.Location = New System.Drawing.Point(45, 542)
        Me.lblFunctionReq.Name = "lblFunctionReq"
        Me.lblFunctionReq.Size = New System.Drawing.Size(75, 15)
        Me.lblFunctionReq.TabIndex = 6
        Me.lblFunctionReq.Text = "功能要求:"
        '
        'txtFunctionReq
        '
        Me.txtFunctionReq.Location = New System.Drawing.Point(131, 539)
        Me.txtFunctionReq.Multiline = True
        Me.txtFunctionReq.Name = "txtFunctionReq"
        Me.txtFunctionReq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFunctionReq.Size = New System.Drawing.Size(458, 60)
        Me.txtFunctionReq.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtFunctionReq, "功能要求/技术条件，可多行")
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(45, 626)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(45, 15)
        Me.lblRemark.TabIndex = 6
        Me.lblRemark.Text = "备注:"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(131, 619)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(451, 56)
        Me.txtRemark.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtRemark, "备注，可多行")
        '
        'lblStatusBar
        '
        Me.lblStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusBar.Location = New System.Drawing.Point(45, 752)
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(650, 28)
        Me.lblStatusBar.TabIndex = 6
        Me.lblStatusBar.Text = "                         "
        Me.ToolTip1.SetToolTip(Me.lblStatusBar, "显示当前记录/总记录/操作提示")
        '
        'lblUpdateTime
        '
        Me.lblUpdateTime.AutoSize = True
        Me.lblUpdateTime.Location = New System.Drawing.Point(418, 715)
        Me.lblUpdateTime.Name = "lblUpdateTime"
        Me.lblUpdateTime.Size = New System.Drawing.Size(75, 15)
        Me.lblUpdateTime.TabIndex = 6
        Me.lblUpdateTime.Text = "更新时间:"
        '
        'cboProcessType
        '
        Me.cboProcessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProcessType.FormattingEnabled = True
        Me.cboProcessType.Location = New System.Drawing.Point(495, 174)
        Me.cboProcessType.Name = "cboProcessType"
        Me.cboProcessType.Size = New System.Drawing.Size(121, 23)
        Me.cboProcessType.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cboProcessType, "选 8 类之一")
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(119, 374)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(121, 23)
        Me.cboStatus.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cboStatus, "请选择状态：草稿/评审中/修订中/已发布/已作废")
        '
        'dtpUpdateTime
        '
        Me.dtpUpdateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        Me.dtpUpdateTime.Enabled = False
        Me.dtpUpdateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUpdateTime.Location = New System.Drawing.Point(495, 705)
        Me.dtpUpdateTime.Name = "dtpUpdateTime"
        Me.dtpUpdateTime.Size = New System.Drawing.Size(200, 25)
        Me.dtpUpdateTime.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.dtpUpdateTime, "系统自动生成，不可修改;记录这条工序最后一次修改的时间")
        '
        'dtpCreateTime
        '
        Me.dtpCreateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        Me.dtpCreateTime.Enabled = False
        Me.dtpCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCreateTime.Location = New System.Drawing.Point(110, 705)
        Me.dtpCreateTime.Name = "dtpCreateTime"
        Me.dtpCreateTime.Size = New System.Drawing.Size(200, 25)
        Me.dtpCreateTime.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.dtpCreateTime, "系统自动生成，不可修改;记录这条工序第一次录入的时间")
        '
        'lblProcessType
        '
        Me.lblProcessType.AutoSize = True
        Me.lblProcessType.Location = New System.Drawing.Point(396, 175)
        Me.lblProcessType.Name = "lblProcessType"
        Me.lblProcessType.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessType.TabIndex = 6
        Me.lblProcessType.Text = "工序类型:"
        '
        'cboSection
        '
        Me.cboSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSection.FormattingEnabled = True
        Me.cboSection.Location = New System.Drawing.Point(131, 179)
        Me.cboSection.Name = "cboSection"
        Me.cboSection.Size = New System.Drawing.Size(121, 23)
        Me.cboSection.TabIndex = 8
        '
        'btnPrev
        '
        Me.btnPrev.Location = New System.Drawing.Point(681, 278)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(75, 23)
        Me.btnPrev.TabIndex = 10
        Me.btnPrev.Text = "上一条"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.SystemColors.Control
        Me.btnNext.Location = New System.Drawing.Point(681, 336)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 10
        Me.btnNext.Text = "下一条"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'dgvMain
        '
        Me.dgvMain.AllowUserToAddRows = False
        Me.dgvMain.AllowUserToDeleteRows = False
        Me.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Location = New System.Drawing.Point(842, 25)
        Me.dgvMain.MultiSelect = False
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.ReadOnly = True
        Me.dgvMain.RowHeadersVisible = False
        Me.dgvMain.RowTemplate.Height = 27
        Me.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMain.Size = New System.Drawing.Size(824, 718)
        Me.dgvMain.TabIndex = 11
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(694, 226)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(75, 23)
        Me.btnFirst.TabIndex = 10
        Me.btnFirst.Text = "第一条"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.BackColor = System.Drawing.SystemColors.Control
        Me.btnLast.Location = New System.Drawing.Point(681, 391)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(75, 23)
        Me.btnLast.TabIndex = 10
        Me.btnLast.Text = "最后一条"
        Me.btnLast.UseVisualStyleBackColor = False
        '
        'L_FmeaMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1709, 789)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.dtpCreateTime)
        Me.Controls.Add(Me.dtpUpdateTime)
        Me.Controls.Add(Me.cboProcessType)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.cboSection)
        Me.Controls.Add(Me.lblUpdateTime)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.lblRemark)
        Me.Controls.Add(Me.lblCreateTime)
        Me.Controls.Add(Me.txtOwner)
        Me.Controls.Add(Me.lblOwner)
        Me.Controls.Add(Me.lblProcessType)
        Me.Controls.Add(Me.txtFunctionReq)
        Me.Controls.Add(Me.txtFunction)
        Me.Controls.Add(Me.txtFmeaTeam)
        Me.Controls.Add(Me.lblFunction)
        Me.Controls.Add(Me.txtVersion)
        Me.Controls.Add(Me.txtDeptName)
        Me.Controls.Add(Me.lblStatusBar)
        Me.Controls.Add(Me.txtProcessName)
        Me.Controls.Add(Me.lblFunctionReq)
        Me.Controls.Add(Me.lblDeptName)
        Me.Controls.Add(Me.lblFmeaTeam)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblProcessName)
        Me.Controls.Add(Me.txtProcessOrder)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblSection)
        Me.Controls.Add(Me.lblProcessOrder)
        Me.Controls.Add(Me.txtProcessNo)
        Me.Controls.Add(Me.lblProcessNo)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnNew)
        Me.Name = "L_FmeaMain"
        Me.Text = "FMEA工序维护"
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnNew As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnDelete As Windows.Forms.Button
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents lblProcessNo As Windows.Forms.Label
    Friend WithEvents txtProcessNo As Windows.Forms.TextBox
    Friend WithEvents lblSection As Windows.Forms.Label
    Friend WithEvents lblProcessOrder As Windows.Forms.Label
    Friend WithEvents txtProcessOrder As Windows.Forms.TextBox
    Friend WithEvents lblDeptName As Windows.Forms.Label
    Friend WithEvents txtDeptName As Windows.Forms.TextBox
    Friend WithEvents lblStatus As Windows.Forms.Label
    Friend WithEvents lblFunction As Windows.Forms.Label
    Friend WithEvents txtFunction As Windows.Forms.TextBox
    Friend WithEvents lblProcessName As Windows.Forms.Label
    Friend WithEvents txtProcessName As Windows.Forms.TextBox
    Friend WithEvents lblVersion As Windows.Forms.Label
    Friend WithEvents txtVersion As Windows.Forms.TextBox
    Friend WithEvents lblOwner As Windows.Forms.Label
    Friend WithEvents txtOwner As Windows.Forms.TextBox
    Friend WithEvents lblFmeaTeam As Windows.Forms.Label
    Friend WithEvents txtFmeaTeam As Windows.Forms.TextBox
    Friend WithEvents lblCreateTime As Windows.Forms.Label
    Friend WithEvents lblFunctionReq As Windows.Forms.Label
    Friend WithEvents txtFunctionReq As Windows.Forms.TextBox
    Friend WithEvents lblRemark As Windows.Forms.Label
    Friend WithEvents txtRemark As Windows.Forms.TextBox
    Friend WithEvents lblStatusBar As Windows.Forms.Label
    Friend WithEvents lblUpdateTime As Windows.Forms.Label
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents lblProcessType As Windows.Forms.Label
    Friend WithEvents cboSection As Windows.Forms.ComboBox
    Friend WithEvents cboProcessType As Windows.Forms.ComboBox
    Friend WithEvents cboStatus As Windows.Forms.ComboBox
    Friend WithEvents dtpUpdateTime As Windows.Forms.DateTimePicker
    Friend WithEvents dtpCreateTime As Windows.Forms.DateTimePicker
    Friend WithEvents btnPrev As Windows.Forms.Button
    Friend WithEvents btnNext As Windows.Forms.Button
    Friend WithEvents dgvMain As Windows.Forms.DataGridView
    Friend WithEvents btnFirst As Windows.Forms.Button
    Friend WithEvents btnLast As Windows.Forms.Button
End Class
