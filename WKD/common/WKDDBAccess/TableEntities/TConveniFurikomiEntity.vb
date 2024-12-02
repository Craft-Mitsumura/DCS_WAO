Public Class TConveniFurikomiEntity
	Inherits TableEntityBase
	''' <summary>
	''' データ年月
	''' </summary>
	Public Property dtnengetu As String

	''' <summary>
	''' 顧客番号（委託者Ｎｏ）
	''' </summary>
	Public Property itakuno As String

	''' <summary>
	''' 顧客番号（オーナーＮｏ）
	''' </summary>
	Public Property ownerno As String

	''' <summary>
	''' 顧客番号（生徒Ｎｏ）
	''' </summary>
	Public Property seitono As String

	''' <summary>
	''' 顧客番号内ＳＥＱ番号
	''' </summary>
	Public Property kseqno As String

	''' <summary>
	''' 処理区分
	''' </summary>
	Public Property syokbn As String

	''' <summary>
	''' 金額
	''' </summary>
	Public Property skingaku As Decimal?

	''' <summary>
	''' 入会金
	''' </summary>
	Public Property nyukaikin As Decimal?

	''' <summary>
	''' 授業料
	''' </summary>
	Public Property jugyoryo As Decimal?

	''' <summary>
	''' 施設関連諸費
	''' </summary>
	Public Property skanhi As Decimal?

	''' <summary>
	''' テキスト費
	''' </summary>
	Public Property texthi As Decimal?

	''' <summary>
	''' テスト費
	''' </summary>
	Public Property testhi As Decimal?

	''' <summary>
	''' 郵便番号
	''' </summary>
	Public Property yubin As String

	''' <summary>
	''' 住所１－１（漢字）
	''' </summary>
	Public Property jusyo1_1 As String

	''' <summary>
	''' 住所１－２（漢字）
	''' </summary>
	Public Property jusyo1_2 As String

	''' <summary>
	''' 住所２－１（漢字）
	''' </summary>
	Public Property jusyo2_1 As String

	''' <summary>
	''' 住所２－２（漢字）
	''' </summary>
	Public Property jusyo2_2 As String

	''' <summary>
	''' 保護者名（漢字）
	''' </summary>
	Public Property hogosnm As String

	''' <summary>
	''' 生徒名（漢字）
	''' </summary>
	Public Property seitonm As String

	''' <summary>
	''' 振替銀行コード
	''' </summary>
	Public Property fkbankcd As String

	''' <summary>
	''' 振替支店コード
	''' </summary>
	Public Property fksitencd As String

	''' <summary>
	''' 振替種目
	''' </summary>
	Public Property fksyumoku As String

	''' <summary>
	''' 振替口座番号
	''' </summary>
	Public Property fkkouzano As String

	''' <summary>
	''' 振替開始年月
	''' </summary>
	Public Property kaisiym As String

	''' <summary>
	''' 口座名義人名（カナ）
	''' </summary>
	Public Property kouzanm As String

	''' <summary>
	''' 差出人郵便番号
	''' </summary>
	Public Property s_yubin As String

	''' <summary>
	''' 差出人住所１（漢字）
	''' </summary>
	Public Property s_jusyo1 As String

	''' <summary>
	''' 差出人住所２（漢字）
	''' </summary>
	Public Property s_jusyo2 As String

	''' <summary>
	''' 差出人名（漢字）
	''' </summary>
	Public Property s_sasinm As String

End Class
