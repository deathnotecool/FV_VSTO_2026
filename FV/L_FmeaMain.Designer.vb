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
        Me.lblStatusBar = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dtpCreateTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpUpdateTime = New System.Windows.Forms.DateTimePicker()
        Me.cboProcessType = New System.Windows.Forms.ComboBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.txtOwner = New System.Windows.Forms.TextBox()
        Me.txtFunctionReq = New System.Windows.Forms.TextBox()
        Me.txtFunction = New System.Windows.Forms.TextBox()
        Me.txtFmeaTeam = New System.Windows.Forms.TextBox()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.txtDeptName = New System.Windows.Forms.TextBox()
        Me.txtProcessName = New System.Windows.Forms.TextBox()
        Me.txtProcessOrder = New System.Windows.Forms.TextBox()
        Me.txtProcessNo = New System.Windows.Forms.TextBox()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCopyNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboSection = New System.Windows.Forms.ComboBox()
        Me.lblUpdateTime = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblCreateTime = New System.Windows.Forms.Label()
        Me.lblOwner = New System.Windows.Forms.Label()
        Me.lblProcessType = New System.Windows.Forms.Label()
        Me.lblFunction = New System.Windows.Forms.Label()
        Me.lblFunctionReq = New System.Windows.Forms.Label()
        Me.lblDeptName = New System.Windows.Forms.Label()
        Me.lblFmeaTeam = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblProcessName = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblSection = New System.Windows.Forms.Label()
        Me.lblProcessOrder = New System.Windows.Forms.Label()
        Me.lblProcessNo = New System.Windows.Forms.Label()
        Me.btnOpenDetail = New System.Windows.Forms.Button()
        Me.btnOpenPChart = New System.Windows.Forms.Button()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblStatusBar
        '
        Me.lblStatusBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusBar.Location = New System.Drawing.Point(736, 683)
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(781, 31)
        Me.lblStatusBar.TabIndex = 6
        Me.lblStatusBar.Text = "                         "
        Me.lblStatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.lblStatusBar, "显示当前记录/总记录/操作提示")
        '
        'dtpCreateTime
        '
        Me.dtpCreateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpCreateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        Me.dtpCreateTime.Enabled = False
        Me.dtpCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCreateTime.Location = New System.Drawing.Point(91, 581)
        Me.dtpCreateTime.Name = "dtpCreateTime"
        Me.dtpCreateTime.Size = New System.Drawing.Size(200, 25)
        Me.dtpCreateTime.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.dtpCreateTime, "系统自动生成，不可修改;记录这条工序第一次录入的时间")
        '
        'dtpUpdateTime
        '
        Me.dtpUpdateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpUpdateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        Me.dtpUpdateTime.Enabled = False
        Me.dtpUpdateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUpdateTime.Location = New System.Drawing.Point(421, 581)
        Me.dtpUpdateTime.Name = "dtpUpdateTime"
        Me.dtpUpdateTime.Size = New System.Drawing.Size(200, 25)
        Me.dtpUpdateTime.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.dtpUpdateTime, "系统自动生成，不可修改;记录这条工序最后一次修改的时间")
        '
        'cboProcessType
        '
        Me.cboProcessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProcessType.FormattingEnabled = True
        Me.cboProcessType.Location = New System.Drawing.Point(443, 88)
        Me.cboProcessType.Name = "cboProcessType"
        Me.cboProcessType.Size = New System.Drawing.Size(144, 23)
        Me.cboProcessType.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.cboProcessType, "选 8 类之一")
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(90, 274)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(144, 23)
        Me.cboStatus.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.cboStatus, "请选择状态：草稿/评审中/修订中/已发布/已作废")
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.Location = New System.Drawing.Point(90, 494)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(507, 80)
        Me.txtRemark.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.txtRemark, "备注，可多行")
        '
        'txtOwner
        '
        Me.txtOwner.Location = New System.Drawing.Point(443, 211)
        Me.txtOwner.Name = "txtOwner"
        Me.txtOwner.Size = New System.Drawing.Size(144, 25)
        Me.txtOwner.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.txtOwner, "录负责人姓名")
        '
        'txtFunctionReq
        '
        Me.txtFunctionReq.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFunctionReq.Location = New System.Drawing.Point(90, 403)
        Me.txtFunctionReq.Multiline = True
        Me.txtFunctionReq.Name = "txtFunctionReq"
        Me.txtFunctionReq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFunctionReq.Size = New System.Drawing.Size(531, 80)
        Me.txtFunctionReq.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.txtFunctionReq, "功能要求/技术条件，可多行")
        '
        'txtFunction
        '
        Me.txtFunction.Location = New System.Drawing.Point(90, 312)
        Me.txtFunction.Multiline = True
        Me.txtFunction.Name = "txtFunction"
        Me.txtFunction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFunction.Size = New System.Drawing.Size(531, 80)
        Me.txtFunction.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.txtFunction, "工序功能描述，可多行")
        '
        'txtFmeaTeam
        '
        Me.txtFmeaTeam.Location = New System.Drawing.Point(443, 274)
        Me.txtFmeaTeam.Name = "txtFmeaTeam"
        Me.txtFmeaTeam.Size = New System.Drawing.Size(144, 25)
        Me.txtFmeaTeam.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.txtFmeaTeam, "多人用 ; 分隔，如 张三;李四;王五")
        '
        'txtVersion
        '
        Me.txtVersion.Location = New System.Drawing.Point(443, 143)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(144, 25)
        Me.txtVersion.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.txtVersion, "人工填，如 V1.0")
        '
        'txtDeptName
        '
        Me.txtDeptName.Location = New System.Drawing.Point(90, 211)
        Me.txtDeptName.Name = "txtDeptName"
        Me.txtDeptName.Size = New System.Drawing.Size(144, 25)
        Me.txtDeptName.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.txtDeptName, "录部门名")
        '
        'txtProcessName
        '
        Me.txtProcessName.Location = New System.Drawing.Point(443, 26)
        Me.txtProcessName.Name = "txtProcessName"
        Me.txtProcessName.Size = New System.Drawing.Size(144, 25)
        Me.txtProcessName.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.txtProcessName, "录工序名，如 入库检")
        '
        'txtProcessOrder
        '
        Me.txtProcessOrder.Location = New System.Drawing.Point(90, 143)
        Me.txtProcessOrder.Name = "txtProcessOrder"
        Me.txtProcessOrder.Size = New System.Drawing.Size(144, 25)
        Me.txtProcessOrder.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.txtProcessOrder, "录排序号，如 10、20、30")
        '
        'txtProcessNo
        '
        Me.txtProcessNo.Location = New System.Drawing.Point(90, 26)
        Me.txtProcessNo.Name = "txtProcessNo"
        Me.txtProcessNo.Size = New System.Drawing.Size(144, 25)
        Me.txtProcessNo.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.txtProcessNo, "录 CP 编号，如 O-01")
        '
        'btnPrev
        '
        Me.btnPrev.Location = New System.Drawing.Point(655, 164)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(75, 31)
        Me.btnPrev.TabIndex = 10
        Me.btnPrev.Text = "上一条"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.SystemColors.Control
        Me.btnNext.Location = New System.Drawing.Point(655, 207)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 31)
        Me.btnNext.TabIndex = 10
        Me.btnNext.Text = "下一条"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'dgvMain
        '
        Me.dgvMain.AllowUserToAddRows = False
        Me.dgvMain.AllowUserToDeleteRows = False
        Me.dgvMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Location = New System.Drawing.Point(736, 12)
        Me.dgvMain.MultiSelect = False
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.ReadOnly = True
        Me.dgvMain.RowHeadersVisible = False
        Me.dgvMain.RowTemplate.Height = 27
        Me.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMain.Size = New System.Drawing.Size(781, 668)
        Me.dgvMain.TabIndex = 11
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(655, 121)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(75, 31)
        Me.btnFirst.TabIndex = 10
        Me.btnFirst.Text = "第一条"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.BackColor = System.Drawing.SystemColors.Control
        Me.btnLast.Location = New System.Drawing.Point(655, 250)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(75, 31)
        Me.btnLast.TabIndex = 10
        Me.btnLast.Text = "最后一条"
        Me.btnLast.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCopyNew)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(628, 69)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "工具栏"
        '
        'btnCopyNew
        '
        Me.btnCopyNew.Location = New System.Drawing.Point(118, 24)
        Me.btnCopyNew.Name = "btnCopyNew"
        Me.btnCopyNew.Size = New System.Drawing.Size(75, 31)
        Me.btnCopyNew.TabIndex = 9
        Me.btnCopyNew.Text = "复制新增"
        Me.btnCopyNew.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(538, 24)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 31)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(433, 24)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 31)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "刷新"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(328, 24)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 31)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "删除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(223, 24)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 31)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(13, 24)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 31)
        Me.btnNew.TabIndex = 5
        Me.btnNew.Text = "新增"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.dtpCreateTime)
        Me.GroupBox2.Controls.Add(Me.dtpUpdateTime)
        Me.GroupBox2.Controls.Add(Me.cboProcessType)
        Me.GroupBox2.Controls.Add(Me.cboStatus)
        Me.GroupBox2.Controls.Add(Me.cboSection)
        Me.GroupBox2.Controls.Add(Me.lblUpdateTime)
        Me.GroupBox2.Controls.Add(Me.txtRemark)
        Me.GroupBox2.Controls.Add(Me.lblRemark)
        Me.GroupBox2.Controls.Add(Me.lblCreateTime)
        Me.GroupBox2.Controls.Add(Me.txtOwner)
        Me.GroupBox2.Controls.Add(Me.lblOwner)
        Me.GroupBox2.Controls.Add(Me.lblProcessType)
        Me.GroupBox2.Controls.Add(Me.txtFunctionReq)
        Me.GroupBox2.Controls.Add(Me.txtFunction)
        Me.GroupBox2.Controls.Add(Me.txtFmeaTeam)
        Me.GroupBox2.Controls.Add(Me.lblFunction)
        Me.GroupBox2.Controls.Add(Me.txtVersion)
        Me.GroupBox2.Controls.Add(Me.txtDeptName)
        Me.GroupBox2.Controls.Add(Me.txtProcessName)
        Me.GroupBox2.Controls.Add(Me.lblFunctionReq)
        Me.GroupBox2.Controls.Add(Me.lblDeptName)
        Me.GroupBox2.Controls.Add(Me.lblFmeaTeam)
        Me.GroupBox2.Controls.Add(Me.lblVersion)
        Me.GroupBox2.Controls.Add(Me.lblProcessName)
        Me.GroupBox2.Controls.Add(Me.txtProcessOrder)
        Me.GroupBox2.Controls.Add(Me.lblStatus)
        Me.GroupBox2.Controls.Add(Me.lblSection)
        Me.GroupBox2.Controls.Add(Me.lblProcessOrder)
        Me.GroupBox2.Controls.Add(Me.txtProcessNo)
        Me.GroupBox2.Controls.Add(Me.lblProcessNo)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 100)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(627, 614)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "信息输入"
        '
        'cboSection
        '
        Me.cboSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSection.FormattingEnabled = True
        Me.cboSection.Location = New System.Drawing.Point(90, 88)
        Me.cboSection.Name = "cboSection"
        Me.cboSection.Size = New System.Drawing.Size(144, 23)
        Me.cboSection.TabIndex = 35
        '
        'lblUpdateTime
        '
        Me.lblUpdateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUpdateTime.AutoSize = True
        Me.lblUpdateTime.Location = New System.Drawing.Point(340, 586)
        Me.lblUpdateTime.Name = "lblUpdateTime"
        Me.lblUpdateTime.Size = New System.Drawing.Size(75, 15)
        Me.lblUpdateTime.TabIndex = 20
        Me.lblUpdateTime.Text = "更新时间:"
        '
        'lblRemark
        '
        Me.lblRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(43, 527)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(45, 15)
        Me.lblRemark.TabIndex = 24
        Me.lblRemark.Text = "备注:"
        '
        'lblCreateTime
        '
        Me.lblCreateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCreateTime.AutoSize = True
        Me.lblCreateTime.Location = New System.Drawing.Point(9, 586)
        Me.lblCreateTime.Name = "lblCreateTime"
        Me.lblCreateTime.Size = New System.Drawing.Size(75, 15)
        Me.lblCreateTime.TabIndex = 18
        Me.lblCreateTime.Text = "创建时间:"
        '
        'lblOwner
        '
        Me.lblOwner.AutoSize = True
        Me.lblOwner.Location = New System.Drawing.Point(357, 215)
        Me.lblOwner.Name = "lblOwner"
        Me.lblOwner.Size = New System.Drawing.Size(60, 15)
        Me.lblOwner.TabIndex = 15
        Me.lblOwner.Text = "负责人:"
        '
        'lblProcessType
        '
        Me.lblProcessType.AutoSize = True
        Me.lblProcessType.Location = New System.Drawing.Point(342, 91)
        Me.lblProcessType.Name = "lblProcessType"
        Me.lblProcessType.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessType.TabIndex = 14
        Me.lblProcessType.Text = "工序类型:"
        '
        'lblFunction
        '
        Me.lblFunction.AutoSize = True
        Me.lblFunction.Location = New System.Drawing.Point(9, 345)
        Me.lblFunction.Name = "lblFunction"
        Me.lblFunction.Size = New System.Drawing.Size(75, 15)
        Me.lblFunction.TabIndex = 17
        Me.lblFunction.Text = "工序功能:"
        '
        'lblFunctionReq
        '
        Me.lblFunctionReq.AutoSize = True
        Me.lblFunctionReq.Location = New System.Drawing.Point(9, 436)
        Me.lblFunctionReq.Name = "lblFunctionReq"
        Me.lblFunctionReq.Size = New System.Drawing.Size(75, 15)
        Me.lblFunctionReq.TabIndex = 11
        Me.lblFunctionReq.Text = "功能要求:"
        '
        'lblDeptName
        '
        Me.lblDeptName.AutoSize = True
        Me.lblDeptName.Location = New System.Drawing.Point(9, 215)
        Me.lblDeptName.Name = "lblDeptName"
        Me.lblDeptName.Size = New System.Drawing.Size(75, 15)
        Me.lblDeptName.TabIndex = 12
        Me.lblDeptName.Text = "责任部门:"
        '
        'lblFmeaTeam
        '
        Me.lblFmeaTeam.AutoSize = True
        Me.lblFmeaTeam.Location = New System.Drawing.Point(340, 277)
        Me.lblFmeaTeam.Name = "lblFmeaTeam"
        Me.lblFmeaTeam.Size = New System.Drawing.Size(77, 15)
        Me.lblFmeaTeam.TabIndex = 13
        Me.lblFmeaTeam.Text = "FMEA小组:"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(357, 153)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(60, 15)
        Me.lblVersion.TabIndex = 16
        Me.lblVersion.Text = "版本号:"
        '
        'lblProcessName
        '
        Me.lblProcessName.AutoSize = True
        Me.lblProcessName.Location = New System.Drawing.Point(342, 29)
        Me.lblProcessName.Name = "lblProcessName"
        Me.lblProcessName.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessName.TabIndex = 19
        Me.lblProcessName.Text = "工序名称:"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(39, 277)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(45, 15)
        Me.lblStatus.TabIndex = 21
        Me.lblStatus.Text = "状态:"
        '
        'lblSection
        '
        Me.lblSection.AutoSize = True
        Me.lblSection.Location = New System.Drawing.Point(24, 91)
        Me.lblSection.Name = "lblSection"
        Me.lblSection.Size = New System.Drawing.Size(60, 15)
        Me.lblSection.TabIndex = 22
        Me.lblSection.Text = "工序段:"
        '
        'lblProcessOrder
        '
        Me.lblProcessOrder.AutoSize = True
        Me.lblProcessOrder.Location = New System.Drawing.Point(9, 153)
        Me.lblProcessOrder.Name = "lblProcessOrder"
        Me.lblProcessOrder.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessOrder.TabIndex = 23
        Me.lblProcessOrder.Text = "工序顺序:"
        '
        'lblProcessNo
        '
        Me.lblProcessNo.AutoSize = True
        Me.lblProcessNo.Location = New System.Drawing.Point(9, 29)
        Me.lblProcessNo.Name = "lblProcessNo"
        Me.lblProcessNo.Size = New System.Drawing.Size(75, 15)
        Me.lblProcessNo.TabIndex = 10
        Me.lblProcessNo.Text = "工序编号:"
        '
        'btnOpenDetail
        '
        Me.btnOpenDetail.Location = New System.Drawing.Point(655, 293)
        Me.btnOpenDetail.Name = "btnOpenDetail"
        Me.btnOpenDetail.Size = New System.Drawing.Size(75, 31)
        Me.btnOpenDetail.TabIndex = 14
        Me.btnOpenDetail.Text = "明细维护"
        Me.btnOpenDetail.UseVisualStyleBackColor = True
        '
        'btnOpenPChart
        '
        Me.btnOpenPChart.Location = New System.Drawing.Point(655, 336)
        Me.btnOpenPChart.Name = "btnOpenPChart"
        Me.btnOpenPChart.Size = New System.Drawing.Size(75, 31)
        Me.btnOpenPChart.TabIndex = 15
        Me.btnOpenPChart.Text = "P图维护"
        Me.btnOpenPChart.UseVisualStyleBackColor = True
        '
        'L_FmeaMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1529, 720)
        Me.Controls.Add(Me.btnOpenPChart)
        Me.Controls.Add(Me.btnOpenDetail)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.lblStatusBar)
        Me.Name = "L_FmeaMain"
        Me.Text = "FMEA工序维护"
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblStatusBar As Windows.Forms.Label
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents btnPrev As Windows.Forms.Button
    Friend WithEvents btnNext As Windows.Forms.Button
    Friend WithEvents dgvMain As Windows.Forms.DataGridView
    Friend WithEvents btnFirst As Windows.Forms.Button
    Friend WithEvents btnLast As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents btnDelete As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnNew As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents dtpCreateTime As Windows.Forms.DateTimePicker
    Friend WithEvents dtpUpdateTime As Windows.Forms.DateTimePicker
    Friend WithEvents cboProcessType As Windows.Forms.ComboBox
    Friend WithEvents cboStatus As Windows.Forms.ComboBox
    Friend WithEvents cboSection As Windows.Forms.ComboBox
    Friend WithEvents lblUpdateTime As Windows.Forms.Label
    Friend WithEvents txtRemark As Windows.Forms.TextBox
    Friend WithEvents lblRemark As Windows.Forms.Label
    Friend WithEvents lblCreateTime As Windows.Forms.Label
    Friend WithEvents txtOwner As Windows.Forms.TextBox
    Friend WithEvents lblOwner As Windows.Forms.Label
    Friend WithEvents lblProcessType As Windows.Forms.Label
    Friend WithEvents txtFunctionReq As Windows.Forms.TextBox
    Friend WithEvents txtFunction As Windows.Forms.TextBox
    Friend WithEvents txtFmeaTeam As Windows.Forms.TextBox
    Friend WithEvents lblFunction As Windows.Forms.Label
    Friend WithEvents txtVersion As Windows.Forms.TextBox
    Friend WithEvents txtDeptName As Windows.Forms.TextBox
    Friend WithEvents txtProcessName As Windows.Forms.TextBox
    Friend WithEvents lblFunctionReq As Windows.Forms.Label
    Friend WithEvents lblDeptName As Windows.Forms.Label
    Friend WithEvents lblFmeaTeam As Windows.Forms.Label
    Friend WithEvents lblVersion As Windows.Forms.Label
    Friend WithEvents lblProcessName As Windows.Forms.Label
    Friend WithEvents txtProcessOrder As Windows.Forms.TextBox
    Friend WithEvents lblStatus As Windows.Forms.Label
    Friend WithEvents lblSection As Windows.Forms.Label
    Friend WithEvents lblProcessOrder As Windows.Forms.Label
    Friend WithEvents txtProcessNo As Windows.Forms.TextBox
    Friend WithEvents lblProcessNo As Windows.Forms.Label
    Friend WithEvents btnCopyNew As Windows.Forms.Button
    Friend WithEvents btnOpenDetail As Windows.Forms.Button
    Friend WithEvents btnOpenPChart As Windows.Forms.Button
End Class
