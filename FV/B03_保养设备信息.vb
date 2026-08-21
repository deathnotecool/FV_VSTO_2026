Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831,710      
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
'关于最新的模板信息更新到此Win窗体.
Public Class B03_保养设备信息
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim objConnection2th As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\设备管理.accdb")  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\设备管理\设备管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\设备管理\设备管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\设备管理.accdb")  '公司共享盘
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 保养.* FROM 保养 ORDER BY 保养单号", objConnection2th) '这个构造函数使我们不必写SelectCommand属性代码.
    Dim objDataAdapter1th As New OleDbDataAdapter() '该构造函数需要使用SelectCommand属性.用来填充履历卡数据的
    Dim objDataSet As New DataSet()                 '该对象作为数据的容器,将所有数据存储到内存中,无需与数据库保持连接.
    Dim objDataSet1th As New DataSet()              '作为临时存放数据的数据适配器.
    Dim objDataView As DataView                     'DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图     
    Dim objDataView1th As DataView                  '履历卡相关数据和复合框相关数据用.
    Dim objCurrencyManager As CurrencyManager       '一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object                           '声明变量,数据库用
    '创建一个过程将在初始化代码中调用,以用来填充数据和显示数据
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()                            '初始化一个DataSet对象 Initialize a new instance of the DataSet object.
        '向DataSet对象填充由SqlDataAdapter对象SelectCommand属性从数据库检索到的数据.'Fill the DataSet object with data.. 
        '注意:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,连接没打开就会自动打开填充数据后保持打开连接对象.  
        objDataAdapter.Fill(objDataSet, "wxxx")               '这里的表是初始构建起来的,命名为wxxx.
        objDataView = New DataView(objDataSet.Tables("wxxx")) 'DataView对象允许对DataSet中的记录进行排序、查找和浏览.
        'CurrencyManager(数据源集合)对象集合包含于BindingContect(内置于Windows窗体,无须创建)中,将CurrencyManager对象设置为DataView对象。
        objCurrencyManager =
      CType(Me.BindingContext(objDataView), CurrencyManager)  'Set our CurrencyManager object to the DataView object.
    End Sub
    '创建一个过程以用来将窗体中的控件绑定到DataView对象.
    Private Sub BindFields()
        Dim i As Byte = 0
        '控件的DataBindings属性的Clear方法逐一清除控件上的绑定(控件可能与之前的绑定DataView数据源) Clear any previous bindings..
        myArray = {"保养单号", "设备编号", "保养费用", "保养级别", "保养内容", "保养时间", "替换件编号", "工时"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        For i = 0 To UBound(myArray)        '控件逐一绑定DateView数据源,add方法第一参数为控件的属性,第二参数为被绑定的数据源,第三参数为被绑定给控件的数据字段.
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "保养时间" Then GroupBox1.Controls(myArray(i).ToString).Text = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd")                 '转换日期格式类型.
            If GroupBox1.Controls(myArray(i).ToString).Name = "保养费用" Then GroupBox1.Controls(myArray(i).ToString).Text = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Integer), "￥###,##.00")             '转换货币格式类型.
        Next i
        ToolStripLabel1.Text = "Ready"                                         '显示一个"只读"状态    Display a ready status..
    End Sub
    '创建一个能在窗体上显示当前记录位置的过程
    Private Sub ShowPosition()
        'Format number in the txtPrice field to include cents
        Try
            保养时间.Text = Format(CType(GroupBox1.Controls("保养时间").Text, Date), "yyyy/MM/dd") '定义格式
        Catch e As System.Exception                                    '声明一个错误变量类型
            GroupBox1.Controls("保养时间").Text = CType(Now, String)   '如果异常(文本框为空)那么将日期写为当前日期
            保养时间.Text = Format(CType(GroupBox1.Controls("保养时间").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
        End Try
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()      '显示当前记录位置并标记记录数.Display the current position and the number of records
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
        Dim intPosition As Integer
        objCurrencyManager.Position -= 1                                                             'Move to the previous record..
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
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
    '窗体启动触发事件
    Private Sub B03_保养设备信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定控件      
        FillDataSetAndView()                       '需要说明的是,Fill方法会执行SelectCommand,Connection属性保持为调用该方法时的状态.
        ShowPosition()                             '调用ShowPosition方法显示当前记录标签位置    Show the current record position..
        grdAuthorTitles.AutoGenerateColumns = True '让grd控件创建所需要的所有列.  Set the DataGridView properties to bind it to our data..
        grdAuthorTitles.DataSource = objDataSet    '设置DataSet对象作为gird控件的数据源(实际上就是一个绑定过程,告知控件从哪里获得数据)
        grdAuthorTitles.DataMember = "wxxx"        'gird控件要显示数据源(填充过数据的DataSet对象)具体的表名称.
        Dim objAlignRightCellStyle As New DataGridViewCellStyle                     '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例)
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '将对齐方式格式改为垂直居中向右对齐,从而能对运行状态字段进行对齐.
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()                  '定义交叉行样式,先创建DataGridViewCellStyle对象(grd控件单元格样式实例)   
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke                        '设置样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle   '奇数行属性设置刚创建的样式(烟白色)
        Dim objCurrencyCellStyle As New DataGridViewCellStyle() '创建DataGridViewCellStyle对象'Declare and set the style for currency cells .. 
        '设置单元格格式为货币型参考 'objCurrencyCellStyle.Format = "$#,##0.00"
        objCurrencyCellStyle.Format = "C"
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft   '将对齐方式改为居右对齐
        '设置控件列标题'Change column names and styles using the column index                    
        grdAuthorTitles.Columns(0).HeaderText = "保养单号"
        grdAuthorTitles.Columns(1).HeaderText = "设备编号"
        grdAuthorTitles.Columns(2).HeaderText = "保养费用"
        grdAuthorTitles.Columns(3).HeaderText = "保养级别"
        grdAuthorTitles.Columns(4).HeaderText = "保养内容"
        grdAuthorTitles.Columns(5).HeaderText = "保养时间"
        grdAuthorTitles.Columns(6).HeaderText = "替换件编号"
        grdAuthorTitles.Columns(7).HeaderText = "工时"
        grdAuthorTitles.Columns(6).Width = 75    '设置指定列默认宽度小一点
        grdAuthorTitles.Columns(7).Width = 65
        '改变字段标题名称和样式'Change column names and styles using the column name  
        grdAuthorTitles.Columns("保养费用").HeaderCell.Value = "维保费用"              '重新设置列标题的值显示为"Retail Price"
        grdAuthorTitles.Columns("保养费用").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles.Columns("保养费用").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        '给组合框添加项目  'Add items to the combo box.. 
        排序字段.Items.Clear()
        排序字段.Items.Clear()
        'For i = 0 To UBound(myArray)
        '    排序字段.Items.Add(GroupBox1.Controls(myArray(i).ToString).Name.ToString)
        'Next i
        排序字段.Items.AddRange(myArray)    '批量添加项,参数为数组.
        排序字段.SelectedIndex = 0          '默认选择第一项
        保养级别.Items.Clear()
        保养级别.Items.Add("年度保养")      '添加单项
        保养级别.Items.Add("常规例保")
        objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
        objDataAdapter1th.SelectCommand.Connection = objConnection2th   '建立与数据库的连接
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "设备编号" & " from " & "设备名称 ORDER BY 设备编号" '写入SQL语句
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "sbxx02")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tb As DataTable = objDataSet1th.Tables("sbxx02") '声明一个表类型,并赋值给该变量.
        设备编号.Items.Clear()                               '清楚复合框项目集
        For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
            设备编号.Items.Add(tb.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next
        BindFields()                                         '调用绑定控件过程
        '履历卡()'可以注释该句,因为上面语句会触发设备编号复合框事件,调用履历卡.
    End Sub
    Private Sub 履历卡()
        '创建DataGridViewCellStyle对象(grd控件单元格样式实例)   
        'Declare and set the style for currency cells ..
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        '设置单元格格式为货币型参考
        objCurrencyCellStyle.Format = "$#,##0.00"
        objCurrencyCellStyle.Format = "C"
        '将对齐方式改为居右对齐
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        '设置控件列标题                    
        'Declare and set the currency header alignment property..
        Dim objAlignRightCellStyle As New DataGridViewCellStyle
        '初始化OleDbCommand类的一个实例,并将其分配给SelectCommand属性.   Set the SelectCommand properties..
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        '将Connection属性设置为连接对象.用来与数据库通信.
        objDataAdapter1th.SelectCommand.Connection = objConnection2th
        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 保养.* FROM 保养 WHERE 保养.设备编号='" & GroupBox1.Controls("设备编号").Text & "' ORDER BY 保养单号"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        grdAuthorTitles1th.DataSource = objDataSet1th
        grdAuthorTitles1th.AutoGenerateColumns = True
        objDataAdapter1th.Fill(objDataSet1th, "wxxx1")
        objDataView1th = New DataView(objDataSet1th.Tables("wxxx1"))
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
        grdAuthorTitles1th.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
        '因为数据已填充到DataSet对象中了,所以可以关闭数据库的连接(通信)   Close the database connection..
        grdAuthorTitles1th.DataMember = "wxxx1"
        grdAuthorTitles1th.Columns(0).HeaderText = "保养单号"
        grdAuthorTitles1th.Columns(1).HeaderText = "设备编号"
        grdAuthorTitles1th.Columns(2).HeaderText = "保养费用"
        grdAuthorTitles1th.Columns(3).HeaderText = "保养级别"
        grdAuthorTitles1th.Columns(4).HeaderText = "保养内容"
        grdAuthorTitles1th.Columns(5).HeaderText = "保养时间"
        grdAuthorTitles1th.Columns(6).HeaderText = "替换件编号"
        grdAuthorTitles1th.Columns(7).HeaderText = "工时"
        '设置指定列默认宽度小一点
        grdAuthorTitles1th.Columns(6).Width = 75
        grdAuthorTitles1th.Columns(7).Width = 65
        grdAuthorTitles1th.Columns("保养费用").HeaderCell.Value = "维保费用"                  '重新设置列标题的值显示为"Retail Price"
        grdAuthorTitles1th.Columns("保养费用").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles1th.Columns("保养费用").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
    End Sub
    '排序按钮,确定对哪个字段进行排序.单击事件.'注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件进行排序.
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象,相关字段的sort属性.'Determine the appropriate item selected and set the Sort property of the DataView object.. 

        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "保养单号"
            Case 1
                objDataView.Sort = "设备编号"
            Case 2
                objDataView.Sort = "保养费用"
            Case 3
                objDataView.Sort = "保养级别"
            Case 4
                objDataView.Sort = "保养内容"
            Case 5
                objDataView.Sort = "保养时间"
            Case 6
                objDataView.Sort = "替换件编号"
            Case 7
                objDataView.Sort = "工时"
        End Select
        btnMoveFirst_Click(Nothing, Nothing)    '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted" '修改状态标签Text属性. Display a message that the records have been sorted..
    End Sub
    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        Dim intPosition As Integer        '执行查找,声明当前局部变量.'Declare local variables..
        Dim str条件 As String = ""
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
        Select Case 排序字段.SelectedIndex 'Determine the appropriate item selected And set the Sort property of the DataView object..
            Case 0
                objDataView.Sort = "保养单号"
                str条件 = "保养单号"              '相当于字段名
            Case 1
                objDataView.Sort = "设备编号"
                str条件 = "设备编号"
            Case 2
                objDataView.Sort = "保养费用"
                str条件 = "保养费用"
            Case 3
                objDataView.Sort = "保养级别"
                str条件 = "保养级别"
            Case 4
                objDataView.Sort = "保养内容"
                str条件 = "保养内容"
            Case 5
                objDataView.Sort = "保养时间"
                str条件 = "保养时间"
            Case 6
                objDataView.Sort = "替换件编号"
                str条件 = "替换件编号"
            Case 7
                objDataView.Sort = "工时"
                str条件 = "工时"
        End Select
        objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"  'DataView数据表中筛选数据集.相当于SQL语句.
        intPosition = objCurrencyManager.Position   '位置赋值给变量
        If intPosition = -1 Then                    '状态栏提示没有找到记录 Display a message that the record was not found..
            ToolStripLabel1.Text = "Record Not Found"
        Else
            ToolStripLabel1.Text = "Record Found" '否则状态栏显示已找到记录. Otherwise display a message that the record was
        End If
        ShowPosition()                            '重新显示当前记录位置. Show the current record position..
    End Sub


    '查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If 查询条件.Text.Length = 0 Then              '如果是空值
            B03_保养设备信息_Load(Nothing, Nothing)   '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置
        End If
    End Sub

    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    '新建按钮事件
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        Dim i As Byte = 0             '声明局部变量
        '控件的DataBindings属性(返回ControlBindingsCollection类)的Clear方法逐一清除控件上的绑定(控件可能之前的绑定DataView数据源)    
        myArray = {"保养单号", "设备编号", "保养费用", "保养级别", "保养内容", "保养时间", "替换件编号", "工时"}
        For i = 0 To UBound(myArray)                             '清空简单控件值
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
        保养单号1.Enabled = False                                '设置禁止使用控件
    End Sub

    '添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        Dim intMaxID As Integer    '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字'Declare local variables and objects. 
        Dim strID As String        '变量用来存储authors表的主键并设置authors表的新键.
        Dim objCommand As OleDbCommand = New OleDbCommand()  '实例化一个新查询实例.
        '创建一个命令实例,并传入SQL字符串. '从保养编号表中,按照指定条件(设备编号匹配数据库最后条记录)执行查询记录.
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 保养 ORDER BY 保养单号 DESC", objConnection2th)
        objConnection2th.Open()                            '打开数据库连接对象. 
        Dim maxId As Object = maxIdCommand.ExecuteScalar() '调用SqlCommand方法(只返回一行一列),并把结果赋值给变量(对象).
        If maxId Is DBNull.Value Then                      '如果返回结果是空值(If the MaxID column is null..),那么执行..
            intMaxID = 1000                                '设置一个默认值1000(Set a default value of 1000..)..
        Else
            strID = CType(maxId, String)                   '否则执行,将maxId(变量值)转换成String型,并赋值给变量strId..
            intMaxID = CType(strID.Remove(0, 2), Integer)  '利用Remove方法删除sb前缀,并转换整型,且赋值给变量intMaxID..
            intMaxID += 1                                  '变量加1..  Increment the value..
        End If
        '--------------------------------------------------------------------

        '变量转换成字符串,并与DM连接,构建一个新主键.   Finally, set the new ID..
        'strID = "SB" & intMaxID.ToString
        '变量转换成字符串,并与DM连接,构建一个新主键.   Finally, set the new ID..
        Select Case Len(intMaxID.ToString)
            Case 1
                strID = "BY00" & intMaxID.ToString
            Case 2
                strID = "BY0" & intMaxID.ToString
        End Select

        '设置命令对象的属性 Set the SqlCommand object properties..
        '将含有连接字符串的连接对象赋值给Connection属性
        objCommand.Connection = objConnection2th
        '保养单号.Enabled = True

        '将CommandText属性(要执行的SQL字符串)设置指定的值    myArray = {"", "", "", "", "", "", "", ""}
        objCommand.CommandText = "INSERT INTO 保养 " &
        "(保养单号, 设备编号, 保养费用, 保养级别, 保养内容, 保养时间, 替换件编号, 工时) " &
        "VALUES(@保养单号, @设备编号, @保养费用, @保养级别, @保养内容, @保养时间, @替换件编号, @工时)"

        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列.. 
        'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the ' CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@保养单号", strID)
        objCommand.Parameters.AddWithValue("@设备编号", 设备编号.Text)
        objCommand.Parameters.AddWithValue("@保养费用", 保养费用.Text).DbType _
            = DbType.Currency
        objCommand.Parameters.AddWithValue("@保养级别", 保养级别.Text)
        objCommand.Parameters.AddWithValue("@保养内容", 保养内容.Text)
        objCommand.Parameters.AddWithValue("@保养时间", 保养时间.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@替换件编号", 替换件编号.Text)
        objCommand.Parameters.AddWithValue("@工时", 工时.Text)

        myArray = {"保养单号", "设备编号", "保养费用", "保养级别", "保养内容", "保养时间", "替换件编号", "工时"}
        For i = 0 To UBound(myArray)
            If myArray(i).ToString <> "保养单号" Then
                If GroupBox1.Controls(myArray(i).ToString).Text.Length = 0 Then MsgBox("请输入完整数据在添加数据") : _
                    新建_Click(Nothing, Nothing) : objConnection2th.Close() : Exit Sub
            End If
        Next i

        '执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..
        Try '截取异常
            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
        Catch SqlExceptionErr As OleDbException
            MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示错误信息
        End Try '结束截取

        '关闭数据库连接 Close the connection..
        objConnection2th.Close()

        '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        B03_保养设备信息_Load(Nothing, Nothing)

        '设置你保存的那个记录位置    Set the record position to the one that you saved..
        objCurrencyManager.Position = objCurrencyManager.Count - 1

        ShowPosition()

        '状态栏显示你添加的信息   Display a message that the record was added..
        ToolStripLabel1.Text = "Record Added"
        履历卡()
    End Sub
    '--------------------------------------------------------------------------------------


    '更新数据库
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        '声明一个局部变量和创建一个命令对象  Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()

        '当前记录位置赋值给变量intPosstion. Save the current record position..
        intPosition = objCurrencyManager.Position

        '设置命令对象一些属性 Set the SqlCommand object properties..
        objCommand.Connection = objConnection2th  '使用数据库连接对象来设置命令对象的Connection属性.

        '接着使用SQL字符串设置CommandText属性.
        'SQL语句表示按照指定条件,更新表设备名称  "放置地点", "制造商", "制造日期", "保养费用", "运行状态"等

        objCommand.CommandText = "UPDATE 保养 " &
            "SET 设备编号 = @设备编号,保养费用 = @保养费用,保养级别 = @保养级别,保养内容 = @保养内容,保养时间 = @保养时间,替换件编号 = @替换件编号,工时 = @工时 WHERE 保养单号 = @保养单号"

        '命令命令类型为默认CommandType.Text类型,可以省略
        objCommand.CommandType = CommandType.Text

        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the
        ' CommandText property..
        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
        objCommand.Parameters.AddWithValue("@设备编号", 设备编号.Text)
        objCommand.Parameters.AddWithValue("@保养费用", 保养费用.Text).DbType = DbType.Currency  '转换类型.
        objCommand.Parameters.AddWithValue("@保养级别", 保养级别.Text)  '转换类型.
        objCommand.Parameters.AddWithValue("@保养内容", 保养内容.Text)
        objCommand.Parameters.AddWithValue("@保养时间", 保养时间.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@替换件编号", 替换件编号.Text)
        objCommand.Parameters.AddWithValue("@工时", 工时.Text)

        objCommand.Parameters.AddWithValue _
            ("@保养单号", BindingContext(objDataView).Current("保养单号"))

        '打开带连接字符的数据库连接  Open the connection..
        objConnection2th.Open()
        '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objCommand.ExecuteNonQuery()
        '关闭数据库连接  Close the connection..
        objConnection2th.Close()
        '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
        B03_保养设备信息_Load(Nothing, Nothing)
        ' 设置你保存过的记录位置 Set the record position to the one that you saved..
        objCurrencyManager.Position = intPosition

        '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        ShowPosition()
        '显示状态信息  Display a message that the record was updated..
        ToolStripLabel1.Text = "Record Updated"
        履历卡()
    End Sub

    '删除记录
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        '定义一个局部变量和命令对象 Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()

        '保存当前记录位置-1以用来记录删除位置.  Save the current record position—1 for the one to be
        ' deleted..
        intPosition = Me.BindingContext(objDataView).Position - 1

        '如果没有记录,则设置记录位置为o.    If the position is less than 0 set it to 0..
        If intPosition < 0 Then
            intPosition = 0
        End If

        '设置命令对象属性 Set the Command object properties..
        objCommand.Connection = objConnection2th
        objCommand.CommandText = "DELETE FROM 保养 " &
            "WHERE 保养单号 = @保养单号"

        '给title_id字段提供相应的参数  Parameter for the title_id field..
        objCommand.Parameters.AddWithValue _
        ("@保养单号", BindingContext(objDataView).Current("保养单号"))

        '打开数据库连接 Open the database connection..
        objConnection2th.Open()

        '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objCommand.ExecuteNonQuery()

        '关闭数据库连接 Close the connection..
        objConnection2th.Close()

        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        B03_保养设备信息_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition

        '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        ShowPosition()

        '显示一个已删除的信息.  Display a message that the record was deleted..
        ToolStripLabel1.Text = "Record Deleted"
        履历卡()
    End Sub

    '获取项目值模板
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
        履历卡()
    End Sub

    '退出
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection2th = Nothing            '清理连接对象,释放内存

        Globals.Ribbons.Ribbon1.Button28.Enabled = True

        Me.Close()

    End Sub

    '关闭
    Private Sub B03_保养设备信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection2th = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button28.Enabled = True

    End Sub


    Private Sub 设备编号_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 设备编号.SelectedIndexChanged
        履历卡()
    End Sub


End Class