
Imports System.ComponentModel

<ProvideProperty("Index", GetType(GrapeCity.Win.Editors.GcDate))> Public Class AximDateArray
    Inherits Microsoft.VisualBasic.Compatibility.VB6.BaseOcxArray
    Implements IExtenderProvider

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal Container As IContainer)
        MyBase.New(Container)
    End Sub

    Public Shadows Event [Change](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [EditModeChange](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [InvalidInput](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [ClickEvent](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [DblClick](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [MouseEnterEvent](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [MouseExit](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [SpinUp](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [SpinDown](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [DropDownOpen](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [Leave](ByVal sender As System.Object, ByVal e As System.EventArgs)

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Function CanExtend(ByVal target As Object) As Boolean Implements IExtenderProvider.CanExtend
        If TypeOf target Is GrapeCity.Win.Editors.GcDate Then
            Return BaseCanExtend(target)
        End If
    End Function

    Public Function GetIndex(ByVal o As GrapeCity.Win.Editors.GcDate) As Short
        Return BaseGetIndex(o)
    End Function

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Sub SetIndex(ByVal o As GrapeCity.Win.Editors.GcDate, ByVal Index As Short)
        BaseSetIndex(o, Index)
    End Sub

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Function ShouldSerializeIndex(ByVal o As GrapeCity.Win.Editors.GcDate) As Boolean
        Return BaseShouldSerializeIndex(o)
    End Function

    <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Sub ResetIndex(ByVal o As GrapeCity.Win.Editors.GcDate)
        BaseResetIndex(o)
    End Sub

    Default Public ReadOnly Property Item(ByVal Index As Short) As GrapeCity.Win.Editors.GcDate
        Get
            Item = CType(BaseGetItem(Index), GrapeCity.Win.Editors.GcDate)
        End Get
    End Property

    Protected Overrides Function GetControlInstanceType() As System.Type
        Return GetType(GrapeCity.Win.Editors.GcDate)
    End Function

    Protected Overrides Sub HookUpControlEvents(ByVal o As Object)
        Dim ctl As GrapeCity.Win.Editors.GcDate = CType(o, GrapeCity.Win.Editors.GcDate)
        'MyBase.HookUpControlEvents(o)
        If Not ChangeEvent Is Nothing Then
            AddHandler ctl.TextChanged, New System.EventHandler(AddressOf HandleChange)
        End If
        If Not EditModeChangeEvent Is Nothing Then
            AddHandler ctl.EnabledChanged, New System.EventHandler(AddressOf HandleEditModeChange)
        End If
        If Not InvalidInputEvent Is Nothing Then
            AddHandler ctl.InvalidInput, New System.EventHandler(AddressOf HandleInvalidInput)
        End If
        If Not ClickEventEvent Is Nothing Then
            AddHandler ctl.Click, New System.EventHandler(AddressOf HandleClickEvent)
        End If
        If Not DblClickEvent Is Nothing Then
            AddHandler ctl.DoubleClick, New System.EventHandler(AddressOf HandleDblClick)
        End If
        If Not MouseEnterEventEvent Is Nothing Then
            AddHandler ctl.MouseEnter, New System.EventHandler(AddressOf HandleMouseEnterEvent)
        End If
        If Not MouseExitEvent Is Nothing Then
            AddHandler ctl.MouseLeave, New System.EventHandler(AddressOf HandleMouseExit)
        End If
        If Not SpinUpEvent Is Nothing Then
            AddHandler ctl.SpinUp, New System.EventHandler(AddressOf HandleSpinUp)
        End If
        If Not SpinDownEvent Is Nothing Then
            AddHandler ctl.SpinDown, New System.EventHandler(AddressOf HandleSpinDown)
        End If
        If Not DropDownOpenEvent Is Nothing Then
            AddHandler ctl.DropDownOpened, New System.EventHandler(AddressOf HandleDropDownOpen)
        End If
        If Not LeaveEvent Is Nothing Then
            AddHandler ctl.Leave, New System.EventHandler(AddressOf HandleLeave)
        End If
    End Sub

    Private Function HandleChange(ByVal sender As System.Object, ByVal e As System.EventArgs) As Integer
        RaiseEvent [Change](sender, e)
    End Function

    Private Function HandleEditModeChange(ByVal sender As System.Object, ByVal e As System.EventArgs) As Integer
        RaiseEvent [EditModeChange](sender, e)
    End Function

    Private Function HandleInvalidInput(ByVal sender As System.Object, ByVal e As System.EventArgs) As Integer
        RaiseEvent [InvalidInput](sender, e)
    End Function

    Private Sub HandleClickEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [ClickEvent](sender, e)
    End Sub

    Private Sub HandleDblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [DblClick](sender, e)
    End Sub

    Private Sub HandleMouseEnterEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [MouseEnterEvent](sender, e)
    End Sub

    Private Sub HandleMouseExit(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [MouseExit](sender, e)
    End Sub

    Private Sub HandleSpinUp(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [SpinUp](sender, e)
    End Sub

    Private Sub HandleSpinDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [SpinDown](sender, e)
    End Sub

    Private Sub HandleDropDownOpen(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [DropDownOpen](sender, e)
    End Sub

    Private Sub HandleLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent [Leave](sender, e)
    End Sub

End Class

