Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
' myArray
Public Class K01_入库编号信息管理
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    'Dim strYiFangPath As String = "\\192.168.3.52\Users\进销存管理.accdb"
    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\入库编号信息管理.accdb"
    Dim strMyHomerComputerPath As String = "E:\access\入库编号信息管理.accdb"
    Dim strMyCompanyComputerPath As String = "D:\6 总务\access\入库编号信息管理.accdb"
    Dim objConnection1th As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)

    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '这个构造函数使我们不必写Adapter属性SelectCommand相关代码.已经加入相关参数(SQL语句)和数据库连接对象...
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 入库编号信息.* FROM 入库编号信息 ORDER BY 发货日期", objConnection1th)

    '该构造函数需要使用SelectCommand属性.用来填充临时数据的
    Dim objDataAdapter1th As New OleDbDataAdapter()
    Dim objDataSet As New DataSet()     '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet()  '声明作用域为类级的对象,该对象作为临时数据的容器,将所有临时数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView  '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objDataView1th As DataView      '声明作用域为类级的对象,DataView类用来表示定制临时表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager   '声明作用域为类级的对象,CurrencyManger对象用于控制绑定数据的移动;作为管理Binding对象的列表
    Dim myArray() As String     '声明数组变量,数组长度为要引用的数据表字段数量.

    '创建一个过程,将在Load事件(初始化代码)调用,并用来填充数据和显示数据.
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '调用模块级对象,并重新初始化该(DataSet)对象
        '向DataSet对象填充由Sql(Ole)DataAdapter对象SelectCommand属性从数据库检索到的数据.. 
        '注意:Fill方法使用选择命令SelectCommand.Connection.如果该链接已打开,就会自动打开填充数据后保持打开连接对象,反之则反.  
        objDataAdapter.Fill(objDataSet, "bl")  '表(bl)是初始构建起来的,命名为bl.
        objDataView = New DataView(objDataSet.Tables("bl"))   '初始化并构建DataView对象.
        'CurrencyManager(窗体获取到的数据记录集合)对象包含于BindingContect集合(内置于Win窗体,无须创建)中,
        '将DataView对象转化为CurrencyManager对象.
        objCurrencyManager = CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub

    '创建一个过程,逐一将窗体中的控件属性和指定数据源列绑定(创建Binding),并将其添加到集合中.
    Private Sub BindFields()
        On Error Resume Next
        Dim i As Byte = 0
        '控件获取到的数据绑定(DataBindings属性),逐一清除(Clear方法)控件上的绑定(控件可能之前绑定过旧的DataView数据源) 
        myArray = {"订单号", "供应商", "发货日期", "图号", "型号", "规格", "区分", "材质", "数量", "净重", "炉批号",
            "热处理号", "锻件编号", "采购编码", "客户编号", "备注说明", "入库完成"}

        '逐一绑定控件的DataBindings属性前,清除之前绑定的数据源连接.
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i

        '控件重新逐一绑定DateView数据源,add方法第一参数为要绑定的控件属性的名称,第二参数为要绑定的数据源,
        '第三参数为要绑定给控件的数据字段(指定列).
        For i = 0 To UBound(myArray)
            If GroupBox1.Controls(myArray(i).ToString).Name <> "入库完成" Then
                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            Else
                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Checked", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            End If

            'GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "发货日期" Then GroupBox1.Controls(myArray(i).ToString).Text _
                = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
        Next i

        ToolStripLabel1.Text = "Ready"  '显示一个"只读"状态...
    End Sub

    '创建过程,并显示当前单个记录的位置.
    Private Sub ShowPosition()
        '格式化日期指定短日期格式,
        Try
            发货日期.Text = Format(CType(GroupBox1.Controls("发货日期").Text, Date), "yyyy/MM/dd") '定义格式
        Catch e As System.Exception   '声明一个错误变量类型
            '如果异常(新建记录时,日期文本框为空),那么转换当前日期类型为文本类型,并写入文本框中...
            GroupBox1.Controls("发货日期").Text = CType(Now, String)
            发货日期.Text = Format(CType(GroupBox1.Controls("发货日期").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
        End Try
        '显示当前记录位置,并标记记录数.
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()
    End Sub

    '按钮单击事件,移动第一条记录
    Private Sub btnMoveFirst_Click(Sender As Object,
            E As EventArgs) Handles btnMoveFirst.Click
        Dim intPosition As Integer
        objCurrencyManager.Position = 0  '设置当前记录为第一条记录.
        intPosition = objCurrencyManager.Position   '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件关联
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        '控件与数据源(objDataView)绑定,通过CurrencyManager对象指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
        ShowPosition()
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 

    End Sub

    '按钮单击事件,移动上一条记录
    Private Sub btnMovePrevious_Click(Sender As Object,
            E As EventArgs) Handles btnMovePrevious.Click
        Dim intPosition As Integer
        objCurrencyManager.Position -= 1 'Move to the previous record..
        intPosition = objCurrencyManager.Position  '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged  '解除事件.
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格.
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件.
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
    End Sub

    '按钮单击事件,移动下一条记录.
    Private Sub btnMoveNext_Click(Sender As Object,
            E As EventArgs) Handles btnMoveNext.Click
        Dim intPosition As Integer
        '移动下一条记录. 
        objCurrencyManager.Position += 1 'Move to the next record..
        intPosition = objCurrencyManager.Position  '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
    End Sub

    '按钮单击事件,移动最后一条记录
    Private Sub btnMoveLast_Click(Sender As Object,
            E As EventArgs) Handles btnMoveLast.Click
        Dim intPosition As Integer
        '移动最后一条记录,不需要调用重新绑定过程,自动同步的,只要不更新,就不存在数据源集的变更 
        objCurrencyManager.Position = objCurrencyManager.Count - 1 ' Set the record position to the last record..
        intPosition = objCurrencyManager.Position   '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
        If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
    End Sub

    '加载窗体触发事件,FillDataSetAndView方法会执行命令(SelectCommand),其Connection属性保持为调用该方法时的状态.
    Private Sub K01_入库编号信息管理_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'myArray = {"订单号", "供应商", "发货日期", "图号", "型号", "规格", "区分", "材质", "数量", "净重", "炉批号",
        '    "热处理号", "锻件编号", "采购编码", "客户编号", "备注说明", "入库完成"}
        On Error Resume Next
        FillDataSetAndView() '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定数据源字段到指定控件.
        ShowPosition() '调用ShowPosition方法,并显示当前记录标签位置    
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.
        grdAuthorTitles.DataSource = objDataSet '设置DataSet对象,作为gird控件的数据来源(实际上就是一个绑定过程,告知控件从哪里获得数据).
        grdAuthorTitles.DataMember = "bl"  '设置gird控件要显示的数据源(具体的表名称).
        '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例),将对齐方式格式改为垂直居中向右对齐.
        Dim objAlignRightCellStyle As New DataGridViewCellStyle
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '初始化DataGridViewCellStyle对象(grd控件单元格样式实例) 作为交叉行样式 
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置交叉样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)

        '初始化DataGridViewCellStyle对象,将设置单元格格式为货币型.
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式改为居中向左对齐
        objCurrencyCellStyle.Format = "¥#,##0.00" '样式格式为货币型(美元或者人民币$¥)
        'objCurrencyCellStyle.Format = "C"  '样式格式为货币型(人民币)
        grdAuthorTitles.Columns(0).HeaderText = "订单号"   '设置控件列标题   
        grdAuthorTitles.Columns(1).HeaderText = "供应商"
        grdAuthorTitles.Columns(2).HeaderText = "发货日期"
        grdAuthorTitles.Columns(3).HeaderText = "图号"
        grdAuthorTitles.Columns(4).HeaderText = "型号"
        grdAuthorTitles.Columns(5).HeaderText = "规格"
        grdAuthorTitles.Columns(6).HeaderText = "区分"
        grdAuthorTitles.Columns(7).HeaderText = "材质"
        grdAuthorTitles.Columns(8).HeaderText = "数量"
        grdAuthorTitles.Columns(9).HeaderText = "净重"
        grdAuthorTitles.Columns(10).HeaderText = "炉批号"
        grdAuthorTitles.Columns(11).HeaderText = "热处理号"
        grdAuthorTitles.Columns(12).HeaderText = "锻件编号"
        grdAuthorTitles.Columns(13).HeaderText = "采购编码"
        grdAuthorTitles.Columns(14).HeaderText = "客户编号"
        grdAuthorTitles.Columns(15).HeaderText = "备注说明"
        grdAuthorTitles.Columns(15).Width = 285 '设置指定列默认宽度大一点
        grdAuthorTitles.Columns(16).HeaderText = "入库完成"
        grdAuthorTitles.Columns(16).Width = 60 '设置指定列默认宽度大一点
        '自动调整列宽.
        'grdAuthorTitles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells


        ''改变字段标题名称和样式'Change column names and styles using the column name  
        grdAuthorTitles.Columns("备注说明").HeaderCell.Value = "特别说明" '重新设置列标题的值显示为"描述"
        '单元格内容重新调用样式(之前设定的-货币样式)
        'grdAuthorTitles.Columns("备注说明").DefaultCellStyle = objCurrencyCellStyle
        'grdAuthorTitles.Columns("备注说明").HeaderCell.Style = objAlignRightCellStyle

        '以下标识红色的代码注释保留...
        'For i As Integer = 0 To grdAuthorTitles.RowCount - 2                           '有一个空白行也算一行
        '    If CType(grdAuthorTitles.Item(16, i).Value.ToString(), Boolean) Then
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
        '    Else
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
        '    End If
        'Next

        objCurrencyCellStyle = Nothing     '清除样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        排序字段.Items.Clear()   '给组合框添加项目  'Add items to the combo box..
        排序字段.Items.AddRange(myArray)
        排序字段.SelectedIndex = 13         '默认选择第一项
        区分.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        '添加项目
        区分.Items.Add("I/N") ： 区分.Items.Add("O/T") ： 区分.Items.Add("O/T1") ： 区分.Items.Add("O/T2")

        供应商.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        供应商.Items.Add("荣程A") ： 供应商.Items.Add("新顺章B") ： 供应商.Items.Add("海陆C") ： 供应商.Items.Add("利元D") ： 供应商.Items.Add("广源E") ： 供应商.Items.Add("瑞鑫F")

        'objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
        'objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 ORDER BY 产品规格" '写入SQL语句

        ''objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 ORDER BY 物品编号" '写入SQL语句
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        'objDataAdapter1th.Fill(objDataSet1th, "wpxx01")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        'Dim tb As DataTable = objDataSet1th.Tables("wpxx01") '声明一个表类型,并赋值给该变量.
        '产品规格.Items.Clear()                               '清楚复合框项目集
        'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
        '    产品规格.Items.Add(tb.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
        BindFields() '调用绑定控件过程,因为有复合框,所以放在事件最后面.
    End Sub

    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件数据源进行排序.
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        Select Case 排序字段.SelectedIndex      'Determine the appropriate item selected and set the Sort property of the DataView object..            
            ' {"管理编号", "发货日期", "客户", "供应商", "产品规格", "加工设备", "发现过程", "不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因", "备注"}
            Case 0
                objDataView.Sort = "订单号"   '按字段设备编号升序排序,下同.
            Case 1
                objDataView.Sort = "供应商"
            Case 2
                objDataView.Sort = "发货日期"
            Case 3
                objDataView.Sort = "图号"
            Case 4
                objDataView.Sort = "型号"
            Case 5
                objDataView.Sort = "规格"
            Case 6
                objDataView.Sort = "区分"
            Case 7
                objDataView.Sort = "材质"
            Case 8
                objDataView.Sort = "数量"
            Case 9
                objDataView.Sort = "净重"
            Case 10
                objDataView.Sort = "炉批号"
            Case 11
                objDataView.Sort = "热处理号"
            Case 12
                objDataView.Sort = "锻件编号"
            Case 13
                objDataView.Sort = "采购编码"
            Case 14
                objDataView.Sort = "客户编号"
            Case 15
                objDataView.Sort = "备注说明"
            Case 16
                objDataView.Sort = "入库完成"
        End Select
        btnMoveFirst_Click(Nothing, Nothing)      '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted"   '修改状态标签Text属性. Display a message that the records have been sorted..
    End Sub

    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        Dim intPosition As Integer              '执行查找,声明当前局部变量.'Declare local variables.. 
        Dim str条件 As String = ""
        '根据选定的项并设置DataView对象(源数据是指定表)相关字段的sort属性,  
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "订单号"
                str条件 = "订单号"
            Case 1
                objDataView.Sort = "供应商"
                str条件 = "供应商"

            Case 2
                objDataView.Sort = "发货日期"
                str条件 = "发货日期"
            Case 3
                objDataView.Sort = "图号"
                str条件 = "图号"
            Case 4
                objDataView.Sort = "型号"
                str条件 = "型号"
            Case 5
                objDataView.Sort = "规格"
                str条件 = "规格"
            Case 6
                objDataView.Sort = "区分"
                str条件 = "区分"
            Case 7
                objDataView.Sort = "材质"
                str条件 = "材质"
            Case 8
                objDataView.Sort = "数量"
                str条件 = "数量"
            Case 9
                objDataView.Sort = "净重"
                str条件 = "净重"
            Case 10
                objDataView.Sort = "炉批号"
                str条件 = "炉批号"
            Case 11
                objDataView.Sort = "热处理号"
                str条件 = "热处理号"
            Case 12
                objDataView.Sort = "锻件编号"
                str条件 = "锻件编号"
            Case 13
                objDataView.Sort = "采购编码"
                str条件 = "采购编码"
            Case 14
                objDataView.Sort = "客户编号"
                str条件 = "客户编号"
            Case 15
                objDataView.Sort = "备注说明"
                str条件 = "备注说明"
            Case 16
                objDataView.Sort = "入库完成"
                str条件 = "入库完成"
        End Select

        '识别日期类型和布尔类型的数据,并设定相应的语句查询...
        If str条件 = "发货日期" Then
            objDataView.RowFilter = str条件 & "=#" & CType(查询条件.Text, Date).ToShortDateString & "#" 'Accesss数据库日期需要加"#" Date = #12/31/2008 16:44:58#"
        ElseIf str条件 <> "入库完成" Then    '非布尔类型的设置like语句模糊查询..
            objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"
        Else '其他语句都用布尔语句查询,输入0或者1.
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
            K01_入库编号信息管理_Load(Nothing, Nothing)
        Else
            Exit Sub
        End If
    End Sub

    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    '新建按钮事件
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        On Error Resume Next
        Dim i As Byte = 0   '声明局部变量
        myArray = {"订单号", "供应商", "发货日期", "图号", "型号", "规格", "区分", "材质", "数量", "净重", "炉批号",
            "热处理号", "锻件编号", "采购编码", "客户编号", "备注说明", "入库完成"}
        For i = 0 To UBound(myArray)  '显示的控件显示空值...
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
    End Sub

    '添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个新的查询.
        '设置sql执行命令对象的属性 Set the SqlCommand object properties...
        '将连接字符串的连接对象赋值给Connection属性,并打开
        objCommand.Connection = objConnection1th
        objConnection1th.Open()

        排序字段.SelectedIndex = 13
        查询条件.Text = 采购编码.Text

        '指定SQL语句写入值,并对指定参数赋值...
        objCommand.CommandText = "INSERT INTO 入库编号信息 " &
        "(订单号, 供应商, 发货日期, 图号, 型号, 规格, 区分, 材质, 数量, 净重, 炉批号, 热处理号, 锻件编号, 采购编码, 客户编号, 备注说明, 入库完成) " &
        "VALUES(@订单号, @供应商, @发货日期, @图号, @型号, @规格, @区分, @材质, @数量, @净重, @炉批号, @热处理号, @锻件编号, @采购编码, @客户编号, @备注说明, @入库完成)"
        objCommand.Parameters.AddWithValue("@订单号", 订单号.Text)
        objCommand.Parameters.AddWithValue("@供应商", 供应商.Text)
        objCommand.Parameters.AddWithValue("@发货日期", 发货日期.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@图号", 图号.Text)
        objCommand.Parameters.AddWithValue("@型号", 型号.Text)
        objCommand.Parameters.AddWithValue("@规格", 规格.Text)
        objCommand.Parameters.AddWithValue("@区分", 区分.Text)
        objCommand.Parameters.AddWithValue("@材质", 材质.Text)
        objCommand.Parameters.AddWithValue("@数量", 数量.Text).DbType = DbType.Single
        objCommand.Parameters.AddWithValue("@净重", 净重.Text).DbType = DbType.Single
        objCommand.Parameters.AddWithValue("@炉批号", 炉批号.Text)
        objCommand.Parameters.AddWithValue("@热处理号", 热处理号.Text)
        objCommand.Parameters.AddWithValue("@锻件编号", 锻件编号.Text)
        objCommand.Parameters.AddWithValue("@采购编码", 采购编码.Text)
        objCommand.Parameters.AddWithValue("@客户编号", 客户编号.Text)
        objCommand.Parameters.AddWithValue("@备注说明", 备注说明.Text)
        objCommand.Parameters.AddWithValue("@入库完成", 入库完成.Checked).DbType = DbType.Boolean '试试可不可以删
        Try '截取异常,执行SQL命令对象插入新数据  Execute the SqlCommand object to insert the new data..
            '执行命令对象以更新数据(主要对数据库操作)
            objCommand.ExecuteNonQuery()
        Catch SqlExceptionErr As OleDbException '声明异常类型
            '如果出错,提示异常类型错误信息
            MessageBox.Show(SqlExceptionErr.Message)
        End Try '结束截取
        objConnection1th.Close() '关闭数据库连接 Close the connection..
        K01_入库编号信息管理_Load(Nothing, Nothing) '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        objCurrencyManager.Position = objCurrencyManager.Count - 1   '设置你保存的那个记录位置    Set the record position to the one that you saved..
        ShowPosition()                                               '标签显示位置.
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        执行查询_Click(Nothing, Nothing)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Added"    '状态栏显示你添加的信息   Display a message that the record was added..
    End Sub

    '更新(修改)数据库记录...
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        '声明一个局部变量和创建一个命令对象  Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        intPosition = objCurrencyManager.Position  '当前记录位置赋值给变量intPosstion. Save the current record position..
        objCommand.Connection = objConnection1th '设置命令对象一些属性 Set the SqlCommand object properties..
        排序字段.SelectedIndex = 13
        查询条件.Text = 采购编码.Text
        'SQL语句:使用SQL字符串设置CommandText属性.表示按照指定条件,更新表字段..
        objCommand.CommandText = "UPDATE 入库编号信息 " &
            "SET 订单号 = @订单号,供应商 = @供应商,发货日期 = @发货日期,图号 = @图号,型号 = @型号,规格 = @规格,区分 = @区分,材质 = @材质,数量 = @数量,净重 = @净重,炉批号 = @炉批号,热处理号 = @热处理号,
锻件编号 = @锻件编号,客户编号 = @客户编号,备注说明 = @备注说明,入库完成 = @入库完成 WHERE 采购编码 = @采购编码"
        '命令类型为默认CommandType.Text类型,可以省略,'型号规格字段以相应的文本框Text属性传递给参数设定值...
        objCommand.CommandType = CommandType.Text
        objCommand.Parameters.AddWithValue("@订单号", 订单号.Text)
        objCommand.Parameters.AddWithValue("@供应商", 供应商.Text)
        objCommand.Parameters.AddWithValue("@发货日期", 发货日期.Text).DbType = DbType.Date  '转换类型.
        objCommand.Parameters.AddWithValue("@图号", 图号.Text)
        objCommand.Parameters.AddWithValue("@型号", 型号.Text)
        objCommand.Parameters.AddWithValue("@规格", 规格.Text)
        objCommand.Parameters.AddWithValue("@区分", 区分.Text)
        objCommand.Parameters.AddWithValue("@材质", 材质.Text)
        objCommand.Parameters.AddWithValue("@数量", 数量.Text).DbType = DbType.Single  '转换类型.
        objCommand.Parameters.AddWithValue("@净重", 净重.Text).DbType = DbType.Single  '转换类型.
        objCommand.Parameters.AddWithValue("@炉批号", 炉批号.Text)
        objCommand.Parameters.AddWithValue("@热处理号", 热处理号.Text)
        objCommand.Parameters.AddWithValue("@锻件编号", 锻件编号.Text)
        objCommand.Parameters.AddWithValue("@客户编号", 客户编号.Text)
        objCommand.Parameters.AddWithValue("@备注说明", 备注说明.Text)
        objCommand.Parameters.AddWithValue("@入库完成", 入库完成.Checked).DbType = DbType.Boolean '试试可不可以删
        objCommand.Parameters.AddWithValue _
            ("@采购编码", BindingContext(objDataView).Current("采购编码"))
        objConnection1th.Open()    '打开带连接字符的数据库连接  Open the connection..
        objCommand.ExecuteNonQuery()   '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()    '关闭数据库连接  Close the connection..
        K01_入库编号信息管理_Load(Nothing, Nothing) '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
        objCurrencyManager.Position = intPosition   ' 设置你保存过的记录位置 Set the record position to the one that you saved..
        ShowPosition() '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        '显示状态信息  Display a message that the record was updated..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        执行查询_Click(Nothing, Nothing)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Updated"
    End Sub

    '删除记录
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        '定义一个局部变量和命令对象 Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        '保存当前记录位置-1以用来记录删除位置.  Save the current record position—1 for the one to be deleted...
        intPosition = Me.BindingContext(objDataView).Position - 1
        If intPosition < 0 Then  '如果没有记录,则设置记录位置为0.    If the position is less than 0 set it to 0..
            intPosition = 0
        End If
        objCommand.Connection = objConnection1th      '设置命令对象属性 Set the Command object properties..
        objCommand.CommandText = "DELETE FROM 入库编号信息 " &
            "WHERE 采购编码 = @采购编码"
        '给title_id字段提供相应的参数  Parameter for the title_id field..
        objCommand.Parameters.AddWithValue _
        ("@采购编码", BindingContext(objDataView).Current("采购编码"))
        objConnection1th.Open()     '打开数据库连接 Open the database connection..
        objCommand.ExecuteNonQuery()     '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()         '关闭数据库连接 Close the connection..
        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        K01_入库编号信息管理_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition
        ShowPosition()  '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        '显示一个已删除的信息.  Display a message that the record was deleted..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Deleted"
    End Sub

    '获取项目值模板
    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        'On Error Resume Next
        Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index
        BindFields()
        objCurrencyManager.Position = intPosition
        ShowPosition()
    End Sub

    '退出
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btnCodeForIncoming.Enabled = True
        Me.Close()
    End Sub

    '关闭
    Private Sub K01_入库编号信息管理_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing         '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btnCodeForIncoming.Enabled = True
        Me.Close()
    End Sub

    Private Sub 发货日期_GotFocus(sender As Object, e As EventArgs) Handles 发货日期.GotFocus
        发货日期.Mask = "0000/00/00"
    End Sub

    Private Sub 发货日期_LostFocus(sender As Object, e As EventArgs) Handles 发货日期.LostFocus
        Dim strDate As String
        strDate = 发货日期.Text
        发货日期.Mask = ""
        发货日期.Text = strDate
    End Sub

    Private Sub 发货日期_TextChanged(sender As Object, e As EventArgs) Handles 发货日期.TextChanged
        Dim strStrogeValue
        If Len(发货日期.Text) = 10 Then strStrogeValue = 发货日期.Text : 发货日期_LostFocus(Nothing, Nothing)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnReseting.Click
        查询条件.Text = ""
    End Sub

    '扫码枪写入值,触发事件...
    Private Sub txtPosition1_TextChanged(sender As Object, e As EventArgs) Handles txtPosition1.TextChanged
        'If InStr(图片路径.Text, "xls") > 0 Then     '如果是包含有xls后缀存储名
        '    xlapp.Workbooks.Open(图片路径.Text)     '打开EXCEL
        'If InStr(备注说明.Text, ":") > 0 Then
        On Error Resume Next
        objConnection1th.Close()
        Dim i As Byte = 0, myArray1 As Object
        '确定二维码数量中连接字符号":"的数量是否等于齐全(15个)...
        If UBound(Split(txtPosition1.Text, ":")) = 15 Then
            '控件获取到的数据绑定(DataBindings属性),逐一清除(Clear方法)控件上的绑定(控件可能之前绑定过旧的DataView数据源) 
            myArray1 = {"订单号", "供应商", "发货日期", "图号", "型号", "规格", "区分", "材质", "数量", "净重", "炉批号",
            "热处理号", "锻件编号", "采购编码", "客户编号"}
            For i = 0 To UBound(myArray1)
                GroupBox1.Controls(myArray(i).ToString).Text = Split(txtPosition1.Text, ":")(i)
            Next i
            txtPosition1.Text = ""
            添加_Click(Nothing, Nothing)
            txtPosition1.Focus()
        Else
            Exit Sub
        End If
        'PauseWait(5000)
        '添加_Click(Nothing, Nothing)
        '备注说明.Text = ""
        'Else                                        '否则
        '    Exit Sub
        'End If                  '结束语句
        '添加_Click(Nothing, Nothing)
        'txtPosition1.Text = ""
    End Sub

    '扫描触发事件的文本框获得焦点,准备开始扫描
    Private Sub btnScan_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        txtPosition1.Text = ""
        新建_Click(Nothing, Nothing)
        txtPosition1.Focus()
    End Sub

    Public Sub PauseWait(ByVal HowLong As Long)
        Dim tick As Long
        tick = My.Computer.Clock.TickCount
        Do
            Application.DoEvents()
        Loop Until tick + HowLong < My.Computer.Clock.TickCount
    End Sub

    Private Sub btnDisplayingRedData_Click(sender As Object, e As EventArgs) Handles btnDisplayingRedData.Click
        For i As Integer = 0 To grdAuthorTitles.RowCount - 2                           '有一个空白行也算一行
            If CType(grdAuthorTitles.Item(16, i).Value.ToString(), Boolean) Then
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
            Else
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
            End If
        Next
    End Sub
End Class