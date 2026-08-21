<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WN18081501_列和区域删除图
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WN18081501_列和区域删除图))
        Me.cboDisplayAdress = New System.Windows.Forms.ComboBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.optDeleteColumnPicture = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optApointAreaDeletePicture = New System.Windows.Forms.RadioButton()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboDisplayAdress
        '
        Me.cboDisplayAdress.FormattingEnabled = True
        Me.cboDisplayAdress.Location = New System.Drawing.Point(39, 23)
        Me.cboDisplayAdress.Name = "cboDisplayAdress"
        Me.cboDisplayAdress.Size = New System.Drawing.Size(124, 20)
        Me.cboDisplayAdress.TabIndex = 11
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(169, 72)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 9
        Me.btnOk.Text = "&OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(179, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "列  删除图片"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "在"
        '
        'optDeleteColumnPicture
        '
        Me.optDeleteColumnPicture.AutoSize = True
        Me.optDeleteColumnPicture.Checked = True
        Me.optDeleteColumnPicture.Location = New System.Drawing.Point(8, 30)
        Me.optDeleteColumnPicture.Name = "optDeleteColumnPicture"
        Me.optDeleteColumnPicture.Size = New System.Drawing.Size(107, 16)
        Me.optDeleteColumnPicture.TabIndex = 0
        Me.optDeleteColumnPicture.TabStop = True
        Me.optDeleteColumnPicture.Text = "删除指定列图片"
        Me.optDeleteColumnPicture.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optApointAreaDeletePicture)
        Me.GroupBox1.Controls.Add(Me.optDeleteColumnPicture)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(149, 98)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'optApointAreaDeletePicture
        '
        Me.optApointAreaDeletePicture.AutoSize = True
        Me.optApointAreaDeletePicture.Location = New System.Drawing.Point(8, 52)
        Me.optApointAreaDeletePicture.Name = "optApointAreaDeletePicture"
        Me.optApointAreaDeletePicture.Size = New System.Drawing.Size(119, 16)
        Me.optApointAreaDeletePicture.TabIndex = 1
        Me.optApointAreaDeletePicture.Text = "删除指定区域图片"
        Me.optApointAreaDeletePicture.UseVisualStyleBackColor = True
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(169, 118)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 10
        Me.btnQuit.Text = "&Quit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'WN18081501_列和区域删除图
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(265, 167)
        Me.Controls.Add(Me.cboDisplayAdress)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnQuit)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WN18081501_列和区域删除图"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "批量删除图片"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboDisplayAdress As Windows.Forms.ComboBox
    Friend WithEvents btnOk As Windows.Forms.Button
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents optDeleteColumnPicture As Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents optApointAreaDeletePicture As Windows.Forms.RadioButton
    Friend WithEvents btnQuit As Windows.Forms.Button
End Class
