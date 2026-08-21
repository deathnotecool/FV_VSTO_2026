'所有插件需要调用的跨模块级别的任务...
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel


' ★★★GN004 结构定义：温度数据点 ★★★
' 学习要点：
'   1. 结构是值类型，适合存储轻量级数据组合
'   2. 结构可以包含字段、属性和方法
'   3. 适合在不需要继承的场景下使用
'   4. 本例中用于存储单个时间点下的4个通道温度值
' ============================================================
Public Structure TemperaturePoint
    ' ★ 字段（存储实际数据）
    Public dtmTime As DateTime          ' 时间戳
    Public dblCH01 As Double            ' 通道1温度
    Public dblCH02 As Double            ' 通道2温度
    Public dblCH03 As Double            ' 通道3温度
    Public dblCH04 As Double            ' 通道4温度

    ' ★ 构造函数（方便快速创建实例）
    Public Sub New(time As DateTime, ch01 As Double, ch02 As Double, ch03 As Double, ch04 As Double)
        dtmTime = time
        dblCH01 = ch01
        dblCH02 = ch02
        dblCH03 = ch03
        dblCH04 = ch04
    End Sub

    ' ★ 只读属性：计算平均温度
    Public ReadOnly Property dblAverageTemp() As Double
        Get
            Return (dblCH01 + dblCH02 + dblCH03 + dblCH04) / 4.0
        End Get
    End Property

    ' ★ 方法：判断该点是否所有通道都超过某个阈值
    Public Function IsAllChannelsAbove(threshold As Double) As Boolean
        Return dblCH01 >= threshold AndAlso
               dblCH02 >= threshold AndAlso
               dblCH03 >= threshold AndAlso
               dblCH04 >= threshold
    End Function

    ' ★ 重写 ToString 方法，方便调试
    Public Overrides Function ToString() As String
        Return dtmTime.ToString("yyyy-MM-dd HH:mm:ss") &
               " | CH01:" & dblCH01.ToString("F1") &
               " CH02:" & dblCH02.ToString("F1") &
               " CH03:" & dblCH03.ToString("F1") &
               " CH04:" & dblCH04.ToString("F1") &
               " 平均:" & dblAverageTemp.ToString("F1")
    End Function
End Structure






' ★ 定义枚举（放在 Module 外面）
Public Enum FindType As Byte
    最大值 = 0
    最小值 = 1
    众数 = 2
    平均值 = 3
End Enum

