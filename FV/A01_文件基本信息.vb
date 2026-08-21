Imports System.Windows.Forms 'Define a NameSpace. 定义命名空间


'窗体主要显示文件清单信息及版本号...
Public Class A01_文件基本信息

    Public f1 As Integer 'As a searched position of listivwe controler item specified...作为指定项目列表的搜索位置


    '窗体加载时触发事件
    Private Sub A01_文件基本信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strMyDataPath As String        '声明变量,数据库路径
        Dim i As Integer = 0        '声明变量,作为步长值

        '指定数据库名称
        strMyDataPath = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\文件管理.accdb"  '定义数据库路径(公司共享盘)
        'strMyDataPath = "F:\2 笔记记录\8 过程信息管理\文件管理\文件管理.accdb"  '家里台式机测试


        '给变量赋值为一维数组,改数组变量是公共变量
        myArray = {"文件号", "文件名称", "版次", "收件人", "存放位置", "发布日期", "使用部门", "实施日期", "文件类别", "存储名", "备注"}


        '建立与数据库的连接的代码模块...
        '创建数据库连接对象( ado最顶层对象cnn)ADO的最顶层),
        '指定数据库连接, 打开指定数据库
        cnn = CreateObject("adodb.Connection")
        With cnn    '引用数据库连接对象(类)
            .Provider = "microsoft.Ace.OLEDB.12.0"   '指定数据库引擎，提供者Access 2010及其以上版本
            .Open(strMyDataPath)       '打开指定的数据库
        End With    '结束语句



        文件类别复合框设置()      '调用子程序，为文件类别复合框设置项目(这里省略Call 语句)

        '调用子程序，为签订部门复合框设置项目
        使用部门复合框设置()

        '调用子程序,查询文件基本信息
        查询文件基本信息()


        '调用子程序,显示文件基本信息
        显示文件基本信息()
        '调用子程序,显示某文件的详细信息写入到listview控件中
        显示文件清单()
        显示文件更改情况()
        'ListView2.Items(0).Selected = True
        'ListView2.Focus()
        Me.ToolTip1.SetToolTip(发布日期, "请输入完整的日期数字,如19880213") '该工具ToolTip的SetToolTip方法,第一参数用来引用控件,第二参数表示鼠标移动过去显示的文字
        Me.ToolTip1.SetToolTip(实施日期, "请输入完整的日期数字,如19880213")
    End Sub

#Region "'创建文件类别添加到复合框的方法 250930"
    Public Sub 文件类别复合框设置()     '第一次运行是被窗体加载后事件调用
        On Error Resume Next '错误继续执行...

        Dim rsFileClassName As Object       '声明变量,类型为对象,一个可调用记录集(类)...
        Dim bytCounter As Integer = 0    '声明变量为整型...

        rsFileClassName = CreateObject("ADODB.Recordset")   '创建一个实例(无信息的记录集对象),方便引用

        '打开指定记录集对象,第1参数数据库表名,第2参数数据库对象(已指定数据库连接...)
        '第3参数使用的指定的游标类型,4参数是锁定类型,这里设置可操作记录的锁定类型
        'rsFileClassName.Open "文件类别信息", cnn, adOpenKeyset, adLockOptimistic 
        rsFileClassName.Open("文件类别信息", cnn, 1, 3)


        '引用文件类别复合框
        With 文件类别
            .Items.Clear()   '清除项目
            For bytCounter = 0 To rsFileClassName.RecordCount - 1     '在0到记录集数目-1(打开是所有文件类别信息表的记录)上循环
                .Items.Add(rsFileClassName.Fields(0).value)   '添加项目(字段值的value属性是默认值)为当前字段值
                rsFileClassName.MoveNext                      '定位到下一个记录
            Next bytCounter '循环记录集数量
        End With  '结束引用


        '记录对象关闭并释放变量
        rsFileClassName.Close
        rsFileClassName = Nothing

    End Sub
#End Region

#Region "使用部门复合框设置 250930"
    Public Sub 使用部门复合框设置()          '接窗体加载后事件的子程序
        Dim rsDepatmentClass As Object                    '声明变量
        rsDepatmentClass = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用


        Dim bytCounter As Integer    '声明变量
        '打开指定记录集对象,第一参数数据库表名,第二参数数据库对象(已经打开指定的数据库连接),
        '第3参数使用的指定的游标类型, 第4参数是锁定类型, 这里设置可操作记录的锁定类型
        rsDepatmentClass.Open("新罗公司部门", cnn, 1, 3)


        '引用签订部门复合框
        With 使用部门
            .Items.Clear()      '清除项目

            '在1到记录集数量上循环
            For bytCounter = 0 To rsDepatmentClass.RecordCount - 1
                .Items.Add(rsDepatmentClass.Fields(0).value)      '添加项目(字段值的value属性是默认值)为当前字段值
                rsDepatmentClass.MoveNext          '定位到下一个记录
            Next bytCounter      '循环语句继续添加项目
        End With    '结束引用语句


        rsDepatmentClass.Close       '关闭数据库指定的记录对象
        rsDepatmentClass = Nothing   '释放数据库对象变量

    End Sub
#End Region

