<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN190810_VBA代码笔记
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
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtCodeDetail = New System.Windows.Forms.TextBox()
        Me.btnCopyCode = New System.Windows.Forms.Button()
        Me.btnDeleteNote = New System.Windows.Forms.Button()
        Me.btnEditNote = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.btnAddNote = New System.Windows.Forms.Button()
        Me.txtNewCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNewRemark = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNewTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbFilterCategory = New System.Windows.Forms.ComboBox()
        Me.btnInsertCode = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(173, 15)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(155, 23)
        Me.ComboBox1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(336, 14)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(217, 25)
        Me.TextBox1.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(911, 733)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(83, 29)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "退出"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.BackColor = System.Drawing.SystemColors.Window
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(13, 51)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(892, 717)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(558, 12)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(83, 29)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "查询"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtCodeDetail
        '
        Me.txtCodeDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCodeDetail.Location = New System.Drawing.Point(1001, 51)
        Me.txtCodeDetail.Multiline = True
        Me.txtCodeDetail.Name = "txtCodeDetail"
        Me.txtCodeDetail.ReadOnly = True
        Me.txtCodeDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCodeDetail.Size = New System.Drawing.Size(712, 352)
        Me.txtCodeDetail.TabIndex = 7
        Me.txtCodeDetail.Text = "点击列表行查看完整代码"
        Me.txtCodeDetail.WordWrap = False
        '
        'btnCopyCode
        '
        Me.btnCopyCode.Location = New System.Drawing.Point(822, 12)
        Me.btnCopyCode.Name = "btnCopyCode"
        Me.btnCopyCode.Size = New System.Drawing.Size(83, 29)
        Me.btnCopyCode.TabIndex = 8
        Me.btnCopyCode.Text = "复制代码"
        Me.btnCopyCode.UseVisualStyleBackColor = True
        '
        'btnDeleteNote
        '
        Me.btnDeleteNote.Location = New System.Drawing.Point(734, 12)
        Me.btnDeleteNote.Name = "btnDeleteNote"
        Me.btnDeleteNote.Size = New System.Drawing.Size(83, 29)
        Me.btnDeleteNote.TabIndex = 16
        Me.btnDeleteNote.Text = "删除笔记"
        Me.btnDeleteNote.UseVisualStyleBackColor = True
        '
        'btnEditNote
        '
        Me.btnEditNote.Location = New System.Drawing.Point(646, 12)
        Me.btnEditNote.Name = "btnEditNote"
        Me.btnEditNote.Size = New System.Drawing.Size(83, 29)
        Me.btnEditNote.TabIndex = 17
        Me.btnEditNote.Text = "编辑笔记"
        Me.btnEditNote.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbCategory)
        Me.GroupBox1.Controls.Add(Me.btnAddNote)
        Me.GroupBox1.Controls.Add(Me.txtNewCode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtNewRemark)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtNewTitle)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(1001, 434)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(712, 334)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "编辑笔记"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 15)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "分类:"
        '
        'cmbCategory
        '
        Me.cmbCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(64, 29)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(476, 23)
        Me.cmbCategory.TabIndex = 30
        '
        'btnAddNote
        '
        Me.btnAddNote.Location = New System.Drawing.Point(600, 18)
        Me.btnAddNote.Name = "btnAddNote"
        Me.btnAddNote.Size = New System.Drawing.Size(100, 34)
        Me.btnAddNote.TabIndex = 34
        Me.btnAddNote.Text = "保存笔记"
        Me.btnAddNote.UseVisualStyleBackColor = True
        '
        'txtNewCode
        '
        Me.txtNewCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewCode.Location = New System.Drawing.Point(64, 129)
        Me.txtNewCode.Multiline = True
        Me.txtNewCode.Name = "txtNewCode"
        Me.txtNewCode.Size = New System.Drawing.Size(636, 199)
        Me.txtNewCode.TabIndex = 33
        Me.txtNewCode.WordWrap = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 221)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 15)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "代码："
        '
        'txtNewRemark
        '
        Me.txtNewRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewRemark.Location = New System.Drawing.Point(64, 95)
        Me.txtNewRemark.MaxLength = 200
        Me.txtNewRemark.Name = "txtNewRemark"
        Me.txtNewRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNewRemark.Size = New System.Drawing.Size(636, 25)
        Me.txtNewRemark.TabIndex = 32
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "备注："
        '
        'txtNewTitle
        '
        Me.txtNewTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewTitle.Location = New System.Drawing.Point(64, 61)
        Me.txtNewTitle.MaxLength = 100
        Me.txtNewTitle.Name = "txtNewTitle"
        Me.txtNewTitle.Size = New System.Drawing.Size(636, 25)
        Me.txtNewTitle.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "标题:"
        '
        'cmbFilterCategory
        '
        Me.cmbFilterCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilterCategory.FormattingEnabled = True
        Me.cmbFilterCategory.Location = New System.Drawing.Point(11, 15)
        Me.cmbFilterCategory.Name = "cmbFilterCategory"
        Me.cmbFilterCategory.Size = New System.Drawing.Size(155, 23)
        Me.cmbFilterCategory.TabIndex = 19
        '
        'btnInsertCode
        '
        Me.btnInsertCode.Location = New System.Drawing.Point(911, 635)
        Me.btnInsertCode.Name = "btnInsertCode"
        Me.btnInsertCode.Size = New System.Drawing.Size(75, 35)
        Me.btnInsertCode.TabIndex = 20
        Me.btnInsertCode.Text = "插入代码"
        Me.btnInsertCode.UseVisualStyleBackColor = True
        '
        'WIN190810_VBA代码笔记
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1725, 781)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnInsertCode)
        Me.Controls.Add(Me.cmbFilterCategory)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnEditNote)
        Me.Controls.Add(Me.btnDeleteNote)
        Me.Controls.Add(Me.btnCopyCode)
        Me.Controls.Add(Me.txtCodeDetail)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "WIN190810_VBA代码笔记"
        Me.Text = "WIN190810_VBA代码笔记"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBox1 As Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents btnExit As Windows.Forms.Button
    Friend WithEvents ListView1 As Windows.Forms.ListView
    Friend WithEvents btnSearch As Windows.Forms.Button
    Friend WithEvents txtCodeDetail As Windows.Forms.TextBox
    Friend WithEvents btnCopyCode As Windows.Forms.Button
    Friend WithEvents btnDeleteNote As Windows.Forms.Button
    Friend WithEvents btnEditNote As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnAddNote As Windows.Forms.Button
    Friend WithEvents txtNewCode As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents txtNewRemark As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtNewTitle As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents cmbCategory As Windows.Forms.ComboBox
    Friend WithEvents cmbFilterCategory As Windows.Forms.ComboBox
    Friend WithEvents btnInsertCode As Windows.Forms.Button
End Class
