Imports System.Windows.Forms    '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data        '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb  '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing     '使用颜色命名空间
Public Class D03_调休节假日信息
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim objConnection1th As New OleDbConnection _
            ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb")  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\人力资源管理\人力资源管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\设备管理\设备管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb")  '公司共享盘
    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '这个构造函数使我们不必写Adapter属性SelectCommand相关代码.已经加入相关参数(SQL语句)
    Dim objDataAdapter As New OleDbDataAdapter("SELECT 调休与节假日.* FROM 调休与节假日 ORDER BY 日期", objConnection1th)
    Dim objDataSet As New DataSet()     '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView         '声明作用域为类级的对象,DataView类用来表示定制表-从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager   '声明作用域为类级的对象,CurrencyManger对象用于控制绑定数据的移动;作为管理Binding对象的列表
    Dim myArray() As String     '声明数组变量,数组长度为要引用的数据表字段数量.

    '创建一个过程,将在Load事件(初始化代码)调用,并用来填充数据和显示数据...
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()   '调用模块级对象,并重新初始化该(DataSet)对象
        '向DataSet对象填充由Sql(Ole)DataAdapter对象SelectCommand属性从数据库检索到的数据.. 
        '注意:Fill方法使用选择命令SelectCommand.Connection.如果该链接已打开,就会自动打开填充数据后保持打开连接对象,反之则反.  
        objDataAdapter.Fill(objDataSet, "jb")                 '表(zs)是初始构建起来的,命名为zs.
        objDataView = New DataView(objDataSet.Tables("jb"))   '初始化并构建DataView对象.
        'CurrencyManager(窗体获取到的数据记录集合)对象包含于BindingContect集合(内置于Win窗体,无须创建)中,
        '将DataView对象转化为CurrencyManager对象.
        objCurrencyManager = CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub

    '创建一个过程,逐一将窗体中的控件属性和指定数据源创建Binding,并将其添加到集合中.
    Private Sub BindFields()
        Dim i As Byte = 0
        '控件获取到的数据绑定(DataBindings属性),逐一清除(Clear方法)控件上的绑定(控件可能之前绑定过旧的DataView数据源) 
        myArray = {"分类", "日期", "加班倍数", "备注"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        '控件重新逐一绑定DateView数据源,add方法第一参数为要绑定的控件属性的名称,第二参数为要绑定的数据源,第三参数为要绑定给控件的数据字段(列表).
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "日期" Then GroupBox1.Controls(myArray(i).ToString).Text _
                = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Date), "yyyy/MM/dd") '转换日期格式类型.
        Next i
        ToolStripLabel1.Text = "Ready"   '显示一个"只读"状态..
    End Sub

    '创建过程,并显示当前单个记录的位置.
    Private Sub ShowPosition()
        Try                                                                                        '格式化日期指定短日期格式.
            日期.Text = Format(CType(GroupBox1.Controls("日期").Text, Date), "yyyy/MM/dd") '定义格式
        Catch e As System.Exception                                                                '声明一个错误变量类型
            GroupBox1.Controls("日期").Text = CType(Now, String)    '如果异常(文本框为空),那么转换当前日期类型为文本类型,并写入文本框中.
            日期.Text = Format(CType(GroupBox1.Controls("日期").Text, Date), "yyyy/MM/dd")  '重新转换Date类型.
        End Try
    End Sub

    '加载窗体触发事件
    Private Sub D03_调休节假日信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'On Error Resume Next                        '需要说明的是,Fill方法会执行命令(SelectCommand),其Connection属性保持为调用该方法时的状态.
        FillDataSetAndView()                        '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定数据源字段到指定控件.
        ShowPosition()                              '调用ShowPosition方法,并显示当前记录标签位置    
        'BindFields()                                '调用绑定控件过程,因为有复合框,所以放在事件最后面.
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.
        grdAuthorTitles.DataSource = objDataSet     '设置DataSet对象,作为gird控件的数据来源(实际上就是一个绑定过程,告知控件从哪里获得数据).
        grdAuthorTitles.DataMember = "jb"           '设置gird控件要显示的数据源(具体的表名称).
        Dim objAlignRightCellStyle As New DataGridViewCellStyle                       '初始化DataGridViewCellStyle对象(作为grd控件单元格或标题样式实例) 
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight   '将对齐方式格式改为垂直居中向右对齐,从而能对运行状态字段进行对齐.
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()   '初始化DataGridViewCellStyle对象(grd控件单元格样式实例) 作为交叉行样式  
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke     '设置交叉样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)
        'Dim objCurrencyCellStyle As New DataGridViewCellStyle()      '初始化DataGridViewCellStyle对象,将设置单元格格式为货币型.
        'objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式改为居中向左对齐
        ''objCurrencyCellStyle.Format = "$#,##0.00"                                '样式格式为货币型(美元$人民币¥)
        'objCurrencyCellStyle.Format = "C"                                         '样式格式为货币型(人民币)
        'myArray = {"分类", "日期", "加班倍数", "备注"}
        grdAuthorTitles.Columns(0).HeaderText = "分类"   '设置控件列标题   
        grdAuthorTitles.Columns(1).HeaderText = "日期"
        grdAuthorTitles.Columns(2).HeaderText = "加班倍数"
        grdAuthorTitles.Columns(3).HeaderText = "备注"
        grdAuthorTitles.Columns(3).Width = 395 '设置指定列默认宽度大一点
        'objCurrencyCellStyle = Nothing     '清除样式对象(单元格记录内容用)


        grdAuthorTitles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        分类.Items.Clear()         '给组合框添加项目  'Add items to the combo box..
        分类.Items.Add("调休日")
        分类.Items.Add("节假日")
        加班倍数.Items.Clear()    '给组合框添加项目  'Add items to the combo box..
        加班倍数.Items.Add("1.5")
        加班倍数.Items.Add("2")   '维修类型.SelectedIndex = 0    '默认选择第一项
        加班倍数.Items.Add("3")   '维修类型.SelectedIndex = 0    '默认选择第一项
        BindFields() '调用绑定控件过程
    End Sub

    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        Dim objCommand As OleDbCommand = New OleDbCommand() '创建一个新的查询.
        '创建一个命令实例并传入SQL字符串  Create a new SqlCommand object..'从表设备编号表中按照指定条件设备编号匹配数据库最后条的记录
        objConnection1th.Open()   '打开数据库连接 Open the connection, execute the command SELECT TOP 1 * FROM 表名 ORDER BY 排序字段 DESC
        objCommand.Connection = objConnection1th '设置命令对象的属性 Set the SqlCommand object properties..'将连接字符串的连接对象赋值给Connection属性
        '维修单号.Enabled = True'将CommandText属性(要执行的SQL字符串)设置指定的值
        'myArray = {"序列号", "姓名", "性别", "出生年月", "技术职称", "专业等级", "发证日期", "有效期至", "证件编号"}
        objCommand.CommandText = "INSERT INTO 调休与节假日 " &
        "(分类, 日期, 加班倍数, 备注) " &
        "VALUES(@分类, @日期, @加班倍数, @备注)"
        '添加在SQL中的CommandText属性占位符参数,参数为指定Parameters集合列..'AddWithValue方法接受参数名和要添加的对象 
        'Add parameters For the placeholders In the SQL In the 'CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@分类", 分类.Text)
        objCommand.Parameters.AddWithValue("@日期", 日期.Text).DbType = DbType.Date
        objCommand.Parameters.AddWithValue("@加班倍数", 加班倍数.Text)                                                             '转
        objCommand.Parameters.AddWithValue("@备注", 备注.Text)
        myArray = {"分类", "日期", "加班倍数", "备注"}
        Try                               '截取异常'执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..
            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
        Catch SqlExceptionErr As OleDbException         '声明异常类型
            MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示异常类型错误信息
        End Try                                         '结束截取
        objConnection1th.Close()                        '关闭数据库连接 Close the connection..
        D03_调休节假日信息_Load(Nothing, Nothing)         '调用方法填充数据到指定字段及绑定控件  Fill the dataset and bind the fields..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(objCurrencyManager.Count - 1).Cells(0)    '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Added"    '状态栏显示你添加的信息   Display a message that the record was added..
    End Sub

    Private Sub 新建_Click(sender As Object, e As EventArgs) Handles 新建.Click
        Dim i As Byte = 0             '声明局部变量
        myArray = {"分类", "日期", "加班倍数", "备注"}
        For i = 0 To UBound(myArray)  '清空简单控件值
            GroupBox1.Controls(myArray(i).ToString).Text = ""
        Next i
    End Sub

    '获取项目值
    Private Sub grdAuthorTitles_SelectionChanged(sender As Object, e As EventArgs) Handles grdAuthorTitles.SelectionChanged
        'On Error Resume Next
        Dim intPosition As Integer = grdAuthorTitles.CurrentRow.Index
        BindFields()
        objCurrencyManager.Position = intPosition
    End Sub

    Private Sub D03_调休节假日信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing  '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing   '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btn调休节假信息.Enabled = True  '重新启用按钮
    End Sub

    Private Sub 分类_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 分类.SelectedIndexChanged
        If 分类.Text = "调休日" Then
            加班倍数.Enabled = False
            加班倍数.Text = "1.5"
        Else
            加班倍数.Enabled = True
            加班倍数.Text = ""
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '定义一个局部变量和命令对象 Declare local variables and objects..
        Dim intPosition As Integer
        Dim objCommand As OleDbCommand = New OleDbCommand()
        '保存当前记录位置-1以用来记录删除位置. 
        intPosition = Me.BindingContext(objDataView).Position - 1
        If intPosition < 0 Then  '如果没有记录,则设置记录位置为o.    
            intPosition = 0
        End If
        objCommand.Connection = objConnection1th      '设置命令对象属性..
        objCommand.CommandText = "DELETE FROM 调休与节假日 " &
            "WHERE 日期 = @日期"
        '给“日期”字段提供相应的参数...
        objCommand.Parameters.AddWithValue _
        ("@日期", BindingContext(objDataView).Current("日期"))
        objConnection1th.Open()     '打开数据库连接 Open the database connection..
        objCommand.ExecuteNonQuery()     '执行命令查询以更新数据...
        objConnection1th.Close()         '关闭数据库连接 Close the connection..
        '填充数据并绑定字段 Fill the DataSet and bind the fields..
        'FillDataSetAndView()
        'BindFields()
        '注意:这里注释上面2句过程主要是为了调用Adapata
        D03_调休节假日信息_Load(Nothing, Nothing)
        '设置你保存过的位置给记录位置 Set the record position to the one that you saved..
        Me.BindingContext(objDataView).Position = intPosition
        ShowPosition()  '上面调用过程CurrrencyMananger默认显示第一个记录位置处,所以重新调用过程记录位置 Show the current record position..
        '显示一个已删除的信息.  Display a message that the record was deleted..
        RemoveHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged   '解除事件
        grdAuthorTitles.CurrentCell = grdAuthorTitles.Rows(intPosition).Cells(0)                     '视图控件指针选择指定行第一个单元格
        AddHandler grdAuthorTitles.SelectionChanged, AddressOf grdAuthorTitles_SelectionChanged      '绑定事件
        ToolStripLabel1.Text = "Record Deleted"
    End Sub

    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection1th = Nothing         '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.btn调休节假信息.Enabled = True
        Me.Close()
    End Sub


End Class