Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms

''' <summary>
''' 功能：FMEA 失效模式明细维护窗体
''' </summary>
Public Class L_FmeaDetail

    ' ========== 窗体级变量 ==========

    ''' <summary>
    ''' 功能：当前主表 ID，由主窗体传入
    ''' </summary>
    Public Property MainID As Long = 0

    ''' <summary>
    ''' 功能：存放从数据库读出的明细数据
    ''' </summary>
    Private dtDetail As DataTable

    ''' <summary>
    ''' 功能：当前显示的是第几行（0 开始）
    ''' </summary>
    Private intCurrentRow As Integer = 0

    ''' <summary>
    ''' 功能：是否处于新增模式。True=新增，False=编辑
    ''' </summary>
    Private blnIsNew As Boolean = False

    ' ========== 窗体加载 ==========

    ''' <summary>
    ''' 功能：窗体加载，初始化下拉框、显示工序信息、载入明细
    ''' </summary>
    Private Sub L_FmeaDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            InitComboBox()
            ShowMainInfo()
            LoadData()
            BindGrid()
            If dtDetail.Rows.Count > 0 Then
                intCurrentRow = 0
                ShowRecord()
            Else
                ClearControls()
                blnIsNew = True
            End If
        Catch ex As Exception
            MessageBox.Show("加载失败：" & ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：初始化下拉框的选项，禁止手输
    ''' </summary>
    Private Sub InitComboBox()
        cboFailureModeNo.Items.Clear()
        Try
            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand("SELECT strFailureModeNo, strFailureModeName FROM tblFailureModeDict WHERE blnIsDeleted=False ORDER BY lngSortOrder", conn)
                    conn.Open()
                    Using rd As OleDbDataReader = cmd.ExecuteReader()
                        While rd.Read()
                            cboFailureModeNo.Items.Add(rd("strFailureModeNo").ToString() & " | " & rd("strFailureModeName").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("加载失效模式字典失败：" & ex.Message)
        End Try
        cboFailureModeNo.DropDownStyle = ComboBoxStyle.DropDownList

        cboAP.Items.Clear()
        cboAP.Items.Add("高")
        cboAP.Items.Add("中")
        cboAP.Items.Add("低")
        cboAP.DropDownStyle = ComboBoxStyle.DropDownList
        cboAP.Enabled = False
    End Sub

    ''' <summary>
    ''' 功能：显示顶部当前工序信息（只读）
    ''' </summary>
    Private Sub ShowMainInfo()
        Try
            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand("SELECT strProcessNo, strProcessName FROM tblFMEA_Main WHERE lngID=?", conn)
                    cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                    conn.Open()
                    Using rd As OleDbDataReader = cmd.ExecuteReader()
                        If rd.Read() Then
                            txtMainNo.Text = rd("strProcessNo").ToString()
                            txtMainName.Text = rd("strProcessName").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("加载工序信息失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：从数据库读取当前工序的明细记录，存入 dtDetail
    ''' </summary>
    Private Sub LoadData()
        Using conn As OleDbConnection = GetConnection()
            Dim strSql As String = "SELECT * FROM tblFMEA_Detail " &
                                   "WHERE lngMainID = ? " &
                                   "ORDER BY lngProcessOrder"
            Using da As New OleDbDataAdapter(strSql, conn)
                da.SelectCommand.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                dtDetail = New DataTable()
                da.Fill(dtDetail)
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' 功能：把 dtDetail 绑定到 DataGridView，并设中文列头
    ''' </summary>
    Private Sub BindGrid()
        dgvDetail.DataSource = Nothing
        dgvDetail.AutoGenerateColumns = False
        dgvDetail.Columns.Clear()

        Dim colNo As New DataGridViewTextBoxColumn()
        colNo.HeaderText = "失效模式编码"
        colNo.DataPropertyName = "strFailureModeNo"
        dgvDetail.Columns.Add(colNo)

        Dim colName As New DataGridViewTextBoxColumn()
        colName.HeaderText = "失效模式名称"
        colName.DataPropertyName = "strFailureModeName"
        dgvDetail.Columns.Add(colName)

        Dim colS As New DataGridViewTextBoxColumn()
        colS.HeaderText = "S"
        colS.DataPropertyName = "intSeverity"
        dgvDetail.Columns.Add(colS)

        Dim colO As New DataGridViewTextBoxColumn()
        colO.HeaderText = "O"
        colO.DataPropertyName = "intOccurrence"
        dgvDetail.Columns.Add(colO)

        Dim colD As New DataGridViewTextBoxColumn()
        colD.HeaderText = "D"
        colD.DataPropertyName = "intDetection"
        dgvDetail.Columns.Add(colD)

        Dim colRPN As New DataGridViewTextBoxColumn()
        colRPN.HeaderText = "RPN"
        colRPN.DataPropertyName = "intRPN"
        dgvDetail.Columns.Add(colRPN)

        Dim colAP As New DataGridViewTextBoxColumn()
        colAP.HeaderText = "AP"
        colAP.DataPropertyName = "strAP"
        dgvDetail.Columns.Add(colAP)

        dgvDetail.DataSource = dtDetail
    End Sub

    ''' <summary>
    ''' 功能：把 dtDetail 中当前行的数据显示到窗体控件上
    ''' </summary>
    Private Sub ShowRecord()
        If dtDetail Is Nothing OrElse dtDetail.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtDetail.Rows.Count Then Return

        Dim dr As DataRow = dtDetail.Rows(intCurrentRow)

        SetComboByModeNo(If(IsDBNull(dr("strFailureModeNo")), "", dr("strFailureModeNo").ToString()))
        txtFailureModeName.Text = If(IsDBNull(dr("strFailureModeName")), "", dr("strFailureModeName").ToString())
        txtFailureEffect.Text = If(IsDBNull(dr("memFailureEffect")), "", dr("memFailureEffect").ToString())
        txtFailureCause.Text = If(IsDBNull(dr("memFailureCause")), "", dr("memFailureCause").ToString())
        txtPrevention.Text = If(IsDBNull(dr("memPrevention")), "", dr("memPrevention").ToString())
        txtDetection.Text = If(IsDBNull(dr("memDetection")), "", dr("memDetection").ToString())
        txtSeverity.Text = If(IsDBNull(dr("intSeverity")), "0", dr("intSeverity").ToString())
        txtOccurrence.Text = If(IsDBNull(dr("intOccurrence")), "0", dr("intOccurrence").ToString())
        txtDetectionScore.Text = If(IsDBNull(dr("intDetection")), "0", dr("intDetection").ToString())
        txtRPN.Text = If(IsDBNull(dr("intRPN")), "0", dr("intRPN").ToString())
        cboAP.Text = If(IsDBNull(dr("strAP")), "", dr("strAP").ToString())
        txtDetailOrder.Text = If(IsDBNull(dr("lngProcessOrder")), "0", dr("lngProcessOrder").ToString())
        txtRemark.Text = If(IsDBNull(dr("memRemark")), "", dr("memRemark").ToString())

        ' 非新增模式：编码只读
        cboFailureModeNo.Enabled = blnIsNew

        ' 同步表格高亮
        If dgvDetail.Rows.Count > intCurrentRow Then
            dgvDetail.ClearSelection()
            dgvDetail.Rows(intCurrentRow).Selected = True
        End If

        lblStatusBar.Text = "当前第 " & (intCurrentRow + 1) & " 条 / 共 " & dtDetail.Rows.Count & " 条"
    End Sub

    ''' <summary>
    ''' 功能：清空所有录入控件，准备新增一条记录
    ''' </summary>
    Private Sub ClearControls()
        cboFailureModeNo.SelectedIndex = -1
        txtFailureModeName.Text = ""
        txtFailureEffect.Text = ""
        txtFailureCause.Text = ""
        txtPrevention.Text = ""
        txtDetection.Text = ""
        txtSeverity.Text = "0"
        txtOccurrence.Text = "0"
        txtDetectionScore.Text = "0"
        txtRPN.Text = "0"
        cboAP.SelectedIndex = -1
        txtDetailOrder.Text = "0"
        txtRemark.Text = ""

        cboFailureModeNo.Enabled = True
        lblStatusBar.Text = "新增模式，请填写后保存"
    End Sub

    ''' <summary>
    ''' 功能：保存前校验输入是否合法
    ''' </summary>
    Private Function ValidateInput() As Boolean
        If cboFailureModeNo.SelectedIndex < 0 Then
            MessageBox.Show("请选择失效模式编码")
            cboFailureModeNo.Focus()
            Return False
        End If

        Dim intS, intO, intD As Integer
        If Not Integer.TryParse(txtSeverity.Text.Trim(), intS) OrElse intS < 1 OrElse intS > 10 Then
            MessageBox.Show("严重度S必须是 1-10 的数字")
            txtSeverity.Focus()
            Return False
        End If
        If Not Integer.TryParse(txtOccurrence.Text.Trim(), intO) OrElse intO < 1 OrElse intO > 10 Then
            MessageBox.Show("频度O必须是 1-10 的数字")
            txtOccurrence.Focus()
            Return False
        End If
        If Not Integer.TryParse(txtDetectionScore.Text.Trim(), intD) OrElse intD < 1 OrElse intD > 10 Then
            MessageBox.Show("探测度D必须是 1-10 的数字")
            txtDetectionScore.Focus()
            Return False
        End If

        Return True
    End Function

    ' ========== 按钮事件 ==========

    ''' <summary>
    ''' 功能：新增按钮，进入新增模式
    ''' </summary>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        blnIsNew = True
        ClearControls()
        cboFailureModeNo.Focus()
    End Sub

    ''' <summary>
    ''' 功能：复制新增，把当前记录值填入控件，编码清空
    ''' </summary>
    Private Sub btnCopyNew_Click(sender As Object, e As EventArgs) Handles btnCopyNew.Click
        Try
            If dtDetail Is Nothing OrElse dtDetail.Rows.Count = 0 Then
                MessageBox.Show("没有可复制的记录")
                Return
            End If
            blnIsNew = True
            cboFailureModeNo.SelectedIndex = -1
            cboFailureModeNo.Enabled = True
            cboFailureModeNo.Focus()
            lblStatusBar.Text = "复制新增模式，请选择新编码后保存"
        Catch ex As Exception
            MessageBox.Show("复制新增失败：" & ex.Message)
        End Try
    End Sub

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

            LoadData()
            BindGrid()
            If dtDetail.Rows.Count > 0 Then
                intCurrentRow = dtDetail.Rows.Count - 1
                ShowRecord()
            End If
            blnIsNew = False
            lblStatusBar.Text = "保存成功"
        Catch ex As Exception
            MessageBox.Show("保存失败：" & ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：删除按钮，物理删除当前明细
    ''' </summary>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If blnIsNew Then
                MessageBox.Show("新增模式下不能删除")
                Return
            End If
            If dtDetail Is Nothing OrElse dtDetail.Rows.Count = 0 Then Return

            If MessageBox.Show("确定删除当前明细吗？", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            Dim lngID As Long = CLng(dtDetail.Rows(intCurrentRow)("lngID"))

            SyncLock WriteLock
                Using conn As OleDbConnection = GetConnection()
                    Using cmd As New OleDbCommand("DELETE FROM tblFMEA_Detail WHERE lngID=?", conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngID
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End SyncLock

            LoadData()
            BindGrid()
            intCurrentRow = 0
            If dtDetail.Rows.Count > 0 Then
                ShowRecord()
            Else
                ClearControls()
                blnIsNew = True
            End If
            lblStatusBar.Text = "已删除"
        Catch ex As Exception
            MessageBox.Show("删除失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：刷新按钮，重新载入数据
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadData()
            BindGrid()
            If dtDetail.Rows.Count > 0 Then
                If intCurrentRow >= dtDetail.Rows.Count Then intCurrentRow = 0
                ShowRecord()
            Else
                ClearControls()
                blnIsNew = True
            End If
            lblStatusBar.Text = "已刷新"
        Catch ex As Exception
            MessageBox.Show("刷新失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：关闭按钮
    ''' </summary>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 功能：点表格行，上方控件显示该行详情
    ''' </summary>
    Private Sub dgvDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellClick
        If e.RowIndex < 0 Then Return
        intCurrentRow = e.RowIndex
        ShowRecord()
    End Sub

    ''' <summary>
    ''' 功能：失效模式编码改变时，自动带出名称
    ''' </summary>
    Private Sub cboFailureModeNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFailureModeNo.SelectedIndexChanged
        If cboFailureModeNo.SelectedIndex < 0 Then Return
        Dim strNo As String = GetModeNoFromCombo()
        If strNo = "" Then Return

        Try
            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand("SELECT strFailureModeName FROM tblFailureModeDict WHERE strFailureModeNo=?", conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strNo
                    conn.Open()
                    Dim obj As Object = cmd.ExecuteScalar()
                    If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then
                        txtFailureModeName.Text = obj.ToString()
                    Else
                        txtFailureModeName.Text = ""
                    End If
                End Using
            End Using
        Catch ex As Exception
            txtFailureModeName.Text = ""
        End Try
    End Sub

    ''' <summary>
    ''' 功能：S/O/D 任一改变，自动算 RPN 和 AP
    ''' </summary>
    Private Sub CalcRPN()
        Dim intS, intO, intD As Integer
        If Not Integer.TryParse(txtSeverity.Text.Trim(), intS) Then intS = 0
        If Not Integer.TryParse(txtOccurrence.Text.Trim(), intO) Then intO = 0
        If Not Integer.TryParse(txtDetectionScore.Text.Trim(), intD) Then intD = 0

        ' 自动算 RPN
        txtRPN.Text = (intS * intO * intD).ToString()

        ' 查 AP 规则，区间匹配
        If intS > 0 AndAlso intO > 0 AndAlso intD > 0 Then
            Try
                Dim strSql As String = "SELECT TOP 1 strAP FROM tblAP_Rule " &
                "WHERE ? BETWEEN intSMin AND intSMax " &
                "AND ? BETWEEN intOMin AND intOMax " &
                "AND ? BETWEEN intDMin AND intDMax " &
                "AND blnIsDeleted=False " &
                "ORDER BY lngSortOrder"

                Using conn As OleDbConnection = GetConnection()
                    Using cmd As New OleDbCommand(strSql, conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = intS
                        cmd.Parameters.Add("p2", OleDbType.Integer).Value = intO
                        cmd.Parameters.Add("p3", OleDbType.Integer).Value = intD
                        conn.Open()
                        Dim obj As Object = cmd.ExecuteScalar()
                        If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then
                            cboAP.Text = obj.ToString()
                        Else
                            cboAP.SelectedIndex = -1   ' 清空选择
                        End If
                    End Using
                End Using
            Catch ex As Exception
                cboAP.SelectedIndex = -1
            End Try
        Else
            cboAP.SelectedIndex = -1
        End If
    End Sub

    Private Sub txtSeverity_TextChanged(sender As Object, e As EventArgs) Handles txtSeverity.TextChanged
        CalcRPN()
    End Sub

    Private Sub txtOccurrence_TextChanged(sender As Object, e As EventArgs) Handles txtOccurrence.TextChanged
        CalcRPN()
    End Sub

    Private Sub txtDetectionScore_TextChanged(sender As Object, e As EventArgs) Handles txtDetectionScore.TextChanged
        CalcRPN()
    End Sub

    ' ========== 数据库操作 ==========

    ''' <summary>
    ''' 功能：插入一条新明细
    ''' </summary>
    Private Sub InsertRecord()
        SyncLock WriteLock
            Dim strSql As String = "INSERT INTO tblFMEA_Detail " &
                "(lngMainID, strFailureModeNo, strFailureModeName, memFailureEffect, memFailureCause, " &
                "memPrevention, memDetection, intSeverity, intOccurrence, intDetection, intRPN, strAP, " &
                "lngProcessOrder, dtmCreateTime, dtmUpdateTime, memRemark) " &
                "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"

            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = GetModeNoFromCombo()
                    cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = txtFailureModeName.Text
                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtFailureEffect.Text
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtFailureCause.Text
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtPrevention.Text
                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtDetection.Text
                    cmd.Parameters.Add("p8", OleDbType.Integer).Value = CInt(txtSeverity.Text)
                    cmd.Parameters.Add("p9", OleDbType.Integer).Value = CInt(txtOccurrence.Text)
                    cmd.Parameters.Add("p10", OleDbType.Integer).Value = CInt(txtDetectionScore.Text)
                    cmd.Parameters.Add("p11", OleDbType.Integer).Value = CInt(txtRPN.Text)
                    cmd.Parameters.Add("p12", OleDbType.VarWChar).Value = cboAP.Text
                    cmd.Parameters.Add("p13", OleDbType.Integer).Value = CInt(txtDetailOrder.Text)
                    cmd.Parameters.Add("p14", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p15", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p16", OleDbType.LongVarWChar).Value = txtRemark.Text
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：更新当前明细
    ''' </summary>
    Private Sub UpdateRecord()
        If dtDetail Is Nothing OrElse dtDetail.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtDetail.Rows.Count Then Return

        SyncLock WriteLock
            Dim lngID As Long = CLng(dtDetail.Rows(intCurrentRow)("lngID"))
            Dim strSql As String = "UPDATE tblFMEA_Detail SET " &
                "strFailureModeNo=?, strFailureModeName=?, memFailureEffect=?, memFailureCause=?, " &
                "memPrevention=?, memDetection=?, intSeverity=?, intOccurrence=?, intDetection=?, " &
                "intRPN=?, strAP=?, lngProcessOrder=?, dtmUpdateTime=?, memRemark=? " &
                "WHERE lngID=?"

            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = GetModeNoFromCombo()
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = txtFailureModeName.Text
                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtFailureEffect.Text
                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtFailureCause.Text
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtPrevention.Text
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtDetection.Text
                    cmd.Parameters.Add("p7", OleDbType.Integer).Value = CInt(txtSeverity.Text)
                    cmd.Parameters.Add("p8", OleDbType.Integer).Value = CInt(txtOccurrence.Text)
                    cmd.Parameters.Add("p9", OleDbType.Integer).Value = CInt(txtDetectionScore.Text)
                    cmd.Parameters.Add("p10", OleDbType.Integer).Value = CInt(txtRPN.Text)
                    cmd.Parameters.Add("p11", OleDbType.VarWChar).Value = cboAP.Text
                    cmd.Parameters.Add("p12", OleDbType.Integer).Value = CInt(txtDetailOrder.Text)
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
    ''' 功能：从下拉框显示文本中提取编码（"FM-001 | 尺寸超差" → "FM-001"）
    ''' </summary>
    Private Function GetModeNoFromCombo() As String
        If cboFailureModeNo.SelectedIndex < 0 Then Return ""
        Dim strItem As String = cboFailureModeNo.Text
        Dim arr As String() = strItem.Split("|"c)
        If arr.Length > 0 Then Return arr(0).Trim()
        Return ""
    End Function

    ''' <summary>
    ''' 功能：按编码在下拉框中定位选中项
    ''' </summary>
    Private Sub SetComboByModeNo(ByVal strNo As String)
        For i As Integer = 0 To cboFailureModeNo.Items.Count - 1
            Dim strItem As String = cboFailureModeNo.Items(i).ToString()
            Dim arr As String() = strItem.Split("|"c)
            If arr.Length > 0 AndAlso arr(0).Trim() = strNo Then
                cboFailureModeNo.SelectedIndex = i
                Return
            End If
        Next
        cboFailureModeNo.SelectedIndex = -1
    End Sub

End Class