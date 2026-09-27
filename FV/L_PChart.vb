Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel

''' <summary>
''' 功能：FMEA P 图六因素维护窗体
''' </summary>
Public Class L_PChart

    ''' <summary>
    ''' 功能：当前主表 ID，由主窗体传入
    ''' </summary>
    Public Property MainID As Long = 0

    ''' <summary>
    ''' 功能：存放从数据库读出的 P 图数据
    ''' </summary>
    Private dtPChart As DataTable

    ''' <summary>
    ''' 功能：是否已有记录
    ''' </summary>
    Private blnExists As Boolean = False

    ''' <summary>
    ''' 功能：窗体加载，显示工序信息、载入 P 图
    ''' </summary>
    Private Sub L_PChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ShowMainInfo()
            LoadData()
        Catch ex As Exception
            MessageBox.Show("加载失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：显示顶部工序信息
    ''' </summary>
    Private Sub ShowMainInfo()
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
    End Sub

    ''' <summary>
    ''' 功能：从 tblPChart 载入当前工序的 P 图
    ''' </summary>
    Private Sub LoadData()
        Using conn As OleDbConnection = GetConnection()
            Using da As New OleDbDataAdapter("SELECT * FROM tblPChart WHERE lngMainID=?", conn)
                da.SelectCommand.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                dtPChart = New DataTable()
                da.Fill(dtPChart)
            End Using
        End Using

        If dtPChart.Rows.Count > 0 Then
            blnExists = True
            ShowRecord()
        Else
            blnExists = False
            ClearControls()
        End If
    End Sub

    ''' <summary>
    ''' 功能：把当前 P 图数据显示到控件
    ''' </summary>
    Private Sub ShowRecord()
        If dtPChart Is Nothing OrElse dtPChart.Rows.Count = 0 Then Return
        Dim dr As DataRow = dtPChart.Rows(0)

        txtInfoInput.Text = If(IsDBNull(dr("memInfoInput")), "", dr("memInfoInput").ToString())
        txtControlFactor.Text = If(IsDBNull(dr("memControlFactor")), "", dr("memControlFactor").ToString())
        txtSystemFunction.Text = If(IsDBNull(dr("memSystemFunction")), "", dr("memSystemFunction").ToString())
        txtExpectedOutput.Text = If(IsDBNull(dr("memExpectedOutput")), "", dr("memExpectedOutput").ToString())
        txtUnexpectedOutput.Text = If(IsDBNull(dr("memUnexpectedOutput")), "", dr("memUnexpectedOutput").ToString())
        txtNoiseFactor.Text = If(IsDBNull(dr("memNoiseFactor")), "", dr("memNoiseFactor").ToString())
        txtRemark.Text = If(IsDBNull(dr("memRemark")), "", dr("memRemark").ToString())

        lblStatusBar.Text = "已加载 P 图"
    End Sub

    ''' <summary>
    ''' 功能：清空控件
    ''' </summary>
    Private Sub ClearControls()
        txtInfoInput.Text = ""
        txtControlFactor.Text = ""
        txtSystemFunction.Text = ""
        txtExpectedOutput.Text = ""
        txtUnexpectedOutput.Text = ""
        txtNoiseFactor.Text = ""
        txtRemark.Text = ""
        lblStatusBar.Text = "无 P 图，可手工录入或从 Excel 导入"
    End Sub

    ''' <summary>
    ''' 功能：保存按钮，INSERT 或 UPDATE
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If MainID = 0 Then
                MessageBox.Show("请先选择工序")
                Return
            End If

            SyncLock WriteLock
                Using conn As OleDbConnection = GetConnection()
                    conn.Open()

                    If blnExists Then
                        Dim strSql As String = "UPDATE tblPChart SET " &
                            "memInfoInput=?, memControlFactor=?, memSystemFunction=?, " &
                            "memExpectedOutput=?, memUnexpectedOutput=?, memNoiseFactor=?, " &
                            "memRemark=?, dtmUpdateTime=? WHERE lngMainID=?"
                        Using cmd As New OleDbCommand(strSql, conn)
                            cmd.Parameters.Add("p1", OleDbType.LongVarWChar).Value = txtInfoInput.Text
                            cmd.Parameters.Add("p2", OleDbType.LongVarWChar).Value = txtControlFactor.Text
                            cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtSystemFunction.Text
                            cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtExpectedOutput.Text
                            cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtUnexpectedOutput.Text
                            cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtNoiseFactor.Text
                            cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtRemark.Text
                            cmd.Parameters.Add("p8", OleDbType.Date).Value = Now
                            cmd.Parameters.Add("p9", OleDbType.Integer).Value = MainID
                            cmd.ExecuteNonQuery()
                        End Using
                    Else
                        Dim strSql As String = "INSERT INTO tblPChart " &
                            "(lngMainID, memInfoInput, memControlFactor, memSystemFunction, " &
                            "memExpectedOutput, memUnexpectedOutput, memNoiseFactor, memRemark, " &
                            "dtmCreateTime, dtmUpdateTime) VALUES (?,?,?,?,?,?,?,?,?,?)"
                        Using cmd As New OleDbCommand(strSql, conn)
                            cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                            cmd.Parameters.Add("p2", OleDbType.LongVarWChar).Value = txtInfoInput.Text
                            cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtControlFactor.Text
                            cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtSystemFunction.Text
                            cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtExpectedOutput.Text
                            cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtUnexpectedOutput.Text
                            cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtNoiseFactor.Text
                            cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = txtRemark.Text
                            cmd.Parameters.Add("p9", OleDbType.Date).Value = Now
                            cmd.Parameters.Add("p10", OleDbType.Date).Value = Now
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End SyncLock

            LoadData()
            lblStatusBar.Text = "保存成功"
        Catch ex As Exception
            MessageBox.Show("保存失败：" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：关闭按钮
    ''' </summary>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 功能：安全读取单元格文本，空返回空串
    ''' </summary>
    Private Function GetCellText(ByVal ws As Excel.Worksheet, ByVal strAddr As String) As String
        Dim rng As Excel.Range = ws.Range(strAddr)
        If rng.Value Is Nothing Then Return ""
        Return rng.Value.ToString().Trim()
    End Function

    ''' <summary>
    ''' 功能：从 Excel 当前工作表按固定单元格导入 P 图
    ''' </summary>
    Private Sub ImportPChartFromExcel()
        If MainID = 0 Then
            MessageBox.Show("请先选择工序")
            Return
        End If

        If blnExists Then
            If MessageBox.Show("当前工序已有 P 图，是否覆盖？", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
                Return
            End If
        End If

        Dim ws As Excel.Worksheet = xlapp.ActiveSheet
        Dim strInfoInput As String = GetCellText(ws, "A2")
        Dim strControlFactor As String = GetCellText(ws, "B2")
        Dim strSystemFunction As String = GetCellText(ws, "C2")
        Dim strExpectedOutput As String = GetCellText(ws, "F2")
        Dim strUnexpectedOutput As String = GetCellText(ws, "G2")
        Dim strNoiseFactor As String = GetCellText(ws, "A6")
        Dim strRemark As String = GetCellText(ws, "A10")

        If strInfoInput = "" AndAlso strControlFactor = "" AndAlso strSystemFunction = "" AndAlso
           strExpectedOutput = "" AndAlso strUnexpectedOutput = "" AndAlso strNoiseFactor = "" Then
            MessageBox.Show("当前工作表未检测到 P 图内容，请检查单元格 A2/B2/C2/F2/G2/A6")
            Return
        End If

        SyncLock WriteLock
            Using conn As OleDbConnection = GetConnection()
                conn.Open()
                Using cmdDel As New OleDbCommand("DELETE FROM tblPChart WHERE lngMainID=?", conn)
                    cmdDel.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                    cmdDel.ExecuteNonQuery()
                End Using
                Dim strSql As String = "INSERT INTO tblPChart " &
                    "(lngMainID, memInfoInput, memControlFactor, memSystemFunction, " &
                    "memExpectedOutput, memUnexpectedOutput, memNoiseFactor, memRemark, " &
                    "dtmCreateTime, dtmUpdateTime) VALUES (?,?,?,?,?,?,?,?,?,?)"
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                    cmd.Parameters.Add("p2", OleDbType.LongVarWChar).Value = strInfoInput
                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = strControlFactor
                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = strSystemFunction
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = strExpectedOutput
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = strUnexpectedOutput
                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = strNoiseFactor
                    cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = strRemark
                    cmd.Parameters.Add("p9", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p10", OleDbType.Date).Value = Now
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock

        LoadData()
        lblStatusBar.Text = "P 图导入成功"
    End Sub

    ''' <summary>
    ''' 功能：从 Excel 导入 P 图按钮
    ''' </summary>
    Private Sub btnImportPChart_Click(sender As Object, e As EventArgs) Handles btnImportPChart.Click
        Try
            ImportPChartFromExcel()
        Catch ex As Exception
            MessageBox.Show("导入失败：" & ex.Message)
        End Try
    End Sub

End Class

'Imports System.Data.OleDb
'Imports System.Data
'Imports System.Windows.Forms

'''' <summary>
'''' 功能：FMEA P图六因素维护窗体（主表 1-1）
'''' </summary>
'Public Class L_PChart

'    ' ========== 窗体级变量 ==========

'    ''' <summary>
'    ''' 功能：当前主表 ID，由主窗体传入
'    ''' </summary>
'    Public Property MainID As Long = 0

'    ''' <summary>
'    ''' 功能：是否已存在 P 图记录。True=已存在走 UPDATE，False=新增走 INSERT
'    ''' </summary>
'    Private blnExists As Boolean = False

'    ''' <summary>
'    ''' 功能：已存在记录的 lngID，UPDATE 时用
'    ''' </summary>
'    Private lngPChartID As Long = 0

'    ' ========== 窗体加载 ==========

'    ''' <summary>
'    ''' 功能：窗体加载，显示工序信息，载入 P 图
'    ''' </summary>
'    Private Sub L_PChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        Try
'            ShowMainInfo()
'            LoadData()
'        Catch ex As Exception
'            MessageBox.Show("加载失败：" & ex.Message & vbCrLf & ex.StackTrace)
'        End Try
'    End Sub

'    ''' <summary>
'    ''' 功能：显示顶部当前工序信息（只读）
'    ''' </summary>
'    Private Sub ShowMainInfo()
'        Using conn As OleDbConnection = GetConnection()
'            Using cmd As New OleDbCommand("SELECT strProcessNo, strProcessName FROM tblFMEA_Main WHERE lngID=?", conn)
'                cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
'                conn.Open()
'                Using rd As OleDbDataReader = cmd.ExecuteReader()
'                    If rd.Read() Then
'                        txtMainNo.Text = rd("strProcessNo").ToString()
'                        txtMainName.Text = rd("strProcessName").ToString()
'                    End If
'                End Using
'            End Using
'        End Using
'    End Sub

'    ''' <summary>
'    ''' 功能：按 MainID 读 P 图记录，有则显示，无则清空
'    ''' </summary>
'    Private Sub LoadData()
'        Using conn As OleDbConnection = GetConnection()
'            Using cmd As New OleDbCommand("SELECT * FROM tblPChart WHERE lngMainID=?", conn)
'                cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
'                conn.Open()
'                Using rd As OleDbDataReader = cmd.ExecuteReader()
'                    If rd.Read() Then
'                        blnExists = True
'                        lngPChartID = CLng(rd("lngID"))
'                        txtInfoInput.Text = If(IsDBNull(rd("memInfoInput")), "", rd("memInfoInput").ToString())
'                        txtControlFactor.Text = If(IsDBNull(rd("memControlFactor")), "", rd("memControlFactor").ToString())
'                        txtSystemFunction.Text = If(IsDBNull(rd("memSystemFunction")), "", rd("memSystemFunction").ToString())
'                        txtExpectedOutput.Text = If(IsDBNull(rd("memExpectedOutput")), "", rd("memExpectedOutput").ToString())
'                        txtUnexpectedOutput.Text = If(IsDBNull(rd("memUnexpectedOutput")), "", rd("memUnexpectedOutput").ToString())
'                        txtNoiseFactor.Text = If(IsDBNull(rd("memNoiseFactor")), "", rd("memNoiseFactor").ToString())
'                        txtRemark.Text = If(IsDBNull(rd("memRemark")), "", rd("memRemark").ToString())
'                        lblStatusBar.Text = "已加载 P 图，可修改后保存"
'                    Else
'                        blnExists = False
'                        lngPChartID = 0
'                        ClearControls()
'                        lblStatusBar.Text = "暂无 P 图，填写后保存"
'                    End If
'                End Using
'            End Using
'        End Using
'    End Sub

'    ''' <summary>
'    ''' 功能：清空所有录入控件
'    ''' </summary>
'    Private Sub ClearControls()
'        txtInfoInput.Text = ""
'        txtControlFactor.Text = ""
'        txtSystemFunction.Text = ""
'        txtExpectedOutput.Text = ""
'        txtUnexpectedOutput.Text = ""
'        txtNoiseFactor.Text = ""
'        txtRemark.Text = ""
'    End Sub

'    ' ========== 按钮事件 ==========

'    ''' <summary>
'    ''' 功能：保存按钮，按是否存在走 INSERT 或 UPDATE
'    ''' </summary>
'    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
'        Try
'            If blnExists Then
'                UpdateRecord()
'            Else
'                InsertRecord()
'            End If

'            LoadData()
'            lblStatusBar.Text = "保存成功"
'        Catch ex As Exception
'            MessageBox.Show("保存失败：" & ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
'        End Try
'    End Sub

'    ''' <summary>
'    ''' 功能：关闭按钮
'    ''' </summary>
'    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
'        Me.Close()
'    End Sub

'    ' ========== 数据库操作 ==========

'    ''' <summary>
'    ''' 功能：插入一条新 P 图记录
'    ''' </summary>
'    Private Sub InsertRecord()
'        SyncLock WriteLock
'            Dim strSql As String = "INSERT INTO tblPChart " &
'                "(lngMainID, memInfoInput, memControlFactor, memSystemFunction, " &
'                "memExpectedOutput, memUnexpectedOutput, memNoiseFactor, memRemark, " &
'                "dtmCreateTime, dtmUpdateTime) " &
'                "VALUES (?,?,?,?,?,?,?,?,?,?)"

'            Using conn As OleDbConnection = GetConnection()
'                Using cmd As New OleDbCommand(strSql, conn)
'                    cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
'                    cmd.Parameters.Add("p2", OleDbType.LongVarWChar).Value = txtInfoInput.Text
'                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtControlFactor.Text
'                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtSystemFunction.Text
'                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtExpectedOutput.Text
'                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtUnexpectedOutput.Text
'                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtNoiseFactor.Text
'                    cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = txtRemark.Text
'                    cmd.Parameters.Add("p9", OleDbType.Date).Value = Now
'                    cmd.Parameters.Add("p10", OleDbType.Date).Value = Now
'                    conn.Open()
'                    cmd.ExecuteNonQuery()
'                End Using
'            End Using
'        End SyncLock
'    End Sub

'    ''' <summary>
'    ''' 功能：更新已存在的 P 图记录
'    ''' </summary>
'    Private Sub UpdateRecord()
'        SyncLock WriteLock
'            Dim strSql As String = "UPDATE tblPChart SET " &
'                "memInfoInput=?, memControlFactor=?, memSystemFunction=?, " &
'                "memExpectedOutput=?, memUnexpectedOutput=?, memNoiseFactor=?, " &
'                "memRemark=?, dtmUpdateTime=? " &
'                "WHERE lngID=?"

'            Using conn As OleDbConnection = GetConnection()
'                Using cmd As New OleDbCommand(strSql, conn)
'                    cmd.Parameters.Add("p1", OleDbType.LongVarWChar).Value = txtInfoInput.Text
'                    cmd.Parameters.Add("p2", OleDbType.LongVarWChar).Value = txtControlFactor.Text
'                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtSystemFunction.Text
'                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtExpectedOutput.Text
'                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtUnexpectedOutput.Text
'                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtNoiseFactor.Text
'                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtRemark.Text
'                    cmd.Parameters.Add("p8", OleDbType.Date).Value = Now
'                    cmd.Parameters.Add("p9", OleDbType.Integer).Value = lngPChartID
'                    conn.Open()
'                    cmd.ExecuteNonQuery()
'                End Using
'            End Using
'        End SyncLock
'    End Sub

'    ''' <summary>
'    ''' 功能：安全读取单元格文本，空返回空串
'    ''' </summary>
'    Private Function GetCellText(ByVal ws As Excel.Worksheet, ByVal strAddr As String) As String
'        Dim rng As Excel.Range = ws.Range(strAddr)
'        If rng.Value Is Nothing Then Return ""
'        Return rng.Value.ToString().Trim()
'    End Function

'    ''' <summary>
'    ''' 功能：检查当前工序是否已有 P 图
'    ''' </summary>
'    Private Function PChartExists(ByVal lngMainID As Long) As Boolean
'        Using conn As OleDbConnection = GetConnection()
'            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM tblPChart WHERE lngMainID=?", conn)
'                cmd.Parameters.Add("p1", OleDbType.Integer).Value = lngMainID
'                conn.Open()
'                Dim result As Object = cmd.ExecuteScalar()
'                If IsDBNull(result) Then Return False
'                Return CInt(result) > 0
'            End Using
'        End Using
'    End Function

'    ''' <summary>
'    ''' 功能：从 Excel 当前工作表按固定单元格导入 P 图
'    ''' </summary>
'    Private Sub ImportPChartFromExcel()
'        ' 1. 检查当前工序
'        If MainID = 0 Then
'            MessageBox.Show("请先选择工序")
'            Return
'        End If

'        ' 2. 判断是否已有 P 图，有则提示覆盖
'        If PChartExists(MainID) Then
'            If MessageBox.Show("当前工序已有 P 图，是否覆盖？", "确认", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
'                Return
'            End If
'        End If

'        ' 3. 读单元格
'        Dim ws As Excel.Worksheet = xlapp.ActiveSheet
'        Dim strInfoInput As String = GetCellText(ws, "A2")
'        Dim strControlFactor As String = GetCellText(ws, "B2")
'        Dim strSystemFunction As String = GetCellText(ws, "C2")
'        Dim strExpectedOutput As String = GetCellText(ws, "F2")
'        Dim strUnexpectedOutput As String = GetCellText(ws, "G2")
'        Dim strNoiseFactor As String = GetCellText(ws, "A6")
'        Dim strRemark As String = GetCellText(ws, "A10")

'        ' 4. 校验：至少一个因素不为空
'        If strInfoInput = "" AndAlso strControlFactor = "" AndAlso strSystemFunction = "" AndAlso
'           strExpectedOutput = "" AndAlso strUnexpectedOutput = "" AndAlso strNoiseFactor = "" Then
'            MessageBox.Show("当前工作表未检测到 P 图内容，请检查单元格 A2/B2/C2/F2/G2/A6")
'            Return
'        End If

'        ' 5. 写库：先删后插
'        SyncLock WriteLock
'            Using conn As OleDbConnection = GetConnection()
'                conn.Open()

'                Using cmdDel As New OleDbCommand("DELETE FROM tblPChart WHERE lngMainID=?", conn)
'                    cmdDel.Parameters.Add("p1", OleDbType.Integer).Value = MainID
'                    cmdDel.ExecuteNonQuery()
'                End Using

'                Dim strSql As String = "INSERT INTO tblPChart " &
'                    "(lngMainID, memInfoInput, memControlFactor, memSystemFunction, " &
'                    "memExpectedOutput, memUnexpectedOutput, memNoiseFactor, memRemark, " &
'                    "dtmCreateTime, dtmUpdateTime) " &
'                    "VALUES (?,?,?,?,?,?,?,?,?,?)"

'                Using cmd As New OleDbCommand(strSql, conn)
'                    cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
'                    cmd.Parameters.Add("p2", OleDbType.LongVarWChar).Value = strInfoInput
'                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = strControlFactor
'                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = strSystemFunction
'                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = strExpectedOutput
'                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = strUnexpectedOutput
'                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = strNoiseFactor
'                    cmd.Parameters.Add("p8", OleDbType.LongVarWChar).Value = strRemark
'                    cmd.Parameters.Add("p9", OleDbType.Date).Value = Now
'                    cmd.Parameters.Add("p10", OleDbType.Date).Value = Now
'                    cmd.ExecuteNonQuery()
'                End Using
'            End Using
'        End SyncLock

'        MessageBox.Show("P 图导入成功")
'    End Sub

'    ''' <summary>
'    ''' 功能：从 Excel 导入 P 图按钮
'    ''' </summary>
'    Private Sub btnImportPChart_Click(sender As Object, e As EventArgs)
'        Try
'            ImportPChartFromExcel()
'        Catch ex As Exception
'            MessageBox.Show("导入失败：" & ex.Message & vbCrLf & ex.StackTrace)
'        End Try
'    End Sub


'End Class