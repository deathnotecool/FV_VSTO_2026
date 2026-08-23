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
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtCodeDetail = New System.Windows.Forms.TextBox()
        Me.btnCopyCode = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNewTitle = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNewRemark = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNewCode = New System.Windows.Forms.TextBox()
        Me.btnAddNote = New System.Windows.Forms.Button()
        Me.btnDeleteNote = New System.Windows.Forms.Button()
        Me.btnEditNote = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(16, 15)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(160, 23)
        Me.ComboBox1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(185, 15)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(240, 25)
        Me.TextBox1.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(543, 15)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 29)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "退出"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(13, 51)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(923, 717)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(435, 15)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 29)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "查询"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(874, 9)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 29)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "格式转换"
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.Visible = False
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
        Me.btnCopyCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopyCode.Location = New System.Drawing.Point(1001, 13)
        Me.btnCopyCode.Name = "btnCopyCode"
        Me.btnCopyCode.Size = New System.Drawing.Size(100, 34)
        Me.btnCopyCode.TabIndex = 8
        Me.btnCopyCode.Text = "复制代码"
        Me.btnCopyCode.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(950, 480)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 15)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "标题:"
        '
        'txtNewTitle
        '
        Me.txtNewTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewTitle.Location = New System.Drawing.Point(1001, 470)
        Me.txtNewTitle.MaxLength = 100
        Me.txtNewTitle.Name = "txtNewTitle"
        Me.txtNewTitle.Size = New System.Drawing.Size(712, 25)
        Me.txtNewTitle.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(943, 516)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "备注："
        '
        'txtNewRemark
        '
        Me.txtNewRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewRemark.Location = New System.Drawing.Point(1001, 506)
        Me.txtNewRemark.MaxLength = 200
        Me.txtNewRemark.Name = "txtNewRemark"
        Me.txtNewRemark.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNewRemark.Size = New System.Drawing.Size(712, 25)
        Me.txtNewRemark.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(943, 663)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 15)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "代码："
        '
        'txtNewCode
        '
        Me.txtNewCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewCode.Location = New System.Drawing.Point(1001, 542)
        Me.txtNewCode.Multiline = True
        Me.txtNewCode.Name = "txtNewCode"
        Me.txtNewCode.Size = New System.Drawing.Size(712, 225)
        Me.txtNewCode.TabIndex = 14
        Me.txtNewCode.WordWrap = False
        '
        'btnAddNote
        '
        Me.btnAddNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNote.Location = New System.Drawing.Point(1001, 430)
        Me.btnAddNote.Name = "btnAddNote"
        Me.btnAddNote.Size = New System.Drawing.Size(100, 34)
        Me.btnAddNote.TabIndex = 15
        Me.btnAddNote.Text = "保存笔记"
        Me.btnAddNote.UseVisualStyleBackColor = True
        '
        'btnDeleteNote
        '
        Me.btnDeleteNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteNote.Location = New System.Drawing.Point(1107, 11)
        Me.btnDeleteNote.Name = "btnDeleteNote"
        Me.btnDeleteNote.Size = New System.Drawing.Size(100, 34)
        Me.btnDeleteNote.TabIndex = 16
        Me.btnDeleteNote.Text = "删除笔记"
        Me.btnDeleteNote.UseVisualStyleBackColor = True
        '
        'btnEditNote
        '
        Me.btnEditNote.Location = New System.Drawing.Point(731, 8)
        Me.btnEditNote.Name = "btnEditNote"
        Me.btnEditNote.Size = New System.Drawing.Size(100, 34)
        Me.btnEditNote.TabIndex = 17
        Me.btnEditNote.Text = "编辑笔记"
        Me.btnEditNote.UseVisualStyleBackColor = True
        '
        'WIN190810_VBA代码笔记
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1725, 781)
        Me.Controls.Add(Me.btnEditNote)
        Me.Controls.Add(Me.btnDeleteNote)
        Me.Controls.Add(Me.btnAddNote)
        Me.Controls.Add(Me.txtNewCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtNewRemark)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNewTitle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCopyCode)
        Me.Controls.Add(Me.txtCodeDetail)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "WIN190810_VBA代码笔记"
        Me.Text = "WIN190810_VBA代码笔记"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBox1 As Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents btnExit As Windows.Forms.Button
    Friend WithEvents ListView1 As Windows.Forms.ListView
    Friend WithEvents btnSearch As Windows.Forms.Button
    Friend WithEvents btnSave As Windows.Forms.Button
    Friend WithEvents txtCodeDetail As Windows.Forms.TextBox
    Friend WithEvents btnCopyCode As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtNewTitle As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtNewRemark As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents txtNewCode As Windows.Forms.TextBox
    Friend WithEvents btnAddNote As Windows.Forms.Button
    Friend WithEvents btnDeleteNote As Windows.Forms.Button
    Friend WithEvents btnEditNote As Windows.Forms.Button
End Class
