<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WIN190512_二维码
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN190512_二维码))
        Me.btnCreateQukCode = New System.Windows.Forms.Button()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.btnSelcetArea = New System.Windows.Forms.Button()
        Me.btnReadQuickCode = New System.Windows.Forms.Button()
        Me.更新按钮 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnIncomingSelectArea = New System.Windows.Forms.Button()
        Me.btnDisplaying = New System.Windows.Forms.Button()
        Me.txtIncomingAddress = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.更新按钮1 = New System.Windows.Forms.Button()
        Me.btnCreateQukCode1 = New System.Windows.Forms.Button()
        Me.btnSelcetArea1 = New System.Windows.Forms.Button()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCreateQukCode
        '
        Me.btnCreateQukCode.Location = New System.Drawing.Point(10, 47)
        Me.btnCreateQukCode.Name = "btnCreateQukCode"
        Me.btnCreateQukCode.Size = New System.Drawing.Size(75, 23)
        Me.btnCreateQukCode.TabIndex = 0
        Me.btnCreateQukCode.Text = "生成二维码"
        Me.btnCreateQukCode.UseVisualStyleBackColor = True
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(10, 20)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(166, 21)
        Me.txtAddress.TabIndex = 1
        '
        'btnSelcetArea
        '
        Me.btnSelcetArea.Location = New System.Drawing.Point(182, 18)
        Me.btnSelcetArea.Name = "btnSelcetArea"
        Me.btnSelcetArea.Size = New System.Drawing.Size(75, 23)
        Me.btnSelcetArea.TabIndex = 2
        Me.btnSelcetArea.Text = "选择区域"
        Me.btnSelcetArea.UseVisualStyleBackColor = True
        '
        'btnReadQuickCode
        '
        Me.btnReadQuickCode.Location = New System.Drawing.Point(101, 47)
        Me.btnReadQuickCode.Name = "btnReadQuickCode"
        Me.btnReadQuickCode.Size = New System.Drawing.Size(75, 23)
        Me.btnReadQuickCode.TabIndex = 4
        Me.btnReadQuickCode.Text = "读取数据"
        Me.btnReadQuickCode.UseVisualStyleBackColor = True
        '
        '更新按钮
        '
        Me.更新按钮.Location = New System.Drawing.Point(182, 47)
        Me.更新按钮.Name = "更新按钮"
        Me.更新按钮.Size = New System.Drawing.Size(75, 23)
        Me.更新按钮.TabIndex = 5
        Me.更新按钮.Text = "更新按钮"
        Me.更新按钮.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSelcetArea)
        Me.GroupBox1.Controls.Add(Me.更新按钮)
        Me.GroupBox1.Controls.Add(Me.btnCreateQukCode)
        Me.GroupBox1.Controls.Add(Me.btnReadQuickCode)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(265, 80)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "新罗-热处理不良通报"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnIncomingSelectArea)
        Me.GroupBox2.Controls.Add(Me.btnDisplaying)
        Me.GroupBox2.Controls.Add(Me.txtIncomingAddress)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 202)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(265, 51)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "新罗-入库编号信息录入"
        '
        'btnIncomingSelectArea
        '
        Me.btnIncomingSelectArea.Location = New System.Drawing.Point(10, 17)
        Me.btnIncomingSelectArea.Name = "btnIncomingSelectArea"
        Me.btnIncomingSelectArea.Size = New System.Drawing.Size(247, 28)
        Me.btnIncomingSelectArea.TabIndex = 2
        Me.btnIncomingSelectArea.Text = "选择区域自动生成二维码"
        Me.btnIncomingSelectArea.UseVisualStyleBackColor = True
        '
        'btnDisplaying
        '
        Me.btnDisplaying.Location = New System.Drawing.Point(65, 19)
        Me.btnDisplaying.Name = "btnDisplaying"
        Me.btnDisplaying.Size = New System.Drawing.Size(39, 23)
        Me.btnDisplaying.TabIndex = 0
        Me.btnDisplaying.Text = "生成"
        Me.btnDisplaying.UseVisualStyleBackColor = True
        '
        'txtIncomingAddress
        '
        Me.txtIncomingAddress.Location = New System.Drawing.Point(10, 19)
        Me.txtIncomingAddress.Name = "txtIncomingAddress"
        Me.txtIncomingAddress.Size = New System.Drawing.Size(83, 21)
        Me.txtIncomingAddress.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.更新按钮1)
        Me.GroupBox3.Controls.Add(Me.btnCreateQukCode1)
        Me.GroupBox3.Controls.Add(Me.btnSelcetArea1)
        Me.GroupBox3.Controls.Add(Me.txtAddress1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 98)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(265, 80)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "新罗-组装区不良通报"
        '
        '更新按钮1
        '
        Me.更新按钮1.Location = New System.Drawing.Point(182, 51)
        Me.更新按钮1.Name = "更新按钮1"
        Me.更新按钮1.Size = New System.Drawing.Size(75, 23)
        Me.更新按钮1.TabIndex = 6
        Me.更新按钮1.Text = "更新按钮"
        Me.更新按钮1.UseVisualStyleBackColor = True
        '
        'btnCreateQukCode1
        '
        Me.btnCreateQukCode1.Location = New System.Drawing.Point(10, 51)
        Me.btnCreateQukCode1.Name = "btnCreateQukCode1"
        Me.btnCreateQukCode1.Size = New System.Drawing.Size(75, 23)
        Me.btnCreateQukCode1.TabIndex = 4
        Me.btnCreateQukCode1.Text = "生成二维码"
        Me.btnCreateQukCode1.UseVisualStyleBackColor = True
        '
        'btnSelcetArea1
        '
        Me.btnSelcetArea1.Location = New System.Drawing.Point(182, 18)
        Me.btnSelcetArea1.Name = "btnSelcetArea1"
        Me.btnSelcetArea1.Size = New System.Drawing.Size(75, 23)
        Me.btnSelcetArea1.TabIndex = 3
        Me.btnSelcetArea1.Text = "选择区域"
        Me.btnSelcetArea1.UseVisualStyleBackColor = True
        '
        'txtAddress1
        '
        Me.txtAddress1.Location = New System.Drawing.Point(10, 20)
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(166, 21)
        Me.txtAddress1.TabIndex = 0
        '
        'WIN190512_二维码
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(285, 264)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WIN190512_二维码"
        Me.Text = "WIN190512_二维码"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnCreateQukCode As Windows.Forms.Button
    Friend WithEvents txtAddress As Windows.Forms.TextBox
    Friend WithEvents btnSelcetArea As Windows.Forms.Button
    Friend WithEvents btnReadQuickCode As Windows.Forms.Button
    Friend WithEvents 更新按钮 As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents btnIncomingSelectArea As Windows.Forms.Button
    Friend WithEvents btnDisplaying As Windows.Forms.Button
    Friend WithEvents txtIncomingAddress As Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents 更新按钮1 As Windows.Forms.Button
    Friend WithEvents btnCreateQukCode1 As Windows.Forms.Button
    Friend WithEvents btnSelcetArea1 As Windows.Forms.Button
    Friend WithEvents txtAddress1 As Windows.Forms.TextBox
End Class