#Region "查询文件基本信息 251007"
    Public Sub 查询文件基本信息()   '在加载窗体后,作为被调用的子程序
        rs = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        '打开(创建)指定数据库表(文件基本信息)的记录集,第一参数数据库表名,第二参数数据库对象(已经打开指定的数据库连接),
        '第3参数使用的指定的游标类型,4参数是锁定类型,这里设置可操作记录的锁定类型
        rs.Open("文件基本信息", cnn, 1, 3)
        文件记录数目.Text = "数据库中共有 " & rs.RecordCount & " 条文件记录"
    End Sub
#End Region

#Region "显示文件基本信息 251013"
    '在加载窗体后,调用的子程序,显示数据库的第一条记录..
    Public Sub 显示文件基本信息()
        On Error Resume Next            '程序出错继续执行下一句
        Dim bytCounter As Integer         '声明变量

        If rs.BOF And rs.EOF Then       '显示文件基本信息,如果没有记录(BOF表示记录开始前,EOF表示记录结束后,同时为true表示没记录)执行下面语句
            新文件.PerformClick()       '调用按钮的Click事件
        Else

            '在数组上遍历...
            For bytCounter = 0 To UBound(myArray)
                If IsNothing(rs.Fields(bytCounter).value) Then  '如果字段值下面没有记录...
                    Me.Controls(myArray(bytCounter).ToString).Text = ""
                Else
                    '如果控件包含日期关键字...
                    If Me.Controls(myArray(bytCounter).ToString).Name = "发布日期" Or Me.Controls(myArray(bytCounter).ToString).Name = "实施日期" Then
                        Me.Controls(myArray(bytCounter).ToString).Text = CType(rs.Fields(bytCounter).value, String)  '转换成文本类型,并在控件中显示
                    Else
                        Me.Controls(myArray(bytCounter).ToString).Text = rs.Fields(bytCounter).value.ToString   '记录值逐一写入控件值中...
                    End If
                End If
            Next  '循环
        End If  '结束语句
    End Sub
#End Region

#Region " '创建新文件信息 251015"
    Private Sub 新文件_Click(sender As Object, e As EventArgs) Handles 新文件.Click
        Dim bytCounter As Integer


        '清除窗体上各个控件的数据，或将某控件的值设置为默认状态
        For bytCounter = 0 To UBound(myArray)
            Me.Controls(myArray(i).ToString).Text = ""
        Next


        文件类别.SelectedIndex = 2  '文件类别复合框选第3项
        使用部门.SelectedIndex = 3  '使用部门复合框选第4项
        发布日期.Text = Format(Today(), "yyyy-M-d")    '文本框（签订日期）输入相应的格式
        实施日期.Text = Format(Today(), "yyyy-M-d")    '文本框（签订日期）输入相应的格式
        ListView1.Clear()                              '清空视图框项目值
        文件号.Focus()      '焦点返回到文件号文本框


    End Sub
#End Region

#Region "'控件类似于文件台账功能,显示文件清单"
    Public Sub 显示文件清单()   '创建一个过程,视图显示控件所填充数据
        On Error Resume Next    '出错继续在错误处执行
        Dim bytCounter As Integer        '声明变量
        Dim strDate As String         '声明一个文本型变量


        '引用视图控件 设置ListView1的标题、显示类型、整行选择和网格线属性
        With ListView2
            .Columns.Clear()        '清除标题行
            .Clear()                '清除项目集
            .View = View.Details    '报表输出视图
            .FullRowSelect = True   '允许整行选择
            .GridLines = True       '允许网格线


            '遍历字段...
            For bytCounter = 0 To rs.Fields.Count - 1
                .Columns.Add(rs.Fields(bytCounter).Name.ToString, 100)   '添加标题
            Next


            '为ListView1设置各行,显示数据记录                       
            For bytCounter = 1 To rs.RecordCount

                '添加项目,首列为项目名称
                Dim itm As ListViewItem = ListView2.Items.Add(rs.Fields(0).Value.ToString)


                '在1到字段总数-1上循环,向项目名后添加数据
                For j = 1 To rs.Fields.Count - 1
                    If TypeName(rs.Fields(j).value) = "Date" Then            '如果字段类型是日期型的.
                        strDate = rs.Fields(j).value.ToShortDateString             '给变量赋值,设置短日期格式的文本
                        itm.SubItems.AddRange({strDate})                           '短日期格式的文本逐一写入项目中
                    Else
                        itm.SubItems.AddRange({rs.Fields(j).value.ToString}) '从第2列开始添加索引列的子项目值
                    End If
                Next j          '循环语句
                rs.MoveNext     '定位到下一条记录
            Next  '循环
        End With    '结束引用对象


        ListView2.Columns(4).Width = 0   '隐藏存储列.
        显示文件更改情况()
    End Sub
#End Region

