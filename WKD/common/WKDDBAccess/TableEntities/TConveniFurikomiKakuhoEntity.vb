Public Class TConveniFurikomiKakuhoEntity
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
	''' データ種別
	''' </summary>
	Public Property dtsybt As String

	''' <summary>
	''' CVS店舗収納年月日
	''' </summary>
	Public Property syndate As String

	''' <summary>
	''' CVS店舗収納時分
	''' </summary>
	Public Property syntime As String

	''' <summary>
	''' 識別子
	''' </summary>
	Public Property skbt As String

	''' <summary>
	''' 国コード
	''' </summary>
	Public Property kuni As String

	''' <summary>
	''' ＭＵＦ企業コード
	''' </summary>
	Public Property mufcd As String

	''' <summary>
	''' 収納企業コード
	''' </summary>
	Public Property kgycd As String

	''' <summary>
	''' 収納企業名（カナ）
	''' </summary>
	Public Property kgynmkn As String

	''' <summary>
	''' 再発行区分
	''' </summary>
	Public Property shkkkbn As String

	''' <summary>
	''' 支払期限
	''' </summary>
	Public Property shrikgn As String

	''' <summary>
	''' 印紙フラグ
	''' </summary>
	Public Property insiflg As String

	''' <summary>
	''' 金額
	''' </summary>
	Public Property kingk As Decimal?

	''' <summary>
	''' 全体ﾁｪｯｸﾃﾞｨｼﾞｯﾄ
	''' </summary>
	Public Property cd As String

	''' <summary>
	''' CVS受付店舗コード
	''' </summary>
	Public Property uktncd As String

	''' <summary>
	''' データ取得年月日
	''' </summary>
	Public Property stkdate As String

	''' <summary>
	''' 振込予定年月日
	''' </summary>
	Public Property frytdate As String

	''' <summary>
	''' 経理処理年月日
	''' </summary>
	Public Property krsydate As String

	''' <summary>
	''' CVSコード
	''' </summary>
	Public Property cvscd As String

End Class
