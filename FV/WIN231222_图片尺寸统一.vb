Public Class WIN231222_图片尺寸统一
    Private Sub butRefference_Click(sender As Object, e As EventArgs) Handles butRefference.Click
        Dim strSelctTypeName As String = "", obj As Object, sapObject As Object

        '只选了1个对象,程序出错，跳转到line处执行代码
        On Error GoTo line

        ' 执行以下代码,说明选了2个以上对象..
        For Each obj In xlapp.Selection

            MsgBox("请选择1个参考图表/图片后,选中需要操作的图表/图片范围,再按执行按钮", vbInformation, "提示") : Exit Sub
        Next

        '设标签
line:
        '对象类型名称,和对象赋值给变量
        strSelctTypeName = TypeName(xlapp.Selection)
        sapObject = xlapp.Selection

        '根据不同类型对象,将对象名称写入文本框,      
        '注:选择单一的图表对象selection.name,只显示图表类型名称,需要按xlapp.ActiveSheet.ChartObjects.item(1).name, 但同时选2个以上对象(数组),单一元素显示完整的名称
        Select Case strSelctTypeName
            '排除单元格对象,并提醒
            Case "Range"
                MsgBox("请选择1个参考图表/图片后,选中需要操作的图表/图片范围,再按执行按钮", vbInformation, "提示") : Exit Sub

                '图表对象执行..
            Case "ChartArea"
                txtName.Text = xlapp.ActiveSheet.ChartObjects.item(1).name

                '其他图片对象,执行..
            Case Else
                txtName.Text = sapObject.Name
        End Select

    End Sub

    Private Sub butPerform_Click(sender As Object, e As EventArgs) Handles butPerform.Click
        Dim ChartCount As Byte, ActiveChart As Byte, strSelctTypeName As String '声明变量 rdbPicture rdbChart
        Dim i As Byte, obj As Object
        Dim strName As String, arrShapCollect() As String, shapElement As Object, strShapeName As String   '声明变量

        On Error GoTo line
        '执行以下代码, 说明选了2个以上对象,否则出错,执行错误标签下的语句..
        For Each obj In xlapp.Selection
        Next

        '遍历选中的项目.
        '注:选择单一的图表对象selection.name,只显示图表类型名称, 但2个以上对象,显示完整的名称
        For Each shapElement In xlapp.Selection
                i = i + 1   '计数器累加

                ReDim Preserve arrShapCollect(0 To i - 1)   '重新声明1维数据
                arrShapCollect(i - 1) = shapElement.Name    '向数组指定位置添加元素赋值
            Next

            xlapp.Selection.ShapeRange.LockAspectRatio = 0  '解锁横向纵向锁定

            '遍历数组
            For Each strShapeName In arrShapCollect
                xlapp.ActiveSheet.Shapes(strShapeName).Width = xlapp.ActiveSheet.Shapes(txtName.Text).Width
                xlapp.ActiveSheet.Shapes(strShapeName).Height = xlapp.ActiveSheet.Shapes(txtName.Text).Height
            Next
        Exit Sub
line:
        MsgBox("请选择2个以上图表/图片后,再按执行按钮", vbInformation, "提示")
    End Sub

    '关闭重新启用按钮
    Private Sub WIN231222_图片尺寸统一_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnControlSize.Enabled = True  '按钮重新启用. '关闭窗体,vsto中不能使用 VBA的unload me语句
    End Sub


End Class