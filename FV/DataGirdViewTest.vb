Imports System.Windows.Forms  '使用窗体命名空间,窗体尺寸831, 710
Imports System.Data           '使用DatSet和DataView类所必须的.
Imports System.Data.OleDb     '使用OleDbConnection、OleDbAdapter、OleDbCommand、OleDbParameter类所必须的.
Imports System.Drawing        '使用颜色命名空间
Public Class DataGirdViewTest

    'Dim sqlConn As SqlConnection

    'Dim sqlCmd As SqlCommand

    'Dim sqlDa As SqlDataAdapter
    'Dim sqlDs As DataSet

    '    sqlConn = New SqlConnection("Data Source=.;Initial Catalog=jwinfo;Integrated Security=True;")




    '    sqlDa = New SqlDataAdapter("SELECT * FROM 学生信息", sqlConn)

    '    sqlDs = New DataSet()
    '    sqlDa.Fill(sqlDs, "学生信息")

    '    RowMergeView1.DataSource = sqlDs.Tables("学生信息")

    '    RowMergeView1.AddSpanHeader(2, 2, "合并后的名称")
    '    RowMergeView1.ColumnHeadersHeight = 40
    '    RowMergeView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing











    '声明作用域为类级的对象,该对象建立了与数据库的连接,此时数据库为Access.
    Dim objConnection As New OleDbConnection _
               ("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb")  '公司共享盘





    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=D:\2_公司专用\3笔记记录\0_过程信息管理笔记\进销存管理\进销存管理.accdb")  '三星笔记本
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=F:\2 笔记记录\8 过程信息管理\进销存管理\进销存管理.accdb")  '家里台式机
    '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\进销存管理.accdb")  '公司共享盘

    '声明作用域为类级的对象,该对象用于从数据库中读取数据,并填充到DataSet对象中.
    '该构造函数使用了SelectCommand属性的一个字符串和一个表示数据库连接的对象来初始化SqlAdapater对象.
    '这个构造函数使我们不必写Adapter属性代码.
    Dim objDataAdapter As New OleDbDataAdapter("SELECT * FROM 学生信息", objConnection)
    Dim objDataSet As New DataSet()    '声明作用域为类级的对象,该对象作为数据的容器,将所有数据存储到内存中,并不连接到数据库.
    Dim objDataView As DataView        '声明作用域为类级的对象,DataView类用来表示定制从数据库返回以及存储在DatSet(DataTable)中的记录视图
    Dim objCurrencyManager As CurrencyManager  '声明作用域为类级的对象,一个CurrencyManger对象,用于控制绑定数据的移动.作为管理Binding对象的列表
    Dim myArray As Object                      '声明变量,数据库用

    '创建一个过程将在初始化代码中调用,以用来填充数据和显示数据
    Private Sub FillDataSetAndView()
        objDataSet = New DataSet()  '创建并初始化一个数据集对象赋值给变量 Initialize a new instance of the DataSet object.
        '向DataSet对象填充由SqlDataAdapter对象的选择命令SelectCommand属性从数据库检索到的数据填充. 
        '注意:Fill方法使用选择命令SelectCommand.connection,如果该连接已打开,那么执行该选择命令,连接没打开就会自动打开填充数据后关闭连接  Fill the DataSet object with data..
        objDataAdapter.Fill(objDataSet, "学生信息")  '这里没有设置SelectCommand属性,因为在初始化Adapter对象时,已经使用了相应的参数.
        '设置对应表为数据源绑定到DataView类  Set the DataView object to the DataSet object.
        RowMergeView1.DataSource = objDataSet.Tables("学生信息")
        'RowMergeView1.(2, 2, "合并后的名称")

        objDataView = New DataView(objDataSet.Tables("学生信息"))
        'BindingContect管理CurrencyManager(保持数据与控件同步的对象)集合,指定相应的CurrencyManger,引用定制视图源作为指定的CurrencyManager      Set our CurrencyManager object to the DataView object.
        objCurrencyManager =
      CType(Me.BindingContext(objDataView), CurrencyManager)
    End Sub




















End Class