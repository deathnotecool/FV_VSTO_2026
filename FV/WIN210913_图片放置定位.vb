Public Class WIN210913_图片放置定位
    '功能:放置图片
    Private Sub btnPosition_Click(sender As Object, e As EventArgs) Handles btnPosition.Click

        On Error Resume Next
        Dim strName As String, rngSlecRange As Excel.Range, intShapLeft As Integer, intShapTop As Integer, intShapWide As Integer, intShapHeight As String


        '判定是否选择了单元格,如果是,那么提示选择图片,并退出程序.
        If TypeName(xlapp.Selection) = "Range" Then MsgBox("先选择图片,在运行此功能键") : Exit Sub '判定是否选中图片

        strName = xlapp.Selection.ShapeRange.Name                 '给变量赋值为图片名称
        rngSlecRange = xlapp.InputBox("请选择单元格：", Type:=8) '选择单元格
        If rngSlecRange.Count > 1 Then MsgBox("只能选择一个单元格,请重新选择") : Exit Sub '判定是否选中多个单元格

        txtCellAddress.Text = rngSlecRange.Address

        '分别设置单元格上、左边距及高度和宽度.注意:单元格可能是合并单元格,这里不管是否为合并单元格,都当成合并单元格设置.
        intShapLeft = rngSlecRange.MergeArea.Left    '获取单元格区域的左边距()
        intShapTop = rngSlecRange.MergeArea.Top      '获取单元格区域的上边距
        intShapWide = rngSlecRange.MergeArea.Width   '获取单元格区域的宽度
        intShapHeight = rngSlecRange.MergeArea.Height  '获取单元格区域的高度
        xlapp.ActiveSheet.Shapes(strName).Select        '重新选择图片

        xlapp.Selection.ShapeRange.LockAspectRatio = 0          '使图片不锁定比例
        xlapp.Selection.ShapeRange.Height = intShapHeight - 6   '移动图片与选择的单元格高度距离相等(少6个单位,如果填充不到当中,是分屏显示器分辨率影响的)
        xlapp.Selection.ShapeRange.Width = intShapWide - 6      '移动图片与选择的单元格宽度距离相等(少6个单位)
        xlapp.Selection.ShapeRange.Left = intShapLeft + 3       '移动图片与选择的单元格左边距离(多3个单位)

        xlapp.Selection.ShapeRange.Top = intShapTop + 3       '移动图片与选择的单元格上部距离相等(多3个单位)

        With xlapp.Selection                                    '定义图片大小位置随单元格变化而变化
            .Placement = 1
        End With

    End Sub

    '功能:退出按钮关闭窗体
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Me.Close()  '关闭窗体,vsto中不能使用 VBA的unload me语句
        WIN210913_图片放置定位_Closed(Nothing, Nothing)
    End Sub

    '功能:默认的关闭按钮,重新启用功能按钮
    Private Sub WIN210913_图片放置定位_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnAreaLocalPicture.Enabled = True  '按钮重新启用. '关闭窗体,vsto中不能使用 VBA的unload me语句
    End Sub

    '功能:指定功能区域选择图片
    Private Sub btnSelectAreaPic_Click(sender As Object, e As EventArgs) Handles btnSelectAreaPic.Click

        Dim sapShapeItem As Excel.Shape, rngSelectRange As Excel.Range, strPicturesName As String = "", bytCounter As Byte = 0
        If TypeName(xlapp.Selection) <> "Range" Then MsgBox("先选择单元格,在运行此功能键") : Exit Sub '判定是否选中单元格

        '选择图片区域
        rngSelectRange = xlapp.InputBox(Prompt:="请选择图片所在区域", Default:=xlapp.Selection.Address, Type:=8)

        '遍历所有图像
        For Each sapShapeItem In xlapp.ActiveSheet.Shapes   '遍历本表所有图形对象
            If sapShapeItem.Type <> 3 Then '如果不是图表

                '如果图片覆盖的单元格与所选区域有交集那么执行图片名字以" ' "合并连接...
                If Not xlapp.Intersect(xlapp.Range(sapShapeItem.TopLeftCell, sapShapeItem.BottomRightCell), rngSelectRange) Is Nothing Then
                    If Len(strPicturesName) = 0 Then '如果strPicturesName变量是空值,首次赋值
                        strPicturesName = sapShapeItem.Name
                    Else '不是初始值,串联项目值,更新变量值
                        strPicturesName = strPicturesName & "," & sapShapeItem.Name
                    End If
                End If
            End If
        Next sapShapeItem

        '根据"'" 分列图片名称
        xlapp.ActiveSheet.Shapes.Range(Split(strPicturesName, ",")).Select

    End Sub


    '功能:找出图片覆盖的单元格/区域
    Private Sub btnSelectCell_Click(sender As Object, e As EventArgs) Handles btnSelectCell.Click
        On Error Resume Next
        Dim sapShapeItem As Object, rngSelectRange As Excel.Range, shape As Excel.Shape
        Dim selectedCount As Integer = 0

        If TypeName(xlapp.Selection) = "Range" Then MsgBox("先选择图片,在运行此功能键") : Exit Sub '判定是否选中图片

        sapShapeItem = xlapp.Selection '选中的对象赋值给变量


        For Each shape In xlapp.Selection.ShapeRange

            selectedCount = selectedCount + 1

        Next shape


        If selectedCount > 1 Then MsgBox("只能选择一张图片,请重新选择") : Exit Sub  '判定是否选中多个图片

        rngSelectRange = xlapp.Range(sapShapeItem.TopLeftCell.Address, sapShapeItem.BottomRightCell.Address) '图片所在单元格区域赋值给变量

        rngSelectRange.UnMerge() '防止已有合并,取消合并
        rngSelectRange.Merge() '重新合并
    End Sub


End Class