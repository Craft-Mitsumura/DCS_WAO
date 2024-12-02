Imports Npgsql

Public Class DBClient

    Private connectionString As String

    Public Sub New()
        Me.New(SettingManager.GetInstance.ConnectionString)
    End Sub

    ' コンストラクタ：接続文字列を設定
    Private Sub New(connectionString As String)
        Me.connectionString = connectionString
    End Sub

    ' データベース接続の作成
    Private Function GetConnection() As NpgsqlConnection
        Return New NpgsqlConnection(connectionString)
    End Function

    ' トランザクションを開始して複数行のデータを作成・更新・削除する
    Public Function ExecuteNonQuery(query As String, listOfParameters As List(Of List(Of NpgsqlParameter))) As Boolean
        Using connection As NpgsqlConnection = GetConnection()
            Dim transaction As NpgsqlTransaction = Nothing

            Try
                connection.Open()
                ' トランザクションの開始
                transaction = connection.BeginTransaction()

                For Each parameters As List(Of NpgsqlParameter) In listOfParameters
                    Using command As New NpgsqlCommand(query, connection, transaction)
                        command.Parameters.AddRange(parameters.ToArray())
                        command.ExecuteNonQuery()
                    End Using
                Next

                ' トランザクションのコミット
                transaction.Commit()
                Return True
            Catch ex As Exception
                ' エラーが発生した場合はロールバック
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
                Return False
            End Try
        End Using
    End Function

    ' トランザクションを開始してデータを作成・更新・削除する
    Public Function ExecuteNonQuery(query As String, parameters As List(Of NpgsqlParameter)) As Boolean
        Using connection As NpgsqlConnection = GetConnection()
            Dim transaction As NpgsqlTransaction = Nothing

            Try
                connection.Open()
                ' トランザクションの開始
                transaction = connection.BeginTransaction()

                Using command As New NpgsqlCommand(query, connection, transaction)
                    command.Parameters.AddRange(parameters.ToArray())
                    command.ExecuteNonQuery()
                End Using

                ' トランザクションのコミット
                transaction.Commit()
                Return True
            Catch ex As Exception
                ' エラーが発生した場合はロールバック
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
                Return False
            End Try
        End Using
    End Function

    ' トランザクションを開始してストアドプロシージャを実行する
    Public Function ExecuteStoredProcedure(spName As String, parameters As List(Of NpgsqlParameter)) As Boolean
        Using connection As NpgsqlConnection = GetConnection()
            Dim transaction As NpgsqlTransaction = Nothing

            Try
                connection.Open()
                ' トランザクションの開始
                transaction = connection.BeginTransaction()

                Using command As New NpgsqlCommand(spName, connection, transaction)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddRange(parameters.ToArray())
                    command.ExecuteNonQuery()
                End Using

                ' トランザクションのコミット
                transaction.Commit()
                Return True
            Catch ex As Exception
                ' エラーが発生した場合はロールバック
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
                Return False
            End Try
        End Using
    End Function

    ' データを取得する
    Public Function GetData(query As String, parameters As List(Of NpgsqlParameter)) As DataTable
        Dim dt As New DataTable()
        Using connection As NpgsqlConnection = GetConnection()
            Try
                connection.Open()
                Using command As New NpgsqlCommand(query, connection)
                    command.Parameters.AddRange(parameters.ToArray())
                    Using reader As NpgsqlDataReader = command.ExecuteReader()
                        dt.Load(reader)
                    End Using
                End Using
            Catch ex As Exception
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
            End Try
        End Using
        Return dt
    End Function

End Class
