Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms

''' <summary>
''' 功能：FMEA 工序主表维护窗体
''' </summary>
Public Class L_FmeaMain

    ' ========== 窗体级变量 ==========

    ''' <summary>
    ''' 功能：存放从数据库读出的主表数据
    ''' </summary>
    Private dtMain As DataTable

    ''' <summary>
    ''' 功能：当前显示的是第几行（0 开始）
    ''' </summary>
    Private intCurrentRow As Integer = 0

    ''' <summary>
    ''' 功能：是否处于新增模式。True=新增，False=编辑
    ''' </summary>
    Private blnIsNew As Boolean = False

    ' ========== 窗体加载事件 ==========

    ''' <summary>
    ''' 功能：窗体第一次打开时执行，初始化下拉框并载入数据
    ''' </summary>
    Private Sub L_FmeaMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 第 1 步：初始化三个下拉框的选项
        InitComboBox()

        ' 第 2 步：从数据库读取主表数据
        LoadData()

        ' 第 3 步：把第一条数据显示到控件上
        If dtMain.Rows.Count > 0 Then
            intCurrentRow = 0
            ShowRecord()
        Else
            ' 如果表里没数据，直接进入新增模式
            ClearControls()
            blnIsNew = True
        End If
    End Sub

    ''' <summary>
    ''' 功能：初始化三个下拉框的选项，禁止手输
    ''' </summary>
    Private Sub InitComboBox()
        ' 工序段下拉框：先清空再加，防止重复执行时叠加选项
        cboSection.Items.Clear()
        cboSection.Items.Add("外圈")
        cboSection.Items.Add("内圈")
        cboSection.Items.Add("组装")
        cboSection.Items.Add("油漆")
        ' DropDownList = 只能从列表选，不能手输，防止错别字
        cboSection.DropDownStyle = ComboBoxStyle.DropDownList

        ' 工序类型下拉框：8 类枚举
        cboProcessType.Items.Clear()
        cboProcessType.Items.Add("检验")
        cboProcessType.Items.Add("机械加工")
        cboProcessType.Items.Add("热处理")
        cboProcessType.Items.Add("装配")
        cboProcessType.Items.Add("打码")
        cboProcessType.Items.Add("包装")
        cboProcessType.Items.Add("油漆")
        cboProcessType.Items.Add("仓储")
        cboProcessType.DropDownStyle = ComboBoxStyle.DropDownList

        ' 状态下拉框：5 态枚举
        cboStatus.Items.Clear()
        cboStatus.Items.Add("草稿")
        cboStatus.Items.Add("评审中")
        cboStatus.Items.Add("修订中")
        cboStatus.Items.Add("已发布")
        cboStatus.Items.Add("已作废")
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub


    ''' <summary>
    ''' 功能：从数据库读取 tblFMEA_Main 未删除记录，存入 dtMain
    ''' </summary>
    Private Sub LoadData()
        Using conn As OleDbConnection = GetConnection()
            Dim strSql As String = "SELECT * FROM tblFMEA_Main " &
                               "WHERE blnIsDeleted = False " &
                               "ORDER BY lngProcessOrder"
            Using da As New OleDbDataAdapter(strSql, conn)
                dtMain = New DataTable()
                da.Fill(dtMain)
            End Using
        End Using

        ' 数据读完后绑定表格
        BindGrid()
    End Sub

    ''' <summary>
    ''' 功能：把 dtMain 中当前行的数据显示到窗体控件上
    ''' </summary>
    Private Sub ShowRecord()
        ' 防御：没有数据或行号越界，直接返回
        If dtMain Is Nothing OrElse dtMain.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtMain.Rows.Count Then Return

        ' 取出当前行，后面反复用
        Dim dr As DataRow = dtMain.Rows(intCurrentRow)

        ' 文本框：先判空再赋值，避免 DBNull 转字符串报错
        txtProcessNo.Text = If(IsDBNull(dr("strProcessNo")), "", dr("strProcessNo").ToString())
        txtProcessName.Text = If(IsDBNull(dr("strProcessName")), "", dr("strProcessName").ToString())
        txtProcessOrder.Text = If(IsDBNull(dr("lngProcessOrder")), "0", dr("lngProcessOrder").ToString())
        txtVersion.Text = If(IsDBNull(dr("strVersion")), "", dr("strVersion").ToString())
        txtDeptName.Text = If(IsDBNull(dr("strDeptName")), "", dr("strDeptName").ToString())
        txtOwner.Text = If(IsDBNull(dr("strOwner")), "", dr("strOwner").ToString())
        txtFmeaTeam.Text = If(IsDBNull(dr("strFmeaTeam")), "", dr("strFmeaTeam").ToString())
        txtFunction.Text = If(IsDBNull(dr("memFunction")), "", dr("memFunction").ToString())
        txtFunctionReq.Text = If(IsDBNull(dr("memFunctionReq")), "", dr("memFunctionReq").ToString())
        txtRemark.Text = If(IsDBNull(dr("memRemark")), "", dr("memRemark").ToString())

        ' 下拉框：用 SelectedItem 直接赋字符串，找不到会自动为空
        cboSection.Text = If(IsDBNull(dr("strSection")), "", dr("strSection").ToString())
        cboProcessType.Text = If(IsDBNull(dr("strProcessType")), "", dr("strProcessType").ToString())
        cboStatus.Text = If(IsDBNull(dr("strStatus")), "", dr("strStatus").ToString())

        ' 日期框：先判空，再转 DateTime 赋值
        If Not IsDBNull(dr("dtmCreateTime")) Then dtpCreateTime.Value = CDate(dr("dtmCreateTime"))
        If Not IsDBNull(dr("dtmUpdateTime")) Then dtpUpdateTime.Value = CDate(dr("dtmUpdateTime"))

        ' 非新增模式：编号只读，防止误改唯一键
        txtProcessNo.ReadOnly = Not blnIsNew

        ' 状态栏显示当前进度
        lblStatusBar.Text = "当前第 " & (intCurrentRow + 1) & " 条 / 共 " & dtMain.Rows.Count & " 条"

        ' 同步表格高亮
        If dgvMain.Rows.Count > intCurrentRow Then
            dgvMain.ClearSelection()
            dgvMain.Rows(intCurrentRow).Selected = True
        End If

    End Sub

    ''' <summary>
    ''' 功能：清空所有录入控件，准备新增一条记录
    ''' </summary>
    Private Sub ClearControls()
        ' 文本框清空
        txtProcessNo.Text = ""
        txtProcessName.Text = ""
        txtProcessOrder.Text = "0"
        txtVersion.Text = "V1.0"        ' 版本号默认值
        txtDeptName.Text = ""
        txtOwner.Text = ""
        txtFmeaTeam.Text = ""
        txtFunction.Text = ""
        txtFunctionReq.Text = ""
        txtRemark.Text = ""

        ' 下拉框恢复到默认选项
        If cboSection.Items.Count > 0 Then cboSection.SelectedIndex = 0
        If cboProcessType.Items.Count > 0 Then cboProcessType.SelectedIndex = 0
        ' 状态默认“草稿”，注意大小写要和 InitComboBox 里加的一致
        cboStatus.Text = "草稿"

        ' 日期框显示当前时间（新增时先给个值，保存时再覆盖）
        dtpCreateTime.Value = Now
        dtpUpdateTime.Value = Now

        ' 新增模式：编号可编辑
        txtProcessNo.ReadOnly = False

        ' 状态栏提示
        lblStatusBar.Text = "新增模式，请填写后保存"
    End Sub

    '''' <summary>
    '''' 功能：保存前校验输入是否合法，不合法返回 False 并提示
    '''' </summary>
    'Private Function ValidateInput() As Boolean
    '    ' 工序编号必填
    '    If txtProcessNo.Text.Trim() = "" Then
    '        MessageBox.Show("工序编号不能为空")
    '        txtProcessNo.Focus()
    '        Return False
    '    End If

    '    ' 工序名称必填
    '    If txtProcessName.Text.Trim() = "" Then
    '        MessageBox.Show("工序名称不能为空")
    '        txtProcessName.Focus()
    '        Return False
    '    End If

    '    ' 工序段必须选择
    '    If cboSection.SelectedIndex < 0 Then
    '        MessageBox.Show("请选择工序段")
    '        cboSection.Focus()
    '        Return False
    '    End If

    '    ' 工序类型必须选择
    '    If cboProcessType.SelectedIndex < 0 Then
    '        MessageBox.Show("请选择工序类型")
    '        cboProcessType.Focus()
    '        Return False
    '    End If

    '    ' 工序顺序必须是数字
    '    Dim intOrder As Integer
    '    If Not Integer.TryParse(txtProcessOrder.Text.Trim(), intOrder) Then
    '        MessageBox.Show("工序顺序必须是数字")
    '        txtProcessOrder.Focus()
    '        Return False
    '    End If

    '    ' 状态必须选择
    '    If cboStatus.SelectedIndex < 0 Then
    '        MessageBox.Show("请选择状态")
    '        cboStatus.Focus()
    '        Return False
    '    End If

    '    ' 工序编号唯一性校验：编辑模式查其他行，新增模式查全部
    '    If IsProcessNoExists(txtProcessNo.Text.Trim()) Then
    '        MessageBox.Show("工序编号已存在：" & txtProcessNo.Text.Trim())
    '        txtProcessNo.Focus()
    '        Return False
    '    End If

    '    Return True
    'End Function

    ''' <summary>
    ''' 功能：保存前校验输入是否合法，不合法返回 False 并提示
    ''' </summary>
    Private Function ValidateInput() As Boolean
        If txtProcessNo.Text.Trim() = "" Then
            MessageBox.Show("工序编号不能为空")
            txtProcessNo.Focus()
            Return False
        End If

        If txtProcessName.Text.Trim() = "" Then
            MessageBox.Show("工序名称不能为空")
            txtProcessName.Focus()
            Return False
        End If

        If cboSection.SelectedIndex < 0 Then
            MessageBox.Show("请选择工序段")
            cboSection.Focus()
            Return False
        End If

        If cboProcessType.SelectedIndex < 0 Then
            MessageBox.Show("请选择工序类型")
            cboProcessType.Focus()
            Return False
        End If

        Dim intOrder As Integer
        If Not Integer.TryParse(txtProcessOrder.Text.Trim(), intOrder) Then
            MessageBox.Show("工序顺序必须是数字")
            txtProcessOrder.Focus()
            Return False
        End If

        If cboStatus.SelectedIndex < 0 Then
            MessageBox.Show("请选择状态")
            cboStatus.Focus()
            Return False
        End If

        If IsProcessNoExists(txtProcessNo.Text.Trim()) Then
            MessageBox.Show("工序编号已存在：" & txtProcessNo.Text.Trim())
            txtProcessNo.Focus()
            Return False
        End If

        Return True
    End Function



    '''' <summary>
    '''' 功能：检查工序编号是否已存在（排除当前正在编辑的行）
    '''' </summary>
    'Private Function IsProcessNoExists(ByVal strNo As String) As Boolean
    '    Dim strSql As String
    '    If blnIsNew Then
    '        strSql = "SELECT COUNT(*) FROM tblFMEA_Main " &
    '             "WHERE strProcessNo = ? AND blnIsDeleted = False"
    '    Else
    '        strSql = "SELECT COUNT(*) FROM tblFMEA_Main " &
    '             "WHERE strProcessNo = ? AND blnIsDeleted = False AND lngID <> ?"
    '    End If

    '    Using conn As OleDbConnection = GetConnection()
    '        Using cmd As New OleDbCommand(strSql, conn)
    '            cmd.Parameters.AddWithValue("?", strNo)
    '            If Not blnIsNew Then
    '                cmd.Parameters.AddWithValue("?", CLng(dtMain.Rows(intCurrentRow)("lngID")))
    '            End If
    '            conn.Open()
    '            Dim result As Object = cmd.ExecuteScalar()
    '            If IsDBNull(result) Then Return False
    '            Return CInt(result) > 0
    '        End Using
    '    End Using
    'End Function

    ''' <summary>
    ''' 功能：检查工序编号是否已存在（排除当前正在编辑的行）
    ''' </summary>
    Private Function IsProcessNoExists(ByVal strNo As String) As Boolean
        Dim strSql As String

        If blnIsNew Then
            strSql = "SELECT COUNT(*) FROM tblFMEA_Main WHERE strProcessNo = ? AND blnIsDeleted = False"
        Else
            strSql = "SELECT COUNT(*) FROM tblFMEA_Main WHERE strProcessNo = ? AND blnIsDeleted = False AND lngID <> ?"
        End If

        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand(strSql, conn)
                ' 第一个参数：工序编号
                cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strNo

                ' 编辑模式才加第二个参数：排除自己
                If Not blnIsNew Then
                    Dim lngID As Long = CLng(dtMain.Rows(intCurrentRow)("lngID"))
                    cmd.Parameters.Add("p2", OleDbType.Integer).Value = lngID
                End If

                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If IsDBNull(result) Then Return False
                Return CInt(result) > 0
            End Using
        End Using
    End Function



    ' ========== 按钮事件 ==========

    ''' <summary>
    ''' 功能：新增按钮，进入新增模式
    ''' </summary>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        blnIsNew = True
        ClearControls()
        txtProcessNo.Focus()
    End Sub

    '''' <summary>
    '''' 功能：保存按钮，校验后 INSERT 或 UPDATE
    '''' </summary>
    'Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Try
    '        If Not ValidateInput() Then Return

    '        If blnIsNew Then
    '            InsertRecord()
    '        Else
    '            UpdateRecord()
    '        End If

    '        LoadData()
    '        intCurrentRow = FindRowByProcessNo(txtProcessNo.Text.Trim())
    '        If intCurrentRow < 0 Then intCurrentRow = 0
    '        ShowRecord()
    '        blnIsNew = False
    '        lblStatusBar.Text = "保存成功"
    '    Catch ex As Exception
    '        MessageBox.Show("保存失败：" & ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
    '    End Try
    'End Sub


    ''' <summary>
    ''' 功能：保存按钮，校验后 INSERT 或 UPDATE
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not ValidateInput() Then Return

            If blnIsNew Then
                InsertRecord()
            Else
                UpdateRecord()
            End If

            ' 保存后重新载入，界面显示最新数据
            Dim strNo As String = txtProcessNo.Text.Trim()
            LoadData()
            intCurrentRow = FindRowByProcessNo(strNo)
            If intCurrentRow < 0 Then intCurrentRow = 0
            ShowRecord()
            blnIsNew = False
            lblStatusBar.Text = "保存成功"
        Catch ex As Exception
            MessageBox.Show("保存失败：" & ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' 功能：刷新按钮，重新载入数据
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim strNo As String = txtProcessNo.Text.Trim()
        LoadData()
        intCurrentRow = FindRowByProcessNo(strNo)
        If intCurrentRow < 0 Then intCurrentRow = 0
        If dtMain.Rows.Count > 0 Then
            ShowRecord()
        Else
            ClearControls()
            blnIsNew = True
        End If
        lblStatusBar.Text = "已刷新"
    End Sub

    ''' <summary>
    ''' 功能：关闭按钮，关窗体
    ''' </summary>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 功能：插入一条新记录（带全局写锁，防并发冲突）
    ''' </summary>
    Private Sub InsertRecord()
        SyncLock WriteLock
            Dim strSql As String = "INSERT INTO tblFMEA_Main " &
            "(strProcessNo, strProcessName, strSection, strProcessType, lngProcessOrder, " &
            "memFunction, memFunctionReq, strDeptName, strOwner, strFmeaTeam, " &
            "strVersion, strStatus, dtmCreateTime, dtmUpdateTime, memRemark, blnIsDeleted) " &
            "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,False)"

            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = txtProcessNo.Text.Trim()
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = txtProcessName.Text.Trim()
                    cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = cboSection.Text
                    cmd.Parameters.Add("p4", OleDbType.VarWChar).Value = cboProcessType.Text
                    cmd.Parameters.Add("p5", OleDbType.Integer).Value = CInt(txtProcessOrder.Text.Trim())
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtFunction.Text
                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtFunctionReq.Text
                    cmd.Parameters.Add("p8", OleDbType.VarWChar).Value = txtDeptName.Text
                    cmd.Parameters.Add("p9", OleDbType.VarWChar).Value = txtOwner.Text
                    cmd.Parameters.Add("p10", OleDbType.VarWChar).Value = txtFmeaTeam.Text
                    cmd.Parameters.Add("p11", OleDbType.VarWChar).Value = txtVersion.Text
                    cmd.Parameters.Add("p12", OleDbType.VarWChar).Value = cboStatus.Text
                    cmd.Parameters.Add("p13", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p14", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p15", OleDbType.LongVarWChar).Value = txtRemark.Text
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：更新当前记录（带全局写锁，防并发冲突）
    ''' </summary>
    Private Sub UpdateRecord()
        If dtMain Is Nothing OrElse dtMain.Rows.Count = 0 Then
            MessageBox.Show("没有可更新的数据")
            Return
        End If
        If intCurrentRow < 0 OrElse intCurrentRow >= dtMain.Rows.Count Then
            MessageBox.Show("行号越界：" & intCurrentRow)
            Return
        End If

        SyncLock WriteLock
            Dim lngID As Long = CLng(dtMain.Rows(intCurrentRow)("lngID"))
            Dim strSql As String = "UPDATE tblFMEA_Main SET " &
            "strProcessNo=?, strProcessName=?, strSection=?, strProcessType=?, lngProcessOrder=?, " &
            "memFunction=?, memFunctionReq=?, strDeptName=?, strOwner=?, strFmeaTeam=?, " &
            "strVersion=?, strStatus=?, dtmUpdateTime=?, memRemark=? " &
            "WHERE lngID=?"

            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = txtProcessNo.Text.Trim()
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = txtProcessName.Text.Trim()
                    cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = cboSection.Text
                    cmd.Parameters.Add("p4", OleDbType.VarWChar).Value = cboProcessType.Text
                    cmd.Parameters.Add("p5", OleDbType.Integer).Value = CInt(txtProcessOrder.Text.Trim())
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtFunction.Text
                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtFunctionReq.Text
                    cmd.Parameters.Add("p8", OleDbType.VarWChar).Value = txtDeptName.Text
                    cmd.Parameters.Add("p9", OleDbType.VarWChar).Value = txtOwner.Text
                    cmd.Parameters.Add("p10", OleDbType.VarWChar).Value = txtFmeaTeam.Text
                    cmd.Parameters.Add("p11", OleDbType.VarWChar).Value = txtVersion.Text
                    cmd.Parameters.Add("p12", OleDbType.VarWChar).Value = cboStatus.Text
                    cmd.Parameters.Add("p13", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p14", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.Parameters.Add("p15", OleDbType.Integer).Value = lngID
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：删除按钮，软删除当前记录（带全局写锁）
    ''' </summary>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If blnIsNew Then
                MessageBox.Show("新增模式下不能删除")
                Return
            End If
            If dtMain Is Nothing OrElse dtMain.Rows.Count = 0 Then Return
            If intCurrentRow < 0 OrElse intCurrentRow >= dtMain.Rows.Count Then
                MessageBox.Show("行号越界")
                Return
            End If

            If MessageBox.Show("确定删除当前记录吗？", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            Dim lngID As Long = CLng(dtMain.Rows(intCurrentRow)("lngID"))

            SyncLock WriteLock
                Using conn As OleDbConnection = GetConnection()
                    Using cmd As New OleDbCommand("UPDATE tblFMEA_Main SET blnIsDeleted=True WHERE lngID=?", conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngID
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End SyncLock

            LoadData()
            intCurrentRow = 0
            If dtMain.Rows.Count > 0 Then
                ShowRecord()
            Else
                ClearControls()
                blnIsNew = True
            End If
            lblStatusBar.Text = "已删除"
        Catch ex As Exception
            MessageBox.Show("删除失败：" & ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
        End Try
    End Sub


    ''' <summary>
    ''' 功能：按工序编号查找 dtMain 中的行号，找不到返回 -1
    ''' </summary>
    Private Function FindRowByProcessNo(ByVal strNo As String) As Integer
        For i As Integer = 0 To dtMain.Rows.Count - 1
            If dtMain.Rows(i)("strProcessNo").ToString() = strNo Then Return i
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 功能：窗体关闭时恢复 Ribbon 按钮可用
    ''' </summary>
    Private Sub L_FmeaMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' 恢复 Ribbon 按钮，否则关掉窗体后按钮再也点不动
        Globals.Ribbons.Ribbon1.btnOpenFmeaMain.Enabled = True
    End Sub
    ''' <summary>
    ''' 功能：上一条，切到上一条记录
    ''' </summary>
    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If dtMain.Rows.Count = 0 Then Return
        If intCurrentRow > 0 Then
            intCurrentRow -= 1
            ShowRecord()
        End If
    End Sub

    ''' <summary>
    ''' 功能：下一条，切到下一条记录
    ''' </summary>
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If dtMain.Rows.Count = 0 Then Return
        If intCurrentRow < dtMain.Rows.Count - 1 Then
            intCurrentRow += 1
            ShowRecord()
        End If
    End Sub

    ''' <summary>
    ''' 功能：跳到第一条记录
    ''' </summary>
    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        If dtMain Is Nothing OrElse dtMain.Rows.Count = 0 Then Return
        intCurrentRow = 0
        ShowRecord()
    End Sub

    ''' <summary>
    ''' 功能：跳到最后一条记录
    ''' </summary>
    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        If dtMain Is Nothing OrElse dtMain.Rows.Count = 0 Then Return
        intCurrentRow = dtMain.Rows.Count - 1
        ShowRecord()
    End Sub



    ''' <summary>
    ''' 功能：把 dtMain 绑定到 DataGridView，并设中文列头
    ''' </summary>
    Private Sub BindGrid()
        ' 先解绑，防止重复绑定
        dgvMain.DataSource = Nothing
        dgvMain.AutoGenerateColumns = False
        dgvMain.Columns.Clear()

        ' 手动加列，只显示需要的
        Dim colNo As New DataGridViewTextBoxColumn()
        colNo.HeaderText = "工序编号"
        colNo.DataPropertyName = "strProcessNo"
        dgvMain.Columns.Add(colNo)

        Dim colName As New DataGridViewTextBoxColumn()
        colName.HeaderText = "工序名称"
        colName.DataPropertyName = "strProcessName"
        dgvMain.Columns.Add(colName)

        Dim colSection As New DataGridViewTextBoxColumn()
        colSection.HeaderText = "工序段"
        colSection.DataPropertyName = "strSection"
        dgvMain.Columns.Add(colSection)

        Dim colType As New DataGridViewTextBoxColumn()
        colType.HeaderText = "工序类型"
        colType.DataPropertyName = "strProcessType"
        dgvMain.Columns.Add(colType)

        Dim colOrder As New DataGridViewTextBoxColumn()
        colOrder.HeaderText = "工序顺序"
        colOrder.DataPropertyName = "lngProcessOrder"
        dgvMain.Columns.Add(colOrder)

        Dim colStatus As New DataGridViewTextBoxColumn()
        colStatus.HeaderText = "状态"
        colStatus.DataPropertyName = "strStatus"
        dgvMain.Columns.Add(colStatus)

        ' 绑定数据
        dgvMain.DataSource = dtMain
    End Sub

    ''' <summary>
    ''' 功能：点表格行，上方控件显示该行详情
    ''' </summary>
    Private Sub dgvMain_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMain.CellClick
        If e.RowIndex < 0 Then Return
        intCurrentRow = e.RowIndex
        ShowRecord()
    End Sub

    ''' <summary>
    ''' 功能：复制当前记录内容，清空编号，进入新增模式
    ''' </summary>
    Private Sub btnCopyNew_Click(sender As Object, e As EventArgs) Handles btnCopyNew.Click
        If dtMain Is Nothing OrElse dtMain.Rows.Count = 0 Then
            MessageBox.Show("没有可复制的记录")
            Return
        End If

        ' 先把当前记录值留在控件上（ShowRecord 已填好），只需清编号
        blnIsNew = True
        txtProcessNo.Text = ""            ' 编号清空，让用户填新的
        txtProcessNo.ReadOnly = False     ' 编号可编辑
        txtProcessNo.Focus()

        lblStatusBar.Text = "复制新增模式，请填写新编号后保存"
    End Sub


End Class