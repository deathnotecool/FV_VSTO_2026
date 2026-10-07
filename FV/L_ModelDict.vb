Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms

''' <summary>
''' 功能：型号字典维护窗体
''' </summary>
Public Class L_ModelDict

    Private dtModel As DataTable
    Private intCurrentRow As Integer = 0
    Private blnIsNew As Boolean = False

    ''' <summary>
    ''' 功能：窗体加载
    ''' </summary>
    Private Sub L_ModelDict_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadData()
            If dtModel.Rows.Count > 0 Then
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
    ''' 功能：载入数据
    ''' </summary>
    Private Sub LoadData()
        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter("SELECT * FROM tblModel WHERE blnIsDeleted=False ORDER BY lngSortOrder, lngID", conn)
                dtModel = New DataTable()
                da.Fill(dtModel)
            End Using
        End Using
        BindGrid()
    End Sub

    ''' <summary>
    ''' 功能：绑定表格
    ''' </summary>
    Private Sub BindGrid()
        dgvModel.AutoGenerateColumns = False
        dgvModel.Columns.Clear()

        Dim col1 As New DataGridViewTextBoxColumn()
        col1.Name = "colDrawingNo"
        col1.HeaderText = "图号"
        col1.DataPropertyName = "strDrawingNo"
        col1.Width = 150
        dgvModel.Columns.Add(col1)

        Dim col2 As New DataGridViewTextBoxColumn()
        col2.Name = "colPartNo"
        col2.HeaderText = "客户品号"
        col2.DataPropertyName = "strCustomerPartNo"
        col2.Width = 150
        dgvModel.Columns.Add(col2)

        Dim col3 As New DataGridViewTextBoxColumn()
        col3.Name = "colSort"
        col3.HeaderText = "排序"
        col3.DataPropertyName = "lngSortOrder"
        col3.Width = 60
        dgvModel.Columns.Add(col3)

        dgvModel.DataSource = dtModel
    End Sub

    ''' <summary>
    ''' 功能：显示当前行
    ''' </summary>
    Private Sub ShowRecord()
        If dtModel Is Nothing OrElse dtModel.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtModel.Rows.Count Then Return

        Dim dr As DataRow = dtModel.Rows(intCurrentRow)

        txtDrawingNo.Text = If(IsDBNull(dr("strDrawingNo")), "", dr("strDrawingNo").ToString())
        txtCustomerPartNo.Text = If(IsDBNull(dr("strCustomerPartNo")), "", dr("strCustomerPartNo").ToString())
        txtSortOrder.Text = If(IsDBNull(dr("lngSortOrder")), "0", dr("lngSortOrder").ToString())
        txtRemark.Text = If(IsDBNull(dr("memRemark")), "", dr("memRemark").ToString())

        txtDrawingNo.ReadOnly = Not blnIsNew

        If dgvModel.Rows.Count > intCurrentRow Then
            dgvModel.ClearSelection()
            dgvModel.Rows(intCurrentRow).Selected = True
        End If

        lblStatusBar.Text = "当前第 " & (intCurrentRow + 1) & " 条 / 共 " & dtModel.Rows.Count & " 条"
    End Sub

    ''' <summary>
    ''' 功能：清空控件
    ''' </summary>
    Private Sub ClearControls()
        txtDrawingNo.Text = ""
        txtCustomerPartNo.Text = ""
        txtSortOrder.Text = GetNextSortOrder().ToString()
        txtRemark.Text = ""
        txtDrawingNo.ReadOnly = False
        lblStatusBar.Text = "新增模式"
    End Sub

    ''' <summary>
    ''' 功能：取下一个排序号，最大+10
    ''' </summary>
    Private Function GetNextSortOrder() As Integer
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT MAX(lngSortOrder) FROM tblModel WHERE blnIsDeleted=False", conn)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing OrElse IsDBNull(result) Then Return 10
                Return CInt(result) + 10
            End Using
        End Using
    End Function

    ''' <summary>
    ''' 功能：校验输入
    ''' </summary>
    Private Function ValidateInput() As Boolean
        If txtDrawingNo.Text.Trim() = "" Then
            MessageBox.Show("图号不能为空")
            txtDrawingNo.Focus()
            Return False
        End If
        If txtCustomerPartNo.Text.Trim() = "" Then
            MessageBox.Show("客户品号不能为空")
            txtCustomerPartNo.Focus()
            Return False
        End If

        Dim intOrder As Integer = 0
        If Not Integer.TryParse(txtSortOrder.Text.Trim(), intOrder) Then
            MessageBox.Show("排序必须是数字")
            txtSortOrder.Focus()
            Return False
        End If

        If IsDrawingExists(txtDrawingNo.Text.Trim()) Then
            MessageBox.Show("图号已存在：" & txtDrawingNo.Text.Trim())
            txtDrawingNo.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 功能：检查图号是否已存在（排除自己）
    ''' </summary>
    Private Function IsDrawingExists(ByVal strNo As String) As Boolean
        Dim strSql As String
        If blnIsNew Then
            strSql = "SELECT COUNT(*) FROM tblModel WHERE strDrawingNo=? AND strCustomerPartNo=? AND blnIsDeleted=False"
        Else
            strSql = "SELECT COUNT(*) FROM tblModel WHERE strDrawingNo=? AND strCustomerPartNo=? AND blnIsDeleted=False AND lngID<>?"
        End If

        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand(strSql, conn)
                cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = strNo
                cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = txtCustomerPartNo.Text.Trim()
                If Not blnIsNew Then
                    cmd.Parameters.Add("p3", OleDbType.Integer).Value = CLng(dtModel.Rows(intCurrentRow)("lngID"))
                End If
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If IsDBNull(result) Then Return False
                Return CInt(result) > 0
            End Using
        End Using
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        blnIsNew = True
        ClearControls()
        txtDrawingNo.Focus()
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

            Dim strNo As String = txtDrawingNo.Text.Trim()
            Dim strPart As String = txtCustomerPartNo.Text.Trim()
            LoadData()
            intCurrentRow = FindRowByKey(strNo, strPart)
            If intCurrentRow < 0 Then intCurrentRow = 0
            If dtModel.Rows.Count > 0 Then ShowRecord()
            blnIsNew = False
            lblStatusBar.Text = "保存成功"
            MessageBox.Show("保存成功")
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
                Dim strSql As String = "INSERT INTO tblModel " &
                    "(strDrawingNo, strCustomerPartNo, lngSortOrder, blnIsDeleted, memRemark) " &
                    "VALUES (?,?,?,?,?)"
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = txtDrawingNo.Text.Trim()
                    cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = txtCustomerPartNo.Text.Trim()
                    cmd.Parameters.Add("p3", OleDbType.Integer).Value = CInt(txtSortOrder.Text.Trim())
                    cmd.Parameters.Add("p4", OleDbType.Boolean).Value = False
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：更新记录
    ''' </summary>
    Private Sub UpdateRecord()
        If dtModel Is Nothing OrElse dtModel.Rows.Count = 0 Then Return
        If intCurrentRow < 0 OrElse intCurrentRow >= dtModel.Rows.Count Then Return

        SyncLock WriteLock
            Dim lngID As Long = CLng(dtModel.Rows(intCurrentRow)("lngID"))
            Using conn As OleDbConnection = GetConnection()
                conn.Open()
                Dim strSql As String = "UPDATE tblModel SET " &
                    "strCustomerPartNo=?, lngSortOrder=?, memRemark=? " &
                    "WHERE lngID=?"
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.VarWChar).Value = txtCustomerPartNo.Text.Trim()
                    cmd.Parameters.Add("p2", OleDbType.Integer).Value = CInt(txtSortOrder.Text.Trim())
                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.Parameters.Add("p4", OleDbType.Integer).Value = lngID
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：删除按钮
    ''' </summary>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If blnIsNew Then
                MessageBox.Show("新增模式下不能删除")
                Return
            End If
            If dtModel Is Nothing OrElse dtModel.Rows.Count = 0 Then Return
            If MessageBox.Show("确定删除当前记录吗？", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            SyncLock WriteLock
                Dim lngID As Long = CLng(dtModel.Rows(intCurrentRow)("lngID"))
                Using conn As OleDbConnection = GetConnection()
                    conn.Open()
                    Using cmd As New OleDbCommand("UPDATE tblModel SET blnIsDeleted=True WHERE lngID=?", conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngID
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End SyncLock

            LoadData()
            intCurrentRow = 0
            If dtModel.Rows.Count > 0 Then
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim strNo As String = txtDrawingNo.Text.Trim()
        Dim strPart As String = txtCustomerPartNo.Text.Trim()
        LoadData()
        intCurrentRow = FindRowByKey(strNo, strPart)
        If intCurrentRow < 0 Then intCurrentRow = 0
        If dtModel.Rows.Count > 0 Then
            ShowRecord()
        Else
            ClearControls()
            blnIsNew = True
        End If
        lblStatusBar.Text = "已刷新"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 功能：点表格行，按图号+品号在 dtModel 里定位
    ''' </summary>
    Private Sub dgvModel_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvModel.CellClick
        If e.RowIndex < 0 Then Return

        ' 从 DataView 取当前行数据
        Dim drv As DataRowView = dgvModel.Rows(e.RowIndex).DataBoundItem
        If drv Is Nothing Then Return

        Dim strNo As String = drv("strDrawingNo").ToString()
        Dim strPart As String = drv("strCustomerPartNo").ToString()

        ' 在 dtModel 里找对应行号
        intCurrentRow = FindRowByKey(strNo, strPart)
        If intCurrentRow < 0 Then Return

        blnIsNew = False
        ShowRecord()
    End Sub

    ''' <summary>
    ''' 功能：搜索过滤
    ''' </summary>
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        FilterGrid()
    End Sub

    Private Sub FilterGrid()
        If dtModel Is Nothing Then Return
        Dim strKey As String = txtSearch.Text.Trim()
        If strKey = "" Then
            dgvModel.DataSource = dtModel
            Return
        End If
        Dim dv As New DataView(dtModel)
        strKey = strKey.Replace("'", "''")
        dv.RowFilter = "strDrawingNo LIKE '%" & strKey & "%' OR strCustomerPartNo LIKE '%" & strKey & "%'"
        dgvModel.DataSource = dv
    End Sub

    ''' <summary>
    ''' 功能：按图号+品号查行号
    ''' </summary>
    Private Function FindRowByKey(ByVal strNo As String, ByVal strPart As String) As Integer
        For i As Integer = 0 To dtModel.Rows.Count - 1
            If dtModel.Rows(i)("strDrawingNo").ToString() = strNo AndAlso
               dtModel.Rows(i)("strCustomerPartNo").ToString() = strPart Then Return i
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 功能：复制新增，图号沿用，品号清空
    ''' </summary>
    Private Sub btnCopyNew_Click(sender As Object, e As EventArgs) Handles btnCopyNew.Click
        If dtModel Is Nothing OrElse dtModel.Rows.Count = 0 Then
            MessageBox.Show("没有可复制的记录")
            Return
        End If

        blnIsNew = True
        ' 图号沿用当前值，品号清空
        txtCustomerPartNo.Text = ""
        txtDrawingNo.ReadOnly = False
        txtSortOrder.Text = GetNextSortOrder().ToString()
        txtCustomerPartNo.Focus()

        lblStatusBar.Text = "复制新增模式，图号已保留，请填写品号后保存"
    End Sub
End Class