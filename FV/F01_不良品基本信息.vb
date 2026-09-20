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
'   3. objDataAdapter1th 的连接在 联动查询_SelectedIndexChanged 中初始化，
'      依赖 Load 中提前调用 联动查询_SelectedIndexChanged(Nothing, Nothing)。
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

    ' ============================================================
    ' ★★★ 硬编码列表（集中管理，便于维护） ★★★
    ' ============================================================
    ' 【设计意图】把下拉框选项集中到类顶部定义，避免散落在 Load 里。
    '             未来修改列表只需改这里，不影响业务逻辑代码。
    ' 【TODO】后续可考虑迁移到配置文件或数据库表，实现"用户可自行维护"。

    ' ---- 客户列表 ----
    Private ReadOnly str客户列表() As String = {
        "日本日立", "德国久保田", "日本久保田", "常州现代", "GE", "印度日立",
        "发注至总公司", "苏州斗山山猫", "烟台斗山", "VOLVO", "远景能源"
    }



    ' ---- 类型区分列表 ----
    Private ReadOnly str类型区分列表() As String = {
        "I/N", "O/T", "Assembly"
    }



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
    '''   1. 绑定前先清除所有控件的旧绑定，避免残留（防止字段错位或显示异常）。
    '''   2. 针对 CheckBox 单独处理 Checked 属性，而非 Text。
    '''   3. 日期字段通过 Binding.Format 事件统一格式化为 yyyy/MM/dd，
    '''      避免显示"2018/03/23 0:00:00"（本次优化）。
    ''' </remarks>
    Private Sub BindFields()
        ' ============================================================
        ' ★★★ 第1步：清除所有控件的旧绑定（防止累积） ★★★
        ' ============================================================
        ' 说明：遍历 GroupBox1 中的所有控件，清除其 DataBindings 集合。
        '       如果不清除，在多次调用 BindFields 时（如刷新数据），
        '       旧绑定会与新绑定叠加，导致显示混乱或报错。
        For i As Byte = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString()).DataBindings.Clear()
        Next i

        ' ============================================================
        ' ★★★ 第2步：重新绑定数据字段到控件 ★★★
        ' ============================================================
        ' 说明：DataBindings.Add(属性名, 数据源, 字段名)
        '       对于普通控件（文本框等），绑定 Text 属性。
        '       对于 CheckBox，绑定 Checked 属性（布尔值）。
        '       对于"发生日期"，额外挂 Format 事件统一格式化。
        For i As Byte = 0 To UBound(myArray)
            Dim strControlName As String = myArray(i).ToString()
            Dim ctrl As Control = GroupBox1.Controls(strControlName)

            If TypeOf ctrl Is CheckBox Then
                ' ---- CheckBox：绑定 Checked 属性 ----
                ctrl.DataBindings.Add("Checked", objDataView, strControlName)

            ElseIf strControlName = "发生日期" Then
                ' ---- 发生日期：绑定 Text + 挂 Format 事件 ----
                ' 【原因】MaskedTextBox + DataBindings 默认会把日期显示为
                '         "2018/03/23 0:00:00"，加 Format 事件可在每次显示时统一格式化。
                Dim objDateBinding As Binding = ctrl.DataBindings.Add("Text", objDataView, strControlName)
                AddHandler objDateBinding.Format, AddressOf 发生日期_Binding_Format

            Else
                ' ---- 其他普通控件：绑定 Text 属性 ----
                ctrl.DataBindings.Add("Text", objDataView, strControlName)
            End If
        Next i

        ' ============================================================
        ' ★★★ 第3步：更新状态栏提示 ★★★
        ' ============================================================
        ToolStripLabel1.Text = "Ready"
    End Sub

    ''' <summary>
    ''' 功能：为"发生日期"的数据绑定提供格式化回调。
    '''       每次从数据源读取值显示到控件时，都会调用本方法，
    '''       将日期值格式化为 yyyy/MM/dd，避免显示"2018/03/23 0:00:00"。
    '''       涉及对象：发生日期（MaskedTextBox）、Binding。
    ''' </summary>
    ''' <remarks>
    ''' 【机制说明】
    '''   - Binding.Format 事件在"数据源 → 控件"方向触发（显示时）。
    '''   - e.Value 是数据源的值，可以修改后返回，控件显示的就是修改后的值。
    ''' 【历史踩坑】
    '''   不加 Format 事件时，Access 的日期字段（含时间部分 0:00:00）会原样显示，
    '''   用户看到 "2018/03/23 0:00:00" 不美观（本次已修复）。
    ''' 【注意】
    '''   - 若 e.Value 为 DBNull，需保留 DBNull，否则会显示异常。
    ''' </remarks>
    Private Sub 发生日期_Binding_Format(sender As Object, e As ConvertEventArgs)
        ' 若值为 DBNull，保持 DBNull（避免转换异常）
        If e.Value Is DBNull.Value OrElse e.Value Is Nothing Then
            e.Value = ""
            Return
        End If

        ' 尝试将值转换为 Date 并按短日期格式回写
        Dim dt As Date
        If Date.TryParse(e.Value.ToString(), dt) Then
            e.Value = Format(dt, "yyyy/MM/dd")
        End If
        ' 若转换失败（如空值、格式错误），保持原值
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
    '''   1. Grid 数据源由 objDataSet 改为 objDataView（RowFilter 才能生效）。
    '''   2. 数据加载通过 FillDataSetAndView() 完成，该方法内部已加异常处理。
    '''   3. 列样式设置必须在 Grid 绑定数据源之后，否则 AutoGenerateColumns 会覆盖样式。
    ''' 【性能优化记录】（已完成，保留说明供参考）
    '''   - SQL 直接 DESC 排序（省 ~1000ms）
    '''   - 数据库查询共用连接（省 ~500ms）
    '''   - 下拉框惰性化（省 ~400ms）
    '''   - 下拉框 BeginUpdate/EndUpdate（省 ~100ms）
    '''   - 总耗时：3063ms → 1125ms（-63%）
    ''' </remarks>
    Private Sub F01_不良品基本信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ============================================================
        ' ★★★ 第1步：加载数据 ★★★
        ' ============================================================
        FillDataSetAndView()

        ' ============================================================
        ' ★★★ 第2步：Grid 绑定 + 双缓冲 ★★★
        ' ============================================================
        ' 开启双缓冲（反射方式，改善 3000 行滚动性能）
        ' 【原理】双缓冲先把内容绘制到内存，再一次性输出到屏幕，避免逐行绘制造成的闪烁和卡顿。
        ' 【说明】DoubleBuffered 是受保护属性，需通过反射访问（WinForms 通用技巧）。
        Dim objProp As System.Reflection.PropertyInfo = GetType(DataGridView).GetProperty(
        "DoubleBuffered",
        System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        If objProp IsNot Nothing Then
            objProp.SetValue(grdAuthorTitles, True, Nothing)
        End If

        grdAuthorTitles.AutoGenerateColumns = True
        grdAuthorTitles.DataSource = objDataView

        ' ============================================================
        ' ★★★ 第3步：绑定字段到 GroupBox1 内控件 ★★★
        ' ============================================================
        ' 【关键】BindFields() 只在此处调用一次，建立持久绑定。
        '         后续切换记录时 CurrencyManager 会自动同步控件，无需重复调用。
        BindFields()

        ' ============================================================
        ' ★★★ 第4步：配置 DataGridView 列标题与单元格样式 ★★★
        ' ============================================================
        ' 说明：所有列样式设置必须在 Grid 绑定数据源之后执行，
        '       否则 AutoGenerateColumns 重新生成列时会覆盖此处设置。

        ' ---- 4.1 定义通用样式对象 ----
        Dim objAlignRightCellStyle As New DataGridViewCellStyle
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle

        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        objCurrencyCellStyle.Format = "¥#,##0.00"

        ' ---- 4.2 设置各列标题 ----
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

        ' ---- 4.3 金额类列设置特殊样式 ----
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

        ' ============================================================
        ' ★★★ 第5步：硬编码下拉框填充 ★★★
        ' ============================================================
        ' 【性能优化】用 BeginUpdate/EndUpdate 包裹批量填充，
        '             避免每次 Add 都触发 ComboBox 重绘。

        ' ---- 排序字段 ----
        排序字段.Items.Clear()
        排序字段.Items.AddRange(myArray)
        排序字段.SelectedIndex = 0

        ' ---- 客户 ----
        客户.BeginUpdate()
        客户.Items.Clear()
        客户.Items.AddRange(str客户列表)
        客户.EndUpdate()

        ' ---- 类型区分 ----
        类型区分.BeginUpdate()
        类型区分.Items.Clear()
        类型区分.Items.AddRange(str类型区分列表)
        类型区分.EndUpdate()

        ' ============================================================
        ' ★★★ 第6步：数据库读取下拉框（发现过程、不良类型） ★★★
        ' ============================================================
        ' 【性能优化】三次查询共用一次连接，减少握手开销。
        objConnection1th.Open()
        Try
            ' ---- 供应商下拉框（从 物品信息 表 DISTINCT 取） ----
            objDataAdapter1th.SelectCommand = New OleDbCommand()
            objDataAdapter1th.SelectCommand.Connection = objConnection1th
            objDataAdapter1th.SelectCommand.CommandText =
            "SELECT DISTINCT 供应商 FROM 物品信息 WHERE 供应商 IS NOT NULL ORDER BY 供应商"
            objDataSet1th = New DataSet()
            objDataAdapter1th.Fill(objDataSet1th, "wpxx供应商")
            Dim tbSupplier As DataTable = objDataSet1th.Tables("wpxx供应商")
            供应商.BeginUpdate()
            供应商.Items.Clear()
            For inCounter = 0 To tbSupplier.Rows.Count - 1
                供应商.Items.Add(tbSupplier.Rows(inCounter).Item(0).ToString())
            Next
            供应商.EndUpdate()


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

        ' ============================================================
        ' ★★★ 第7步：排序下拉框同步 + 位置标签刷新 ★★★
        ' ============================================================
        ' 【性能优化】SQL 已 ORDER BY DESC，无需再 Sort。
        If 排序字段.Items.Count > 1 Then
            排序字段.SelectedIndex = 1
        End If
        ShowPosition()
    End Sub

    ''' <summary>
    ''' 功能：根据"排序字段"下拉框的当前选择，对 DataView 按对应字段排序，
    '''       并定位到排序后的第一条记录。
    '''       涉及对象：排序字段（ComboBox）、objDataView、btnMoveFirst_Click、ToolStripLabel1。
    ''' </summary>
    ''' <remarks>
    ''' 【机制说明】
    '''   - 通过 myArray 的索引取字段名，避免 20 个 Case 的硬编码。
    '''   - myArray 与"排序字段"下拉框的索引一一对应（见 Load 中的 AddRange）。
    '''   - "发生日期"特殊处理为 DESC（降序），与 FillDataSetAndView 的默认排序保持一致。
    ''' 【优化说明】
    '''   原代码用 20 个 Select Case 硬编码字段名，与 执行查询_Click 里的 Case 重复。
    '''   现改为动态取字段，未来新增字段只需改 myArray，无需改本方法。
    ''' 【历史踩坑】
    '''   - 若 intIndex 超出 myArray 范围（下拉框项与数组不同步），会取到错误的字段名。
    '''     故加了边界检查，越界时不执行排序。
    ''' </remarks>
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        ' ============================================================
        ' ★★★ 第1步：校验索引合法性 ★★★
        ' ============================================================
        Dim intIndex As Integer = 排序字段.SelectedIndex
        If intIndex < 0 OrElse intIndex > UBound(myArray) Then
            ' 下拉框未选中或索引越界，不执行排序
            Return
        End If

        ' ============================================================
        ' ★★★ 第2步：按 myArray 索引取字段名，设置 Sort ★★★
        ' ============================================================
        Dim strSortField As String = myArray(intIndex)

        ' "发生日期"特殊处理为 DESC（与 FillDataSetAndView 的默认排序一致）
        If strSortField = "发生日期" Then
            objDataView.Sort = strSortField & " DESC"
        Else
            objDataView.Sort = strSortField
        End If

        ' ============================================================
        ' ★★★ 第3步：定位到排序后的第一条记录 ★★★
        ' ============================================================
        ' 说明：排序后 CurrencyManager.Position 会重置，需重新定位首条并刷新标签。
        btnMoveFirst_Click(Nothing, Nothing)

        ToolStripLabel1.Text = "Records Sorted"
    End Sub

    ''' <summary>
    ''' 功能：根据"排序字段"下拉框的选择，对 DataView 设置 Sort 和 RowFilter，
    '''       实现"排序 + 条件筛选"的联合查询，并更新状态栏提示与当前记录位置。
    '''       涉及对象：排序字段（ComboBox）、查询条件（TextBox）、objDataView、
    '''               objCurrencyManager、ToolStripLabel1、txtRecordPosition。
    ''' </summary>
    ''' <remarks>
    ''' 【三种筛选模式】
    '''   ① 日期字段（发生日期）：用 #日期# 语法精确匹配；
    '''   ② 布尔字段（处置完成）：用 True/False 匹配；
    '''   ③ 文本字段（其他）：用 LIKE '%关键词%' 模糊匹配（UCase 转大写）。
    ''' 【历史踩坑】
    '''   1. 原代码用 20 个 Select Case 硬编码字段名，与 执行排序_Click 重复。
    '''      现改为动态取 myArray 索引，风格统一。
    '''   2. 日期筛选必须用 # 包裹，否则 DataView 会按文本比较，匹配失败。
    '''   3. RowFilter 语法错误会抛异常，需在用户输入不合规时给出提示（TODO）。
    ''' 【注意】
    '''   - 查询后 CurrencyManager.Position 会被重置，需调用 ShowPosition 刷新标签。
    '''   - 若查询结果为空，objCurrencyManager.Position 可能为 -1，状态栏提示"未找到"。
    ''' </remarks>
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        ' ============================================================
        ' ★★★ 第1步：根据下拉框索引动态取字段名 ★★★
        ' ============================================================
        ' 说明：myArray 与"排序字段"下拉框索引一一对应（见 Load 中的 AddRange）。
        Dim intIndex As Integer = 排序字段.SelectedIndex
        If intIndex < 0 OrElse intIndex > UBound(myArray) Then
            ' 下拉框未选中或索引越界，不执行查询
            Return
        End If

        Dim str条件 As String = myArray(intIndex)

        ' ============================================================
        ' ★★★ 第2步：设置 Sort（排序） ★★★
        ' ============================================================
        ' 说明："发生日期"按降序排（与 FillDataSetAndView 默认排序一致），其他按升序。
        If str条件 = "发生日期" Then
            objDataView.Sort = str条件 & " DESC"
        Else
            objDataView.Sort = str条件
        End If

        ' ============================================================
        ' ★★★ 第3步：设置 RowFilter（筛选） ★★★
        ' ============================================================
        ' 说明：根据字段类型选择不同的 RowFilter 语法。
        '       历史踩坑：日期必须用 # 包裹，否则匹配失败。
        Try
            If str条件 = "发生日期" Then
                ' ---- 日期字段：用 #yyyy/MM/dd# 精确匹配 ----
                ' 说明：CType 转换可能因用户输入格式错误而抛异常，需捕获。
                Dim dtSearch As Date
                If Not Date.TryParse(查询条件.Text, dtSearch) Then
                    MessageBox.Show("日期格式不正确，请输入如 2018/3/23 的格式。",
                                "查询条件错误", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                objDataView.RowFilter = str条件 & "=#" & dtSearch.ToShortDateString & "#"

            ElseIf str条件 = "处置完成" Then
                ' ---- 布尔字段：用 True/False 匹配 ----
                Dim bolSearch As Boolean
                If Not Boolean.TryParse(查询条件.Text, bolSearch) Then
                    MessageBox.Show("该字段只能输入 True 或 False。",
                                "查询条件错误", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                objDataView.RowFilter = str条件 & "=" & bolSearch.ToString()

            Else
                ' ---- 文本字段：用 LIKE 模糊匹配（不区分大小写） ----
                ' 说明：UCase 转大写后模糊匹配，用户输入 "abc" 能匹配 "ABC"。
                objDataView.RowFilter = UCase(str条件) & " LIKE '%" & 查询条件.Text & "%'"
            End If

        Catch ex As Exception
            ' ---- RowFilter 语法错误、字段不存在等异常 ----
            MessageBox.Show("查询条件有误：" & ex.Message,
                        "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine(String.Format("执行查询_Click 异常: {0}", ex.ToString()))
            Return
        End Try

        ' ============================================================
        ' ★★★ 第4步：检查查询结果并更新状态栏 ★★★
        ' ============================================================
        ' 说明：Position = -1 表示没有匹配的记录。
        Dim intPosition As Integer = objCurrencyManager.Position
        If intPosition = -1 Then
            ToolStripLabel1.Text = "Record Not Found"
        Else
            ToolStripLabel1.Text = "Record Found"
        End If

        ' ============================================================
        ' ★★★ 第5步：刷新"当前记录位置"标签 ★★★
        ' ============================================================
        ' 说明：RowFilter 变化后 CurrencyManager.Position 可能被重置，需刷新显示。
        ShowPosition()
    End Sub

    ''' <summary>
    ''' 功能：监听"查询条件"文本框的内容变化，当文本框清空时自动重新加载全部数据。
    '''       涉及对象：查询条件（TextBox）、F01_不良品基本信息_Load。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   清空查询条件时自动刷新，符合"清空 = 重置"的用户直觉。
    ''' 【优化说明】
    '''   原代码用"输入 DELETE"启用删除按钮（隐藏开关），交互不直观。
    '''   现改为：删除按钮由 Grid 选中行自动启用/禁用（见 grdAuthorTitles_SelectionChanged）。
    ''' 【历史踩坑】
    '''   F01_不良品基本信息_Load 会重新加载全部数据并重建 DataView，
    '''   只有文本框长度 = 0 时触发，避免频繁 Load。
    ''' </remarks>
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        ' 清空文本框 → 重新加载全部数据
        If 查询条件.Text.Length = 0 Then
            F01_不良品基本信息_Load(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' 功能：在"查询条件"文本框中按下 Enter 键时，触发查询操作。
    '''       等价于点击"查询"按钮，方便用户快速执行查询。
    '''       涉及对象：查询条件（TextBox）、执行查询_Click。
    ''' </summary>
    ''' <remarks>
    ''' 【机制说明】
    '''   - KeyDown 事件在按键按下时触发，KeyCode 为 Keys.Enter 时执行查询。
    '''   - 直接调用 执行查询_Click(Nothing, Nothing)，复用查询逻辑（不重复造轮子）。
    ''' </remarks>
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        ' 按下 Enter 键 → 触发查询（等价于点击"查询"按钮）
        If e.KeyCode = Keys.Enter Then
            执行查询_Click(Nothing, Nothing)
        End If
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
                ' 删除后数据为空 → 禁用删除按钮
                删除.Enabled = False
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

            ' 删除完成后禁用删除按钮（防止用户误操作）
            ' 说明：删除后 CurrencyManager 位置变化，用户需重新选中一行才能再次删除。
            删除.Enabled = False

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
    ''' 功能：用户点击 DataGridView 某一行时，同步更新"当前记录位置"标签，
    '''       并根据是否有选中行自动启用/禁用"删除"按钮。
    '''       涉及对象：grdAuthorTitles、objCurrencyManager、txtRecordPosition、删除。
    ''' </summary>
    ''' <remarks>
    ''' 【历史踩坑】
    '''   原代码在此事件中调用 BindFields()，每次点击 Grid 都会重绑 21 个控件，
    '''   3000 行滚动时导致严重卡顿（已优化）。
    ''' 【优化说明】
    '''   原删除按钮的启用依赖"输入 DELETE"隐藏开关，交互不直观。
    '''   现改为：有选中行 → 启用删除；无选中行 → 禁用删除。
    ''' </remarks>
    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        ' 用户点击 Grid 行 → CurrencyManager.Position 自动同步 → 刷新标签
        ShowPosition()

        ' 有选中行 → 启用删除按钮；无选中行（如数据为空）→ 禁用
        删除.Enabled = (grdAuthorTitles.CurrentRow IsNot Nothing)
    End Sub

    'Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
    '    ' 用户点击 Grid 行 → CurrencyManager.Position 自动同步 → 只需刷新标签
    '    ShowPosition()
    'End Sub


    ''' <summary>
    ''' 功能：关闭 F01_不良品基本信息 窗体，并恢复 Ribbon 上的"不良品信息"按钮。
    '''       涉及对象：Ribbon1.btn不良品信息、Me。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   - Ribbon 按钮在打开窗体时会禁用（防止重复打开），关闭时需恢复，
    '''     否则用户关闭窗体后无法再次打开（历史踩坑）。
    ''' 【优化说明】
    '''   原代码手动置 objDataAdapter / objConnection1th 为 Nothing，
    '''   但窗体关闭后整个实例都会被 GC 回收，无需手动清理，
    '''   且置 Nothing 会导致"若未来改为窗体复用则崩溃"的隐患（已移除）。
    ''' </remarks>
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        ' ============================================================
        ' ★★★ 第1步：恢复 Ribbon 按钮为可用 ★★★
        ' ============================================================
        ' 【关键】打开窗体时 Ribbon 按钮被禁用，关闭时必须恢复。
        Globals.Ribbons.Ribbon1.btn不良品信息.Enabled = True

        ' ============================================================
        ' ★★★ 第2步：关闭窗体 ★★★
        ' ============================================================
        Me.Close()
    End Sub

    ''' <summary>
    ''' 功能：窗体关闭时（无论点"退出"按钮还是点右上角 X），恢复 Ribbon 按钮为可用。
    '''       涉及对象：Globals.Ribbons.Ribbon1.btn不良品信息。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   作为"退出_Click"的兜底：用户点右上角 X 关闭窗体时，也会触发本事件，
    '''   确保 Ribbon 按钮恢复，避免用户无法再次打开窗体（本次测试已踩坑）。
    ''' 【历史踩坑】
    '''   1. 原方法名 D01_资质证书信息_Closed 是从其他项目复制的，命名错误，已改。
    '''   2. 原方法含 objDataAdapter / objConnection1th 置 Nothing，
    '''      但窗体关闭后实例会被 GC 回收，无需手动清理，且有"窗体复用崩溃"隐患（已删）。
    ''' </remarks>
    Private Sub F01_不良品基本信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ' 恢复 Ribbon 按钮为可用（兜底：即使用户点右上角 X 关闭）
        Globals.Ribbons.Ribbon1.btn不良品信息.Enabled = True
    End Sub


    ''' <summary>
    ''' 功能：当"产品规格"下拉框的选中项变化时，自动从"物品信息"表查询对应单价和重量，
    '''       并联动计算加工费用、损失成本；同时从"赔偿比例"表读取当前"发现过程"对应的比例。
    '''       涉及对象：产品规格、类型区分、供应商、发现过程（四个 ComboBox），
    '''               材料费用、重量、加工费用、损失成本、不良数量（五个 TextBox），
    '''               objDataAdapter1th、objDataSet1th、objConnection1th。
    ''' </summary>
    ''' <remarks>
    ''' 【触发场景】
    '''   ① 用户手动修改"产品规格"下拉框；
    '''   ② 用户修改"类型区分"→ 由 类型区分_SelectedIndexChanged 调用本方法；
    '''   ③ 用户修改"发现过程"→ 由 发现过程_SelectedIndexChanged 调用本方法；
    '''   ④ 用户修改"供应商"→ 由 供应商_SelectedIndexChanged 调用本方法（若有）。
    ''' 【历史踩坑】
    '''   1. 原代码用 On Error Resume Next 吞掉所有异常，导致出错无提示、难排查。
    '''      现改为 Try...Catch，至少通过 Debug.WriteLine 记录异常。
    '''   2. 原代码"损失成本"计算行有中文全角括号，会编译失败（已修正为半角）。
    ''' 【注意】
    '''   - 若查询结果为空（tb.Rows.Count = 0），跳过联动计算，避免索引越界。
    '''   - 此处只做 UI 联动，不涉及数据库写操作。
    '''   - 本方法不直接 Handles 其他下拉框，避免与已有独立事件方法重复触发。
    ''' </remarks>
    Private Sub 联动查询_SelectedIndexChanged(sender As Object, e As EventArgs) _
    Handles 产品规格.SelectedIndexChanged
        ' ============================================================
        ' ★★★ 第1步：异常处理外层 ★★★
        ' ============================================================
        ' 原因：原代码用 On Error Resume Next，会吞掉所有异常（如查询为空、类型转换失败），
        '       导致用户看不到错误。现改用 Try...Catch，至少记录日志。
        Try
            ' ============================================================
            ' ★★★ 第2步：查询"物品信息"表，获取单价和重量 ★★★
            ' ============================================================
            ' 说明：按"产品规格 + 类型区分 + 供应商"三个条件联合查询，
            '       因为同一规格在不同供应商/类型下单价可能不同。
            objDataAdapter1th.SelectCommand = New OleDbCommand()
            objDataAdapter1th.SelectCommand.Connection = objConnection1th
            objDataAdapter1th.SelectCommand.CommandText =
            "SELECT 物品价格, 重量 FROM 物品信息 " &
            "WHERE (产品规格 = '" & 产品规格.Text & "' AND 区分 = '" & 类型区分.Text &
            "' AND 供应商 = '" & 供应商.Text & "')"
            objDataAdapter1th.SelectCommand.CommandType = CommandType.Text

            objDataSet1th = New DataSet()
            objDataAdapter1th.Fill(objDataSet1th, "wpxx02")
            Dim tb As DataTable = objDataSet1th.Tables("wpxx02")

            ' ---- 若查询结果为空，直接跳过，避免索引越界 ----
            If tb.Rows.Count = 0 Then
                Debug.WriteLine("联动查询：物品信息查询结果为空，跳过联动计算。")
                Return
            End If

            ' ---- 填充"材料费用"和"重量" ----
            材料费用.Text = tb.Rows(0).Item(0).ToString()
            重量.Text = tb.Rows(0).Item(1).ToString()

            ' ============================================================
            ' ★★★ 第3步：查询"赔偿比例"表，获取加工费用比例 ★★★
            ' ============================================================
            ' 说明：按"发现过程"字段查询对应的赔偿比例。
            objDataAdapter1th.SelectCommand.CommandText =
            "SELECT 赔偿比例.* FROM 赔偿比例 WHERE (发现过程 = '" & 发现过程.Text & "')"
            objDataAdapter1th.Fill(objDataSet1th, "wpxx05")
            Dim tb001 As DataTable = objDataSet1th.Tables("wpxx05")

            ' ---- 若查询结果为空，跳过加工费用计算 ----
            If tb001.Rows.Count = 0 Then
                Debug.WriteLine("联动查询：赔偿比例查询结果为空，跳过加工费用计算。")
                Return
            End If

            ' ============================================================
            ' ★★★ 第4步：计算加工费用 ★★★
            ' ============================================================
            ' 公式：加工费用 = 赔偿比例 × 材料费用 × 不良数量
            ' 说明：赔偿比例取自 tb001 的第2列（Item(1)）。
            加工费用.Text = (CType(tb001.Rows(0).Item(1).ToString(), Single) *
                         CType(材料费用.Text, Single) *
                         CType(不良数量.Text, Integer)).ToString()

            ' ============================================================
            ' ★★★ 第5步：计算损失成本 ★★★
            ' ============================================================
            ' 公式：损失成本 = 加工费用 + 材料费用 × 不良数量
            ' 【历史踩坑】原代码此处用了中文全角括号（），会编译失败；现修正为半角 ()。
            ' 【注意】损失成本.Text 是 String 类型，需 .ToString() 显式转换。
            损失成本.Text = (CType(加工费用.Text, Single) +
                         CType(材料费用.Text, Single) * CType(不良数量.Text, Integer)).ToString()

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：记录异常，避免程序崩溃 ★★★
            ' ============================================================
            ' 说明：此处不弹 MessageBox，避免用户频繁修改下拉框时不断弹窗；
            '       只写调试日志，便于开发时排查。
            Debug.WriteLine(String.Format("联动查询 异常: {0}", ex.ToString()))
        End Try
    End Sub

    Private Sub 类型区分_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 类型区分.SelectedIndexChanged
        联动查询_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub 发现过程_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 发现过程.SelectedIndexChanged
        联动查询_SelectedIndexChanged(Nothing, Nothing)
        完成工序.Text = Split(发现过程.Text, "（")(0)

    End Sub

    ''' <summary>
    ''' 功能：当"不良类型"下拉框选中项变化时，从"不良类型分类"表查询该类型对应的
    '''       "因素确定"下拉框选项，并重新填充"因素确定"。
    '''       当选择"客户发现不良"时，启用金额类字段（重量/损失成本/材料费用/加工费用）
    '''       并清零，供用户手动填写；其他情况则禁用这些字段（由系统联动计算）。
    '''       涉及对象：不良类型（ComboBox）、因素确定（ComboBox）、
    '''               重量、损失成本、材料费用、加工费用（TextBox），
    '''               objDataAdapter1th、objDataSet1th。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   - "不良类型 → 因素确定"是级联关系：不同类型下可选的因素不同。
    '''   - "客户发现不良"是特殊情况：这类不良不由内部加工导致，
    '''     所以没有材料/加工费用，需要用户手动填写金额。
    ''' 【历史踩坑】
    '''   1. 原代码直接访问 objDataAdapter1th.SelectCommand.CommandText，
    '''      若首次操作就是改"不良类型"，SelectCommand 可能为 Nothing，会抛异常。
    '''      现加 Nothing 检查，自动初始化（同 供应商_SelectedIndexChanged 的写法）。
    '''   2. 原代码无异常处理，查询失败会崩溃。
    '''      现加 Try...Catch，失败时记录日志并跳过，不阻塞用户。
    '''   3. 原代码把字段的 Visible 属性注释掉，改用 Enabled 属性，
    '''      效果更柔和（用户能看到字段但不可编辑）。
    ''' </remarks>
    Private Sub 不良类型_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 不良类型.SelectedIndexChanged
        ' ============================================================
        ' ★★★ 第1步：异常处理外层 + 确保 SelectCommand 已初始化 ★★★
        ' ============================================================
        Try
            ' 【关键】原代码直接访问 SelectCommand.CommandText，
            '         若首次操作就是改"不良类型"，SelectCommand 为 Nothing 会崩。
            If objDataAdapter1th.SelectCommand Is Nothing Then
                objDataAdapter1th.SelectCommand = New OleDbCommand()
                objDataAdapter1th.SelectCommand.Connection = objConnection1th
            End If

            ' ============================================================
            ' ★★★ 第2步：查询"不良类型分类"表，获取对应"因素确定"选项 ★★★
            ' ============================================================
            ' 说明：按"不良类型"字段筛选，结果填充到"因素确定"下拉框。
            objDataAdapter1th.SelectCommand.CommandText =
            "SELECT 不良类型分类.* FROM 不良类型分类 WHERE 不良类型 = '" & 不良类型.Text & "'"

            objDataSet1th = New DataSet()
            objDataAdapter1th.Fill(objDataSet1th, "wpxx15")
            Dim tb3 As DataTable = objDataSet1th.Tables("wpxx15")

            ' ---- 重新填充"因素确定"下拉框 ----
            ' 说明：Item(2) 是"因素确定"字段（第3列，索引 2）。
            因素确定.BeginUpdate()
            因素确定.Items.Clear()
            For inCounter = 0 To tb3.Rows.Count - 1
                因素确定.Items.Add(tb3.Rows(inCounter).Item(2).ToString())
            Next
            因素确定.EndUpdate()

            ' ============================================================
            ' ★★★ 第3步：根据"不良类型"是否为"客户发现不良"切换字段启用状态 ★★★
            ' ============================================================
            If 不良类型.Text = "客户发现不良" Then
                ' ---- 客户发现不良：启用金额类字段，清零，供用户手动填写 ----
                ' 说明：这类不良不由内部加工导致，没有材料/加工费用，需用户手动填写。
                重量.Enabled = True
                重量.Text = "0"
                损失成本.Enabled = True
                损失成本.Text = "0"
                材料费用.Enabled = True
                材料费用.Text = "0"
                加工费用.Enabled = True
                加工费用.Text = "0"
            Else
                ' ---- 其他类型：禁用金额类字段（由系统联动计算） ----
                损失成本.Enabled = False
                材料费用.Enabled = False
                加工费用.Enabled = False
            End If

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：记录日志，不弹窗（避免频繁切换时弹窗打扰） ★★★
            ' ============================================================
            Debug.WriteLine(String.Format("不良类型_SelectedIndexChanged 异常: {0}", ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' 功能：当"供应商"下拉框选中项变化时，从"物品信息"表查询该供应商供应的所有"产品规格"，
    '''       并重新填充"产品规格"下拉框（供用户进一步选择）。
    '''       涉及对象：供应商（ComboBox）、产品规格（ComboBox）、objDataAdapter1th、objDataSet1th。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   供应商 → 产品规格 是"级联关系"：不同供应商供应的产品规格不同，
    '''   供应商变化时需要刷新产品规格列表，避免用户选到该供应商不供应的规格。
    ''' 【历史踩坑】
    '''   1. 原代码直接访问 objDataAdapter1th.SelectCommand.CommandText，
    '''      若 SelectCommand 为 Nothing（如首次操作就是改供应商），会抛 NullReferenceException。
    '''      现增加 Nothing 检查，自动初始化。
    '''   2. 原代码无异常处理，查询失败（如 SQL 语法错误、网络中断）时程序崩溃。
    '''      现加 Try...Catch，失败时记录日志并跳过，不阻塞用户。
    ''' 【注意】
    '''   - 本方法只填充"产品规格"下拉框，不触发联动计算；
    '''     用户选完产品规格后由 联动查询_SelectedIndexChanged 触发联动。
    '''   - SQL 拼接存在注入风险，但内部工具用户均为同事，风险可接受（TODO：后续可改参数化）。
    ''' </remarks>
    Private Sub 供应商_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 供应商.SelectedIndexChanged
        Try
            ' ============================================================
            ' ★★★ 第1步：确保 SelectCommand 已初始化 ★★★
            ' ============================================================
            ' 【关键】原代码直接访问 SelectCommand.CommandText，
            '         若首次操作是改供应商（未先改产品规格），SelectCommand 为 Nothing 会崩。
            If objDataAdapter1th.SelectCommand Is Nothing Then
                objDataAdapter1th.SelectCommand = New OleDbCommand()
                objDataAdapter1th.SelectCommand.Connection = objConnection1th
            End If

            ' ============================================================
            ' ★★★ 第2步：查询该供应商供应的所有"产品规格" ★★★
            ' ============================================================
            ' 说明：SELECT DISTINCT 去重，WHERE 按供应商筛选。
            objDataAdapter1th.SelectCommand.CommandText =
            "SELECT DISTINCT 产品规格 FROM 物品信息 WHERE 供应商 = '" & 供应商.Text & "'"

            objDataSet1th = New DataSet()
            objDataAdapter1th.Fill(objDataSet1th, "wpxx2019042701")
            Dim tb2019042701 As DataTable = objDataSet1th.Tables("wpxx2019042701")

            ' ============================================================
            ' ★★★ 第3步：重新填充"产品规格"下拉框 ★★★
            ' ============================================================
            ' 说明：先 Clear 再 Add，避免累积；BeginUpdate/EndUpdate 减少重绘。
            产品规格.BeginUpdate()
            产品规格.Items.Clear()
            For inCounter = 0 To tb2019042701.Rows.Count - 1
                产品规格.Items.Add(tb2019042701.Rows(inCounter).Item(0).ToString())
            Next
            产品规格.EndUpdate()

        Catch ex As Exception
            ' ============================================================
            ' ★★★ 异常处理：记录日志，不弹窗（避免频繁切换时弹窗打扰） ★★★
            ' ============================================================
            Debug.WriteLine(String.Format("供应商_SelectedIndexChanged 异常: {0}", ex.ToString()))
        End Try
    End Sub


    ''' <summary>
    ''' 功能：当"发生日期"控件获得焦点时，设置 MaskedTextBox 的掩码为 "0000/00/00"，
    '''       强制用户按 yyyy/MM/dd 格式输入日期。
    '''       涉及对象：发生日期（MaskedTextBox）。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   获得焦点时才启用掩码，让用户能看到输入提示（如"____/__/__"），
    '''   避免用户在未聚焦时看到丑陋的掩码符号。
    ''' 【机制说明】
    '''   Mask 属性值 "0000/00/00" 表示：
    '''     - 0000：4 位必填数字（年）
    '''     - 00：2 位必填数字（月）
    '''     - 00：2 位必填数字（日）
    '''   / 为字面分隔符。
    ''' 【历史踩坑】
    '''   若 Mask 未设置或设置错误，用户输入的日期可能被截断或格式错乱。
    ''' </remarks>
    Private Sub 发生日期_GotFocus(sender As Object, e As EventArgs) Handles 发生日期.GotFocus
        ' 获得焦点时启用掩码，提示用户输入格式
        发生日期.Mask = "0000/00/00"
    End Sub


    ''' <summary>
    ''' 功能：当"发生日期"控件失去焦点时，先保存当前文本，清空 Mask，再回写文本，
    '''       目的是让用户输入完成后不再显示掩码占位符（更美观）。
    '''       涉及对象：发生日期（MaskedTextBox）。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   MaskedTextBox 在 Mask 生效时会显示 "____/__/__" 这样的占位符，
    '''   用户输完日期后失去焦点时，清空 Mask 显示更干净。
    ''' 【关键顺序】
    '''   必须先保存 strDate，再清空 Mask，再回写。
    '''   若先清空 Mask，Text 会被清空；再回写才能保留用户输入。
    ''' 【历史踩坑】
    '''   如果用户输入不完整（如 "2018/3/"），清空 Mask 后文本可能失真，
    '''   但这是原设计权衡，保持原样。
    ''' </remarks>
    Private Sub 发生日期_LostFocus(sender As Object, e As EventArgs) Handles 发生日期.LostFocus
        ' 保存当前文本
        Dim strDate As String
        strDate = 发生日期.Text

        ' 清空掩码
        发生日期.Mask = ""

        ' 回写文本（此时显示的是用户输入的纯文本，不再有掩码占位符）
        发生日期.Text = strDate
    End Sub


    ''' <summary>
    ''' 功能：监听"发生日期"文本框内容变化，当输入满 10 位（即完整日期 yyyy/MM/dd）时，
    '''       自动调用 LostFocus 处理（清空 Mask 美化显示）。
    '''       涉及对象：发生日期（MaskedTextBox）、发生日期_LostFocus。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   用户手动输入 10 位日期后无需离开控件，界面自动美化。
    ''' 【历史踩坑】
    '''   1. strStrogeValue 变量声明未指定类型（Variant/Object），
    '''      赋值后从未被读取，实际是"死代码"（可删除，本次保留原样）。
    '''   2. Len() 是 VB6 风格，VB.NET 推荐 str.Length，
    '''      但为了最小改动，保留原样。
    ''' </remarks>
    Private Sub 发生日期_TextChanged(sender As Object, e As EventArgs) Handles 发生日期.TextChanged
        ' 声明变量（原代码未指定类型，保留原样）
        Dim strStrogeValue

        ' 当输入长度为 10（即 yyyy/MM/dd 完整日期）时，调用 LostFocus 美化显示
        If Len(发生日期.Text) = 10 Then
            strStrogeValue = 发生日期.Text
            发生日期_LostFocus(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' 功能：点击"重设"按钮时，清空"查询条件"文本框。
    '''       由于 查询条件_TextChanged 监听清空动作并自动刷新数据，
    '''       所以清空文本框等价于"重置查询并恢复全部记录"。
    '''       涉及对象：查询条件（TextBox）、btnReseting（Button）、
    '''               查询条件_TextChanged。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   用户执行查询后，想恢复全部记录，只需点"重设"清空查询条件，
    '''   即可通过 TextChanged 链式触发 Load 重新加载全部数据。
    ''' 【命名说明】
    '''   方法名为 Button1_Click 但 Handles btnReseting.Click，
    '''   是原作者重命名按钮后未同步改方法名。建议后续改名为 btnReseting_Click。
    ''' </remarks>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnReseting.Click
        ' 清空查询条件 → 触发 查询条件_TextChanged → 自动重新加载全部数据
        查询条件.Text = ""
    End Sub

    ''' <summary>
    ''' 功能：弹出文件选择对话框（可多选），将选中的第一个图片复制到共享盘固定目录，
    '''       并更新"图片路径"文本框为共享盘路径。
    '''       涉及对象：xlapp（Excel Application）、图片路径（TextBox）、
    '''               My.Computer.FileSystem（文件操作）。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   - 图片统一存放在共享盘，便于多人查看；不直接存用户本地路径（可能失效）。
    '''   - 使用 xlapp.GetOpenFilename（Excel 的对话框）而非 WinForms 的 OpenFileDialog，
    '''     与 Excel 环境一致（如用户熟悉 Excel 的文件选择器）。
    ''' 【历史踩坑】
    '''   1. 原代码 bytPosition 声明为 Byte，路径长度超过 255 会溢出。
    '''      现改为 Integer（.NET 推荐）。
    '''   2. 原代码无异常处理，文件复制失败（权限/网络/磁盘满）会崩溃。
    '''      现加 Try...Catch，失败时提示用户。
    '''   3. 原代码用 Kill 删除同名文件，若 Kill 失败（如文件被占用）会抛异常。
    '''      现改为 File.Delete（更安全）+ 异常捕获。
    '''   4. 目标目录未检查是否存在。若共享盘目录被删，复制会失败。
    '''      现加目录存在性检查。
    ''' 【注意】
    '''   - 只取用户选择的第一个文件；多选时忽略后续文件（设计如此）。
    '''   - 若用户在对话框中取消，objFileArray 不是数组，直接退出。
    ''' </remarks>
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        ' ============================================================
        ' ★★★ 第1步：弹出文件选择对话框 ★★★
        ' ============================================================
        ' 说明：xlapp.GetOpenFilename 是 Excel 的文件选择对话框，
        '       倒数第二个参数 True 表示"允许多选"。
        Dim objFileArray As Object
        objFileArray = xlapp.GetOpenFilename("所有文件(*.*),*.*", , , , True)

        ' 用户取消 → 返回值不是数组 → 直接退出
        If Not IsArray(objFileArray) Then
            Exit Sub
        End If

        ' ============================================================
        ' ★★★ 第2步：异常处理外层 ★★★
        ' ============================================================
        Try
            ' ---- 2.1 取第一个文件路径 ----
            Dim strSelectedFile As String = CStr(objFileArray(1))   ' Excel 返回的数组从 1 开始

            ' ---- 2.2 目标共享盘目录 ----
            Dim strRecordPath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\1 Pictures\"

            ' 检查目标目录是否存在
            If Not My.Computer.FileSystem.DirectoryExists(strRecordPath) Then
                MessageBox.Show("共享盘目录不存在，请检查网络连接：" & vbCrLf & strRecordPath,
                            "目录不存在", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' ---- 2.3 提取文件名 ----
            ' 【优化】bytPosition 改为 Integer，避免路径超过 255 时溢出（原 Byte 类型隐患）
            Dim strFileName As String = System.IO.Path.GetFileName(strSelectedFile)

            ' ---- 2.4 若共享盘已有同名文件，先删除 ----
            Dim strTargetPath As String = strRecordPath & strFileName
            If My.Computer.FileSystem.FileExists(strTargetPath) Then
                System.IO.File.Delete(strTargetPath)   ' 比 Kill 更安全（保留异常信息）
            End If

            ' ---- 2.5 复制文件到共享盘 ----
            System.IO.File.Copy(strSelectedFile, strTargetPath, True)   ' True = 覆盖

            ' ---- 2.6 更新"图片路径"文本框 ----
            图片路径.Text = strTargetPath

        Catch ex As Exception
            ' ---- 异常处理：文件复制失败时给出友好提示 ----
            MessageBox.Show("复制文件失败：" & ex.Message,
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine(String.Format("btnImport_Click 异常: {0}", ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' 功能：根据"图片路径"文本框中的路径，在 PictureBox1 中显示图片。
    '''       涉及对象：图片路径（TextBox）、PictureBox1。
    ''' </summary>
    ''' <remarks>
    ''' 【历史踩坑】
    '''   1. 原代码无异常处理，路径无效/文件不存在时程序崩溃。
    '''      现加 Try...Catch 并提示用户。
    '''   2. 【重要】Image.FromFile 会锁定图片文件！
    '''      若之后再次导入同名文件（File.Delete 删除旧文件），会因"文件被占用"失败。
    '''      解决方案：用 FileStream 打开后复制到 MemoryStream，再创建 Image，
    '''              这样 Image 不锁定原文件（本方法已用此方案）。
    '''   3. 原代码未释放上一张图片，多次加载会导致内存泄漏。
    '''      解决方案：加载新图前先释放 PictureBox1.Image（本方法已实现）。
    ''' 【注意】
    '''   - 只显示图片，不处理其他类型文件（原代码注释里有打开 xls 的逻辑，已废弃）。
    ''' </remarks>
    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        ' ============================================================
        ' ★★★ 第1步：路径合法性检查 ★★★
        ' ============================================================
        If String.IsNullOrWhiteSpace(图片路径.Text) Then
            MessageBox.Show("图片路径为空，请先导入图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not My.Computer.FileSystem.FileExists(图片路径.Text) Then
            MessageBox.Show("文件不存在：" & vbCrLf & 图片路径.Text,
                        "文件不存在", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' ============================================================
        ' ★★★ 第2步：加载图片（避免锁定文件） ★★★
        ' ============================================================
        Try
            ' ---- 2.1 释放上一张图片（避免内存泄漏） ----
            ' 【关键】PictureBox1.Image 需要显式 Dispose，否则 GDI 资源累积。
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
                PictureBox1.Image = Nothing
            End If

            ' ---- 2.2 用 FileStream + MemoryStream 加载，避免锁定原文件 ----
            ' 【原因】Image.FromFile 会锁定文件，导致后续无法删除/覆盖同名文件。
            Dim objImage As Image
            Using fs As New System.IO.FileStream(图片路径.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Using ms As New System.IO.MemoryStream()
                    fs.CopyTo(ms)
                    ms.Position = 0
                    objImage = Image.FromStream(ms)
                End Using
            End Using

            PictureBox1.Image = objImage

        Catch ex As Exception
            MessageBox.Show("加载图片失败：" & ex.Message,
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine(String.Format("btnOpenFile_Click 异常: {0}", ex.ToString()))
        End Try
    End Sub


    ''' <summary>
    ''' 功能：双击 PictureBox1 时，用系统默认程序打开"图片路径"指向的文件。
    '''       如果是 Excel 文件（.xls），用 Excel 打开；其他文件用资源管理器打开。
    '''       涉及对象：图片路径（TextBox）、PictureBox1、xlapp、Shell。
    ''' </summary>
    ''' <remarks>
    ''' 【历史踩坑】
    '''   1. 原代码用 On Error Resume Next + Err.Number 判断（VBA 风格），
    '''      现保留是为了不引入大改动，但建议后续改为 Try...Catch。
    '''   2. 最后一行 PictureBox1.Image = Nothing 会清空图片显示，
    '''      这是原设计意图（双击打开外部程序后，释放 PictureBox 显示）。
    '''      若想保留图片显示，可删除此行。
    ''' </remarks>
    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        On Error Resume Next
        If InStr(图片路径.Text, "xls") > 0 Then     ' 如果是 Excel 文件
            xlapp.Workbooks.Open(图片路径.Text)      ' 用 Excel 打开
        Else                                        ' 否则
            Shell("explorer.exe " & 图片路径.Text, vbMaximizedFocus)   ' 用资源管理器打开
        End If
        If Err.Number <> 0 Then MsgBox("打开对应文件失败", , "提示")
        PictureBox1.Image = Nothing   ' 清空图片显示（原设计意图）
    End Sub

    ''' <summary>
    ''' 功能：监听"图片路径"文本框的内容变化，同步到隐藏文本框 txtPathEqual。
    '''       涉及对象：图片路径（TextBox）、txtPathEqual（TextBox，隐藏）。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   txtPathEqual 是隐藏文本框，作用是"中转"：通过它的 TextChanged 事件
    '''   触发图片加载（见 txtPathEqual_TextChanged）。
    '''   为什么绕这一圈？因为直接让"图片路径"触发加载可能与数据绑定冲突；
    '''   通过中间控件"延迟一拍"触发，更稳定。
    ''' 【历史踩坑】
    '''   这是原作者的设计，虽然绕弯子，但能工作，故保留。
    '''   未来若重构，可考虑直接在 grdAuthorTitles_SelectionChanged 里加载图片。
    ''' </remarks>
    Private Sub 图片路径_TextChanged(sender As Object, e As EventArgs) Handles 图片路径.TextChanged
        ' 同步到隐藏文本框，触发下一环
        txtPathEqual.Text = 图片路径.Text
    End Sub

    ''' <summary>
    ''' 功能：隐藏文本框 txtPathEqual 内容变化时，触发图片加载（btnOpenFile_Click）。
    '''       若"图片路径"为空，则清空 PictureBox1。
    '''       涉及对象：txtPathEqual、图片路径、btnOpenFile_Click、PictureBox1。
    ''' </summary>
    ''' <remarks>
    ''' 【触发链】
    '''   用户点 Grid 行 → 图片路径 变化 → 图片路径_TextChanged → txtPathEqual 变化
    '''   → 本方法 → btnOpenFile_Click → PictureBox1 显示图片。
    ''' </remarks>
    Private Sub txtPathEqual_TextChanged(sender As Object, e As EventArgs) Handles txtPathEqual.TextChanged
        If Len(图片路径.Text) > 0 Then
            ' 有路径 → 加载图片
            btnOpenFile_Click(Nothing, Nothing)
        Else
            ' 无路径 → 清空图片
            PictureBox1.Image = Nothing
        End If
    End Sub


    ''' <summary>
    ''' 功能：在窗体上按住鼠标左键拖动时，动态调整 GroupBox4（Grid 区域）和
    '''       GroupBox5（图片区域）的宽度，实现类似"分隔条"效果。
    '''       涉及对象：F01_不良品基本信息（Me）、GroupBox4、GroupBox5、MouseEventArgs。
    ''' </summary>
    ''' <remarks>
    ''' 【设计意图】
    '''   用户按住左键横向拖动，可以手动调整左右两个区域的宽度，
    '''   方便根据内容（数据多/图片大）灵活分配空间。
    ''' 【机制说明】
    '''   1. 只在按下左键（e.Button = MouseButtons.Left）时响应；
    '''   2. 鼠标太靠边（X < 40 或 X > 宽度-40）时不动，避免拖出边界；
    '''   3. GroupBox4 宽度 = 鼠标 X 坐标 - GroupBox4 左边界；
    '''   4. GroupBox5 左边界和宽度同步调整，保持两区域不重叠。
    ''' 【历史踩坑】
    '''   1. 原代码用 On Error Resume Next 吞掉所有异常，出错无提示。
    '''      本次保留（因为拖动过程可能频繁触发，加 Try 反而影响性能），
    '''      但建议后续改为 Try...Catch 以便排查。
    '''   2. 代码里有一行 GroupBox4.Left = GroupBox4.Left 是"自赋值"，
    '''      无实际作用（可能是原作者调试残留），本次保留原样。
    '''   3. 边界判断"40"是经验值，若用户屏幕 DPI 不同可能需要调整。
    ''' </remarks>
    Private Sub F01_不良品基本信息_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        ' 说明：拖动过程频繁触发，用 On Error Resume Next 避免异常打断拖动体验。
        '       （后续可考虑改为 Try...Catch + Debug.WriteLine）
        On Error Resume Next

        Dim lngOldWidth As Long

        ' ---- 只在按住鼠标左键时响应 ----
        If e.Button = MouseButtons.Left Then
            With GroupBox4
                ' 保存 GroupBox4 的原始宽度（用于计算 GroupBox5 的新左边界）
                lngOldWidth = .Width

                ' ---- 边界保护：鼠标太靠边时不动，避免拖出可视区域 ----
                If e.X > Me.Width - 40 OrElse e.X < 40 Then Exit Sub

                ' ---- 调整 GroupBox4 宽度 ----
                .Width = e.X - .Left   ' 新宽度 = 鼠标 X - 左边界

                ' ---- 同步调整 GroupBox5 的位置和宽度 ----
                ' 说明：GroupBox5 的左边界 = 原左边界 + (GroupBox4 宽度变化量)
                '       宽度 = 原宽度 - (GroupBox4 宽度变化量)，保持两区域不重叠。
                GroupBox5.Left = e.X + GroupBox5.Left - (GroupBox4.Left + lngOldWidth)
                GroupBox5.Width = GroupBox5.Width - (.Width - lngOldWidth)
            End With
        End If
    End Sub

    ''' <summary>
    ''' 功能：从当前打开的 Excel 工作表导入不良品数据到 Access 数据库。
    '''       流程：读取选区 → 逐行解析 → 按管理编号去重 → 批量 INSERT。
    '''       涉及对象：xlapp（Excel Application）、objConnection1th、OleDbCommand。
    ''' </summary>
    ''' <remarks>
    ''' 【Excel 列结构】（15 列，从第 2 行开始为数据行）
    '''   A=NO(忽略), B=产品规格, C=类型区分, D=管理编号, E=发生日期, F=客户,
    '''   G=加工设备, H=发现过程, I=操作者, J=不良数量, K=完成工序,
    '''   L=不良现象及原因, M=不良类型, N=供应商, O=备注
    ''' 【去重策略】按"管理编号"跳过已存在的记录。
    ''' 【默认值】数据库多出的 7 字段填：加工费用=0、材料费用=0、损失成本=0、
    '''           重量=0、处置完成=False、因素确定=""、图片路径=""。
    ''' 【历史踩坑】
    '''   1. 用户必须在 Excel 里选中数据区域，代码只读取选区。
    '''   2. 若选区没有数据，需提示用户。
    '''   3. 导入过程可能较慢（逐行 INSERT），建议用事务批量提交（TODO）。
    '''   4. 日期字段需用 CDate 转换，否则 Access 可能识别为文本。
    ''' </remarks>
    Private Sub btnDataImport_Click(sender As Object, e As EventArgs) Handles btnDataImport.Click
        ' ============================================================
        ' ★★★ 第1步：检查 Excel 环境 + 选区合法性 ★★★
        ' ============================================================
        ' 说明：xlapp 是 Excel Application 对象，如果为 Nothing 说明环境异常。
        If xlapp Is Nothing Then
            MessageBox.Show("Excel 环境异常，无法读取数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' 检查当前选区是否有效
        Dim rngSelection As Excel.Range
        Try
            rngSelection = CType(xlapp.Selection, Excel.Range)
        Catch
            MessageBox.Show("请先在 Excel 里选中要导入的数据区域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try

        ' 检查选区行数（至少要有 1 行数据）
        ' 【设计意图】用户只选数据行（不含标题），选区第 1 行就是数据。
        Dim intRowCount As Integer = rngSelection.Rows.Count
        If intRowCount < 1 Then
            MessageBox.Show("请先选中要导入的数据区域（不含标题行）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' ============================================================
        ' ★★★ 第2步：确认导入 ★★★
        ' ============================================================
        Dim dlgResult As DialogResult = MessageBox.Show(
        String.Format("即将导入 {0} 行数据（选区全部视为数据，不含标题）。" & vbCrLf &
                      "已存在的管理编号将被跳过。" & vbCrLf & vbCrLf & "确认导入吗？",
                      intRowCount),
        "导入确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If dlgResult <> DialogResult.Yes Then Exit Sub

        ' ============================================================
        ' ★★★ 第3步：逐行读取并导入 ★★★
        ' ============================================================
        Dim intSuccess As Integer = 0   ' 成功导入数
        Dim intSkipped As Integer = 0   ' 跳过的重复数
        Dim intFailed As Integer = 0    ' 失败数

        ' 暂停界面更新，提升性能
        xlapp.ScreenUpdating = False

        Try
            ' 打开数据库连接（整个导入过程只用一次连接）
            objConnection1th.Open()

            ' 从第 1 行开始遍历（选区全部视为数据）
            For intRow As Integer = 1 To intRowCount
                Try
                    ' ---- 3.1 读取当前行各列的值 ----
                    ' 说明：rngSelection.Cells(行, 列) 中行/列都从 1 开始。
                    Dim strProduct As String = GetCellText(rngSelection, intRow, 2)   ' B: 产品规格
                    Dim strType As String = GetCellText(rngSelection, intRow, 3)      ' C: 类型区分
                    Dim strManageId As String = GetCellText(rngSelection, intRow, 4)  ' D: 管理编号
                    Dim strDate As String = GetCellText(rngSelection, intRow, 5)      ' E: 发生日期
                    Dim strCustomer As String = GetCellText(rngSelection, intRow, 6)  ' F: 客户
                    Dim strEquipment As String = GetCellText(rngSelection, intRow, 7) ' G: 加工设备
                    Dim strFoundProcess As String = GetCellText(rngSelection, intRow, 8)  ' H: 发现过程
                    Dim strOperator As String = GetCellText(rngSelection, intRow, 9)  ' I: 操作者
                    Dim strQty As String = GetCellText(rngSelection, intRow, 10)      ' J: 不良数量
                    Dim strProcess As String = GetCellText(rngSelection, intRow, 11)  ' K: 完成工序
                    Dim strPhenom As String = GetCellText(rngSelection, intRow, 12)   ' L: 不良现象及原因
                    Dim strBadType As String = GetCellText(rngSelection, intRow, 13)  ' M: 不良类型
                    Dim strSupplier As String = GetCellText(rngSelection, intRow, 14) ' N: 供应商
                    Dim strRemark As String = GetCellText(rngSelection, intRow, 15)   ' O: 备注

                    ' ---- 3.2 跳过空行（管理编号为空） ----
                    If String.IsNullOrWhiteSpace(strManageId) Then
                        Continue For
                    End If

                    ' ---- 3.3 去重检查（按管理编号） ----
                    If IsManageIdExists(strManageId) Then
                        intSkipped += 1
                        Continue For
                    End If

                    ' ---- 3.4 执行 INSERT ----
                    Dim strSql As String = "INSERT INTO 不良品信息 " &
                    "(管理编号, 发生日期, 客户, 供应商, 产品规格, 加工设备, 发现过程, " &
                    "不良类型, 操作者, 类型区分, 不良数量, 完成工序, " &
                    "加工费用, 材料费用, 损失成本, 不良现象及原因, 备注, 重量, " &
                    "处置完成, 因素确定, 图片路径) " &
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

                    Dim cmd As New OleDbCommand(strSql, objConnection1th)
                    cmd.Parameters.AddWithValue("@管理编号", strManageId)
                    ' 日期：若为空则用当前日期兜底；否则用 CDate 转换
                    If String.IsNullOrWhiteSpace(strDate) Then
                        cmd.Parameters.AddWithValue("@发生日期", Date.Now).DbType = DbType.Date
                    Else
                        cmd.Parameters.AddWithValue("@发生日期", CDate(strDate)).DbType = DbType.Date
                    End If
                    cmd.Parameters.AddWithValue("@客户", strCustomer)
                    cmd.Parameters.AddWithValue("@供应商", strSupplier)
                    cmd.Parameters.AddWithValue("@产品规格", strProduct)
                    cmd.Parameters.AddWithValue("@加工设备", strEquipment)
                    cmd.Parameters.AddWithValue("@发现过程", strFoundProcess)
                    cmd.Parameters.AddWithValue("@不良类型", strBadType)
                    cmd.Parameters.AddWithValue("@操作者", strOperator)
                    cmd.Parameters.AddWithValue("@类型区分", strType)
                    cmd.Parameters.AddWithValue("@不良数量", strQty)
                    cmd.Parameters.AddWithValue("@完成工序", strProcess)
                    cmd.Parameters.AddWithValue("@加工费用", 0).DbType = DbType.Single
                    cmd.Parameters.AddWithValue("@材料费用", 0).DbType = DbType.Single
                    cmd.Parameters.AddWithValue("@损失成本", 0).DbType = DbType.Single
                    cmd.Parameters.AddWithValue("@不良现象及原因", strPhenom)
                    cmd.Parameters.AddWithValue("@备注", strRemark)
                    cmd.Parameters.AddWithValue("@重量", 0).DbType = DbType.Single
                    cmd.Parameters.AddWithValue("@处置完成", False).DbType = DbType.Boolean
                    cmd.Parameters.AddWithValue("@因素确定", "")
                    cmd.Parameters.AddWithValue("@图片路径", "")

                    cmd.ExecuteNonQuery()
                    intSuccess += 1

                Catch exRow As Exception
                    ' 单行失败不影响其他行
                    intFailed += 1
                    Debug.WriteLine(String.Format("导入第 {0} 行失败: {1}", intRow, exRow.Message))
                End Try
            Next

        Catch ex As Exception
            MessageBox.Show("导入过程出错：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Debug.WriteLine(String.Format("btnDataImport_Click 异常: {0}", ex.ToString()))
        Finally
            ' 确保连接关闭、恢复屏幕刷新
            If objConnection1th.State = ConnectionState.Open Then objConnection1th.Close()
            xlapp.ScreenUpdating = True
        End Try

        ' ============================================================
        ' ★★★ 第4步：提示导入结果 + 刷新界面 ★★★
        ' ============================================================
        MessageBox.Show(
        String.Format("导入完成！" & vbCrLf & vbCrLf &
                      "成功：{0} 条" & vbCrLf &
                      "跳过（已存在）：{1} 条" & vbCrLf &
                      "失败：{2} 条",
                      intSuccess, intSkipped, intFailed),
        "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' 刷新窗体数据，让新导入的记录显示出来
        If intSuccess > 0 Then
            F01_不良品基本信息_Load(Nothing, Nothing)
        End If
    End Sub


    ''' <summary>
    ''' 功能：从 Excel Range 中读取指定单元格的文本值（统一转为 String）。
    '''       用于导入时逐格读取，避免 Nothing / DBNull 引起的异常。
    ''' </summary>
    ''' <param name="rng">Excel Range 对象</param>
    ''' <param name="intRow">行号（从 1 开始）</param>
    ''' <param name="intCol">列号（从 1 开始）</param>
    ''' <returns>单元格的文本值；若为空则返回空字符串</returns>
    Private Function GetCellText(rng As Excel.Range, intRow As Integer, intCol As Integer) As String
        Try
            Dim objValue As Object = rng.Cells(intRow, intCol).Value
            If objValue Is Nothing Then Return ""
            Return objValue.ToString().Trim()
        Catch
            Return ""
        End Try
    End Function


    ''' <summary>
    ''' 功能：检查指定"管理编号"是否已存在于数据库（用于导入时去重）。
    ''' </summary>
    ''' <param name="strManageId">管理编号</param>
    ''' <returns>True = 已存在；False = 不存在</returns>
    Private Function IsManageIdExists(strManageId As String) As Boolean
        Try
            Dim strSql As String = "SELECT COUNT(*) FROM 不良品信息 WHERE 管理编号 = ?"
            Dim cmd As New OleDbCommand(strSql, objConnection1th)
            cmd.Parameters.AddWithValue("@管理编号", strManageId)
            Dim intCount As Integer = CInt(cmd.ExecuteScalar())
            Return intCount > 0
        Catch ex As Exception
            Debug.WriteLine(String.Format("IsManageIdExists 异常: {0}", ex.ToString()))
            Return False   ' 查询失败时按"不存在"处理（后续 INSERT 会因主键冲突报错）
        End Try
    End Function

End Class