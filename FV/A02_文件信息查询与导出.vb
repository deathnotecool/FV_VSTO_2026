Imports System.Windows.Forms


'2022.12.14 开始
Public Class A02_文件信息查询与导出
    Private Sub A02_文件信息查询与导出_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myData As String            '声明变量及数据类型
        '指定数据库名称
        myData = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\文件管理.accdb"  '公司共享盘
        'myData = "F:\2 笔记记录\8 过程信息管理\文件管理\文件管理.accdb"  '台式机测试
        'myData = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\文件管理.accdb"  '公司共享盘
        'myData = "D:\3笔记记录\0_过程信息管理笔记\文件管理\文件管理.accdb" '三星笔记本本地测试
        '建立与数据库的连接
        cnn = CreateObject("ADODB.Connection")   '创建数据库对象
        With cnn        '引用数据库对象
            .Provider = "microsoft.Ace.OLEDB.12.0"   '数据库引擎提供者
            .Open（myData）        '打开指定名称的数据库对象
            '        .Provider = "microsoft.jet.oledb.4.0"
            '        .Open mydata
        End With        '结束引用对象语句
        '为信息种类复合框设置项目
        With 信息种类       '引用复合框对象
            .Items.Add（"文件基本信息"）     '添加项目为合同基本信息
            .Items.Add（"文件创建更改信息"）   '添加项目为合同收费信息
        End With                                '结束引用语句
        信息种类.SelectedIndex = 1              '设置默认选择行
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
        '运算符.ListIndex = 0  'VBA 选择默认行
        运算符.SelectedIndex = 0
    End Sub


#Region "信息种类发生变化时触发的事件"
    Private Sub 信息种类_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 信息种类.SelectedIndexChanged
        'On Error Resume Next        '错误时继续执行
        Dim SQL As String, i As Integer
        With 查询项目       '引用查询项目复合框
            .Items.Clear()  '清除项目
            SQL = "select * from " & 信息种类.Text 'SQL语句表示从信息种类文本框值对应的数据表,筛选出所有字段值信息
            rs = CreateObject("ADODB.Recordset") '创建一个无信息的记录集对象,方便引用
            rs.Open(SQL, cnn, 1, 3) '打开指定记录集
            For i = 0 To rs.Fields.Count - 1 '在0到字段数量上循环
                .Items.Add(rs.Fields(i).Name.ToString) '添加项目
            Next i
        End With  '结束引用语句
        查询项目.SelectedIndex = 0      '选择默认项目为第一项
        Call 清除显示信息()       '调用子程序
    End Sub


#End Region
#Region "清除显示信息"
    Public Sub 清除显示信息()       '接主程序
        With ListView1              '引用视图控件
            .Columns.Clear()    '清除标题行
            .Clear()    '清除项目集
            .View = View.Details   '报表输出视图
            .FullRowSelect = True   '允许整行选择
            .GridLines = True       '网格线显示
        End With                    '结束引用语句
    End Sub
#End Region
    '2022.12.14 结束



#Region "查询项目"
    Private Sub 查询项目_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 查询项目.SelectedIndexChanged
        On Error Resume Next            '错误继续执行
        Dim SQL As String
        rs = CreateObject("ADODB.Recordset")        '创建记录对象
        SQL = "select distinct " & 查询项目.Text & " from " & 信息种类.Text  'SQL语句表示从信息种类符合框显示的对应的数据表,筛选选不重复值(查询项目复合框显示的值)
        rs.Open(SQL, cnn, 1, 3)  '打开记录集对象
        条件值1.Text = ""  '清除文本框条件值1的值
        条件值2.Text = ""  '清除文本框条件值2的值
        条件值1.Items.Clear()
        条件值2.Items.Clear()
        For i = 1 To rs.RecordCount     '在1到记录数量上循环
            条件值1.Items.Add(rs.Fields(查询项目.Text).value.ToString)   '添加项目值为记录字段所对应的值
            条件值2.Items.Add(rs.Fields(查询项目.Text).value.ToString)   '添加项目值为记录字段所对应的值
            rs.MoveNext     '定位到下一条记录
        Next i      '结束循环
        条件值1.SelectedIndex = 0       '默认选择第一行项目
        条件值2.SelectedIndex = 0       '默认选择第一行项目
        Call 清除显示信息()       '调用子程序
    End Sub
#End Region
#Region "运算符变化"
    Private Sub 运算符_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 运算符.SelectedIndexChanged
        If 运算符.Text <> "between" Then   '如果运算符的值不是"between",那么执行
            Label_and.Visible = False       '标签隐藏
            Label_Value2.Visible = False    '标签隐藏
            条件值2.Visible = False         '复合框隐藏
            条件值1.Width = 179             '重新定义条件值1复合框宽度
        Else        '否则执行
            Label_and.Visible = True        '显示标签跟复合框
            Label_Value2.Visible = True
            条件值2.Visible = True
            条件值1.Width = 179      '重新定义条件值1复合框宽度
        End If      '结束判定
    End Sub
#End Region
#Region "重设条件"
    Private Sub 重设条件_Click(sender As Object, e As EventArgs) Handles 重设条件.Click
        信息种类.SelectedIndex = 0          '选择默认行
        查询项目.SelectedIndex = 0           '选择默认行
        运算符.SelectedIndex = 0             '选择默认行
        条件值1.SelectedIndex = 0            '选择默认行
        条件值2.SelectedIndex = 0            '选择默认行
        Call 清除显示信息()           '调用子程序
    End Sub
