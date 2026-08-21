<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN210913_图片放置定位
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN210913_图片放置定位))
        Me.txtCellAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPosition = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSelectAreaPic = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSelectCell = New System.Windows.Forms.Button()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCellAddress
        '
        Me.txtCellAddress.Location = New System.Drawing.Point(113, 26)
        Me.txtCellAddress.Name = "txtCellAddress"
        Me.txtCellAddress.Size = New System.Drawing.Size(146, 21)
        Me.txtCellAddress.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "图片移动至单元格"
        '
        'btnPosition
        '
        Me.btnPosition.Location = New System.Drawing.Point(265, 24)
        Me.btnPosition.Name = "btnPosition"
        Me.btnPosition.Size = New System.Drawing.Size(75, 23)
        Me.btnPosition.TabIndex = 2
        Me.btnPosition.Text = "..."
        Me.btnPosition.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnPosition)
        Me.GroupBox1.Controls.Add(Me.txtCellAddress)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(346, 77)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "定位图片"
        '
        'btnSelectAreaPic
        '
        Me.btnSelectAreaPic.Location = New System.Drawing.Point(6, 35)
        Me.btnSelectAreaPic.Name = "btnSelectAreaPic"
        Me.btnSelectAreaPic.Size = New System.Drawing.Size(113, 23)
        Me.btnSelectAreaPic.TabIndex = 2
        Me.btnSelectAreaPic.Text = "单元格区域选图"
        Me.ToolTip1.SetToolTip(Me.btnSelectAreaPic, "选中单元格区域中的所有图片" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.btnSelectAreaPic.UseVisualStyleBackColor = True
        '
        'btnSelectCell
        '
        Me.btnSelectCell.Location = New System.Drawing.Point(125, 35)
        Me.btnSelectCell.Name = "btnSelectCell"
        Me.btnSelectCell.Size = New System.Drawing.Size(113, 23)
        Me.btnSelectCell.TabIndex = 2
        Me.btnSelectCell.Text = "覆盖合并单元格"
        Me.ToolTip1.SetToolTip(Me.btnSelectCell, "选中图片的状态下,将合并覆盖区域的单元格")
        Me.btnSelectCell.UseVisualStyleBackColor = True
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(293, 206)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 4
        Me.btnQuit.Text = "退出"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSelectAreaPic)
        Me.GroupBox2.Controls.Add(Me.btnSelectCell)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 105)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(346, 77)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "单元格选图和反选"
        '
        'WIN210913_图片放置定位
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(390, 238)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "WIN210913_图片放置定位"
        Me.Text = "图片放置和选择"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents txtCellAddress As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btnPosition As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnSelectAreaPic As Windows.Forms.Button
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents btnSelectCell As Windows.Forms.Button
    Friend WithEvents btnQuit As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
End Class
