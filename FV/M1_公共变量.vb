Module M1_公共变量

    ''Globals代表全局匹配,ThisAddin代表插件对象,Application代表程序
    'Public xlapp As Excel.Application = Globals.ThisAddIn.Application

    ''' <summary>
    ''' 获取当前 Excel 应用程序对象（实时获取，避免初始化空值）
    ''' </summary>
    Public ReadOnly Property xlapp() As Excel.Application
        Get
            Return Globals.ThisAddIn.Application
        End Get
    End Property

    ' ★★★ 颜色常量定义（改为 Public，供所有模块共享） ★★★
    Public Const COLOR_DARK_YELLOW As Integer = 44
    Public Const COLOR_LIGHT_YELLOW As Integer = 6
    Public Const COLOR_LIGHT_GREEN As Integer = 35
    Public Const COLOR_GREEN As Integer = 43


    '声明两个公共变量，其中Targetsht代表要备份数据的工作表，TargetRng代表要备份数据的区域的地址
    Public Targetsht As Excel.Worksheet, TargetRng As String

    '声明变量(数组),文件管理数据库调用
    Public myArray As Object


    Public cnn As Object   '声明数据库连接变量
    Public rs As Object, rs1 As Object

    '声明模块级变量,方便调用
    Public wjh As String, gglb As String, gxrq As String, ggms As String, bc As String, qz As String, bz As String
    Public arr As Object, i As Long  'GN011_获取文件信息
    Public strFielPath As String     'GN012声明文件夹变量



    '阅读功能使用
    Public YueDu As Boolean = True '声明一个布尔型的公共变量

End Module
