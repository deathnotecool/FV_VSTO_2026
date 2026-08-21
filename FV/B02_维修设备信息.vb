Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Public Class B02_维修设备信息
    '声明作用域为类级的变量,创建一个对象的实例（该对象建立了与数据库的连接）此时数据库为Access.
    Dim objConnection1th As New OleDbConnection _
           ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\设备管理\设备管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\设备管理\设备管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\设备管理\设备管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\设备管理.accdb")  '公司共享盘
    '声明作用域为类级的变量,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '以下构造函数不必写Adapter属性的SelectCommand相关代码.已经加入相关参数(SQL语句)
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 维修.* FROM 维修 ORDER BY 维修单号", objConnection1th)
    Dim objDataAdapter1th As New OleDbDataAdapter()  '该构造函数需要使用SelectCommand属性.用来填充履历卡数据的。
    Dim objDataSet As New DataSet()     '声明作用域为类级的变量,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet()
    Dim objDataView As DataView   '声明作用域为类级的变量,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图。
    Dim objDataView1th As DataView
    Dim objCurrencyManager As CurrencyManager  '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object                       '声明变量,数据库用

    '创建一个过程，在初始化代码中调用；用来填充数据和显示数据。
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '初始化一个DataSet对象 Initialize a new instance of the DataSet object.
        'SqlDataAdapter对象（构造函数或SelectCommand属性）从数据库检索到的数据向DataSet对象填充.'Fill the DataSet object with data.. 
        '注意:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,连接没打开就会自动打开填充数据后保持打开连接对象.  
        objDataAdapter.Fill(objDataSet, "wxxx")  '这里的表是初始构建起来的,命名为wxxx.
        '创建一个DataView类的实例。
        objDataView = New DataView(objDataSet.Tables("wxxx"))  'DataView对象允许对DataSet中的记录进行排序、查找和浏览.
        'CurrencyManager(数据源集合)对象集合包含于BindingContect(内置于Windows窗体,无须创建)中,将DataView对象对象转化为CurrencyManager。
        objCurrencyManager =
      CType(Me.BindingContext(objDataView), CurrencyManager)    'Set our CurrencyManager object to the DataView object.
    End Sub

    '创建一个过程以用来将窗体中的控件绑定到DataView对象.
    Private Sub BindFields()
        Dim i As Byte = 0
        '控件的DataBindings属性的Clear方法逐一清除控件上的绑定(控件可能与之前的绑定DataView数据源) Clear any previous bindings..
        myArray = {"维修单号", "设备编号", "申请人", "报修时间", "故障描述", "维修类型", "维修工时", "维修价格", "替换件编号", "修理描述", "完工日期"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear() '逐一清除控件数据源(这里为定制表视图对象DataView)绑定
        Next i
        For i = 0 To UBound(myArray)  '控件逐一绑定DateView数据源,add方法第一参数为控件的属性,第二参数为被绑定的数据源,第三参数为被绑定给控件的数据字段.
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "报修时间" Then GroupBox1.Controls(myArray(i).ToString).Text _
                = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
            If GroupBox1.Controls(myArray(i).ToString).Name = "完工日期" Then GroupBox1.Controls(myArray(i).ToString).Text _
                = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
        Next i
        ToolStripLabel1.Text = "Ready"                                                            '显示一个"只读"状态    Display a ready status..
    End Sub

    '创建一个能在窗体上显示当前记录位置的过程
    Private Sub ShowPosition()
        Try                                                                                        '格式化日期指定短日期格式.
            报修时间.Text = Format(CType(GroupBox1.Controls("报修时间").Text, Date), "yyyy/MM/dd") '定义格式
            完工日期.Text = Format(CType(GroupBox1.Controls("完工日期").Text, Date), "yyyy/MM/dd") '定义格式
        Catch e As System.Exception                                                                '声明一个错误变量类型
            GroupBox1.Controls("报修时间").Text = CType(Now, String)    '如果异常(文本框为空)那么转换日期类型为文本类型写入当前日期.
            GroupBox1.Controls("完工日期").Text = CType(Now, String)
            报修时间.Text = Format(CType(GroupBox1.Controls("报修时间").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
            完工日期.Text = Format(CType(GroupBox1.Controls("完工日期").Text, Date), "yyyy/MM/dd")
        End Try
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()  '显示当前记录位置并标记记录数. Display the current position and the number of records
    End Sub

    '按钮单击事件,移动第一条记录
    Private Sub btnMoveFirst_Click(Sender As Object,
            E As EventArgs) Handles btnMoveFirst.Click
        Dim intPosition As Integer
        objCurrencyManager.Position = 0  '设置当前记录为第一条记录 'Set the record position to the first record..
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()                   '控件被绑定定制视图DataView对象,是同步的,更新记录标签. Show the current record position..
    End Sub

    '按钮单击事件,移动上一条记录
    Private Sub btnMovePrevious_Click(Sender As Object,
            E As EventArgs) Handles btnMovePrevious.Click
        Dim intPosition As Integer  '声明变量对象保存记录所在的位置。
        objCurrencyManager.Position -= 1  '当前选中的记录（数据）位置上移1个单位。 'Move to the previous record..
        intPosition = objCurrencyManager.Position  '记录当前记录位置，并赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged  '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)  '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged  '绑定事件
        ShowPosition()  '控件被绑定到DataView对象,是同步的,需要更新调用显示数据过程.  Show the current record position..
    End Sub

    '按钮单击事件,移动下一条记录
    Private Sub btnMoveNext_Click(Sender As Object,
            E As EventArgs) Handles btnMoveNext.Click
        Dim intPosition As Integer
        '移动下一条记录,不需要调用重新绑定过程,自动同步的,只要不更新,就不存在数据源集的变更  
        objCurrencyManager.Position += 1                                                     'Move to the next record..
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()                                                                               'Show the current record position..
    End Sub

    '按钮单击事件,移动最后一条记录
    Private Sub btnMoveLast_Click(Sender As Object,
            E As EventArgs) Handles btnMoveLast.Click
        Dim intPosition As Integer
        '移动最后一条记录,不需要调用重新绑定过程,自动同步的,只要不更新,就不存在数据源集的变更 
        objCurrencyManager.Position = objCurrencyManager.Count - 1 ' Set the record position to the last record..
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()                          '控件被绑定到DataView对象,是同步的,需要更新调用显示数据过程.    Show the current record position..
    End Sub

    '加载窗体触发事件
    Private Sub B02_维修设备信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next  '需要说明的是,Fill方法会执行SelectCommand,Connection属性保持为调用该方法时的状态.
        FillDataSetAndView()  '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定数据源字段到指定控件.
        ShowPosition()  '调用ShowPosition方法显示当前记录标签位置    Show the current record position..
        'BindFields()  '调用绑定控件过程,因为有复合框,所以放在事件最后面.
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.  Set the DataGridView properties to bind it to our data..
        grdAuthorTitles.DataSource = objDataSet  '设置DataSet对象作为gird控件的数据来源(实际上就是一个绑定过程,告知控件从哪里获得数据)
        grdAuthorTitles.DataMember = "wxxx"  'gird控件要显示数据源(填充过数据的DataSet对象)具体的表名称
        Dim objAlignRightCellStyle As New DataGridViewCellStyle  '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例) 

        '将对齐方式格式改为垂直居中向右对齐,从而能对运行状态字段进行对齐.
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()  '初始化DataGridViewCellStyle对象(grd控件单元格样式实例)作为交叉行样式.  
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置交叉样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
        Dim objCurrencyCellStyle As New DataGridViewCellStyle() '初始化DataGridViewCellStyle对象,将设置单元格格式为货币型.
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式改为居中向左对齐
        'objCurrencyCellStyle.Format = "$#,##0.00"   '样式格式为货币型(美元)
        objCurrencyCellStyle.Format = "C"         '样式格式为货币型(人民币)
        grdAuthorTitles.Columns(0).HeaderText = "维修单号"   '设置控件列标题  Change column names and styles using the column index 
        grdAuthorTitles.Columns(1).HeaderText = "设备编号"
        grdAuthorTitles.Columns(2).HeaderText = "申请人"
        grdAuthorTitles.Columns(3).HeaderText = "报修时间"
        grdAuthorTitles.Columns(4).HeaderText = "故障描述"
        grdAuthorTitles.Columns(5).HeaderText = "维修类型"
        grdAuthorTitles.Columns(6).HeaderText = "维修工时"
        grdAuthorTitles.Columns(7).HeaderText = "维修价格"
        grdAuthorTitles.Columns(8).HeaderText = "替换件编号"
        grdAuthorTitles.Columns(9).HeaderText = "修理描述"
        grdAuthorTitles.Columns(10).HeaderText = "完工日期"
        grdAuthorTitles.Columns(9).Width = 165 '设置指定列默认宽度小一点
        '改变字段标题名称和样式'Change column names and styles using the column name  
        grdAuthorTitles.Columns("维修价格").HeaderCell.Value = "维修成本"  '重新设置列标题的值显示为"描述"
        grdAuthorTitles.Columns("维修价格").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-居中右对齐)
        grdAuthorTitles.Columns("维修价格").DefaultCellStyle = objCurrencyCellStyle  '单元格内容重新调用样式(之前设定的-居中左对齐)
        For i As Integer = 0 To grdAuthorTitles.RowCount - 1  '有一个空白行也算一行
            If Math.Ceiling((Now.Subtract(CType(grdAuthorTitles.Item(3, i).Value.ToString(), Date)).TotalDays -
                            Now.Subtract(CType(grdAuthorTitles.Item(10, i).Value.ToString(), Date)).TotalDays)) >= 1 _
                            Or CType(grdAuthorTitles.Item(6, i).Value.ToString(), Integer) > 8 Then
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)  '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red  '字体颜色设置为红色
            Else
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)  '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black   '字体颜色设置为黑色
            End If
        Next
        objCurrencyCellStyle = Nothing  '清除样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        排序字段.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        排序字段.Items.AddRange(myArray)
        排序字段.SelectedIndex = 0    '默认选择第一项
        维修类型.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        维修类型.Items.Add("故障维修")
        维修类型.Items.Add("不定期检修")   '维修类型.SelectedIndex = 0    '默认选择第一项
        objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令实例给数据适配器命令对象
        objDataAdapter1th.SelectCommand.Connection = objConnection1th   '数据库连接桥梁赋值给连接属性
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "设备编号" & " from " & "设备名称 ORDER BY 设备编号" 'SQL命令语句

        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象. 'Fill the DataSet object with data..
        objDataSet1th = New DataSet() '初始化一个内存数据对象
        objDataAdapter1th.Fill(objDataSet1th, "sbxx02")  '填充数据源到表(没有可以自动创建),sbxx02
        'objDataView1th = New DataView(objDataSet.Tables("jhxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("sbxx02")  '将指定表赋值给变量tb
        设备编号.Items.Clear()  '清除设备编号复合框值
        'For inCounter = 0 To tb.Rows.Count - 1                          '在DatSet构建的表中遍历行.
        '    'strResult = .Rows(inCounter).Item("username").ToString & "" & .Rows(inCounter).Item("password").ToString
        '    设备编号.Items.Add(tb.Rows(inCounter).Item(0).ToString)     '添加项目值为记录字段所对应的值
        'Next
        设备编号.DataSource = tb
        设备编号.ValueMember = "设备编号"
        BindFields()                                '调用绑定控件过程
    End Sub

    '新建一个过程,显示设备履历卡.
    Private Sub 履历卡()
        On Error Resume Next  '出错继续执行下一句代码.
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
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 维修.* FROM 维修 WHERE 维修.设备编号='" &
            GroupBox1.Controls("设备编号").Text & "' ORDER BY 维修单号"

        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        objDataSet1th = New DataSet()  '数据适配器对象开始检索数据并填充到DataSet对象 'Fill the DataSet object with data..
        grdAuthorTitles1th.DataSource = objDataSet1th  '设置控件的数据源
        grdAuthorTitles1th.AutoGenerateColumns = True  '全部显示列.
        objDataAdapter1th.Fill(objDataSet1th, "wxxx1")  '显示表
        objDataView1th = New DataView(objDataSet1th.Tables("wxxx1"))   '初始化一个DataView对象并写入参数构建
        Dim objAlternatingCellStyle As New DataGridViewCellStyle() '初始化一个样式
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke   '设置样式背景色为烟灰色
        'grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle  '设置奇数行属性设置刚创建的样式(烟白色)
        grdAuthorTitles1th.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '设置奇数行属性设置刚创建的样式(烟白色)

        '因为数据已填充到DataSet对象中了,可以关闭数据库的连接(通信)   Close the database connection..
        grdAuthorTitles1th.DataMember = "wxxx1"
        grdAuthorTitles1th.Columns(0).HeaderText = "维修单号"
        grdAuthorTitles1th.Columns(1).HeaderText = "设备编号"
        grdAuthorTitles1th.Columns(2).HeaderText = "申请人"
        grdAuthorTitles1th.Columns(3).HeaderText = "报修时间"
        grdAuthorTitles1th.Columns(4).HeaderText = "故障描述"
        grdAuthorTitles1th.Columns(5).HeaderText = "维修类型"
        grdAuthorTitles1th.Columns(6).HeaderText = "维修工时"
        grdAuthorTitles1th.Columns(7).HeaderText = "维修价格"
        grdAuthorTitles1th.Columns(8).HeaderText = "替换件编号"
        grdAuthorTitles1th.Columns(9).HeaderText = "修理描述"
        grdAuthorTitles1th.Columns(10).HeaderText = "完工日期"
        grdAuthorTitles1th.Columns("维修价格").HeaderCell.Value = "维修成本"              '重新设置列标题的值显示为"Retail Price"
        grdAuthorTitles1th.Columns("维修价格").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles1th.Columns("维修价格").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式
        For i As Integer = 0 To grdAuthorTitles1th.RowCount - 1                           '有一个空白行也算一行
            If Math.Ceiling((Now.Subtract(CType(grdAuthorTitles1th.Item(3, i).Value.ToString(), Date)).TotalDays - Now.Subtract(CType(grdAuthorTitles1th.Item(10, i).Value.ToString(), Date)).TotalDays)) >= 1 Or CType(grdAuthorTitles1th.Item(6, i).Value.ToString(), Integer) > 8 Then
                grdAuthorTitles1th.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular) '创建一个字体类,并构建相关属性
                grdAuthorTitles1th.Rows(i).DefaultCellStyle.ForeColor = Color.Red
            Else
                grdAuthorTitles1th.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)
                grdAuthorTitles1th.Rows(i).DefaultCellStyle.ForeColor = Color.Black
            End If
        Next
    End Sub

    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件数据源进行排序.
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性.
        Select Case 排序字段.SelectedIndex      'Determine the appropriate item selected and set the Sort property of the DataView object.. 
            Case 0
                objDataView.Sort = "维修单号"   '按字段设备编号升序排序,下同.
            Case 1
                objDataView.Sort = "设备编号"
            Case 2
                objDataView.Sort = "申请人"
            Case 3
                objDataView.Sort = "报修时间"
            Case 4
                objDataView.Sort = "故障描述"
            Case 5
                objDataView.Sort = "维修类型"
            Case 6
                objDataView.Sort = "维修工时"
            Case 7
                objDataView.Sort = "维修价格"
            Case 8
                objDataView.Sort = "替换件编号"
            Case 9
                objDataView.Sort = "修理描述"
            Case 10
                objDataView.Sort = "完工日期"
        End Select
        btnMoveFirst_Click(Nothing, Nothing)      '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted"   '修改状态标签Text属性. Display a message that the records have been sorted..
    End Sub

    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        Dim intPosition As Integer  '执行查找,声明当前局部变量.'Declare local variables.. 
        Dim str条件 As String = ""
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
        'Determine the appropriate item selected And set the Sort property of the DataView object..
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "维修单号"
                str条件 = "维修单号"
            Case 1
                objDataView.Sort = "设备编号"
                str条件 = "设备编号"
            Case 2
                objDataView.Sort = "申请人"
                str条件 = "申请人"
            Case 3
                objDataView.Sort = "报修时间"
                str条件 = "报修时间"
            Case 4
                objDataView.Sort = "故障描述"
                str条件 = "故障描述"
            Case 5
                objDataView.Sort = "维修类型"
                str条件 = "维修类型"
            Case 6
                objDataView.Sort = "维修工时"
                str条件 = "维修工时"
            Case 7
                objDataView.Sort = "维修价格"
                str条件 = "维修价格"
            Case 8
                objDataView.Sort = "替换件编号"
                str条件 = "替换件编号"
            Case 9
                objDataView.Sort = "修理描述"
                str条件 = "修理描述"
            Case 10
                objDataView.Sort = "完工日期"
                str条件 = "完工日期"
        End Select
        objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"   'DataView数据表中筛选数据集(类似SQL语句).
        intPosition = objCurrencyManager.Position  '默认位置0赋值给变量
        If intPosition = -1 Then  '状态栏提示没有找到记录 Display a message that the record was not found..
            ToolStripLabel1.Text = "Record Not Found"  '标签显示字符.
            '否则状态栏显示字符. Otherwise display a message that the record was ' found and reposition the CurrencyManager to that record..
        Else
            ToolStripLabel1.Text = "Record Found"
        End If
        ShowPosition()                                      '重新显示当前记录位置. Show the current record position..
    End Sub

    '查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If 查询条件.Text.Length = 0 Then              '如果是空值
            B02_维修设备信息_Load(Nothing, Nothing)   '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置
        End If
    End Sub

    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    '新建按钮事件
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        Dim i As Byte = 0             '声明局部变量
        myArray = {"维修单号", "设备编号", "申请人", "报修时间", "故障描述", "维修类型", "维修工时", "维修价格", "替换件编号", "修理描述", "完工日期"}
        For i = 0 To UBound(myArray)  '清空简单控件值
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
        维修单号.Enabled = False      '设置禁止使用控件
    End Sub

    '添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字'Declare local variables and objects.. 
        Dim intMaxID As Integer
        Dim strID As String = ""  '变量用来存储authors表的主键并设置authors表的新键
        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个新的查询.
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..'从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 维修 ORDER BY 维修单号 DESC", objConnection1th)  '存贮当前记录位置给变量  Save the current record position..
        objConnection1th.Open()   '打开数据库连接 Open the connection, execute the command SELECT TOP 1 * FROM 表名 ORDER BY 排序字段 DESC
        Dim maxId As Object = maxIdCommand.ExecuteScalar()  '调用SqlCommand的一个执行方法(只返回一行一列).并把结果赋值给变量
        If maxId Is DBNull.Value Then                       '如果返回结果是空值那么执行    If the MaxID column is null..
            intMaxID = 1000   '设置一个默认值1000.Set a default value of 1000..
        Else
            strID = CType(maxId, String)  '否则执行将maxId换成String型.strId.otherwise set the strID variable to the value in MaxID..
            intMaxID = CType(strID.Remove(0, 2), Integer)   '利用Remove方法删除sb前缀,转换整型赋值给变量intMaxID.Get the integer part of the string..
            intMaxID += 1                                   '变量加1.Increment the value..
        End If
        '变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..'strID = "SB" & intMaxID.ToString
        '变量转换成字符串,并与DM连接,构建一个新主键.Finally, set the new ID..
        Select Case Len(intMaxID.ToString)
            Case 1
                strID = "WX00" & intMaxID.ToString
            Case 2
                strID = "WX0" & intMaxID.ToString
            Case Else
                strID = "WX" & intMaxID.ToString
        End Select
        '设置命令对象的属性 Set the SqlCommand object properties..'将连接字符串的连接对象赋值给Connection属性.
        objCommand.Connection = objConnection1th
        '维修单号.Enabled = True'将CommandText属性(要执行的SQL字符串)设置指定的值.
        objCommand.CommandText = "INSERT INTO 维修 " &
        "(维修单号, 设备编号, 申请人, 报修时间, 故障描述, 维修类型, 维修工时, 维修价格, 替换件编号, 修理描述,完工日期) " &
        "VALUES(@维修单号, @设备编号, @申请人, @报修时间, @故障描述, @维修类型, @维修工时, @维修价格, @替换件编号, @修理描述, @完工日期)"
        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列..'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the 'CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@维修单号", strID)          '指定参数写入值,下同.
        objCommand.Parameters.AddWithValue("@设备编号", 设备编号.Text)
        objCommand.Parameters.AddWithValue("@申请人", 申请人.Text)
        objCommand.Parameters.AddWithValue("@报修时间", 报修时间.Text).DbType = DbType.Date '转换日期类型
        objCommand.Parameters.AddWithValue("@故障描述", 故障描述.Text)
        objCommand.Parameters.AddWithValue("@维修类型", 维修类型.Text)
        objCommand.Parameters.AddWithValue("@维修工时", 维修工时.Text)
        objCommand.Parameters.AddWithValue("@维修价格", 维修价格.Text).DbType _
            = DbType.Currency                                                               '转换货币类型
        objCommand.Parameters.AddWithValue("@替换件编号", 替换件编号.Text)
        objCommand.Parameters.AddWithValue("@修理描述", 修理描述.Text)
        objCommand.Parameters.AddWithValue("@完工日期", 完工日期.Text)
        myArray = {"维修单号", "设备编号", "申请人", "报修时间", "故障描述", "维修类型", "维修工时", "维修价格", "替换件编号", "修理描述", "完工日期"}
        For i = 0 To UBound(myArray)
            If myArray(i).ToString <> "维修单号" Then   '如果名称只要不是维修单号,那么要执行.
                If GroupBox1.Controls(myArray(i).ToString).Text.Length = 0 Then MsgBox("请输入完整数据在添加数据") : _
                    新建_Click(Nothing, Nothing) : objConnection1th.Close() : Exit Sub
            End If
        Next i
        Try                               '截取异常'执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..
            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
        Catch SqlExceptionErr As OleDbException         '声明异常类型
            MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示异常类型错误信息
        End Try                                         '结束截取
        objConnection1th.Close()                        '关闭数据库连接 Close the connection..
        B02_维修设备信息_Load(Nothing, Nothing)         '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        objCurrencyManager.Position = objCurrencyManager.Count - 1   '设置你保存的那个记录位置    Set the record position to the one that you saved..
        ShowPosition()    '标签显示位置.
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Added"    '状态栏显示你添加的信息   Display a message that the record was added..
        履历卡()
    End Sub

    '更新数据库
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        '声明一个局部变量和创建一个命令对象  Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        intPosition = objCurrencyManager.Position  '当前记录位置赋值给变量intPosstion. Save the current record position..
        objCommand.Connection = objConnection1th '设置命令对象一些属性 Set the SqlCommand object properties..
        'SQL语句表示按照指定条件,更新表设备名称  "放置地点", "制造商", "制造日期", "使用部门", "运行状态"等
        objCommand.CommandText = "UPDATE 维修 " &
            "SET 设备编号 = @设备编号,申请人 = @申请人,报修时间 = @报修时间,故障描述 = @故障描述,维修类型 = @维修类型,维修工时 = @维修工时,维修价格 = @维修价格,替换件编号 = @替换件编号,修理描述 = @修理描述,完工日期 = @完工日期 WHERE 维修单号 = @维修单号"  ' '接着使用SQL字符串设置CommandText属性.
        objCommand.CommandType = CommandType.Text '命令类型为默认CommandType.Text类型,可以省略
        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the
        ' CommandText property..
        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
        objCommand.Parameters.AddWithValue("@设备编号", 设备编号.Text)
        objCommand.Parameters.AddWithValue("@申请人", 申请人.Text)
        objCommand.Parameters.AddWithValue("@报修时间", 报修时间.Text).DbType = DbType.Date  '转换类型.
        objCommand.Parameters.AddWithValue("@故障描述", 故障描述.Text)
        objCommand.Parameters.AddWithValue("@维修类型", 维修类型.Text)
        objCommand.Parameters.AddWithValue("@维修工时", 维修工时.Text).DbType = DbType.Byte
        objCommand.Parameters.AddWithValue("@维修价格", 维修价格.Text).DbType = DbType.Currency  '转换类型.
        objCommand.Parameters.AddWithValue("@替换件编号", 替换件编号.Text)
        objCommand.Parameters.AddWithValue("@修理描述", 修理描述.Text)
        objCommand.Parameters.AddWithValue("@完工日期", 完工日期.Text)
        objCommand.Parameters.AddWithValue _
            ("@维修单号", BindingContext(objDataView).Current("维修单号"))
        objConnection1th.Open()   '打开带连接字符的数据库连接  Open the connection..
        objCommand.ExecuteNonQuery()  '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()   '关闭数据库连接  Close the connection..
        B02_维修设备信息_Load(Nothing, Nothing) '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
        objCurrencyManager.Position = intPosition   ' 设置你保存过的记录位置 Set the record position to the one that you saved..
        ShowPosition() '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        '显示状态信息  Display a message that the record was updated..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        履历卡()
        ToolStripLabel1.Text = "Record Updated"
    End Sub

    '删除记录
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        '定义一个局部变量和命令对象 Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        '保存当前记录位置-1以用来记录删除位置.  Save the current record position—1 for the one to be deleted..
        intPosition = Me.BindingContext(objDataView).Position - 1
        If intPosition < 0 Then  '如果没有记录,则设置记录位置为0.    If the position is less than 0 set it to 0..
            intPosition = 0
        End If
        objCommand.Connection = objConnection1th      '设置命令对象属性 Set the Command object properties..
        objCommand.CommandText = "DELETE FROM 维修 " &
            "WHERE 维修单号 = @维修单号"
        '给title_id字段提供相应的参数  Parameter for the title_id field..
        objCommand.Parameters.AddWithValue _
        ("@维修单号", BindingContext(objDataView).Current("维修单号"))
        objConnection1th.Open()     '打开数据库连接 Open the database connection..
        objCommand.ExecuteNonQuery()     '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()         '关闭数据库连接 Close the connection..
        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        B02_维修设备信息_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition
        ShowPosition()  '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        '显示一个已删除的信息.  Display a message that the record was deleted..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Deleted"
        履历卡()
    End Sub

    '获取项目值模板
    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        'On Error Resume Next
        Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index
        BindFields()
        objCurrencyManager.Position = intPosition
        ShowPosition()
        履历卡()
    End Sub

    '退出
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button27.Enabled = True
        Me.Close()
    End Sub

    '关闭
    Private Sub B02_维修设备信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing         '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button27.Enabled = True
    End Sub
    '设备编号值发生变化
    Private Sub 设备编号_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 设备编号.SelectedIndexChanged
        履历卡()
    End Sub


End Class