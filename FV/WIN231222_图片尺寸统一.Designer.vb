<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN231222_图片尺寸统一
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN231222_图片尺寸统一))
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.butRefference = New System.Windows.Forms.Button()
        Me.butPerform = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(128, 30)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(132, 21)
        Me.txtName.TabIndex = 1
        '
        'butRefference
        '
        Me.butRefference.Location = New System.Drawing.Point(290, 28)
        Me.butRefference.Name = "butRefference"
        Me.butRefference.Size = New System.Drawing.Size(75, 23)
        Me.butRefference.TabIndex = 1
        Me.butRefference.TabStop = False
        Me.butRefference.Text = "选择参考图"
        Me.butRefference.UseVisualStyleBackColor = True
        '
        'butPerform
        '
        Me.butPerform.Location = New System.Drawing.Point(371, 28)
        Me.butPerform.Name = "butPerform"
        Me.butPerform.Size = New System.Drawing.Size(75, 23)
        Me.butPerform.TabIndex = 2
        Me.butPerform.Text = "执行"
        Me.butPerform.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "输入参考图名称"
        '
        'WIN231222_图片尺寸统一
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(467, 79)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.butPerform)
        Me.Controls.Add(Me.butRefference)
        Me.Controls.Add(Me.txtName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WIN231222_图片尺寸统一"
        Me.Text = "WIN231222_图片尺寸统一"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtName As Windows.Forms.TextBox
    Friend WithEvents butRefference As Windows.Forms.Button
    Friend WithEvents butPerform As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
