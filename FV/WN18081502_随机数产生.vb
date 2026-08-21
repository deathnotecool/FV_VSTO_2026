Imports System.Drawing

Public Class WN18081502_随机数产生
    '产生随机数-180105
    REM 功能：13实践-疑难60-P141 生成随机数 关键词：rnd,collection
    '声明一个单元格对象和布尔变量
    Dim rng As Excel.Range      ', ckbOption As Boolean

    '退出按钮-单击事件
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles CommandButton3.Click
        Globals.Ribbons.Ribbon1.Button25.Enabled = True     '重新启用按钮
        Me.Close()        '关闭窗体
    End Sub

    '窗体启动加载
    Private Sub 随机数产生_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        On Error Resume Next
        TextBox4.BackColor = Color.Gray  '对应小数位数的文本框显示灰色
        'TextBox1.Text = xlapp.Selection.Address '将地址赋值给文本框1
        rng = xlapp.Range(TextBox1.Text)    '赋值给窗体全局匹配变量对象
        TextBox4.Enabled = False '禁止编辑小数位数    '
    End Sub

    '确定按钮,单击事件
    Private Sub CommandButton2_Click(sender As Object, e As EventArgs) Handles CommandButton2.Click
        On Error Resume Next
        随机数产生_Load(Nothing, Nothing)  '重新调用加载事件

        '声明起始值和终止值变量
        Dim 起始值 As Double, 结束值 As Double
        Dim k As Integer, eI As Byte

        '文本字符串转换成数值
        起始值 = CType(TextBox2.Text, Double)
        结束值 = CType(TextBox3.Text, Double)
        eI = CType(TextBox4.Text, Double)

        '  起始值 = TextBox2.Value: 结束值 = TextBox3.Value '指定起止数，表示在此范围产生不重复随机数，可以修改为其他值
        If CheckBox1.Checked Then
            For k = 1 To rng.Count
                rng(k).Value = xlapp.Round(Rnd() * (结束值 - 起始值) + 起始值, eI)
            Next k
        Else
            For k = 1 To rng.Count
                rng(k).Value = xlapp.Round(Rnd() * (结束值 - 起始值) + 起始值, 0)
            Next k
        End If

    End Sub

    Private Sub CommandButton1_Click(sender As Object, e As EventArgs) Handles CommandButton1.Click
        On Error Resume Next
        Dim strRangeAddress As String
        strRangeAddress = xlapp.Selection.address
        rng = xlapp.InputBox("请指定随机数区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        TextBox1.Text = rng.Address
        rng.Select()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox4.Enabled = True
            TextBox4.BackColor = Color.White
        Else
            TextBox4.Enabled = False
            TextBox4.BackColor = Color.Gray
        End If
    End Sub

    Private Sub 随机数产生_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.Button25.Enabled = True
    End Sub




End Class