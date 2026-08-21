
'所有代码在deepseek检查存储
Imports System.Diagnostics
Imports System.Windows.Forms    '声明窗体命名空间.
Public Class UserControl2
    Dim tndTagetNode As New TreeNode    '实例化一个节点的实例对象,鼠标单击Node事件会用到

    '=== 新增：保存所有工作簿的展开状态 ===
    Private 已展开工作簿 As New List(Of String)

    '加载用户控件时,触发该事件.
    Private Sub UserControl2_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' ★★★ 让 TreeView 填充整个用户控件 ★★★
        Me.Dock = DockStyle.Fill          ' ← 这行需要添加
        TreeView1.Dock = DockStyle.Fill
        FillTvw()   '调用方法
    End Sub

    '填充树型控件内容 - 添加参数控制是否保存展开状态
    Public Sub FillTvw(Optional ByVal 保存展开状态 As Boolean = False)
        On Error Resume Next

        '=== 如果需要保存展开状态，先记录当前展开的工作簿 ===
        If 保存展开状态 Then
            已展开工作簿.Clear()
            For Each node As TreeNode In TreeView1.Nodes
                If node.IsExpanded Then
                    已展开工作簿.Add(node.Text)
                End If
            Next
        End If

        TreeView1.Nodes.Clear()

        For Each wkb As Microsoft.Office.Interop.Excel.Workbook In xlapp.Workbooks
            Dim wkbNode As TreeNode = TreeView1.Nodes.Add(wkb.Name, wkb.Name)

            For Each wks As Microsoft.Office.Interop.Excel.Worksheet In wkb.Sheets
                wkbNode.Nodes.Add(wks.Name, wks.Name)
            Next
        Next

        '=== 恢复之前展开的工作簿 ===
        If 已展开工作簿.Count > 0 Then
            For Each node As TreeNode In TreeView1.Nodes
                If 已展开工作簿.Contains(node.Text) Then
                    node.Expand()
                End If
            Next
        End If

        '=== 确保活动工作簿展开（如果它还没展开） ===
        If xlapp.ActiveWorkbook IsNot Nothing Then
            Dim activeWkbNode As TreeNode = TreeView1.Nodes(xlapp.ActiveWorkbook.Name)
            If activeWkbNode IsNot Nothing Then
                activeWkbNode.Expand()

                '选中活动工作表
                If xlapp.ActiveSheet IsNot Nothing Then
                    For Each child As TreeNode In activeWkbNode.Nodes
                        If child.Text = xlapp.ActiveSheet.Name Then
                            TreeView1.SelectedNode = child
                            Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 点击节点 → 直接激活对应的窗口（像Windows文件管理器一样）
    ''' </summary>
    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick


        Try
            Dim 节点 As TreeNode = e.Node
            If 节点.Parent Is Nothing Then
                ' 点击的是工作簿节点 → 直接激活工作簿
                xlapp.Workbooks(节点.Text).Activate()
            Else
                ' 点击的是工作表节点 → 先激活工作簿，再激活工作表
                Dim wbName As String = 节点.Parent.Text
                xlapp.Workbooks(wbName).Activate()
                xlapp.ActiveWorkbook.Sheets(节点.Text).Activate()
            End If
        Catch
        End Try
    End Sub

End Class








