'VSTO开发记录模板      按指定格式回答开发的项目 我在电脑上A修改了1。。。。
'FV_VSTO_开发记录_v2.0_20260810  定期提醒deepseek已开发的所有内容

' ============================================================
' FV 插件 - 功能清单（按 GN 编号索引）
' ============================================================
' GN001 | 智能调整行高 | 一键调整当前工作表已用区域的行高，支持混合换行
' GN002 | 提取指定列数据 | 从当前工作表提取 B、P、A、K 列，生成新工作表
' GN003 | 名称管理 | 辅助工具，查看/筛选/删除自定义名称
' GN004 | 热处理数据分析 | 识别4个通道的加热/保温时间，自动分组，生成图表
' GN005 | 下拉框定位 | 选中区域后，下拉选择最大值/最小值/众数/平均值，自动定位并高亮（使用枚举优化）
' ============================================================


Imports Microsoft.Office.Tools.Ribbon   '命名空间:office功能区,方便调用相关功能 
Imports System.Windows.Forms  '命名空间:窗体,方便调用它的功能，而无需完整书写完整的父对象 
Imports System.Drawing  '命名空间：图形,方便调用它的功能，而无需完整书写完整的父对象 
Imports Microsoft.Office.Interop.Excel
Imports System.Diagnostics



#Region "常用功能"
''____________________备份数据、记录区域 ___________________________
'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
'Call 备份(Targetsht, TargetRng)
'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True
''____________________备份数据、记录区域___________________________

#End Region


Public Class Ribbon1
    ' ★★★ Ribbon1.vb 顶部添加一个公共方法 ★★★
    Private Sub 确保XLAM已加载()
        ThisAddIn.LoadXlamOnDemand()
    End Sub



#Region "创建功能区加载时发生的事件-250810"
    Private Sub Ribbon1_Load(sender As Object, e As RibbonUIEventArgs) Handles Me.Load
        'MsgBox("Hi,the addnis is sucessfully running") '测试是否成功运行语句
        '"检查是否注册过插件与是否有最新版本"
        On Error Resume Next '出错继续执行下面语句.
        Dim j As Byte, k As Integer


        '打开插件工作簿,没有这句将连锁触发已加载的FV.xlam事件
        'xlapp.EnableEvents = False '禁止响应事件,
        'xlapp.Workbooks.Open("C:\Program Files\FV\FV.xlam") '打开插件工作簿
        'xlapp.ActiveWorkbook.Sheets(1).select '选择第一个表
        'xlapp.Workbooks("FV.xlam").IsAddin = True  '插件工作簿设定不可编辑模式
        'xlapp.EnableEvents = True '恢复响应事件


        '查看注册表写入的值是否为1（1代表成功过注册了该插件）
        '如果不是设定的1值（只要运行regedit→HKEY_CURRENT_USER\Software\VB and VBA Program Settings\
        '写入1也可成功注册该插件），那么运行程序.
        j = GetSetting("MyApp1", "Startup", "b") '获取注册表参数b数值
        If j <> 1 Then '不等于1，表示之前未正确注册过
            jiance() '调用任务要求用户输入注册码“注册”
        End If

    End Sub
#End Region


#Region "GN006_同列相同数据合并-250813"
    '将同一列连续的相同数据合并。
    Private Sub btnMergeRange_Click(sender As Object, e As RibbonControlEventArgs) Handles btnMergeRange.Click
        确保XLAM已加载()
        '.....................教程调用代码.................
        If My.Computer.Keyboard.CtrlKeyDown Then        '按了Ctrl 键后,单击鼠标触发教程调用代码...
            If My.Computer.Network.IsAvailable Then     '如果网络可用
                If getweb() Then                  '调用方法检查外网是否可用
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/SameDataMerge")     '如果可用外网，使用域名打开网页
                    Exit Sub '退出程序
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/SameDataMerge") '不可用外网，使用局域网IP 打开网页
                    Exit Sub
                End If
            End If
            Exit Sub  '网络不可用，退出程序
        End If
        '.....................教程调用 结束...............


        'On Error GoTo Errline '当程序执行出错时，跳转到标签ErrLine处.


        ' ★★★ 声明变量（符合命名规范） ★★★
        Dim rngTargetRange As Excel.Range
        Dim rngFirstMergeRange As Excel.Range
        Dim rngSelectionArea As Excel.Range
        Dim bytCounter As Integer = 0

        Try
            ' ============================================================
            ' ★★★ 第1步：备份数据（用于撤销） ★★★
            ' ============================================================
            M2_调用的任务.BackupActiveSheet()
            Globals.Ribbons.Ribbon1.btnUndo.Enabled = True

            ' ============================================================
            ' ★★★ 第2步：参数验证 ★★★
            ' ============================================================
            If TypeName(xlapp.Selection) <> "Range" Then
                MessageBox.Show("请选择单元格区域！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If xlapp.Selection.Count = 1 Then
                MessageBox.Show("请选择多个单元格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If xlapp.Selection.Columns.Count > 1 Then
                MessageBox.Show("只支持单列数据操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' ============================================================
            ' ★★★ 第3步：性能优化（关闭刷新、计算、提示） ★★★
            ' ============================================================
            xlapp.ScreenUpdating = False
            xlapp.Calculation = Excel.XlCalculation.xlCalculationManual
            xlapp.DisplayAlerts = False

            ' ============================================================
            ' ★★★ 第4步：获取有效选区 ★★★
            ' ============================================================
            rngSelectionArea = xlapp.Intersect(xlapp.ActiveSheet.UsedRange, xlapp.Selection)

            If rngSelectionArea Is Nothing Then
                MessageBox.Show("所选区域无有效数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            rngFirstMergeRange = rngSelectionArea(1)

            ' ============================================================
            ' ★★★ 第5步：遍历合并（核心逻辑） ★★★
            ' ============================================================
            For Each rngTargetRange In rngSelectionArea.Offset(1, 0).Resize(rngSelectionArea.Count, 1)
                bytCounter += 1

                If bytCounter = rngSelectionArea.Count Then
                    ' 最后一组数据合并
                    xlapp.Range(rngFirstMergeRange, rngTargetRange.Offset(-1, 0)).Merge()
                ElseIf rngTargetRange.Value <> rngTargetRange.Offset(-1, 0).Value Then
                    ' 当前值不等于上一行，合并之前的连续相同单元格
                    xlapp.Range(rngFirstMergeRange, rngTargetRange.Offset(-1, 0)).Merge()
                    rngFirstMergeRange = rngTargetRange
                End If
            Next

            ' 选中原区域第一个单元格
            rngSelectionArea(1).Select()

            MessageBox.Show("合并完成！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("合并时发生错误：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ' ============================================================
            ' ★★★ 第6步：恢复 Excel 设置 ★★★
            ' ============================================================
            xlapp.Calculation = Excel.XlCalculation.xlCalculationAutomatic
            xlapp.DisplayAlerts = True
            xlapp.ScreenUpdating = True
        End Try

    End Sub
#End Region


#Region "A01_调用文件信息窗体-250823"
    '显示文件的基本信息，如发布的实施日期，名称，履历版次...
    Private Sub btnFileInfo_Click(sender As Object, e As RibbonControlEventArgs) Handles btnFileInfo.Click
        Dim f As New A01_文件基本信息   '声明变量，并实例化一个对象
        On Error Resume Next
        'f.ShowDialog()  '模态窗体,不可以编辑除窗体以为的其他位置
        f.Show() '显示窗体,非模态窗体可以编辑窗体以为的其他位置
        btnFileInfo.Enabled = False '禁止按钮再次单击（不再启用）...
    End Sub
#End Region




    Private Sub Button18_Click(sender As Object, e As RibbonControlEventArgs) Handles Button18.Click
        Dim f As New A02_文件信息查询与导出
        'f.ShowDialog()  '模态窗体,不可以编辑
        f.Show() '显示窗体,非模态窗体可以编辑
        Button18.Enabled = False
    End Sub
    Private Sub Button19_Click(sender As Object, e As RibbonControlEventArgs) Handles Button19.Click
        Dim f As New A03_文件创建更改管理
        'f.ShowDialog()  '模态窗体,不可以编辑
        f.Show() '显示窗体,非模态窗体可以编辑
        Button19.Enabled = False
    End Sub










#Region "WN18081503_文件批量重命名"
    Private Sub btnBatchNaming_Click(sender As Object, e As RibbonControlEventArgs) Handles btnBatchNaming.Click
        Dim f As New WN18081503_文件批量重命名  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnBatchNaming.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub
#End Region
#Region "'WN002_随机数范围_随机数范围"
    Private Sub Button25_Click(sender As Object, e As RibbonControlEventArgs) Handles Button25.Click
        Dim f As New WN18081502_随机数产生      '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button25.Enabled = False     '让按钮禁用，防止打开多个窗口
    End Sub
#End Region
#Region "Win 列和区域删图"
    Private Sub btnColumnAndAreaDeletePicture_Click(sender As Object, e As RibbonControlEventArgs) Handles btnColumnAndAreaDeletePicture.Click

        '.....................教程调用 起始
        If My.Computer.Keyboard.CtrlKeyDown Then        '按了Ctrl 键后,单击鼠标触发教程调用代码...
            If My.Computer.Network.IsAvailable Then     '如果网络外网可用
                If getweb() Then                        '
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/DeletePictures")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/DeletePictures") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If
        '.....................教程调用 结束

        Dim f As New WN18081501_列和区域删除图  '实例化一个类的对象.
        f.Show() '显示实例窗体
        btnColumnAndAreaDeletePicture.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub
#End Region

#Region "指定区域放置图片"
    Private Sub btnAreaLocalPicture_Click(sender As Object, e As RibbonControlEventArgs) Handles btnAreaLocalPicture.Click
        '判定是否是否按了Ctrl + 鼠标左键,再判定是否可以上外网...
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/ForFv_PositionPiture")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/ForFv_PositionPiture") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If

        Dim f As New WIN210913_图片放置定位  '实例化一个类的对象.
        f.Show() '显示实例窗体
        btnAreaLocalPicture.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub
#End Region

#Region "多图排列2"
    Private Sub Button3_Click(sender As Object, e As RibbonControlEventArgs) Handles btnSort.Click
        '判定是否是否按了Ctrl + 鼠标左键,再判定是否可以上外网...
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/sortPittures")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/sortPittures") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If

        Dim f As New WIN231210_多图排放  '实例化一个类的对象.
        f.Show() '显示实例窗体
        btnSort.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

#Region "图统一大小"
    Private Sub btnControlSize_Click(sender As Object, e As RibbonControlEventArgs) Handles btnControlSize.Click

        '判定是否是否按了Ctrl + 鼠标左键,再判定是否可以上外网...
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/sameSize")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/sameSize") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If

        Dim f As New WIN231222_图片尺寸统一
        '实例化一个类的对象.
        f.Show() '显示实例窗体
        btnControlSize.Enabled = False '让按钮禁用，防止打开多个窗口

    End Sub
#End Region




#Region "相同数据取消合并2023.09.14"
    '选择列的合并单元格取消合并，并填充相同数据。
    Private Sub btnUnMergeRange_Click(sender As Object, e As RibbonControlEventArgs) Handles btnUnMergeRange.Click

        '.....................教程调用 起始
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/UnMerge")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/UnMerge")          '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If
        '.....................教程调用 结束


        On Error Resume Next              '出错继续执行下一行代码

        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        'Call 备份(Targetsht, TargetRng)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True
        ''____________________备份数据、记录区域___________________________

        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True


        Dim rngTargetRange As Excel.Range '声明单元格对象的变量
        xlapp.ScreenUpdating = False     '禁止屏幕刷新
        xlapp.Calculation = -4135        '将计算方式改为手动，提升工作效率
        'xlapp.DisplayAlerts = False      '禁止提示,这里没用,只是为了保持模板结构,因为取消合并单元格并不会有提示.
        '交集赋值给变量

        rngTargetRange = xlapp.Intersect(xlapp.Selection, xlapp.ActiveSheet.UsedRange)    '将选区与活动工作表的已用区域的交集赋值给变量。
        rngTargetRange.UnMerge()  '取消合并
        rngTargetRange.SpecialCells(4).FormulaR1C1 = "=R[-1]C" '对空白单元格输入公式,4是空白单元格枚举值,这里FormulaR1C1方法批量写公式
        rngTargetRange.Value = rngTargetRange.Value            '将选区的公式转换成值
        xlapp.Calculation = -4105      '将计算方式还原为自动
        'xlapp.DisplayAlerts = True     '还原提示
        xlapp.ScreenUpdating = True    '还原屏幕刷新

        ''.......................................................................................
        'xlapp.OnUndo("撤消[取消合并]", "撤消") '这里代码激活的是FV.xlam加载项的撤销方法

        ''.......................................................................................

    End Sub
#End Region
#Region "GN003_身份证提取信息"
    Private Sub btnExtractId_Click(sender As Object, e As RibbonControlEventArgs) Handles btnExtractId.Click
        On Error Resume Next  '出错继续执行下一行代码
        '声明变量,这里arrSaveRange不能声明String类型,因为下面代码将批量用单元格Value属性(可能是:非文本)批量给数组赋值,VSTO不能自己判定成全是String类型.
        '声明动态二维数组变量需要以"(,)"形式,一维数组为"()"形式.
        Dim rngSelectRange As Excel.Range, intStep As Integer, strExtractString As String, arrSaveRange(,) As Object, arrExtactString(,) As String
        Dim rngPositionRange As Excel.Range
        '加入防错判定.
        If TypeName(xlapp.Selection) <> "Range" Then MsgBox("请选择存放身份证号码的区域") : Exit Sub 'typename函数判断选中的是否为单元格.
        rngSelectRange = xlapp.Intersect(xlapp.Selection, xlapp.ActiveSheet.UsedRange)  '利用intersect方法将选区与已用区域产生交集,防止选中过多空白区域
        If rngSelectRange.Columns.Count > 1 Then MsgBox("只能选择单列", vbOKOnly + vbInformation, "出错提示") : Exit Sub '如果选中了多列,则退出过程
        If rngSelectRange(1).value = "" Then MsgBox("请选择身份证号码存放区域", vbOKOnly + vbInformation, "出错提示") : Exit Sub '第1个单元格是空值,退出过程
        '单元格值批量赋值给对象数组变量.模板:单元格值批量赋值给变体对象中
        arrSaveRange = rngSelectRange.Value
        ReDim arrExtactString(0 To UBound(arrSaveRange) - 1, 0 To 2) '重新声明维数,Vsto声明的数组下标必须从0开始,除了上面的工作表区域赋值.

        '数组比较特殊,跟VBA一样,数组下标是1,所以1到数组上标上遍历,绝大数数组下标是从0开始.
        For intStep = 1 To UBound(arrSaveRange)
            If Len(arrSaveRange(intStep, 1)) = 18 Then '如果字符长度为18执行以下语句
                arrExtactString(intStep - 1, 0) = IIf((Mid(arrSaveRange(intStep, 1), 15, 3) Mod 2), "男", "女") '利用身份证第15位数字起往后数3位除以2,除不尽的为男,除的尽的为女
                strExtractString = Mid(arrSaveRange(intStep, 1), 7, 4) & "-" & Mid(arrSaveRange(intStep, 1), 11, 2) & "-" & Mid(arrSaveRange(intStep, 1), 13, 2)  '提取出生年月日赋值给变量myStr
                '变量再逐一赋值给第i行,第2列的元素
                arrExtactString(intStep - 1, 1) = strExtractString
                '利用Evaluate函数将文本形式的公式转化成工作表公式,并将结果值逐一赋值给第i行3列的元素
                '下面注释是VBA语法,可以直接DateDif函数计算相减年,但是VSTO日期不能直接减,需要用函数,这里Substract是当前日期-指定日期,TotalDays属性是转换成总天数.int函数/365获取整年.
                'arr2(i - 1, 2) = xlapp.Evaluate("DATEDIF(" & DateSerial(Split(Mystr, "-")(0), Split(Mystr, "-")(1), Split(Mystr, "-")(2)) & ", NOW()," & """Y""" & ")")
                arrExtactString(intStep - 1, 2) = Int(Now.Subtract(DateSerial(Split(strExtractString, "-")(0), Split(strExtractString, "-")(1), Split(strExtractString, "-")(2))).TotalDays _
                    / 365)
            End If
        Next intStep

        '选定放置单元格,使其根据数组上标,自动扩展相应的要写入的区域..
        rngPositionRange = xlapp.InputBox("请直接选择放置区域", , , , , , , 8)    '选择放置区域,单元格区域赋值给变量单元格
        rngPositionRange.Offset(-1, 0).Resize(1, 3).Value = {"性别", "出生日期", "年龄"}     '在指定单元格写入一维数组值
        rngPositionRange.Resize(UBound(arrSaveRange), 3).NumberFormatLocal = "@"   '设置文本格式
        xlapp.ScreenUpdating = False     '禁止屏幕刷新
        rngPositionRange.Resize(UBound(arrSaveRange), 3).Value = arrExtactString  '批量写入单元格中
        rngPositionRange.CurrentRegion.EntireColumn.AutoFit()      '自动调整列宽
        rngPositionRange.CurrentRegion.Borders.LineStyle = 1       '加框线
        rngPositionRange.CurrentRegion.HorizontalAlignment = -4108 '水平中间放置
        rngPositionRange.CurrentRegion.VerticalAlignment = -4108   '垂直中间放置
        xlapp.ScreenUpdating = True     '回复屏幕刷新
    End Sub
#End Region
#Region "GN004_生成工资条"
    Private Sub btnCreateBill_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCreateBill.Click
        '声明变量,工资表标题行一定要在工作表的首行才能成功执行
        Dim intRowsNumb As Integer, intStep As Integer, rngTitle As Excel.Range  '声明一个Integer型的变量，用于取代已用数据区域的行数ActiveSheet.UsedRange.Rows.Count
        xlapp.ScreenUpdating = False  '关闭屏幕刷新，从而加快代码执行速度
        xlapp.ActiveSheet.Copy(, xlapp.Sheets(xlapp.Sheets.Count))  '将活动工作表复制一份,放置最后一个工作表之后.
        '遍历已用区域行,插入2行.
        For intStep = xlapp.ActiveSheet.UsedRange.Rows.Count To 3 Step -1  '使用For Next循环，起始值为已用区域的最后一行，终止值为3.
            xlapp.Cells(intStep, 1).Resize(2, 1).EntireRow.Insert    '插入两行，其中插入的第一行便于裁剪为间隔行，另一行存放标题.
        Next intStep

        '将已用区域行数赋值给变量,复制标题,并将起始粘贴区域对象赋值给变量rngTitle.
        intRowsNumb = xlapp.ActiveSheet.UsedRange.Rows.Count  '将已用数据区域的行数赋值给变量intRowNum
        xlapp.Rows("1:1").Copy     '复制第一行标题
        rngTitle = xlapp.Rows("4:4") '首先将第4行赋值给变量rng,它是第一个需要插入标题的行

        '遍历将张贴标题的起始区域与终点区域(已用区域行数).
        For intStep = 4 To intRowsNumb Step 3      '使用For Next循环，起始值为4，终止值为已用数据区域的最后一行
            rngTitle = xlapp.Union(rngTitle, xlapp.Rows(intStep)) '将变量rng与第Item行合并为一个Range对象，合并完成后Rng变量将包含每一个需要插入标题的行
        Next

        '引用标题区域,并设置一些参数..
        With rngTitle                        '引用Rng对象
            .Select()                        '选择Rng代表的区域(即需要插入标题的行）
            xlapp.ActiveSheet.Paste          '执行粘贴操作（将第一行标题粘贴到rng所代表的所有行中）
            .Offset(-1, 0).Borders.LineStyle = Microsoft.Office.Core.XlConstants.xlNone  '对间隔行取消边框，从而使工资表更美观
            .Offset(-1, 0).RowHeight = 7                                                 '指定间隔行的行高为7
        End With
        xlapp.Range("a1").Select()   '选择A1单元格
        xlapp.ScreenUpdating = True  '恢复屏幕刷新
    End Sub
#End Region