#Region "文件控件类似于履历卡显示"
    Public Sub 显示文件更改情况()   '窗体加载后,显示文件
        On Error Resume Next        '出错继续在错误位置后继续运行
        Dim i As Integer            '声明变量
        Dim SQL As String           '声明SQL语句的文本变量
        Dim rsx As Object           '声明变量,作为记录
        Dim a As String             '声明文本,将日期字段格式转换成文本...
        rsx = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用



        'SQL语句表示,从文件创建更改信息表中,根据指定条件(字段文件号的字段值等于文本框文件号的值),筛选所有所有字段记录
        SQL = "select * from 文件创建更改信息 where 文件号='" & 文件号.Text & "'"
        rsx.Open(SQL, cnn, 1, 3)     '打开指定记录集对象



        With ListView1               '引用视图框控件
            '设置ListView1的标题、显示类型、整行选择和网格线属性
            .Columns.Clear()        '清除标题行  '.ColumnHeaders.Clear    'VBA 清除列标题
            .Clear()                '清除项目集
            .View = View.Details    '报表输出视图 '.View = lvwReport       '显示报表视图
            .FullRowSelect = True   '允许整行选中
            .GridLines = True       '显示网格线


            For i = 0 To rsx.Fields.Count - 1  '为ListView1设置标题'在0到字段数量上减1循环语句(引用指定的字段的下标是从0开始)
                .Columns.Add(rsx.Fields(i).Name.ToString, 100)  '添加标题
            Next i '循环语句


            '为ListView1设置各行数据
            For i = 1 To rsx.RecordCount                                                    '在1到记录数量上循环
                Dim itm As ListViewItem = ListView1.Items.Add(rsx.Fields(0).Value.ToString) '声明项目名(首列)并赋值
                For j = 1 To rsx.Fields.Count - 1                                           '在1到字段总数-1上循环
                    If TypeName(rsx.Fields(j).value) = "Date" Then                          '字段如果是日期类型
                        'a = rsx.Fields(j).value.ToShortDateString  '给文本变量赋值
                        'itm.SubItems.AddRange({a})                                          '写入项目值
                        a = rsx.Fields(j).value.ToShortDateString  '给文本变量赋值
                        itm.SubItems.Add(a)                                          '写入项目值
                    Else
                        'itm.SubItems.AddRange({rsx.Fields(j).value.ToString})               '从第2列开始添加索引列的子项目值
                        itm.SubItems.Add(rsx.Fields(j).value.ToString)               '从第2列开始添加索引列的子项目值
                    End If
                Next j                                                                      '循环语句
                rsx.MoveNext                                                                '定位到下一条记录
            Next i                                                                          '循环
        End With


        rsx.Close       '关闭指定记录集对象
        rsx = Nothing   '释放变量
    End Sub
#End Region



#Region "添加一条记录信息"
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        Dim i As Integer, SQL As String     '声明变量
        For i = 0 To UBound(myArray) - 1    '判断是否在窗体上输入了必要的文件数据,在0到数组上标-1上循环语句









            '如果控件名称不等于备注,存储名,存放位置,那么执行下列语句
            If Me.Controls(myArray(i).ToString).Name <> "备注" And Me.Controls(myArray(i).ToString).Name <> "存储名" And Me.Controls(myArray(i)).Name <> "存放位置" Then
                If Me.Controls(myArray(i).ToString).Text = "" Then                   '相关控件值设置为空值
                    MsgBox(Me.Controls(myArray(i).ToString).Name & "不能为空！", 16) '提示相关控件(文本框)信息不能为空值
                    Me.Controls(myArray(i).ToString).Focus()                         '焦点重新回到控件上
                    Exit Sub                                                         '退出程序
                End If
            End If
        Next i

        '提示增加信息
        If MsgBox("本操作将添加新的文件记录！" & vbCrLf & "是否要添加？", vbQuestion + vbYesNo, "添加记录") = vbNo Then Exit Sub 'NO退出程序

        Dim rsNum As Object                       '首先判断在数据库中是否存在相同的文件号,声明变量
        rsNum = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用

        'SQL语句表示在文件基本信息表中,按照指定条件(字段文件号的字段值等于文件号文本框的值),筛选出文件号
        SQL = "select 文件号 from 文件基本信息 where 文件号='" & 文件号.Text & "'"
        rsNum.Open(SQL, cnn, 1, 3)      '打开指定的SQL语句信息

        If rsNum.RecordCount > 0 Then   '如果有记录,那么执行下列语句,提示信息,有重复文件号了
            MsgBox("在数据库中已经存在有编号为<" & 文件号.Text & ">的文件！" & vbCrLf & "请重新输入文件号！", vbOKOnly + vbCritical, "警告")
            Me.文件号.Text = ""    '将文件号文字框数据清除
            Me.文件号.Focus()      '将焦点移到文件号文字框
            GoTo hhh               '跳转标签处退出添加过程
        End If
        SQL = "select * from 文件基本信息"     '准备将窗体上的数据添加到数据库中,SQL语句表示,选择文件基本信息表,选择所有字段值记录
        rs = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        rs.Open(SQL, cnn, 1, 3) '按指定对象打开记录集对象

        With rs           '开始添加数据
            .AddNew                            '逐一添加各个字段的数据
            For i = 0 To UBound(myArray)    '在0到数组上标上循环
                If Me.Controls(myArray(i).ToString).Name = "发布日期" Or Me.Controls(myArray(i).ToString).Name = "实施日期" Then

                    '别删除  下面注释掉的语句也是转换成真日期的,mystr = xlapp.WorksheetFunction.Text(Me.Controls(myArray(i).ToString).Text, "0000-00-00"),  'mydate = DateSerial(Split(mystr, "-")(0), Split(mystr, "-")(1), Split(mystr, "-")(2))
                    Dim mystr As String, mydate As Date             '声明变量
                    mystr = Me.Controls(myArray(i).ToString).Text   '变量赋值
                    mydate = CType(mystr, Date)                     '转换日期类型
                    .Fields(i).value = mydate                       '写入数据库指定字段
                Else
                    .Fields(i).value = Me.Controls(myArray(i).ToString).Text '写入数据库指定字段的添加值
                End If
            Next i
            .Update     '更新数据表
        End With

        MsgBox("已经成功将新文件数据添加到数据库中！", vbInformation, "添加记录")     '提示成功信息

        '刷新查询  'Call 显示文件基本信息()
        Call 查询文件基本信息()
        Call 显示文件清单()
