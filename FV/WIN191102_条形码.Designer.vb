<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WIN191102_条形码
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIN191102_条形码))
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.btnSelcetArea = New System.Windows.Forms.Button()
        Me.btnCreateBarCode = New System.Windows.Forms.Button()
        Me.btnCreate21EA = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(12, 12)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(253, 21)
        Me.txtAddress.TabIndex = 0
        '
        'btnSelcetArea
        '
        Me.btnSelcetArea.Location = New System.Drawing.Point(12, 39)
        Me.btnSelcetArea.Name = "btnSelcetArea"
        Me.btnSelcetArea.Size = New System.Drawing.Size(75, 23)
        Me.btnSelcetArea.TabIndex = 1
        Me.btnSelcetArea.Text = "选择区域"
        Me.btnSelcetArea.UseVisualStyleBackColor = True
        '
        'btnCreateBarCode
        '
        Me.btnCreateBarCode.Location = New System.Drawing.Point(95, 39)
        Me.btnCreateBarCode.Name = "btnCreateBarCode"
        Me.btnCreateBarCode.Size = New System.Drawing.Size(76, 23)
        Me.btnCreateBarCode.TabIndex = 2
        Me.btnCreateBarCode.Text = "生成条形码"
        Me.btnCreateBarCode.UseVisualStyleBackColor = True
        '
        'btnCreate21EA
        '
        Me.btnCreate21EA.Location = New System.Drawing.Point(179, 39)
        Me.btnCreate21EA.Name = "btnCreate21EA"
        Me.btnCreate21EA.Size = New System.Drawing.Size(86, 23)
        Me.btnCreate21EA.TabIndex = 3
        Me.btnCreate21EA.Text = "全部生成"
        Me.btnCreate21EA.UseVisualStyleBackColor = True
        '
        'WIN191102_条形码
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(277, 71)
        Me.Controls.Add(Me.btnCreate21EA)
        Me.Controls.Add(Me.btnCreateBarCode)
        Me.Controls.Add(Me.btnSelcetArea)
        Me.Controls.Add(Me.txtAddress)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WIN191102_条形码"
        Me.Text = "Volvo条形码"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtAddress As Windows.Forms.TextBox
    Friend WithEvents btnSelcetArea As Windows.Forms.Button
    Friend WithEvents btnCreateBarCode As Windows.Forms.Button
    Friend WithEvents btnCreate21EA As Windows.Forms.Button
End Class
