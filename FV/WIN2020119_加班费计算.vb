Public Class WIN2020119_加班费计算
    Private Sub btnCaulate_Click(sender As Object, e As EventArgs) Handles btnCaulate.Click
        Dim sinEachDay As Single = 0, sinEachHour As Single = 0, rngSlecRange As Excel.Range
        Dim msgResult As MsgBoxResult

        sinEachDay = CType(txtTotalBasic.Text, Single) / CType(txtDays.Text, Single)
        sinEachHour = sinEachDay / 8
        Dim sinOneAndHalf As Single = 0, sinTwoTimes As Single = 0, sinThreeTimes As Single = 0
        sinOneAndHalf = CType(txtMoreHalf.Text, Single) * sinEachHour * 1.5
        sinTwoTimes = CType(txtMore2.Text, Single) * sinEachHour * 2
        sinThreeTimes = CType(txtMore3.Text, Single) * sinEachHour * 3
        txtTotalAddMoney.Text = (sinOneAndHalf + sinTwoTimes + sinThreeTimes).ToString

        msgResult = MsgBox("计算值放入单元格:" + Chr(10) + "如选择是：选择指定的单元格；" + Chr(10) + "选择否：放弃填入指定单元格", vbYesNo + vbQuestion, "操作方式") '选择操作方式
        If msgResult = 6 Then  '如果选择是,打开软件，并修改注册表项目c的新值...
            rngSlecRange = xlapp.InputBox("请选择单元格：", Type:=8) '选择单元格
            rngSlecRange.Value = "加班费总计" : rngSlecRange.Offset(0, 1).Value = txtTotalAddMoney.Text

            rngSlecRange.Offset(1, 0).Value = "基本工资" : rngSlecRange.Offset(1, 1).Value = CType(txtTotalBasic.Text, Integer)
            rngSlecRange.Offset(2, 0).Value = "岗位补贴" : rngSlecRange.Offset(2, 1).Value = CType(txt岗位补贴.Text, Integer)
            rngSlecRange.Offset(3, 0).Value = "特殊津贴" : rngSlecRange.Offset(3, 1).Value = CType(txt特殊津贴.Text, Integer)
            rngSlecRange.Offset(4, 0).Value = "公积金代缴" : rngSlecRange.Offset(4, 1).Value = CType(strAccumulationFund.Text, Integer)
            rngSlecRange.Offset(5, 0).Value = "社保" : rngSlecRange.Offset(5, 1).Value = CType(txtSocialSecurity.Text, Integer)
            rngSlecRange.Offset(6, 0).Value = "实际收入" : rngSlecRange.Offset(6, 1).FormulaR1C1 = "=SUM(R[-6]C:R[-1]C)"
        End If


    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Globals.Ribbons.Ribbon1.btnAddMoney.Enabled = True
        Me.Close()
    End Sub

    Private Sub WIN2020119_加班费计算_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnAddMoney.Enabled = True
    End Sub


End Class