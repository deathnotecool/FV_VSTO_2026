Imports System.Drawing
Imports Microsoft.Office.Tools.Ribbon
Imports System.Windows.Forms

Public Class WIN191102_条形码
    '生成条形码
    'Shared Function MakeQRT(ByVal qrtext As String, Optional ByVal width As Integer = 230, Optional ByVal height As Integer = 90, Optional ByVal margin As Integer = 1) As Bitmap
    Shared Function MakeQRT(ByVal qrtext As String, Optional ByVal width As Integer = 240, Optional ByVal height As Integer = 90, Optional ByVal margin As Integer = 1) As Bitmap
        Dim writer As New ZXing.BarcodeWriter             '构建一个图像智能类
        writer.Format = ZXing.BarcodeFormat.CODE_128      '智能类图像格式设置为条形码
        Dim opt As New ZXing.QrCode.QrCodeEncodingOptions '构建一个条形码操作对象
        opt.DisableECI = True   '设置为True才可以调整编码
        opt.CharacterSet = "UTF-8"  '文本编码，建议设置为UTF-8
        opt.Width = width    '宽度
        opt.Height = height  '高度
        opt.Margin = margin  '边距，貌似不是像素格式，因此不宜设置过大
        writer.Options = opt '设置用于编码的选项容器
        Return writer.Write(qrtext) '内容写入智能类
    End Function

    '读取条形码
    Shared Function ReadQT(ByVal bmp As Bitmap) As String
        Dim reader As New ZXing.BarcodeReader      '新建一个图像智能类
        reader.Options.CharacterSet = "UTF-8"      '文本编码，建议设置为UTF-8,手机也可以扫.默认为ISO-8859-1英文字符集，移动设备常用UTF-8字符集编码
        Dim ret As ZXing.Result = reader.Decode(bmp)     '声明一个用于读取器的对象(条形码图片)并赋值给变量.
        If ret Is Nothing Then                           '如果读取不到指定二维码图片
            Return Nothing                               '函数过程值返回Nothing
        Else                                             '否则
            Return ret.Text                              '返回图片内容(读取条形码)
        End If
    End Function


    ''生成条形码
    'Private Sub Button39_Click(sender As Object, e As RibbonControlEventArgs) Handles Button39.Click
    '    Dim btmBtm As Bitmap = MakeQRT(".14738285.43781.SL19J22101", , , 1)    '声明图形对象
    '    btmBtm.Save（"C:\Users\WF\Desktop\myBitmap2.bmp"）  '图片保存到指定路径
    'End Sub

    Private Sub btnSelcetArea_Click(sender As Object, e As EventArgs) Handles btnSelcetArea.Click
        On Error Resume Next    '出错继续执行下一句代码
        Dim strRecordSpace As String = "", rngRng As Excel.Range, strRecord As String, bookPath As String = xlapp.ActiveWorkbook.FullName
        Dim objFso As Object, strRangeAddress As String, rngTargetRange As Excel.Range
        'Dim myArray As String() = {"No.:", "型号:", "区分:", "产品编号:", "发生日期:", "客户:", "设备名:", "发现过程:", "不良类型:", "操作者:", "数量:", "完成工序:", "不良原因:"}
        Dim bytCounter As Byte = 0

        'xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
        objFso = CreateObject("scripting.filesystemobject")  '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists("C:\Option1") = True Then      '如果存在指定的文件夹,那么执行
            objFso.DeleteFolder("C:\Option1")                  '删除名为CopyOption的文件夹
            MkDir("C:\Option1")                          '创建指定文件夹
        Else
            MkDir("C:\Option1")     '创建指定文件夹
        End If
        strRangeAddress = xlapp.Selection.address
        rngRng = xlapp.InputBox("请指定随机数区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        txtAddress.Text = rngRng.Address  '显示单元格地址
        rngRng.Select() '选中单元格

        'rngRng = xlapp.Range("a1:l2")
        '    rngRng.Select() '选中单元格
        '    For i = 1 To 21  '遍历选区中的所有区域
        '        'strRecord = "." & rngRng(7).value & "." & rngRng(19).value & "." & rngRng(23).value
        '        strRecord = "." & rngRng(7).value & "." & rngRng(19).value & "." & Mid(rngRng(23).value, 1, 7) & Val(Mid(rngRng(23).value, 8)) + (i - 1)

        '        Dim btmBtm As Bitmap = MakeQRT(strRecord, , , 1) '生成条形码图片
        '        'MsgBox(Val(Mid(rngRng(23).value, 8)))
        '        'For Each rngTargetRange In rngRng
        '        '    'strRecordSpace = strRecordSpace & rngTargetRange.Value & " "
        '        '    bytCounter = bytCounter + 1
        '        '    'strRecord = myArray(bytCounter - 1) & rngTargetRange.Value & vbCrLf & strRecord
        '        '    strRecord = strRecord & vbCrLf & rngTargetRange.Value
        '        'Next
        '        'btmBtm.Save（"C:\Option1\" & Mid(rngRng(23).value, 1, 7) & rngRng(23).value & ".bmp"）

        '        btmBtm.Save（"C:\Option1\" & Mid(rngRng(23).value, 1, 7) & Val(Mid(rngRng(23).value, 8)) + (i - 1) & ".bmp"）
        '    Next

        'strRecord = "." & rngRng(7).value & "." & rngRng(19).value & "." & rngRng(23).value
        strRecord = "." & rngRng(7).value & "." & rngRng(19).value & "." & Mid(rngRng(23).value, 1, 7) & Val(Mid(rngRng(23).value, 8))
        Dim btmBtm As Bitmap = MakeQRT(strRecord, , , 1) '生成条形码图片
            btmBtm.Save（"C:\Option1\" & Mid(rngRng(23).value, 1, 7) & Val(Mid(rngRng(23).value, 8)) & ".bmp"）


        xlapp.Workbooks(bookPath).Activate()

        'Dim strDirName As String
        '只操作文件，不对文件夹处理...
        'strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...

        xlapp.ActiveWorkbook.Save()






        'xlapp.ScreenUpdating = True    '关闭屏幕更新闪烁
        'xlapp.ActiveWorkbook.Close()
        'xlapp.Workbooks(bookPath).Close()
        'xlapp.Workbooks.Open(bookPath)
        'Me.Close()
    End Sub

    Private Sub btnCreateBarCode_Click(sender As Object, e As EventArgs) Handles btnCreateBarCode.Click
        On Error Resume Next
        Dim strDirName As String, bookPath As String = xlapp.ActiveWorkbook.FullName
        Dim arrArray() As String, bytCounter As Byte = 0, bytShapesNumber As Byte
        'Dim strDirName As String, bookPath As String = "C:\Users\WF\Desktop\工作簿1.xlsm"
        'btmBtm.Save（"C:\Users\WF\Desktop\btmBtm1.bmp"）C:\Users\WF\Desktop\[工作簿1.xlsm]
        'MsgBox(TypeName(btmBtm))
        '只操作文件，不对文件夹处理...
        strDirName = Dir("C:\Option1" & "\*.*")     '获取文件的名称(可能存在文件夹)...
        Do While Len(strDirName) <> 0  '只要文件名称长度大于0就一直循环下去
            xlapp.ActiveSheet.Shapes.AddPicture("C:\Option1\" & strDirName, False, True, 100, 100, 100, 100)
            bytCounter = bytCounter + 1
            ReDim Preserve arrArray(0 To bytCounter - 1)   '重置一维数组上标
            arrArray(bytCounter - 1) = strDirName      '在一维数组中写入值  

            strDirName = Dir()   '查找下一个子文件
        Loop

        'For i = 1 To 21
        bytShapesNumber = xlapp.ActiveSheet.Shapes.Count
        xlapp.ActiveSheet.Shapes(bytShapesNumber).Height = 62.3622047244
        xlapp.ActiveSheet.Shapes(bytShapesNumber).Width = 200.125984252
        xlapp.ActiveSheet.Shapes(bytShapesNumber).Name = Split(arrArray(0), ".")(0) & "."


        For Each rngTarget In xlapp.ActiveSheet.UsedRange

            If rngTarget.Value <> Nothing Then
                If xlapp.ActiveSheet.Shapes(bytShapesNumber).Name = rngTarget.Value.ToString Then
                    xlapp.ActiveSheet.Shapes(bytShapesNumber).Left = rngTarget.Left + 8       '移动图片与选择的单元格左边距离
                    xlapp.ActiveSheet.Shapes(bytShapesNumber).Top = rngTarget.Top          '移动图片与选择的单元格上部距离相等
                End If
            End If
        Next



        'Next

    End Sub



    Private Sub btnCreate21EA_Click(sender As Object, e As EventArgs) Handles btnCreate21EA.Click
        On Error Resume Next
        Dim strDirName As String, bookPath As String = xlapp.ActiveWorkbook.FullName
        Dim arrArray() As String, bytCounter As Byte = 0
        Dim strRecordSpace As String = "", rngRng As Excel.Range, strRecord As String
        Dim objFso As Object, strRangeAddress As String, rngTargetRange As Excel.Range
        'Dim myArray As String() = {"No.:", "型号:", "区分:", "产品编号:", "发生日期:", "客户:", "设备名:", "发现过程:", "不良类型:", "操作者:", "数量:", "完成工序:", "不良原因:"}
        'xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
        xlapp.ActiveSheet.DrawingObjects.Delete
        objFso = CreateObject("scripting.filesystemobject")  '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists("C:\Option1") = True Then      '如果存在指定的文件夹,那么执行
            objFso.DeleteFolder("C:\Option1")                  '删除名为CopyOption的文件夹
            MkDir("C:\Option1")                          '创建指定文件夹
        Else
            MkDir("C:\Option1")     '创建指定文件夹
        End If

        rngRng = xlapp.Range("a1:l2")
            rngRng.Select() '选中单元格
            For i = 1 To 21  '遍历选区中的所有区域
                'strRecord = "." & rngRng(7).value & "." & rngRng(19).value & "." & rngRng(23).value
                strRecord = "." & rngRng(7).value & "." & rngRng(19).value & "." & Mid(rngRng(23).value, 1, 7) & Val(Mid(rngRng(23).value, 8)) + (i - 1)
            Dim btmBtm As Bitmap = MakeQRT(strRecord, , , 1) '生成条形码图片
            'MsgBox(Val(Mid(rngRng(23).value, 8)))
            'For Each rngTargetRange In rngRng
            '    'strRecordSpace = strRecordSpace & rngTargetRange.Value & " "
            '    bytCounter = bytCounter + 1
            '    'strRecord = myArray(bytCounter - 1) & rngTargetRange.Value & vbCrLf & strRecord
            '    strRecord = strRecord & vbCrLf & rngTargetRange.Value
            'Next
            'btmBtm.Save（"C:\Option1\" & Mid(rngRng(23).value, 1, 7) & rngRng(23).value & ".bmp"）

            btmBtm.Save（"C:\Option1\" & Mid(rngRng(23).value, 1, 7) & Val(Mid(rngRng(23).value, 8)) + (i - 1) & ".bmp"）
            Next

        'Dim strDirName As String, bookPath As String = "C:\Users\WF\Desktop\工作簿1.xlsm"
        'btmBtm.Save（"C:\Users\WF\Desktop\btmBtm1.bmp"）C:\Users\WF\Desktop\[工作簿1.xlsm]
        'MsgBox(TypeName(btmBtm))
        '只操作文件，不对文件夹处理...
        strDirName = Dir("C:\Option1" & "\*.*")     '获取文件的名称(可能存在文件夹)...
        Do While Len(strDirName) <> 0  '只要文件名称长度大于0就一直循环下去
            xlapp.ActiveSheet.Shapes.AddPicture("C:\Option1\" & strDirName, False, True, 100, 100, 100, 100)
            bytCounter = bytCounter + 1
            ReDim Preserve arrArray(0 To bytCounter - 1)   '重置一维数组上标
            arrArray(bytCounter - 1) = strDirName      '在一维数组中写入值         
            strDirName = Dir()   '查找下一个子文件
        Loop

        For i = 1 To 21
            xlapp.ActiveSheet.Shapes(i).Height = 62.3622047244
            xlapp.ActiveSheet.Shapes(i).Width = 200.125984252
            xlapp.ActiveSheet.Shapes(i).Name = Split(arrArray(i - 1), ".")(0) & "."
            For Each rngTarget In xlapp.ActiveSheet.UsedRange
                If rngTarget.Value <> Nothing Then
                    If xlapp.ActiveSheet.Shapes(i).Name = rngTarget.Value.ToString Then
                        xlapp.ActiveSheet.Shapes(i).Left = rngTarget.Left + 8       '移动图片与选择的单元格左边距离
                        xlapp.ActiveSheet.Shapes(i).Top = rngTarget.Top          '移动图片与选择的单元格上部距离相等
                    End If
                End If
            Next
        Next
        xlapp.Workbooks(bookPath).Activate()

        'Dim strDirName As String
        '只操作文件，不对文件夹处理...
        'strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...

        xlapp.ActiveWorkbook.Save()

    End Sub
End Class