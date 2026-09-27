<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class L_FmeaDetail
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
        Me.lblMainNo = New System.Windows.Forms.Label()
        Me.lblMainName = New System.Windows.Forms.Label()
        Me.txtMainNo = New System.Windows.Forms.TextBox()
        Me.txtMainName = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCopyNew = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblStatusBar = New System.Windows.Forms.Label()
        Me.txtFailureEffect = New System.Windows.Forms.TextBox()
        Me.txtFailureModeName = New System.Windows.Forms.TextBox()
        Me.cboFailureModeNo = New System.Windows.Forms.ComboBox()
        Me.dgvDetail = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtDetection = New System.Windows.Forms.TextBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.txtProcessChar = New System.Windows.Forms.TextBox()
        Me.txtProductChar = New System.Windows.Forms.TextBox()
        Me.txtDetailOrder = New System.Windows.Forms.TextBox()
        Me.txtDetectionScore = New System.Windows.Forms.TextBox()
        Me.txtOccurrence = New System.Windows.Forms.TextBox()
        Me.txtPrevention = New System.Windows.Forms.TextBox()
        Me.txtSeverity = New System.Windows.Forms.TextBox()
        Me.txtRPN = New System.Windows.Forms.TextBox()
        Me.txtFailureCause = New System.Windows.Forms.TextBox()
        Me.lblDetailOrder = New System.Windows.Forms.Label()
        Me.lblDetectionScore = New System.Windows.Forms.Label()
        Me.lblRPN = New System.Windows.Forms.Label()
        Me.lblOccurrence = New System.Windows.Forms.Label()
        Me.lblAP = New System.Windows.Forms.Label()
        Me.lblDetection = New System.Windows.Forms.Label()
        Me.lblSeverity = New System.Windows.Forms.Label()
        Me.lblPrevention = New System.Windows.Forms.Label()
        Me.lblFailureCause = New System.Windows.Forms.Label()
        Me.lblFailureEffect = New System.Windows.Forms.Label()
        Me.lblProcessChar = New System.Windows.Forms.Label()
        Me.lblProductChar = New System.Windows.Forms.Label()
        Me.lblFailureModeName = New System.Windows.Forms.Label()
        Me.lblFailureModeNo = New System.Windows.Forms.Label()
        Me.cboAP = New System.Windows.Forms.ComboBox()
        Me.btnImportDetail = New System.Windows.Forms.Button()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.lblImagePath = New System.Windows.Forms.Label()
        Me.btnPickImage = New System.Windows.Forms.Button()
        Me.btnClearImage = New System.Windows.Forms.Button()
        Me.btnOpenDict = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMainNo
        '
        Me.lblMainNo.AutoSize = True
        Me.lblMainNo.Location = New System.Drawing.Point(33, 26)
        Me.lblMainNo.Name = "lblMainNo"
        Me.lblMainNo.Size = New System.Drawing.Size(82, 15)
        Me.lblMainNo.TabIndex = 1
        Me.lblMainNo.Text = "工序编号："
        '
        'lblMainName
        '
        Me.lblMainName.AutoSize = True
        Me.lblMainName.Location = New System.Drawing.Point(263, 26)
        Me.lblMainName.Name = "lblMainName"
        Me.lblMainName.Size = New System.Drawing.Size(82, 15)
        Me.lblMainName.TabIndex = 1
        Me.lblMainName.Text = "工序名称："
        '
        'txtMainNo
        '
        Me.txtMainNo.Location = New System.Drawing.Point(117, 21)
        Me.txtMainNo.Name = "txtMainNo"
        Me.txtMainNo.ReadOnly = True
        Me.txtMainNo.Size = New System.Drawing.Size(141, 25)
        Me.txtMainNo.TabIndex = 2
        '
        'txtMainName
        '
        Me.txtMainName.Location = New System.Drawing.Point(347, 21)
        Me.txtMainName.Name = "txtMainName"
        Me.txtMainName.ReadOnly = True
        Me.txtMainName.Size = New System.Drawing.Size(141, 25)
        Me.txtMainName.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnCopyNew)
        Me.GroupBox1.Location = New System.Drawing.Point(31, 66)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(650, 78)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "工具栏"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(9, 31)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 26)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "新增"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(569, 31)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 26)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(457, 31)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 26)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "刷新"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(345, 31)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 26)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "删除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(233, 31)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 26)
        Me.btnSave.TabIndex = 16
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCopyNew
        '
        Me.btnCopyNew.Location = New System.Drawing.Point(121, 31)
        Me.btnCopyNew.Name = "btnCopyNew"
        Me.btnCopyNew.Size = New System.Drawing.Size(75, 26)
        Me.btnCopyNew.TabIndex = 6
        Me.btnCopyNew.Text = "复制新增"
        Me.btnCopyNew.UseVisualStyleBackColor = True
        '
        'lblStatusBar
        '
        Me.lblStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusBar.Location = New System.Drawing.Point(687, 665)
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(223, 31)
        Me.lblStatusBar.TabIndex = 7
        Me.lblStatusBar.Text = "                         "
        Me.lblStatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.lblStatusBar, "显示当前记录/总记录/操作提示")
        '
        'txtFailureEffect
        '
        Me.txtFailureEffect.Location = New System.Drawing.Point(143, 299)
        Me.txtFailureEffect.Multiline = True
        Me.txtFailureEffect.Name = "txtFailureEffect"
        Me.txtFailureEffect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFailureEffect.Size = New System.Drawing.Size(504, 110)
        Me.txtFailureEffect.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtFailureEffect, "失效影响：")
        '
        'txtFailureModeName
        '
        Me.txtFailureModeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFailureModeName.Location = New System.Drawing.Point(145, 37)
        Me.txtFailureModeName.Name = "txtFailureModeName"
        Me.txtFailureModeName.ReadOnly = True
        Me.txtFailureModeName.Size = New System.Drawing.Size(497, 25)
        Me.txtFailureModeName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtFailureModeName, "失效模式名称")
        '
        'cboFailureModeNo
        '
        Me.cboFailureModeNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboFailureModeNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFailureModeNo.FormattingEnabled = True
        Me.cboFailureModeNo.Location = New System.Drawing.Point(145, 7)
        Me.cboFailureModeNo.Name = "cboFailureModeNo"
        Me.cboFailureModeNo.Size = New System.Drawing.Size(497, 23)
        Me.cboFailureModeNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cboFailureModeNo, "失效模式编码")
        '
        'dgvDetail
        '
        Me.dgvDetail.AllowUserToAddRows = False
        Me.dgvDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetail.Location = New System.Drawing.Point(687, 75)
        Me.dgvDetail.Name = "dgvDetail"
        Me.dgvDetail.ReadOnly = True
        Me.dgvDetail.RowHeadersVisible = False
        Me.dgvDetail.RowTemplate.Height = 27
        Me.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetail.Size = New System.Drawing.Size(697, 580)
        Me.dgvDetail.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.txtDetection)
        Me.Panel1.Controls.Add(Me.txtRemark)
        Me.Panel1.Controls.Add(Me.lblRemark)
        Me.Panel1.Controls.Add(Me.txtProcessChar)
        Me.Panel1.Controls.Add(Me.txtProductChar)
        Me.Panel1.Controls.Add(Me.txtDetailOrder)
        Me.Panel1.Controls.Add(Me.txtDetectionScore)
        Me.Panel1.Controls.Add(Me.txtOccurrence)
        Me.Panel1.Controls.Add(Me.txtPrevention)
        Me.Panel1.Controls.Add(Me.txtSeverity)
        Me.Panel1.Controls.Add(Me.txtRPN)
        Me.Panel1.Controls.Add(Me.txtFailureCause)
        Me.Panel1.Controls.Add(Me.txtFailureEffect)
        Me.Panel1.Controls.Add(Me.txtFailureModeName)
        Me.Panel1.Controls.Add(Me.lblDetailOrder)
        Me.Panel1.Controls.Add(Me.lblDetectionScore)
        Me.Panel1.Controls.Add(Me.lblRPN)
        Me.Panel1.Controls.Add(Me.lblOccurrence)
        Me.Panel1.Controls.Add(Me.lblAP)
        Me.Panel1.Controls.Add(Me.lblDetection)
        Me.Panel1.Controls.Add(Me.lblSeverity)
        Me.Panel1.Controls.Add(Me.lblPrevention)
        Me.Panel1.Controls.Add(Me.lblFailureCause)
        Me.Panel1.Controls.Add(Me.lblFailureEffect)
        Me.Panel1.Controls.Add(Me.lblProcessChar)
        Me.Panel1.Controls.Add(Me.lblProductChar)
        Me.Panel1.Controls.Add(Me.lblFailureModeName)
        Me.Panel1.Controls.Add(Me.lblFailureModeNo)
        Me.Panel1.Controls.Add(Me.cboAP)
        Me.Panel1.Controls.Add(Me.cboFailureModeNo)
        Me.Panel1.Location = New System.Drawing.Point(31, 150)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(650, 902)
        Me.Panel1.TabIndex = 8
        '
        'txtDetection
        '
        Me.txtDetection.Location = New System.Drawing.Point(143, 645)
        Me.txtDetection.Multiline = True
        Me.txtDetection.Name = "txtDetection"
        Me.txtDetection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDetection.Size = New System.Drawing.Size(504, 110)
        Me.txtDetection.TabIndex = 8
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.Location = New System.Drawing.Point(145, 852)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(499, 44)
        Me.txtRemark.TabIndex = 15
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(70, 868)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(52, 15)
        Me.lblRemark.TabIndex = 38
        Me.lblRemark.Text = "备注："
        '
        'txtProcessChar
        '
        Me.txtProcessChar.Location = New System.Drawing.Point(143, 184)
        Me.txtProcessChar.Multiline = True
        Me.txtProcessChar.Name = "txtProcessChar"
        Me.txtProcessChar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProcessChar.Size = New System.Drawing.Size(504, 110)
        Me.txtProcessChar.TabIndex = 4
        '
        'txtProductChar
        '
        Me.txtProductChar.Location = New System.Drawing.Point(143, 69)
        Me.txtProductChar.Multiline = True
        Me.txtProductChar.Name = "txtProductChar"
        Me.txtProductChar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProductChar.Size = New System.Drawing.Size(504, 110)
        Me.txtProductChar.TabIndex = 3
        '
        'txtDetailOrder
        '
        Me.txtDetailOrder.Location = New System.Drawing.Point(461, 822)
        Me.txtDetailOrder.Name = "txtDetailOrder"
        Me.txtDetailOrder.Size = New System.Drawing.Size(183, 25)
        Me.txtDetailOrder.TabIndex = 14
        '
        'txtDetectionScore
        '
        Me.txtDetectionScore.Location = New System.Drawing.Point(143, 789)
        Me.txtDetectionScore.Name = "txtDetectionScore"
        Me.txtDetectionScore.Size = New System.Drawing.Size(183, 25)
        Me.txtDetectionScore.TabIndex = 11
        '
        'txtOccurrence
        '
        Me.txtOccurrence.Location = New System.Drawing.Point(459, 760)
        Me.txtOccurrence.Name = "txtOccurrence"
        Me.txtOccurrence.Size = New System.Drawing.Size(183, 25)
        Me.txtOccurrence.TabIndex = 10
        '
        'txtPrevention
        '
        Me.txtPrevention.Location = New System.Drawing.Point(143, 414)
        Me.txtPrevention.Multiline = True
        Me.txtPrevention.Name = "txtPrevention"
        Me.txtPrevention.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPrevention.Size = New System.Drawing.Size(504, 110)
        Me.txtPrevention.TabIndex = 6
        '
        'txtSeverity
        '
        Me.txtSeverity.Location = New System.Drawing.Point(143, 758)
        Me.txtSeverity.Name = "txtSeverity"
        Me.txtSeverity.Size = New System.Drawing.Size(183, 25)
        Me.txtSeverity.TabIndex = 9
        '
        'txtRPN
        '
        Me.txtRPN.Location = New System.Drawing.Point(459, 791)
        Me.txtRPN.Name = "txtRPN"
        Me.txtRPN.ReadOnly = True
        Me.txtRPN.Size = New System.Drawing.Size(183, 25)
        Me.txtRPN.TabIndex = 12
        '
        'txtFailureCause
        '
        Me.txtFailureCause.Location = New System.Drawing.Point(145, 529)
        Me.txtFailureCause.Multiline = True
        Me.txtFailureCause.Name = "txtFailureCause"
        Me.txtFailureCause.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFailureCause.Size = New System.Drawing.Size(504, 110)
        Me.txtFailureCause.TabIndex = 7
        '
        'lblDetailOrder
        '
        Me.lblDetailOrder.AutoSize = True
        Me.lblDetailOrder.Location = New System.Drawing.Point(353, 825)
        Me.lblDetailOrder.Name = "lblDetailOrder"
        Me.lblDetailOrder.Size = New System.Drawing.Size(82, 15)
        Me.lblDetailOrder.TabIndex = 22
        Me.lblDetailOrder.Text = "明细顺序："
        '
        'lblDetectionScore
        '
        Me.lblDetectionScore.AutoSize = True
        Me.lblDetectionScore.Location = New System.Drawing.Point(47, 794)
        Me.lblDetectionScore.Name = "lblDetectionScore"
        Me.lblDetectionScore.Size = New System.Drawing.Size(75, 15)
        Me.lblDetectionScore.TabIndex = 20
        Me.lblDetectionScore.Text = "探测度D："
        '
        'lblRPN
        '
        Me.lblRPN.AutoSize = True
        Me.lblRPN.Location = New System.Drawing.Point(383, 794)
        Me.lblRPN.Name = "lblRPN"
        Me.lblRPN.Size = New System.Drawing.Size(46, 15)
        Me.lblRPN.TabIndex = 19
        Me.lblRPN.Text = "RPN："
        '
        'lblOccurrence
        '
        Me.lblOccurrence.AutoSize = True
        Me.lblOccurrence.Location = New System.Drawing.Point(369, 763)
        Me.lblOccurrence.Name = "lblOccurrence"
        Me.lblOccurrence.Size = New System.Drawing.Size(60, 15)
        Me.lblOccurrence.TabIndex = 25
        Me.lblOccurrence.Text = "频度O："
        '
        'lblAP
        '
        Me.lblAP.AutoSize = True
        Me.lblAP.Location = New System.Drawing.Point(56, 825)
        Me.lblAP.Name = "lblAP"
        Me.lblAP.Size = New System.Drawing.Size(68, 15)
        Me.lblAP.TabIndex = 18
        Me.lblAP.Text = "AP等级："
        '
        'lblDetection
        '
        Me.lblDetection.AutoSize = True
        Me.lblDetection.Location = New System.Drawing.Point(40, 694)
        Me.lblDetection.Name = "lblDetection"
        Me.lblDetection.Size = New System.Drawing.Size(82, 15)
        Me.lblDetection.TabIndex = 17
        Me.lblDetection.Text = "探测措施："
        '
        'lblSeverity
        '
        Me.lblSeverity.AutoSize = True
        Me.lblSeverity.Location = New System.Drawing.Point(47, 763)
        Me.lblSeverity.Name = "lblSeverity"
        Me.lblSeverity.Size = New System.Drawing.Size(75, 15)
        Me.lblSeverity.TabIndex = 16
        Me.lblSeverity.Text = "严重度S："
        '
        'lblPrevention
        '
        Me.lblPrevention.AutoSize = True
        Me.lblPrevention.Location = New System.Drawing.Point(40, 455)
        Me.lblPrevention.Name = "lblPrevention"
        Me.lblPrevention.Size = New System.Drawing.Size(82, 15)
        Me.lblPrevention.TabIndex = 26
        Me.lblPrevention.Text = "预防措施："
        '
        'lblFailureCause
        '
        Me.lblFailureCause.AutoSize = True
        Me.lblFailureCause.Location = New System.Drawing.Point(42, 572)
        Me.lblFailureCause.Name = "lblFailureCause"
        Me.lblFailureCause.Size = New System.Drawing.Size(82, 15)
        Me.lblFailureCause.TabIndex = 15
        Me.lblFailureCause.Text = "失效原因："
        '
        'lblFailureEffect
        '
        Me.lblFailureEffect.AutoSize = True
        Me.lblFailureEffect.Location = New System.Drawing.Point(40, 335)
        Me.lblFailureEffect.Name = "lblFailureEffect"
        Me.lblFailureEffect.Size = New System.Drawing.Size(82, 15)
        Me.lblFailureEffect.TabIndex = 24
        Me.lblFailureEffect.Text = "失效影响："
        '
        'lblProcessChar
        '
        Me.lblProcessChar.AutoSize = True
        Me.lblProcessChar.Location = New System.Drawing.Point(40, 230)
        Me.lblProcessChar.Name = "lblProcessChar"
        Me.lblProcessChar.Size = New System.Drawing.Size(82, 15)
        Me.lblProcessChar.TabIndex = 14
        Me.lblProcessChar.Text = "过程特性："
        '
        'lblProductChar
        '
        Me.lblProductChar.AutoSize = True
        Me.lblProductChar.Location = New System.Drawing.Point(40, 113)
        Me.lblProductChar.Name = "lblProductChar"
        Me.lblProductChar.Size = New System.Drawing.Size(82, 15)
        Me.lblProductChar.TabIndex = 23
        Me.lblProductChar.Text = "产品特性："
        '
        'lblFailureModeName
        '
        Me.lblFailureModeName.AutoSize = True
        Me.lblFailureModeName.Location = New System.Drawing.Point(10, 40)
        Me.lblFailureModeName.Name = "lblFailureModeName"
        Me.lblFailureModeName.Size = New System.Drawing.Size(112, 15)
        Me.lblFailureModeName.TabIndex = 13
        Me.lblFailureModeName.Text = "失效模式名称："
        '
        'lblFailureModeNo
        '
        Me.lblFailureModeNo.AutoSize = True
        Me.lblFailureModeNo.Location = New System.Drawing.Point(10, 15)
        Me.lblFailureModeNo.Name = "lblFailureModeNo"
        Me.lblFailureModeNo.Size = New System.Drawing.Size(112, 15)
        Me.lblFailureModeNo.TabIndex = 21
        Me.lblFailureModeNo.Text = "失效模式编码："
        '
        'cboAP
        '
        Me.cboAP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAP.Enabled = False
        Me.cboAP.FormattingEnabled = True
        Me.cboAP.Location = New System.Drawing.Point(145, 821)
        Me.cboAP.Name = "cboAP"
        Me.cboAP.Size = New System.Drawing.Size(183, 23)
        Me.cboAP.TabIndex = 13
        '
        'btnImportDetail
        '
        Me.btnImportDetail.Location = New System.Drawing.Point(1210, 21)
        Me.btnImportDetail.Name = "btnImportDetail"
        Me.btnImportDetail.Size = New System.Drawing.Size(155, 39)
        Me.btnImportDetail.TabIndex = 24
        Me.btnImportDetail.Text = "从Excel导入明细表"
        Me.btnImportDetail.UseVisualStyleBackColor = True
        '
        'picImage
        '
        Me.picImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImage.Location = New System.Drawing.Point(687, 703)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(697, 349)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picImage.TabIndex = 25
        Me.picImage.TabStop = False
        '
        'lblImagePath
        '
        Me.lblImagePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblImagePath.Font = New System.Drawing.Font("隶书", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblImagePath.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblImagePath.Location = New System.Drawing.Point(957, 665)
        Me.lblImagePath.Name = "lblImagePath"
        Me.lblImagePath.Size = New System.Drawing.Size(223, 31)
        Me.lblImagePath.TabIndex = 7
        Me.lblImagePath.Text = "无图片                   "
        Me.lblImagePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnPickImage
        '
        Me.btnPickImage.Location = New System.Drawing.Point(1186, 664)
        Me.btnPickImage.Name = "btnPickImage"
        Me.btnPickImage.Size = New System.Drawing.Size(92, 32)
        Me.btnPickImage.TabIndex = 26
        Me.btnPickImage.Text = "选图"
        Me.btnPickImage.UseVisualStyleBackColor = True
        '
        'btnClearImage
        '
        Me.btnClearImage.Location = New System.Drawing.Point(1284, 664)
        Me.btnClearImage.Name = "btnClearImage"
        Me.btnClearImage.Size = New System.Drawing.Size(92, 32)
        Me.btnClearImage.TabIndex = 26
        Me.btnClearImage.Text = "" & Global.Microsoft.VisualBasic.ChrW(9) & "清除"
        Me.btnClearImage.UseVisualStyleBackColor = True
        '
        'btnOpenDict
        '
        Me.btnOpenDict.Location = New System.Drawing.Point(1025, 21)
        Me.btnOpenDict.Name = "btnOpenDict"
        Me.btnOpenDict.Size = New System.Drawing.Size(155, 39)
        Me.btnOpenDict.TabIndex = 24
        Me.btnOpenDict.Text = "失效模式字典"
        Me.btnOpenDict.UseVisualStyleBackColor = True
        '
        'L_FmeaDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1388, 1055)
        Me.Controls.Add(Me.btnClearImage)
        Me.Controls.Add(Me.btnPickImage)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.btnOpenDict)
        Me.Controls.Add(Me.btnImportDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblImagePath)
        Me.Controls.Add(Me.lblStatusBar)
        Me.Controls.Add(Me.dgvDetail)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtMainName)
        Me.Controls.Add(Me.txtMainNo)
        Me.Controls.Add(Me.lblMainName)
        Me.Controls.Add(Me.lblMainNo)
        Me.Name = "L_FmeaDetail"
        Me.Text = "L_FmeaDetail"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMainNo As Windows.Forms.Label
    Friend WithEvents lblMainName As Windows.Forms.Label
    Friend WithEvents txtMainNo As Windows.Forms.TextBox
    Friend WithEvents txtMainName As Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnNew As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents btnDelete As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnCopyNew As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents dgvDetail As Windows.Forms.DataGridView
    Friend WithEvents lblStatusBar As Windows.Forms.Label
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents txtDetection As Windows.Forms.TextBox
    Friend WithEvents txtRemark As Windows.Forms.TextBox
    Friend WithEvents lblRemark As Windows.Forms.Label
    Friend WithEvents txtProcessChar As Windows.Forms.TextBox
    Friend WithEvents txtProductChar As Windows.Forms.TextBox
    Friend WithEvents txtDetailOrder As Windows.Forms.TextBox
    Friend WithEvents txtDetectionScore As Windows.Forms.TextBox
    Friend WithEvents txtOccurrence As Windows.Forms.TextBox
    Friend WithEvents txtPrevention As Windows.Forms.TextBox
    Friend WithEvents txtSeverity As Windows.Forms.TextBox
    Friend WithEvents txtRPN As Windows.Forms.TextBox
    Friend WithEvents txtFailureCause As Windows.Forms.TextBox
    Friend WithEvents txtFailureEffect As Windows.Forms.TextBox
    Friend WithEvents txtFailureModeName As Windows.Forms.TextBox
    Friend WithEvents lblDetailOrder As Windows.Forms.Label
    Friend WithEvents lblDetectionScore As Windows.Forms.Label
    Friend WithEvents lblRPN As Windows.Forms.Label
    Friend WithEvents lblOccurrence As Windows.Forms.Label
    Friend WithEvents lblAP As Windows.Forms.Label
    Friend WithEvents lblDetection As Windows.Forms.Label
    Friend WithEvents lblSeverity As Windows.Forms.Label
    Friend WithEvents lblPrevention As Windows.Forms.Label
    Friend WithEvents lblFailureCause As Windows.Forms.Label
    Friend WithEvents lblFailureEffect As Windows.Forms.Label
    Friend WithEvents lblProcessChar As Windows.Forms.Label
    Friend WithEvents lblProductChar As Windows.Forms.Label
    Friend WithEvents lblFailureModeName As Windows.Forms.Label
    Friend WithEvents lblFailureModeNo As Windows.Forms.Label
    Friend WithEvents cboAP As Windows.Forms.ComboBox
    Friend WithEvents cboFailureModeNo As Windows.Forms.ComboBox
    Friend WithEvents btnImportDetail As Windows.Forms.Button
    Friend WithEvents picImage As Windows.Forms.PictureBox
    Friend WithEvents lblImagePath As Windows.Forms.Label
    Friend WithEvents btnPickImage As Windows.Forms.Button
    Friend WithEvents btnClearImage As Windows.Forms.Button
    Friend WithEvents btnOpenDict As Windows.Forms.Button
End Class
