<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WN18081503_文件批量重命名
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WN18081503_文件批量重命名))
        Me.lstDisplayFullName = New System.Windows.Forms.ListBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.optReplaceApointString = New System.Windows.Forms.RadioButton()
        Me.optInsertPostName = New System.Windows.Forms.RadioButton()
        Me.optInsertPreName = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblOriginString = New System.Windows.Forms.Label()
        Me.txtNewString = New System.Windows.Forms.TextBox()
        Me.txtOriginString = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstDisplayFullName
        '
        Me.lstDisplayFullName.FormattingEnabled = True
        Me.lstDisplayFullName.ItemHeight = 12
        Me.lstDisplayFullName.Location = New System.Drawing.Point(31, 63)
        Me.lstDisplayFullName.Name = "lstDisplayFullName"
        Me.lstDisplayFullName.Size = New System.Drawing.Size(250, 196)
        Me.lstDisplayFullName.TabIndex = 14
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(395, 12)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 11
        Me.btnBrowse.Text = "浏览..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(31, 12)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(331, 21)
        Me.txtFilePath.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "路径"
        '
        'optReplaceApointString
        '
        Me.optReplaceApointString.AutoSize = True
        Me.optReplaceApointString.Checked = True
        Me.optReplaceApointString.Location = New System.Drawing.Point(17, 57)
        Me.optReplaceApointString.Name = "optReplaceApointString"
        Me.optReplaceApointString.Size = New System.Drawing.Size(95, 16)
        Me.optReplaceApointString.TabIndex = 7
        Me.optReplaceApointString.TabStop = True
        Me.optReplaceApointString.Text = "替换指定字符"
        Me.optReplaceApointString.UseVisualStyleBackColor = True
        '
        'optInsertPostName
        '
        Me.optInsertPostName.AutoSize = True
        Me.optInsertPostName.Location = New System.Drawing.Point(17, 35)
        Me.optInsertPostName.Name = "optInsertPostName"
        Me.optInsertPostName.Size = New System.Drawing.Size(119, 16)
        Me.optInsertPostName.TabIndex = 6
        Me.optInsertPostName.Text = "插入到原名称之后"
        Me.optInsertPostName.UseVisualStyleBackColor = True
        '
        'optInsertPreName
        '
        Me.optInsertPreName.AutoSize = True
        Me.optInsertPreName.Location = New System.Drawing.Point(17, 13)
        Me.optInsertPreName.Name = "optInsertPreName"
        Me.optInsertPreName.Size = New System.Drawing.Size(119, 16)
        Me.optInsertPreName.TabIndex = 5
        Me.optInsertPreName.Text = "插入到原名称之前"
        Me.optInsertPreName.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblOriginString)
        Me.GroupBox1.Controls.Add(Me.optReplaceApointString)
        Me.GroupBox1.Controls.Add(Me.txtNewString)
        Me.GroupBox1.Controls.Add(Me.txtOriginString)
        Me.GroupBox1.Controls.Add(Me.optInsertPostName)
        Me.GroupBox1.Controls.Add(Me.optInsertPreName)
        Me.GroupBox1.Location = New System.Drawing.Point(287, 63)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(183, 157)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "命名方式"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "新字符"
        '
        'lblOriginString
        '
        Me.lblOriginString.AutoSize = True
        Me.lblOriginString.Location = New System.Drawing.Point(17, 89)
        Me.lblOriginString.Name = "lblOriginString"
        Me.lblOriginString.Size = New System.Drawing.Size(41, 12)
        Me.lblOriginString.TabIndex = 8
        Me.lblOriginString.Text = "原字符"
        '
        'txtNewString
        '
        Me.txtNewString.Location = New System.Drawing.Point(64, 113)
        Me.txtNewString.Name = "txtNewString"
        Me.txtNewString.Size = New System.Drawing.Size(100, 21)
        Me.txtNewString.TabIndex = 1
        '
        'txtOriginString
        '
        Me.txtOriginString.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOriginString.Location = New System.Drawing.Point(64, 86)
        Me.txtOriginString.Name = "txtOriginString"
        Me.txtOriginString.Size = New System.Drawing.Size(100, 21)
        Me.txtOriginString.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(287, 233)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 12
        Me.btnOK.Text = "确定"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(395, 233)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "取消"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sslStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 273)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(484, 22)
        Me.StatusStrip1.TabIndex = 17
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'sslStatus
        '
        Me.sslStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.sslStatus.Name = "sslStatus"
        Me.sslStatus.Size = New System.Drawing.Size(44, 17)
        Me.sslStatus.Text = "Ready"
        '
        'WN18081503_文件批量重命名
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 295)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lstDisplayFullName)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WN18081503_文件批量重命名"
        Me.Text = "批量重命名"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lstDisplayFullName As Windows.Forms.ListBox
    Friend WithEvents btnBrowse As Windows.Forms.Button
    Friend WithEvents txtFilePath As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents optReplaceApointString As Windows.Forms.RadioButton
    Friend WithEvents optInsertPostName As Windows.Forms.RadioButton
    Friend WithEvents optInsertPreName As Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents lblOriginString As Windows.Forms.Label
    Friend WithEvents txtNewString As Windows.Forms.TextBox
    Friend WithEvents txtOriginString As Windows.Forms.TextBox
    Friend WithEvents btnOK As Windows.Forms.Button
    Friend WithEvents btnCancel As Windows.Forms.Button
    Friend WithEvents StatusStrip1 As Windows.Forms.StatusStrip
    Friend WithEvents sslStatus As Windows.Forms.ToolStripStatusLabel
End Class
