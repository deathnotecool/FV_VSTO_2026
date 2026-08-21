Public Class WIN190119_数据选择

    Private Sub CommandButton1_Click(sender As Object, e As EventArgs) Handles CommandButton1.Click
        On Error GoTo errLine  '当程序出错时跳转至ErrLine处
        Dim rng As Excel.Range, rg As Excel.Range, cell As Excel.Range, i As Long  '声明变量
        '如果文本框TextBox1中未录入数据，那么提示用户，然后结束过程
        If TextBox1.Text = "" Then MsgBox("请填写完整再执行本工具。", 64, "提示") : Exit Sub
        '如果活动工作表中没有数据，那么提示用户，然后关闭窗体
        If xlapp.WorksheetFunction.CountA(xlapp.Cells) = 0 Then MsgBox("空表不能使用定位工具。", 64, "提示") : Exit Sub
        '如果选择的对象不是单元格，那么提示用户，然后关闭窗体
        If TypeName(xlapp.Selection) <> "Range" Then MsgBox("请选择单元格区域！", 64, "提示") : Exit Sub
        '如果选区小于3个单元格，那么提示用户然后关闭窗体
        If xlapp.Selection.Count < 3 Then MsgBox("请选择相对较大的区域", 64, "提示") : Exit Sub
        rng = xlapp.Intersect(xlapp.ActiveSheet.UsedRange, xlapp.Selection)     '将已用数据区域与选区的交集赋值给rng
        If Not IsNumeric(TextBox1.Text) Then GoTo 文本  '如果文字框中输入的是文本则执行“文本”标签处的语句
        If ComboBox1.Text = "<" Then GoTo 小于  '如果复合框的值是“<”则执行“小于”标签处的语句
        If ComboBox1.Text = ">" Then GoTo 大于  '如果复合框的值是“>”则执行“大于”标签处的语句
        If ComboBox1.Text = "＝" Then GoTo 等于  '如果复合框的值是“=”则执行“等于”标签处的语句
        If ComboBox1.Text = "<>" Then GoTo 不等  '如果复合框的值是“<>”则执行“不等”标签处的语句
文本:     '设置一个标签
        If ComboBox1.Text = "<" Or ComboBox1.Text = ">" Then MsgBox("文本定位只能用'等于'和'不等于'") : Exit Sub
        If ComboBox1.Text = "＝" Then GoTo 文本1  '如果组合框的值是等号就执行“文本1”标签处的语句
        If ComboBox1.Text = "<>" Then GoTo 文本2  '如果组合框的值是不等号就执行“文本2”标签处的语句
文本1:     '设置一个标签
        For Each cell In rng  '遍历rng对象区域
            If cell.Value Like TextBox1.Text Then  '如果单元格中的字符与文字框中的字符同类
                i = i + 1  '累加变量
                '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
            End If
        Next
        GoTo errLine  '跳转到标签Errline处，不需要执行标签Errline之前的其它代码
文本2:     '设置一个标签
        For Each cell In rng  '遍历rng对象区域
            '如果单元格中的字符与文字框中的字符不同类,而且是文本
            If Not (cell.Value Like TextBox1.Text) And Not IsNumeric(cell) Then
                i = i + 1  '累加变量
                '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
            End If
        Next
        GoTo errLine  '跳转到标签Errline处，不需要执行标签Errline之前的其它代码
小于:     '设置一个标签
        For Each cell In rng  '遍历rng对象区域
            If cell.Value < --(TextBox1.Text) Then  '如果cell的值小于文本框TextBox1的值
                If Len(cell.Value) * IsNumeric(cell.Value) Then  '如果单元格中有数值，而且非空(空白单元格被当作数值0处理)
                    i = i + 1  '累加变量
                    '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                    If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
                End If
            End If
        Next
        GoTo errLine  '跳转到标签Errline处，不需要执行标签Errline之前的其它代码
大于:     '设置一个标签
        For Each cell In rng  '遍历rng对象区域
            If cell.Value > --(TextBox1.Text) Then  '如果cell的值大于文本框TextBox1的值
                If Len(cell.Value) * IsNumeric(cell.Value) Then   '如果单元格中有数值，而且非空(空白单元格被当作数值0处理)
                    i = i + 1   '累加变量
                    '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                    If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
                End If
            End If
        Next
        GoTo errLine  '跳转到标签Errline处，不需要执行标签Errline之前的其它代码
等于:     '设置一个标签
        For Each cell In rng  '遍历rng对象区域
            If cell.Value = --(TextBox1.Text) Then  '如果cell的值等于文本框TextBox1的值
                If Len(cell.Value) * IsNumeric(cell.Value) Then  '如果单元格中有数值，而且非空(空白单元格被当作数值0处理)
                    i = i + 1   '累加变量
                    '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                    If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
                End If
            End If
        Next
        GoTo errLine  '跳转到标签Errline处，不需要执行标签Errline之前的其它代码
不等:     '设置一个标签
        For Each cell In rng  '遍历rng对象区域
            If cell.Value <> --(TextBox1.Text) Then   '如果cell的值不等于文本框TextBox1的值
                If Len(cell.Value) * IsNumeric(cell.Value) Then  '如果单元格中有数值，而且非空(空白单元格被当作数值0处理)
                    i = i + 1  '累加变量
                    '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                    If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
                End If
            End If
        Next
