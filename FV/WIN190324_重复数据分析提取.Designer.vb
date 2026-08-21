<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN190324_重复数据分析提取
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
        Me.btnSelectFstArea = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.txtFstArea = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnSecArea = New System.Windows.Forms.Button()
        Me.txtSecArea = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSelectFstArea
        '
        Me.btnSelectFstArea.Location = New System.Drawing.Point(178, 9)
        Me.btnSelectFstArea.Name = "btnSelectFstArea"
        Me.btnSelectFstArea.Size = New System.Drawing.Size(43, 23)
        Me.btnSelectFstArea.TabIndex = 0
        Me.btnSelectFstArea.Text = "---"
        Me.btnSelectFstArea.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(12, 86)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(145, 268)
        Me.ListBox1.TabIndex = 1
        '
        'txtFstArea
        '
        Me.txtFstArea.Location = New System.Drawing.Point(58, 9)
        Me.txtFstArea.Name = "txtFstArea"
        Me.txtFstArea.Size = New System.Drawing.Size(100, 21)
        Me.txtFstArea.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "区域一"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(564, 9)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "提取"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(32, 373)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "导入"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnSecArea
        '
        Me.btnSecArea.Location = New System.Drawing.Point(430, 7)
        Me.btnSecArea.Name = "btnSecArea"
        Me.btnSecArea.Size = New System.Drawing.Size(43, 23)
        Me.btnSecArea.TabIndex = 0
        Me.btnSecArea.Text = "---"
        Me.btnSecArea.UseVisualStyleBackColor = True
        '
        'txtSecArea
        '
        Me.txtSecArea.Location = New System.Drawing.Point(310, 7)
        Me.txtSecArea.Name = "txtSecArea"
        Me.txtSecArea.Size = New System.Drawing.Size(100, 21)
        Me.txtSecArea.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(263, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "区域二"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "相同项"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(274, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "区域一有,区域二没有"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(520, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 12)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "区域二有,区域一没有"
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 12
        Me.ListBox2.Location = New System.Drawing.Point(276, 86)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(145, 268)
        Me.ListBox2.TabIndex = 1
        '
        'ListBox3
        '
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 12
        Me.ListBox3.Location = New System.Drawing.Point(522, 86)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(145, 268)
        Me.ListBox3.TabIndex = 1
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(310, 373)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "导入"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(564, 373)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 0
        Me.Button6.Text = "导入"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'WIN190324_重复数据分析提取
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 418)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSecArea)
        Me.Controls.Add(Me.txtFstArea)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnSecArea)
        Me.Controls.Add(Me.btnSelectFstArea)
        Me.Name = "WIN190324_重复数据分析提取"
        Me.Text = "WIN190324_重复数据分析提取"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSelectFstArea As Windows.Forms.Button
    Friend WithEvents ListBox1 As Windows.Forms.ListBox
    Friend WithEvents txtFstArea As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Button3 As Windows.Forms.Button
    Friend WithEvents btnSecArea As Windows.Forms.Button
    Friend WithEvents txtSecArea As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents ListBox2 As Windows.Forms.ListBox
    Friend WithEvents ListBox3 As Windows.Forms.ListBox
    Friend WithEvents Button4 As Windows.Forms.Button
    Friend WithEvents Button6 As Windows.Forms.Button
End Class