#End Region
#Region "开始查询"
    Private Sub 开始查询_Click(sender As Object, e As EventArgs) Handles 开始查询.Click
        Dim SQL As String, i As Integer          '声明变量
        Dim Condition As String, Con0 As String, Con1 As String, Con2 As String
        '设置查询条件
        Con0 = " where "        '给变量赋值
        '如果查询项目的值等于相应的日期值
        If 查询项目.Text = "发布日期" Or 查询项目.Text = "实施日期" Or 查询项目.Text = "更新日期" Then
            '设置相应的日期格式
            '发布日期.Text = Format(Today(), "yyyy-M-d")     '文本框（签订日期）输入相应的格式
            'Con1 = "#" & Format(条件值1.Text, "yyyy-M-d") & "#"
            'Con2 = "#" & Format(条件值2.Text, "yyyy-M-d") & "#"
            'Con1 = "#" & xlapp.WorksheetFunction.Text(Me.Controls(条件值1.Text).Text.ToUpper, "0000-00-00") & "#"
            'Con2 = "#" & xlapp.WorksheetFunction.Text(Me.Controls(条件值2.Text).Text.ToUpper, "0000-00-00") & "#"
            'Split(mystr, "-")(0)
            Con1 = "#" & Split(条件值1.Text, " ")(0) & "#"
            Con2 = "#" & Split(条件值2.Text, " ")(0) & "#"
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
        SQL = "select * from " & 信息种类.Text & Condition
        '开始查询
        rs = CreateObject("ADODB.Recordset")     '创建记录对象
        rs.Open(SQL, cnn, 1, 3)        '打开对应的记录集
        If rs.BOF And rs.EOF Then       '如果没有记录
            '提示信息
            MsgBox("没有查询到结果！", vbCritical, "查询结果")
            Exit Sub    '退出程序
        End If  '结束判定语句
        '将查询结果显示在Listview控件中
        With ListView1
            '设置ListView1的标题、显示类型、整行选择和网格线属性
            .Columns.Clear()    '清除标题行
            .Clear()    '清除项目集
            .View = View.Details   '报表输出视图
            .FullRowSelect = True       '允许选择整行
            .GridLines = True           '网格线显示
            '为ListView1设置标题
            For i = 0 To rs.Fields.Count - 1        '在0到字段值数量上-1上循环
                '.ColumnHeaders.Add , , rs.Fields(i).Name        '别删 VBA给标题行逐个命名
                .Columns.Add(rs.Fields(i).Name.ToString, 100)   '给标题行逐个命名
            Next i      '循环
            '为ListView1设置各行数据
            '为ListView1设置各行数据
            For i = 1 To rs.RecordCount     '在1到记录数量上循环
                Dim itm As ListViewItem = ListView1.Items.Add(rs.Fields(0).Value.ToString) '首列为项目父值（相当于行标题）.
                For j = 1 To rs.Fields.Count - 1    '在1到字段总数-1上循环
                    '别删  参考 itm.SubItems.AddRange({"钢笔", "500", "2012-9-15"})
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
            rs.MoveFirst    '移到第一条记录
        End With        '结束引用语句
    End Sub
#End Region
#Region "数据导出"
    Private Sub 数据导出_Click(sender As Object, e As EventArgs) Handles 数据导出.Click
        Dim wb As Excel.Workbook      '声明变量及数据类型
        Dim ws As Excel.Worksheet     '声明变量及数据类型
        Dim i As Integer, j As Integer  '声明变量及数据类型
        wb = xlapp.Workbooks.Add       '给变量赋值,新建工作簿
        ws = wb.ActiveSheet     '给变量赋值,激活活动工作簿
        With ws                     '引用工作表
            For i = 0 To rs.Fields.Count - 1    '在0到字段数量上-1处循环
                .Cells(1, i + 1) = rs.Fields(i).Name        '逐一写入字段名
            Next i      '循环
            With .Range(xlapp.Cells(1, 1), xlapp.Cells(1, rs.Fields.Count))   '引用区域
                .Font.Bold = True       '字体加粗
                .HorizontalAlignment = -4108 '区域对齐方式为中心
            End With        '结束引用语句
            For i = 1 To rs.RecordCount     '在1到记录数量上循环
                For j = 0 To rs.Fields.Count - 1
                    .Cells(i + 1, j + 1) = rs.Fields(j).value     '在单元格上写入字段值
                    '                 If rs.Fields(j).Type = adDate Then      '如果字段值类型是日期
                    If rs.Fields(j).Type = 7 Then      '如果字段值类型是日期
                        .Cells(i + 1, j + 1).NumberFormat = "yyyy-mm-dd"        '设置日期格式
                    End If      '结束判定
                    '                 If rs.Fields(j).Type = adCurrency Then      '如果字段类型是货币
                    If rs.Fields(j).Type = 6 Then      '如果字段类型是货币
                        .Cells(i + 1, j + 1).NumberFormat = "#,##0.00"      '设置货币千分号格式
                    End If      '结束判定
                Next j      '循环
                rs.MoveNext     '移动到下一条记录
            Next i      '循环
            .Columns.AutoFit()    '列标自动设置适应宽度
        End With        '结束引用
        ws = Nothing        '释放变量
        wb = Nothing
        关闭窗体.PerformClick()
    End Sub
#End Region
#Region "关闭窗体"
    Private Sub 关闭窗体_Click(sender As Object, e As EventArgs) Handles 关闭窗体.Click
        On Error Resume Next
        cnn.Close   '关闭指定的数据库连接
        rs = Nothing    '释放变量
        cnn = Nothing
        Globals.Ribbons.Ribbon1.Button18.Enabled = True
        Me.Close()
    End Sub
#End Region
#Region "文件信息查询与导出"
    Private Sub A02_文件信息查询与导出_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        关闭窗体_Click(Nothing, Nothing)
    End Sub
#End Region
End Class