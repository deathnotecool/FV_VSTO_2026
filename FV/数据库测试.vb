Imports System.Windows.Forms  '使用窗体命名空间
Imports System.Data     '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing      '使用颜色命名空间

Public Class 数据库测试
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim objConnection As New OleDbConnection _
                ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2 笔记记录\0 过程信息管理笔记\图书管理\图书管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\图书管理\图书管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\图书管理\图书管理.accdb")  '家里台式机
    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '该构造函数使用了SelectCommand属性的一个字符串和一个表示数据库连接的对象来初始化SqlAdapater对象.
    '这个构造函数使我们不必写Adapter属性代码.
    Dim objDataAdapter As New OleDbDataAdapter("SELECT authors.au_id, authors.au_lname, authors.au_fname, titles.title_id, titles.title, titles.price " &
            "From titles INNER Join (authors INNER Join titleauthor On authors.au_id = titleauthor.au_id) ON titles.title_id = titleauthor.title_id " &
            "Order By authors.au_lname, authors.au_fname", objConnection)

    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet As New DataSet()

    '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objDataView As DataView

    '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim objCurrencyManager As CurrencyManager

    '创建一个过程将在初始化代码中调用,以用来填充数据和显示数据以获得数据库中最新的数据填充DatSet对象.
    Private Sub FillDataSetAndView()
        '初始化DataSet对象的一个新实例.这里初始化表示 Initialize a new instance of the DataSet object.
        '我们不希望在已有记录的DataSet对象中添加新纪录,而是在一个新的DataSet对象中添加新纪录.
        objDataSet = New DataSet()

        '向DataSet对象填充由SqlDataAdapter对象的选择命令SelectCommand属性从数据库检索到的数据,命名相关的表的名称 
        '注意1:这里没有SelectCommand属性是因为已经在初始AdaPter是加入了相关字符串及数据库连接对象.
        '注意2:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,那么执行该选择命令,连接没打开就会自动打开填充数据后关闭连接  
        '注意3:Fill方法的第2参数可以随便写,即构造的Table表对象,但是建议填写数据源相关的表名,方便理解.Fill the DataSet object with data..
        objDataAdapter.Fill(objDataSet, "authors")

        '初始化DataView对象,查看来自DataSet对象中authnors表(设置DataSet类对应表数据源绑定到DataView类  
        'Set the DataView object to the DataSet object).DataView对象允许对DataSet中的记录进行排序、查找和浏览.
        objDataView = New DataView(objDataSet.Tables("authors"))

        '初始化CurrencyManager(管理DataView,Table,DataSet)对象,该对象的集合包含可用的数据源,包含于BindingContect(内置于Windows窗体中,无须创建)中,
        '指定CurrencyManger,以数据集视图为索引号,调用DataView数据源作为指定的CurrencyManager      Set our CurrencyManager object to the DataView object.
        objCurrencyManager =
                  CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub

    '创建一个过程以用来将窗体中的控件绑定到DataView对象上.
    Private Sub BindFields()
        '控件的DataBindings属性(返回ControlBindingsCollection类)的Clear方法,清除很重要,因为一旦通过增加、更新或删除行数据更改了DataView对象,
        '数据库更新而DataView对象只显示已更改的数据,所以逐一清除控件重新填充DataViw对象并重新绑定控件.    Clear any previous bindings..
        txtLastName.DataBindings.Clear()
        txtFirstName.DataBindings.Clear()
        txtBookTitle.DataBindings.Clear()
        txtPrice.DataBindings.Clear()

        '重新绑定数据源对象(DataView对象),控件的DataBindings属性(返回ControlBindingsCollection对象)的Add方法  Add new bindings to the DataView object..
        '第一参数是绑定到控件的所用的属性,第2参数是绑定的数据源(可以是DatSet、DataView、DataTable,这里是DataView对象,第3参数是数据源中的各个字段)
        txtLastName.DataBindings.Add("Text", objDataView, "au_lname")
        txtFirstName.DataBindings.Add("Text", objDataView, "au_fname")
        txtBookTitle.DataBindings.Add("Text", objDataView, "title")
        txtPrice.DataBindings.Add("Text", objDataView, "price")

        '显示一个"只读"状态    Display a ready status..
        ToolStripLabel1.Text = "Ready"
    End Sub

    'CurrencyManager对象会追踪DataView对象中当前记录的位置.
    '创建一个能在窗体上显示当前记录位置的过程
    Private Sub ShowPosition()
        'Format number in the txtPrice field to include cents
        Try
            txtPrice.Text = Format(CType(txtPrice.Text, Double), "c") '定义格式
        Catch e As System.Exception
            txtPrice.Text = "0"     '如果异常(文本框为空)那么将书价写为0
            txtPrice.Text = Format(CType(txtPrice.Text, Double), "c")  '重新转换Decimal类型.
        End Try

        '显示当前记录位置并标记记录数.    Display the current position and the number of records
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()
    End Sub

    '初始化代码-加载窗体触发的事件.
    Private Sub 数据库测试_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''初始化OleDbCommand类的一个实例,并将其分配给SelectCommand属性.   Set the SelectCommand properties..
        'objDataAdapter.SelectCommand = New OleDbCommand()

        ''将Connection属性设置为连接对象.用来与数据库通信.
        'objDataAdapter.SelectCommand.Connection = objConnection

        ''设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        ''该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).
        ''选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        'objDataAdapter.SelectCommand.CommandText = "SELECT authors.au_id, authors.au_lname, authors.au_fname, titles.title_id, titles.title, titles.price " &
        '    "From titles INNER Join (authors INNER Join titleauthor On authors.au_id = titleauthor.au_id) ON titles.title_id = titleauthor.title_id " &
        '    "Order By authors.au_lname, authors.au_fname"

        ''这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        'objDataAdapter.SelectCommand.CommandType = CommandType.Text

        ''上面设置完所有属性后,可以先打开数据库连接
        'objConnection.Open()

        ''数据适配器对象开始检索数据并填充到DataSet对象
        ''Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        ''Fill the DataSet object with data..
        'objDataAdapter.Fill(objDataSet, "authors")

        ''因为数据已填充到DataSet对象中了,所以可以关闭数据库的连接(通信)   Close the database connection..
        ''需要说明的是,Fill方法会执行SelectCommand,并保持为调用该方法时的状态.
        'objConnection.Close()

        ''让grd控件创建所需要的所有列.  Set the DataGridView properties to bind it to our data..
        'grdAuthorTitles.AutoGenerateColumns = True

        ''设置DataSet对象作为gird控件的数据源(实际上就是一个绑定过程,告知控件从哪里获得数据)
        'grdAuthorTitles.DataSource = objDataSet

        ''gird控件要显示数据源(填充过数据的DataSet对象)的表名称
        'grdAuthorTitles.DataMember = "authors"


        ''创建DataGridViewCellStyle对象(grd控件单元格样式实例)  Declare and set the currency header alignment property..
        'Dim objAlignRightCellStyle As New DataGridViewCellStyle

        ''将对齐方式改为居右对齐,从而能对价格字段进行对齐.
        'objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        ''定义交叉行样式,先创建DataGridViewCellStyle对象(grd控件单元格样式实例)     Declare and set the alternating rows style..
        'Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        'objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置样式背景色为烟白色
        'grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式

        ''创建DataGridViewCellStyle对象(grd控件单元格样式实例)   Declare and set the style for currency cells ..
        'Dim objCurrencyCellStyle As New DataGridViewCellStyle()

        ''设置单元格格式为货币型.
        'objCurrencyCellStyle.Format = "$#,##0.00"
        ''objCurrencyCellStyle.Format = "C"
        ''将对齐方式改为居右对齐
        'objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        ''设置控件列标题              Change column names and styles using the column index
        'grdAuthorTitles.Columns(0).HeaderText = "Last Name" '姓
        'grdAuthorTitles.Columns(1).HeaderText = "First Name"    '名
        'grdAuthorTitles.Columns(2).HeaderText = "Book Title"    '书名
        'grdAuthorTitles.Columns(2).Width = 225      '设置宽度大一点更易于读全书名

        ''改变价格标题名称和样式  Change column names and styles using the column name
        'grdAuthorTitles.Columns("price").HeaderCell.Value = "Retail Price"          '重新设置列标题的值显示为"Retail Price"
        'grdAuthorTitles.Columns("price").HeaderCell.Style = objAlignRightCellStyle '重新调用列标题样式(之前设定的)
        'grdAuthorTitles.Columns("price").DefaultCellStyle = objCurrencyCellStyle    '重新调用货币样式(之前设定的)

        ''清理内存及数据适配器对象
        'objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        'objConnection = Nothing            '清理连接对象,释放内存
        'objCurrencyCellStyle = Nothing     '清除货币单元格样式对象
        'objAlternatingCellStyle = Nothing  '清除交互单元格样式
        'objAlignRightCellStyle = Nothing   '清除右单元格样式

        '给组合框添加项目 Add items to the combo box..
        cboField.Items.Add("Last Name")  '添加列名"Last Name"
        cboField.Items.Add("First Name")  '添加列名"First Name"
        cboField.Items.Add("Book Title")     '添加列名""Book Title""
        cboField.Items.Add("Price")  '添加列名"Price"
        '选择第一项目作为默认显示项目  Make the first item selected..
        cboField.SelectedIndex = 0
        '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定控件      Fill the DataSet and bind the fields..
        FillDataSetAndView()
        BindFields()

        '调用过程显示当前记录位置    Show the current record position..
        ShowPosition()
    End Sub

    '处理过程显示DataView对象中的第一条记录,按钮单击事件.
    Private Sub btnMoveFirst_Click(Sender As Object,
            E As EventArgs) Handles btnMoveFirst.Click
        ' 设置第一条记录位置为0(即第1条记录)    Set the record position to the first record..
        objCurrencyManager.Position = 0

        '控件被绑定到DataView对象,是同步的,需要更新调用显示记录位置方法.   Show the current record position..
        ShowPosition()
    End Sub

    '移动上一条记录,按钮单击事件.
    Private Sub btnMovePrevious_Click(Sender As Object,
            E As EventArgs) Handles btnMovePrevious.Click

        '移动到上一条记录.      Move to the previous record..
        '注意:该记录不会移动到第1条记录之前,只会保持在位置0处.
        objCurrencyManager.Position -= 1

        '控件被绑定到DataView对象,CurrencyManager对象又是在管理是同步的,需要更新调用显示数据过程.  
        'Show the current record position..
        ShowPosition()
    End Sub

    '移动下一条记录,按钮单击事件.
    Private Sub btnMoveNext_Click(Sender As Object,
            E As EventArgs) Handles btnMoveNext.Click

        '移动下一条记录,       Move to the next record..
        objCurrencyManager.Position += 1

        '控件被绑定到DataView对象,是同步的,需要更新调用显示数据过程.   
        '同样的,CurrencyManager对象将检测出DataView对象的最后条记录,且不允许在下移.   Show the current record position..
        ShowPosition()
    End Sub

    '移动最后一条记录，按钮单击事件
    Private Sub btnMoveLast_Click(Sender As Object,
            E As EventArgs) Handles btnMoveLast.Click
        '移动到最后条记录. Set the record position to the last record..
        objCurrencyManager.Position = objCurrencyManager.Count - 1

        '控件被绑定到DataView对象,是同步的,需要更新调用显示数据过程.    Show the current record position..
        ShowPosition()
    End Sub

    '排序按钮,确定对哪个字段进行排序.单击事件
    Private Sub btnPerformSort_Click(Sender As Object,
        E As EventArgs) Handles btnPerformSort.Click

        '根据选定的项并设置DataView对象的相关字段的sort属性, 
        'Determine the appropriate item selected and set the Sort property of the DataView object..
        Select Case cboField.SelectedIndex
            Case 0 'Last Name
                objDataView.Sort = "au_lname"
            Case 1 'First Name
                objDataView.Sort = "au_fname"
            Case 2 'Book Title
                objDataView.Sort = "title"
            Case 3 'Price
                objDataView.Sort = "price"
        End Select

        '调用单击首条记录按钮  Call the click event for the MoveFirst button..
        btnMoveFirst_Click(Nothing, Nothing)

        ' '修改状态标签Text属性. Display a message that the records have been sorted..
        ToolStripLabel1.Text = "Records Sorted"
    End Sub

    '执行查找,单击事件
    Private Sub btnPerformSearch_Click(Sender As Object,
        E As EventArgs) Handles btnPerformSearch.Click
        '声明当前局部变量.   Declare local variables..
        Dim intPosition As Integer

        '根据选定的项并设置DataView对象的sort属性排序数据集     
        'Determine the appropriate item selected And set the Sort property of the DataView object..
        Select Case cboField.SelectedIndex
            Case 0 'Last Name
                objDataView.Sort = "au_lname"
            Case 1 'First Name
                objDataView.Sort = "au_fname"
            Case 2 'Book Title
                objDataView.Sort = "title"
            Case 3 'Price
                objDataView.Sort = "price"
        End Select

        '如果是项目索引在3以内,即非价格 If the search field is not price then..
        If cboField.SelectedIndex < 3 Then

            '直接搜索文本框Text属性 Find the last name, first name, or title..
            intPosition = objDataView.Find(txtSearchCriteria.Text)
        Else

            '否则肯定是价格搜索字段Text属性前,必须先转换成数据库对应的货币浮点型 . otherwise find the price..
            intPosition = objDataView.Find(CType(txtSearchCriteria.Text, Double))
        End If

        '如果没有搜索到记录
        If intPosition = -1 Then
            '状态栏提示没有找到记录 Display a message that the record was not found..
            ToolStripLabel1.Text = "Record Not Found"
        Else

            '否则状态栏显示已找到记录. Otherwise display a message that the record was
            ' found and reposition the CurrencyManager to that record..
            ToolStripLabel1.Text = "Record Found"
            '利用CurrencyManager类找到对应的位置
            objCurrencyManager.Position = intPosition
        End If
        '重新显示当前记录位置. Show the current record position..
        ShowPosition()
    End Sub

    '新建记录- 单击事件
    Private Sub btnNew_Click(Sender As Object,
            E As EventArgs) Handles btnNew.Click

        '清除书名和价格字段 Clear the book title and price fields..
        txtBookTitle.Text = ""
        txtPrice.Text = ""
    End Sub

    Private Sub btnAdd_Click(Sender As Object,
            E As EventArgs) Handles btnAdd.Click
        '声明一个局部变量intPosition作为记录位置,intMaxID作为最大连续数字        Declare local variables and objects..
        Dim intPosition As Integer, intMaxID As Integer

        Dim strID As String     '变量用来存储authors表的主键并设置authors表的新键

        Dim objCommand As OleDbCommand = New OleDbCommand()    '创建一个新的查询,准备向titleauthor和titles表中插入新记录.
        Dim objCommand1 As OleDbCommand = New OleDbCommand()
        '存贮当前记录位置给变量  Save the current record position..
        intPosition = objCurrencyManager.Position

        '创建一个命令实例并传入SQL字符串以及在整个程序走使用的连接  Create a new SqlCommand object..
        '从表titles中按照指定条件书名id匹配以DM开头的记录,选择聚合函数的字段并给予别名MaxId)
        Dim maxIdCommand As OleDbCommand = New OleDbCommand _
       ("SELECT MAX(title_id) AS MaxID " &
        "FROM titles WHERE title_id LIKE 'DM%'", objConnection)



        '打开数据库连接 Open the connection, execute the command
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

            '利用Remove方法删除DM前缀,转换整型赋值给变量intMaxID.       Get the integer part of the string..
            intMaxID = CType(strID.Remove(0, 2), Integer)

            '变量加1  Increment the value..
            intMaxID += 1
        End If

        '变量转换成字符串,并与DM连接,构建一个新主键.   Finally, set the new ID..
        strID = "DM" & intMaxID.ToString

        '设置命令对象的属性 Set the SqlCommand object properties..
        '将含有连接字符串的连接对象赋值给Connection属性
        objCommand.Connection = objConnection
        objCommand1.Connection = objConnection
        '将CommandText属性(要执行的SQL字符串)设置指定的值
        'SQL语句表示插入值到titles表和titleauthor表.
        '    objCommand.CommandText = "INSERT INTO titles " &
        '"(title_id, title, type, price, pubdate) " &
        '"VALUES(@title_id,@title,@type, @price, @pubdate)" '&
        '"INSERT INTO titleauthor (au_id, title_id) VALUES(@au_id,@title_id)"
        objCommand.CommandText = "INSERT INTO titles " &
        "(title_id, title, type, price, pubdate) " &
        "VALUES(@title_id,@title,@type, @price, @pubdate)"


        objCommand1.CommandText = "INSERT INTO titleauthor (au_id, title_id) VALUES(@au_id,@title_id)"



        '添加在SQL中的CommandText属性占位符参数,参数为title_id列.. 
        'AddWithValue方法接受参数名和要添加的对象 Add parameters For the placeholders In the SQL In the ' CommandText property..Parameter for the title_id column..
        objCommand.Parameters.AddWithValue("@title_id", strID)
            '添加在SQL中的CommandText属性占位符参数,参数为title列.. Parameter for the title column..
            objCommand.Parameters.AddWithValue("@title", txtBookTitle.Text)
            '添加在SQL中的CommandText属性占位符参数,参数为type列.. Parameter for the type column
            objCommand.Parameters.AddWithValue("@type", "Demo")
            '添加在SQL中的CommandText属性占位符参数,参数为price列.. Parameter for the price column..
            objCommand.Parameters.AddWithValue("@price", txtPrice.Text).DbType _
            = DbType.Currency
            '添加在SQL中的CommandText属性占位符参数,参数为pubdate列..price Parameter for the pubdate column
            objCommand.Parameters.AddWithValue("@pubdate", Date.Now.ToShortDateString)

        '添加在SQL中的CommandText属性占位符参数,参数为au_id列    Parameter for the au_id column..
        objCommand1.Parameters.AddWithValue _
                      ("@au_id", BindingContext(objDataView).Current("au_id"))

        objCommand1.Parameters.AddWithValue("@title_id", strID)

        '执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..
        Try '截取异常
            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据
            objCommand1.ExecuteNonQuery()  '执行命令对象以更新数据
        Catch SqlExceptionErr As OleDbException
            MessageBox.Show(SqlExceptionErr.Message)    '提示错误信息

        End Try '结束截取


        '关闭数据库连接 Close the connection..
        objConnection.Close()

        '调用方法填充数据到指定字段  Fill the dataset and bind the fields..
        FillDataSetAndView()
        BindFields()

        '设置你保存的那个记录位置    Set the record position to the one that you saved..
        objCurrencyManager.Position = intPosition

        '调用方法,显示当前记录 Show the current record position..
        ShowPosition()

        '状态栏显示你添加的信息   Display a message that the record was added..
        ToolStripLabel1.Text = "Record Added"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        xlapp.Range("a1").Value = Split("someone@example.com", "@", 1)



        'Try
        '    Me.Text = "版本V" & Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        'Catch ex As Exception
        '    Me.Text = "未知版本"
        'End Try


        'xlapp.Range("a1").Value = My.Application.Info.Version.ToString()



    End Sub

    'Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles 发生日期.TextChanged

    'End Sub

    Private Sub 发生日期_LostFocus(sender As Object, e As EventArgs)
        Format(CType(GroupBox1.Controls("发生日期").Text, Date), "yyyy/MM/dd") '定义格式
    End Sub
End Class