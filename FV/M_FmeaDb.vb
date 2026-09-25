Imports System.Data.OleDb
Imports System.Data

''' <summary>
''' 功能：FMEA 模块数据库公共资源
''' </summary>
Module M_FmeaDb

    ''' <summary>
    ''' 功能：全局写锁对象，保证共享盘 Access 写操作串行
    ''' </summary>
    Public ReadOnly WriteLock As New Object()

    ''' <summary>
    ''' 功能：缓存数据库路径，避免每次探测网络
    ''' </summary>
    Private _cachedPath As String = Nothing

    ''' <summary>
    ''' 功能：返回 FMEA 数据库连接字符串（多环境自动切换）
    ''' </summary>
    Public ReadOnly Property FmeaConnStr() As String
        Get
            ' 已探测过，直接用缓存
            If _cachedPath IsNot Nothing Then
                Return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _cachedPath & ";"
            End If

            ' 1. 公司共享盘正式库
            Dim strServer As String = "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\FMEA质量数据.accdb"

            ' 2. 本机测试库候选路径（哪个存在用哪个）
            Dim strLocals() As String = {
            "E:\6 工作总务\1-2 数据统计 26.06.02\FMEA质量数据_测试.accdb",
            "D:\Data\FMEA质量数据_测试.accdb",
            "D:\FMEA质量数据_测试.accdb",
            "C:\Data\FMEA质量数据_测试.accdb"
        }

            ' 先探测共享盘是否可达
            Dim blnServerOk As Boolean = False
            Try
                Dim t As New System.Threading.Thread(Sub()
                                                         blnServerOk = System.IO.Directory.Exists("\\192.168.3.250\Erpupgrade\")
                                                     End Sub)
                t.IsBackground = True
                t.Start()
                If Not t.Join(500) Then
                    blnServerOk = False
                End If
            Catch
                blnServerOk = False
            End Try

            ' 共享盘可达 → 用正式库
            If blnServerOk Then
                _cachedPath = strServer
                Return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _cachedPath & ";"
            End If

            ' 共享盘不可达 → 逐个探测本机测试库
            For Each strPath As String In strLocals
                If System.IO.File.Exists(strPath) Then
                    _cachedPath = strPath
                    Return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _cachedPath & ";"
                End If
            Next

            ' 都没找到，抛异常
            Throw New Exception("找不到 FMEA 数据库，请检查路径")
        End Get
    End Property

    ''' <summary>
    ''' 功能：返回一个新的 OleDbConnection 对象
    ''' </summary>
    Public Function GetConnection() As OleDbConnection
        Return New OleDbConnection(FmeaConnStr)
    End Function

End Module