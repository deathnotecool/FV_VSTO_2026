
Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Imports System.IO   '命名空间：文件,方便调用它的功能，而无需完整书写完整的父对象 190802

Public Class J01_产品合格检信息确认
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\合格检验系统确认.accdb"
    'Dim strYiFangPath As String = "\\192.168.3.52\Users\进销存管理.accdb"
    Dim strMyHomerComputerPath As String = "E:\access\合格检验系统确认.accdb"
    Dim strMyCompanyComputerPath As String = "D:\6 总务\access\合格检验系统确认.accdb"
    Dim objConnection1th As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)



    Dim strShareStorge As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\2 MakeQRE\"
    Dim strRecordPath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\3 Record\"
    Dim myCompanyStorge As String = "C:\MakeQRE\"
    '  Dim objConnection1th As New OleDbConnection _
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\合格检验系统确认.accdb")  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\不良品信息管理\不良品信息管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\不良品信息管理\不良品信息管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\不良品信息管理.accdb")  '公司共享盘
    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '这个构造函数使我们不必写Adapter属性SelectCommand相关代码.已经加入相关参数(SQL语句)

    'Dim objDataAdapter As New OleDbDataAdapter("SELECT 合格检信息.* FROM 合格检信息 ORDER BY 检查ID", objConnection1th)
    Dim objDataAdapter As OleDbDataAdapter
    Dim strGetData As String = "SELECT 合格检信息.* FROM 合格检信息 ORDER BY 检查ID"

    Dim objDataAdapter1th As New OleDbDataAdapter()  '该构造函数需要使用SelectCommand属性.用来填充履历卡数据的
    Dim objDataSet As New DataSet()     '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet()  '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView         '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objDataView1th As DataView      '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager   '声明作用域为类级的对象,CurrencyManger对象用于控制绑定数据的移动;作为管理Binding对象的列表
    Dim myArray() As String                       '声明数组变量,数组长度为要引用的数据表字段数量.
    'myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员",
    '"确认判定", "路径", "备注说明"}

    '调用模块级对象,重新初始化该(DataSet)对象
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()
        '向DataSet对象填充由Sql(Ole)DataAdapter对象SelectCommand属性从数据库检索到的数据.. 
        '注意:Fill方法使用选择命令SelectCommand.Connection.如果该链接已打开,就会自动打开填充数据后保持打开连接对象,反之则反.  
        objDataAdapter.Fill(objDataSet, "bl")  '表(bl)是初始构建起来的,命名为bl.
        objDataView = New DataView(objDataSet.Tables("bl"))   '初始化并构建一个DataView对象.
        'CurrencyManager(窗体获取到的数据库数据记录)的集合对象,包含于BindingContect集合对象(内置于Win窗体,无须创建)中,
        '将DataView对象转化为CurrencyManager对象.
        objCurrencyManager = CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub


    '创建一个过程,逐一将窗体中的控件属性和指定数据源创建Binding,并将其添加到集合中.
    Private Sub BindFields()
        On Error Resume Next
        Dim i As Byte = 0
        '控件获取到的数据绑定(DataBindings属性),逐一清除(Clear方法)控件上的绑定(控件可能之前绑定过旧的DataView数据源) 
        myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员",
            "确认判定", "路径", "备注说明"}
        For i = 0 To UBound(myArray)
            '清除所有绑定的数据记录
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        '控件重新逐一绑定DateView数据源,add方法第一参数为要绑定的控件属性的名称,第二参数为要绑定的数据源, 第三参数为要绑定给控件的数据字段(所有的记录列表值).
        For i = 0 To UBound(myArray)
            If GroupBox1.Controls(myArray(i).ToString).Name = "确认判定" Then
                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Checked", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            Else
                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            End If
            If GroupBox1.Controls(myArray(i).ToString).Name = "检查日期" Then GroupBox1.Controls(myArray(i).ToString).Text _
                    = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
        Next i
        ToolStripLabel1.Text = "Ready"  '显示一个"只读"状态..
    End Sub

    '创建过程,并显示当前单个记录的位置.
    Private Sub ShowPosition()
        Try  '格式化日期指定短日期格式.
            检查日期.Text = Format(CType(GroupBox1.Controls("检查日期").Text, Date), "yyyy/MM/dd")  '重新定义日期格式
        Catch e As System.Exception   '声明一个错误变量类型
            '如果异常(文本框为空),那么转换当前日期类型为文本类型,并写入文本框中,重新转换Date类型.
            GroupBox1.Controls("检查日期").Text = CType(Now, String)
            检查日期.Text = Format(CType(GroupBox1.Controls("检查日期").Text, Date), "yyyy/MM/dd")
        End Try
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
        " of " & objCurrencyManager.Count() '显示当前记录位置,并标记记录数. 
    End Sub

    '按钮单击事件,移动第一条记录
    Private Sub btnMoveFirst_Click(Sender As Object,
                E As EventArgs) Handles btnMoveFirst.Click
        Dim intPosition As Integer '声明一个用于记录位置点的变量
        objCurrencyManager.Position = 0  '设置当前记录为第一条记录.
        intPosition = objCurrencyManager.Position   '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件关联,防止事件的干扰
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '程序结束前重新绑定事件
        '控件绑定的CurrencyManager对象与objDataView对象数据完全相等,通过CurrencyManager对象指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
        ShowPosition()
        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
    End Sub

    '按钮单击事件,移动上一条记录
    Private Sub btnMovePrevious_Click(Sender As Object,
                E As EventArgs) Handles btnMovePrevious.Click
        Dim intPosition As Integer
        objCurrencyManager.Position -= 1 'Move to the previous record..
        intPosition = objCurrencyManager.Position  '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged  '解除事件.
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格.
        'AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件.
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件.
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.

        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
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
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
        'If 查询条件.Text <> "" Then grdAuthorTitles.Rows(0).Selected = True 'CurrentCell 
        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
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
        'If 查询条件.Text <> "" Then grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(0).Cells(0) 'CurrentCell 
    End Sub

    '加载窗体触发事件
    Private Sub J01_产品合格检信息确认_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '需要说明的是,Fill方法会执行命令(SelectCommand),其Connection属性保持为调用该方法时的状态.
        'On Error Resume Next
        PictureBox1.Image = Nothing
        objDataAdapter = New OleDbDataAdapter(strGetData, objConnection1th)

        FillDataSetAndView() '调用FillDataSetAndView过程检索数据并在后面调用BindFields过程绑定数据源字段到指定控件.
        ShowPosition()  '调用ShowPosition方法,并显示当前记录标签位置    
        'BindFields()  '调用绑定控件过程,因为有复合框,所以放在事件最后面.
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.
        grdAuthorTitles.DataSource = objDataSet '设置DataSet对象,作为gird控件的数据来源(实际上就是一个绑定过程,告知控件从哪里获得数据).
        grdAuthorTitles.DataMember = "bl"  '设置gird控件要显示的数据源(具体的表名称).
        '将对齐方式格式改为垂直居中向右对齐.
        Dim objAlignRightCellStyle As New DataGridViewCellStyle  '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例) 
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dim objAlternatingCellStyle As New DataGridViewCellStyle() '初始化DataGridViewCellStyle对象(grd控件单元格样式实例) 作为交叉行样式  
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置交叉样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()  '初始化DataGridViewCellStyle对象,将设置单元格格式为货币型.
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式改为居中向左对齐
        objCurrencyCellStyle.Format = "¥#,##0.00" '样式格式为货币型(美元或者人民币$¥)
        'objCurrencyCellStyle.Format = "C"  '样式格式为货币型(人民币)
        '    myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员",
        ''"确认判定", "路径", "备注说明"}

        grdAuthorTitles.Columns(0).HeaderText = "检查ID"   '设置控件列标题   
        grdAuthorTitles.Columns(1).HeaderText = "产品编号"
        grdAuthorTitles.Columns(2).HeaderText = "客户"
        grdAuthorTitles.Columns(3).HeaderText = "型号"
        grdAuthorTitles.Columns(4).HeaderText = "类型区分"
        grdAuthorTitles.Columns(5).HeaderText = "工序ID"
        grdAuthorTitles.Columns(6).HeaderText = "项目"
        grdAuthorTitles.Columns(7).HeaderText = "检查区域"
        grdAuthorTitles.Columns(8).HeaderText = "检查日期"
        grdAuthorTitles.Columns(9).HeaderText = "检验员"
        grdAuthorTitles.Columns(10).HeaderText = "确认判定"
        grdAuthorTitles.Columns(11).HeaderText = "路径"
        grdAuthorTitles.Columns(12).HeaderText = "备注说明"
        grdAuthorTitles.Columns(11).Width = 0 '设置指定列默认宽度为0
        '改变字段标题名称和样式'Change column names and styles using the column name  
        grdAuthorTitles.Columns("备注说明").HeaderCell.Value = "特别说明" '重新设置列标题的值显示为"描述"


        ''标题重新调用列标题样式(之前设定的-居中右对齐)
        'grdAuthorTitles.Columns("加工费扣").HeaderCell.Style = objAlignRightCellStyle
        ''单元格内容重新调用样式(之前设定的-货币样式)
        'grdAuthorTitles.Columns("加工费扣").DefaultCellStyle = objCurrencyCellStyle
        ''单元格内容重新调用样式(之前设定的-货币样式)
        'grdAuthorTitles.Columns("毛坯费扣").DefaultCellStyle = objCurrencyCellStyle


        ''遍历记录数量
        'For i As Integer = 0 To grdAuthorTitles.RowCount - 1  '有一个空白行也算一行
        '    If Math.Ceiling(CType(grdAuthorTitles.Item(7, i).Value.ToString(), Date).Subtract(Now).TotalDays) <= 20 Then
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
        '    Else
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
        '    End If
        'Next

        'For i As Integer = 0 To grdAuthorTitles.RowCount - 2                           '有一个空白行也算一行
        '    If CType(grdAuthorTitles.Item(10, i).Value.ToString(), Boolean) Then
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
        '    Else
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
        '        grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
        '    End If
        'Next
        grdAuthorTitles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
        objCurrencyCellStyle = Nothing     '清除样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        grdAuthorTitles.Columns(11).Visible = False '隐藏路径列

        排序字段.Items.Clear()   '给组合框添加项目  'Add items to the combo box..
        排序字段.Items.AddRange(myArray)
        排序字段.SelectedIndex = 0         '默认选择第一项

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

        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "客户" & " from " & "物品信息 ORDER BY 客户" '写入SQL语句
        'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        'objDataAdapter1th.Fill(objDataSet1th, "wpxx04")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        'Dim tb1 As DataTable = objDataSet1th.Tables("wpxx04") '声明一个表类型,并赋值给该变量.
        '客户.Items.Clear()                               '清楚复合框项目集
        'For inCounter = 0 To tb1.Rows.Count - 1               '在表行数上循环
        '    客户.Items.Add(tb1.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
        检验员.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        检验员.Items.Add("杨伟伟") ： 检验员.Items.Add("裴建华") ： 检验员.Items.Add("杭森业") ： 检验员.Items.Add("宗礼清") ： 检验员.Items.Add("杨宇") ： 检验员.Items.Add("邵松")
        BindFields()  '调用绑定控件过程
    End Sub

    Private Sub 工序信息()
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
        objDataAdapter1th.SelectCommand.Connection = objConnection1th  '将Connection属性设置为连接对象.用来与数据库通信.
        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        'GroupBox3.Controls("维修单号").Text  
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        'objDataAdapter1th.SelectCommand.CommandText = "SELECT 工序信息.* FROM 工序信息  WHERE ((客户 = " & "'" & 客户.Text & "') and (产品规格='" & 型号.Text & "') and (区分='" & 类型区分1.Text & "')) ORDER BY 工序ID"  'ORDER BY 工序ID"
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 工序信息.* FROM 工序信息 ORDER BY 工序ID"  'ORDER BY 工序ID"

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

        grdAuthorTitles1.Columns(0).HeaderText = "工序ID"
        grdAuthorTitles1.Columns(1).HeaderText = "项目"
        grdAuthorTitles1.Columns(2).HeaderText = "检查区域"
        grdAuthorTitles1.Columns(3).HeaderText = "客户"
        grdAuthorTitles1.Columns(4).HeaderText = "备注说明"
        'grdAuthorTitles1.Columns(6).Width = 2065 '设置指定列默认宽度小一点
        'grdAuthorTitles1.Columns(1).Width = 265
        'grdAuthorTitles1.Columns(2).Width = 265

        grdAuthorTitles1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells

        '改变字段标题名称和样式       'Change column names and styles using the column name
        grdAuthorTitles1.Columns("备注说明").HeaderCell.Value = "特别备注"                  '重新设置列标题的值显示为"状态"
        grdAuthorTitles1.Columns("备注说明").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles1.Columns("备注说明").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)

    End Sub
    Private Sub 工序ID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 工序ID.SelectedIndexChanged
        On Error Resume Next
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection1th
        objDataAdapter1th.SelectCommand.CommandText = "select * from 工序信息 where 工序ID='" & 工序ID.Text & "'" &
        " ORDER BY 工序ID"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        Dim objDataSet1th = New DataSet()
        objDataAdapter1th.Fill(objDataSet1th, "xhxx4")
        'objDataView1th = New DataView(objDataSet.Tables("jhxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("xhxx4")
        'Dim a As Byte = tb.Columns.Count - 1
        'Dim i As Byte
        项目.Text = tb.Rows(0).Item(1).ToString
        检查区域.Text = tb.Rows(0).Item(2).ToString
        客户.Text = tb.Rows(0).Item(3).ToString
        型号.Text = tb.Rows(0).Item(5).ToString
        类型区分.Text = tb.Rows(0).Item(6).ToString




        项目.Enabled = False
        检查区域.Enabled = False
        客户.Enabled = False
        型号.Enabled = False
        类型区分.Enabled = False


        '成本单价.Items.Clear()
        'For inCounter = 0 To tb1.Rows.Count - 1
        '    成本单价.Items.Add(tb1.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
    End Sub



    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件数据源进行排序.
    'myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员",
    '"确认判定", "路径", "备注说明"}


    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click

        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性.
        Select Case 排序字段.SelectedIndex      'Determine the appropriate item selected and set the Sort property of the DataView object..            
            Case 0
                objDataView.Sort = "检查ID"   '按字段设备编号升序排序,下同.
            Case 1
                objDataView.Sort = "产品编号"
            Case 2
                objDataView.Sort = "客户"
            Case 3
                objDataView.Sort = "型号"
            Case 4
                objDataView.Sort = "类型区分"
            Case 5
                objDataView.Sort = "工序ID"
            Case 6
                objDataView.Sort = "项目"
            Case 7
                objDataView.Sort = "检查区域"
            Case 8
                objDataView.Sort = "检查日期"
            Case 9
                objDataView.Sort = "检验员"
            Case 10
                objDataView.Sort = "确认判定"
            Case 11
                objDataView.Sort = "路径"
            Case 12
                objDataView.Sort = "备注说明"
        End Select
        btnMoveFirst_Click(Nothing, Nothing)      '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted"   '修改状态标签Text属性. Display a message that the records have been sorted..
    End Sub

    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        On Error Resume Next
        'myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员",
        '"确认判定", "路径", "备注说明"}
        Dim intPosition As Integer              '执行查找,声明当前局部变量.'Declare local variables.. 

        Dim str条件 As String = ""



        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  

        'Determine the appropriate item selected And set the Sort property of the DataView object..
        Select Case 排序字段.SelectedIndex
              '"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"
            Case 0
                objDataView.Sort = "检查ID"
                str条件 = "检查ID"
            Case 1
                objDataView.Sort = "产品编号"
                str条件 = "产品编号"

            Case 2
                objDataView.Sort = "客户"
                str条件 = "客户"
            Case 3
                objDataView.Sort = "型号"
                str条件 = "型号"
            Case 4
                objDataView.Sort = "类型区分"
                str条件 = "类型区分"
            Case 5
                objDataView.Sort = "工序ID"
                str条件 = "工序ID"
            Case 6
                objDataView.Sort = "项目"
                str条件 = "项目"
                '"不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
            Case 7
                objDataView.Sort = "检查区域"
                str条件 = "检查区域"
            Case 8
                objDataView.Sort = "检查日期"
                str条件 = "检查日期"
            Case 9
                objDataView.Sort = "检验员"
                str条件 = "检验员"
            Case 10
                objDataView.Sort = "确认判定"
                str条件 = "确认判定"
            Case 11
                objDataView.Sort = "路径"
                str条件 = "路径"
            Case 12
                objDataView.Sort = "备注说明"
                str条件 = "备注说明"
        End Select
        If str条件 = "检查日期" Then
            objDataView.RowFilter = str条件 & "=#" & CType(查询条件.Text, Date).ToShortDateString & "#" '"Date = #12/31/2008 16:44:58#"
        ElseIf str条件 = "确认判定" Then    'DataView数据表中筛选数据集(类似SQL语句).
            objDataView.RowFilter = str条件 & "=" & CType(查询条件.Text, Boolean)
        Else
            objDataView.RowFilter = UCase(str条件) & " Like  '%" & 查询条件.Text & "%'"
        End If




        Dim strSplitData As String(), intCounter1 As Byte = 0
        If 类型区分1.Text = "Assembly" Then

            For i = 1 To 5 Step 2
                intCounter1 = intCounter1 + 1
                ReDim Preserve strSplitData(0 To intCounter1 - 1)   '重置一维数组上标
                strSplitData(intCounter1 - 1) = Trim(Split(产品编号.Text, "\")(i))  '在一维数组中写入值 
                'ReDim Preserve strSplitData(0 To )
                'strSplitData() = Split(产品编号.Text, "\")(i - 1)
                'xlapp.Range("a" & i).Value = Trim(s)
                'xlapp.Range("a1").Value = Trim(产品编号.Text)
            Next
        End If






        If str条件 = "产品编号" And 类型区分1.Text <> "Assembly" Then
            strGetData = "SELECT 合格检信息.* FROM 合格检信息 ORDER BY 检查ID"
            J01_产品合格检信息确认_Load(Nothing, Nothing)
            strGetData = "SELECT 合格检信息.* FROM 合格检信息 WHERE 产品编号 LIKE '%" & 查询条件.Text & "%' ORDER BY 检查ID"
        ElseIf str条件 = "产品编号" And 类型区分1.Text = "Assembly" Then
            strGetData = "SELECT 合格检信息.* FROM 合格检信息 ORDER BY 检查ID"
            J01_产品合格检信息确认_Load(Nothing, Nothing)
            strGetData = "SELECT 合格检信息.* FROM 合格检信息 WHERE 产品编号 = '" & strSplitData(0) & "' OR 产品编号 = '" & strSplitData(1) & "'  OR 产品编号 = '" & strSplitData(2) & "' ORDER BY 检查ID"
        Else

            strGetData = "SELECT 合格检信息.* FROM 合格检信息 ORDER BY 检查ID"

            J01_产品合格检信息确认_Load(Nothing, Nothing)
            strGetData = "SELECT 合格检信息.* FROM 合格检信息 WHERE " & str条件 & " LIKE '%" & 查询条件.Text & "%' ORDER BY 检查ID"
        End If


        J01_产品合格检信息确认_Load(Nothing, Nothing)
        ShowPosition() '重新显示当前记录位置. Show the current record position..
        intPosition = objCurrencyManager.Position  '默认位置赋值给变量
        If intPosition = -1 Then  '状态栏提示没有找到记录 Display a message that the record was not found..
            ToolStripLabel1.Text = "Record Not Found"  '标签显示字符.
            '否则状态栏显示字符..
        Else
            ToolStripLabel1.Text = "Record Found"
        End If
    End Sub



    '查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If 查询条件.Text.Length = 0 Then  '如果是空值
            '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置..
            strGetData = "SELECT 合格检信息.* FROM 合格检信息 ORDER BY 检查ID"
            J01_产品合格检信息确认_Load(Nothing, Nothing)
        ElseIf 查询条件.Text = "delete" Then
            删除.Enabled = True
        End If
    End Sub


    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub




    '新建按钮事件
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        Dim i As Byte = 0             '声明局部变量
        myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员",
        "确认判定", "路径", "备注说明"}
        For i = 0 To UBound(myArray)  '清空简单控件值
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
        GroupBox1.Controls(myArray(10).ToString).Text = 1
        '客户.SelectedIndex = 0  '默认选择第一项

        '检查ID.Enabled = False      '设置禁止使用控件
    End Sub


    '添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        检查ID.Enabled = True
        Dim intMaxID As Integer     '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字'Declare local variables and objects..  
        Dim strID As String = ""    '变量用来存储authors表的主键并设置authors表的新键
        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个新的查询.
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..'从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 合格检信息 ORDER BY 检查ID DESC", objConnection1th)  '存贮当前记录位置给变量  Save the current record position..
        objConnection1th.Open()   '打开数据库连接 Open the connection, execute the command SELECT TOP 1 * FROM 表名 ORDER BY 排序字段 DESC
        Dim maxId As Object = maxIdCommand.ExecuteScalar()  '调用SqlCommand的一个执行方法(只返回一行一列).并把结果赋值给变量
        If maxId Is DBNull.Value Then                       '如果返回结果是空值那么执行    If the MaxID column is null..
            intMaxID = 1000000                                 '设置一个默认值1000.Set a default value of 1000..
        Else
            strID = CType(maxId, String)                    '否则执行将maxId换成String型.strId.otherwise set the strID variable to the value in MaxID..
            intMaxID = CType(strID.Remove(0, 2), Integer)   '利用Remove方法删除sb前缀,转换整型赋值给变量intMaxID.Get the integer part of the string..
            intMaxID += 1                                   '变量加1.Increment the value..
        End If
        '变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..'strID = "SB" & intMaxID.ToString
        '变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..
        Select Case Len(intMaxID.ToString)

            Case 1
                strID = "XL00000" & intMaxID.ToString
            Case 2
                strID = "XL0000" & intMaxID.ToString
            Case 3
                strID = "XL000" & intMaxID.ToString
            Case 4
                strID = "XL00" & intMaxID.ToString
            Case 5
                strID = "XL0" & intMaxID.ToString

            Case Else
                    strID = "XL" & intMaxID.ToString

        End Select

        objCommand.Connection = objConnection1th '设置命令对象的属性 Set the SqlCommand object properties..'将连接字符串的连接对象赋值给Connection属性
        'objConnection1th.Open()




        'myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员","确认判定", "路径", "备注说明"}
        'objCommand.CommandText = "INSERT INTO 不良品信息 " &
        '"(管理编号, 发生日期, 客户, 供应商, 产品规格, 加工设备, 发现过程, 不良类型, 操作者, 类型区分, 不良数量, 完成工序, 加工费用, 材料费用, 损失成本, 不良现象及原因) " &
        '"VALUES(@管理编号, @发生日期, @客户, @供应商, @产品规格, @加工设备, @发现过程, @不良类型, @操作者, @类型区分, @不良数量, @完成工序, @加工费用, @材料费用, @损失成本, @不良现象及原因)"
        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列..'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the 'CommandText property..Parameter for the title_id column..
        objCommand.CommandText = "INSERT INTO 合格检信息 " &
            "(检查ID, 产品编号, 客户, 型号, 类型区分, 工序ID, 项目, 检查区域, 检查日期, 检验员, 确认判定, 路径, 备注说明) " &
            "VALUES(@检查ID, @产品编号, @客户, @型号, @类型区分, @工序ID, @项目, @检查区域, @检查日期, @检验员, @确认判定, @路径, @备注说明)"
        objCommand.Parameters.AddWithValue("@检查ID", strID)          '指定参数写入值,下同.
        objCommand.Parameters.AddWithValue("@产品编号", 产品编号.Text)
        objCommand.Parameters.AddWithValue("@客户", 客户.Text)
        objCommand.Parameters.AddWithValue("@型号", 型号.Text)
        objCommand.Parameters.AddWithValue("@类型区分", 类型区分1.Text)
        objCommand.Parameters.AddWithValue("@工序ID", 工序ID.Text)
        objCommand.Parameters.AddWithValue("@项目", 项目.Text)
        objCommand.Parameters.AddWithValue("@检查区域", 检查区域.Text)
        objCommand.Parameters.AddWithValue("@检查日期", 检查日期.Text).DbType = DbType.Date '转换日期类型
        objCommand.Parameters.AddWithValue("@检验员", 检验员.Text)
        objCommand.Parameters.AddWithValue("@确认判定", 确认判定.Checked).DbType = DbType.Boolean '试试可不可以删
        objCommand.Parameters.AddWithValue("@路径", 路径.Text)
        objCommand.Parameters.AddWithValue("@备注说明", 备注说明.Text)

        '........强制输入文本框保留
        'For i = 0 To UBound(myArray)
        '    If myArray(i).ToString <> "维修单号" Then   '如果名称只要不是维修单号,那么要执行.
        '        If GroupBox1.Controls(myArray(i).ToString).Text.Length = 0 Then MsgBox("请输入完整数据在添加数据") : _
        '            新建_Click(Nothing, Nothing) : objConnection1th.Close() : Exit Sub
        '    End If
        'Next i
        Try                               '截取异常'执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..
            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
        Catch SqlExceptionErr As OleDbException         '声明异常类型
            MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示异常类型错误信息
        End Try                                         '结束截取
        objConnection1th.Close()                        '关闭数据库连接 Close the connection..
        J01_产品合格检信息确认_Load(Nothing, Nothing)         '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        objCurrencyManager.Position = objCurrencyManager.Count - 1   '设置你保存的那个记录位置    Set the record position to the one that you saved..
        ShowPosition()                                               '标签显示位置.
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'strGetData = "SELECT 合格检信息.* FROM 合格检信息 ORDER BY 检查ID"

        'J01_产品合格检信息确认_Load(Nothing, Nothing)

        排序字段.SelectedIndex = 0
        查询条件.Text = strID

        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        执行查询_Click(Nothing, Nothing)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        'btnReseting_Click(Nothing， Nothing)
        'strGetData = "SELECT 合格检信息.* FROM 合格检信息 WHERE " & 检查ID.Text & " LIKE '%" & 查询条件.Text & "%' ORDER BY 检查ID"






        检查ID.Enabled = False




        ToolStripLabel1.Text = "Record Added"    '状态栏显示你添加的信息   Display a message that the record was added..


    End Sub
    Sub 关联工序生成()
        Dim arrProceesCount As String(), bytCounter As Byte, a As String = 客户.Text
        Dim b As String = 产品编号.Text, c As String = 类型区分1.Text, d As Date = Now, e As String = ""
        Dim f As Boolean = 确认判定.Checked
        For j = 0 To 工序ID.Items.Count - 1
            bytCounter = bytCounter + 1
            ReDim Preserve arrProceesCount(0 To bytCounter - 1)
            arrProceesCount(j) = 工序ID.Items(j).ToString
        Next


        For k = 0 To bytCounter - 1
            客户.Text = a
            产品编号.Text = b
            类型区分1.Text = c
            检查日期.Text = d
            检验员.Text = e
            确认判定.Checked = f







            工序ID.Text = arrProceesCount(k).ToString
            工序ID_SelectedIndexChanged(Nothing, Nothing)
            添加_Click(Nothing, Nothing)
        Next
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnConnectProcess.Click

        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件关联,防止事件的干扰









        关联工序生成()

        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '程序结束前重新绑定事件

    End Sub

    '更新数据库
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        '声明一个局部变量和创建一个命令对象  Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        查询条件.Text = 检查ID.Text

        intPosition = objCurrencyManager.Position  '当前记录位置赋值给变量intPosstion. Save the current record position..
        objCommand.Connection = objConnection1th '设置命令对象一些属性 Set the SqlCommand object properties..
        'SQL语句表示按照指定条件,更新表字段..
        'myArray = {"检查ID", "产品编号", "客户", "型号", "类型区分", "工序ID", "项目", "检查区域", "检查日期", "检验员","确认判定", "路径", "备注说明"}
        ' '接着使用SQL字符串设置CommandText属性.
        objCommand.CommandText = "UPDATE 合格检信息 " &
                "SET 产品编号 = @产品编号,客户 = @客户,型号 = @型号,类型区分 = @类型区分,
工序ID = @工序ID,项目 = @项目,检查区域 = @检查区域,检查日期 = @检查日期,检验员 = @检验员,确认判定 = @确认判定,
路径 = @路径,备注说明 = @备注说明 WHERE 检查ID = @检查ID"
        objCommand.CommandType = CommandType.Text '命令类型为默认CommandType.Text类型,可以省略
        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the
        ' CommandText property..
        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
        objCommand.Parameters.AddWithValue("@产品编号", 产品编号.Text)
        objCommand.Parameters.AddWithValue("@客户", 客户.Text)
        objCommand.Parameters.AddWithValue("@型号", 型号.Text)
        objCommand.Parameters.AddWithValue("@类型区分", 类型区分1.Text)
        objCommand.Parameters.AddWithValue("@工序ID", 工序ID.Text)
        objCommand.Parameters.AddWithValue("@项目", 项目.Text)
        objCommand.Parameters.AddWithValue("@检查区域", 检查区域.Text)
        objCommand.Parameters.AddWithValue("@检查日期", 检查日期.Text).DbType = DbType.Date  '转换类型.
        objCommand.Parameters.AddWithValue("@检验员", 检验员.Text)
        objCommand.Parameters.AddWithValue("@确认判定", 确认判定.Checked).DbType = DbType.Boolean  '转换类型.
        objCommand.Parameters.AddWithValue("@路径", 路径.Text)
        objCommand.Parameters.AddWithValue("@备注说明", 备注说明.Text)
        objCommand.Parameters.AddWithValue _
                ("@检查ID", BindingContext(objDataView).Current("检查ID"))
        objConnection1th.Open()    '打开带连接字符的数据库连接  Open the connection..
        objCommand.ExecuteNonQuery()   '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()    '关闭数据库连接  Close the connection..
        '排序字段.SelectedIndex = 1
        '查询条件.Text = 产品编号.Text




        J01_产品合格检信息确认_Load(Nothing, Nothing) '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
        objCurrencyManager.Position = intPosition   ' 设置你保存过的记录位置 Set the record position to the one that you saved..
        ShowPosition() '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        '显示状态信息  Display a message that the record was updated..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        排序字段.SelectedIndex = 0

        执行查询_Click(Nothing, Nothing)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Updated"
    End Sub

    '删除记录
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        '定义一个局部变量和命令对象 Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        '保存当前记录位置-1以用来记录删除位置.  Save the current record position—1 for the one to be
        ' deleted..
        intPosition = Me.BindingContext(objDataView).Position - 1
        If intPosition < 0 Then  '如果没有记录,则设置记录位置为0.    If the position is less than 0 set it to 0..
            intPosition = 0
        End If
        objCommand.Connection = objConnection1th      '设置命令对象属性 Set the Command object properties..
        objCommand.CommandText = "DELETE FROM 合格检信息 " &
                "WHERE 检查ID = @检查ID"
        '给ID字段提供相应的参数  Parameter for the ID field..
        objCommand.Parameters.AddWithValue _
            ("@检查ID", BindingContext(objDataView).Current("检查ID"))
        objConnection1th.Open()     '打开数据库连接 Open the database connection..
        objCommand.ExecuteNonQuery()     '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()         '关闭数据库连接 Close the connection..
        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        J01_产品合格检信息确认_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition
        ShowPosition()  '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        '显示一个已删除的信息.  Display a message that the record was deleted..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        'ToolStripLabel1.Text = "Record Deleted"
        '删除.Enabled = False
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
        Globals.Ribbons.Ribbon1.btnQcChecked.Enabled = True
        Me.Close()
    End Sub

    '关闭
    Private Sub J01_产品合格检信息确认_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing         '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btnQcChecked.Enabled = True
    End Sub

    'Private Sub 客户_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    '不对数据库操作,不需要执行SQL语句
    '    'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
    '    工序信息()
    '    objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 WHERE 客户 = " & "'" & 客户.Text & "'"  '写入SQL语句
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx2019042701")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb2019042701 As DataTable = objDataSet1th.Tables("wpxx2019042701") '声明一个表类型,并赋值给该变量.
    '    型号.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
    '    For inCounter = 0 To tb2019042701.Rows.Count - 1               '在表行数上循环
    '        型号.Items.Add(tb2019042701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next
    '    objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "区分" & " from " & "物品信息 WHERE ((客户 = " & "'" & 客户.Text & "') and (产品规格='" & 型号.Text & "'))" '" & 型号.Text & "'" '写入SQL语句
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx191207")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb191207 As DataTable = objDataSet1th.Tables("wpxx191207") '声明一个表类型,并赋值给该变量.


    '    类型区分.Items.Clear()
    '    For inCounter = 0 To tb191207.Rows.Count - 1               '在表行数上循环
    '        类型区分.Items.Add(tb191207.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next

    '    objDataAdapter1th.SelectCommand.CommandText = "SELECT 工序ID FROM 工序信息  WHERE ((客户 = " & "'" & 客户.Text & "') and (产品规格='" & 型号.Text & "') and (区分='" & 类型区分.Text & "')) ORDER BY 工序ID"  'ORDER BY 工序ID"
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx191207")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb19120701 As DataTable = objDataSet1th.Tables("wpxx191207") '声明一个表类型,并赋值给该变量.
    '    工序ID.Items.Clear()
    '    For inCounter = 0 To tb19120701.Rows.Count - 1               '在表行数上循环
    '        工序ID.Items.Add(tb19120701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next

    'End Sub
    'Private Sub 类型区分_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    工序信息()
    '    objDataAdapter1th.SelectCommand.CommandText = "SELECT 工序ID FROM 工序信息  WHERE ((客户 = " & "'" & 客户.Text & "') and (产品规格='" & 型号.Text & "') and (区分='" & 类型区分.Text & "')) ORDER BY 工序ID"  'ORDER BY 工序ID"
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx191207")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb19120701 As DataTable = objDataSet1th.Tables("wpxx191207") '声明一个表类型,并赋值给该变量.
    '    工序ID.Items.Clear()
    '    For inCounter = 0 To tb19120701.Rows.Count - 1               '在表行数上循环
    '        工序ID.Items.Add(tb19120701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next

    'End Sub

    'Private Sub 型号_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    '不对数据库操作,不需要执行SQL语句
    '    工序信息()
    '    objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "区分" & " from " & "物品信息 WHERE ((客户 = " & "'" & 客户.Text & "') and (产品规格='" & 型号.Text & "'))" '" & 型号.Text & "'" '写入SQL语句
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx191207")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb191207 As DataTable = objDataSet1th.Tables("wpxx191207") '声明一个表类型,并赋值给该变量.
    '    类型区分.Items.Clear()
    '    For inCounter = 0 To tb191207.Rows.Count - 1               '在表行数上循环
    '        类型区分.Items.Add(tb191207.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next

    '    objDataAdapter1th.SelectCommand.CommandText = "SELECT 工序ID FROM 工序信息  WHERE ((客户 = " & "'" & 客户.Text & "') and (产品规格='" & 型号.Text & "') and (区分='" & 类型区分.Text & "')) ORDER BY 工序ID"  'ORDER BY 工序ID"
    '    objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
    '    objDataAdapter1th.Fill(objDataSet1th, "wpxx191207")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
    '    Dim tb19120701 As DataTable = objDataSet1th.Tables("wpxx191207") '声明一个表类型,并赋值给该变量.
    '    工序ID.Items.Clear()
    '    For inCounter = 0 To tb19120701.Rows.Count - 1               '在表行数上循环
    '        工序ID.Items.Add(tb19120701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
    '    Next
    'End Sub


    Private Sub 检查日期检查日期_GotFocus(sender As Object, e As EventArgs) Handles 检查日期.GotFocus
        'Dim strDate As String
        检查日期.Mask = "0000/00/00"
    End Sub


    Private Sub 检查日期_LostFocus(sender As Object, e As EventArgs) Handles 检查日期.LostFocus
        Dim strDate As String
        strDate = 检查日期.Text
        检查日期.Mask = ""
        检查日期.Text = strDate
    End Sub

    Private Sub 检查日期_TextChanged(sender As Object, e As EventArgs) Handles 检查日期.TextChanged
        Dim strStrogeValue
        If Len(检查日期.Text) = 10 Then strStrogeValue = 检查日期.Text : 检查日期_LostFocus(Nothing, Nothing)
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'xlapp.Range("a1").Value = 产品编号.Text
        'xlapp.ActiveSheet.Shapes(bytShapesNumber).Name = Split(arrArray(0), ".")(0) & "."
        Dim s As String
        For i = 1 To 10
            s = Split(产品编号.Text, "\")(i - 1)
            xlapp.Range("a" & i).Value = Trim(s)
            'xlapp.Range("a1").Value = Trim(产品编号.Text)
        Next
    End Sub



    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        On Error Resume Next    '出错继续执行下一句代码
        Dim strRecordSpace As String = "", rngRng As Excel.Range, strRecord As String, bookPath As String = xlapp.ActiveWorkbook.FullName '.strRangeAddress As String,
        Dim objFso As Object, rngTargetRange As Excel.Range, aryTittle As String()
        aryTittle = {"检查ID:", "产品编号:", "客户:", "型号:", "类型区分:", "工序ID:", "项目:", "检查区域:", "检查日期:", "检验员:",
            "确认判定:", "路径:", "备注说明:"}
        Dim bytCounter As Byte = 0
        'xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
        objFso = CreateObject("scripting.filesystemobject")   '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists("C:\MakeQRE") = True Then '如果存在指定的文件夹,那么执行
            'objFso.DeleteFolder("C:\MakeQRE")             '删除名为CopyOption的文件夹
            'MkDir("C:\MakeQRE")                          '创建指定文件夹
        Else
            MkDir("C:\MakeQRE")                          '创建指定文件夹
        End If


        'xlapp.ActiveSheet.Shapes(bytShapesNumber).Name = Split(arrArray(0), ".")(0) & "."
        Dim strSplitData As String(), intCounter1 As Byte = 0
        For i = 1 To 5 Step 2
            intCounter1 = intCounter1 + 1

            ReDim Preserve strSplitData(0 To intCounter1 - 1)   '重置一维数组上标
            strSplitData(intCounter1 - 1) = Trim(Split(产品编号.Text, "\")(i))  '在一维数组中写入值 

            'ReDim Preserve strSplitData(0 To )
            'strSplitData() = Split(产品编号.Text, "\")(i - 1)
            'xlapp.Range("a" & i).Value = Trim(s)
            'xlapp.Range("a1").Value = Trim(产品编号.Text)
        Next


        'strRangeAddress = xlapp.Selection.address
        'rngRng = xlapp.InputBox("请指定随机数区域", "区域", strRangeAddress, , , , , 8) '弹出一个输入框让用户选择区域
        'txtAddress.Text = rngRng.Address
        'rngRng.Select()

        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
        '管理编号 LIKE '%" & 查询条件.Text & "%'

        objDataAdapter1th.SelectCommand.CommandText = "SELECT 合格检信息.* FROM 合格检信息 WHERE 产品编号 LIKE '%" & strSplitData(0).ToString & "%' OR 产品编号 LIKE '%" & strSplitData(1).ToString & "%' OR 产品编号 LIKE '%" & strSplitData(2).ToString & "%' ORDER BY 检查ID"  '写入SQL语句
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx191208")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb3 As DataTable = objDataSet1th.Tables("wpxx191208") '声明一个表类型,并赋值给该变量.
        'MsgBox(tb3.Rows.Count & vbCrLf & tb3.Columns.Count & vbCrLf & tb3.Rows(3).Item(10).ToString)

        '因素确定.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
        '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
        '不良类型.Items.Add("客户发现不良")
        For inCounter = 0 To tb3.Rows.Count - 1               '在表行数上循环
            '因素确定.Items.Add(tb3.Rows(inCounter).Item(2).ToString)   '添加项目值为记录字段所对应的值
        Next

        Dim bytRow As Byte = tb3.Rows.Count - 1, bytColumn As Byte = tb3.Columns.Count - 1
        For i = 0 To bytRow
            If tb3.Rows(i).Item(10).ToString = "False" Then MsgBox("禁止张贴合格二维码标签,产品有检查项目未确认,检查工序ID:" & vbCrLf & tb3.Rows(i).Item(0).ToString & "") : Exit Sub
            If tb3.Rows(i).Item(4).ToString = "Assembly" Then
                For j = 0 To bytColumn
                    strRecord = strRecord & vbCrLf & aryTittle(j) & tb3.Rows(i).Item(j).ToString
                Next
            End If
        Next
        'MsgBox(strRecord)

        'For Each rngTargetRange In rngRng
        '    strRecordSpace = strRecordSpace & rngTargetRange.Value & " "
        '    bytCounter = bytCounter + 1
        'Next

        Dim btmBtm As Bitmap = MakeQRE(strRecord, , , 1)
        If My.Computer.FileSystem.FileExists(strShareStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp") = True Then Kill(strShareStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp") '如果存在指定的文件夹,那么执行  Kill()
        btmBtm.Save（strShareStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp"）




        路径.Text = strShareStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp"

        '判定是否存在文件,并复制转移本地
        If My.Computer.FileSystem.FileExists(myCompanyStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp") = True Then Kill(myCompanyStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp") '如果存在指定的文件夹,那么执行  Kill()
        System.IO.File.Copy(strShareStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp", myCompanyStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp")
        PictureBox1.Image = Image.FromFile(myCompanyStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp")

        xlapp.Workbooks(bookPath).Activate()

        xlapp.ActiveWorkbook.Save()
        '只操作文件，不对文件夹处理...
        'strDirName = Dir("C:\Option" & "\*.*")                 '获取文件的名称(可能存在文件夹)...
        'xlapp.ActiveWorkbook.Save()
        'xlapp.ScreenUpdating = True    '关闭屏幕更新闪烁
        'xlapp.ActiveWorkbook.Close()
        'xlapp.Workbooks(bookPath).Close()
        'xlapp.Workbooks.Open(bookPath)
        'Me.Close()

    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Dim strSplitData As String(), intCounter1 As Byte = 0
        For i = 1 To 5 Step 2
            intCounter1 = intCounter1 + 1
            ReDim Preserve strSplitData(0 To intCounter1 - 1)   '重置一维数组上标
            strSplitData(intCounter1 - 1) = Trim(Split(产品编号.Text, "\")(i))  '在一维数组中写入值 
        Next
        xlapp.ActiveSheet.Shapes.AddPicture(strShareStorge & strSplitData(0).ToString & "_" & 工序ID.Text & ".bmp", False, True, 100, 100, 100, 100)
        xlapp.ActiveSheet.Shapes(xlapp.ActiveSheet.Shapes.count).name = strSplitData(0).ToString & "_" & 工序ID.Text
        xlapp.ActiveSheet.Shapes（strSplitData(0).ToString & "_" & 工序ID.Text）.select




        'Dim strSplitData As String(), intCounter1 As Byte = 0
        'For i = 1 To 5 Step 2
        '    intCounter1 = intCounter1 + 1
        '    ReDim Preserve strSplitData(0 To intCounter1 - 1)   '重置一维数组上标
        '    strSplitData(intCounter1 - 1) = Trim(Split(产品编号.Text, "\")(i))  '在一维数组中写入值 
        'Next
        'Dim strName As String, rngSlecRange As Excel.Range, intShapLeft As Integer, intShapTop As Integer, intShapWide As Integer, intShapHeight As String
        ''判定是否选择了单元格,如果是,那么提示选择图片,并退出程序.
        'If TypeName(xlapp.Selection) = "Range" Then MsgBox("先选择图片,在运行此功能键") : Exit Sub '判定是否选中图片
        'strName = xlapp.Selection.ShapeRange.Name                 '给变量赋值为图片名称
        'rngSlecRange = xlapp.InputBox("请选择单元格：", Type:=8) '选择单元格
        ''分别设置单元格上、左边距及高度和宽度.需要注意的是,单元格可能是合并单元格,所以,这里不管是否为合并单元格,统一当成合并单元格设置.
        'intShapLeft = rngSlecRange.MergeArea.Left    '获取单元格区域的左边距()
        'intShapTop = rngSlecRange.MergeArea.Top      '获取单元格区域的上边距
        'intShapWide = rngSlecRange.MergeArea.Width   '获取单元格区域的宽度
        'intShapHeight = rngSlecRange.MergeArea.Height            '获取单元格区域的高度
        'xlapp.ActiveSheet.Shapes(strName).Select        '重新选择图片

        'xlapp.Selection.ShapeRange.LockAspectRatio = 0          '使图片不锁定比例


        'xlapp.Selection.ShapeRange.Height = intShapHeight - 6   '移动图片与选择的单元格高度距离相等
        'xlapp.Selection.ShapeRange.Width = intShapWide - 6      '移动图片与选择的单元格宽度距离相等

        'xlapp.Selection.ShapeRange.Left = intShapLeft + 3       '移动图片与选择的单元格左边距离
        'xlapp.Selection.ShapeRange.Top = intShapTop + 3         '移动图片与选择的单元格上部距离相等

        'With xlapp.Selection                                    '定义图片大小位置随单元格变化而变化
        '    .Placement = 1
        'End With
        'rngSlecRange.Offset(-1, 0).Value = 客户.Text & ":" & 型号.Text
        'rngSlecRange.Offset(2, 0).Value = "成品:" & strSplitData(0).ToString
        'rngSlecRange.Offset(4, 0).Value = "IN:" & strSplitData(1).ToString
        'rngSlecRange.Offset(6, 0).Value = "OT:" & strSplitData(2).ToString



    End Sub






    Private Sub btnReseting_Click(sender As Object, e As EventArgs) Handles btnReseting.Click
        查询条件.Text = ""
        PictureBox1.Image = Nothing

    End Sub

    Private Sub 产品编号_GotFocus(sender As Object, e As EventArgs) Handles 产品编号.GotFocus
        If 类型区分1.Text = "Assembly" Then
            产品编号.Mask = "\A\ssebly\\AAAAAAAAAA\\IN\\AAAAAAAA\\OT\\AAAAAAAA"
        Else

            产品编号.Mask = ""
        End If

    End Sub

    Private Sub 产品编号_LostFocus(sender As Object, e As EventArgs) Handles 产品编号.LostFocus
        Dim strNumber As String
        strNumber = 产品编号.Text
        产品编号.Mask = ""
        产品编号.Text = strNumber
    End Sub

    Shared Function MakeQRE(ByVal qrtext As String, Optional ByVal width As Integer = 800, Optional ByVal height As Integer = 800, Optional ByVal margin As Integer = 1) As Bitmap
        Dim writer As New ZXing.BarcodeWriter       '新建一个图像智能类
        writer.Format = ZXing.BarcodeFormat.QR_CODE         '智能类图像格式设置为二维码
        Dim opt As New ZXing.QrCode.QrCodeEncodingOptions   '创建一个二维码操作对象
        opt.DisableECI = True      '设置为True才可以调整编码
        opt.CharacterSet = "UTF-8" '文本编码，建议设置为UTF-8,手机也可以扫.默认为ISO-8859-1英文字符集，但一般移动设备常用UTF-8字符集编码
        opt.Width = width   '宽度
        opt.Height = height '高度
        opt.Margin = margin  '边距，貌似不是像素格式，因此不宜设置过大
        writer.Options = opt   '设置用于编码的选项容器
        Return writer.Write(qrtext) '内容写入智能类
    End Function

    Private Sub 定位_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim strSplitData As String(), intCounter1 As Byte = 0
        For i = 1 To 5 Step 2
            intCounter1 = intCounter1 + 1
            ReDim Preserve strSplitData(0 To intCounter1 - 1)   '重置一维数组上标
            strSplitData(intCounter1 - 1) = Trim(Split(产品编号.Text, "\")(i))  '在一维数组中写入值 
        Next
        Dim strName As String, rngSlecRange As Excel.Range, intShapLeft As Integer, intShapTop As Integer, intShapWide As Integer, intShapHeight As String
        '判定是否选择了单元格,如果是,那么提示选择图片,并退出程序.
        If TypeName(xlapp.Selection) = "Range" Then MsgBox("先选择图片,在运行此功能键") : Exit Sub '判定是否选中图片
        strName = xlapp.Selection.ShapeRange.Name                 '给变量赋值为图片名称
        rngSlecRange = xlapp.InputBox("请选择单元格：", Type:=8) '选择单元格
        '分别设置单元格上、左边距及高度和宽度.需要注意的是,单元格可能是合并单元格,所以,这里不管是否为合并单元格,统一当成合并单元格设置.
        intShapLeft = rngSlecRange.MergeArea.Left    '获取单元格区域的左边距()
        intShapTop = rngSlecRange.MergeArea.Top      '获取单元格区域的上边距
        intShapWide = rngSlecRange.MergeArea.Width   '获取单元格区域的宽度
        intShapHeight = rngSlecRange.MergeArea.Height            '获取单元格区域的高度
        xlapp.ActiveSheet.Shapes(strName).Select        '重新选择图片

        xlapp.Selection.ShapeRange.LockAspectRatio = 0          '使图片不锁定比例


        xlapp.Selection.ShapeRange.Height = intShapHeight - 6   '移动图片与选择的单元格高度距离相等
        xlapp.Selection.ShapeRange.Width = intShapWide - 6      '移动图片与选择的单元格宽度距离相等

        xlapp.Selection.ShapeRange.Left = intShapLeft + 3       '移动图片与选择的单元格左边距离
        xlapp.Selection.ShapeRange.Top = intShapTop + 3         '移动图片与选择的单元格上部距离相等

        With xlapp.Selection                                    '定义图片大小位置随单元格变化而变化
            .Placement = 1
        End With
        Dim rngMoveRange As Excel.Range
        rngSlecRange(1).Offset(-1, 0).Value = 客户.Text & ":" & 型号.Text & "-" & 检查区域.Text & "检合格"
        rngMoveRange = rngSlecRange(1).Offset(0, 1)
        'MsgBox(rngMoveRange.Address)

        rngMoveRange.Value = "成品:" & strSplitData(0).ToString
        rngMoveRange.Offset(1, 0).Value = "IN:" & strSplitData(1).ToString
        rngMoveRange.Offset(3, 0).Value = "OT:" & strSplitData(2).ToString


    End Sub

    Private Sub 工序ID_Click(sender As Object, e As EventArgs) Handles 工序ID.Click
        objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
        objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 工序ID FROM 工序信息 ORDER BY 工序ID"  'ORDER BY 工序ID"
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx191207")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb19120701 As DataTable = objDataSet1th.Tables("wpxx191207") '声明一个表类型,并赋值给该变量.
        工序ID.Items.Clear()
        For inCounter = 0 To tb19120701.Rows.Count - 1               '在表行数上循环
            工序ID.Items.Add(tb19120701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next

        工序信息()
    End Sub

    Private Sub grdAuthorTitles1_Click(sender As Object, e As EventArgs) Handles grdAuthorTitles1.Click
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.CurrentRow.Index  '视图控件指针选择指定行第一个单元格
        工序ID.SelectedIndex = grdAuthorTitles1.CurrentRow.Index  '视图控件指针选择指定行第一个单元格


    End Sub

    'Private Sub grdAuthorTitles1_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles1.SelectionChanged
    '    'On Error Resume Next
    '    Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index
    '    BindFields()
    '    objCurrencyManager.Position = intPosition
    '    ShowPosition()
    'End Sub

















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



End Class