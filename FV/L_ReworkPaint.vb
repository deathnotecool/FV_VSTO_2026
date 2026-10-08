Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms

''' <summary>
''' 功能：油漆QC返工记录录入窗体
''' </summary>
Public Class L_ReworkPaint

    Private lngHeadID As Long = 0
    Private blnIsNew As Boolean = True

    ''' <summary>
    ''' 功能：窗体加载
    ''' </summary>
    Private Sub L_ReworkPaint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            InitComboBox()
            InitGrid()
            dtpDate.Value = Now
            Dim intHour As Integer = Now.Hour
            cboShift.Text = If(intHour >= 8 AndAlso intHour < 20, "白班", "夜班")
            LoadHeadList()
            If dgvHead.Rows.Count = 0 Then
                NewRecord()
            End If
        Catch ex As Exception
            MessageBox.Show("加载失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：初始化下拉框
    ''' </summary>
    Private Sub InitComboBox()
        cboShift.Items.Clear()
        cboShift.Items.Add("白班")
        cboShift.Items.Add("夜班")
        cboShift.DropDownStyle = ComboBoxStyle.DropDownList

        cboInspector.Items.Clear()
        cboInspector.Items.Add("孙锋")
        cboInspector.DropDownStyle = ComboBoxStyle.DropDown
    End Sub

    ''' <summary>
    ''' 功能：初始化 DataGridView 列
    ''' </summary>
    Private Sub InitGrid()
        dgvDetail.AutoGenerateColumns = False
        dgvDetail.Columns.Clear()

        ' 检查类型
        Dim colCheck As New DataGridViewComboBoxColumn()
        colCheck.Name = "colCheckType"
        colCheck.HeaderText = "检查类型"
        colCheck.Items.AddRange("来料检查", "油漆施工检查", "出库检")
        colCheck.Width = 110
        dgvDetail.Columns.Add(colCheck)

        ' 系列号
        Dim colSerial As New DataGridViewTextBoxColumn()
        colSerial.Name = "colSerialNo"
        colSerial.HeaderText = "系列号"
        colSerial.Width = 100
        dgvDetail.Columns.Add(colSerial)

        ' 图号(品号) 下拉
        Dim colDrawingPart As New DataGridViewComboBoxColumn()
        colDrawingPart.Name = "colDrawingPartNo"
        colDrawingPart.HeaderText = "图号(品号)"
        colDrawingPart.Width = 180
        LoadDrawingPartItems(colDrawingPart)
        dgvDetail.Columns.Add(colDrawingPart)

        ' 隐藏列：图号
        Dim colDrawing As New DataGridViewTextBoxColumn()
        colDrawing.Name = "colDrawingNo"
        colDrawing.Visible = False
        dgvDetail.Columns.Add(colDrawing)

        ' 隐藏列：客户品号
        Dim colPartNo As New DataGridViewTextBoxColumn()
        colPartNo.Name = "colCustomerPartNo"
        colPartNo.Visible = False
        dgvDetail.Columns.Add(colPartNo)

        ' 缺陷类型（下拉）
        Dim colDefect As New DataGridViewComboBoxColumn()
        colDefect.Name = "colDefectType"
        colDefect.HeaderText = "缺陷类型"
        colDefect.Items.AddRange("轻微划伤", "中度划伤", "重度划伤", "加工面台阶纹路", "生锈",
                                 "油封破损", "附着力不足", "外观不良", "颜色色差", "膜厚不合格",
                                 "光泽度不合格", "追溯号不清晰", "软点S标识遗漏")
        colDefect.Width = 130
        dgvDetail.Columns.Add(colDefect)

        ' 处理方式（下拉）
        Dim colHandle As New DataGridViewComboBoxColumn()
        colHandle.Name = "colHandleType"
        colHandle.HeaderText = "处理方式"
        colHandle.Items.AddRange("抛磨放行", "放行", "返工", "报废")
        colHandle.Width = 90
        dgvDetail.Columns.Add(colHandle)

        ' 数量
        Dim colQty As New DataGridViewTextBoxColumn()
        colQty.Name = "colQty"
        colQty.HeaderText = "数量"
        colQty.Width = 60
        dgvDetail.Columns.Add(colQty)

        ' 备注
        Dim colRemark As New DataGridViewTextBoxColumn()
        colRemark.Name = "colRemark"
        colRemark.HeaderText = "备注"
        colRemark.Width = 150
        dgvDetail.Columns.Add(colRemark)
    End Sub

    ''' <summary>
    ''' 功能：加载"图号(品号)"下拉项
    ''' </summary>
    Private Sub LoadDrawingPartItems(ByVal col As DataGridViewComboBoxColumn)
        col.Items.Clear()
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT strDrawingNo, strCustomerPartNo FROM tblModel WHERE blnIsDeleted=False ORDER BY lngSortOrder", conn)
                conn.Open()
                Using rd As OleDbDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        col.Items.Add(rd("strDrawingNo").ToString() & "(" & rd("strCustomerPartNo").ToString() & ")")
                    End While
                End Using
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' 功能：新建记录
    ''' </summary>
    Private Sub NewRecord()
        blnIsNew = True
        lngHeadID = 0
        dtpDate.Value = Now
        Dim intHour As Integer = Now.Hour
        cboShift.Text = If(intHour >= 8 AndAlso intHour < 20, "白班", "夜班")
        cboInspector.Text = ""
        txtTotalQty.Text = "0"
        txtHeadRemark.Text = ""
        dgvDetail.Rows.Clear()
        lblStatusBar.Text = "新增模式"
    End Sub

    ''' <summary>
    ''' 功能：保存按钮
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If cboShift.Text.Trim() = "" Then
                MessageBox.Show("请选择班次")
                Return
            End If
            If cboInspector.Text.Trim() = "" Then
                MessageBox.Show("请选择或输入检验员")
                Return
            End If
            Dim intTotal As Integer = 0
            If Not Integer.TryParse(txtTotalQty.Text.Trim(), intTotal) Then
                MessageBox.Show("检验总数必须是数字")
                Return
            End If
            If dgvDetail.Rows.Count = 0 Then
                MessageBox.Show("请至少录入一条明细")
                Return
            End If

            SyncLock WriteLock
                Using conn As OleDbConnection = GetConnection()
                    conn.Open()

                    If blnIsNew Then
                        Dim strSqlHead As String = "INSERT INTO tblReworkPaintHead " &
                            "(dtmDate, strShift, strInspector, lngTotalQty, dtmCreateTime, dtmUpdateTime, blnIsDeleted, memRemark) " &
                            "VALUES (?,?,?,?,?,?,False,?)"
                        Using cmd As New OleDbCommand(strSqlHead, conn)
                            cmd.Parameters.Add("p1", OleDbType.Date).Value = dtpDate.Value.Date
                            cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = cboShift.Text
                            cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = cboInspector.Text.Trim()
                            cmd.Parameters.Add("p4", OleDbType.Integer).Value = intTotal
                            cmd.Parameters.Add("p5", OleDbType.Date).Value = Now
                            cmd.Parameters.Add("p6", OleDbType.Date).Value = Now
                            cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtHeadRemark.Text
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmd As New OleDbCommand("SELECT MAX(lngID) FROM tblReworkPaintHead", conn)
                            lngHeadID = CLng(cmd.ExecuteScalar())
                        End Using
                    Else
                        Dim strSqlHead As String = "UPDATE tblReworkPaintHead SET " &
                            "dtmDate=?, strShift=?, strInspector=?, lngTotalQty=?, dtmUpdateTime=?, memRemark=? " &
                            "WHERE lngID=?"
                        Using cmd As New OleDbCommand(strSqlHead, conn)
                            cmd.Parameters.Add("p1", OleDbType.Date).Value = dtpDate.Value.Date
                            cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = cboShift.Text
                            cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = cboInspector.Text.Trim()
                            cmd.Parameters.Add("p4", OleDbType.Integer).Value = intTotal
                            cmd.Parameters.Add("p5", OleDbType.Date).Value = Now
                            cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtHeadRemark.Text
                            cmd.Parameters.Add("p7", OleDbType.Integer).Value = lngHeadID
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmd As New OleDbCommand("DELETE FROM tblReworkPaintDetail WHERE lngHeadID=?", conn)
                            cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngHeadID
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    For i As Integer = 0 To dgvDetail.Rows.Count - 1
                        Dim row As DataGridViewRow = dgvDetail.Rows(i)
                        If row.IsNewRow Then Continue For

                        Dim strCheck As String = GetCell(row, "colCheckType")
                        Dim strSerial As String = GetCell(row, "colSerialNo")
                        Dim strDrawingNo As String = GetCell(row, "colDrawingNo")
                        Dim strCustomerPartNo As String = GetCell(row, "colCustomerPartNo")
                        Dim strDefect As String = GetCell(row, "colDefectType")
                        Dim strHandle As String = GetCell(row, "colHandleType")
                        Dim strQty As String = GetCell(row, "colQty")
                        Dim strRemark As String = GetCell(row, "colRemark")

                        If strCheck = "" AndAlso strDefect = "" Then Continue For

                        Dim intQty As Integer = 0
                        Integer.TryParse(strQty, intQty)

                        Dim strSqlDetail As String = "INSERT INTO tblReworkPaintDetail " &
                            "(lngHeadID, strCheckType, strSerialNo, strDrawingNo, strCustomerPartNo, strDefectType, strHandleType, lngQty, dtmCreateTime, memRemark) " &
                            "VALUES (?,?,?,?,?,?,?,?,?,?)"
                        Using cmd As New OleDbCommand(strSqlDetail, conn)
                            cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngHeadID
                            cmd.Parameters.Add("p2", OleDbType.VarWChar).Value = strCheck
                            cmd.Parameters.Add("p3", OleDbType.VarWChar).Value = strSerial
                            cmd.Parameters.Add("p4", OleDbType.VarWChar).Value = strDrawingNo
                            cmd.Parameters.Add("p5", OleDbType.VarWChar).Value = strCustomerPartNo
                            cmd.Parameters.Add("p6", OleDbType.VarWChar).Value = strDefect
                            cmd.Parameters.Add("p7", OleDbType.VarWChar).Value = strHandle
                            cmd.Parameters.Add("p8", OleDbType.Integer).Value = intQty
                            cmd.Parameters.Add("p9", OleDbType.Date).Value = Now
                            cmd.Parameters.Add("p10", OleDbType.LongVarWChar).Value = strRemark
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                End Using
            End SyncLock

            blnIsNew = False
            Dim lngKeepID As Long = lngHeadID
            LoadHeadListNoAutoSelect()
            SelectHeadByID(lngKeepID)
            lblStatusBar.Text = "保存成功，主表ID=" & lngHeadID
            MessageBox.Show("保存成功")
        Catch ex As Exception
            MessageBox.Show("保存失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：取单元格字符串
    ''' </summary>
    Private Function GetCell(ByVal row As DataGridViewRow, ByVal strColName As String) As String
        If row.Cells(strColName).Value Is Nothing Then Return ""
        Return row.Cells(strColName).Value.ToString().Trim()
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
    End Sub

    Private Sub btnAddRow_Click(sender As Object, e As EventArgs) Handles btnAddRow.Click
        dgvDetail.Rows.Add()
    End Sub

    Private Sub btnDelRow_Click(sender As Object, e As EventArgs) Handles btnDelRow.Click
        If dgvDetail.CurrentRow IsNot Nothing AndAlso Not dgvDetail.CurrentRow.IsNewRow Then
            dgvDetail.Rows.Remove(dgvDetail.CurrentRow)
        End If
    End Sub

    ''' <summary>
    ''' 功能：删除按钮
    ''' </summary>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If blnIsNew OrElse lngHeadID = 0 Then
                MessageBox.Show("新增模式下不能删除")
                Return
            End If
            If MessageBox.Show("确定删除当前记录吗？将同时删除明细，不可恢复。", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return

            SyncLock WriteLock
                Using conn As OleDbConnection = GetConnection()
                    conn.Open()
                    Using cmd As New OleDbCommand("DELETE FROM tblReworkPaintDetail WHERE lngHeadID=?", conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngHeadID
                        cmd.ExecuteNonQuery()
                    End Using
                    Using cmd As New OleDbCommand("DELETE FROM tblReworkPaintHead WHERE lngID=?", conn)
                        cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngHeadID
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End SyncLock

            NewRecord()
            LoadHeadList()
            lblStatusBar.Text = "已删除"
        Catch ex As Exception
            MessageBox.Show("删除失败：" & ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadHeadList()
        NewRecord()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub L_ReworkPaint_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Globals.Ribbons.Ribbon1.btnOpenReworkPaint.Enabled = True
    End Sub

    ''' <summary>
    ''' 功能：载入历史表头列表，自动选最新
    ''' </summary>
    Private Sub LoadHeadList()
        Dim dt As New DataTable()
        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter("SELECT * FROM tblReworkPaintHead WHERE blnIsDeleted=False ORDER BY dtmDate DESC, lngID DESC", conn)
                da.Fill(dt)
            End Using
        End Using

        BindHeadGrid(dt)

        If dt.Rows.Count > 0 Then
            dgvHead.ClearSelection()
            dgvHead.Rows(0).Selected = True
            LoadHeadRecord(0)
        End If
    End Sub

    ''' <summary>
    ''' 功能：载入历史列表，不自动选
    ''' </summary>
    Private Sub LoadHeadListNoAutoSelect()
        Dim dt As New DataTable()
        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter("SELECT * FROM tblReworkPaintHead WHERE blnIsDeleted=False ORDER BY dtmDate DESC, lngID DESC", conn)
                da.Fill(dt)
            End Using
        End Using
        BindHeadGrid(dt)
    End Sub

    ''' <summary>
    ''' 功能：绑定 Head 列表
    ''' </summary>
    Private Sub BindHeadGrid(ByVal dt As DataTable)
        dgvHead.AutoGenerateColumns = False
        dgvHead.Columns.Clear()

        Dim col1 As New DataGridViewTextBoxColumn()
        col1.Name = "colDate"
        col1.HeaderText = "日期"
        col1.DataPropertyName = "dtmDate"
        dgvHead.Columns.Add(col1)

        Dim col2 As New DataGridViewTextBoxColumn()
        col2.Name = "colShift"
        col2.HeaderText = "班次"
        col2.DataPropertyName = "strShift"
        dgvHead.Columns.Add(col2)

        Dim col3 As New DataGridViewTextBoxColumn()
        col3.Name = "colInspector"
        col3.HeaderText = "检验员"
        col3.DataPropertyName = "strInspector"
        dgvHead.Columns.Add(col3)

        Dim col4 As New DataGridViewTextBoxColumn()
        col4.Name = "colTotal"
        col4.HeaderText = "检验总数"
        col4.DataPropertyName = "lngTotalQty"
        dgvHead.Columns.Add(col4)

        dgvHead.DataSource = dt
    End Sub

    Private Sub dgvHead_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHead.CellClick
        If e.RowIndex < 0 Then Return
        LoadHeadRecord(e.RowIndex)
    End Sub

    ''' <summary>
    ''' 功能：加载指定行号的表头到控件和明细
    ''' </summary>
    Private Sub LoadHeadRecord(ByVal intRowIdx As Integer)
        If intRowIdx < 0 OrElse intRowIdx >= dgvHead.Rows.Count Then Return
        Dim drv As DataRowView = dgvHead.Rows(intRowIdx).DataBoundItem
        If drv Is Nothing Then Return

        lngHeadID = CLng(drv("lngID"))
        blnIsNew = False

        dtpDate.Value = CDate(drv("dtmDate"))
        cboShift.Text = drv("strShift").ToString()
        cboInspector.Text = drv("strInspector").ToString()
        txtTotalQty.Text = drv("lngTotalQty").ToString()
        txtHeadRemark.Text = If(IsDBNull(drv("memRemark")), "", drv("memRemark").ToString())

        LoadDetail(lngHeadID)
        lblStatusBar.Text = "已加载记录 ID=" & lngHeadID
    End Sub

    ''' <summary>
    ''' 功能：按 ID 选中 dgvHead 行
    ''' </summary>
    Private Sub SelectHeadByID(ByVal lngID As Long)
        For i As Integer = 0 To dgvHead.Rows.Count - 1
            Dim drv As DataRowView = dgvHead.Rows(i).DataBoundItem
            If drv IsNot Nothing AndAlso CLng(drv("lngID")) = lngID Then
                dgvHead.ClearSelection()
                dgvHead.Rows(i).Selected = True
                LoadHeadRecord(i)
                Return
            End If
        Next
    End Sub

    ''' <summary>
    ''' 功能：载入明细
    ''' </summary>
    Private Sub LoadDetail(ByVal lngHead As Long)
        dgvDetail.Rows.Clear()
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT * FROM tblReworkPaintDetail WHERE lngHeadID=? ORDER BY lngID", conn)
                cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngHead
                conn.Open()
                Using rd As OleDbDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        Dim intIdx As Integer = dgvDetail.Rows.Add()
                        Dim strDrawing As String = If(IsDBNull(rd("strDrawingNo")), "", rd("strDrawingNo").ToString())
                        Dim strPartNo As String = If(IsDBNull(rd("strCustomerPartNo")), "", rd("strCustomerPartNo").ToString())
                        dgvDetail.Rows(intIdx).Cells("colCheckType").Value = If(IsDBNull(rd("strCheckType")), "", rd("strCheckType").ToString())
                        dgvDetail.Rows(intIdx).Cells("colSerialNo").Value = If(IsDBNull(rd("strSerialNo")), "", rd("strSerialNo").ToString())
                        dgvDetail.Rows(intIdx).Cells("colDrawingPartNo").Value = strDrawing & "(" & strPartNo & ")"
                        dgvDetail.Rows(intIdx).Cells("colDrawingNo").Value = strDrawing
                        dgvDetail.Rows(intIdx).Cells("colCustomerPartNo").Value = strPartNo
                        dgvDetail.Rows(intIdx).Cells("colDefectType").Value = If(IsDBNull(rd("strDefectType")), "", rd("strDefectType").ToString())
                        dgvDetail.Rows(intIdx).Cells("colHandleType").Value = If(IsDBNull(rd("strHandleType")), "", rd("strHandleType").ToString())
                        dgvDetail.Rows(intIdx).Cells("colQty").Value = If(IsDBNull(rd("lngQty")), "0", rd("lngQty").ToString())
                        dgvDetail.Rows(intIdx).Cells("colRemark").Value = If(IsDBNull(rd("memRemark")), "", rd("memRemark").ToString())
                    End While
                End Using
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' 功能：图号(品号)变更时拆存
    ''' </summary>
    Private Sub dgvDetail_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellValueChanged
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        If dgvDetail.Columns(e.ColumnIndex).Name = "colDrawingPartNo" Then
            Dim strVal As String = ""
            If dgvDetail.Rows(e.RowIndex).Cells("colDrawingPartNo").Value IsNot Nothing Then
                strVal = dgvDetail.Rows(e.RowIndex).Cells("colDrawingPartNo").Value.ToString()
            End If

            Dim strDrawing As String = ""
            Dim strPartNo As String = ""
            If strVal.Contains("(") AndAlso strVal.Contains(")") Then
                Dim intPos As Integer = strVal.IndexOf("("c)
                strDrawing = strVal.Substring(0, intPos).Trim()
                strPartNo = strVal.Substring(intPos + 1).Replace(")", "").Trim()
            End If

            dgvDetail.Rows(e.RowIndex).Cells("colDrawingNo").Value = strDrawing
            dgvDetail.Rows(e.RowIndex).Cells("colCustomerPartNo").Value = strPartNo
        End If
    End Sub

    ''' <summary>
    ''' 功能：吃掉 DataGridView 下拉值无效报错
    ''' </summary>
    Private Sub dgvDetail_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvDetail.DataError
        e.ThrowException = False
    End Sub

End Class