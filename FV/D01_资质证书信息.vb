Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Public Class D01_资质证书信息
    Private cmb As CmbDatagridbiew
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim objConnection1th As New OleDbConnection _
             ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\人力资源管理\人力资源管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\人力资源管理\人力资源管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\设备管理\设备管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb")  '公司共享盘
    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '这个构造函数使我们不必写Adapter属性SelectCommand相关代码.已经加入相关参数(SQL语句)
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 人员资质证书.* FROM 人员资质证书 ORDER BY 序列号", objConnection1th)
    'Dim objDataAdapter1th As New OleDbDataAdapter()  '该构造函数需要使用SelectCommand属性.用来填充履历卡数据的
    Dim objDataSet As New DataSet()     '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet()  '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView         '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objDataView1th As DataView      '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager   '声明作用域为类级的对象,CurrencyManger对象用于控制绑定数据的移动;作为管理Binding对象的列表
    Dim myArray() As String                       '声明数组变量,数组长度为要引用的数据表字段数量.

    '创建一个过程,将在Load事件(初始化代码)调用,并用来填充数据和显示数据.
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()                              '调用模块级对象,并重新初始化该(DataSet)对象
        '向DataSet对象填充由Sql(Ole)DataAdapter对象SelectCommand属性从数据库检索到的数据.. 
        '注意:Fill方法使用选择命令SelectCommand.Connection.如果该链接已打开,就会自动打开填充数据后保持打开连接对象,反之则反.  
        objDataAdapter.Fill(objDataSet, "zs")                 '表(zs)是初始构建起来的,命名为zs.
        objDataView = New DataView(objDataSet.Tables("zs"))   '初始化并构建DataView对象.


        'CurrencyManager(窗体获取到的数据记录集合)对象包含于BindingContect集合(内置于Win窗体,无须创建)中,
        '将DataView对象转化为CurrencyManager对象.
        objCurrencyManager = CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub

    '创建一个过程,逐一将窗体中的控件属性和指定数据源创建Binding,并将其添加到集合中.
    Private Sub BindFields()
        Dim i As Byte = 0
        '控件获取到的数据绑定(DataBindings属性),逐一清除(Clear方法)控件上的绑定(控件可能之前绑定过旧的DataView数据源) 
        myArray = {"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号", "有效"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        '控件重新逐一绑定DateView数据源,add方法第一参数为要绑定的控件属性的名称,第二参数为要绑定的数据源,第三参数为要绑定给控件的数据字段(列表).
        For i = 0 To UBound(myArray)
            'If GroupBox1.Controls(myArray(i).ToString).Name <> "有效" Then
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            'Else
            'GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Checked", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            'End If
            If GroupBox1.Controls(myArray(i).ToString).Name = "发证日期" Then GroupBox1.Controls(myArray(i).ToString).Text _
                = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
            If GroupBox1.Controls(myArray(i).ToString).Name = "有效期至" Then GroupBox1.Controls(myArray(i).ToString).Text _
            = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
            'If GroupBox1.Controls(myArray(i).ToString).Name = "有效" Then GroupBox1.Controls(myArray(i).ToString).Text _
            '    = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
        Next i
        ToolStripLabel1.Text = "Ready"                                                            '显示一个"只读"状态..
    End Sub

    '创建过程,并显示当前单个记录的位置.
    Private Sub ShowPosition()
        Try                                                                                        '格式化日期指定短日期格式.
            发证日期.Text = Format(CType(GroupBox1.Controls("发证日期").Text, Date), "yyyy/MM/dd") '定义格式
            有效期至.Text = Format(CType(GroupBox1.Controls("有效期至").Text, Date), "yyyy/MM/dd") '定义格式
        Catch e As System.Exception                                                                '声明一个错误变量类型
            GroupBox1.Controls("发证日期").Text = CType(Now, String)    '如果异常(文本框为空),那么转换当前日期类型为文本类型,并写入文本框中.
            GroupBox1.Controls("有效期至").Text = CType(Now, String)    '如果异常(文本框为空),那么转换当前日期类型为文本类型,并写入文本框中.
            发证日期.Text = Format(CType(GroupBox1.Controls("发证日期").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
            有效期至.Text = Format(CType(GroupBox1.Controls("有效期至").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
        End Try
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()                                 '显示当前记录位置,并标记记录数. 
    End Sub

    '按钮单击事件,移动第一条记录
    Private Sub btnMoveFirst_Click(Sender As Object,
            E As EventArgs) Handles btnMoveFirst.Click
        Dim intPosition As Integer
        objCurrencyManager.Position = 0  '设置当前记录为第一条记录.
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件关联
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()    '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
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
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
    End Sub

    '按钮单击事件,移动下一条记录
    Private Sub btnMoveNext_Click(Sender As Object,
            E As EventArgs) Handles btnMoveNext.Click
        Dim intPosition As Integer
        '移动下一条记录. 
        objCurrencyManager.Position += 1                                                     'Move to the next record..
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
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
        ShowPosition()  '控件与数据源(objDataView)绑定,通过CurrencyManager指定位置,因为控件绑定同一数据源,所以控件显示的记录是同步的.
    End Sub

    '加载窗体触发事件
    Private Sub D01_资质证书信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next                        '需要说明的是,Fill方法会执行命令(SelectCommand),其Connection属性保持为调用该方法时的状态.
        FillDataSetAndView()                        '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定数据源字段到指定控件.
        ShowPosition()                              '调用ShowPosition方法,并显示当前记录标签位置    
        'BindFields()                                '调用绑定控件过程,因为有复合框,所以放在事件最后面.
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.
        grdAuthorTitles.DataSource = objDataSet     '设置DataSet对象,作为gird控件的数据来源(实际上就是一个绑定过程,告知控件从哪里获得数据).
        grdAuthorTitles.DataMember = "zs"           '设置gird控件要显示的数据源(具体的表名称).
        Dim objAlignRightCellStyle As New DataGridViewCellStyle                       '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例) 
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight   '将对齐方式格式改为垂直居中向右对齐,从而能对运行状态字段进行对齐.
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()                '初始化DataGridViewCellStyle对象(grd控件单元格样式实例) 作为交叉行样式  
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke                      '设置交叉样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)


        'Dim objCurrencyCellStyle As New DataGridViewCellStyle()      '初始化DataGridViewCellStyle对象,将设置单元格格式为货币型.
        'objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式改为居中向左对齐
        ''objCurrencyCellStyle.Format = "$#,##0.00"                                '样式格式为货币型(美元$人民币¥)
        'objCurrencyCellStyle.Format = "C"                                         '样式格式为货币型(人民币)
        grdAuthorTitles.Columns(0).HeaderText = "序列号"   '设置控件列标题   
        grdAuthorTitles.Columns(1).HeaderText = "姓名"
        grdAuthorTitles.Columns(2).HeaderText = "性别"
        grdAuthorTitles.Columns(3).HeaderText = "出生年月"
        grdAuthorTitles.Columns(4).HeaderText = "技术职称"
        grdAuthorTitles.Columns(5).HeaderText = "专业等级"
        grdAuthorTitles.Columns(6).HeaderText = "发证日期"
        grdAuthorTitles.Columns(7).HeaderText = "有效期至"
        grdAuthorTitles.Columns(8).HeaderText = "证件编号"
        grdAuthorTitles.Columns(8).Width = 130 '设置指定列默认宽度大一点
        grdAuthorTitles.Columns(9).HeaderText = "效"
        grdAuthorTitles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter '默认标题名称居中...
        ''改变字段标题名称和样式'Change column names and styles using the column name  
        'grdAuthorTitles.Columns("维修价格").HeaderCell.Value = "维修成本"              '重新设置列标题的值显示为"描述"
        'grdAuthorTitles.Columns("维修价格").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-居中右对齐)
        'grdAuthorTitles.Columns("维修价格").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        '遍历记录数量
        For i As Integer = 0 To grdAuthorTitles.RowCount - 1                           '有一个空白行也算一行
            If Math.Ceiling(CType(grdAuthorTitles.Item(7, i).Value.ToString(), Date).Subtract(Now).TotalDays) <= 20 Then
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
            Else
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
            End If
        Next
        'objCurrencyCellStyle = Nothing     '清除样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        排序字段.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        排序字段.Items.AddRange(myArray)
        排序字段.SelectedIndex = 0         '默认选择第一项
        性别.Items.Clear()             '给组合框添加项目  'Add items to the combo box..
        性别.Items.Add("男")
        性别.Items.Add("女")   '维修类型.SelectedIndex = 0    '默认选择第一项
        BindFields() '调用绑定控件过程
        cmb = New CmbDatagridbiew(Me.grdAuthorTitles)
        cmb.Add(0, 1, 0, 3) '代表（0，1）单元格 与（0，3）单元格合并

        'objDataAdapter1th.SelectCommand = New OleDbCommand()            '初始化一个命令对象
        'objDataAdapter1th.SelectCommand.Connection = objConnection2th   '建立与数据库的连接
        'objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "设备编号" & " from " & "设备名称 ORDER BY 设备编号" '写入SQL语句
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        'objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        'objDataAdapter1th.Fill(objDataSet1th, "sbxx02")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        'Dim tb As DataTable = objDataSet1th.Tables("sbxx02") '声明一个表类型,并赋值给该变量.
        '设备编号.Items.Clear()                               '清楚复合框项目集
        'For inCounter = 0 To tb.Rows.Count - 1               '在表行数上循环
        '    设备编号.Items.Add(tb.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        'Next
    End Sub



    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件数据源进行排序.
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性.
        Select Case 排序字段.SelectedIndex      'Determine the appropriate item selected and set the Sort property of the DataView object..            
            '"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"
            Case 0
                objDataView.Sort = "序列号"   '按字段设备编号升序排序,下同.
            Case 1
                objDataView.Sort = "姓名"
            Case 2
                objDataView.Sort = "性别"
            Case 3
                objDataView.Sort = "出生年月"
            Case 4
                objDataView.Sort = "技术职称"
            Case 5
                objDataView.Sort = "专业等级"
            Case 6
                objDataView.Sort = "发证日期"
            Case 7
                objDataView.Sort = "有效期至"
            Case 8
                objDataView.Sort = "证件编号"
        End Select
        btnMoveFirst_Click(Nothing, Nothing)      '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted"   '修改状态标签Text属性. Display a message that the records have been sorted..
    End Sub

    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        Dim intPosition As Integer              '执行查找,声明当前局部变量.'Declare local variables.. 
        Dim str条件 As String = ""
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
        'Determine the appropriate item selected And set the Sort property of the DataView object..
        Select Case 排序字段.SelectedIndex
              '"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"
            Case 0
                objDataView.Sort = "序列号"
                str条件 = "序列号"
            Case 1
                objDataView.Sort = "姓名"
                str条件 = "姓名"
            Case 2
                objDataView.Sort = "性别"
                str条件 = "性别"
            Case 3
                objDataView.Sort = "出生年月"
                str条件 = "出生年月"
            Case 4
                objDataView.Sort = "技术职称"
                str条件 = "技术职称"
            Case 5
                objDataView.Sort = "专业等级"
                str条件 = "专业等级"
            Case 6
                objDataView.Sort = "发证日期"
                str条件 = "发证日期"
            Case 7
                objDataView.Sort = "有效期至"
                str条件 = "有效期至"
            Case 8
                objDataView.Sort = "证件编号"
                str条件 = "证件编号"

        End Select
        objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"    'DataView数据表中筛选数据集(类似SQL语句).
        intPosition = objCurrencyManager.Position                                      '默认位置0赋值给变量
        If intPosition = -1 Then                            '状态栏提示没有找到记录 Display a message that the record was not found..
            ToolStripLabel1.Text = "Record Not Found"       '标签显示字符.
        Else    '否则状态栏显示字符. Otherwise display a message that the record was ' found and reposition the CurrencyManager to that record..
            ToolStripLabel1.Text = "Record Found"
        End If
        ShowPosition()                                      '重新显示当前记录位置. Show the current record position..
    End Sub

    '查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If 查询条件.Text.Length = 0 Then              '如果是空值
            D01_资质证书信息_Load(Nothing, Nothing)   '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置
        End If
    End Sub

    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    '新建按钮事件
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        Dim i As Byte = 0             '声明局部变量
        myArray = {"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"}
        For i = 0 To UBound(myArray)  '清空简单控件值
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
        序列号.Enabled = False      '设置禁止使用控件
    End Sub
    '添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        Dim intMaxID As Integer     '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字'Declare local variables and objects..  
        Dim strID As String = ""    '变量用来存储authors表的主键并设置authors表的新键
        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个新的查询.
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..'从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 人员资质证书 ORDER BY 序列号 DESC", objConnection1th)  '存贮当前记录位置给变量  Save the current record position..
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
        '维修单号.Enabled = True'将CommandText属性(要执行的SQL字符串)设置指定的值
        'myArray = {"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"}
        objCommand.CommandText = "INSERT INTO 人员资质证书 " &
        "(序列号, 姓名, 性别, 出生年月, 技术职称, 专业等级, 发证日期, 有效期至, 证件编号) " &
        "VALUES(@序列号, @姓名, @性别, @出生年月, @技术职称, @专业等级, @发证日期, @有效期至, @证件编号)"
        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列..'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the 'CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@序列号", strID)          '指定参数写入值,下同.
        objCommand.Parameters.AddWithValue("@姓名", 姓名.Text)
        objCommand.Parameters.AddWithValue("@性别", 性别.Text)
        objCommand.Parameters.AddWithValue("@出生年月", 出生年月.Text) '转换日期类型
        objCommand.Parameters.AddWithValue("@技术职称", 技术职称.Text)
        objCommand.Parameters.AddWithValue("@专业等级", 专业等级.Text)
        objCommand.Parameters.AddWithValue("@发证日期", 发证日期.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@有效期至", 有效期至.Text).DbType = DbType.Date                                                              '转
        objCommand.Parameters.AddWithValue("@证件编号", 证件编号.Text)
        myArray = {"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"}
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
        D01_资质证书信息_Load(Nothing, Nothing)         '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        objCurrencyManager.Position = objCurrencyManager.Count - 1   '设置你保存的那个记录位置    Set the record position to the one that you saved..
        ShowPosition()                                               '标签显示位置.
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
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
        'SQL语句表示按照指定条件,更新表设备名称  "放置地点", "制造商", "制造日期", "使用部门", "运行状态"等
        'myArray = {"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"}
        objCommand.CommandText = "UPDATE 人员资质证书 " &
            "SET 姓名 = @姓名,性别 = @性别,出生年月 = @出生年月,技术职称 = @技术职称,专业等级 = @专业等级,发证日期 = @发证日期,有效期至 = @有效期至,证件编号 = @证件编号 WHERE 序列号 = @序列号"  ' '接着使用SQL字符串设置CommandText属性.
        objCommand.CommandType = CommandType.Text '命令类型为默认CommandType.Text类型,可以省略
        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the
        ' CommandText property..
        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
        objCommand.Parameters.AddWithValue("@姓名", 姓名.Text)
        objCommand.Parameters.AddWithValue("@性别", 性别.Text)
        objCommand.Parameters.AddWithValue("@出生年月", 出生年月.Text)
        objCommand.Parameters.AddWithValue("@技术职称", 技术职称.Text)
        objCommand.Parameters.AddWithValue("@专业等级", 专业等级.Text)
        objCommand.Parameters.AddWithValue("@发证日期", 发证日期.Text).DbType = DbType.Date  '转换类型.
        objCommand.Parameters.AddWithValue("@有效期至", 有效期至.Text).DbType = DbType.Date  '转换类型..
        objCommand.Parameters.AddWithValue("@证件编号", 证件编号.Text)
        objCommand.Parameters.AddWithValue _
            ("@序列号", BindingContext(objDataView).Current("序列号"))
        objConnection1th.Open()                 '打开带连接字符的数据库连接  Open the connection..
        objCommand.ExecuteNonQuery()            '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()                '关闭数据库连接  Close the connection..
        D01_资质证书信息_Load(Nothing, Nothing) '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
        objCurrencyManager.Position = intPosition   ' 设置你保存过的记录位置 Set the record position to the one that you saved..
        ShowPosition() '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        '显示状态信息  Display a message that the record was updated..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
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
        If intPosition < 0 Then  '如果没有记录,则设置记录位置为o.    If the position is less than 0 set it to 0..
            intPosition = 0
        End If
        objCommand.Connection = objConnection1th      '设置命令对象属性 Set the Command object properties..
        objCommand.CommandText = "DELETE FROM 人员资质证书 " &
            "WHERE 序列号 = @序列号"
        '给title_id字段提供相应的参数  Parameter for the title_id field..
        objCommand.Parameters.AddWithValue _
        ("@序列号", BindingContext(objDataView).Current("序列号"))
        objConnection1th.Open()     '打开数据库连接 Open the database connection..
        objCommand.ExecuteNonQuery()     '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objConnection1th.Close()         '关闭数据库连接 Close the connection..
        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        D01_资质证书信息_Load(Nothing, Nothing)
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
        Globals.Ribbons.Ribbon1.btn证书.Enabled = True
        Me.Close()
    End Sub

    '关闭
    Private Sub D01_资质证书信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing         '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btn证书.Enabled = True
    End Sub


End Class