hhh:
        rsNum.Close         '关闭指定记录集
        rsNum = Nothing     '释放变量
    End Sub
#End Region



#Region "修改文件信息"
    Private Sub 修改_Click(sender As Object, e As EventArgs) Handles 修改.Click
        Dim SQL As String
        '如果点了NO,则退出程序
        If MsgBox("本操作将修改文件号为<" & 文件号.Text & ">的文件记录！" & vbCrLf & "是否要更新？", vbQuestion + vbYesNo, "更新记录") = vbNo Then Exit Sub
        Dim i As Integer    '声明变量
        '准备修改记录,SQL语句表示,从文件基本信息表,按照指定条件(文件号字段值=文件号文本框的值),筛选所有字段值记录
        SQL = "select * from 文件基本信息 where 文件号='" & 文件号.Text & "'"
        rs = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        rs.Open(SQL, cnn, 1, 3)             '打开指定信息的记录集
        '修改更新记录
        With rs                             '引用指定信息的记录
            For i = 0 To UBound(myArray)     '在0到数组上标上循环
                If Me.Controls(myArray(i).ToString).Name = "实施日期" Or Me.Controls(myArray(i)).Name = "发布日期" Then
                    Dim mystr As String, mydate As Date             '声明变量
                    mystr = Me.Controls(myArray(i).ToString).Text   '给变量赋值
                    'mydate = DateSerial(Split(mystr, "/")(0), Split(mystr, "/")(1), Split(mystr, "/")(2)):'发布日期.Text = Format(Today(), "yyyy-M-d")    '文本框（签订日期）输入相应的格式
                    mydate = CType(mystr, Date)                     '转换日期类型
                    .Fields(i).value = mydate                       '变量写入数据库指定字段
                Else                                                            '否则执行语句
                    .Fields(i).value = Me.Controls(myArray(i).ToString).Text    '字段值等于对应控件上的值
                End If  '结束语句
            Next i      '循环语句
            .Update     '更新数据表
        End With        '结束引用语句
        MsgBox("已经成功将编号为<" & 文件号.Text & ">的文件记录进行了更新！", vbInformation, "更新记录")    '提示信息
        '刷新查询
        Call 查询文件基本信息()    '调用子程序
        Call 显示文件清单()
        'ListView2.Items(f1).Selected = True
        'ListView2.Focus()
    End Sub
#End Region

#Region "'删除文件信息"
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        Dim SQL As String
        If MsgBox("本操作将删除编号为<" & 文件号.Text & ">的文件记录！" & vbCrLf & "是否要删除？", vbQuestion + vbYesNo, "删除记录") = vbNo Then Exit Sub  '如果对提示框选择了NO,那么退出程序
        SQL = "delete from 文件基本信息 where 文件号='" & 文件号.Text & "'"      '删除,SQL语句表示,从文件基本信息表中,按指定条件,删除指定记录
        rs = cnn.Execute(SQL)                                                    '数据库连接打开指定记录集对象
        SQL = "delete from 文件创建更改信息 where 文件号='" & 文件号.Text & "'"  '删除更改信息,SQL语句表示删除指定记录
        rs = cnn.Execute(SQL)   '数据库连接打开指定记录集对象
        MsgBox("已经成功将编号为<" & 文件号.Text & ">的文件记录删除！", vbInformation, "删除记录") '提示删除成功的信息
        Call 查询文件基本信息()   '调用子程序
        Call 显示文件基本信息()   '调用子程序
        Call 显示文件更改情况()   '调用子程序
        Call 显示文件清单()       '调用子程序
    End Sub
#End Region

#Region "第一条"
    Private Sub 第一条_Click(sender As Object, e As EventArgs) Handles 第一条.Click
        If rs.BOF And rs.EOF Then Exit Sub '如果没有有记录，就退出程序...
        '如果是第一条记录之前，就退出过程，以免再次单击此按钮时出现错误 'If rs.BOF Then Exit Sub
        rs.MoveFirst  '将指针移到第一条记录
        '调用子程序在窗体上显示第一条记录
        Call 显示文件基本信息()   '调用子程序
        Call 显示文件更改情况()   '调用子程序
        Call 显示文件清单()       '调用子程序
        ListView2.Items(0).Selected = True '选中第一条项目...
    End Sub
#End Region

#Region "下一条"
    Private Sub 下一条_Click(sender As Object, e As EventArgs) Handles 下一条.Click
        If rs.BOF And rs.EOF Then Exit Sub  '如果数据表中没有记录，就退出过程
        If rs.EOF Then Exit Sub             '如果是最末一条记录之后，就退出过程，以免再次单击此按钮时出现错误
        rs.MoveNext                         '将指针移到下一条记录, '调用子程序在窗体上显示下一条记录
        If rs.EOF Then Exit Sub             '如果已经是最末一条记录，就退出过程，以免再次单击此按钮时出现错误
        ListView2.Items(rs.AbsolutePosition - 1).Selected = True    '显示控件蓝色光标
        ListView2.Focus()                                           '聚焦控件
    End Sub
