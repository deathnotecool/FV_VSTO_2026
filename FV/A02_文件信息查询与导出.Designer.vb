<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class A02_文件信息查询与导出
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(A02_文件信息查询与导出))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label_Value2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label_and = New System.Windows.Forms.Label()
        Me.信息种类 = New System.Windows.Forms.ComboBox()
        Me.查询项目 = New System.Windows.Forms.ComboBox()
        Me.条件值1 = New System.Windows.Forms.ComboBox()
        Me.运算符 = New System.Windows.Forms.ComboBox()
        Me.条件值2 = New System.Windows.Forms.ComboBox()
        Me.重设条件 = New System.Windows.Forms.Button()
        Me.开始查询 = New System.Windows.Forms.Button()
        Me.数据导出 = New System.Windows.Forms.Button()
        Me.关闭窗体 = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择要查询的信息种类"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "查询项目"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(177, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "运算符"
        '
        'Label_Value2
        '
        Me.Label_Value2.AutoSize = True
        Me.Label_Value2.Location = New System.Drawing.Point(563, 38)
        Me.Label_Value2.Name = "Label_Value2"
        Me.Label_Value2.Size = New System.Drawing.Size(47, 12)
        Me.Label_Value2.TabIndex = 0
        Me.Label_Value2.Text = "条件值2"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(331, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 12)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "条件值1"
        '
        'Label_and
        '
        Me.Label_and.AutoSize = True
        Me.Label_and.Location = New System.Drawing.Point(516, 59)
        Me.Label_and.Name = "Label_and"
        Me.Label_and.Size = New System.Drawing.Size(23, 12)
        Me.Label_and.TabIndex = 0
        Me.Label_and.Text = "and"
        '
        '信息种类
        '
        Me.信息种类.FormattingEnabled = True
        Me.信息种类.Location = New System.Drawing.Point(179, 6)
        Me.信息种类.Name = "信息种类"
        Me.信息种类.Size = New System.Drawing.Size(121, 20)
        Me.信息种类.TabIndex = 1
        '
        '查询项目
        '
        Me.查询项目.FormattingEnabled = True
        Me.查询项目.Location = New System.Drawing.Point(14, 56)
        Me.查询项目.Name = "查询项目"
        Me.查询项目.Size = New System.Drawing.Size(121, 20)
        Me.查询项目.TabIndex = 1
        '
        '条件值1
        '
        Me.条件值1.FormattingEnabled = True
        Me.条件值1.Location = New System.Drawing.Point(333, 56)
        Me.条件值1.Name = "条件值1"
        Me.条件值1.Size = New System.Drawing.Size(157, 20)
        Me.条件值1.TabIndex = 1
        '
        '运算符
        '
        Me.运算符.FormattingEnabled = True
        Me.运算符.Location = New System.Drawing.Point(179, 56)
        Me.运算符.Name = "运算符"
        Me.运算符.Size = New System.Drawing.Size(121, 20)
        Me.运算符.TabIndex = 1
        '
        '条件值2
        '
        Me.条件值2.FormattingEnabled = True
        Me.条件值2.Location = New System.Drawing.Point(565, 56)
        Me.条件值2.Name = "条件值2"
        Me.条件值2.Size = New System.Drawing.Size(157, 20)
        Me.条件值2.TabIndex = 1
        '
        '重设条件
        '
        Me.重设条件.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.重设条件.Location = New System.Drawing.Point(647, 85)
        Me.重设条件.Name = "重设条件"
        Me.重设条件.Size = New System.Drawing.Size(75, 23)
        Me.重设条件.TabIndex = 2
        Me.重设条件.Text = "重设条件"
        Me.重设条件.UseVisualStyleBackColor = True
        '
        '开始查询
        '
        Me.开始查询.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.开始查询.Location = New System.Drawing.Point(647, 129)
        Me.开始查询.Name = "开始查询"
        Me.开始查询.Size = New System.Drawing.Size(75, 23)
        Me.开始查询.TabIndex = 2
        Me.开始查询.Text = "开始查询"
        Me.开始查询.UseVisualStyleBackColor = True
        '
        '数据导出
        '
        Me.数据导出.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.数据导出.Location = New System.Drawing.Point(647, 173)
        Me.数据导出.Name = "数据导出"
        Me.数据导出.Size = New System.Drawing.Size(75, 23)
        Me.数据导出.TabIndex = 2
        Me.数据导出.Text = "数据导出"
        Me.数据导出.UseVisualStyleBackColor = True
        '
        '关闭窗体
        '
        Me.关闭窗体.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.关闭窗体.Location = New System.Drawing.Point(647, 217)
        Me.关闭窗体.Name = "关闭窗体"
        Me.关闭窗体.Size = New System.Drawing.Size(75, 23)
        Me.关闭窗体.TabIndex = 2
        Me.关闭窗体.Text = "关闭窗体"
        Me.关闭窗体.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Location = New System.Drawing.Point(14, 85)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(616, 242)
        Me.ListView1.TabIndex = 3
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'A02_文件信息查询与导出
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(736, 339)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.关闭窗体)
        Me.Controls.Add(Me.数据导出)
        Me.Controls.Add(Me.开始查询)
        Me.Controls.Add(Me.重设条件)
        Me.Controls.Add(Me.条件值2)
        Me.Controls.Add(Me.条件值1)
        Me.Controls.Add(Me.运算符)
        Me.Controls.Add(Me.查询项目)
        Me.Controls.Add(Me.信息种类)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label_and)
        Me.Controls.Add(Me.Label_Value2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "A02_文件信息查询与导出"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "文件信息查询与导出"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label_Value2 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label_and As Windows.Forms.Label
    Friend WithEvents 信息种类 As Windows.Forms.ComboBox
    Friend WithEvents 查询项目 As Windows.Forms.ComboBox
    Friend WithEvents 条件值1 As Windows.Forms.ComboBox
    Friend WithEvents 运算符 As Windows.Forms.ComboBox
    Friend WithEvents 条件值2 As Windows.Forms.ComboBox
    Friend WithEvents 重设条件 As Windows.Forms.Button
    Friend WithEvents 开始查询 As Windows.Forms.Button
    Friend WithEvents 数据导出 As Windows.Forms.Button
    Friend WithEvents 关闭窗体 As Windows.Forms.Button
    Friend WithEvents ListView1 As Windows.Forms.ListView
End Class