errLine:     '设置一个标签
        '如果有错误，那么提示用户，且结束过程、关闭窗体
        If Err.Number() <> 0 Then MsgBox("未找到数据", 64, "提示") : Exit Sub
        rg.Select  '选择rn对象区域
        xlapp.StatusBar = "已找到" & i & "个符合条件的值"  '在状态栏显示找到的目标个数
        Me.Close()
    End Sub


    Private Sub CommandButton2_Click(sender As Object, e As EventArgs) Handles CommandButton2.Click
        On Error GoTo errLine  '当程序出错时跳转到Errline标签处
        '如果两个文本框都没有填写数据，那么提示用户然后结束过程
        If TextBox2.Text = "" Or TextBox3.Text = "" Then MsgBox("请填写完整再执行本工具。", 64, "提示") : Exit Sub
        '如果TextBox3的值小于等于TextBox2的值，那么提示用户，然后结束过程
        If Val(TextBox3.Text) <= Val(TextBox2.Text) Then MsgBox("起始值必须小于终止值。", 64, "提示") : Exit Sub
        Dim rng As Excel.Range, rg As Excel.Range, cell As Excel.Range, i As Long  '声明变量
        '如果工作表中没有数据，那么提示用户，然后结束过程且关闭窗体
        If xlapp.WorksheetFunction.CountA(xlapp.ActiveSheet.Cells) = 0 Then MsgBox("当前表空白，拒绝执行本工具。", 64, "提示") : Exit Sub
        '如果用户选择的对象不是单元格，那么提示用户然后结束过程且关闭窗体
        If TypeName(xlapp.Selection) <> "Range" Then MsgBox("请选择单元格区域！", 64, "提示") : Exit Sub
        '如果选区小于3个单元格，那么提示用户然后关闭窗体
        If xlapp.Selection.Count < 3 Then MsgBox("请选择相对较大的区域", 64, "提示") : Exit Sub
        rng = xlapp.Intersect(xlapp.ActiveSheet.UsedRange, xlapp.Selection)     '将已用数据区域与选区的交集赋值给rng
        If OptionButton1.Checked Then  '如果选择“范围之内”
            For Each cell In rng   '遍历rng对象区域
                If cell.Value >= Val(TextBox2.Text) And cell.Value <= Val(TextBox3.Text) Then  '如果满足设定的两个条件
                    If Len(cell.Value) * IsNumeric(cell.Value) Then  '如果非空且是数字
                        i = i + 1  '累加变量
                        '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                        If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
                    End If
                End If
            Next cell
        Else  '否则
            For Each cell In rng  '遍历rng对象区域
                If cell.Value < Val(TextBox2.Text) Or cell.Value > Val(TextBox3.Text) Then  '如果满足设定的两个条件
                    If Len(cell.Value) * IsNumeric(cell.Value) Then  '如果非空且是数字
                        i = i + 1  '累加变量
                        '如果是第一个，那么将cell赋值给变量rg,否则将rg与cell合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                        If i = 1 Then rg = cell Else rg = xlapp.Union(rg, cell)
                    End If
                End If
            Next cell
        End If
errLine:
        If Err.Number <> 0 Then MsgBox("未找到数据", 64, "提示") : Exit Sub  '如果有错误则提示未找数据
        rg.Select   '如果已找到数值，那么选择对象区域，且在状态栏显示数量
        xlapp.StatusBar = "已找到" & i & "个符合条件的值"
        Me.Close()
    End Sub

    Private Sub WIN190119_数据选择_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim arr As Object
        arr = {">", "<", "＝", "<>"}
        With ComboBox1   '引用签订部门复合框
            .Items.Clear()      '清除项目
            .Items.AddRange(arr)      '添加项目(字段值的value属性是默认值)为当前字段值
        End With    '结束引用语句
        ComboBox1.SelectedIndex = 2        '默认选择第一项

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Call 极值
        Exit Sub
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Call 极值
        Exit Sub
    End Sub
    Sub 极值()
        On Error Resume Next
        Dim rg As Excel.Range, cell As Excel.Range, Num As Double, rng As Excel.Range  '声明变量
        If TypeName(xlapp.Selection) <> "Range" Then MsgBox("请选择单元格区域！", vbInformation, "提示") : Exit Sub   '如果选择的对象不是单元格就结束过程
        If xlapp.WorksheetFunction.Count(xlapp.Selection) = 0 Then Exit Sub     '如果没有数值则退出
        Num = IIf(ActiveControl.Text = "定位最大值", xlapp.WorksheetFunction.Max(xlapp.Selection), xlapp.WorksheetFunction.Min(xlapp.Selection))      '根据选择控件决定目标值大小
        For Each rng In xlapp.Intersect(xlapp.ActiveSheet.UsedRange, xlapp.Selection)     '遍历选区与已用区域的交集
            If Len(rng.Value) * IsNumeric(rng.Value) Then  '如果是数值
                If rng.Value = Num Then  '如果等于变量NUM
                    i = i + 1  '累加变量
                    '如果是第一个，那么将rng赋值给变量rg,否则将rg与rng合并(当循环完成后，变量rg即包含符合条件的所有单元格)
                    If i = 1 Then rg = rng Else rg = xlapp.Union(rg, rng)
                End If
            End If
        Next
        If i > 0 Then  '如果已找到符合条件的单元格
            rg.Select    '那么选择rg对象所代表的区域
            xlapp.StatusBar = "已找到" & i & "个符合条件的值"  '在状态栏显示符合条件的单元格数量
        End If
        i = 0
        rg = Nothing
        Me.Close()
    End Sub

End Class