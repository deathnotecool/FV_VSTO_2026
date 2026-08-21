Imports System.Windows.Forms    '声明命名空间
Public Class WN18081503_文件批量重命名
    '窗体加载触发事件.
    Private Sub WN001_批量重命名_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnOK.Enabled = False   '禁用确定命令按钮...
    End Sub

    '单机按钮触发该事件.
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
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
            lstDisplayFullName.Items.AddRange(arrFileArrayResetting)
            txtFilePath.Text = Replace(objFileArray(1), Dir(objFileArray(1)), "")
        Else
            Exit Sub  '结束过程
        End If
        btnOK.Enabled = True  '让CommandButton2呈可用状态
    End Sub

    '点选选项框(插入字符之前),触发事件.
    Private Sub optInsertPreName_Click(sender As Object, e As EventArgs) Handles optInsertPreName.Click
        '隐藏文本框和标签.
        txtOriginString.Visible = False
        lblOriginString.Visible = False
    End Sub

    '点选选项框(插入字符之后),触发事件.
    Private Sub optInsertPostName_Click(sender As Object, e As EventArgs) Handles optInsertPostName.Click
        '隐藏文本框和标签.
        txtOriginString.Visible = False
        lblOriginString.Visible = False
    End Sub

    '点选选项框(替换字符),触发事件.
    Private Sub optReplaceApointString_Click(sender As Object, e As EventArgs) Handles optReplaceApointString.Click
        txtOriginString.Visible = True
        lblOriginString.Visible = True
    End Sub

    '新字符框发生变化时,触发事件.
    Private Sub txtNewString_TextChanged(sender As Object, e As EventArgs) Handles txtNewString.TextChanged
        '判定录入了数据执行..
        If Len(txtNewString.Text) > 0 Then
            '如果右边一位是“\/?*:<>|”中的任意一位字符
            If Strings.Right(txtNewString.Text, 1) = "\" Or Strings.Right(txtNewString.Text, 1) = "/" Or Strings.Right(txtNewString.Text, 1) = ":" Or Strings.Right(txtNewString.Text, 1) = "?" Or Strings.Right(txtNewString.Text, 1) = "*" Or Strings.Right(txtNewString.Text, 1) = "<" Or Strings.Right(txtNewString.Text, 1) = ">" Or Strings.Right(txtNewString.Text, 1) = "|" Then
                MsgBox("不能使用" & Strings.Right(txtNewString.Text, 1) & "对文件命名", vbInformation) '提示用户
                txtNewString.Text = Strings.Left(txtNewString.Text, Len(txtNewString.Text) - 1) '赋值给文本框Text属性
            End If
        End If
    End Sub

    '单机OK键触发事件.
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim bytPointPosition As Byte, bytSlashPosition As Byte, intStep As Integer, strFile() As String, strFileName As String, strShortName As String

        If optReplaceApointString.Checked Then  '如果选择了“替换指定字符”.
            '第二个文本框是空值，提示用户并结束程序
            If Len(txtOriginString.Text) = 0 Then MsgBox("请输入需要替换的名称") : Exit Sub
        Else
            '第三个文本框是空值，提示用户并结束程序
            If Len(txtNewString.Text) = 0 Then MsgBox("请输入替换后的名称") : Exit Sub
        End If
        '根据列表框的元素个数,重置数组变量的上标.
        ReDim strFile(0 To lstDisplayFullName.Items.Count - 1)
        '逐步遍历列表框的所有元素.
        For intStep = 0 To lstDisplayFullName.Items.Count - 1  '遍历列表框的所有元素
            bytPointPosition = InStr(1, StrReverse(lstDisplayFullName.Items(intStep)), ".")   '计算后缀名前面的小圆点的位置
            bytSlashPosition = InStr(1, StrReverse(lstDisplayFullName.Items(intStep)), "\")   '计算文件名称前面的“\”的位置
            '如果选择了“插入到原名称之前”.
            If optInsertPreName.Checked Then
                '生成新的文件名称(在原来的文件名称之前插入值),第一个&之前的字符是文件的路径
                'TextBox3.Value表示要插入的字符,后面的Right的计算结果则是原本的文件名称
                strFileName = Strings.Left(lstDisplayFullName.Items(intStep),
                                           1 + Len(lstDisplayFullName.Items(intStep)) - bytSlashPosition) &
                    txtNewString.Text & Strings.Right(lstDisplayFullName.Items(intStep), bytSlashPosition - 1)
                FileSystem.Rename(lstDisplayFullName.Items(intStep), strFileName) '用Rename语句对文件重命名

                '如果选择了“插入到原名称之后"..
            ElseIf optInsertPostName.Checked Then
                strFileName = Strings.Left(lstDisplayFullName.Items(intStep), Len(lstDisplayFullName.Items(intStep)) - bytPointPosition) &
                    txtNewString.Text & Strings.Right(lstDisplayFullName.Items(intStep), bytPointPosition)
                FileSystem.Rename(lstDisplayFullName.Items(intStep), strFileName) '用Rename语句对文件重命名
            Else
                '记录文件的短名称(不包含后缀名).
                strShortName = Strings.Left(Dir(lstDisplayFullName.Items(intStep)), Len(Dir(lstDisplayFullName.Items(intStep))) - bytPointPosition)
                '生成新的文件名称.
                strFileName = Strings.Left(lstDisplayFullName.Items(intStep), 1 + Len(lstDisplayFullName.Items(intStep)) - bytSlashPosition) &
                Replace(strShortName, txtOriginString.Text, txtNewString.Text) &
                Strings.Right(lstDisplayFullName.Items(intStep), bytPointPosition)
                FileSystem.Rename(lstDisplayFullName.Items(intStep), strFileName)  '用Rename对文件重命名，VBA中是采用Name语句命名
            End If
            strFile(intStep) = strFileName      '将新文件名称导入到数组中
        Next intStep
        lstDisplayFullName.Items.Clear()            '清除列表框的值
        lstDisplayFullName.Items.AddRange(strFile)  '向列表框追加新的内容
    End Sub

    '单机按钮,触发该事件.
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()  '关闭窗体
        Globals.Ribbons.Ribbon1.btnBatchNaming.Enabled = True     '重新使按钮可用.
    End Sub

    '关闭窗体,触发该事件.
    Private Sub 批量重命名_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnBatchNaming.Enabled = True     '重新使按钮可用.
    End Sub

End Class