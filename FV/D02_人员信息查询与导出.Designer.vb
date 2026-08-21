<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class D02_人员信息查询与导出
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.grdAuthorTitles1th = New System.Windows.Forms.DataGridView()
        Me.关闭窗体 = New System.Windows.Forms.Button()
        Me.数据导出 = New System.Windows.Forms.Button()
        Me.开始查询 = New System.Windows.Forms.Button()
        Me.重设条件 = New System.Windows.Forms.Button()
        Me.条件值2 = New System.Windows.Forms.ComboBox()
        Me.条件值1 = New System.Windows.Forms.ComboBox()
        Me.运算符 = New System.Windows.Forms.ComboBox()
        Me.查询项目 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label_and = New System.Windows.Forms.Label()
        Me.Label_Value2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.信息种类 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdAuthorTitles1th, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.grdAuthorTitles1th)
        Me.GroupBox3.Location = New System.Drawing.Point(60, 95)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(637, 249)
        Me.GroupBox3.TabIndex = 71
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "信息记录框"
        '
        'grdAuthorTitles1th
        '
        Me.grdAuthorTitles1th.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles1th.Location = New System.Drawing.Point(5, 15)
        Me.grdAuthorTitles1th.Name = "grdAuthorTitles1th"
        Me.grdAuthorTitles1th.RowTemplate.Height = 23
        Me.grdAuthorTitles1th.Size = New System.Drawing.Size(626, 228)
        Me.grdAuthorTitles1th.TabIndex = 33
        '
        '关闭窗体
        '
        Me.关闭窗体.Location = New System.Drawing.Point(722, 318)
        Me.关闭窗体.Name = "关闭窗体"
        Me.关闭窗体.Size = New System.Drawing.Size(75, 23)
        Me.关闭窗体.TabIndex = 67
        Me.关闭窗体.Text = "关闭窗体"
        Me.关闭窗体.UseVisualStyleBackColor = True
        '
        '数据导出
        '
        Me.数据导出.Location = New System.Drawing.Point(722, 247)
        Me.数据导出.Name = "数据导出"
        Me.数据导出.Size = New System.Drawing.Size(75, 23)
        Me.数据导出.TabIndex = 68
        Me.数据导出.Text = "数据导出"
        Me.数据导出.UseVisualStyleBackColor = True
        '
        '开始查询
        '
        Me.开始查询.Location = New System.Drawing.Point(722, 176)
        Me.开始查询.Name = "开始查询"
        Me.开始查询.Size = New System.Drawing.Size(75, 23)
        Me.开始查询.TabIndex = 69
        Me.开始查询.Text = "开始查询"
        Me.开始查询.UseVisualStyleBackColor = True
        '
        '重设条件
        '
        Me.重设条件.Location = New System.Drawing.Point(722, 105)
        Me.重设条件.Name = "重设条件"
        Me.重设条件.Size = New System.Drawing.Size(75, 23)
        Me.重设条件.TabIndex = 70
        Me.重设条件.Text = "重设条件"
        Me.重设条件.UseVisualStyleBackColor = True
        '
        '条件值2
        '
        Me.条件值2.FormattingEnabled = True
        Me.条件值2.Location = New System.Drawing.Point(576, 68)
        Me.条件值2.Name = "条件值2"
        Me.条件值2.Size = New System.Drawing.Size(121, 20)
        Me.条件值2.TabIndex = 63
        '
        '条件值1
        '
        Me.条件值1.FormattingEnabled = True
        Me.条件值1.Location = New System.Drawing.Point(411, 68)
        Me.条件值1.Name = "条件值1"
        Me.条件值1.Size = New System.Drawing.Size(121, 20)
        Me.条件值1.TabIndex = 64
        '
        '运算符
        '
        Me.运算符.FormattingEnabled = True
        Me.运算符.Location = New System.Drawing.Point(225, 68)
        Me.运算符.Name = "运算符"
        Me.运算符.Size = New System.Drawing.Size(121, 20)
        Me.运算符.TabIndex = 65
        '
        '查询项目
        '
        Me.查询项目.FormattingEnabled = True
        Me.查询项目.Location = New System.Drawing.Point(59, 68)
        Me.查询项目.Name = "查询项目"
        Me.查询项目.Size = New System.Drawing.Size(121, 20)
        Me.查询项目.TabIndex = 66
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(409, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 12)
        Me.Label5.TabIndex = 58
        Me.Label5.Text = "条件值1"
        '
        'Label_and
        '
        Me.Label_and.AutoSize = True
        Me.Label_and.Location = New System.Drawing.Point(541, 71)
        Me.Label_and.Name = "Label_and"
        Me.Label_and.Size = New System.Drawing.Size(23, 12)
        Me.Label_and.TabIndex = 59
        Me.Label_and.Text = "and"
        '
        'Label_Value2
        '
        Me.Label_Value2.AutoSize = True
        Me.Label_Value2.Location = New System.Drawing.Point(574, 50)
        Me.Label_Value2.Name = "Label_Value2"
        Me.Label_Value2.Size = New System.Drawing.Size(47, 12)
        Me.Label_Value2.TabIndex = 60
        Me.Label_Value2.Text = "条件值2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(223, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "运算符"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(58, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "查询项目"
        '
        '信息种类
        '
        Me.信息种类.FormattingEnabled = True
        Me.信息种类.Location = New System.Drawing.Point(225, 12)
        Me.信息种类.Name = "信息种类"
        Me.信息种类.Size = New System.Drawing.Size(121, 20)
        Me.信息种类.TabIndex = 57
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(57, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 12)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "选择要查询的信息种类"
        '
        'D02_人员信息查询与导出
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(855, 357)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.关闭窗体)
        Me.Controls.Add(Me.数据导出)
        Me.Controls.Add(Me.开始查询)
        Me.Controls.Add(Me.重设条件)
        Me.Controls.Add(Me.条件值2)
        Me.Controls.Add(Me.条件值1)
        Me.Controls.Add(Me.运算符)
        Me.Controls.Add(Me.查询项目)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label_and)
        Me.Controls.Add(Me.Label_Value2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.信息种类)
        Me.Controls.Add(Me.Label1)
        Me.Name = "D02_人员信息查询与导出"
        Me.Text = "D02_人员信息查询与导出"
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.grdAuthorTitles1th, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents grdAuthorTitles1th As Windows.Forms.DataGridView
    Friend WithEvents 关闭窗体 As Windows.Forms.Button
    Friend WithEvents 数据导出 As Windows.Forms.Button
    Friend WithEvents 开始查询 As Windows.Forms.Button
    Friend WithEvents 重设条件 As Windows.Forms.Button
    Friend WithEvents 条件值2 As Windows.Forms.ComboBox
    Friend WithEvents 条件值1 As Windows.Forms.ComboBox
    Friend WithEvents 运算符 As Windows.Forms.ComboBox
    Friend WithEvents 查询项目 As Windows.Forms.ComboBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label_and As Windows.Forms.Label
    Friend WithEvents Label_Value2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents 信息种类 As Windows.Forms.ComboBox
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