#End Region

#Region "上一条"
    Private Sub 上一条_Click(sender As Object, e As EventArgs) Handles 上一条.Click
        If rs.BOF And rs.EOF Then Exit Sub  '如果数据表中没有记录，就退出过程
        If rs.BOF Then Exit Sub             '如果已经是第一条记录，就退出过程，以免再次单击此按钮时出现错误
        rs.MovePrevious                     '将指针移到上一条记录
        If rs.BOF Then Exit Sub             '如果已经是第一条记录，就退出过程，以免再次单击此按钮时出现错误
        ListView2.Items(rs.AbsolutePosition - 1).Selected = True    '显示控件蓝色光标
        ListView2.Focus()                                           '聚焦控件
    End Sub
#End Region

#Region "最末条_Click"
    Private Sub 最末条_Click(sender As Object, e As EventArgs) Handles 最末条.Click
        If rs.BOF And rs.EOF Then Exit Sub    '如果数据表中没有记录，就退出过程
        rs.MoveLast                           '如果已经是最末一条记录，就退出过程，以免再次单击此按钮时出现错误
        '如果已经是最末一条记录，就退出过程，以免再次单击此按钮时出现错误
        '    If rs.EOF Then Exit Sub
        '调用子程序在窗体上显示最末条记录
        ListView2.Items(rs.AbsolutePosition - 1).Selected = True    '显示控件蓝色光标
        ListView2.Focus()  '聚焦控件
        Call 显示文件基本信息()   '调用子程序
        Call 显示文件更改情况()   '调用子程序
    End Sub
#End Region

#Region "查询_Click"
    Private Sub 查询_Click(sender As Object, e As EventArgs) Handles 查询.Click
        On Error Resume Next
        Dim myId As String, strSeachMethod As String                                   '声明变量
        Dim rsSerch As Object                                '声明并创建记录对象
        rsSerch = CreateObject("ADODB.Recordset")            '创建一个无信息的记录集对象,方便引用
        新文件.PerformClick()                                '调用新文件按钮事件
        Me.Visible = False                                   '隐藏窗体

        strSeachMethod = xlapp.InputBox("输入数字1(文件号)或者数字2(文件名称)", "选择方式")  '给变量myId赋值(输入文件号)   'If Sh.Name Like "*货款" Then  '如果sh的工作表名称以“货款”结尾
        If strSeachMethod = 2 Then
            myId = xlapp.InputBox("请输入关键字：", "文件查询")  '给变量myId赋值(输入文件号)   'If Sh.Name Like "*货款" Then  '如果sh的工作表名称以“货款”结尾
            Me.Visible = True                                    '显示窗体
            Me.TopMost = True                                    '置顶窗体
            If Len(Trim(myId)) = 0 Then                          '如果字段长度为0
                MsgBox("没有输入关键字！", vbCritical, "警告")   '提示信息
                Exit Sub    '退出程序
            End If          '结束if语句
            rs.MoveFirst    '移动到首条记录上
            For i = 1 To rs.RecordCount       '在1到记录数量上循环
                If rs.Fields("文件名称").value.ToString Like "*" & UCase(myId) & "*" Then     '这个like是VB的语法不能用SQL语法 "%" & myId & "%"如果记录集相关字段(文件号)的值=变量的值,那么执行
                    'myId.ToLower可以把字符串改成全部小写字母,myId.ToUpper可以转换成全部大写字母
                    Call 显示文件基本信息()   '调用子程序
                    Call 显示文件更改情况()   '调用子程序
                    f1 = rs.AbsolutePosition - 1  '把文件记录信息的位置赋值给模块级变量f1
                    ListView2.Items(f1).Selected = True '在listview中选中搜索到的记录行...
                    ListView2.Focus()  'listview 获得焦点...
                    Exit Sub    '退出程序
                Else            '否则
                    rs.MoveNext '移动到下一条记录
                End If          '结束判断语句
            Next i              '循环
            MsgBox("没有文件名称为<" & myId & ">的文件！", vbCritical, "查询结果")      '提示搜索失败信息:'rs.MoveFirst '移动到第一条记录
            Me.TopMost = True   '置顶
        Else
            myId = xlapp.InputBox("请输入关键字：", "文件查询")  '给变量myId赋值(输入文件号)   'If Sh.Name Like "*货款" Then  '如果sh的工作表名称以“货款”结尾
            Me.Visible = True                                    '显示窗体
            Me.TopMost = True                                    '置顶窗体
            If Len(Trim(myId)) = 0 Then                          '如果字段长度为0
                MsgBox("没有输入关键字！", vbCritical, "警告")   '提示信息
                Exit Sub    '退出程序
            End If          '结束if语句
            rs.MoveFirst    '移动到首条记录上
            For i = 1 To rs.RecordCount       '在1到记录数量上循环
                If rs.Fields("文件号").value.ToString Like "*" & UCase(myId) & "*" Then     '这个like是VB的语法不能用SQL语法 "%" & myId & "%"如果记录集相关字段(文件号)的值=变量的值,那么执行
                    'myId.ToLower可以把字符串改成全部小写字母,myId.ToUpper可以转换成全部大写字母
                    Call 显示文件基本信息()   '调用子程序
                    Call 显示文件更改情况()   '调用子程序
                    f1 = rs.AbsolutePosition - 1
                    ListView2.Items(f1).Selected = True
                    ListView2.Focus()
                    Exit Sub    '退出程序
                Else            '否则
                    rs.MoveNext '移动到下一条记录
                End If          '结束判断语句
            Next i              '循环
            MsgBox("没有文件号为<" & myId & ">的文件！", vbCritical, "查询结果")      '提示搜索失败信息:'rs.MoveFirst '移动到第一条记录
            Me.TopMost = True   '置顶
        End If
    End Sub
