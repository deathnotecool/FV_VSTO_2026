Public Class WIN140117_批量转换PDF
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        On Error Resume Next  '程序出错时继续执行
        xlapp.ScreenUpdating = False   '关闭屏幕更新，提升速度

        Dim pathStr As String, objWordApp As Object, Str As String, FileCount As Integer = 0   '声明变量  
        Dim strExtensionName As String, IsClose As Boolean, isKill As MsgBoxResult


        '显示一个选择文件夹的对话框,选择了文件夹获取名称，否则退出
        With xlapp.FileDialog(4)
            If .Show = -1 Then pathStr = .SelectedItems(0) Else xlapp.ScreenUpdating = True : Exit Sub
        End With

        If Strings.Right(pathStr, 1) <> "\" Then pathStr = pathStr & "\"  '如果变量Pathstr不以“\”结尾则追加“\”,C盘是带"\"

        '给变量赋值为选择的Yes或者No.
        isKill = MsgBox("转换成功后需要删除原文件吗?", vbDefaultButton2 + vbYesNo + vbQuestion, "是否删除原文件")

        '获取Word应用程序
        If rbWord.Checked Then
            objWordApp = GetObject(, "Word.Application")
            If Err.Number <> 0 Then                                   '如果获取不成功
                IsClose = False                                       '将变量IsClose赋值为False(当前是没有打开的WORD 程序,并判定最后要不要退出程序用).
                objWordApp = CreateObject("Word.Application")         '建一个Word对象(程序)
            Else
                IsClose = True                                        '将变量赋值为true
            End If

            '开始查找文件，格式为所有Word文件
            Str = Dir(pathStr & "*.do*")
            Do While Len(Str) > 0             '只要获取的文件名称长度大于0
                FileCount = FileCount + 1  '累加变量，该变量代表文件数量
                objWordApp.Documents.Open(pathStr & Str)                                             '打开文档
                strExtensionName = CreateObject("Scripting.FileSystemObject").getextensionname(Str)  '获取文档的扩展名

                '开始进行格式转换，两个参数分别表示文件名称和格式(wdExportFormatPDF表示PDF格式，wdExportFormatXPS表示XPS格式)
                objWordApp.ActiveDocument.ExportAsFixedFormat(pathStr & Replace(Str, strExtensionName, "pdf"), 17)
                objWordApp.Documents(Str).Close(False)       '关闭文档(不保存更改)


                If isKill = vbYes Then Kill(pathStr & Str)   '如果用户选择了“是”,那么使用Kill语句删除Word文档
                Str = Dir()                                  '查找下一个
            Loop
            If IsClose = False Then objWordApp.Quit(False)   '如果变量IsClose值为False，那么关闭Word应用程序
            xlapp.ScreenUpdating = True                      '恢复屏幕更新
            MsgBox("成功转换了" & FileCount & "个Word文档", vbOKOnly, "友情提示")
        Else

            Dim ExtensionName As String   '声明变量
            Str = Dir(pathStr & "*.xl*")   '开始查找文件，格式为所有Excel文件

            Do While Len(Str) > 0             '只要获取的文件名称长度大于0
                FileCount = FileCount + 1  '累加变量，该变量代表文件数量
                xlapp.Workbooks.Open(pathStr & Str) '打开文档
                ExtensionName = CreateObject("Scripting.FileSystemObject").getextensionname(Str)  '获取文档的扩展名
                '开始进行格式转换，两个参数分别表示格式和文件名称
                xlapp.ActiveWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, pathStr & Replace(Str, ExtensionName, "pdf"))
                xlapp.ActiveWorkbook.Close(False)       '关闭文档

                '如果用户选择了“是”,那么使用Kill语句删除Excel文档
                If isKill = vbYes Then Kill(pathStr & Str)
                Str = Dir()  '查找下一个
            Loop
            xlapp.ScreenUpdating = True       '恢复屏幕更新
            MsgBox("成功转换了" & FileCount & "个Excel文档", vbOKOnly, "友情提示")
        End If


    End Sub

    Private Sub WIN140117_批量转换PDF_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnConversionPDF.Enabled = True     '重新启用按钮
        Me.Close()        '关闭窗体
    End Sub
End Class