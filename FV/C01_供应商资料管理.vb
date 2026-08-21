Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Public Class C01_供应商资料管理
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb"
    Dim strYiFangPath As String = "\\192.168.3.52\Users\进销存管理.accdb"
    Dim strMyHomerComputerPath As String = "E:\access\进销存管理.accdb"
    Dim strMyCompanyComputerPath As String = "D:\6 总务\access\进销存管理.accdb"
    Dim objConnection As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\进销存管理\进销存管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\进销存管理\进销存管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb")  '公司共享盘

    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '该构造函数使用了SelectCommand属性的一个字符串和一个表示数据库连接的对象来初始化SqlAdapater对象.
    '这个构造函数使我们不必写Adapter属性代码.
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 供应商信息.* FROM 供应商信息 ORDER BY 供应商编码", objConnection)
    Dim objDataSet As New DataSet()    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView        '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager  '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object                      '声明变量,数据库用

    '创建一个过程将在初始化代码中调用,以用来填充数据和显示数据
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '创建并初始化一个数据集对象赋值给变量 Initialize a new instance of the DataSet object.
        '向DataSet对象填充由SqlDataAdapter对象的选择命令SelectCommand属性从数据库检索到的数据填充. 
        '注意:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,那么执行该选择命令,连接没打开就会自动打开填充数据后关闭连接  Fill the DataSet object with data..
        objDataAdapter.Fill(objDataSet, "GYSXX")  '这里没有设置SelectCommand属性,因为在初始化Adapter对象时,已经使用了相应的参数.
        '设置对应表为数据源绑定到DataView类  Set the DataView object to the DataSet object.
        objDataView = New DataView(objDataSet.Tables("GYSXX"))
        'BindingContect管理CurrencyManager(保持数据与控件同步的对象)集合,指定相应的CurrencyManger,引用定制视图源作为指定的CurrencyManager      Set our CurrencyManager object to the DataView object.
        objCurrencyManager =
      CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub

    '创建一个过程以用来将窗体中的控件绑定到DataView对象.
    Private Sub BindFields()
        Dim i As Byte = 0
        '控件的DataBindings属性(返回ControlBindingsCollection类)的Clear方法逐一清除控件上的绑定(控件可能与之前数据源捆绑)    
        'Clear any previous bindings..
        myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        '控件逐一绑定DateView数据源,第3参数是数据字段
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
        Next i
        ToolStripLabel1.Text = "Ready"     '显示一个"准备"状态    Display a ready status..
    End Sub

    '创建一个能在窗体上显示当前记录位置的过程
    Private Sub ShowPosition()
        '显示当前记录位置并标记记录数. Display the current position and the number of records
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()
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
    Private Sub C01_供应商资料管理_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        objDataAdapter.SelectCommand.CommandType = CommandType.Text    'SelectCommand的CommandType属性是CommandType.Text是默认属性.
        '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定控件      
        '需要说明的是,Fill方法会执行SelectCommand,并保持为调用该方法时的状态.
        'Fill the DataSet and bind the fields..
        FillDataSetAndView()

        ShowPosition()  '调用过程显示当前标签记录位置    Show the current record position..
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.  Set the DataGridView properties to bind it to our data..
        grdAuthorTitles.DataSource = objDataSet '设置DataSet对象作为gird控件的数据源(实际上就是一个绑定过程,告知控件从哪里获得数据)
        grdAuthorTitles.DataMember = "GYSXX"  'gird控件要显示数据源(填充过数据的DataSet对象)具体的表名称
        Dim objAlignRightCellStyle As New DataGridViewCellStyle '创建DataGridViewCellStyle对象(grd控件单元格样式实例) 'Declare and set the currency header alignment property..
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式格式改为垂直居中水平向左对齐.
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()    '定义交叉行样式Declare and set the alternating rows style..
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟灰色)
        '创建DataGridViewCellStyle对象(grd控件单元格样式实例)   
        'Declare and set the style for currency cells ..
        '设置单元格格式为货币型(参考).
        'objCurrencyCellStyle.Format = "$#,##0.00"
        'objCurrencyCellStyle.Format = "C"
        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft      '将对齐方式改为居右对齐
        '设置控件列标题  Change column names and styles using the column index
        'myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        grdAuthorTitles.Columns(0).HeaderText = "供应商编码"
        grdAuthorTitles.Columns(1).HeaderText = "供应商名称"
        grdAuthorTitles.Columns(2).HeaderText = "通讯地址"
        grdAuthorTitles.Columns(3).HeaderText = "邮政编码"
        grdAuthorTitles.Columns(4).HeaderText = "手机号码"
        grdAuthorTitles.Columns(5).HeaderText = "传真号码"
        grdAuthorTitles.Columns(6).HeaderText = "联系人"
        grdAuthorTitles.Columns(7).HeaderText = "电话"
        grdAuthorTitles.Columns(8).HeaderText = "Email"
        grdAuthorTitles.Columns(9).HeaderText = "备注"
        grdAuthorTitles.Columns(9).Width = 2065 '设置指定列默认宽度小一点
        grdAuthorTitles.Columns(1).Width = 265 '设置指定列默认宽度小一点
        grdAuthorTitles.Columns(2).Width = 265 '设置指定列默认宽度小一点
        '改变字段标题名称和样式  
        'Change column names and styles using the column name
        grdAuthorTitles.Columns("通讯地址").HeaderCell.Value = "地址"                  '重新设置列标题的值显示为"地址"
        grdAuthorTitles.Columns("通讯地址").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(垂直居中水平向左对齐.)
        grdAuthorTitles.Columns("通讯地址").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        For i = 0 To UBound(myArray)    '给组合框添加项目 Add items to the combo box..
            排序字段.Items.Add(GroupBox1.Controls(myArray(i).ToString).Name.ToString)
        Next i
        排序字段.SelectedIndex = 0       '默认选择第一项
        BindFields()


    End Sub



    '180120-排序按钮,确定对哪个字段进行排序.单击事件    '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件进行排序
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性, 
        'Determine the appropriate item selected and set the Sort property of the DataView object..
        'myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "供应商编码"
            Case 1
                objDataView.Sort = "供应商名称"
            Case 2
                objDataView.Sort = "通讯地址"
            Case 3
                objDataView.Sort = "邮政编码"
            Case 4
                objDataView.Sort = "手机号码"
            Case 5
                objDataView.Sort = "传真号码"
            Case 6
                objDataView.Sort = "联系人"
            Case 7
                objDataView.Sort = "电话"
            Case 8
                objDataView.Sort = "Email"
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
        Dim str条件 As String = ""
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性,  
        'Determine the appropriate item selected And set the Sort property of the DataView object..
        'myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "供应商编码"
                str条件 = "供应商编码"
            Case 1
                objDataView.Sort = "供应商名称"
                str条件 = "供应商名称"
            Case 2
                objDataView.Sort = "通讯地址"
                str条件 = "通讯地址"
            Case 3
                objDataView.Sort = "邮政编码"
                str条件 = "邮政编码"
            Case 4
                objDataView.Sort = "手机号码"
                str条件 = "手机号码"
            Case 5
                objDataView.Sort = "传真号码"
                str条件 = "传真号码"
            Case 6
                objDataView.Sort = "联系人"
                str条件 = "联系人"
            Case 7
                objDataView.Sort = "电话"
                str条件 = "电话"
            Case 8
                objDataView.Sort = "Email"
                str条件 = "Email"
            Case 9
                objDataView.Sort = "备注"
                str条件 = "备注"
        End Select
        'DataView数据表中筛选数据集.
        objDataView.RowFilter = UCase(str条件) & " like  '%" & 查询条件.Text & "%'"
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
            C01_供应商资料管理_Load(Nothing, Nothing)   '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置
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
        myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        '清空简单控件值
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
        供应商编码.Enabled = False  '设置禁止使用控件
    End Sub

    '180120-添加按钮事件
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click

        'On Error Resume Next
        '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字        
        'Declare local variables and objects..
        Dim intMaxID As Integer
        Dim strID As String = ""                                     '变量用来存储authors表的主键并设置authors表的新键
        Dim objCommand As OleDbCommand = New OleDbCommand()     '创建一个新的查询,准备向titleauthor和titles表中插入新记录.
        '存贮当前记录位置给变量  Save the current record position..
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..
        '从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT TOP 1 * FROM 供应商信息 ORDER BY 供应商编码 DESC", objConnection)
        '打开数据库连接 Open the connection, execute the command SELECT TOP 1 * FROM 表名 ORDER BY 排序字段 DESC
        objConnection.Open()
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
            intMaxID = CType(strID.Remove(0, 3), Integer)
            '变量加1  Increment the value..
            intMaxID += 1
        End If
        '变量转换成字符串,并与SB连接,构建一个新主键.   Finally, set the new ID..
        'strID = "SB" & intMaxID.ToString
        '变量转换成字符串,并与SB连接,构建一个新主键.   Finally, set the new ID..
        Select Case Len(intMaxID.ToString)
            Case 1
                strID = "GYS00" & intMaxID.ToString
            Case 2
                strID = "GYS0" & intMaxID.ToString
        End Select
        '设置命令对象的属性 Set the SqlCommand object properties..
        '将含有连接字符串的连接对象赋值给Connection属性
        objCommand.Connection = objConnection
        供应商编码.Enabled = True
        '将CommandText属性(要执行的SQL字符串)设置指定的值
        'myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        objCommand.CommandText = "INSERT INTO 供应商信息 " &
        "(供应商编码, 供应商名称, 通讯地址, 邮政编码, 手机号码, 传真号码, 联系人, 电话, Email, 备注) " &
        "VALUES(@供应商编码, @供应商名称, @通讯地址, @邮政编码, @手机号码, @传真号码, @联系人, @电话, @Email, @备注)"
        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列.. 
        'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the ' CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@供应商编码", strID)
        objCommand.Parameters.AddWithValue("@供应商名称", 供应商名称.Text)
        objCommand.Parameters.AddWithValue("@通讯地址", 通讯地址.Text)
        objCommand.Parameters.AddWithValue("@邮政编码", 邮政编码.Text)
        objCommand.Parameters.AddWithValue("@手机号码", 手机号码.Text)
        objCommand.Parameters.AddWithValue("@传真号码", 传真号码.Text)
        objCommand.Parameters.AddWithValue("@联系人", 联系人.Text)
        objCommand.Parameters.AddWithValue("@电话", 电话.Text)
        objCommand.Parameters.AddWithValue("@Email", Email.Text)
        objCommand.Parameters.AddWithValue("@备注", 备注.Text)

        myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        供应商编码.Text = strID
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
        C01_供应商资料管理_Load(Nothing, Nothing)
        '设置你保存的那个记录位置    Set the record position to the one that you saved..
        objCurrencyManager.Position = objCurrencyManager.Count - 1
        ShowPosition()
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        执行查询_Click(Nothing, Nothing)
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Added" '状态栏显示你添加的信息   Display a message that the record was added..
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
        '接着使用SQL字符串设置CommandText属性.
        'SQL语句表示按照指定条件,更新表设备名称  "放置地点", "制造商", "制造日期", "使用部门", "运行状态"等
        '  myArray = {"供应商编码", "供应商名称", "通讯地址", "邮政编码", "手机号码", "传真号码", "联系人", "电话", "Email", "备注"}
        objCommand.CommandText = "UPDATE 供应商信息 " &
            "SET 供应商名称 = @供应商名称,通讯地址 = @通讯地址,邮政编码 = @邮政编码,手机号码 = @手机号码,传真号码 = @传真号码,联系人 = @联系人,电话 = @电话,Email = @Email,备注 = @备注 WHERE 供应商编码 = @供应商编码"
        '命令命令类型为默认CommandType.Text类型,可以省略
        objCommand.CommandType = CommandType.Text
        '向Parameters(执行的SQL语句如果以参数形式传递,那么将形成一个参数集合)集合添加适当的参数
        ' Add parameters for the placeholders in the SQL in the
        ' CommandText property..
        '型号规格字段以相应的文本框Text属性传递给参数设定值      Parameter for the title field..
        objCommand.Parameters.AddWithValue("@供应商名称", 供应商名称.Text)
        objCommand.Parameters.AddWithValue("@通讯地址", 通讯地址.Text)
        objCommand.Parameters.AddWithValue("@邮政编码", 邮政编码.Text)
        objCommand.Parameters.AddWithValue("@手机号码", 手机号码.Text)
        objCommand.Parameters.AddWithValue("@传真号码", 传真号码.Text)
        objCommand.Parameters.AddWithValue("@联系人", 联系人.Text)
        objCommand.Parameters.AddWithValue("@电话", 电话.Text)
        objCommand.Parameters.AddWithValue("@Email", Email.Text)
        objCommand.Parameters.AddWithValue("@备注", 备注.Text)
        objCommand.Parameters.AddWithValue _
            ("@供应商编码", BindingContext(objDataView).Current("供应商编码"))
        '打开带连接字符的数据库连接  Open the connection..
        objConnection.Open()
        '执行命令对象以更新数据 Execute the SqlCommand object to update the data..
        objCommand.ExecuteNonQuery()
        '关闭数据库连接  Close the connection..
        objConnection.Close()
        '调用方法显示数据和绑定字段  Fill the DataSet and bind the fields..
        C01_供应商资料管理_Load(Nothing, Nothing)
        ' 设置你保存过的记录位置 Set the record position to the one that you saved..
        objCurrencyManager.Position = intPosition
        '加载窗体后,CurrencyManager默认显示的第一条记录,所以重新调用ShowPositon过程显示正确记录位置. Show the current record position..
        ShowPosition()
        '显示状态信息  Display a message that the record was updated..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        'grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
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
        objCommand.CommandText = "DELETE FROM 供应商信息 " &
            "WHERE 供应商编码 = @供应商编码"
        '给title_id字段提供相应的参数  Parameter for the title_id field..
        objCommand.Parameters.AddWithValue _
        ("@供应商编码", BindingContext(objDataView).Current("供应商编码"))
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
        C01_供应商资料管理_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition
        '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        ShowPosition()
        '显示一个已删除的信息.  Display a message that the record was deleted..
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
        Globals.Ribbons.Ribbon1.Button31.Enabled = True     '重新使按钮可用.
        Me.Close()  '关闭窗体
    End Sub

    '180120-关闭
    Private Sub C01_供应商资料管理_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button31.Enabled = True  '重新使按钮可用.
    End Sub




End Class