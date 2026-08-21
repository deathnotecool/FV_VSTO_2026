<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class D03_调休节假日信息
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
        Me.grdAuthorTitles = New System.Windows.Forms.DataGridView()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.备注 = New System.Windows.Forms.TextBox()
        Me.添加 = New System.Windows.Forms.Button()
        Me.加班倍数 = New System.Windows.Forms.ComboBox()
        Me.分类 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.新建 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.日期 = New System.Windows.Forms.DateTimePicker()
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdAuthorTitles
        '
        Me.grdAuthorTitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAuthorTitles.Location = New System.Drawing.Point(12, 107)
        Me.grdAuthorTitles.Name = "grdAuthorTitles"
        Me.grdAuthorTitles.RowTemplate.Height = 23
        Me.grdAuthorTitles.Size = New System.Drawing.Size(744, 330)
        Me.grdAuthorTitles.TabIndex = 41
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        Me.lbl1.Location = New System.Drawing.Point(3, 23)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(29, 12)
        Me.lbl1.TabIndex = 0
        Me.lbl1.Text = "分类"
        '
        '备注
        '
        Me.备注.Location = New System.Drawing.Point(56, 64)
        Me.备注.Name = "备注"
        Me.备注.Size = New System.Drawing.Size(529, 21)
        Me.备注.TabIndex = 1
        '
        '添加
        '
        Me.添加.Location = New System.Drawing.Point(15, 51)
        Me.添加.Name = "添加"
        Me.添加.Size = New System.Drawing.Size(51, 20)
        Me.添加.TabIndex = 5
        Me.添加.Text = "添加"
        Me.添加.UseVisualStyleBackColor = True
        '
        '加班倍数
        '
        Me.加班倍数.FormattingEnabled = True
        Me.加班倍数.Location = New System.Drawing.Point(468, 19)
        Me.加班倍数.Name = "加班倍数"
        Me.加班倍数.Size = New System.Drawing.Size(117, 20)
        Me.加班倍数.TabIndex = 6
        '
        '分类
        '
        Me.分类.FormattingEnabled = True
        Me.分类.Location = New System.Drawing.Point(56, 20)
        Me.分类.Name = "分类"
        Me.分类.Size = New System.Drawing.Size(117, 20)
        Me.分类.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(197, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "日期"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "备注"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(391, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "加班倍数"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnQuit)
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Controls.Add(Me.添加)
        Me.GroupBox2.Controls.Add(Me.新建)
        Me.GroupBox2.Location = New System.Drawing.Point(619, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(137, 81)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "操作选项"
        '
        '新建
        '
        Me.新建.Location = New System.Drawing.Point(15, 20)
        Me.新建.Name = "新建"
        Me.新建.Size = New System.Drawing.Size(51, 22)
        Me.新建.TabIndex = 18
        Me.新建.Text = "新建"
        Me.新建.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.日期)
        Me.GroupBox1.Controls.Add(Me.lbl1)
        Me.GroupBox1.Controls.Add(Me.加班倍数)
        Me.GroupBox1.Controls.Add(Me.备注)
        Me.GroupBox1.Controls.Add(Me.分类)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(601, 93)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "调休节假日信息"
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(80, 51)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(51, 20)
        Me.btnQuit.TabIndex = 19
        Me.btnQuit.Text = "退出"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(80, 20)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(51, 22)
        Me.btnDelete.TabIndex = 20
        Me.btnDelete.Text = "删除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 446)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(769, 25)
        Me.ToolStrip1.TabIndex = 44
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripLabel1.Text = "ToolStripLabel1"
        '
        '日期
        '
        Me.日期.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.日期.Location = New System.Drawing.Point(249, 20)
        Me.日期.Name = "日期"
        Me.日期.Size = New System.Drawing.Size(123, 21)
        Me.日期.TabIndex = 7
        Me.日期.Value = New Date(2018, 10, 7, 11, 18, 21, 0)
        '
        'D03_调休节假日信息
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 471)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdAuthorTitles)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "D03_调休节假日信息"
        Me.Text = "D03_调休节假日信息"
        CType(Me.grdAuthorTitles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents grdAuthorTitles As Windows.Forms.DataGridView
    Friend WithEvents lbl1 As Windows.Forms.Label
    Friend WithEvents 备注 As Windows.Forms.TextBox
    Friend WithEvents 添加 As Windows.Forms.Button
    Friend WithEvents 加班倍数 As Windows.Forms.ComboBox
    Friend WithEvents 分类 As Windows.Forms.ComboBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents 新建 As Windows.Forms.Button
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents btnQuit As Windows.Forms.Button
    Friend WithEvents btnDelete As Windows.Forms.Button
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As Windows.Forms.ToolStripLabel
    Friend WithEvents 日期 As Windows.Forms.DateTimePicker
End Class
