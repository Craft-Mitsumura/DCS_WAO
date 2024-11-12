Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmFurikaeDataRuiseki
	Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private mForm As New FormClass

    Private Const mExeMsg As String = "�U�֋��z�\��\ �� ���ʒm���f�[�^��ݐς��܂��B" & vbCrLf & vbCrLf

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdExec_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExec.Click
        Dim sql As String
        Dim cnt As Integer
        Dim WhereSQL, msg As String
        Dim ix As Integer
        Dim ms As New MouseClass
        Call ms.Start()

        '//2004/05/17 �ݐό����̃��O�ǉ��̂��߂ɓ��t��ޔ�
        Dim RuisekiDate As String
        '//���X�g�Ń`�F�b�N���ꂽ�f�[�^�� IN ���...�B
        WhereSQL = " WHERE FASQNO IN("
        RuisekiDate = "("
        For Each entry As Object In lstFurikaeBi.CheckedItems
            cnt = cnt + 1
            WhereSQL = WhereSQL & CDate(entry).ToString("yyyyMMdd") & ","
            '//2004/05/17 �ݐό����̃��O�ǉ��̂��߂ɓ��t��ޔ�
            RuisekiDate = RuisekiDate & entry & ","
        Next
        WhereSQL = VB.Left(WhereSQL, Len(WhereSQL) - 1) & ")"
        '//2004/05/17 �ݐό����̃��O�ǉ��̂��߂ɓ��t��ޔ�
        RuisekiDate = VB.Left(RuisekiDate, Len(RuisekiDate) - 1) & ")"
        If cnt = 0 Then
            msg = "�ݐς��ׂ��f�[�^�͂���܂���ł����B"
            lblMessage.Text = mExeMsg & msg
            Call MsgBox(msg, MsgBoxStyle.Information, mCaption)
            Exit Sub
        End If

        On Error GoTo cmdExec_ClickError

        '//2003/02/03 �X�V��ԃt���O���`�F�b�N���Čx�� �ǉ�:0=DB�쐬,1=�\��쐬,2=�\��捞,3=�����쐬
        Dim dyn As DataTable
        sql = "SELECT FASQNO FROM tfFurikaeYoteiData"
        sql = sql & WhereSQL
        sql = sql & " AND FAUPFG < '" & MainModule.eKouFuriKubun.SeikyuText & "'"
        sql = sql & " AND COALESCE(FAKYFG, '0') = '0' "
        dyn = gdDBS.ExecuteDataForBinding(sql)
        If Not IsNothing(dyn) Then
            msg = "�����f�[�^�̍쐬����Ă��Ȃ��f�[�^�����݂��܂�." & vbCrLf & "�ݐϏ����𑱍s���܂����H"
            lblMessage.Text = mExeMsg & msg
            If MsgBoxResult.Ok <> MsgBox(msg, MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, mCaption) Then
                Exit Sub
            End If
        End If

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand
            cmd.Connection = connection
            connection.Open()

            transaction = connection.BeginTransaction()

                '//2004/06/03 �V�K�����������t��ی�҃}�X�^(CANWDT)�ɐݒ�F���z�u�O�v�͐V�K�Ƃ��Ȃ�
                sql = "UPDATE tcHogoshaMaster SET "
            sql = sql & " CANWDT = CURRENT_TIMESTAMP "
            sql = sql & " WHERE (CAITKB,CAKYCD,CAHGCD) IN("
                sql = sql & "       SELECT FAITKB,FAKYCD,FAHGCD "
                sql = sql & "       FROM tfFurikaeYoteiData "
                '//2007/04/19 WAO�͋��z���͖����Ȃ̂ŏ������͂���
                'sql = sql & "       WHERE (COALESCE(faskgk,0) > 0 OR COALESCE(fahkgk,0) > 0) "
                sql = sql & "     )"
            sql = sql & "  AND CANWDT IS NULL"
            cmd.CommandText = sql
            cnt = cmd.ExecuteNonQuery()

            '//�ݐ�
            sql = "INSERT INTO tfFurikaeYoteiTran "
                sql = sql & " SELECT * FROM tfFurikaeYoteiData"
            sql = sql & WhereSQL
            cmd.CommandText = sql
            cnt = cmd.ExecuteNonQuery()
            '//�ݐς��������폜
            sql = " DELETE FROM tfFurikaeYoteiData"
            sql = sql & WhereSQL
                gdDBS.ExecuteNonQuery(sql)
                '//2003/02/04 ����U�����E��������U�֓� ���X�V����
                Dim KouFuriDay, FurikomiDay As Short
                Dim KouFuriDate, FurikomiDate As Date

                '//�U�����F�_��҈���
                FurikomiDay = gdDBS.SystemUpdate("AAFKDT")
                '//�����̐U�������Z�o�����
                FurikomiDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXFK"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXFK"), 5, 2)) + 1, FurikomiDay)
                '//����U���� �ݒ�
                gdDBS.SystemUpdate("AANXFK") = CDate(NextDay(FurikomiDate)).ToString("yyyyMMdd")

                '//�����U�֓��F�ی�҈���
                KouFuriDay = gdDBS.SystemUpdate("AAKZDT")
                '//�����̌����U�֓����Z�o�����
                '//2010/02/23 �Q�O�P�O�N�Q���� 2/27,28 ���c�Ɠ��łȂ��ׁA�U�֓��� 3/1 �ɂȂ��Ă��܂��Ă���̂łP�������ݒ肵�Ă��܂��o�O�Ή�
                Dim wDay, addMonth As Short
                wDay = CShort(VB.Right(gdDBS.SystemUpdate("AANXKZ"), 2))
                If KouFuriDay <= wDay Then
                    addMonth = 1
                End If
                KouFuriDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXKZ"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXKZ"), 5, 2)) + addMonth, KouFuriDay)
                KouFuriDate = NextDay(KouFuriDate)
                '//��������U�֓� �ݒ�
                gdDBS.SystemUpdate("AANXKZ") = CDate(NextDay(KouFuriDate)).ToString("yyyyMMdd")

                '//2004/04/12 �����U�֓����r���Ĉȍ~�̓��� �Đݒ�
                If FurikomiDate < KouFuriDate Then '//�N����
                    If FurikomiDay < KouFuriDay Then '//�@�@��
                        FurikomiDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXKZ"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXKZ"), 5, 2)) + 1, FurikomiDay)
                    Else
                        FurikomiDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXKZ"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXKZ"), 5, 2)) + 0, FurikomiDay)
                    End If
                    '//����U���� �Đݒ�
                    gdDBS.SystemUpdate("AANXFK") = CDate(NextDay(FurikomiDate)).ToString("yyyyMMdd")
                End If
                '//2004/05/17 �ݐό����̃��O�ǉ�
                Call gdDBS.AutoLogOut(mCaption, "�����U�ւc�a�ݐ� = " & cnt & " �� �Ώ� = " & RuisekiDate)

                lblMessage.Text = mExeMsg & cnt & " ���̃f�[�^���ݐς���܂����B"
                '//���s�X�V�t���O�ݒ�
                gdDBS.SystemUpdate("AAUPDE") = 1
            transaction.Commit()
            Exit Sub
                End Using
