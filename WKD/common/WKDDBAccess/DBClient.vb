Imports Npgsql

Public Class DBClient

    Private connectionString As String

    Public Sub New()
        Me.New(SettingManager.GetInstance.ConnectionString)
    End Sub

    ' �R���X�g���N�^�F�ڑ��������ݒ�
    Private Sub New(connectionString As String)
        Me.connectionString = connectionString
    End Sub

    ' �f�[�^�x�[�X�ڑ��̍쐬
    Private Function GetConnection() As NpgsqlConnection
        Return New NpgsqlConnection(connectionString)
    End Function

    ' �g�����U�N�V�������J�n���ĕ����s�̃f�[�^���쐬�E�X�V�E�폜����
    Public Function ExecuteNonQuery(query As String, listOfParameters As List(Of List(Of NpgsqlParameter))) As Boolean
        Using connection As NpgsqlConnection = GetConnection()
            Dim transaction As NpgsqlTransaction = Nothing

            Try
                connection.Open()
                ' �g�����U�N�V�����̊J�n
                transaction = connection.BeginTransaction()

                For Each parameters As List(Of NpgsqlParameter) In listOfParameters
                    Using command As New NpgsqlCommand(query, connection, transaction)
                        command.Parameters.AddRange(parameters.ToArray())
                        command.ExecuteNonQuery()
                    End Using
                Next

                ' �g�����U�N�V�����̃R�~�b�g
                transaction.Commit()
                Return True
            Catch ex As Exception
                ' �G���[�����������ꍇ�̓��[���o�b�N
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
                Return False
            End Try
        End Using
    End Function

    ' �g�����U�N�V�������J�n���ăf�[�^���쐬�E�X�V�E�폜����
    Public Function ExecuteNonQuery(query As String, parameters As List(Of NpgsqlParameter)) As Boolean
        Using connection As NpgsqlConnection = GetConnection()
            Dim transaction As NpgsqlTransaction = Nothing

            Try
                connection.Open()
                ' �g�����U�N�V�����̊J�n
                transaction = connection.BeginTransaction()

                Using command As New NpgsqlCommand(query, connection, transaction)
                    command.Parameters.AddRange(parameters.ToArray())
                    command.ExecuteNonQuery()
                End Using

                ' �g�����U�N�V�����̃R�~�b�g
                transaction.Commit()
                Return True
            Catch ex As Exception
                ' �G���[�����������ꍇ�̓��[���o�b�N
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
                Return False
            End Try
        End Using
    End Function

    ' �g�����U�N�V�������J�n���ăX�g�A�h�v���V�[�W�������s����
    Public Function ExecuteStoredProcedure(spName As String, parameters As List(Of NpgsqlParameter)) As Boolean
        Using connection As NpgsqlConnection = GetConnection()
            Dim transaction As NpgsqlTransaction = Nothing

            Try
                connection.Open()
                ' �g�����U�N�V�����̊J�n
                transaction = connection.BeginTransaction()

                Using command As New NpgsqlCommand(spName, connection, transaction)
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.AddRange(parameters.ToArray())
                    command.ExecuteNonQuery()
                End Using

                ' �g�����U�N�V�����̃R�~�b�g
                transaction.Commit()
                Return True
            Catch ex As Exception
                ' �G���[�����������ꍇ�̓��[���o�b�N
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
                Return False
            End Try
        End Using
    End Function

    ' �f�[�^���擾����
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
