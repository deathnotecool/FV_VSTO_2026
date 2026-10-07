Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel

''' <summary>
''' 功能：返工数据导出模块
''' </summary>
Module M_ReworkExport

    ''' <summary>
    ''' 功能：导出返工数据到 Excel（8 个 Sheet）
    ''' </summary>
    Public Sub ExportReworkToExcel()
        Try
            Dim xlApp As Excel.Application = Globals.ThisAddIn.Application
            Dim xlBook As Excel.Workbook = xlApp.Workbooks.Add()

            ' 1. 组装明细
            Dim s1 As Excel.Worksheet = CType(xlBook.Sheets(1), Excel.Worksheet)
            s1.Name = "组装明细"
            ExportAssySheet(s1)

            ' 2. 油漆明细
            Dim s2 As Excel.Worksheet = AddSheet(xlBook, "油漆明细")
            ExportPaintSheet(s2)

            ' 3. 组装-缺陷TOP
            Dim s3 As Excel.Worksheet = AddSheet(xlBook, "组装-缺陷TOP")
            ExportDefectTop(s3, "tblReworkAssyDetail", "tblReworkAssyHead", "lngTotalCheck")

            ' 4. 油漆-缺陷TOP
            Dim s4 As Excel.Worksheet = AddSheet(xlBook, "油漆-缺陷TOP")
            ExportDefectTop(s4, "tblReworkPaintDetail", "tblReworkPaintHead", "lngTotalQty")

            ' 5. 组装-检验员
            Dim s5 As Excel.Worksheet = AddSheet(xlBook, "组装-检验员")
            ExportInspectorStat(s5, "tblReworkAssyDetail", "tblReworkAssyHead")

            ' 6. 油漆-检验员
            Dim s6 As Excel.Worksheet = AddSheet(xlBook, "油漆-检验员")
            ExportInspectorStat(s6, "tblReworkPaintDetail", "tblReworkPaintHead")

            ' 7. 组装-趋势
            Dim s7 As Excel.Worksheet = AddSheet(xlBook, "组装-趋势")
            ExportTrend(s7, "tblReworkAssyDetail", "tblReworkAssyHead", "lngTotalCheck")

            ' 8. 油漆-趋势
            Dim s8 As Excel.Worksheet = AddSheet(xlBook, "油漆-趋势")
            ExportTrend(s8, "tblReworkPaintDetail", "tblReworkPaintHead", "lngTotalQty")

            s1.Activate()
            MessageBox.Show("返工数据导出成功")
        Catch ex As Exception
            MessageBox.Show("导出失败：" & ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' 功能：导出缺陷 TOP 汇总
    ''' </summary>
    Private Sub ExportDefectTop(ByVal xlSheet As Excel.Worksheet, ByVal strDetail As String, ByVal strHead As String, ByVal strTotalCol As String)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT d.strDefectType, COUNT(*) AS 出现次数, SUM(d.lngQty) AS 总数量, " &
            "SUM(IIF(d.strHandleType='抛磨放行', d.lngQty, 0)) AS 抛磨放行, " &
            "SUM(IIF(d.strHandleType='放行', d.lngQty, 0)) AS 放行, " &
            "SUM(IIF(d.strHandleType='返工', d.lngQty, 0)) AS 返工, " &
            "SUM(IIF(d.strHandleType='报废', d.lngQty, 0)) AS 报废 " &
            "FROM " & strDetail & " d " &
            "INNER JOIN " & strHead & " h ON d.lngHeadID = h.lngID " &
            "WHERE h.blnIsDeleted = False " &
            "GROUP BY d.strDefectType " &
            "ORDER BY SUM(d.lngQty) DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"缺陷类型", "出现次数", "总数量", "抛磨放行", "放行", "返工", "报废"}
        WriteSheet(xlSheet, dt, strHeaders)
        AddBarChart(xlSheet, dt.Rows.Count, "缺陷TOP（按数量）")


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
            "WHERE h.blnIsDeleted = False " &
            "GROUP BY h.strInspector " &
            "ORDER BY SUM(d.lngQty) DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"检验员", "检出次数", "检出数量", "抛磨放行", "放行", "返工", "报废"}
        WriteSheet(xlSheet, dt, strHeaders)

        AddBarChart(xlSheet, dt.Rows.Count, "检验员检出统计")
    End Sub

    ''' <summary>
    ''' 功能：导出按日趋势（只统计不合格数）
    ''' </summary>
    Private Sub ExportTrend(ByVal xlSheet As Excel.Worksheet, ByVal strDetail As String, ByVal strHead As String, ByVal strTotalCol As String)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT h.dtmDate AS 日期, SUM(d.lngQty) AS 不合格数, COUNT(*) AS 检出次数 " &
            "FROM " & strDetail & " d " &
            "INNER JOIN " & strHead & " h ON d.lngHeadID = h.lngID " &
            "WHERE h.blnIsDeleted = False " &
            "GROUP BY h.dtmDate " &
            "ORDER BY h.dtmDate DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"日期", "不合格数", "检出次数"}
        WriteSheet(xlSheet, dt, strHeaders)

        AddLineChart(xlSheet, dt.Rows.Count, "按日不合格数趋势")
    End Sub











    ''' <summary>
    ''' 功能：加 Sheet
    ''' </summary>
    Private Function AddSheet(ByVal xlBook As Excel.Workbook, ByVal strName As String) As Excel.Worksheet
        Dim s As Excel.Worksheet = CType(xlBook.Sheets.Add(After:=xlBook.Sheets(xlBook.Sheets.Count)), Excel.Worksheet)
        s.Name = strName
        Return s
    End Function


    ''' <summary>
    ''' 功能：导出组装缺陷汇总到 Sheet
    ''' </summary>
    Private Sub ExportAssySummary(ByVal xlSheet As Excel.Worksheet)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT strDefectType, COUNT(*) AS 出现次数, SUM(lngQty) AS 总数量, " &
            "SUM(IIF(strHandleType='抛磨放行', lngQty, 0)) AS 抛磨放行, " &
            "SUM(IIF(strHandleType='放行', lngQty, 0)) AS 放行, " &
            "SUM(IIF(strHandleType='返工', lngQty, 0)) AS 返工, " &
            "SUM(IIF(strHandleType='报废', lngQty, 0)) AS 报废 " &
            "FROM tblReworkAssyDetail d " &
            "INNER JOIN tblReworkAssyHead h ON d.lngHeadID = h.lngID " &
            "WHERE h.blnIsDeleted = False " &
            "GROUP BY strDefectType " &
            "ORDER BY SUM(lngQty) DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"缺陷类型", "出现次数", "总数量", "抛磨放行", "放行", "返工", "报废"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub

    ''' <summary>
    ''' 功能：导出油漆缺陷汇总到 Sheet
    ''' </summary>
    Private Sub ExportPaintSummary(ByVal xlSheet As Excel.Worksheet)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT strDefectType, COUNT(*) AS 出现次数, SUM(lngQty) AS 总数量, " &
            "SUM(IIF(strHandleType='抛磨放行', lngQty, 0)) AS 抛磨放行, " &
            "SUM(IIF(strHandleType='放行', lngQty, 0)) AS 放行, " &
            "SUM(IIF(strHandleType='返工', lngQty, 0)) AS 返工, " &
            "SUM(IIF(strHandleType='报废', lngQty, 0)) AS 报废 " &
            "FROM tblReworkPaintDetail d " &
            "INNER JOIN tblReworkPaintHead h ON d.lngHeadID = h.lngID " &
            "WHERE h.blnIsDeleted = False " &
            "GROUP BY strDefectType " &
            "ORDER BY SUM(lngQty) DESC"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"缺陷类型", "出现次数", "总数量", "抛磨放行", "放行", "返工", "报废"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub

    ''' <summary>
    ''' 功能：把 DataTable 写入 Sheet
    ''' </summary>
    Private Sub WriteSheet(ByVal xlSheet As Excel.Worksheet, ByVal dt As DataTable, ByVal strHeaders() As String)
        ' 写表头
        For i As Integer = 0 To strHeaders.Length - 1
            xlSheet.Cells(1, i + 1) = strHeaders(i)
        Next

        Dim rngHeader As Excel.Range = xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, strHeaders.Length))
        rngHeader.Font.Bold = True
        rngHeader.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

        ' 写数据
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dr As DataRow = dt.Rows(i)
            Dim intRow As Integer = i + 2
            For j As Integer = 0 To dt.Columns.Count - 1
                xlSheet.Cells(intRow, j + 1) = If(IsDBNull(dr(j)), "", dr(j).ToString())
            Next
        Next

        xlSheet.Columns.AutoFit()
    End Sub

    Private Sub ExportAssySheet(ByVal xlSheet As Excel.Worksheet)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT h.dtmDate, h.strShift, h.strInspector, h.lngTotalCheck, " &
            "d.strSerialNo, d.strDrawingNo, d.strCustomerPartNo, " &
            "d.strDefectType, d.strHandleType, d.lngQty, d.memRemark " &
            "FROM tblReworkAssyHead h " &
            "LEFT JOIN tblReworkAssyDetail d ON h.lngID = d.lngHeadID " &
            "WHERE h.blnIsDeleted = False " &
            "ORDER BY h.dtmDate DESC, h.lngID DESC, d.lngID"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"日期", "班次", "检验员", "总检验数", "系列号", "图号", "客户品号", "缺陷类型", "处理方式", "数量", "备注"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub

    Private Sub ExportPaintSheet(ByVal xlSheet As Excel.Worksheet)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT h.dtmDate, h.strShift, h.strInspector, h.lngTotalQty, " &
            "d.strCheckType, d.strSerialNo, d.strDrawingNo, d.strCustomerPartNo, " &
            "d.strDefectType, d.strHandleType, d.lngQty, d.memRemark " &
            "FROM tblReworkPaintHead h " &
            "LEFT JOIN tblReworkPaintDetail d ON h.lngID = d.lngHeadID " &
            "WHERE h.blnIsDeleted = False " &
            "ORDER BY h.dtmDate DESC, h.lngID DESC, d.lngID"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        Dim strHeaders() As String = {"日期", "班次", "检验员", "检验总数", "检查类型", "系列号", "图号", "客户品号", "缺陷类型", "处理方式", "数量", "备注"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub


    ''' <summary>
    ''' 功能：生成柱状图（美化）
    ''' </summary>
    Private Sub AddBarChart(ByVal xlSheet As Excel.Worksheet, ByVal intRowCount As Integer, ByVal strTitle As String)
        If intRowCount = 0 Then Return

        Dim rngData As Excel.Range = xlSheet.Range("A1:B" & (intRowCount + 1))
        Dim chtObj As Excel.ChartObject = xlSheet.ChartObjects.Add(Left:=400, Top:=20, Width:=550, Height:=320)

        With chtObj.Chart
            .SetSourceData(Source:=rngData)
            .ChartType = Excel.XlChartType.xlColumnClustered
            .HasTitle = True
            .ChartTitle.Text = strTitle
            .ChartTitle.Font.Size = 14
            .ChartTitle.Font.Name = "微软雅黑"
            .ChartTitle.Font.Bold = True

            ' 不要图例
            .HasLegend = False

            ' 数据标签
            Dim ser As Excel.Series = CType(.SeriesCollection(1), Excel.Series)
            ser.HasDataLabels = True
            ser.DataLabels.Font.Size = 10
            ser.DataLabels.Position = Excel.XlDataLabelPosition.xlLabelPositionOutsideEnd

            ' 网格线淡化
            Dim ax As Excel.Axis = CType(.Axes(Excel.XlAxisType.xlValue), Excel.Axis)
            ax.HasMajorGridlines = True
            ax.MajorGridlines.Format.Line.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
        End With
    End Sub

    ''' <summary>
    ''' 功能：生成折线图（美化）
    ''' </summary>
    Private Sub AddLineChart(ByVal xlSheet As Excel.Worksheet, ByVal intRowCount As Integer, ByVal strTitle As String)
        If intRowCount = 0 Then Return

        Dim rngData As Excel.Range = xlSheet.Range("A1:B" & (intRowCount + 1))
        Dim chtObj As Excel.ChartObject = xlSheet.ChartObjects.Add(Left:=400, Top:=20, Width:=550, Height:=320)

        With chtObj.Chart
            .SetSourceData(Source:=rngData)
            .ChartType = Excel.XlChartType.xlLine
            .HasTitle = True
            .ChartTitle.Text = strTitle
            .ChartTitle.Font.Size = 14
            .ChartTitle.Font.Name = "微软雅黑"
            .ChartTitle.Font.Bold = True

            .HasLegend = False

            Dim ser As Excel.Series = CType(.SeriesCollection(1), Excel.Series)
            ser.HasDataLabels = True
            ser.DataLabels.Font.Size = 10
            ser.DataLabels.Position = Excel.XlDataLabelPosition.xlLabelPositionAbove
            ser.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SteelBlue)
            ser.Border.Weight = 2

            Dim ax As Excel.Axis = CType(.Axes(Excel.XlAxisType.xlValue), Excel.Axis)
            ax.HasMajorGridlines = True
            ax.MajorGridlines.Format.Line.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
        End With
    End Sub








End Module
