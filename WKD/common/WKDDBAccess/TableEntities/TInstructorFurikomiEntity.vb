Public Class TInstructorFurikomiEntity
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
	''' 顧客番号（インストラクターＮｏ）
	''' </summary>
	Public Property instno As String

	''' <summary>
	''' 振込金額（税引前）
	''' </summary>
	Public Property fkinzem As Decimal?

	''' <summary>
	''' 銀行コード
	''' </summary>
	Public Property bankcd As String

	''' <summary>
	''' 支店コード
	''' </summary>
	Public Property sitencd As String

	''' <summary>
	''' 預金種目
	''' </summary>
	Public Property syumok As String

	''' <summary>
	''' 口座番号
	''' </summary>
	Public Property kozono As String

	''' <summary>
	''' 預金者名義（カナ）
	''' </summary>
	Public Property meigkn As String

	''' <summary>
	''' 振込金額（税引後）
	''' </summary>
	Public Property fkinzeg As Decimal?

	''' <summary>
	''' 源泉徴収税額
	''' </summary>
	Public Property zeigak As Decimal?

	''' <summary>
	''' 振込年月
	''' </summary>
	Public Property frinengetu As String

	''' <summary>
	''' 郵便番号
	''' </summary>
	Public Property yubin As String

	''' <summary>
	''' 氏名（漢字）
	''' </summary>
	Public Property namekj As String

	''' <summary>
	''' 氏名（カナ）
	''' </summary>
	Public Property namekn As String

	''' <summary>
	''' 生年
	''' </summary>
	Public Property seiyyyy As String

	''' <summary>
	''' 生月
	''' </summary>
	Public Property seimm As String

	''' <summary>
	''' 生日
	''' </summary>
	Public Property seidd As String

	''' <summary>
	''' 入社年
	''' </summary>
	Public Property nyunen As String

	''' <summary>
	''' 入社月
	''' </summary>
	Public Property nyutuki As String

	''' <summary>
	''' 入社日
	''' </summary>
	Public Property nyuhi As String

	''' <summary>
	''' 退職年
	''' </summary>
	Public Property tainen As String

	''' <summary>
	''' 退職月
	''' </summary>
	Public Property taituki As String

	''' <summary>
	''' 退職年
	''' </summary>
	Public Property taihi As String

	''' <summary>
	''' 振込手数料
	''' </summary>
	Public Property fritesu As Decimal?

	''' <summary>
	''' 年調資料出力フラグ
	''' </summary>
	Public Property nencho_flg As String

	''' <summary>
	''' 住所１（漢字）
	''' </summary>
	Public Property jusyo1 As String

	''' <summary>
	''' 住所２（漢字）
	''' </summary>
	Public Property jusyo2 As String

End Class
