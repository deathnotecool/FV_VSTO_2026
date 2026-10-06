Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel

''' <summary>
''' 功能：返工数据导出模块
''' </summary>
Module M_ReworkExport

    ''' <summary>
    ''' 功能：导出组装QC和油漆QC返工数据到 Excel（两个 Sheet）
    ''' </summary>
    Public Sub ExportReworkToExcel()
        Try
            Dim xlApp As Excel.Application = Globals.ThisAddIn.Application
            Dim xlBook As Excel.Workbook = xlApp.Workbooks.Add()
            Dim xlSheetAssy As Excel.Worksheet = CType(xlBook.Sheets(1), Excel.Worksheet)
            xlSheetAssy.Name = "组装QC"

            ' 导出组装
            ExportAssySheet(xlSheetAssy)

            ' 加第二个 Sheet
            Dim xlSheetPaint As Excel.Worksheet = CType(xlBook.Sheets.Add(After:=xlBook.Sheets(xlBook.Sheets.Count)), Excel.Worksheet)
            xlSheetPaint.Name = "油漆QC"

            ' 导出油漆
            ExportPaintSheet(xlSheetPaint)

            ' 激活第一个 Sheet
            xlSheetAssy.Activate()

            MessageBox.Show("返工数据导出成功")
        Catch ex As Exception
            MessageBox.Show("导出失败：" & ex.Message)
        End Try
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
            "d.strSerialNo, d.strModel, d.strDefectType, d.strHandleType, d.lngQty, d.memRemark " &
            "FROM tblReworkAssyHead h " &
            "LEFT JOIN tblReworkAssyDetail d ON h.lngID = d.lngHeadID " &
            "WHERE h.blnIsDeleted = False " &
            "ORDER BY h.dtmDate DESC, h.lngID DESC, d.lngID"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        ' ★ 这里 ★
        Dim strHeaders() As String = {"日期", "班次", "检验员", "总检验数", "系列号", "型号", "缺陷类型", "处理方式", "数量", "备注"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub

    Private Sub ExportPaintSheet(ByVal xlSheet As Excel.Worksheet)
        Dim dt As New DataTable()
        Dim strSql As String =
            "SELECT h.dtmDate, h.strShift, h.strInspector, h.strCheckType, " &
            "h.lngIncomingCheck, h.lngIncomingFail, h.lngPaintTotal, h.lngPaintFail, " &
            "d.strSerialNo, d.strModel, d.strDefectType, d.strHandleType, d.lngQty, d.memRemark " &
            "FROM tblReworkPaintHead h " &
            "LEFT JOIN tblReworkPaintDetail d ON h.lngID = d.lngHeadID " &
            "WHERE h.blnIsDeleted = False " &
            "ORDER BY h.dtmDate DESC, h.lngID DESC, d.lngID"

        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter(strSql, conn)
                da.Fill(dt)
            End Using
        End Using

        ' ★ 这里 ★
        Dim strHeaders() As String = {"日期", "班次", "检验员", "检查类型", "来料抽检", "来料不合格", "喷漆总数", "油漆不合格", "系列号", "型号", "缺陷类型", "处理方式", "数量", "备注"}
        WriteSheet(xlSheet, dt, strHeaders)
    End Sub

End Module
