Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Diagnostics
''' <summary>
''' 功能：FMEA 失效模式明细维护窗体
''' </summary>
Public Class L_FmeaDetail

    ' ========== 窗体级变量 ==========

    ''' <summary>
    ''' 功能：标记字典窗体是否已打开，防多开
    ''' </summary>
    Private blnDictOpen As Boolean = False

    ''' <summary>
    ''' 功能:图片路径
    ''' </summary>
    Private strImagePath As String = ""
    Private strPicDir As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\1 Pictures\FMEA_Pictures\FMEA_Detail\"


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
        txtProductChar.Text = If(IsDBNull(dr("strProductChar")), "", dr("strProductChar").ToString())
        txtProcessChar.Text = If(IsDBNull(dr("strProcessChar")), "", dr("strProcessChar").ToString())
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

        cboFailureModeNo.Enabled = blnIsNew

        If dgvDetail.Rows.Count > intCurrentRow Then
            dgvDetail.ClearSelection()
            dgvDetail.Rows(intCurrentRow).Selected = True
        End If


        ' 显示图片
        strImagePath = If(IsDBNull(dr("strImagePath")), "", dr("strImagePath").ToString())
        ShowImage(strImagePath)
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
        txtProductChar.Text = ""
        txtProcessChar.Text = ""
        strImagePath = ""
        If picImage.Image IsNot Nothing Then
            picImage.Image.Dispose()
            picImage.Image = Nothing
        End If
        lblImagePath.Text = "无图片"

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
    ''' 功能：按失效模式编码查找 dtDetail 中的行号，找不到返回 -1
    ''' </summary>
    Private Function FindRowByModeNo(ByVal strNo As String) As Integer
        For i As Integer = 0 To dtDetail.Rows.Count - 1
            If dtDetail.Rows(i)("strFailureModeNo").ToString() = strNo Then Return i
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 功能：保存按钮，校验后 INSERT 或 UPDATE
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not ValidateInput() Then Return

            ' 先记下当前选中的失效模式编码，保存后定位用
            Dim strNo As String = ""
            If cboFailureModeNo.Text.Contains("|") Then
                strNo = cboFailureModeNo.Text.Split("|"c)(0).Trim()
            End If

            If blnIsNew Then
                InsertRecord()
            Else
                UpdateRecord()
            End If


            RefreshFailureModeCombo()
            LoadData()
            BindGrid()

            ' 按刚保存的编码定位
            If Not String.IsNullOrEmpty(strNo) Then
                intCurrentRow = FindRowByModeNo(strNo)
            End If
            If intCurrentRow < 0 Then intCurrentRow = 0
            If dtDetail.Rows.Count > 0 Then
                ShowRecord()
            Else
                ClearControls()
                blnIsNew = True
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

    Private Sub dgvDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellClick
        If e.RowIndex < 0 Then Return

        blnIsNew = False
        RefreshFailureModeCombo()
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
    ''' 功能：插入一条新明细记录（带全局写锁）
    ''' </summary>
    Private Sub InsertRecord()
        SyncLock WriteLock
            ' 从 "FM-001 | 尺寸超差" 拆出编号和名称
            Dim arr As String() = cboFailureModeNo.Text.Split("|"c)
            Dim strNo As String = arr(0).Trim()
            Dim strName As String = If(arr.Length > 1, arr(1).Trim(), "")

            'Dim strSql As String = "INSERT INTO tblFMEA_Detail " &
            '"(lngMainID, strFailureModeNo, strFailureModeName, strProductChar, strProcessChar, " &
            '"memFailureEffect, memFailureCause, memPrevention, memDetection, " &
            '"intSeverity, intOccurrence, intDetection, intRPN, strAP, " &
            '"lngProcessOrder, dtmCreateTime, dtmUpdateTime, memRemark) " &
            '"VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"

            Dim strSql As String = "INSERT INTO tblFMEA_Detail " &
    "(lngMainID, strFailureModeNo, strFailureModeName, strProductChar, strProcessChar, " &
    "memFailureEffect, memFailureCause, memPrevention, memDetection, " &
    "intSeverity, intOccurrence, intDetection, intRPN, strAP, " &
    "lngProcessOrder, dtmCreateTime, dtmUpdateTime, memRemark, strImagePath) " &
    "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"


            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = strNo
                    cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = strName
                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtProductChar.Text.Trim()
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtProcessChar.Text.Trim()
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtFailureEffect.Text
                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtFailureCause.Text
                    cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = txtPrevention.Text
                    cmd.Parameters.Add("p9", OleDbType.LongVarWChar).Value = txtDetection.Text
                    cmd.Parameters.Add("p10", OleDbType.Integer).Value = CInt(txtSeverity.Text.Trim())
                    cmd.Parameters.Add("p11", OleDbType.Integer).Value = CInt(txtOccurrence.Text.Trim())
                    cmd.Parameters.Add("p12", OleDbType.Integer).Value = CInt(txtDetectionScore.Text.Trim())
                    cmd.Parameters.Add("p13", OleDbType.Integer).Value = CInt(txtRPN.Text.Trim())
                    cmd.Parameters.Add("p14", OleDbType.VarWChar).Value = cboAP.Text
                    cmd.Parameters.Add("p15", OleDbType.Integer).Value = CInt(txtDetailOrder.Text.Trim())
                    cmd.Parameters.Add("p16", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p17", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p18", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.Parameters.Add("p19", OleDbType.VarWChar).Value = If(strImagePath Is Nothing, "", strImagePath)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：更新当前明细记录（带全局写锁）
    ''' </summary>
    Private Sub UpdateRecord()
        If dtDetail Is Nothing OrElse dtDetail.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtDetail.Rows.Count Then Return

        SyncLock WriteLock
            ' 从 "FM-001 | 尺寸超差" 拆出编号和名称
            Dim arr As String() = cboFailureModeNo.Text.Split("|"c)
            Dim strNo As String = arr(0).Trim()
            Dim strName As String = If(arr.Length > 1, arr(1).Trim(), "")

            Dim lngID As Long = CLng(dtDetail.Rows(intCurrentRow)("lngID"))

            Dim strSql As String = "UPDATE tblFMEA_Detail SET " &
            "strFailureModeNo=?, strFailureModeName=?, strProductChar=?, strProcessChar=?, " &
            "memFailureEffect=?, memFailureCause=?, memPrevention=?, memDetection=?, " &
            "intSeverity=?, intOccurrence=?, intDetection=?, intRPN=?, strAP=?, " &
            "lngProcessOrder=?, dtmUpdateTime=?, memRemark=?, strImagePath=? " &
            "WHERE lngID=?"

            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strNo
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = strName
                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtProductChar.Text.Trim()
                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtProcessChar.Text.Trim()
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtFailureEffect.Text
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtFailureCause.Text
                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtPrevention.Text
                    cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = txtDetection.Text
                    cmd.Parameters.Add("p9", OleDbType.Integer).Value = CInt(txtSeverity.Text.Trim())
                    cmd.Parameters.Add("p10", OleDbType.Integer).Value = CInt(txtOccurrence.Text.Trim())
                    cmd.Parameters.Add("p11", OleDbType.Integer).Value = CInt(txtDetectionScore.Text.Trim())
                    cmd.Parameters.Add("p12", OleDbType.Integer).Value = CInt(txtRPN.Text.Trim())
                    cmd.Parameters.Add("p13", OleDbType.VarWChar).Value = cboAP.Text
                    cmd.Parameters.Add("p14", OleDbType.Integer).Value = CInt(txtDetailOrder.Text.Trim())
                    cmd.Parameters.Add("p15", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p16", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.Parameters.Add("p17", OleDbType.VarWChar).Value = If(strImagePath Is Nothing, "", strImagePath)
                    cmd.Parameters.Add("p18", OleDbType.Integer).Value = lngID
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
    ''' 功能：按失效模式编码选中下拉框
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

    ''' <summary>
    ''' 功能：按名称查失效模式编号（精确→去空格→模糊）
    ''' </summary>
    Private Function GetModeNoByName(ByVal strName As String) As String
        Dim strClean As String = strName.Trim().Replace("　", "").Replace(" ", "")

        Using conn As OleDbConnection = GetConnection()
            conn.Open()

            Using cmd As New OleDbCommand("SELECT strFailureModeNo, strFailureModeName FROM tblFailureModeDict WHERE blnIsDeleted=False", conn)
                Using rd As OleDbDataReader = cmd.ExecuteReader()
                    Dim strFuzzyNo As String = ""
                    Dim strFuzzyName As String = ""

                    While rd.Read()
                        Dim strDbNo As String = rd("strFailureModeNo").ToString()
                        Dim strDbName As String = rd("strFailureModeName").ToString()
                        Dim strDbClean As String = strDbName.Trim().Replace("　", "").Replace(" ", "")

                        If strDbClean = strClean Then
                            Return strDbNo
                        End If

                        If strFuzzyNo = "" Then
                            If strDbClean.Contains(strClean) OrElse strClean.Contains(strDbClean) Then
                                strFuzzyNo = strDbNo
                                strFuzzyName = strDbName
                            End If
                        End If
                    End While

                    If strFuzzyNo <> "" Then
                        If MessageBox.Show("Excel 里的名称：" & strName & vbCrLf &
                                           "模糊匹配到：" & strFuzzyNo & " | " & strFuzzyName & vbCrLf &
                                           "是否使用？", "模糊匹配", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            Return strFuzzyNo
                        End If
                    End If
                End Using
            End Using
        End Using

        Return ""
    End Function

    ''' <summary>
    ''' 功能：安全转数字，空或非数字返回 0
    ''' </summary>
    Private Function ParseInt(ByVal str As String) As Integer
        Dim intVal As Integer = 0
        Integer.TryParse(str, intVal)
        Return intVal
    End Function

    ''' <summary>
    ''' 功能：按 S/O/D 查 AP 等级（区间匹配）
    ''' </summary>
    Private Function GetAPBySOD(ByVal intS As Integer, ByVal intO As Integer, ByVal intD As Integer) As String
        Using conn As OleDbConnection = GetConnection()
            Dim strSql As String = "SELECT strAP FROM tblAP_Rule " &
            "WHERE blnIsDeleted=False " &
            "AND intSMin<=? AND intSMax>=? " &
            "AND intOMin<=? AND intOMax>=? " &
            "AND intDMin<=? AND intDMax>=?"
            Using cmd As New OleDbCommand(strSql, conn)
                cmd.Parameters.Add("p1", OleDbType.Integer).Value = intS
                cmd.Parameters.Add("p2", OleDbType.Integer).Value = intS
                cmd.Parameters.Add("p3", OleDbType.Integer).Value = intO
                cmd.Parameters.Add("p4", OleDbType.Integer).Value = intO
                cmd.Parameters.Add("p5", OleDbType.Integer).Value = intD
                cmd.Parameters.Add("p6", OleDbType.Integer).Value = intD
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing Then Return ""
                If IsDBNull(result) Then Return ""
                Return result.ToString()
            End Using
        End Using
    End Function

    ''' <summary>
    ''' 功能：安全读取 Excel 单元格文本，保留换行
    ''' </summary>
    Private Function GetCellText(ByVal ws As Excel.Worksheet, ByVal strAddr As String) As String
        Try
            Dim rng As Excel.Range = ws.Range(strAddr)
            If rng.Value Is Nothing Then Return ""
            If rng.Value Is DBNull.Value Then Return ""

            Dim strVal As String = rng.Value.ToString()
            If strVal Is Nothing Then Return ""

            ' 先归一成 vbLf，再统一成 vbCrLf，避免重复转换
            strVal = strVal.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)

            Return strVal.Trim()
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' 功能：从 Excel 选中区域导入 FMEA 明细
    ''' </summary>
    Private Sub ImportDetailFromExcel()
        If MainID = 0 Then
            MessageBox.Show("请先选择工序")
            Return
        End If

        ' 自动算默认地址：当前选区首行到末行，拼 C~L
        Dim strDefault As String = ""
        Try
            Dim rngSel As Excel.Range = xlapp.Selection
            If rngSel IsNot Nothing Then
                Dim intFirstRow As Integer = rngSel.Row
                Dim intLastRow As Integer = rngSel.Row + rngSel.Rows.Count - 1
                strDefault = "C" & intFirstRow & ":L" & intLastRow
            End If
        Catch
            strDefault = ""
        End Try

        ' 弹 InputBox，默认填好地址
        Dim rngInput As Excel.Range = Nothing
        Try
            Dim obj As Object = xlapp.InputBox("请确认导入区域（已按当前选区自动填好）", "选择导入区域", strDefault, Type:=8)
            If obj Is Nothing Then Return
            rngInput = CType(obj, Excel.Range)
        Catch
            Return
        End Try

        If rngInput Is Nothing Then Return
        If rngInput.Rows.Count < 1 Then
            MessageBox.Show("请至少选择一行")
            Return
        End If

        If MessageBox.Show("即将导入 " & rngInput.Rows.Count & " 行，是否继续？", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Return
        End If

        Dim ws As Excel.Worksheet = rngInput.Worksheet
        Dim intStartRow As Integer = rngInput.Row
        Dim intRowCount As Integer = rngInput.Rows.Count
        Dim intSuccess As Integer = 0
        Dim intFail As Integer = 0
        ' 取当前主表最大顺序号，接着排
        Dim intMaxOrder As Integer = GetMaxOrderByMain(MainID)


        For i As Integer = 0 To intRowCount - 1
            Dim intRow As Integer = intStartRow + i
            ' 顺序号按行号递增，10、20、30
            Dim intOrder As Integer = intMaxOrder + (i + 1) * 10

            Dim strProductChar As String = GetCellText(ws, "C" & intRow)
            Dim strProcessChar As String = GetCellText(ws, "D" & intRow)
            Dim strModeName As String = GetCellText(ws, "E" & intRow)
            Dim strEffect As String = GetCellText(ws, "F" & intRow)
            Dim intS As Integer = ParseInt(GetCellText(ws, "G" & intRow))
            Dim strCause As String = GetCellText(ws, "H" & intRow)
            Dim strPrevention As String = GetCellText(ws, "I" & intRow)
            Dim intO As Integer = ParseInt(GetCellText(ws, "J" & intRow))
            Dim strDetection As String = GetCellText(ws, "K" & intRow)
            Dim intD As Integer = ParseInt(GetCellText(ws, "L" & intRow))

            If strProductChar Is Nothing Then strProductChar = ""
            If strProcessChar Is Nothing Then strProcessChar = ""
            If strModeName Is Nothing Then strModeName = ""
            If strEffect Is Nothing Then strEffect = ""
            If strCause Is Nothing Then strCause = ""
            If strPrevention Is Nothing Then strPrevention = ""
            If strDetection Is Nothing Then strDetection = ""

            If strModeName = "" Then
                intFail += 1
                Continue For
            End If

            ' 匹配编号
            Dim strModeNo As String = GetModeNoByName(strModeName)
            If strModeNo Is Nothing Then strModeNo = ""

            ' 没匹配到，问是否新建
            If strModeNo = "" Then
                Dim dr As DialogResult = MessageBox.Show("字典里找不到失效模式：" & strModeName & vbCrLf & vbCrLf &
                                                     "是否新建一条字典记录？", "新建失效模式", MessageBoxButtons.YesNoCancel)
                If dr = DialogResult.Cancel Then
                    Exit For
                ElseIf dr = DialogResult.Yes Then
                    strModeNo = GetNextModeNo()
                    InsertModeDict(strModeNo, strModeName)
                Else
                    intFail += 1
                    Continue For
                End If
            Else
                ' 匹配到了，检查名称是否一致
                Dim strDbName As String = GetModeNameByNo(strModeNo)
                If strDbName <> strModeName Then
                    If MessageBox.Show("字典里该编号的名称：" & strDbName & vbCrLf &
                                   "Excel 里的名称：" & strModeName & vbCrLf & vbCrLf &
                                   "是否用 Excel 的名称覆盖字典？", "名称不一致", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        UpdateModeName(strModeNo, strModeName)
                    End If
                End If
            End If

            Dim intRPN As Integer = intS * intO * intD
            Dim strAP As String = GetAPBySOD(intS, intO, intD)
            If strAP Is Nothing Then strAP = ""

            SyncLock WriteLock
                Using conn As OleDbConnection = GetConnection()
                    conn.Open()
                    Dim strSql As String = "INSERT INTO tblFMEA_Detail " &
                    "(lngMainID, strFailureModeNo, strFailureModeName, strProductChar, strProcessChar, " &
                    "memFailureEffect, memFailureCause, memPrevention, memDetection, " &
                    "intSeverity, intOccurrence, intDetection, intRPN, strAP, " &
                    "lngProcessOrder, dtmCreateTime, dtmUpdateTime, memRemark, strImagePath) " &
                    "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"
                    Using cmd As New OleDbCommand(strSql, conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                        cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = strModeNo
                        cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = strModeName
                        cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = strProductChar
                        cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = strProcessChar

                        cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = strEffect
                        cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = strCause
                        cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = strPrevention
                        cmd.Parameters.Add("p9", OleDbType.LongVarWChar).Value = strDetection
                        cmd.Parameters.Add("p10", OleDbType.Integer).Value = intS
                        cmd.Parameters.Add("p11", OleDbType.Integer).Value = intO
                        cmd.Parameters.Add("p12", OleDbType.Integer).Value = intD
                        cmd.Parameters.Add("p13", OleDbType.Integer).Value = intRPN
                        cmd.Parameters.Add("p14", OleDbType.VarWChar).Value = strAP
                        cmd.Parameters.Add("p15", OleDbType.Integer).Value = intOrder
                        cmd.Parameters.Add("p16", OleDbType.Date).Value = Now
                        cmd.Parameters.Add("p17", OleDbType.Date).Value = Now
                        cmd.Parameters.Add("p18", OleDbType.LongVarWChar).Value = ""
                        cmd.Parameters.Add("p19", OleDbType.VarWChar).Value = ""
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End SyncLock

            intSuccess += 1
        Next

        '' 刷新下拉框
        'RefreshFailureModeCombo()
        'LoadData()
        'BindGrid()

        '' 定位到最后一条
        'If dtDetail.Rows.Count > 0 Then
        '    intCurrentRow = dtDetail.Rows.Count - 1
        '    ShowRecord()
        'End If

        LoadData()
        BindGrid()

        If dtDetail.Rows.Count > 0 Then
            intCurrentRow = dtDetail.Rows.Count - 1
            RefreshFailureModeCombo()   ' 显示前刷
            ShowRecord()
        End If

        blnIsNew = False
        lblStatusBar.Text = "导入完成，成功 " & intSuccess & " 条，失败 " & intFail & " 条"
        MessageBox.Show("导入完成" & vbCrLf & "成功：" & intSuccess & " 条" & vbCrLf & "失败：" & intFail & " 条")
    End Sub
    ''' <summary>
    ''' 功能：从 Excel 导入明细按钮
    ''' </summary>
    Private Sub btnImportDetail_Click(sender As Object, e As EventArgs) Handles btnImportDetail.Click
        Try
            ImportDetailFromExcel()
        Catch ex As Exception
            MessageBox.Show("导入失败：" & ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：根据路径显示图片，空则清空
    ''' </summary>
    Private Sub ShowImage(ByVal strPath As String)
        ' 先释放旧图，防内存泄漏
        If picImage.Image IsNot Nothing Then
            picImage.Image.Dispose()
            picImage.Image = Nothing
        End If

        If String.IsNullOrEmpty(strPath) Then
            lblImagePath.Text = "无图片"
            Return
        End If

        lblImagePath.Text = System.IO.Path.GetFileName(strPath)

        Try
            If System.IO.File.Exists(strPath) Then
                ' 用 FileStream 读，不锁文件
                Using fs As New System.IO.FileStream(strPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    picImage.Image = Image.FromStream(fs)
                End Using
            Else
                lblImagePath.Text = "图片不存在：" & System.IO.Path.GetFileName(strPath)
            End If
        Catch ex As Exception
            lblImagePath.Text = "图片加载失败"
        End Try
    End Sub

    ''' <summary>
    ''' 功能：点图片，用系统默认程序打开大图
    ''' </summary>
    Private Sub picImage_Click(sender As Object, e As EventArgs) Handles picImage.Click
        If String.IsNullOrEmpty(strImagePath) Then Return

        If Not System.IO.File.Exists(strImagePath) Then
            MessageBox.Show("图片不存在：" & strImagePath)
            Return
        End If

        Try
            Process.Start(strImagePath)
        Catch ex As Exception
            MessageBox.Show("打开图片失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：选图片，复制到共享盘并显示
    ''' </summary>
    Private Sub btnPickImage_Click(sender As Object, e As EventArgs) Handles btnPickImage.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp"
            ofd.Title = "选择失效图片"
            If ofd.ShowDialog() <> DialogResult.OK Then Return

            Try
                ' 目录不存在就建
                If Not System.IO.Directory.Exists(strPicDir) Then
                    System.IO.Directory.CreateDirectory(strPicDir)
                End If

                ' 自动命名：失效模式编码_日期_序号.jpg
                Dim strModeNo As String = ""
                If cboFailureModeNo.Text.Contains("|") Then
                    strModeNo = cboFailureModeNo.Text.Split("|"c)(0).Trim()
                End If
                Dim strDate As String = DateTime.Now.ToString("yyyyMMdd")
                Dim strExt As String = System.IO.Path.GetExtension(ofd.FileName)

                ' 找不重名的序号
                Dim intSeq As Integer = 1
                Dim strNewFile As String = ""
                Do
                    strNewFile = strPicDir & strModeNo & "_" & strDate & "_" & intSeq.ToString("00") & strExt
                    intSeq += 1
                Loop While System.IO.File.Exists(strNewFile)

                ' 复制文件
                System.IO.File.Copy(ofd.FileName, strNewFile, False)

                ' 存路径、显示
                strImagePath = strNewFile
                ShowImage(strImagePath)
            Catch ex As Exception
                MessageBox.Show("选图片失败：" & ex.Message)
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' 功能：清除当前图片（只清路径，不删文件）
    ''' </summary>
    Private Sub btnClearImage_Click(sender As Object, e As EventArgs) Handles btnClearImage.Click
        strImagePath = ""
        ShowImage("")
    End Sub


    ''' <summary>
    ''' 功能：按编号查失效模式名称
    ''' </summary>
    Private Function GetModeNameByNo(ByVal strNo As String) As String
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT strFailureModeName FROM tblFailureModeDict WHERE strFailureModeNo=?", conn)
                cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strNo
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return ""
                Return result.ToString()
            End Using
        End Using
    End Function

    ''' <summary>
    ''' 功能：生成下一个失效模式编号，FM-001 格式
    ''' </summary>
    Private Function GetNextModeNo() As String
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT MAX(strFailureModeNo) FROM tblFailureModeDict WHERE strFailureModeNo LIKE 'FM-%'", conn)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then
                    Return "FM-001"
                End If
                Dim strMax As String = result.ToString()
                Dim intNum As Integer = 0
                Integer.TryParse(strMax.Replace("FM-", ""), intNum)
                Return "FM-" & (intNum + 1).ToString("000")
            End Using
        End Using
    End Function
    ''' <summary>
    ''' 功能：新建字典记录（排序号自动取最大+10）
    ''' </summary>
    Private Sub InsertModeDict(ByVal strNo As String, ByVal strName As String)
        SyncLock WriteLock
            Using conn As OleDbConnection = GetConnection()
                conn.Open()

                ' 取最大排序号 +10
                Dim intSort As Integer = 10
                Using cmdMax As New OleDbCommand("SELECT MAX(lngSortOrder) FROM tblFailureModeDict WHERE blnIsDeleted=False", conn)
                    Dim result As Object = cmdMax.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        intSort = CInt(result) + 10
                    End If
                End Using

                ' 插入
                Using cmd As New OleDbCommand("INSERT INTO tblFailureModeDict (strFailureModeNo, strFailureModeName, lngSortOrder, blnIsDeleted, dtmCreateTime) VALUES (?,?,?,?,?)", conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strNo
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = strName
                    cmd.Parameters.Add("p3", OleDbType.Integer).Value = intSort
                    cmd.Parameters.Add("p4", OleDbType.Boolean).Value = False
                    cmd.Parameters.Add("p5", OleDbType.Date).Value = Now
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：更新字典表的失效模式名称
    ''' </summary>
    Private Sub UpdateModeName(ByVal strNo As String, ByVal strName As String)
        SyncLock WriteLock
            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand("UPDATE tblFailureModeDict SET strFailureModeName=? WHERE strFailureModeNo=?", conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strName
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = strNo
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub


    ''' <summary>
    ''' 功能：只刷新失效模式编码下拉框
    ''' </summary>
    Private Sub RefreshFailureModeCombo()
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
        End Try
    End Sub
    ''' <summary>
    ''' 功能：取当前主表下明细的最大顺序号
    ''' </summary>
    Private Function GetMaxOrderByMain(ByVal lngMainID As Long) As Integer
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT MAX(lngProcessOrder) FROM tblFMEA_Detail WHERE lngMainID=?", conn)
                cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngMainID
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return 0
                Return CInt(result)
            End Using
        End Using
    End Function

    ''' <summary>
    ''' 功能：打开失效模式字典维护窗体（防多开）
    ''' </summary>
    Private Sub btnOpenDict_Click(sender As Object, e As EventArgs) Handles btnOpenDict.Click
        If blnDictOpen Then
            MessageBox.Show("字典窗体已打开")
            Return
        End If

        Dim f As New L_FailureModeDict()
        AddHandler f.FormClosed, Sub()
                                     blnDictOpen = False
                                     ' 关闭后刷新下拉框，新条目能选到
                                     RefreshFailureModeCombo()
                                 End Sub
        blnDictOpen = True
        f.Show()
    End Sub




End Class