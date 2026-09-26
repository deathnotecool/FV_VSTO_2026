Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Forms

''' <summary>
''' 功能：FMEA P图六因素维护窗体（主表 1-1）
''' </summary>
Public Class L_PChart

    ' ========== 窗体级变量 ==========

    ''' <summary>
    ''' 功能：当前主表 ID，由主窗体传入
    ''' </summary>
    Public Property MainID As Long = 0

    ''' <summary>
    ''' 功能：是否已存在 P 图记录。True=已存在走 UPDATE，False=新增走 INSERT
    ''' </summary>
    Private blnExists As Boolean = False

    ''' <summary>
    ''' 功能：已存在记录的 lngID，UPDATE 时用
    ''' </summary>
    Private lngPChartID As Long = 0

    ' ========== 窗体加载 ==========

    ''' <summary>
    ''' 功能：窗体加载，显示工序信息，载入 P 图
    ''' </summary>
    Private Sub L_PChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ShowMainInfo()
            LoadData()
        Catch ex As Exception
            MessageBox.Show("加载失败：" & ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：显示顶部当前工序信息（只读）
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
    ''' 功能：按 MainID 读 P 图记录，有则显示，无则清空
    ''' </summary>
    Private Sub LoadData()
        Using conn As OleDbConnection = GetConnection()
            Using cmd As New OleDbCommand("SELECT * FROM tblPChart WHERE lngMainID=?", conn)
                cmd.Parameters.Add("p1", OleDbType.Integer).Value = MainID
                conn.Open()
                Using rd As OleDbDataReader = cmd.ExecuteReader()
                    If rd.Read() Then
                        blnExists = True
                        lngPChartID = CLng(rd("lngID"))
                        txtInfoInput.Text = If(IsDBNull(rd("memInfoInput")), "", rd("memInfoInput").ToString())
                        txtControlFactor.Text = If(IsDBNull(rd("memControlFactor")), "", rd("memControlFactor").ToString())
                        txtSystemFunction.Text = If(IsDBNull(rd("memSystemFunction")), "", rd("memSystemFunction").ToString())
                        txtExpectedOutput.Text = If(IsDBNull(rd("memExpectedOutput")), "", rd("memExpectedOutput").ToString())
                        txtUnexpectedOutput.Text = If(IsDBNull(rd("memUnexpectedOutput")), "", rd("memUnexpectedOutput").ToString())
                        txtNoiseFactor.Text = If(IsDBNull(rd("memNoiseFactor")), "", rd("memNoiseFactor").ToString())
                        txtRemark.Text = If(IsDBNull(rd("memRemark")), "", rd("memRemark").ToString())
                        lblStatusBar.Text = "已加载 P 图，可修改后保存"
                    Else
                        blnExists = False
                        lngPChartID = 0
                        ClearControls()
                        lblStatusBar.Text = "暂无 P 图，填写后保存"
                    End If
                End Using
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' 功能：清空所有录入控件
    ''' </summary>
    Private Sub ClearControls()
        txtInfoInput.Text = ""
        txtControlFactor.Text = ""
        txtSystemFunction.Text = ""
        txtExpectedOutput.Text = ""
        txtUnexpectedOutput.Text = ""
        txtNoiseFactor.Text = ""
        txtRemark.Text = ""
    End Sub

    ' ========== 按钮事件 ==========

    ''' <summary>
    ''' 功能：保存按钮，按是否存在走 INSERT 或 UPDATE
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If blnExists Then
                UpdateRecord()
            Else
                InsertRecord()
            End If

            LoadData()
            lblStatusBar.Text = "保存成功"
        Catch ex As Exception
            MessageBox.Show("保存失败：" & ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' 功能：关闭按钮
    ''' </summary>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' ========== 数据库操作 ==========

    ''' <summary>
    ''' 功能：插入一条新 P 图记录
    ''' </summary>
    Private Sub InsertRecord()
        SyncLock WriteLock
            Dim strSql As String = "INSERT INTO tblPChart " &
                "(lngMainID, memInfoInput, memControlFactor, memSystemFunction, " &
                "memExpectedOutput, memUnexpectedOutput, memNoiseFactor, memRemark, " &
                "dtmCreateTime, dtmUpdateTime) " &
                "VALUES (?,?,?,?,?,?,?,?,?,?)"

            Using conn As OleDbConnection = GetConnection()
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
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

    ''' <summary>
    ''' 功能：更新已存在的 P 图记录
    ''' </summary>
    Private Sub UpdateRecord()
        SyncLock WriteLock
            Dim strSql As String = "UPDATE tblPChart SET " &
                "memInfoInput=?, memControlFactor=?, memSystemFunction=?, " &
                "memExpectedOutput=?, memUnexpectedOutput=?, memNoiseFactor=?, " &
                "memRemark=?, dtmUpdateTime=? " &
                "WHERE lngID=?"

            Using conn As OleDbConnection = GetConnection()
                Using cmd As New OleDbCommand(strSql, conn)
                    cmd.Parameters.Add("p1", OleDbType.LongVarWChar).Value = txtInfoInput.Text
                    cmd.Parameters.Add("p2", OleDbType.LongVarWChar).Value = txtControlFactor.Text
                    cmd.Parameters.Add("p3", OleDbType.LongVarWChar).Value = txtSystemFunction.Text
                    cmd.Parameters.Add("p4", OleDbType.LongVarWChar).Value = txtExpectedOutput.Text
                    cmd.Parameters.Add("p5", OleDbType.LongVarWChar).Value = txtUnexpectedOutput.Text
                    cmd.Parameters.Add("p6", OleDbType.LongVarWChar).Value = txtNoiseFactor.Text
                    cmd.Parameters.Add("p7", OleDbType.LongVarWChar).Value = txtRemark.Text
                    cmd.Parameters.Add("p8", OleDbType.Date).Value = Now
                    cmd.Parameters.Add("p9", OleDbType.Integer).Value = lngPChartID
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End SyncLock
    End Sub

End Class