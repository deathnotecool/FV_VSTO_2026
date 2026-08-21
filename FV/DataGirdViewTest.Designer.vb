<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataGirdViewTest
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
        Me.RowMergeView1 = New System.Windows.Forms.DataGridView()
        CType(Me.RowMergeView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RowMergeView1
        '
        Me.RowMergeView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RowMergeView1.Location = New System.Drawing.Point(42, 12)
        Me.RowMergeView1.Name = "RowMergeView1"
        Me.RowMergeView1.RowTemplate.Height = 23
        Me.RowMergeView1.Size = New System.Drawing.Size(633, 322)
        Me.RowMergeView1.TabIndex = 0
        '
        'DataGirdViewTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 468)
        Me.Controls.Add(Me.RowMergeView1)
        Me.Name = "DataGirdViewTest"
        Me.Text = "DataGirdViewTest"
        CType(Me.RowMergeView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RowMergeView1 As Windows.Forms.DataGridView
End Class
