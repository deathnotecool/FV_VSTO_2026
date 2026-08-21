Public Class WIN190324_重复数据分析提取
    Dim rng1 As Excel.Range, rng2 As Excel.Range

    Private Sub btnSelectFstArea_Click(sender As Object, e As EventArgs) Handles btnSelectFstArea.Click
        Dim strRangeAddress As String
        On Error Resume Next
        strRangeAddress = xlapp.Selection.address
        rng1 = xlapp.InputBox("请指定第一区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        txtFstArea.Text = rng1.Address
        rng1.Select()
    End Sub

    Private Sub btnSecArea_Click(sender As Object, e As EventArgs) Handles btnSecArea.Click
        On Error Resume Next
        Dim strRangeAddress As String
        strRangeAddress = xlapp.Selection.address
        rng2 = xlapp.InputBox("请指定第一区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        txtSecArea.Text = rng2.Address
        rng2.Select()
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim rng As Excel.Range, i As Long    '声明变量
        xlapp.ScreenUpdating = False  '关闭屏幕更新
        xlapp.Calculation = -4135  '手动计算
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        On Error Resume Next  '如果有错继续执行下一句
        For Each rng In rng1  '再次遍历单元格
            If Len(rng) > 0 Then '如果rng的字符数量大于0
                If xlapp.WorksheetFunction.CountIf(rng2, rng.Text) >= 1 Then  '如果单元格rng在整个区域中不止一个



                    ListBox1.Items.Add(rng.Text)
                Else
                    ListBox2.Items.Add(rng.Text)
                End If
            End If
        Next rng
        For Each rng In rng2  '再次遍历单元格
            If Len(rng) > 0 Then '如果rng的字符数量大于0
                If xlapp.WorksheetFunction.CountIf(rng1, rng.Text) = 0 Then  '如果单元格rng在整个区域中不止一个
                    ListBox3.Items.Add(rng.Text)
                End If
            End If
        Next rng



        Dim newList As New System.Collections.ArrayList()
        With ListBox1
            For i = 0 To .Items.Count - 1
                If Not newList.Contains(.Items(i)) Then
                    newList.Add(.Items(i))
                End If
            Next i
            .Items.Clear()
            For i = 0 To newList.Count - 1
                .Items.Add(newList(i))
            Next i
        End With

        Dim newList1 As New System.Collections.ArrayList()
        With ListBox2
            For i = 0 To .Items.Count - 1
                If Not newList1.Contains(.Items(i)) Then
                    newList1.Add(.Items(i))
                End If
            Next i
            .Items.Clear()
            For i = 0 To newList1.Count - 1
                .Items.Add(newList1(i))
            Next i
        End With

        Dim newList2 As New System.Collections.ArrayList()
        With ListBox3
            For i = 0 To .Items.Count - 1
                If Not newList2.Contains(.Items(i)) Then
                    newList2.Add(.Items(i))
                End If
            Next i
            .Items.Clear()
            For i = 0 To newList2.Count - 1
                .Items.Add(newList2(i))
            Next i
        End With





        xlapp.ScreenUpdating = True  '恢复屏幕刷新
        xlapp.Calculation = -4105  '自动计算






        'ListBox1.Items.Add(2)
        'xlapp.Range("a1").Value = ListBox1.Items(1)
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim rng As Excel.Range, objArr As Object, i As Integer = 0
        rng = xlapp.InputBox("请指定放置的起始单元格", "区域", , , , , , 8) '弹出一个输入框让用户选择区域

        For i = 1 To ListBox1.Items.Count
            ReDim Preserve objArr(0 To i - 1)      '重置数组上标,这里的i要注意是连续执行的,执行成功的i跟不成功的i都累积
            objArr(i - 1) = ListBox1.Items(i - 1)      '空值,可能省略关系也不大,默认为空值
        Next

        rng.Resize(ListBox1.Items.Count, 1).Value = xlapp.WorksheetFunction.Transpose(objArr)
        'rng.Resize(ListBox1.Items.Count, 1).Value = xlapp.WorksheetFunction.Transpose(objArr)
        'rng.Resize(ListBox1.Items.Count, 1).Value = xlapp.WorksheetFunction.Transpose(objArr)
        'xlapp.Range("a1").Value = ListBox1.Items(1)
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim rng As Excel.Range, objArr As Object, i As Integer = 0
        rng = xlapp.InputBox("请指定放置的起始单元格", "区域", , , , , , 8) '弹出一个输入框让用户选择区域

        For i = 1 To ListBox2.Items.Count
            ReDim Preserve objArr(0 To i - 1)      '重置数组上标,这里的i要注意是连续执行的,执行成功的i跟不成功的i都累积
            objArr(i - 1) = ListBox2.Items(i - 1)      '空值,可能省略关系也不大,默认为空值
        Next

        rng.Resize(ListBox2.Items.Count, 1).Value = xlapp.WorksheetFunction.Transpose(objArr)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim rng As Excel.Range, objArr As Object, i As Integer = 0
        rng = xlapp.InputBox("请指定放置的起始单元格", "区域", , , , , , 8) '弹出一个输入框让用户选择区域

        For i = 1 To ListBox3.Items.Count
            ReDim Preserve objArr(0 To i - 1)      '重置数组上标,这里的i要注意是连续执行的,执行成功的i跟不成功的i都累积
            objArr(i - 1) = ListBox3.Items(i - 1)      '空值,可能省略关系也不大,默认为空值
        Next

        rng.Resize(ListBox3.Items.Count, 1).Value = xlapp.WorksheetFunction.Transpose(objArr)
    End Sub


End Class