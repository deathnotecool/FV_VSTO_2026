Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms

''' <summary>
''' 功能：失效模式字典维护窗体
''' </summary>
Public Class L_FailureModeDict

    ''' <summary>
    ''' 功能：存放字典数据
    ''' </summary>
    Private dtDict As DataTable

    ''' <summary>
    ''' 功能：当前显示行号
    ''' </summary>
    Private intCurrentRow As Integer = 0

    ''' <summary>
    ''' 功能：是否新增模式
    ''' </summary>
    Private blnIsNew As Boolean = False

    ''' <summary>
    ''' 功能：窗体加载
    ''' </summary>
    Private Sub L_FailureModeDict_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            InitComboBox()
            LoadData()
            BindGrid()
            If dtDict.Rows.Count > 0 Then
                intCurrentRow = 0
                ShowRecord()
            Else
                ClearControls()
                blnIsNew = True
            End If
        Catch ex As Exception
            MessageBox.Show("加载失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：初始化类别下拉框
    ''' </summary>
    Private Sub InitComboBox()
        cboCategory.Items.Clear()
        cboCategory.Items.Add("尺寸")
        cboCategory.Items.Add("外观")
        cboCategory.Items.Add("功能")
        cboCategory.Items.Add("材料")
        cboCategory.Items.Add("其他")
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    ''' <summary>
    ''' 功能：载入字典数据
    ''' </summary>
    Private Sub LoadData()
        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter("SELECT * FROM tblFailureModeDict WHERE blnIsDeleted=False ORDER BY lngSortOrder, strFailureModeNo", conn)
                dtDict = New DataTable()
                da.Fill(dtDict)
            End Using
        End Using
        BindGrid()
    End Sub

    ''' <summary>
    ''' 功能：绑定 DataGridView
    ''' </summary>
    Private Sub BindGrid()
        dgvDict.DataSource = Nothing
        dgvDict.AutoGenerateColumns = False
        dgvDict.Columns.Clear()

        Dim col1 As New DataGridViewTextBoxColumn()
        col1.HeaderText = "编码"
        col1.DataPropertyName = "strFailureModeNo"
        dgvDict.Columns.Add(col1)

        Dim col2 As New DataGridViewTextBoxColumn()
        col2.HeaderText = "名称"
        col2.DataPropertyName = "strFailureModeName"
        dgvDict.Columns.Add(col2)

        Dim col3 As New DataGridViewTextBoxColumn()
        col3.HeaderText = "类别"
        col3.DataPropertyName = "strCategory"
        dgvDict.Columns.Add(col3)

        Dim col4 As New DataGridViewTextBoxColumn()
        col4.HeaderText = "排序"
        col4.DataPropertyName = "lngSortOrder"
        dgvDict.Columns.Add(col4)

        dgvDict.DataSource = dtDict
    End Sub

    ''' <summary>
    ''' 功能：显示当前行到控件
    ''' </summary>
    Private Sub ShowRecord()
        If dtDict Is Nothing OrElse dtDict.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtDict.Rows.Count Then Return

        Dim dr As DataRow = dtDict.Rows(intCurrentRow)

        txtNo.Text = If(IsDBNull(dr("strFailureModeNo")), "", dr("strFailureModeNo").ToString())
        txtName.Text = If(IsDBNull(dr("strFailureModeName")), "", dr("strFailureModeName").ToString())
        cboCategory.Text = If(IsDBNull(dr("strCategory")), "", dr("strCategory").ToString())
        txtSortOrder.Text = If(IsDBNull(dr("lngSortOrder")), "0", dr("lngSortOrder").ToString())
        txtDescription.Text = If(IsDBNull(dr("memDescription")), "", dr("memDescription").ToString())
        txtRemark.Text = If(IsDBNull(dr("memRemark")), "", dr("memRemark").ToString())

        ' 编辑时编码只读
        txtNo.ReadOnly = Not blnIsNew

        ' 高亮表格
        If dgvDict.Rows.Count > intCurrentRow Then
            dgvDict.ClearSelection()
            dgvDict.Rows(intCurrentRow).Selected = True
        End If

        lblStatusBar.Text = "当前第 " & (intCurrentRow + 1) & " 条 / 共 " & dtDict.Rows.Count & " 条"
    End Sub

    ''' <summary>
    ''' 功能：清空控件
    ''' </summary>
    Private Sub ClearControls()
        txtNo.Text = ""
        txtName.Text = ""
        cboCategory.SelectedIndex = -1
        ' 排序号自动取下一个
        txtSortOrder.Text = GetNextSortOrder().ToString()
        txtDescription.Text = ""
        txtRemark.Text = ""
        txtNo.ReadOnly = False
        lblStatusBar.Text = "新增模式"
    End Sub

    ''' <summary>
    ''' 功能：校验输入
    ''' </summary>
    Private Function ValidateInput() As Boolean
        If txtNo.Text.Trim() = "" Then
            MessageBox.Show("编码不能为空")
            txtNo.Focus()
            Return False
        End If
        If txtName.Text.Trim() = "" Then
            MessageBox.Show("名称不能为空")
            txtName.Focus()
            Return False
        End If

        Dim intOrder As Integer = 0
        If Not Integer.TryParse(txtSortOrder.Text.Trim(), intOrder) Then
            MessageBox.Show("排序必须是数字")
            txtSortOrder.Focus()
            Return False
        End If

        If IsNoExists(txtNo.Text.Trim()) Then
            MessageBox.Show("编码已存在：" & txtNo.Text.Trim())
            txtNo.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 功能：检查编码是否已存在（排除自己）
    ''' </summary>
    Private Function IsNoExists(ByVal strNo As String) As Boolean
        Dim strSql As String
        If blnIsNew Then
            strSql = "SELECT COUNT(*) FROM tblFailureModeDict WHERE strFailureModeNo=? AND blnIsDeleted=False"
        Else
            strSql = "SELECT COUNT(*) FROM tblFailureModeDict WHERE strFailureModeNo=? AND blnIsDeleted=False AND lngID<>?"
        End If

        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand(strSql, conn)
                cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strNo
                If Not blnIsNew Then
                    cmd.Parameters.Add("p2", OleDbType.Integer).Value = CLng(dtDict.Rows(intCurrentRow)("lngID"))
                End If
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If IsDBNull(result) Then Return False
                Return CInt(result) > 0
            End Using
        End Using
    End Function

    ''' <summary>
    ''' 功能：取下一个排序号，最大值 + 10
    ''' </summary>
    Private Function GetNextSortOrder() As Integer
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT MAX(lngSortOrder) FROM tblFailureModeDict WHERE blnIsDeleted=False", conn)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return 10
                Return CInt(result) + 10
            End Using
        End Using
    End Function

    ''' <summary>
    ''' 功能：新增按钮，编码自动填下一个
    ''' </summary>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        blnIsNew = True
        ClearControls()
        txtNo.Text = GetNextModeNo()
        txtNo.ReadOnly = False
        txtNo.Focus()
        txtNo.SelectAll()
    End Sub

    ''' <summary>
    ''' 功能：保存按钮
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not ValidateInput() Then Return

            If blnIsNew Then
                InsertRecord()
            Else
                UpdateRecord()
            End If

            Dim strNo As String = txtNo.Text.Trim()
            LoadData()
            intCurrentRow = FindRowByNo(strNo)
            If intCurrentRow < 0 Then intCurrentRow = 0
            If dtDict.Rows.Count > 0 Then ShowRecord()
            blnIsNew = False
            lblStatusBar.Text = "保存成功"
        Catch ex As Exception
            MessageBox.Show("保存失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：插入记录
    ''' </summary>
    Private Sub InsertRecord()
        SyncLock WriteLock
            Using conn As OleDbConnection = GetConnection()
                conn.Open()
                Dim strSql As String = "INSERT INTO tblFailureModeDict " &
                    "(strFailureModeNo, strFailureModeName, strCategory, memDescription, lngSortOrder, blnIsDeleted, dtmCreateTime, memRemark) " &
                    "VALUES (?,?,?,?,?,?,?,?)"
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = txtNo.Text.Trim()
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = txtName.Text.Trim()
                    cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = cboCategory.Text
                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtDescription.Text
                    cmd.Parameters.Add("p5", OleDbType.Integer).Value = CInt(txtSortOrder.Text.Trim())
                    cmd.Parameters.Add("p6", OleDbType.Boolean).Value = False
                    cmd.Parameters.Add("p7", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：更新记录
    ''' </summary>
    Private Sub UpdateRecord()
        If dtDict Is Nothing OrElse dtDict.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtDict.Rows.Count Then Return

        SyncLock WriteLock
            Dim lngID As Long = CLng(dtDict.Rows(intCurrentRow)("lngID"))
            Using conn As OleDbConnection = GetConnection()
                conn.Open()
                Dim strSql As String = "UPDATE tblFailureModeDict SET " &
                    "strFailureModeName=?, strCategory=?, memDescription=?, lngSortOrder=?, memRemark=? " &
                    "WHERE lngID=?"
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = txtName.Text.Trim()
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = cboCategory.Text
                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtDescription.Text
                    cmd.Parameters.Add("p4", OleDbType.Integer).Value = CInt(txtSortOrder.Text.Trim())
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.Parameters.Add("p6", OleDbType.Integer).Value = lngID
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：删除按钮（软删除）
    ''' </summary>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If blnIsNew Then
                MessageBox.Show("新增模式下不能删除")
                Return
            End If
            If dtDict Is Nothing OrElse dtDict.Rows.Count = 0 Then Return
            If MessageBox.Show("确定删除当前记录吗？", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            SyncLock WriteLock
                Dim lngID As Long = CLng(dtDict.Rows(intCurrentRow)("lngID"))
                Using conn As OleDbConnection = GetConnection()
                    conn.Open()
                    Using cmd As New OleDbCommand("UPDATE tblFailureModeDict SET blnIsDeleted=True WHERE lngID=?", conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngID
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End SyncLock

            LoadData()
            intCurrentRow = 0
            If dtDict.Rows.Count > 0 Then
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
    ''' 功能：刷新按钮
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim strNo As String = txtNo.Text.Trim()
        LoadData()
        intCurrentRow = FindRowByNo(strNo)
        If intCurrentRow < 0 Then intCurrentRow = 0
        If dtDict.Rows.Count > 0 Then
            ShowRecord()
        Else
            ClearControls()
            blnIsNew = True
        End If
        lblStatusBar.Text = "已刷新"
    End Sub

    ''' <summary>
    ''' 功能：关闭按钮
    ''' </summary>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvDict_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDict.CellClick
        If e.RowIndex < 0 Then Return

        ' 从 DataView 里取当前行的编码
        Dim drv As DataRowView = dgvDict.Rows(e.RowIndex).DataBoundItem
        If drv Is Nothing Then Return
        Dim strNo As String = drv("strFailureModeNo").ToString()

        ' 在 dtDict 里找对应行号
        intCurrentRow = FindRowByNo(strNo)
        If intCurrentRow < 0 Then Return

        blnIsNew = False
        ShowRecord()
    End Sub

    ''' <summary>
    ''' 功能：按编码查行号
    ''' </summary>
    Private Function FindRowByNo(ByVal strNo As String) As Integer
        For i As Integer = 0 To dtDict.Rows.Count - 1
            If dtDict.Rows(i)("strFailureModeNo").ToString() = strNo Then Return i
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 功能：搜索框内容变化，过滤字典列表
    ''' </summary>
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        FilterGrid()
    End Sub

    ''' <summary>
    ''' 功能：按关键字过滤 DataGridView
    ''' </summary>
    Private Sub FilterGrid()
        If dtDict Is Nothing Then Return

        Dim strKey As String = txtSearch.Text.Trim()

        If strKey = "" Then
            dgvDict.DataSource = dtDict
            Return
        End If

        Dim dv As New DataView(dtDict)
        ' 编码或名称含关键字，双引号转义防注入
        strKey = strKey.Replace("'", "''")
        dv.RowFilter = "strFailureModeNo LIKE '%" & strKey & "%' OR strFailureModeName LIKE '%" & strKey & "%'"
        dgvDict.DataSource = dv
    End Sub


    ''' <summary>
    ''' 功能：复制当前记录内容，编码自动填下一个，其他字段沿用
    ''' </summary>
    Private Sub btnCopyNew_Click(sender As Object, e As EventArgs) Handles btnCopyNew.Click
        If dtDict Is Nothing OrElse dtDict.Rows.Count = 0 Then
            MessageBox.Show("没有可复制的记录")
            Return
        End If

        blnIsNew = True
        txtNo.Text = GetNextModeNo()   ' 自动填下一个编码
        txtNo.ReadOnly = False
        txtSortOrder.Text = GetNextSortOrder().ToString()
        txtNo.Focus()
        txtNo.SelectAll()

        lblStatusBar.Text = "复制新增模式，编码已自动填，可修改"
    End Sub

    ''' <summary>
    ''' 功能：取下一个失效模式编码，FM-xxx 格式
    ''' </summary>
    Private Function GetNextModeNo() As String
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT MAX(strFailureModeNo) FROM tblFailureModeDict WHERE strFailureModeNo LIKE 'FM-%'", conn)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return "FM-001"
                Dim strMax As String = result.ToString()
                Dim intNum As Integer = 0
                Integer.TryParse(strMax.Replace("FM-", ""), intNum)
                Return "FM-" & (intNum + 1).ToString("000")
            End Using
        End Using
    End Function

End Class