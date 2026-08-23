Imports System.Windows.Forms
Imports System.Text
Imports System.IO

Public Class WIN190810_VBA代码笔记
    Dim myArray() As String                       '声明数组变量,数组长度为要引用的数据表字段数量.
    ' ★★★ 新增：用于在内存中存放所有代码笔记的列表容器 ★★★
    ' List(Of String()) 表示一个列表，里面的每一项都是一个字符串数组
    ' 每个字符串数组包含4个元素：标题、编号、备注、代码正文
    Private allNotes As List(Of String())
    Private intEditingIndex As Integer = -1   ' -1 表示没有正在编辑的笔记

    REM 功    能: 宝3-17.5.7-P387 在窗体中跨工作表查询 关键词：array,combox1


    ''' <summary>
    ''' 从资源文件加载所有代码笔记到内存列表
    ''' </summary>
    ''' <returns>包含所有笔记的列表，每条笔记是一个4元素字符串数组</returns>
    Private Function LoadNotesFromResource() As List(Of String())
        ' 1. 创建一个空的列表，用来存放所有笔记
        Dim lstNotes As New List(Of String())()

        ' 2. 定义资源名称的前缀，只加载以 "Snippet_" 开头的资源项
        Dim strResourcePrefix As String = "Snippet_"

        ' 3. 获取资源文件中所有的资源项
        '    ResourceManager 负责管理 .resx 文件中的资源
        '    GetResourceSet 返回所有资源的集合
        Dim objResSet As System.Resources.ResourceSet = My.Resources.CodeNotes.ResourceManager.GetResourceSet(
            Threading.Thread.CurrentThread.CurrentCulture,
            True,
            True
        )

        ' 4. 遍历资源集合中的每一项
        For Each objEntry As System.Collections.DictionaryEntry In objResSet
            ' 获取当前资源的名称（比如 "Snippet_001"）
            Dim strKey As String = objEntry.Key.ToString()

            ' 5. 只处理以 "Snippet_" 开头的资源（过滤掉其他无关资源）
            If strKey.StartsWith(strResourcePrefix) Then
                ' 获取资源的值（就是那串用 | 分隔的文本）
                Dim strValue As String = objEntry.Value.ToString()

                ' 6. 按 | 符号拆分，得到4个字段的数组
                '    arrFields(0) = 标题
                '    arrFields(1) = 编号
                '    arrFields(2) = 备注
                '    arrFields(3) = 代码正文（含 \n 换行符）
                Dim arrFields As String() = strValue.Split(New Char() {"|"c, "｜"c}, StringSplitOptions.None)

                ' 7. 确保拆分出来的字段数量至少是4个（防止数据不完整）
                If arrFields.Length >= 4 Then
                    ' 8. ★★★ 关键步骤：将代码正文中的 \n 还原成真正的换行符 ★★★
                    '    资源文件中存的是 "Sub xxx()\n    Dim rng\nEnd Sub"
                    '    这里把 \n 替换成 vbCrLf（VB中的换行符），变成真正的多行文本
                    arrFields(3) = arrFields(3).Replace("\n", vbCrLf)

                    ' 9. 将处理好的这条笔记（4个字段）添加到列表中
                    lstNotes.Add(arrFields)
                End If
            End If
        Next

        ' 10. 返回加载完成的所有笔记列表
        Return lstNotes
    End Function

    ''' <summary>
    ''' 从外部文件加载所有代码笔记到内存列表
    ''' </summary>
    Private Function LoadNotesFromFile() As List(Of String())
        ' 1. 定义用户数据目录下的文件路径
        Dim strUserFilePath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FV_VSTO", "CodeNotes.txt")

        ' 2. 确保用户目录存在
        Dim strUserDirectory As String = System.IO.Path.GetDirectoryName(strUserFilePath)
        If Not System.IO.Directory.Exists(strUserDirectory) Then
            System.IO.Directory.CreateDirectory(strUserDirectory)
        End If

        ' 3. ★★★ 如果用户目录下没有 CodeNotes.txt，从安装目录复制 ★★★
        If Not System.IO.File.Exists(strUserFilePath) Then
            ' 获取安装目录下的文件路径（插件所在的目录）
            Dim strInstallFilePath As String = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodeNotes.txt")

            ' 如果安装目录下的文件存在，则复制到用户目录
            If System.IO.File.Exists(strInstallFilePath) Then
                System.IO.File.Copy(strInstallFilePath, strUserFilePath, True)
            Else
                ' 如果安装目录下也没有文件，返回空列表
                Return New List(Of String())()
            End If
        End If

        ' 4. 从用户目录读取文件
        Dim lines As String() = System.IO.File.ReadAllLines(strUserFilePath, System.Text.Encoding.UTF8)
        Dim lstNotes As New List(Of String())()

        For Each line As String In lines
            If Not String.IsNullOrWhiteSpace(line) Then
                Dim arrFields As String() = line.Split(New Char() {"|"c}, StringSplitOptions.None)
                If arrFields.Length >= 4 Then
                    arrFields(3) = arrFields(3).Replace("\n", vbCrLf)
                    lstNotes.Add(arrFields)
                End If
            End If
        Next

        Return lstNotes
    End Function


    ''' <summary>
    ''' 将内存中的笔记列表保存到外部文件
    ''' </summary>
    Private Sub SaveNotesToFile(ByVal lstNotes As List(Of String()))


        ' 1. 构建文件路径
        ' ★★★ 修改后 ★★★
        Dim strFilePath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FV_VSTO", "CodeNotes.txt")

        ' 确保目录存在
        Dim strDirectory As String = System.IO.Path.GetDirectoryName(strFilePath)
        If Not System.IO.Directory.Exists(strDirectory) Then
            System.IO.Directory.CreateDirectory(strDirectory)
        End If
        ' 2. 准备要写入的行列表
        Dim lines As New List(Of String)()

        For Each arrNote As String() In lstNotes
            ' 将代码正文中的换行符替换为 \n 用于存储
            Dim strCodeForStorage As String = arrNote(3).Replace(vbCrLf, "\n")
            ' 组合成一行：标题|编号|备注|代码
            Dim strLine As String = arrNote(0) & "|" & arrNote(1) & "|" & arrNote(2) & "|" & strCodeForStorage
            lines.Add(strLine)
        Next

        ' 3. 写入文件
        System.IO.File.WriteAllLines(strFilePath, lines, System.Text.Encoding.UTF8)
    End Sub

    '当在文字框中输入新的查找对象时，清空列表框中上一次的结果
    Private Sub TextBox1_Change()
        On Error Resume Next
        With ListView1         '引用视图控件
            '设置ListView1的标题、显示类型、整行选择和网格线属性
            .Columns.Clear()        '清除标题行
            .Clear()                '清除项目集
            .View = View.Details    '报表输出视图
            .FullRowSelect = True   '允许整行选择
            .GridLines = True       '允许网格线
        End With
    End Sub


    ''' <summary>
    ''' 当窗体被激活时触发（每次切换到该窗体时）
    ''' 负责初始化下拉框、加载数据、刷新列表
    ''' </summary>
    Private Sub WIN190810_VBA代码笔记_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ' ★★★ 1. 初始化下拉框（查询条件） ★★★
        ' 定义下拉框显示的两个选项
        myArray = {"详细代码", "备注"}
        ComboBox1.Items.Clear()               ' 清空原有项
        ComboBox1.Items.AddRange(myArray)     ' 添加新项
        ComboBox1.SelectedIndex = 0           ' 默认选中第一项 "详细代码"

        ' ★★★ 2. ★★★ 从资源文件加载所有代码笔记到内存 ★★★
        ' 调用刚才添加的 LoadNotesFromResource 方法
        allNotes = LoadNotesFromFile()
        ' 调试代码已移除，无弹窗

        ' ★★★ 3. 刷新列表显示（显示全部笔记） ★★★
        ' 调用刷新的方法，传入全部笔记（不进行筛选）
        RefreshListView(allNotes)
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)

        If ListView1.Items.Count > 0 Then
            ListView1.Items(0).Selected = True
            ListView1.Focus()
        End If

    End Sub

    ''' <summary>
    ''' 查询按钮：根据下拉框选中的字段和文本框输入的关键词，筛选并显示笔记
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ' ★★★ 1. 安全检查：如果 allNotes 为空，先加载数据 ★★★
        ' 防止在窗体未激活时点击查询导致空引用错误
        If allNotes Is Nothing OrElse allNotes.Count = 0 Then
            allNotes = LoadNotesFromResource()
        End If

        ' ★★★ 2. 获取用户输入的关键词（去除首尾空格） ★★★
        Dim strSearchText As String = TextBox1.Text.Trim()

        ' ★★★ 3. 判断下拉框选中的是“详细代码”还是“备注” ★★★
        '    下拉框索引0对应“详细代码”，索引1对应“备注”
        '    在数据数组中，索引0是标题，索引3是代码正文
        '    为了更准确搜索，我们定义：
        '    当选择“详细代码”时，搜索标题（arr(0)）+ 代码正文（arr(3)）
        '    当选择“备注”时，只搜索备注（arr(2)）
        Dim intColIndex As Integer
        If ComboBox1.SelectedIndex = 0 Then
            ' 选择“详细代码”：搜索标题 + 代码正文（两个字段合并）
            intColIndex = -1  ' 用 -1 表示特殊处理：搜索标题+代码正文
        Else
            ' 选择“备注”：只搜索备注字段（索引2）
            intColIndex = 2
        End If

        ' ★★★ 4. 创建筛选结果列表 ★★★
        Dim lstFiltered As New List(Of String())()

        ' ★★★ 5. 判断用户是否输入了关键词 ★★★
        If String.IsNullOrEmpty(strSearchText) Then
            ' 如果关键词为空，显示全部笔记
            lstFiltered = allNotes
        Else
            ' 如果有关键词，遍历所有笔记进行筛选
            For Each arrNote As String() In allNotes
                Dim blnMatch As Boolean = False

                If intColIndex = -1 Then
                    ' 搜索“详细代码”模式：在标题（索引0）和代码正文（索引3）中查找
                    If arrNote(0).IndexOf(strSearchText, StringComparison.OrdinalIgnoreCase) >= 0 Then
                        blnMatch = True
                    ElseIf arrNote(3).IndexOf(strSearchText, StringComparison.OrdinalIgnoreCase) >= 0 Then
                        blnMatch = True
                    End If
                Else
                    ' 搜索“备注”模式：只在备注（索引2）中查找
                    If arrNote(intColIndex).IndexOf(strSearchText, StringComparison.OrdinalIgnoreCase) >= 0 Then
                        blnMatch = True
                    End If
                End If

                ' 如果匹配，将这条笔记加入筛选结果列表
                If blnMatch Then
                    lstFiltered.Add(arrNote)
                End If
            Next
        End If

        ' ★★★ 6. 调用刷新方法，显示筛选结果 ★★★
        RefreshListView(lstFiltered)
    End Sub



    ''' <summary>
    ''' 刷新 ListView 显示指定的笔记列表
    ''' </summary>
    ''' <param name="lstData">要显示的笔记列表，每条笔记是一个4元素字符串数组</param>
    Private Sub RefreshListView(lstData As List(Of String()))
        ' 1. 清空 ListView 中现有的所有行和列
        ListView1.Items.Clear()
        ListView1.Columns.Clear()

        ' 2. 设置 ListView 的显示模式为详细信息（表格样式）
        ListView1.View = View.Details

        ' 3. 添加列标题，并设置宽度
        ListView1.Columns.Add("标题", 300)
        ListView1.Columns.Add("编号", 100)
        ListView1.Columns.Add("备注", 300)
        ListView1.Columns.Add("代码正文", 600)

        ' 4. 检查传入的数据是否为空
        If lstData Is Nothing Then
            ' 如果数据为空，直接退出
            Return
        End If

        ' 5. 遍历列表中的每一条笔记，添加到 ListView 中
        For Each arrNote As String() In lstData
            ' 检查数组是否包含4个元素，并且第一个元素不为空
            If arrNote IsNot Nothing AndAlso arrNote.Length >= 4 AndAlso Not String.IsNullOrEmpty(arrNote(0)) Then
                ' 创建一行，第一列显示标题
                Dim itm As New ListViewItem(arrNote(0))
                ' 添加后续列
                itm.SubItems.Add(arrNote(1))   ' 编号
                itm.SubItems.Add(arrNote(2))   ' 备注
                itm.SubItems.Add(arrNote(3))   ' 代码正文
                ' 将整行添加到 ListView
                ListView1.Items.Add(itm)
            End If
        Next

        ' 6. 强制禁止自动调整列宽（防止列宽被重置）
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)
    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        If e.IsSelected Then
            ' 选中时，显示代码正文
            txtCodeDetail.Text = e.Item.SubItems(3).Text
        Else
            ' 取消选中时，清空代码文本框
            txtCodeDetail.Text = ""
        End If
    End Sub


    'Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
    '    Dim Sht As Excel.Worksheet, Rng As Excel.Range, targetrng As Excel.Range, msg1 As MsgBoxResult '声明变量
    '    'On Error Resume Next



    '    For Each Sht In xlapp.Worksheets        '在工作表集合中循环
    '        Sht.Activate()      '激活该活动工作表
    '        For Each Rng In Sht.Range(xlapp.Cells(1, 2), xlapp.Cells(xlapp.Cells.Rows.Count, 2).End(-4162))      '在已用单元格中循环
    '            'If Rng.Value <> "" And Rng.Value = e.Item.SubItems(1).Text Then  '如果有单元格与列表框所选的值相同,则执行下面语句
    '            If Rng.Value <> "" And Rng.Offset(0, -1).Value = e.Item.SubItems(0).Text And Rng.Value = e.Item.SubItems(1).Text Then  '如果有单元格与列表框所选的值相同,则执行下面语句

    '                'msg1 = MsgBox(Rng.Value & Chr(10) & "选择是：选中对应单元格；" + Chr(10) + "选择否：仅退出该提示窗口。", vbYesNo, "操作方式")  '提示信息结果赋值给变量
    '                '    'msg1 = MsgBox(Rng.Value & Chr(10) & "选择是：选中对应单元格；" + Chr(10) + "选择否：仅退出该提示窗口。", vbYesNo, "操作方式")  '提示信息结果赋值给变量
    '                'If msg1 = vbYes Then  '如果选择了是那么执行下面语句
    '                '    Rng.Select()  '选中单元格
    '                '    Exit Sub     '退出程序
    '                'End If

    '                'msg1 = MsgBox(Rng.Value & Chr(10) & "选择是：选中对应单元格；" + Chr(10) + "选择否：仅退出该提示窗口。", vbYesNo, "操作方式")  '提示信息结果赋值给变量
    '                'If msg1 = vbYes Then  '如果选择了是那么执行下面语句
    '                Rng.Select()  '选中单元格
    '                Exit Sub     '退出程序
    '                'End If





    '            End If
    '        Next Rng
    '    Next Sht
    '    'If e.IsSelected Then MsgBox(e.Item.SubItems(1).Text) '文本框显示鼠标选择项第二列内容
    'End Sub



    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then btnSearch_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub



    ''' <summary>
    ''' 复制代码按钮：将当前显示的代码正文复制到剪贴板
    ''' </summary>
    Private Sub btnCopyCode_Click(sender As Object, e As EventArgs) Handles btnCopyCode.Click
        ' 1. 检查文本框是否有内容
        If String.IsNullOrEmpty(txtCodeDetail.Text) Then
            MessageBox.Show("没有可复制的代码，请先选择一条笔记。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 2. 将文本框内容复制到剪贴板
        Try
            Clipboard.SetText(txtCodeDetail.Text)
            MessageBox.Show("代码已复制到剪贴板！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("复制失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 保存笔记：将用户输入的新笔记添加到列表并保存到文件
    ''' </summary>
    Private Sub btnAddNote_Click(sender As Object, e As EventArgs)
        ' 1. 获取用户输入
        Dim strTitle As String = txtNewTitle.Text.Trim()
        Dim strRemark As String = txtNewRemark.Text.Trim()
        Dim strCode As String = txtNewCode.Text.Trim()

        ' 2. 验证标题
        If String.IsNullOrEmpty(strTitle) Then
            MessageBox.Show("请输入笔记标题！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNewTitle.Focus()
            Return
        End If

        ' 3. 验证代码正文
        If String.IsNullOrEmpty(strCode) Then
            MessageBox.Show("请输入代码正文！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNewCode.Focus()
            Return
        End If

        ' ★★★ 判断是新增还是编辑（根据模块级变量 intEditingIndex） ★★★
        Dim blnIsEditing As Boolean = (intEditingIndex >= 0)
        Dim intEditIndex As Integer = intEditingIndex

        ' 4. 生成新ID（如果是编辑模式，使用原有ID）
        Dim strNewID As String
        If blnIsEditing AndAlso intEditIndex >= 0 AndAlso intEditIndex < allNotes.Count Then
            strNewID = allNotes(intEditIndex)(1)
        Else
            ' 新增模式：生成新ID
            Dim intMaxID As Integer = 0
            If allNotes IsNot Nothing AndAlso allNotes.Count > 0 Then
                For Each arrNote As String() In allNotes
                    Dim strID As String = arrNote(1).Replace("GN", "").Trim()
                    Dim intID As Integer = 0
                    If Integer.TryParse(strID, intID) Then
                        If intID > intMaxID Then intMaxID = intID
                    End If
                Next
            End If
            strNewID = "GN" & (intMaxID + 1).ToString("D3")
        End If

        ' 5. 将代码中的换行符替换成 \n 用于存储
        Dim strCodeForStorage As String = strCode.Replace(vbCrLf, "\n")

        ' 6. 创建新笔记数组
        Dim arrNewNote As String() = {strTitle, strNewID, strRemark, strCodeForStorage}

        ' 7. ★★★ 添加到内存列表或更新 ★★★
        If blnIsEditing AndAlso intEditIndex >= 0 AndAlso intEditIndex < allNotes.Count Then
            ' 编辑模式：替换原有笔记
            allNotes(intEditIndex) = arrNewNote
        Else
            ' 新增模式：添加到列表
            If allNotes Is Nothing Then
                allNotes = New List(Of String())()
            End If
            allNotes.Add(arrNewNote)
        End If

        ' 8. ★★★ 保存到文件 ★★★
        SaveNotesToFile(allNotes)

        ' 9. 刷新列表显示
        RefreshListView(allNotes)

        ' 10. 清空输入框
        txtNewTitle.Clear()
        txtNewRemark.Clear()
        txtNewCode.Clear()

        ' ★★★ 重置编辑状态 ★★★
        intEditingIndex = -1

        MessageBox.Show("笔记保存成功！" & If(blnIsEditing, "已更新", "新ID：" & strNewID), "完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnDeleteNote_Click(sender As Object, e As EventArgs) Handles btnDeleteNote.Click
        ' 1. 检查是否有选中的行
        If ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("请先选中要删除的笔记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. 获取选中行的索引
        Dim intSelectedIndex As Integer = ListView1.SelectedItems(0).Index

        ' 3. 确认删除操作
        Dim strTitle As String = allNotes(intSelectedIndex)(0)
        If MessageBox.Show("确定要删除笔记 """ & strTitle & """ 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        ' 4. 从内存列表中移除
        allNotes.RemoveAt(intSelectedIndex)

        ' 5. 保存到文件
        SaveNotesToFile(allNotes)

        ' 6. 刷新列表显示
        RefreshListView(allNotes)

        ' 7. 清空代码显示
        txtCodeDetail.Clear()

        MessageBox.Show("笔记已删除！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnEditNote_Click(sender As Object, e As EventArgs) Handles btnEditNote.Click
        ' 1. 检查是否有选中的行
        If ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("请先选中要编辑的笔记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. 获取选中行的索引
        Dim intSelectedIndex As Integer = ListView1.SelectedItems(0).Index

        ' 3. 获取该笔记的当前数据
        Dim arrNote As String() = allNotes(intSelectedIndex)
        Dim strOldTitle As String = arrNote(0)
        Dim strOldRemark As String = arrNote(2)
        Dim strOldCode As String = arrNote(3)

        ' 4. 将当前数据显示到输入框中（供用户修改）
        txtNewTitle.Text = strOldTitle
        txtNewRemark.Text = strOldRemark
        txtNewCode.Text = strOldCode

        ' ★★★ 记录当前正在编辑的笔记索引 ★★★
        intEditingIndex = intSelectedIndex

        ' 5. 提示用户修改后点击"保存笔记"
        MessageBox.Show("请修改右侧输入框中的内容，然后点击'保存笔记'完成更新。", "编辑提示", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' 6. 将焦点定位到标题输入框
        txtNewTitle.Focus()
        txtNewTitle.SelectAll()
    End Sub
End Class