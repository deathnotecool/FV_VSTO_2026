Imports System.Windows.Forms

Public Class A03_文件创建更改管理
    Private Sub A03_文件创建更改管理_Invalidated(sender As Object, e As InvalidateEventArgs) Handles Me.Invalidated
        Dim myData As String        '声明变量
        Dim SQL As String           '声明变量
        Dim i As Integer            '声明变量
        '指定数据库名称
        myData = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\文件管理.accdb"  '公司共享盘
        'myData = "D:\2 笔记记录\8 过程信息管理\文件管理\文件管理.accdb"  '台式机测试
        'myData = "D:\3笔记记录\0_过程信息管理笔记\文件管理\文件管理.accdb" '三星笔记本本地测试
        'myData = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\文件管理.accdb"  '公司共享盘
        '设置窗体控件组（也是数据表的各个字段组）
        myArray = {"文件号", "更改类别", "版次", "更改描述", "更新日期", "签字", "备注"}
        '建立与数据库的连接,创建ado最顶层对象
        cnn = CreateObject("ADODB.Connection")
        With cnn    '引用最顶层数据库对象
            .Provider = "microsoft.Ace.OLEDB.12.0"   '数据库引擎提供者
            .Open(myData)
            '        .Provider = "microsoft.jet.oledb.4.0"   '数据库提供者
            '        .Open mydata    '打开指定数据库名称信息
        End With        '结束引用对象语句
        '调用子程序，为更改类别复合框设置项目
        Call 更改类别复合框设置()
        '从“文件基本信息”中查询文件号，设置给“文件号”复合框
        '    Dim rsx As New ADODB.Recordset
        Dim rsx As Object
        rsx = CreateObject("ADODB.Recordset")
        SQL = "select 文件号 from 文件基本信息"
        rsx.Open(SQL, cnn, 1, 3)
        With 文件号
            .Items.Clear()   '清除项目
            For i = 1 To rsx.RecordCount
                '.Items.Add(rsx.Fields(0).value)
                .Items.Add(rsx.Fields("文件号").value)
                rsx.MoveNext
            Next i
        End With
        文件号.SelectedIndex = 0
        rsx.Close
        rsx = Nothing
        '调用子程序，查询并显示文件更改明细信息
        Call 查询文件更改明细   '调用子程序
        Call 显示文件更改明细
        '    Call 显示文件更改情况
        Call 查询文件基本信息
        Call 显示文件清单
    End Sub
    Public Sub 更改类别复合框设置()     '上接主程序
        '    Dim rsx As New ADODB.Recordset  '声明记录对象
        Dim rsx As Object  '声明记录对象
        rsx = CreateObject("ADODB.Recordset")
        rsx.Open("更改类别信息", cnn, 1, 3)   '打开指定数据表对象
        With 更改类别   '引用复合框
            .Items.Clear()   '清除项目
            '在0到记录集数目-1(打开是所有文件类别信息表的记录)上循环
            For i = 0 To rsx.RecordCount - 1
                '添加项目(字段值的value属性是默认值)为当前字段值
                .Items.Add(rsx.Fields(0).value)
                rsx.MoveNext    '定位到下一个记录
            Next i  '循环记录集数量
        End With        '结束引用
        rsx.Close       '关闭记录
        rsx = Nothing       '释放变量
    End Sub
    Public Sub 查询文件更改明细()   '接主程序
        Dim SQL As String           '声明变量
        'SQL语句表示,从文件更改信息信息表中,按照指定条件(文件号字段值等于文件号文本框的值),那么筛选出所有字段的值
        SQL = "select * from 文件创建更改信息 where 文件号='" & 文件号.Text & "'"
        '    Set rs = New ADODB.Recordset    '创建记录对象
        rs = CreateObject("ADODB.Recordset")

        rs.Open(SQL, cnn, 1, 3)    '打开指定SQL语句信息的记录集
    End Sub
    Public Sub 显示文件更改明细()   '接主程序
        On Error Resume Next        '出错继续在错误处执行
        Dim i As Integer
        With ListView1      '引用视图控件
            '设置ListView1的标题、显示类型、整行选择和网格线属性
            .Columns.Clear()    '清除标题行
            .Clear()    '清除项目集
            .View = View.Details   '报表输出视图
            .FullRowSelect = True   '允许整行选择
            .GridLines = True       '允许网格线
            '为ListView1设置标题,在0到字段数量-1上循环
            For i = 0 To rs.Fields.Count - 1
                .Columns.Add(rs.Fields(i).Name.ToString, 100)  '添加标题
            Next i
            '为ListView1设置各行数据
            For i = 1 To rs.RecordCount     '在1到记录数量上循环
                Dim itm As ListViewItem = ListView1.Items.Add(rs.Fields(0).Value.ToString)
                For j = 1 To rs.Fields.Count - 1    '在1到字段总数-1上循环
                    'itm.SubItems.AddRange({"钢笔", "500", "2012-9-15"})
                    If TypeName(rs.Fields(j).value) = "Date" Then
                        Dim a As String
                        a = rs.Fields(j).value.ToShortDateString    '设置短日期格式的文本
                        itm.SubItems.AddRange({a})      '短日期格式的文本逐一写入项目中
                    Else
                        itm.SubItems.AddRange({rs.Fields(j).value.ToString}) '从第2列开始添加索引列的子项目值
                    End If
                Next j      '循环语句
                rs.MoveNext     '定位到下一条记录
            Next i  '循环
        End With    '结束引用对象
        rs.MoveFirst    '回到第一条记录
    End Sub
    Public Sub 显文件更改信息()
        On Error Resume Next        '错误继续在错误处执行语句
        Dim i As Integer            '声明变量
        '显示文件更改的第一条信息
        For i = 0 To UBound(myArray)    '遍历数组元素
            If IsNothing(rs.Fields(i).value) Then   '如果没有记录执行
                Me.Controls(myArray(i).ToString).Text = ""  '赋值为空值
            Else    '否则
                If Me.Controls(myArray(i).ToString).Name = "更新日期" Then  '如果控件名称是"更新日期",那么执行
                    Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToShortDateString    '给控件写入短日期格式
                Else    '否则
                    Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToString   '记录值逐一写入控件值中
                End If
            End If
        Next i
    End Sub
