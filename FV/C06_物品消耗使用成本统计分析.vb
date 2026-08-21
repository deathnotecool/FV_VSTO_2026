
Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710      myArray = {"保养单号", "设备编号", "保养费用", "保养级别", "保养内容", "保养时间", "替换件编号", "工时"}
Imports System.Data     '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing      '使用颜色命名空间
Public Class C06_物品消耗使用成本统计分析
    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim strSharePath As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb"
    Dim strYiFangPath As String = "\\192.168.3.52\Users\进销存管理.accdb"
    Dim strMyHomerComputerPath As String = "E:\access\进销存管理.accdb"
    Dim strMyCompanyComputerPath As String = "D:\6 总务\access\进销存管理.accdb"
    Dim objConnection2th As New OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" & strSharePath)  '公司共享盘



    'Dim objConnection2th As New OleDbConnection _
    '  ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb")  '公司共享盘
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\进销存管理\进销存管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\进销存管理\进销存管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb")  '公司共享盘
    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '该构造函数使用了SelectCommand属性的一个字符串和一个表示数据库连接的对象来初始化SqlAdapater对象.
    '这个构造函数使我们不必写Adapter属性代码.
    'Dim objDataAdapter As New OleDbDataAdapter("SELECT 保养.* FROM 保养 ORDER BY 保养单号", objConnection2th)
    Dim objDataAdapter1th As New OleDbDataAdapter()
    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    'Dim objDataSet As New DataSet()
    Dim objDataSet1th As New DataSet()
    '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    'Dim objDataView As DataView
    Dim objDataView1th As DataView
    Dim objCurrencyManager As CurrencyManager '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object     '声明变量,数据库用
    Private Sub C06_物品消耗使用成本统计分析_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        起始日期.Value = Now.AddMonths(-1).ToShortDateString
        '    起始日期.Value = Date
        截止日期.Value = Now.ToShortDateString
        查询物品名称()
    End Sub

    Public Sub 查询物品名称()
        'On Error Resume Next
        'On Error Resume Next        '错误时继续执行
        '初始化OleDbCommand类的一个实例,并将其分配给SelectCommand属性.   Set the SelectCommand properties..
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        '将Connection属性设置为连接对象.用来与数据库通信.
        objDataAdapter1th.SelectCommand.Connection = objConnection2th
        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        objDataAdapter1th.SelectCommand.CommandText = "select distinct 物品名称 from 物品消耗使用信息  where 出库使用日期 between #" &
      起始日期.Value & "# and #" & 截止日期.Value & "#"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        'Try
        objDataAdapter1th.Fill(objDataSet1th, "fxxx1")
        'Catch SqlExceptionErr As System.Exception
        'MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示错误信息
        'End Try
        objDataView1th = New DataView(objDataSet1th.Tables("fxxx1"))
        Dim tb As DataTable = objDataSet1th.Tables("fxxx1")
        Dim a As Byte = tb.Rows.Count - 1
        Dim i As Byte
        物品名称.Items.Clear()
        With 物品名称
            For i = 0 To a
                .Items.Add(tb.Rows(i).Item(0).ToString)
            Next
        End With  '结束引用语句
        物品名称.SelectedIndex = 0      '选择默认项目为第一项
    End Sub
    Private Sub 物品名称_SelectedIndexChanged(sender As Object, e As EventArgs) Handles 物品名称.SelectedIndexChanged
        'On Error Resume Next        '错误时继续执行
        '初始化OleDbCommand类的一个实例,并将其分配给SelectCommand属性.   Set the SelectCommand properties..
        objDataAdapter1th.SelectCommand = New OleDbCommand()
        '将Connection属性设置为连接对象.用来与数据库通信.
        objDataAdapter1th.SelectCommand.Connection = objConnection2th
        '设置选择命令字符串的CommandText属性设置为要要执行的SQL语句(也可以是存储过程)
        '该SQL语句表示2个一对多,即多对多关系,从连接表中按指定条件(au_id相等的titleauthor记录,title_id相等的记录).     
        '选出指定列(姓,名,书名,价格),并按指定条件(名和姓)升序排序
        objDataAdapter1th.SelectCommand.CommandText = "select distinct 物品规格 from 采购物品信息 " &
             "where 物品名称='" & 物品名称.Text & "'"
        '这里的SelectCommand的CommandType属性就是CommandType.Text,是默认属性可以省略的.
        objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
        '数据适配器对象开始检索数据并填充到DataSet对象
        'Fill方法的第二参数可以随便填,最好填相关的数据源表,方便理解.  
        'Fill the DataSet object with data..
        objDataSet1th = New DataSet()
        'Try
        objDataAdapter1th.Fill(objDataSet1th, "fxxx2")
        'Catch SqlExceptionErr As System.Exception
        '    MessageBox.Show(SqlExceptionErr.Message)    '如果出错,提示错误信息
        'End Try
        objDataView1th = New DataView(objDataSet1th.Tables("fxxx2"))
        Dim tb As DataTable = objDataSet1th.Tables("fxxx2")
        Dim a As Byte = tb.Rows.Count - 1
        Dim i As Byte
        物品规格.Items.Clear()
        With 物品规格
            .Items.Add("全部规格")
            For i = 0 To a
                .Items.Add(tb.Rows(i).Item(0).ToString)
            Next
        End With
        物品规格.SelectedIndex = 0      '选择默认项目为第一项
    End Sub

    Private Sub 重置条件_Click(sender As Object, e As EventArgs) Handles 重置条件.Click
        On Error Resume Next        '防出错,继续执行
        起始日期.Value = Now.AddMonths(-1).ToShortDateString       '重新赋值给起始日期控件值
        截止日期.Value = Now.ToShortDateString           '重新赋值结束日期控件值
        物品名称.SelectedIndex = 0  '选择首行项目
        物品规格.SelectedIndex = 0  '选择首行项目
    End Sub

    Private Sub 统计分析_Click(sender As Object, e As EventArgs) Handles 统计分析.Click
        'On Error Resume Next
        Dim i As Integer
        Dim wb As Excel.Workbook      '声明变量及数据类型
        Dim ws As Excel.Worksheet     '声明变量及数据类型
        Dim sql As String
        wb = xlapp.Workbooks.Add       '给变量赋值,新建工作簿
        ws = wb.ActiveSheet     '给变量赋值,激活活动工作簿
        '设置工作表标题
        ws.Range("A1").Value = 物品名称.Text & " 的消耗/使用成本统计分析"
        With ws.Range("A1:E1")
            .Merge()
            .Font.Size = 15
            .RowHeight = 20
            .Font.Bold = True
            .HorizontalAlignment = -4108
            .Borders(9).Weight = -4138
        End With
        ws.Range("A2:E2").Value = {"出库使用日期", "物品名称", "物品规格", "消耗使用数量", "消耗使用成本"}
        '从数据表中查询数据
        i = 3
        xlapp.ScreenUpdating = False    '禁止屏幕刷新，提升工作效率
        For myDateDay As Integer = 0 To (Now.Subtract(起始日期.Value).TotalDays - Now.Subtract(截止日期.Value).TotalDays)
            ws.Range("A" & i).Value = Format(起始日期.Value.AddDays(myDateDay), "yyyy-MM-dd")
            ws.Range("B" & i).Value = 物品名称.Text
            ws.Range("C" & i).Value = 物品规格.Text
            '计算某日指定规格之采购项目的销售总数和销售总额
            If 物品规格.Text = "全部规格" Then
                '从加工使用信息表中,按照指定条件(使用日期=变量值,采购项目名称值=控件值),根据所选字段(聚合函数合计值)返回记录集并对字段名称重新命名
                sql = "select sum(消耗使用数量) as aa," &
                    "sum(消耗使用数量*成本单价) as bb from 物品消耗使用信息 " &
                     "where 出库使用日期=#" & Format(起始日期.Value.AddDays(myDateDay), "yyyy-MM-dd") & "#" &
                   " and 物品名称='" & 物品名称.Text & "'"
            Else
                sql = "select sum(消耗使用数量) as aa," &
                     "sum(消耗使用数量*成本单价) as bb from 物品消耗使用信息 " &
                     "where 出库使用日期=#" & Format(起始日期.Value.AddDays(myDateDay), "yyyy-MM-dd") & "#" &
                     " and 物品名称='" & 物品名称.Text & "'" &
                     " and 物品规格='" & 物品规格.Text & "'"
            End If
            'SQL语句执行后产生的数据填充一个表的模板
            objDataAdapter1th.SelectCommand = New OleDbCommand()
            objDataAdapter1th.SelectCommand.Connection = objConnection2th
            objDataAdapter1th.SelectCommand.CommandText = sql
            objDataAdapter1th.SelectCommand.CommandType = CommandType.Text
            objDataSet1th = New DataSet()
            Dim tbx As Byte = 0
            objDataAdapter1th.Fill(objDataSet1th, CType(tbx + i, String))
            'objDataView1th = New DataView(objDataSet.Tables("jhxx2"))
            Dim tb As DataTable = objDataSet1th.Tables(CType(tbx + i, String))
            'Dim a As Byte = tb.Columns.Count - 1
            ws.Range("D" & i).Value = tb.Rows(0).Item(0).ToString()   '添加项目值为记录字段所对应的值
            ws.Range("E" & i).Value = tb.Rows(0).Item(1).ToString()
            i = i + 1           '计数器加1
        Next myDateDay     '循环
        ws.Columns.AutoFit()  '自动调整列宽
        xlapp.Range("A2:E30").Select()      '选择区域
        xlapp.Charts.Add()  '新建图表
        xlapp.ActiveChart.ChartType = 51   '活动图表类型为簇状柱形图(本来就是默认的,多设置下保险起见)
        xlapp.ActiveChart.SetSourceData(Source:=
          xlapp.Sheets("Sheet1").Range("A2:A" & i - 1 & ",D2:E" & i - 1), PlotBy:=2)  '为指定图表设置源数据区域。第一参数表示:包含源数据的区域。第二参数:指定数据绘制方式。可为以下 XlRowCol 常量之一：xlColumns 或 xlRows。
        xlapp.ActiveChart.SeriesCollection(2).Select      '选择第二系列(已用成本金额)
        xlapp.ActiveChart.SeriesCollection(2).AxisGroup = 2   '更换次坐标轴
        '    ActiveChart.ChartArea.Select        '选择图表活动区域
        xlapp.ActiveChart.SeriesCollection(2).Select  '选择第二系列
        xlapp.ActiveChart.SeriesCollection(2).ChartType = 65      '更换类型带数据点的曲线视图
        '开始绘制统计分析图表
        '    Range("A2:E6").Select
        ''    Charts.Add
        '    ActiveChart.ApplyCustomType ChartType:=xlBuiltIn, TypeName:="两轴线-柱图"
        '    ActiveChart.SetSourceData Source:= _
        '        Sheets("Sheet1").Range("A2:A" & i - 1 & ",D2:E" & i - 1), PlotBy:=xlColumns
        xlapp.ActiveChart.Location(Where:=2, Name:="Sheet1")  '图表移动到新位置
        With xlapp.ActiveChart
            .HasTitle = True    '显示标题
            .ChartTitle.Characters.Text = 物品名称.Text & " 使用成本统计图表"
            .Axes(1, 1).HasTitle = True     '显示纵坐标轴标题   '
            .Axes(1, 1).AxisTitle.Characters.Text = "出库使用日期"     '重新赋值为日期
            .Axes(2, 1).HasTitle = True   '显示横坐标轴标题
            .Axes(2, 1).AxisTitle.Characters.Text = "消耗使用数量"    '重新赋值为已用数量
            '        .Axes(xlCategory, xlSecondary).HasTitle = False     '次坐标
            .Axes(2, 2).HasTitle = True      '显示次坐标标题
            .Axes(2, 2).AxisTitle.Characters.Text = "消耗使用成本(元)"       '重新赋值为加工使用成本元

            .SeriesCollection(2).Select
            With xlapp.Selection
                .MarkerStyle = 1
                .MarkerSize = 4
            End With
            xlapp.Selection.MarkerStyle = 8

        End With
        ws = Nothing    '释放变量
        wb = Nothing
        xlapp.ScreenUpdating = True    '禁止屏幕刷新，提升工作效率
        'C06_物品消耗使用成本统计分析_Load(Nothing, Nothing)
        关闭退出_Click(Nothing, Nothing)

    End Sub

    Private Sub 关闭退出_Click(sender As Object, e As EventArgs) Handles 关闭退出.Click
        '清理内存及数据适配器对象
        objDataAdapter1th = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection2th = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button36.Enabled = True     '重新使按钮可用.
        Me.Close()  '关闭窗体
    End Sub

    Private Sub C06_物品消耗使用成本统计分析_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        objDataAdapter1th = Nothing           '清理数据适配器对象,释放内存 ' Clean up
        objConnection2th = Nothing            '清理连接对象,释放内存
        Globals.Ribbons.Ribbon1.Button36.Enabled = True  '重新使按钮可用.
    End Sub


End Class