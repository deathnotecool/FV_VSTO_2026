Imports System.Windows.Forms

''' <summary>
''' 功能：日期范围选择窗体
''' </summary>
Public Class L_DateRange

    Public Property StartDate As Date = Date.Today
    Public Property EndDate As Date = Date.Today

    Private Sub L_DateRange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpStart.Value = StartDate
        dtpEnd.Value = EndDate
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If dtpStart.Value > dtpEnd.Value Then
            MessageBox.Show("起始日期不能大于结束日期")
            Return
        End If
        StartDate = dtpStart.Value
        EndDate = dtpEnd.Value
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class