#End Region

#Region "退出"
    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        cnn.Close  '关闭指定的数据库连接
        rs = Nothing  '释放变量
        cnn = Nothing  '释放变量
        Globals.Ribbons.Ribbon1.btnFileInfo.Enabled = True  'Rib.Button17.Enabled = True
        Me.Close()   '关闭窗体
    End Sub
#End Region

#Region "添加类别"
    Private Sub 添加类别_Click(sender As Object, e As EventArgs) Handles 添加类别.Click
        Dim lb As String    '声明变量
        lb = 文件类别.Text '给变量lb赋值
        If Len(Trim(lb)) = 0 Then   '如果字符为0长度,那么执行下面语句
            MsgBox("没有输入文件类别名称！不能添加！", vbCritical, "警告")   '提示警告信息
            Exit Sub    '退出程序
        End If  '结束判定
        Dim rsx As Object '声明对象
        rsx = CreateObject("ADODB.Recordset")
        Dim SQL As String   '声明变量
        'SQL语句表示从文件类别信息表中按照指定条件(文件类别字段值=lb),筛选出所有字段值记录
        SQL = "select * from 文件类别信息 where 文件类别='" & lb & "'"
        rsx.Open(SQL, cnn, 1, 3)   '打开指定记录集对象
        If rsx.BOF And rsx.EOF Then     '如果没有记录
            rsx.AddNew  '新添加一行
            rsx.Fields(0) = lb  '记录的第一字段(索引下标是0)开始重新赋值
            rsx.Update  '更新记录集
            '提示添加成功
            MsgBox("添加完毕！", vbInformation, "添加文件类别")
        Else    '否则
            '提示已存在
            MsgBox("已经存在了同名的文件类别名称！", vbCritical, "警告")
        End If
        rsx.Close   '关闭记录集对象
        rsx = Nothing   '释放变量
        Call 文件类别复合框设置()     '调用子程序
        文件类别.Text = lb
    End Sub
#End Region

#Region "删除类别"
    '删除文件类别复选框里面的项目
    Private Sub 删除类别_Click(sender As Object, e As EventArgs) Handles 删除类别.Click
        Dim lb As String    '声明类别名称的变量
        lb = 文件类别.Text '给变量lb赋值
        If Len(Trim(lb)) = 0 Then   '如果字符为0长度,那么执行下面语句
            MsgBox("没有输入相关文件类别名称！不能删除！", vbCritical, "警告")   '提示警告信息
            Exit Sub    '退出程序
        End If  '结束判定
        Dim rsx As Object '声明记录集对象,作为类别对象记录
        rsx = CreateObject("ADODB.Recordset") '赋值
        Dim SQL As String   '声明变量,作为执行语句
        'SQL语句表示从文件类别信息表中按照指定条件(文件类别字段值=lb),筛选出所有字段值记录
        SQL = "select * from 文件类别信息 where 文件类别='" & lb & "'"

        rsx.Open(SQL, cnn, 1, 3)   '打开指定记录集对象
        If rsx.BOF = False And rsx.EOF = False Then '末条和首行都有记录,表示有记录,如果有记录
            SQL = "delete from 文件类别信息 where 文件类别='" & lb & "'"
            '以下两种方式打开记录集,注释掉的一种需要先关闭已打开的记录集,另一种不需要先关闭
            'rsx.Close
            'rsx.Open SQL, cnn, 1, 3   '打开指定记录集对象


            rsx = cnn.Execute(SQL)   '数据库连接打开指定记录集对象
            SQL = "select * from 文件类别信息 " '重新写SQL语句,查询所有类别记录

            '用了delete语句不能直接使用update更新,需要重新打开记录集才能更新
            rsx.Open(SQL, cnn, 1, 3)   '打开指定记录集对象
            rsx.Update  '更新记录集

            '提示添加成功
            MsgBox("删除完毕！", vbInformation, "删除文件类别")
        Else    '否则提示,没有要删除的记录
            MsgBox("不存在相关的文件类别名称！", vbCritical, "警告")
        End If
        'rsx.Close   '关闭记录集对象,可以省略,也可以注释
        rsx = Nothing   '释放变量
        Call 文件类别复合框设置()     '调用子程序
        文件类别.Text = ""
    End Sub
#End Region

