Public Class WIN231210_多图排放


    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        'On Error Resume Next  '忽略出错继续执行以下语句...
        Dim rngSlecRange As Excel.Range, objFilenname As Object, shpName As Object, intCounter As Integer = 0, shpInstShap As Excel.Shape  '声明变量
        rngSlecRange = xlapp.InputBox("请选择单元格：", Type:=8) '选择单元格
        If rngSlecRange.Count > 1 Then MsgBox("只能选择一个单元格,请重新选择") : Exit Sub '判定是否选中多个单元格

        txtCellAddress.Text = rngSlecRange.Address

        If TypeName(xlapp.Selection) <> "Range" Then MsgBox("先选择一个单元格,在运行此功能键") : Exit Sub   '如果当前选择的对象不是单元格则结束过程...

        objFilenname = xlapp.GetOpenFilename("所有图片文件 (*.jpg;*.bmp;*.png;*.gif), _
*.jpg;*.bmp;*.png;*.gif", , "请选择所有待插入的图片文件", , True) '弹出一个选择图片文件的对话框，支持jpg bmp png和gif四种图片格式，允许选择多个文件
        If TypeName(objFilenname) = "Boolean" Then Exit Sub       '如果用户选择了取消键，那么结束过程

        '遍历产生的数组(图片路径集合)，其中变量shpName代表每一个图片路径
        For Each shpName In objFilenname
            '插入shpName所代表的图片文件，且图片的左边距、上边距、宽度与高度皆与单元格保持一致,赋值给变量.

            If rbDown.Checked = True Then
                shpInstShap = xlapp.ActiveSheet.Shapes.AddPicture(shpName, 0, -1, rngSlecRange.Offset(intCounter, 0).Left + 3,
                                                              rngSlecRange.Offset(intCounter, 0).Top + 3,
                                                              rngSlecRange.Offset(intCounter, 0).Width - 6,
                                                              rngSlecRange.Offset(intCounter, 0).Height - 6)
                shpInstShap.Placement = 1        '将shp的对象位置设置为“大小与位置随单元格而变”，目的是修改单元格的高度与宽度时图片也相应的变化
            Else
                shpInstShap = xlapp.ActiveSheet.Shapes.AddPicture(shpName, 0, -1, rngSlecRange.Offset(0, intCounter).Left + 3,
                                                                                  rngSlecRange.Offset(0, intCounter).Top + 3 _
                                                                                  , rngSlecRange.Offset(0, intCounter).Width - 6,
                                                                                  rngSlecRange.Offset(0, intCounter).Height - 6)
                shpInstShap.Placement = 1  '将shp的对象位置设置为“大小与位置随单元格而变”，目的是修改单元格的高度与宽度时图片也相应的变化

            End If


            If cbDisplay.Checked = True And rbDown.Checked = True Then
                rngSlecRange.Offset(intCounter, -1).Value = Split(Dir(shpName), ".")(0)  '将插入的图片命名为硬盘中的图片名称，包括扩展名
                rngSlecRange.Offset(-1, -1).Value = "图片名称" : rngSlecRange.Offset(-1, 0).Value = "图片放置处"
                shpInstShap.Name = Split(Dir(shpName), ".")(0)  '将插入的图片命名为硬盘中的图片名称，包括扩展名
            ElseIf cbDisplay.Checked = True And rbRight.Checked = True Then
                rngSlecRange.Offset(-1, intCounter).Value = Split(Dir(shpName), ".")(0)  '将插入的图片命名为硬盘中的图片名称，包括扩展名
                rngSlecRange.Offset(-1, -1).Value = "图片名称" : rngSlecRange.Offset(0, -1).Value = "图片放置处"
                shpInstShap.Name = Split(Dir(shpName), ".")(0) '将插入的图片命名为硬盘中的图片名称，包括扩展名
            End If

            intCounter = intCounter + 1      '累加计数器
        Next shpName
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()  '关闭窗体,vsto中不能使用 VBA的unload me语句
        WIN231210_多图排放_Closed(Nothing, Nothing)
    End Sub

    Private Sub WIN231210_多图排放_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnSort.Enabled = True  '按钮重新启用. '关闭窗体,vsto中不能使用 VBA的unload me语句
    End Sub


End Class