<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIN190515_孔间距计算
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN190515_孔间距计算))
        Me.txtPitch = New System.Windows.Forms.TextBox()
        Me.txtAngle = New System.Windows.Forms.TextBox()
        Me.txtDimt1 = New System.Windows.Forms.TextBox()
        Me.txtDimt2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnEvaluate = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtPitch
        '
        Me.txtPitch.Location = New System.Drawing.Point(95, 19)
        Me.txtPitch.Name = "txtPitch"
        Me.txtPitch.Size = New System.Drawing.Size(100, 21)
        Me.txtPitch.TabIndex = 1
        '
        'txtAngle
        '
        Me.txtAngle.Location = New System.Drawing.Point(95, 49)
        Me.txtAngle.Name = "txtAngle"
        Me.txtAngle.Size = New System.Drawing.Size(100, 21)
        Me.txtAngle.TabIndex = 2
        '
        'txtDimt1
        '
        Me.txtDimt1.Location = New System.Drawing.Point(95, 79)
        Me.txtDimt1.Name = "txtDimt1"
        Me.txtDimt1.Size = New System.Drawing.Size(100, 21)
        Me.txtDimt1.TabIndex = 3
        '
        'txtDimt2
        '
        Me.txtDimt2.Location = New System.Drawing.Point(95, 109)
        Me.txtDimt2.Name = "txtDimt2"
        Me.txtDimt2.Size = New System.Drawing.Size(100, 21)
        Me.txtDimt2.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "节圆直径"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "2孔之间夹角"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "孔径1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(48, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 12)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "孔径2"
        '
        'btnEvaluate
        '
        Me.btnEvaluate.Location = New System.Drawing.Point(8, 145)
        Me.btnEvaluate.Name = "btnEvaluate"
        Me.btnEvaluate.Size = New System.Drawing.Size(75, 23)
        Me.btnEvaluate.TabIndex = 5
        Me.btnEvaluate.Text = "Evaluate"
        Me.btnEvaluate.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(120, 145)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'WIN190515_孔间距计算
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(227, 189)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnEvaluate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDimt2)
        Me.Controls.Add(Me.txtDimt1)
        Me.Controls.Add(Me.txtAngle)
        Me.Controls.Add(Me.txtPitch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WIN190515_孔间距计算"
        Me.Text = "WIN190515_孔间距计算"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtPitch As Windows.Forms.TextBox
    Friend WithEvents txtAngle As Windows.Forms.TextBox
    Friend WithEvents txtDimt1 As Windows.Forms.TextBox
    Friend WithEvents txtDimt2 As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents btnEvaluate As Windows.Forms.Button
    Friend WithEvents btnExit As Windows.Forms.Button
End Class
