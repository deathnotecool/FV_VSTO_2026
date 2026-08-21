Imports System.Windows.Forms    '窗体命名空间.

Public Class WN18081501_列和区域删除图      '窗体类壳()默认创建.
    'GN005_列和区域删图 180201 (不删除图表,只删除图形)
    Public Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        On Error Resume Next   '防错语句,继续执行下一句

        '声明变量..
        Dim sapShapeItem As Excel.Shape, rngSelectRange As Excel.Range, strRangeAdress As String

        '将显示单元格地址的文本框的Text属性值,赋值给变量.
        strRangeAdress = cboDisplayAdress.Text

        '如果点选了删除指定列图片选项按钮..
        If optDeleteColumnPicture.Checked Then
            If MsgBox("确定删除吗?", vbYesNo + vbQuestion, "是否删除") = vbNo Then Exit Sub '如果选了No将退出该事件

            '遍历活动工作表的所有图形对象.
            For Each sapShapeItem In xlapp.ActiveSheet.Shapes
                If sapShapeItem.Type <> 3 Then '如果不是图表, 
                    '如果图形对象所覆盖的区域与选中列重叠,那么删除该对象.
                    If Not xlapp.Intersect(xlapp.Range(sapShapeItem.TopLeftCell, sapShapeItem.BottomRightCell),
                 xlapp.Range(strRangeAdress & ":" & strRangeAdress)) Is Nothing Then
                        sapShapeItem.Delete()
                    End If
                End If
            Next sapShapeItem
        Else
            '选项按钮选择的删除区域执行以下代码.
            rngSelectRange = xlapp.InputBox(Prompt:="请选择要删除的区域", Default:=xlapp.Selection.Address, Type:=8)
            If rngSelectRange Is Nothing Then Exit Sub        '如果没有选择区域,就退出过程
            For Each sapShapeItem In xlapp.ActiveSheet.Shapes   '遍历本表所有图形对象
                If sapShapeItem.Type <> 3 Then '如果不是图表
                    If Not xlapp.Intersect(xlapp.Range(sapShapeItem.TopLeftCell, sapShapeItem.BottomRightCell), rngSelectRange) Is Nothing Then
                        sapShapeItem.Delete()
                    End If
                End If
            Next sapShapeItem
        End If
        'CommandButton3_Click(Me, New EventArgs())  '调用带参数的事件,这个技术来自百度,可以输入参数,调用任何事件
        btnQuit_Click(Nothing, Nothing)      '调用方法.
    End Sub

    '窗体激活时触发事件
    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '遍历数字范围,计数器作为单元格列索引.其中Split索引为0显示的将是空值"".
        Dim intStep As Integer
        For intStep = 1 To 256 '从1到256
            cboDisplayAdress.Items.Add(Split(xlapp.Cells(1, intStep).Address, "$")(1))
        Next intStep
        cboDisplayAdress.Text = Split(xlapp.Cells(1, xlapp.ActiveCell.Column).Address, "$")(1) '让复合框默认显示当前列的列标
    End Sub

    '窗体关闭后触发事件
    Private Sub 列和区域删除图_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnColumnAndAreaDeletePicture.Enabled = True  '按钮重新启用.
    End Sub

    '关闭窗体
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Me.Close()  '关闭窗体,vsto中不能使用unload me语句
        列和区域删除图_Closed(Nothing, Nothing)
    End Sub

    '选中了删除区域图片
    Private Sub optApointAreaDeletePicture_Click(sender As Object, e As EventArgs) Handles optApointAreaDeletePicture.Click
        Label1.Text = "请选择你要删除的区域,然后按确定键执行." : Label2.Visible = False : cboDisplayAdress.Visible = False
        btnOk_Click(Nothing, Nothing)
    End Sub

    '选中了删列图片
    Private Sub optDeleteColumnPicture_Click(sender As Object, e As EventArgs) Handles optDeleteColumnPicture.Click
        Label1.Text = "在" : Label2.Visible = True : cboDisplayAdress.Visible = True
    End Sub


End Class