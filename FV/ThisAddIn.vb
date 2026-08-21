Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Diagnostics

Public Class ThisAddIn

    ' ===== 变量声明区域 =====

    ' ★★★ XLAM 加载标记 ★★★
    Public Shared xlamLoaded As Boolean = False

    Private 检查定时器 As System.Windows.Forms.Timer

    Public Shared objTargetSheet As Excel.Worksheet = Nothing
    Public Shared strTargetRng As String = ""

    Public 任务窗格 As Microsoft.Office.Tools.CustomTaskPane = Nothing

    ' ★★★ 使用 Excel.Workbook（COM 原生类型） ★★★
    Private 窗格字典 As New Dictionary(Of Excel.Workbook, Microsoft.Office.Tools.CustomTaskPane)
    Private 控件字典 As New Dictionary(Of Excel.Workbook, UserControl2)

    ' 用于跟踪已处理的工作簿
    Private 已处理工作簿列表 As New List(Of String)

    ' ============================================================
    ' ★★★ 插件启动事件 ★★★
    ' ============================================================
    Private Sub ThisAddIn_Startup(sender As Object, e As System.EventArgs) Handles Me.Startup
        Try
            If xlapp Is Nothing Then
                MessageBox.Show("无法连接到Excel应用程序", "错误")
                Return
            End If

            ' 注册事件
            AddHandler xlapp.WorkbookBeforeClose, AddressOf Application_WorkbookBeforeClose
            AddHandler xlapp.SheetChange, AddressOf OnSheetChange
            AddHandler xlapp.WorkbookActivate, AddressOf Application_WorkbookActivate
            AddHandler xlapp.SheetActivate, AddressOf Application_SheetActivate

            ' ★★★ 延迟 2 秒创建任务窗格，让 Excel 界面先完全显示 ★★★
            Dim paneTimer As New System.Windows.Forms.Timer()
            paneTimer.Interval = 500
            AddHandler paneTimer.Tick, Sub()
                                           For Each 工作簿 As Excel.Workbook In xlapp.Workbooks
                                               为工作簿添加窗格(工作簿)
                                           Next

                                           检查定时器 = New System.Windows.Forms.Timer()
                                           检查定时器.Interval = 1000
                                           AddHandler 检查定时器.Tick, AddressOf 检查新工作簿
                                           检查定时器.Start()

                                           paneTimer.Stop()
                                           paneTimer.Dispose()
                                       End Sub
            paneTimer.Start()

            Debug.WriteLine("插件启动完成")

        Catch ex As Exception
            MessageBox.Show("启动错误: " & ex.Message)

        End Try
    End Sub

    ' ============================================================
    ' ★★★ 加载 XLAM（按需加载） ★★★
    ' ============================================================
    ' ============================================================
    ' ★★★ 加载 XLAM（按需加载） ★★★
    ' ============================================================
    Public Shared Sub LoadXlamOnDemand()
        If xlamLoaded Then Exit Sub

        Try
            Dim xlamPath As String = "C:\Program Files\FV\FV.xlam"

            If Not System.IO.File.Exists(xlamPath) Then
                Debug.WriteLine("FV.xlam 文件不存在")
                Exit Sub
            End If

            ' ★★★ 检查是否已加载（按显示名称 "Fv"） ★★★
            Try
                Dim addin As Excel.AddIn = xlapp.AddIns("Fv")
                If addin IsNot Nothing Then
                    If Not addin.Installed Then
                        addin.Installed = True
                    End If
                    xlamLoaded = True
                    Debug.WriteLine("FV.xlam 已加载")
                    Return
                End If
            Catch
            End Try

            ' 未加载则添加
            Dim newAddin As Excel.AddIn = xlapp.AddIns.Add(xlamPath, True)
            newAddin.Installed = True
            xlamLoaded = True
            Debug.WriteLine("FV.xlam 加载成功")

        Catch ex As Exception
            Debug.WriteLine("加载 FV.xlam 失败: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ★★★ 插件关闭事件 ★★★
    ' ============================================================
    Private Sub ThisAddIn_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
        Try
            '' ★★★ 卸载 XLAM，清理注册表 ★★★
            'UnloadXlam()


            If 检查定时器 IsNot Nothing Then
                检查定时器.Stop()
                检查定时器.Dispose()
            End If

            ' ★★★ 取消注册事件，释放引用 ★★★
            RemoveHandler xlapp.WorkbookBeforeClose, AddressOf Application_WorkbookBeforeClose
            RemoveHandler xlapp.SheetChange, AddressOf OnSheetChange
            RemoveHandler xlapp.WorkbookActivate, AddressOf Application_WorkbookActivate
            RemoveHandler xlapp.SheetActivate, AddressOf Application_SheetActivate
        Catch
        End Try

        ' 清理所有窗格
        Dim 所有工作簿 As New List(Of Excel.Workbook)(窗格字典.Keys)
        For Each 工作簿 In 所有工作簿
            移除工作簿窗格(工作簿)
        Next

        窗格字典.Clear()
        控件字典.Clear()
        任务窗格 = Nothing
        已处理工作簿列表.Clear()

        Debug.WriteLine("插件已关闭")
    End Sub

    ' ★★★ 在工作簿关闭前取消勾选（更早触发） ★★★
    Private Sub Application_WorkbookBeforeClose(ByVal Wb As Excel.Workbook, ByRef Cancel As Boolean)
        Try
            Dim addin As Excel.AddIn = xlapp.AddIns("Fv")
            If addin IsNot Nothing Then
                addin.Installed = False
                xlamLoaded = False
                Debug.WriteLine("FV.xlam 已取消勾选（WorkbookBeforeClose）")
            End If
        Catch
        End Try
    End Sub


    'Public Shared Sub UnloadXlam()
    '    Try
    '        ' ★★★ 使用加载项对话框中显示的名称 "Fv" ★★★
    '        Dim addin As Excel.AddIn = xlapp.AddIns("Fv")
    '        If addin IsNot Nothing Then
    '            addin.Installed = False
    '            xlamLoaded = False
    '            Debug.WriteLine("FV.xlam 已卸载（取消勾选）")
    '        End If
    '    Catch ex As Exception
    '        Debug.WriteLine("卸载 FV.xlam 失败: " & ex.Message)
    '    End Try
    'End Sub

    ' ============================================================
    ' ★★★ 定时检查新工作簿 ★★★
    ' ============================================================
    Private Sub 检查新工作簿(sender As Object, e As EventArgs)
        Try
            If xlapp Is Nothing Then Exit Sub

            For Each 工作簿 As Excel.Workbook In xlapp.Workbooks
                Dim key As String = 工作簿.Name
                If Not 已处理工作簿列表.Contains(key) Then
                    已处理工作簿列表.Add(key)
                    为工作簿添加窗格(工作簿)
                    Debug.WriteLine("定时器检测到新工作簿: " & key)
                End If
            Next

            ' 检查是否有工作簿被关闭
            Dim 待移除列表 As New List(Of String)
            For Each key In 已处理工作簿列表
                Dim 存在 As Boolean = False
                For Each 工作簿 As Excel.Workbook In xlapp.Workbooks
                    If 工作簿.Name = key Then
                        存在 = True
                        Exit For
                    End If
                Next
                If Not 存在 Then
                    待移除列表.Add(key)
                End If
            Next

            For Each key In 待移除列表
                已处理工作簿列表.Remove(key)
            Next

        Catch ex As Exception
            Debug.WriteLine("检查新工作簿错误: " & ex.Message)
        End Try
    End Sub


    ' ============================================================
    ' ★★★ 为工作簿添加窗格 ★★★
    ' ============================================================
    Private Sub 为工作簿添加窗格(ByVal 目标工作簿 As Excel.Workbook)
        Try
            ' 如果已存在窗格，跳过
            If 窗格字典.ContainsKey(目标工作簿) Then Exit Sub

            ' 1. 创建用户控件实例
            Dim 新用户控件 As New UserControl2()

            ' 2. 获取工作簿对应的窗口
            Dim 目标窗口 As Excel.Window = Nothing
            If 目标工作簿.Windows.Count > 0 Then
                目标窗口 = 目标工作簿.Windows(1)
            Else
                目标窗口 = xlapp.ActiveWindow
            End If

            If 目标窗口 Is Nothing Then
                Debug.WriteLine("无法为 " & 目标工作簿.Name & " 找到窗口")
                Exit Sub
            End If

            ' 3. 创建任务窗格，关联到工作簿窗口
            Dim 窗格标题 As String = "FV 任务窗格"
            Dim 新任务窗格 As Microsoft.Office.Tools.CustomTaskPane =
                Me.CustomTaskPanes.Add(新用户控件, 窗格标题, 目标窗口)

            ' 4. 设置基本属性：停靠右侧，宽度 300，可见
            With 新任务窗格
                .DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight
                .Width = 300
                .Visible = True
            End With

            ' 5. ★★★ 延迟调整高度（150ms），让窗格先完成加载 ★★★
            Dim heightTimer As New System.Windows.Forms.Timer()
            heightTimer.Interval = 150
            AddHandler heightTimer.Tick, Sub()
                                             Try
                                                 ' 获取 Excel 窗口高度，减去 30 磅（状态栏+边距）
                                                 Dim paneHeight As Integer = xlapp.ActiveWindow.Height - 30
                                                 If paneHeight > 100 Then
                                                     新任务窗格.Height = paneHeight
                                                 End If
                                             Catch
                                             End Try
                                             heightTimer.Stop()
                                             heightTimer.Dispose()
                                         End Sub
            heightTimer.Start()

            ' 6. 保存到字典
            控件字典.Add(目标工作簿, 新用户控件)
            窗格字典.Add(目标工作簿, 新任务窗格)

            ' 7. 监听工作簿关闭事件，自动移除窗格
            AddHandler 目标工作簿.BeforeClose,
                Sub(ByRef 取消 As Boolean)
                    移除工作簿窗格(目标工作簿)
                End Sub

            ' 8. 填充 TreeView 数据
            新用户控件.FillTvw()

            ' 9. 设置全局任务窗格引用（供 Ribbon 按钮使用）
            If 任务窗格 Is Nothing Then
                任务窗格 = 新任务窗格
            End If

            Debug.WriteLine("已为 " & 目标工作簿.Name & " 创建窗格")

        Catch ex As Exception
            Debug.WriteLine("为 " & 目标工作簿.Name & " 创建窗格失败: " & ex.Message)
        End Try
    End Sub


    'Private Sub 为工作簿添加窗格(ByVal 目标工作簿 As Excel.Workbook)
    '    Try
    '        If 窗格字典.ContainsKey(目标工作簿) Then Exit Sub

    '        Dim 新用户控件 As New UserControl2()

    '        Dim 目标窗口 As Excel.Window = Nothing
    '        If 目标工作簿.Windows.Count > 0 Then
    '            目标窗口 = 目标工作簿.Windows(1)
    '        Else
    '            目标窗口 = xlapp.ActiveWindow
    '        End If

    '        If 目标窗口 Is Nothing Then
    '            Debug.WriteLine("无法为 " & 目标工作簿.Name & " 找到窗口")
    '            Exit Sub
    '        End If

    '        Dim 窗格标题 As String = "FV 任务窗格"
    '        Dim 新任务窗格 As Microsoft.Office.Tools.CustomTaskPane =
    '        Me.CustomTaskPanes.Add(新用户控件, 窗格标题, 目标窗口)

    '        ' ★★★ 先设置基本属性并显示 ★★★
    '        With 新任务窗格
    '            .DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight
    '            .Width = 300
    '            .Visible = True
    '        End With

    '        ' ★★★ 延迟调整高度，确保窗格已完全加载 ★★★
    '        Dim heightTimer As New System.Windows.Forms.Timer()
    '        heightTimer.Interval = 100
    '        AddHandler heightTimer.Tick, Sub()
    '                                         Try
    '                                             ' 获取 Excel 窗口的工作区高度（不包括标题栏）
    '                                             Dim excelHeight As Integer = xlapp.ActiveWindow.Height
    '                                             ' 减去状态栏高度（约 20-25 磅），让窗格底部与状态栏对齐
    '                                             Dim paneHeight As Integer = excelHeight - 25
    '                                             If paneHeight > 100 Then
    '                                                 新任务窗格.Height = paneHeight
    '                                                 Debug.WriteLine("任务窗格高度已调整: " & paneHeight)
    '                                             End If
    '                                         Catch
    '                                         End Try
    '                                         heightTimer.Stop()
    '                                         heightTimer.Dispose()
    '                                     End Sub
    '        heightTimer.Start()

    '        控件字典.Add(目标工作簿, 新用户控件)
    '        窗格字典.Add(目标工作簿, 新任务窗格)

    '        AddHandler 目标工作簿.BeforeClose,
    '        Sub(ByRef 取消 As Boolean)
    '            移除工作簿窗格(目标工作簿)
    '        End Sub

    '        新用户控件.FillTvw()

    '        If 任务窗格 Is Nothing Then
    '            任务窗格 = 新任务窗格
    '        End If

    '        Debug.WriteLine("已为 " & 目标工作簿.Name & " 创建窗格")

    '    Catch ex As Exception
    '        Debug.WriteLine("为 " & 目标工作簿.Name & " 创建窗格失败: " & ex.Message)
    '    End Try
    'End Sub


    ' ============================================================
    ' ★★★ 移除工作簿窗格 ★★★
    ' ============================================================
    Private Sub 移除工作簿窗格(ByVal 工作簿 As Excel.Workbook)
        Try
            If 窗格字典.ContainsKey(工作簿) Then
                Dim 窗格 = 窗格字典(工作簿)
                Me.CustomTaskPanes.Remove(窗格)
                窗格字典.Remove(工作簿)
            End If

            If 控件字典.ContainsKey(工作簿) Then
                控件字典.Remove(工作簿)
            End If

            Debug.WriteLine("已移除 " & 工作簿.Name & " 的窗格")
        Catch ex As Exception
            Debug.WriteLine("移除 " & 工作簿.Name & " 窗格失败: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' ★★★ 事件处理程序 ★★★
    ' ============================================================

    Private Sub Application_WorkbookActivate(ByVal 工作簿 As Excel.Workbook)
        Try
            If 控件字典.ContainsKey(工作簿) Then
                控件字典(工作簿).FillTvw()
            End If
        Catch
        End Try
    End Sub

    Private Sub Application_SheetActivate(工作表 As Object)
        Try
            Dim 当前工作簿 As Excel.Workbook = xlapp.ActiveWorkbook
            If 当前工作簿 IsNot Nothing AndAlso 控件字典.ContainsKey(当前工作簿) Then
                控件字典(当前工作簿).FillTvw()
            End If
        Catch
        End Try
    End Sub

    Private Sub OnSheetChange(Sh As Object, Target As Excel.Range)
        Try
            ThisAddIn.objTargetSheet = xlapp.ActiveSheet
            ThisAddIn.strTargetRng = xlapp.ActiveSheet.UsedRange.Address
        Catch
        End Try
    End Sub

    ' ============================================================
    ' ★★★ 公开方法（供 Ribbon 按钮使用） ★★★
    ' ============================================================

    Public Function 获取当前任务窗格() As Microsoft.Office.Tools.CustomTaskPane
        Dim 当前工作簿 As Excel.Workbook = xlapp.ActiveWorkbook
        If 当前工作簿 IsNot Nothing AndAlso 窗格字典.ContainsKey(当前工作簿) Then
            Return 窗格字典(当前工作簿)
        End If
        Return Nothing
    End Function

    Public Sub 切换任务窗格()
        Dim pane = 获取当前任务窗格()
        If pane IsNot Nothing Then
            pane.Visible = Not pane.Visible
        Else
            MessageBox.Show("当前工作簿没有任务窗格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class