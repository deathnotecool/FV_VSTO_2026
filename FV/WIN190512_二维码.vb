Imports System.Drawing
Imports Microsoft.Office.Tools.Ribbon
Imports System.Windows.Forms
Public Class WIN190512_二维码
    '生成二维码的属性

    Shared Function MakeQRE(ByVal qrtext As String, Optional ByVal width As Integer = 150, Optional ByVal height As Integer = 150, Optional ByVal margin As Integer = 1) As Bitmap
        Dim writer As New ZXing.BarcodeWriter       '新建一个图像智能类
        writer.Format = ZXing.BarcodeFormat.QR_CODE         '智能类图像格式设置为二维码
        Dim opt As New ZXing.QrCode.QrCodeEncodingOptions   '创建一个二维码操作对象
        opt.DisableECI = True      '设置为True才可以调整编码
        opt.CharacterSet = "UTF-8" '文本编码，建议设置为UTF-8,手机也可以扫.默认为ISO-8859-1英文字符集，但一般移动设备常用UTF-8字符集编码
        opt.Width = width   '宽度
        opt.Height = height '高度
        opt.Margin = margin  '边距，貌似不是像素格式，因此不宜设置过大
        writer.Options = opt   '设置用于编码的选项容器
        Return writer.Write(qrtext) '内容写入智能类
    End Function

    '读取二维码
    Shared Function ReadQR(ByVal bmp As Bitmap) As String
        Dim reader As New ZXing.BarcodeReader            '新建一个图像智能类
        reader.Options.CharacterSet = "UTF-8"            '文本编码，建议设置为UTF-8,手机也可以扫.默认为ISO-8859-1英文字符集，移动设备常用UTF-8字符集编码
        Dim ret As ZXing.Result = reader.Decode(bmp)     '声明一个用于读取器的对象(二维码图片)并赋值给变量.
        If ret Is Nothing Then                           '如果读取不到指定二维码图片
            Return Nothing                               '函数过程值返回Nothing
        Else                                             '否则
            Return ret.Text                              '返回图片内容(读取二维码)
        End If
    End Function

    '其实点之前已经生成了,只是调用
    Private Sub btnCreateQukCode_Click(sender As Object, e As EventArgs) Handles btnCreateQukCode.Click
        'On Error Resume Next    '出错继续执行下一句代码

        Dim strDirName As String, bookPath As String = xlapp.ActiveWorkbook.FullName

        'Dim strDirName As String, bookPath As String = "C:\Users\WF\Desktop\工作簿1.xlsm"

        'btmBtm.Save（"C:\Users\WF\Desktop\btmBtm1.bmp"）C:\Users\WF\Desktop\[工作簿1.xlsm]
        'MsgBox(TypeName(btmBtm))
        '只操作文件，不对文件夹处理...
        strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...
        xlapp.ActiveSheet.Shapes.AddPicture("C:\Option\" & strDirName, False, True, 100, 100, 100, 100)
        MsgBox("已成功生成二维码，请按更新按钮显示")
        '更新按钮_Click(Nothing, Nothing)

    End Sub

    '更新按钮
    Private Sub 更新按钮_Click(sender As Object, e As EventArgs) Handles 更新按钮.Click
        On Error Resume Next
        Dim sapAddName As Excel.Shape, rngSlecRange As Excel.Range
        sapAddName = xlapp.ActiveSheet.DrawingObjects.Select    '给变量赋值为图片
        'Me.Close() '关闭窗体
        'xlapp.Workbooks(xlapp.ActiveWorkbook.FullName).Activate()
        xlapp.Selection.Cut '剪切二维码
        rngSlecRange = xlapp.Range("a3").End(-4121).Offset(1) '选择单元格
        rngSlecRange.Select()
        PauseWait(1500)
        xlapp.ActiveSheet.Paste '粘贴
    End Sub




    Public Sub PauseWait(ByVal HowLong As Long)
        Dim tick As Long
        tick = My.Computer.Clock.TickCount
        Do
            Application.DoEvents()
        Loop Until tick + HowLong < My.Computer.Clock.TickCount
    End Sub


    Private Sub btnSelcetArea_Click(sender As Object, e As EventArgs) Handles btnSelcetArea.Click
        'On Error Resume Next    '出错继续执行下一句代码
        Dim strRecordSpace As String = "", rngRng As Excel.Range, strRecord As String, bookPath As String = xlapp.ActiveWorkbook.FullName
        Dim objFso As Object, strRangeAddress As String, rngTargetRange As Excel.Range
        Dim myArray As String() = {"No.:", "型号:", "区分:", "产品编号:", "发生日期:", "线圈规格-编号:", "设备名:", "发现过程:", "不良类型:", "操作者:", "数量:", "完成工序:", "不良原因:"}
        Dim bytCounter As Byte = 0
        'xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
        objFso = CreateObject("scripting.filesystemobject")   '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists("C:\Option") = True Then '如果存在指定的文件夹,那么执行
            objFso.DeleteFolder("C:\Option")             '删除名为CopyOption的文件夹
            MkDir("C:\Option")                          '创建指定文件夹
        Else
            MkDir("C:\Option")                          '创建指定文件夹
        End If
        strRangeAddress = xlapp.Selection.address
        rngRng = xlapp.InputBox("请指定随机数区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        txtAddress.Text = rngRng.Address
        rngRng.Select()

        For Each rngTargetRange In rngRng
            strRecordSpace = strRecordSpace & rngTargetRange.Value & " "
            bytCounter = bytCounter + 1
            'strRecord = myArray(bytCounter - 1) & rngTargetRange.Value & vbCrLf & strRecord
            strRecord = strRecord & vbCrLf & myArray(bytCounter - 1) & rngTargetRange.Value
        Next
        Dim btmBtm As Bitmap = MakeQRE(strRecord, , , 1)
        btmBtm.Save（"C:\Option\" & rngRng(1).value & ".bmp"）
        xlapp.Workbooks(bookPath).Activate()
        'Dim strDirName As String
        '只操作文件，不对文件夹处理...
        'strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...

        'xlapp.ActiveWorkbook.Save()

        'xlapp.ScreenUpdating = True    '关闭屏幕更新闪烁
        'xlapp.ActiveWorkbook.Close()
        'xlapp.Workbooks(bookPath).Close()
        'xlapp.Workbooks.Open(bookPath)
        'Me.Close()

    End Sub

    Private Sub btnReadQuickCode_Click(sender As Object, e As EventArgs) Handles btnReadQuickCode.Click
        On Error Resume Next
        Dim rngEvalutRange As Excel.Range, strDirName As String, strShapeName As String, rngMyCell As Excel.Range
        'Dim arrCollect As Object
        'Dim bytCounter As Byte = 0, i As Byte = 0
        Dim shpActivePicture As Excel.Shape
        Dim objFso As Object
        objFso = CreateObject("scripting.filesystemobject")             '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists("C:\OptionS") = True Then '如果存在指定的文件夹,那么执行
            objFso.DeleteFolder("C:\OptionS")             '删除名为CopyOption的文件夹
            MkDir("C:\OptionS")                          '创建指定文件夹
        Else
            MkDir("C:\OptionS")                          '创建指定文件夹
        End If
        shpActivePicture = xlapp.ActiveSheet.Shapes(xlapp.Selection.Name) '无法直接用Selction引用图片
        strShapeName = xlapp.Selection.Name

        Clipboard.Clear()
        rngMyCell = xlapp.InputBox("请选择区域", "放置的单元格", , , , , , 8)
        With xlapp.ActiveSheet
            'For Each shp In .Shapes   xlapp.ActiveSheet.Shapes(strShapeName).Width
            '                If shp.Type = 1 Then
            'shpActivePicture = xlapp.Selection.Copy
            'Clipboard.Clear()


            shpActivePicture = .Shapes(xlapp.Selection.Name).Copy
            shpActivePicture.Select()

            'With .ChartObjects.Add(10, 10, 11, 11).Chart
            '    .Paste
            '    '                        .Export ThisWorkbook.Path & "" & SH.Name & "_" & shp.TopLeftCell.Offset(0, -1).Value & ".jpg"
            '    '.Export(xlapp.ActiveWorkbook.Path & "\1.bmp")
            '    .Export("C:\Users\WF\Desktop\btmBtm1.bmp")
            '    '.Parent.Delete
            'End With
            ''                End If
        End With
        Clipboard.GetImage().Save("C:\OptionS\" & strShapeName & ".bmp")
        Clipboard.Clear()


        Dim btmBtm As Bitmap = New Bitmap("C:\OptionS\" & strShapeName & ".bmp")    '声明一个图形对象(指定完整路径)

        rngMyCell.Value = ReadQR(btmBtm)
    End Sub

    Private Sub btnIncomingSelectArea_Click(sender As Object, e As EventArgs) Handles btnIncomingSelectArea.Click
        'On Error Resume Next    '出错继续执行下一句代码
        Dim strRecordSpace As String = "", rngRng As Excel.Range, strRecord As String = "", bookPath As String = xlapp.ActiveWorkbook.FullName
        Dim objFso As Object, strRangeAddress As String, rngTargetRange As Excel.Range
        Dim myArray As String()
        Dim bytCounter As Byte = 0
        Dim strDirName As String
        Dim arrArray() As String
        'xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
        objFso = CreateObject("scripting.filesystemobject")   '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists("C:\Option") = True Then '如果存在指定的文件夹,那么执行
            objFso.DeleteFolder("C:\Option")             '删除名为CopyOption的文件夹
            MkDir("C:\Option")                          '创建指定文件夹
        Else
            MkDir("C:\Option")                          '创建指定文件夹
        End If
        strRangeAddress = xlapp.Selection.address
        rngRng = xlapp.InputBox("请指定随机数区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        txtAddress.Text = rngRng.Address
        rngRng.Select()

        xlapp.ActiveSheet.DrawingObjects.Delete '删除图形
        'MsgBox(rngRng.Columns.Count & ":" & rngRng.Rows.Count)

        'MsgBox(rngRng(1).row)

        'For i = 2 To rngRng.Rows.Count + 1
        For i = rngRng(1).Row To rngRng(1).Row + rngRng.Rows.Count - 1

            For Each rngTargetRange In xlapp.Range("A" & i & ":O" & i) 'N改成O列
                'strRecord = myArray(bytCounter - 1) & rngTargetRange.Value & vbCrLf & strRecord
                strRecord = strRecord & rngTargetRange.Value & ":"
            Next
            Dim btmBtm As Bitmap = MakeQRE(strRecord, , , 1)
                btmBtm.Save（"C:\Option\" & xlapp.Cells(i, 14).VALUE & ".bmp"） '按列行取名图片
                strRecord = ""
            Next

            strDirName = Dir("C:\Option" & "\*.*")     '获取文件的名称(可能存在文件夹)...
        Do While Len(strDirName) <> 0  '只要文件名称长度大于0就一直循环下去
            xlapp.ActiveSheet.Shapes.AddPicture("C:\Option\" & strDirName, False, True, 100, 100, 100, 100)
            bytCounter = bytCounter + 1
            ReDim Preserve arrArray(0 To bytCounter - 1)   '重置一维数组上标
            arrArray(bytCounter - 1) = strDirName      '在一维数组中写入值         
            strDirName = Dir()   '查找下一个子文件
        Loop

        'For i = 1 To rngRng.Rows.Count

        For i = 1 To rngRng.Rows.Count
            xlapp.ActiveSheet.Shapes(i).Height = 88
            xlapp.ActiveSheet.Shapes(i).Width = 88
            xlapp.ActiveSheet.Shapes(i).Name = Split(arrArray(i - 1), ".")(0) & "."
            For Each rngTarget In xlapp.ActiveSheet.UsedRange
                'For Each rngTarget In rngRng
                If rngTarget.Value <> Nothing Then
                    If xlapp.ActiveSheet.Shapes(i).Name = rngTarget.Value.ToString Then
                        xlapp.ActiveSheet.Shapes(i).Left = rngTarget.Left + 5     '移动图片与选择的单元格左边距离
                        xlapp.ActiveSheet.Shapes(i).Top = rngTarget.Top + 5       '移动图片与选择的单元格上部距离相等
                        rngTarget.RowHeight = 97.5
                        rngTarget.ColumnWidth = 15.38
                    End If
                End If
            Next
        Next
        WIN190512_二维码_Closed(Nothing, Nothing)
        xlapp.Workbooks(bookPath).Activate()
        'Dim strDirName As String
        '只操作文件，不对文件夹处理...
        'strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...
        'xlapp.ActiveWorkbook.Save()
        'xlapp.ScreenUpdating = True    '关闭屏幕更新闪烁
        'xlapp.ActiveWorkbook.Close()
        'xlapp.Workbooks(bookPath).Close()
        'xlapp.Workbooks.Open(bookPath)

    End Sub



    Private Sub WIN190512_二维码_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Me.Close()
    End Sub

    Private Sub btnSelcetArea1_Click(sender As Object, e As EventArgs) Handles btnSelcetArea1.Click
        'On Error Resume Next    '出错继续执行下一句代码
        Dim strRecordSpace As String = "", rngRng As Excel.Range, strRecord As String, bookPath As String = xlapp.ActiveWorkbook.FullName
        Dim objFso As Object, strRangeAddress As String, rngTargetRange As Excel.Range
        Dim myArray As String() = {"序号:", "发生日期:", "检查人员:", "型号:", "成品系列号:", "返修区分:", "返修系列号:", "数量:", "不良描述:", "返修状态:", "验证人:", "注释:", "完成日期:", "不良持续天数:"}
        Dim bytCounter As Byte = 0
        'xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
        objFso = CreateObject("scripting.filesystemobject")   '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists("C:\Option") = True Then '如果存在指定的文件夹,那么执行
            objFso.DeleteFolder("C:\Option")             '删除名为CopyOption的文件夹
            MkDir("C:\Option")                          '创建指定文件夹
        Else
            MkDir("C:\Option")                          '创建指定文件夹
        End If
        strRangeAddress = xlapp.Selection.address
        rngRng = xlapp.InputBox("请指定随机数区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        txtAddress1.Text = rngRng.Address
        rngRng.Select()

        For Each rngTargetRange In rngRng
            strRecordSpace = strRecordSpace & rngTargetRange.Value & " "
            bytCounter = bytCounter + 1
            'strRecord = myArray(bytCounter - 1) & rngTargetRange.Value & vbCrLf & strRecord
            strRecord = strRecord & vbCrLf & myArray(bytCounter - 1) & rngTargetRange.Value
        Next
        Dim btmBtm As Bitmap = MakeQRE(strRecord, , , 1)
        btmBtm.Save（"C:\Option\" & rngRng(1).value & ".bmp"）
        xlapp.Workbooks(bookPath).Activate()
        'Dim strDirName As String
        '只操作文件，不对文件夹处理...
        'strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...

        'xlapp.ActiveWorkbook.Save()

        'xlapp.ScreenUpdating = True    '关闭屏幕更新闪烁
        'xlapp.ActiveWorkbook.Close()
        'xlapp.Workbooks(bookPath).Close()
        'xlapp.Workbooks.Open(bookPath)
        'Me.Close()
    End Sub

    Private Sub btnCreateQukCode1_Click(sender As Object, e As EventArgs) Handles btnCreateQukCode1.Click
        On Error Resume Next    '出错继续执行下一句代码

        Dim strDirName As String, bookPath As String = xlapp.ActiveWorkbook.FullName

        'Dim strDirName As String, bookPath As String = "C:\Users\WF\Desktop\工作簿1.xlsm"

        'btmBtm.Save（"C:\Users\WF\Desktop\btmBtm1.bmp"）C:\Users\WF\Desktop\[工作簿1.xlsm]
        'MsgBox(TypeName(btmBtm))
        '只操作文件，不对文件夹处理...
        xlapp.ActiveSheet.DrawingObjects.Delete '删除图形
        strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...
        xlapp.ActiveSheet.Shapes.AddPicture("C:\Option\" & strDirName, False, True, 100, 100, 100, 100)
        MsgBox("已成功生成二维码，请按更新按钮显示")
        '更新按钮_Click(Nothing, Nothing)

    End Sub

    Private Sub 更新按钮1_Click(sender As Object, e As EventArgs) Handles 更新按钮1.Click
        On Error Resume Next
        Dim sapAddName As Excel.Shape, rngSlecRange As Excel.Range
        sapAddName = xlapp.ActiveSheet.DrawingObjects.Select    '给变量赋值为图片
        'Me.Close() '关闭窗体
        'xlapp.Workbooks(xlapp.ActiveWorkbook.FullName).Activate()
        xlapp.Selection.Cut '剪切二维码
        rngSlecRange = xlapp.Range("a3").End(-4121).Offset(1) '选择单元格
        rngSlecRange.Select()
        PauseWait(1500)
        xlapp.ActiveSheet.Paste '粘贴
    End Sub



    'Private Sub btnDisplaying_Click(sender As Object, e As EventArgs) Handles btnDisplaying.Click
    '    On Error Resume Next
    '    Dim strDirName As String, bookPath As String = xlapp.ActiveWorkbook.FullName
    '    Dim arrArray() As String, bytCounter As Byte = 0
    '    Dim strRecordSpace As String = "", rngRng As Excel.Range, strRecord As String
    '    Dim objFso As Object, strRangeAddress As String, rngTargetRange As Excel.Range
    '    'Dim myArray As String() = {"No.:", "型号:", "区分:", "产品编号:", "发生日期:", "线圈规格-编号:", "设备名:", "发现过程:", "不良类型:", "操作者:", "数量:", "完成工序:", "不良原因:"}
    '    'xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
    '    xlapp.ActiveSheet.DrawingObjects.Delete


    '    'Dim strDirName As String, bookPath As String = "C:\Users\WF\Desktop\工作簿1.xlsm"
    '    'btmBtm.Save（"C:\Users\WF\Desktop\btmBtm1.bmp"）C:\Users\WF\Desktop\[工作簿1.xlsm]
    '    'MsgBox(TypeName(btmBtm))
    '    '只操作文件，不对文件夹处理...
    '    strDirName = Dir("C:\Option1" & "\*.*")     '获取文件的名称(可能存在文件夹)...
    '    Do While Len(strDirName) <> 0  '只要文件名称长度大于0就一直循环下去
    '        xlapp.ActiveSheet.Shapes.AddPicture("C:\Option1\" & strDirName, False, True, 100, 100, 100, 100)
    '        bytCounter = bytCounter + 1
    '        ReDim Preserve arrArray(0 To bytCounter - 1)   '重置一维数组上标
    '        arrArray(bytCounter - 1) = strDirName      '在一维数组中写入值         
    '        strDirName = Dir()   '查找下一个子文件
    '    Loop

    '    For i = 1 To 21
    '        xlapp.ActiveSheet.Shapes(i).Height = 62.3622047244
    '        xlapp.ActiveSheet.Shapes(i).Width = 200.125984252
    '        xlapp.ActiveSheet.Shapes(i).Name = Split(arrArray(i - 1), ".")(0) & "."
    '        For Each rngTarget In xlapp.ActiveSheet.UsedRange
    '            If rngTarget.Value <> Nothing Then
    '                If xlapp.ActiveSheet.Shapes(i).Name = rngTarget.Value.ToString Then
    '                    xlapp.ActiveSheet.Shapes(i).Left = rngTarget.Left + 8       '移动图片与选择的单元格左边距离
    '                    xlapp.ActiveSheet.Shapes(i).Top = rngTarget.Top          '移动图片与选择的单元格上部距离相等
    '                End If
    '            End If
    '        Next
    '    Next
    '    xlapp.Workbooks(bookPath).Activate()

    '    'Dim strDirName As String
    '    '只操作文件，不对文件夹处理...
    '    'strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...

    '    xlapp.ActiveWorkbook.Save()
    'End Sub



    'Private Sub btnReadQuickCode_Click(sender As Object, e As EventArgs) Handles btnReadQuickCode.Click
    '    'On Error Resume Next
    '    Dim rngEvalutRange As Excel.Range, strDirName As String, SelRng As String, rngMyCell As Excel.Range
    '    'Dim arrCollect As Object
    '    'Dim bytCounter As Byte = 0, i As Byte = 0
    '    Dim shpActivePicture As Excel.Shape
    '    'Kill(xlapp.ActiveWorkbook.Path & "\1.bmp")

    '    shpActivePicture = xlapp.ActiveSheet.Shapes(xlapp.Selection.Name)
    '    With xlapp.ActiveSheet
    '        'For Each shp In .Shapes   xlapp.ActiveSheet.Shapes(strShapeName).Width
    '        '                If shp.Type = 1 Then
    '        'shpActivePicture = xlapp.Selection.Copy
    '        shpActivePicture = xlapp.ActiveSheet.Shapes(xlapp.Selection.Name).Copy
    '        With .ChartObjects.Add(10, 10, 11, 11).Chart
    '            .Paste
    '            '                        .Export ThisWorkbook.Path & "" & SH.Name & "_" & shp.TopLeftCell.Offset(0, -1).Value & ".jpg"
    '            '.Export(xlapp.ActiveWorkbook.Path & "\1.bmp")
    '            .Export("C:\Users\WF\Desktop\btmBtm1.bmp")
    '            '.Parent.Delete
    '        End With
    '        '                End If
    '    End With


    '    'Dim btmBtm As Bitmap = New Bitmap(xlapp.ActiveWorkbook.Path & "\1.bmp")   '声明一个图形对象(指定完整路径)C:\Users\WF\Desktop

    '    Dim btmBtm As Bitmap = New Bitmap("C:\Users\WF\Desktop\btmBtm1.bmp")    '声明一个图形对象(指定完整路径)
    '    'Kill("C:" & "\1.bmp") C:\Users\WF\Desktop
    '    'Dim btmBtm As Bitmap = New Bitmap("C:\Users\WF\Desktop" & "\1.bmp")
    '    'Dim strData As String
    '    'rngEvalutRange = xlapp.Range("a1")                                      '声明单元格对象
    '    '调用函数过程的结果返回值写入指定的单元格内
    '    'SelRng = xlapp.Selection.Address   '单元格地址赋值给文本变量
    '    rngMyCell = xlapp.InputBox("请选择区域", "放置的单元格", , , , , , 8)
    '    rngMyCell.Value = ReadQR(btmBtm)
    '    'Dim btmBtm As Bitmap = New Bitmap("C:\Users\WF\Desktop\btmBtm1.bmp")    '声明一个图形对象(指定完整路径)
    '    'rngEvalutRange = xlapp.Range("a1")                                      '声明单元格对象
    '    'rngEvalutRange.Value = ReadQR(btmBtm)                                   '调用函数过程的结果返回值写入指定的单元格内
    '    'arrCollect = Split(strData, ":")(1)
    '    'For Each strDataSingle In myArray
    '    '    bytCounter = bytCounter + 2
    '    '    i = i + 1
    '    '    ReDim Preserve arrCollect(0 To i - 1)          '重新定义上标
    '    '    arrCollect(i) = Split(strData, ":")(bytCounter - 1)               '逐一写入数组
    '    '    MsgBox(arrCollect(i))
    '    'Next
    'End Sub
    ''Private Sub WIN190512_二维码_Closed(sender As Object, e As EventArgs) Handles Me.Closed
    ''    On Error Resume Next
    ''    Kill(xlapp.ActiveWorkbook.Path & "\1.bmp")
    ''End Sub

    '剪贴板的使用,代码测试可用

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    'Clipboard.SetText("luanqibaz") '存放到剪贴板
    '    Clipboard.SetText(xlapp.ActiveSheet.range("a1").value) '存放到剪贴板
    '    txtPrice.Text = Clipboard.GetText() '获取剪贴板数据
    '    Clipboard.SetText(txtPrice.Text) '存放到剪贴板
    '    xlapp.ActiveSheet.Range("$A$8").Select
    '    xlapp.ActiveSheet.Paste

    'End Sub











End Class