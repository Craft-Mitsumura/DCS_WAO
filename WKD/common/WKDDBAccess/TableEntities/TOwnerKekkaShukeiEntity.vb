Public Class TOwnerKekkaShukeiEntity
	Inherits TableEntityBase
	''' <summary>
	''' データ年月
	''' </summary>
	Public Property dtnengetu As String

	''' <summary>
	''' オーナーＮｏ
	''' </summary>
	Public Property ownerno As String

	''' <summary>
	''' ｺﾝﾋﾞﾆ未納件数
	''' </summary>
	Public Property mnokensu As Decimal?

	''' <summary>
	''' ｺﾝﾋﾞﾆ未納金額
	''' </summary>
	Public Property mnokingk As Decimal?

End Class
