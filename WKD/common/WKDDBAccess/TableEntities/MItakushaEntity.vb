Public Class MItakushaEntity
	Inherits TableEntityBase
	''' <summary>
	''' 委託者コード
	''' </summary>
	Public Property itakuno As Decimal?

	''' <summary>
	''' 委託者名
	''' </summary>
	Public Property itaknm As String

	''' <summary>
	''' 取引銀行コード
	''' </summary>
	Public Property bankcd As Decimal?

	''' <summary>
	''' 取引銀行名
	''' </summary>
	Public Property banknm As String

	''' <summary>
	''' 取引支店コード
	''' </summary>
	Public Property sitencd As Decimal?

	''' <summary>
	''' 取引支店名
	''' </summary>
	Public Property sitennm As String

	''' <summary>
	''' 預金種目
	''' </summary>
	Public Property syumoku As Decimal?

	''' <summary>
	''' 口座番号
	''' </summary>
	Public Property kouzano As Decimal?

	''' <summary>
	''' 郵便番号
	''' </summary>
	Public Property s_yubin As String

	''' <summary>
	''' 住所１（漢字）
	''' </summary>
	Public Property s_jusyo1 As String

	''' <summary>
	''' 住所２（漢字）
	''' </summary>
	Public Property s_jusyo2 As String

	''' <summary>
	''' 差出人名（漢字）
	''' </summary>
	Public Property s_sasinm As String

End Class