#End Region
#Region "GN008_多图向左排列-180202"
    Private Sub Button4_Click(sender As Object, e As RibbonControlEventArgs)
        On Error Resume Next
        Dim objFilenname As Object, shpName As Object, intCounter As Integer, shpInstShap As Excel.Shape    '声明变量
        If TypeName(xlapp.Selection) <> "Range" Then MsgBox("先选择一个单元格,在运行此功能键") : Exit Sub   '如果当前选择的对象不是单元格则结束过程
        objFilenname = xlapp.GetOpenFilename("所有图片文件 (*.jpg;*.bmp;*.png;*.gif),*.jpg;*.bmp;*.png;*.gif", , "请选择所有待插入的图片文件", , True)
        If TypeName(objFilenname) = "Boolean" Then Exit Sub  '如果用户选择了取消键，那么结束过程
        For Each shpName In objFilenname                     '遍历用户选择的所有文件，其中变量shpName代表每一个图片文件名
            '插入shpName所代表的图片文件，且图片的左边距、上边距、宽度与高度皆与活插入的单元格保持一致,赋值给变量.
            shpInstShap = xlapp.ActiveSheet.Shapes.AddPicture(shpName, 0, -1, xlapp.ActiveCell.Offset(0, intCounter).Left,
                  xlapp.ActiveCell.Offset(0, intCounter).Top, xlapp.ActiveCell.Offset(0, intCounter).Width, xlapp.ActiveCell.Offset(0, intCounter).Height)
            xlapp.ActiveCell.Offset(-1, intCounter).Value = Split(Dir(shpName), ".")(0)  '将插入的图片命名为硬盘中的图片名称，包括扩展名
            shpInstShap.Placement = 1  '将shp的对象位置设置为“大小与位置随单元格而变”，目的是修改单元格的高度与宽度时图片也相应的变化
            shpInstShap.Name = Split(Dir(shpName), ".")(0) '将插入的图片命名为硬盘中的图片名称，包括扩展名
            intCounter = intCounter + 1  '累加计数器
            xlapp.ActiveCell.Offset(-1, -1).Value = "图片名称" : xlapp.ActiveCell.Offset(0, -1).Value = "图片放置处"
        Next shpName
    End Sub
#End Region
#Region "批量转PDF-240106"
    Private Sub btnWordConversionPDF_Click(sender As Object, e As RibbonControlEventArgs) Handles btnConversionPDF.Click
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/transformToPdf")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/transformToPdf") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If


        Dim f As New WIN140117_批量转换PDF  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnConversionPDF.Enabled = False '让按钮禁用，防止打开多个窗口


    End Sub
#End Region
    '#Region "GN010_Excel批量转PDF-18.02.03  "
    '    Private Sub Button22_Click(sender As Object, e As RibbonControlEventArgs)
    '        Dim pathStr As String

    '        '显示一个选择文件夹的对话框,如果选择了文件夹则取其名称，否则退出过程
    '        With xlapp.FileDialog(Microsoft.Office.Core.MsoFileDialogType.msoFileDialogFolderPicker)
    '            If .Show = True Then pathStr = .SelectedItems(0) Else Exit Sub
    '        End With

    '        '如果变量Pathstr不以“\”结尾(打开的是C盘":"),则追加“\”
    '        If Right(pathStr, 1) <> "\" Then pathStr = pathStr & "\"
    '        Dim Str As String, FileCount As Integer = 0           '声明变量
    '        Dim ExtensionName As String, isKill As MsgBoxResult   '声明变量


    '        '给变量赋值为选择的Yes或者No.
    '        isKill = MsgBox("转换成功后需要删除原文件吗?", vbDefaultButton2 + vbYesNo + vbQuestion, "是否删除原文件")
    '        xlapp.ScreenUpdating = False   '关闭屏幕更新，提升速度
    '        Str = Dir(pathStr & "*.xl*")   '开始查找文件，格式为所有Excel文件


    '        While Len(Str) > 0             '只要获取的文件名称长度大于0
    '            FileCount = FileCount + 1  '累加变量，该变量代表文件数量
    '            xlapp.Workbooks.Open(pathStr & Str) '打开文档
    '            ExtensionName = CreateObject("Scripting.FileSystemObject").getextensionname(Str)  '获取文档的扩展名
    '            '开始进行格式转换，两个参数分别表示格式和文件名称
    '            xlapp.ActiveWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, pathStr & Replace(Str, ExtensionName, "pdf"))
    '            xlapp.ActiveWorkbook.Close(False)       '关闭文档
    '            '如果用户选择了“是”,那么使用Kill语句删除Excel文档
    '            If isKill = vbYes Then Kill(pathStr & Str)
    '            Str = Dir()  '查找下一个
    '        End While
    '        xlapp.ScreenUpdating = True       '恢复屏幕更新
    '        MsgBox("成功转换了" & FileCount & "个Excel文档", vbOKOnly, "友情提示")
    '    End Sub
    '#End Region


#Region "GN011_获取文件信息_180205"
    '功    能:使用FSO对象遍历子文件夹创建文件目录 
    Public Sub Button16_Click(sender As Object, e As RibbonControlEventArgs) Handles Button16.Click
        Dim strPathStr As String, rngRangelArea As Excel.Range, strSlectRng As String                '声明变量
        With xlapp.FileDialog(Microsoft.Office.Core.MsoFileDialogType.msoFileDialogFolderPicker)  '创建一个选择文件夹的对话框
            If .Show = -1 Then strPathStr = .SelectedItems(0) Else Exit Sub  '如果选择了文件夹则单击“确定”则提取目录的路径，否则退出程序
        End With
        strSlectRng = xlapp.Range("a2").Address
        rngRangelArea = xlapp.InputBox("请选择放置的起始单元格", "提取文件名及链接", strSlectRng, , , , , 8)  '单元格赋值给单元格对象变量
        i = 0                                                              '将变量的默认值设置为0
        If Right(strPathStr, 1) <> "\" Then strPathStr = strPathStr & "\"  '如果路径右边没有“\”则追加一个“\”
        xlapp.Cells.Clear()           '清除所有单元格的数据
        xlapp.ScreenUpdating = False  '关闭屏幕更新，加快代码执行效率
        查找(strPathStr)              '调用查找程序   

        'mark 
        If i > 0 Then                 '如果找到的文件大于0个
            'xlapp.Range("a2").Resize(i, 3).Value = xlapp.WorksheetFunction.Transpose(arr) '结果已保存在数组arr中.
            'xlapp.Range("a1:c1").Value = {"路径", "文件名", "大小（MB）"}                 '在A1:C1区域写入标题
            'xlapp.Range("a:c").EntireColumn.AutoFit()                                     '按字符多少自动调整宽度
            'xlapp.Range("c:c").NumberFormat = "0.00"                                      '将C列设置为数值格式“0.00”

            rngRangelArea.Resize(i, 3).Value = xlapp.WorksheetFunction.Transpose(arr) '结果已保存在数组arr中.
            rngRangelArea.Offset(-1, 0).Resize(1, 3).Value = {"路径", "文件名", "大小（MB）"}                 '在A1:C1区域写入标题
            rngRangelArea.Offset(-1, 0).Resize(1, 3).EntireColumn.AutoFit()                                     '按字符多少自动调整宽度

            '易错：Resize（参数行-从本身开始算，参数列-从本身开始算列）
            rngRangelArea.Offset(0, 2).Resize(i).NumberFormatLocal = "0.00"   '将C列设置为数值格式“0.00” 
        End If
        rngRangelArea.CurrentRegion.Borders.LineStyle = 1  '加框线
        xlapp.ScreenUpdating = True    '恢复屏幕更新
        If MsgBox("是否对文件建立超链接", vbYesNo, "操作方式") = vbYes Then rngRangelArea.Offset(-1, 0).Select() : 文件链接()
    End Sub
