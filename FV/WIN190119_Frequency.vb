Public Class WIN190119_Frequency
    Dim rng As Excel.Range, rngFirst As Excel.Range '模块调用变量
    Private Sub btnDetermineArea_Click(sender As Object, e As EventArgs) Handles btnDetermineArea.Click
        On Error Resume Next

        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        'Call 备份(Targetsht, TargetRng)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True '激活撤销按钮可用
        ''____________________备份数据、记录区域___________________________

        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True

        Dim strRangeAddress As String
        strRangeAddress = xlapp.Selection.Address
        txtAnalysisArea.Text = strRangeAddress
        rng = xlapp.InputBox("Select the range of analysis area", "Range area", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        txtAnalysisArea.Text = rng.Address  '选择区域的地址赋值给显示文本框
        rng = xlapp.Range(txtAnalysisArea.Text) '通过显示文本框地址,单元格范围重新赋值给rng
        '        
        txtAnalysisArea.Text = rng.Address
        rngFirst = xlapp.InputBox("Select  one cell around 3 columns which are empty", "Range area", , , , , , 8) '弹出一个输入框让用户选择区域

        SetSED() '定义左边开始/结束
        AddChart() '插入图表

        ''.......................................................................................
        'xlapp.OnUndo("撤消[频次分析工具结果]", "撤消") '这里代码调用,并激活的是FV.xlam加载项的撤销方法

        ''.......................................................................................
        btnExit_Click(Nothing, Nothing)
    End Sub
    Sub SetSED()
        Dim intStart As Double, intEnd As Double, btyStep As Double, arrRange() As Object, i As Byte, dblAccumulate As Double

        '转换数据类型,除了以下方式还有Ctype 函数: CType(testNumber, Single)
        intStart = CDbl(txtStartValue.Text)
        intEnd = CDbl(txtEndValue.Text)
        btyStep = CDbl(txtStepValue.Text)
        i = 0

        '累加器＜最终数字前,执行...
        Do
            If dblAccumulate < intEnd Then
                i = i + 1

                '重定义数组,并给数组每一个元素赋值
                ReDim Preserve arrRange(0 To i - 1)
                dblAccumulate = intStart + btyStep * (i - 1)
                arrRange(i - 1) = dblAccumulate
            Else
                Exit Do
            End If
        Loop
        rngFirst.Offset(-1, 0).Resize(1, 2).Value = {"Section", "Frequency"}    '填写标题
        rngFirst.Resize(i).Value = xlapp.WorksheetFunction.Transpose(arrRange)
        rngFirst.Offset(, 1).Resize(i).FormulaArray = "=FREQUENCY(" & rng.Address & "," & rngFirst.Resize(i).Address & ")"  '相当于数组山键
        rngFirst.Offset(, 1).Resize(i).Value = rngFirst.Offset(, 1).Resize(i).Value '公式转换成值...
        rngFirst.CurrentRegion.EntireColumn.AutoFit()      '自动调整列宽
        rngFirst.CurrentRegion.Borders.LineStyle = 1       '加框线
        rngFirst.CurrentRegion.HorizontalAlignment = -4108 '水平中间放置
        rngFirst.CurrentRegion.VerticalAlignment = -4108   '垂直中间放置
    End Sub

    Sub AddChart()
        xlapp.ScreenUpdating = False     '恢复屏幕刷新
        On Error Resume Next
        rngFirst.Select()
        xlapp.ActiveSheet.Shapes.AddChart.Select   '插入空白图表，并选中..
        xlapp.ActiveChart.ChartType = 51    '活动 图表为簇状柱形图..
        xlapp.ActiveChart.SetSourceData(Source:=xlapp.Range(xlapp.ActiveSheet.Name & "!" & xlapp.ActiveCell.CurrentRegion.Address)) '限定图表区域...
        xlapp.ActiveChart.SeriesCollection(1).Delete    '删除表系列1...
        xlapp.ActiveChart.SeriesCollection(1).Select    '选中系列1

        xlapp.ActiveChart.ChartArea.Copy()  '复制系列
        xlapp.ActiveChart.Paste()           '粘贴系列
        xlapp.ActiveChart.SeriesCollection(1).Select
        xlapp.ActiveChart.SeriesCollection(1).ChartType = 65 '系列改为曲线图..

        xlapp.ActiveChart.SeriesCollection(1).Select '选中第2系列
        With xlapp.Selection
            .MarkerStyle = 8
            .MarkerSize = 4
        End With
        xlapp.ActiveChart.SetElement(208)   '显示数据标签在顶端..
        'xlapp.ActiveChart.Legend.LegendEntries(1).Select   '选中图例
        'xlapp.Selection.Delete
        xlapp.ActiveChart.ChartTitle.Select()
        xlapp.Selection.Format.TextFrame2.TextRange.Characters.Text = "Frequency Analysis" '标题标签修改
        xlapp.ActiveChart.Axes(2).Select '
        xlapp.ActiveChart.Axes(2).MajorUnit = 1
        xlapp.ActiveChart.SeriesCollection(2).XValues = "" & "=" & "'" & xlapp.ActiveSheet.Name & "'" & "!" & rngFirst.Resize(rngFirst.CurrentRegion.Rows.Count - 1).Address & ""
        xlapp.ScreenUpdating = True     '回复屏幕刷新
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Globals.Ribbons.Ribbon1.btnFrequency.Enabled = True     '重新启用按钮
        Me.Close()        '关闭窗体
    End Sub



    Private Sub WIN190119_Frequency_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnFrequency.Enabled = True     '重新启用按钮
    End Sub


End Class