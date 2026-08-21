Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710      myArray = {"保养单号", "设备编号", "保养费用", "保养级别", "保养内容", "保养时间", "替换件编号", "工时"}
Imports System.Data     '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing      '使用颜色命名空间
Public Class D02_人员信息查询与导出
    Dim objConnection2th As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb")  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\进销存管理\进销存管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\进销存管理\进销存管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\费用管理.accdb")  '公司共享盘
    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '该构造函数使用了SelectCommand属性的一个字符串和一个表示数据库连接的对象来初始化SqlAdapater对象.
    '这个构造函数使我们不必写Adapter属性代码.
    'Dim objDataAdapter As New OleDbDataAdapter("SELECT 保养.* FROM 保养 ORDER BY 保养单号", objConnection2th)

    '声明变量
    Dim objDataAdapter1th As New OleDbDataAdapter() '实例化一个类的对象.

    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataSet1th As New DataSet()  '实例化一个类的对象

    '声明作用域为类级的对象,DataView类用来表示定制----从数据库返回存储在DatSet(DataTable)中的记录视图.
    Dim objDataView1th As DataView

    '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表.
    Dim objCurrencyManager As CurrencyManager

    '声明变量,数据库用.
    Dim myArray As Object

    Private Sub D02_人员信息查询与导出_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '为信息种类复合框设置项目
        With 信息种类       '引用复合框对象
            .Items.Add（"人员资质证书"）     '添加项目为合同基本信息
        End With                                '结束引用语句
        信息种类.SelectedIndex = 0              '设置默认选择行
        '为运算符复合框设置项目
        With 运算符                         '引用复合框(运算符)对象
            .Items.Add（"="）                '添加项目(=,>,<,>=.....)
            .Items.Add（">"）
            .Items.Add（"<"）
            .Items.Add（">="）
            .Items.Add（"<="）
            .Items.Add（"<>"）
            .Items.Add（"like"）
            .Items.Add（"between"）
        End With        '结束引用语句
        '运算符.ListIndex = 0        'VBA 选择默认行
        运算符.SelectedIndex = 0
    End Sub

    Private Sub 信息种类_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 信息种类.SelectedIndexChanged
        'On Error Resume Next      
        '实例化OleDbCommand类的一个对象,并连接SelectCommand属性.  
        objDataAdapter1th.SelectCommand = New OleDbCommand()

        '将Connection属性设置为连接对象.用来与数据库通信.
        objDataAdapter1th.SelectCommand.Connection = objConnection2th

        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序

        objDataAdapter1th.SelectCommand.CommandText = "SELECT " & 信息种类.Text & ".* FROM " & 信息种类.Text

        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text

        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        objDataAdapter1th.Fill(objDataSet1th, "wxxx1")
        objDataView1th = New DataView(objDataSet1th.Tables("wxxx1"))
        Dim tb As DataTable = objDataSet1th.Tables("wxxx1")
        Dim a As Byte = tb.Columns.Count - 1
        Dim i As Byte
        查询项目.Items.Clear()
        With 查询项目
            For i = 0 To a
                .Items.Add(tb.Columns(i).ColumnName)
            Next
        End With

        '结束引用语句
        查询项目.SelectedIndex = 0      '选择默认项目为第一项
        清除显示信息()       '调用子程序
    End Sub


    Private Sub 清除显示信息()

        On Error Resume Next
        'objDataAdapter1th.SelectCommand = New OleDbCommand()
        'objDataAdapter1th.SelectCommand.Connection = objConnection2th
        'objDataAdapter1th.SelectCommand.CommandText = "select * from " & 信息种类.Text & " where " & 查询项目.Text & " =TEST"
        'Dim objDataSet2th As DataSet = New DataSet
        'objDataAdapter1th.Fill(objDataSet2th, "wxxx4")
        ''objDataView1th = New DataView(objDataSet1th.Tables("wxxx3"))
        'Dim tb As DataTable = objDataSet1th.Tables("wxxx4")

        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text

        ''数据适配器对象开始检索数据并填充到DataSet对象
        ''Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        ''Fill the DataSet object with data..
        'Dim tb As DataTable = New DataTable





        'grdAuthorTitles1th.DataSource = tb

        'tb.Rows.Clear()

        'foreach(GridViewRow row In GridView1.Rows)
        '{
        ''    row.Cells.Clear;
        '}

        For i As Integer = 0 To grdAuthorTitles1th.Rows.Count - 1

            'grdAuthorTitles1th.Columns.RemoveAt(i)
            ' 删除第一行
            grdAuthorTitles1th.Rows.RemoveAt(i)
        Next



    End Sub

    Private Sub 查询项目_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 查询项目.SelectedIndexChanged
        'On Error Resume Next            '错误继续执行
        'Dim SQL As String
        '查询项目.SelectedIndex = 0      '选择默认项目为第一项
        'rs = CreateObject("ADODB.Recordset")        '创建记录对象
        'SQL语句表示从信息种类符合框显示的对应的数据表,筛选选不重复值(查询项目复合框显示的值)
        'SQL = "select distinct " & 查询项目.Text & " from " & 信息种类.Text
        'rs.Open(SQL, cnn, 1, 3)        '打开记录集对象


        '初始化OleDbCommand类的一个实例,并将其分配给SelectCommand属性.   Set the SelectCommand properties..
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        '将Connection属性设置为连接对象.用来与数据库通信.
        objDataAdapter1th.SelectCommand.Connection = objConnection2th

        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序

        objDataAdapter1th.SelectCommand.CommandText = "select distinct " & 查询项目.Text & " from " & 信息种类.Text

        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text

        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        objDataAdapter1th.Fill(objDataSet1th, "wxxx2")
        objDataView1th = New DataView(objDataSet1th.Tables("wxxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("wxxx2")
        Dim a As Byte = tb.Columns.Count - 1
        Dim i As Byte
        条件值1.Text = ""       '清除文本框条件值1的值
        条件值2.Text = ""       '清除文本框条件值2的值
        条件值1.Items.Clear()
        条件值2.Items.Clear()


        With tb
            For inCounter = 0 To .Rows.Count - 1
                'strResult = .Rows(inCounter).Item("username").ToString _
                '    & "" & .Rows(inCounter).Item("password").ToString
                'MessageBox.Show(strResult)
                '条件值1.Items.Add(.Rows(inCounter).Item(查询项目.Text).ToString)   '添加项目值为记录字段所对应的值
                '条件值2.Items.Add(.Rows(inCounter).Item(查询项目.Text).ToString)    '添加项目值为记录字段所对应的值

                'Format(CType(条件值1.Text.ToString, Date), "yyyy/MM/dd")
                If 查询项目.Text = "发证日期" Or 查询项目.Text = "有效期至" Then
                    条件值1.Items.Add(Format(CType(.Rows(inCounter).Item(查询项目.Text).ToString, Date), "yyyy/MM/dd"))  '添加项目值为记录字段所对应的值
                    条件值2.Items.Add(Format(CType(.Rows(inCounter).Item(查询项目.Text).ToString, Date), "yyyy/MM/dd"))    '添加项目值为记录字段所对应的值
                Else
                    条件值1.Items.Add(.Rows(inCounter).Item(查询项目.Text).ToString)   '添加项目值为记录字段所对应的值
                    条件值2.Items.Add(.Rows(inCounter).Item(查询项目.Text).ToString)    '添加项目值为记录字段所对应的值

                End If


            Next


        End With

        条件值1.SelectedIndex = 0       '默认选择第一行项目
        条件值2.SelectedIndex = 0       '默认选择第一行项目

        '结束引用语句

        清除显示信息()       '调用子程序

    End Sub

    Private Sub 运算符_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 运算符.SelectedIndexChanged
        If 运算符.Text <> "between" Then   '如果运算符的值不是"between",那么执行
            Label_and.Visible = False       '标签隐藏
            Label_Value2.Visible = False    '标签隐藏
            条件值2.Visible = False         '复合框隐藏
            条件值1.Width = 250             '重新定义条件值1复合框宽度
        Else        '否则执行
            Label_and.Visible = True        '显示标签跟复合框
            Label_Value2.Visible = True
            条件值2.Visible = True
            条件值1.Width = 121      '重新定义条件值1复合框宽度
        End If      '结束判定
    End Sub
    Private Sub 重设条件_Click(sender As Object, e As EventArgs) Handles 重设条件.Click

        信息种类.SelectedIndex = 0          '选择默认行
        查询项目.SelectedIndex = 0           '选择默认行
        运算符.SelectedIndex = 0             '选择默认行
        条件值1.SelectedIndex = 0            '选择默认行
        条件值2.SelectedIndex = 0            '选择默认行
        清除显示信息()           '调用子程序
    End Sub

    Private Sub 开始查询_Click(sender As Object, e As EventArgs) Handles 开始查询.Click
        On Error Resume Next

        清除显示信息()
        Dim i As Integer          '声明变量
        Dim Condition As String, Con0 As String, Con1 As String, Con2 As String
        '设置查询条件
        Con0 = " where "        '给变量赋值
        '如果查询项目的值等于相应的日期值
        If 查询项目.Text = "发证日期" Or 查询项目.Text = "有效期至" Then

            '设置相应的日期格式
            '发布日期.Text = Format(Today(), "yyyy-M-d")     '文本框（签订日期）输入相应的格式
            'Con1 = "#" & Format(条件值1.Text, "yyyy-M-d") & "#"
            'Con2 = "#" & Format(条件值2.Text, "yyyy-M-d") & "#"
            Con1 = "#" & CType(Me.Controls("条件值1").Text, Date).ToShortDateString & "#"
            'Con2 = "#" & xlapp.WorksheetFunction.Text(Me.Controls(条件值2.Text).Text.ToUpper, "0000-00-00") & "#"
            Con2 = "#" & CType(Me.Controls("条件值2").Text, Date).ToShortDateString & "#"
        Else        '否则给变量赋值
            Con1 = "'" & 条件值1.Text.ToUpper & "'"
            Con2 = "'" & 条件值2.Text.ToUpper & "'"
        End If      '结束判定语句
        Condition = " where " & 查询项目.Text.ToUpper  '给变量赋值
        If 运算符.Text = "between" Then        '如果运算符值等于between
            Condition = Condition & " between " & Con1 & " and " & Con2     '重新给变量赋值SQL语句
        ElseIf 运算符.Text = "like" Then       '否则如果复合框的值等于"like",那么执行
            Condition = Condition & " like '%" & 条件值1.Text.ToUpper & "%'"       '重新给变量赋值SQL语句
        Else        '否则
            Condition = Condition & 运算符.Text & Con1     '重新给变量赋值SQL语句
        End If  '结束判定语句
        '设置SQL语句,根据信息种类复合框值对应的表,按照指定条件(以上设置的变量),筛选出所有字段值


        '将Connection属性设置为连接对象.用来与数据库通信.
        objDataAdapter1th.SelectCommand.Connection = objConnection2th

        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序

        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..

        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        '------------
        '创建DataGridViewCellStyle对象(grd控件单元格样式实例)   
        'Declare and set the style for currency cells ..
        '设置单元格格式为货币型参考
        'objCurrencyCellStyle.Format = "$#,##0.00"
        'objCurrencyCellStyle.Format = "C"

        objDataAdapter1th.SelectCommand = New OleDbCommand()
        objDataAdapter1th.SelectCommand.Connection = objConnection2th
        objDataAdapter1th.SelectCommand.CommandText = "select * from " & 信息种类.Text & Condition
        objDataAdapter1th.Fill(objDataSet1th, "wxxx3")
        'objDataView1th = New DataView(objDataSet1th.Tables("wxxx3"))
        Dim tb As DataTable = objDataSet1th.Tables("wxxx3")

        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text

        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..

        grdAuthorTitles1th.DataSource = objDataSet1th
        grdAuthorTitles1th.AutoGenerateColumns = True
        '设置控件列标题                    

        'Declare and set the currency header alignment property..


        '初始化OleDbCommand类的一个实例,并将其分配给SelectCommand属性.   Set the SelectCommand properties..


        '将Connection属性设置为连接对象.用来与数据库通信.


        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     GroupBox3.Controls("维修单号").Text  
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序

        Dim objAlternatingCellStyle As New DataGridViewCellStyle()
        objAlternatingCellStyle.BackColor = Color.WhiteSmoke  '设置样式背景色为烟灰色
        grdAuthorTitles1th.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle '奇数行属性设置刚创建的样式(烟白色)


        '因为数据已填充到DataSet对象中了,所以可以关闭数据库的连接(通信)   Close the database connection..
        grdAuthorTitles1th.DataMember = "wxxx3"

        '---
        Dim a As Byte = tb.Columns.Count - 1


        For i = 0 To a
            grdAuthorTitles1th.Columns(i).HeaderText = tb.Columns(i).ColumnName

        Next


        '结束引用语句

        '清除显示信息()       '调用子程序


        '-----
    End Sub
    Private Sub 数据导出_Click(sender As Object, e As EventArgs) Handles 数据导出.Click
        On Error Resume Next
        Dim wb As Excel.Workbook      '声明变量及数据类型
        Dim ws As Excel.Worksheet     '声明变量及数据类型
        Dim i As Integer, j As Integer  '声明变量及数据类型
        wb = xlapp.Workbooks.Add       '给变量赋值,新建工作簿
        ws = wb.ActiveSheet     '给变量赋值,激活活动工作簿
        '---
        'On Error Resume Next        '错误时继续执行
        '初始化OleDbCommand类的一个实例,并将其分配给SelectCommand属性.   Set the SelectCommand properties..
        'objDataAdapter1th.SelectCommand = New OleDbCommand()
        '将Connection属性设置为连接对象.用来与数据库通信.
        'objDataAdapter1th.SelectCommand.Connection = objConnection2th

        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        'objDataAdapter1th.SelectCommand.CommandText = "SELECT " & 信息种类.Text & ".* FROM " & 信息种类.Text
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        'objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        'objDataSet1th = New DataSet()
        'objDataAdapter1th.Fill(objDataSet1th, "wxxx1")
        'objDataView1th = New DataView(objDataSet1th.Tables("wxxx1"))
        Dim tb As DataTable = objDataSet1th.Tables("wxxx3")
        Dim a As Byte = tb.Columns.Count - 1
        'With 查询项目
        '    For i = 0 To a
        '        .Items.Add(tb.Columns(i).ColumnName)
        '    Next
        'End With
        ''---
        With ws                     '引用工作表
            For i = 0 To a   '在0到字段数量上-1处循环
                .Cells(1, i + 1) = tb.Columns(i).ColumnName       '逐一写入字段名
            Next i      '循环
            With .Range(xlapp.Cells(1, 1), xlapp.Cells(1, tb.Columns.Count))   '引用区域
                .Font.Bold = True       '字体加粗
                .HorizontalAlignment = -4108 '区域对齐方式为中心
            End With        '结束引用语句
            For i = 1 To tb.Rows.Count    '在1到记录数量上循环
                '----

                '-----
                For j = 0 To a
                    .Cells(i + 1, j + 1) = tb.Rows(i - 1).Item(j).ToString '在单元格上写入字段值
                    '                 If rs.Fields(j).Type = adDate Then      '如果字段值类型是日期
                    If tb.Rows(i - 1).Item(j).GetType.ToString = "System.DateTime" Then      '如果字段值类型是日期
                        .Cells(i + 1, j + 1).NumberFormat = "yyyy-mm-dd"        '设置日期格式
                        .Cells(i + 1, j + 1) = tb.Rows(i - 1).Item(j).ToString
                    ElseIf j = a Then
                        '.Cells(i + 1, j + 1) = tb.Rows(i - 1).Item(j).ToString
                        .Cells(i + 1, j + 1) = "'" & tb.Rows(i - 1).Item(j).ToString

                    End If      '结束判定
                    ''                 If rs.Fields(j).Type = adCurrency Then      '如果字段类型是货币
                    'If rs.Fields(j).Type = 6 Then      '如果字段类型是货币
                    '    .Cells(i + 1, j + 1).NumberFormat = "#,##0.00"      '设置货币千分号格式
                    'End If      '结束判定
                Next j      '循环
                '移动到下一条记录
            Next i      '循环
            .Columns.AutoFit()    '列标自动设置适应宽度
        End With        '结束引用
        ws = Nothing        '释放变量
        wb = Nothing
        '关闭窗体.PerformClick()
        关闭窗体_Click(Nothing, Nothing)

    End Sub

    Private Sub 关闭窗体_Click(sender As Object, e As EventArgs) Handles 关闭窗体.Click
        On Error Resume Next
        objConnection2th.Close()   '关闭指定的数据库连接
        objDataAdapter1th = Nothing
        objConnection2th = Nothing
        objDataView1th = Nothing
        objCurrencyManager = Nothing
        Globals.Ribbons.Ribbon1.btnInformationExtract.Enabled = True
        myArray = Nothing
        Me.Close()
    End Sub

    Private Sub D02_人员信息查询与导出_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        关闭窗体_Click(Nothing, Nothing)
    End Sub


End Class