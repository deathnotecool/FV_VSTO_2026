<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class L_FmeaDetail
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
        Me.lblMainNo = New System.Windows.Forms.Label()
        Me.lblMainName = New System.Windows.Forms.Label()
        Me.txtMainNo = New System.Windows.Forms.TextBox()
        Me.txtMainName = New System.Windows.Forms.TextBox()
        Me.cboFailureModeNo = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCopyNew = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.txtDetailOrder = New System.Windows.Forms.TextBox()
        Me.txtDetectionScore = New System.Windows.Forms.TextBox()
        Me.txtOccurrence = New System.Windows.Forms.TextBox()
        Me.txtPrevention = New System.Windows.Forms.TextBox()
        Me.txtSeverity = New System.Windows.Forms.TextBox()
        Me.txtRPN = New System.Windows.Forms.TextBox()
        Me.txtDetection = New System.Windows.Forms.TextBox()
        Me.txtFailureCause = New System.Windows.Forms.TextBox()
        Me.txtFailureEffect = New System.Windows.Forms.TextBox()
        Me.txtFailureModeName = New System.Windows.Forms.TextBox()
        Me.lblRemark = New System.Windows.Forms.Label()
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
        Me.lblFailureModeName = New System.Windows.Forms.Label()
        Me.lblFailureModeNo = New System.Windows.Forms.Label()
        Me.cboAP = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblStatusBar = New System.Windows.Forms.Label()
        Me.dgvDetail = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'cboFailureModeNo
        '
        Me.cboFailureModeNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFailureModeNo.FormattingEnabled = True
        Me.cboFailureModeNo.Location = New System.Drawing.Point(138, 29)
        Me.cboFailureModeNo.Name = "cboFailureModeNo"
        Me.cboFailureModeNo.Size = New System.Drawing.Size(183, 23)
        Me.cboFailureModeNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cboFailureModeNo, "失效模式编码")
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
        Me.btnSave.TabIndex = 5
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
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtRemark)
        Me.GroupBox2.Controls.Add(Me.txtDetailOrder)
        Me.GroupBox2.Controls.Add(Me.txtDetectionScore)
        Me.GroupBox2.Controls.Add(Me.txtOccurrence)
        Me.GroupBox2.Controls.Add(Me.txtPrevention)
        Me.GroupBox2.Controls.Add(Me.txtSeverity)
        Me.GroupBox2.Controls.Add(Me.txtRPN)
        Me.GroupBox2.Controls.Add(Me.txtDetection)
        Me.GroupBox2.Controls.Add(Me.txtFailureCause)
        Me.GroupBox2.Controls.Add(Me.txtFailureEffect)
        Me.GroupBox2.Controls.Add(Me.txtFailureModeName)
        Me.GroupBox2.Controls.Add(Me.lblRemark)
        Me.GroupBox2.Controls.Add(Me.lblDetailOrder)
        Me.GroupBox2.Controls.Add(Me.lblDetectionScore)
        Me.GroupBox2.Controls.Add(Me.lblRPN)
        Me.GroupBox2.Controls.Add(Me.lblOccurrence)
        Me.GroupBox2.Controls.Add(Me.lblAP)
        Me.GroupBox2.Controls.Add(Me.lblDetection)
        Me.GroupBox2.Controls.Add(Me.lblSeverity)
        Me.GroupBox2.Controls.Add(Me.lblPrevention)
        Me.GroupBox2.Controls.Add(Me.lblFailureCause)
        Me.GroupBox2.Controls.Add(Me.lblFailureEffect)
        Me.GroupBox2.Controls.Add(Me.lblFailureModeName)
        Me.GroupBox2.Controls.Add(Me.lblFailureModeNo)
        Me.GroupBox2.Controls.Add(Me.cboAP)
        Me.GroupBox2.Controls.Add(Me.cboFailureModeNo)
        Me.GroupBox2.Location = New System.Drawing.Point(31, 150)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(650, 898)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "信息输入"
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.Location = New System.Drawing.Point(138, 776)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(504, 114)
        Me.txtRemark.TabIndex = 7
        '
        'txtDetailOrder
        '
        Me.txtDetailOrder.Location = New System.Drawing.Point(459, 722)
        Me.txtDetailOrder.Name = "txtDetailOrder"
        Me.txtDetailOrder.Size = New System.Drawing.Size(183, 25)
        Me.txtDetailOrder.TabIndex = 7
        '
        'txtDetectionScore
        '
        Me.txtDetectionScore.Location = New System.Drawing.Point(138, 662)
        Me.txtDetectionScore.Name = "txtDetectionScore"
        Me.txtDetectionScore.Size = New System.Drawing.Size(183, 25)
        Me.txtDetectionScore.TabIndex = 7
        '
        'txtOccurrence
        '
        Me.txtOccurrence.Location = New System.Drawing.Point(459, 602)
        Me.txtOccurrence.Name = "txtOccurrence"
        Me.txtOccurrence.Size = New System.Drawing.Size(183, 25)
        Me.txtOccurrence.TabIndex = 7
        '
        'txtPrevention
        '
        Me.txtPrevention.Location = New System.Drawing.Point(138, 200)
        Me.txtPrevention.Multiline = True
        Me.txtPrevention.Name = "txtPrevention"
        Me.txtPrevention.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPrevention.Size = New System.Drawing.Size(504, 114)
        Me.txtPrevention.TabIndex = 7
        '
        'txtSeverity
        '
        Me.txtSeverity.Location = New System.Drawing.Point(138, 602)
        Me.txtSeverity.Name = "txtSeverity"
        Me.txtSeverity.Size = New System.Drawing.Size(183, 25)
        Me.txtSeverity.TabIndex = 7
        '
        'txtRPN
        '
        Me.txtRPN.Location = New System.Drawing.Point(459, 662)
        Me.txtRPN.Name = "txtRPN"
        Me.txtRPN.ReadOnly = True
        Me.txtRPN.Size = New System.Drawing.Size(183, 25)
        Me.txtRPN.TabIndex = 7
        '
        'txtDetection
        '
        Me.txtDetection.Location = New System.Drawing.Point(138, 460)
        Me.txtDetection.Multiline = True
        Me.txtDetection.Name = "txtDetection"
        Me.txtDetection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDetection.Size = New System.Drawing.Size(504, 114)
        Me.txtDetection.TabIndex = 7
        '
        'txtFailureCause
        '
        Me.txtFailureCause.Location = New System.Drawing.Point(138, 330)
        Me.txtFailureCause.Multiline = True
        Me.txtFailureCause.Name = "txtFailureCause"
        Me.txtFailureCause.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFailureCause.Size = New System.Drawing.Size(504, 114)
        Me.txtFailureCause.TabIndex = 7
        '
        'txtFailureEffect
        '
        Me.txtFailureEffect.Location = New System.Drawing.Point(138, 70)
        Me.txtFailureEffect.Multiline = True
        Me.txtFailureEffect.Name = "txtFailureEffect"
        Me.txtFailureEffect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFailureEffect.Size = New System.Drawing.Size(504, 114)
        Me.txtFailureEffect.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtFailureEffect, "失效影响：")
        '
        'txtFailureModeName
        '
        Me.txtFailureModeName.Location = New System.Drawing.Point(459, 28)
        Me.txtFailureModeName.Name = "txtFailureModeName"
        Me.txtFailureModeName.ReadOnly = True
        Me.txtFailureModeName.Size = New System.Drawing.Size(183, 25)
        Me.txtFailureModeName.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtFailureModeName, "失效模式名称")
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(65, 809)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(52, 15)
        Me.lblRemark.TabIndex = 4
        Me.lblRemark.Text = "备注："
        '
        'lblDetailOrder
        '
        Me.lblDetailOrder.AutoSize = True
        Me.lblDetailOrder.Location = New System.Drawing.Point(346, 727)
        Me.lblDetailOrder.Name = "lblDetailOrder"
        Me.lblDetailOrder.Size = New System.Drawing.Size(82, 15)
        Me.lblDetailOrder.TabIndex = 5
        Me.lblDetailOrder.Text = "明细顺序："
        '
        'lblDetectionScore
        '
        Me.lblDetectionScore.AutoSize = True
        Me.lblDetectionScore.Location = New System.Drawing.Point(42, 667)
        Me.lblDetectionScore.Name = "lblDetectionScore"
        Me.lblDetectionScore.Size = New System.Drawing.Size(75, 15)
        Me.lblDetectionScore.TabIndex = 4
        Me.lblDetectionScore.Text = "探测度D："
        '
        'lblRPN
        '
        Me.lblRPN.AutoSize = True
        Me.lblRPN.Location = New System.Drawing.Point(378, 667)
        Me.lblRPN.Name = "lblRPN"
        Me.lblRPN.Size = New System.Drawing.Size(46, 15)
        Me.lblRPN.TabIndex = 4
        Me.lblRPN.Text = "RPN："
        '
        'lblOccurrence
        '
        Me.lblOccurrence.AutoSize = True
        Me.lblOccurrence.Location = New System.Drawing.Point(364, 607)
        Me.lblOccurrence.Name = "lblOccurrence"
        Me.lblOccurrence.Size = New System.Drawing.Size(60, 15)
        Me.lblOccurrence.TabIndex = 5
        Me.lblOccurrence.Text = "频度O："
        '
        'lblAP
        '
        Me.lblAP.AutoSize = True
        Me.lblAP.Location = New System.Drawing.Point(49, 727)
        Me.lblAP.Name = "lblAP"
        Me.lblAP.Size = New System.Drawing.Size(68, 15)
        Me.lblAP.TabIndex = 4
        Me.lblAP.Text = "AP等级："
        '
        'lblDetection
        '
        Me.lblDetection.AutoSize = True
        Me.lblDetection.Location = New System.Drawing.Point(35, 509)
        Me.lblDetection.Name = "lblDetection"
        Me.lblDetection.Size = New System.Drawing.Size(82, 15)
        Me.lblDetection.TabIndex = 4
        Me.lblDetection.Text = "探测措施："
        '
        'lblSeverity
        '
        Me.lblSeverity.AutoSize = True
        Me.lblSeverity.Location = New System.Drawing.Point(42, 607)
        Me.lblSeverity.Name = "lblSeverity"
        Me.lblSeverity.Size = New System.Drawing.Size(75, 15)
        Me.lblSeverity.TabIndex = 4
        Me.lblSeverity.Text = "严重度S："
        '
        'lblPrevention
        '
        Me.lblPrevention.AutoSize = True
        Me.lblPrevention.Location = New System.Drawing.Point(35, 247)
        Me.lblPrevention.Name = "lblPrevention"
        Me.lblPrevention.Size = New System.Drawing.Size(82, 15)
        Me.lblPrevention.TabIndex = 5
        Me.lblPrevention.Text = "预防措施："
        '
        'lblFailureCause
        '
        Me.lblFailureCause.AutoSize = True
        Me.lblFailureCause.Location = New System.Drawing.Point(35, 381)
        Me.lblFailureCause.Name = "lblFailureCause"
        Me.lblFailureCause.Size = New System.Drawing.Size(82, 15)
        Me.lblFailureCause.TabIndex = 4
        Me.lblFailureCause.Text = "失效原因："
        '
        'lblFailureEffect
        '
        Me.lblFailureEffect.AutoSize = True
        Me.lblFailureEffect.Location = New System.Drawing.Point(35, 117)
        Me.lblFailureEffect.Name = "lblFailureEffect"
        Me.lblFailureEffect.Size = New System.Drawing.Size(82, 15)
        Me.lblFailureEffect.TabIndex = 5
        Me.lblFailureEffect.Text = "失效影响："
        '
        'lblFailureModeName
        '
        Me.lblFailureModeName.AutoSize = True
        Me.lblFailureModeName.Location = New System.Drawing.Point(334, 33)
        Me.lblFailureModeName.Name = "lblFailureModeName"
        Me.lblFailureModeName.Size = New System.Drawing.Size(112, 15)
        Me.lblFailureModeName.TabIndex = 4
        Me.lblFailureModeName.Text = "失效模式名称："
        '
        'lblFailureModeNo
        '
        Me.lblFailureModeNo.AutoSize = True
        Me.lblFailureModeNo.Location = New System.Drawing.Point(5, 33)
        Me.lblFailureModeNo.Name = "lblFailureModeNo"
        Me.lblFailureModeNo.Size = New System.Drawing.Size(112, 15)
        Me.lblFailureModeNo.TabIndex = 5
        Me.lblFailureModeNo.Text = "失效模式编码："
        '
        'cboAP
        '
        Me.cboAP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAP.Enabled = False
        Me.cboAP.FormattingEnabled = True
        Me.cboAP.Location = New System.Drawing.Point(138, 723)
        Me.cboAP.Name = "cboAP"
        Me.cboAP.Size = New System.Drawing.Size(183, 23)
        Me.cboAP.TabIndex = 3
        '
        'lblStatusBar
        '
        Me.lblStatusBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusBar.Location = New System.Drawing.Point(687, 1017)
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(697, 31)
        Me.lblStatusBar.TabIndex = 7
        Me.lblStatusBar.Text = "                         "
        Me.lblStatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.lblStatusBar, "显示当前记录/总记录/操作提示")
        '
        'dgvDetail
        '
        Me.dgvDetail.AllowUserToAddRows = False
        Me.dgvDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetail.Location = New System.Drawing.Point(687, 75)
        Me.dgvDetail.Name = "dgvDetail"
        Me.dgvDetail.ReadOnly = True
        Me.dgvDetail.RowHeadersVisible = False
        Me.dgvDetail.RowTemplate.Height = 27
        Me.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetail.Size = New System.Drawing.Size(697, 928)
        Me.dgvDetail.TabIndex = 6
        '
        'L_FmeaDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1388, 1063)
        Me.Controls.Add(Me.lblStatusBar)
        Me.Controls.Add(Me.dgvDetail)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtMainName)
        Me.Controls.Add(Me.txtMainNo)
        Me.Controls.Add(Me.lblMainName)
        Me.Controls.Add(Me.lblMainNo)
        Me.Name = "L_FmeaDetail"
        Me.Text = "L_FmeaDetail"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMainNo As Windows.Forms.Label
    Friend WithEvents lblMainName As Windows.Forms.Label
    Friend WithEvents txtMainNo As Windows.Forms.TextBox
    Friend WithEvents txtMainName As Windows.Forms.TextBox
    Friend WithEvents cboFailureModeNo As Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnNew As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents btnDelete As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnCopyNew As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents lblFailureModeName As Windows.Forms.Label
    Friend WithEvents lblFailureModeNo As Windows.Forms.Label
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents txtFailureModeName As Windows.Forms.TextBox
    Friend WithEvents txtRemark As Windows.Forms.TextBox
    Friend WithEvents txtDetailOrder As Windows.Forms.TextBox
    Friend WithEvents txtDetectionScore As Windows.Forms.TextBox
    Friend WithEvents txtOccurrence As Windows.Forms.TextBox
    Friend WithEvents txtPrevention As Windows.Forms.TextBox
    Friend WithEvents txtSeverity As Windows.Forms.TextBox
    Friend WithEvents txtRPN As Windows.Forms.TextBox
    Friend WithEvents txtDetection As Windows.Forms.TextBox
    Friend WithEvents txtFailureCause As Windows.Forms.TextBox
    Friend WithEvents txtFailureEffect As Windows.Forms.TextBox
    Friend WithEvents lblRemark As Windows.Forms.Label
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
    Friend WithEvents cboAP As Windows.Forms.ComboBox
    Friend WithEvents dgvDetail As Windows.Forms.DataGridView
    Friend WithEvents lblStatusBar As Windows.Forms.Label
End Class
