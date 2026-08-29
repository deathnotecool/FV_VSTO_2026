Imports System.Windows.Forms
Imports System.Text
Imports System.IO
Imports System.Drawing   ' ★★★ 新增这一行 ★★★

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
            ' 获取正确的安装目录下的文件路径
            Dim strInstallFilePath As String = "C:\Program Files\FV\CodeNotes.txt"


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
                If arrFields.Length >= 5 Then
                    ' 按照标准顺序：标题(0)|编号(1)|备注(2)|分类(3)|代码(4)
                    Dim arrNew(4) As String
                    arrNew(0) = arrFields(0)   ' 标题
                    arrNew(1) = arrFields(1)   ' 编号
                    arrNew(2) = arrFields(2)   ' 备注
                    arrNew(3) = arrFields(4)   ' 代码正文（索引4，需要还原换行符）
                    arrNew(4) = arrFields(3)   ' 分类（索引3）

                    ' 还原代码正文中的换行符
                    arrNew(3) = arrNew(3).Replace("\n", vbCrLf)

                    lstNotes.Add(arrNew)
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
        Dim strFilePath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FV_VSTO", "CodeNotes.txt")

        ' 2. 确保目录存在
        Dim strDirectory As String = System.IO.Path.GetDirectoryName(strFilePath)
        If Not System.IO.Directory.Exists(strDirectory) Then
            System.IO.Directory.CreateDirectory(strDirectory)
        End If

        ' 3. 准备要写入的行列表
        Dim lines As New List(Of String)()

        For Each arrNote As String() In lstNotes
            ' ★★★ 关键修复：确保每个笔记数组至少有5个字段 ★★★
            Dim arrFixed As String()
            If arrNote.Length >= 5 Then
                arrFixed = arrNote
            Else
                ' 如果不足5个，补足到5个（缺少的字段用空字符串填充）
                arrFixed = New String(4) {}
                Array.Copy(arrNote, arrFixed, arrNote.Length)
                ' 确保第4个（索引3）是代码正文，第5个（索引4）是分类
                If arrNote.Length = 4 Then
                    ' 如果是4字段旧格式：标题|编号|备注|代码，把代码移到索引3，索引4留空
                    arrFixed(0) = arrNote(0)   ' 标题
                    arrFixed(1) = arrNote(1)   ' 编号
                    arrFixed(2) = arrNote(2)   ' 备注
                    arrFixed(3) = arrNote(3)   ' 代码
                    arrFixed(4) = ""           ' 分类（空）
                End If
            End If

            ' 将代码正文中的换行符替换为 \n 用于存储
            Dim strCodeForStorage As String = arrFixed(3).Replace(vbCrLf, "\n")
            ' 组合成一行：标题|编号|备注|分类|代码
            'Dim strLine As String = arrFixed(0) & "|" & arrFixed(1) & "|" & arrFixed(2) & "|" & arrFixed(4) & "|" & strCodeForStorage
            Dim strLine As String = arrFixed(0) & "|" & arrFixed(1) & "|" & arrFixed(2) & "|" & arrFixed(4) & "|" & strCodeForStorage

            lines.Add(strLine)
        Next

        ' 4. 写入文件
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

        '' ★★★ 初始化分类下拉框（cmbCategory）预设选项 ★★★
        'cmbCategory.Items.Clear()
        'cmbCategory.Items.Add("VBA基础")
        'cmbCategory.Items.Add(".NET基础 ")
        'cmbCategory.Items.Add("通用代码块")

        ' 可选：设置默认选中第一项，或留空让用户自己选
        cmbCategory.SelectedIndex = -1   ' 不选中任何项，让用户自行选择或输入
        ' 或者 cmbCategory.SelectedIndex = 0  ' 默认选中 "VBA基础"

        ' ★★★ 2. ★★★ 从资源文件加载所有代码笔记到内存 ★★★
        ' 调用刚才添加的 LoadNotesFromResource 方法
        allNotes = LoadNotesFromFile()
        ' 调试代码已移除，无弹窗

        ' ★★★ 3. 刷新列表显示（显示全部笔记） ★★★
        ' 调用刷新的方法，传入全部笔记（不进行筛选）
        RefreshListView(allNotes)
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)

        ' 刷新分类筛选下拉框
        RefreshCategoryFilter()
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


    '''' <summary>
    '''' 刷新所有分类下拉框：从 allNotes 中提取所有不重复的分类
    '''' 同时填充：筛选下拉框（cmbFilterCategory）和编辑区下拉框（cmbCategory）
    '''' </summary>
    'Private Sub RefreshCategoryFilter()
    '    ' 从文件加载分类列表
    '    Dim lstCategories As List(Of String) = LoadCategoriesFromFile()
    '    lstCategories.Sort()

    '    ' 刷新筛选下拉框
    '    cmbFilterCategory.Items.Clear()
    '    cmbFilterCategory.Items.Add("所有分类")
    '    For Each strCat As String In lstCategories
    '        cmbFilterCategory.Items.Add(strCat)
    '    Next
    '    cmbFilterCategory.SelectedIndex = 0

    '    ' 刷新编辑区下拉框
    '    cmbCategory.Items.Clear()
    '    For Each strCat As String In lstCategories
    '        cmbCategory.Items.Add(strCat)
    '    Next
    '    If cmbCategory.Items.Count > 0 Then
    '        cmbCategory.SelectedIndex = 0
    '    End If
    'End Sub


    ''' <summary>
    ''' 刷新所有分类下拉框：从 Categories.txt 加载分类和颜色
    ''' 同时填充：筛选下拉框（cmbFilterCategory）和编辑区下拉框（cmbCategory）
    ''' </summary>
    Private Sub RefreshCategoryFilter()
        ' 1. 从文件加载分类和颜色映射
        Dim dictCategories As Dictionary(Of String, String) = LoadCategoriesFromFile()

        ' 2. 提取所有分类名称（用于下拉框显示）
        Dim lstCategories As List(Of String) = dictCategories.Keys.ToList()

        ' 3. 排序
        lstCategories.Sort()

        ' 4. 刷新筛选下拉框（cmbFilterCategory）
        cmbFilterCategory.Items.Clear()
        cmbFilterCategory.Items.Add("所有分类")
        For Each strCat As String In lstCategories
            cmbFilterCategory.Items.Add(strCat)
        Next
        cmbFilterCategory.SelectedIndex = 0

        ' 5. 刷新编辑区下拉框（cmbCategory）
        cmbCategory.Items.Clear()
        For Each strCat As String In lstCategories
            cmbCategory.Items.Add(strCat)
        Next

        If cmbCategory.Items.Count > 0 Then
            cmbCategory.SelectedIndex = 0
        End If
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

        ListView1.Columns.Add("标题", 250)
        ListView1.Columns.Add("编号", 80)
        ListView1.Columns.Add("分类", 120)   ' 第3列
        ListView1.Columns.Add("备注", 200)   ' 第4列
        ListView1.Columns.Add("代码正文", 500)

        ' 4. 检查传入的数据是否为空
        If lstData Is Nothing Then
            ' 如果数据为空，直接退出
            Return
        End If

        ' 5. 遍历列表中的每一条笔记，添加到 ListView 中..
        For Each arrNote As String() In lstData
            ' 在 For Each arrNote As String() In lstData 循环内部，添加以下代码（放在最前面）
            'MessageBox.Show("第1字段(标题): " & arrNote(0) & vbCrLf &
            '    "第2字段(编号): " & arrNote(1) & vbCrLf &
            '    "第3字段(备注): " & arrNote(2) & vbCrLf &
            '    "第4字段(分类): " & arrNote(4) & vbCrLf &
            '    "第5字段(代码): " & arrNote(3))

            ' 检查数组是否包含4个元素，并且第一个元素不为空
            If arrNote IsNot Nothing AndAlso arrNote.Length >= 5 AndAlso Not String.IsNullOrEmpty(arrNote(0)) Then                ' 创建一行，第一列显示标题
                Dim itm As New ListViewItem(arrNote(0))   ' 标题

                ' ★★★ 根据分类设置行背景色 ★★★
                Dim strCategory As String = arrNote(4).Trim()
                If String.IsNullOrEmpty(strCategory) Then
                    strCategory = "未分类"
                End If


                ' 从字典中获取颜色名称
                Dim dictCategories As Dictionary(Of String, String) = LoadCategoriesFromFile()
                Dim strColorName As String = "LightGray"
                If dictCategories.ContainsKey(strCategory) Then
                    strColorName = dictCategories(strCategory)
                End If

                ' 将颜色名称转换为 Color 对象
                itm.BackColor = GetColorFromName(strColorName)


                itm.SubItems.Add(arrNote(1))              ' 编号
                itm.SubItems.Add(arrNote(4))              ' 分类（第5个字段，索引4）  ← 移到第3列
                itm.SubItems.Add(arrNote(2))              ' 备注                         ← 移到第4列
                itm.SubItems.Add(arrNote(3))              ' 代码正文                ' 将整行添加到 ListView
                ListView1.Items.Add(itm)
            End If
        Next

        ' 6. 强制禁止自动调整列宽（防止列宽被重置）..
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)

        ' 更新统计信息
        UpdateStatistics()

    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        If e.IsSelected Then
            ' 选中时，显示代码正文
            txtCodeDetail.Text = e.Item.SubItems(4).Text
        Else
            ' 取消选中时，清空代码文本框
            txtCodeDetail.Text = ""
        End If
    End Sub






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
        cmbCategory.Text = arrNote(4)   ' 加载分类（索引4）

        ' ★★★ 记录当前正在编辑的笔记索引 ★★★
        intEditingIndex = intSelectedIndex

        ' 5. 提示用户修改后点击"保存笔记"
        MessageBox.Show("请修改右侧输入框中的内容，然后点击'保存笔记'完成更新。", "编辑提示", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' 6. 将焦点定位到标题输入框
        txtNewTitle.Focus()
        txtNewTitle.SelectAll()
    End Sub

    ''' <summary>
    ''' 保存笔记：将用户输入的新笔记添加到列表并保存到文件
    ''' </summary>
    Private Sub btnAddNote_Click(sender As Object, e As EventArgs) Handles btnAddNote.Click
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

        ' 6. ★★★ 创建5字段笔记数组：标题|编号|备注|分类|代码 ★★★
        '    分类暂时留空（默认值），后续可扩展
        Dim strCategory As String = cmbCategory.Text.Trim()
        Dim arrNewNote As String() = {strTitle, strNewID, strRemark, strCodeForStorage, strCategory}

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
        cmbCategory.Text = ""

        ' 刷新分类筛选下拉框
        RefreshCategoryFilter()

        ' 刷新统计信息
        UpdateStatistics()
    End Sub

    Private Sub cmbFilterCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterCategory.SelectedIndexChanged
        ' 1. 如果当前没有任何笔记，直接退出
        If allNotes Is Nothing OrElse allNotes.Count = 0 Then
            Return
        End If

        ' 2. 获取用户选择的分类文本
        Dim strSelectedCategory As String = cmbFilterCategory.SelectedItem.ToString()

        ' 3. 如果选择的是“所有分类”，则显示全部笔记
        If strSelectedCategory = "所有分类" Then
            RefreshListView(allNotes)
            Return
        End If

        ' 4. 否则，创建一个筛选后的列表
        Dim lstFiltered As New List(Of String())()

        For Each arrNote As String() In allNotes
            ' 判断笔记的分类是否与选中项匹配
            If arrNote.Length >= 5 AndAlso arrNote(4).Trim() = strSelectedCategory Then
                lstFiltered.Add(arrNote)
            End If
        Next

        ' 5. 显示筛选结果
        RefreshListView(lstFiltered)
    End Sub


    ''' <summary>
    ''' 插入代码：将选中笔记的代码正文写入 Excel 当前选中的单元格
    ''' </summary>
    Private Sub btnInsertCode_Click(sender As Object, e As EventArgs) Handles btnInsertCode.Click
        ' 1. 检查是否有选中的行
        If ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("请先选中一条笔记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. 获取当前选中的笔记
        Dim intSelectedIndex As Integer = ListView1.SelectedItems(0).Index
        Dim arrNote As String() = allNotes(intSelectedIndex)

        ' 3. 获取代码正文（索引3）
        Dim strCode As String = arrNote(3)

        ' 4. 检查代码是否为空
        If String.IsNullOrEmpty(strCode) Then
            MessageBox.Show("该笔记没有代码正文！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 5. 获取当前 Excel 的选中区域，并定位到第一个单元格
        Dim rng As Excel.Range = xlapp.Selection
        If rng.Count > 1 Then
            rng = rng.Cells(1, 1)
        End If


        ' ★★★ 确保撤销功能所需的 FV.xlam 已加载 ★★★
        Globals.Ribbons.Ribbon1.确保XLAM已加载()
        ' ★★★ 在修改单元格之前，插入这两行 ★★★
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True

        ' 6. 将代码写入单元格
        rng.NumberFormat = "@"   ' ★★★ 新增：强制设为文本格式
        rng.Value = strCode

        ' 7. 选中这个单元格，让用户直观看到
        rng.Select()

        ' 8. 在 Excel 状态栏提示
        xlapp.StatusBar = "代码已插入到单元格 " & rng.Address

        ' 9. 弹出提示（可选）
        MessageBox.Show("代码已插入到单元格 " & rng.Address & "！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' 添加分类：弹出输入框让用户输入新分类名称，并添加到下拉框
    ''' </summary>
    Private Sub btnAddCategory_Click(sender As Object, e As EventArgs) Handles btnAddCategory.Click
        ' 1. 弹出输入框让用户输入新分类名称
        Dim strNewCategory As String = InputBox("请输入新分类名称：", "添加分类", "")

        ' 2. 如果用户取消或未输入内容，则退出
        If String.IsNullOrEmpty(strNewCategory) Then
            Return
        End If

        ' 3. 去除首尾空格
        strNewCategory = strNewCategory.Trim()

        ' 4. 检查分类是否已存在（不区分大小写）
        Dim blnExists As Boolean = False
        For Each strItem As String In cmbCategory.Items
            If strItem.ToLower() = strNewCategory.ToLower() Then
                blnExists = True
                Exit For
            End If
        Next

        If blnExists Then
            MessageBox.Show("分类 '" & strNewCategory & "' 已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 5. 将新分类添加到 cmbCategory 下拉框中
        cmbCategory.Items.Add(strNewCategory)

        ' 6. 保存分类列表到文件
        Dim lstCategories As New List(Of String)()
        For Each strItem As String In cmbCategory.Items
            lstCategories.Add(strItem)
        Next
        SaveCategoriesToFile(lstCategories)

        ' 7. 刷新分类筛选下拉框（从文件重新加载，保持一致）
        RefreshCategoryFilter()

        ' 8. 自动选中新添加的分类
        cmbCategory.SelectedIndex = cmbCategory.Items.Count - 1

        MessageBox.Show("分类 '" & strNewCategory & "' 添加成功！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' 删除分类：从下拉框中移除选中的分类，并清理笔记中该分类的引用
    ''' </summary>
    Private Sub btnDeleteCategory_Click(sender As Object, e As EventArgs) Handles btnDeleteCategory.Click
        ' 1. 检查是否有选中的分类
        If cmbCategory.SelectedIndex = -1 Then
            MessageBox.Show("请先在分类下拉框中选中要删除的分类！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. 获取选中的分类名称
        Dim strCategoryToDelete As String = cmbCategory.SelectedItem.ToString()

        ' 3. 如果分类为空字符串，提示不能删除
        If String.IsNullOrEmpty(strCategoryToDelete) Then
            MessageBox.Show("不能删除空分类！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 4. 检查是否有笔记正在使用该分类（区分大小写，保留原始输入）
        Dim blnInUse As Boolean = False
        If allNotes IsNot Nothing AndAlso allNotes.Count > 0 Then
            For Each arrNote As String() In allNotes
                If arrNote.Length >= 5 AndAlso arrNote(4) = strCategoryToDelete Then
                    blnInUse = True
                    Exit For
                End If
            Next
        End If

        ' 5. 如果分类正在使用，提示用户并询问是否强制删除（将笔记分类改为空）
        If blnInUse Then
            Dim dialogResult As DialogResult = MessageBox.Show(
                "分类 '" & strCategoryToDelete & "' 正在被笔记使用。" & vbCrLf &
                "如果删除，相关笔记的分类将被清空。" & vbCrLf &
                "确定要继续吗？",
                "分类正在使用",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )

            If dialogResult = DialogResult.No Then
                Return
            End If

            ' 强制删除：将所有笔记中该分类字段清空
            For i As Integer = 0 To allNotes.Count - 1
                If allNotes(i).Length >= 5 AndAlso allNotes(i)(4) = strCategoryToDelete Then
                    allNotes(i)(4) = ""
                End If
            Next

            ' 保存到文件
            SaveNotesToFile(allNotes)
            ' 刷新列表显示
            RefreshListView(allNotes)
        End If



        ' 6. 从 cmbCategory 下拉框中移除该分类
        cmbCategory.Items.Remove(strCategoryToDelete)

        ' 7. 保存分类列表到文件
        Dim lstCategories As New List(Of String)()
        For Each strItem As String In cmbCategory.Items
            lstCategories.Add(strItem)
        Next
        SaveCategoriesToFile(lstCategories)

        ' 8. 刷新分类筛选下拉框
        RefreshCategoryFilter()

        MessageBox.Show("分类 '" & strCategoryToDelete & "' 已删除！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' 获取分类文件的路径（与 CodeNotes.txt 在同一目录）
    ''' </summary>
    Private Function GetCategoryFilePath() As String
        Dim strUserFolder As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FV_VSTO")
        Return System.IO.Path.Combine(strUserFolder, "Categories.txt")
    End Function

    '''' <summary>
    '''' 从 Categories.txt 文件加载所有分类
    '''' </summary>
    'Private Function LoadCategoriesFromFile() As List(Of String)
    '    Dim lstCategories As New List(Of String)()
    '    Dim strFilePath As String = GetCategoryFilePath()

    '    If System.IO.File.Exists(strFilePath) Then
    '        Dim lines As String() = System.IO.File.ReadAllLines(strFilePath, System.Text.Encoding.UTF8)
    '        For Each line As String In lines
    '            Dim strTrimmed As String = line.Trim()
    '            If Not String.IsNullOrEmpty(strTrimmed) Then
    '                lstCategories.Add(strTrimmed)
    '            End If
    '        Next
    '    End If

    '    ' 如果文件不存在或为空，返回一个包含默认分类的列表
    '    If lstCategories.Count = 0 Then
    '        lstCategories.AddRange({"VBA基础", ".NET基础", "通用代码块"})
    '    End If

    '    Return lstCategories
    'End Function

    ''' <summary>
    ''' 从 Categories.txt 文件加载所有分类及其颜色
    ''' </summary>
    ''' <returns>分类名称 → 颜色名称 的字典</returns>
    Private Function LoadCategoriesFromFile() As Dictionary(Of String, String)
        Dim dictCategories As New Dictionary(Of String, String)()
        Dim strFilePath As String = GetCategoryFilePath()

        If System.IO.File.Exists(strFilePath) Then
            Dim lines As String() = System.IO.File.ReadAllLines(strFilePath, System.Text.Encoding.UTF8)
            For Each line As String In lines
                Dim strTrimmed As String = line.Trim()
                If Not String.IsNullOrEmpty(strTrimmed) Then
                    ' 按 | 拆分，格式：分类名称|颜色代码
                    Dim arrParts As String() = strTrimmed.Split(New Char() {"|"c}, StringSplitOptions.None)
                    Dim strCategory As String = arrParts(0).Trim()
                    Dim strColor As String = "LightGray"   ' 默认颜色

                    If arrParts.Length >= 2 Then
                        strColor = arrParts(1).Trim()
                    End If

                    If Not dictCategories.ContainsKey(strCategory) Then
                        dictCategories.Add(strCategory, strColor)
                    End If
                End If
            Next
        End If

        ' 如果文件不存在或为空，返回默认分类和颜色
        If dictCategories.Count = 0 Then
            dictCategories.Add("VBA基础", "LightBlue")
            dictCategories.Add(".NET基础", "LightGreen")
            dictCategories.Add("通用代码块", "LightYellow")
            dictCategories.Add("未分类", "White")
            dictCategories.Add("其他", "LightGray")
        End If

        Return dictCategories
    End Function


    ''' <summary>
    ''' 将颜色名称字符串转换为 System.Drawing.Color 对象
    ''' </summary>
    Private Function GetColorFromName(ByVal strColorName As String) As Color
        Try
            Return Color.FromName(strColorName)
        Catch ex As Exception
            Return Color.LightGray
        End Try
    End Function


    ''' <summary>
    ''' 将分类列表保存到 Categories.txt 文件
    ''' </summary>
    Private Sub SaveCategoriesToFile(ByVal lstCategories As List(Of String))
        Dim strFilePath As String = GetCategoryFilePath()
        ' 确保目录存在
        Dim strDirectory As String = System.IO.Path.GetDirectoryName(strFilePath)
        If Not System.IO.Directory.Exists(strDirectory) Then
            System.IO.Directory.CreateDirectory(strDirectory)
        End If

        ' 写入所有分类（每行一个）
        System.IO.File.WriteAllLines(strFilePath, lstCategories, System.Text.Encoding.UTF8)
    End Sub


    ''' <summary>
    ''' 导出笔记：将当前所有笔记导出为一个独立的文本文件
    ''' </summary>
    Private Sub btnExportNotes_Click(sender As Object, e As EventArgs) Handles btnExportNotes.Click
        ' 1. 检查是否有笔记可导出
        If allNotes Is Nothing OrElse allNotes.Count = 0 Then
            MessageBox.Show("没有可导出的笔记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. 弹出保存文件对话框，让用户选择保存位置
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*"
        sfd.FileName = "FV_CodeNotes_Backup_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".txt"
        sfd.Title = "导出代码笔记"

        If sfd.ShowDialog() = DialogResult.Cancel Then
            Return
        End If

        ' 3. 准备要导出的数据
        Dim lines As New List(Of String)()

        ' 先写入一个文件头，方便识别
        lines.Add("# FV 代码笔记导出文件")
        lines.Add("# 导出时间：" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        lines.Add("# 格式：标题|编号|备注|分类|代码正文")
        lines.Add("# ============================================")

        For Each arrNote As String() In allNotes
            If arrNote.Length >= 5 Then
                ' 将代码正文中的换行符替换为 \n 以便存储
                Dim strCodeForExport As String = arrNote(3).Replace(vbCrLf, "\n")
                Dim strLine As String = arrNote(0) & "|" & arrNote(1) & "|" & arrNote(2) & "|" & arrNote(4) & "|" & strCodeForExport
                lines.Add(strLine)
            End If
        Next

        ' 4. 写入文件
        Try
            System.IO.File.WriteAllLines(sfd.FileName, lines, System.Text.Encoding.UTF8)
            MessageBox.Show("成功导出 " & (lines.Count - 4) & " 条笔记到：" & vbCrLf & sfd.FileName, "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("导出失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' 导入笔记：从之前导出的文件中恢复笔记（支持灵活解析字段顺序）
    ''' </summary>
    Private Sub btnImportNotes_Click(sender As Object, e As EventArgs) Handles btnImportNotes.Click
        ' ★★★ 变量声明区 ★★★
        Dim ofd As New OpenFileDialog()
        Dim dialogResult As DialogResult
        Dim lines As String()
        Dim intImportCount As Integer = 0
        Dim lstNewNotes As New List(Of String())()
        Dim intMaxID As Integer = 0
        Dim dictFieldOrder As New Dictionary(Of Integer, Integer)()   ' 导入文件列索引 → 标准索引(0-4) 的映射
        Dim blnHasHeader As Boolean = False

        ' 1. 弹出文件选择对话框
        ofd.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*"
        ofd.Title = "导入代码笔记"

        If ofd.ShowDialog() = DialogResult.Cancel Then
            Return
        End If

        ' 2. 读取文件
        lines = System.IO.File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8)

        ' 3. 解析头部，确定字段顺序
        For Each line As String In lines
            Dim strTrimmed As String = line.Trim()
            If strTrimmed.StartsWith("# 格式：") Then
                ' 提取格式声明，例如：标题|编号|备注|分类|代码正文
                Dim strFormat As String = strTrimmed.Replace("# 格式：", "").Trim()
                Dim arrFormatFields As String() = strFormat.Split(New Char() {"|"c}, StringSplitOptions.None)

                ' 标准字段顺序（索引0-4）与内部存储一致
                Dim arrStandardFields As String() = {"标题", "编号", "备注", "分类", "代码正文"}

                ' 建立映射：导入文件的列索引 → 标准索引
                For i As Integer = 0 To arrFormatFields.Length - 1
                    For j As Integer = 0 To arrStandardFields.Length - 1
                        If arrFormatFields(i) = arrStandardFields(j) Then
                            dictFieldOrder(i) = j
                            Exit For
                        End If
                    Next
                Next

                blnHasHeader = True
                Exit For
            End If
        Next

        ' 如果找不到格式声明，尝试按默认顺序（标题|编号|备注|分类|代码正文）解析
        If Not blnHasHeader Then
            ' 默认映射：i → i（即第1列是标题，第2列是编号...）
            For i As Integer = 0 To 4
                dictFieldOrder(i) = i
            Next
        End If

        ' 4. 解析数据（跳过以 # 开头的注释行和空行）
        For Each line As String In lines
            Dim strTrimmed As String = line.Trim()
            If String.IsNullOrEmpty(strTrimmed) OrElse strTrimmed.StartsWith("#") Then
                Continue For
            End If

            ' 按 | 拆分
            Dim arrFields As String() = line.Split(New Char() {"|"c}, StringSplitOptions.None)

            ' 检查字段数量是否足够
            If arrFields.Length < dictFieldOrder.Count Then
                Continue For   ' 跳过字段数量不足的行
            End If

            ' ★★★ 根据映射填充标准数组 ★★★
            Dim arrStandard(4) As String
            For Each kvp As KeyValuePair(Of Integer, Integer) In dictFieldOrder
                Dim intImportIndex As Integer = kvp.Key
                Dim intStandardIndex As Integer = kvp.Value
                If intImportIndex < arrFields.Length AndAlso intStandardIndex < arrStandard.Length Then
                    arrStandard(intStandardIndex) = arrFields(intImportIndex).Trim()

                    '' ★★★ 插入位置：输出映射关系（调试用） ★★★
                    'MessageBox.Show("导入列" & intImportIndex & " → 标准索引" & intStandardIndex & vbCrLf &
                    '                "值：" & arrFields(intImportIndex).Trim())
                End If
            Next

            ' ★★★ 交换索引3（代码正文）和索引4（分类）的内容 ★★★
            Dim strTemp As String = arrStandard(3)
            arrStandard(3) = arrStandard(4)
            arrStandard(4) = strTemp


            ' 验证：标题和编号不能为空
            If String.IsNullOrEmpty(arrStandard(0)) OrElse String.IsNullOrEmpty(arrStandard(1)) Then
                Continue For   ' 跳过无效行
            End If

            ' ★★★ 关键：将代码正文中的 \n 还原为换行符（代码正文在标准索引3） ★★★
            If Not String.IsNullOrEmpty(arrStandard(3)) Then
                arrStandard(3) = arrStandard(3).Replace("\n", vbCrLf)
            End If

            ' 添加到导入列表
            lstNewNotes.Add(arrStandard)
            intImportCount += 1

            '' ★★★ 调试：显示刚刚添加的 arrStandard 内容 ★★★
            'MessageBox.Show("arrStandard(3) = " & arrStandard(3) & vbCrLf &
            '    "arrStandard(4) = " & arrStandard(4))

        Next

        ' 5. 检查是否有有效数据
        If intImportCount = 0 Then
            MessageBox.Show("未找到有效数据，请确认文件格式正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 6. 确认是否追加
        dialogResult = MessageBox.Show(
            "找到 " & intImportCount & " 条笔记。" & vbCrLf &
            "是否追加到当前笔记列表？",
            "确认导入",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If dialogResult = DialogResult.No Then
            Return
        End If

        ' 7. 追加到 allNotes（处理ID冲突，重新编号）
        If allNotes IsNot Nothing AndAlso allNotes.Count > 0 Then
            For Each arrNote As String() In allNotes
                If arrNote.Length >= 5 Then
                    Dim strID As String = arrNote(1).Replace("GN", "").Trim()
                    Dim intID As Integer = 0
                    If Integer.TryParse(strID, intID) Then
                        If intID > intMaxID Then intMaxID = intID
                    End If
                End If
            Next
        End If

        For Each arrNote As String() In lstNewNotes
            ' 生成新ID
            intMaxID += 1
            Dim strNewID As String = "GN" & intMaxID.ToString("D3")
            ' 更新笔记数组中的ID
            arrNote(1) = strNewID
            allNotes.Add(arrNote)
        Next

        ' 8. 保存到文件
        SaveNotesToFile(allNotes)

        ' 9. 刷新列表显示
        RefreshListView(allNotes)

        ' 10. 刷新分类筛选下拉框
        RefreshCategoryFilter()

        MessageBox.Show("成功导入 " & intImportCount & " 条笔记！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' 更新统计信息：显示总笔记数和各分类数量
    ''' </summary>
    Private Sub UpdateStatistics()
        ' 1. 如果没有任何笔记，显示提示
        If allNotes Is Nothing OrElse allNotes.Count = 0 Then
            lblStatistics.Text = "暂无笔记"
            Return
        End If

        ' 2. 统计总数
        Dim intTotal As Integer = allNotes.Count

        ' 3. 统计各分类数量
        Dim dictCategoryCount As New Dictionary(Of String, Integer)()
        For Each arrNote As String() In allNotes
            If arrNote.Length >= 5 Then
                Dim strCategory As String = arrNote(4).Trim()
                If String.IsNullOrEmpty(strCategory) Then
                    strCategory = "未分类"
                End If
                If dictCategoryCount.ContainsKey(strCategory) Then
                    dictCategoryCount(strCategory) += 1
                Else
                    dictCategoryCount(strCategory) = 1
                End If
            End If
        Next

        ' 4. 拼接显示文本
        Dim strDisplay As String = "共 " & intTotal & " 条笔记"
        For Each kvp As KeyValuePair(Of String, Integer) In dictCategoryCount
            strDisplay &= " | " & kvp.Key & ": " & kvp.Value & "条"
        Next

        lblStatistics.Text = strDisplay
    End Sub






    Private Sub btnExit_Click_1(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class