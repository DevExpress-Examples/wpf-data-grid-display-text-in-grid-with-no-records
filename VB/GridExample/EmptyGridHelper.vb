Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace GridExample

    Public Class EmptyGridHelper

        Public Shared ReadOnly HasVisibleRowsProperty As DependencyProperty = DependencyProperty.RegisterAttached("HasVisibleRows", GetType(Boolean), GetType(EmptyGridHelper), New PropertyMetadata(False))

        Public Shared ReadOnly EmptyGridTemplateProperty As DependencyProperty = DependencyProperty.RegisterAttached("EmptyGridTemplate", GetType(DataTemplate), GetType(EmptyGridHelper), New UIPropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf EmptyGridTemplateChanged)))

        Private Shared Sub EmptyGridTemplateChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim view As TableView = TryCast(sender, TableView)
            Dim emptyGridTemplate As DataTemplate = CType(e.NewValue, DataTemplate)
            If view Is Nothing OrElse emptyGridTemplate Is Nothing Then Return
            AddHandler view.Grid.PropertyChanged, New System.ComponentModel.PropertyChangedEventHandler(AddressOf Grid_PropertyChanged)
            AddHandler view.Grid.ItemsSourceChanged, New ItemsSourceChangedEventHandler(AddressOf Grid_ItemsSourceChanged)
            AddHandler view.Loaded, New RoutedEventHandler(AddressOf view_Loaded)
        End Sub

        Private Shared Sub view_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim view As TableView = TryCast(sender, TableView)
            Dim emptyGridTemplate As DataTemplate = CType(view.GetValue(EmptyGridTemplateProperty), DataTemplate)
            If view Is Nothing OrElse emptyGridTemplate Is Nothing Then Return
            AddEptyGridContentPresenter(view, emptyGridTemplate)
        End Sub

        Private Shared Sub AddEptyGridContentPresenter(ByVal view As TableView, ByVal emptyGridTemplate As DataTemplate)
            Dim emptyGridContentresenter As ContentPresenter = New ContentPresenter()
            emptyGridContentresenter.ContentTemplate = emptyGridTemplate
            emptyGridContentresenter.HorizontalAlignment = HorizontalAlignment.Stretch
            emptyGridContentresenter.VerticalAlignment = VerticalAlignment.Stretch
            Dim emptyGridContentVisibilityBinding As Binding = New Binding("(local:EmptyGridHelper.HasVisibleRows)") With {.Source = view}
            emptyGridContentresenter.SetBinding(ContentPresenter.VisibilityProperty, emptyGridContentVisibilityBinding)
            Dim container As Grid = CType(LayoutHelper.FindElementByName(view, "rowPresenterGrid"), Grid)
            container.Children.Add(emptyGridContentresenter)
        End Sub

        Private Shared Sub Grid_ItemsSourceChanged(ByVal sender As Object, ByVal e As ItemsSourceChangedEventArgs)
            If e.NewDataSource Is Nothing Then
                CType(sender, GridControl).View.SetValue(HasVisibleRowsProperty, False)
            End If
        End Sub

        Private Shared Sub Grid_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
            If Equals(e.PropertyName, "VisibleRowCount") Then
                Dim grid As GridControl = CType(sender, GridControl)
                grid.View.SetValue(HasVisibleRowsProperty, grid.VisibleRowCount <> 0)
            End If
        End Sub

'#Region "CLRs"
        Public Shared Function GetHasVisibleRows(ByVal obj As DependencyObject) As Boolean
            Return CBool(obj.GetValue(HasVisibleRowsProperty))
        End Function

        Public Shared Sub SetHasVisibleRows(ByVal obj As DependencyObject, ByVal value As Boolean)
            obj.SetValue(HasVisibleRowsProperty, value)
        End Sub

        Public Shared Function GetEmptyGridTemplate(ByVal obj As DependencyObject) As DataTemplate
            Return CType(obj.GetValue(EmptyGridTemplateProperty), DataTemplate)
        End Function

        Public Shared Sub SetEmptyGridTemplate(ByVal obj As DependencyObject, ByVal value As DataTemplate)
            obj.SetValue(EmptyGridTemplateProperty, value)
        End Sub
'#End Region
    End Class
End Namespace