#Region "创建一个查询文件基本信息公共过程，方便调用"
    Public Sub 查询文件基本信息()   '在加载窗体后,调用的子程序
        '已声明公共变量
        rs1 = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        '打开(创建)指定数据库表(合同基本信息)的记录集,第一参数数据库表名,第二参数数据库对象(已经打开指定的数据库连接),3参数使用的指定的游标类型,4参数是锁定类型,这里设置可操作记录的锁定类型
        rs1.Open("文件基本信息", cnn, 1, 3)
    End Sub
#End Region
    Public Sub 显示文件清单()   '接主程序
        'On Error Resume Next        '出错继续在错误处执行
        Dim i As Integer
        With ListView2      '引用视图控件
            '设置ListView1的标题、显示类型、整行选择和网格线属性
            .Columns.Clear()    '清除标题行
            .Clear()    '清除项目集
            .View = View.Details   '报表输出视图
            .FullRowSelect = True   '允许整行选择
            .GridLines = True       '允许网格线
            '为ListView1设置标题,在0到字段数量-1上循环
            For i = 0 To rs1.Fields.Count - 1
                .Columns.Add(rs1.Fields(i).Name.ToString, 100)   '添加标题
            Next i
            '为ListView1设置各行数据
            For i = 1 To rs1.RecordCount     '在1到记录数量上循环
                Dim itm As ListViewItem = ListView2.Items.Add(rs1.Fields(0).Value.ToString)
                For j = 1 To rs1.Fields.Count - 1    '在1到字段总数-1上循环
                    'itm.SubItems.AddRange({"钢笔", "500", "2012-9-15"})
                    If TypeName(rs1.Fields(j).value) = "Date" Then
                        Dim a As String '声明变量作为添加的子项目的元素
                        a = rs1.Fields(j).value.ToShortDateString    '设置短日期格式的文本
                        itm.SubItems.AddRange({a})      '短日期格式的文本逐一写入项目中
                    Else
                        itm.SubItems.AddRange({rs1.Fields(j).value.ToString}) '从第2列开始添加索引列的子项目值
                    End If
                Next j      '循环语句
                rs1.MoveNext     '定位到下一条记录
            Next i  '循环
        End With    '结束引用对象
        rs1.MoveFirst    '回到第一条记录
    End Sub

    Private Sub 查询_Click(sender As Object, e As EventArgs) Handles 查询.Click
        'On Error Resume Next
        Dim myId As String      '声明变量
        Dim SQL As String       '声明SQL变量
        Dim i As Integer        '声明变量
        'Dim rsSerch As New ADODB.Recordset  '声明记录对象
        Dim rsSerch As Object  '声明记录对象
        rsSerch = CreateObject("ADODB.Recordset")
        For i = 0 To UBound(myArray)        '在0到数组上标上循环
            Me.Controls(myArray(i).ToString).Text = ""  '控件文本框全部清空数据
        Next i      '循环语句
        ListView1.Clear()   '清除项目.Clear       '清空视图控件项目值
        Me.Visible = False  '隐藏窗体
        myId = xlapp.InputBox("请输入文件号：", "文件查询")   '输入的值赋值给变量
        Me.Visible = True   '显示窗体
        'Me.TopMost = True
        If Len(Trim(myId)) = 0 Then     '如果字段为0,
            '提示信息
            MsgBox("没有输入文件号！", vbCritical, "警告")
            Exit Sub    '退出程序
        End If      '结束判定
        '从文件创建更改信息表中,获取所有字段记录集
        SQL = "select * from 文件创建更改信息 "     '以下模糊查询
        rs = CreateObject("ADODB.Recordset")         '创建记录对象
        rs.Open(SQL, cnn, 1, 3)        '打开指定记录对象
        For i = 1 To rs.RecordCount     '在1到记录数量上循环
            If rs.Fields("文件号").value.ToString Like "*" & UCase(myId) & "*" Then      '如果记录集相关字段(文件号)的值=变量的值,那么执行
                Call 显文件更改信息()       '调用子程序
                Call 查询文件更改明细()
                Call 显示文件更改明细()       '调用子程序
                Exit Sub
            Else
                rs.MoveNext     '记录光标移动到下一条
            End If      '结束判定
        Next i
        MsgBox("没有文件号为<" & myId & ">的文件更改创建信息！", vbCritical, "查询结果")
    End Sub
    Private Sub 删除_Click(sender As Object, e As EventArgs) Handles 删除.Click
        Dim SQL As String
        '如果选择了NO,则退出程序
        If MsgBox("本操作将删除编号为<" & wjh & ">的文件更改记录！" _
        & vbCrLf & "是否要删除？",
        vbQuestion + vbYesNo, "删除记录") = vbNo Then Exit Sub
        '   Call 文件号_Change
        'SQL语句表示从合同收费信息表中,按照指定条件(相应字段等于相应的变量值),则删除该记录
        SQL = "delete * from 文件创建更改信息 where 文件号='" & wjh & "'" _
        & " and 更改类别='" & 更改类别.Text & "'" _
        & " and 版次='" & 版次.Text & "'" _
        & " and 更新日期=#" & 更新日期.Text & "#" _
        & " and 签字='" & 签字.Text & "'" _
        & " and 更改描述='" & 更改描述.Text & "'"
        rs = CreateObject("ADODB.Recordset")
        rs.Open(SQL, cnn, 1, 3)        '打开指定记录对象
        '    Set rs = cnn.Execute(SQL)       '打开相应的记录集对象
        '提示成功删除记录
        MsgBox("已经成功将编号为<" & wjh & ">的文件更改记录删除！",
        vbInformation, "删除记录")
        '刷新显示
        Call 查询文件更改明细()   '调用子程序
        Call 显示文件更改明细()   '调用子程序
    End Sub
