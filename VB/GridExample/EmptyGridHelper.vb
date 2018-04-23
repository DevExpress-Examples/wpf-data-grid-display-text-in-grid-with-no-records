Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core.Native
Imports System.Windows.Controls
Imports System.Windows.Shapes
Imports System.Windows.Data
Imports DevExpress.Xpf.Core

Namespace GridExample
	Public Class EmptyGridHelper

		Public Shared ReadOnly HasVisibleRowsProperty As DependencyProperty = DependencyProperty.RegisterAttached("HasVisibleRows", GetType(Boolean), GetType(EmptyGridHelper), New PropertyMetadata(False))

		Public Shared ReadOnly EmptyGridTemplateProperty As DependencyProperty = DependencyProperty.RegisterAttached("EmptyGridTemplate", GetType(DataTemplate), GetType(EmptyGridHelper), New UIPropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf EmptyGridTemplateChanged)))

		Private Shared Sub EmptyGridTemplateChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim view As TableView = TryCast(sender, TableView)
			Dim emptyGridTemplate As DataTemplate = CType(e.NewValue, DataTemplate)
			If view Is Nothing OrElse emptyGridTemplate Is Nothing Then
				Return
			End If

			AddHandler view.Grid.PropertyChanged, AddressOf Grid_PropertyChanged
			AddHandler view.Grid.ItemsSourceChanged, AddressOf Grid_ItemsSourceChanged

			AddHandler view.Loaded, AddressOf view_Loaded
		End Sub

		Private Shared Sub view_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim view As TableView = TryCast(sender, TableView)

			Dim emptyGridTemplate As DataTemplate = CType(view.GetValue(EmptyGridHelper.EmptyGridTemplateProperty), DataTemplate)
			If view Is Nothing OrElse emptyGridTemplate Is Nothing Then
				Return
			End If

			AddEptyGridContentPresenter(view, emptyGridTemplate)
		End Sub

		Private Shared Sub AddEptyGridContentPresenter(ByVal view As TableView, ByVal emptyGridTemplate As DataTemplate)
			Dim emptyGridContentresenter As New ContentPresenter()
			emptyGridContentresenter.ContentTemplate = emptyGridTemplate

			emptyGridContentresenter.HorizontalAlignment = HorizontalAlignment.Stretch
			emptyGridContentresenter.VerticalAlignment = VerticalAlignment.Stretch

            Dim emptyGridContentVisibilityBinding As New Binding("(local:EmptyGridHelper.HasVisibleRows)") With {.Source = view, .Converter = New BoolToVisibilityInverseConverter()}
			emptyGridContentresenter.SetBinding(ContentPresenter.VisibilityProperty, emptyGridContentVisibilityBinding)

			Dim container As Grid = CType(LayoutHelper.FindElementByName(view, "rowPresenterGrid"), Grid)

			container.Children.Add(emptyGridContentresenter)
		End Sub


		Private Shared Sub Grid_ItemsSourceChanged(ByVal sender As Object, ByVal e As ItemsSourceChangedEventArgs)
			If e.NewDataSource Is Nothing Then
				CType(sender, GridControl).View.SetValue(EmptyGridHelper.HasVisibleRowsProperty, False)
			End If
		End Sub

		Private Shared Sub Grid_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
			If e.PropertyName = "VisibleRowCount" Then
				Dim grid As GridControl = CType(sender, GridControl)
				grid.View.SetValue(EmptyGridHelper.HasVisibleRowsProperty, grid.VisibleRowCount <> 0)
			End If
		End Sub

		#Region "CLRs"
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
		#End Region  

	End Class
End Namespace
