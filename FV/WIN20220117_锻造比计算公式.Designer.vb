<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN20220117_锻造比计算公式
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
        Me.picbDisplayPicture = New System.Windows.Forms.PictureBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.冲孔中心内径 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.坯料内径 = New System.Windows.Forms.TextBox()
        Me.冲孔外径 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.岗位补贴 = New System.Windows.Forms.Label()
        Me.坯料外径 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.下料高度 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.墩粗高度 = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnCaulate = New System.Windows.Forms.Button()
        Me.锻造比 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.坯料高度 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.picbDisplayPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picbDisplayPicture
        '
        Me.picbDisplayPicture.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picbDisplayPicture.Location = New System.Drawing.Point(6, 12)
        Me.picbDisplayPicture.Name = "picbDisplayPicture"
        Me.picbDisplayPicture.Size = New System.Drawing.Size(988, 468)
        Me.picbDisplayPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picbDisplayPicture.TabIndex = 55
        Me.picbDisplayPicture.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.picbDisplayPicture)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(1000, 486)
        Me.GroupBox5.TabIndex = 58
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "图片"
        '
        '冲孔中心内径
        '
        Me.冲孔中心内径.Location = New System.Drawing.Point(653, 22)
        Me.冲孔中心内径.Name = "冲孔中心内径"
        Me.冲孔中心内径.Size = New System.Drawing.Size(100, 21)
        Me.冲孔中心内径.TabIndex = 4
        Me.冲孔中心内径.Text = "440"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(561, 26)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 12)
        Me.Label10.TabIndex = 78
        Me.Label10.Text = "冲孔中心内径"
        '
        '坯料内径
        '
        Me.坯料内径.Location = New System.Drawing.Point(263, 60)
        Me.坯料内径.Name = "坯料内径"
        Me.坯料内径.Size = New System.Drawing.Size(100, 21)
        Me.坯料内径.TabIndex = 6
        Me.坯料内径.Text = "3358"
        '
        '冲孔外径
        '
        Me.冲孔外径.Location = New System.Drawing.Point(446, 22)
        Me.冲孔外径.Name = "冲孔外径"
        Me.冲孔外径.Size = New System.Drawing.Size(100, 21)
        Me.冲孔外径.TabIndex = 3
        Me.冲孔外径.Text = "1528"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(195, 63)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 77
        Me.Label8.Text = "坯料内径"
        '
        '岗位补贴
        '
        Me.岗位补贴.AutoSize = True
        Me.岗位补贴.Location = New System.Drawing.Point(378, 26)
        Me.岗位补贴.Name = "岗位补贴"
        Me.岗位补贴.Size = New System.Drawing.Size(53, 12)
        Me.岗位补贴.TabIndex = 74
        Me.岗位补贴.Text = "冲孔外径"
        '
        '坯料外径
        '
        Me.坯料外径.Location = New System.Drawing.Point(79, 59)
        Me.坯料外径.Name = "坯料外径"
        Me.坯料外径.Size = New System.Drawing.Size(100, 21)
        Me.坯料外径.TabIndex = 5
        Me.坯料外径.Text = "3807"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 63)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "坯料外径"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 71
        Me.Label6.Text = "下料高度"
        '
        '下料高度
        '
        Me.下料高度.Location = New System.Drawing.Point(80, 22)
        Me.下料高度.Name = "下料高度"
        Me.下料高度.Size = New System.Drawing.Size(100, 21)
        Me.下料高度.TabIndex = 1
        Me.下料高度.Text = "1764"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(195, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "墩粗高度"
        '
        '墩粗高度
        '
        Me.墩粗高度.Location = New System.Drawing.Point(263, 22)
        Me.墩粗高度.Name = "墩粗高度"
        Me.墩粗高度.Size = New System.Drawing.Size(100, 21)
        Me.墩粗高度.TabIndex = 2
        Me.墩粗高度.Text = "380"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(859, 52)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnCaulate
        '
        Me.btnCaulate.Location = New System.Drawing.Point(859, 20)
        Me.btnCaulate.Name = "btnCaulate"
        Me.btnCaulate.Size = New System.Drawing.Size(75, 23)
        Me.btnCaulate.TabIndex = 8
        Me.btnCaulate.Text = "计算"
        Me.btnCaulate.UseVisualStyleBackColor = True
        '
        '锻造比
        '
        Me.锻造比.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.锻造比.Enabled = False
        Me.锻造比.Location = New System.Drawing.Point(653, 52)
        Me.锻造比.Name = "锻造比"
        Me.锻造比.Size = New System.Drawing.Size(100, 21)
        Me.锻造比.TabIndex = 82
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(585, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "锻造比"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.坯料高度)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.锻造比)
        Me.GroupBox1.Controls.Add(Me.btnCaulate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.墩粗高度)
        Me.GroupBox1.Controls.Add(Me.冲孔中心内径)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.下料高度)
        Me.GroupBox1.Controls.Add(Me.坯料内径)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.冲孔外径)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.坯料外径)
        Me.GroupBox1.Controls.Add(Me.岗位补贴)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 498)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(994, 88)
        Me.GroupBox1.TabIndex = 83
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "参数信息"
        '
        '坯料高度
        '
        Me.坯料高度.Location = New System.Drawing.Point(446, 60)
        Me.坯料高度.Name = "坯料高度"
        Me.坯料高度.Size = New System.Drawing.Size(100, 21)
        Me.坯料高度.TabIndex = 7
        Me.坯料高度.Text = "253"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(378, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 83
        Me.Label2.Text = "坯料高度"
        '
        'WIN20220117_锻造比计算公式
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 598)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "WIN20220117_锻造比计算公式"
        Me.Text = "WIN20220117_锻造比计算公式"
        CType(Me.picbDisplayPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picbDisplayPicture As Windows.Forms.PictureBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents 冲孔中心内径 As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents 坯料内径 As Windows.Forms.TextBox
    Friend WithEvents 冲孔外径 As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents 岗位补贴 As Windows.Forms.Label
    Friend WithEvents 坯料外径 As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents 下料高度 As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents 墩粗高度 As Windows.Forms.TextBox
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents btnCaulate As Windows.Forms.Button
    Friend WithEvents 锻造比 As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents 坯料高度 As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
End Class
