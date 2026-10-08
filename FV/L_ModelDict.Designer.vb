<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class L_ModelDict
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
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblSortOrder = New System.Windows.Forms.Label()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.txtCustomerPartNo = New System.Windows.Forms.TextBox()
        Me.lblCustomerPartNo = New System.Windows.Forms.Label()
        Me.lblStatusBar = New System.Windows.Forms.Label()
        Me.dgvModel = New System.Windows.Forms.DataGridView()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.txtDrawingNo = New System.Windows.Forms.TextBox()
        Me.btnCopyNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblDrawingNo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        CType(Me.dgvModel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(77, 29)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(573, 25)
        Me.txtSearch.TabIndex = 19
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.Location = New System.Drawing.Point(146, 203)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(458, 333)
        Me.txtRemark.TabIndex = 13
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(58, 357)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(45, 15)
        Me.lblRemark.TabIndex = 41
        Me.lblRemark.Text = "备注:"
        '
        'lblSortOrder
        '
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Location = New System.Drawing.Point(58, 149)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(45, 15)
        Me.lblSortOrder.TabIndex = 37
        Me.lblSortOrder.Text = "排序:"
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(146, 144)
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(458, 25)
        Me.txtSortOrder.TabIndex = 11
        '
        'txtCustomerPartNo
        '
        Me.txtCustomerPartNo.Location = New System.Drawing.Point(146, 89)
        Me.txtCustomerPartNo.Name = "txtCustomerPartNo"
        Me.txtCustomerPartNo.Size = New System.Drawing.Size(458, 25)
        Me.txtCustomerPartNo.TabIndex = 9
        '
        'lblCustomerPartNo
        '
        Me.lblCustomerPartNo.AutoSize = True
        Me.lblCustomerPartNo.Location = New System.Drawing.Point(21, 94)
        Me.lblCustomerPartNo.Name = "lblCustomerPartNo"
        Me.lblCustomerPartNo.Size = New System.Drawing.Size(82, 15)
        Me.lblCustomerPartNo.TabIndex = 38
        Me.lblCustomerPartNo.Text = "客户品号："
        '
        'lblStatusBar
        '
        Me.lblStatusBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusBar.Location = New System.Drawing.Point(686, 666)
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(729, 31)
        Me.lblStatusBar.TabIndex = 23
        Me.lblStatusBar.Text = "                         "
        Me.lblStatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgvModel
        '
        Me.dgvModel.AllowUserToAddRows = False
        Me.dgvModel.AllowUserToDeleteRows = False
        Me.dgvModel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvModel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvModel.Location = New System.Drawing.Point(686, 29)
        Me.dgvModel.MultiSelect = False
        Me.dgvModel.Name = "dgvModel"
        Me.dgvModel.ReadOnly = True
        Me.dgvModel.RowHeadersVisible = False
        Me.dgvModel.RowTemplate.Height = 27
        Me.dgvModel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvModel.Size = New System.Drawing.Size(729, 622)
        Me.dgvModel.TabIndex = 20
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(328, 24)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 31)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "删除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(223, 24)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 31)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(13, 24)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 31)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "新增"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'txtDrawingNo
        '
        Me.txtDrawingNo.Location = New System.Drawing.Point(146, 25)
        Me.txtDrawingNo.Name = "txtDrawingNo"
        Me.txtDrawingNo.Size = New System.Drawing.Size(458, 25)
        Me.txtDrawingNo.TabIndex = 8
        '
        'btnCopyNew
        '
        Me.btnCopyNew.Location = New System.Drawing.Point(118, 24)
        Me.btnCopyNew.Name = "btnCopyNew"
        Me.btnCopyNew.Size = New System.Drawing.Size(75, 31)
        Me.btnCopyNew.TabIndex = 2
        Me.btnCopyNew.Text = "复制新增"
        Me.btnCopyNew.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(538, 24)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 31)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCopyNew)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(628, 69)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "工具栏"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(433, 24)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 31)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "刷新"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblDrawingNo
        '
        Me.lblDrawingNo.AutoSize = True
        Me.lblDrawingNo.Location = New System.Drawing.Point(51, 30)
        Me.lblDrawingNo.Name = "lblDrawingNo"
        Me.lblDrawingNo.Size = New System.Drawing.Size(52, 15)
        Me.lblDrawingNo.TabIndex = 36
        Me.lblDrawingNo.Text = "图号："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 15)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "搜索："
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtRemark)
        Me.GroupBox2.Controls.Add(Me.lblRemark)
        Me.GroupBox2.Controls.Add(Me.lblSortOrder)
        Me.GroupBox2.Controls.Add(Me.txtSortOrder)
        Me.GroupBox2.Controls.Add(Me.txtCustomerPartNo)
        Me.GroupBox2.Controls.Add(Me.lblCustomerPartNo)
        Me.GroupBox2.Controls.Add(Me.txtDrawingNo)
        Me.GroupBox2.Controls.Add(Me.lblDrawingNo)
        Me.GroupBox2.Location = New System.Drawing.Point(22, 137)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(628, 560)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "信息输入"
        '
        'L_ModelDict
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1422, 734)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblStatusBar)
        Me.Controls.Add(Me.dgvModel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "L_ModelDict"
        Me.Text = "L_ModelDict"
        CType(Me.dgvModel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtSearch As Windows.Forms.TextBox
    Friend WithEvents txtRemark As Windows.Forms.TextBox
    Friend WithEvents lblRemark As Windows.Forms.Label
    Friend WithEvents lblSortOrder As Windows.Forms.Label
    Friend WithEvents txtSortOrder As Windows.Forms.TextBox
    Friend WithEvents txtCustomerPartNo As Windows.Forms.TextBox
    Friend WithEvents lblCustomerPartNo As Windows.Forms.Label
    Friend WithEvents lblStatusBar As Windows.Forms.Label
    Friend WithEvents dgvModel As Windows.Forms.DataGridView
    Friend WithEvents btnDelete As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents btnNew As Windows.Forms.Button
    Friend WithEvents txtDrawingNo As Windows.Forms.TextBox
    Friend WithEvents btnCopyNew As Windows.Forms.Button
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnRefresh As Windows.Forms.Button
    Friend WithEvents lblDrawingNo As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
End Class
