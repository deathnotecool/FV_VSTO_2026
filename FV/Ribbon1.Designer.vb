Partial Class Ribbon1
    Inherits Microsoft.Office.Tools.Ribbon.RibbonBase

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Windows.Forms 类撰写设计器支持所必需的
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New(Globals.Factory.GetRibbonFactory())

        '组件设计器需要此调用。
        InitializeComponent()

    End Sub

    '组件重写释放以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '组件设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是组件设计器所必需的
    '可使用组件设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Ribbon1))
        Dim RibbonDropDownItemImpl1 As Microsoft.Office.Tools.Ribbon.RibbonDropDownItem = Me.Factory.CreateRibbonDropDownItem
        Dim RibbonDropDownItemImpl2 As Microsoft.Office.Tools.Ribbon.RibbonDropDownItem = Me.Factory.CreateRibbonDropDownItem
        Dim RibbonDropDownItemImpl3 As Microsoft.Office.Tools.Ribbon.RibbonDropDownItem = Me.Factory.CreateRibbonDropDownItem
        Dim RibbonDropDownItemImpl4 As Microsoft.Office.Tools.Ribbon.RibbonDropDownItem = Me.Factory.CreateRibbonDropDownItem
        Dim RibbonDropDownItemImpl5 As Microsoft.Office.Tools.Ribbon.RibbonDropDownItem = Me.Factory.CreateRibbonDropDownItem
        Me.Tab1 = Me.Factory.CreateRibbonTab
        Me.Group3 = Me.Factory.CreateRibbonGroup
        Me.Menu7 = Me.Factory.CreateRibbonMenu
        Me.btnFileInfo = Me.Factory.CreateRibbonButton
        Me.Button19 = Me.Factory.CreateRibbonButton
        Me.Button18 = Me.Factory.CreateRibbonButton
        Me.btnCertifcate = Me.Factory.CreateRibbonButton
        Me.btnCertificateOutput = Me.Factory.CreateRibbonButton
        Me.Separator7 = Me.Factory.CreateRibbonSeparator
        Me.Menu8 = Me.Factory.CreateRibbonMenu
        Me.Button26 = Me.Factory.CreateRibbonButton
        Me.Button27 = Me.Factory.CreateRibbonButton
        Me.Button28 = Me.Factory.CreateRibbonButton
        Me.Button29 = Me.Factory.CreateRibbonButton
        Me.Button30 = Me.Factory.CreateRibbonButton
        Me.Separator8 = Me.Factory.CreateRibbonSeparator
        Me.Menu9 = Me.Factory.CreateRibbonMenu
        Me.Button31 = Me.Factory.CreateRibbonButton
        Me.Button33 = Me.Factory.CreateRibbonButton
        Me.Button32 = Me.Factory.CreateRibbonButton
        Me.Button34 = Me.Factory.CreateRibbonButton
        Me.Button35 = Me.Factory.CreateRibbonButton
        Me.Button36 = Me.Factory.CreateRibbonButton
        Me.Button37 = Me.Factory.CreateRibbonButton
        Me.Menu19 = Me.Factory.CreateRibbonMenu
        Me.btnInputOrderInfo = Me.Factory.CreateRibbonButton
        Me.btnCodeForIncoming = Me.Factory.CreateRibbonButton
        Me.btnStorgeCheck = Me.Factory.CreateRibbonButton
        Me.btnLayoutCode = Me.Factory.CreateRibbonButton
        Me.Menu11 = Me.Factory.CreateRibbonMenu
        Me.btn证书 = Me.Factory.CreateRibbonButton
        Me.btnInformationExtract = Me.Factory.CreateRibbonButton
        Me.btn调休节假信息 = Me.Factory.CreateRibbonButton
        Me.btn加班时间 = Me.Factory.CreateRibbonButton
        Me.Menu14 = Me.Factory.CreateRibbonMenu
        Me.btn不良品信息 = Me.Factory.CreateRibbonButton
        Me.btn不良品信息查询与导出 = Me.Factory.CreateRibbonButton
        Me.btn不良信息分析 = Me.Factory.CreateRibbonButton
        Me.btn索赔信息 = Me.Factory.CreateRibbonButton
        Me.btn索赔信息查询与导出 = Me.Factory.CreateRibbonButton
        Me.btnGetInform = Me.Factory.CreateRibbonButton
        Me.Menu12 = Me.Factory.CreateRibbonMenu
        Me.btn电能 = Me.Factory.CreateRibbonButton
        Me.Menu17 = Me.Factory.CreateRibbonMenu
        Me.btnQcChecked = Me.Factory.CreateRibbonButton
        Me.btnQcCheckedNew = Me.Factory.CreateRibbonButton
        Me.btnSerachConformityInformathing = Me.Factory.CreateRibbonButton
        Me.btnSerachConformityInformathingNew = Me.Factory.CreateRibbonButton
        Me.Menu16 = Me.Factory.CreateRibbonMenu
        Me.btnTesting = Me.Factory.CreateRibbonButton
        Me.btnSearchInspect = Me.Factory.CreateRibbonButton
        Me.btnCost = Me.Factory.CreateRibbonButton
        Me.Button2 = Me.Factory.CreateRibbonButton
        Me.Group2 = Me.Factory.CreateRibbonGroup
        Me.Menu4 = Me.Factory.CreateRibbonMenu
        Me.btnMergeRange = Me.Factory.CreateRibbonButton
        Me.btnUnMergeRange = Me.Factory.CreateRibbonButton
        Me.btnMergeCellsRetainContonts = Me.Factory.CreateRibbonButton
        Me.Group1 = Me.Factory.CreateRibbonGroup
        Me.Menu15 = Me.Factory.CreateRibbonMenu
        Me.btnFrequency = Me.Factory.CreateRibbonButton
        Me.btnDataCollect = Me.Factory.CreateRibbonButton
        Me.btnDistance = Me.Factory.CreateRibbonButton
        Me.btnRatio = Me.Factory.CreateRibbonButton
        Me.btnAnalyzeHeatTreatmentData = Me.Factory.CreateRibbonButton
        Me.Separator1 = Me.Factory.CreateRibbonSeparator
        Me.Menu6 = Me.Factory.CreateRibbonMenu
        Me.btnExtractId = Me.Factory.CreateRibbonButton
        Me.btnCreateBill = Me.Factory.CreateRibbonButton
        Me.Separator2 = Me.Factory.CreateRibbonSeparator
        Me.Menu1 = Me.Factory.CreateRibbonMenu
        Me.btnColumnAndAreaDeletePicture = Me.Factory.CreateRibbonButton
        Me.btnAreaLocalPicture = Me.Factory.CreateRibbonButton
        Me.btnSort = Me.Factory.CreateRibbonButton
        Me.btnControlSize = Me.Factory.CreateRibbonButton
        Me.Separator3 = Me.Factory.CreateRibbonSeparator
        Me.Menu2 = Me.Factory.CreateRibbonMenu
        Me.btnConversionPDF = Me.Factory.CreateRibbonButton
        Me.Button16 = Me.Factory.CreateRibbonButton
        Me.btnBatchNaming = Me.Factory.CreateRibbonButton
        Me.Button24 = Me.Factory.CreateRibbonButton
        Me.Separator4 = Me.Factory.CreateRibbonSeparator
        Me.Menu5 = Me.Factory.CreateRibbonMenu
        Me.Button10 = Me.Factory.CreateRibbonButton
        Me.Button11 = Me.Factory.CreateRibbonButton
        Me.Button12 = Me.Factory.CreateRibbonButton
        Me.Button13 = Me.Factory.CreateRibbonButton
        Me.Button14 = Me.Factory.CreateRibbonButton
        Me.btnCheckWords = Me.Factory.CreateRibbonButton
        Me.btn标示重复值 = Me.Factory.CreateRibbonButton
        Me.btnCompare = Me.Factory.CreateRibbonButton
        Me.Button25 = Me.Factory.CreateRibbonButton
        Me.btnAutoFontSize = Me.Factory.CreateRibbonButton
        Me.Menu20 = Me.Factory.CreateRibbonMenu
        Me.btnDeleteEmptyRows = Me.Factory.CreateRibbonButton
        Me.btnCopyData = Me.Factory.CreateRibbonButton
        Me.Menu18 = Me.Factory.CreateRibbonMenu
        Me.btnSplitWorkbook = Me.Factory.CreateRibbonButton
        Me.btnSplitName = Me.Factory.CreateRibbonButton
        Me.Separator5 = Me.Factory.CreateRibbonSeparator
        Me.Menu3 = Me.Factory.CreateRibbonMenu
        Me.Button6 = Me.Factory.CreateRibbonButton
        Me.btnDisplayDate = Me.Factory.CreateRibbonButton
        Me.Button7 = Me.Factory.CreateRibbonButton
        Me.btnOpenWeb = Me.Factory.CreateRibbonButton
        Me.btnIP = Me.Factory.CreateRibbonButton
        Me.btnHideErr = Me.Factory.CreateRibbonButton
        Me.btnSearchNote = Me.Factory.CreateRibbonButton
        Me.btnAddMoney = Me.Factory.CreateRibbonButton
        Me.Separator6 = Me.Factory.CreateRibbonSeparator
        Me.Menu13 = Me.Factory.CreateRibbonMenu
        Me.btn奇偶定位 = Me.Factory.CreateRibbonButton
        Me.btnGreaterData = Me.Factory.CreateRibbonButton
        Me.btnLessData = Me.Factory.CreateRibbonButton
        Me.DropDown1 = Me.Factory.CreateRibbonDropDown
        Me.toggleButton1 = Me.Factory.CreateRibbonToggleButton
        Me.Separator9 = Me.Factory.CreateRibbonSeparator
        Me.Menu10 = Me.Factory.CreateRibbonMenu
        Me.Button20 = Me.Factory.CreateRibbonButton
        Me.Button38 = Me.Factory.CreateRibbonButton
        Me.Button39 = Me.Factory.CreateRibbonButton
        Me.Button40 = Me.Factory.CreateRibbonButton
        Me.btnForDosan = Me.Factory.CreateRibbonButton
        Me.btnQuickCode = Me.Factory.CreateRibbonButton
        Me.Button5 = Me.Factory.CreateRibbonButton
        Me.Group4 = Me.Factory.CreateRibbonGroup
        Me.btnUndo = Me.Factory.CreateRibbonButton
        Me.Button1 = Me.Factory.CreateRibbonButton
        Me.TEST = Me.Factory.CreateRibbonButton
        Me.ntyRibbon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Tab1.SuspendLayout()
        Me.Group3.SuspendLayout()
        Me.Group2.SuspendLayout()
        Me.Group1.SuspendLayout()
        Me.Group4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tab1
        '
        Me.Tab1.Groups.Add(Me.Group3)
        Me.Tab1.Groups.Add(Me.Group2)
        Me.Tab1.Groups.Add(Me.Group1)
        Me.Tab1.Groups.Add(Me.Group4)
        Me.Tab1.KeyTip = "W"
        Me.Tab1.Label = "FV"
        Me.Tab1.Name = "Tab1"
        Me.Tab1.Position = Me.Factory.RibbonPosition.BeforeOfficeId("TabHome")
        '
        'Group3
        '
        Me.Group3.Items.Add(Me.Menu7)
        Me.Group3.Items.Add(Me.Separator7)
        Me.Group3.Items.Add(Me.Menu8)
        Me.Group3.Items.Add(Me.Separator8)
        Me.Group3.Items.Add(Me.Menu9)
        Me.Group3.Items.Add(Me.Menu19)
        Me.Group3.Items.Add(Me.Menu11)
        Me.Group3.Items.Add(Me.Menu14)
        Me.Group3.Items.Add(Me.Menu12)
        Me.Group3.Items.Add(Me.Menu17)
        Me.Group3.Items.Add(Me.Menu16)
        Me.Group3.Label = "F之数据库"
        Me.Group3.Name = "Group3"
        '
        'Menu7
        '
        Me.Menu7.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu7.Image = CType(resources.GetObject("Menu7.Image"), System.Drawing.Image)
        Me.Menu7.Items.Add(Me.btnFileInfo)
        Me.Menu7.Items.Add(Me.Button19)
        Me.Menu7.Items.Add(Me.Button18)
        Me.Menu7.Items.Add(Me.btnCertifcate)
        Me.Menu7.Items.Add(Me.btnCertificateOutput)
        Me.Menu7.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu7.Label = "文件管理"
        Me.Menu7.Name = "Menu7"
        Me.Menu7.ShowImage = True
        '
        'btnFileInfo
        '
        Me.btnFileInfo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnFileInfo.Image = CType(resources.GetObject("btnFileInfo.Image"), System.Drawing.Image)
        Me.btnFileInfo.Label = "文件基本信息"
        Me.btnFileInfo.Name = "btnFileInfo"
        Me.btnFileInfo.ScreenTip = "显示文件信息[A01]"
        Me.btnFileInfo.ShowImage = True
        Me.btnFileInfo.SuperTip = "详细罗列文件清单及其信息"
        '
        'Button19
        '
        Me.Button19.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button19.Image = CType(resources.GetObject("Button19.Image"), System.Drawing.Image)
        Me.Button19.Label = "文件履历卡"
        Me.Button19.Name = "Button19"
        Me.Button19.ShowImage = True
        '
        'Button18
        '
        Me.Button18.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button18.Image = CType(resources.GetObject("Button18.Image"), System.Drawing.Image)
        Me.Button18.Label = "文件信息查询与导出"
        Me.Button18.Name = "Button18"
        Me.Button18.ShowImage = True
        '
        'btnCertifcate
        '
        Me.btnCertifcate.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnCertifcate.Image = CType(resources.GetObject("btnCertifcate.Image"), System.Drawing.Image)
        Me.btnCertifcate.Label = "质保书总台账"
        Me.btnCertifcate.Name = "btnCertifcate"
        Me.btnCertifcate.ShowImage = True
        '
        'btnCertificateOutput
        '
        Me.btnCertificateOutput.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnCertificateOutput.Image = CType(resources.GetObject("btnCertificateOutput.Image"), System.Drawing.Image)
        Me.btnCertificateOutput.Label = "质保书信息查询与导出"
        Me.btnCertificateOutput.Name = "btnCertificateOutput"
        Me.btnCertificateOutput.ShowImage = True
        '
        'Separator7
        '
        Me.Separator7.Name = "Separator7"
        '
        'Menu8
        '
        Me.Menu8.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu8.Enabled = False
        Me.Menu8.Image = CType(resources.GetObject("Menu8.Image"), System.Drawing.Image)
        Me.Menu8.Items.Add(Me.Button26)
        Me.Menu8.Items.Add(Me.Button27)
        Me.Menu8.Items.Add(Me.Button28)
        Me.Menu8.Items.Add(Me.Button29)
        Me.Menu8.Items.Add(Me.Button30)
        Me.Menu8.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu8.Label = "设备管理"
        Me.Menu8.Name = "Menu8"
        Me.Menu8.ShowImage = True
        '
        'Button26
        '
        Me.Button26.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button26.Image = CType(resources.GetObject("Button26.Image"), System.Drawing.Image)
        Me.Button26.Label = "设备基本信息"
        Me.Button26.Name = "Button26"
        Me.Button26.ShowImage = True
        '
        'Button27
        '
        Me.Button27.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button27.Image = CType(resources.GetObject("Button27.Image"), System.Drawing.Image)
        Me.Button27.Label = "维修资料"
        Me.Button27.Name = "Button27"
        Me.Button27.ShowImage = True
        '
        'Button28
        '
        Me.Button28.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button28.Image = CType(resources.GetObject("Button28.Image"), System.Drawing.Image)
        Me.Button28.Label = "保养资料"
        Me.Button28.Name = "Button28"
        Me.Button28.ShowImage = True
        '
        'Button29
        '
        Me.Button29.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button29.Image = CType(resources.GetObject("Button29.Image"), System.Drawing.Image)
        Me.Button29.Label = "备件库存"
        Me.Button29.Name = "Button29"
        Me.Button29.ShowImage = True
        '
        'Button30
        '
        Me.Button30.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button30.Image = CType(resources.GetObject("Button30.Image"), System.Drawing.Image)
        Me.Button30.Label = "信息查询导出"
        Me.Button30.Name = "Button30"
        Me.Button30.ShowImage = True
        '
        'Separator8
        '
        Me.Separator8.Name = "Separator8"
        '
        'Menu9
        '
        Me.Menu9.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu9.Image = CType(resources.GetObject("Menu9.Image"), System.Drawing.Image)
        Me.Menu9.Items.Add(Me.Button31)
        Me.Menu9.Items.Add(Me.Button33)
        Me.Menu9.Items.Add(Me.Button32)
        Me.Menu9.Items.Add(Me.Button34)
        Me.Menu9.Items.Add(Me.Button35)
        Me.Menu9.Items.Add(Me.Button36)
        Me.Menu9.Items.Add(Me.Button37)
        Me.Menu9.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu9.Label = "采购管理"
        Me.Menu9.Name = "Menu9"
        Me.Menu9.ShowImage = True
        '
        'Button31
        '
        Me.Button31.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button31.Image = CType(resources.GetObject("Button31.Image"), System.Drawing.Image)
        Me.Button31.Label = "供应商信息"
        Me.Button31.Name = "Button31"
        Me.Button31.ShowImage = True
        '
        'Button33
        '
        Me.Button33.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button33.Image = CType(resources.GetObject("Button33.Image"), System.Drawing.Image)
        Me.Button33.Label = "采购-物品信息"
        Me.Button33.Name = "Button33"
        Me.Button33.ShowImage = True
        '
        'Button32
        '
        Me.Button32.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button32.Image = CType(resources.GetObject("Button32.Image"), System.Drawing.Image)
        Me.Button32.Label = "采购-进货信息"
        Me.Button32.Name = "Button32"
        Me.Button32.ShowImage = True
        '
        'Button34
        '
        Me.Button34.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button34.Image = CType(resources.GetObject("Button34.Image"), System.Drawing.Image)
        Me.Button34.Label = "物品消耗信息"
        Me.Button34.Name = "Button34"
        Me.Button34.ShowImage = True
        '
        'Button35
        '
        Me.Button35.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button35.Image = CType(resources.GetObject("Button35.Image"), System.Drawing.Image)
        Me.Button35.Label = "物品库存信息"
        Me.Button35.Name = "Button35"
        Me.Button35.ShowImage = True
        '
        'Button36
        '
        Me.Button36.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button36.Image = CType(resources.GetObject("Button36.Image"), System.Drawing.Image)
        Me.Button36.Label = "物品消耗分析"
        Me.Button36.Name = "Button36"
        Me.Button36.ShowImage = True
        '
        'Button37
        '
        Me.Button37.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button37.Image = CType(resources.GetObject("Button37.Image"), System.Drawing.Image)
        Me.Button37.Label = "物品管理查询与导出"
        Me.Button37.Name = "Button37"
        Me.Button37.ShowImage = True
        '
        'Menu19
        '
        Me.Menu19.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu19.Image = CType(resources.GetObject("Menu19.Image"), System.Drawing.Image)
        Me.Menu19.Items.Add(Me.btnInputOrderInfo)
        Me.Menu19.Items.Add(Me.btnCodeForIncoming)
        Me.Menu19.Items.Add(Me.btnStorgeCheck)
        Me.Menu19.Items.Add(Me.btnLayoutCode)
        Me.Menu19.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu19.Label = "原料信息管理"
        Me.Menu19.Name = "Menu19"
        Me.Menu19.ShowImage = True
        '
        'btnInputOrderInfo
        '
        Me.btnInputOrderInfo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnInputOrderInfo.Image = CType(resources.GetObject("btnInputOrderInfo.Image"), System.Drawing.Image)
        Me.btnInputOrderInfo.Label = "订单转数据库"
        Me.btnInputOrderInfo.Name = "btnInputOrderInfo"
        Me.btnInputOrderInfo.ShowImage = True
        '
        'btnCodeForIncoming
        '
        Me.btnCodeForIncoming.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnCodeForIncoming.Image = CType(resources.GetObject("btnCodeForIncoming.Image"), System.Drawing.Image)
        Me.btnCodeForIncoming.Label = "入库编号信息"
        Me.btnCodeForIncoming.Name = "btnCodeForIncoming"
        Me.btnCodeForIncoming.ShowImage = True
        '
        'btnStorgeCheck
        '
        Me.btnStorgeCheck.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnStorgeCheck.Image = CType(resources.GetObject("btnStorgeCheck.Image"), System.Drawing.Image)
        Me.btnStorgeCheck.Label = "订单入库情况"
        Me.btnStorgeCheck.Name = "btnStorgeCheck"
        Me.btnStorgeCheck.ShowImage = True
        '
        'btnLayoutCode
        '
        Me.btnLayoutCode.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnLayoutCode.Image = CType(resources.GetObject("btnLayoutCode.Image"), System.Drawing.Image)
        Me.btnLayoutCode.Label = "导出编号信息"
        Me.btnLayoutCode.Name = "btnLayoutCode"
        Me.btnLayoutCode.ShowImage = True
        '
        'Menu11
        '
        Me.Menu11.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu11.Image = CType(resources.GetObject("Menu11.Image"), System.Drawing.Image)
        Me.Menu11.Items.Add(Me.btn证书)
        Me.Menu11.Items.Add(Me.btnInformationExtract)
        Me.Menu11.Items.Add(Me.btn调休节假信息)
        Me.Menu11.Items.Add(Me.btn加班时间)
        Me.Menu11.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu11.Label = "人力资源管理"
        Me.Menu11.Name = "Menu11"
        Me.Menu11.ShowImage = True
        '
        'btn证书
        '
        Me.btn证书.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btn证书.Image = CType(resources.GetObject("btn证书.Image"), System.Drawing.Image)
        Me.btn证书.Label = "资质证书"
        Me.btn证书.Name = "btn证书"
        Me.btn证书.ShowImage = True
        '
        'btnInformationExtract
        '
        Me.btnInformationExtract.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnInformationExtract.Image = CType(resources.GetObject("btnInformationExtract.Image"), System.Drawing.Image)
        Me.btnInformationExtract.Label = "人事信息导出"
        Me.btnInformationExtract.Name = "btnInformationExtract"
        Me.btnInformationExtract.ShowImage = True
        '
        'btn调休节假信息
        '
        Me.btn调休节假信息.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btn调休节假信息.Image = CType(resources.GetObject("btn调休节假信息.Image"), System.Drawing.Image)
        Me.btn调休节假信息.Label = "调休节假日信息"
        Me.btn调休节假信息.Name = "btn调休节假信息"
        Me.btn调休节假信息.ShowImage = True
        '
        'btn加班时间
        '
        Me.btn加班时间.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btn加班时间.Image = CType(resources.GetObject("btn加班时间.Image"), System.Drawing.Image)
        Me.btn加班时间.Label = "加班时间统计"
        Me.btn加班时间.Name = "btn加班时间"
        Me.btn加班时间.ShowImage = True
        '
        'Menu14
        '
        Me.Menu14.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu14.Items.Add(Me.btn不良品信息)
        Me.Menu14.Items.Add(Me.btn不良品信息查询与导出)
        Me.Menu14.Items.Add(Me.btn不良信息分析)
        Me.Menu14.Items.Add(Me.btn索赔信息)
        Me.Menu14.Items.Add(Me.btn索赔信息查询与导出)
        Me.Menu14.Items.Add(Me.btnGetInform)
        Me.Menu14.Label = "不良品管理系"
        Me.Menu14.Name = "Menu14"
        Me.Menu14.OfficeImageId = "FileStartWorkflow"
        Me.Menu14.ShowImage = True
        '
        'btn不良品信息
        '
        Me.btn不良品信息.Label = "不良品信息"
        Me.btn不良品信息.Name = "btn不良品信息"
        Me.btn不良品信息.OfficeImageId = "InkDeleteAllInk"
        Me.btn不良品信息.ShowImage = True
        '
        'btn不良品信息查询与导出
        '
        Me.btn不良品信息查询与导出.Image = CType(resources.GetObject("btn不良品信息查询与导出.Image"), System.Drawing.Image)
        Me.btn不良品信息查询与导出.Label = "不良品信息查询与导出"
        Me.btn不良品信息查询与导出.Name = "btn不良品信息查询与导出"
        Me.btn不良品信息查询与导出.ShowImage = True
        '
        'btn不良信息分析
        '
        Me.btn不良信息分析.Image = CType(resources.GetObject("btn不良信息分析.Image"), System.Drawing.Image)
        Me.btn不良信息分析.Label = "不良信息分析"
        Me.btn不良信息分析.Name = "btn不良信息分析"
        Me.btn不良信息分析.ShowImage = True
        '
        'btn索赔信息
        '
        Me.btn索赔信息.Image = CType(resources.GetObject("btn索赔信息.Image"), System.Drawing.Image)
        Me.btn索赔信息.Label = "索赔信息"
        Me.btn索赔信息.Name = "btn索赔信息"
        Me.btn索赔信息.ShowImage = True
        '
        'btn索赔信息查询与导出
        '
        Me.btn索赔信息查询与导出.Image = CType(resources.GetObject("btn索赔信息查询与导出.Image"), System.Drawing.Image)
        Me.btn索赔信息查询与导出.Label = "索赔信息查询与导出"
        Me.btn索赔信息查询与导出.Name = "btn索赔信息查询与导出"
        Me.btn索赔信息查询与导出.ShowImage = True
        '
        'btnGetInform
        '
        Me.btnGetInform.Image = CType(resources.GetObject("btnGetInform.Image"), System.Drawing.Image)
        Me.btnGetInform.Label = "信息提取"
        Me.btnGetInform.Name = "btnGetInform"
        Me.btnGetInform.ScreenTip = "GN002_信息提取"
        Me.btnGetInform.ShowImage = True
        Me.btnGetInform.SuperTip = "快速 提取发生日期,不良现象及原因,管理编号,不良数量"
        '
        'Menu12
        '
        Me.Menu12.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu12.Image = CType(resources.GetObject("Menu12.Image"), System.Drawing.Image)
        Me.Menu12.Items.Add(Me.btn电能)
        Me.Menu12.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu12.Label = "能源消耗管理"
        Me.Menu12.Name = "Menu12"
        Me.Menu12.ShowImage = True
        '
        'btn电能
        '
        Me.btn电能.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btn电能.Image = CType(resources.GetObject("btn电能.Image"), System.Drawing.Image)
        Me.btn电能.Label = "电量统计"
        Me.btn电能.Name = "btn电能"
        Me.btn电能.ShowImage = True
        '
        'Menu17
        '
        Me.Menu17.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu17.Items.Add(Me.btnQcChecked)
        Me.Menu17.Items.Add(Me.btnQcCheckedNew)
        Me.Menu17.Items.Add(Me.btnSerachConformityInformathing)
        Me.Menu17.Items.Add(Me.btnSerachConformityInformathingNew)
        Me.Menu17.Label = "合格记录管理"
        Me.Menu17.Name = "Menu17"
        Me.Menu17.OfficeImageId = "AcceptInvitation"
        Me.Menu17.ShowImage = True
        '
        'btnQcChecked
        '
        Me.btnQcChecked.Image = CType(resources.GetObject("btnQcChecked.Image"), System.Drawing.Image)
        Me.btnQcChecked.Label = "合格确认(风电)"
        Me.btnQcChecked.Name = "btnQcChecked"
        Me.btnQcChecked.ShowImage = True
        '
        'btnQcCheckedNew
        '
        Me.btnQcCheckedNew.Image = CType(resources.GetObject("btnQcCheckedNew.Image"), System.Drawing.Image)
        Me.btnQcCheckedNew.Label = "合格确认(工程)"
        Me.btnQcCheckedNew.Name = "btnQcCheckedNew"
        Me.btnQcCheckedNew.ShowImage = True
        '
        'btnSerachConformityInformathing
        '
        Me.btnSerachConformityInformathing.Image = CType(resources.GetObject("btnSerachConformityInformathing.Image"), System.Drawing.Image)
        Me.btnSerachConformityInformathing.Label = "合格信息查询(风电)"
        Me.btnSerachConformityInformathing.Name = "btnSerachConformityInformathing"
        Me.btnSerachConformityInformathing.ShowImage = True
        '
        'btnSerachConformityInformathingNew
        '
        Me.btnSerachConformityInformathingNew.Image = CType(resources.GetObject("btnSerachConformityInformathingNew.Image"), System.Drawing.Image)
        Me.btnSerachConformityInformathingNew.Label = "合格信息查询(工程)"
        Me.btnSerachConformityInformathingNew.Name = "btnSerachConformityInformathingNew"
        Me.btnSerachConformityInformathingNew.ShowImage = True
        '
        'Menu16
        '
        Me.Menu16.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu16.Items.Add(Me.btnTesting)
        Me.Menu16.Items.Add(Me.btnSearchInspect)
        Me.Menu16.Items.Add(Me.btnCost)
        Me.Menu16.Items.Add(Me.Button2)
        Me.Menu16.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu16.Label = "检测试验管理"
        Me.Menu16.Name = "Menu16"
        Me.Menu16.OfficeImageId = "CreateReportInDesignView"
        Me.Menu16.ShowImage = True
        '
        'btnTesting
        '
        Me.btnTesting.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnTesting.Image = CType(resources.GetObject("btnTesting.Image"), System.Drawing.Image)
        Me.btnTesting.Label = "检查测试信息"
        Me.btnTesting.Name = "btnTesting"
        Me.btnTesting.ShowImage = True
        '
        'btnSearchInspect
        '
        Me.btnSearchInspect.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnSearchInspect.Image = CType(resources.GetObject("btnSearchInspect.Image"), System.Drawing.Image)
        Me.btnSearchInspect.Label = "检测测试信息查询"
        Me.btnSearchInspect.Name = "btnSearchInspect"
        Me.btnSearchInspect.ShowImage = True
        '
        'btnCost
        '
        Me.btnCost.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnCost.Image = CType(resources.GetObject("btnCost.Image"), System.Drawing.Image)
        Me.btnCost.Label = "质量消费台账"
        Me.btnCost.Name = "btnCost"
        Me.btnCost.ShowImage = True
        '
        'Button2
        '
        Me.Button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Label = "质量费用查询"
        Me.Button2.Name = "Button2"
        Me.Button2.ShowImage = True
        '
        'Group2
        '
        Me.Group2.Items.Add(Me.Menu4)
        Me.Group2.Label = "强化内置功能"
        Me.Group2.Name = "Group2"
        '
        'Menu4
        '
        Me.Menu4.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu4.Image = CType(resources.GetObject("Menu4.Image"), System.Drawing.Image)
        Me.Menu4.Items.Add(Me.btnMergeRange)
        Me.Menu4.Items.Add(Me.btnUnMergeRange)
        Me.Menu4.Items.Add(Me.btnMergeCellsRetainContonts)
        Me.Menu4.Label = "合并技能包"
        Me.Menu4.Name = "Menu4"
        Me.Menu4.ShowImage = True
        '
        'btnMergeRange
        '
        Me.btnMergeRange.Image = CType(resources.GetObject("btnMergeRange.Image"), System.Drawing.Image)
        Me.btnMergeRange.Label = "相同数据合并"
        Me.btnMergeRange.Name = "btnMergeRange"
        Me.btnMergeRange.ScreenTip = "相同数据合并[GN001]"
        Me.btnMergeRange.ShowImage = True
        Me.btnMergeRange.SuperTip = "可以将同一列连续的相同数据合并"
        '
        'btnUnMergeRange
        '
        Me.btnUnMergeRange.Image = CType(resources.GetObject("btnUnMergeRange.Image"), System.Drawing.Image)
        Me.btnUnMergeRange.Label = "增强版:取消合并"
        Me.btnUnMergeRange.Name = "btnUnMergeRange"
        Me.btnUnMergeRange.ScreenTip = "相同数据取消合并"
        Me.btnUnMergeRange.ShowImage = True
        Me.btnUnMergeRange.SuperTip = "将你选中的某列,取消合并,并在空单元格中填充合并数据."
        '
        'btnMergeCellsRetainContonts
        '
        Me.btnMergeCellsRetainContonts.Image = CType(resources.GetObject("btnMergeCellsRetainContonts.Image"), System.Drawing.Image)
        Me.btnMergeCellsRetainContonts.Label = "合并后保留值"
        Me.btnMergeCellsRetainContonts.Name = "btnMergeCellsRetainContonts"
        Me.btnMergeCellsRetainContonts.ScreenTip = "强化内置合并功能"
        Me.btnMergeCellsRetainContonts.ShowImage = True
        Me.btnMergeCellsRetainContonts.SuperTip = "合并单元格后,保留所有值."
        '
        'Group1
        '
        Me.Group1.Items.Add(Me.Menu15)
        Me.Group1.Items.Add(Me.Separator1)
        Me.Group1.Items.Add(Me.Menu6)
        Me.Group1.Items.Add(Me.Separator2)
        Me.Group1.Items.Add(Me.Menu1)
        Me.Group1.Items.Add(Me.Separator3)
        Me.Group1.Items.Add(Me.Menu2)
        Me.Group1.Items.Add(Me.Separator4)
        Me.Group1.Items.Add(Me.Menu5)
        Me.Group1.Items.Add(Me.Menu20)
        Me.Group1.Items.Add(Me.Menu18)
        Me.Group1.Items.Add(Me.Separator5)
        Me.Group1.Items.Add(Me.Menu3)
        Me.Group1.Items.Add(Me.Separator6)
        Me.Group1.Items.Add(Me.Menu13)
        Me.Group1.Items.Add(Me.DropDown1)
        Me.Group1.Items.Add(Me.toggleButton1)
        Me.Group1.Items.Add(Me.Separator9)
        Me.Group1.Items.Add(Me.Menu10)
        Me.Group1.Label = "Ctrl+鼠标左键 功能按钮,调出教程"
        Me.Group1.Name = "Group1"
        '
        'Menu15
        '
        Me.Menu15.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu15.Image = CType(resources.GetObject("Menu15.Image"), System.Drawing.Image)
        Me.Menu15.Items.Add(Me.btnFrequency)
        Me.Menu15.Items.Add(Me.btnDataCollect)
        Me.Menu15.Items.Add(Me.btnDistance)
        Me.Menu15.Items.Add(Me.btnRatio)
        Me.Menu15.Items.Add(Me.btnAnalyzeHeatTreatmentData)
        Me.Menu15.Label = "质量分析工具"
        Me.Menu15.Name = "Menu15"
        Me.Menu15.ShowImage = True
        '
        'btnFrequency
        '
        Me.btnFrequency.Label = "频率分析"
        Me.btnFrequency.Name = "btnFrequency"
        Me.btnFrequency.OfficeImageId = "SlideMasterChartPlaceholderInsert"
        Me.btnFrequency.ScreenTip = "频率分析"
        Me.btnFrequency.ShowImage = True
        Me.btnFrequency.SuperTip = "根据设定的区间,统计分析频次"
        '
        'btnDataCollect
        '
        Me.btnDataCollect.Image = CType(resources.GetObject("btnDataCollect.Image"), System.Drawing.Image)
        Me.btnDataCollect.Label = "数据定位"
        Me.btnDataCollect.Name = "btnDataCollect"
        Me.btnDataCollect.ScreenTip = "选中指定单元格"
        Me.btnDataCollect.ShowImage = True
        Me.btnDataCollect.SuperTip = "查看而不破坏单元格,如填充颜色"
        Me.btnDataCollect.Tag = ""
        '
        'btnDistance
        '
        Me.btnDistance.Image = CType(resources.GetObject("btnDistance.Image"), System.Drawing.Image)
        Me.btnDistance.Label = "孔间距计算"
        Me.btnDistance.Name = "btnDistance"
        Me.btnDistance.ScreenTip = "轴承节圆孔间距"
        Me.btnDistance.ShowImage = True
        Me.btnDistance.SuperTip = "根据输入的参数,计算两孔边最短距离"
        '
        'btnRatio
        '
        Me.btnRatio.Image = CType(resources.GetObject("btnRatio.Image"), System.Drawing.Image)
        Me.btnRatio.Label = "锻造比计算"
        Me.btnRatio.Name = "btnRatio"
        Me.btnRatio.ScreenTip = "锻造比简易计算器"
        Me.btnRatio.ShowImage = True
        Me.btnRatio.SuperTip = "只适用于锻件为墩粗+碾环工序"
        '
        'btnAnalyzeHeatTreatmentData
        '
        Me.btnAnalyzeHeatTreatmentData.Image = CType(resources.GetObject("btnAnalyzeHeatTreatmentData.Image"), System.Drawing.Image)
        Me.btnAnalyzeHeatTreatmentData.Label = "回火数据分析"
        Me.btnAnalyzeHeatTreatmentData.Name = "btnAnalyzeHeatTreatmentData"
        Me.btnAnalyzeHeatTreatmentData.ScreenTip = "GN004 - 热处理回火数据分析"
        Me.btnAnalyzeHeatTreatmentData.ShowImage = True
        Me.btnAnalyzeHeatTreatmentData.SuperTip = "单击后找到保温时间开始/结束点,以及保温时间段"
        '
        'Separator1
        '
        Me.Separator1.Name = "Separator1"
        '
        'Menu6
        '
        Me.Menu6.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu6.Image = CType(resources.GetObject("Menu6.Image"), System.Drawing.Image)
        Me.Menu6.Items.Add(Me.btnExtractId)
        Me.Menu6.Items.Add(Me.btnCreateBill)
        Me.Menu6.Label = "人事财务工具"
        Me.Menu6.Name = "Menu6"
        Me.Menu6.ShowImage = True
        '
        'btnExtractId
        '
        Me.btnExtractId.Image = CType(resources.GetObject("btnExtractId.Image"), System.Drawing.Image)
        Me.btnExtractId.Label = "身份证提取信息"
        Me.btnExtractId.Name = "btnExtractId"
        Me.btnExtractId.ScreenTip = "GN003_身份证提取信息"
        Me.btnExtractId.ShowImage = True
        Me.btnExtractId.SuperTip = "选中含有身份证号码单元格列并执行该功能."
        '
        'btnCreateBill
        '
        Me.btnCreateBill.Image = CType(resources.GetObject("btnCreateBill.Image"), System.Drawing.Image)
        Me.btnCreateBill.Label = "生成工资条"
        Me.btnCreateBill.Name = "btnCreateBill"
        Me.btnCreateBill.ScreenTip = "GN004_生成工资条"
        Me.btnCreateBill.ShowImage = True
        Me.btnCreateBill.SuperTip = "从工资单执行该按钮,工资单必须首行为标题."
        '
        'Separator2
        '
        Me.Separator2.Name = "Separator2"
        '
        'Menu1
        '
        Me.Menu1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu1.Image = CType(resources.GetObject("Menu1.Image"), System.Drawing.Image)
        Me.Menu1.Items.Add(Me.btnColumnAndAreaDeletePicture)
        Me.Menu1.Items.Add(Me.btnAreaLocalPicture)
        Me.Menu1.Items.Add(Me.btnSort)
        Me.Menu1.Items.Add(Me.btnControlSize)
        Me.Menu1.Label = "图片处理技术"
        Me.Menu1.Name = "Menu1"
        Me.Menu1.ShowImage = True
        '
        'btnColumnAndAreaDeletePicture
        '
        Me.btnColumnAndAreaDeletePicture.Image = CType(resources.GetObject("btnColumnAndAreaDeletePicture.Image"), System.Drawing.Image)
        Me.btnColumnAndAreaDeletePicture.Label = "列和区域删图"
        Me.btnColumnAndAreaDeletePicture.Name = "btnColumnAndAreaDeletePicture"
        Me.btnColumnAndAreaDeletePicture.ScreenTip = "列和区域删图"
        Me.btnColumnAndAreaDeletePicture.ShowImage = True
        Me.btnColumnAndAreaDeletePicture.SuperTip = "可以删除指定列上或指定区域的图片."
        '
        'btnAreaLocalPicture
        '
        Me.btnAreaLocalPicture.Image = CType(resources.GetObject("btnAreaLocalPicture.Image"), System.Drawing.Image)
        Me.btnAreaLocalPicture.Label = "区域放置图片"
        Me.btnAreaLocalPicture.Name = "btnAreaLocalPicture"
        Me.btnAreaLocalPicture.ScreenTip = "区域放置图片"
        Me.btnAreaLocalPicture.ShowImage = True
        Me.btnAreaLocalPicture.SuperTip = "选中图片后,指定单元格放置,或合并图片覆盖的单元格."
        '
        'btnSort
        '
        Me.btnSort.Image = CType(resources.GetObject("btnSort.Image"), System.Drawing.Image)
        Me.btnSort.Label = "多图排列"
        Me.btnSort.Name = "btnSort"
        Me.btnSort.ScreenTip = "多图向下或向右排列"
        Me.btnSort.ShowImage = True
        Me.btnSort.SuperTip = "选中一个单元格后,将在该单元格下方或右边依次排列图片."
        '
        'btnControlSize
        '
        Me.btnControlSize.Image = CType(resources.GetObject("btnControlSize.Image"), System.Drawing.Image)
        Me.btnControlSize.Label = "图统一尺寸"
        Me.btnControlSize.Name = "btnControlSize"
        Me.btnControlSize.ScreenTip = "图统一尺寸"
        Me.btnControlSize.ShowImage = True
        Me.btnControlSize.SuperTip = "文本框内输入参考图形名称，作为统一大小的参考图。"
        '
        'Separator3
        '
        Me.Separator3.Name = "Separator3"
        '
        'Menu2
        '
        Me.Menu2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu2.Image = CType(resources.GetObject("Menu2.Image"), System.Drawing.Image)
        Me.Menu2.Items.Add(Me.btnConversionPDF)
        Me.Menu2.Items.Add(Me.Button16)
        Me.Menu2.Items.Add(Me.btnBatchNaming)
        Me.Menu2.Items.Add(Me.Button24)
        Me.Menu2.Label = "文件处理技术"
        Me.Menu2.Name = "Menu2"
        Me.Menu2.ShowImage = True
        '
        'btnConversionPDF
        '
        Me.btnConversionPDF.Image = CType(resources.GetObject("btnConversionPDF.Image"), System.Drawing.Image)
        Me.btnConversionPDF.Label = "批量转PDF"
        Me.btnConversionPDF.Name = "btnConversionPDF"
        Me.btnConversionPDF.ScreenTip = "批量转PDF"
        Me.btnConversionPDF.ShowImage = True
        Me.btnConversionPDF.SuperTip = "选中待转化的文件夹,批量将Word/Excel 文件转化为PDF文件."
        '
        'Button16
        '
        Me.Button16.Image = CType(resources.GetObject("Button16.Image"), System.Drawing.Image)
        Me.Button16.Label = "获取文件信息"
        Me.Button16.Name = "Button16"
        Me.Button16.ScreenTip = "GN0011_获取文件信息"
        Me.Button16.ShowImage = True
        Me.Button16.SuperTip = "选择指定文件夹,执行该功能按钮,将把选择中的所有文件夹及子文件信息提取出来"
        '
        'btnBatchNaming
        '
        Me.btnBatchNaming.Label = "文件批量命名"
        Me.btnBatchNaming.Name = "btnBatchNaming"
        Me.btnBatchNaming.OfficeImageId = "FormControlEditBox"
        Me.btnBatchNaming.ScreenTip = "WN18081503_文件批量命名"
        Me.btnBatchNaming.ShowImage = True
        Me.btnBatchNaming.SuperTip = "可以批量对指定文件夹内文件重新命名修改."
        '
        'Button24
        '
        Me.Button24.Image = CType(resources.GetObject("Button24.Image"), System.Drawing.Image)
        Me.Button24.Label = "文件集合移动"
        Me.Button24.Name = "Button24"
        Me.Button24.ScreenTip = "GN0013_文件集合移动"
        Me.Button24.ShowImage = True
        Me.Button24.SuperTip = "（神罗天征技能，慎用）操作前先备份,主要是将文件夹及子文件夹下文件移动到一个指定文件夹内."
        '
        'Separator4
        '
        Me.Separator4.Name = "Separator4"
        '
        'Menu5
        '
        Me.Menu5.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu5.Image = CType(resources.GetObject("Menu5.Image"), System.Drawing.Image)
        Me.Menu5.Items.Add(Me.Button10)
        Me.Menu5.Items.Add(Me.Button11)
        Me.Menu5.Items.Add(Me.Button12)
        Me.Menu5.Items.Add(Me.Button13)
        Me.Menu5.Items.Add(Me.Button14)
        Me.Menu5.Items.Add(Me.btnCheckWords)
        Me.Menu5.Items.Add(Me.btn标示重复值)
        Me.Menu5.Items.Add(Me.btnCompare)
        Me.Menu5.Items.Add(Me.Button25)
        Me.Menu5.Items.Add(Me.btnAutoFontSize)
        Me.Menu5.Label = "字符数据处理"
        Me.Menu5.Name = "Menu5"
        Me.Menu5.ShowImage = True
        '
        'Button10
        '
        Me.Button10.Image = CType(resources.GetObject("Button10.Image"), System.Drawing.Image)
        Me.Button10.Label = "获取数字"
        Me.Button10.Name = "Button10"
        Me.Button10.ScreenTip = "GN0014_获取数字"
        Me.Button10.ShowImage = True
        Me.Button10.SuperTip = "选择要匹配的单列区域,执行,或指定单元格区域执行分列."
        '
        'Button11
        '
        Me.Button11.Image = CType(resources.GetObject("Button11.Image"), System.Drawing.Image)
        Me.Button11.Label = "去除数字"
        Me.Button11.Name = "Button11"
        Me.Button11.ScreenTip = "GN0015_去除数字"
        Me.Button11.ShowImage = True
        Me.Button11.SuperTip = "可以选中某列区域操作."
        '
        'Button12
        '
        Me.Button12.Image = CType(resources.GetObject("Button12.Image"), System.Drawing.Image)
        Me.Button12.Label = "获取字母"
        Me.Button12.Name = "Button12"
        Me.Button12.ScreenTip = "GN0016_获取字母"
        Me.Button12.ShowImage = True
        Me.Button12.SuperTip = "选择区域获取字母."
        '
        'Button13
        '
        Me.Button13.Image = CType(resources.GetObject("Button13.Image"), System.Drawing.Image)
        Me.Button13.Label = "去除字母"
        Me.Button13.Name = "Button13"
        Me.Button13.ScreenTip = "GN0017_去除字母"
        Me.Button13.ShowImage = True
        Me.Button13.SuperTip = "选择区域,删除字母"
        '
        'Button14
        '
        Me.Button14.Image = CType(resources.GetObject("Button14.Image"), System.Drawing.Image)
        Me.Button14.Label = "自定义删除符号"
        Me.Button14.Name = "Button14"
        Me.Button14.ScreenTip = "GN0018_自定义删除符号"
        Me.Button14.ShowImage = True
        Me.Button14.SuperTip = "选定区域删除指定符号."
        '
        'btnCheckWords
        '
        Me.btnCheckWords.Image = CType(resources.GetObject("btnCheckWords.Image"), System.Drawing.Image)
        Me.btnCheckWords.Label = "英语拼写检查"
        Me.btnCheckWords.Name = "btnCheckWords"
        Me.btnCheckWords.ScreenTip = "GN180820 01 英语单词拼写检查"
        Me.btnCheckWords.ShowImage = True
        Me.btnCheckWords.SuperTip = "选择该功能前，请选择区域，将帮你自动判定是否单词错误."
        '
        'btn标示重复值
        '
        Me.btn标示重复值.Image = CType(resources.GetObject("btn标示重复值.Image"), System.Drawing.Image)
        Me.btn标示重复值.Label = "标示重复值"
        Me.btn标示重复值.Name = "btn标示重复值"
        Me.btn标示重复值.ShowImage = True
        '
        'btnCompare
        '
        Me.btnCompare.Image = CType(resources.GetObject("btnCompare.Image"), System.Drawing.Image)
        Me.btnCompare.Label = "区域数据比较"
        Me.btnCompare.Name = "btnCompare"
        Me.btnCompare.ShowImage = True
        '
        'Button25
        '
        Me.Button25.Image = CType(resources.GetObject("Button25.Image"), System.Drawing.Image)
        Me.Button25.Label = "随机数范围"
        Me.Button25.Name = "Button25"
        Me.Button25.ScreenTip = "WN18081502 随机数范围"
        Me.Button25.ShowImage = True
        Me.Button25.SuperTip = "可以设置一个数值范围"
        '
        'btnAutoFontSize
        '
        Me.btnAutoFontSize.Image = CType(resources.GetObject("btnAutoFontSize.Image"), System.Drawing.Image)
        Me.btnAutoFontSize.Label = "自动调整行高"
        Me.btnAutoFontSize.Name = "btnAutoFontSize"
        Me.btnAutoFontSize.ScreenTip = "GN001_自动调整行高"
        Me.btnAutoFontSize.ShowImage = True
        Me.btnAutoFontSize.SuperTip = "单击后自动匹配已用单元格区域合适的行高"
        '
        'Menu20
        '
        Me.Menu20.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu20.Items.Add(Me.btnDeleteEmptyRows)
        Me.Menu20.Items.Add(Me.btnCopyData)
        Me.Menu20.Label = "工作表处理"
        Me.Menu20.Name = "Menu20"
        Me.Menu20.OfficeImageId = "SmartArtStylesGallery"
        Me.Menu20.ShowImage = True
        '
        'btnDeleteEmptyRows
        '
        Me.btnDeleteEmptyRows.Label = "删除已用区域空行"
        Me.btnDeleteEmptyRows.Name = "btnDeleteEmptyRows"
        Me.btnDeleteEmptyRows.OfficeImageId = "MergeCells"
        Me.btnDeleteEmptyRows.ShowImage = True
        '
        'btnCopyData
        '
        Me.btnCopyData.Label = "多区域复制"
        Me.btnCopyData.Name = "btnCopyData"
        Me.btnCopyData.OfficeImageId = "WindowSwitchWindowsMenuExcel"
        Me.btnCopyData.ShowImage = True
        '
        'Menu18
        '
        Me.Menu18.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu18.Image = CType(resources.GetObject("Menu18.Image"), System.Drawing.Image)
        Me.Menu18.Items.Add(Me.btnSplitWorkbook)
        Me.Menu18.Items.Add(Me.btnSplitName)
        Me.Menu18.Label = "工作簿处理"
        Me.Menu18.Name = "Menu18"
        Me.Menu18.ShowImage = True
        '
        'btnSplitWorkbook
        '
        Me.btnSplitWorkbook.Image = CType(resources.GetObject("btnSplitWorkbook.Image"), System.Drawing.Image)
        Me.btnSplitWorkbook.Label = "拆分工作薄"
        Me.btnSplitWorkbook.Name = "btnSplitWorkbook"
        Me.btnSplitWorkbook.ShowImage = True
        '
        'btnSplitName
        '
        Me.btnSplitName.Image = CType(resources.GetObject("btnSplitName.Image"), System.Drawing.Image)
        Me.btnSplitName.Label = "供应商拆分"
        Me.btnSplitName.Name = "btnSplitName"
        Me.btnSplitName.ShowImage = True
        '
        'Separator5
        '
        Me.Separator5.Name = "Separator5"
        '
        'Menu3
        '
        Me.Menu3.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu3.Image = CType(resources.GetObject("Menu3.Image"), System.Drawing.Image)
        Me.Menu3.Items.Add(Me.Button6)
        Me.Menu3.Items.Add(Me.btnDisplayDate)
        Me.Menu3.Items.Add(Me.Button7)
        Me.Menu3.Items.Add(Me.btnOpenWeb)
        Me.Menu3.Items.Add(Me.btnIP)
        Me.Menu3.Items.Add(Me.btnHideErr)
        Me.Menu3.Items.Add(Me.btnSearchNote)
        Me.Menu3.Items.Add(Me.btnAddMoney)
        Me.Menu3.Label = "其他功能"
        Me.Menu3.Name = "Menu3"
        Me.Menu3.ShowImage = True
        '
        'Button6
        '
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.Label = "解除表格密码"
        Me.Button6.Name = "Button6"
        Me.Button6.ScreenTip = "GN0019_解除表格密码"
        Me.Button6.ShowImage = True
        Me.Button6.SuperTip = "批量解除表格密码"
        '
        'btnDisplayDate
        '
        Me.btnDisplayDate.Image = CType(resources.GetObject("btnDisplayDate.Image"), System.Drawing.Image)
        Me.btnDisplayDate.Label = "表单窗格"
        Me.btnDisplayDate.Name = "btnDisplayDate"
        Me.btnDisplayDate.ScreenTip = "集合列表清单"
        Me.btnDisplayDate.ShowImage = True
        Me.btnDisplayDate.SuperTip = "支持EXCEL 2010版本及以下"
        '
        'Button7
        '
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"), System.Drawing.Image)
        Me.Button7.Label = "批量加密表格"
        Me.Button7.Name = "Button7"
        Me.Button7.ScreenTip = "GN020_批量加密表格"
        Me.Button7.ShowImage = True
        '
        'btnOpenWeb
        '
        Me.btnOpenWeb.Image = CType(resources.GetObject("btnOpenWeb.Image"), System.Drawing.Image)
        Me.btnOpenWeb.Label = "打开网页"
        Me.btnOpenWeb.Name = "btnOpenWeb"
        Me.btnOpenWeb.ShowImage = True
        '
        'btnIP
        '
        Me.btnIP.Image = CType(resources.GetObject("btnIP.Image"), System.Drawing.Image)
        Me.btnIP.Label = "本机IP"
        Me.btnIP.Name = "btnIP"
        Me.btnIP.ShowImage = True
        '
        'btnHideErr
        '
        Me.btnHideErr.Image = CType(resources.GetObject("btnHideErr.Image"), System.Drawing.Image)
        Me.btnHideErr.Label = "错误值隐藏"
        Me.btnHideErr.Name = "btnHideErr"
        Me.btnHideErr.ScreenTip = "GN190107_错误值隐藏"
        Me.btnHideErr.ShowImage = True
        Me.btnHideErr.SuperTip = "发生错误的单元格对其进行隐藏."
        '
        'btnSearchNote
        '
        Me.btnSearchNote.Image = CType(resources.GetObject("btnSearchNote.Image"), System.Drawing.Image)
        Me.btnSearchNote.Label = "VBA代码笔记"
        Me.btnSearchNote.Name = "btnSearchNote"
        Me.btnSearchNote.ShowImage = True
        '
        'btnAddMoney
        '
        Me.btnAddMoney.Image = CType(resources.GetObject("btnAddMoney.Image"), System.Drawing.Image)
        Me.btnAddMoney.Label = "加班费计算"
        Me.btnAddMoney.Name = "btnAddMoney"
        Me.btnAddMoney.ShowImage = True
        '
        'Separator6
        '
        Me.Separator6.Name = "Separator6"
        '
        'Menu13
        '
        Me.Menu13.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu13.Image = CType(resources.GetObject("Menu13.Image"), System.Drawing.Image)
        Me.Menu13.Items.Add(Me.btn奇偶定位)
        Me.Menu13.Items.Add(Me.btnGreaterData)
        Me.Menu13.Items.Add(Me.btnLessData)
        Me.Menu13.Label = "定位技术"
        Me.Menu13.Name = "Menu13"
        Me.Menu13.ShowImage = True
        '
        'btn奇偶定位
        '
        Me.btn奇偶定位.Image = CType(resources.GetObject("btn奇偶定位.Image"), System.Drawing.Image)
        Me.btn奇偶定位.Label = "定位奇偶"
        Me.btn奇偶定位.Name = "btn奇偶定位"
        Me.btn奇偶定位.ScreenTip = "GN190105_定位奇偶"
        Me.btn奇偶定位.ShowImage = True
        Me.btn奇偶定位.SuperTip = "根据选定类型，对已用区域选择奇偶行."
        '
        'btnGreaterData
        '
        Me.btnGreaterData.Label = "大于（G）..."
        Me.btnGreaterData.Name = "btnGreaterData"
        Me.btnGreaterData.OfficeImageId = "ConditionalFormattingHighlightGreaterThan"
        Me.btnGreaterData.ScreenTip = "GN 180817 01 大于设定的数值"
        Me.btnGreaterData.ShowImage = True
        Me.btnGreaterData.SuperTip = "输入值后，自动选中已用区域大于的值."
        '
        'btnLessData
        '
        Me.btnLessData.Label = "小于（L）"
        Me.btnLessData.Name = "btnLessData"
        Me.btnLessData.OfficeImageId = "ConditionalFormattingHighlightLessThan"
        Me.btnLessData.ScreenTip = "GN 180817 02 小于设定的数值"
        Me.btnLessData.ShowImage = True
        Me.btnLessData.SuperTip = "输入值后，自动选中已用区域小于的值."
        '
        'DropDown1
        '
        RibbonDropDownItemImpl1.Label = "请选择条件"
        RibbonDropDownItemImpl2.Label = "最大值"
        RibbonDropDownItemImpl3.Label = "最小值"
        RibbonDropDownItemImpl4.Label = "平均值"
        RibbonDropDownItemImpl5.Label = "众数"
        Me.DropDown1.Items.Add(RibbonDropDownItemImpl1)
        Me.DropDown1.Items.Add(RibbonDropDownItemImpl2)
        Me.DropDown1.Items.Add(RibbonDropDownItemImpl3)
        Me.DropDown1.Items.Add(RibbonDropDownItemImpl4)
        Me.DropDown1.Items.Add(RibbonDropDownItemImpl5)
        Me.DropDown1.Label = "定位数值"
        Me.DropDown1.Name = "DropDown1"
        Me.DropDown1.ScreenTip = "GN021_定位"
        Me.DropDown1.SuperTip = "选择区域,下拉选项,最大:深黄;最小:浅黄;众数:亮绿;平均值:绿色."
        '
        'toggleButton1
        '
        Me.toggleButton1.Image = CType(resources.GetObject("toggleButton1.Image"), System.Drawing.Image)
        Me.toggleButton1.Label = "行列聚光灯"
        Me.toggleButton1.Name = "toggleButton1"
        Me.toggleButton1.ShowImage = True
        '
        'Separator9
        '
        Me.Separator9.Name = "Separator9"
        '
        'Menu10
        '
        Me.Menu10.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Menu10.Image = CType(resources.GetObject("Menu10.Image"), System.Drawing.Image)
        Me.Menu10.Items.Add(Me.Button20)
        Me.Menu10.Items.Add(Me.Button38)
        Me.Menu10.Items.Add(Me.Button39)
        Me.Menu10.Items.Add(Me.Button40)
        Me.Menu10.Items.Add(Me.btnForDosan)
        Me.Menu10.Items.Add(Me.btnQuickCode)
        Me.Menu10.Items.Add(Me.Button5)
        Me.Menu10.Label = "二维/条形码"
        Me.Menu10.Name = "Menu10"
        Me.Menu10.ShowImage = True
        '
        'Button20
        '
        Me.Button20.Enabled = False
        Me.Button20.Label = "测试二维码生成"
        Me.Button20.Name = "Button20"
        Me.Button20.ScreenTip = " 'GN0019_二维码生成"
        Me.Button20.ShowImage = True
        '
        'Button38
        '
        Me.Button38.Enabled = False
        Me.Button38.Label = "测试读取二维码"
        Me.Button38.Name = "Button38"
        Me.Button38.ShowImage = True
        '
        'Button39
        '
        Me.Button39.Image = CType(resources.GetObject("Button39.Image"), System.Drawing.Image)
        Me.Button39.Label = "条形码volvo"
        Me.Button39.Name = "Button39"
        Me.Button39.ShowImage = True
        '
        'Button40
        '
        Me.Button40.Enabled = False
        Me.Button40.Label = "测试条形码读取"
        Me.Button40.Name = "Button40"
        Me.Button40.ShowImage = True
        '
        'btnForDosan
        '
        Me.btnForDosan.Image = CType(resources.GetObject("btnForDosan.Image"), System.Drawing.Image)
        Me.btnForDosan.Label = "斗山模板专用"
        Me.btnForDosan.Name = "btnForDosan"
        Me.btnForDosan.ShowImage = True
        '
        'btnQuickCode
        '
        Me.btnQuickCode.Image = CType(resources.GetObject("btnQuickCode.Image"), System.Drawing.Image)
        Me.btnQuickCode.Label = "二维码"
        Me.btnQuickCode.Name = "btnQuickCode"
        Me.btnQuickCode.ShowImage = True
        '
        'Button5
        '
        Me.Button5.Label = "测试按钮CCC"
        Me.Button5.Name = "Button5"
        Me.Button5.ShowImage = True
        '
        'Group4
        '
        Me.Group4.Items.Add(Me.btnUndo)
        Me.Group4.Items.Add(Me.Button1)
        Me.Group4.Items.Add(Me.TEST)
        Me.Group4.Label = "谨慎使用"
        Me.Group4.Name = "Group4"
        '
        'btnUndo
        '
        Me.btnUndo.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.btnUndo.Enabled = False
        Me.btnUndo.Image = CType(resources.GetObject("btnUndo.Image"), System.Drawing.Image)
        Me.btnUndo.Label = "FV专用撤销"
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.ShowImage = True
        Me.btnUndo.SuperTip = "只撤销数据类操作,不支持图片类型动作后的撤销."
        '
        'Button1
        '
        Me.Button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.KeyTip = "F"
        Me.Button1.Label = "加班时间计算1"
        Me.Button1.Name = "Button1"
        Me.Button1.ShowImage = True
        '
        'TEST
        '
        Me.TEST.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge
        Me.TEST.Label = "TEST"
        Me.TEST.Name = "TEST"
        Me.TEST.ShowImage = True
        Me.TEST.Visible = False
        '
        'ntyRibbon
        '
        Me.ntyRibbon.Text = "NotifyIcon1"
        Me.ntyRibbon.Visible = True
        '
        'Ribbon1
        '
        Me.Name = "Ribbon1"
        Me.RibbonType = "Microsoft.Excel.Workbook"
        Me.Tabs.Add(Me.Tab1)
        Me.Tab1.ResumeLayout(False)
        Me.Tab1.PerformLayout()
        Me.Group3.ResumeLayout(False)
        Me.Group3.PerformLayout()
        Me.Group2.ResumeLayout(False)
        Me.Group2.PerformLayout()
        Me.Group1.ResumeLayout(False)
        Me.Group1.PerformLayout()
        Me.Group4.ResumeLayout(False)
        Me.Group4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Tab1 As Microsoft.Office.Tools.Ribbon.RibbonTab
    Friend WithEvents Group1 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents btnColumnAndAreaDeletePicture As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnAreaLocalPicture As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu1 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnSort As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu2 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnBatchNaming As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu3 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents Button6 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button7 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Group2 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents Menu4 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnMergeRange As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnUnMergeRange As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu5 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents Button10 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button11 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button12 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button13 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button14 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu6 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnExtractId As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Group3 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents Menu7 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnFileInfo As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Separator1 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents Separator2 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents Separator3 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents Separator4 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents Separator5 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents DropDown1 As Microsoft.Office.Tools.Ribbon.RibbonDropDown
    Friend WithEvents Button18 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button19 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Separator6 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents Button16 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button20 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnConversionPDF As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnCreateBill As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button24 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button25 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu8 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents Button26 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button27 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button28 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button29 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button30 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu9 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents Button31 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button32 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button33 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button34 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button35 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Separator7 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents Separator8 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents Button36 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button37 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button38 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button39 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu10 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents Button40 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu11 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btn证书 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnDisplayDate As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents ntyRibbon As Windows.Forms.NotifyIcon
    Friend WithEvents btnInformationExtract As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu12 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btn电能 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnOpenWeb As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnIP As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu13 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btn奇偶定位 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Separator9 As Microsoft.Office.Tools.Ribbon.RibbonSeparator
    Friend WithEvents btnGreaterData As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnLessData As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnCheckWords As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu14 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btn不良品信息 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btn不良品信息查询与导出 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btn不良信息分析 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnHideErr As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btn调休节假信息 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btn加班时间 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnControlSize As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu15 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnFrequency As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnDataCollect As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu16 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnCost As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button2 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btn标示重复值 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnCompare As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnCertifcate As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnCertificateOutput As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnQuickCode As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnDistance As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnMergeCellsRetainContonts As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSearchNote As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnUndo As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Group4 As Microsoft.Office.Tools.Ribbon.RibbonGroup
    Friend WithEvents btn索赔信息 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btn索赔信息查询与导出 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSearchInspect As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnQcChecked As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSerachConformityInformathing As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnTesting As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnCodeForIncoming As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnLayoutCode As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSplitWorkbook As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnAddMoney As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSplitName As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu17 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents Menu18 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnStorgeCheck As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu19 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnInputOrderInfo As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button5 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Button1 As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents Menu20 As Microsoft.Office.Tools.Ribbon.RibbonMenu
    Friend WithEvents btnDeleteEmptyRows As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnForDosan As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnRatio As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnQcCheckedNew As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnSerachConformityInformathingNew As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnCopyData As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents TEST As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents toggleButton1 As Microsoft.Office.Tools.Ribbon.RibbonToggleButton
    Friend WithEvents btnGetInform As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnAutoFontSize As Microsoft.Office.Tools.Ribbon.RibbonButton
    Friend WithEvents btnAnalyzeHeatTreatmentData As Microsoft.Office.Tools.Ribbon.RibbonButton
End Class

Partial Class ThisRibbonCollection

    <System.Diagnostics.DebuggerNonUserCode()> _
    Friend ReadOnly Property Ribbon1() As Ribbon1
        Get
            Return Me.GetRibbon(Of Ribbon1)()
        End Get
    End Property
End Class
