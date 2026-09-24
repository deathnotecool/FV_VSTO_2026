Imports System.Data.OleDb
Imports System.Data
''' <summary>
''' 功能：FMEA 模块数据库公共资源
''' </summary>
Module M_FmeaDb

    ''' <summary>
    ''' 功能：返回 FMEA 数据库连接字符串
    ''' </summary>
    Public ReadOnly Property FmeaConnStr() As String
        Get
            Return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" &
                   "\\192.168.3.250\Erpupgrade\王飞共享体系资料\access\FMEA质量数据.accdb;"
        End Get
    End Property

    ''' <summary>
    ''' 功能：返回一个新的 OleDbConnection 对象
    ''' </summary>
    Public Function GetConnection() As OleDbConnection
        Return New OleDbConnection(FmeaConnStr)
    End Function

    ''' <summary>
    ''' 功能：全局写锁对象，保证共享盘 Access 写操作串行
    ''' </summary>
    Public ReadOnly WriteLock As New Object()

End Module