#Region "新纪录按钮触发事件"
    Private Sub 新记录_Click(sender As Object, e As EventArgs) Handles 新记录.Click
        On Error Resume Next    '错误在错误处继续执行
        Dim i As Integer    '声明变量
        For i = 0 To UBound(myArray)    '在0到数组变量上循环
            Me.Controls(myArray(i).ToString).Text = ""  '情况文本框中的值
        Next i  '循环
        更改类别.SelectedIndex = 0      '复合框选择第一项
        更新日期.Text = Format(Today(), "yyyy-M-d")      '更新日期文本框值为指定格式的日期
        文件号.Focus()     '文件号接收焦点
    End Sub
#End Region

    Public Sub 文件号_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 文件号.SelectedIndexChanged
        On Error Resume Next
        Dim i As Integer            '声明变量
        For i = 1 To UBound(myArray)        '在1到数组变量上标上循环
            Me.Controls(myArray(i).ToString).Text = ""      '在相应文本框控件上清空数值
        Next i
        Call 查询文件更改明细()       '调用子程序
        Call 显文件更改信息()       '调用子程序
        Call 显示文件更改明细()       '调用子程序
        wjh = 文件号.Text
        gglb = 更改类别.Text
        Dim mystr As String, mydate As Date     '声明变量
        mystr = Me.Controls("更新日期").Text   '给变量赋值
        mydate = DateSerial(Split(mystr, "/")(0), Split(mystr, "/")(1), Split(mystr, "/")(2))
        gxrq = mydate.ToString
        ggms = 更改描述.Text
        bc = 版次.Text
        qz = 签字.Text
        bz = 备注.Text
    End Sub
    Private Sub 添加_Click(sender As Object, e As EventArgs) Handles 添加.Click
        Dim i As Integer, SQL As String   '声明变量
        '判断是否在窗体上输入了必要的文件更改信息
        For i = 0 To UBound(myArray)
            '如果控件名称不等于备注那么执行
            If Me.Controls(myArray(i).ToString).Name <> "备注" Then
                '如果控件名称的值等于空值那么执行
                If Me.Controls(myArray(i).ToString).Text = "" Then
                    '提示信息不能为空
                    MsgBox(Me.Controls(myArray(i).ToString).Name & "不能为空！", vbCritical)
                    '控件获得焦点
                    Me.Controls(myArray(i)).Focus
                    Exit Sub    '退出程序
                End If  '结束判定
            End If  '结束判定
        Next i      '继续循环
        '如果选择了NO,那么退出程序
        If MsgBox("本操作将添加新的文件更改记录！" & vbCrLf & "是否要添加？",
            vbQuestion + vbYesNo, "添加记录") = vbNo Then Exit Sub
        '准备将窗体上的数据添加到数据库中,SQL语句从文件创建更改信息表,筛选所有字段值记录
        SQL = "select * from 文件创建更改信息"
        rs = CreateObject("ADODB.Recordset")
        rs.Open(SQL, cnn, 1, 3)        '打开指定SQL语句的记录集对象
        '开始添加数据
        With rs     '引用指定的记录集对象
            .AddNew    '添加各个字段的数据,添加新的一行记录(全部为空,需要输入相应的值)
            For i = 0 To UBound(myArray)    '在0到数组上标上循环
                If Me.Controls(myArray(i).ToString).Name = "更新日期" Then   '如果文本框的名称为更新日期,那么执行
                    Dim mystr As String, mydate As Date
                    mystr = Me.Controls(myArray(i).ToString).Text
                    mydate = CType(mystr, Date)
                    'mystr = xlapp.WorksheetFunction.Text(Me.Controls(myArray(i).ToString).Text, "0000-00-00")
                    'mydate = DateSerial(Split(mystr, "-")(0), Split(mystr, "-")(1), Split(mystr, "-")(2))
                    .Fields(i).value = mydate '只要是关于日期的文本框,设定指定控件值的格式为中日期
                    '字段值等于文本控件值(按照指定日期格式)
                Else    '否则
                    .Fields(i).value = Me.Controls(myArray(i).ToString).Text  '直接等于文本框控件的值
                End If      '结束判定语句
            Next i      '循环
            .Update    '更新数据表
        End With        '结束引用对象语句
        '提示成功信息
        MsgBox("已经成功将该文件更改信息数据添加到数据库中！", vbInformation, "添加记录")
        '刷新显示
        Call 显文件更改信息()   '调用子程序
        Call 查询文件更改明细()
        Call 显示文件更改明细()
        '    End If
        rs.Close    '关闭指定记录集对象
        rs = Nothing    '释放变量
    End Sub

    Private Sub 修改_Click(sender As Object, e As EventArgs) Handles 修改.Click
        '如果选择了no,那么退出程序
        If MsgBox("本操作将修改文件号为<" & wjh & ">的文件更改记录！" _
            & vbCrLf & "是否要更新？",
            vbQuestion + vbYesNo, "更新记录") = vbNo Then Exit Sub
        Dim i As Integer, SQL As String     '声明变量
        '    wjh = 文件号.Value
        '    gglb = 更改类别.Value
        '    gxrq = Format(更新日期.Value, "yyyy-mm-dd")
        '    ggms = 更改描述.Value
        '    bc = 版次.Value
        '    qz = 签字.Value
        '    bz = 备注.Value
        '修改更新记录,SQL语句表示从合同收费信息表,按照指定条件(相应的字段同时满足变量的值),重新设置文件号,更改类别,更新日期,更改描述,备注字段的值
        Dim mystr As String, mydate As Date     '声明变量
        mystr = Me.Controls("更新日期").Text   '给变量赋值
        'mydate = DateSerial(Split(mystr, "/")(0), Split(mystr, "/")(1), Split(mystr, "/")(2))


        mydate = CType(mystr, Date)



        SQL = "update 文件创建更改信息 set " _
            & "文件号='" & 文件号.Text & "'," _
            & "更改类别='" & 更改类别.Text & "'," _
            & "更新日期=#" & mydate.ToShortDateString & "#," _
            & "更改描述='" & 更改描述.Text & "'," _
            & "版次='" & 版次.Text & "'," _
            & "备注='" & Trim(备注.Text) & "'," _
            & "签字='" & 签字.Text & "' " _
            & " where 文件号='" & wjh & "'" _
            & " and 更改类别='" & gglb & "'" _
            & " and 更新日期=#" & gxrq & "#" _
            & " and 更改描述='" & ggms & "'" _
            & " and 版次='" & bc & "'" _
            & " and 签字='" & qz & "'" _
            & "and 备注='" & bz & "' "
        '    Set rs = New ADODB.Recordset    '创建记录对象
        rs = CreateObject("ADODB.Recordset")   '创建记录对象
        rs.Open(SQL, cnn, 1, 3)    '打开记录集对象
        '提示成功信息
        MsgBox("已经成功将编号为<" & wjh & ">的文件创建更改记录进行了更新！",
        vbInformation, "更新记录")
        '刷新显示
        Call 查询文件更改明细()       '调用子程序
        Call 显示文件更改明细()
    End Sub
