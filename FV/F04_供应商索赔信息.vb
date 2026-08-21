
Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
' myArray = {"ID", "统计始日", "统计止日", "赔偿单号", "供应商", "图号", "区分", "数量", "加工费扣", "毛坯费扣", "毛坯抵扣", "供应商确认", "补货状态", "赔偿表证明",  "备注"}

Public Class F04_供应商索赔信息

    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim objConnection1th As New OleDbConnection _
      ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\不良品信息管理.accdb")  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\不良品信息管理\不良品信息管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\不良品信息管理\不良品信息管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\不良品信息管理.accdb")  '公司共享盘
    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '这个构造函数使我们不必写数据适配器Adapter属性SelectCommand相关代码.已经加入相关参数(SQL语句)
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 索赔信息.* FROM 索赔信息 ORDER BY ID", objConnection1th)
    Dim objDataAdapter1th As New OleDbDataAdapter()  '该构造函数需要使用数据适配器(OleDbDataAdapter)的SelectCommand属性.用来填充履历卡数据的
    Dim objDataSet As New DataSet()     '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet()  '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView         '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objDataView1th As DataView      '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager   '声明作用域为类级的对象,CurrencyManger对象用于控制绑定数据的移动;作为管理Binding(相当于视图容器)对象的列表
    Dim myArray() As String                       '声明数组变量,数组长度为要引用的数据表字段数量.
    'Dim strDate1 As String
    'Dim strReduceMonery1 As String
    '创建一个过程,将在Load事件(初始化代码)调用,并用来填充数据和显示数据..

    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '调用模块级对象,并重新初始化该(DataSet)对象
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
        myArray = {"ID", "统计始日", "统计止日", "赔偿单号", "供应商", "图号", "区分", "数量", "加工费扣", "毛坯费扣",
            "毛坯抵扣", "供应商确认", "补货状态", "赔偿表证明", "备注"}

        For i = 0 To UBound(myArray)
            '清除所有绑定的数据记录
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        '控件重新逐一绑定DateView数据源,add方法第一参数为要绑定的控件属性的名称,第二参数为要绑定的数据源,
        '第三参数为要绑定给控件的数据字段(列表).
        For i = 0 To UBound(myArray)
            If GroupBox1.Controls(myArray(i).ToString).Name = "毛坯抵扣" Or GroupBox1.Controls(myArray(i).ToString).Name = "供应商确认" Or
                GroupBox1.Controls(myArray(i).ToString).Name = "补货状态" Then
                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Checked", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            Else
                GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            End If

            'GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "统计始日" Or GroupBox1.Controls(myArray(i).ToString).Name = "统计止日" Then GroupBox1.Controls(myArray(i).ToString).Text _
                    = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.


        Next i

        AddHandler 补货状态.CheckedChanged, AddressOf 补货状态_CheckedChanged      '绑定事件
        AddHandler 毛坯抵扣.CheckedChanged, AddressOf 毛坯抵扣_CheckedChanged      '绑定事件

        ToolStripLabel1.Text = "Ready"  '显示一个"只读"状态..
    End Sub

    '创建过程,并显示当前单个记录的位置.
    Private Sub ShowPosition()
        Try  '格式化日期指定短日期格式.
            统计始日.Text = Format(CType(GroupBox1.Controls("统计始日").Text, Date), "yyyy/MM/dd") '定义格式
            统计止日.Text = Format(CType(GroupBox1.Controls("统计止日").Text, Date), "yyyy/MM/dd") '定义格式
        Catch e As System.Exception   '声明一个错误变量类型
            '如果异常(文本框为空),那么转换当前日期类型为文本类型,并写入文本框中.
            GroupBox1.Controls("统计始日").Text = CType(Now, String)
            统计始日.Text = Format(CType(GroupBox1.Controls("统计始日").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
            GroupBox1.Controls("统计止日").Text = CType(Now, String)
            统计始日.Text = Format(CType(GroupBox1.Controls("统计止日").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
        End Try
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
        " of " & objCurrencyManager.Count() '显示当前记录位置,并标记记录数. 




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
        '控件绑定的CurrencyManager对象与objDataView对象数据相等,通过CurrencyManager对象指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
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
        'AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件.
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '绑定事件.
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
        'If 查询条件.Text <> "" Then grdAuthorTitles.Rows(0).Selected = True 'CurrentCell 
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

    '加载窗体触发事件
    Private Sub F04_供应商索赔信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '需要说明的是,Fill方法会执行命令(SelectCommand),其Connection属性保持为调用该方法时的状态.
        On Error Resume Next
        RemoveHandler 补货状态.CheckedChanged, AddressOf 补货状态_CheckedChanged
        RemoveHandler 毛坯抵扣.CheckedChanged, AddressOf 毛坯抵扣_CheckedChanged

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
        grdAuthorTitles.Columns(0).HeaderText = "ID"   '设置控件列标题   
        grdAuthorTitles.Columns(1).HeaderText = "统计始日"
        grdAuthorTitles.Columns(2).HeaderText = "统计止日"
        grdAuthorTitles.Columns(3).HeaderText = "赔偿单号"
        grdAuthorTitles.Columns(4).HeaderText = "供应商"
        grdAuthorTitles.Columns(5).HeaderText = "图号"
        grdAuthorTitles.Columns(6).HeaderText = "区分"
        grdAuthorTitles.Columns(7).HeaderText = "数量"
        grdAuthorTitles.Columns(8).HeaderText = "加工费扣"
        grdAuthorTitles.Columns(9).HeaderText = "毛坯费扣"
        grdAuthorTitles.Columns(10).HeaderText = "毛坯抵扣"
        grdAuthorTitles.Columns(11).HeaderText = "供应商确认"
        grdAuthorTitles.Columns(12).HeaderText = "补货状态"
        grdAuthorTitles.Columns(13).HeaderText = "赔偿表证明"
        grdAuthorTitles.Columns(14).HeaderText = "备注"
        grdAuthorTitles.Columns(14).Width = 530 '设置指定列默认宽度大一点


        ''改变字段标题名称和样式'Change column names and styles using the column name  
        grdAuthorTitles.Columns("加工费扣").HeaderCell.Value = "加工费扣除" '重新设置列标题的值显示为"描述"
        '标题重新调用列标题样式(之前设定的-居中右对齐)
        grdAuthorTitles.Columns("加工费扣").HeaderCell.Style = objAlignRightCellStyle
        '单元格内容重新调用样式(之前设定的-货币样式)
        grdAuthorTitles.Columns("加工费扣").DefaultCellStyle = objCurrencyCellStyle

        '单元格内容重新调用样式(之前设定的-货币样式)
        grdAuthorTitles.Columns("毛坯费扣").DefaultCellStyle = objCurrencyCellStyle


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

        For i As Integer = 0 To grdAuthorTitles.RowCount - 1                           '有一个空白行也算一行
            If CType(grdAuthorTitles.Item(11, i).Value.ToString(), Boolean) And CType(grdAuthorTitles.Item(12, i).Value.ToString(), Boolean) Then
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色

            Else
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
            End If
        Next

        objCurrencyCellStyle = Nothing     '清除样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)

        grdAuthorTitles.Columns(13).Visible = False


        排序字段.Items.Clear()   '给组合框添加项目  'Add items to the combo box..
        排序字段.Items.AddRange(myArray)
        排序字段.SelectedIndex = 0         '默认选择第一项

        '供应商.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        '供应商.Items.Add("荣程A") ： 供应商.Items.Add("新顺章B") ： 供应商.Items.Add("海陆C") ： 供应商.Items.Add("利元D") ： 供应商.Items.Add("广源E") ： 供应商.Items.Add("恒杰F")

        区分.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        区分.Items.Add("I/N") ： 区分.Items.Add("O/T") ： 区分.Items.Add("O/T1") ： 区分.Items.Add("O/T2")

        '维修类型.SelectedIndex = 0  '默认选择第一项

        objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
        objDataAdapter1th.SelectCommand.Connection = objConnection1th   '建立与数据库的连接
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
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "供应商" & " from " & "物品信息 ORDER BY 供应商" '写入SQL语句
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx04")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb1 As DataTable = objDataSet1th.Tables("wpxx04") '声明一个表类型,并赋值给该变量.
        供应商.Items.Clear()                               '清楚复合框项目集
        For inCounter = 0 To tb1.Rows.Count - 1               '在表行数上循环
            供应商.Items.Add(tb1.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next

        供应商_SelectedIndexChanged(Nothing, Nothing)


        ''objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct 不良类型" & " from " & "不良类型分类" '写入SQL语句
        'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        'objDataAdapter1th.Fill(objDataSet1th, "wpxx14")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        'Dim tb2 As DataTable = objDataSet1th.Tables("wpxx14") '声明一个表类型,并赋值给该变量.
        '不良类型.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        ''不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
        ''不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
        ''不良类型.Items.Add("客户发现不良")
        'For inCounter = 0 To tb2.Rows.Count - 1               '在表行数上循环
        '    不良类型.Items.Add(tb2.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
        BindFields()  '调用绑定控件过程

    End Sub

    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件数据源进行排序.
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性.
        Select Case 排序字段.SelectedIndex      'Determine the appropriate item selected and set the Sort property of the DataView object..            
            '  myArray = {"ID", "统计始日", "统计止日", "赔偿单号", "供应商", "图号", "区分", "数量", "加工费扣", "毛坯费扣", "毛坯抵扣", "供应商确认", "补货状态", "赔偿表证明",  "备注"}
            Case 0
                objDataView.Sort = "ID"   '按字段设备编号升序排序,下同.
            Case 1
                objDataView.Sort = "统计始日"
            Case 2
                objDataView.Sort = "统计止日"
            Case 3
                objDataView.Sort = "赔偿单号"
            Case 4
                objDataView.Sort = "供应商"
            Case 5
                objDataView.Sort = "图号"
            Case 6
                objDataView.Sort = "区分"
            Case 7
                objDataView.Sort = "数量"
            Case 8
                objDataView.Sort = "加工费扣"
            Case 9
                objDataView.Sort = "毛坯费扣"
            Case 10
                objDataView.Sort = "毛坯抵扣"
            Case 11
                objDataView.Sort = "供应商确认"
            Case 12
                objDataView.Sort = "补货状态"
            Case 13
                objDataView.Sort = "赔偿表证明"
            Case 14
                objDataView.Sort = "备注"
        End Select
        btnMoveFirst_Click(Nothing, Nothing)      '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted"   '修改状态标签Text属性. Display a message that the records have been sorted..
    End Sub

    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        ' myArray = {"ID", "统计始日", "统计止日", "赔偿单号", "供应商", "图号", "区分", "数量", "加工费扣", "毛坯费扣", "毛坯抵扣", "供应商确认", "补货状态", "赔偿表证明",  "备注"}
        Dim intPosition As Integer              '执行查找,声明当前局部变量.'Declare local variables.. 
        Dim str条件 As String = ""
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
        'Determine the appropriate item selected And set the Sort property of the DataView object..
        Select Case 排序字段.SelectedIndex
              '"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"
            Case 0
                objDataView.Sort = "ID"
                str条件 = "ID"
            Case 1
                objDataView.Sort = "统计始日"
                str条件 = "统计始日"

            Case 2
                objDataView.Sort = "统计止日"
                str条件 = "统计止日"
            Case 3
                objDataView.Sort = "赔偿单号"
                str条件 = "赔偿单号"
            Case 4
                objDataView.Sort = "供应商"
                str条件 = "供应商"
            Case 5
                objDataView.Sort = "图号"
                str条件 = "图号"
            Case 6
                objDataView.Sort = "区分"
                str条件 = "区分"
                '"不良类型", "操作者", "类型区分", "不良数量", "完成工序", "加工费用", "材料费用", "损失成本", "不良现象及原因"}
            Case 7
                objDataView.Sort = "数量"
                str条件 = "数量"
            Case 8
                objDataView.Sort = "加工费扣"
                str条件 = "加工费扣"
            Case 9
                objDataView.Sort = "毛坯费扣"
                str条件 = "毛坯费扣"
            Case 10
                objDataView.Sort = "毛坯抵扣"
                str条件 = "毛坯抵扣"
            Case 11
                objDataView.Sort = "供应商确认"
                str条件 = "供应商确认"
            Case 12
                objDataView.Sort = "补货状态"
                str条件 = "补货状态"
            Case 13
                objDataView.Sort = "赔偿表证明"
                str条件 = "赔偿表证明"
            Case 14
                objDataView.Sort = "备注"
                str条件 = "备注"

        End Select
        If str条件 = "统计始日" Or str条件 = "统计止日" Then
            objDataView.RowFilter = str条件 & "=#" & CType(查询条件.Text, Date).ToShortDateString & "#" '"Date = #12/31/2008 16:44:58#"
        ElseIf str条件 = "毛坯抵扣" Or str条件 = "供应商确认" Or str条件 = "补货状态" Then    'DataView数据表中筛选数据集(类似SQL语句).
            objDataView.RowFilter = str条件 & "=" & CType(查询条件.Text, Boolean)
        Else
            objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"
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
        If 查询条件.Text.Length = 0 Then  '如果是空值
            '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置..
            F04_供应商索赔信息_Load(Nothing, Nothing)
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
        myArray = {"ID", "统计始日", "统计止日", "赔偿单号", "供应商", "图号", "区分", "数量", "加工费扣", "毛坯费扣", "毛坯抵扣", "供应商确认", "补货状态", "赔偿表证明", "备注"}
        For i = 0 To UBound(myArray)  '清空简单控件值
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
        GroupBox1.Controls(myArray(10).ToString).Text = 1
        供应商.SelectedIndex = 0  '默认选择第一项
        图号.SelectedIndex = 0  '默认选择第一项
        区分.SelectedIndex = 0
        '管理编号.Enabled = False      '设置禁止使用控件
    End Sub

    '添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click

        Dim intMaxID As Integer     '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字'Declare local variables and objects..  
        Dim strID As String = ""    '变量用来存储authors表的主键并设置authors表的新键
        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个新的查询.
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..'从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 索赔信息 ORDER BY ID DESC", objConnection1th)  '存贮当前记录位置给变量  Save the current record position..
        objConnection1th.Open()   '打开数据库连接 Open the connection, execute the command SELECT TOP 1 * FROM 表名 ORDER BY 排序字段 DESC
        Dim maxId As Object = maxIdCommand.ExecuteScalar()  '调用SqlCommand的一个执行方法(只返回一行一列).并把结果赋值给变量
        If maxId Is DBNull.Value Then                       '如果返回结果是空值那么执行    If the MaxID column is null..
            intMaxID = 1000                                 '设置一个默认值1000.Set a default value of 1000..
        Else
            strID = CType(maxId, String)                    '否则执行将maxId换成String型.strId.otherwise set the strID variable to the value in MaxID..
            intMaxID = CType(strID.Remove(0, 2), Integer)   '利用Remove方法删除sb前缀,转换整型赋值给变量intMaxID.Get the integer part of the string..
            intMaxID += 1                                   '变量加1.Increment the value..
        End If
        '变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..'strID = "SB" & intMaxID.ToString
        '变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..
        Select Case Len(intMaxID.ToString)
            Case 1
                strID = "XL00" & intMaxID.ToString
            Case 2
                strID = "XL0" & intMaxID.ToString
            Case Else
                strID = "XL" & intMaxID.ToString
        End Select

        objCommand.Connection = objConnection1th '设置命令对象的属性 Set the SqlCommand object properties..'将连接字符串的连接对象赋值给Connection属性
        'objConnection1th.Open()

        排序字段.SelectedIndex = 0
        查询条件.Text = strID

        'myArray = {"ID", "统计始日", "统计止日", "赔偿单号", "供应商", "图号", "区分", "数量", "加工费扣", "毛坯费扣", "毛坯抵扣", "供应商确认", "补货状态", "赔偿表证明", "备注"}
        'objCommand.CommandText = "INSERT INTO 不良品信息 " &
        '"(管理编号, 发生日期, 客户, 供应商, 产品规格, 加工设备, 发现过程, 不良类型, 操作者, 类型区分, 不良数量, 完成工序, 加工费用, 材料费用, 损失成本, 不良现象及原因) " &
        '"VALUES(@管理编号, @发生日期, @客户, @供应商, @产品规格, @加工设备, @发现过程, @不良类型, @操作者, @类型区分, @不良数量, @完成工序, @加工费用, @材料费用, @损失成本, @不良现象及原因)"
        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列..'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the 'CommandText property..Parameter for the title_id column..
        objCommand.CommandText = "INSERT INTO 索赔信息 " &
            "(ID, 统计始日, 统计止日, 赔偿单号, 供应商, 图号, 区分, 数量, 加工费扣, 毛坯费扣, 毛坯抵扣, 供应商确认, 补货状态, 赔偿表证明, 备注) " &
            "VALUES(@ID, @统计始日, @统计止日, @赔偿单号, @供应商, @图号, @区分, @数量, @加工费扣, @毛坯费扣, @毛坯抵扣, @供应商确认, @补货状态, @赔偿表证明, @备注)"

        objCommand.Parameters.AddWithValue("@ID", strID)          '指定参数写入值,下同.
        objCommand.Parameters.AddWithValue("@统计始日", 统计始日.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@统计止日", 统计止日.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@赔偿单号", 赔偿单号.Text) '转换日期类型
        objCommand.Parameters.AddWithValue("@供应商", 供应商.Text)
        objCommand.Parameters.AddWithValue("@图号", 图号.Text)
        objCommand.Parameters.AddWithValue("@区分", 区分.Text)
        objCommand.Parameters.AddWithValue("@数量", 数量.Text).DbType = DbType.Byte
        objCommand.Parameters.AddWithValue("@加工费扣", 加工费扣.Text).DbType = DbType.Single
        objCommand.Parameters.AddWithValue("@毛坯费扣", 毛坯费扣.Text).DbType = DbType.Single
        objCommand.Parameters.AddWithValue("@毛坯抵扣", 毛坯抵扣.Checked).DbType = DbType.Boolean '试试可不可以删
        objCommand.Parameters.AddWithValue("@供应商确认", 供应商确认.Checked).DbType = DbType.Boolean '试试可不可以删
        objCommand.Parameters.AddWithValue("@补货状态", 补货状态.Checked).DbType = DbType.Boolean '试试可不可以删
        objCommand.Parameters.AddWithValue("@赔偿表证明", 赔偿表证明.Text)

        objCommand.Parameters.AddWithValue("@备注", 备注.Text)

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
        F04_供应商索赔信息_Load(Nothing, Nothing)         '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        objCurrencyManager.Position = objCurrencyManager.Count - 1   '设置你保存的那个记录位置    Set the record position to the one that you saved..
        ShowPosition()                                               '标签显示位置.





        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件

        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        执行查询_Click(Nothing, Nothing)


        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件



        ToolStripLabel1.Text = "Record Added"    '状态栏显示你添加的信息   Display a message that the record was added..
    End Sub

    '更新数据库
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        '声明一个局部变量和创建一个命令对象  Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        intPosition = objCurrencyManager.Position  '当前记录位置赋值给变量intPosstion. Save the current record position..
        objCommand.Connection = objConnection1th '设置命令对象一些属性 Set the SqlCommand object properties..




        'SQL语句表示按照指定条件,更新表字段..
        ' myArray = {"ID", "统计始日", "统计止日", "赔偿单号", "供应商", "图号", "区分", "数量", "加工费扣", "毛坯费扣", "毛坯抵扣", "供应商确认", "补货状态", "赔偿表证明", "备注"}
        ' '接着使用SQL字符串设置CommandText属性.
        objCommand.CommandText = "UPDATE 索赔信息 " &
                "SET 统计始日 = @统计始日,统计止日 = @统计止日,赔偿单号 = @赔偿单号,供应商 = @供应商,图号 = @图号,
区分 = @区分,数量 = @数量,加工费扣 = @加工费扣,毛坯费扣 = @毛坯费扣,毛坯抵扣 = @毛坯抵扣,供应商确认 = @供应商确认,
补货状态 = @补货状态,赔偿表证明 = @赔偿表证明,备注 = @备注 WHERE ID = @ID"
        objCommand.CommandType = CommandType.Text '命令类型为默认CommandType.Text类型,可以省略
        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the
        ' CommandText property..
        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
        objCommand.Parameters.AddWithValue("@统计始日", 统计始日.Text).DbType = DbType.Date  '转换类型.
        objCommand.Parameters.AddWithValue("@统计止日", 统计止日.Text)
        objCommand.Parameters.AddWithValue("@赔偿单号", 赔偿单号.Text)
        objCommand.Parameters.AddWithValue("@供应商", 供应商.Text)
        objCommand.Parameters.AddWithValue("@图号", 图号.Text)
        objCommand.Parameters.AddWithValue("@区分", 区分.Text)
        objCommand.Parameters.AddWithValue("@数量", 数量.Text)
        objCommand.Parameters.AddWithValue("@加工费扣", 加工费扣.Text).DbType = DbType.Single  '转换类型.
        objCommand.Parameters.AddWithValue("@毛坯费扣", 毛坯费扣.Text).DbType = DbType.Single  '转换类型.
        objCommand.Parameters.AddWithValue("@毛坯抵扣", 毛坯抵扣.Checked).DbType = DbType.Boolean  '转换类型.
        objCommand.Parameters.AddWithValue("@供应商确认", 供应商确认.Checked).DbType = DbType.Boolean  '转换类型.
        objCommand.Parameters.AddWithValue("@补货状态", 补货状态.Checked).DbType = DbType.Boolean  '转换类型.
        objCommand.Parameters.AddWithValue("@赔偿表证明", 赔偿表证明.Text)
        objCommand.Parameters.AddWithValue("@备注", 备注.Text)

        objCommand.Parameters.AddWithValue _
                ("@ID", BindingContext(objDataView).Current("ID"))

        objConnection1th.Open()    '打开带连接字符的数据库连接  Open the connection..
        objCommand.ExecuteNonQuery()   '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()    '关闭数据库连接  Close the connection..
        查询条件.Text = ID.Text

        F04_供应商索赔信息_Load(Nothing, Nothing) '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
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
        objCommand.CommandText = "DELETE FROM 索赔信息 " &
                "WHERE ID = @ID"
        '给ID字段提供相应的参数  Parameter for the ID field..
        objCommand.Parameters.AddWithValue _
            ("@ID", BindingContext(objDataView).Current("ID"))
        objConnection1th.Open()     '打开数据库连接 Open the database connection..
        objCommand.ExecuteNonQuery()     '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()         '关闭数据库连接 Close the connection..
        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        F04_供应商索赔信息_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition
        ShowPosition()  '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        '显示一个已删除的信息.  Display a message that the record was deleted..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Deleted"
        删除.Enabled = False
    End Sub

    '获取项目值模板
    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        'On Error Resume Next
        Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index
        RemoveHandler 补货状态.CheckedChanged, AddressOf 补货状态_CheckedChanged
        RemoveHandler 毛坯抵扣.CheckedChanged, AddressOf 毛坯抵扣_CheckedChanged

        BindFields()
        objCurrencyManager.Position = intPosition
        ShowPosition()


    End Sub

    '退出
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btn索赔信息.Enabled = True
        Me.Close()
    End Sub

    '关闭
    Private Sub F04_供应商索赔信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing         '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btn索赔信息.Enabled = True
    End Sub




    Private Sub 供应商_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 供应商.SelectedIndexChanged
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "发现过程" & " from " & "赔偿比例 ORDER BY 发现过程" '写入SQL语句
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "产品规格" & " from " & "物品信息 WHERE 供应商 = " & "'" & 供应商.Text & "'"  '写入SQL语句
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx2019042701")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb2019042701 As DataTable = objDataSet1th.Tables("wpxx2019042701") '声明一个表类型,并赋值给该变量.
        图号.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        '不良类型.Items.Add("外注不良") ： 不良类型.Items.Add("内部工程不良-人员") ： 不良类型.Items.Add("内部工程不良-条件")
        '不良类型.Items.Add("内部工程不良-设备") ： 不良类型.Items.Add("内部工程不良-工具") ： 不良类型.Items.Add("内部工程不良-其他")
        '不良类型.Items.Add("客户发现不良")
        For inCounter = 0 To tb2019042701.Rows.Count - 1               '在表行数上循环
            图号.Items.Add(tb2019042701.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next
    End Sub


    Private Sub 统计始日_GotFocus(sender As Object, e As EventArgs) Handles 统计始日.GotFocus
        'Dim strDate As String
        统计始日.Mask = "0000/00/00"
    End Sub


    Private Sub 统计始日_LostFocus(sender As Object, e As EventArgs) Handles 统计始日.LostFocus
        Dim strDate As String
        strDate = 统计始日.Text
        统计始日.Mask = ""
        统计始日.Text = strDate
    End Sub

    Private Sub 统计始日_TextChanged(sender As Object, e As EventArgs) Handles 统计始日.TextChanged
        Dim strStrogeValue
        If Len(统计始日.Text) = 10 Then strStrogeValue = 统计始日.Text : 统计始日_LostFocus(Nothing, Nothing)
    End Sub

    Private Sub 统计止日_GotFocus(sender As Object, e As EventArgs) Handles 统计止日.GotFocus
        统计止日.Mask = "0000/00/00"
    End Sub


    Private Sub 统计止日_LostFocus(sender As Object, e As EventArgs) Handles 统计止日.LostFocus
        Dim strDate As String
        strDate = 统计止日.Text
        统计止日.Mask = ""
        统计止日.Text = strDate
    End Sub

    Private Sub 统计止日_TextChanged(sender As Object, e As EventArgs) Handles 统计止日.TextChanged
        Dim strStrogeValue
        If Len(统计止日.Text) = 10 Then strStrogeValue = 统计止日.Text : 统计止日_LostFocus(Nothing, Nothing)
    End Sub



    Private Sub 毛坯抵扣_CheckedChanged(sender As Object, e As EventArgs) Handles 毛坯抵扣.CheckedChanged
        'AddHandler 毛坯抵扣.CheckedChanged, AddressOf 毛坯抵扣_CheckedChanged      '绑定事件
        'RemoveHandler 补货状态.CheckedChanged, AddressOf 补货状态_CheckedChanged
        'AddHandler 补货状态.CheckedChanged, AddressOf 补货状态_CheckedChanged      '绑定事件



        'If 毛坯抵扣.Checked Then
        '    strReduceMonery = 加工费扣.Text
        '    加工费扣.Text = 0
        'Else
        '    加工费扣.Text = strReduceMonery

        'End If

        If 毛坯抵扣.Checked Then

            加工费扣.Text = 0

        End If

    End Sub

    Private Sub 补货状态_CheckedChanged(sender As Object, e As EventArgs) Handles 补货状态.CheckedChanged
        'AddHandler 补货状态.CheckedChanged, AddressOf 补货状态_CheckedChanged      '绑定事件
        'RemoveHandler 毛坯抵扣.CheckedChanged, AddressOf 毛坯抵扣_CheckedChanged
        'AddHandler 毛坯抵扣.CheckedChanged, AddressOf 毛坯抵扣_CheckedChanged      '绑定事件


        'Dim strReduceMoney As String
        'If 补货状态.Checked Then
        '    strReduceMoney = 毛坯费扣.Text
        '    毛坯费扣.Text = 0
        'Else
        '    毛坯费扣.Text = strReduceMoney

        'End If


        If 补货状态.Checked Then
            毛坯费扣.Text = 0
        End If

    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        '声明一个变体型变量(在VB.net中已经不能再称之为变体型变量，而是Object.
        Dim objFileArray As Object, arrFileArrayResetting() As String
        Dim strRecordPath As String = "\\192.168.3.12\DataBaseRecord\1 claim\", bytPosition As Byte, bytAfterNamePosition As Byte, strFileName As String

        objFileArray = xlapp.GetOpenFilename("所有文件(*.*）,*.*", , , , True) '弹出一个选择文件的对话框,并设置可以多选.
        'IsArray函数判定是否是数组,如果用户选择了文件(此时变量objFilearr是数组,如果没有选择文件则返回值不是数组)
        If IsArray(objFileArray) Then
            '重置数组维数,这里减1表示,上一数组下标是从1开始的.以下语句还可以改成:
            'Dim arr(objFileArray.LongLength - 1) As Object   '声明一个下标为0,上标为文件数量-1的数组变量
            ReDim arrFileArrayResetting(UBound(objFileArray) - 1)
            '被复制的下数组标为1,数组拷贝到目标数组,起始放置点为0,即目标数组下标处开始存放被复制的数组元素.
            objFileArray.CopyTo(arrFileArrayResetting, 0)
            '将选择的所有文件名称导入到列表框中,并去除文件名称,在指定的文本框中显示文件路径.
            '打开路径.Items.AddRange(arrFileArrayResetting)
            'Replace(objFileArray(1), Dir(objFileArray(1)), "")
            '打开路径.Text = arrFileArrayResetting(0)
            '打开路径.Text = Replace(arrFileArrayResetting(0), "D:", "\\192.168.3.250")
            赔偿表证明.Text = arrFileArrayResetting(0)
            bytPosition = InStr(1, StrReverse(赔偿表证明.Text), "\")   '计算文件名称前面的“\”的位置
            bytAfterNamePosition = InStr(1, StrReverse(赔偿表证明.Text), ".")   '计算文件名称前面的“\”的位置
            strFileName = Mid(赔偿表证明.Text, Len(赔偿表证明.Text) - bytPosition + 2)

            If My.Computer.FileSystem.FileExists(strRecordPath & strFileName) = True Then Kill(strRecordPath & strFileName) '如果存在指定的文件夹,那么执行  Kill()
            System.IO.File.Copy(赔偿表证明.Text, strRecordPath & strFileName)

            赔偿表证明.Text = strRecordPath & strFileName

        Else
            Exit Sub  '结束过程
        End If

    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        On Error Resume Next
        If InStr(赔偿表证明.Text, "xls") > 0 Then     '如果是包含有xls后缀存储名
            xlapp.Workbooks.Open(赔偿表证明.Text)     '打开EXCEL
        Else                                        '否则
            'Shell("winword.exe " & 存放位置.Text, vbMaximizedFocus)   'shell函数打开word文档程序    
            Shell("explorer.exe " & 赔偿表证明.Text, vbMaximizedFocus)   '打开所有资源程序
        End If                  '结束语句
        If Err.Number <> 0 Then MsgBox("打开对应文件失败",, "提示")
    End Sub

    Private Sub 加工费扣_TextChanged(sender As Object, e As EventArgs) Handles 加工费扣.TextChanged
        On Error Resume Next
        If CType(加工费扣.Text, Byte) <> 0 Then
            毛坯抵扣.Checked = False
        Else
            毛坯抵扣.Checked = True
        End If

    End Sub

    Private Sub 毛坯费扣_TextChanged(sender As Object, e As EventArgs) Handles 毛坯费扣.TextChanged
        On Error Resume Next
        If CType(毛坯费扣.Text, Byte) <> 0 Then
            补货状态.Checked = False
        Else
            补货状态.Checked = True
        End If
    End Sub

    Private Sub btnReseting_Click(sender As Object, e As EventArgs) Handles btnReseting.Click
        查询条件.Text = ""

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









End Class
