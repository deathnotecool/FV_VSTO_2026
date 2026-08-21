<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN2020119_加班费计算
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN2020119_加班费计算))
        Me.btnCaulate = New System.Windows.Forms.Button()
        Me.txtTotalBasic = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDays = New System.Windows.Forms.TextBox()
        Me.txtTotalAddMoney = New System.Windows.Forms.TextBox()
        Me.txtMoreHalf = New System.Windows.Forms.TextBox()
        Me.txtMore2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMore3 = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.strAccumulationFund = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSocialSecurity = New System.Windows.Forms.TextBox()
        Me.岗位补贴 = New System.Windows.Forms.Label()
        Me.txt岗位补贴 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt特殊津贴 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnCaulate
        '
        Me.btnCaulate.Location = New System.Drawing.Point(46, 243)
        Me.btnCaulate.Name = "btnCaulate"
        Me.btnCaulate.Size = New System.Drawing.Size(75, 23)
        Me.btnCaulate.TabIndex = 7
        Me.btnCaulate.Text = "计算"
        Me.btnCaulate.UseVisualStyleBackColor = True
        '
        'txtTotalBasic
        '
        Me.txtTotalBasic.Location = New System.Drawing.Point(104, 10)
        Me.txtTotalBasic.Name = "txtTotalBasic"
        Me.txtTotalBasic.Size = New System.Drawing.Size(100, 21)
        Me.txtTotalBasic.TabIndex = 1
        Me.txtTotalBasic.Text = "3210"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "基本工资"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(218, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "计薪天数"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(218, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "加班费总计"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(218, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 12)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "2倍时间"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 12)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "1.5倍时间"
        '
        'txtDays
        '
        Me.txtDays.Location = New System.Drawing.Point(297, 10)
        Me.txtDays.Name = "txtDays"
        Me.txtDays.Size = New System.Drawing.Size(100, 21)
        Me.txtDays.TabIndex = 2
        Me.txtDays.Text = "21.75"
        '
        'txtTotalAddMoney
        '
        Me.txtTotalAddMoney.Enabled = False
        Me.txtTotalAddMoney.Location = New System.Drawing.Point(297, 92)
        Me.txtTotalAddMoney.Name = "txtTotalAddMoney"
        Me.txtTotalAddMoney.Size = New System.Drawing.Size(100, 21)
        Me.txtTotalAddMoney.TabIndex = 6
        '
        'txtMoreHalf
        '
        Me.txtMoreHalf.Location = New System.Drawing.Point(104, 51)
        Me.txtMoreHalf.Name = "txtMoreHalf"
        Me.txtMoreHalf.Size = New System.Drawing.Size(100, 21)
        Me.txtMoreHalf.TabIndex = 3
        '
        'txtMore2
        '
        Me.txtMore2.Location = New System.Drawing.Point(297, 51)
        Me.txtMore2.Name = "txtMore2"
        Me.txtMore2.Size = New System.Drawing.Size(100, 21)
        Me.txtMore2.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 12)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "3倍时间"
        '
        'txtMore3
        '
        Me.txtMore3.Location = New System.Drawing.Point(104, 92)
        Me.txtMore3.Name = "txtMore3"
        Me.txtMore3.Size = New System.Drawing.Size(100, 21)
        Me.txtMore3.TabIndex = 5
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(297, 231)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "关闭"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 187)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 12)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "公积金代缴"
        '
        'strAccumulationFund
        '
        Me.strAccumulationFund.Location = New System.Drawing.Point(104, 184)
        Me.strAccumulationFund.Name = "strAccumulationFund"
        Me.strAccumulationFund.Size = New System.Drawing.Size(100, 21)
        Me.strAccumulationFund.TabIndex = 10
        Me.strAccumulationFund.Text = "-321"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(220, 186)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "社保"
        '
        'txtSocialSecurity
        '
        Me.txtSocialSecurity.Location = New System.Drawing.Point(297, 187)
        Me.txtSocialSecurity.Name = "txtSocialSecurity"
        Me.txtSocialSecurity.Size = New System.Drawing.Size(100, 21)
        Me.txtSocialSecurity.TabIndex = 12
        Me.txtSocialSecurity.Text = "-729.75"
        '
        '岗位补贴
        '
        Me.岗位补贴.AutoSize = True
        Me.岗位补贴.Location = New System.Drawing.Point(25, 139)
        Me.岗位补贴.Name = "岗位补贴"
        Me.岗位补贴.Size = New System.Drawing.Size(53, 12)
        Me.岗位补贴.TabIndex = 9
        Me.岗位补贴.Text = "岗位补贴"
        '
        'txt岗位补贴
        '
        Me.txt岗位补贴.Location = New System.Drawing.Point(104, 136)
        Me.txt岗位补贴.Name = "txt岗位补贴"
        Me.txt岗位补贴.Size = New System.Drawing.Size(100, 21)
        Me.txt岗位补贴.TabIndex = 10
        Me.txt岗位补贴.Text = "2430"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(220, 138)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "特殊津贴"
        '
        'txt特殊津贴
        '
        Me.txt特殊津贴.Location = New System.Drawing.Point(297, 139)
        Me.txt特殊津贴.Name = "txt特殊津贴"
        Me.txt特殊津贴.Size = New System.Drawing.Size(100, 21)
        Me.txt特殊津贴.TabIndex = 12
        Me.txt特殊津贴.Text = "2000"
        '
        'WIN2020119_加班费计算
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 298)
        Me.Controls.Add(Me.txt特殊津贴)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtSocialSecurity)
        Me.Controls.Add(Me.txt岗位补贴)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.岗位补贴)
        Me.Controls.Add(Me.strAccumulationFund)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtMore3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMore2)
        Me.Controls.Add(Me.txtMoreHalf)
        Me.Controls.Add(Me.txtTotalAddMoney)
        Me.Controls.Add(Me.txtDays)
        Me.Controls.Add(Me.txtTotalBasic)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnCaulate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WIN2020119_加班费计算"
        Me.Text = "WIN2020119_加班费计算"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCaulate As Windows.Forms.Button
    Friend WithEvents txtTotalBasic As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtDays As Windows.Forms.TextBox
    Friend WithEvents txtTotalAddMoney As Windows.Forms.TextBox
    Friend WithEvents txtMoreHalf As Windows.Forms.TextBox
    Friend WithEvents txtMore2 As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents txtMore3 As Windows.Forms.TextBox
    Friend WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents strAccumulationFund As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents txtSocialSecurity As Windows.Forms.TextBox
    Friend WithEvents 岗位补贴 As Windows.Forms.Label
    Friend WithEvents txt岗位补贴 As Windows.Forms.TextBox
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents txt特殊津贴 As Windows.Forms.TextBox
End Class