#Region "添加部门_Click"
    '详细注释可参考上面的添加类别
    Private Sub 添加部门_Click(sender As Object, e As EventArgs) Handles 添加部门.Click
        Dim bm As String        '声明变量
        bm = 使用部门.Text     '变量赋值
        If Len(Trim(bm)) = 0 Then   '如果字符长度为空
            '提示信息警告
            MsgBox("没有输入使用部门名称！不能添加！", vbCritical, "警告")
            Exit Sub    '退出程序
        End If  '结束判定语句
        Dim rsx As Object  '声明并创建记录集对象
        rsx = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        Dim SQL As String   '声明变量
        '指定SQL语句,表示选择部门信息表,按照指定条件(字段部门名称=变量bm的值),筛选出所有字段值记录
        SQL = "select * from 新罗公司部门 where 部门名称='" & bm & "'"
        rsx.Open(SQL, cnn, 1, 3)   '打开指定记录集对象
        If rsx.BOF And rsx.EOF Then '如果没有相关记录
            rsx.AddNew  '没有记录就添加新的一行
            rsx.Fields(0) = bm  '字段值重新赋值为变量bm的值
            rsx.Update      '指定记录值更新
            MsgBox("添加完毕！", vbInformation, "添加部门")  '提示成功信息
        Else    '否则执行
            MsgBox("已经存在了同名的部门！", vbCritical, "警告")     '提出警告信息
        End If  '结束判定语句
        rsx.Close   '关闭记录集对象
        rsx = Nothing   '释放变量
        Call 使用部门复合框设置()     '调用子程序
        使用部门.Text = bm '重新给签订部门文本框值修改值
    End Sub
#End Region

#Region "删除部门"
    Private Sub 删除部门_Click(sender As Object, e As EventArgs) Handles 删除部门.Click
        Dim bm As String    '声明变量
        bm = 使用部门.Text '给变量bm赋值
        If Len(Trim(bm)) = 0 Then   '如果字符为0长度,那么执行下面语句
            MsgBox("没有输入部门名称！不能添加！", vbCritical, "警告")   '提示警告信息
            Exit Sub    '退出程序
        End If  '结束判定
        Dim rsx As Object '声明对象
        rsx = CreateObject("ADODB.Recordset")
        Dim SQL As String   '声明变量
        'SQL语句表示从新罗公司部门表中按照指定条件(部门名称字段值=bm),筛选出所有字段值记录
        SQL = "select * from 新罗公司部门 where 部门名称='" & bm & "'"
        rsx.Open(SQL, cnn, 1, 3)   '打开指定记录集对象
        If rsx.BOF = False And rsx.EOF = False Then '如果有记录
            SQL = "delete from 新罗公司部门 where 部门名称='" & bm & "'"
            '以下两种方式打开记录集,注释掉的一种需要先关闭已打开的记录集,另一种不需要先关闭
            'rsx.Close
            'rsx.Open SQL, cnn, 1, 3   '打开指定记录集对象
            rsx = cnn.Execute(SQL)   '数据库连接打开指定记录集对象
            SQL = "select * from 新罗公司部门 "
            rsx.Open(SQL, cnn, 1, 3)   '打开指定记录集对象
            '用了delete语句不能使用update更新,需要重新打开记录集才能更新
            rsx.Update  '更新记录集
            '提示添加成功
            MsgBox("删除完毕！", vbInformation, "删除部门名称")
        Else    '否则
            '提示已存在
            MsgBox("不存在相关的部门名称名称！", vbCritical, "警告")
        End If
        'rsx.Close   '关闭记录集对象
        rsx = Nothing   '释放变量
        Call 使用部门复合框设置()     '调用子程序
        使用部门.Text = ""
    End Sub
#End Region

#Region "发生项目项转移触发的事件"
    'ListView 变动触发事件,逐一重新给文本框赋值当前记录
    Private Sub ListView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView2.SelectedIndexChanged
        'If ListView2.SelectedItems.Count <> 0 Then
        '    文件记录数目.Text = ListView2.SelectedItems(0).Index '当前选定项的索引
        '    'Label1.Text = ListView1.SelectedItems(1).Text  '当前选定项的内容
        'End If
        Dim i As Integer, a As Integer    '声明变量
        Call 查询文件基本信息()   '调用子程序
        rs.MoveFirst    '记录集移动到第一条
        If ListView2.SelectedItems.Count <> 0 Then
            rs.AbsolutePosition = ListView2.SelectedItems(0).Index + 1   '修改记录值位置为项目行索引值
            a = rs.AbsolutePosition
        End If
        For i = 0 To UBound(myArray)    '在0到数组上标上循环
            If IsNothing(rs.Fields(i).value) Then    '如果字段值为空白
                Me.Controls(myArray(i).ToString).Text = ""  '控件文本框为空值
            Else    '否则
                If Me.Controls(myArray(i).ToString).Name = "发布日期" Or Me.Controls(myArray(i).ToString).Name = "实施日期" Then
                    Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToShortDateString
                Else
                    Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToString   '记录值逐一写入控件值中
                End If
                'Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToString   '控件文本框值等于对应字段值
            End If      '结束判定
        Next i          '继续循环
        文件记录数目.Text = "数据库中共有 " & rs.RecordCount & " 条文件记录" & Space(5) & "目前是第 " & a & " 条文件记录"
        f1 = a - 1    '赋值给模块变量,作为记录位置
        Call 显示文件更改情况()   '调用子程序
    End Sub
#End Region

