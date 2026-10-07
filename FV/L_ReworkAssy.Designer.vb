<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class L_ReworkAssy
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
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblStatusBar = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.txtHeadRemark = New System.Windows.Forms.TextBox()
        Me.txtTotalCheck = New System.Windows.Forms.TextBox()
        Me.btnDelRow = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboInspector = New System.Windows.Forms.ComboBox()
        Me.lblHeadRemark = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblShift = New System.Windows.Forms.Label()
        Me.lblInspector = New System.Windows.Forms.Label()
        Me.lblTotalCheck = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.dgvDetail = New System.Windows.Forms.DataGridView()
        Me.dgvHead = New System.Windows.Forms.DataGridView()
        Me.btnOpenModelDict = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvHead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAddRow
        '
        Me.btnAddRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddRow.Location = New System.Drawing.Point(1199, 702)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(75, 31)
        Me.btnAddRow.TabIndex = 58
        Me.btnAddRow.Text = "加一行"
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'lblStatusBar
        '
        Me.lblStatusBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusBar.Location = New System.Drawing.Point(13, 702)
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(1064, 31)
        Me.lblStatusBar.TabIndex = 54
        Me.lblStatusBar.Text = "                         "
        Me.lblStatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.lblStatusBar, "显示当前记录/总记录/操作提示")
        '
        'dtpDate
        '
        Me.dtpDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.dtpDate.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(78, 29)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(200, 25)
        Me.dtpDate.TabIndex = 61
        Me.ToolTip1.SetToolTip(Me.dtpDate, "录入发现时间")
        '
        'cboShift
        '
        Me.cboShift.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.FormattingEnabled = True
        Me.cboShift.Location = New System.Drawing.Point(418, 30)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.Size = New System.Drawing.Size(200, 23)
        Me.cboShift.TabIndex = 53
        Me.ToolTip1.SetToolTip(Me.cboShift, "选 8 类之一")
        '
        'txtHeadRemark
        '
        Me.txtHeadRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtHeadRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeadRemark.Location = New System.Drawing.Point(79, 136)
        Me.txtHeadRemark.Multiline = True
        Me.txtHeadRemark.Name = "txtHeadRemark"
        Me.txtHeadRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHeadRemark.Size = New System.Drawing.Size(539, 80)
        Me.txtHeadRemark.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.txtHeadRemark, "备注，可多行")
        '
        'txtTotalCheck
        '
        Me.txtTotalCheck.Location = New System.Drawing.Point(418, 87)
        Me.txtTotalCheck.Name = "txtTotalCheck"
        Me.txtTotalCheck.Size = New System.Drawing.Size(200, 25)
        Me.txtTotalCheck.TabIndex = 54
        Me.ToolTip1.SetToolTip(Me.txtTotalCheck, "录排序号，如 10、20、30")
        '
        'btnDelRow
        '
        Me.btnDelRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelRow.Location = New System.Drawing.Point(1286, 702)
        Me.btnDelRow.Name = "btnDelRow"
        Me.btnDelRow.Size = New System.Drawing.Size(75, 31)
        Me.btnDelRow.TabIndex = 59
        Me.btnDelRow.Text = "删一行"
        Me.btnDelRow.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpDate)
        Me.GroupBox2.Controls.Add(Me.cboShift)
        Me.GroupBox2.Controls.Add(Me.cboInspector)
        Me.GroupBox2.Controls.Add(Me.txtHeadRemark)
        Me.GroupBox2.Controls.Add(Me.lblHeadRemark)
        Me.GroupBox2.Controls.Add(Me.lblDate)
        Me.GroupBox2.Controls.Add(Me.lblShift)
        Me.GroupBox2.Controls.Add(Me.txtTotalCheck)
        Me.GroupBox2.Controls.Add(Me.lblInspector)
        Me.GroupBox2.Controls.Add(Me.lblTotalCheck)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(627, 222)
        Me.GroupBox2.TabIndex = 57
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " 表头信息："
        '
        'cboInspector
        '
        Me.cboInspector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInspector.FormattingEnabled = True
        Me.cboInspector.Location = New System.Drawing.Point(79, 88)
        Me.cboInspector.Name = "cboInspector"
        Me.cboInspector.Size = New System.Drawing.Size(200, 23)
        Me.cboInspector.TabIndex = 52
        '
        'lblHeadRemark
        '
        Me.lblHeadRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblHeadRemark.AutoSize = True
        Me.lblHeadRemark.Location = New System.Drawing.Point(31, 168)
        Me.lblHeadRemark.Name = "lblHeadRemark"
        Me.lblHeadRemark.Size = New System.Drawing.Size(45, 15)
        Me.lblHeadRemark.TabIndex = 60
        Me.lblHeadRemark.Text = "备注:"
        '
        'lblDate
        '
        Me.lblDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(24, 34)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(52, 15)
        Me.lblDate.TabIndex = 57
        Me.lblDate.Text = "日期："
        '
        'lblShift
        '
        Me.lblShift.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblShift.AutoSize = True
        Me.lblShift.Location = New System.Drawing.Point(346, 34)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(52, 15)
        Me.lblShift.TabIndex = 56
        Me.lblShift.Text = "班次："
        '
        'lblInspector
        '
        Me.lblInspector.AutoSize = True
        Me.lblInspector.Location = New System.Drawing.Point(9, 92)
        Me.lblInspector.Name = "lblInspector"
        Me.lblInspector.Size = New System.Drawing.Size(67, 15)
        Me.lblInspector.TabIndex = 58
        Me.lblInspector.Text = "检验员："
        '
        'lblTotalCheck
        '
        Me.lblTotalCheck.AutoSize = True
        Me.lblTotalCheck.Location = New System.Drawing.Point(316, 92)
        Me.lblTotalCheck.Name = "lblTotalCheck"
        Me.lblTotalCheck.Size = New System.Drawing.Size(82, 15)
        Me.lblTotalCheck.TabIndex = 59
        Me.lblTotalCheck.Text = "总检验数："
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(403, 24)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 31)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "刷新"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(273, 24)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 31)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "删除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(143, 24)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 31)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(533, 24)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 31)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(628, 69)
        Me.GroupBox1.TabIndex = 56
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "工具栏"
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
        'dgvDetail
        '
        Me.dgvDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvDetail.Location = New System.Drawing.Point(661, 24)
        Me.dgvDetail.MultiSelect = False
        Me.dgvDetail.Name = "dgvDetail"
        Me.dgvDetail.RowHeadersVisible = False
        Me.dgvDetail.RowTemplate.Height = 27
        Me.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetail.Size = New System.Drawing.Size(701, 672)
        Me.dgvDetail.TabIndex = 55
        '
        'dgvHead
        '
        Me.dgvHead.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvHead.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvHead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHead.Location = New System.Drawing.Point(13, 348)
        Me.dgvHead.MultiSelect = False
        Me.dgvHead.Name = "dgvHead"
        Me.dgvHead.ReadOnly = True
        Me.dgvHead.RowHeadersVisible = False
        Me.dgvHead.RowTemplate.Height = 27
        Me.dgvHead.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvHead.Size = New System.Drawing.Size(627, 348)
        Me.dgvHead.TabIndex = 60
        '
        'btnOpenModelDict
        '
        Me.btnOpenModelDict.Location = New System.Drawing.Point(1112, 702)
        Me.btnOpenModelDict.Name = "btnOpenModelDict"
        Me.btnOpenModelDict.Size = New System.Drawing.Size(75, 31)
        Me.btnOpenModelDict.TabIndex = 61
        Me.btnOpenModelDict.Text = "型号字典"
        Me.btnOpenModelDict.UseVisualStyleBackColor = True
        '
        'L_ReworkAssy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1388, 745)
        Me.Controls.Add(Me.btnOpenModelDict)
        Me.Controls.Add(Me.dgvHead)
        Me.Controls.Add(Me.btnAddRow)
        Me.Controls.Add(Me.lblStatusBar)
        Me.Controls.Add(Me.btnDelRow)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvDetail)
        Me.Name = "L_ReworkAssy"
        Me.Text = "L_ReworkAssy"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAddRow As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents lblStatusBar As Windows.Forms.Label
    Friend WithEvents dtpDate As Windows.Forms.DateTimePicker
    Friend WithEvents cboShift As Windows.Forms.ComboBox
    Friend WithEvents txtHeadRemark As Windows.Forms.TextBox
    Friend WithEvents txtTotalCheck As Windows.Forms.TextBox
    Friend WithEvents btnDelRow As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents cboInspector As Windows.Forms.ComboBox
    Friend WithEvents lblHeadRemark As Windows.Forms.Label
    Friend WithEvents lblDate As Windows.Forms.Label
    Friend WithEvents lblShift As Windows.Forms.Label
    Friend WithEvents lblInspector As Windows.Forms.Label
    Friend WithEvents lblTotalCheck As Windows.Forms.Label
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents btnDelete As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnNew As Windows.Forms.Button
    Friend WithEvents dgvDetail As Windows.Forms.DataGridView
    Friend WithEvents dgvHead As Windows.Forms.DataGridView
    Friend WithEvents btnOpenModelDict As Windows.Forms.Button
End Class
