Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Public Class B01_设备基本信息
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim objConnection As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\设备管理.accdb")  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\设备管理\设备管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\设备管理\设备管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\设备管理.accdb")  '公司共享盘

    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '这个构造函数使我们不必写Adapter属性代码.
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 设备名称.* FROM 设备名称 ORDER BY 设备编号", objConnection)
    Dim objDataAdapter1th As New OleDbDataAdapter()   '来初始化SqlAdapater对象,需要使用SelectCommand属性.
    Dim objDataSet As New DataSet()    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet() '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView        '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objDataView1th As DataView     '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager  '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object                      '声明变量,数据库用

    '创建一个过程将在初始化代码中调用,以用来填充数据和显示数据
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '初始化一个数据集对象赋值给变量 Initialize a new instance of the DataSet object.
        '向DataSet对象填充由SqlDataAdapter对象的选择命令SelectCommand属性从数据库检索到的数据填充. 
        '注意:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,那么执行该选择命令,连接没打开就会自动打开填充数据后关闭连接  Fill the DataSet object with data..
        objDataAdapter.Fill(objDataSet, "sbxx")  '这里没有设置SelectCommand属性,因为在初始化Adapter对象时,已经使用了相应的参数(new构造SQL语句).
        objDataView = New DataView(objDataSet.Tables("sbxx")) '设置对应表为数据源绑定到DataView类Set the DataView object to the DataSet object.
        'BindingContect管理CurrencyManager(保持数据与控件同步的对象)集合.Set our CurrencyManager object to the DataView object.
        objCurrencyManager =
      CType(Me.BindingContext(objDataView), CurrencyManager) '这里的ObjCurrencyManager对象是一个包含DataView所有行数据集合对象,单行数据可索引.
    End Sub

    '创建一个过程,用来将窗体中的控件绑定到DataView对象.
    Private Sub BindFields()
        Dim i As Byte = 0   '声明变量,用来做数组字段的索引号.
        '控件的DataBindings属性的Clear方法逐一清除控件上的绑定(控件可能与之前数据源捆绑)'Clear any previous bindings..    
        myArray = {"设备编号", "设备名称", "型号规格", "放置地点", "制造商", "使用日期", "使用部门", "运行状态"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()  'Clear any previous bindings..
        Next i
        For i = 0 To UBound(myArray)                                      '控件逐一绑定DateView数据源,第3参数是数据字段
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "使用日期" Then GroupBox1.Controls(myArray(i).ToString).Text = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd")    '修改日期格式文本框显示短日期(可能数据库是长日期).
        Next i
        ToolStripLabel1.Text = "Ready"     '显示一个"准备"状态    Display a ready status..
    End Sub

    '创建一个能在窗体上显示当前记录位置的过程
    Private Sub ShowPosition()
        Try                                                                                        '错误处理
            使用日期.Text = Format(CType(GroupBox1.Controls("使用日期").Text, Date), "yyyy/MM/dd") '记录位置调用时,定义格式
        Catch e As System.Exception
            GroupBox1.Controls("使用日期").Text = CType(Now, String)                                '如果异常(文本框为空)那么将日期写为当前日期
            使用日期.Text = Format(CType(GroupBox1.Controls("使用日期").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
        End Try
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()       '显示当前记录位置并标记记录数. Display the current position and the number of records
    End Sub

    '按钮单击事件,移动第一条记录
    Private Sub btnMoveFirst_Click(Sender As Object,
            E As EventArgs) Handles btnMoveFirst.Click
        Dim intPosition As Integer
        ' Set the record position to the first record..
        objCurrencyManager.Position = 0 '设置当前记录为第一条记录,控件显示数据与指定记录自动同步的,只要不更新,就不存在数据源集的变更.
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition() '更新调用显示数据标签位置.' Show the current record position..
    End Sub

    '按钮单击事件,移动上一条记录
    Private Sub btnMovePrevious_Click(Sender As Object,
            E As EventArgs) Handles btnMovePrevious.Click
        Dim intPosition As Integer
        '移动上一条记录,只要不更新,就不存在数据源集的记录变更   'Move to the previous record.. 
        objCurrencyManager.Position -= 1 '控件被绑定到DataView数据源对象,所有控件记录集是同步的,需要更新调用显示数据位置标签过程. .
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition() '更新调用显示数据标签位置.' Show the current record position..
    End Sub

    '按钮单击事件,移动下一条记录
    Private Sub btnMoveNext_Click(Sender As Object,
            E As EventArgs) Handles btnMoveNext.Click
        Dim intPosition As Integer
        objCurrencyManager.Position += 1  '移动下一条记录,只要不更新,就不存在数据源集的记录变更 'Move to the next record.. 
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()                    '更新调用显示数据标签位置.' Show the current record position..
    End Sub

    '按钮单击事件,移动最后一条记录
    Private Sub btnMoveLast_Click(Sender As Object,
            E As EventArgs) Handles btnMoveLast.Click
        Dim intPosition As Integer
        objCurrencyManager.Position = objCurrencyManager.Count - 1  '移动最后一条记录' Set the record position to the last record..  
        intPosition = objCurrencyManager.Position                                                    '记录位置赋值给变量
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ShowPosition()                                           '更新调用显示数据标签位置.' Show the current record position..
    End Sub

    '窗体加载事件.
    Private Sub B01_设备基本信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        objDataAdapter.SelectCommand.CommandType = CommandType.Text    'SelectCommand的CommandType属性的默认属性是CommandType.Text.
        '补充说明,Fill方法会无需写代码也会执行SelectCommand的Connection属性,且保持为调用该方法时的状态.'Fill the DataSet and bind the fields..
        FillDataSetAndView()   '调用FillDataSetAndView过程检索数据 
        BindFields()           '调用BindFields过程绑定控件   
        ShowPosition()         '更新调用显示数据标签位置.' Show the current record position..
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.  Set the DataGridView properties to bind it to our data..
        grdAuthorTitles.DataSource = objDataSet     '设置DataSet对象作为gird控件的数据源(实际上就是一个绑定过程,告知控件从哪里获得数据)
        grdAuthorTitles.DataMember = "sbxx"         'gird控件要显示数据源(具体的表名称).
        Dim objAlignRightCellStyle As New DataGridViewCellStyle '初始化一个样式实例. 'Declare and set the currency header alignment property..
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '将对齐方式格式改为垂直居中右对齐.
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()              '定义交叉行样式. Declare and set the alternating rows style..
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke                    '设置样式背景色为烟灰色使用日期
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟灰色)
        'Declare and set the style for currency cells ..
        '设置单元格格式为货币型(参考).
        'objCurrencyCellStyle.Format = "￥#,##0.00"
        'objCurrencyCellStyle.Format = "C"
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '将对齐方式改为居中右对齐
        grdAuthorTitles.Columns(0).HeaderText = "设备编号"              '设置控件列标题  Change column names and styles using the column index
        grdAuthorTitles.Columns(1).HeaderText = "设备名称"
        grdAuthorTitles.Columns(2).HeaderText = "型号规格"
        grdAuthorTitles.Columns(3).HeaderText = "放置地点"
        grdAuthorTitles.Columns(4).HeaderText = "制造商"
        grdAuthorTitles.Columns(5).HeaderText = "使用日期"
        grdAuthorTitles.Columns(6).HeaderText = "使用部门"
        grdAuthorTitles.Columns(7).HeaderText = "运行状态"
        grdAuthorTitles.Columns(7).Width = 65 '设置指定列默认宽度小一点
        '改变字段标题名称和样式  'Change column names and styles using the column name 
        grdAuthorTitles.Columns("运行状态").HeaderCell.Value = "状态"                  '重新设置列标题的值显示为"状态"
        grdAuthorTitles.Columns("运行状态").HeaderCell.Style = objAlignRightCellStyle  '修改列标题样式(居中右对齐).
        grdAuthorTitles.Columns("运行状态").DefaultCellStyle = objCurrencyCellStyle    '设定指定列单元格样式(居中右对齐).
        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        排序字段.Items.Clear()
        For i = 0 To UBound(myArray)       '给组合框添加项目 Add items to the combo box..
            排序字段.Items.Add(GroupBox1.Controls(myArray(i).ToString).Name.ToString)
        Next i
        排序字段.SelectedIndex = 0         '默认选择第一项
    End Sub

    '创建一个履历视图显示方法,以供调用.
    Private Sub 履历卡()
        On Error Resume Next
        '创建DataGridViewCellStyle对象. 'Declare and set the style for currency cells ..
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()                     '初始化一个控件样式实例.
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft    '将对齐方式改为居中左对齐.(单元格如果宽度改变可能不太清晰)
        Dim objAlignRightCellStyle As New DataGridViewCellStyle                     '将对齐方式格式改为垂直居中右对齐. 
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '将对齐方式格式改为垂直居中右对齐.
        objDataAdapter1th.SelectCommand = New OleDbCommand()        '初始化OleDbCommand类的一个实例.   Set the SelectCommand properties..
        objDataAdapter1th.SelectCommand.Connection = objConnection  '将Connection属性设置为连接对象.用来与数据库通信.
        '设置选择命令字符串的CommandText属性(要执行的SQL语句也可以是存储过程)
        '选出指定列并按指定条件升序排序.
        objDataAdapter1th.SelectCommand.CommandText = "SELECT 维修.* FROM 维修 WHERE 维修.设备编号='" &
            GroupBox1.Controls("设备编号").Text & "' ORDER BY 维修单号"
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的d的CommandType默认属性是CommandType.Text(可以省略).
        '数据适配器对象检索数据并填充到DataSet, Fill方法的第二参数可以随便填,最好填相关的数据源表. 'Fill the DataSet object with data..
        objDataSet1th = New DataSet()                                '创建一个DataSet实例,相当于用内存来存储数据
        grdAuthorTitles1th.DataSource = objDataSet1th                '设置控件所显示的数据的数据源为DataSet(objDataSet1th)
        grdAuthorTitles1th.AutoGenerateColumns = True                '设置显示列为所有列.
        objDataAdapter1th.Fill(objDataSet1th, "wxxx1")               '向表(表如未创建,将自动创建)中填充数据.
        objDataView1th = New DataView(objDataSet1th.Tables("wxxx1")) '初始化并构造一个DataView实例
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()   '初始化一个DataGridViewCellStyle(数据库视图控件对象的样式).
        'objCurrencyCellStyle.Format = "￥#,##0.00"                  '设置样式格式为人民币样式.
        objCurrencyCellStyle.Format = "C"                            '设置样式格式为货币样式.(跟上句注释句一样的效果)
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke         '设置交叉行样式背景色为烟灰色
        'grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
        grdAuthorTitles1th.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
        '因为Fill方法启用前,链接是关闭的,所以打开链接后又关闭数据库的连接(通信)   Close the database connection..
        grdAuthorTitles1th.DataMember = "wxxx1"                 '设置数据源(objDataSet1th)中控件显示表的名称.
        grdAuthorTitles1th.Columns(0).HeaderText = "维修单号"   '设置标题名称,下同
        grdAuthorTitles1th.Columns(1).HeaderText = "设备编号"
        grdAuthorTitles1th.Columns(2).HeaderText = "申请人"
        grdAuthorTitles1th.Columns(3).HeaderText = "报修时间"
        grdAuthorTitles1th.Columns(4).HeaderText = "故障描述"
        grdAuthorTitles1th.Columns(5).HeaderText = "维修类型"
        grdAuthorTitles1th.Columns(6).HeaderText = "维修工时"
        grdAuthorTitles1th.Columns(7).HeaderText = "维修价格"
        grdAuthorTitles1th.Columns(8).HeaderText = "替换件编号"
        grdAuthorTitles1th.Columns(9).HeaderText = "修理描述"
        grdAuthorTitles1th.Columns("维修价格").HeaderCell.Value = "维修成本"              '重新设置列标题的值显示为"维修成本"
        grdAuthorTitles1th.Columns("维修价格").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-居中右对齐)
        grdAuthorTitles1th.Columns("维修价格").DefaultCellStyle = objCurrencyCellStyle    '单元格重新调用样式(之前设定的-居中左对齐,货币样式)
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

    '排序按钮,确定对哪个字段进行排序.单击事件 '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件进行排序
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性
        Select Case 排序字段.SelectedIndex 'Determine the appropriate item selected and set the Sort property of the DataView object.. 
            Case 0
                objDataView.Sort = "设备编号"   '按字段设备编号升序排序,下同.
            Case 1
                objDataView.Sort = "设备名称"
            Case 2
                objDataView.Sort = "型号规格"
            Case 3
                objDataView.Sort = "放置地点"
            Case 4
                objDataView.Sort = "制造商"
            Case 5
                objDataView.Sort = "使用日期"
            Case 6
                objDataView.Sort = "使用部门"
            Case 7
                objDataView.Sort = "运行状态"
        End Select
        btnMoveFirst_Click(Nothing, Nothing)     '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        ToolStripLabel1.Text = "Records Sorted"  '修改状态标签Text属性，提示已排序. Display a message that the records have been sorted..
    End Sub

    '创建查询方法
    Private Sub 执行查询_Click(sender As Object, e As EventArgs) Handles 执行查询.Click
        Dim intPosition As Integer '声明当前局部变量.  'Declare local variables..
        Dim str条件 As String = ""
        Select Case 排序字段.SelectedIndex      '根据选定的项并设置DataView对象(源数据是定制表sbxx)相关字段的sort属性.
            Case 0
                objDataView.Sort = "设备编号"   'ObjDataView的Sort属性设置为设备编号字段,即按设备编号排序.
                str条件 = "设备编号"            '设置变量为设备编号.
            Case 1
                objDataView.Sort = "设备名称"
                str条件 = "设备名称"
            Case 2
                objDataView.Sort = "型号规格"
                str条件 = "型号规格"
            Case 3
                objDataView.Sort = "放置地点"
                str条件 = "放置地点"
            Case 4
                objDataView.Sort = "制造商"
                str条件 = "制造商"
            Case 5
                objDataView.Sort = "使用日期"
                str条件 = "使用日期"
            Case 6
                objDataView.Sort = "使用部门"
                str条件 = "使用部门"
            Case 7
                objDataView.Sort = "运行状态"
                str条件 = "运行状态"
        End Select
        objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"  'DataView数据表中筛选数据集.
        intPosition = objCurrencyManager.Position                                    '默认位置赋值给变量(没有记录位置是-1,有的话是0)
        If intPosition = -1 Then                                                     '状态栏提示没有找到记录 
            ToolStripLabel1.Text = "Record Not Found"                                'Display a message that the record was not found..
        Else                                      'Otherwise display a message that the record was found                
            ToolStripLabel1.Text = "Record Found" 'and reposition the CurrencyManager to that record..
        End If
        ShowPosition()                            '重新显示当前记录位置. Show the current record position..
    End Sub

    '查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If 查询条件.Text.Length = 0 Then              '如果是空值
            B01_设备基本信息_Load(Nothing, Nothing)   '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置
        End If
    End Sub

    '按下Enter执行查询
    Private Sub 查询条件_KeyDown(sender As Object, e As KeyEventArgs) Handles 查询条件.KeyDown
        If e.KeyCode = Keys.Enter Then 执行查询_Click(Nothing, Nothing) '如果按下了Enter键,那么调用查询过程.
    End Sub

    '新建按钮事件
    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        Dim i As Byte = 0                                         '声明局部变量
        myArray = {"设备编号", "设备名称", "型号规格", "放置地点", "制造商", "使用日期", "使用部门", "运行状态"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).Text = ""     '清空简单控件值
        Next i
        设备编号.Enabled = False                                  '设置禁止使用控件
    End Sub

    '添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        Dim intMaxID As Integer                                 '声明一个局部变量intMaxID作为最大连续数字'Declare local variables and objects.. 
        Dim strID As String = ""                                '变量用来存储设备信息表的主键.
        Dim objCommand As OleDbCommand = New OleDbCommand()     '初始化一个新的命令,准备向设备信息表中插入新记录.
        '存贮当前记录位置给变量  Save the current record position..'创建一个命令实例(参数为Select开头的SQL字符串)  Create a new SqlCommand object..
        '从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 设备名称 ORDER BY 设备编号 DESC", objConnection)
        objConnection.Open()                                '打开数据库连接 Open the connection, execute the command.
        Dim maxId As Object = maxIdCommand.ExecuteScalar()  '调用SqlCommand的一个执行方法(只返回一行一列).并把结果赋值给变量
        If maxId Is DBNull.Value Then                       '如果返回结果是空值那么执行 If the MaxID column is null..
            intMaxID = 1000                                 '设置一个默认值1000. Set a default value of 1000..
        Else
            strID = CType(maxId, String)                    '否则将maxId转换成String型赋值给变量 otherwise set the strID variable to the value in MaxID..
            intMaxID = CType(strID.Remove(0, 2), Integer)   '利用Remove方法删除sb前缀,转换整型赋值给变量intMaxID. Get the integer part of the string..
            intMaxID += 1                                   '变量加1  Increment the value..
        End If
        Select Case Len(intMaxID.ToString)          '变量转换成字符串,并与SB连接,构建一个新主键. Finally, set the new ID..
            Case 1
                strID = "SB00" & intMaxID.ToString
            Case 2
                strID = "SB0" & intMaxID.ToString
            Case 3
                strID = "SB" & intMaxID.ToString
        End Select
        objCommand.Connection = objConnection  '设置命令对象的属性 Set the SqlCommand object properties..'将含有连接字符串的连接对象赋值给Connection属性
        objCommand.CommandText = "INSERT INTO 设备名称 " &
        "(设备编号, 设备名称, 型号规格, 放置地点, 制造商, 使用日期, 使用部门, 运行状态) " &
        "VALUES(@设备编号, @设备名称, @型号规格, @放置地点, @制造商, @使用日期, @使用部门, @运行状态)" '将CommandText属性(要执行的SQL字符串)设置指定的值
        'Add parameters For the placeholders In the SQL In the CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@设备编号", strID)          '添加CommandText属性占位符参数.. 'AddWithValue方法接受参数名和要添加的数据,下同. 
        objCommand.Parameters.AddWithValue("@设备名称", 设备名称.Text)
        objCommand.Parameters.AddWithValue("@型号规格", 型号规格.Text)
        objCommand.Parameters.AddWithValue("@放置地点", 放置地点.Text)
        objCommand.Parameters.AddWithValue("@制造商", 制造商.Text)
        objCommand.Parameters.AddWithValue("@使用日期", 使用日期.Text).DbType = DbType.Date '转换日期类型
        objCommand.Parameters.AddWithValue("@使用部门", 使用部门.Text)
        objCommand.Parameters.AddWithValue("@运行状态", 运行状态.Text)
        myArray = {"设备编号", "设备名称", "型号规格", "放置地点", "制造商", "使用日期", "使用部门", "运行状态"}
        For i = 0 To UBound(myArray)
            If myArray(i).ToString <> "设备编号" Then   '除了设备编号不要填,简单控件都要填数据.
                If GroupBox1.Controls(myArray(i).ToString).Text.Length = 0 Then MsgBox("请输入完整数据在添加数据") : _
                    新建_Click(Nothing, Nothing) : objConnection.Close() : Exit Sub
            End If
        Next i
        Try                                            '开始截取异常
            objCommand.ExecuteNonQuery()               '执行命令对象以更新数据(主要对数据库操作)Execute the SqlCommand object to insert the new data..
        Catch OledbExceptionErr As OleDbException
            MessageBox.Show(OledbExceptionErr.Message) '如果出错,提示数据库错误信息
        End Try                                        '结束截取
        objConnection.Close()                          '关闭数据库连接 Close the connection..
        B01_设备基本信息_Load(Nothing, Nothing)        '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        objCurrencyManager.Position = objCurrencyManager.Count - 1  '设置你保存的那个记录位置 Set the record position to the one that you saved..
        ShowPosition()                                 '标签显示位置.
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Added"          '状态栏显示你添加的信息.Display a message that the record was added..

    End Sub

    '更新数据库
    Private Sub 更新_Click(sender As Object, e As EventArgs) Handles 更新.Click
        Dim intPosition As Integer                          '声明一个局部变量  Declare local variables and objects..
        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个命令对象
        intPosition = objCurrencyManager.Position           '当前记录位置赋值给变量intPosstion. Save the current record position..
        '设置命令对象一些属性 Set the SqlCommand object properties..
        objCommand.Connection = objConnection  '使用数据库连接对象来设置命令对象的Connection属性.
        '接着使用SQL字符串设置CommandText属性.按照指定条件,更新表设备名称  "放置地点", "制造商", "使用日期", "使用部门", "运行状态"等
        objCommand.CommandText = "UPDATE 设备名称 " &
            "SET 设备名称 = @设备名称,型号规格 = @型号规格,放置地点 = @放置地点,制造商 = @制造商,使用日期 = @使用日期,使用部门 = @使用部门,运行状态 = @运行状态 WHERE 设备编号 = @设备编号"
        objCommand.CommandType = CommandType.Text   '命令命令类型为默认CommandType.Text类型,可以省略
        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the CommandText property.. '给参数设定值 Parameter for the title field..
        objCommand.Parameters.AddWithValue("@设备名称", 设备名称.Text)
        objCommand.Parameters.AddWithValue("@型号规格", 型号规格.Text)
        objCommand.Parameters.AddWithValue("@放置地点", 放置地点.Text)
        objCommand.Parameters.AddWithValue("@制造商", 制造商.Text)
        objCommand.Parameters.AddWithValue("@使用日期", 使用日期.Text).DbType = DbType.Date  '转换类型.
        objCommand.Parameters.AddWithValue("@使用部门", 使用部门.Text)
        objCommand.Parameters.AddWithValue("@运行状态", 运行状态.Text)
        objCommand.Parameters.AddWithValue _
            ("@设备编号", BindingContext(objDataView).Current("设备编号"))  '当前记录行位置,指定字段值写入参数
        objConnection.Open()                                                '打开带连接字符的数据库连接  Open the connection..
        objCommand.ExecuteNonQuery()              '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objConnection.Close()                     '关闭数据库连接  Close the connection..
        B01_设备基本信息_Load(Nothing, Nothing)   '调用方法显示数据和绑定字段(标签位置此时显示第一条)  Fill the DataSet and bind the fields..
        objCurrencyManager.Position = intPosition '设置你保存过的记录位置 Set the record position to the one that you saved..
        ShowPosition()      '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Updated"                                     '显示状态信息  Display a message that the record was updated..
    End Sub

    '删除记录
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        Dim intPosition As Integer                                  '定义一个局部变量和命令对象 Declare local variables and objects..
        Dim objCommand As OleDbCommand = New OleDbCommand()
        '保存当前记录位置-1以用来记录删除位置.  Save the current record position—1 for the one to be deleted..
        intPosition = Me.BindingContext(objDataView).Position - 1 '等同于 intPosition = objCurrencyManager.Position-1
        If intPosition < 0 Then                                   '如果为第一条记录
            intPosition = 0                                       '则设置记录位置为0. If the position is less than 0 set it to 0..
        End If
        objCommand.Connection = objConnection                     '设置命令对象属性 Set the Command object properties..
        objCommand.CommandText = "DELETE FROM 设备名称 " &
            "WHERE 设备编号 = @设备编号"
        '给title_id字段提供相应的参数  Parameter for the title_id field..
        objCommand.Parameters.AddWithValue _
        ("@设备编号", BindingContext(objDataView).Current("设备编号"))
        objConnection.Open()            '打开数据库连接 Open the database connection..
        objCommand.ExecuteNonQuery()    '执行命令查询以更新数据 Execute the SqlCommand object to update the data..
        objConnection.Close()           '关闭数据库连接 Close the connection..
        '填充数据并绑定字段 Fill the DataSet and bind the fields.. 'FillDataSetAndView() 'BindFields()
        B01_设备基本信息_Load(Nothing, Nothing)
        Me.BindingContext(objDataView).Position = intPosition  '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        ShowPosition()                  '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Deleted" '显示一个已删除的信息.  Display a message that the record was deleted..
    End Sub

    '选择项目(行)发生更改时,触发事件(注意:前面移动记录用到了禁止该过程,因为该方法的intPossiton跟面板信息记录位置不一致.)
    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        On Error Resume Next                                            '出错继续执行
        Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index   '当前行赋值给变量,初始是0
        BindFields()                                                    '绑定控件
        objCurrencyManager.Position = intPosition                       '显示对应记录信息
        ShowPosition()
    End Sub

    '退出
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button26.Enabled = True     '重新使按钮可用.
        Me.Close()  '关闭窗体
    End Sub

    '×关闭
    Private Sub B01_设备基本信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button26.Enabled = True  '重新使按钮可用.
    End Sub

    '根据设备编号同步履历卡
    Private Sub 设备编号_TextChanged(sender As Object, e As EventArgs) Handles 设备编号.TextChanged
        履历卡()       '调用设备编号对应的履历
    End Sub


    '用户提示：操作选项区域的状态栏信息.
    Private Sub B01_设备基本信息_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        StatusStrip1.Items.Clear()
        StatusStrip1.Items.Add("请根据需求谨慎操作    " & DateAndTime.Now)
    End Sub

    Private Sub 新建_MouseMove(sender As Object, e As MouseEventArgs) Handles 新建.MouseMove
        StatusStrip1.Items.Clear()
        StatusStrip1.Items.Add("单击此按钮，将清空输入栏以便新建一条记录")
    End Sub

    Private Sub 添加_MouseMove(sender As Object, e As MouseEventArgs) Handles 添加.MouseMove
        StatusStrip1.Items.Clear()
        StatusStrip1.Items.Add("单击此按钮，将添加一条记录")
    End Sub


#Region "StatuStrip功能演示"
    Private Sub 更新_MouseMove(sender As Object, e As MouseEventArgs) Handles 更新.MouseMove
        StatusStrip1.Items.Clear()
        StatusStrip1.Items.Add("单击此按钮，将更新一条记录")
    End Sub

    Private Sub 删除_MouseMove(sender As Object, e As MouseEventArgs) Handles 删除.MouseMove
        StatusStrip1.Items.Clear()
        StatusStrip1.Items.Add("单击此按钮，将删除一条记录")
    End Sub

    Private Sub 退出_MouseMove(sender As Object, e As MouseEventArgs) Handles 退出.MouseMove
        StatusStrip1.Items.Clear()
        StatusStrip1.Items.Add("单击此按钮，将退出系统")
    End Sub



#End Region


End Class