Module M2_调用的任务
    ''' <summary>
    ''' GN005 核心方法：在指定区域中查找目标值并高亮
    ''' </summary>
    ''' <summary>
    ''' GN005 核心方法：在指定区域中查找目标值并高亮
    ''' </summary>
    Public Sub FindPosition(rngSource As Excel.Range, Optional enmFindType As FindType = FindType.平均值)

        If rngSource Is Nothing Then
            MessageBox.Show("单元格区域无效！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If rngSource.Cells.Count = 0 Then
            MessageBox.Show("所选区域为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Dim intMatchCount As Integer = 0
            Dim rngFoundCell As Excel.Range = Nothing
            Dim rngTarget As Excel.Range = Nothing
            Dim strFirstAddress As String = ""
            Dim dblTargetValue As Double = 0

            ' ★★★ 第1步：根据查找类型计算目标值 ★★★
            Select Case enmFindType
                Case FindType.最大值
                    dblTargetValue = xlapp.WorksheetFunction.Max(rngSource)
                Case FindType.最小值
                    dblTargetValue = xlapp.WorksheetFunction.Min(rngSource)
                Case FindType.众数
                    dblTargetValue = xlapp.WorksheetFunction.Mode(rngSource)
                Case FindType.平均值
                    dblTargetValue = xlapp.WorksheetFunction.Average(rngSource)
                Case Else
                    MessageBox.Show("无效的查找类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
            End Select

            Dim strFindName As String = enmFindType.ToString()

            ' ★★★ 第2步：只清除与本次查找类型对应的颜色（同类型清除，不同类型保留） ★★★
            Dim colorToClear As Integer = 0
            Select Case enmFindType
                Case FindType.最大值
                    colorToClear = M1_公共变量.COLOR_DARK_YELLOW
                Case FindType.最小值
                    colorToClear = M1_公共变量.COLOR_LIGHT_YELLOW
                Case FindType.众数
                    colorToClear = M1_公共变量.COLOR_LIGHT_GREEN
                Case FindType.平均值
                    colorToClear = M1_公共变量.COLOR_GREEN
            End Select

            For Each cell As Excel.Range In rngSource.Cells
                If cell.Interior.ColorIndex = colorToClear Then
                    cell.Interior.ColorIndex = Excel.XlColorIndex.xlColorIndexNone
                End If
            Next

            ' ★★★ 第3步：选择高亮颜色 ★★★
            Dim intColorIndex As Integer = M1_公共变量.COLOR_LIGHT_YELLOW
            Select Case enmFindType
                Case FindType.最大值
                    intColorIndex = M1_公共变量.COLOR_DARK_YELLOW
                Case FindType.最小值
                    intColorIndex = M1_公共变量.COLOR_LIGHT_YELLOW
                Case FindType.众数
                    intColorIndex = M1_公共变量.COLOR_LIGHT_GREEN
                Case FindType.平均值
                    intColorIndex = M1_公共变量.COLOR_GREEN
            End Select

            ' ★★★ 第4步：查找目标值 ★★★
            rngFoundCell = rngSource.Find(
            What:=dblTargetValue,
            After:=rngSource(rngSource.Cells.Count),
            LookIn:=Excel.XlFindLookIn.xlValues,
            LookAt:=Excel.XlLookAt.xlWhole
        )

            If rngFoundCell Is Nothing Then
                MessageBox.Show("未找到 " & strFindName & "：" & dblTargetValue, "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' ★★★ 第5步：查找所有匹配项 ★★★
            strFirstAddress = rngFoundCell.Address
            rngTarget = rngFoundCell
            intMatchCount = 1

            Do
                rngFoundCell = rngSource.Find(
                What:=dblTargetValue,
                After:=rngFoundCell,
                LookIn:=Excel.XlFindLookIn.xlValues,
                LookAt:=Excel.XlLookAt.xlWhole
            )

                If rngFoundCell Is Nothing Then Exit Do
                If rngFoundCell.Address = strFirstAddress Then Exit Do

                rngTarget = xlapp.Union(rngTarget, rngFoundCell)
                intMatchCount += 1
            Loop

            ' ★★★ 第6步：高亮所有匹配的单元格 ★★★
            rngTarget.Interior.ColorIndex = intColorIndex

            ' ★★★ 第7步：更新私有字段 ★★★
            fndLastFindType = enmFindType
            intLastMatchCount = intMatchCount
            dblLastTargetValue = dblTargetValue

            ' ★★★ 第8步：状态栏显示结果 ★★★
            xlapp.StatusBar = "找到 " & intMatchCount & " 个 " & strFindName &
                          "：" & dblTargetValue & "（已用颜色标记）"

        Catch ex As Exception
            MessageBox.Show("计算时发生错误：" & ex.Message & vbCrLf &
                        "请确认所选区域包含有效的数值数据！", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '' ============================================================
    '' GN005：定位指定区域中的最大值、最小值、众数或平均值所在的单元格
    '' ============================================================
    'Public Sub FindPosition(rngSource As Excel.Range, Optional enmFindType As FindType = FindType.平均值)

    '    If rngSource Is Nothing Then
    '        MessageBox.Show("单元格区域无效！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub
    '    End If

    '    If rngSource.Cells.Count = 0 Then
    '        MessageBox.Show("所选区域为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub
    '    End If

    '    Try
    '        Dim intMatchCount As Integer = 0
    '        Dim rngFoundCell As Excel.Range = Nothing
    '        Dim rngTarget As Excel.Range = Nothing
    '        Dim strFirstAddress As String = ""
    '        Dim dblTargetValue As Double = 0

    '        Select Case enmFindType
    '            Case FindType.最大值
    '                dblTargetValue = xlapp.WorksheetFunction.Max(rngSource)
    '            Case FindType.最小值
    '                dblTargetValue = xlapp.WorksheetFunction.Min(rngSource)
    '            Case FindType.众数
    '                dblTargetValue = xlapp.WorksheetFunction.Mode(rngSource)
    '            Case FindType.平均值
    '                dblTargetValue = xlapp.WorksheetFunction.Average(rngSource)
    '            Case Else
    '                MessageBox.Show("无效的查找类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '        End Select

    '        Dim strFindName As String = enmFindType.ToString()

    '        ' ★★★ 根据查找类型选择高亮颜色 ★★★
    '        Dim intColorIndex As Integer
    '        Select Case enmFindType
    '            Case FindType.最大值
    '                intColorIndex = COLOR_DARK_YELLOW
    '            Case FindType.最小值
    '                intColorIndex = COLOR_LIGHT_YELLOW
    '            Case FindType.众数
    '                intColorIndex = COLOR_LIGHT_GREEN
    '            Case FindType.平均值
    '                intColorIndex = COLOR_GREEN
    '        End Select


    '        ' 查找目标值
    '        rngFoundCell = rngSource.Find(
    '            What:=dblTargetValue,
    '            After:=rngSource(rngSource.Cells.Count),
    '            LookIn:=Excel.XlFindLookIn.xlValues,
    '            LookAt:=Excel.XlLookAt.xlWhole,
    '            SearchOrder:=Excel.XlSearchOrder.xlByRows,
    '            SearchDirection:=Excel.XlSearchDirection.xlNext,
    '            MatchCase:=False
    '        )

    '        If rngFoundCell Is Nothing Then
    '            MessageBox.Show("未找到 " & strFindName & "：" & dblTargetValue, "提示",
    '                            MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If

    '        strFirstAddress = rngFoundCell.Address
    '        rngTarget = rngFoundCell
    '        intMatchCount = 1

    '        Do
    '            rngFoundCell = rngSource.Find(
    '                What:=dblTargetValue,
    '                After:=rngFoundCell,
    '                LookIn:=Excel.XlFindLookIn.xlValues,
    '                LookAt:=Excel.XlLookAt.xlWhole,
    '                SearchOrder:=Excel.XlSearchOrder.xlByRows,
    '                SearchDirection:=Excel.XlSearchDirection.xlNext,
    '                MatchCase:=False
    '            )

    '            If rngFoundCell Is Nothing Then Exit Do
    '            If rngFoundCell.Address = strFirstAddress Then Exit Do

    '            rngTarget = xlapp.Union(rngTarget, rngFoundCell)
    '            intMatchCount += 1
    '        Loop

    '        ' ★★★ 高亮（不删除旧高亮，不同颜色叠加） ★★★
    '        rngTarget.Interior.ColorIndex = intColorIndex

    '        ' 更新私有字段
    '        fndLastFindType = enmFindType
    '        intLastMatchCount = intMatchCount
    '        dblLastTargetValue = dblTargetValue

    '        ' 状态栏显示结果
    '        xlapp.StatusBar = "找到 " & intMatchCount & " 个 " & strFindName &
    '                          "：" & dblTargetValue & "（已用颜色标记）"

    '    Catch ex As Exception
    '        MessageBox.Show("计算时发生错误：" & ex.Message & vbCrLf &
    '                        "请确认所选区域包含有效的数值数据！", "错误",
    '                        MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

#Region "检查插件是否注册过"
    '20190803 注册码检测是否通过
    '如果之前已经成功注册插件，将不会运行本程序
    Sub jiance()
        Dim cpuSet As Object, a As String = "", c As String = ""
        Dim cpu As Object， i As Byte

        'Cpu对象的集合赋值给变量cpuSet.
        cpuSet = GetObject("winmgmts:{impersonationLevel=impersonate}").InstancesOf("Win32_Processor")
        For Each cpu In cpuSet  '遍历本机cpu对象。
            a = (cpu.ProcessorId)   'cup序列号赋值给a
        Next

        For i = 1 To Len(a)  '在CpuID的长度上循环.
            c = c & Asc(Mid(a, i, 1))   '生成注册码.
        Next


        '弹出对话框
        a = xlapp.InputBox("如您未获得注册码,联系作者QQ:88451376获取", "请输入注册码")  '要求输入注册码，并重新赋值给a
        If a = c Then    '如果输入的注册码匹配本机的cpu转换的序列号

            '向主目录VB and VBA Program的文件夹的指定项目保存键值1，表示成功注册
            '提示成功注册。
            SaveSetting("MyApp1", "Startup", "b", "1")
            MsgBox("恭喜你已成功注册本插件")
        Else

            '非正确注册的人员，计算机提示错误，并锁定按钮，或者菜单项目。
            MsgBox("注册码错误")
            Globals.Ribbons.Ribbon1.Button1.Enabled = False
            Globals.Ribbons.Ribbon1.Menu1.Enabled = False
            Globals.Ribbons.Ribbon1.Menu2.Enabled = False
            Globals.Ribbons.Ribbon1.Menu3.Enabled = False
            Globals.Ribbons.Ribbon1.Menu4.Enabled = False
            Globals.Ribbons.Ribbon1.Menu5.Enabled = False
            Globals.Ribbons.Ribbon1.Menu6.Enabled = False
            Globals.Ribbons.Ribbon1.Menu7.Enabled = False
            Globals.Ribbons.Ribbon1.Menu8.Enabled = False
            Globals.Ribbons.Ribbon1.Menu9.Enabled = False
            Globals.Ribbons.Ribbon1.Menu10.Enabled = False
            Globals.Ribbons.Ribbon1.Menu11.Enabled = False
            Globals.Ribbons.Ribbon1.Menu12.Enabled = False
            Globals.Ribbons.Ribbon1.Menu13.Enabled = False
            Globals.Ribbons.Ribbon1.Menu14.Enabled = False
            Globals.Ribbons.Ribbon1.Menu15.Enabled = False
            Globals.Ribbons.Ribbon1.Menu16.Enabled = False
            Globals.Ribbons.Ribbon1.Menu17.Enabled = False
            Globals.Ribbons.Ribbon1.Menu18.Enabled = False
            Globals.Ribbons.Ribbon1.Menu19.Enabled = False
            Globals.Ribbons.Ribbon1.Menu20.Enabled = False
        End If
    End Sub
#End Region

#Region "检查是否能连接外网"
    '创建函数,得到布尔值结果;链接百度网,判定是否可以联网
    Function getweb() As Boolean
        Try
            System.Net.WebRequest.Create("http://www.baidu.com").GetResponse()
            Return True  '返回值 True
        Catch ex As Exception  '上不了网,返回值 False
            Return False
        End Try
    End Function
#End Region

    ' GN005：定位指定区域中的最大值、最小值、众数或平均值所在的单元格
    Private fndLastFindType As FindType
    Private intLastMatchCount As Integer
    Private dblLastTargetValue As Double

    Public ReadOnly Property strLastFindInfo() As String
        Get
            If intLastMatchCount = 0 AndAlso dblLastTargetValue = 0 Then
                Return "尚未执行任何查找操作"
            End If
            Dim strFindName As String = [Enum].GetName(GetType(FindType), fndLastFindType)
            Return "最近查找：" & strFindName &
                   "，找到 " & intLastMatchCount & " 个" &
                   "，目标值：" & dblLastTargetValue
        End Get
    End Property



    '2019.05.23 检查 GN011_获取文件信息
    '查找文件的子过程，有一个必选参数
    Public Sub 查找(路径 As String)
        'Dim strDirs() As String  '或者声明下面注释语句 Dim strDirs As object
        Dim strDirs As Object
        Dim intCounter As Long, strFileName As String, strSubFileName As String, j As Integer

        If Right(路径, 1) <> "\" Then 路径 = 路径 & "\"    '如果路径最后一位非\则追加一个\
        strFileName = Dir(路径 & "*.*", vbDirectory)    '获取文件和文件夹的名称

        Do While Len(strFileName) <> 0                   '只要文件名称长度<> 0就一直循环下去
            If Left(strFileName, 1) <> "." Then          '如果左边第一字符不是"."(目的是排除父级目录)
                strSubFileName = 路径 & strFileName      '获取子文件名称
                If (GetAttr(strSubFileName) And vbDirectory) = vbDirectory Then  '如果文件是文件夹
                    intCounter = intCounter + 1                                  '累加计数器，此计数器代表文件夹数量
                    ReDim Preserve strDirs(0 To intCounter - 1)                  '重新声明数组的储存空间
                    strDirs(intCounter - 1) = strSubFileName                     '将子文件夹名称写入数组strDirs中
                Else  '如果是文件
                    i = i + 1  '累加计数器，此计数器代表文件数量
                    ReDim Preserve arr(0 To 2, 0 To i - 1)    '重新声明数组的储存空间
                    arr(0, i - 1) = 路径                                       '将文件路径写入数组的第1行第i列
                    arr(1, i - 1) = strFileName                                '将文件名称写入数组的第2行第i列
                    arr(2, i - 1) = FileLen(路径 & strFileName) / 1024 / 1024  '将文件大小写入数组的第3行第i列
                End If
            End If
            strFileName = Dir()  '查找下一个文件或者文件夹
        Loop

        For j = 1 To intCounter
            '遍历数组Dirs，对其中的所有子文件夹进行查找,把子文件夹当成主文件夹继续查找
            '调用自身，对子文件夹再次查找，如果子文件夹中有文件则将文件路径、名称和大小导入到数组arr中，
            '如果是文件夹，则将文件夹名称导入数组Dirs中，参与下一次查找。
            查找(strDirs(j - 1))
        Next j
    End Sub

    'GN011_获取文件信息_180205
    Public Sub 文件链接()
        '遍历目录中的所有文件(首行是标题，因此从2开始)
        Dim j As Integer
        For j = xlapp.ActiveCell.Row + 1 To xlapp.Cells(xlapp.Rows.Count, xlapp.ActiveCell.Column).End(-4162).Row
            '使用Hyperlinks.Add方法为文件目录创建超级链接
            xlapp.ActiveSheet.Hyperlinks.Add(xlapp.Cells(j, xlapp.ActiveCell.Column + 1),
                                             xlapp.Cells(j, xlapp.ActiveCell.Column).value.ToString &
                                             xlapp.Cells(j, xlapp.ActiveCell.Column + 1).value.ToString)
        Next j
    End Sub

#Region "文件移动集合调用"
    'GN013_文件移动集合-180206
    Sub 查找子文件夹(路径 As String)
        On Error Resume Next
        Dim objFso As Object, objFp As Object, objFpf As Object, objFd As System.IO.DirectoryInfo, a As Integer
        Dim drectInfo As System.IO.DirectoryInfo                                                          '声明主目录对象
        Dim objDirs() As String, longFileCounter As Long, strDirName As String, strDirName1 As String, j As Integer '声明变量
        objFso = CreateObject("scripting.filesystemobject") '创建一个FSO顶层对象并赋值给变量
        If Right(路径, 1) <> "\" Then 路径 = 路径 & "\"     '如果路径最后一位非\则追加一个\
        strDirName = Dir(路径 & "*.*", 16)                  '获取文件夹的名称.
        Do While Len(strDirName) <> 0                       '只要文件名称长度大于0就一直循环下去
            strDirName1 = 路径 & strDirName      '获取子文件（夹）名称
            If (GetAttr(strDirName1) And 16) = 16 Then      '如果是文件夹
                longFileCounter = longFileCounter + 1             '累加计数器，此计数器代表文件夹数量
                ReDim Preserve objDirs(0 To longFileCounter - 1)  '重新声明数组的储存空间
                objDirs(longFileCounter - 1) = strDirName1        '将子文件夹名称写入数组objDirs中
            End If
            strDirName = Dir()    '接上一个DIR函数查找下一个文件夹
        Loop
        For j = 1 To longFileCounter  '遍历数组Dirs，对其中的所有子文件夹进行查找
            drectInfo = New IO.DirectoryInfo(objDirs(j - 1))    '创建子文件夹作为父级文件夹的实例
            '在主目录中循环子文件夹
            For Each objFd In drectInfo.GetDirectories
                If objFd.FullName Like "*CopyOption" Then '当遍历数组二级文件夹后期创建的文件夹时，直接排除，不需要将里面的子文件夹里的文件在移动到创建的文件夹里。
                    Exit For
                Else
                    '只操作文件，不对文件夹处理...
                    strDirName = Dir(objFd.FullName & "\*.*")                 '获取文件的名称(可能存在文件夹)...
                    Do While Len(strDirName) <> 0                             '只要文件名称长度大于0就一直循环下去
                        If (GetAttr(strDirName) And 16) <> 16 Then            '如果不是文件夹而是文件的话
                            Kill(strFielPath & "CopyOption" & strDirName)     '删除指定文件夹内的对应的文件,防止执行错误
                        End If      '结束语句
                        strDirName = Dir()  '查找下一个文件（夹）.
                    Loop
                    objFso.movefile(objFd.FullName & "\*.*", strFielPath & "\CopyOption")  '移动子文件夹内文件到指定文件夹内
                End If
            Next                         '遍历下一个子文件夹
            '递归执行程序本身,相当于新的1级文件夹框架内运行，继续获取子文件夹(此时子文件夹当作主文件夹)内的子文件夹，从数组第一个文件夹遍历，如果子文件内没有文件夹.
            '代码执行到For j,因为文件夹数组将是空值，无需执行文件转移代码，因为上一级的文件都清空了直接跳转到Next j，遍历文件夹数组，即执行下一个文件夹.
            查找子文件夹(objDirs(j - 1))
        Next j
    End Sub
#End Region
#Region "'GN020_批量加密表格"
    REM 其中数组参数ShtNam由0到254个参数组成，必须带括号(总数量255个，减去第一参数PassWord,因此ShtName包含0-254个参数)
    Public Sub 加密工作表(PassWord As String, ParamArray ShtName() As Object)
        On Error Resume Next  '当执行代码出错时继续执行下一句
        '声明三个变量，变量Item用于遍历数组，因此只能用变体型。变量Sht代表工作表对象,Errname代表错误的工作表名称
        Dim Item1 As String, sht As Excel.Worksheet, ErrName As String = ""
        If UBound(ShtName) >= 0 Then  '如果数组参数ShtName有值(只要有赋值其上标就会大于等于0，未赋值时其上标等于-1)
            For Each Item1 In ShtName  '遍历数组Shtname的 <<第一个参数,也是数组>>中的  所有元素
                sht = xlapp.Sheets(Item1)   '以Item为表名引用对应的表，然后将它赋值给变量Sht
                If Err.Number <> 0 Or sht.ProtectContents = True Then '如果赋值出错
                    ErrName = ErrName & Chr(10) & Item1  '那么记录下该参数的值
                    Err.Clear()  '清除错误，避免影响下一轮判断
                Else  '否则
                    sht.Protect(PassWord)  '以PassWord的值为密码保护sht所代表的表
                End If
            Next
            If Len(ErrName) > 0 Then MsgBox("部分工作表加密失败,之前的工作表可能含有密码:" & ErrName)  '如果Errname的长度大于0，那么提示用户
        End If
    End Sub


#End Region




    'Public Function RunCMD(ByVal Commands As String, Optional ByVal TimeOutSencond As Integer = 3 * 60) As String
    '    Dim myProcess As New Process()
    '    Dim myProcessStartInfo As New ProcessStartInfo("cmd.exe")
    '    myProcessStartInfo.UseShellExecute = False
    '    myProcessStartInfo.RedirectStandardOutput = True
    '    myProcessStartInfo.CreateNoWindow = True
    '    myProcessStartInfo.Arguments = "/c " & Commands
    '    myProcess.StartInfo = myProcessStartInfo
    '    myProcess.Start()
    '    myProcess.WaitForExit(TimeOutSencond * 1000)
    '    Dim myStreamReader As IO.StreamReader = myProcess.StandardOutput
    '    Dim myString As String = myStreamReader.ReadToEnd()
    '    myProcess.Close()
    '    Return myString
    'End Function

    '备份数据的过程，其中参数sht代表要备份的工作表对象，rngAddress则代表要备份的区域地址


    REM 功    能: 宝3-9.2.1-P179-同一个工作簿的工作表数据合并到一张表中 关键词：worksheet.delete
    Sub 合并到总表()
        Dim sht As Excel.Worksheet, i As Byte   '声明变量
        xlapp.ScreenUpdating = False  '关闭屏幕刷新
        On Error Resume Next  '当程序出错时继续执行下一句
        xlapp.DisplayAlerts = False  '关闭提示(删除工作表时会有提示)
        xlapp.Worksheets("发注编号信息模板").Delete  '删除“总表”(假设有总表的话)
        xlapp.Worksheets.Add.Name = "发注编号信息模板"  '新建一人工作表，然后命名为“总表”
        For Each sht In xlapp.Worksheets  '遍历活动工作簿中的所有工作表
            If sht.Name <> "发注编号信息模板" Then  '如果sht的名字不等于“总表”
                '如果工作表A列有值（忽略空表或者A列无值的工作表）
                If xlapp.WorksheetFunction.CountA(sht.Range("A:A")) > 0 Then
                    i = i + 1  '累加变量
                    If i = 1 Then  '如果变量i的值等于1
                        sht.UsedRange.Copy()  '复制sht工作表的已用区域
                        xlapp.Range("a1").PasteSpecial(13)  '粘贴到活动工作表的A1单元格
                        xlapp.Range("a1").PasteSpecial(-4163)  '再次粘贴，只粘贴值(防止合并前的公式的值不一致)
                        xlapp.Range("a1").PasteSpecial(8)  '再次粘贴，只粘贴列宽
                    Else
                        sht.UsedRange.Offset(1, 0).Copy()  '复制sht工作表的已用区域(排除标题行)
                        With xlapp.Cells(xlapp.Rows.Count, 1).End(-4162).Offset(1, 0)  '引用A列最后一个非空行的下一行
                            .PasteSpecial(13)  '粘贴
                            .PasteSpecial(-4163)  '再次粘贴，只粘贴列宽
                            .PasteSpecial(8)  '再次粘贴，只粘贴列宽
                        End With
                    End If
                End If
            End If
        Next sht
        xlapp.ScreenUpdating = True  '恢复屏幕刷新
        xlapp.ActiveWorkbook.Save()
    End Sub


#Region "备份，撤销数据"

    ''复制活动工作表已用区域到插件工作簿中
    ''注意这里的方法,带有2个参数,调用的时候,要传递参数sht,rngAddress
    'Sub 备份(sht As Excel.Worksheet, rngAddress As String)

    '    xlapp.Workbooks("FV.xlam").Sheets(1).Cells.Clear '删除ThisWorkbook第一个工作表的所有单元格的值
    '    sht.Range(rngAddress).Copy(xlapp.Workbooks("FV.xlam").Sheets(1).Range(rngAddress)) '将活动工作表sht的已用区域rngAddress,复制到加载项工作簿FV.xlam
    'End Sub


    ''还原备份的数据
    'Sub 撤消()
    '    '将ThisWorkbook的第一个工作表的TargetRng区域的值恢复到Targetsht表中
    '    '要注意两个工作表的区域是对应的，都采用TargetRng
    '    On Error Resume Next
    '    xlapp.Workbooks("FV.xlam").Sheets(1).Range(TargetRng).Copy(Targetsht.Range(TargetRng))
    'End Sub



    ''' <summary>
    ''' GN006 - 备份当前活动工作表的指定区域到 FV.xlam
    ''' </summary>
    Public Sub BackupActiveSheet(Optional rngAddress As String = "")
        Try
            Dim xlamWb As Excel.Workbook = GetOrOpenXlam()
            If xlamWb Is Nothing Then Exit Sub

            Dim wsSource As Excel.Worksheet = xlapp.ActiveSheet
            Dim wsTarget As Excel.Worksheet = xlamWb.Sheets(1)

            Dim backupRange As Excel.Range
            If String.IsNullOrEmpty(rngAddress) Then
                backupRange = wsSource.UsedRange
            Else
                Try
                    backupRange = wsSource.Range(rngAddress)
                Catch
                    MessageBox.Show("指定的区域地址无效！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If

            wsTarget.Cells.Clear()
            backupRange.Copy(wsTarget.Range(backupRange.Address))

            ThisAddIn.objTargetSheet = wsSource
            ThisAddIn.strTargetRng = backupRange.Address

            ' ★★★ 状态栏提示（3秒后自动清除） ★★★
            ShowStatusBarMessage("备份完成！区域：" & backupRange.Address)

        Catch ex As Exception
            MessageBox.Show("备份失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' GN006 - 从 FV.xlam 恢复数据到当前活动工作表
    ''' </summary>
    Public Sub RestoreFromBackup()
        Try

            If String.IsNullOrEmpty(ThisAddIn.strTargetRng) Then
                ShowStatusBarMessage("没有可恢复的备份数据！")
                Exit Sub
            End If

            Dim xlamWb As Excel.Workbook = GetOrOpenXlam()
            If xlamWb Is Nothing Then Exit Sub

            Dim wsBackup As Excel.Worksheet = xlamWb.Sheets(1)
            Dim wsTarget As Excel.Worksheet = xlapp.ActiveSheet

            Dim backupRange As Excel.Range = Nothing
            Try
                backupRange = wsBackup.Range(ThisAddIn.strTargetRng)
            Catch
                MessageBox.Show("备份数据已损坏或区域无效！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            Dim targetRange As Excel.Range = Nothing
            Try
                targetRange = wsTarget.Range(ThisAddIn.strTargetRng)
            Catch
                MessageBox.Show("目标区域无效！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            targetRange.Clear()
            backupRange.Copy()
            targetRange.PasteSpecial(Excel.XlPasteType.xlPasteAll)

            xlapp.CutCopyMode = False

            ShowStatusBarMessage("数据恢复成功！")

        Catch ex As Exception
            MessageBox.Show("恢复失败：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ' ★★★ 在需要 XLAM 的公共方法中，先按需加载 ★★★
    Private Function GetOrOpenXlam() As Excel.Workbook
        ' ★★★ 先确保 XLAM 已加载 ★★★
        ThisAddIn.LoadXlamOnDemand()

        Try
            Return xlapp.Workbooks("FV.xlam")
        Catch
            Dim xlamPath As String = "C:\Program Files\FV\FV.xlam"
            If System.IO.File.Exists(xlamPath) Then
                Return xlapp.Workbooks.Open(xlamPath)
            Else
                Return Nothing
            End If
        End Try
    End Function


    ''' <summary>
    ''' 在状态栏显示消息，3秒后自动清除
    ''' </summary>
    Private Sub ShowStatusBarMessage(message As String)
        xlapp.StatusBar = message
        Dim timer As New System.Windows.Forms.Timer()
        timer.Interval = 3000
        AddHandler timer.Tick, Sub(s, e)
                                   Try
                                       xlapp.StatusBar = False
                                   Catch
                                   End Try
                                   timer.Stop()
                                   timer.Dispose()
                               End Sub
        timer.Start()
    End Sub


#End Region




    'Public Sub PauseWait(ByVal HowLong As Long)
    '    Dim tick As Long
    '    tick = My.Computer.Clock.TickCount
    '    Do
    '        xlapp.DoEvents()
    '    Loop Until tick + HowLong < My.Computer.Clock.TickCount
    'End Sub



End Module
