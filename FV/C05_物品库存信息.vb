Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间

Public Class C05_物品库存信息
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb"
    Dim strYiFangPath As String = "\\192.168.3.52\Users\进销存管理.accdb"
    Dim strMyHomerComputerPath As String = "E:\access\进销存管理.accdb"
    Dim strMyCompanyComputerPath As String = "D:\6 总务\access\进销存管理.accdb"
    Dim objConnection As New OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)  '公司共享盘


    Dim objDataAdapter As New OleDbDataAdapter("SELECT 库存信息.* FROM 库存信息 ORDER BY 物品编码", objConnection)
    Dim objDataAdapter1th As New OleDbDataAdapter()
    Dim objDataSet As New DataSet()    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView        '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager  '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object                      '声明变量,数据库用

    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '重新初始化一个数据集对象赋值给变量 Initialize a new instance of the DataSet object.
        '向DataSet对象填充由SqlDataAdapter对象的选择命令SelectCommand属性从数据库检索到的数据填充. 
        '注意:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,那么执行该选择命令,连接没打开就会自动打开填充数据后关闭连接  Fill the DataSet object with data..
        objDataAdapter.Fill(objDataSet, "KCXX01")  '这里没有设置SelectCommand属性,因为在初始化Adapter对象时,已经使用了相应的参数.
        '设置对应表为数据源绑定到DataView类  Set the DataView object to the DataSet object.
        objDataView = New DataView(objDataSet.Tables("KCXX01"))
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
        myArray = {"物品编码", "物品名称", "物品规格", "计量单位", "库存数量", "进货单价", "库存金额"}
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Clear()
        Next i
        '控件逐一绑定DateView数据源,第3参数是数据字段
        For i = 0 To UBound(myArray)
            GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
            If GroupBox1.Controls(myArray(i).ToString).Name = "库存金额" Then GroupBox1.Controls(myArray(i).ToString).Text = Format(CType(GroupBox1.Controls(myArray(i).ToString).Text, Integer), "#,##0.00")
            'GroupBox1.Controls(myArray(i).ToString).DataBindings.Add("Text", objDataView, GroupBox1.Controls(myArray(i).ToString).Name)
        Next i
        ToolStripLabel1.Text = "Ready"     '显示一个"准备"状态    Display a ready status..
    End Sub

    '创建一个能在窗体上显示当前记录位置的过程
    Private Sub ShowPosition()
        '格式化数字txtPrice字段,包含美分.   Format number in the txtPrice field to include cents
        Try
            库存金额.Text = Format(CType(GroupBox1.Controls("库存金额").Text, Integer), "¥#,##0.00") '定义格式
        Catch e As System.Exception
            GroupBox1.Controls("库存金额").Text = CType(0, String)   '如果异常(文本框为空)那么将日期写为当前日期
            库存金额.Text = Format(CType(GroupBox1.Controls("库存金额").Text, Integer), "¥#,##0.00")  '重新转换Date类型.
        End Try
        '显示当前记录位置并标记记录数. Display the current position and the number of records
        txtRecordPosition.Text = objCurrencyManager.Position + 1 &
    " of " & objCurrencyManager.Count()
    End Sub

    '180125 按钮单击事件,移动第一条记录
    Private Sub btnMoveFirst_Click(Sender As Object,
            E As EventArgs) Handles btnMoveFirst.Click
        '设置当前记录为第一条记录,不需要调用重新绑定,自动同步的,只要不更新,就不存在数据源集的变更   
        ' Set the record position to the first record..
        objCurrencyManager.Position = 0
        '控件被绑定到DataView数据源对象,所有控件记录集是同步的,需要更新调用显示数据位置标签过程.  
        ' Show the current record position..
        ShowPosition()
    End Sub
    '180125-按钮单击事件,移动上一条记录
    Private Sub btnMovePrevious_Click(Sender As Object,
            E As EventArgs) Handles btnMovePrevious.Click
        '移动上一条记录,只要不更新,就不存在数据源集的记录变更  
        'Move to the previous record..
        objCurrencyManager.Position -= 1
        '控件被绑定到DataView数据源对象,所有控件记录集是同步的,需要更新调用显示数据位置标签过程. .  Show the current record position..
        ShowPosition()
    End Sub
    '180119-按钮单击事件,移动下一条记录
    Private Sub btnMoveNext_Click(Sender As Object,
            E As EventArgs) Handles btnMoveNext.Click
        '移动下一条记录,只要不更新,就不存在数据源集的记录变更  
        'Move to the next record..
        objCurrencyManager.Position += 1
        '控件被绑定到DataView数据源对象,所有控件记录集是同步的,需要更新调用显示数据位置标签过程.    Show the current record position..
        ShowPosition()
    End Sub

    '180119-按钮单击事件,移动最后一条记录
    Private Sub btnMoveLast_Click(Sender As Object,
            E As EventArgs) Handles btnMoveLast.Click
        '移动最后一条记录,只要不更新,就不存在数据源集的记录变更  
        ' Set the record position to the last record..
        objCurrencyManager.Position = objCurrencyManager.Count - 1
        '控件被绑定到DataView对象,是同步的,需要更新调用显示数据位置标签过程.   Show the current record position..
        ShowPosition()
    End Sub

    Private Sub C05_物品库存信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '---------------------------------------
        'On Error Resume Next
        Dim objCommand As OleDbCommand = New OleDbCommand()
        objCommand.Connection = objConnection
        objConnection.Close()
        objConnection.Open()
        objCommand.CommandText = "delete from 库存信息"


        Try '截取异常
            objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
        Catch SqlExceptionErr As OleDbException
            MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示错误信息
        End Try '结束截取



        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection
        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & "物品编码" & " from " & "采购物品信息 ORDER BY 物品编码"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        Dim objDataSet1th = New DataSet()
        objDataAdapter1th.Fill(objDataSet1th, "kcxx2")
        'objDataView1th = New DataView(objDataSet.Tables("jhxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("kcxx2")
        Dim n As Integer = tb.Rows.Count
        'Dim n1 As Byte = tb.Columns.Count
        If n <= 0 Then Exit Sub    '如果数量小于0,那么
        Dim myCode() As Object, myName() As Object, mySpec() As Object, myUnit() As Object, myprice() As Object  '声明变量
        Dim myNum As Object, myMoney As Object, bytProduction() As Integer
        ReDim myCode(0 To n - 1)      '重新定义数组上标,采购项目编码用
        ReDim myName(0 To n - 1)  '重新定义数组上标,采购项目名称用
        ReDim mySpec(0 To n - 1)     '重新定义数组上标,采购项目名称用
        ReDim myUnit(0 To n - 1)      '重新定义数组上标,计量单位用
        ReDim myprice(0 To n - 1)     '重新定义数组上标,库存单价用
        ReDim myNum(0 To n - 1, 0 To 1)
        ReDim myMoney(0 To n - 1, 0 To 1)

        For i As Integer = 0 To n - 1
            myCode(i) = tb.Rows(i).Item(0).ToString  '赋值给
            '物品编码.Items.Add(tb.Rows(inCounter).Item(0).ToString)   '添加项目值为记录字段所对应的值
        Next

        For i As Integer = 0 To n - 1
            'For j As Integer = 0 To n1 - 1
            objDataAdapter1th.SelectCommand.CommandText = "Select 物品名称,物品规格,计量单位,进货单价 from 采购进货信息" &
                  " where 物品编码='" & myCode(i) & "'"
            objDataAdapter1th.Fill(objDataSet1th, "kcxx3" & i)
            Dim tb1 As DataTable = New DataTable()
            tb1 = objDataSet1th.Tables("kcxx3" & i)
            Dim k As Byte = tb1.Rows.Count
            myName(i) = tb1.Rows(0).Item(0).ToString
            mySpec(i) = tb1.Rows(0).Item(1).ToString
            myUnit(i) = tb1.Rows(0).Item(2).ToString
            myprice(i) = tb1.Rows(0).Item(3).ToString
            objDataAdapter1th.SelectCommand.CommandText = "select sum(进货数量) as 数量," &
                            " sum(进货数量*进货单价) as 金额 from 采购进货信息" &
                              " where 物品编码='" & myCode(i) & "'"
            objDataAdapter1th.Fill(objDataSet1th, "kcxx4" & i)
            Dim tb2 As DataTable = New DataTable()
            tb2 = objDataSet1th.Tables("kcxx4" & i)
            myNum(i, 0) = tb2.Rows(0).Item(0).ToString
            myMoney(i, 0) = tb2.Rows(0).Item(1).ToString
            If myNum(i, 0) = "" Then myNum(i, 0) = 0
            If myMoney(i, 0) = "" Then myMoney(i, 0) = 0
            'tb1 = Nothing
        Next
        For i As Integer = 0 To n - 1
            objDataAdapter1th.SelectCommand.CommandText = "select sum(消耗使用数量) as 数量," _
                            & " sum(消耗使用数量*成本单价) as 金额 from 物品消耗使用信息" _
                            & " where 物品编码='" & myCode(i) & "'"
            objDataAdapter1th.Fill(objDataSet1th, "kcxx5" & i)
            Dim tb2 As DataTable = New DataTable()
            tb2 = objDataSet1th.Tables("kcxx5" & i)
            If tb2.Rows.Count = 0 Then
                myNum(i, 1) = 0
                myMoney(i, 1) = 0
            Else
                myNum(i, 1) = tb2.Rows(0).Item(0).ToString
                myMoney(i, 1) = tb2.Rows(0).Item(1).ToString
                If myNum(i, 1) = "" Then myNum(i, 1) = 0
                If myMoney(i, 1) = "" Then myMoney(i, 1) = 0

            End If
            'tb2 = Nothing
        Next

        For i As Integer = 0 To n - 1
            objCommand.CommandText = "INSERT INTO 库存信息 " &
      "(物品编码, 物品名称, 物品规格, 计量单位, 库存数量, 进货单价, 库存金额) " &
            "VALUES(" & "'" & myCode(i) & "'" & "," & "'" & myName(i) & "'" & "," & "'" & mySpec(i) & "'" & "," & "'" &
            myUnit(i) & "'" & "," & (CType(myNum(i, 0), Integer) - CType(myNum(i, 1), Integer)).ToString &
            "," & CType(myprice(i), Double) & "," & (CType(myMoney(i, 0), Double) - CType(myMoney(i, 1), Double)).ToString & ")"

            '      objCommand.CommandText = "INSERT INTO 库存信息 " &
            '"(物品编码) " &
            '"VALUES(" & "'" & myCode(i) & "'" & ")"
            '      objCommand.CommandText = "INSERT INTO 库存信息 " &
            '"(物品编码, 物品名称, 物品规格, 计量单位, 库存数量, 进货单价, 库存金额) " &
            '"VALUES(@物品编码, @物品名称, @物品规格, @计量单位, @库存数量, @进货单价, @库存金额)"
            'objCommand.Parameters.AddWithValue("@物品编码", myCode(i))
            'objCommand.Parameters.AddWithValue("@物品名称", myName(i))
            'objCommand.Parameters.AddWithValue("@物品规格", mySpec(i))
            'objCommand.Parameters.AddWithValue("@计量单位", myUnit(i))
            'objCommand.Parameters.AddWithValue("@库存数量", (CType(myNum(i, 0), Integer) - CType(myNum(i, 1), Integer)))
            'objCommand.Parameters.AddWithValue("@进货单价", myprice(i)).DbType = DbType.Currency
            'objCommand.Parameters.AddWithValue("@库存金额", (CType(myMoney(i, 0), Integer) - CType(myMoney(i, 1), Integer))).DbType = DbType.Currency
            '执行命令对象插入新数据  Execute the SqlCommand object to insert the new data..



            Try '截取异常
                objCommand.ExecuteNonQuery()  '执行命令对象以更新数据(主要对数据库操作)
            Catch OledbExceptionErr As OleDbException
                MessageBox.Show(OledbExceptionErr.Message)    '如果出错,提示错误信息
            End Try '结束截取

        Next
        '-------------
        objDataAdapter.SelectCommand.CommandType = CommandType.Text    'SelectCommand的CommandType属性是CommandType.Text是默认属性.
        '调用FillDataSetAndView过程检索数据并调用BindFields过程绑定控件      
        '需要说明的是,Fill方法会执行SelectCommand,并保持为调用该方法时的状态. 'Fill the DataSet and bind the fields..
        FillDataSetAndView()
        BindFields()
        ShowPosition()  '调用过程显示当前标签记录位置    Show the current record position..
        grdAuthorTitles.AutoGenerateColumns = True  '让grd控件创建所需要的所有列.  Set the DataGridView properties to bind it to our data..
        grdAuthorTitles.DataSource = objDataSet '设置DataSet对象作为gird控件的数据源(实际上就是一个绑定过程,告知控件从哪里获得数据)
        grdAuthorTitles.DataMember = "KCXX01"  'gird控件要显示数据源(填充过数据的DataSet对象)具体的表名称
        Dim objAlignRightCellStyle As New DataGridViewCellStyle '创建DataGridViewCellStyle对象(grd控件单元格样式实例) 'Declare and set the currency header alignment property..
        objAlignRightCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft  '将对齐方式格式改为垂直居中向右对齐.
        Dim objAlternatingCellStyle As New DataGridViewCellStyle()    '定义交叉行样式Declare and set the alternating rows style..
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置样式背景色为烟灰色
        grdAuthorTitles.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟灰色)
        '创建DataGridViewCellStyle对象(grd控件单元格样式实例)   
        'Declare and set the style for currency cells ..
        '设置单元格格式为货币型(参考).

        Dim objCurrencyCellStyle As New DataGridViewCellStyle()
        objCurrencyCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft      '将对齐方式改为居右对齐
        objCurrencyCellStyle.Format = "¥#,##0.00"
        objCurrencyCellStyle.Format = "C"
        '设置控件列标题  Change column names and styles using the column index
        'myArray = {"物品编码", "物品名称", "物品规格", "计量单位", "库存数量", "进货单价", "库存金额"}
        grdAuthorTitles.Columns(0).HeaderText = "物品编码"
        grdAuthorTitles.Columns(1).HeaderText = "物品名称"
        grdAuthorTitles.Columns(2).HeaderText = "物品规格"
        grdAuthorTitles.Columns(3).HeaderText = "计量单位"
        grdAuthorTitles.Columns(4).HeaderText = "库存数量"
        grdAuthorTitles.Columns(5).HeaderText = "进货单价"
        grdAuthorTitles.Columns(6).HeaderText = "库存金额"
        'grdAuthorTitles.Columns(6).Width = 165 '设置指定列默认宽度小一点
        '改变字段标题名称和样式  
        'Change column names and styles using the column name
        'grdAuthorTitles.Columns("进货单价").HeaderCell.Value = "进货单价"                  '重新设置列标题的值显示为"状态"
        grdAuthorTitles.Columns("进货单价").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles.Columns("进货单价").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)
        grdAuthorTitles.Columns("库存金额").HeaderCell.Style = objAlignRightCellStyle  '标题重新调用列标题样式(之前设定的-垂直右对齐)
        grdAuthorTitles.Columns("库存金额").DefaultCellStyle = objCurrencyCellStyle    '单元格内容重新调用样式(之前设定的-垂直右对齐)


        objDataAdapter1th.SelectCommand.CommandText = "select 最低库存 " & " from " & "采购物品信息 ORDER BY 物品编码" '写入SQL语句
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text  '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataSet1th = New DataSet()                        '数据适配器对象开始检索数据并填充到DataSet对象
        objDataAdapter1th.Fill(objDataSet1th, "wpxx02")      'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.
        Dim tbProduction As DataTable = objDataSet1th.Tables("wpxx02") '声明一个表类型,并赋值给该变量.
        ReDim bytProduction(0 To tbProduction.Rows.Count - 1)
        For inCounter = 0 To tbProduction.Rows.Count - 1               '在表行数上循环
            'intCounter = intCounter + 1                                  '累加计数器，此计数器代表文件夹数量
            bytProduction(inCounter) = CType(tbProduction.Rows(inCounter).Item(0).ToString, Integer)
        Next
        For i As Integer = 0 To grdAuthorTitles.RowCount - 2                           '有一个空白行也算一行
            'If Math.Ceiling(CType(grdAuthorTitles.Item(4, i).Value.ToString(), Date).Subtract(Now).TotalDays) <= 20 Then
            If (CType(grdAuthorTitles.Item(4, i).Value.ToString(), Integer) - bytProduction(i)) < 0 Then

                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Red                            '字体颜色设置为红色
            Else
                grdAuthorTitles.Rows(i).DefaultCellStyle.Font = New Font("宋体", 9, FontStyle.Regular)    '构建一个字体类及相关属性
                grdAuthorTitles.Rows(i).DefaultCellStyle.ForeColor = Color.Black                          '字体颜色设置为黑色
            End If
        Next


        objCurrencyCellStyle = Nothing     '清除单元格样式对象(单元格记录内容用)
        objAlternatingCellStyle = Nothing  '清除交叉单元格样式
        objAlignRightCellStyle = Nothing   '清除列标题样式(标题用)
        排序字段.Items.Clear()
        For i = 0 To UBound(myArray)    '给组合框添加项目 Add items to the combo box..
            排序字段.Items.Add(GroupBox1.Controls(myArray(i).ToString).Name.ToString)
        Next i
        排序字段.SelectedIndex = 0       '默认选择第一项
    End Sub

    '180120-排序按钮,确定对哪个字段进行排序.单击事件    '注:DateGirdView控件视图自带单击列标题排序,这里针对的是绑定的简单控件进行排序
    Private Sub 执行排序_Click(sender As Object, e As EventArgs) Handles 执行排序.Click
        '根据选定的项并设置DataView对象(源数据是指定表sbxx)相关字段的sort属性, 
        'Determine the appropriate item selected and set the Sort property of the DataView object..
        ' myArray = {"物品编码", "物品名称", "物品规格", "计量单位", "库存数量", "进货单价", "库存金额"}
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "物品编码"
            Case 1
                objDataView.Sort = "物品名称"
            Case 2
                objDataView.Sort = "物品规格"
            Case 3
                objDataView.Sort = "计量单位"
            Case 4
                objDataView.Sort = "库存数量"
            Case 5
                objDataView.Sort = "进货单价"
            Case 6
                objDataView.Sort = "库存金额"
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
        'myArray = {"物品编码", "物品名称", "物品规格", "计量单位", "库存数量", "进货单价", "库存金额"}
        Select Case 排序字段.SelectedIndex
            Case 0
                objDataView.Sort = "物品编码"
                str条件 = "物品编码"
            Case 1
                objDataView.Sort = "物品名称"
                str条件 = "物品名称"
            Case 2
                objDataView.Sort = "物品规格"
                str条件 = "物品规格"
            Case 3
                objDataView.Sort = "计量单位"
                str条件 = "计量单位"
            Case 4
                objDataView.Sort = "库存数量"
                str条件 = "库存数量"
            Case 5
                objDataView.Sort = "进货单价"
                str条件 = "进货单价"
            Case 6
                objDataView.Sort = "库存金额"
                str条件 = "库存金额"
        End Select
        'DataView数据表中筛选数据集.
        objDataView.RowFilter = UCase(str条件) & " Like  '%" & 查询条件.Text & "%'"
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

    '180125-查询条件变化事件
    Private Sub 查询条件_TextChanged(sender As Object, e As EventArgs) Handles 查询条件.TextChanged
        If 查询条件.Text.Length = 0 Then              '如果是空值
            C05_物品库存信息_Load(Nothing, Nothing)   '调用加载窗体事件.填充数据显示DateGirdVie完整视图,绑定控件,显示当前记录位置
        End If
    End Sub

    '180125-获取项目值模板
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
        Globals.Ribbons.Ribbon1.Button35.Enabled = True     '重新使按钮可用.
        Globals.Ribbons.Ribbon1.Button29.Enabled = True     '重新使按钮可用.
        Me.Close()  '关闭窗体
    End Sub

    Private Sub C05_物品库存信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '清理内存及数据适配器对象
        objDataAdapter = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button35.Enabled = True  '重新使按钮可用.
        Globals.Ribbons.Ribbon1.Button29.Enabled = True  '重新使按钮可用.
    End Sub


End Class