#End Region
#Region "GN013_文件移动集合-180206"
    Private Sub Button24_Click(sender As Object, e As RibbonControlEventArgs) Handles Button24.Click
        Dim objFso As Object, objFp As Object, objFpf As Object, objFd As System.IO.DirectoryInfo, a As Integer, strDirName As String
        On Error Resume Next    '出错继续执行下一句代码
        If MsgBox("已备份好要移动的文件夹可以单击是." & Chr(13) &
                  "退回备份指定的文件夹,单击否.", vbDefaultButton2 + vbYesNo + vbQuestion, "备份文件夹") _
                  = vbNo Then Exit Sub  '如果单击否表示还没有备份文件夹将退出过程.
        xlapp.ScreenUpdating = False    '关闭屏幕更新闪烁
        With xlapp.FileDialog(Microsoft.Office.Core.MsoFileDialogType.msoFileDialogFolderPicker) '创建一个浏览文件夹的对话框
            If .Show = -1 Then strFielPath = .SelectedItems(0) Else Exit Sub                         '如果选择了“打开”就记录下路径，且赋值给变量.
        End With
        If Right(strFielPath, 1) <> "\" Then strFielPath = strFielPath & "\"  '如果路径右边没有“\”则追加一个“\”
        objFso = CreateObject("scripting.filesystemobject")             '创建一个FSO顶层对象并赋值给变量
        If objFso.FolderExists(strFielPath & "\CopyOption") = True Then '如果存在指定的文件夹,那么执行
            objFso.DeleteFolder(strFielPath & "CopyOption")             '删除名为CopyOption的文件夹
            MkDir(strFielPath & "\CopyOption")                          '创建指定文件夹
        Else
            MkDir(strFielPath & "\CopyOption")                          '创建指定文件夹
        End If

        objFp = objFso.GetFolder(strFielPath) '主文件夹写入到变量对象中
        a = 0
        For Each objFpf In objFp.Files    '遍历主文件夹中的文件
            a += 1
            If a > 0 Then Exit For
        Next
        If a > 0 Then objFso.movefile(strFielPath & "\*.*", strFielPath & "\CopyOption")   '文件移动到创建的文件夹内
        Dim drectInfo As System.IO.DirectoryInfo   '声明主目录对象
        drectInfo = New IO.DirectoryInfo(strFielPath)   '创建主文件实例
        For Each objSubFoder In drectInfo.GetDirectories        '在主目录中循环子文件夹
            If Not objSubFoder.FullName Like "*CopyOption" Then   '文件夹内有文件，且非创建的子文件夹
                strDirName = Dir(objSubFoder.FullName & "\*.*")   '获取子文件夹内子文件的名称
                Do While Len(strDirName) <> 0                                      '只要文件名称长度大于0就一直循环下去
                    If (GetAttr(strDirName) And 16) <> 16 Then  '如果不是文件夹
                        Kill(strFielPath & "CopyOption" & "\" & strDirName)            '删除备份文件夹内可能出现的同名的文件.
                    End If
                    strDirName = Dir()                                             '查找下一个子文件。
                Loop
                '★★★★执行第一层移动,即选择的文件夹作为主文件夹,里面的子文件夹不作考虑,只把主文件夹里文件移动到新建的文件夹里面
                objFso.movefile(objSubFoder.FullName & "\*.*", strFielPath & "\CopyOption")  '移动第2级文件夹内的文件到指定备份文件夹内
            End If
        Next                     '遍历下一个子文件夹,因为只要第一层移动.后面遍历其他子文件夹没有任何意义
        '-------------------------------
        查找子文件夹(strFielPath)    '调用程序
        If MsgBox("重要提醒,文件已全部提取成功到指定文件夹CopyOption." & Chr(13) & "如果单击否,将保留空文件夹,否则将帮你删除空文件夹.", vbDefaultButton2 + vbYesNo + vbQuestion, "备份文件夹") = vbNo Then
            xlapp.ScreenUpdating = True : Exit Sub              '开启更新 
        Else
            For Each objFd In drectInfo.GetDirectories          '在主目录中循环子目录
                If Not objFd.FullName Like "*CopyOption" Then   '文件夹内有文件且非创建的子文件夹.
                    objFso.DeleteFolder(objFd.FullName)         '逐一删除开始打开的主文件夹里的子文件夹.
                End If
            Next                                                '遍历下一个子文件夹
            xlapp.ScreenUpdating = True                         '开启更新
        End If
    End Sub
#End Region
#Region "'GN0014_获取数字"
    Private Sub Button10_Click(sender As Object, e As RibbonControlEventArgs) Handles Button10.Click
        On Error Resume Next
        Dim strDataOrign As String, strResult As String, strSlectRng As String, strMyStr As String, i As Integer = 0
        Dim rngRangelArea As Excel.Range, rngPlaceRange As Excel.Range, msgResult As MsgBoxResult  '这里跟VBA不一样,VBA是VBmsgboxresult
        Dim objArr() As Object, objArr1() As Object, j As Integer = 0, k As Byte = 0               '声明变量

        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量Targetsht赋值活动工作表(从这个表的数据转移到FV.xlam的第一个表) 
        'TargetRng = Targetsht.UsedRange.Address '对公共变量TargetRng赋值活动工作表的已用区域
        'Call 备份(Targetsht, TargetRng) '调用工程级任务(注:这里代码调用的是VSTO 中的的撤销方法)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True  'VSTO 的撤销按钮恢复启用
        ''____________________备份数据、记录区域___________________________

        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True


        strSlectRng = xlapp.Selection.Address                                             '单元格地址赋值给文本变量
        rngRangelArea = xlapp.InputBox("请选择区域", "提取数字", strSlectRng, , , , , 8)  '单元格赋值给单元格对象变量
        strResult = ""                                                              '设置初始值
        With CreateObject("VBSCRIPT.REGEXP")    '创建正则表达式对象
            For Each rngEvlutRng As Excel.Range In rngRangelArea   '遍历数据区域
                i = i + 1                     '计数器
                strMyStr = rngEvlutRng.Value  '将搜索的文本赋予变量
                .Global = True                '全局匹配
                .Pattern = "\d+(\.\d*)?"      '指定搜索条件，匹配所有单个数字,小数点前用转义符号,括号后面?元字符表示可能没有小数.
                If .test(rngEvlutRng.Value) Then  '如果匹配成功
                    For Each objMatch As Object In .Execute(strMyStr)     '遍历搜索结果
                        strResult = strResult & objMatch.value & " "      '将所有符合条件的数值累加起来成一个文本
                    Next
                    ReDim Preserve objArr(0 To i - 1)   '重置一维数组上标
                    objArr(i - 1) = strResult           '在一维数组中写入值               
                    strResult = ""                      '清空数据
                Else
                    ReDim Preserve objArr(0 To i - 1)      '重置数组上标,这里的i要注意是连续执行的,执行成功的i跟不成功的i都累积
                    objArr(i - 1) = Nothing      '空值,可能省略关系也不大,默认为空值
                End If
            Next rngEvlutRng
            msgResult = MsgBox("选择是：原区域放置；" + Chr(10) + "选择否：分列放置。", vbYesNo + vbQuestion, "操作方式") '选择操作方式
            If msgResult = 6 Then                                                           '如果选择是,那么执行,否则执行

                'rngRangelArea.Value = xlapp.WorksheetFunction.Transpose(objArr) : Exit Sub  '放置在原区域
                rngRangelArea.Value = xlapp.WorksheetFunction.Transpose(objArr)   '放置在原区域
            Else   '否则放置在指定区域
                rngPlaceRange = xlapp.InputBox("请选择放置点区域", "提取单列数字", , , , , , 8)
                With CreateObject("VBSCRIPT.REGEXP") '创建正则表达式引用
                    For j = 0 To i - 1               '在 0到 区域数量上循环
                        .Pattern = "\d+(\.\d*)? "    '指定搜索条件，取所有数字
                        .Global = True               '全局匹配
                        If .test(objArr(j)) Then                '如果匹配成功
                            For Each objMatch As Object In .Execute(objArr(j)) '对每个字符区域(单元格)遍历搜索结果
                                k = k + 1         '计数器
                                ReDim Preserve objArr1(0 To k - 1)  '重置上标
                                objArr1(k - 1) = objMatch.value     '写入值
                            Next
                            rngPlaceRange.Offset(j, 1).Resize(1, k).Value = objArr1  '利用循环横坐标方向,开始逐步写入分段值
                            k = 0            '释放计数器
                            Erase objArr1    '释放清空数组
                        End If               '结束if 语句
                    Next
                    '第一列放置所有匹配值
                    rngPlaceRange.Resize(rngRangelArea.Count, 1).Value = xlapp.WorksheetFunction.Transpose(objArr)
                End With
            End If
            For i = 1 To rngPlaceRange.CurrentRegion.Columns.Count  '利用循环写入标题
                If i = 1 Then
                    rngPlaceRange.Offset(-1, i - 1).Value = "未拆分列"
                Else
                    rngPlaceRange.Offset(-1, i - 1).Value = "第" & i - 1 & "列"
                End If
            Next
            rngPlaceRange.CurrentRegion.Borders.LineStyle = 1                 '加框线
            rngPlaceRange.CurrentRegion.CurrentRegion.EntireColumn.AutoFit()  '自动调整列宽 

            ''.......................................................................................
            'xlapp.OnUndo("撤消[获取数字]", "撤消") '这里代码调用的是FV.xlam加载项的撤销方法

            ''.......................................................................................
        End With
    End Sub
#End Region
#Region "GN0015_去除数字"
    Private Sub Button11_Click(sender As Object, e As RibbonControlEventArgs) Handles Button11.Click
        'On Error Resume Next
        'Dim Item As Object, Result As String
        Dim strMyStr As String, rngEvalutRange As Excel.Range, rngMyCell As Excel.Range,
            i As Integer = 0, SelRng As String, objArr() As Object, strGetData As String
        SelRng = xlapp.Selection.Address   '单元格地址赋值给文本变量
        rngMyCell = xlapp.InputBox("请选择区域", "删除数字", SelRng, , , , , 8)
        With CreateObject("VBSCRIPT.REGEXP")         '创建正则表达式引用
            For Each rngEvalutRange In rngMyCell     '遍历数据区域
                i = i + 1
                strMyStr = rngEvalutRange.Text  '将搜索的文本赋予变量
                .Pattern = "\d+(\.\d*)?"  '指定搜索条件，取所有数字
                .Global = True   '全局匹配
                If .test(strMyStr) Then                        '如果匹配成功
                    strGetData = .Replace(strMyStr, "")        '清空数值后,下面将写入数组
                    ReDim Preserve objArr(0 To i - 1)          '重新定义上标
                    objArr(i - 1) = strGetData                 '逐一写入数组
                Else
                    ReDim Preserve objArr(0 To i - 1)          '重新定义上标
                    objArr(i - 1) = strMyStr                   '写入原值(不含数值)
                End If
            Next rngEvalutRange
        End With
        rngMyCell.Value = xlapp.WorksheetFunction.Transpose(objArr)   '批量写入数组值到单元格.
    End Sub
#End Region
#Region "GN0016_获取字母"
    Private Sub Button12_Click(sender As Object, e As RibbonControlEventArgs) Handles Button12.Click
        On Error Resume Next
        Dim strMyStr As String, objItem As Object, strResult As String = "", rngRange As Excel.Range, rngMyCell As Excel.Range
        Dim i As Long = 0, strSelRng As String, objArr() As Object, msgResult As MsgBoxResult  'VBA是VBmsgboxresult               
        strSelRng = xlapp.Selection.Address  '将选择的单元格地址赋值给变量
        rngMyCell = xlapp.InputBox("请选择区域", "提取字母", strSelRng, , , , , 8)  '将选择的单元格赋值给变量
        msgResult = MsgBox("单词与单词是否加入空格" & vbCrLf & "是否要添加？", vbQuestion + vbYesNo, "添加空格")
        With CreateObject("VBSCRIPT.REGEXP")    '创建正则表达式引用
            For Each rngRange In rngMyCell   '遍历数据区域
                i = i + 1     '创建计数器
                strMyStr = rngRange.Value      '将搜索的文本赋予变量
                .Pattern = "\b[a-zA-Z]+\b"      '指定搜索条件，取所有字母,其中匹配单词界限\b.
                .Global = True    '全局匹配
                If .test(strMyStr) Then     '如果匹配成功
                    For Each objItem In .Execute(strMyStr)      '遍历搜索结果
                        If msgResult = vbNo Then
                            strResult = strResult & objItem.value       '将所有符合条件的字母提取出来,其中objItem.value不能省略.
                        Else
                            strResult = strResult & objItem.value & " "  '将所有符合条件的字母提取出来,其中objItem.value不能省略.
                        End If
                    Next     '循环
                    ReDim Preserve objArr(0 To i - 1)    '重新定义数组维数,i-1表示单元格数量-1的个数,因为下标是从0开始的,同下
                    objArr(i - 1) = strResult     '给单一的数组元素赋值
                    strResult = ""                                  '清空数据
                Else
                    ReDim Preserve objArr(0 To i - 1)               '重新定义数组维数
                    objArr(i) = ""                                  '第i个数组元素值为空值
                End If
            Next rngRange
        End With                                                    '结束with引用语句
        rngMyCell.Value = xlapp.WorksheetFunction.Transpose(objArr) '放置数组在单元格区域
    End Sub
#End Region
#Region "GN0017_去除字母"
    Private Sub Button13_Click(sender As Object, e As RibbonControlEventArgs) Handles Button13.Click
        On Error Resume Next
        'Dim Item As Object,Result As String
        Dim strMyStr As String, rngRange As Excel.Range, rngSelctCells As Excel.Range, i As Long = 0
        Dim strSelRng As String, objArr() As Object, strGetData As String
        strSelRng = xlapp.Selection.Address                                             '单元格地址赋值给变量
        rngSelctCells = xlapp.InputBox("请选择区域", "删除字母", strSelRng, , , , , 8)  '单元格对象赋值给变量
        With CreateObject("VBSCRIPT.REGEXP")                    '创建正则表达式引用
            For Each rngRange In rngSelctCells                  '遍历数据区域
                i = i + 1                                       '添加计数器,记录循环单元格的数量
                strMyStr = rngRange.Value                       '将搜索的文本赋予变量
                .Pattern = "\b[a-zA-Z]+\b"                      '指定搜索条件，取所有字母
                .Global = True                                  '全局匹配
                If .test(strMyStr) Then                         '如果匹配成功
                    strGetData = .Replace(strMyStr, "")         '利用正则资源的函数替换成空
                    ReDim Preserve objArr(0 To i - 1)           '重新声明数组维数
                    objArr(i - 1) = strGetData                  '逐一给数组元素赋值替换后的值
                Else
                    ReDim Preserve objArr(0 To i - 1)           '重新定义数组维数
                    objArr(i - 1) = strMyStr                    '逐一给数组元素赋值原值
                End If
            Next rngRange
        End With                                                          '结束with语句(引用正则对象)
        rngSelctCells.Value = xlapp.WorksheetFunction.Transpose(objArr)   '数组写入单元格中
    End Sub
#End Region
#Region "GN0018_自定义删除符号"
    Private Sub Button14_Click(sender As Object, e As RibbonControlEventArgs) Handles Button14.Click
        On Error Resume Next
        Dim strMyStr As String, rngRange As Excel.Range, rngMyCell As Excel.Range, i As Integer = 0
        Dim strSelRng As String, objArr() As Object, strCustPat As String, strGetData As String
        strSelRng = xlapp.Selection.Address  '选择的区域地址赋予变量作为inpotbox方法的默认参数
        rngMyCell = xlapp.InputBox("请选择区域", , strSelRng, , , , , 8)    '选择区域单元格对象赋值给变量
        strCustPat = xlapp.InputBox("请输入要删除的符号", , "-", , , , , 2)  '选择要删除的符号,默认"-"
        If strCustPat = "." Then strCustPat = "\."                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "?" Then strCustPat = "\?"                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "(" Then strCustPat = "\("                           '如果是删除.,那么需要加转义符号.
        If strCustPat = ")" Then strCustPat = "\)"                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "[" Then strCustPat = "\["                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "]" Then strCustPat = "\]"                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "|" Then strCustPat = "\|"                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "{" Then strCustPat = "\{"                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "}" Then strCustPat = "\}"                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "^" Then strCustPat = "\^"                           '如果是删除.,那么需要加转义符号.
        If strCustPat = "$" Then strCustPat = "\$"                           '如果是删除.,那么需要加转义符号.
        With CreateObject("VBSCRIPT.REGEXP")    '创建正则表达式引用
            For Each rngRange In rngMyCell    '遍历数据区域
                i = i + 1     '计数器使用,记录循环单元格的数量
                strMyStr = rngRange.Value   '将搜索的文本赋予变量
                .Pattern = strCustPat    '指定搜索条件
                .Global = True   '全局匹配
                If .test(strMyStr) Then   '如果匹配成功
                    strGetData = .Replace(strMyStr, "") '利用正则表达式Replace方法删除特定的符号
                    ReDim Preserve objArr(0 To i - 1)   '重置数组下标到上标
                    objArr(i - 1) = strGetData  '给数组元素逐一赋值
                Else                                    '否则匹配不成功,即没有找到相应的符号则
                    ReDim Preserve arr(0 To i - 1)      '同上给数组重置下标上标
                    objArr(i - 1) = strMyStr            '给第I-1个元素赋值为本身的值
                End If
            Next rngRange
        End With                                        '结束with引用语句
        rngMyCell.Value = xlapp.WorksheetFunction.Transpose(objArr)   '所有数组元素值写入指定单元格区域
    End Sub
#End Region
#Region "GN0019_解除表格密码"
    Private Sub Button6_Click(sender As Object, e As RibbonControlEventArgs) Handles Button6.Click
        '对工作表加密，记住Protect方法的参数变化,4次加密
        Dim Item As Excel.Worksheet
        For Each Item In xlapp.Worksheets
            Item.Protect(DrawingObjects:=True, Contents:=True, AllowFiltering:=True)
            Item.Protect(DrawingObjects:=False, Contents:=True, AllowFiltering:=True)
            Item.Protect(DrawingObjects:=True, Contents:=True, AllowFiltering:=True)
            Item.Protect(DrawingObjects:=False, Contents:=True, AllowFiltering:=True)
            Item.Unprotect()  '解密
        Next
        'MsgBox("恭喜，解密成功" & Chr(13) & "该功能只针对EXCLE13,10,07,03解密")   '用msgbox函数或者用下MessageBox类的Show方法
        MessageBox.Show("解密成功" & Chr(13) & "该功能只针对EXCLE13,10,07,03解密", "恭喜")
    End Sub
#End Region
#Region "'GN020_批量加密表格"
    Private Sub Button7_Click(sender As Object, e As RibbonControlEventArgs) Handles Button7.Click
        Dim arr() As Object, item1 As Excel.Worksheet, i As Byte = 0, j As String
        j = xlapp.InputBox(Prompt:="在对话框中输入密码", Title:="密码", Type:=2)
        For Each item1 In xlapp.Worksheets
            i = i + 1
            ReDim Preserve arr(0 To i - 1)
            arr(i - 1) = item1.Name
        Next
        加密工作表(j, arr) '以自定义字符为密码对所有工作表加密
    End Sub
#End Region
#Region "'GN021_下拉框选择项目发生事件"
    'Private Sub DropDown1_SelectionChanged(sender As Object, e As RibbonControlEventArgs) Handles DropDown1.SelectionChanged
    '    Dim sty As String  '声明变量
    '    Dim rng As Excel.Range      '定义单元格变量
    '    sty = Me.DropDown1.SelectedItem.Label   '给变量赋值为所选项目值的标签
    '    rng = xlapp.Intersect(xlapp.Selection, xlapp.ActiveSheet.UsedRange)    '将选区与活动工作表的已用区域的交集赋值给变量rng
    '    jzdw(rng, sty)     '调用公共模块中的公共程序
    '    Me.DropDown1.SelectedItemIndex = 0      '调用完程序后,还原首项,防止不能在原区域上更新
    'End Sub

    ' ★★★ 私有字段（符合 VB2015 命名规范：前缀 + 驼峰） ★★★
    Private fndLastFindType As FindType          ' 最近一次查找类型
    Private intLastMatchCount As Integer         ' 最近一次找到的个数
    Private dblLastTargetValue As Double         ' 最近一次目标值

    ' ============================================================
    ' ★★★ 只读属性（返回最近一次查找的统计信息） ★★★
    ' ============================================================
    Public ReadOnly Property strLastFindInfo() As String
        Get
            ' 如果还没有执行过查找，返回提示信息
            If intLastMatchCount = 0 AndAlso dblLastTargetValue = 0 Then
                Return "尚未执行任何查找操作"
            End If

            ' 获取枚举对应的中文名称
            Dim strFindName As String = [Enum].GetName(GetType(FindType), fndLastFindType)

            ' 拼接统计信息
            Return "最近查找：" & strFindName &
                   "，找到 " & intLastMatchCount & " 个" &
                   "，目标值：" & dblLastTargetValue
        End Get
    End Property

    ''' <summary>
    ''' GN005：定位指定区域中的最大值、最小值、众数或平均值所在的单元格
    ''' </summary>
    Private Sub DropDown1_SelectionChanged(sender As Object, e As RibbonControlEventArgs) Handles DropDown1.SelectionChanged

        ' 1. 检查是否选中了有效项
        If Me.DropDown1.SelectedItem Is Nothing Then
            Exit Sub
        End If

        ' 2. 获取选中的标签文字
        Dim strLabel As String = Me.DropDown1.SelectedItem.Label

        ' 3. 如果是提示项，不执行操作
        If strLabel = "请选择条件" OrElse String.IsNullOrEmpty(strLabel) Then
            Exit Sub
        End If

        ' 4. 将下拉框的文本转换为枚举值
        Dim enType As FindType
        Select Case strLabel
            Case "最大值"
                enType = FindType.最大值
            Case "最小值"
                enType = FindType.最小值
            Case "众数"
                enType = FindType.众数
            Case "平均值"
                enType = FindType.平均值
            Case Else
                MessageBox.Show("未知的查找类型：" & strLabel, "提示")
                Exit Sub
        End Select

        ' ★★★ 获取当前选中的有效区域 ★★★
        Dim rngSelection As Excel.Range = xlapp.Intersect(xlapp.Selection, xlapp.ActiveSheet.UsedRange)

        If rngSelection Is Nothing Then
            MessageBox.Show("请先选中一个有效的单元格区域！", "提示")
            Exit Sub
        End If

        ' ★★★ 保存原始选区（用于恢复，保持状态栏统计不变） ★★★
        Dim rngOriginalSelection As Excel.Range = xlapp.Selection

        ' 执行查找
        Dim blnOldScreenUpdating As Boolean = xlapp.ScreenUpdating
        xlapp.ScreenUpdating = False

        Try
            ' ★★★ 调用核心方法（传入区域和查找类型） ★★★
            M2_调用的任务.FindPosition(rngSelection, enType)

            ' 恢复原始选区
            If rngOriginalSelection IsNot Nothing Then
                rngOriginalSelection.Select()
            End If

        Catch ex As Exception
            MessageBox.Show("操作时发生错误：" & ex.Message, "错误")
        Finally
            xlapp.ScreenUpdating = blnOldScreenUpdating

            ' ★★★ 关键修复：还原下拉框到首项，允许重复选择同一项目 ★★★
            ' 如果不还原，用户再次点击同一选项时不会触发 SelectionChanged 事件
            RemoveHandler Me.DropDown1.SelectionChanged, AddressOf DropDown1_SelectionChanged
            Me.DropDown1.SelectedItemIndex = 0
            AddHandler Me.DropDown1.SelectionChanged, AddressOf DropDown1_SelectionChanged
        End Try

    End Sub
#End Region



#Region "GN190105_奇偶行定位"
    Private Sub btn奇偶定位_Click(sender As Object, e As RibbonControlEventArgs) Handles btn奇偶定位.Click
        On Error Resume Next
        Dim rng As Excel.Range, msgResult As MsgBoxResult
        rng = xlapp.ActiveSheet.UsedRange '仅对已用区域定位
        msgResult = MsgBox("选择Yes将选中工作表中的所有已用区域的偶数行，" & Chr(13) & "否则您将选中奇数行。", vbYesNo + vbQuestion, "友情提示")
        '在最后一列创建辅区，在辅助区中添加公式"=IF(MOD(ROW(),2)=1,0/0,"")"
        'xlapp.Range("b1").Formula = "=sum(a1:a3)"
        With xlapp.Range(xlapp.Cells(rng.Row, xlapp.Columns.Count), xlapp.Cells(rng.Row + rng.Rows.Count - 1, xlapp.Columns.Count))
            Select Case msgResult
                Case 6
                    .Formula = "=if(mod(row(),2)=0,0/0,"""")"
                    .SpecialCells(-4123, 16).EntireRow.Select()  '定位有错误的行
                Case 7
                    .Formula = "=if(mod(row(),2)=1,0/0,"""")"
                    .SpecialCells(-4123, 16).EntireRow.Select()  '定位有错误的行
            End Select
            .Clear()  '清除辅助区
        End With
    End Sub
#End Region
#Region "GN 180817 01 大于某个选定的值"
    Private Sub btnGreaterData_Click(sender As Object, e As RibbonControlEventArgs) Handles btnGreaterData.Click
        Dim rng As Excel.Range, RngTemp As Excel.Range, cell As Excel.Range, i As Long, intGreaterData As Integer '声明变量
        On Error Resume Next  '当程序出错时继续执行下一句(如果不存在数值时代码会出错)
        intGreaterData = xlapp.InputBox(Prompt:="超过某值", Title:="输入数值", Type:=2) '输入比较值...
        '将活动工作表已用区域的数值区域赋值给变量rng
        rng = xlapp.Intersect(xlapp.Selection, xlapp.ActiveSheet.UsedRange).SpecialCells(2, 1)
        '如果赋值不成功(错误编码为1004)，那么提示用户，且结束过程
        If Err.Number = 1004 Then MsgBox("当前表没有数值。", vbInformation, "提示") : Exit Sub
        For Each cell In rng  '利用For Each...Next循环语句遍历Rng区域的每个单元格
            If cell.Value > intGreaterData Then  '如果数值大于1000
                i = i + 1  '累加变量，该变量等于符合条件的单元格个数
                '如果变量i等于 1，那么将找到的单元格赋予变量Rngtemp,否则将找到的单元格与变量Rngtemp合并
                If i = 1 Then RngTemp = cell Else RngTemp = xlapp.Union(RngTemp, cell)
            End If
        Next cell
        If i > 0 Then  '如果存在符合条件的单元格
            RngTemp.Select()  '选择变量Rngtemp所代表的区域
            xlapp.StatusBar = "找到" & i & "个大于" & intGreaterData & "的单元格" '在状态栏显示符合条件的单元格个数
        End If
    End Sub
#End Region
#Region "GN 180817 02 小于设定的数值"
    Private Sub btnLessData_Click(sender As Object, e As RibbonControlEventArgs) Handles btnLessData.Click
        Dim rng As Excel.Range, RngTemp As Excel.Range, cell As Excel.Range, i As Long, intGreaterData As Integer '声明变量
        'On Error Resume Next  '当程序出错时继续执行下一句(如果不存在数值时代码会出错)
        intGreaterData = xlapp.InputBox(Prompt:="小于某值", Title:="输入数值", Type:=2)
        '将活动工作表已用区域的数值区域赋值给变量rng
        rng = xlapp.Intersect(xlapp.Selection, xlapp.ActiveSheet.UsedRange).SpecialCells(2, 1)
        '如果赋值不成功(错误编码为1004)，那么提示用户，且结束过程
        If Err.Number = 1004 Then MsgBox("当前表没有数值。", vbInformation, "提示") : Exit Sub
        For Each cell In rng  '利用For Each...Next循环语句遍历Rng区域的每个单元格
            If cell.Value < intGreaterData Then  '如果数值大于1000
                i = i + 1  '累加变量，该变量等于符合条件的单元格个数
                '如果变量i等于 1，那么将找到的单元格赋予变量Rngtemp,否则将找到的单元格与变量Rngtemp合并
                If i = 1 Then RngTemp = cell Else RngTemp = xlapp.Union(RngTemp, cell)
            End If
        Next cell
        If i > 0 Then     '如果存在符合条件的单元格
            RngTemp.Select()  '选择变量Rngtemp所代表的区域
            xlapp.StatusBar = "找到" & i & "个小于" & intGreaterData & "的单元格" '在状态栏显示符合条件的单元格个数
        End If
    End Sub
#End Region
#Region "GN190107_错误值隐藏"
    Private Sub btnHideErr_Click(sender As Object, e As RibbonControlEventArgs) Handles btnHideErr.Click
        Dim rng As Excel.Range, cell As Excel.Range
        On Error Resume Next '防错,避免找不到错误值时产生错误
        With xlapp.ActiveSheet.UsedRange  '仅对已用区域进行定位
            rng = .SpecialCells(-4123, 16)  '将公式结果为错误的区域赋值给变量rng
            If Err.Number = 0 Then '如果成功定位到错误的公式.
                rng = xlapp.Union(rng, .SpecialCells(2, 16))  '将公式的错误单元格与常量错误单元格合并.
            Else  '否则将常量错误单元格赋值给变量rng
                rng = .SpecialCells(2, 16)
            End If
            xlapp.ScreenUpdating = False  '并闭屏幕更新,加快速度
            For Each cell In rng   '遍历所有错误单元格
                cell.Font.Color = cell.Interior.Color  '将字体色设置为背景色
            Next
            xlapp.ScreenUpdating = True  '恢复屏幕更新
        End With
    End Sub
#End Region
#Region "GN180820 01 英语单词拼写检查"
    Private Sub btnCheckWords_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCheckWords.Click
        Dim rng As Excel.Range
        '遍历选区与已用区域的交集
        For Each rng In xlapp.Intersect(xlapp.Selection, xlapp.ActiveSheet.UsedRange)
            '首先将单词转换成首字母大写，然后在默认词典中检查，如果不存在
            If Not xlapp.CheckSpelling(StrConv(rng.Value, vbProperCase)) Then
                '将单元格背景设置为灰色
                rng.Interior.ColorIndex = 17
            End If
        Next
    End Sub
#End Region

    '--------------数据库---------------------


    '设备基本信息-180109
    Private Sub Button26_Click(sender As Object, e As RibbonControlEventArgs) Handles Button26.Click
        Dim f As New B01_设备基本信息  '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button26.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button27_Click(sender As Object, e As RibbonControlEventArgs) Handles Button27.Click
        Dim f As New B02_维修设备信息  '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button27.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button28_Click(sender As Object, e As RibbonControlEventArgs) Handles Button28.Click
        Dim f As New B03_保养设备信息  '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button28.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button30_Click(sender As Object, e As RibbonControlEventArgs) Handles Button30.Click
        Dim f As New B04_设备信息查询与导出  '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button30.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button31_Click(sender As Object, e As RibbonControlEventArgs) Handles Button31.Click
        Dim f As New C01_供应商资料管理  '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button31.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button32_Click(sender As Object, e As RibbonControlEventArgs) Handles Button32.Click
        Dim f As New C02_采购进货信息管理 '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button32.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button33_Click(sender As Object, e As RibbonControlEventArgs) Handles Button33.Click
        Dim f As New C03_采购物品信息管理 '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button33.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button34_Click(sender As Object, e As RibbonControlEventArgs) Handles Button34.Click
        Dim f As New C04_物品消耗使用信息管理 '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button34.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button35_Click(sender As Object, e As RibbonControlEventArgs) Handles Button35.Click
        Dim f As New C05_物品库存信息 '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button35.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button36_Click(sender As Object, e As RibbonControlEventArgs) Handles Button36.Click
        Dim f As New C06_物品消耗使用成本统计分析 '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button36.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button37_Click(sender As Object, e As RibbonControlEventArgs) Handles Button37.Click
        Dim f As New C07_物品信息查询与导出 '声明变量并创建一个窗体实例
        f.Show()                     '显示实例窗体
        Button37.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub Button29_Click(sender As Object, e As RibbonControlEventArgs) Handles Button29.Click
        Dim f As New C05_物品库存信息 '声明变量并创建一个窗体实例
        f.Show()                      '显示实例窗体
        Button29.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

#Region "数据库：资格证书"
    '资质证书
    Private Sub btn证书_Click(sender As Object, e As RibbonControlEventArgs) Handles btn证书.Click
        Dim f As New D01_资质证书信息 '声明变量并创建一个窗体实例
        f.Show()                      '显示实例窗体
        ntyRibbon.Visible = True '显示任务栏图标
        ntyRibbon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info '在气球上显示信息图标
        'ntyRibbon.Icon = Drawing.SystemIcons.Information '在任务栏显示系统信息图标
        ntyRibbon.BalloonTipTitle = "人员资格证" '指定提示信息的标题
        ntyRibbon.BalloonTipText = "人员资格证信息仅供查看，请勿泄漏，谢谢合作!" '指定提示信息的正文
        ntyRibbon.Text = "资格证信息：有效日期、人员、种类等." '指定任务栏图标的名称
        ntyRibbon.ShowBalloonTip(3000) '指定提示信息的显示时间，单位为毫秒
        ' .....更多代码.....
        btn证书.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub


    '关闭信息提示时，触发该事件.
    Private Sub ntyRibbon_BalloonTipClosed(sender As Object, e As EventArgs) Handles ntyRibbon.BalloonTipClosed
        ntyRibbon.Visible = False
    End Sub
#End Region

#Region "数据库电能消耗统计"
    '打开能资源消耗统计（电能）
    Private Sub btn电能_Click(sender As Object, e As RibbonControlEventArgs) Handles btn电能.Click

        Dim f As New E01_电能消耗统计 '声明变量并创建一个窗体实例
        f.Show()                      '显示实例窗体
        btn电能.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

#End Region



#Region "Usr_显示隐藏自定义窗格"
    Private Sub btnDisplayDate_Click(sender As Object, e As RibbonControlEventArgs) Handles btnDisplayDate.Click
        '切换任务窗格的显示状态，单击显示再单击隐藏
        Globals.ThisAddIn.任务窗格.Visible = Not Globals.ThisAddIn.任务窗格.Visible
        'btnDisplayDate.Label = IIf(btnDisplayDate.Label = "已显示F定义窗格", "已隐藏F定义窗格", "已显示F定义窗格") '切换菜单文字
    End Sub
#End Region


    Private Sub btnInformationExtract_Click(sender As Object, e As RibbonControlEventArgs) Handles btnInformationExtract.Click
        Dim f As New D02_人员信息查询与导出 '声明变量并创建一个窗体实例
        f.Show()                      '显示实例窗体
        btnInformationExtract.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    '打开网页（百度）
    Private Sub btnOpenWeb_Click(sender As Object, e As RibbonControlEventArgs) Handles btnOpenWeb.Click
        If My.Computer.Keyboard.CtrlKeyDown Then
            System.Diagnostics.Process.Start("http://wangfei.qicp.vip")
        End If
        'Shell("explorer.exe C:\Recovery.txt")  '打开C盘指定文档的函数。
    End Sub

    '显示IP地址
    Private Sub btnIP_Click(sender As Object, e As RibbonControlEventArgs) Handles btnIP.Click
        'Dim SW As IO.StreamWriter =
        '    My.Computer.FileSystem.OpenTextFileWriter("d:\test.bat", False, System.Text.Encoding.GetEncoding("GB2312"))

        'SW.WriteLine("@echo off")
        'SW.Write("ipconfig")
        'SW.WriteLine("pause")
        'SW.Close()
        'System.Diagnostics.Process.Start("d:\test.bat")
        'Shell("cmd /c ipconfig")  '打开C盘指定文档的函数。
        'Console.ReadLine()

        'RunCMD("ipconfig", 600)

        Dim IPAdress As System.Net.IPAddress, HostName As String
        HostName = System.Net.Dns.GetHostName
        IPAdress = System.Net.Dns.GetHostByName(HostName).AddressList.GetValue(0)
        xlapp.Range("a1").Value = HostName
        xlapp.Range("a2").Value = IPAdress.ToString
        MsgBox("本机名:" & HostName & Chr(13) & "本机IP：" & IPAdress.ToString)

    End Sub









    Private Sub Button1_Click(sender As Object, e As RibbonControlEventArgs) Handles btn不良品信息.Click
        Dim f As New F01_不良品基本信息 '声明变量并创建一个窗体实例
        f.Show()                      '显示实例窗体
        btn不良品信息.Enabled = False      '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btn不良品信息查询与导出_Click(sender As Object, e As RibbonControlEventArgs) Handles btn不良品信息查询与导出.Click
        Dim f As New F02_不良品信息查询与导出 '声明变量并创建一个窗体实例
        f.Show()  '显示实例窗体
        btn不良品信息查询与导出.Enabled = False  '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btn不良信息分析_Click(sender As Object, e As RibbonControlEventArgs) Handles btn不良信息分析.Click
        Dim f As New F03_不良品发生分析 '声明变量并创建一个窗体实例
        f.Show()  '显示实例窗体
        btn不良信息分析.Enabled = False  '让按钮禁用，防止打开多个窗口
    End Sub



    Private Sub btn调休节假信息_Click(sender As Object, e As RibbonControlEventArgs) Handles btn调休节假信息.Click
        Dim f As New D03_调休节假日信息 '声明变量并创建一个窗体实例
        f.Show()  '显示实例窗体
        btn调休节假信息.Enabled = False  '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btn加班时间_Click(sender As Object, e As RibbonControlEventArgs) Handles btn加班时间.Click
        On Error Resume Next    '没有这一句数据库(记录集)测试错误...
        Dim myData As String, myArray() As String， rs As Object       '声明变量,数据库路径
        Dim i As Byte = 0, rngSelection As Excel.Range, bytCounter As Byte, dan As Single, shuang As Single, san As Single '声明变量
        Dim rng3 As Excel.Range, rng4 As Excel.Range, k As Integer = 0, j As Byte = 0

        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        'Call 备份(Targetsht, TargetRng)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True  '这里代码调用的是VSTO EXCEL加载项的撤销方法
        ''____________________备份数据、记录区域___________________________
        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True

        xlapp.ScreenUpdating = False    '禁止屏幕刷新，提升工作效率
        myData = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb"  '指定数据库名称，三星笔记本本地测试

        '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb")  '公司共享盘
        'myData = "F:\2 笔记记录\8 过程信息管理\文件管理\文件管理.accdb"  '家里台式机测试
        'myData = "D:\2 笔记记录\0 过程信息管理笔记\文件管理\文件管理.accdb" '三星笔记本本地测试
        '给变量赋值为一维数组,改数组变量是公共变量
        myArray = {"分类", "日期", "加班倍数", "备注"}
        '建立与数据库的连接,创建数据库连接对象(ADO的最顶层),这里还没指定数据库连接,打开指定数据库
        cnn = CreateObject("adodb.Connection")
        '引用cnn(ado最顶层对象)
        With cnn    '引用数据库连接对象
            .Provider = "microsoft.Ace.OLEDB.12.0"   '指定数据库引擎提供者是Access
            .Open(myData)                            '建立指定的数据库连接
        End With    '结束语句
        rngSelection = xlapp.Selection  '将选择区域赋值给变量...
        bytCounter = rngSelection.Rows.Count + 1    '从第2行单元格开始选择区域的总行数+第1行的数量赋值给变量..
        xlapp.Columns("f:o").Delete     '删除f:o列...
        'xlapp.Range("d1:d100").Cut(xlapp.Range("dd1"))  '剪切后，粘贴到目标单元格（B2）...
        'xlapp.Columns("D:D").NumberFormatLocal = "@"
        'xlapp.Range("dd1:dd100").Cut(xlapp.Range("d1"))

        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count '从第2行到最后一行上遍历写入公式...
            xlapp.Cells(j, 200).FormulaR1C1 = "=SUBSTITUTE(RC[-199],""."",""/"")"  '写入公式，用“/”代替“.”。
            xlapp.Cells(j, 200).Value = xlapp.Cells(j, 200).Value  '去除公式，只写入值.
        Next
        xlapp.Cells(2, 200).CurrentRegion.Cut(xlapp.Range("a2")) '剪切已替换完成的日期值，写入到A2单元格为起点...
        xlapp.Range("aa2:aa" & bytCounter).FormulaR1C1 = "=WEEKDAY(RC[-26],2)"  '写入公式（不需要遍历，公式可以相对引用单元格），获取星期...
        xlapp.Range("aa2:aa" & bytCounter).Value = xlapp.Range("aa2:aa" & bytCounter).Value  '去除公式，单元格只写入值...
        xlapp.Range("aa2").CurrentRegion.Cut(xlapp.Range("b2"))  '剪切后，粘贴到目标单元格（B2）...
        '数组值批量写入到单元格中...
        xlapp.Range("f1:m1").Value = {"正常上班时间", "正常下班时间", "超8小时时长", "超8小时有效时长", "是否正常", "单总时长", "双总时长", "三总时长"}
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count  '从第2行到最后一行遍历...
            If xlapp.Range("d" & j).Value <> "" Then  '如果不为空值（上班打卡...）
                'If Hour(xlapp.Range("d" & j).Value) < 17 Then  '小于5点上班，即判定为白班工作时间...
                If xlapp.Range("d" & j).Value < 0.71 Then  '小于5点上班，即判定为白班工作时间...
                    '写入上班开始时间，由公式转换成值...
                    xlapp.Range("f" & j).Value = "7:30" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
                    xlapp.Range("g" & j).Value = "16:30" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value

                    '写入公式，上下班时间间隔多少小时及分钟...
                    xlapp.Range("h" & j).FormulaR1C1 = "=TEXT(MOD(RC[-3]-RC[-1],1),""hh小时mm分"")"
                    xlapp.Range("h" & j).Value = xlapp.Range("h" & j).Value  '去除公式，写入值...

                    '如果白班不满1小时的加班，则判定没有加班,否则将小时数加分钟...
                    xlapp.Range("i" & j).FormulaR1C1 =
         "=IF(VALUE(MID(RC[-1],1,2))=0,0,VALUE(MID(RC[-1],1,2))+IF(VALUE(MID(RC[-1],LEN(RC[-1])-2,2))>=30,0.5,0)-0.5)"
                    xlapp.Range("i" & j).Value = xlapp.Range("i" & j).Value  '去除公式写入值...
                    '看是否早退、迟到、加班时长超过5.5小时，如果为以上任意一种情形，那么判断为非正常上班时间...
                    xlapp.Range("j" & j).FormulaR1C1 = "=IF(OR((RC[-4]-RC[-6])<0,(RC[-5]-RC[-3])<0,RC[-1]>5.5),""非正常上班时间"",""正常上班时间"")" : xlapp.Range("j" & j).Value = xlapp.Range("j" & j).Value
                    '如果为正常上班时间，且非星期1-5，那么将加班时间计算为8+有效加班时间...
                    xlapp.Range("k" & j).FormulaR1C1 = "=IF(AND(RC[-1]=""正常上班时间"",RC[-9]>5),8+RC[-2],RC[-2])"
                    xlapp.Range("k" & j).Value = xlapp.Range("k" & j).Value '去除公式，写入值...
                    If xlapp.Range("j" & j).Value = "非正常上班时间" Then  '如果是非正常上班时间,那么加班时间清零...
                        'xlapp.Range("j" & j).EntireRow.Interior.Color = 255
                        xlapp.Range("j" & j).EntireRow.Font.Color = -16776961
                    End If
                Else
                    '判定为夜班，那么...
                    xlapp.Range("f" & j).Value = "19:00" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
                    xlapp.Range("g" & j).Value = "3:30" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value
                    xlapp.Range("h" & j).FormulaR1C1 = "=TEXT(MOD(RC[-3]-RC[-1],1),""hh小时mm分"")" : xlapp.Range("h" & j).Value = xlapp.Range("h" & j).Value
                    xlapp.Range("i" & j).FormulaR1C1 =
         "=VALUE(MID(RC[-1],1,2))+IF(VALUE(MID(RC[-1],LEN(RC[-1])-2,2))>=30,0.5,0)"
                    xlapp.Range("i" & j).Value = xlapp.Range("i" & j).Value
                    xlapp.Range("j" & j).FormulaR1C1 = "=IF(OR((RC[-4]-RC[-6])<0,(RC[-5]-RC[-3])<0,RC[-1]>5.5),""非正常上班时间"",""正常上班时间"")" : xlapp.Range("j" & j).Value = xlapp.Range("j" & j).Value
                    xlapp.Range("k" & j).FormulaR1C1 = "=IF(AND(RC[-1]=""正常上班时间"",RC[-9]>5),8+RC[-2],RC[-2])"
                    xlapp.Range("k" & j).Value = xlapp.Range("k" & j).Value
                    '非正常上班时间，所在单元格整行填充颜色为红色...
                    If xlapp.Range("j" & j).Value = "非正常上班时间" Then
                        'xlapp.Range("j" & j).EntireRow.Interior.Color = 255
                        xlapp.Range("j" & j).EntireRow.Font.Color = -16776961
                    End If
                End If
            End If
        Next
        '如果是星期天加班时间，逐一剪切写入右偏移1格的单元格...
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            If xlapp.Range("b" & j).Value > 5 Then
                xlapp.Range("k" & j).Cut(xlapp.Range("l" & j))
            End If
        Next
        rs = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        '打开(创建)指定数据库表(文件基本信息)的记录集,第一参数数据库表名,第二参数数据库对象(已经打开指定的数据库连接),3参数使用的指定的游标类型,4参数是锁定类型,这里设置可操作记录的锁定类型
        rs.Open("调休与节假日", cnn, 1, 3)
        rs.MoveFirst    '移动到首条记录上
        '从第2行到最后一行遍历...
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            For i = 1 To rs.RecordCount                                                     '在1到记录数量上循环
                'If rs.Fields("日期").value.ToString Like "*" & xlapp.Range("a" & j).Value.ToString & "*" Then 
                '这个like是VB的语法不能用SQL语法 "%" & myId & "%"
                '如果数据库的日期（记录集字段日期）=所在单元格日期值，那么执行...
                If rs.Fields("日期").value.ToString = xlapp.Range("a" & j).Value.ToString Then
                    '如果数据库中的字段“分类”等于“调休日”，且员工已打卡上班...
                    If rs.Fields("分类").value.ToString = "调休日" And xlapp.Range("d" & j).Value.ToString <> "" Then
                        xlapp.Range("k" & j).Value = xlapp.Range("l" & j).Value - 8 '调休日（一般为星期6、日）减去8小时...
                        xlapp.Range("l" & j).Value = ""   '清空原星期6，7加班时间，并退出For循环...
                        Exit For
                        '如果匹配的数据库字段“分类”为节假日，且3倍上班...
                    ElseIf rs.Fields("分类").value.ToString = "节假日" And rs.Fields("加班倍数").value = 3 And xlapp.Range("d" & j).Value.ToString <> "" Then
                        '三倍有效加班时间+8个小时...
                        xlapp.Range("m" & j).Value = xlapp.Range("i" & j).Value + 8
                        xlapp.Range("k" & j).Value = "" : xlapp.Range("l" & j).Value = "" : Exit For  '清空原先的加班时间，并退出循环...
                        '如果日期节假日为2倍上班...
                    ElseIf rs.Fields("分类").value.ToString = "节假日" And rs.Fields("加班倍数").value = 2 And xlapp.Range("d" & j).Value.ToString <> "" Then
                        xlapp.Range("l" & j).Value = xlapp.Range("i" & j).Value + 8  '有效加班时间+8个小时...
                        xlapp.Range("k" & j).Value = "" : Exit For  '清空前期预留的加班时间...
                    End If
                End If          '结束判断语句
                rs.MoveNext '移动到下一条记录
            Next i  '循环
            i = 0
            rs.MoveFirst    '移动到首条记录上
        Next
        '从第2行到最后一行遍历，统计加班时间
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            If xlapp.Range("j" & j).Value = "非正常上班时间" Or xlapp.Range("k" & j).Value < 0 Then
                xlapp.Range("k" & j).Value = "" : xlapp.Range("l" & j).Value = "" : xlapp.Range("m" & j).Value = ""
            End If
            dan = dan + xlapp.Range("k" & j).Value
            shuang = shuang + xlapp.Range("l" & j).Value
            san = san + xlapp.Range("m" & j).Value
        Next

        '从第2行到最后一行遍历，填充星期6，7单元格颜色...
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            If xlapp.Range("b" & j).Value > 5 Then
                xlapp.Range("b" & j).Offset(0, -1).Resize(1, 13).Interior.ThemeColor = 10
                xlapp.Range("b" & j).Offset(0, -1).Resize(1, 13).Interior.TintAndShade = 0.399975585192419
            End If
        Next
        xlapp.Range("a2").CurrentRegion.Borders.LineStyle = 1       '加框线
        xlapp.Range("k" & bytCounter + 2).Value = "单  " & dan & "小时"
        xlapp.Range("l" & bytCounter + 2).Value = "双  " & shuang & "小时"
        xlapp.Range("m" & bytCounter + 2).Value = "三  " & san & "小时"
        xlapp.ScreenUpdating = True    '禁止屏幕刷新，提升工作效率


        ''.......................................................................................
        'xlapp.OnUndo("撤消[同列相同数据合并]", "撤消") '这里代码调用的是FV.xlam加载项的撤销方法

        ''.......................................................................................

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As RibbonControlEventArgs) Handles Button1.Click

        '.... 7点30分上班 代码开始...

        On Error Resume Next    '没有这一句数据库(记录集)测试错误...
        Dim myData As String, myArray() As String， rs As Object       '声明变量,数据库路径
        Dim i As Byte = 0, rngSelection As Excel.Range, bytCounter As Byte, dan As Single, shuang As Single, san As Single '声明变量
        Dim rng3 As Excel.Range, rng4 As Excel.Range, k As Integer = 0, j As Byte = 0
        xlapp.ScreenUpdating = False    '禁止屏幕刷新，提升工作效率
        myData = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb"  '指定数据库名称，三星笔记本本地测试
        Dim bytCounter1 As Byte = 0

        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        'Call 备份(Targetsht, TargetRng)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True  '这里代码调用的是VSTO EXCEL加载项的撤销方法
        ''____________________备份数据、记录区域___________________________
        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True


        '("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb")  '公司共享盘
        'myData = "F:\2 笔记记录\8 过程信息管理\文件管理\文件管理.accdb"  '家里台式机测试
        'myData = "D:\2 笔记记录\0 过程信息管理笔记\文件管理\文件管理.accdb" '三星笔记本本地测试
        '给变量赋值为一维数组,改数组变量是公共变量
        myArray = {"分类", "日期", "加班倍数", "备注"}
        '建立与数据库的连接,创建数据库连接对象(ADO的最顶层),这里还没指定数据库连接,打开指定数据库
        cnn = CreateObject("adodb.Connection")
        '引用cnn(ado最顶层对象)
        With cnn    '引用数据库连接对象
            .Provider = "microsoft.Ace.OLEDB.12.0"   '指定数据库引擎提供者是Access
            .Open(myData)                            '建立指定的数据库连接
        End With    '结束语句
        rngSelection = xlapp.Selection  '将选择区域赋值给变量...
        bytCounter = rngSelection.Rows.Count + 1    '从第2行单元格开始选择区域的总行数+第1行的数量赋值给变量..
        xlapp.Columns("f:o").Delete     '删除f:o列...
        'xlapp.Range("d1:d100").Cut(xlapp.Range("dd1"))  '剪切后，粘贴到目标单元格（B2）...
        'xlapp.Columns("D:D").NumberFormatLocal = "@"
        'xlapp.Range("dd1:dd100").Cut(xlapp.Range("d1"))

        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count '从第2行到最后一行上遍历写入公式...
            xlapp.Cells(j, 200).FormulaR1C1 = "=SUBSTITUTE(RC[-199],""."",""/"")"  '写入公式，用“/”代替“.”。
            xlapp.Cells(j, 200).Value = xlapp.Cells(j, 200).Value  '去除公式，只写入值.
        Next
        xlapp.Cells(2, 200).CurrentRegion.Cut(xlapp.Range("a2")) '剪切已替换完成的日期值，写入到A2单元格为起点...
        xlapp.Range("aa2:aa" & bytCounter).FormulaR1C1 = "=WEEKDAY(RC[-26],2)"  '写入公式（不需要遍历，公式可以相对引用单元格），获取星期...
        xlapp.Range("aa2:aa" & bytCounter).Value = xlapp.Range("aa2:aa" & bytCounter).Value  '去除公式，单元格只写入值...
        xlapp.Range("aa2").CurrentRegion.Cut(xlapp.Range("b2"))  '剪切后，粘贴到目标单元格（B2）...
        '数组值批量写入到单元格中...
        xlapp.Range("f1:m1").Value = {"正常上班时间", "正常下班时间", "超8小时时长", "超8小时有效时长", "是否正常", "单总时长", "双总时长", "三总时长"}
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count  '从第2行到最后一行遍历...
            If xlapp.Range("d" & j).Value <> "" Then  '如果不为空值（上班打卡...）
                'If Hour(xlapp.Range("d" & j).Value) < 17 Then  '小于5点上班，即判定为白班工作时间...
                If xlapp.Range("d" & j).Value < 0.71 Then  '小于5点上班，即判定为白班工作时间...
                    '写入上班开始时间，由公式转换成值...
                    xlapp.Range("f" & j).Value = "7:30" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
                    xlapp.Range("g" & j).Value = "16:30" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value

                    '写入公式，上下班时间间隔多少小时及分钟...
                    xlapp.Range("h" & j).FormulaR1C1 = "=TEXT(MOD(RC[-3]-RC[-1],1),""hh小时mm分"")"
                    xlapp.Range("h" & j).Value = xlapp.Range("h" & j).Value  '去除公式，写入值...

                    '如果白班不满1小时的加班，则判定没有加班,否则将小时数加分钟...
                    xlapp.Range("i" & j).FormulaR1C1 =
         "=IF(VALUE(MID(RC[-1],1,2))=0,0,VALUE(MID(RC[-1],1,2))+IF(VALUE(MID(RC[-1],LEN(RC[-1])-2,2))>=30,0.5,0)-0.5)"
                    xlapp.Range("i" & j).Value = xlapp.Range("i" & j).Value  '去除公式写入值...
                    '看是否早退、迟到、加班时长超过5.5小时，如果为以上任意一种情形，那么判断为非正常上班时间...
                    xlapp.Range("j" & j).FormulaR1C1 = "=IF(OR((RC[-4]-RC[-6])<0,(RC[-5]-RC[-3])<0,RC[-1]>5.5),""非正常上班时间"",""正常上班时间"")" : xlapp.Range("j" & j).Value = xlapp.Range("j" & j).Value
                    '如果为正常上班时间，且非星期1-5，那么将加班时间计算为8+有效加班时间...
                    xlapp.Range("k" & j).FormulaR1C1 = "=IF(AND(RC[-1]=""正常上班时间"",RC[-9]>5),8+RC[-2],RC[-2])"
                    xlapp.Range("k" & j).Value = xlapp.Range("k" & j).Value '去除公式，写入值...
                    If xlapp.Range("j" & j).Value = "非正常上班时间" Then  '如果是非正常上班时间,那么加班时间清零...
                        'xlapp.Range("j" & j).EntireRow.Interior.Color = 255
                        xlapp.Range("j" & j).EntireRow.Font.Color = -16776961
                    End If
                Else
                    bytCounter1 = bytCounter1 + 1
                    '判定为夜班，那么...
                    'xlapp.Range("f" & j).Value = "19:00" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
                    'xlapp.Range("g" & j).Value = "3:30" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value

                    xlapp.Range("f" & j).Value = "19:00" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
                    xlapp.Range("g" & j).Value = "3:30" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value
                    xlapp.Range("h" & j).FormulaR1C1 = "=TEXT(MOD(RC[-3]-RC[-1],1),""hh小时mm分"")" : xlapp.Range("h" & j).Value = xlapp.Range("h" & j).Value
                    xlapp.Range("i" & j).FormulaR1C1 =
         "=VALUE(MID(RC[-1],1,2))+IF(VALUE(MID(RC[-1],LEN(RC[-1])-2,2))>=30,0.5,0)"
                    xlapp.Range("i" & j).Value = xlapp.Range("i" & j).Value
                    xlapp.Range("j" & j).FormulaR1C1 = "=IF(OR((RC[-4]-RC[-6])<0,(RC[-5]-RC[-3])<0,RC[-1]>5.5),""非正常上班时间"",""正常上班时间"")" : xlapp.Range("j" & j).Value = xlapp.Range("j" & j).Value
                    xlapp.Range("k" & j).FormulaR1C1 = "=IF(AND(RC[-1]=""正常上班时间"",RC[-9]>5),8+RC[-2],RC[-2])"
                    xlapp.Range("k" & j).Value = xlapp.Range("k" & j).Value
                    '非正常上班时间，所在单元格整行填充颜色为红色...
                    If xlapp.Range("j" & j).Value = "非正常上班时间" Then
                        'xlapp.Range("j" & j).EntireRow.Interior.Color = 255
                        xlapp.Range("j" & j).EntireRow.Font.Color = -16776961
                    End If
                End If
            End If
        Next
        '如果是星期天加班时间，逐一剪切写入右偏移1格的单元格...
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            If xlapp.Range("b" & j).Value > 5 Then
                xlapp.Range("k" & j).Cut(xlapp.Range("l" & j))
            End If
        Next

        rs = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        '打开(创建)指定数据库表(文件基本信息)的记录集,第一参数数据库表名,第二参数数据库对象(已经打开指定的数据库连接),3参数使用的指定的游标类型,4参数是锁定类型,这里设置可操作记录的锁定类型
        rs.Open("调休与节假日", cnn, 1, 3)
        rs.MoveFirst    '移动到首条记录上
        '从第2行到最后一行遍历...
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            For i = 1 To rs.RecordCount                                                     '在1到记录数量上循环
                'If rs.Fields("日期").value.ToString Like "*" & xlapp.Range("a" & j).Value.ToString & "*" Then 
                '这个like是VB的语法不能用SQL语法 "%" & myId & "%"
                '如果数据库的日期（记录集字段日期）=所在单元格日期值，那么执行...
                If rs.Fields("日期").value.ToString = xlapp.Range("a" & j).Value.ToString Then
                    '如果数据库中的字段“分类”等于“调休日”，且员工已打卡上班...
                    If rs.Fields("分类").value.ToString = "调休日" And xlapp.Range("d" & j).Value.ToString <> "" Then
                        xlapp.Range("k" & j).Value = xlapp.Range("l" & j).Value - 8 '调休日（一般为星期6、日）减去8小时...
                        xlapp.Range("l" & j).Value = ""   '清空原星期6，7加班时间，并退出For循环...
                        Exit For
                        '如果匹配的数据库字段“分类”为节假日，且3倍上班...
                    ElseIf rs.Fields("分类").value.ToString = "节假日" And rs.Fields("加班倍数").value = 3 And xlapp.Range("d" & j).Value.ToString <> "" Then
                        '三倍有效加班时间+8个小时...
                        xlapp.Range("m" & j).Value = xlapp.Range("i" & j).Value + 8
                        xlapp.Range("k" & j).Value = "" : xlapp.Range("l" & j).Value = "" : Exit For  '清空原先的加班时间，并退出循环...
                        '如果日期节假日为2倍上班...
                    ElseIf rs.Fields("分类").value.ToString = "节假日" And rs.Fields("加班倍数").value = 2 And xlapp.Range("d" & j).Value.ToString <> "" Then
                        xlapp.Range("l" & j).Value = xlapp.Range("i" & j).Value + 8  '有效加班时间+8个小时...
                        xlapp.Range("k" & j).Value = "" : Exit For  '清空前期预留的加班时间...
                    End If
                End If          '结束判断语句
                rs.MoveNext '移动到下一条记录
            Next i  '循环
            i = 0
            rs.MoveFirst    '移动到首条记录上
        Next
        '从第2行到最后一行遍历，统计加班时间
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            If xlapp.Range("j" & j).Value = "非正常上班时间" Or xlapp.Range("k" & j).Value < 0 Then
                xlapp.Range("k" & j).Value = "" : xlapp.Range("l" & j).Value = "" : xlapp.Range("m" & j).Value = ""
            End If
            dan = dan + xlapp.Range("k" & j).Value
            shuang = shuang + xlapp.Range("l" & j).Value
            san = san + xlapp.Range("m" & j).Value
        Next

        '从第2行到最后一行遍历，填充星期6，7单元格颜色...
        For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
            If xlapp.Range("b" & j).Value > 5 Then
                xlapp.Range("b" & j).Offset(0, -1).Resize(1, 13).Interior.ThemeColor = 9
                xlapp.Range("b" & j).Offset(0, -1).Resize(1, 13).Interior.TintAndShade = 0.399975585192419
            End If
        Next
        xlapp.Range("a2").CurrentRegion.Borders.LineStyle = 1       '加框线
        xlapp.Range("k" & bytCounter + 1).Value = "单  " & dan & "小时"
        xlapp.Range("l" & bytCounter + 1).Value = "双  " & shuang & "小时"
        xlapp.Range("m" & bytCounter + 1).Value = "三  " & san & "小时"

        xlapp.Range("k" & bytCounter + 2 & ":" & "m" & bytCounter + 2).Merge()
        xlapp.Range("k" & bytCounter + 2).Value = "夜班天数共计:" & bytCounter1 & "天"


        xlapp.ScreenUpdating = True    '禁止屏幕刷新，提升工作效率

        ''.......................................................................................
        'xlapp.OnUndo("撤消[同列相同数据合并]", "撤消") '这里代码调用的是FV.xlam加载项的撤销方法

        ''.......................................................................................
        '.... 7点30分上班 代码结束戳记...






        ''....8点开始上班代码.....

        'On Error Resume Next    '没有这一句数据库(记录集)测试错误...
        'Dim myData As String, myArray() As String， rs As Object       '声明变量,数据库路径
        'Dim i As Byte = 0, rngSelection As Excel.Range, bytCounter As Byte, dan As Single, shuang As Single, san As Single '声明变量
        'Dim rng3 As Excel.Range, rng4 As Excel.Range, k As Integer = 0, j As Byte = 0
        'xlapp.ScreenUpdating = False    '禁止屏幕刷新，提升工作效率
        'myData = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb"  '指定数据库名称，三星笔记本本地测试
        'Dim bytCounter1 As Byte = 0

        '''____________________备份数据、记录区域___________________________
        ''Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        ''TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        ''Call 备份(Targetsht, TargetRng)
        ''Globals.Ribbons.Ribbon1.btnUndo.Enabled = True  '这里代码调用的是VSTO EXCEL加载项的撤销方法
        '''____________________备份数据、记录区域___________________________

        '' ============================================================
        '' ★★★ 第1步：备份数据（用于撤销） ★★★
        '' ============================================================
        'M2_调用的任务.BackupActiveSheet()
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True

        ''("Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\人力资源管理.accdb")  '公司共享盘
        ''myData = "F:\2 笔记记录\8 过程信息管理\文件管理\文件管理.accdb"  '家里台式机测试
        ''myData = "D:\2 笔记记录\0 过程信息管理笔记\文件管理\文件管理.accdb" '三星笔记本本地测试
        ''给变量赋值为一维数组,改数组变量是公共变量
        'myArray = {"分类", "日期", "加班倍数", "备注"}
        ''建立与数据库的连接,创建数据库连接对象(ADO的最顶层),这里还没指定数据库连接,打开指定数据库
        'cnn = CreateObject("adodb.Connection")
        ''引用cnn(ado最顶层对象)
        'With cnn    '引用数据库连接对象
        '    .Provider = "microsoft.Ace.OLEDB.12.0"   '指定数据库引擎提供者是Access
        '    .Open(myData)                            '建立指定的数据库连接
        'End With    '结束语句
        'rngSelection = xlapp.Selection  '将选择区域赋值给变量...
        'bytCounter = rngSelection.Rows.Count + 1    '从第2行单元格开始选择区域的总行数+第1行的数量赋值给变量..
        'xlapp.Columns("f:o").Delete     '删除f:o列...
        ''xlapp.Range("d1:d100").Cut(xlapp.Range("dd1"))  '剪切后，粘贴到目标单元格（B2）...
        ''xlapp.Columns("D:D").NumberFormatLocal = "@"
        ''xlapp.Range("dd1:dd100").Cut(xlapp.Range("d1"))

        'For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count '从第2行到最后一行上遍历写入公式...
        '    xlapp.Cells(j, 200).FormulaR1C1 = "=SUBSTITUTE(RC[-199],""."",""/"")"  '写入公式，用“/”代替“.”。
        '    xlapp.Cells(j, 200).Value = xlapp.Cells(j, 200).Value  '去除公式，只写入值.
        'Next
        'xlapp.Cells(2, 200).CurrentRegion.Cut(xlapp.Range("a2")) '剪切已替换完成的日期值，写入到A2单元格为起点...
        'xlapp.Range("aa2:aa" & bytCounter).FormulaR1C1 = "=WEEKDAY(RC[-26],2)"  '写入公式（不需要遍历，公式可以相对引用单元格），获取星期...
        'xlapp.Range("aa2:aa" & bytCounter).Value = xlapp.Range("aa2:aa" & bytCounter).Value  '去除公式，单元格只写入值...
        'xlapp.Range("aa2").CurrentRegion.Cut(xlapp.Range("b2"))  '剪切后，粘贴到目标单元格（B2）...
        ''数组值批量写入到单元格中...
        'xlapp.Range("f1:m1").Value = {"正常上班时间", "正常下班时间", "超8小时时长", "超8小时有效时长", "是否正常", "单总时长", "双总时长", "三总时长"}
        'For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count  '从第2行到最后一行遍历...
        '    If xlapp.Range("d" & j).Value <> "" Then  '如果不为空值（上班打卡...）
        '        'If Hour(xlapp.Range("d" & j).Value) < 17 Then  '小于5点上班，即判定为白班工作时间...
        '        If xlapp.Range("d" & j).Value < 0.71 Then  '小于5点上班，即判定为白班工作时间...
        '            '写入上班开始时间，由公式转换成值...
        '            xlapp.Range("f" & j).Value = "8:00" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
        '            xlapp.Range("g" & j).Value = "17:00" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value

        '            '写入公式，上下班时间间隔多少小时及分钟...
        '            xlapp.Range("h" & j).FormulaR1C1 = "=TEXT(MOD(RC[-3]-RC[-1],1),""hh小时mm分"")"
        '            xlapp.Range("h" & j).Value = xlapp.Range("h" & j).Value  '去除公式，写入值...

        '            '如果白班不满1小时的加班，则判定没有加班,否则将小时数加分钟...
        '            xlapp.Range("i" & j).FormulaR1C1 =
        ' "=IF(VALUE(MID(RC[-1],1,2))=0,0,VALUE(MID(RC[-1],1,2))+IF(VALUE(MID(RC[-1],LEN(RC[-1])-2,2))>=30,0.5,0)-0.5)"
        '            xlapp.Range("i" & j).Value = xlapp.Range("i" & j).Value  '去除公式写入值...
        '            '看是否早退、迟到、加班时长超过5.5小时，如果为以上任意一种情形，那么判断为非正常上班时间...
        '            xlapp.Range("j" & j).FormulaR1C1 = "=IF(OR((RC[-4]-RC[-6])<0,(RC[-5]-RC[-3])<0,RC[-1]>5.5),""非正常上班时间"",""正常上班时间"")" : xlapp.Range("j" & j).Value = xlapp.Range("j" & j).Value
        '            '如果为正常上班时间，且非星期1-5，那么将加班时间计算为8+有效加班时间...
        '            xlapp.Range("k" & j).FormulaR1C1 = "=IF(AND(RC[-1]=""正常上班时间"",RC[-9]>5),8+RC[-2],RC[-2])"
        '            xlapp.Range("k" & j).Value = xlapp.Range("k" & j).Value '去除公式，写入值...
        '            If xlapp.Range("j" & j).Value = "非正常上班时间" Then  '如果是非正常上班时间,那么加班时间清零...
        '                'xlapp.Range("j" & j).EntireRow.Interior.Color = 255
        '                xlapp.Range("j" & j).EntireRow.Font.Color = -16776961
        '            End If
        '        Else
        '            bytCounter1 = bytCounter1 + 1
        '            '判定为夜班，那么...
        '            'xlapp.Range("f" & j).Value = "19:00" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
        '            'xlapp.Range("g" & j).Value = "3:30" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value

        '            xlapp.Range("f" & j).Value = "19:00" : xlapp.Range("f" & j).Value = xlapp.Range("f" & j).Value
        '            xlapp.Range("g" & j).Value = "3:30" : xlapp.Range("g" & j).Value = xlapp.Range("g" & j).Value
        '            xlapp.Range("h" & j).FormulaR1C1 = "=TEXT(MOD(RC[-3]-RC[-1],1),""hh小时mm分"")" : xlapp.Range("h" & j).Value = xlapp.Range("h" & j).Value
        '            xlapp.Range("i" & j).FormulaR1C1 =
        ' "=VALUE(MID(RC[-1],1,2))+IF(VALUE(MID(RC[-1],LEN(RC[-1])-2,2))>=30,0.5,0)"
        '            xlapp.Range("i" & j).Value = xlapp.Range("i" & j).Value
        '            xlapp.Range("j" & j).FormulaR1C1 = "=IF(OR((RC[-4]-RC[-6])<0,(RC[-5]-RC[-3])<0,RC[-1]>5.5),""非正常上班时间"",""正常上班时间"")" : xlapp.Range("j" & j).Value = xlapp.Range("j" & j).Value
        '            xlapp.Range("k" & j).FormulaR1C1 = "=IF(AND(RC[-1]=""正常上班时间"",RC[-9]>5),8+RC[-2],RC[-2])"
        '            xlapp.Range("k" & j).Value = xlapp.Range("k" & j).Value
        '            '非正常上班时间，所在单元格整行填充颜色为红色...
        '            If xlapp.Range("j" & j).Value = "非正常上班时间" Then
        '                'xlapp.Range("j" & j).EntireRow.Interior.Color = 255
        '                xlapp.Range("j" & j).EntireRow.Font.Color = -16776961
        '            End If
        '        End If
        '    End If
        'Next
        ''如果是星期天加班时间，逐一剪切写入右偏移1格的单元格...
        'For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
        '    If xlapp.Range("b" & j).Value > 5 Then
        '        xlapp.Range("k" & j).Cut(xlapp.Range("l" & j))
        '    End If
        'Next

        'rs = CreateObject("ADODB.Recordset")   '创建一个无信息的记录集对象,方便引用
        ''打开(创建)指定数据库表(文件基本信息)的记录集,第一参数数据库表名,第二参数数据库对象(已经打开指定的数据库连接),3参数使用的指定的游标类型,4参数是锁定类型,这里设置可操作记录的锁定类型
        'rs.Open("调休与节假日", cnn, 1, 3)
        'rs.MoveFirst    '移动到首条记录上
        ''从第2行到最后一行遍历...
        'For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
        '    For i = 1 To rs.RecordCount                                                     '在1到记录数量上循环
        '        'If rs.Fields("日期").value.ToString Like "*" & xlapp.Range("a" & j).Value.ToString & "*" Then 
        '        '这个like是VB的语法不能用SQL语法 "%" & myId & "%"
        '        '如果数据库的日期（记录集字段日期）=所在单元格日期值，那么执行...
        '        If rs.Fields("日期").value.ToString = xlapp.Range("a" & j).Value.ToString Then
        '            '如果数据库中的字段“分类”等于“调休日”，且员工已打卡上班...
        '            If rs.Fields("分类").value.ToString = "调休日" And xlapp.Range("d" & j).Value.ToString <> "" Then
        '                xlapp.Range("k" & j).Value = xlapp.Range("l" & j).Value - 8 '调休日（一般为星期6、日）减去8小时...
        '                xlapp.Range("l" & j).Value = ""   '清空原星期6，7加班时间，并退出For循环...
        '                Exit For
        '                '如果匹配的数据库字段“分类”为节假日，且3倍上班...
        '            ElseIf rs.Fields("分类").value.ToString = "节假日" And rs.Fields("加班倍数").value = 3 And xlapp.Range("d" & j).Value.ToString <> "" Then
        '                '三倍有效加班时间+8个小时...
        '                xlapp.Range("m" & j).Value = xlapp.Range("i" & j).Value + 8
        '                xlapp.Range("k" & j).Value = "" : xlapp.Range("l" & j).Value = "" : Exit For  '清空原先的加班时间，并退出循环...
        '                '如果日期节假日为2倍上班...
        '            ElseIf rs.Fields("分类").value.ToString = "节假日" And rs.Fields("加班倍数").value = 2 And xlapp.Range("d" & j).Value.ToString <> "" Then
        '                xlapp.Range("l" & j).Value = xlapp.Range("i" & j).Value + 8  '有效加班时间+8个小时...
        '                xlapp.Range("k" & j).Value = "" : Exit For  '清空前期预留的加班时间...
        '            End If
        '        End If          '结束判断语句
        '        rs.MoveNext '移动到下一条记录
        '    Next i  '循环
        '    i = 0
        '    rs.MoveFirst    '移动到首条记录上
        'Next
        ''从第2行到最后一行遍历，统计加班时间
        'For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
        '    If xlapp.Range("j" & j).Value = "非正常上班时间" Or xlapp.Range("k" & j).Value < 0 Then
        '        xlapp.Range("k" & j).Value = "" : xlapp.Range("l" & j).Value = "" : xlapp.Range("m" & j).Value = ""
        '    End If
        '    dan = dan + xlapp.Range("k" & j).Value
        '    shuang = shuang + xlapp.Range("l" & j).Value
        '    san = san + xlapp.Range("m" & j).Value
        'Next

        ''从第2行到最后一行遍历，填充星期6，7单元格颜色...
        'For j = 2 To xlapp.Range("a1").CurrentRegion.Rows.Count
        '    If xlapp.Range("b" & j).Value > 5 Then
        '        xlapp.Range("b" & j).Offset(0, -1).Resize(1, 13).Interior.ThemeColor = 9
        '        xlapp.Range("b" & j).Offset(0, -1).Resize(1, 13).Interior.TintAndShade = 0.399975585192419
        '    End If
        'Next
        'xlapp.Range("a2").CurrentRegion.Borders.LineStyle = 1       '加框线
        'xlapp.Range("k" & bytCounter + 1).Value = "单  " & dan & "小时"
        'xlapp.Range("l" & bytCounter + 1).Value = "双  " & shuang & "小时"
        'xlapp.Range("m" & bytCounter + 1).Value = "三  " & san & "小时"

        'xlapp.Range("k" & bytCounter + 2 & ":" & "m" & bytCounter + 2).Merge()
        'xlapp.Range("k" & bytCounter + 2).Value = "夜班天数共计:" & bytCounter1 & "天"


        'xlapp.ScreenUpdating = True    '禁止屏幕刷新，提升工作效率

        '''.......................................................................................
        ''xlapp.OnUndo("撤消[同列相同数据合并]", "撤消") '这里代码调用的是FV.xlam加载项的撤销方法

        '''.......................................................................................


        ''........... 8点开始上班代码结束...................
    End Sub

    Private Sub btnFrequency_Click(sender As Object, e As RibbonControlEventArgs) Handles btnFrequency.Click

        '.....................教程调用 起始
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/ForFv_frequency")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/ForFv_frequency") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If
        '.....................教程调用 结束

        Dim f As New WIN190119_Frequency  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnFrequency.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnDataCollect_Click(sender As Object, e As RibbonControlEventArgs) Handles btnDataCollect.Click
        '.....................教程调用 起始
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/ForFv_SelectionCells")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/ForFv_SelectionCells") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If
        '.....................教程调用 结束

        Dim f As New WIN190119_数据选择  '实例化一个类的对象
        f.Show() '调用类对象的属性
        'btnDataCollect.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub



    Private Sub Button2_Click(sender As Object, e As RibbonControlEventArgs) Handles Button2.Click
        Dim f As New G02_质量花费查询 '实例化一个类的对象
        f.Show() '调用类对象的属性
        'btnDataCollect.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub


    ''' <summary>连续3个撇号，并放置在方法上端使用。
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btn标示重复值_Click(sender As Object, e As RibbonControlEventArgs) Handles btn标示重复值.Click
        If TypeName(xlapp.Selection) <> "Range" Then Exit Sub  '如果选择对象不是单元格则退出
        If xlapp.Selection.Count < 3 Then MsgBox("请选区一个较大的非空区域再执行", vbInformation, "提示") : Exit Sub  '选区太小则退出
        'If xlapp.Selection.Count < 3 Then MessageBox.Show("请选区一个较大的非空区域再执行", "提示", vbInformation) : Exit Sub  '选区太小则退出
        If xlapp.Selection.Areas.Count > 1 Then MsgBox("仅对一个区域生效。", vbInformation, "提示") : Exit Sub  '如果选择多个区域则退出
        If xlapp.Selection.Rows.Count = xlapp.Rows.Count Or xlapp.Selection.Columns.Count = xlapp.Columns.Count Then MsgBox("请不要选择整行整列!", vbInformation, "提示") : Exit Sub '如果选择了整行或者整列则结束过程
        xlapp.ScreenUpdating = False  '关闭屏幕更新
        xlapp.Calculation = -4135  '手动计算
        Dim rng As Excel.Range, i As Long, rngg As Excel.Range     '声明变量
        i = 0 '对i赋与默认值0
        On Error Resume Next  '如果有错继续执行下一句
        rngg = xlapp.Intersect(xlapp.ActiveSheet.UsedRange, xlapp.Selection)  '将选区与已用区域的交集赋值给变量
        rngg.Interior.ColorIndex = -4142  '清除原有的背景颜色
        '通过循环在原字符后面加一个“︼”，如果大于15位，在前面加一个“'”。用途是避免15位以上的数字包括身份证号在计算重复时出错，同时也避免最后删除“︼”后以科学记数形式显示，从而保护数据不被破坏
        For Each cell In rngg  '遍历所有单元格
            If Len(cell) > 0 Then cell.Value = IIf(Len(cell.Text) > 14, "'", "") & cell.Text & "︼"  '如果数据非空则添加前后缀“'”和“︼”
        Next
        For Each rng In rngg  '再次遍历单元格
            If Len(rng) > 0 Then '如果rng的字符数量大于0
                If xlapp.WorksheetFunction.CountIf(rngg, rng.Text) > 1 Then  '如果单元格rng在整个区域中不止一个
                    '用IV1：IV100做辅助区，存放重复值。
                    If xlapp.WorksheetFunction.CountIf(xlapp.Cells(1, xlapp.Columns.Count).Resize(54, 1), rng.Text) = 0 Then  '如果在辅助区中没有单元格rng的值
                        i = i + 1   '那么累加变量 ,该变量等于重复值的个数
                        xlapp.Cells(i, xlapp.Columns.Count) = rng.Text  '在最后一列存放重复值
                    End If
                    '对rng单元格设置背景色，颜色值为rng的值在IV列辅助区中的排位+2。加2是需要排除黑色和白色
                    rng.Interior.ColorIndex = 2 + xlapp.WorksheetFunction.Match(rng, xlapp.Cells(1, xlapp.Columns.Count).Resize(54, 1), 0)
                End If
                If i > 54 Then Exit For  '当i大于54时，退出循环（Excel 2003仅56种颜色，除黑白色外只有54色）
            End If
        Next rng
        rngg.Replace("︼", "", 2)  '将后缀删除
        xlapp.Cells(1, xlapp.Columns.Count).Resize(54, 1).Clear  '清除辅助列
        xlapp.ScreenUpdating = True  '恢复屏幕刷新
        xlapp.Calculation = -4105  '自动计算
    End Sub

    Private Sub btnCompare_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCompare.Click
        Dim f As New WIN190324_重复数据分析提取 '实例化一个类的对象
        f.Show() '调用类对象的属性
        'btnCompare.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnCertifcate_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCertifcate.Click
        Dim f As New H01_产品质量证明书管理 '实例化一个类的对象
        f.Show() '调用类对象的属性
        'btnCompare.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnCertificateOutput_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCertificateOutput.Click
        Dim f As New H02_产品证明书信息导出 '实例化一个类的对象
        f.Show() '调用类对象的属性
        'btnCompare.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub


#Region "生成二维码"
    '生成二维码的属性
    Shared Function MakeQRE(ByVal qrtext As String, Optional ByVal width As Integer = 200, Optional ByVal height As Integer = 200, Optional ByVal margin As Integer = 1) As Bitmap
        Dim writer As New ZXing.BarcodeWriter       '新建一个图像智能类
        writer.Format = ZXing.BarcodeFormat.QR_CODE         '智能类图像格式设置为二维码
        Dim opt As New ZXing.QrCode.QrCodeEncodingOptions   '创建一个二维码操作对象
        opt.DisableECI = True      '设置为True才可以调整编码
        opt.CharacterSet = "UTF-8" '文本编码，建议设置为UTF-8,手机也可以扫.默认为ISO-8859-1英文字符集，但一般移动设备常用UTF-8字符集编码
        opt.Width = width   '宽度
        opt.Height = height '高度
        opt.Margin = margin  '边距，貌似不是像素格式，因此不宜设置过大
        writer.Options = opt   '设置用于编码的选项容器
        Return writer.Write(qrtext) '内容写入智能类
    End Function
    '生成二维码
    Private Sub Button20_Click(sender As Object, e As RibbonControlEventArgs) Handles Button20.Click
        Dim rngRng As Excel.Range
        rngRng = xlapp.Range("a1")
        Dim btmBtm As Bitmap = MakeQRE(rngRng.Value, , , 1)
        btmBtm.Save（"C:\Users\WF\Desktop\btmBtm1.bmp"）
        'MsgBox(TypeName(btmBtm))
    End Sub

    '读取二维码
    Private Sub Button38_Click(sender As Object, e As RibbonControlEventArgs) Handles Button38.Click
        Dim rngEvalutRange As Excel.Range
        Dim btmBtm As Bitmap = New Bitmap("C:\Users\WF\Desktop\btmBtm1.bmp")    '声明一个图形对象(指定完整路径)
        rngEvalutRange = xlapp.Range("a1")                                      '声明单元格对象
        rngEvalutRange.Value = ReadQR(btmBtm)                                   '调用函数过程的结果返回值写入指定的单元格内
    End Sub

    '读取二维码
    Shared Function ReadQR(ByVal bmp As Bitmap) As String
        Dim reader As New ZXing.BarcodeReader            '新建一个图像智能类
        reader.Options.CharacterSet = "UTF-8"            '文本编码，建议设置为UTF-8,手机也可以扫.默认为ISO-8859-1英文字符集，移动设备常用UTF-8字符集编码
        Dim ret As ZXing.Result = reader.Decode(bmp)     '声明一个用于读取器的对象(二维码图片)并赋值给变量.
        If ret Is Nothing Then                           '如果读取不到指定二维码图片
            Return Nothing                               '函数过程值返回Nothing
        Else                                             '否则
            Return ret.Text                              '返回图片内容(读取二维码)
        End If
    End Function



    '生成条形码
    Private Sub Button39_Click(sender As Object, e As RibbonControlEventArgs) Handles Button39.Click
        'Dim btmBtm As Bitmap = MakeQRT(".14738285.43781.SL19J22101", , , 1)    '声明图形对象
        'btmBtm.Save（"C:\Users\WF\Desktop\myBitmap2.bmp"）  '图片保存到指定路径

        Dim f As New WIN191102_条形码
        f.Show()


    End Sub

    '生成条形码
    Shared Function MakeQRT(ByVal qrtext As String, Optional ByVal width As Integer = 230, Optional ByVal height As Integer = 90, Optional ByVal margin As Integer = 1) As Bitmap
        Dim writer As New ZXing.BarcodeWriter             '新建一个图像智能类
        writer.Format = ZXing.BarcodeFormat.CODE_128      '智能类图像格式设置为二维码
        Dim opt As New ZXing.QrCode.QrCodeEncodingOptions '创建一个二维码操作对象
        opt.DisableECI = True                             '设置为True才可以调整编码
        opt.CharacterSet = "UTF-8"                        '文本编码，建议设置为UTF-8
        opt.Width = width    '宽度
        opt.Height = height  '高度
        opt.Margin = margin  '边距，貌似不是像素格式，因此不宜设置过大
        writer.Options = opt '设置用于编码的选项容器
        Return writer.Write(qrtext) '内容写入智能类
    End Function

    '读取条形码
    Private Sub Button40_Click(sender As Object, e As RibbonControlEventArgs) Handles Button40.Click
        Dim rngEvalutRange As Excel.Range
        Dim btmBtm As Bitmap = New Bitmap("C:\Users\WF\Desktop\myBitmap2.bmp")
        rngEvalutRange = xlapp.Range("a1")
        rngEvalutRange.Value = ReadQR(btmBtm)                                    '调用函数过程的结果返回值写入指定的单元格内
    End Sub

    '读取条形码
    Shared Function ReadQT(ByVal bmp As Bitmap) As String
        Dim reader As New ZXing.BarcodeReader            '新建一个图像智能类
        reader.Options.CharacterSet = "UTF-8"            '文本编码，建议设置为UTF-8,手机也可以扫.默认为ISO-8859-1英文字符集，移动设备常用UTF-8字符集编码
        Dim ret As ZXing.Result = reader.Decode(bmp)     '声明一个用于读取器的对象(条形码图片)并赋值给变量.
        If ret Is Nothing Then                           '如果读取不到指定二维码图片
            Return Nothing                               '函数过程值返回Nothing
        Else                                             '否则
            Return ret.Text                              '返回图片内容(读取条形码)
        End If
    End Function
#End Region


    Public Sub btnQuickCode_Click(sender As Object, e As RibbonControlEventArgs) Handles btnQuickCode.Click
        Dim f As New WIN190512_二维码
        f.Show()
    End Sub

    Private Sub btnDistance_Click(sender As Object, e As RibbonControlEventArgs) Handles btnDistance.Click

        '.....................教程调用
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/ForFv_Page1")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/ForFv_Page1") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If
        '.....................教程调用

        Dim f As New WIN190515_孔间距计算


        f.Show()
        btnDistance.Enabled = False '让按钮禁用，防止打开多个窗口


    End Sub

    '测试按钮
    Private Sub 测试按钮_Click_1(sender As Object, e As RibbonControlEventArgs) Handles Button5.Click
        Dim f As New K04_发注编号信息管理  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnStorgeCheck.Enabled = False '让按钮禁用，防止打开多个窗口

    End Sub


    '合并单元格保留值
    Private Sub btnMergeCellsRetainContonts_Click(sender As Object, e As RibbonControlEventArgs) Handles btnMergeCellsRetainContonts.Click

        '.....................教程调用 起始
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/MergeRetain")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/MergeRetain") '不可用
                    Exit Sub
                End If
            End If
            Exit Sub
        End If
        '.....................教程调用 结束

        On Error Resume Next
        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        'Call 备份(Targetsht, TargetRng)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True
        ''____________________备份数据、记录区域___________________________

        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True

        Dim rngTarget As Excel.Range, strMerge As String, i As Byte '声明变量
        If TypeName(xlapp.Selection) <> "Range" Then Exit Sub  '如果Selection不是单元格，那么结束过程

        For i = 1 To xlapp.Selection.Areas.Count  '遍历选区中的所有区域
            With xlapp.Selection.Areas(i)  '引用第i个区域
                strMerge = ""  '设置变量的初始值为空文本，避免变量参与下一个区域的运算时还保留当前区域的值，从而影响结果
                For Each rngTarget In .Cells '遍历第i个区域的所有单元格
                    strMerge = strMerge & Chr(10) & rngTarget.Value.ToString '将区域中的所有单元格的值合并起来，存入变量Mystr中
                Next rngTarget
                .ClearContents '清除第i个区域的值
                .merge '合并第i个区域
                .HorizontalAlignment = -4108 '居中显示
                .Value = strMerge '将合并合的结果写入合并后的区域中
            End With
        Next i


        ''.......................................................................................
        'xlapp.OnUndo("撤消[合并后保留值]", "撤消") '代码关联excel的FV.xlam文件里的撤销方法
        ''.......................................................................................

    End Sub

    Private Sub btnSearchNote_Click(sender As Object, e As RibbonControlEventArgs) Handles btnSearchNote.Click
        Dim wb As Excel.Workbook
        'Dim strNotePath As String = "C:\Program Files\FV\FV.xla"
        Dim f As New WIN190810_VBA代码笔记
        'MsgBox("笔记路径:" & strNotePath)
        'wb = xlapp.Workbooks.Open(strNotePath)

        'PauseWait(1500)

        xlapp.Workbooks.Open("C:\Program Files\FV\FV.xlam")
        xlapp.ActiveWorkbook.Sheets(1).select '选择第一个表
        xlapp.Workbooks("FV.xlam").IsAddin = True
        xlapp.Workbooks("FV.xlam").IsAddin = False

        f.Show()

    End Sub




    '插件中按钮单击恢复数据
    Private Sub btnUndo_Click(sender As Object, e As RibbonControlEventArgs) Handles btnUndo.Click
        'Call 撤消() '这里代码调用的是VSTO 的撤销方法
        M2_调用的任务.RestoreFromBackup()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = False
    End Sub

    Private Sub btn索赔信息_Click(sender As Object, e As RibbonControlEventArgs) Handles btn索赔信息.Click
        Dim f As New F04_供应商索赔信息  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btn索赔信息.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btn索赔信息查询与导出_Click(sender As Object, e As RibbonControlEventArgs) Handles btn索赔信息查询与导出.Click
        Dim f As New F05_索赔信息查询与导出  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btn索赔信息查询与导出.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnTesting_Click(sender As Object, e As RibbonControlEventArgs) Handles btnTesting.Click
        Dim f As New I01_检测试验信息管理 '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnTesting.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnCost_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCost.Click
        Dim f As New G01_CostApply  '实例化一个类的对象
        f.Show() '调用类对象的属性
        'btnDataCollect.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnSearchInspect_Click(sender As Object, e As RibbonControlEventArgs) Handles btnSearchInspect.Click
        Dim f As New I02_检测测试信息查询与导出 '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnSearchInspect.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnQcChecked_Click(sender As Object, e As RibbonControlEventArgs) Handles btnQcChecked.Click
        'Dim f As New J01_产品合格检信息确认 '实例化一个类的对象



        If My.Computer.Keyboard.CtrlKeyDown Then
            ''If My.Computer.Network.Ping("www.baidu.com", 2000) Then
            'If My.Computer.Network.Ping("www.ddddddd123u.com", 2000) Then
            '    MsgBox(1)
            'Else
            '    MsgBox(2)

            'End If
            'If My.Computer.Network.IsAvailable Then
            '    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/教程/打开最新版次")     '如果可用

            '    Exit Sub
            'Else
            '    MsgBox("请链接网络")
            '    Exit Sub
            '    'If CreateObject("Wscript.shell").Run("ping http://wangfei.qicp.vip/web/教程/打开最新版次 -n 1", 0, True) <> 0 Then
            '    '    System.Diagnostics.Process.Start("http://192.168.3.12/web/教程/打开最新版次")    '如果没联网就退出程序
            '    'Else
            '    '    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/教程/打开最新版次")     '如果可用
            '    'End If
            '    'System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/教程/打开最新版次")     '如果可用
            'End If

            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/ForFv_Page0")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/ForFv_Page0") '不可用

                    Exit Sub
                End If
            End If
            Exit Sub
        End If

        Dim f As New J02_产品合格检信息确认SqlServer '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnQcChecked.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnQcCheckedNew_Click(sender As Object, e As RibbonControlEventArgs) Handles btnQcCheckedNew.Click
        'Dim f As New J01_产品合格检信息确认 '实例化一个类的对象



        If My.Computer.Keyboard.CtrlKeyDown Then
            ''If My.Computer.Network.Ping("www.baidu.com", 2000) Then
            'If My.Computer.Network.Ping("www.ddddddd123u.com", 2000) Then
            '    MsgBox(1)
            'Else
            '    MsgBox(2)

            'End If
            'If My.Computer.Network.IsAvailable Then
            '    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/教程/打开最新版次")     '如果可用

            '    Exit Sub
            'Else
            '    MsgBox("请链接网络")
            '    Exit Sub
            '    'If CreateObject("Wscript.shell").Run("ping http://wangfei.qicp.vip/web/教程/打开最新版次 -n 1", 0, True) <> 0 Then
            '    '    System.Diagnostics.Process.Start("http://192.168.3.12/web/教程/打开最新版次")    '如果没联网就退出程序
            '    'Else
            '    '    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/教程/打开最新版次")     '如果可用
            '    'End If
            '    'System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/教程/打开最新版次")     '如果可用
            'End If

            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/ForFv_Page0")     '如果可用
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/ForFv_Page0") '不可用

                    Exit Sub
                End If
            End If
            Exit Sub
        End If

        Dim f As New J02_产品合格检信息确认SqlServerNew '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnQcCheckedNew.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub





    Private Sub btnSerachConformityInformathing_Click(sender As Object, e As RibbonControlEventArgs) Handles btnSerachConformityInformathing.Click
        Dim f As New J03_合格检信息查询与导出 '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnSerachConformityInformathing.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub


    Private Sub btnSerachConformityInformathingNew_Click(sender As Object, e As RibbonControlEventArgs) Handles btnSerachConformityInformathingNew.Click
        Dim f As New J03_合格检信息查询与导出New '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnSerachConformityInformathingNew.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub






    Private Sub btnCodeForIncoming_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCodeForIncoming.Click
        Dim f As New K01_入库编号信息管理 '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnSerachConformityInformathing.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnLayoutCode_Click(sender As Object, e As RibbonControlEventArgs) Handles btnLayoutCode.Click
        Dim f As New K02_入库编号信息查询与导出 '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnLayoutCode.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    '拆分工作薄...
    Private Sub btnSplitWorkbook_Click(sender As Object, e As RibbonControlEventArgs) Handles btnSplitWorkbook.Click
        Dim path As String, sht As Excel.Worksheet  '声明变量
        With xlapp.FileDialog(4)  '弹出对话框让用户选择路径
            If .Show = -1 Then  '如果选择了文件夹则将路径赋值给变量path.
                path = .SelectedItems(0) & IIf(Right(.SelectedItems(0), 1) = "\", "", "\")
            Else
                Exit Sub '取消的话退出程序.
            End If
        End With
        xlapp.ScreenUpdating = False '关闭屏幕更新
        For Each sht In xlapp.Worksheets  '遍历所有表
            sht.Copy()  '将表复制到新工作簿中
            xlapp.ActiveWorkbook.SaveAs(path & sht.Name, 51)  '将新工作簿保存在刚才选择的路径中,且以表名保存为工作簿名
            xlapp.ActiveWorkbook.Close(False)  '关闭工作簿
        Next sht
        xlapp.ScreenUpdating = True  '重新开启屏幕更新

        REM  Worksheet.Copy方法表示复制工作表，当指定了参数时表示将工作表复制到指定的位置
        REM  当忽略了参数时则表示将工作表复制到新工作簿中
        REM  它的语法如下：
        REM  Worksheet.Copy(Before, After)
        REM  Workbook.SaveAs方法表示将工作簿另存，从而产生一个具有相同内容的新工作簿，它的语法如下：
        REM  Workbook.SaveAs(FileName, FileFormat, Password, WriteResPassword, ReadOnlyRecommended, CreateBackup, AccessMode, ConflictResolution, AddToMru, TextCodepage, TextVisualLayout, Local)
    End Sub

    '加班工资计算..
    Private Sub btnAddMoney_Click(sender As Object, e As RibbonControlEventArgs) Handles btnAddMoney.Click
        Dim f As New WIN2020119_加班费计算  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnAddMoney.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    '拆分同一类型的供应商至多个不同的工作薄..
    '拆分引用参考宝3,P280 案例"将职员表按学历拆分多个工作表."
    Private Sub btnSplitName_Click(sender As Object, e As RibbonControlEventArgs) Handles btnSplitName.Click
        Dim strSuplierName() As String, data As Object, arr As Object  '声明数组变量
        Dim i As Byte, RowCount As Integer, TargetCount As Integer  '声明变量
        On Error Resume Next  '代码错误时继续执行下一步

        '将5个供应商名称写入数组变量strSuplierName...
        strSuplierName = {"荣程A", "顺章B", "海陆C", "利元D", "广源E", "瑞鑫F"}
        data = xlapp.Range("a1").CurrentRegion.Value   '将A1单元格的当前区域的值赋予变量data(注:赋值的是二维数组,行,列下标从1开始)
        xlapp.DisplayAlerts = False    '关闭提示(删除工作表时会有提示)
        '遍历“strSuplierName”数组的每一个元素
        For i = 0 To UBound(strSuplierName)
            TargetCount = 0    '将变量TargetCount指定为0(每一轮循环前必须以0开始，否则计数不准确)

            '遍历数组data的每一行(标题行例外，所以从2开始)
            For RowCount = 2 To UBound(data)
                If data(RowCount, 2) = strSuplierName(i) Then     '如果data的第二列的值等于数组“strSuplierName”第i个元素
                    TargetCount = TargetCount + 1                 '累加计数器，表示本轮循环中有多少个strSuplierName与“strSuplierName(i)”相同
                    ReDim Preserve arr(0 To 16, 0 To TargetCount - 1)    '重置数组arr的维数、下标与上标(其中第2维的上标根据实际情况随时增加)
                    arr(0, TargetCount - 1) = data(RowCount, 1) '将data 数组二维方向切换写入 arr数组中
                    arr(1, TargetCount - 1) = data(RowCount, 2) '将数组data中的strSuplierName追加到数组arr的第2行第TargetCount列中
                    arr(2, TargetCount - 1) = data(RowCount, 3)
                    arr(3, TargetCount - 1) = data(RowCount, 4)
                    arr(4, TargetCount - 1) = data(RowCount, 5)
                    arr(5, TargetCount - 1) = data(RowCount, 6)
                    arr(6, TargetCount - 1) = data(RowCount, 7)
                    arr(7, TargetCount - 1) = data(RowCount, 8)
                    arr(8, TargetCount - 1) = data(RowCount, 9)
                    arr(9, TargetCount - 1) = data(RowCount, 10)
                    arr(10, TargetCount - 1) = data(RowCount, 11)
                    arr(11, TargetCount - 1) = data(RowCount, 12)
                    arr(12, TargetCount - 1) = data(RowCount, 13)
                    arr(13, TargetCount - 1) = data(RowCount, 14)
                    arr(14, TargetCount - 1) = data(RowCount, 15)
                    arr(15, TargetCount - 1) = data(RowCount, 16)
                    arr(16, TargetCount - 1) = data(RowCount, 17)
                End If
            Next RowCount

            '如果TargetCount的值大于0(表示找到了符合条件的供应商的信息)
            If TargetCount > 0 Then
                xlapp.Worksheets(strSuplierName(i)).Delete   '删除以strSuplierName名称命名的工作表(假设有的话)
                xlapp.Worksheets.Add(After:=xlapp.Worksheets(xlapp.Worksheets.Count)).Name = strSuplierName(i)    '新建一个工作表，以strSuplierName命名

                '引用数组“strSuplierName”中第i个元素命名的工作表
                With xlapp.Worksheets(strSuplierName(i))
                    .Range("A1:Q1") = {"订单号", "供应商", "发货日期", "图号", "型号", "规格", "区分", "材质", "数量", "净重", "炉批号", "热处理号", "锻件编号", "采购编码", "客户编号", "备注说明", "入库完成"} '写入标题
                    .Range("A2").Resize(TargetCount, 17) = xlapp.WorksheetFunction.Transpose(arr)  '将数组转置后导出到工作表中
                    'rngPositionRange.Resize(UBound(arrSaveRange), 3).NumberFormatLocal = "@"   '设置文本格式
                    .Range("C2").Resize(TargetCount, 1).NumberFormatLocal = "yyyy/mm/dd"
                    .UsedRange.Borders.LineStyle = 1         '添加边框
                End With
                Erase arr  '清除数组中的值，进入下一轮循环(否则会保留前面的数据，从而拆分不准确)
            End If
        Next i
    End Sub

    Private Sub btnStorgeCheck_Click(sender As Object, e As RibbonControlEventArgs) Handles btnStorgeCheck.Click
        Dim f As New K03_发注订单信息查看  '实例化一个类的对象
        f.Show() '调用类对象的属性
        btnStorgeCheck.Enabled = False '让按钮禁用，防止打开多个窗口
    End Sub

    Private Sub btnInputOrderInfo_Click(sender As Object, e As RibbonControlEventArgs) Handles btnInputOrderInfo.Click
        合并到总表()
    End Sub

    Private Sub btnDeleteEmptyRows_Click(sender As Object, e As RibbonControlEventArgs) Handles btnDeleteEmptyRows.Click
        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        'Call 备份(Targetsht, TargetRng)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True
        ''____________________备份数据、记录区域___________________________

        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True

        '引用活动工作表的已用数据区域
        With xlapp.ActiveSheet.UsedRange
            '引用工作表最后一列中对应于已用数据区域起止行的单元格
            '也就是说起始与结束行与UsedRange对应的最后一列的区域，用此处作为删除空行的辅助区域
            With xlapp.Cells(.Row, xlapp.Columns.Count).Resize(.Rows.Count, 1)
                '在辅助区域中写入公式，公式的含义是计算已用数据区域中的当前行的数据个数，然后0除以数据个数
                '目的是将数据个数大于0(即非空行)时返回0值，而数据个数等于0（即空行）时返回错误值
                .Formula = "=0/counta(" & xlapp.ActiveSheet.UsedRange.Cells(1).Resize(1, xlapp.ActiveSheet.UsedRange.Columns.Count).Address(0, 0) & ")"
                '在辅助区中定位结果为错误值的公式所在单元格，然后整行删除
                .SpecialCells(-4123, 16).EntireRow.Delete
                '删除辅助区域
                .EntireColumn.Delete
            End With
        End With
    End Sub

    Private Sub btnForDosan_Click(sender As Object, e As RibbonControlEventArgs) Handles btnForDosan.Click
        xlapp.ScreenUpdating = False  '关闭屏幕刷新
        Dim i As Long   '声明变量

        '判定第一行是否是空行
        If xlapp.WorksheetFunction.CountA(xlapp.Rows(1)) = 0 Then xlapp.Rows(1).Delete

        '查找最后一个单元格所在行
        For i = xlapp.Cells.Find("*", xlapp.Cells(1, 1), -4163, 1, 1, 2).Row + 1 To 1 Step -1
            '如果C列和E列不是合并单元格,就整行删除...
            If xlapp.Cells(i, 3).MergeCells = False And xlapp.Cells(i, 3).Offset(0, 2).MergeCells = False Then
                xlapp.Rows(xlapp.Cells(i, 3).Row).Delete
            End If
        Next i
        '删除第8列
        xlapp.Columns(8).Delete
        xlapp.ScreenUpdating = True  '恢复屏幕刷新
    End Sub

    Private Sub btnRatio_Click(sender As Object, e As RibbonControlEventArgs) Handles btnRatio.Click
        '.....................教程调用 起始
        If My.Computer.Keyboard.CtrlKeyDown Then
            If My.Computer.Network.IsAvailable Then
                If getweb() Then
                    System.Diagnostics.Process.Start("http://wangfei.qicp.vip/web/CourseForFv/ForFv_ratio")     '如果可用外网
                    Exit Sub
                Else
                    System.Diagnostics.Process.Start("http://192.168.3.12/web/CourseForFv/ForFv_ratio") '不可用外网,使用局域网
                    Exit Sub
                End If
            End If
            Exit Sub
        End If
        '.....................教程调用 结束

        Dim f As New WIN20220117_锻造比计算公式  '实例化一个窗体对象
        f.Show() '显示窗体
        btnRatio.Enabled = False '让按钮禁用，防止打开多个窗口

    End Sub




    '代码思路分析：
    '首先需要计算当前所选择的多区域的第一行的行号。Selection.row只对单区域有效，多区域时只取第一个区域的首行行号
    '而第一个区域并不一定是多区域中左上角的区域，所以本例采用了循环语句逐一比较，取其中的最小值

    '接着使用Application.InputBox方法创建一个输入框，让用户指定目标存放位置
    '最后使用For Next循环语句逐一复制selection中每个区域到目标区域，复制时目区域与数据源的关系由变量RowOffset和ColOffset决定
    '而这两个变量的值分等于Selection的最小行号和最小列号与Application.InputBox方法所确定的目标单元格的行号与列号的差异
    '也就是说，只要计算出第一个区域与目标区域的距离，通过循环语句逐个复制区域即可，复制时都参照该距离来决定目标区域。
    '可以将两句ScreenUpdating语句删除，然后按F8键逐句运行代码，从而观察代码的执行过程，更利于理解代码的思路
    Private Sub btnCopyData_Click(sender As Object, e As RibbonControlEventArgs) Handles btnCopyData.Click
        Dim TopRow As Long, LeftCol As Integer '声明变量TopRow与LeftCol，分别用于储存行数与列数
        TopRow = xlapp.Rows.Count                '对变量TopRow赋值为最大行的行号
        LeftCol = xlapp.Columns.Count            '对变量LeftCol赋值为最大列的列号
        Dim i As Integer  '声明Integer类型的变量i，用于循环语句中
        For i = 1 To xlapp.Selection.Areas.Count     '遍历选区中的所有区域
            '如果区域的行号小于变量TopRow，那么将新的行号赋值给变量TopRow，从而尽可能地获取Selection的最小行号(LeftCol也相同方式处理)
            If xlapp.Selection.Areas(i).Row < TopRow Then TopRow = xlapp.Selection.Areas(i).Row
            If xlapp.Selection.Areas(i).Column < LeftCol Then LeftCol = xlapp.Selection.Areas(i).Column
        Next

        Dim PasteRange As Excel.Range           '声明Range型的变量，用于储存粘贴数据时的目标区域
        '让用户指定目标单元格，程序将基于此单元格与cells(TopRow,LeftCol)的偏移量决定如何定位目标区域
        PasteRange = xlapp.InputBox(Prompt:="请选择复制对象的存放区，如果有数据将覆盖。", Title:="选择区域", Type:=8)
        PasteRange = PasteRange.Cells(1) '将Range变量重置为其左上角的单元格(避免用户选择了区域)
        xlapp.ScreenUpdating = False   '关闭屏幕刷新，从而加快代码执行速度
        Dim RowOffset As Long, ColOffset As Integer '声明两个变量，分别用于储存目标区域与数据源的行差与列差
        On Error GoTo 错误                   '当执行代码产生错误时，跳转到标签“错误”处(复制数据时超过边界就会出错)
        For i = 1 To xlapp.Selection.Areas.Count   '遍历选区中的所有区域
            RowOffset = xlapp.Selection.Areas(i).Row - TopRow    '计算当前区域的行号与变量TopRow的差值
            ColOffset = xlapp.Selection.Areas(i).Column - LeftCol '计算当前区域的列号与变量LeftCol 的差值
            xlapp.Selection.Areas(i).Copy(PasteRange.Offset(RowOffset, ColOffset)) '根据两个差值确定目标区域，然后执行复制
        Next i
        xlapp.ScreenUpdating = True    '恢复屏幕更新
        Exit Sub                             '退出程序，避免执行后面的代码
错误:        '设置一个标签，若前面的代码执行有误时就接着执行此处的代码
        MsgBox("超出边界，无法粘贴数据。", vbOKOnly, "友情提示")
    End Sub

    Private Sub TEST_Click(sender As Object, e As RibbonControlEventArgs) Handles TEST.Click
        Dim f As New WIN231222_图片尺寸统一
        '实例化一个类的对象.
        f.Show() '显示实例窗体
        btnSort.Enabled = False '让按钮禁用，防止打开多个窗口

    End Sub




    ' 记录功能开关状态的变量
    Private _isAutoSelectEnabled As Boolean = False
    ' 切换按钮点击事件
    Private Sub toggleButton1_Click(sender As Object, e As RibbonControlEventArgs) Handles toggleButton1.Click
        Dim toggleBtn = TryCast(sender, RibbonToggleButton)
        If toggleBtn Is Nothing Then Return

        _isAutoSelectEnabled = toggleBtn.Checked

        If _isAutoSelectEnabled Then
            ' 绑定应用程序级的选区更改事件
            AddHandler Globals.ThisAddIn.Application.SheetSelectionChange, AddressOf Application_SheetSelectionChange
            MessageBox.Show("自动选择整行整列功能已开启", "提示")
        Else
            ' 解绑事件
            RemoveHandler Globals.ThisAddIn.Application.SheetSelectionChange, AddressOf Application_SheetSelectionChange
            MessageBox.Show("自动选择整行整列功能已关闭", "提示")

            ' 恢复为只选中当前活动单元格
            Try
                Globals.ThisAddIn.Application.EnableEvents = False
                Dim activeCell = Globals.ThisAddIn.Application.ActiveCell
                activeCell.Select()
            Finally
                Globals.ThisAddIn.Application.EnableEvents = True
            End Try
        End If
    End Sub


    ' 核心事件处理：当选区发生变化时触发
    Private Sub Application_SheetSelectionChange(ByVal Sh As Object, ByVal Target As Range)
        ' 如果功能未开启，或选中了多个单元格，则不处理
        If Not _isAutoSelectEnabled OrElse Target.Cells.CountLarge > 1 Then
            Return
        End If

        ' 防止事件递归
        Globals.ThisAddIn.Application.EnableEvents = False
        Globals.ThisAddIn.Application.ScreenUpdating = False

        Try
            ' 选择整行和整列
            Globals.ThisAddIn.Application.Union(
                Target.EntireRow,
                Target.EntireColumn
            ).Select()

            ' 确保最初点击的单元格保持为活动单元格
            Target.Activate()
        Catch ex As Exception
            ' 可以在此记录日志
            System.Diagnostics.Debug.WriteLine("错误: " & ex.Message)
        Finally
            ' 恢复事件和屏幕更新
            Globals.ThisAddIn.Application.ScreenUpdating = True
            Globals.ThisAddIn.Application.EnableEvents = True
        End Try
    End Sub

    Private Sub btnGetInform_Click(sender As Object, e As RibbonControlEventArgs) Handles btnGetInform.Click

        ' ============================================================
        ' GN002 - 提取指定列数据（优化版）
        ' 功能：从当前工作表提取指定列数据，生成新工作表
        ' 提取列：A(管理编号)、B(发生日期)、E(产品型号)、
        '         J(类型区分)、K(不良数量)、P(不良现象及原因)
        ' 输出格式：
        '   发生时间: B列
        '   型号：E列（J列）
        '   数量: K列
        '   编号：A列
        '   问题：P列
        '   原因:
        '   措施:
        ' ============================================================


        Dim xlApp As Excel.Application = Globals.ThisAddIn.Application

        ' ----- 第1步：获取当前活动工作表 -----
        Dim ws As Excel.Worksheet = xlApp.ActiveSheet
        If ws Is Nothing Then
            MessageBox.Show("没有打开任何工作表！", "提示")
            Exit Sub
        End If

        ' ----- 第2步：获取已用区域的行范围 -----
        Dim usedRange As Excel.Range = ws.UsedRange
        If usedRange Is Nothing OrElse usedRange.Rows.Count <= 1 Then
            MessageBox.Show("当前工作表的已用区域没有数据行！", "提示")
            Exit Sub
        End If

        Dim startRow As Integer = usedRange.Row + 1   ' 跳过标题行，从第2行开始
        Dim endRow As Integer = usedRange.Row + usedRange.Rows.Count - 1

        ' 如果 startRow > endRow，说明只有标题行没有数据
        If startRow > endRow Then
            MessageBox.Show("只有标题行，没有数据行！", "提示")
            Exit Sub
        End If

        ' ----- 第3步：创建新工作表（放在最后） -----
        Dim currentWb As Excel.Workbook = ws.Parent
        Dim newWs As Excel.Worksheet = DirectCast(
            currentWb.Worksheets.Add(After:=currentWb.Worksheets(currentWb.Worksheets.Count)),
            Excel.Worksheet
        )
        newWs.Name = "提取结果_" & DateTime.Now.ToString("yyyyMMdd_HHmmss")

        ' ----- 第4步：关闭屏幕刷新 -----
        Dim oldScreenUpdating As Boolean = xlApp.ScreenUpdating
        xlApp.ScreenUpdating = False

        Dim outputRow As Integer = 1
        Dim processedCount As Integer = 0

        Try
            ' ----- 第5步：逐行提取数据 -----
            For currentRow As Integer = startRow To endRow
                ' 读取各列数据
                Dim vA As String = GetCellText(ws.Cells(currentRow, 1))   ' A列：管理编号
                Dim vB As String = GetCellText(ws.Cells(currentRow, 2))   ' B列：发生日期
                Dim vE As String = GetCellText(ws.Cells(currentRow, 5))   ' E列：产品型号
                Dim vJ As String = GetCellText(ws.Cells(currentRow, 10))  ' J列：类型区分
                Dim vK As String = GetCellText(ws.Cells(currentRow, 11))  ' K列：不良数量
                Dim vP As String = GetCellText(ws.Cells(currentRow, 16))  ' P列：不良现象及原因

                ' 跳过空行（关键字段都为空时跳过）
                If vA = "" AndAlso vB = "" AndAlso vE = "" AndAlso vJ = "" AndAlso vK = "" AndAlso vP = "" Then
                    Continue For
                End If

                ' 拼接型号：E列 + J列，格式 "产品型号（类型区分）"
                Dim modelText As String = vE
                If vJ <> "" Then
                    modelText = vE & "（" & vJ & "）"
                End If

                ' 按模板拼接信息
                Dim infoText As String = "发生日期: " & vB & vbCrLf &
                                         "型号：" & modelText & vbCrLf &
                                         "数量: " & vK & "个" & vbCrLf &
                                         "编号：" & vA & vbCrLf &
                                         "问题：" & vP & vbCrLf &
                                         "原因:" & vbCrLf &
                                         "措施:" & vbCrLf

                ' 写入新工作表的 A 列
                newWs.Cells(outputRow, 1).Value = infoText
                outputRow += 1
                processedCount += 1
            Next

            ' ----- 第6步：设置列宽 -----
            newWs.Columns("A:A").ColumnWidth = 100

            ' ----- 第7步：激活新工作表 -----
            newWs.Activate()

            ' 状态栏提示
            xlApp.StatusBar = "提取完成！共提取了 " & processedCount & " 条记录。"

        Catch ex As Exception
            MessageBox.Show("提取数据时出错：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            xlApp.ScreenUpdating = oldScreenUpdating

            ' 3秒后清除状态栏
            Dim timer As New System.Timers.Timer(3000)
            AddHandler timer.Elapsed, Sub(a As Object, b As System.Timers.ElapsedEventArgs)
                                          Try
                                              xlApp.StatusBar = False
                                          Catch
                                          End Try
                                          timer.Stop()
                                          timer.Dispose()
                                      End Sub
            timer.AutoReset = False
            timer.Start()
        End Try
    End Sub

    '''' <summary>
    '''' 获取单元格的文本值
    '''' </summary>
    'Private Function GetCellText(cell As Excel.Range) As String
    '    Try
    '        If cell.Value IsNot Nothing Then
    '            Return cell.Value.ToString().Trim()
    '        End If
    '    Catch
    '    End Try
    '    Return ""
    'End Function


    ''' <summary>
    ''' 获取单元格的显示文本（而非底层值）
    ''' 适用于日期、数字等已格式化显示的内容
    ''' </summary>
    Private Function GetCellText(cell As Excel.Range) As String
        Try
            ' ★ 使用 .Text 获取单元格显示的文本，而不是 .Value
            ' 这样日期显示为 "2026/7/8" 而不是 "2026/7/8 0:00:00"
            Dim displayText As String = cell.Text
            If Not String.IsNullOrWhiteSpace(displayText) Then
                Return displayText.Trim()
            End If
        Catch
        End Try
        Return ""
    End Function


    Private Sub btnAutoFontSize_Click(sender As Object, e As RibbonControlEventArgs) Handles btnAutoFontSize.Click
        ' ============================================================
        ' 功能编号：GN001
        ' 功能：一键调整当前工作表已用区域的行高
        '       支持：纯自动换行、纯强制换行、混合换行
        ' 原理：
        '   1. 先开启所有单元格的自动换行
        '   2. 对每个有文本的单元格，判断是否包含强制换行符
        '   3. 包含强制换行 → 逐段计算折行数，累加得总行数
        '   4. 不包含 → 直接按列宽计算折行数
        '   5. 取该行所有单元格的最大行数 × 字体高度 + 留白
        ' ============================================================

        Dim xlApp As Excel.Application = Globals.ThisAddIn.Application

        ' ----- 第1步：获取当前活动工作表 -----
        Dim ws As Excel.Worksheet = xlApp.ActiveSheet
        If ws Is Nothing Then
            MessageBox.Show("没有打开任何工作表！", "提示")
            Exit Sub
        End If

        ' ----- 第2步：获取已用单元格区域 -----
        Dim usedRange As Excel.Range = ws.UsedRange
        If usedRange Is Nothing OrElse usedRange.Rows.Count <= 1 Then
            MessageBox.Show("当前工作表的已用区域没有数据行！", "提示")
            Exit Sub
        End If

        ' ----- 第3步：开启自动换行（必须） -----
        usedRange.WrapText = True

        ' 获取已用区域的行列范围
        Dim startRow As Integer = usedRange.Row
        Dim endRow As Integer = startRow + usedRange.Rows.Count - 1
        Dim startCol As Integer = usedRange.Column
        Dim endCol As Integer = startCol + usedRange.Columns.Count - 1

        ' ----- 第4步：预计算每列的字符容量（以中文字符为单位） -----
        Dim colCharCapacity As New Dictionary(Of Integer, Integer)
        For col As Integer = startCol To endCol
            Dim colWidth As Double = ws.Columns(col).ColumnWidth
            ' 列宽单位是英文字符宽度，中文字符 ≈ 2倍
            ' 减 1 留余量，防止正好卡边
            Dim capacity As Integer = Math.Max(1, CInt(Math.Floor(colWidth / 2)) - 1)
            colCharCapacity(col) = capacity
        Next

        ' ----- 第5步：关闭屏幕刷新 -----
        Dim oldScreenUpdating As Boolean = xlApp.ScreenUpdating
        xlApp.ScreenUpdating = False

        Dim processedCount As Integer = 0

        Try
            ' 用于存储每行最终行高
            Dim rowHeightDict As New Dictionary(Of Integer, Double)

            ' ----- 第6步：逐行遍历，计算每行需要的行数 -----
            For currentRow As Integer = startRow To endRow
                Dim maxLineCount As Integer = 0
                Dim rowFontSize As Single = 11
                Dim hasText As Boolean = False

                For currentCol As Integer = startCol To endCol
                    Dim cell As Excel.Range = ws.Cells(currentRow, currentCol)
                    Dim cellValue As Object = cell.Value

                    If cellValue Is Nothing OrElse String.IsNullOrWhiteSpace(cellValue.ToString()) Then
                        Continue For
                    End If

                    hasText = True
                    Dim cellText As String = cellValue.ToString()

                    ' 获取字体大小
                    Try
                        rowFontSize = cell.Font.Size
                    Catch
                    End Try

                    Dim charsPerLine As Integer = colCharCapacity(currentCol)
                    Dim lineCount As Integer = 0

                    ' ★★★ 核心：判断是否包含强制换行符 ★★★
                    If cellText.Contains(vbLf) Then
                        ' 按强制换行符分段
                        Dim segments As String() = cellText.Split(New Char() {vbLf}, StringSplitOptions.RemoveEmptyEntries)

                        ' 逐段计算每段的折行数，累加
                        For Each seg As String In segments
                            Dim trimmedSeg As String = seg.Trim()
                            If trimmedSeg = "" Then Continue For
                            ' 计算该段文本需要的折行数
                            Dim segLines As Integer = CalculateLinesNeeded(trimmedSeg, charsPerLine)
                            lineCount += segLines
                        Next

                        ' ★ 段落之间加空行（每段后面空一行，最后一段除外）
                        If segments.Length > 0 Then
                            lineCount += (segments.Length - 1)
                        End If

                    Else
                        ' 没有强制换行，直接按列宽计算折行数
                        lineCount = CalculateLinesNeeded(cellText, charsPerLine)
                    End If

                    ' 取该行所有单元格的最大行数
                    If lineCount > maxLineCount Then
                        maxLineCount = lineCount
                    End If
                Next

                If Not hasText OrElse maxLineCount = 0 Then
                    Continue For
                End If

                ' 确保至少1行
                If maxLineCount < 1 Then
                    maxLineCount = 1
                End If

                ' ----- 第7步：计算行高 -----
                ' 行高 = 字体高度 × 行数 + 7磅留白
                Dim newRowHeight As Double = rowFontSize * maxLineCount + 7

                If newRowHeight < 15 Then
                    newRowHeight = 15
                End If

                rowHeightDict(currentRow) = newRowHeight
                processedCount += 1
            Next

            ' ----- 第8步：批量设置行高 -----
            For Each kvp In rowHeightDict
                ws.Rows(kvp.Key).RowHeight = kvp.Value
            Next

            xlApp.StatusBar = "行高调整完成！共处理了 " & processedCount & " 行（支持混合换行）。"

        Catch ex As Exception
            MessageBox.Show("调整行高时出错：" & ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            xlApp.ScreenUpdating = oldScreenUpdating

            ' 3秒后清除状态栏
            Dim timer As New System.Timers.Timer(3000)
            AddHandler timer.Elapsed, Sub(a As Object, b As System.Timers.ElapsedEventArgs)
                                          Try
                                              xlApp.StatusBar = False
                                          Catch
                                          End Try
                                          timer.Stop()
                                          timer.Dispose()
                                      End Sub
            timer.AutoReset = False
            timer.Start()
        End Try
    End Sub


    ''' <summary>
    ''' 计算一段纯文本（不含强制换行符）在指定列宽下需要折成几行
    ''' </summary>
    Private Function CalculateLinesNeeded(text As String, charsPerLine As Integer) As Integer
        If String.IsNullOrWhiteSpace(text) Then
            Return 0
        End If

        ' 统计中英文字符数
        Dim chineseCount As Integer = 0
        Dim englishCount As Integer = 0

        For Each ch As Char In text
            If AscW(ch) >= &H4E00 AndAlso AscW(ch) <= &H9FFF Then
                chineseCount += 1
            ElseIf (ch >= "0" AndAlso ch <= "9") OrElse (ch >= "A" AndAlso ch <= "Z") OrElse (ch >= "a" AndAlso ch <= "z") Then
                englishCount += 1
            Else
                englishCount += 1 ' 标点等按英文算
            End If
        Next

        ' 总宽度：中文=1个单位，英文=0.5个单位
        Dim totalWidth As Double = chineseCount + englishCount * 0.5

        ' 向上取整
        Dim lines As Integer = CInt(Math.Ceiling(totalWidth / charsPerLine))

        Return Math.Max(lines, 1)
    End Function

    ' ============================================================
    ' GN004 - 热处理回火数据分析（使用 TemperaturePoint 结构版）
    ' 功能：识别4个通道的加热/保温时间，使用结构存储数据
    ' 每组独立识别阈值
    ' 输出 J:P 列（7列）
    ' 图表仅使用 B:F 列数据
    ' ============================================================

    Private Sub btnAnalyzeHeatTreatmentData_Click(sender As Object, e As RibbonControlEventArgs) Handles btnAnalyzeHeatTreatmentData.Click
        ' ★★★ 使用 M1_公共变量 中的 xlapp 公共属性（不重新声明局部变量） ★★★
        Dim ws As Excel.Worksheet = xlapp.ActiveSheet

        If ws Is Nothing Then
            MessageBox.Show("没有打开任何工作表！", "提示")
            Exit Sub
        End If

        ' ----- 第1步：获取数据范围 -----
        Dim lastRow As Integer = ws.Cells(ws.Rows.Count, 1).End(Excel.XlDirection.xlUp).Row
        If lastRow < 3 Then
            MessageBox.Show("数据行数不足（至少需要3行数据）！", "提示")
            Exit Sub
        End If

        Dim timeCol As Integer = 2
        Dim tempCols As Integer() = {3, 4, 5, 6}
        Dim channelNames As String() = {"CH01", "CH02", "CH03", "CH04"}

        ' ★★★ 使用结构存储所有数据点 ★★★
        Dim tempPoints As New List(Of TemperaturePoint)

        ' 逐行读取数据，存入结构
        For i As Integer = 2 To lastRow
            Dim time As DateTime = ws.Cells(i, timeCol).Value
            Dim ch01 As Double = ws.Cells(i, tempCols(0)).Value
            Dim ch02 As Double = ws.Cells(i, tempCols(1)).Value
            Dim ch03 As Double = ws.Cells(i, tempCols(2)).Value
            Dim ch04 As Double = ws.Cells(i, tempCols(3)).Value

            Dim point As New TemperaturePoint(time, ch01, ch02, ch03, ch04)
            tempPoints.Add(point)
        Next

        ' ----- 第2步：动态识别分组（每组独立识别阈值） -----
        Dim groups As New List(Of Integer())
        Dim searchStart As Integer = 0

        Do While searchStart <= tempPoints.Count - 3
            ' 寻找"开始加热"：4个通道中最晚出现连续3次增加的行号
            Dim groupStart As Integer = -1
            Dim channelStartRows(3) As Integer

            For ch As Integer = 0 To 3
                channelStartRows(ch) = -1
                For i As Integer = searchStart To tempPoints.Count - 3
                    Dim v1 As Double = GetChannelValue(tempPoints(i), ch)
                    Dim v2 As Double = GetChannelValue(tempPoints(i + 1), ch)
                    Dim v3 As Double = GetChannelValue(tempPoints(i + 2), ch)
                    If v1 < v2 AndAlso v2 < v3 Then
                        channelStartRows(ch) = i
                        Exit For
                    End If
                Next
            Next

            For ch As Integer = 0 To 3
                If channelStartRows(ch) > groupStart Then
                    groupStart = channelStartRows(ch)
                End If
            Next

            If groupStart < 0 Then Exit Do

            ' 先用临时阈值 170 找到该组的范围
            Dim tempThreshold As Double = 170
            Dim holdStartIdx As Integer = -1
            Dim holdStartTime As Nullable(Of DateTime) = Nothing

            For i As Integer = groupStart To tempPoints.Count - 5
                Dim allReady As Boolean = True
                For ch As Integer = 0 To 3
                    Dim v As Double = GetChannelValue(tempPoints(i), ch)
                    If v < tempThreshold Then
                        allReady = False
                        Exit For
                    End If
                Next
                If allReady Then
                    Dim stable As Boolean = True
                    For j As Integer = 0 To 4
                        For ch As Integer = 0 To 3
                            Dim vj As Double = GetChannelValue(tempPoints(i + j), ch)
                            If vj < tempThreshold - 1 Then
                                stable = False
                                Exit For
                            End If
                        Next
                        If Not stable Then Exit For
                    Next
                    If stable Then
                        holdStartIdx = i
                        holdStartTime = tempPoints(i).dtmTime
                        Exit For
                    End If
                End If
            Next

            If holdStartIdx < 0 Then
                searchStart = groupStart + 1
                Continue Do
            End If

            Dim groupEnd As Integer = tempPoints.Count - 1
            If holdStartTime.HasValue Then
                Dim minHoldEndTime As DateTime = holdStartTime.Value.AddMinutes(10)

                For i As Integer = holdStartIdx + 1 To tempPoints.Count - 1
                    If tempPoints(i).dtmTime < minHoldEndTime Then
                        Continue For
                    End If

                    Dim anyBelow As Boolean = False
                    For ch As Integer = 0 To 3
                        Dim v As Double = GetChannelValue(tempPoints(i), ch)
                        If v < tempThreshold Then
                            anyBelow = True
                            Exit For
                        End If
                    Next

                    If anyBelow Then
                        groupEnd = i - 1
                        Exit For
                    End If
                Next
            End If

            ' 在该组范围内扫描最高温度，独立识别阈值
            Dim groupMaxTemp As Double = 0
            For i As Integer = groupStart To groupEnd
                For ch As Integer = 0 To 3
                    Dim v As Double = GetChannelValue(tempPoints(i), ch)
                    If v > groupMaxTemp Then
                        groupMaxTemp = v
                    End If
                Next
            Next

            Dim groupThreshold As Double = 170
            If groupMaxTemp >= 205 Then
                groupThreshold = 200
            ElseIf groupMaxTemp >= 175 Then
                groupThreshold = 170
            End If

            ' 用该组独立的阈值重新确定保温开始和保温结束
            Dim finalHoldStartIdx As Integer = -1
            Dim finalHoldStartTime As Nullable(Of DateTime) = Nothing

            For i As Integer = groupStart To tempPoints.Count - 5
                Dim allReady As Boolean = True
                For ch As Integer = 0 To 3
                    Dim v As Double = GetChannelValue(tempPoints(i), ch)
                    If v < groupThreshold Then
                        allReady = False
                        Exit For
                    End If
                Next
                If allReady Then
                    Dim stable As Boolean = True
                    For j As Integer = 0 To 4
                        For ch As Integer = 0 To 3
                            Dim vj As Double = GetChannelValue(tempPoints(i + j), ch)
                            If vj < groupThreshold - 1 Then
                                stable = False
                                Exit For
                            End If
                        Next
                        If Not stable Then Exit For
                    Next
                    If stable Then
                        finalHoldStartIdx = i
                        finalHoldStartTime = tempPoints(i).dtmTime
                        Exit For
                    End If
                End If
            Next

            If finalHoldStartIdx < 0 Then
                finalHoldStartIdx = holdStartIdx
                finalHoldStartTime = holdStartTime
            End If

            Dim finalGroupEnd As Integer = tempPoints.Count - 1
            If finalHoldStartTime.HasValue Then
                Dim minHoldEndTime As DateTime = finalHoldStartTime.Value.AddMinutes(10)

                For i As Integer = finalHoldStartIdx + 1 To tempPoints.Count - 1
                    If tempPoints(i).dtmTime < minHoldEndTime Then
                        Continue For
                    End If

                    Dim anyBelow As Boolean = False
                    For ch As Integer = 0 To 3
                        Dim v As Double = GetChannelValue(tempPoints(i), ch)
                        If v < groupThreshold Then
                            anyBelow = True
                            Exit For
                        End If
                    Next

                    If anyBelow Then
                        finalGroupEnd = i - 1
                        Exit For
                    End If
                Next
            End If

            If finalGroupEnd >= groupStart Then
                groups.Add(New Integer() {groupStart, finalHoldStartIdx, finalGroupEnd, CInt(groupThreshold)})
            End If

            searchStart = finalGroupEnd + 1
        Loop

        If groups.Count = 0 Then
            MessageBox.Show("未识别到有效数据组！", "提示")
            Exit Sub
        End If

        ' ----- 第3步：分析每组数据，提取精确时间和最高温度 -----
        Dim allResults As New List(Of Object())
        Dim allHighlightRows As New List(Of Integer)

        For idx As Integer = 0 To groups.Count - 1
            Dim g As Integer() = groups(idx)
            Dim startIdx As Integer = g(0)
            Dim holdStartIdx As Integer = g(1)
            Dim endIdx As Integer = g(2)
            Dim threshold As Double = g(3)

            Dim tStartHeat As Object = Nothing
            Dim tHoldStart As Object = Nothing
            Dim tHoldEnd As Object = Nothing
            Dim duration As Double = 0

            If startIdx >= 0 Then tStartHeat = tempPoints(startIdx).dtmTime
            If holdStartIdx >= 0 Then tHoldStart = tempPoints(holdStartIdx).dtmTime
            If endIdx >= 0 Then tHoldEnd = tempPoints(endIdx).dtmTime

            If IsDate(tHoldStart) AndAlso IsDate(tHoldEnd) Then
                duration = (CDate(tHoldEnd) - CDate(tHoldStart)).TotalHours
            End If

            ' 计算该组真实最高温度
            Dim realMaxTemp As Double = 0
            If startIdx >= 0 AndAlso endIdx >= startIdx Then
                For i As Integer = startIdx To endIdx
                    For ch As Integer = 0 To 3
                        Dim v As Double = GetChannelValue(tempPoints(i), ch)
                        If v > realMaxTemp Then
                            realMaxTemp = v
                        End If
                    Next
                Next
            End If

            allResults.Add(New Object() {
                startIdx + 2, holdStartIdx + 2, endIdx + 2,
                tStartHeat, tHoldStart, tHoldEnd,
                duration, threshold, realMaxTemp
            })

            If startIdx >= 0 Then allHighlightRows.Add(startIdx + 2)
            If holdStartIdx >= 0 Then allHighlightRows.Add(holdStartIdx + 2)
            If endIdx >= 0 Then allHighlightRows.Add(endIdx + 2)
        Next

        If allResults.Count = 0 Then
            MessageBox.Show("所有组分析失败！", "提示")
            Exit Sub
        End If

        ' ----- 第4步：高亮标记 A:F 列 -----
        ws.Range("A:F").Interior.ColorIndex = Excel.XlColorIndex.xlColorIndexNone
        For Each rowNum As Integer In allHighlightRows
            If rowNum > 0 Then
                'ws.Range(ws.Cells(rowNum, 1), ws.Cells(rowNum, 6)).Interior.ColorIndex = 6
                ' ★★★ 使用 M2_调用的任务 中的公共颜色常量 ★★★
                ws.Range(ws.Cells(rowNum, 1), ws.Cells(rowNum, 6)).Interior.ColorIndex = M1_公共变量.COLOR_LIGHT_YELLOW
            End If
        Next

        ' ----- 第5步：写入结果（每组一行，含超链接） -----
        ws.Range("J2:N" & ws.Rows.Count).ClearContents()

        ws.Cells(1, 10).Value = "组号"
        ws.Cells(1, 11).Value = "开始加热时间"
        ws.Cells(1, 12).Value = "保温开始时间"
        ws.Cells(1, 13).Value = "保温结束时间"
        ws.Cells(1, 14).Value = "保温时长(小时)"

        Dim outputRow As Integer = 2
        For Each res As Object() In allResults
            Dim startRow As Integer = CInt(res(0))
            Dim holdStartRow As Integer = CInt(res(1))
            Dim holdEndRow As Integer = CInt(res(2))

            ws.Cells(outputRow, 10).Value = "第" & (outputRow - 1) & "组"

            ' K列：开始加热时间（超链接 → A:B列）
            If startRow > 0 AndAlso IsDate(res(3)) Then
                Dim targetRange As Excel.Range = ws.Range(ws.Cells(startRow, 1), ws.Cells(startRow, 2))
                ws.Hyperlinks.Add(Anchor:=ws.Cells(outputRow, 11), Address:="", SubAddress:=targetRange.Address, TextToDisplay:=CDate(res(3)).ToString("yyyy-MM-dd HH:mm:ss"))
            End If

            ' L列：保温开始时间（超链接 → A:B列）
            If holdStartRow > 0 AndAlso IsDate(res(4)) Then
                Dim targetRange As Excel.Range = ws.Range(ws.Cells(holdStartRow, 1), ws.Cells(holdStartRow, 2))
                ws.Hyperlinks.Add(Anchor:=ws.Cells(outputRow, 12), Address:="", SubAddress:=targetRange.Address, TextToDisplay:=CDate(res(4)).ToString("yyyy-MM-dd HH:mm:ss"))
            End If

            ' M列：保温结束时间（超链接 → A:B列）
            If holdEndRow > 0 AndAlso IsDate(res(5)) Then
                Dim targetRange As Excel.Range = ws.Range(ws.Cells(holdEndRow, 1), ws.Cells(holdEndRow, 2))
                ws.Hyperlinks.Add(Anchor:=ws.Cells(outputRow, 13), Address:="", SubAddress:=targetRange.Address, TextToDisplay:=CDate(res(5)).ToString("yyyy-MM-dd HH:mm:ss"))
            End If

            ' N列：保温时长
            ws.Cells(outputRow, 14).Value = IIf(CDbl(res(6)) = 0, "", Math.Round(CDbl(res(6)), 2))

            outputRow += 1
        Next

        ' ★★★ 格式美化（使用 With 块，避免变量重复声明） ★★★
        Dim lastResultRow As Integer = outputRow - 1
        If lastResultRow >= 2 Then
            With ws.Range("J1:N" & lastResultRow)
                .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .Borders.Weight = Excel.XlBorderWeight.xlThin
                .Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            End With
            ws.Range("J1:N1").Font.Bold = True
        End If
        ws.Columns("J:N").AutoFit()

        xlapp.StatusBar = "热处理分析完成！共识别到 " & groups.Count & " 组数据，" &
                          "使用 TemperaturePoint 结构存储 " & tempPoints.Count & " 个数据点"

        ' ----- 第6步：生成折线图 -----
        Dim chartName As String = "GN004_TempChart"

        For Each ch As Excel.ChartObject In ws.ChartObjects()
            If ch.Name = chartName Then
                ch.Delete()
                Exit For
            End If
        Next

        Dim chartShape As Excel.Shape
        chartShape = ws.Shapes.AddChart(Excel.XlChartType.xlLineMarkers, ws.Cells(7, 10).Left, ws.Cells(7, 10).Top, 650, 400)
        chartShape.Name = chartName

        Dim chart As Excel.Chart = chartShape.Chart

        Do While chart.SeriesCollection().Count > 0
            chart.SeriesCollection(1).Delete()
        Loop

        Dim xRange As Excel.Range = ws.Range(ws.Cells(2, timeCol), ws.Cells(lastRow, timeCol))

        For ch As Integer = 0 To 3
            Dim yRange As Excel.Range = ws.Range(ws.Cells(2, tempCols(ch)), ws.Cells(lastRow, tempCols(ch)))

            Dim series As Excel.Series = chart.SeriesCollection().NewSeries()
            series.Name = channelNames(ch)
            series.XValues = xRange
            series.Values = yRange
            series.MarkerStyle = Excel.XlMarkerStyle.xlMarkerStyleCircle
            series.MarkerSize = 5
        Next

        chart.HasTitle = True
        chart.ChartTitle.Text = "热处理温度曲线"
        chart.ChartTitle.Font.Size = 12
        chart.ChartTitle.Font.Bold = True

        chart.Axes(Excel.XlAxisType.xlCategory).HasTitle = True
        chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Text = "时间"
        chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Font.Size = 10

        chart.Axes(Excel.XlAxisType.xlValue).HasTitle = True
        chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Text = "温度 (℃)"
        chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Font.Size = 10

        chart.HasLegend = True
        chart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom
    End Sub

    ' ★★★ 辅助函数：获取结构中指定通道的值 ★★★
    Private Function GetChannelValue(point As TemperaturePoint, channelIndex As Integer) As Double
        Select Case channelIndex
            Case 0 : Return point.dblCH01
            Case 1 : Return point.dblCH02
            Case 2 : Return point.dblCH03
            Case 3 : Return point.dblCH04
            Case Else : Return 0
        End Select
    End Function


End Class
