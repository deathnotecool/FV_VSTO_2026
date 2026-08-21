Imports System.Windows.Forms

Public Class WIN190810_VBA代码笔记
    Dim myArray() As String                       '声明数组变量,数组长度为要引用的数据表字段数量.
    REM 功    能: 宝3-17.5.7-P387 在窗体中跨工作表查询 关键词：array,combox1


    'Private Sub UserForm_Activate()
    '    'On Error Resume Next
    '    myArray = {"详细代码", "备注"}
    '    ComboBox1.Items.AddRange(myArray)
    '    'ComboBox1.List = Array("详细代码", "代码ID") '为复合框指定列表内容 vba代码
    '    ComboBox1.SelectedIndex = 0  '默认选择第一项
    'End Sub

    '当在文字框中输入新的查找对象时，清空列表框中上一次的结果
    Private Sub TextBox1_Change()
        On Error Resume Next
        With ListView1         '引用视图控件
            '设置ListView1的标题、显示类型、整行选择和网格线属性
            .Columns.Clear()        '清除标题行
            .Clear()                '清除项目集
            .View = View.Details    '报表输出视图
            .FullRowSelect = True   '允许整行选择
            .GridLines = True       '允许网格线
        End With
    End Sub


    Private Sub WIN190810_VBA代码笔记_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'On Error Resume Next
        myArray = {"详细代码", "备注"}
        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(myArray)
        'ComboBox1.List = Array("详细代码", "代码ID") '为复合框指定列表内容 vba代码
        ComboBox1.SelectedIndex = 0  '默认选择第一项
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        On Error Resume Next
        Dim Sht As Excel.Worksheet, RowCount As Integer, Item As Integer, FindText As String = ""   '声明变量
        Dim aryTitle As Object
        xlapp.Workbooks("FV.xla").IsAddin = False

        aryTitle = {"ID", "详细代码", "书本分类", "备注"}
        Item = 0  '对变量赋值为初始值1,因为标题栏已经占用了一行
        'On Error Resume Next    '出错继续在错误处执行
        Dim i As Integer        '声明变量
        With ListView1         '引用视图控件
            '设置ListView1的标题、显示类型、整行选择和网格线属性
            .Columns.Clear()        '清除标题行
            .Clear()                '清除项目集
            .View = View.Details    '报表输出视图
            .FullRowSelect = True   '允许整行选择
            .GridLines = True       '允许网格线
            For i = 0 To UBound(aryTitle)                 '为ListView1设置标题,在0到字段数量-1上循环
                .Columns.Add(aryTitle(i).ToString, 100)   '添加标题
            Next i

            'ListView1.Columns(0).Width = 50 '列宽根据列内容自适应，此时保证列内容都可见
            'ListView1.Columns(1).Width = 1050 '列宽根据列内容自适应，此时保证列内容都可见
            'yourListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            'ListView1.Columns(2).Width = 60 '列宽根据列内容自适应，此时保证列内容都可见
            'ListView1.Columns(3).Width = 550 '列宽根据列内容自适应，此时保证列内容都可见
            'ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent) '列宽根据列内容自适应，此时保证列内容都可见
            ''为ListView1设置各行数据                       
            'For i = 1 To rs.RecordCount                                                     '在1到记录数量上循环
            '    Dim itm As ListViewItem = ListView1.Items.Add(rs.Fields(0).Value.ToString)  '首列为项目名
            '    For j = 1 To rs.Fields.Count - 1                                            '在1到字段总数-1上循环,向项目名后添加.
            '        'itm.SubItems.AddRange({"钢笔", "500", "2012-9-15"})
            '        itm.SubItems.AddRange({rs.Fields(j).value.ToString}) '从第2列开始添加索引列的子项目值
            '    Next j          '循环语句
            '    rs.MoveNext     '定位到下一条记录
            'Next i  '循环

            For Each Sht In xlapp.Worksheets   '遍历所有工作表
                Sht.Activate()      '激活该活动工作表
                For RowCount = 2 To Sht.Cells(xlapp.Rows.Count, 1).End(-4162).Row   '遍历sht工作表的所有行，首行除外
                    Select Case ComboBox1.Text  '根据复合框的值决然定查找方式
                        Case "详细代码"  '如果用户选择按代码查找
                            FindText = Sht.Cells(RowCount, 2).value  '将sht表中第2列第RowCount行的值赋予变量FindText
                        Case "备注"  '如果用户选择按成绩查找
                            FindText = Sht.Cells(RowCount, 3).value   '将sht表中第3列第RowCount行的值赋予变量FindText
                    End Select
                    If UCase(FindText) Like "*" & UCase(TextBox1.Text) & "*" Then  '如果FindText的值包含了文本框TextBox1的值
                        'Item = Item + 1  '累加计数器
                        'ReDim Preserve arr(0 To 2, 0 To Item - 1)  '重置数组变量
                        'arr(0, Item) = Sht.Cells(RowCount, 1) '将工作表ROWCOUNT行详细代码名称导入数组的Item列第1行
                        'arr(1, Item) = Sht.Name                '将工作表名称导入数组的Item列第2行
                        'arr(2, Item) = Sht.Cells(RowCount, 2)  '将工作表代码ID导入数组的Item列第3行
                        Dim itm As ListViewItem = ListView1.Items.Add(Sht.Cells(RowCount, 1).value.ToString)  '首列为项目名
                        'For j = 1 To 3                                         '在1到字段总数-1上循环,向项目名后添加.
                        'itm.SubItems.AddRange({"钢笔", "500", "2012-9-15"})

                        itm.SubItems.Add(Sht.Cells(RowCount, 2).value.ToString) '从第2列开始添加索引列的子项目值
                        itm.SubItems.Add(Sht.Name) '从第2列开始添加索引列的子项目值
                        itm.SubItems.Add(Sht.Cells(RowCount, 3).value.ToString) '从第3列开始添加索引列的子项目值

                        'itm.SubItems.Add(Sht.Cells(RowCount, 4).value.ToString) '从第2列开始添加索引列的子项目值
                        'Next j          '循环语句
                    End If
                Next RowCount
            Next Sht
        End With    '结束引用对象
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent) '列宽根据列内容自适应，此时保证列内容都可见
        ListView1.Focus()  '将焦点转移到ComboBox1
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        Dim Sht As Excel.Worksheet, Rng As Excel.Range, targetrng As Excel.Range, msg1 As MsgBoxResult '声明变量
        'On Error Resume Next
        For Each Sht In xlapp.Worksheets        '在工作表集合中循环
            Sht.Activate()      '激活该活动工作表
            For Each Rng In Sht.Range(xlapp.Cells(1, 2), xlapp.Cells(xlapp.Cells.Rows.Count, 2).End(-4162))      '在已用单元格中循环
                'If Rng.Value <> "" And Rng.Value = e.Item.SubItems(1).Text Then  '如果有单元格与列表框所选的值相同,则执行下面语句
                If Rng.Value <> "" And Rng.Offset(0, -1).Value = e.Item.SubItems(0).Text And Rng.Value = e.Item.SubItems(1).Text Then  '如果有单元格与列表框所选的值相同,则执行下面语句

                    'msg1 = MsgBox(Rng.Value & Chr(10) & "选择是：选中对应单元格；" + Chr(10) + "选择否：仅退出该提示窗口。", vbYesNo, "操作方式")  '提示信息结果赋值给变量
                    '    'msg1 = MsgBox(Rng.Value & Chr(10) & "选择是：选中对应单元格；" + Chr(10) + "选择否：仅退出该提示窗口。", vbYesNo, "操作方式")  '提示信息结果赋值给变量
                    'If msg1 = vbYes Then  '如果选择了是那么执行下面语句
                    '    Rng.Select()  '选中单元格
                    '    Exit Sub     '退出程序
                    'End If

                    'msg1 = MsgBox(Rng.Value & Chr(10) & "选择是：选中对应单元格；" + Chr(10) + "选择否：仅退出该提示窗口。", vbYesNo, "操作方式")  '提示信息结果赋值给变量
                    'If msg1 = vbYes Then  '如果选择了是那么执行下面语句
                    Rng.Select()  '选中单元格
                    Exit Sub     '退出程序
                    'End If





                End If
            Next Rng
        Next Sht
        'If e.IsSelected Then MsgBox(e.Item.SubItems(1).Text) '文本框显示鼠标选择项第二列内容
    End Sub



    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then btnSearch_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        On Error Resume Next
        Kill("C:\Program Files\FV\FV.xlsm")
        xlapp.Workbooks("FV.xlam").SaveAs(Filename:="C:\Program Files\FV\FV.xlsm", FileFormat:=52, CreateBackup:=False)

        Kill("C:\Program Files\FV\FV.xlam")
        xlapp.Workbooks("FV.xlsm").SaveAs("C:\Program Files\FV\FV.xlam", FileFormat:=18, CreateBackup:=False)

        'Dim wb As Excel.Workbook
        'Dim strNotePath As String = "D:\2 笔记记录\1_EXCEL模板综合笔记\2_书籍笔记\0 VBA代码笔记.xlsm"
        'Dim f As New WIN190810_VBA代码笔记
        'MsgBox("笔记路径:" & strNotePath)
        'wb = xlapp.Workbooks.Open(strNotePath)
        'xlapp.Workbooks("FV.xla").IsAddin = False
        'f.Show()
    End Sub





End Class