cmdExec_ClickError:
        If Not transaction.IsCompleted Then
            transaction.Rollback()
        End If
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
        '// gdDBS.ErrorCheck() �̏�Ɉړ�
        '//    Call gdDBS.Database.Rollback
    End Sub

    Private Function NextDay(ByRef vStartDate As Object) As Object
        Dim ix As Short
        Dim sql As String
        '//�Q�O�A�x�͖������낤!!!
        For ix = 0 To 20
            NextDay = DateSerial(Year(vStartDate), Month(vStartDate), VB.Day(vStartDate) + ix)
            '//1=���j��,2=���j��...,7=�y�j�� �Ȃ̂łQ�ȏ�͌��j��������j���̂͂�
            If (Weekday(NextDay, FirstDayOfWeek.Sunday) Mod 7) >= 2 Then
                sql = "SELECT EADATE FROM teHolidayMaster "
                sql = sql & " WHERE EADATE = " & CDate(NextDay).ToString("yyyyMMdd")
                If IsNothing(gdDBS.ExecuteDataForBinding(sql)) Then
                    Exit Function
                End If
            End If
        Next ix
        '//�I�[�o�[�����̂�...�B
        NextDay = vStartDate
    End Function

    Private Sub frmFurikaeDataRuiseki_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim reg As New RegistryClass
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        lblMessage.Text = mExeMsg

        '//ListBox �Ɍ��݂̗\���S�ă��X�g�A�b�v����B
        Dim sql As String
        Dim dyn As Npgsql.NpgsqlDataReader
        sql = "SELECT FASQNO,TO_CHAR(TO_DATE(FASQNO::VARCHAR,'YYYYMMDD'),'YYYY/MM/DD') AS FaDate"
        sql = sql & " FROM tfFurikaeYoteiData"
        sql = sql & " GROUP BY FASQNO"
        sql = sql & " ORDER BY FASQNO"
        dyn = gdDBS.ExecuteDatareader(sql)
        Call lstFurikaeBi.Items.Clear()
        Do Until Not dyn.Read()
            Call lstFurikaeBi.Items.Add(dyn.GetValue(dyn.GetOrdinal("FaDate")))
            '        lstFurikaeBi.Selected(lstFurikaeBi.NewIndex) = True
        Loop
        Call dyn.Close()
#If 0 Then
    		'''//�`�F�b�N�{�b�N�X�e�X�g�̂��߂ɍ쐬
    		Dim i As Integer
    		For i = 1 To 10
    		Call lstFurikaeBi.AddItem(Format(Now() + i, "yyyy/mm/dd"))
    		lstFurikaeBi.Selected(lstFurikaeBi.NewIndex) = True
    		Next i
#End If
        cmdExec.Enabled = lstFurikaeBi.Items.Count > 0
    End Sub

    Private Sub frmFurikaeDataRuiseki_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmFurikaeDataRuiseki_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mForm = Nothing
        Me.Dispose()
        Call gdForm.Show()
    End Sub

    Private Sub frmFurikaeDataRuiseki_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub lstFurikaeBi_ItemCheck(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ItemCheckEventArgs) Handles lstFurikaeBi.ItemCheck
        '//�`�F�b�N�{�b�N�X�͏�Ƀ`�F�b�N��ԂɈێ�����
        '    lstFurikaeBi.Selected(Item) = True
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub
End Class