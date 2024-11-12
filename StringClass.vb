Option Strict Off
Option Explicit On
Imports System.Text.RegularExpressions

Friend Class StringClass

    '   Function LenMbcs (ByVal str as String)
    '      LenMbcs = LenB(StrConv(str, vbFromUnicode))
    '   End Function
    '
    '   Dim MyString, MyLen
    '   MyString = "�`�ac"
    '   "�`" �� "�a" �͑S�p������ "c" �͔��p�����ł��B
    '   MyLen = Len(MyString)
    '   ' �������Ƃ��� 3 ���Ԃ���܂��B
    '   MyLen = LenB(MyString)      Windows �̏ꍇ�� 6 ���o�C�g���Ƃ��ĕԂ���܂��B
    '   MyLen = LenMbcs(MyString)   Windows �̏ꍇ�� 5 ���Ԃ���܂��B

    Public ReadOnly Property CheckLength(ByVal vStr As String, Optional ByVal vCode As Object = VbStrConv.Wide) As Boolean
        Get
            If vCode = VbStrConv.Wide Then
                'CheckLength = Len(StrConv(vStr, VbStrConv.None)) = Len(StrConv(vStr, VbStrConv.Wide))
                If vStr.Trim() = "" Then
                    CheckLength = True
                Else
                    Dim charStr() As Char
                    charStr = vStr.ToCharArray()
                    For Each s As String In charStr

                        If System.Text.Encoding.Default.GetByteCount(s) = 1 Then
                            CheckLength = False
                            Exit For
                        End If
                        CheckLength = True
                    Next
                End If
            Else
                CheckLength = Hankaku(vStr) 'LenB(vStr) = LenB(StrConv(vStr, vCode))
            End If
        End Get
    End Property

    'Private Property Get Zenkaku(vStr As String) As Boolean
    '    Dim idx As Integer
    '    For idx = 1 To Len(vStr)
    '
    '    Next idx
    'End Property

    Private ReadOnly Property Hankaku(ByVal vStr As String) As Boolean
        Get
            Dim idx As Short
            For idx = 1 To Len(vStr)
                If Asc(Mid(vStr, idx, 1)) < 0 And &HFF < Asc(Mid(vStr, idx, 1)) Then
                    Exit Property
                End If
            Next idx
            Hankaku = True
        End Get
    End Property

    Public ReadOnly Property FixedFormat(ByVal vNum As Object, ByVal vLength As Short) As String
        Get
            On Error Resume Next
            'UPGRADE_WARNING: Couldn't resolve default property of object vNum. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            FixedFormat = Right(Space(vLength) & VB6.Format(vNum, "#,0"), vLength)
        End Get
    End Property
End Class