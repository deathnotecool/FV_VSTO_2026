Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Public Class C02_采购进货信息管理
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb"
    Dim strYiFangPath As String = "\\192.168.3.52\Users\进销存管理.accdb"
    Dim strMyHomerComputerPath As String = "E:\access\进销存管理.accdb"
    Dim strMyCompanyComputerPath As String = "D:\6 总务\access\进销存管理.accdb"
    Dim objConnection As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\进销存管理\进销存管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\进销存管理\进销存管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb")  '公司共享盘

    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '该构造函数使用了SelectCommand属性的一个字符串和一个表示数据库连接的对象来初始化SqlAdapater对象.
    '这个构造函数使我们不必写Adapter属性代码.
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 采购进货信息.* FROM 采购进货信息 ORDER BY 进货编码", objConnection)
    Dim objDataAdapter1th As New OleDbDataAdapter()
    Dim objDataSet As New DataSet()    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet()
    Dim objDataView As DataView        '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objDataView1th As DataView
    Dim objCurrencyManager As CurrencyManager  '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object                      '声明变量,数据库用

    '创建一个过程将在初始化代码中调用,以用来填充数据和显示数据
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '创建并初始化一个数据集对象赋值给变量 Initialize a new instance of the DataSet object.
        '向DataSet对象填充由SqlDataAdapter对象的选择命令SelectCommand属性从数据库检索到的数据填充. 
        '注意:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,那么执行该选择命令,连接没打开就会自动打开填充数据后关闭连接  Fill the DataSet object with data..
        objDataAdapter.Fill(objDataSet, "jhxx")  '这里没有设置SelectCommand属性,因为在初始化Adapter对象时,已经使用了相应的参数.
        '设置对应表为数据源绑定到DataView类  Set the DataView object to the DataSet object.
        objDataView = New DataView(objDataSet.Tables("jhxx"))
        'BindingContect管理CurrencyManager(保持数据与控件同步的对象)集合,指定相应的CurrencyManger,引用定制视图源作为指定的CurrencyManager      Set our CurrencyManager object to the DataView object.
        objCurrencyManager =
      CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub

    '创建一个过程以用来将窗体中的控件绑定到DataView对象.
    Private Sub BindFields()
        On Error Resume Next
        Dim i As Byte = 0
        '控件的DataBindings属性(返回ControlBindingsCollection类)的Clear方法逐一清除控件上的绑定(控件可能与之前数据源捆绑)    
        'Clear any previous bindings..
        myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        '控件逐一绑定DateView数据源,第3参数是数据字段
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "进货日期" Then GroupBox1.Controls(myArray(i).ToString).Text = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd")
        Next i
        ToolStripLabel1.Text = "Ready"     '显示一个"准备"状态    Display a ready status..
    End Sub

    '创建一个能在窗体上显示当前记录位置的过程
    Private Sub ShowPosition()
        '格式化数字txtPrice字段,包含美分.   Format number in the txtPrice field to include cents
        Try
            进货日期.Text = Format(CType(GroupBox1.Controls("进货日期").Text, Date), "yyyy/MM/dd") '定义格式
        Catch e As System.Exception
            GroupBox1.Controls("进货日期").Text = CType(Now, String)   '如果异常(文本框为空)那么将日期写为当前日期
            进货日期.Text = Format(CType(GroupBox1.Controls("进货日期").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
        End Try
        '显示当前记录位置并标记记录数. Display the current position and the number of records
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()
        '物品信息()
    End Sub

    '180119 按钮单击事件,移动第一条记录
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

    '180119-按钮单击事件,移动上一条记录
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

    '180119-按钮单击事件,移动下一条记录
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

    '180119-按钮单击事件,移动最后一条记录
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

    '180119-窗体加载事件.
    Private Sub CO2_采购进货信息管理_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        objDataAdapter.SelectCommand.CommandType = CommandType.Text    'SelectCommand的CommandType属性是CommandType.Text是默认属性.
        '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定控件      
        '需要说明的是,Fill方法会执行SelectCommand,并保持为调用该方法时的状态.
        'Fill the DataSet and bind the fields..
        FillDataSetAndView()
        ShowPosition()  '调用过程显示当前标签记录位置    Show the current record position..
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.  Set the DataGridView properties to bind it to our data..
        grdAuthorTitles.DataSource = objDataSet '设置DataSet对象作为gird控件的数据源(实际上就是一个绑定过程,告知控件从哪里获得数据)
        grdAuthorTitles.DataMember = "jhxx"  'gird控件要显示数据源(填充过数据的DataSet对象)具体的表名称
        Dim objAlignRightCellStyle As New DataGridViewCellStyle '创建DataGridViewCellStyle对象(grd控件单元格样式实例) 'Declare and set the currency header alignment property..
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '将对齐方式格式改为垂直居中向右对齐.
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()    '定义交叉行样式Declare and set the alternating rows style..
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟灰色)
        '创建DataGridViewCellStyle对象(grd控件单元格样式实例)   
        'Declare and set the style for currency cells ..
        '设置单元格格式为货币型(参考).
        'objCurrencyCellStyle.Format = "$#,##0.00"
        'objCurrencyCellStyle.Format = "C"
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight      '将对齐方式改为居右对齐
        '设置控件列标题  Change column names and styles using the column index
        'myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        grdAuthorTitles.Columns(0).HeaderText = "进货编码"
        grdAuthorTitles.Columns(1).HeaderText = "供应商编码"
        grdAuthorTitles.Columns(2).HeaderText = "物品编码"
        grdAuthorTitles.Columns(3).HeaderText = "物品名称"
        grdAuthorTitles.Columns(4).HeaderText = "物品规格"
        grdAuthorTitles.Columns(5).HeaderText = "计量单位"
        grdAuthorTitles.Columns(6).HeaderText = "进货数量"
        grdAuthorTitles.Columns(7).HeaderText = "进货单价"
        grdAuthorTitles.Columns(8).HeaderText = "进货日期"
        grdAuthorTitles.Columns(9).HeaderText = "备注"
        grdAuthorTitles.Columns(9).Width = 65 '设置指定列默认宽度小一点
        grdAuthorTitles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
        '改变字段标题名称和样式  
        'Change column names and styles using the column name
        grdAuthorTitles.Columns("备注").HeaderCell.Value = "特别备注"                  '重新设置列标题的值显示为"状态"
        grdAuthorTitles.Columns("备注").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles.Columns("备注").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        排序字段.Items.Clear()
        For i = 0 To UBound(myArray)    '给组合框添加项目 Add items to the combo box..
            排序字段.Items.Add(GroupBox1.Controls(myArray(i).ToString).Name.ToString)
        Next i
        排序字段.SelectedIndex = 0       '默认选择第一项
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "供应商编码" & " from " & "供应商信息"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        objDataAdapter1th.Fill(objDataSet1th, "jhxx2")
        'objDataView1th = New DataView(objDataSet.Tables("jhxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("jhxx2")
        'Dim a As Byte = tb.Columns.Count - 1
        'Dim i As Byte
        供应商编码.Items.Clear()
        For inCounter = 0 To tb.Rows.Count - 1
            'strResult = .Rows(inCounter).Item("username").ToString _
            '    & "" & .Rows(inCounter).Item("password").ToString
            'MessageBox.Show(strResult)
            供应商编码.Items.Add(tb.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next
        '供应商编码.SelectedIndex = 0       '默认选择第一行项目
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "物品编码" & " from " & "采购物品信息 " & "ORDER BY 物品编码"
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        objDataAdapter1th.Fill(objDataSet1th, "jhxx3")
        Dim tb1 As DataTable = objDataSet1th.Tables("jhxx3")
        物品编码.Items.Clear()
        For inCounter = 0 To tb1.Rows.Count - 1
            物品编码.Items.Add(tb1.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next
        '物品编码.SelectedIndex = 0       '默认选择第一行项目
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "物品名称" & " from " & "采购物品信息 " & "ORDER BY 物品名称"
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        'objDataAdapter1th.Fill(objDataSet1th, "jhxx4")
        'Dim tb2 As DataTable = objDataSet1th.Tables("jhxx4")
        '物品名称.Items.Clear()
        'For inCounter = 0 To tb2.Rows.Count - 1
        '    物品名称.Items.Add(tb2.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
        ''物品名称.SelectedIndex = 0       '默认选择第一行项目
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "物品规格" & " from " & "采购物品信息 " & "ORDER BY 物品规格"
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        'objDataAdapter1th.Fill(objDataSet1th, "jhxx9")
        'Dim tb3 As DataTable = objDataSet1th.Tables("jhxx9")
        '物品规格.Items.Clear()
        'For inCounter = 0 To tb3.Rows.Count - 1
        '    物品规格.Items.Add(tb3.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
        '物品名称.SelectedIndex = 0       '默认选择第一行项目

        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "计量单位" & " from " & "单位信息 " & "ORDER BY 计量单位"
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        'objDataAdapter1th.Fill(objDataSet1th, "jhxx10")
        'Dim tb4 As DataTable = objDataSet1th.Tables("jhxx10")
        '计量单位.Items.Clear()
        'For inCounter = 0 To tb4.Rows.Count - 1
        '    计量单位.Items.Add(tb4.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
        BindFields()


    End Sub

    Private Sub 供应商信息()
        'On Error Resume Next  '出错继续执行下一句代码.
        '创建DataGridViewCellStyle对象'Declare and set the style for currency cells ..  
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '将对齐方式改为居中向右对齐

        '创建DataGridViewCellStyle对象 Declare and set the currency header alignment property. 
        Dim objAlignRightCellStyle As New DataGridViewCellStyle
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '将对齐方式格式改为垂直居中右对齐.
        'objCurrencyCellStyle.Format = "￥#,##0.00"  '设置货币格式
        objCurrencyCellStyle.Format = "C"  '设置货币格式

        '初始化OleDbCommand类,分配SelectCommand属性.   Set the SelectCommand properties..
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection  '将Connection属性设置为连接对象.用来与数据库通信.
        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        'GroupBox3.Controls("维修单号").Text  
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 供应商信息.* FROM 供应商信息 ORDER BY 供应商编码"

        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        objDataSet1th = New DataSet()  '数据适配器对象开始检索数据并填充到DataSet对象 'Fill the DataSet object with data..
        grdAuthorTitles1.DataSource = objDataSet1th  '设置控件的数据源
        grdAuthorTitles1.AutoGenerateColumns = True  '全部显示列.
        objDataAdapter1th.Fill(objDataSet1th, "wxxx2")  '显示表
        objDataView1th = New DataView(objDataSet1th.Tables("wxxx2"))   '初始化一个DataView对象并写入参数构建
        Dim objAlternatingCellStyle As New DataGridViewCellStyle() '初始化一个样式
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke   '设置样式背景色为烟灰色
        'grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle  '设置奇数行属性设置刚创建的样式(烟白色)
        grdAuthorTitles1.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '设置奇数行属性设置刚创建的样式(烟白色)

        '因为数据已填充到DataSet对象中了,可以关闭数据库的连接(通信)   Close the database connection..

        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft      '将对齐方式改为居右对齐
        '设置控件列标题  Change column names and styles using the column index
        'myArray = {"物品编码", "物品名称", "物品规格", "计量单位", "最高库存", "最低库存", "备注"}
        grdAuthorTitles1.DataMember = "wxxx2"

        grdAuthorTitles1.Columns(0).HeaderText = "供应商编码"
        grdAuthorTitles1.Columns(1).HeaderText = "供应商名称"
        grdAuthorTitles1.Columns(2).HeaderText = "通讯地址"
        grdAuthorTitles1.Columns(3).HeaderText = "邮政编码"
        grdAuthorTitles1.Columns(4).HeaderText = "手机号码"
        grdAuthorTitles1.Columns(5).HeaderText = "传真号码"
        grdAuthorTitles1.Columns(6).HeaderText = "联系人"
        grdAuthorTitles1.Columns(7).HeaderText = "电话"
        grdAuthorTitles1.Columns(8).HeaderText = "Email"
        grdAuthorTitles1.Columns(9).HeaderText = "备注"

        'grdAuthorTitles1.Columns(6).Width = 2065 '设置指定列默认宽度小一点
        'grdAuthorTitles1.Columns(1).Width = 265
        'grdAuthorTitles1.Columns(2).Width = 265

        grdAuthorTitles1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells

        '改变字段标题名称和样式       'Change column names and styles using the column name
        grdAuthorTitles1.Columns("备注").HeaderCell.Value = "特别备注"                  '重新设置列标题的值显示为"状态"
        grdAuthorTitles1.Columns("备注").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles1.Columns("备注").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)

    End Sub

    Private Sub 物品信息()
        'On Error Resume Next  '出错继续执行下一句代码.
        '创建DataGridViewCellStyle对象'Declare and set the style for currency cells ..  
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '将对齐方式改为居中向右对齐

        '创建DataGridViewCellStyle对象 Declare and set the currency header alignment property. 
        Dim objAlignRightCellStyle As New DataGridViewCellStyle
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '将对齐方式格式改为垂直居中右对齐.
        'objCurrencyCellStyle.Format = "￥#,##0.00"  '设置货币格式
        objCurrencyCellStyle.Format = "C"  '设置货币格式

        '初始化OleDbCommand类,分配SelectCommand属性.   Set the SelectCommand properties..
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection  '将Connection属性设置为连接对象.用来与数据库通信.
        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        'GroupBox3.Controls("维修单号").Text  
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 采购物品信息.* FROM 采购物品信息 ORDER BY 物品编码"

        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        objDataSet1th = New DataSet()  '数据适配器对象开始检索数据并填充到DataSet对象 'Fill the DataSet object with data..
        grdAuthorTitles1.DataSource = objDataSet1th  '设置控件的数据源
        grdAuthorTitles1.AutoGenerateColumns = True  '全部显示列.
        objDataAdapter1th.Fill(objDataSet1th, "wxxx2")  '显示表
        objDataView1th = New DataView(objDataSet1th.Tables("wxxx2"))   '初始化一个DataView对象并写入参数构建
        Dim objAlternatingCellStyle As New DataGridViewCellStyle() '初始化一个样式
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke   '设置样式背景色为烟灰色
        'grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle  '设置奇数行属性设置刚创建的样式(烟白色)
        grdAuthorTitles1.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '设置奇数行属性设置刚创建的样式(烟白色)

        '因为数据已填充到DataSet对象中了,可以关闭数据库的连接(通信)   Close the database connection..

        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft      '将对齐方式改为居右对齐
        '设置控件列标题  Change column names and styles using the column index
        'myArray = {"物品编码", "物品名称", "物品规格", "计量单位", "最高库存", "最低库存", "备注"}
        grdAuthorTitles1.DataMember = "wxxx2"

        grdAuthorTitles1.Columns(0).HeaderText = "物品编码"
        grdAuthorTitles1.Columns(1).HeaderText = "物品名称"
        grdAuthorTitles1.Columns(2).HeaderText = "物品规格"
        grdAuthorTitles1.Columns(3).HeaderText = "计量单位"
        grdAuthorTitles1.Columns(4).HeaderText = "最高库存"
        grdAuthorTitles1.Columns(5).HeaderText = "最低库存"
        grdAuthorTitles1.Columns(6).HeaderText = "备注"
        'grdAuthorTitles1.Columns(6).Width = 2065 '设置指定列默认宽度小一点
        'grdAuthorTitles1.Columns(1).Width = 265
        'grdAuthorTitles1.Columns(2).Width = 265

        grdAuthorTitles1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells

        '改变字段标题名称和样式       'Change column names and styles using the column name
        grdAuthorTitles1.Columns("备注").HeaderCell.Value = "特别备注"                  '重新设置列标题的值显示为"状态"
        grdAuthorTitles1.Columns("备注").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles1.Columns("备注").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)

    End Sub


    '180120-排序按钮,确定对哪个字段进行排序.单击事件    '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件进行排序
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性, 
        'Determine the appropriate item selected and set the Sort property of the DataView object..
        'myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "进货编码"
            Case 1
                objDataView.Sort = "供应商编码"
            Case 2
                objDataView.Sort = "物品编码"
            Case 3
                objDataView.Sort = "物品名称"
            Case 4
                objDataView.Sort = "物品规格"
            Case 5
                objDataView.Sort = "计量单位"
            Case 6
                objDataView.Sort = "进货数量"
            Case 7
                objDataView.Sort = "进货单价"
            Case 8
                objDataView.Sort = "进货日期"
            Case 9
                objDataView.Sort = "备注"
        End Select

        '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        btnMoveFirst_Click(Nothing, Nothing)
        '修改状态标签Text属性. Display a message that the records have been sorted..
        ToolStripLabel1.Text = "Records Sorted"
    End Sub

    '180120-创建类级查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        '执行查找,声明当前局部变量. 
        'Declare local variables..
        Dim intPosition As Integer
        Dim str条件 As String
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
        'Determine the appropriate item selected And set the Sort property of the DataView object..
        'myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "进货编码"
                str条件 = "进货编码"
            Case 1
                objDataView.Sort = "供应商编码"
                str条件 = "供应商编码"
            Case 2
                objDataView.Sort = "物品编码"
                str条件 = "物品编码"
            Case 3
                objDataView.Sort = "物品名称"
                str条件 = "物品名称"
            Case 4
                objDataView.Sort = "物品规格"
                str条件 = "物品规格"
            Case 5
                objDataView.Sort = "计量单位"
                str条件 = "计量单位"
            Case 6
                objDataView.Sort = "进货数量"
                str条件 = "进货数量"
            Case 7
                objDataView.Sort = "进货单价"
                str条件 = "进货单价"
            Case 8
                objDataView.Sort = "进货日期"
                str条件 = "进货日期"
            Case 9
                objDataView.Sort = "备注"
                str条件 = "备注"
        End Select
        'DataView数据表中筛选数据集.

        If str条件 = "出库使用日期" Then
            objDataView.RowFilter = str条件 & "=#" & CType(查询条件.Text, Date).ToShortDateString & "#" '"Date = #12/31/2008 16:44:58#"
        ElseIf str条件 <> "复选框字段值" Then    'DataView数据表中筛选数据集(类似SQL语句).
            objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"
        Else
            objDataView.RowFilter = str条件 & "=" & CType(查询条件.Text, Boolean)
        End If

        '默认位置0赋值给变量
        intPosition = objCurrencyManager.Position
        '状态栏提示没有找到记录 Display a message that the record was not found..
        If intPosition = -1 Then
            ToolStripLabel1.Text = "Record Not Found"
        Else
            '否则状态栏显示已找到记录. Otherwise display a message that the record was
            ' found and reposition the CurrencyManager to that record..
            ToolStripLabel1.Text = "Record Found"
        End If
        '重新显示当前记录位置. Show the current record position..
        ShowPosition()

    End Sub

    '180120-查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If 查询条件.Text.Length = 0 Then              '如果是空值
            CO2_采购进货信息管理_Load(Nothing, Nothing)   '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置
        End If
    End Sub
    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    '180120-新建按钮事件
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        '声明局部变量
        Dim i As Byte = 0
        '控件的DataBindings属性(返回ControlBindingsCollection类)的Clear方法逐一清除控件上的绑定(控件可能之前的绑定DataView数据源)    
        'Clear any previous bindings..
        myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        '清空简单控件值
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
        进货编码.Enabled = False  '设置禁止使用控件
    End Sub

    '180120-添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字        
        'Declare local variables and objects..
        Dim intMaxID As Integer
        Dim strID As String = ""                                     '变量用来存储authors表的主键并设置authors表的新键
        Dim objCommand As OleDbCommand = New OleDbCommand()     '创建一个新的查询,准备向titleauthor和titles表中插入新记录.
        '存贮当前记录位置给变量  Save the current record position..
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..
        '从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 采购进货信息 ORDER BY 进货编码 DESC", objConnection)
        '打开数据库连接 Open the connection, execute the command SELECT TOP 1 * FROM 表名 ORDER BY 排序字段 DESC
        objConnection.Close()
        objConnection.Open()
        排序字段.SelectedIndex = 0
        查询条件.Text = 进货编码.Text

        '调用SqlCommand的一个执行方法(只返回一行一列).并把结果赋值给变量
        Dim maxId As Object = maxIdCommand.ExecuteScalar()
        '如果返回结果是空值那么执行    If the MaxID column is null..
        If maxId Is DBNull.Value Then
            '设置一个默认值1000.           Set a default value of 1000..
            intMaxID = 1000
        Else
            '否则执行将maxId转换成String型赋值给变量strId.  otherwise set the strID variable to the value in MaxID..
            strID = CType(maxId, String)
            '利用Remove方法删除sb前缀,转换整型赋值给变量intMaxID.       Get the integer part of the string..
            intMaxID = CType(strID.Remove(0, 2), Integer)
            '变量加1  Increment the value..
            intMaxID += 1
        End If
        '变量转换成字符串,并与SB连接,构建一个新主键.   Finally, set the new ID..
        'strID = "SB" & intMaxID.ToString
        '变量转换成字符串,并与SB连接,构建一个新主键.   Finally, set the new ID..
        Select Case Len(intMaxID.ToString)
            Case 1
                strID = "JH00" & intMaxID.ToString
            Case 2
                strID = "JH0" & intMaxID.ToString
        End Select
        '设置命令对象的属性 Set the SqlCommand object properties..
        '将含有连接字符串的连接对象赋值给Connection属性
        objCommand.Connection = objConnection
        '进货编码.Enabled = True
        '将CommandText属性(要执行的SQL字符串)设置指定的值
        'myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        objCommand.CommandText = "INSERT INTO 采购进货信息 " &
        "(进货编码, 供应商编码, 物品编码, 物品名称, 物品规格, 计量单位, 进货数量, 进货单价, 进货日期, 备注) " &
        "VALUES(@进货编码, @供应商编码, @物品编码, @物品名称, @物品规格, @计量单位, @进货数量, @进货单价, @进货日期, @备注)"
        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列.. 
        'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the ' CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@进货编码", strID)
        objCommand.Parameters.AddWithValue("@供应商编码", 供应商编码.Text)
        objCommand.Parameters.AddWithValue("@物品编码", 物品编码.Text)
        objCommand.Parameters.AddWithValue("@物品名称", 物品名称.Text)
        objCommand.Parameters.AddWithValue("@物品规格", 物品规格.Text)
        objCommand.Parameters.AddWithValue("@计量单位", 计量单位.Text)
        objCommand.Parameters.AddWithValue("@进货数量", 进货数量.Text)
        objCommand.Parameters.AddWithValue("@进货单价", 进货单价.Text).DbType = DbType.Currency
        objCommand.Parameters.AddWithValue("@进货日期", 进货日期.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@备注", 备注.Text)
        myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        进货编码.Text = strID
        For i = 0 To UBound(myArray)
            If myArray(i).ToString <> "备注" Then
                If GroupBox1.Controls(myArray(i).ToString).Text.Length = 0 Then MsgBox("请输入完整数据在添加数据") : _
                    新建_Click(Nothing, Nothing) : objConnection.Close() : Exit Sub
            End If
        Next i
        '执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..
        Try '截取异常
            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
        Catch OledbExceptionErr As OleDbException
            MessageBox.Show(OledbExceptionErr.Message)    '如果出错,提示错误信息
        End Try '结束截取
        '关闭数据库连接 Close the connection..
        objConnection.Close()
        '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        CO2_采购进货信息管理_Load(Nothing, Nothing)
        '设置你保存的那个记录位置    Set the record position to the one that you saved..
        objCurrencyManager.Position = objCurrencyManager.Count - 1
        ShowPosition()
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        执行查询_Click(Nothing, Nothing)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件

        '状态栏显示你添加的信息   Display a message that the record was added..
        ToolStripLabel1.Text = "Record Added"
    End Sub

    '180120-更新数据库
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        '声明一个局部变量和创建一个命令对象  Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        '当前记录位置赋值给变量intPosstion. Save the current record position..
        intPosition = objCurrencyManager.Position
        '设置命令对象一些属性 Set the SqlCommand object properties..
        objCommand.Connection = objConnection  '使用数据库连接对象来设置命令对象的Connection属性.
        排序字段.SelectedIndex = 0
        查询条件.Text = 进货编码.Text

        '接着使用SQL字符串设置CommandText属性.
        'SQL语句表示按照指定条件,更新表设备名称  "放置地点", "制造商", "制造日期", "使用部门", "运行状态"等
        'myArray = {"进货编码", "供应商编码", "物品编码", "物品名称", "物品规格", "计量单位", "进货数量", "进货单价", "进货日期", "备注"}
        objCommand.CommandText = "UPDATE 采购进货信息 " &
            "SET 供应商编码 = @供应商编码,物品编码 = @物品编码,物品名称 = @物品名称,物品规格 = @物品规格,计量单位 = @计量单位,进货数量 = @进货数量,进货单价 = @进货单价,进货日期 = @进货日期 ,备注 = @备注  WHERE 进货编码 = @进货编码"
        '命令命令类型为默认CommandType.Text类型,可以省略
        objCommand.CommandType = CommandType.Text
        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the
        ' CommandText property..
        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
        objCommand.Parameters.AddWithValue("@供应商编码", 供应商编码.Text)
        objCommand.Parameters.AddWithValue("@物品编码", 物品编码.Text)
        objCommand.Parameters.AddWithValue("@物品名称", 物品名称.Text)
        objCommand.Parameters.AddWithValue("@物品规格", 物品规格.Text)
        objCommand.Parameters.AddWithValue("@计量单位", 计量单位.Text)
        objCommand.Parameters.AddWithValue("@进货数量", 进货数量.Text)
        objCommand.Parameters.AddWithValue("@进货单价", 进货单价.Text).DbType = DbType.Currency  '转换类型.
        objCommand.Parameters.AddWithValue("@进货日期", 进货日期.Text).DbType = DbType.Date  '转换类型.
        objCommand.Parameters.AddWithValue("@备注", 备注.Text)
        objCommand.Parameters.AddWithValue _
            ("@进货编码", BindingContext(objDataView).Current("进货编码"))
        '打开带连接字符的数据库连接  Open the connection..
        objConnection.Close()
        objConnection.Open()
        '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objCommand.ExecuteNonQuery()
        '关闭数据库连接  Close the connection..
        objConnection.Close()
        '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
        CO2_采购进货信息管理_Load(Nothing, Nothing)
        ' 设置你保存过的记录位置 Set the record position to the one that you saved..
        objCurrencyManager.Position = intPosition
        '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        ShowPosition()
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        执行查询_Click(Nothing, Nothing)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件

        '显示状态信息  Display a message that the record was updated..



        ToolStripLabel1.Text = "Record Updated"
    End Sub

    '180120-删除记录
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        '定义一个局部变量和命令对象 Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        '保存当前记录位置-1以用来记录删除位置.  Save the current record position—1 for the one to be
        ' deleted..
        intPosition = Me.BindingContext(objDataView).Position - 1 '等同于 intPosition = objCurrencyManager.Position-1
        '如果没有记录,则设置记录位置为o.    If the position is less than 0 set it to 0..
        If intPosition < 0 Then
            intPosition = 0
        End If
        '设置命令对象属性 Set the Command object properties..
        objCommand.Connection = objConnection
        objCommand.CommandText = "DELETE FROM 采购进货信息 " &
            "WHERE 进货编码 = @进货编码"
        '给title_id字段提供相应的参数  Parameter for the title_id field..
        objCommand.Parameters.AddWithValue _
        ("@进货编码", BindingContext(objDataView).Current("进货编码"))
        '打开数据库连接 Open the database connection..
        objConnection.Open()
        '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objCommand.ExecuteNonQuery()
        '关闭数据库连接 Close the connection..
        objConnection.Close()
        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        CO2_采购进货信息管理_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition
        '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        ShowPosition()
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件

        '显示一个已删除的信息.  Display a message that the record was deleted..
        ToolStripLabel1.Text = "Record Deleted"
    End Sub

    '180120-获取项目值模板
    'Private Sub grdAuthorTitles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdAuthorTitles.CellContentClick
    '    Dim i As Integer = grdAuthorTitles.CurrentRow.Index
    '    MsgBox(grdAuthorTitles.Item(i, 1).Value.ToString())
    'End Sub
    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        On Error Resume Next
        Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index
        'MsgBox(grdAuthorTitles.Item(i, 1).Value.ToString())
        BindFields()
        objCurrencyManager.Position = intPosition
        ShowPosition()
    End Sub

    '180120-退出
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button32.Enabled = True     '重新使按钮可用.
        Me.Close()  '关闭窗体
    End Sub

    '180120-关闭
    Private Sub C02_采购进货信息管理_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button32.Enabled = True  '重新使按钮可用.
    End Sub


    'Private Sub 进货日期_TextChanged(sender As Object, e As EventArgs) Handles 进货日期.TextChanged
    '    On Error Resume Next
    '    进货日期.Text = Format(CType(GroupBox1.Controls("进货日期").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
    'End Sub

    Private Sub 进货数量_LostFocus(sender As Object, e As EventArgs) Handles 进货数量.LostFocus
        On Error Resume Next
        '声明一个局部变量intPosition作为记录位置,MaxMin作为最大连续数字        
        'Declare local variables and objects..
        Dim MaxInventory As Integer, MinInventory As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()     '创建一个新的查询,准备向titleauthor和titles表中插入新记录.
        '存贮当前记录位置给变量  Save the current record position..
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..

        objDataAdapter1th.SelectCommand = New OleDbCommand()
        'objDataAdapter1th.SelectCommand.Connection = objConnection
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection
        objDataAdapter1th.SelectCommand.CommandText = "select 最高库存,最低库存 from 采购物品信息 " &
            "where 物品编码='" & 物品编码.Text & "'"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        objDataAdapter1th.Fill(objDataSet1th, "jhxx5")
        'objDataView1th = New DataView(objDataSet.Tables("jhxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("jhxx5")
        'Dim a As Byte = tb.Columns.Count - 1
        'Dim i As Byte
        'For inCounter = 0 To tb.Rows.Count - 1
        'strResult = .Rows(inCounter).Item("username").ToString _
        '    & "" & .Rows(inCounter).Item("password").ToString
        'MessageBox.Show(strResult)
        MaxInventory = CType(tb.Rows(0).Item(0).ToString, Integer)   '添加项目值为记录字段所对应的值
        MinInventory = CType(tb.Rows(0).Item(1).ToString, Integer)   '添加项目值为记录字段所对应的值

        'objDataAdapter1th.SelectCommand.CommandText = "select sum(进货数量) as aa from 采购物品信息 where " &
        '  "物品编码='" & 物品编码.Text & "'"
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        'objDataAdapter1th.Fill(objDataSet1th, "jhxx6")
        'Dim tb1 As DataTable = objDataSet1th.Tables("jhxx6")

        Dim myInCommand As OleDbCommand = New OleDbCommand _
       ("select sum(进货数量) as aa from 采购进货信息 where " &
          "物品编码='" & 物品编码.Text & "'", objConnection)
        '打开数据库连接 Open the connection, execute the command 
        objConnection.Open()
        '调用SqlCommand的一个执行方法(只返回一行一列).并把结果赋值给变量
        'myInCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
        Dim myIn As Object = myInCommand.ExecuteScalar()
        '如果返回结果是空值那么执行    If the MaxID column is null..
        If myIn Is DBNull.Value Then
            myIn = 0
        Else
            '否则执行将maxId转换成String型赋值给变量strId.  otherwise set the strID variable to the value in MaxID..
            myIn = CType(myIn, Integer)
        End If
        Dim myOutCommand As OleDbCommand = New OleDbCommand _
       ("select sum(消耗使用数量) as aa from 物品消耗使用信息 " &
         "where 物品编码='" & 物品编码.Text & "'", objConnection)
        'objConnection.Open()
        Dim myOut As Object = myOutCommand.ExecuteScalar()
        If myOut Is DBNull.Value Then
            myOut = 0
        Else
            myOut = CType(myOut, Integer)
        End If
        Dim myNet As Integer = myIn - myOut       '进货数量-已用数量的值赋值给变量作为还可以继续采购的剩余值
        If CType(进货数量.Text, Integer) + myNet > MaxInventory Then     '如果将要进货数量值+可采购的剩余值大于设定的上限,那么
            '提示显示
            MsgBox("当前该物品编号:" & 物品编码.Text & "库存为: " & myNet & " ! " _
             & vbCrLf & "当前输入进货数量累计已经超过了最高库存:" & MaxInventory)

        ElseIf CType(进货数量.Text, Integer) + myNet < MinInventory Then     '否则如果进货数量+剩余量的值小于设定的最小库存量,那么
            MsgBox("当前该物品编号:" & 物品编码.Text & "库存为: " & myNet & " ! " _
                      & vbCrLf & "当前输入进货数量累计已经低于最低库存" & MinInventory, vbCritical, "进货数量")
            进货数量.Focus()      '获得焦点
        End If
    End Sub

    Private Sub 物品编码_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 物品编码.SelectedIndexChanged
        On Error Resume Next
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 采购物品信息.* FROM 采购物品信息 " &
             "where 物品编码='" & 物品编码.Text & "'" & " ORDER BY 物品编码"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        objDataAdapter1th.Fill(objDataSet1th, "jhxx0001")
        'objDataView1th = New DataView(objDataSet.Tables("jhxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("jhxx0001")
        'Dim a As Byte = tb.Columns.Count - 1
        'Dim i As Byte
        'strResult = .Rows(inCounter).Item("username").ToString _
        '    & "" & .Rows(inCounter).Item("password").ToString
        'MessageBox.Show(strResult)
        物品名称.Text = tb.Rows(0).Item(1).ToString  '添加项目值为记录字段所对应的值
        物品规格.Text = tb.Rows(0).Item(2).ToString  '添加项目值为记录字段所对应的值
        计量单位.Text = tb.Rows(0).Item(3).ToString  '添加项目值为记录字段所对应的值
    End Sub

    'Private Sub grdAuthorTitles1_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles1.SelectionChanged
    '    Dim intPosition As Integer = grdAuthorTitles1.CurrentRow.Index
    '    物品编码.Text = grdAuthorTitles1.Item(0, intPosition).Value.ToString()
    '    物品名称.Text = grdAuthorTitles1.Item(1, intPosition).Value.ToString()
    '    物品规格.Text = grdAuthorTitles1.Item(2, intPosition).Value.ToString()
    'End Sub

    Private Sub 物品编码_Click(sender As Object, e As EventArgs) Handles 物品编码.Click
        GroupBox3.Text = "物品信息"
        物品信息()
    End Sub

    Private Sub 供应商编码_Click(sender As Object, e As EventArgs) Handles 供应商编码.Click
        GroupBox3.Text = "供应商信息"
        供应商信息()
    End Sub



    Private Sub 进货日期_GotFocus(sender As Object, e As EventArgs) Handles 进货日期.GotFocus
        进货日期.Mask = "0000/00/00"
    End Sub

    Private Sub 进货日期_LostFocus(sender As Object, e As EventArgs) Handles 进货日期.LostFocus
        Dim strDate As String
        strDate = 进货日期.Text
        进货日期.Mask = ""
        进货日期.Text = strDate
    End Sub

    'Private Sub 进货数量_TextChanged(sender As Object, e As EventArgs) Handles 进货数量.TextChanged
    '    '结束语句
    'End Sub

End Class