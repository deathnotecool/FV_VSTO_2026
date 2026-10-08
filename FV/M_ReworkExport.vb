Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel

''' <summary>
''' 功能：返工数据导出模块
''' </summary>
Module M_ReworkExport

    ''' <summary>
    ''' 功能：导出用日期范围
    ''' </summary>
    Private datStart As Date
    Private datEnd As Date

    ''' <summary>
    ''' 功能：导出组装QC和油漆QC返工数据到 Excel（8 个 Sheet）
    ''' </summary>
    Public Sub ExportReworkToExcel()
        Try
            If Not AskDateRange() Then Return

            xlapp.ScreenUpdating = False

            Dim xlBook As Excel.Workbook = xlapp.Workbooks.Add()

            Dim s1 As Excel.Worksheet = CType(xlBook.Sheets(1), Excel.Worksheet)
            s1.Name = "组装明细"
            ExportAssySheet(s1)

            Dim s2 As Excel.Worksheet = AddSheet(xlBook, "油漆明细")
            ExportPaintSheet(s2)

            Dim s3 As Excel.Worksheet = AddSheet(xlBook, "组装-缺陷TOP")
            ExportDefectTop(s3, "tblReworkAssyDetail", "tblReworkAssyHead")

            Dim s4 As Excel.Worksheet = AddSheet(xlBook, "油漆-缺陷TOP")
            ExportDefectTop(s4, "tblReworkPaintDetail", "tblReworkPaintHead")

            Dim s5 As Excel.Worksheet = AddSheet(xlBook, "组装-检验员")
            ExportInspectorStat(s5, "tblReworkAssyDetail", "tblReworkAssyHead")

            Dim s6 As Excel.Worksheet = AddSheet(xlBook, "油漆-检验员")
            ExportInspectorStat(s6, "tblReworkPaintDetail", "tblReworkPaintHead")

            Dim s7 As Excel.Worksheet = AddSheet(xlBook, "组装-趋势")
            ExportTrend(s7, "tblReworkAssyDetail", "tblReworkAssyHead")

            Dim s8 As Excel.Worksheet = AddSheet(xlBook, "油漆-趋势")
            ExportTrend(s8, "tblReworkPaintDetail", "tblReworkPaintHead")

            s1.Activate()
            xlapp.ScreenUpdating = True
            MessageBox.Show("返工数据导出成功")
        Catch ex As Exception
            xlapp.ScreenUpdating = True
            MessageBox.Show("导出失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：弹日期范围窗，返回是否确认
    ''' </summary>
    Private Function AskDateRange() As Boolean
        Dim datMin As Date = Date.Today
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT MIN(dtmDate) FROM tblReworkAssyHead WHERE blnIsDeleted=False", conn)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    datMin = CDate(result)
                End If
            End Using
        End Using

        Dim f As New L_DateRange()
        f.StartDate = datMin
        f.EndDate = Date.Today

        If f.ShowDialog() <> DialogResult.OK Then Return False

        datStart = f.StartDate.Date
        datEnd = f.EndDate.Date.AddDays(1).AddSeconds(-1)
        Return True
    End Function

    ''' <summary>
    ''' 功能：加 Sheet
    ''' </summary>
    Private Function AddSheet(ByVal xlBook As Excel.Workbook, ByVal strName As String) As Excel.Worksheet
        Dim s As Excel.Worksheet = CType(xlBook.Sheets.Add(After:=xlBook.Sheets(xlBook.Sheets.Count)), Excel.Worksheet)
        s.Name = strName
        Return s
    End Function

    ''' <summary>
    ''' 功能：导出组装明细
    ''' </summary>
    Private Sub ExportAssySheet(ByVal xlSheet As Excel.Worksheet)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT DateValue(h.dtmDate) AS dtmDate, h.strShift, h.strInspector, h.lngTotalCheck, " &
            "d.strSerialNo, d.strDrawingNo, d.strCustomerPartNo, " &
            "d.strDefectType, d.strHandleType, d.lngQty, d.memRemark " &
            "FROM tblReworkAssyHead h " &
            "LEFT JOIN tblReworkAssyDetail d ON h.lngID = d.lngHeadID " &
            "WHERE h.blnIsDeleted = False AND h.dtmDate >= ? AND h.dtmDate <= ? " &
            "ORDER BY h.dtmDate DESC, h.lngID DESC, d.lngID"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.SelectCommand.Parameters.Add("p1", OleDbType.Date).Value = datStart
                da.SelectCommand.Parameters.Add("p2", OleDbType.Date).Value = datEnd
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"日期", "班次", "检验员", "总检验数", "系列号", "图号", "客户品号", "缺陷类型", "处理方式", "数量", "备注"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub

    ''' <summary>
    ''' 功能：导出油漆明细
    ''' </summary>
    Private Sub ExportPaintSheet(ByVal xlSheet As Excel.Worksheet)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT DateValue(h.dtmDate) AS dtmDate, h.strShift, h.strInspector, h.lngTotalCheck, " &
            "d.strSerialNo, d.strDrawingNo, d.strCustomerPartNo, " &
            "d.strDefectType, d.strHandleType, d.lngQty, d.memRemark " &
            "FROM tblReworkAssyHead h " &
            "LEFT JOIN tblReworkAssyDetail d ON h.lngID = d.lngHeadID " &
            "WHERE h.blnIsDeleted = False AND h.dtmDate >= ? AND h.dtmDate <= ? " &
            "ORDER BY h.dtmDate DESC, h.lngID DESC, d.lngID"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.SelectCommand.Parameters.Add("p1", OleDbType.Date).Value = datStart
                da.SelectCommand.Parameters.Add("p2", OleDbType.Date).Value = datEnd
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"日期", "班次", "检验员", "检验总数", "检查类型", "系列号", "图号", "客户品号", "缺陷类型", "处理方式", "数量", "备注"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub

    ''' <summary>
    ''' 功能：导出缺陷TOP
    ''' </summary>
    Private Sub ExportDefectTop(ByVal xlSheet As Excel.Worksheet, ByVal strDetail As String, ByVal strHead As String)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT d.strDefectType, COUNT(*) AS 出现次数, SUM(d.lngQty) AS 总数量, " &
            "SUM(IIF(d.strHandleType='抛磨放行', d.lngQty, 0)) AS 抛磨放行, " &
            "SUM(IIF(d.strHandleType='放行', d.lngQty, 0)) AS 放行, " &
            "SUM(IIF(d.strHandleType='返工', d.lngQty, 0)) AS 返工, " &
            "SUM(IIF(d.strHandleType='报废', d.lngQty, 0)) AS 报废 " &
            "FROM " & strDetail & " d " &
            "INNER JOIN " & strHead & " h ON d.lngHeadID = h.lngID " &
            "WHERE h.blnIsDeleted = False AND h.dtmDate >= ? AND h.dtmDate <= ? " &
            "GROUP BY d.strDefectType " &
            "ORDER BY SUM(d.lngQty) DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.SelectCommand.Parameters.Add("p1", OleDbType.Date).Value = datStart
                da.SelectCommand.Parameters.Add("p2", OleDbType.Date).Value = datEnd
                da.Fill(dt)
            End Using
        End Using

        Dim strSubTitle As String = "统计日期：" & datStart.ToString("yyyy/MM/dd") & " ~ " & datEnd.ToString("yyyy/MM/dd")
        Dim strHeaders() As String = {"缺陷类型", "出现次数", "总数量", "抛磨放行", "放行", "返工", "报废"}
        WriteSheet(xlSheet, dt, strHeaders, strSubTitle)
        AddBarChart(xlSheet, dt.Rows.Count, "缺陷TOP（按数量）", strSubTitle)



    End Sub

    ''' <summary>
    ''' 功能：导出检验员统计
    ''' </summary>
    Private Sub ExportInspectorStat(ByVal xlSheet As Excel.Worksheet, ByVal strDetail As String, ByVal strHead As String)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT h.strInspector AS 检验员, COUNT(*) AS 检出次数, SUM(d.lngQty) AS 检出数量, " &
            "SUM(IIF(d.strHandleType='抛磨放行', d.lngQty, 0)) AS 抛磨放行, " &
            "SUM(IIF(d.strHandleType='放行', d.lngQty, 0)) AS 放行, " &
            "SUM(IIF(d.strHandleType='返工', d.lngQty, 0)) AS 返工, " &
            "SUM(IIF(d.strHandleType='报废', d.lngQty, 0)) AS 报废 " &
            "FROM " & strDetail & " d " &
            "INNER JOIN " & strHead & " h ON d.lngHeadID = h.lngID " &
            "WHERE h.blnIsDeleted = False AND h.dtmDate >= ? AND h.dtmDate <= ? " &
            "GROUP BY h.strInspector " &
            "ORDER BY SUM(d.lngQty) DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.SelectCommand.Parameters.Add("p1", OleDbType.Date).Value = datStart
                da.SelectCommand.Parameters.Add("p2", OleDbType.Date).Value = datEnd
                da.Fill(dt)
            End Using
        End Using

        Dim strSubTitle As String = "统计日期：" & datStart.ToString("yyyy/MM/dd") & " ~ " & datEnd.ToString("yyyy/MM/dd")
        Dim strHeaders() As String = {"检验员", "检出次数", "检出数量", "抛磨放行", "放行", "返工", "报废"}
        WriteSheet(xlSheet, dt, strHeaders, strSubTitle)
        AddBarChart(xlSheet, dt.Rows.Count, "检验员检出统计", strSubTitle)




    End Sub

    ''' <summary>
    ''' 功能：导出按日趋势
    ''' </summary>
    Private Sub ExportTrend(ByVal xlSheet As Excel.Worksheet, ByVal strDetail As String, ByVal strHead As String)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT DateValue(h.dtmDate) AS 日期, SUM(d.lngQty) AS 不合格数, COUNT(*) AS 检出次数 " &
            "FROM " & strDetail & " d " &
            "INNER JOIN " & strHead & " h ON d.lngHeadID = h.lngID " &
            "WHERE h.blnIsDeleted = False AND h.dtmDate >= ? AND h.dtmDate <= ? " &
            "GROUP BY DateValue(h.dtmDate) " &
            "ORDER BY DateValue(h.dtmDate) DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.SelectCommand.Parameters.Add("p1", OleDbType.Date).Value = datStart
                da.SelectCommand.Parameters.Add("p2", OleDbType.Date).Value = datEnd
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"日期", "不合格数", "检出次数"}
        WriteSheet(xlSheet, dt, strHeaders)
        AddComboChart(xlSheet, dt.Rows.Count, "按日不合格数与检出次数趋势")
    End Sub

    ''' <summary>
    ''' 功能：把 DataTable 写入 Sheet，可选副标题
    ''' </summary>
    Private Sub WriteSheet(ByVal xlSheet As Excel.Worksheet, ByVal dt As DataTable, ByVal strHeaders() As String, Optional ByVal strSubTitle As String = "")
        Dim intStartRow As Integer = 1

        ' 有副标题，先写一行
        If strSubTitle <> "" Then
            xlSheet.Cells(1, 1) = strSubTitle
            Dim rngSub As Excel.Range = xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, strHeaders.Length))
            rngSub.Font.Size = 9
            rngSub.Font.Italic = True
            rngSub.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray)
            intStartRow = 2
        End If

        ' 表头
        For i As Integer = 0 To strHeaders.Length - 1
            xlSheet.Cells(intStartRow, i + 1) = strHeaders(i)
        Next

        Dim rngHeader As Excel.Range = xlSheet.Range(xlSheet.Cells(intStartRow, 1), xlSheet.Cells(intStartRow, strHeaders.Length))
        rngHeader.Font.Bold = True
        rngHeader.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

        ' 数据
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dr As DataRow = dt.Rows(i)
            Dim intRow As Integer = intStartRow + 1 + i
            For j As Integer = 0 To dt.Columns.Count - 1
                xlSheet.Cells(intRow, j + 1) = If(IsDBNull(dr(j)), "", dr(j).ToString())
            Next
        Next

        xlSheet.Columns.AutoFit()
    End Sub

    ''' <summary>
    ''' 功能：生成柱状图（美化，可选副标题影响数据起始行）
    ''' </summary>
    Private Sub AddBarChart(ByVal xlSheet As Excel.Worksheet, ByVal intRowCount As Integer, ByVal strTitle As String, Optional ByVal strSubTitle As String = "")
        If intRowCount = 0 Then Return

        Dim intStartRow As Integer = If(strSubTitle = "", 1, 2)
        Dim rngData As Excel.Range = xlSheet.Range("A" & intStartRow & ":B" & (intRowCount + intStartRow))
        Dim chtObj As Excel.ChartObject = xlSheet.ChartObjects.Add(Left:=400, Top:=20, Width:=550, Height:=320)

        With chtObj.Chart
            .SetSourceData(Source:=rngData)
            .ChartType = Excel.XlChartType.xlColumnClustered
            .HasTitle = True
            .ChartTitle.Text = strTitle
            .ChartTitle.Font.Size = 14
            .ChartTitle.Font.Name = "微软雅黑"
            .ChartTitle.Font.Bold = True

            .HasLegend = False

            Dim ser As Excel.Series = CType(.SeriesCollection(1), Excel.Series)
            ser.HasDataLabels = True
            ser.DataLabels.Font.Size = 10
            ser.DataLabels.NumberFormat = "0"
            ser.DataLabels.Font.Bold = True
            ser.DataLabels.Position = Excel.XlDataLabelPosition.xlLabelPositionOutsideEnd

            Dim ax As Excel.Axis = CType(.Axes(Excel.XlAxisType.xlValue), Excel.Axis)
            ax.HasMajorGridlines = True
            ax.MajorGridlines.Format.Line.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
        End With
    End Sub

    ''' <summary>
    ''' 功能：生成组合图（柱状不合格数 + 折线检出次数）
    ''' </summary>
    Private Sub AddComboChart(ByVal xlSheet As Excel.Worksheet, ByVal intRowCount As Integer, ByVal strTitle As String)
        If intRowCount = 0 Then Return

        Dim rngData As Excel.Range = xlSheet.Range("A1:C" & (intRowCount + 1))
        Dim chtObj As Excel.ChartObject = xlSheet.ChartObjects.Add(Left:=450, Top:=20, Width:=600, Height:=350)

        With chtObj.Chart
            .SetSourceData(Source:=rngData)

            ' 柱：不合格数，标签柱内
            Dim ser1 As Excel.Series = CType(.SeriesCollection(1), Excel.Series)
            ser1.ChartType = Excel.XlChartType.xlColumnClustered
            ser1.AxisGroup = Excel.XlAxisGroup.xlPrimary
            ser1.HasDataLabels = True
            ser1.DataLabels.NumberFormat = "0""个"""
            ser1.DataLabels.Font.Bold = True
            ser1.DataLabels.Font.Size = 9
            ser1.DataLabels.Position = Excel.XlDataLabelPosition.xlLabelPositionInsideEnd

            Dim grp As Excel.ChartGroup = CType(.ChartGroups(1), Excel.ChartGroup)
            grp.GapWidth = 150

            ' 线：检出次数，标签线下方
            Dim ser2 As Excel.Series = CType(.SeriesCollection(2), Excel.Series)
            ser2.ChartType = Excel.XlChartType.xlLine
            ser2.AxisGroup = Excel.XlAxisGroup.xlSecondary
            ser2.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            ser2.Border.Weight = 2
            ser2.HasDataLabels = True
            ser2.DataLabels.NumberFormat = "0""次"""
            ser2.DataLabels.Font.Bold = True
            ser2.DataLabels.Font.Size = 9
            ser2.DataLabels.Position = Excel.XlDataLabelPosition.xlLabelPositionBelow

            .HasTitle = True
            .ChartTitle.Text = strTitle
            .ChartTitle.Font.Size = 14
            .ChartTitle.Font.Name = "微软雅黑"
            .ChartTitle.Font.Bold = True

            .HasLegend = True
            .Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom

            Dim ax1 As Excel.Axis = CType(.Axes(Excel.XlAxisType.xlValue), Excel.Axis)
            ax1.HasMajorGridlines = True
            ax1.MajorGridlines.Format.Line.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
            ax1.HasTitle = True
            ax1.AxisTitle.Text = "不合格数"
            ax1.AxisTitle.Font.Size = 10

            Dim ax2 As Excel.Axis = CType(.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlSecondary), Excel.Axis)
            ax2.HasTitle = True
            ax2.AxisTitle.Text = "检出次数"
            ax2.AxisTitle.Font.Size = 10
            ax2.MajorUnit = 1
            ax2.MinimumScale = 0
        End With
    End Sub

End Module