#Region "退出按钮单击触发事件"
    Public Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click
        cnn.Close       '数据库连接关闭
        rs = Nothing        '释放变量
        cnn = Nothing       '释放变量
        Globals.Ribbons.Ribbon1.Button19.Enabled = True
        Me.Close()    '卸载窗体
    End Sub
#End Region
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer, a As Integer    '声明变量
        Call 查询文件更改明细()   '调用子程序
        rs.MoveFirst    '记录集移动到第一条

        If ListView1.SelectedItems.Count <> 0 Then
            rs.AbsolutePosition = ListView1.SelectedItems(0).Index + 1   '修改记录值位置为项目行索引值
            a = rs.AbsolutePosition
        End If
        For i = 0 To UBound(myArray)    '在0到数组上标上循环
            If IsNothing(rs.Fields(i).value) Then    '如果字段值为空白
                Me.Controls(myArray(i).ToString).Text = ""  '控件文本框为空值
            Else    '否则
                If Me.Controls(myArray(i).ToString).Name = "更新日期" Then
                    Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToShortDateString
                Else
                    Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToString   '记录值逐一写入控件值中
                End If
                'Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToString   '控件文本框值等于对应字段值
            End If      '结束判定
        Next i          '继续循环
        '    Call 文件号_Change
        wjh = 文件号.Text       '给变量wjh赋值为文件号文本框值
        gglb = 更改类别.Text        '给变量gglb赋值为更改类别文本框值
        gxrq = 更新日期.Text     '给变量gxrq赋值为文本框值并设置相应的格式
        ggms = 更改描述.Text           '给变量ggms赋值为文本框值
        bc = 版次.Text
        qz = 签字.Text
        bz = 备注.Text
    End Sub

    Private Sub ListView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView2.SelectedIndexChanged
        Dim i As Integer, SQL As String, a As Integer  '声明变量
        Call 查询文件基本信息()   '调用子程序
        rs1.MoveFirst    '记录集移动到第一条
        If ListView2.SelectedItems.Count <> 0 Then
            rs1.AbsolutePosition = ListView2.SelectedItems(0).Index + 1   '修改记录值位置为项目行索引值
            a = rs1.AbsolutePosition
        End If
        Dim rsx As Object                    '声明变量
        rsx = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        'SQL语句表示,从文件创建更改信息表中,根据指定条件(字段合同号的字段值等于文本框文件号的值),筛选所有所有字段记录

        SQL = "select * from 文件创建更改信息 where 文件号='" & rs1.Fields(0).value.ToString & "'"

        'SQL = "select * from 文件创建更改信息 where 文件号='" & rs1.Fields(i) & "'"
        '打开指定记录集对象
        rsx.Open(SQL, cnn, 1, 3)
        If rsx.RecordCount = 0 Then
            MsgBox("没有相关的履历卡记录,请及时添加")
            '文件号.SelectedIndex = a - 1
            For i = 0 To UBound(myArray)    '在0到数组变量上循环
                Me.Controls(myArray(i).ToString).Text = ""  '情况文本框中的值
            Next i  '循环
            RemoveHandler 文件号.SelectedIndexChanged, AddressOf 文件号_SelectedIndexChanged   '解除事件
            文件号.SelectedIndex = a - 1
            AddHandler 文件号.SelectedIndexChanged, AddressOf 文件号_SelectedIndexChanged      '绑定事件
            ListView1.Clear()
            Exit Sub
        End If
        For i = 0 To UBound(myArray)    '在0到数组上标上循环
            If Me.Controls(myArray(i)).Name = "文件号" Then 文件号.SelectedIndex = a
            'If IsNothing(rsx.Fields(i)) Then    '如果字段值为空白
            If IsNothing(rsx.Fields(i).value) Then    '如果字段值为空白
                Me.Controls(myArray(i).ToString).Text = ""  '控件文本框为空值
            Else    '否则
                If Me.Controls(myArray(i).ToString).Name = "更新日期" Then
                    Me.Controls(myArray(i).ToString).Text = rsx.Fields(i).value.ToShortDateString
                Else
                    Me.Controls(myArray(i).ToString).Text = rsx.Fields(i).value.ToString   '记录值逐一写入控件值中
                End If
                'Me.Controls(myArray(i).ToString).Text = rs.Fields(i).value.ToString   '控件文本框值等于对应字段值
            End If      '结束判定
        Next i          '继续循环
        Call 显示文件更改明细()   '调用子程序

    End Sub
#Region "关闭触发事件"
    Private Sub A03_文件创建更改管理_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.Button19.Enabled = True
    End Sub

    Private Sub 更新日期_GotFocus(sender As Object, e As EventArgs) Handles 更新日期.GotFocus
        更新日期.Mask = "0000/00/00"
    End Sub
#End Region

End Class