#Region "打开文件"
    '双击指定文本框,打开文件
    Private Sub 存放位置_DoubleClick(sender As Object, e As EventArgs) Handles 存放位置.DoubleClick
        On Error Resume Next
        If InStr(存放位置.Text, "xls") > 0 Then     '如果是包含有xls后缀存储名
            xlapp.Workbooks.Open(存放位置.Text)     '打开EXCEL
        Else                                        '否则
            'Shell("winword.exe " & 存放位置.Text, vbMaximizedFocus)   'shell函数打开word文档程序    
            Shell("explorer.exe " & 存放位置.Text, vbMaximizedFocus)   '打开所有资源程序
        End If                  '结束语句
        If Err.Number <> 0 Then MsgBox("打开对应文件失败",, "提示")
    End Sub
#End Region

#Region "A01_文件基本信息_Closed"
    '关闭文件时,触发事件,重新启用按钮
    Private Sub A01_文件基本信息_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnFileInfo.Enabled = True
    End Sub
#End Region

#Region "'打开文件"
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        存放位置_DoubleClick(Me, New EventArgs)     '参数可以设置Nothing.
    End Sub
#End Region

    Private Sub 存储名_DoubleClick(sender As Object, e As EventArgs) Handles 存储名.DoubleClick
        On Error Resume Next
        If InStr(存储名.Text, "xls") > 0 Then     '如果是包含有xls后缀存储名
            xlapp.Workbooks.Open(存储名.Text)     '打开EXCEL
        Else
            'Shell("winword.exe " & 存放位置.Text, vbMaximizedFocus)   'shell函数打开word文档程序    
            Shell("explorer.exe " & 存储名.Text, vbMaximizedFocus)   '打开所有资源程序
        End If                  '结束语句
        If Err.Number <> 0 Then MsgBox("打开对应文件失败",, "提示")
    End Sub

    Private Sub btnGetAddress_Click(sender As Object, e As EventArgs) Handles btnGetAddress.Click
        '声明一个变体型变量(在VB.net中已经不能再称之为变体型变量，而是Object.
        Dim objFileArray As Object, arrFileArrayResetting() As String
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
            存放位置.Text = arrFileArrayResetting(0)
        Else
            Exit Sub  '结束过程
        End If
    End Sub


#Region "全导出文件"
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        On Error Resume Next
        Dim wb As Excel.Workbook      '声明变量及数据类型
        Dim ws As Excel.Worksheet     '声明变量及数据类型
        'Dim rngTargetRange As Excel.Range
        ''Dim i As Integer, j As Integer, objArray(,) As Object  '声明变量及数据类型
        wb = xlapp.Workbooks.Add       '给变量赋值,新建工作簿
        ws = wb.ActiveSheet     '给变量赋值,激活活动工作簿
        'xlapp.ScreenUpdating = False  '关闭屏幕更新



        Dim myTable As String = "文件基本信息"    '指定数据表

        rs.close                                  '关闭记录链接
        rs.Open(myTable, cnn, 1, 3)               '打开查询数据集
        xlapp.Cells.Clear()                       '清除Excel表格所有数据
        With rs                                   '准备复制数据
            For i As Integer = 1 To .Fields.Count
                xlapp.Cells(1, i).Value = .Fields(i - 1).Name  '字段名写入单元格中
            Next
            xlapp.Range("A2").CopyFromRecordset(rs)            '复制记录
        End With

        For i = 2 To rs.RecordCount + 1 '数据记录数量+1 表示的是加上标题行为最后一行对应的Excel的行数
            '在B列创建超级链接,从而允许单击单元格时进入相应的工作表
            ws.Hyperlinks.Add(xlapp.Cells(i, 5), xlapp.Cells(i, 5).value.ToString)
            xlapp.Cells(i, 5).value = "Click here"
        Next
        ws.ListObjects.Add(1, xlapp.Range("a1").CurrentRegion,, 1).Name = "表4"
        ws.ListObjects("表4").Sort.SortFields.Clear()
        ws.ListObjects("表4").Sort.SortFields.Add(xlapp.Range("a2"), 0, 1, 0)
        With ws.ListObjects("表4").Sort
            .Header = 1
            .MatchCase = False
            .Orientation = 1
            .SortMethod = 1
            .Apply()
        End With

        xlapp.Range("a2").CurrentRegion.EntireColumn.AutoFit()      '自动调整列宽
        xlapp.Range("a2").CurrentRegion.Borders.LineStyle = 1       '加框线
        xlapp.Range("a2").CurrentRegion.HorizontalAlignment = -4108 '水平中间放置
        xlapp.Range("a2").CurrentRegion.VerticalAlignment = -4108   '垂直中间放置
    End Sub

    Private Sub 发布日期_GotFocus(sender As Object, e As EventArgs) Handles 发布日期.GotFocus
        发布日期.Mask = "0000/00/00"
    End Sub

    Private Sub 实施日期_GotFocus(sender As Object, e As EventArgs) Handles 实施日期.GotFocus
        实施日期.Mask = "0000/00/00"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDisplayUpdatingWindow.Click
        Dim f As New A03_文件创建更改管理
        'f.ShowDialog()  '模态窗体,不可以编辑
        f.Show() '显示窗体,非模态窗体可以编辑
        Globals.Ribbons.Ribbon1.Button19.Enabled = False
    End Sub

#End Region



End Class