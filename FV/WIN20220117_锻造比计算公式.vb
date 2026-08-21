Imports System.Windows.Forms
Imports FV.My.Resources
Public Class WIN20220117_锻造比计算公式
    Private Sub btnCaulate_Click(sender As Object, e As EventArgs) Handles btnCaulate.Click

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

        Dim sinRatio1 As Single = 0, sinRatio2 As Single = 0, rngSlecRange As Excel.Range
        Dim msgResult As MsgBoxResult

        sinRatio1 = CType(下料高度.Text, Single) / CType(墩粗高度.Text, Single)

        sinRatio2 = ((CType(冲孔外径.Text, Single) - CType(冲孔中心内径.Text, Single)) * CType(墩粗高度.Text, Single)) / ((CType(坯料外径.Text, Single) - CType(坯料内径.Text, Single)) * CType(坯料高度.Text, Single))



        锻造比.Text = (sinRatio1 + sinRatio2).ToString

        msgResult = MsgBox("计算值放入单元格:" + Chr(10) + "如选择是：选择指定的单元格；" + Chr(10) + "选择否：放弃填入指定单元格", vbYesNo + vbQuestion, "操作方式") '选择操作方式
        If msgResult = 6 Then  '如果选择是,打开软件，并修改注册表项目c的新值...
            rngSlecRange = xlapp.InputBox("请选择单元格：", Type:=8) '选择单元格
            rngSlecRange.Value = "锻造比" : rngSlecRange.Offset(0, 1).Value = 锻造比.Text
            rngSlecRange.Offset(0, 1).Interior.Color = 65535
            rngSlecRange.Offset(1, 0).Value = "下料高度" : rngSlecRange.Offset(1, 1).Value = 下料高度.Text
            rngSlecRange.Offset(2, 0).Value = "墩粗高度" : rngSlecRange.Offset(2, 1).Value = 墩粗高度.Text
            rngSlecRange.Offset(3, 0).Value = "冲孔外径" : rngSlecRange.Offset(3, 1).Value = 冲孔外径.Text
            rngSlecRange.Offset(4, 0).Value = "冲孔中心内径" : rngSlecRange.Offset(4, 1).Value = 冲孔中心内径.Text
            rngSlecRange.Offset(5, 0).Value = "坯料外径" : rngSlecRange.Offset(5, 1).Value = 坯料外径.Text
            rngSlecRange.Offset(6, 0).Value = "坯料内径" : rngSlecRange.Offset(6, 1).Value = 坯料内径.Text
            rngSlecRange.Offset(7, 0).Value = "坯料高度" : rngSlecRange.Offset(7, 1).Value = 坯料高度.Text
            rngSlecRange.Offset(8, 0).Value = "墩粗比" : rngSlecRange.Offset(8, 1).Value = sinRatio1
            rngSlecRange.Offset(8, 1).Interior.Color = 65535

            rngSlecRange.Offset(9, 0).Value = "碾环比" : rngSlecRange.Offset(9, 1).Value = sinRatio2
            rngSlecRange.Offset(9, 1).Interior.Color = 65535
            ''.......................................................................................
            'xlapp.OnUndo("撤消[锻造比计算]", "撤消") '这里代码调用的是FV.xlam加载项的撤销方法

            ''.......................................................................................
        End If
    End Sub

    Private Sub WIN20220117_锻造比计算公式_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '别删,另一种方式获取值和图片,今后可能非常有用.

        picbDisplayPicture.Image = Spec.ResourceManager.GetObject("锻造流程图")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        '清理内存及数据适配器对象

        Globals.Ribbons.Ribbon1.btnRatio.Enabled = True
        Me.Close()
    End Sub


    Private Sub WIN20220117_锻造比计算公式_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Globals.Ribbons.Ribbon1.btnRatio.Enabled = True
        Me.Close()
    End Sub
End Class