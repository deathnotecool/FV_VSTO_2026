Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Imports System.Diagnostics

' ============================================================
' F01_不良品基本信息 - 模块开发备忘
' ============================================================
' 【Load 重写检查清单】重写 Load 时务必核对以下下拉框：
'   排序字段、客户、供应商、类型区分、产品规格、发现过程、不良类型、因素确定
' 【历史踩坑】
'   1. 曾漏掉"不良类型"，导致下拉框为空（数据源：不良类型分类 表）。
'   2. Grid 必须绑定 objDataView 而非 objDataSet，否则 RowFilter 不生效。
'   3. objDataAdapter1th 的连接在 产品规格_SelectedIndexChanged 中初始化，
'      依赖 Load 中提前调用 产品规格_SelectedIndexChanged(Nothing, Nothing)。
' ============================================================

Public Class F01_不良品基本信息
    ' 【记录用户排序选择】用于更新/删除后恢复用户排序（-1 表示未设置）
    Private intLastSortIndex As Integer = -1


    'OleDbConnection/objConnection1th	电话线（连接通道）
    'OleDbCommand	你说的话（Sql 语句）
    'OleDbDataAdapter	接线员（帮你把话传过去、把答复拿回来）,专门问"不良品信息"的接线员
    'objDataAdapter1th	临时问其他表的接线员
    'DataSet/ objDataSet1th 	你的笔记本（本地缓存）,两本笔记本，装不同数据
    'DataView	笔记本上的"筛选/排序视图"
    'objCurrencyManager	翻页器（管当前显示第几条）


    ' ============================================================
    ' ★★★ OleDb 与 Provider 的关系说明 ★★★
    ' ============================================================
    ' 【OleDb】    ：通用数据访问规范（标准/规则），定义了如何连接和操作数据库的统一方法。
    ' 【Provider】  ：遵循 OleDb 规范的具体驱动程序（翻译官），负责将代码命令翻译成特定数据库能理解的语言。
    ' 【连接字符串】：Provider=Microsoft.Ace.OleDb.12.0  指定使用 Access 数据库的翻译官。
    '
    ' 流程：VB.NET 代码 → OleDb 规范 → Provider(Ace) → Access 数据库
    ' ============================================================

    ' ============================================================
    ' ★★★ 数据库连接配置（不良品信息管理） ★★★
    ' ============================================================

    ' 【用途】定义Access数据库文件的路径（支持多环境切换）
    ' 说明：根据当前使用场景，取消注释对应的路径，并注释掉其他路径。
    ' 【共享盘路径】（推荐，多人协作使用）
    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\不良品信息管理.accdb"
    ' 【个人电脑路径1】（其他电脑时使用）
    ' Dim strMyCompanyComputerPath As String = "D:\6 总务\access\不良品信息管理.accdb"

    ' 【数据库连接对象】
    ' 功能：通过 OleDb 规范建立与 Access 数据库的连接。
    '       具体的数据翻译工作由 Provider 完成。
    ' 参数说明：
    '   Provider：指定数据提供程序，Access 使用 Microsoft.Ace.OleDb.12.0
    '   Data Source：数据库文件的完整路径（此处引用上面定义的共享盘路径）
    Dim objConnection1th As New OleDbConnection _
           ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)


    ' ============================================================
    ' ★★★ 数据适配器与数据集 ★★★
    ' ============================================================

    ' 【数据适配器1（主查询）】
    ' 功能：负责从数据库检索数据，并填充到 DataSet 中。
    ' 参数1：SQL 查询语句（从 不良品信息 表查询所有字段，按 发生日期 排序）
    ' 参数2：数据库连接对象（objConnection1th）
    ' 说明：此适配器在窗体加载时执行，用于获取全部记录。
    ' 【性能优化】SQL 直接降序，避免 DataView.Sort 事后重排（省 ~1000ms）
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 不良品信息.* FROM 不良品信息 ORDER BY 发生日期 DESC", objConnection1th)


    ' 【数据适配器2（辅助查询）】
    ' 功能：用于执行其他临时查询（如填充下拉框数据源）。
    ' 说明：其 SelectCommand 属性在代码中动态设置，不固定 SQL。
    Dim objDataAdapter1th As New OleDbDataAdapter()

    ' 【数据集1（主数据容器）】
    ' 功能：在内存中存储从数据库检索到的数据（离线数据缓存）。
    ' 说明：数据适配器 Fill 方法将数据填充到此对象中，供 DataView 和控件绑定使用。
    Dim objDataSet As New DataSet()

    ' 【数据集2（辅助数据容器）】
    ' 功能：用于存储辅助查询结果（如下拉框的选项数据）。
    Dim objDataSet1th As New DataSet()

    ' 【数据视图】
    ' 功能：为 DataSet 中的数据提供“动态视图”，支持排序、筛选和搜索。
    ' 说明：数据绑定到控件时，绑定的是 DataView，而不是 DataSet 本身。
    '       DataView 可以独立于 DataSet 进行排序和筛选，而不影响原始数据。
    Dim objDataView As DataView
    Dim objDataView1th As DataView

    ' 【货币管理器（CurrencyManager）】
    ' 功能：管理绑定到同一数据源的所有控件的“当前记录位置”。
    ' 说明：当你在窗体中点击“下一条”按钮时，CurrencyManager 负责同步更新
    '       所有绑定控件的显示内容（文本框、复选框等）。
    '       它通过 BindingContext 获取，确保多个控件显示同一条记录。
    Dim objCurrencyManager As CurrencyManager

    ' 【字段映射数组】所有数据库字段名，按顺序排列
    ' 说明：声明时即赋值，避免依赖 BindFields 被调用（历史踩坑）。
    Dim myArray() As String = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备",
        "发现过程", "不良类型", "操作者", "类型区分", "不良数量", "完成工序",
        "加工费用", "材料费用", "损失成本", "不良现象及原因", "备注", "重量",
        "处置完成", "因素确定", "图片路径"}


    ' 【筛选状态标志】记录当前是否处于"只看未完成记录"筛选状态
    ' 说明：True = 当前只显示未完成记录（按钮标签为"显示全部"）；
    '       False = 当前显示全部记录（按钮标签为"未处理记录请点该按钮查看"）。
    Private blnFilteringUnfinished As Boolean = False



    ''' <summary>
    ''' 功能：核心数据加载方法：从 Access 数据库填充 DataSet，并初始化 DataView 和 CurrencyManager。
    ''' </summary>
    ''' <remarks>
    ''' 【调用时机】窗体 Load 事件、刷新按钮、筛选/排序后需重新加载数据时。
    ''' 【数据流】objDataAdapter(Fill) → objDataSet(表"bl") → objDataView → objCurrencyManager
    ''' 【历史优化点】
    '''   1. 填充前清空 DataSet，避免重复累积（历史问题：TableAdapter.ClearBeforeFill 缺失导致数据重复）
    '''   2. 增加 Try...Catch 异常处理，避免网络路径超时导致程序崩溃（历史问题：\\192.168.3.250 访问超时）
    '''   3. 异常时给出用户友好提示，并记录调试日志便于排查
    ''' </remarks>
    Private Sub FillDataSetAndView()
        ' ============================================================
        ' ★★★ 第1步：异常处理外层 ★★★
        ' ============================================================
        ' 原因：数据库连接（网络路径）可能因网络波动、权限问题失败，
        '       必须捕获异常避免整个窗体加载失败。
        Try
            ' 保存用户当前的排序选择（供更新/删除后恢复）
            ' 说明：若为首次加载（objDataView 为 Nothing），intLastSortIndex 记为 -1，
            '       后续不会触发恢复逻辑。
            If objDataView IsNot Nothing AndAlso 排序字段.SelectedIndex >= 0 Then
                intLastSortIndex = 排序字段.SelectedIndex
            End If
            ' ============================================================
            ' ★★★ 第2步：重新初始化 DataSet（确保干净状态） ★★★
            ' ============================================================
            ' 历史踩坑：如果 objDataSet 已有旧数据，再次 Fill 会导致数据累积。
            ' 解决方案：每次填充前重新 New，相当于清空所有表。
            objDataSet = New DataSet()

            ' ============================================================
            ' ★★★ 第3步：执行数据填充 ★★★
            ' ============================================================
            ' 说明：objDataAdapter 的 SelectCommand 已在窗体级定义为：
            '       "SELECT 不良品信息.* FROM 不良品信息 ORDER BY 发生日期"
            ' Fill 方法会自动打开连接（如果未打开），填充完成后保持原状态。
            ' 第二参数 "bl" 是 DataSet 中表的名称，后续通过它引用数据。
            objDataAdapter.Fill(objDataSet, "bl")

            ' ============================================================
            ' ★★★ 第4步：初始化 DataView 和 CurrencyManager ★★★
            ' ============================================================
            ' 【历史踩坑】原代码每次 New DataView，导致 Grid.DataSource 仍指向旧对象，
            '             出现"数据刷新了但 Grid 看不到变化"的问题。
            '             现改为：首次创建，后续复用（只更新 Table），
            '             所有调用点（添加/更新/删除）自动受益。
            ' 【注意】Sort 必须在 Table 更新后重新应用，否则排序会丢失。
            If objDataView Is Nothing Then
                ' 首次调用：创建 DataView 和 CurrencyManager
                objDataView = New DataView(objDataSet.Tables("bl"))
                objCurrencyManager = CType(Me.BindingContext(objDataView), CurrencyManager)
            Else
                ' 后续调用：复用 DataView，只替换 Table
                objDataView.Table = objDataSet.Tables("bl")
            End If

            ' ---- 应用默认排序（发生日期降序） ----
            ' 说明：Table 变更后 Sort 会失效，需重新设置。
            objDataView.Sort = "发生日期 DESC"

            ' ============================================================
            ' ★★★ 第5步：状态栏提示（如果存在） ★★★
            ' ============================================================
            ' 说明：ToolStripLabel1 在窗体设计器中已存在，用于显示"就绪"状态。
            '       此处仅作友好提示，不强制要求。
            If Not IsNothing(ToolStripLabel1) Then
                ToolStripLabel1.Text = "就绪"
            End If

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：记录日志并提示用户 ★★★
            ' ============================================================
            ' 常见失败原因：
            '   1. 网络路径 \\192.168.3.250 不可达（检查网络或VPN）
            '   2. Access 数据库被独占打开（关闭其他连接）
            '   3. Provider 驱动未安装（检查是否安装 Access 2010 引擎）
            MessageBox.Show(
            String.Format("加载不良品数据失败：{0}", ex.Message),
            "FV VSTO - 数据库错误",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

            ' 同时将异常写入调试输出（便于开发时在输出窗口查看）
            Debug.WriteLine(String.Format("FillDataSetAndView 异常: {0}", ex.ToString()))

            ' 状态栏提示错误信息（如果存在）
            If Not IsNothing(ToolStripLabel1) Then
                ToolStripLabel1.Text = "加载失败，请检查数据库连接"
            End If
        End Try
    End Sub


    ''' <summary>
    ''' 功能：将 DataView 数据源中的字段逐一绑定到 GroupBox1 内的窗体控件上，
    '''       使控件显示内容与当前记录保持同步；同时清除旧绑定以防止多次调用时叠加。
    '''       涉及对象：GroupBox1 内所有与数据库字段同名的控件（文本框、组合框、复选框）。
    '''       绑定方向：单向（数据源 → 控件），控件编辑后需手动调用更新逻辑写回数据库。
    ''' </summary>
    ''' <remarks>
    ''' 【绑定机制】通过 Control.DataBindings.Add 建立控件属性（如 Text/Checked）与数据字段的映射。
    ''' 【调用时机】在 FillDataSetAndView 成功加载数据后调用（通常紧随其后）。
    ''' 【历史优化点】
    '''   1. 绑定前先清除所有控件的旧绑定，避免残留（防止字段错位或显示异常）
    '''   2. 针对 CheckBox 单独处理 Checked 属性，而非 Text
    '''   3. 对日期字段进行格式化（统一为 yyyy/MM/dd），避免显示时分秒
    ''' </remarks>
    Private Sub BindFields()
        ' ============================================================
        ' ★★★ 第1步：定义字段映射数组 ★★★
        ' ============================================================
        ' 说明：此数组顺序必须与 DataSet 中表的列顺序一致（或字段名完全匹配）。
        '       共 21 个字段，对应数据库表"不良品信息"的所有列。
        '       历史踩坑：若字段名与控件名不一致，绑定会失败（此处已确保完全一致）。

        'myArray = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分", "不良数量",
        '"完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因", "备注", "重量", "处置完成", "因素确定", "图片路径"}

        ' ============================================================
        ' ★★★ 第2步：清除所有控件的旧绑定（防止累积） ★★★
        ' ============================================================
        ' 说明：遍历 GroupBox1 中的所有控件，清除其 DataBindings 集合。
        '       如果不清除，在多次调用 BindFields 时（如刷新数据），
        '       旧绑定会与新绑定叠加，导致显示混乱或报错。
        For i As Byte = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString()).DataBindings.Clear()
        Next i

        ' ============================================================
        ' ★★★ 第3步：重新绑定数据字段到控件 ★★★
        ' ============================================================
        ' 说明：DataBindings.Add(属性名, 数据源, 字段名)
        '       对于普通控件（文本框等），绑定 Text 属性。
        '       对于 CheckBox，绑定 Checked 属性（布尔值）。
        For i As Byte = 0 To UBound(myArray)
            Dim strControlName As String = myArray(i).ToString()
            Dim ctrl As Control = GroupBox1.Controls(strControlName)

            ' 判断是否为 CheckBox（处置完成）
            If TypeOf ctrl Is CheckBox Then
                ' 绑定 Checked 属性，而非 Text
                ctrl.DataBindings.Add("Checked", objDataView, strControlName)
            Else
                ' 绑定 Text 属性
                ctrl.DataBindings.Add("Text", objDataView, strControlName)
            End If

            ' ============================================================
            ' ★★★ 特殊处理：日期字段格式化为短日期 ★★★
            ' ============================================================
            ' 原因：数据库中的日期可能包含时间部分（如 2025-01-01 00:00:00），
            '       直接显示会不美观。统一格式为 yyyy/MM/dd。
            ' 历史踩坑：如果字段值为 DBNull，直接转换会报错，故使用 Try 捕获。
            If strControlName = "发生日期" Then
                Try
                    Dim dt As Date = CType(ctrl.Text, Date)
                    ctrl.Text = Format(dt, "yyyy/MM/dd")
                Catch ex As Exception
                    ' 若转换失败（如空值），则保持原样
                    ' 此处不处理异常，避免打断绑定流程
                End Try
            End If
        Next i

        ' ============================================================
        ' ★★★ 第4步：更新状态栏提示 ★★★
        ' ============================================================
        ToolStripLabel1.Text = "Ready"  ' 显示就绪状态
    End Sub

    ''' <summary>
    ''' 功能：格式化"发生日期"文本框内容为短日期（yyyy/MM/dd），
    '''       并在 txtRecordPosition 文本框中显示当前记录位置（如 "3 of 120"）。
    '''       涉及对象：GroupBox1 内的"发生日期"控件、txtRecordPosition 控件。
    '''       触发场景：任何记录导航（首条/上一条/下一条/末条）后调用。
    ''' </summary>
    ''' <remarks>
    ''' 【性能优化】
    '''   原代码用 Try...Catch 处理空值/格式错误，首次调用必然抛异常，
    '''   耗时约 120ms（异常构造开销大）。
    '''   现改用 Date.TryParse：不抛异常，返回 Boolean，避免走异常路径。
    ''' 【机制说明】
    '''   - 记录总数来自 objCurrencyManager.Count（当前 DataView 的行数）。
    '''   - 当前索引来自 objCurrencyManager.Position（从 0 开始，显示时 +1）。
    ''' </remarks>
    Private Sub ShowPosition()
        ' ============================================================
        ' ★★★ 第1步：格式化"发生日期"为短日期（TryParse，不抛异常） ★★★
        ' ============================================================
        ' 原因：DataView 绑定到文本框后，日期可能带时间部分（如 2025/01/01 0:00:00），
        '       统一格式为 yyyy/MM/dd 以便阅读。
        ' 优化：用 Date.TryParse 替代 CType + Try...Catch，避免异常开销。
        Dim dtParsed As Date
        Dim strDateText As String = GroupBox1.Controls("发生日期").Text

        If Date.TryParse(strDateText, dtParsed) Then
            ' 解析成功：按短日期格式回写
            发生日期.Text = Format(dtParsed, "yyyy/MM/dd")
        Else
            ' 解析失败（如空值、格式错误）：不抛异常，静默跳过
            ' 说明：原代码在此处填入系统日期，会误导用户；
            '       改为保留空值，让用户在编辑时自行填写。
            发生日期.Text = ""
        End If

        ' ============================================================
        ' ★★★ 第2步：显示当前记录位置 ★★★
        ' ============================================================
        ' 说明：Position 从 0 开始，显示时 +1 更符合用户习惯（如 "1 of 120"）。
        '       Count 为当前 DataView 的记录总数（受筛选影响）。
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
        " of " & objCurrencyManager.Count()
    End Sub



    'Private Sub ShowPosition()
    '    ' ============================================================
    '    ' ★★★ 第1步：格式化"发生日期"为短日期 ★★★
    '    ' ============================================================
    '    ' 原因：DataView 绑定到文本框后，日期可能带时间部分（如 2025/01/01 0:00:00），
    '    '       统一格式为 yyyy/MM/dd 以便阅读。
    '    ' 历史踩坑：空记录或 DBNull 会导致 CType 失败，故用 Try 包裹。
    '    Try
    '        ' 尝试将文本框内容转换为 Date 类型，再按短日期格式回写
    '        发生日期.Text = Format(CType(GroupBox1.Controls("发生日期").Text, Date), "yyyy/MM/dd")
    '    Catch e As System.Exception
    '        ' 转换失败（如空值）时：先用当前系统日期兜底，避免文本框为空导致后续逻辑出错
    '        GroupBox1.Controls("发生日期").Text = CType(Now, String)
    '        ' 再次格式化（此时必定成功）
    '        发生日期.Text = Format(CType(GroupBox1.Controls("发生日期").Text, Date), "yyyy/MM/dd")
    '    End Try

    '    ' ============================================================
    '    ' ★★★ 第2步：显示当前记录位置 ★★★
    '    ' ============================================================
    '    ' 说明：Position 从 0 开始，显示时 +1 更符合用户习惯（如 "1 of 120"）。
    '    '       Count 为当前 DataView 的记录总数（受筛选影响）。
    '    txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    '    " of " & objCurrencyManager.Count()
    'End Sub


    ''' <summary>
    ''' 功能：将当前记录位置移动到数据集的第一条记录（索引 0）。
    '''       同时同步 DataGridView 的选中行为第一条记录的第一列，
    '''       并刷新"当前记录位置"标签显示。
    '''       涉及对象：objCurrencyManager、grdAuthorTitles、txtRecordPosition。
    ''' </summary>
    ''' <remarks>
    ''' 【关键机制】
    '''   - 所有绑定到同一 DataView 的控件，其显示内容由 CurrencyManager.Position 统一控制，
    '''     因此改变 Position 会自动刷新所有控件，无需逐个赋值。
    '''   - DataGridView 的 CurrentCell 需要通过 RemoveHandler/AddHandler 临时解绑
    '''     SelectionChanged 事件，避免程序化移动指针时触发用户的选中逻辑（历史踩坑：会造成死循环或额外查询）。
    ''' 【历史优化点】
    '''   - 若"查询条件"文本框非空，则将 DataGridView 指针重置为筛选结果的第一行，
    '''     避免 Position 与视图显示的行不一致。
    ''' </remarks>
    Private Sub btnMoveFirst_Click(Sender As Object, E As EventArgs) Handles btnMoveFirst.Click
        Dim intPosition As Integer
        objCurrencyManager.Position = 0             ' 定位到第一条记录（索引从 0 开始）
        intPosition = objCurrencyManager.Position   ' 记录位置赋值给变量，供 DataGridView 同步使用

        ' 临时解绑 SelectionChanged 事件：防止程序化改变 CurrentCell 时触发用户逻辑
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  ' 同步 DataGridView 指针
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged  ' 恢复绑定

        ShowPosition()  ' 刷新"当前记录位置"标签

        ' 若处于筛选状态，指针需回到筛选结果的第一行（否则 Position 与视图不一致）
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0)
    End Sub


    ''' <summary>
    ''' 功能：将当前记录位置向前移动一条（Position - 1）。
    '''       同步 DataGridView 指针与记录位置标签，到达首条时 Position 会被 CurrencyManager 自动钳制。
    '''       涉及对象：objCurrencyManager、grdAuthorTitles、txtRecordPosition。
    ''' </summary>
    ''' <remarks>
    ''' 【注意】Position -= 1 在第一条时会保持 0（CurrencyManager 内部已做边界保护）。
    ''' 【历史踩坑】同 btnMoveFirst_Click，需 RemoveHandler/AddHandler 避免事件冲突。
    ''' </remarks>
    Private Sub btnMovePrevious_Click(Sender As Object, E As EventArgs) Handles btnMovePrevious.Click
        Dim intPosition As Integer
        objCurrencyManager.Position -= 1            ' 上一条记录（边界由 CurrencyManager 自动处理）
        intPosition = objCurrencyManager.Position
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
        ShowPosition()
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0)
    End Sub


    ''' <summary>
    ''' 功能：将当前记录位置向后移动一条（Position + 1）。
    '''       同步 DataGridView 指针与记录位置标签，到达末条时 Position 会被 CurrencyManager 自动钳制。
    '''       涉及对象：objCurrencyManager、grdAuthorTitles、txtRecordPosition。
    ''' </summary>
    ''' <remarks>
    ''' 【注意】Position += 1 在最后一条时会保持 Count-1（CurrencyManager 内部已做边界保护）。
    ''' 【历史踩坑】同 btnMoveFirst_Click，需 RemoveHandler/AddHandler 避免事件冲突。
    ''' </remarks>
    Private Sub btnMoveNext_Click(Sender As Object, E As EventArgs) Handles btnMoveNext.Click
        Dim intPosition As Integer
        objCurrencyManager.Position += 1            ' 下一条记录（边界由 CurrencyManager 自动处理）
        intPosition = objCurrencyManager.Position
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
        ShowPosition()
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0)
    End Sub


    ''' <summary>
    ''' 功能：将当前记录位置直接跳到数据集的最后一条记录（Count - 1）。
    '''       同步 DataGridView 指针与记录位置标签。
    '''       涉及对象：objCurrencyManager、grdAuthorTitles、txtRecordPosition。
    ''' </summary>
    ''' <remarks>
    ''' 【注意】若数据集为空（Count = 0），Position = -1 会导致后续 Rows(-1) 报错，
    '''         因此建议在调用前确保 Count > 0（本方法在 Load 后调用，通常已满足）。
    ''' 【历史踩坑】同 btnMoveFirst_Click，需 RemoveHandler/AddHandler 避免事件冲突。
    ''' </remarks>
    Private Sub btnMoveLast_Click(Sender As Object, E As EventArgs) Handles btnMoveLast.Click
        Dim intPosition As Integer
        objCurrencyManager.Position = objCurrencyManager.Count - 1  ' 定位到最后一条记录
        intPosition = objCurrencyManager.Position
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
        ShowPosition()
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0)
    End Sub


    ''' <summary>
    ''' 功能：作为"未处理记录查看"的切换按钮，每次点击切换两种状态：
    '''       ① 未筛选状态 → 用 objDataView.RowFilter 只保留"处置完成 = False"的记录，
    '''                      按钮标签改为"显示全部"；
    '''       ② 已筛选状态 → 清空 RowFilter，恢复显示全部记录，
    '''                      按钮标签改回"未处理记录请点该按钮查看"。
    '''       同时遍历 DataGridView 数据行，将已完成行着黑色、未完成行着红色，
    '''       便于在"全部"视图中一眼区分。
    '''       涉及对象：objDataView、grdAuthorTitles、btnDisplayingRedData、objCurrencyManager。
    ''' </summary>
    ''' <remarks>
    ''' 【关键机制】
    '''   - RowFilter 直接作用于 objDataView，所有绑定到它的控件（Grid、导航按钮、位置标签）
    '''     都会自动同步，无需逐个刷新。
    '''   - 筛选后 objCurrencyManager.Position 会自动重置为 0，需手动调用 ShowPosition() 更新标签。
    ''' 【历史踩坑】
    '''   1. 第18列数据类型为 System.Boolean（已验证），RowFilter 语法用 "处置完成 = False"。
    '''   2. Grid 必须绑定 objDataView 而非 objDataSet，否则 RowFilter 不生效（本次已修复 Load）。
    '''   3. 循环终止条件用 RowCount - 2（跳过末尾空白新行），避免读到 DBNull 抛异常。
    '''   4. 单元格值可能为 Nothing，用 If(...) 兜底为 False，避免 ToString() 抛异常被 WinForms 吞掉。
    ''' </remarks>
    Private Sub btnDisplayingRedData_Click(sender As Object, e As EventArgs) Handles btnDisplayingRedData.Click
        ' ============================================================
        ' ★★★ 第1步：异常处理外层 ★★★
        ' ============================================================
        ' 原因：RowFilter 语法错误、单元格值为 Nothing 都会抛异常，
        '       必须捕获，否则按钮表现为"无反应"。
        Try
            ' ============================================================
            ' ★★★ 第2步：切换筛选状态 ★★★
            ' ============================================================
            ' 使用模块级变量 blnFilteringUnfinished 记录当前是否处于"只看未完成"状态。
            ' 每次点击取反，实现两种状态的来回切换。
            blnFilteringUnfinished = Not blnFilteringUnfinished

            If blnFilteringUnfinished Then
                ' ---- 进入筛选状态：只显示"处置完成 = False"的记录 ----
                ' 说明：字段类型为 System.Boolean，故直接写 False，无需用 0/-1。
                objDataView.RowFilter = "处置完成 = False"
                btnDisplayingRedData.Text = "显示全部"   ' 提示用户"再点一次可恢复"
            Else
                ' ---- 退出筛选状态：清空筛选，恢复全部记录 ----
                objDataView.RowFilter = ""               ' 空字符串 = 不过滤
                btnDisplayingRedData.Text = "未处理记录请点该按钮查看"
            End If

            ' ============================================================
            ' ★★★ 第3步：遍历着色（红色=未完成，黑色=已完成） ★★★
            ' ============================================================
            ' 说明：筛选切换后，Grid 会按新数据源重新渲染，
            '       但已着色的行样式不会被自动清除，故每次点击都重新遍历一遍，
            '       保证两种状态下的颜色都正确。
            ' ---- 复用 Font 对象，避免循环内 New 3000 次（性能优化） ----
            ' 【原理】Font 是 GDI 资源，创建/销毁开销大；
            '         原代码每次循环都 New，导致 3000 行时明显卡顿。
            ' 【注意】对象不手动 Dispose，交给 Grid 持有引用，窗体销毁时统一释放。
            Dim objFont As New Font("宋体", 9, FontStyle.Regular)

            For i As Integer = 0 To grdAuthorTitles.RowCount - 2
                ' 读取第 18 列（"处置完成"）的值，Nothing 时按 False 处理
                Dim objCellValue As Object = grdAuthorTitles.Item(18, i).Value
                Dim bolFinished As Boolean = If(objCellValue Is Nothing, False, CType(objCellValue.ToString(), Boolean))

                ' 字体统一复用 objFont，仅切换颜色
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = objFont

                If bolFinished Then
                    grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black
                Else
                    grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red
                End If
            Next
            ' ============================================================
            ' ★★★ 第4步：筛选后重置记录位置并刷新标签 ★★★
            ' ============================================================
            ' 原因：RowFilter 变化会导致 CurrencyManager.Position 自动归零，
            '       必须手动调用 ShowPosition() 更新"当前记录位置"标签。
            If objCurrencyManager.Count > 0 Then
                objCurrencyManager.Position = 0
            End If
            ShowPosition()

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：给出友好提示 ★★★
            ' ============================================================
            MessageBox.Show(
            String.Format("切换未处理记录视图时出错：{0}", ex.Message),
            "FV VSTO - 显示错误",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )
            Debug.WriteLine(String.Format("btnDisplayingRedData_Click 异常: {0}", ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' 功能：窗体加载时初始化整个不良品信息界面。
    '''       包括：加载数据、绑定数据源、配置 DataGridView 列与样式、
    '''             填充各类下拉框选项、设置默认选中项。
    '''       涉及对象：objDataSet、objDataView、grdAuthorTitles、各类 ComboBox。
    ''' </summary>
    ''' <remarks>
    ''' 【历史踩坑修复】
    '''   1. Grid 数据源由 objDataSet 改为 objDataView（RowFilter 才能生效，本次已修复）。
    '''   2. 数据加载通过 FillDataSetAndView() 完成，该方法内部已加异常处理。
    '''   3. 列样式设置必须在 Grid 绑定数据源之后，否则 AutoGenerateColumns 会覆盖样式。
    ''' </remarks>
    ''' 
    Private Sub F01_不良品基本信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sw As New Stopwatch()
        Dim swTotal As New Stopwatch()
        swTotal.Start()

        ' ============================================================
        ' ★★★ 计时①：FillDataSetAndView（数据加载）
        ' ============================================================
        sw.Restart()
        FillDataSetAndView()
        sw.Stop()
        Debug.WriteLine("① FillDataSetAndView: " & sw.ElapsedMilliseconds & "ms")

        ' ============================================================
        ' ★★★ 计时②：Grid 绑定 + 双缓冲
        ' ============================================================
        sw.Restart()
        ' 开启双缓冲（反射方式，改善 3000 行滚动）
        Dim objProp As System.Reflection.PropertyInfo = GetType(DataGridView).GetProperty(
        "DoubleBuffered",
        System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        If objProp IsNot Nothing Then
            objProp.SetValue(grdAuthorTitles, True, Nothing)
        End If

        grdAuthorTitles.AutoGenerateColumns = True
        grdAuthorTitles.DataSource = objDataView
        sw.Stop()
        Debug.WriteLine("② Grid 绑定+双缓冲: " & sw.ElapsedMilliseconds & "ms")

        ' ============================================================
        ' ★★★ 计时③：BindFields（控件绑定）
        ' ============================================================
        sw.Restart()
        BindFields()
        sw.Stop()
        Debug.WriteLine("③ BindFields: " & sw.ElapsedMilliseconds & "ms")

        ' ============================================================
        ' ★★★ 计时④：列样式设置
        ' ============================================================
        sw.Restart()
        Dim objAlignRightCellStyle As New DataGridViewCellStyle
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle

        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        objCurrencyCellStyle.Format = "¥#,##0.00"

        grdAuthorTitles.Columns(0).HeaderText = "管理编号"
        grdAuthorTitles.Columns(1).HeaderText = "发生日期"
        grdAuthorTitles.Columns(2).HeaderText = "客户"
        grdAuthorTitles.Columns(3).HeaderText = "供应商"
        grdAuthorTitles.Columns(4).HeaderText = "产品规格"
        grdAuthorTitles.Columns(5).HeaderText = "加工设备"
        grdAuthorTitles.Columns(6).HeaderText = "发现过程"
        grdAuthorTitles.Columns(7).HeaderText = "不良类型"
        grdAuthorTitles.Columns(8).HeaderText = "操作者"
        grdAuthorTitles.Columns(9).HeaderText = "类型区分"
        grdAuthorTitles.Columns(10).HeaderText = "不良数量"
        grdAuthorTitles.Columns(11).HeaderText = "完成工序"
        grdAuthorTitles.Columns(12).HeaderText = "加工费用"
        grdAuthorTitles.Columns(13).HeaderText = "材料费用"
        grdAuthorTitles.Columns(14).HeaderText = "损失成本"
        grdAuthorTitles.Columns(15).HeaderText = "不良现象及原因"
        grdAuthorTitles.Columns(15).Width = 130
        grdAuthorTitles.Columns(16).HeaderText = "备注"
        grdAuthorTitles.Columns(17).HeaderText = "重量"
        grdAuthorTitles.Columns(18).HeaderText = "处置完成"
        grdAuthorTitles.Columns(18).Width = 60
        grdAuthorTitles.Columns(19).HeaderText = "因素确定"
        grdAuthorTitles.Columns(20).HeaderText = "图片路径"

        grdAuthorTitles.Columns("加工费用").HeaderCell.Value = "加工费用_内"
        grdAuthorTitles.Columns("加工费用").HeaderCell.Style = objAlignRightCellStyle
        grdAuthorTitles.Columns("加工费用").DefaultCellStyle = objCurrencyCellStyle
        grdAuthorTitles.Columns("材料费用").HeaderCell.Style = objAlignRightCellStyle
        grdAuthorTitles.Columns("材料费用").DefaultCellStyle = objCurrencyCellStyle
        grdAuthorTitles.Columns("损失成本").HeaderCell.Style = objAlignRightCellStyle
        grdAuthorTitles.Columns("损失成本").DefaultCellStyle = objCurrencyCellStyle

        objCurrencyCellStyle = Nothing
        objAlternatingCellStyle = Nothing
        objAlignRightCellStyle = Nothing
        sw.Stop()
        Debug.WriteLine("④ 列样式设置: " & sw.ElapsedMilliseconds & "ms")

        ' ============================================================
        ' ★★★ 计时⑤：硬编码下拉框填充
        ' ============================================================
        ' 【性能优化】用 BeginUpdate/EndUpdate 包裹批量填充，
        '             避免每次 Add 都触发 ComboBox 重绘（省 ~100ms）。
        sw.Restart()

        ' ---- 排序字段：数组批量填充（本身已高效） ----
        排序字段.Items.Clear()
        排序字段.Items.AddRange(myArray)
        排序字段.SelectedIndex = 0

        ' ---- 客户：逐项 Add，用 BeginUpdate 优化 ----
        客户.BeginUpdate()
        客户.Items.Clear()
        客户.Items.Add("日本日立") : 客户.Items.Add("德國久保田") : 客户.Items.Add("日本久保田")
        客户.Items.Add("常州现代") : 客户.Items.Add("GE") : 客户.Items.Add("印度日立")
        客户.Items.Add("发注至总公司") : 客户.Items.Add("苏州斗山山猫") : 客户.Items.Add("烟台斗山")
        客户.Items.Add("VOLVO") : 客户.Items.Add("远景能源")
        客户.EndUpdate()

        ' ---- 供应商：逐项 Add，用 BeginUpdate 优化 ----
        供应商.BeginUpdate()
        供应商.Items.Clear()
        供应商.Items.Add("荣程A") : 供应商.Items.Add("新顺章B") : 供应商.Items.Add("海陆C")
        供应商.Items.Add("利元D") : 供应商.Items.Add("广源E") : 供应商.Items.Add("瑞鑫F")
        供应商.Items.Add("纽威G") : 供应商.Items.Add("荣冠H") : 供应商.Items.Add("派克L")
        供应商.EndUpdate()

        ' ---- 类型区分：逐项 Add，用 BeginUpdate 优化 ----
        类型区分.BeginUpdate()
        类型区分.Items.Clear()
        类型区分.Items.Add("I/N") : 类型区分.Items.Add("O/T") : 类型区分.Items.Add("Assembly")
        类型区分.EndUpdate()

        sw.Stop()
        Debug.WriteLine("⑤ 硬编码下拉框: " & sw.ElapsedMilliseconds & "ms")

        ' ★★★ 计时⑥：数据库读取下拉框（惰性化）
        ' ============================================================
        ' 【性能优化】惰性化：产品规格/加工费用/材料费用的联动查询
        '             推迟到用户实际修改"产品规格"时触发，
        '             Load 里只填充必要的两个下拉框。
        sw.Restart()
        objConnection1th.Open()
        Try
            ' ---- 发现过程下拉框 ----
            objDataAdapter1th.SelectCommand = New OleDbCommand()
            objDataAdapter1th.SelectCommand.Connection = objConnection1th
            objDataAdapter1th.SelectCommand.CommandText = "select distinct 赔偿比例.* from 赔偿比例 ORDER BY 比例"
            objDataSet1th = New DataSet()
            objDataAdapter1th.Fill(objDataSet1th, "wpxx04")
            Dim tb1 As DataTable = objDataSet1th.Tables("wpxx04")
            发现过程.Items.Clear()
            For inCounter = 0 To tb1.Rows.Count - 1
                发现过程.Items.Add(tb1.Rows(inCounter).Item(0).ToString())
            Next

            ' ---- 不良类型下拉框 ----
            objDataAdapter1th.SelectCommand.CommandText = "select distinct 不良类型 from 不良类型分类"
            objDataSet1th = New DataSet()
            objDataAdapter1th.Fill(objDataSet1th, "wpxx14")
            Dim tb2 As DataTable = objDataSet1th.Tables("wpxx14")
            不良类型.Items.Clear()
            For inCounter = 0 To tb2.Rows.Count - 1
                不良类型.Items.Add(tb2.Rows(inCounter).Item(0).ToString())
            Next
        Finally
            objConnection1th.Close()
        End Try
        sw.Stop()
        Debug.WriteLine("⑥ 数据库读取下拉框: " & sw.ElapsedMilliseconds & "ms")

        ' ============================================================
        ' ★★★ 计时⑦：排序 + ShowPosition
        ' ============================================================
        sw.Restart()
        ' 【性能优化】SQL 已 ORDER BY DESC，无需再 Sort
        If 排序字段.Items.Count > 1 Then
            排序字段.SelectedIndex = 1
        End If
        ShowPosition()
        sw.Stop()
        Debug.WriteLine("⑦ 排序+ShowPosition: " & sw.ElapsedMilliseconds & "ms")

        swTotal.Stop()
        Debug.WriteLine("=== Load 总耗时: " & swTotal.ElapsedMilliseconds & "ms ===")
    End Sub
    'Private Sub F01_不良品基本信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    '    ' ============================================================
    '    ' ★★★ 开启 DataGridView 双缓冲，改善 3000 行滚动性能 ★★★
    '    ' ============================================================
    '    ' 【原理】双缓冲先把内容绘制到内存，再一次性输出到屏幕，
    '    '         避免逐行绘制造成的闪烁和卡顿。
    '    ' 【说明】DoubleBuffered 是受保护属性，无法直接设置，
    '    '         需通过反射访问基类的 Protected 属性（这是 WinForms 通用技巧）。
    '    ' 【历史踩坑】不设置该属性时，3000 行滚动明显"一愣一愣"。
    '    Dim objProp As System.Reflection.PropertyInfo = GetType(DataGridView).GetProperty(
    '        "DoubleBuffered",
    '        System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
    '    If objProp IsNot Nothing Then
    '        objProp.SetValue(grdAuthorTitles, True, Nothing)
    '    End If

    '    ' ============================================================
    '    ' ★★★ 第1步：加载数据并刷新界面基础状态 ★★★
    '    ' ============================================================
    '    ' FillDataSetAndView()：填充 objDataSet → 构建 objDataView → 获取 objCurrencyManager
    '    ' ShowPosition()：刷新"当前记录位置"标签（如 "1 of 2617"）
    '    FillDataSetAndView()
    '    ShowPosition()

    '    ' ============================================================
    '    ' ★★★ 第2步：将 Grid 绑定到 objDataView ★★★
    '    ' ============================================================
    '    ' 【关键】必须绑定 objDataView，而非 objDataSet：
    '    '   - RowFilter 只对 DataView 生效，绑定 DataSet 会导致筛选功能失效（本次踩坑）。
    '    '   - DataView 自带表结构，无需再设 DataMember。
    '    ' AutoGenerateColumns = True：让 Grid 根据数据源自动创建所有列。
    '    grdAuthorTitles.AutoGenerateColumns = True
    '    grdAuthorTitles.DataSource = objDataView

    '    ' ============================================================
    '    ' ★★★ 新增：绑定字段到 GroupBox1 内控件 ★★★
    '    ' ============================================================
    '    ' 【关键】BindFields() 只在此处调用一次，建立持久绑定。
    '    '   后续切换记录时，CurrencyManager 会自动同步控件显示，
    '    '   无需在 SelectionChanged 里重复调用（历史踩坑：曾因重复调用导致 3000 行卡顿）。
    '    ' 调用时机：必须在 FillDataSetAndView() 之后，因为 objDataView 已就绪。
    '    BindFields()


    '    ' ============================================================
    '    ' ★★★ 第3步：配置 DataGridView 列标题与单元格样式 ★★★
    '    ' ============================================================
    '    ' 说明：所有列样式设置必须在 Grid 绑定数据源之后执行，
    '    '       否则 AutoGenerateColumns 重新生成列时会覆盖此处设置（历史踩坑）。

    '    ' ---- 3.1 定义通用样式对象 ----
    '    ' objAlignRightCellStyle：右对齐样式，用于金额类列标题。
    '    Dim objAlignRightCellStyle As New DataGridViewCellStyle
    '    objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    '    ' objAlternatingCellStyle：交替行背景色，提升可读性。
    '    Dim objAlternatingCellStyle As New DataGridViewCellStyle()
    '    objAlternatingCellStyle.BackColor = Color.WhiteSmoke
    '    grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle

    '    ' objCurrencyCellStyle：货币格式样式，用于金额类单元格（加工费用/材料费用/损失成本）。
    '    Dim objCurrencyCellStyle As New DataGridViewCellStyle()
    '    objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    '    objCurrencyCellStyle.Format = "¥#,##0.00"

    '    ' ---- 3.2 设置各列标题文字 ----
    '    ' 说明：Grid 自动生成的列标题默认是英文字段名或数据库列名，
    '    '       需逐一映射为中文标题，便于用户理解。
    '    grdAuthorTitles.Columns(0).HeaderText = "管理编号"
    '    grdAuthorTitles.Columns(1).HeaderText = "发生日期"
    '    grdAuthorTitles.Columns(2).HeaderText = "客户"
    '    grdAuthorTitles.Columns(3).HeaderText = "供应商"
    '    grdAuthorTitles.Columns(4).HeaderText = "产品规格"
    '    grdAuthorTitles.Columns(5).HeaderText = "加工设备"
    '    grdAuthorTitles.Columns(6).HeaderText = "发现过程"
    '    grdAuthorTitles.Columns(7).HeaderText = "不良类型"
    '    grdAuthorTitles.Columns(8).HeaderText = "操作者"
    '    grdAuthorTitles.Columns(9).HeaderText = "类型区分"
    '    grdAuthorTitles.Columns(10).HeaderText = "不良数量"
    '    grdAuthorTitles.Columns(11).HeaderText = "完成工序"
    '    grdAuthorTitles.Columns(12).HeaderText = "加工费用"
    '    grdAuthorTitles.Columns(13).HeaderText = "材料费用"
    '    grdAuthorTitles.Columns(14).HeaderText = "损失成本"
    '    grdAuthorTitles.Columns(15).HeaderText = "不良现象及原因"
    '    grdAuthorTitles.Columns(15).Width = 130                 ' 现象描述较长，加宽便于阅读
    '    grdAuthorTitles.Columns(16).HeaderText = "备注"
    '    grdAuthorTitles.Columns(17).HeaderText = "重量"
    '    grdAuthorTitles.Columns(18).HeaderText = "处置完成"
    '    grdAuthorTitles.Columns(18).Width = 60                  ' 布尔值只需小宽度
    '    grdAuthorTitles.Columns(19).HeaderText = "因素确定"
    '    grdAuthorTitles.Columns(20).HeaderText = "图片路径"

    '    ' ---- 3.3 金额类列设置特殊样式（标题右对齐 + 单元格货币格式） ----
    '    ' 说明：Columns("列名") 通过列名引用，需保证列名与数据库字段一致。
    '    grdAuthorTitles.Columns("加工费用").HeaderCell.Value = "加工费用_内"  ' 标题改写便于区分
    '    grdAuthorTitles.Columns("加工费用").HeaderCell.Style = objAlignRightCellStyle
    '    grdAuthorTitles.Columns("加工费用").DefaultCellStyle = objCurrencyCellStyle

    '    grdAuthorTitles.Columns("材料费用").HeaderCell.Style = objAlignRightCellStyle
    '    grdAuthorTitles.Columns("材料费用").DefaultCellStyle = objCurrencyCellStyle

    '    grdAuthorTitles.Columns("损失成本").HeaderCell.Style = objAlignRightCellStyle
    '    grdAuthorTitles.Columns("损失成本").DefaultCellStyle = objCurrencyCellStyle

    '    ' ---- 3.4 释放临时样式对象 ----
    '    ' 说明：样式已赋值给 Grid，临时对象可置 Nothing 释放引用（GC 后续回收）。
    '    objCurrencyCellStyle = Nothing
    '    objAlternatingCellStyle = Nothing
    '    objAlignRightCellStyle = Nothing

    '    ' ============================================================
    '    ' ★★★ 第4步：填充"排序字段"下拉框 ★★★
    '    ' ============================================================
    '    ' 说明：myArray 是模块级数组，包含所有可排序字段名，
    '    '       在 BindFields 或类顶部定义，此处直接复用。
    '    ' 【良好习惯】Clear 后 AddRange，避免重复调用 Load 时累积重复项。
    '    排序字段.Items.Clear()
    '    排序字段.Items.AddRange(myArray)
    '    排序字段.SelectedIndex = 0  ' 默认选第一项（"管理编号"）

    '    ' ============================================================
    '    ' ★★★ 第5步：填充"客户"下拉框 ★★★
    '    ' ============================================================
    '    ' 说明：客户列表目前硬编码，后续建议迁移到数据库表统一维护（TODO）。
    '    客户.Items.Clear()
    '    客户.Items.Add("日本日立") : 客户.Items.Add("德國久保田") : 客户.Items.Add("日本久保田")
    '    客户.Items.Add("常州现代") : 客户.Items.Add("GE") : 客户.Items.Add("印度日立")
    '    客户.Items.Add("发注至总公司") : 客户.Items.Add("苏州斗山山猫") : 客户.Items.Add("烟台斗山")
    '    客户.Items.Add("VOLVO") : 客户.Items.Add("远景能源")

    '    ' ============================================================
    '    ' ★★★ 第6步：填充"供应商"下拉框 ★★★
    '    ' ============================================================
    '    ' 说明：同客户列表，建议后续统一迁移到数据表。
    '    供应商.Items.Clear()
    '    供应商.Items.Add("荣程A") : 供应商.Items.Add("新顺章B") : 供应商.Items.Add("海陆C")
    '    供应商.Items.Add("利元D") : 供应商.Items.Add("广源E") : 供应商.Items.Add("瑞鑫F")
    '    供应商.Items.Add("纽威G") : 供应商.Items.Add("荣冠H") : 供应商.Items.Add("派克L")

    '    ' ============================================================
    '    ' ★★★ 第7步：填充"类型区分"下拉框 ★★★
    '    ' ============================================================
    '    ' 说明：I/N、O/T、Assembly 是内部分类编码，含义由业务侧约定。
    '    类型区分.Items.Clear()
    '    类型区分.Items.Add("I/N") : 类型区分.Items.Add("O/T") : 类型区分.Items.Add("Assembly")

    '    ' ============================================================
    '    ' ★★★ 第8步：触发"产品规格"选中事件，联动刷新其他下拉框 ★★★
    '    ' ============================================================
    '    ' 说明：产品规格选中后会触发 SelectedIndexChanged，联动加载相关选项。
    '    '       此处传入 Nothing 手动触发一次，确保初始状态下下拉框已填充。
    '    产品规格_SelectedIndexChanged(Nothing, Nothing)

    '    ' ============================================================
    '    ' ★★★ 第9步：填充"发现过程"下拉框（从"赔偿比例"表动态读取） ★★★
    '    ' ============================================================
    '    ' 【机制说明】
    '    '   - 用 objDataAdapter1th 执行临时查询，填充到 objDataSet1th，再从表中循环读取。
    '    '   - SQL 用 SELECT DISTINCT 去重，ORDER BY 比例 保证顺序稳定。
    '    ' 【连接来源】
    '    '   objDataAdapter1th 的连接不是声明时绑定的，而是在
    '    '   产品规格_SelectedIndexChanged 事件中通过下面两行绑定：
    '    '       objDataAdapter1th.SelectCommand = New OleDbCommand()
    '    '       objDataAdapter1th.SelectCommand.Connection = objConnection1th
    '    '   因此 Load 里必须先调用 产品规格_SelectedIndexChanged(Nothing, Nothing)
    '    '   完成初始化，才能执行本段的 Fill 操作。
    '    '   ⚠ TODO：该事件用了 On Error Resume Next，会吞异常，后续应改为 Try...Catch。
    '    ' 【历史踩坑】
    '    '   objDataAdapter1th 是模块级对象，多次 Fill 前需重新 New DataSet 避免数据累积。
    '    objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "赔偿比例.*" & " from " & "赔偿比例 ORDER BY 比例"
    '    objDataSet1th = New DataSet()                          ' 重新初始化，避免数据累积
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx04")        ' 第二参数为内存表名，便于后续引用
    '    Dim tb1 As DataTable = objDataSet1th.Tables("wpxx04")  ' 取出表对象
    '    发现过程.Items.Clear()
    '    For inCounter = 0 To tb1.Rows.Count - 1                ' 遍历表行填充下拉框
    '        发现过程.Items.Add(tb1.Rows(inCounter).Item(0).ToString())
    '    Next

    '    ' ============================================================
    '    ' ★★★ 第10步：填充"不良类型"下拉框（从"不良类型分类"表动态读取） ★★★
    '    ' ============================================================
    '    ' 【机制说明】
    '    '   - 与"发现过程"类似，使用 objDataAdapter1th 执行临时查询。
    '    '   - SQL 用 SELECT DISTINCT 去重，来源为"不良类型分类"表。
    '    ' 【连接来源】
    '    '   同第9步：依赖 产品规格_SelectedIndexChanged 中初始化的连接。
    '    ' 【历史踩坑】
    '    '   objDataAdapter1th 是模块级对象，多次 Fill 前需重新 New DataSet 避免数据累积。
    '    objDataAdapter1th.SelectCommand.CommandText = "select distinct 不良类型 from 不良类型分类"
    '    objDataSet1th = New DataSet()                          ' 重新初始化，避免数据累积
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx14")        ' 第二参数为内存表名，便于后续引用
    '    Dim tb2 As DataTable = objDataSet1th.Tables("wpxx14")  ' 取出表对象
    '    不良类型.Items.Clear()
    '    For inCounter = 0 To tb2.Rows.Count - 1                ' 遍历表行填充下拉框
    '        不良类型.Items.Add(tb2.Rows(inCounter).Item(0).ToString())
    '    Next

    '    ' ============================================================
    '    ' ★★★ 第11步：默认按"发生日期"降序排列（新记录在顶部） ★★★
    '    ' ============================================================
    '    ' 【优化说明】原作者在"添加"后才排序，导致打开窗体时视图
    '    '             按"管理编号"排列，与用户预期的"最近不良在顶部"不符。
    '    '   现改为：Load 完成后立即按"发生日期 DESC"排序，
    '    '           保持打开、添加、查询三种场景的排序一致。
    '    ' 【历史踩坑】
    '    '   - 若直接改 objDataView.Sort，不会自动刷新 CurrencyManager 位置，
    '    '     需配合 ShowPosition() 确保标签同步。
    '    objDataView.Sort = "发生日期 DESC"
    '    If 排序字段.Items.Count > 1 Then
    '        排序字段.SelectedIndex = 1   ' 下拉框同步显示"发生日期"
    '    End If
    '    ShowPosition()                   ' 刷新"当前记录位置"标签
    'End Sub

    ''加载窗体触发事件
    'Private Sub F01_不良品基本信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    '需要说明的是,Fill方法会执行命令(SelectCommand),其Connection属性保持为调用该方法时的状态.
    '    'On Error Resume Next
    '    FillDataSetAndView() '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定数据源字段到指定控件.
    '    ShowPosition()  '调用ShowPosition方法,并显示当前记录标签位置    
    '    'BindFields()  '调用绑定控件过程,因为有复合框,所以放在事件最后面.
    '    grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.

    '    ' 【历史踩坑修复】Grid 数据源由 objDataSet 改为 objDataView。
    '    ' 原因：RowFilter 只对 DataView 生效；若 Grid 直接绑 DataSet，
    '    '       则 objDataView.RowFilter 改了也不影响界面，筛选功能会失效（本次调试已踩坑）。
    '    ' 注意：DataView 自带表结构，无需再设 DataMember。
    '    grdAuthorTitles.DataSource = objDataView

    '    '将对齐方式格式改为垂直居中向右对齐.
    '    Dim objAlignRightCellStyle As New DataGridViewCellStyle  '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例) 
    '    objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


    '    Dim objAlternatingCellStyle As New DataGridViewCellStyle() '初始化DataGridViewCellStyle对象(grd控件单元格样式实例) 作为交叉行样式  
    '    objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置交叉样式背景色为烟灰色
    '    grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
    '    Dim objCurrencyCellStyle As New DataGridViewCellStyle()  '初始化DataGridViewCellStyle对象,将设置单元格格式为货币型.
    '    objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式改为居中向左对齐
    '    objCurrencyCellStyle.Format = "¥#,##0.00" '样式格式为货币型(美元或者人民币$¥)
    '    'objCurrencyCellStyle.Format = "C"  '样式格式为货币型(人民币)
    '    grdAuthorTitles.Columns(0).HeaderText = "管理编号"   '设置控件列标题   
    '    'grdAuthorTitles.Columns(1).HeaderText = "发生日期"
    '    grdAuthorTitles.Columns(1).HeaderText = "录入日期"
    '    grdAuthorTitles.Columns(2).HeaderText = "客户"
    '    grdAuthorTitles.Columns(3).HeaderText = "供应商"
    '    grdAuthorTitles.Columns(4).HeaderText = "产品规格"
    '    grdAuthorTitles.Columns(5).HeaderText = "加工设备"
    '    grdAuthorTitles.Columns(6).HeaderText = "发现过程"
    '    grdAuthorTitles.Columns(7).HeaderText = "不良类型"
    '    grdAuthorTitles.Columns(8).HeaderText = "操作者"
    '    grdAuthorTitles.Columns(9).HeaderText = "类型区分"
    '    grdAuthorTitles.Columns(10).HeaderText = "不良数量"
    '    grdAuthorTitles.Columns(11).HeaderText = "完成工序"
    '    grdAuthorTitles.Columns(12).HeaderText = "加工费用"
    '    grdAuthorTitles.Columns(13).HeaderText = "材料费用"
    '    grdAuthorTitles.Columns(14).HeaderText = "损失成本"
    '    grdAuthorTitles.Columns(15).HeaderText = "不良现象及原因"
    '    grdAuthorTitles.Columns(15).Width = 130 '设置指定列默认宽度大一点
    '    grdAuthorTitles.Columns(16).HeaderText = "备注"
    '    grdAuthorTitles.Columns(17).HeaderText = "重量"
    '    grdAuthorTitles.Columns(18).HeaderText = "处置完成"
    '    grdAuthorTitles.Columns(18).Width = 60 '设置指定列默认宽度大一点
    '    grdAuthorTitles.Columns(19).HeaderText = "因素确定"
    '    grdAuthorTitles.Columns(20).HeaderText = "图片路径"
    '    '自动调整列宽.
    '    'grdAuthorTitles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells


    '    ''改变字段标题名称和样式'Change column names and styles using the column name  
    '    grdAuthorTitles.Columns("加工费用").HeaderCell.Value = "加工费用_内" '重新设置列标题的值显示为"描述"
    '    '标题重新调用列标题样式(之前设定的-居中右对齐)
    '    grdAuthorTitles.Columns("加工费用").HeaderCell.Style = objAlignRightCellStyle
    '    '单元格内容重新调用样式(之前设定的-货币样式)
    '    grdAuthorTitles.Columns("加工费用").DefaultCellStyle = objCurrencyCellStyle
    '    grdAuthorTitles.Columns("材料费用").HeaderCell.Style = objAlignRightCellStyle
    '    '单元格内容重新调用样式(之前设定的-货币样式)
    '    grdAuthorTitles.Columns("材料费用").DefaultCellStyle = objCurrencyCellStyle
    '    grdAuthorTitles.Columns("损失成本").HeaderCell.Style = objAlignRightCellStyle
    '    '单元格内容重新调用样式(之前设定的-货币样式)
    '    grdAuthorTitles.Columns("损失成本").DefaultCellStyle = objCurrencyCellStyle

    '    ''遍历记录数量
    '    'For i As Integer = 0 To grdAuthorTitles.RowCount - 1  '有一个空白行也算一行
    '    '    If Math.Ceiling(CType(grdAuthorTitles.Item(7, i).Value.ToString(), Date).Subtract(Now).TotalDays) <= 20 Then
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
    '    '    Else
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
    '    '    End If
    '    'Next

    '    'For i As Integer = 0 To grdAuthorTitles.RowCount - 2                           '有一个空白行也算一行
    '    '    If CType(grdAuthorTitles.Item(18, i).Value.ToString(), Boolean) Then
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色

    '    '    Else
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '    '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
    '    '    End If
    '    'Next

    '    objCurrencyCellStyle = Nothing     '清除样式对象(单元格记录内容用)
    '    objAlternatingCellStyle = Nothing  '清除交叉单元格样式
    '    objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
    '    排序字段.Items.Clear()   '给组合框添加项目  'Add items to the combo box..
    '    排序字段.Items.AddRange(myArray)
    '    排序字段.SelectedIndex = 0         '默认选择第一项
    '    客户.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '    '添加项目
    '    客户.Items.Add("日本日立") ： 客户.Items.Add("德國久保田") ： 客户.Items.Add("日本久保田") ： 客户.Items.Add("常州现代") ： 客户.Items.Add("GE") ： 客户.Items.Add("印度日立") ： 客户.Items.Add("发注至总公司")
    '    客户.Items.Add("苏州斗山山猫") ： 客户.Items.Add("烟台斗山") ： 客户.Items.Add("VOLVO") ： 客户.Items.Add("远景能源")
    '    供应商.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '    供应商.Items.Add("荣程A") ： 供应商.Items.Add("新顺章B") ： 供应商.Items.Add("海陆C") ： 供应商.Items.Add("利元D") ： 供应商.Items.Add("广源E") ： 供应商.Items.Add("瑞鑫F")
    '    供应商.Items.Add("纽威G") ： 供应商.Items.Add("荣冠H") ： 供应商.Items.Add("派克L")


    '    类型区分.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '    类型区分.Items.Add("I/N") ： 类型区分.Items.Add("O/T") ： 类型区分.Items.Add("Assembly")
    '    产品规格_SelectedIndexChanged(Nothing, Nothing)
    '    '维修类型.SelectedIndex = 0  '默认选择第一项




    '    'objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
    '    'objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
    '    'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 ORDER BY 产品规格" '写入SQL语句

    '    ''objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 ORDER BY 物品编号" '写入SQL语句
    '    'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
    '    'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    'objDataAdapter1th.Fill(objDataSet1th, "wpxx01")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    'Dim tb As DataTable = objDataSet1th.Tables("wpxx01") '声明一个表类型,并赋值给该变量.
    '    '产品规格.Items.Clear()                               '清楚复合框项目集
    '    'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
    '    '    产品规格.Items.Add(tb.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    'Next
    '    'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
    '    objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "赔偿比例.*" & " from " & "赔偿比例 ORDER BY 比例" '写入SQL语句
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx04")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb1 As DataTable = objDataSet1th.Tables("wpxx04") '声明一个表类型,并赋值给该变量.
    '    发现过程.Items.Clear()                               '清楚复合框项目集
    '    For inCounter = 0 To tb1.Rows.Count - 1               '在表行数上循环
    '        发现过程.Items.Add(tb1.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next
    '    'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
    '    objDataAdapter1th.SelectCommand.CommandText = "select distinct 不良类型" & " from " & "不良类型分类" '写入SQL语句
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx14")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb2 As DataTable = objDataSet1th.Tables("wpxx14") '声明一个表类型,并赋值给该变量.
    '    不良类型.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '    '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
    '    '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
    '    '不良类型.Items.Add("客户发现不良")
    '    For inCounter = 0 To tb2.Rows.Count - 1               '在表行数上循环
    '        不良类型.Items.Add(tb2.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next
    '    BindFields()  '调用绑定控件过程
    'End Sub

    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件数据源进行排序.
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性.
        Select Case 排序字段.SelectedIndex      'Determine the appropriate item selected and set the Sort property of the DataView object..            
            ' {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因", "备注"}
            Case 0
                objDataView.Sort = "管理编号"   '按字段设备编号升序排序,下同.
            Case 1
                objDataView.Sort = "发生日期"
            Case 2
                objDataView.Sort = "客户"
            Case 3
                objDataView.Sort = "供应商"
            Case 4
                objDataView.Sort = "产品规格"
            Case 5
                objDataView.Sort = "加工设备"
            Case 6
                objDataView.Sort = "发现过程"
            Case 7
                objDataView.Sort = "不良类型"
            Case 8
                objDataView.Sort = "操作者"
            Case 9
                objDataView.Sort = "类型区分"
            Case 10
                objDataView.Sort = "不良数量"
            Case 11
                objDataView.Sort = "完成工序"
            Case 12
                objDataView.Sort = "加工费用"
            Case 13
                objDataView.Sort = "材料费用"
            Case 14
                objDataView.Sort = "损失成本"
            Case 15
                objDataView.Sort = "不良现象及原因"
            Case 16
                objDataView.Sort = "备注"
            Case 17
                objDataView.Sort = "重量"
            Case 18
                objDataView.Sort = "处置完成"
            Case 19
                objDataView.Sort = "因素确定"

        End Select
        btnMoveFirst_Click(Nothing, Nothing)      '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted"   '修改状态标签Text属性. Display a message that the records have been sorted..
    End Sub

    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        ' myArray = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", 
        '"不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
        Dim intPosition As Integer              '执行查找,声明当前局部变量.'Declare local variables.. 
        Dim str条件 As String = ""
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
        'Determine the appropriate item selected And set the Sort property of the DataView object..
        Select Case 排序字段.SelectedIndex
            '"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"
            Case 0
                objDataView.Sort = "管理编号"
                str条件 = "管理编号"
            Case 1
                objDataView.Sort = "发生日期"
                str条件 = "发生日期"

            Case 2
                objDataView.Sort = "客户"
                str条件 = "客户"
            Case 3
                objDataView.Sort = "供应商"
                str条件 = "供应商"
            Case 4
                objDataView.Sort = "产品规格"
                str条件 = "产品规格"
            Case 5
                objDataView.Sort = "加工设备"
                str条件 = "加工设备"
            Case 6
                objDataView.Sort = "发现过程"
                str条件 = "发现过程"
            '"不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
            Case 7
                objDataView.Sort = "不良类型"
                str条件 = "不良类型"
            Case 8
                objDataView.Sort = "操作者"
                str条件 = "操作者"
            Case 9
                objDataView.Sort = "类型区分"
                str条件 = "类型区分"
            Case 10
                objDataView.Sort = "不良数量"
                str条件 = "不良数量"
            Case 11
                objDataView.Sort = "完成工序"
                str条件 = "完成工序"
            Case 12
                objDataView.Sort = "加工费用"
                str条件 = "加工费用"
            Case 13
                objDataView.Sort = "材料费用"
                str条件 = "材料费用"
            Case 14
                objDataView.Sort = "损失成本"
                str条件 = "损失成本"
            Case 15
                objDataView.Sort = "不良现象及原因"
                str条件 = "不良现象及原因"
            Case 16
                objDataView.Sort = "备注"
                str条件 = "备注"
            Case 17
                objDataView.Sort = "重量"
                str条件 = "重量"
            Case 18
                objDataView.Sort = "处置完成"
                str条件 = "处置完成"
            Case 19
                objDataView.Sort = "因素确定"
                str条件 = "因素确定"
        End Select
        If str条件 = "发生日期" Then
            objDataView.RowFilter = str条件 & "=#" & CType(查询条件.Text, Date).ToShortDateString & "#" '"Date = #12/31/2008 16:44:58#"
        ElseIf str条件 <> "处置完成" Then    'DataView数据表中筛选数据集(类似SQL语句).
            objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"
        Else
            objDataView.RowFilter = str条件 & "=" & CType(查询条件.Text, Boolean)
        End If
        intPosition = objCurrencyManager.Position  '默认位置赋值给变量
        If intPosition = -1 Then  '状态栏提示没有找到记录 Display a message that the record was not found..
            ToolStripLabel1.Text = "Record Not Found"  '标签显示字符.
            '否则状态栏显示字符..
        Else
            ToolStripLabel1.Text = "Record Found"
        End If
        ShowPosition() '重新显示当前记录位置. Show the current record position..
    End Sub

    '查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If UCase(查询条件.Text) = "DELETE" Then 删除.Enabled = True
        If 查询条件.Text.Length = 0 Then  '如果是空值
            '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置..
            F01_不良品基本信息_Load(Nothing, Nothing)
        End If
    End Sub

    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    ''' <summary>
    ''' 功能：清空所有输入控件，为录入新记录做准备（不写数据库）。
    '''       同时将各下拉框重置为默认选项，避免残留在上一条记录的值。
    '''       涉及对象：GroupBox1 内所有绑定到 objDataView 的控件。
    ''' </summary>
    ''' <remarks>
    ''' 【关键机制】
    '''   - 清空前必须先用 BindingContext(objDataView).EndCurrentEdit() 提交当前编辑，
    '''     否则文本框中的未提交值可能被 DataView 保留。
    '''   - 新建后"删除/更新"按钮应禁用，防止误操作。
    ''' 【历史踩坑】
    '''   - 直接清空控件 Text 属性，但控件仍绑定在 objDataView 上，
    '''     用户切换记录时可能被数据源覆盖，需先解除绑定再清空。
    ''' </remarks>
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        ' ============================================================
        ' ★★★ 第1步：提交当前编辑（防止未提交值残留） ★★★
        ' ============================================================
        ' EndCurrentEdit：通知 CurrencyManager 结束当前单元格的编辑状态。
        Me.BindingContext(objDataView).EndCurrentEdit()

        ' ============================================================
        ' ★★★ 第2步：遍历控件清空内容 ★★★
        ' ============================================================
        ' 说明：仅清空文本框/组合框的 Text，不清除绑定关系（避免破坏 DataView 联动）。
        '       复选框统一置为 False。
        For i As Byte = 0 To UBound(myArray)
            Dim ctrl As Control = GroupBox1.Controls(myArray(i).ToString())
            If TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False
            Else
                ctrl.Text = ""
            End If
        Next

        ' ============================================================
        ' ★★★ 第3步：启用"添加"按钮，禁用"更新/删除" ★★★
        ' ============================================================
        ' 原因：新建记录尚未入库，只能"添加"，不能"更新"或"删除"。
        添加.Enabled = True
        更新.Enabled = False
        删除.Enabled = False

        ' 光标定位到"管理编号"，方便用户快速录入
        管理编号.Focus()
    End Sub

    ''' <summary>
    ''' 功能：将 GroupBox1 中的当前输入值作为一条新记录，插入 Access 数据库"不良品信息"表，
    '''       成功后清空查询条件、显示全部数据，并自动滚动定位到刚添加的新记录。
    '''       涉及对象：objConnection1th、OleDbCommand、objDataView、objCurrencyManager。
    ''' </summary>
    ''' <remarks>
    ''' 【关键机制】
    '''   - 使用参数化 SQL（OleDbParameter）避免 SQL 注入和日期/文本类型转换错误。
    '''   - 字段顺序需与 INSERT INTO 语句严格一致。
    ''' 【历史踩坑】
    '''   1. 日期字段（发生日期）在 Access 中为 Date 类型，需用 CDate() 转换。
    '''   2. 插入后必须重新 Fill 数据源，否则 Grid 看不到新记录。
    '''   3. 原逻辑用"筛选新记录"方式显示，导致其他记录被 RowFilter 过滤掉，
    '''      用户误以为数据丢失。现改为"显示全部 + 定位到新记录"。
    '''   4. 【重要】必须在 FillDataSetAndView 之前保存新记录的 ID，
    '''      因为 Fill 会重建 DataView 和 CurrencyManager，
    '''      导致 GroupBox1 控件被重新绑定到第一条记录，管理编号.Text 会被覆盖，
    '''      后续用 管理编号.Text 匹配就找不到新记录了（本次踩坑）。
    '''   5. 【重要】FillDataSetAndView 内部重建了 objDataView，
    '''      Grid 的 DataSource 需重新绑定才能看到新数据（已在 FillDataSetAndView 中修复）。
    ''' </remarks>
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        ' ============================================================
        ' ★★★ 第1步：构建 INSERT 语句（参数化） ★★★
        ' ============================================================
        ' 说明：? 是占位符，稍后用 OleDbParameter 按顺序填充实际值，
        '       避免 SQL 拼接带来的类型错误和注入风险。
        ' 注意：OleDb 用 ? 而非 @名称，参数顺序必须与 SQL 占位符严格一致。
        Dim strSql As String = "INSERT INTO 不良品信息 (管理编号, 发生日期, 客户, 供应商, 产品规格, " &
                            "加工设备, 发现过程, 不良类型, 操作者, 类型区分, 不良数量, 完成工序, " &
                            "加工费用, 材料费用, 损失成本, 不良现象及原因, 备注, 重量, 处置完成, 因素确定, 图片路径) " &
                            "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

        ' ============================================================
        ' ★★★ 第2步：执行插入 ★★★
        ' ============================================================
        Try
            Dim cmdInsert As New OleDbCommand(strSql, objConnection1th)

            ' ---- 按顺序添加参数（顺序必须与 SQL 中占位符一致） ----
            cmdInsert.Parameters.AddWithValue("@管理编号", 管理编号.Text)
            cmdInsert.Parameters.AddWithValue("@发生日期", CDate(发生日期.Text))   ' 日期需显式转换
            cmdInsert.Parameters.AddWithValue("@客户", 客户.Text)
            cmdInsert.Parameters.AddWithValue("@供应商", 供应商.Text)
            cmdInsert.Parameters.AddWithValue("@产品规格", 产品规格.Text)
            cmdInsert.Parameters.AddWithValue("@加工设备", 加工设备.Text)
            cmdInsert.Parameters.AddWithValue("@发现过程", 发现过程.Text)
            cmdInsert.Parameters.AddWithValue("@不良类型", 不良类型.Text)
            cmdInsert.Parameters.AddWithValue("@操作者", 操作者.Text)
            cmdInsert.Parameters.AddWithValue("@类型区分", 类型区分.Text)
            cmdInsert.Parameters.AddWithValue("@不良数量", 不良数量.Text)
            cmdInsert.Parameters.AddWithValue("@完成工序", 完成工序.Text)
            cmdInsert.Parameters.AddWithValue("@加工费用", 加工费用.Text)
            cmdInsert.Parameters.AddWithValue("@材料费用", 材料费用.Text)
            cmdInsert.Parameters.AddWithValue("@损失成本", 损失成本.Text)
            cmdInsert.Parameters.AddWithValue("@不良现象及原因", 不良现象及原因.Text)
            cmdInsert.Parameters.AddWithValue("@备注", 备注.Text)
            cmdInsert.Parameters.AddWithValue("@重量", 重量.Text)
            cmdInsert.Parameters.AddWithValue("@处置完成", 处置完成.Checked)
            cmdInsert.Parameters.AddWithValue("@因素确定", 因素确定.Text)
            cmdInsert.Parameters.AddWithValue("@图片路径", 图片路径.Text)

            ' ---- 打开连接并执行 ----
            objConnection1th.Open()
            cmdInsert.ExecuteNonQuery()
            objConnection1th.Close()

            ' ============================================================
            ' ★★★ 第3步：保存新记录 ID（必须在 Fill 之前） ★★★
            ' ============================================================
            ' 【关键】FillDataSetAndView 会重建 DataView 和 CurrencyManager，
            '         导致 GroupBox1 控件被重绑到第一条记录，管理编号.Text 会被覆盖。
            '         因此必须在此处先保存新记录的 ID。
            Dim strNewRecordId As String = 管理编号.Text.Trim()

            MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ============================================================
            ' ★★★ 第4步：刷新数据并定位到新记录 ★★★
            ' ============================================================
            ' ---- 4.1 重新加载全部数据 ----
            FillDataSetAndView()

            ' ---- 4.2 清空查询条件，确保显示全部记录 ----
            查询条件.Text = ""

            ' ---- 4.3 保持下拉框与视图一致 ----
            ' 【说明】排序已在 FillDataSetAndView 中设置为"发生日期 DESC"，
            '         此处只同步下拉框选项，让用户看到当前排序依据。
            If 排序字段.Items.Count > 1 Then
                排序字段.SelectedIndex = 1
            End If

            ' ---- 4.4 定位到刚添加的新记录 ----
            ' 说明：使用保存的 strNewRecordId 匹配，而非 管理编号.Text
            '       （后者在 FillDataSetAndView 后已被覆盖为第一条记录的值）。
            Dim intNewRow As Integer = -1
            For i As Integer = 0 To objDataView.Count - 1
                If objDataView(i)("管理编号").ToString().Trim() = strNewRecordId Then
                    intNewRow = i
                    Exit For
                End If
            Next

            ' ---- 4.5 同步 CurrencyManager 位置与 Grid 显示 ----
            If intNewRow >= 0 Then
                objCurrencyManager.Position = intNewRow
                ShowPosition()

                RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
                grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intNewRow).Cells(0)
                AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged

                ' ---- 让新记录显示在屏幕中部（往上偏移 10 行） ----
                ' 【原因】FirstDisplayedScrollingRowIndex = intNewRow 会让目标行在底部，
                '         因为它保证"底部有数据填充"（WinForms 固有限制）。
                '         往上偏移 10 行可让目标行显示在屏幕中偏上，视觉上更明显。
                ' 【边界处理】若目标行在前 10 行内，则顶部设为 0。
                Dim intScrollTarget As Integer = intNewRow - 10
                If intScrollTarget < 0 Then intScrollTarget = 0
                grdAuthorTitles.FirstDisplayedScrollingRowIndex = intScrollTarget
            Else
                ' 定位失败（理论上不会发生），至少刷新位置标签
                ShowPosition()
            End If

            ToolStripLabel1.Text = "Record Added"   ' 状态栏提示

            ' ---- 4.6 锁定金额类字段（防误改） ----
            重量.Enabled = False
            损失成本.Enabled = False
            材料费用.Enabled = False
            加工费用.Enabled = False

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：确保连接关闭并提示用户 ★★★
            ' ============================================================
            If objConnection1th.State = ConnectionState.Open Then objConnection1th.Close()
            MessageBox.Show("添加失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine(String.Format("添加_Click 异常: {0}", ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' 功能：将当前编辑的控件值通过参数化 UPDATE 语句写回 Access 数据库，
    '''       成功后重新加载数据，并尽力保持用户的排序选择与当前记录位置。
    '''       涉及对象：objConnection1th、OleDbCommand、objDataView、objCurrencyManager。
    ''' </summary>
    ''' <remarks>
    ''' 【关键机制】
    '''   - 使用参数化 SQL（OleDbParameter），避免注入与类型转换错误。
    '''   - WHERE 条件用"管理编号"（主键），确保只更新当前记录。
    ''' 【历史踩坑】
    '''   1. 日期/金额/布尔字段必须显式指定 DbType，否则 Access 可能类型不匹配。
    '''   2. 原代码在更新后重置 排序字段.SelectedIndex = 0，会打乱用户选择，
    '''      且 执行查询_Click 会把 Position 重置为 0，导致定位失效。
    '''      现改为：保留排序选择，只刷新数据，定位回原记录。
    ''' </remarks>
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        ' ============================================================
        ' ★★★ 第1步：保存当前记录位置与排序选择 ★★★
        ' ============================================================
        ' 原因：更新后需要定位回原记录，且不应改变用户的排序选择。
        Dim intPosition As Integer = objCurrencyManager.Position
        Dim intSortIndex As Integer = 排序字段.SelectedIndex
        ' 保存当前记录的"管理编号"，用于更新后定位
        Dim strCurrentId As String = BindingContext(objDataView).Current("管理编号").ToString()

        ' ============================================================
        ' ★★★ 第2步：构建 UPDATE 语句并执行 ★★★
        ' ============================================================
        Try
            Dim objCommand As New OleDbCommand()
            objCommand.Connection = objConnection1th
            objCommand.CommandType = CommandType.Text

            ' ---- 构建 UPDATE 语句（参数化） ----
            objCommand.CommandText = "UPDATE 不良品信息 " &
            "SET 发生日期 = @发生日期, 客户 = @客户, 供应商 = @供应商, 产品规格 = @产品规格, " &
            "加工设备 = @加工设备, 发现过程 = @发现过程, 不良类型 = @不良类型, 操作者 = @操作者, " &
            "类型区分 = @类型区分, 不良数量 = @不良数量, 完成工序 = @完成工序, " &
            "加工费用 = @加工费用, 材料费用 = @材料费用, 损失成本 = @损失成本, " &
            "不良现象及原因 = @不良现象及原因, 备注 = @备注, 重量 = @重量, " &
            "处置完成 = @处置完成, 因素确定 = @因素确定, 图片路径 = @图片路径 " &
            "WHERE 管理编号 = @管理编号"

            ' ---- 添加参数（顺序可任意，名称必须与 SQL 一致） ----
            objCommand.Parameters.AddWithValue("@发生日期", 发生日期.Text).DbType = DbType.Date
            objCommand.Parameters.AddWithValue("@客户", 客户.Text)
            objCommand.Parameters.AddWithValue("@供应商", 供应商.Text)
            objCommand.Parameters.AddWithValue("@产品规格", 产品规格.Text)
            objCommand.Parameters.AddWithValue("@加工设备", 加工设备.Text)
            objCommand.Parameters.AddWithValue("@发现过程", 发现过程.Text)
            objCommand.Parameters.AddWithValue("@不良类型", 不良类型.Text)
            objCommand.Parameters.AddWithValue("@操作者", 操作者.Text)
            objCommand.Parameters.AddWithValue("@类型区分", 类型区分.Text)
            objCommand.Parameters.AddWithValue("@不良数量", 不良数量.Text)
            objCommand.Parameters.AddWithValue("@完成工序", 完成工序.Text)
            objCommand.Parameters.AddWithValue("@加工费用", 加工费用.Text).DbType = DbType.Single
            objCommand.Parameters.AddWithValue("@材料费用", 材料费用.Text).DbType = DbType.Single
            objCommand.Parameters.AddWithValue("@损失成本", 损失成本.Text).DbType = DbType.Single
            objCommand.Parameters.AddWithValue("@不良现象及原因", 不良现象及原因.Text)
            objCommand.Parameters.AddWithValue("@备注", 备注.Text)
            objCommand.Parameters.AddWithValue("@重量", 重量.Text).DbType = DbType.Single
            objCommand.Parameters.AddWithValue("@处置完成", 处置完成.Checked).DbType = DbType.Boolean
            objCommand.Parameters.AddWithValue("@因素确定", 因素确定.Text)
            objCommand.Parameters.AddWithValue("@图片路径", 图片路径.Text)
            ' WHERE 条件：用保存的 strCurrentId，避免用户改了"管理编号"后找不到原记录
            objCommand.Parameters.AddWithValue("@管理编号", strCurrentId)

            ' ---- 执行更新 ----
            objConnection1th.Open()
            objCommand.ExecuteNonQuery()
            objConnection1th.Close()

            ' ============================================================
            ' ★★★ 第3步：刷新数据并定位回原记录 ★★★
            ' ============================================================
            F01_不良品基本信息_Load(Nothing, Nothing)
            ' ---- 恢复用户原排序 ----
            ' 【设计意图】更新是局部操作，不应打断用户的排序选择。
            '             用户可能正在按"客户"排序排查问题，更新后不应跳回默认排序。
            If intLastSortIndex >= 0 AndAlso 排序字段.Items.Count > intLastSortIndex Then
                排序字段.SelectedIndex = intLastSortIndex
                ' 按用户选择的字段重新排序
                Select Case intLastSortIndex
                    Case 0 : objDataView.Sort = "管理编号"
                    Case 1 : objDataView.Sort = "发生日期 DESC"
                    Case 2 : objDataView.Sort = "客户"
                    Case 3 : objDataView.Sort = "供应商"
                    Case 4 : objDataView.Sort = "产品规格"
                    Case 5 : objDataView.Sort = "加工设备"
                    Case 6 : objDataView.Sort = "发现过程"
                    Case 7 : objDataView.Sort = "不良类型"
                    Case 8 : objDataView.Sort = "操作者"
                    Case 9 : objDataView.Sort = "类型区分"
                    Case 10 : objDataView.Sort = "不良数量"
                    Case 11 : objDataView.Sort = "完成工序"
                    Case 12 : objDataView.Sort = "加工费用"
                    Case 13 : objDataView.Sort = "材料费用"
                    Case 14 : objDataView.Sort = "损失成本"
                    Case 15 : objDataView.Sort = "不良现象及原因"
                    Case 16 : objDataView.Sort = "备注"
                    Case 17 : objDataView.Sort = "重量"
                    Case 18 : objDataView.Sort = "处置完成"
                    Case 19 : objDataView.Sort = "因素确定"
                End Select
            End If



            ' ---- 按管理编号重新定位到原记录 ----
            Dim intNewRow As Integer = -1
            For i As Integer = 0 To objDataView.Count - 1
                If objDataView(i)("管理编号").ToString() = strCurrentId Then
                    intNewRow = i
                    Exit For
                End If
            Next

            If intNewRow >= 0 Then
                objCurrencyManager.Position = intNewRow
                ShowPosition()
                RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
                grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intNewRow).Cells(0)
                AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
                grdAuthorTitles.FirstDisplayedScrollingRowIndex = intNewRow
            Else
                ShowPosition()
            End If

            ToolStripLabel1.Text = "Record Updated"

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：确保连接关闭并提示 ★★★
            ' ============================================================
            If objConnection1th.State = ConnectionState.Open Then objConnection1th.Close()
            MessageBox.Show("更新失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine(String.Format("更新_Click 异常: {0}", ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' 功能：删除当前记录（按"管理编号"字段匹配），删除前弹窗二次确认。
    '''       删除成功后重新加载数据，并将位置修正到前一条记录（或首条）。
    '''       涉及对象：objConnection1th、OleDbCommand、objDataView、objCurrencyManager。
    ''' </summary>
    ''' <remarks>
    ''' 【关键机制】
    '''   - 用"管理编号"字段作为 WHERE 条件（假定该字段唯一）。
    '''   - 删除后 Position 修正为 intPosition，若删的是最后一条则回退到新末条。
    ''' 【历史踩坑】
    '''   1. 原代码没有二次确认，误点即删，风险高（本次已加确认弹窗）。
    '''   2. 原代码删除后直接定位 intPosition，但若删的是末条，会越界访问 Rows(-1)。
    '''      本次加了边界判断。
    '''   3. 原代码删除后也调用 F01_Load 重新加载，但排序选择被重置——本次保留。
    ''' </remarks>
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        ' ============================================================
        ' ★★★ 第1步：二次确认（防误删） ★★★
        ' ============================================================
        ' 原因：删除不可撤销，必须让用户明确确认。
        Dim strCurrentId As String = BindingContext(objDataView).Current("管理编号").ToString()
        If MessageBox.Show(
        String.Format("确定删除管理编号为 [{0}] 的记录吗？此操作不可撤销。", strCurrentId),
        "删除确认",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Exit Sub
        End If

        ' ============================================================
        ' ★★★ 第2步：保存删除前的位置（用于删除后定位） ★★★
        ' ============================================================
        ' 说明：删除后当前记录消失，定位到前一条（Position - 1）更符合直觉。
        Dim intPosition As Integer = objCurrencyManager.Position - 1
        If intPosition < 0 Then intPosition = 0

        ' ============================================================
        ' ★★★ 第3步：构建 DELETE 语句并执行 ★★★
        ' ============================================================
        Try
            Dim objCommand As New OleDbCommand()
            objCommand.Connection = objConnection1th
            objCommand.CommandText = "DELETE FROM 不良品信息 WHERE 管理编号 = @管理编号"
            objCommand.Parameters.AddWithValue("@管理编号", strCurrentId)

            objConnection1th.Open()
            objCommand.ExecuteNonQuery()
            objConnection1th.Close()

            ' ============================================================
            ' ★★★ 第4步：刷新数据并修正位置 ★★★
            ' ============================================================
            F01_不良品基本信息_Load(Nothing, Nothing)
            ' ---- 恢复用户原排序 ----
            ' 【设计意图】更新是局部操作，不应打断用户的排序选择。
            '             用户可能正在按"客户"排序排查问题，更新后不应跳回默认排序。
            If intLastSortIndex >= 0 AndAlso 排序字段.Items.Count > intLastSortIndex Then
                排序字段.SelectedIndex = intLastSortIndex
                ' 按用户选择的字段重新排序
                Select Case intLastSortIndex
                    Case 0 : objDataView.Sort = "管理编号"
                    Case 1 : objDataView.Sort = "发生日期 DESC"
                    Case 2 : objDataView.Sort = "客户"
                    Case 3 : objDataView.Sort = "供应商"
                    Case 4 : objDataView.Sort = "产品规格"
                    Case 5 : objDataView.Sort = "加工设备"
                    Case 6 : objDataView.Sort = "发现过程"
                    Case 7 : objDataView.Sort = "不良类型"
                    Case 8 : objDataView.Sort = "操作者"
                    Case 9 : objDataView.Sort = "类型区分"
                    Case 10 : objDataView.Sort = "不良数量"
                    Case 11 : objDataView.Sort = "完成工序"
                    Case 12 : objDataView.Sort = "加工费用"
                    Case 13 : objDataView.Sort = "材料费用"
                    Case 14 : objDataView.Sort = "损失成本"
                    Case 15 : objDataView.Sort = "不良现象及原因"
                    Case 16 : objDataView.Sort = "备注"
                    Case 17 : objDataView.Sort = "重量"
                    Case 18 : objDataView.Sort = "处置完成"
                    Case 19 : objDataView.Sort = "因素确定"
                End Select
            End If



            ' ---- 边界处理：如果删除后数据为空，提示并清空控件 ----
            If objCurrencyManager.Count = 0 Then
                ' 清空 GroupBox1 内的所有控件，避免显示已删除记录的残留值
                For i As Byte = 0 To UBound(myArray)
                    Dim ctrl As Control = GroupBox1.Controls(myArray(i).ToString())
                    If TypeOf ctrl Is CheckBox Then
                        CType(ctrl, CheckBox).Checked = False
                    Else
                        ctrl.Text = ""
                    End If
                Next

                ' 弹出明确提示
                MessageBox.Show("已删除最后一条记录，当前数据为空。", "提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                ToolStripLabel1.Text = "Record Deleted (无剩余记录)"
                Exit Sub
            End If

            ' ---- 位置修正：若删的是末条，intPosition 可能越界 ----
            If intPosition >= objCurrencyManager.Count Then
                intPosition = objCurrencyManager.Count - 1
            End If

            objCurrencyManager.Position = intPosition
            ShowPosition()
            RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
            grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)
            AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged
            grdAuthorTitles.FirstDisplayedScrollingRowIndex = intPosition

            ToolStripLabel1.Text = "Record Deleted"

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：确保连接关闭并提示 ★★★
            ' ============================================================
            If objConnection1th.State = ConnectionState.Open Then objConnection1th.Close()
            MessageBox.Show("删除失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine(String.Format("删除_Click 异常: {0}", ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' 功能：用户点击 DataGridView 某一行时，同步更新"当前记录位置"标签。
    '''       不需要手动绑定字段（DataBindings 已在 Load 时建立，切换行会自动刷新）。
    '''       涉及对象：grdAuthorTitles、objCurrencyManager、txtRecordPosition。
    ''' </summary>
    ''' <remarks>
    ''' 【历史踩坑】
    '''   原代码在此事件中调用 BindFields()，每次点击 Grid 都会重绑 21 个控件，
    '''   3000 行滚动时导致严重卡顿（本次已优化）。
    ''' 【机制说明】
    '''   - DataGridView 的 CurrentRow 变化会自动同步 CurrencyManager.Position。
    '''   - 因此无需再手动赋值 Position，只需刷新位置标签即可。
    ''' </remarks>


    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        ' 【性能优化】临时挂起布局，减少控件刷新次数
        ' 原理：DataBindings 会依次刷新 21 个控件，每个控件都触发一次布局计算，
        '       挂起后所有变更一次性提交，减少重绘次数。
        Me.SuspendLayout()
        Try
            ShowPosition()
        Finally
            Me.ResumeLayout()
        End Try
    End Sub

    'Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
    '    ' 用户点击 Grid 行 → CurrencyManager.Position 自动同步 → 只需刷新标签
    '    ShowPosition()
    'End Sub


    '退出
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btn不良品信息.Enabled = True
        Me.Close()
    End Sub

    '关闭
    Private Sub D01_资质证书信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing         '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btn不良品信息.Enabled = True
    End Sub

    Private Sub 产品规格_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 产品规格.SelectedIndexChanged
        On Error Resume Next
        objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
        objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
        objDataAdapter1th.SelectCommand.CommandText = "select 物品价格, 重量 " & " from " & "物品信息 WHERE (产品规格='" & 产品规格.Text & "'" & " AND 区分 ='" & 类型区分.Text & "' AND 供应商 ='" & 供应商.Text & "')" '写入SQL语句
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx02")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb As DataTable = objDataSet1th.Tables("wpxx02") '声明一个表类型,并赋值给该变量.
        '产品规格.Items.Clear()                               '清楚复合框项目集
        'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
        材料费用.Text = tb.Rows(0).Item(0).ToString   '添加项目值为记录字段所对应的值
        重量.Text = tb.Rows(0).Item(1).ToString   '添加项目值为记录字段所对应的值

        'objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
        'objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
        objDataAdapter1th.SelectCommand.CommandText = "select 赔偿比例.* " & " from " & "赔偿比例 WHERE " & "(发现过程=" & "'" & 发现过程.Text & "')"
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx05")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb001 As DataTable = objDataSet1th.Tables("wpxx05") '声明一个表类型,并赋值给该变量.
        '产品规格.Items.Clear()                               '清楚复合框项目集
        'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
        加工费用.Text = CType((CType(tb001.Rows(0).Item(1).ToString, Single) * CType(材料费用.Text, Single) * CType(不良数量.Text, Integer)), String)  '添加项目值为记录字段所对应的值

        损失成本.Text = (CType(加工费用.Text, Single) + CType(材料费用.Text, Single) * CType(不良数量.Text, Integer)).ToString()
        'Next

    End Sub

    Private Sub 类型区分_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 类型区分.SelectedIndexChanged
        产品规格_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub 发现过程_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 发现过程.SelectedIndexChanged
        产品规格_SelectedIndexChanged(Nothing, Nothing)
        完成工序.Text = Split(发现过程.Text, "（")(0)

    End Sub

    Private Sub 不良类型_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 不良类型.SelectedIndexChanged
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "不良类型分类.*" & " from " & "不良类型分类 WHERE 不良类型 = " & "'" & 不良类型.Text & "'"  '写入SQL语句
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx15")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb3 As DataTable = objDataSet1th.Tables("wpxx15") '声明一个表类型,并赋值给该变量.
        因素确定.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
        '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
        '不良类型.Items.Add("客户发现不良")
        For inCounter = 0 To tb3.Rows.Count - 1               '在表行数上循环
            因素确定.Items.Add(tb3.Rows(inCounter).Item(2).ToString)   '添加项目值为记录字段所对应的值
        Next
        If 不良类型.Text = "客户发现不良" Then
            'Label6.Visible = False
            '供应商.Visible = False
            'Label3.Visible = False
            '产品规格.Visible = False
            'Label12.Visible = False
            '类型区分.Visible = False
            'Label11.Visible = False
            '操作者.Visible = False


            'Label20.Visible = False
            '重量.Visible = False
            重量.Enabled = True
            重量.Text = "0"
            'Label17.Visible = False
            '损失成本.Visible = False
            损失成本.Enabled = True
            损失成本.Text = "0"
            'Label16.Visible = False
            '材料费用.Visible = False
            材料费用.Enabled = True
            材料费用.Text = "0"
            'Label15.Visible = False
            '加工费用.Visible = False
            加工费用.Enabled = True
            加工费用.Text = "0"
        Else
            'Label6.Visible = True
            '供应商.Visible = True
            'Label3.Visible = True
            '产品规格.Visible = True
            'Label12.Visible = True
            '类型区分.Visible = True
            'Label11.Visible = True
            '操作者.Visible = True

            'Label20.Visible = True
            '重量.Visible = True
            'Label17.Visible = True
            '损失成本.Visible = True
            'Label16.Visible = True
            '材料费用.Visible = True
            'Label15.Visible = True
            '加工费用.Visible = True
            损失成本.Enabled = False
            材料费用.Enabled = False
            加工费用.Enabled = False
        End If


    End Sub

    Private Sub 供应商_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 供应商.SelectedIndexChanged
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 WHERE 供应商 = " & "'" & 供应商.Text & "'"  '写入SQL语句
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx2019042701")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb2019042701 As DataTable = objDataSet1th.Tables("wpxx2019042701") '声明一个表类型,并赋值给该变量.
        产品规格.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
        '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
        '不良类型.Items.Add("客户发现不良")
        For inCounter = 0 To tb2019042701.Rows.Count - 1               '在表行数上循环
            产品规格.Items.Add(tb2019042701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next
    End Sub


    Private Sub 发生日期_GotFocus(sender As Object, e As EventArgs) Handles 发生日期.GotFocus
        发生日期.Mask = "0000/00/00"
    End Sub


    Private Sub 发生日期_LostFocus(sender As Object, e As EventArgs) Handles 发生日期.LostFocus
        Dim strDate As String
        strDate = 发生日期.Text
        发生日期.Mask = ""
        发生日期.Text = strDate
    End Sub

    Private Sub 发生日期_TextChanged(sender As Object, e As EventArgs) Handles 发生日期.TextChanged
        Dim strStrogeValue
        If Len(发生日期.Text) = 10 Then strStrogeValue = 发生日期.Text : 发生日期_LostFocus(Nothing, Nothing)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnReseting.Click
        查询条件.Text = ""
    End Sub



    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        '声明一个变体型变量(在VB.net中已经不能再称之为变体型变量，而是Object.
        Dim objFileArray As Object, arrFileArrayResetting() As String
        objFileArray = xlapp.GetOpenFilename("所有文件(*.*）,*.*", , , , True) '弹出一个选择文件的对话框,并设置可以多选.
        Dim strRecordPath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\1 Pictures\", bytPosition As Byte, bytAfterNamePosition As Byte, strFileName As String


        'IsArray函数判定是否是数组,如果用户选择了文件(此时变量objFilearr是数组,如果没有选择文件则返回值不是数组)
        If IsArray(objFileArray) Then
            '重置数组维数,这里减1表示,上一数组下标是从1开始的.以下语句还可以改成:
            'Dim arr(objFileArray.LongLength - 1) As Object   '声明一个下标为0,上标为文件数量-1的数组变量
            ReDim arrFileArrayResetting(UBound(objFileArray) - 1)
            '被复制的下数组标为1,数组拷贝到目标数组,起始放置点为0,即目标数组下标处开始存放被复制的数组元素.
            objFileArray.CopyTo(arrFileArrayResetting, 0)

            图片路径.Text = arrFileArrayResetting(0)
            bytPosition = InStr(1, StrReverse(图片路径.Text), "\")   '计算文件名称前面的“\”的位置
            bytAfterNamePosition = InStr(1, StrReverse(图片路径.Text), ".")   '计算文件名称前面的“\”的位置
            strFileName = Mid(图片路径.Text, Len(图片路径.Text) - bytPosition + 2)

            If My.Computer.FileSystem.FileExists(strRecordPath & strFileName) = True Then Kill(strRecordPath & strFileName) '如果存在指定的文件夹,那么执行  Kill()
            System.IO.File.Copy(图片路径.Text, strRecordPath & strFileName)

            图片路径.Text = strRecordPath & strFileName






            '将选择的所有文件名称导入到列表框中,并去除文件名称,在指定的文本框中显示文件路径.
            '打开路径.Items.AddRange(arrFileArrayResetting)
            'Replace(objFileArray(1), Dir(objFileArray(1)), "")
            '打开路径.Text = arrFileArrayResetting(0)
            '打开路径.Text = Replace(arrFileArrayResetting(0), "D:", "\\192.168.3.250")
            '图片路径.Text = arrFileArrayResetting(0)
        Else
            Exit Sub  '结束过程
        End If
    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        'On Error Resume Next
        'If InStr(图片路径.Text, "xls") > 0 Then     '如果是包含有xls后缀存储名
        '    xlapp.Workbooks.Open(图片路径.Text)     '打开EXCEL
        'Else                                        '否则
        '    'Shell("winword.exe " & 存放位置.Text, vbMaximizedFocus)   'shell函数打开word文档程序    
        '    Shell("explorer.exe " & 图片路径.Text, vbMaximizedFocus)   '打开所有资源程序
        'End If                  '结束语句
        'If Err.Number <> 0 Then MsgBox("打开对应文件失败",, "提示")
        PictureBox1.Image = Image.FromFile(图片路径.Text)
    End Sub


    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        On Error Resume Next
        If InStr(图片路径.Text, "xls") > 0 Then     '如果是包含有xls后缀存储名
            xlapp.Workbooks.Open(图片路径.Text)     '打开EXCEL
        Else                                        '否则
            'Shell("winword.exe " & 存放位置.Text, vbMaximizedFocus)   'shell函数打开word文档程序    
            Shell("explorer.exe " & 图片路径.Text, vbMaximizedFocus)   '打开所有资源程序
        End If                  '结束语句
        If Err.Number <> 0 Then MsgBox("打开对应文件失败",, "提示")
        PictureBox1.Image = Nothing
    End Sub

    Private Sub 图片路径_TextChanged(sender As Object, e As EventArgs) Handles 图片路径.TextChanged

        txtPathEqual.Text = 图片路径.Text

    End Sub

    Private Sub txtPathEqual_TextChanged(sender As Object, e As EventArgs) Handles txtPathEqual.TextChanged

        'PauseWait(1500)
        If Len(图片路径.Text) > 0 Then

            btnOpenFile_Click(Nothing, Nothing)
        Else
            PictureBox1.Image = Nothing
            Exit Sub
        End If
    End Sub


    Public Sub PauseWait(ByVal HowLong As Long)
        Dim tick As Long
        tick = My.Computer.Clock.TickCount
        Do
            Application.DoEvents()
        Loop Until tick + HowLong < My.Computer.Clock.TickCount
    End Sub
    '参考宝3 P375,调整2个控件的大小
    Private Sub F01_不良品基本信息_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        On Error Resume Next
        Dim b As Long
        If e.Button = MouseButtons.Left Then
            With GroupBox4
                b = .Width
                If e.X > Me.Width - 40 Or e.X < 40 Then Exit Sub
                GroupBox4.Left = GroupBox4.Left
                GroupBox4.Width = e.X - GroupBox4.Left
                GroupBox5.Left = e.X + GroupBox5.Left - (GroupBox4.Left + b)
                GroupBox5.Width = GroupBox5.Width - (.Width - b)
                'GroupBox4.Size
            End With
        End If


    End Sub


















    'Private Sub 发生日期_TextChanged(sender As Object, e As EventArgs) Handles 发生日期.TextChanged
    '    Dim strDate As String = ""
    '    Select Case 
    '    发生日期.Text = strDate
    'End Sub

    'Private Sub 材料费用_TextChanged(sender As Object, e As EventArgs) Handles 材料费用.TextChanged
    '    On Error Resume Next
    '    objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
    '    objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
    '    objDataAdapter1th.SelectCommand.CommandText = "select 赔偿比例.* " & " from " & "赔偿比例 WHERE " & "(发现过程=" & "'" & 发现过程.Text & "')"
    '    objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx05")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb As DataTable = objDataSet1th.Tables("wpxx05") '声明一个表类型,并赋值给该变量.
    '    '产品规格.Items.Clear()                               '清楚复合框项目集
    '    'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
    '    加工费用.Text = CType((CType(tb.Rows(0).Item(1).ToString, Single) * CType(材料费用.Text, Single)), String)  '添加项目值为记录字段所对应的值

    '    损失成本.Text = CType(（CType(加工费用.Text, Single) + CType(材料费用.Text, Single)）, Single)
    'End Sub










    '    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    '    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    '    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    '    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\不良品信息管理.accdb"
    '    'Dim strYiFangPath As String = "\\192.168.3.52\Users\进销存管理.accdb"
    '    Dim strMyHomerComputerPath As String = "E:\access\不良品信息管理.accdb"
    '    Dim strMyCompanyComputerPath As String = "D:\6 总务\access\不良品信息管理.accdb"
    '    Dim objConnection1th As New OleDbConnection _
    '               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)


    '    '  Dim objConnection1th As New OleDbConnection _
    '    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\不良品信息管理.accdb")  '公司共享盘


    '    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\不良品信息管理\不良品信息管理.accdb")  '三星笔记本
    '    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\不良品信息管理\不良品信息管理.accdb")  '家里台式机
    '    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\不良品信息管理.accdb")  '公司共享盘
    '    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '    '这个构造函数使我们不必写Adapter属性SelectCommand相关代码.已经加入相关参数(SQL语句)
    '    'Dim objDataAdapter As New OleDbDataAdapter("SELECT 不良品信息.* FROM 不良品信息 ORDER BY 发生日期", objConnection1th)
    '    Dim strGetData As String = "SELECT 不良品信息.* FROM 不良品信息 ORDER BY 发生日期"
    '    Dim objDataAdapter As OleDbDataAdapter



    '    Dim objDataAdapter1th As New OleDbDataAdapter()  '该构造函数需要使用SelectCommand属性.用来填充履历卡数据的
    '    Dim objDataSet As New DataSet()     '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    '    Dim objDataSet1th As New DataSet()  '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    '    Dim objDataView As DataView         '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    '    Dim objDataView1th As DataView      '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    '    Dim objCurrencyManager As CurrencyManager   '声明作用域为类级的对象,CurrencyManger对象用于控制绑定数据的移动;作为管理Binding对象的列表
    '    Dim myArray() As String                       '声明数组变量,数组长度为要引用的数据表字段数量.

    '    '创建一个过程,将在Load事件(初始化代码)调用,并用来填充数据和显示数据.
    '    Private Sub FillDataSetAndView()
    '        objDataSet = New DataSet()  '调用模块级对象,并重新初始化该(DataSet)对象
    '        '向DataSet对象填充由Sql(Ole)DataAdapter对象SelectCommand属性从数据库检索到的数据.. 
    '        '注意:Fill方法使用选择命令SelectCommand.Connection.如果该链接已打开,就会自动打开填充数据后保持打开连接对象,反之则反.  
    '        objDataAdapter.Fill(objDataSet, "bl")  '表(bl)是初始构建起来的,命名为bl.
    '        objDataView = New DataView(objDataSet.Tables("bl"))   '初始化并构建DataView对象.
    '        'CurrencyManager(窗体获取到的数据记录集合)对象包含于BindingContect集合(内置于Win窗体,无须创建)中,
    '        '将DataView对象转化为CurrencyManager对象.
    '        objCurrencyManager = CType(Me.BindingContext(objDataView), CurrencyManager)
    '    End Sub

    '    '创建一个过程,逐一将窗体中的控件属性和指定数据源创建Binding,并将其添加到集合中.
    '    Private Sub BindFields()
    '        On Error Resume Next
    '        Dim i As Byte = 0
    '        '控件获取到的数据绑定(DataBindings属性),逐一清除(Clear方法)控件上的绑定(控件可能之前绑定过旧的DataView数据源) 
    '        myArray = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分", "不良数量",
    '            "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因", "备注", "重量", "处置完成"， "因素确定"}
    '        For i = 0 To UBound(myArray)
    '            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
    '        Next i
    '        '控件重新逐一绑定DateView数据源,add方法第一参数为要绑定的控件属性的名称,第二参数为要绑定的数据源,
    '        '第三参数为要绑定给控件的数据字段(列表).
    '        For i = 0 To UBound(myArray)
    '            If GroupBox1.Controls(myArray(i).ToString).Name <> "处置完成" Then

    '                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
    '            Else
    '                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Checked", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
    '            End If

    '            'GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
    '            If GroupBox1.Controls(myArray(i).ToString).Name = "发生日期" Then GroupBox1.Controls(myArray(i).ToString).Text _
    '                = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
    '        Next i
    '        ToolStripLabel1.Text = "Ready"  '显示一个"只读"状态..
    '    End Sub

    '    '创建过程,并显示当前单个记录的位置.
    '    Private Sub ShowPosition()
    '        Try  '格式化日期指定短日期格式.
    '            发生日期.Text = Format(CType(GroupBox1.Controls("发生日期").Text, Date), "yyyy/MM/dd") '定义格式
    '        Catch e As System.Exception   '声明一个错误变量类型
    '            '如果异常(文本框为空),那么转换当前日期类型为文本类型,并写入文本框中.
    '            GroupBox1.Controls("发生日期").Text = CType(Now, String)
    '            发生日期.Text = Format(CType(GroupBox1.Controls("发生日期").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
    '        End Try
    '        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    '    " of " & objCurrencyManager.Count() '显示当前记录位置,并标记记录数. 
    '    End Sub

    '    '按钮单击事件,移动第一条记录
    '    Private Sub btnMoveFirst_Click(Sender As Object,
    '            E As EventArgs) Handles btnMoveFirst.Click
    '        Dim intPosition As Integer
    '        objCurrencyManager.Position = 0  '设置当前记录为第一条记录.
    '        intPosition = objCurrencyManager.Position   '记录位置赋值给变量
    '        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件关联
    '        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
    '        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
    '        '控件与数据源(objDataView)绑定,通过CurrencyManager对象指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
    '        ShowPosition()
    '        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 

    '    End Sub

    '    '按钮单击事件,移动上一条记录
    '    Private Sub btnMovePrevious_Click(Sender As Object,
    '            E As EventArgs) Handles btnMovePrevious.Click
    '        Dim intPosition As Integer
    '        objCurrencyManager.Position -= 1 'Move to the previous record..
    '        intPosition = objCurrencyManager.Position  '记录位置赋值给变量
    '        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged  '解除事件.
    '        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格.
    '        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件.
    '        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
    '        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 

    '    End Sub

    '    '按钮单击事件,移动下一条记录.
    '    Private Sub btnMoveNext_Click(Sender As Object,
    '            E As EventArgs) Handles btnMoveNext.Click
    '        Dim intPosition As Integer
    '        '移动下一条记录. 
    '        objCurrencyManager.Position += 1 'Move to the next record..
    '        intPosition = objCurrencyManager.Position  '记录位置赋值给变量
    '        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
    '        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
    '        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
    '        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
    '        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 

    '    End Sub

    '    '按钮单击事件,移动最后一条记录
    '    Private Sub btnMoveLast_Click(Sender As Object,
    '            E As EventArgs) Handles btnMoveLast.Click
    '        Dim intPosition As Integer
    '        '移动最后一条记录,不需要调用重新绑定过程,自动同步的,只要不更新,就不存在数据源集的变更 
    '        objCurrencyManager.Position = objCurrencyManager.Count - 1 ' Set the record position to the last record..
    '        intPosition = objCurrencyManager.Position   '记录位置赋值给变量
    '        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
    '        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
    '        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件
    '        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
    '        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
    '    End Sub


    '    '加载窗体触发事件
    '    Private Sub F01_不良品基本信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '        '需要说明的是,Fill方法会执行命令(SelectCommand),其Connection属性保持为调用该方法时的状态.
    '        'On Error Resume Next
    '        objDataAdapter = New OleDbDataAdapter(strGetData, objConnection1th)



    '        FillDataSetAndView() '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定数据源字段到指定控件.
    '        ShowPosition()  '调用ShowPosition方法,并显示当前记录标签位置    
    '        'BindFields()  '调用绑定控件过程,因为有复合框,所以放在事件最后面.
    '        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.
    '        grdAuthorTitles.DataSource = objDataSet '设置DataSet对象,作为gird控件的数据来源(实际上就是一个绑定过程,告知控件从哪里获得数据).
    '        grdAuthorTitles.DataMember = "bl"  '设置gird控件要显示的数据源(具体的表名称).
    '        '将对齐方式格式改为垂直居中向右对齐.
    '        Dim objAlignRightCellStyle As New DataGridViewCellStyle  '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例) 
    '        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        Dim objAlternatingCellStyle As New DataGridViewCellStyle() '初始化DataGridViewCellStyle对象(grd控件单元格样式实例) 作为交叉行样式  
    '        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置交叉样式背景色为烟灰色
    '        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
    '        Dim objCurrencyCellStyle As New DataGridViewCellStyle()  '初始化DataGridViewCellStyle对象,将设置单元格格式为货币型.
    '        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式改为居中向左对齐
    '        objCurrencyCellStyle.Format = "¥#,##0.00" '样式格式为货币型(美元或者人民币$¥)
    '        'objCurrencyCellStyle.Format = "C"  '样式格式为货币型(人民币)
    '        grdAuthorTitles.Columns(0).HeaderText = "管理编号"   '设置控件列标题   
    '        'grdAuthorTitles.Columns(1).HeaderText = "发生日期"
    '        grdAuthorTitles.Columns(1).HeaderText = "录入日期"
    '        grdAuthorTitles.Columns(2).HeaderText = "客户"
    '        grdAuthorTitles.Columns(3).HeaderText = "供应商"
    '        grdAuthorTitles.Columns(4).HeaderText = "产品规格"
    '        grdAuthorTitles.Columns(5).HeaderText = "加工设备"
    '        grdAuthorTitles.Columns(6).HeaderText = "发现过程"
    '        grdAuthorTitles.Columns(7).HeaderText = "不良类型"
    '        grdAuthorTitles.Columns(8).HeaderText = "操作者"
    '        grdAuthorTitles.Columns(9).HeaderText = "类型区分"
    '        grdAuthorTitles.Columns(10).HeaderText = "不良数量"
    '        grdAuthorTitles.Columns(11).HeaderText = "完成工序"
    '        grdAuthorTitles.Columns(12).HeaderText = "加工费用"
    '        grdAuthorTitles.Columns(13).HeaderText = "材料费用"
    '        grdAuthorTitles.Columns(14).HeaderText = "损失成本"
    '        grdAuthorTitles.Columns(15).HeaderText = "不良现象及原因"
    '        grdAuthorTitles.Columns(15).Width = 130 '设置指定列默认宽度大一点
    '        grdAuthorTitles.Columns(16).HeaderText = "备注"
    '        grdAuthorTitles.Columns(17).HeaderText = "重量"
    '        grdAuthorTitles.Columns(18).HeaderText = "处置完成"
    '        grdAuthorTitles.Columns(18).Width = 60 '设置指定列默认宽度大一点
    '        grdAuthorTitles.Columns(19).HeaderText = "因素确定"

    '        '自动调整列宽.
    '        'grdAuthorTitles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells


    '        ''改变字段标题名称和样式'Change column names and styles using the column name  
    '        grdAuthorTitles.Columns("加工费用").HeaderCell.Value = "加工费用_内" '重新设置列标题的值显示为"描述"
    '        '标题重新调用列标题样式(之前设定的-居中右对齐)
    '        grdAuthorTitles.Columns("加工费用").HeaderCell.Style = objAlignRightCellStyle
    '        '单元格内容重新调用样式(之前设定的-货币样式)
    '        grdAuthorTitles.Columns("加工费用").DefaultCellStyle = objCurrencyCellStyle
    '        grdAuthorTitles.Columns("材料费用").HeaderCell.Style = objAlignRightCellStyle
    '        '单元格内容重新调用样式(之前设定的-货币样式)
    '        grdAuthorTitles.Columns("材料费用").DefaultCellStyle = objCurrencyCellStyle
    '        grdAuthorTitles.Columns("损失成本").HeaderCell.Style = objAlignRightCellStyle
    '        '单元格内容重新调用样式(之前设定的-货币样式)
    '        grdAuthorTitles.Columns("损失成本").DefaultCellStyle = objCurrencyCellStyle

    '        ''遍历记录数量
    '        'For i As Integer = 0 To grdAuthorTitles.RowCount - 1  '有一个空白行也算一行
    '        '    If Math.Ceiling(CType(grdAuthorTitles.Item(7, i).Value.ToString(), Date).Subtract(Now).TotalDays) <= 20 Then
    '        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
    '        '    Else
    '        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
    '        '    End If
    '        'Next

    '        For i As Integer = 0 To grdAuthorTitles.RowCount - 2                           '有一个空白行也算一行
    '            If CType(grdAuthorTitles.Item(18, i).Value.ToString(), Boolean) Then
    '                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色

    '            Else
    '                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
    '                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
    '            End If
    '        Next

    '        objCurrencyCellStyle = Nothing     '清除样式对象(单元格记录内容用)
    '        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
    '        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
    '        排序字段.Items.Clear()   '给组合框添加项目  'Add items to the combo box..
    '        排序字段.Items.AddRange(myArray)
    '        排序字段.SelectedIndex = 0         '默认选择第一项
    '        客户.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '        '添加项目
    '        客户.Items.Add("日本日立") ： 客户.Items.Add("德國久保田") ： 客户.Items.Add("日本久保田") ： 客户.Items.Add("常州现代") ： 客户.Items.Add("GE") ： 客户.Items.Add("印度日立") ： 客户.Items.Add("发注至总公司")
    '        客户.Items.Add("苏州斗山") ： 客户.Items.Add("VOLVO")
    '        供应商.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '        供应商.Items.Add("荣程A") ： 供应商.Items.Add("新顺章B") ： 供应商.Items.Add("海陆C") ： 供应商.Items.Add("利元D") ： 供应商.Items.Add("广源E") ： 供应商.Items.Add("恒杰F")

    '        类型区分.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '        类型区分.Items.Add("I/N") ： 类型区分.Items.Add("O/T")
    '        产品规格_SelectedIndexChanged(Nothing, Nothing)
    '        '维修类型.SelectedIndex = 0  '默认选择第一项




    '        'objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
    '        'objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
    '        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 ORDER BY 产品规格" '写入SQL语句

    '        ''objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 ORDER BY 物品编号" '写入SQL语句
    '        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
    '        'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '        'objDataAdapter1th.Fill(objDataSet1th, "wpxx01")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '        'Dim tb As DataTable = objDataSet1th.Tables("wpxx01") '声明一个表类型,并赋值给该变量.
    '        '产品规格.Items.Clear()                               '清楚复合框项目集
    '        'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
    '        '    产品规格.Items.Add(tb.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '        'Next
    '        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
    '        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "赔偿比例.*" & " from " & "赔偿比例 ORDER BY 比例" '写入SQL语句
    '        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '        objDataAdapter1th.Fill(objDataSet1th, "wpxx04")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '        Dim tb1 As DataTable = objDataSet1th.Tables("wpxx04") '声明一个表类型,并赋值给该变量.
    '        发现过程.Items.Clear()                               '清楚复合框项目集
    '        For inCounter = 0 To tb1.Rows.Count - 1               '在表行数上循环
    '            发现过程.Items.Add(tb1.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '        Next
    '        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
    '        objDataAdapter1th.SelectCommand.CommandText = "select distinct 不良类型" & " from " & "不良类型分类" '写入SQL语句
    '        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '        objDataAdapter1th.Fill(objDataSet1th, "wpxx14")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '        Dim tb2 As DataTable = objDataSet1th.Tables("wpxx14") '声明一个表类型,并赋值给该变量.
    '        不良类型.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '        '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
    '        '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
    '        '不良类型.Items.Add("客户发现不良")
    '        For inCounter = 0 To tb2.Rows.Count - 1               '在表行数上循环
    '            不良类型.Items.Add(tb2.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '        Next
    '        BindFields()  '调用绑定控件过程
    '    End Sub

    '    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件数据源进行排序.
    '    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
    '        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性.
    '        Select Case 排序字段.SelectedIndex      'Determine the appropriate item selected and set the Sort property of the DataView object..            
    '            ' {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因", "备注"}
    '            Case 0
    '                objDataView.Sort = "管理编号"   '按字段设备编号升序排序,下同.
    '            Case 1
    '                objDataView.Sort = "发生日期"
    '            Case 2
    '                objDataView.Sort = "客户"
    '            Case 3
    '                objDataView.Sort = "供应商"
    '            Case 4
    '                objDataView.Sort = "产品规格"
    '            Case 5
    '                objDataView.Sort = "加工设备"
    '            Case 6
    '                objDataView.Sort = "发现过程"
    '            Case 7
    '                objDataView.Sort = "不良类型"
    '            Case 8
    '                objDataView.Sort = "操作者"
    '            Case 9
    '                objDataView.Sort = "类型区分"
    '            Case 10
    '                objDataView.Sort = "不良数量"
    '            Case 11
    '                objDataView.Sort = "完成工序"
    '            Case 12
    '                objDataView.Sort = "加工费用"
    '            Case 13
    '                objDataView.Sort = "材料费用"
    '            Case 14
    '                objDataView.Sort = "损失成本"
    '            Case 15
    '                objDataView.Sort = "不良现象及原因"
    '            Case 16
    '                objDataView.Sort = "备注"
    '            Case 17
    '                objDataView.Sort = "重量"
    '            Case 18
    '                objDataView.Sort = "处置完成"
    '            Case 19
    '                objDataView.Sort = "因素确定"

    '        End Select
    '        btnMoveFirst_Click(Nothing, Nothing)      '调用单击首条记录按钮  Call the click event for the MoveFirst button..
    '        ToolStripLabel1.Text = "Records Sorted"   '修改状态标签Text属性. Display a message that the records have been sorted..
    '    End Sub

    '    '创建查询方法
    '    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
    '        ' myArray = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", 
    '        '"不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
    '        Dim intPosition As Integer              '执行查找,声明当前局部变量.'Declare local variables.. 
    '        Dim str条件 As String = ""
    '        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
    '        'Determine the appropriate item selected And set the Sort property of the DataView object..
    '        Select Case 排序字段.SelectedIndex
    '              '"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"
    '            Case 0
    '                objDataView.Sort = "管理编号"
    '                str条件 = "管理编号"
    '            Case 1
    '                objDataView.Sort = "发生日期"
    '                str条件 = "发生日期"

    '            Case 2
    '                objDataView.Sort = "客户"
    '                str条件 = "客户"
    '            Case 3
    '                objDataView.Sort = "供应商"
    '                str条件 = "供应商"
    '            Case 4
    '                objDataView.Sort = "产品规格"
    '                str条件 = "产品规格"
    '            Case 5
    '                objDataView.Sort = "加工设备"
    '                str条件 = "加工设备"
    '            Case 6
    '                objDataView.Sort = "发现过程"
    '                str条件 = "发现过程"
    '                '"不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
    '            Case 7
    '                objDataView.Sort = "不良类型"
    '                str条件 = "不良类型"
    '            Case 8
    '                objDataView.Sort = "操作者"
    '                str条件 = "操作者"
    '            Case 9
    '                objDataView.Sort = "类型区分"
    '                str条件 = "类型区分"
    '            Case 10
    '                objDataView.Sort = "不良数量"
    '                str条件 = "不良数量"
    '            Case 11
    '                objDataView.Sort = "完成工序"
    '                str条件 = "完成工序"
    '            Case 12
    '                objDataView.Sort = "加工费用"
    '                str条件 = "加工费用"
    '            Case 13
    '                objDataView.Sort = "材料费用"
    '                str条件 = "材料费用"
    '            Case 14
    '                objDataView.Sort = "损失成本"
    '                str条件 = "损失成本"
    '            Case 15
    '                objDataView.Sort = "不良现象及原因"
    '                str条件 = "不良现象及原因"
    '            Case 16
    '                objDataView.Sort = "备注"
    '                str条件 = "备注"
    '            Case 17
    '                objDataView.Sort = "重量"
    '                str条件 = "重量"
    '            Case 18
    '                objDataView.Sort = "处置完成"
    '                str条件 = "处置完成"
    '            Case 19
    '                objDataView.Sort = "因素确定"
    '                str条件 = "因素确定"
    '        End Select
    '        If str条件 = "发生日期" Then
    '            objDataView.RowFilter = str条件 & "=#" & CType(查询条件.Text, Date).ToShortDateString & "#" '"Date = #12/31/2008 16:44:58#"
    '        ElseIf str条件 <> "处置完成" Then    'DataView数据表中筛选数据集(类似SQL语句).
    '            objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"
    '        Else
    '            objDataView.RowFilter = str条件 & "=" & CType(查询条件.Text, Boolean)
    '        End If


    '        '重要更新 20191208 指定数据提取,VB.net SQL语句 like模糊只能用%,access用*.
    '        If str条件 = "管理编号" Then
    '            strGetData = "SELECT 不良品信息.* FROM 不良品信息 ORDER BY 发生日期"
    '            F01_不良品基本信息_Load(Nothing, Nothing)
    '            strGetData = "SELECT 不良品信息.* FROM 不良品信息 WHERE 管理编号 LIKE '%" & 查询条件.Text & "%' ORDER BY 发生日期"
    '            'strGetData = "SELECT 不良品信息.* FROM 不良品信息 WHERE 管理编号 = '" & 查询条件.Text & "' ORDER BY 发生日期"
    '        End If
    '        F01_不良品基本信息_Load(Nothing, Nothing)
    '        ShowPosition() '重新显示当前记录位置. Show the current record position..7E4B503
    '        intPosition = objCurrencyManager.Position  '默认位置赋值给变量
    '        If intPosition = -1 Then  '状态栏提示没有找到记录 Display a message that the record was not found..
    '            ToolStripLabel1.Text = "Record Not Found"  '标签显示字符.
    '            '否则状态栏显示字符..
    '        Else
    '            ToolStripLabel1.Text = "Record Found"
    '        End If
    '    End Sub

    '    '查询条件变化事件
    '    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
    '        If 查询条件.Text.Length = 0 Then  '如果是空值
    '            '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置..
    '            strGetData = "SELECT 不良品信息.* FROM 不良品信息 ORDER BY 发生日期"
    '            F01_不良品基本信息_Load(Nothing, Nothing)
    '        End If
    '    End Sub

    '    '按下Enter执行查询
    '    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
    '        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    '    End Sub

    '    '新建按钮事件
    '    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
    '        Dim i As Byte = 0             '声明局部变量
    '        myArray = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分",
    '            "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因", "备注", "重量", "处置完成"}
    '        For i = 0 To UBound(myArray)  '清空简单控件值
    '            GroupBox1.Controls(myArray(i).ToString).Text = ""
    '        Next i
    '        GroupBox1.Controls(myArray(10).ToString).Text = 1
    '        产品规格.SelectedIndex = 0  '默认选择第一项
    '        类型区分.SelectedIndex = 0  '默认选择第一项
    '        发现过程.SelectedIndex = 0
    '        '管理编号.Enabled = False      '设置禁止使用控件
    '    End Sub

    '    '添加按钮事件
    '    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
    '        'Dim intMaxID As Integer     '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字'Declare local variables and objects..  
    '        'Dim strID As String = ""    '变量用来存储authors表的主键并设置authors表的新键
    '        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个新的查询.

    '        查询条件_TextChanged(Nothing, Nothing)

    '        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..
    '        '从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
    '        '存贮当前记录位置给变量 Save the current record position..
    '        'Dim maxIdCommand As OleDbCommand = New OleDbCommand _
    '        '("Select TOP 1 * FROM 不良品信息 ORDER BY 序列号 DESC", objConnection1th)
    '        'objConnection1th.Open()   '打开数据库连接 Open the connection, execute the command SELECT TOP 1 * FROM 表名 ORDER BY 排序字段 DESC
    '        'Dim maxId As Object = maxIdCommand.ExecuteScalar()  '调用SqlCommand的一个执行方法(只返回一行一列).并把结果赋值给变量
    '        'If maxId Is DBNull.Value Then                       '如果返回结果是空值那么执行    If the MaxID column is null..
    '        '    intMaxID = 1000                                 '设置一个默认值1000.Set a default value of 1000..
    '        'Else
    '        '    strID = CType(maxId, String)                    '否则执行将maxId换成String型.strId.otherwise set the strID variable to the value in MaxID..
    '        '    intMaxID = CType(strID.Remove(0, 2), Integer)   '利用Remove方法删除sb前缀,转换整型赋值给变量intMaxID.Get the integer part of the string..
    '        '    intMaxID += 1                                   '变量加1.Increment the value..
    '        'End If
    '        '变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..'strID = "SB" & intMaxID.ToString
    '        ''变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..
    '        'Select Case Len(intMaxID.ToString)
    '        '    Case 1
    '        '        strID = "XL00" & intMaxID.ToString
    '        '    Case 2
    '        '        strID = "XL0" & intMaxID.ToString
    '        '    Case Else
    '        '        strID = "XL" & intMaxID.ToString
    '        'End Select

    '        objCommand.Connection = objConnection1th '设置命令对象的属性 Set the SqlCommand object properties..'将连接字符串的连接对象赋值给Connection属性
    '        objConnection1th.Open()

    '        排序字段.SelectedIndex = 0
    '        查询条件.Text = 管理编号.Text

    '        'myArray = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
    '        'objCommand.CommandText = "INSERT INTO 不良品信息 " &
    '        '"(管理编号, 发生日期, 客户, 供应商, 产品规格, 加工设备, 发现过程, 不良类型, 操作者, 类型区分, 不良数量, 完成工序, 加工费用, 材料费用, 损失成本, 不良现象及原因) " &
    '        '"VALUES(@管理编号, @发生日期, @客户, @供应商, @产品规格, @加工设备, @发现过程, @不良类型, @操作者, @类型区分, @不良数量, @完成工序, @加工费用, @材料费用, @损失成本, @不良现象及原因)"
    '        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列..'AddWithValue方法接受参数名和要添加的对象 
    '        'Add parameters For the placeholders In the SQL In the 'CommandText property..Parameter for the title_id column..
    '        objCommand.CommandText = "INSERT INTO 不良品信息 " &
    '        "(管理编号, 发生日期, 客户, 供应商, 产品规格, 加工设备, 发现过程, 不良类型, 操作者, 类型区分, 不良数量, 完成工序, 加工费用, 材料费用, 损失成本, 不良现象及原因, 备注, 重量, 处置完成, 因素确定) " &
    '        "VALUES(@管理编号, @发生日期, @客户, @供应商, @产品规格, @加工设备, @发现过程, @不良类型, @操作者, @类型区分, @不良数量, @完成工序, @加工费用, @材料费用, @损失成本, @不良现象及原因, @备注, @重量, @处置完成, @因素确定)"

    '        objCommand.Parameters.AddWithValue("@管理编号", 管理编号.Text)          '指定参数写入值,下同.
    '        objCommand.Parameters.AddWithValue("@发生日期", 发生日期.Text).DbType = DbType.Date
    '        objCommand.Parameters.AddWithValue("@客户", 客户.Text)
    '        objCommand.Parameters.AddWithValue("@供应商", 供应商.Text) '转换日期类型
    '        objCommand.Parameters.AddWithValue("@产品规格", 产品规格.Text)
    '        objCommand.Parameters.AddWithValue("@加工设备", 加工设备.Text)
    '        objCommand.Parameters.AddWithValue("@发现过程", 发现过程.Text)
    '        objCommand.Parameters.AddWithValue("@不良类型", 不良类型.Text)
    '        objCommand.Parameters.AddWithValue("@操作者", 操作者.Text)
    '        objCommand.Parameters.AddWithValue("@类型区分", 类型区分.Text)
    '        objCommand.Parameters.AddWithValue("@不良数量", 不良数量.Text).DbType = DbType.Single
    '        objCommand.Parameters.AddWithValue("@完成工序", 完成工序.Text)
    '        objCommand.Parameters.AddWithValue("@加工费用", 加工费用.Text).DbType = DbType.Single
    '        objCommand.Parameters.AddWithValue("@材料费用", 材料费用.Text).DbType = DbType.Single
    '        objCommand.Parameters.AddWithValue("@损失成本", 损失成本.Text).DbType = DbType.Single
    '        objCommand.Parameters.AddWithValue("@不良现象及原因", 不良现象及原因.Text)
    '        objCommand.Parameters.AddWithValue("@备注", 备注.Text)
    '        objCommand.Parameters.AddWithValue("@重量", 重量.Text).DbType = DbType.Single
    '        objCommand.Parameters.AddWithValue("@处置完成", 处置完成.Checked).DbType = DbType.Boolean '试试可不可以删
    '        objCommand.Parameters.AddWithValue("@因素确定", 因素确定.Text) '试试可不可以删
    '        'For i = 0 To UBound(myArray)
    '        '    If myArray(i).ToString <> "维修单号" Then   '如果名称只要不是维修单号,那么要执行.
    '        '        If GroupBox1.Controls(myArray(i).ToString).Text.Length = 0 Then MsgBox("请输入完整数据在添加数据") : _
    '        '            新建_Click(Nothing, Nothing) : objConnection1th.Close() : Exit Sub
    '        '    End If
    '        'Next i
    '        Try                               '截取异常'执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..
    '            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
    '        Catch SqlExceptionErr As OleDbException         '声明异常类型
    '            MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示异常类型错误信息
    '        End Try                                         '结束截取
    '        objConnection1th.Close()                        '关闭数据库连接 Close the connection..
    '        F01_不良品基本信息_Load(Nothing, Nothing)         '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
    '        objCurrencyManager.Position = objCurrencyManager.Count - 1   '设置你保存的那个记录位置    Set the record position to the one that you saved..
    '        ShowPosition()                                               '标签显示位置.
    '        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
    '        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
    '        执行查询_Click(Nothing, Nothing)
    '        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
    '        ToolStripLabel1.Text = "Record Added"    '状态栏显示你添加的信息   Display a message that the record was added..
    '    End Sub

    '    '更新数据库
    '    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
    '        '声明一个局部变量和创建一个命令对象  Declare local variables and objects..
    '        Dim intPosition As Integer
    '        Dim objCommand As OleDbCommand = New OleDbCommand()
    '        intPosition = objCurrencyManager.Position  '当前记录位置赋值给变量intPosstion. Save the current record position..
    '        objCommand.Connection = objConnection1th '设置命令对象一些属性 Set the SqlCommand object properties..
    '        排序字段.SelectedIndex = 0
    '        查询条件.Text = 管理编号.Text
    '        'SQL语句表示按照指定条件,更新表字段..
    '        'myArray = {"管理编号", "发生日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
    '        ' '接着使用SQL字符串设置CommandText属性.
    '        objCommand.CommandText = "UPDATE 不良品信息 " &
    '            "Set 发生日期 = @发生日期,客户 = @客户,供应商 = @供应商,产品规格 = @产品规格,加工设备 = @加工设备,
    '发现过程 = @发现过程,不良类型 = @不良类型,操作者 = @操作者,类型区分 = @类型区分,不良数量 = @不良数量,完成工序 = @完成工序,
    '加工费用 = @加工费用,材料费用 = @材料费用,损失成本 = @损失成本,不良现象及原因 = @不良现象及原因,备注 = @备注,重量 = @重量,处置完成 = @处置完成,因素确定 = @因素确定 WHERE 管理编号 = @管理编号"
    '        objCommand.CommandType = CommandType.Text '命令类型为默认CommandType.Text类型,可以省略
    '        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
    '        ' Add parameters for the placeholders in the SQL in the
    '        ' CommandText property..
    '        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
    '        objCommand.Parameters.AddWithValue("@发生日期", 发生日期.Text).DbType = DbType.Date  '转换类型.
    '        objCommand.Parameters.AddWithValue("@客户", 客户.Text)
    '        objCommand.Parameters.AddWithValue("@供应商", 供应商.Text)
    '        objCommand.Parameters.AddWithValue("@产品规格", 产品规格.Text)
    '        objCommand.Parameters.AddWithValue("@加工设备", 加工设备.Text)
    '        objCommand.Parameters.AddWithValue("@发现过程", 发现过程.Text)
    '        objCommand.Parameters.AddWithValue("@不良类型", 不良类型.Text)
    '        objCommand.Parameters.AddWithValue("@操作者", 操作者.Text)
    '        objCommand.Parameters.AddWithValue("@类型区分", 类型区分.Text)
    '        objCommand.Parameters.AddWithValue("@不良数量", 不良数量.Text)
    '        objCommand.Parameters.AddWithValue("@完成工序", 完成工序.Text)
    '        objCommand.Parameters.AddWithValue("@加工费用", 加工费用.Text).DbType = DbType.Single  '转换类型.
    '        objCommand.Parameters.AddWithValue("@材料费用", 材料费用.Text).DbType = DbType.Single  '转换类型.
    '        objCommand.Parameters.AddWithValue("@损失成本", 损失成本.Text).DbType = DbType.Single  '转换类型.
    '        objCommand.Parameters.AddWithValue("@不良现象及原因", 不良现象及原因.Text)
    '        objCommand.Parameters.AddWithValue("@备注", 备注.Text)
    '        objCommand.Parameters.AddWithValue("@重量", 重量.Text).DbType = DbType.Single  '转换类型.
    '        objCommand.Parameters.AddWithValue("@处置完成", 处置完成.Checked).DbType = DbType.Boolean  '转换类型.
    '        objCommand.Parameters.AddWithValue("@因素确定", 因素确定.Text)
    '        objCommand.Parameters.AddWithValue _
    '            ("@管理编号", BindingContext(objDataView).Current("管理编号"))

    '        objConnection1th.Open()    '打开带连接字符的数据库连接  Open the connection..
    '        objCommand.ExecuteNonQuery()   '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
    '        objConnection1th.Close()    '关闭数据库连接  Close the connection..
    '        F01_不良品基本信息_Load(Nothing, Nothing) '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
    '        objCurrencyManager.Position = intPosition   ' 设置你保存过的记录位置 Set the record position to the one that you saved..
    '        ShowPosition() '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
    '        '显示状态信息  Display a message that the record was updated..
    '        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
    '        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
    '        执行查询_Click(Nothing, Nothing)
    '        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
    '        ToolStripLabel1.Text = "Record Updated"
    '    End Sub

    '    '删除记录
    '    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
    '        '定义一个局部变量和命令对象 Declare local variables and objects..
    '        Dim intPosition As Integer
    '        Dim objCommand As OleDbCommand = New OleDbCommand()
    '        '保存当前记录位置-1以用来记录删除位置.  Save the current record position—1 for the one to be
    '        ' deleted..
    '        intPosition = Me.BindingContext(objDataView).Position - 1
    '        If intPosition < 0 Then  '如果没有记录,则设置记录位置为0.    If the position is less than 0 set it to 0..
    '            intPosition = 0
    '        End If
    '        objCommand.Connection = objConnection1th      '设置命令对象属性 Set the Command object properties..
    '        objCommand.CommandText = "DELETE FROM 不良品信息 " &
    '            "WHERE 管理编号 = @管理编号"
    '        '给title_id字段提供相应的参数  Parameter for the title_id field..
    '        objCommand.Parameters.AddWithValue _
    '        ("@管理编号", BindingContext(objDataView).Current("管理编号"))
    '        objConnection1th.Open()     '打开数据库连接 Open the database connection..
    '        objCommand.ExecuteNonQuery()     '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
    '        objConnection1th.Close()         '关闭数据库连接 Close the connection..
    '        '填充数据并绑定字段 Fill the DataSet and bind the fields..
    '        'FillDataSetAndView()
    '        'BindFields()
    '        '注意:这里注释上面2句过程主要是为了调用Adapata
    '        F01_不良品基本信息_Load(Nothing, Nothing)
    '        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
    '        Me.BindingContext(objDataView).Position = intPosition
    '        ShowPosition()  '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
    '        '显示一个已删除的信息.  Display a message that the record was deleted..
    '        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
    '        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
    '        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
    '        ToolStripLabel1.Text = "Record Deleted"
    '    End Sub

    '    '获取项目值模板
    '    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
    '        'On Error Resume Next
    '        Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index
    '        BindFields()
    '        objCurrencyManager.Position = intPosition
    '        ShowPosition()
    '    End Sub

    '    '退出
    '    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
    '        '清理内存及数据适配器对象
    '        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
    '        objConnection1th = Nothing            '清理连接对象,释放内存
    '        Globals.Ribbons.Ribbon1.btn不良品信息.Enabled = True
    '        Me.Close()
    '    End Sub

    '    '关闭
    '    Private Sub D01_资质证书信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
    '        '清理内存及数据适配器对象
    '        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
    '        objConnection1th = Nothing         '清理连接对象,释放内存
    '        Globals.Ribbons.Ribbon1.btn不良品信息.Enabled = True
    '    End Sub

    '    Private Sub 产品规格_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 产品规格.SelectedIndexChanged
    '        On Error Resume Next
    '        objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
    '        objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
    '        objDataAdapter1th.SelectCommand.CommandText = "select 物品价格, 重量 " & " from " & "物品信息 WHERE (产品规格='" & 产品规格.Text & "'" & " AND 区分 ='" & 类型区分.Text & "')" '写入SQL语句
    '        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
    '        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '        objDataAdapter1th.Fill(objDataSet1th, "wpxx02")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '        Dim tb As DataTable = objDataSet1th.Tables("wpxx02") '声明一个表类型,并赋值给该变量.
    '        '产品规格.Items.Clear()                               '清楚复合框项目集
    '        'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
    '        材料费用.Text = tb.Rows(0).Item(0).ToString   '添加项目值为记录字段所对应的值
    '        重量.Text = tb.Rows(0).Item(1).ToString   '添加项目值为记录字段所对应的值

    '        'objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
    '        'objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
    '        objDataAdapter1th.SelectCommand.CommandText = "select 赔偿比例.* " & " from " & "赔偿比例 WHERE " & "(发现过程=" & "'" & 发现过程.Text & "')"
    '        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
    '        'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '        objDataAdapter1th.Fill(objDataSet1th, "wpxx05")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '        Dim tb001 As DataTable = objDataSet1th.Tables("wpxx05") '声明一个表类型,并赋值给该变量.
    '        '产品规格.Items.Clear()                               '清楚复合框项目集
    '        'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
    '        加工费用.Text = CType((CType(tb001.Rows(0).Item(1).ToString, Single) * CType(材料费用.Text, Single)), String)  '添加项目值为记录字段所对应的值

    '        损失成本.Text = CType(（CType(加工费用.Text, Single) + CType(材料费用.Text, Single)）, Single)
    '        'Next

    '    End Sub

    '    Private Sub 类型区分_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 类型区分.SelectedIndexChanged
    '        产品规格_SelectedIndexChanged(Nothing, Nothing)
    '    End Sub

    '    Private Sub 发现过程_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 发现过程.SelectedIndexChanged
    '        产品规格_SelectedIndexChanged(Nothing, Nothing)
    '        完成工序.Text = Split(发现过程.Text, "（")(0)

    '    End Sub

    '    Private Sub 不良类型_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 不良类型.SelectedIndexChanged
    '        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
    '        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "不良类型分类.*" & " from " & "不良类型分类 WHERE 不良类型 = " & "'" & 不良类型.Text & "'"  '写入SQL语句
    '        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '        objDataAdapter1th.Fill(objDataSet1th, "wpxx15")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '        Dim tb3 As DataTable = objDataSet1th.Tables("wpxx15") '声明一个表类型,并赋值给该变量.
    '        因素确定.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '        '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
    '        '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
    '        '不良类型.Items.Add("客户发现不良")
    '        For inCounter = 0 To tb3.Rows.Count - 1               '在表行数上循环
    '            因素确定.Items.Add(tb3.Rows(inCounter).Item(2).ToString)   '添加项目值为记录字段所对应的值
    '        Next
    '    End Sub

    '    Private Sub 供应商_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 供应商.SelectedIndexChanged
    '        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
    '        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 WHERE 供应商 = " & "'" & 供应商.Text & "'"  '写入SQL语句
    '        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '        objDataAdapter1th.Fill(objDataSet1th, "wpxx2019042701")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '        Dim tb2019042701 As DataTable = objDataSet1th.Tables("wpxx2019042701") '声明一个表类型,并赋值给该变量.
    '        产品规格.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '        '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
    '        '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
    '        '不良类型.Items.Add("客户发现不良")
    '        For inCounter = 0 To tb2019042701.Rows.Count - 1               '在表行数上循环
    '            产品规格.Items.Add(tb2019042701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '        Next
    '    End Sub


    '    Private Sub 发生日期_GotFocus(sender As Object, e As EventArgs) Handles 发生日期.GotFocus
    '        发生日期.Mask = "0000/00/00"
    '    End Sub


    '    Private Sub 发生日期_LostFocus(sender As Object, e As EventArgs) Handles 发生日期.LostFocus
    '        Dim strDate As String
    '        strDate = 发生日期.Text
    '        发生日期.Mask = ""
    '        发生日期.Text = strDate
    '    End Sub

    '    Private Sub 发生日期_TextChanged(sender As Object, e As EventArgs) Handles 发生日期.TextChanged
    '        Dim strStrogeValue
    '        If Len(发生日期.Text) = 10 Then strStrogeValue = 发生日期.Text : 发生日期_LostFocus(Nothing, Nothing)
    '    End Sub

    '    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnReseting.Click
    '        查询条件.Text = ""
    '    End Sub



    '    'Private Sub 发生日期_TextChanged(sender As Object, e As EventArgs) Handles 发生日期.TextChanged
    '    '    Dim strDate As String = ""
    '    '    Select Case 
    '    '    发生日期.Text = strDate
    '    'End Sub

    '    'Private Sub 材料费用_TextChanged(sender As Object, e As EventArgs) Handles 材料费用.TextChanged
    '    '    On Error Resume Next
    '    '    objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
    '    '    objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
    '    '    objDataAdapter1th.SelectCommand.CommandText = "select 赔偿比例.* " & " from " & "赔偿比例 WHERE " & "(发现过程=" & "'" & 发现过程.Text & "')"
    '    '    objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
    '    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx05")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    '    Dim tb As DataTable = objDataSet1th.Tables("wpxx05") '声明一个表类型,并赋值给该变量.
    '    '    '产品规格.Items.Clear()                               '清楚复合框项目集
    '    '    'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
    '    '    加工费用.Text = CType((CType(tb.Rows(0).Item(1).ToString, Single) * CType(材料费用.Text, Single)), String)  '添加项目值为记录字段所对应的值

    '    '    损失成本.Text = CType(（CType(加工费用.Text, Single) + CType(材料费用.Text, Single)）, Single)
    '    'End Sub









End Class