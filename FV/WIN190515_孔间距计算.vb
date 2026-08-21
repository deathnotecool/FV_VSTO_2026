Public Class WIN190515_孔间距计算
    Private Sub btnEvaluate_Click(sender As Object, e As EventArgs) Handles btnEvaluate.Click
        On Error Resume Next

        ''____________________备份数据、记录区域___________________________
        'Targetsht = xlapp.ActiveSheet    '对公共变量赋值，在执行撤消时会用到 Targetsht
        'TargetRng = Targetsht.UsedRange.Address '对公共变量赋值，在执行备份和撤消时会用到TargetRng
        'Call 备份(Targetsht, TargetRng)
        'Globals.Ribbons.Ribbon1.btnUndo.Enabled = True '激活撤销按钮可用
        ''____________________备份数据、记录区域___________________________

        ' ============================================================
        ' ★★★ 第1步：备份数据（用于撤销） ★★★
        ' ============================================================
        M2_调用的任务.BackupActiveSheet()
        Globals.Ribbons.Ribbon1.btnUndo.Enabled = True


        Dim dblSumDimt As Double, dblFinnalValue As Double, msgResult As MsgBoxResult, rngSlecRange As Excel.Range
        dblSumDimt = (CType(txtDimt1.Text, Double) / 2 + CType(txtDimt2.Text, Double) / 2)
        dblFinnalValue = CType(txtPitch.Text, Double) * Math.Sin(Math.PI / 180 * (CType(txtAngle.Text, Double) / 2)) - dblSumDimt
        '(CType(txtDimt1.Text, Double) / 2 + CType(txtDimt2.Text, Double) / 2))
        'MsgBox(CType(txtPitch.Text, Double) * Math.Sin(Math.PI / 180 * (CType(txtAngle.Text / 2, Double)) - ))

        msgResult = MsgBox("您的计算结果为:" & dblFinnalValue & Chr(10) + "如选择是：选择指定的单元格；" + Chr(10) + "选择否：放弃填入指定单元格", vbYesNo + vbQuestion, "操作方式") '选择操作方式

        If msgResult = 6 Then  '如果选择是,打开软件，并修改注册表项目c的新值...
            rngSlecRange = xlapp.InputBox("请选择单元格：", Type:=8) '选择单元格
            rngSlecRange.Value = Math.Round(dblFinnalValue, 2)
        End If

        ''.......................................................................................
        'xlapp.OnUndo("撤消 输入的孔间距值", "撤消") '这里代码调用,并激活的是FV.xlam加载项的撤销方法

        ''.......................................................................................
        btnExit_Click(Nothing, Nothing)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        Globals.Ribbons.Ribbon1.btnDistance.Enabled = True     '重新启用按钮
        Me.Close()        '关闭窗体

    End Sub

    Private Sub WIN190515_孔间距计算_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnDistance.Enabled = True     '重新启用按钮
    End Sub


End Class