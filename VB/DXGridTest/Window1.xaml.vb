Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Wpf.Utils.Themes
Imports DevExpress.Wpf.Grid
Imports DevExpress.Wpf.Core

Namespace DXGridTest
	''' <summary>
	''' Interaction logic for Window1.xaml
	''' </summary>
	Public Partial Class Window1
		Inherits Window
		Public Shared ReadOnly IsGridEmptyProperty As DependencyProperty
		Shared Sub New()
			IsGridEmptyProperty = DependencyProperty.RegisterAttached("IsGridEmpty", GetType(Boolean), GetType(Window1), New FrameworkPropertyMetadata(False, FrameworkPropertyMetadataOptions.Inherits))
		End Sub
		Public Shared Function GetIsGridEmpty(ByVal dependencyObject As DependencyObject) As Boolean
			Return CBool(dependencyObject.GetValue(IsGridEmptyProperty))
		End Function
		Public Shared Sub SetIsGridEmpty(ByVal dependencyObject As DependencyObject, ByVal value As Boolean)
			dependencyObject.SetValue(IsGridEmptyProperty, value)
		End Sub
		Public Sub New()
			InitializeComponent()


			Dim list As List(Of TestData) = New List(Of TestData)()
			For i As Integer = 0 To 99
                list.Add(New TestData() With {.Text = "Row" & i, .Number = i, .Group = i Mod 5})
			Next i
			grid.DataSource = list

			AddHandler grid.LayoutUpdated, AddressOf grid_LayoutUpdated
		End Sub

		Private Sub grid_LayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
			Window1.SetIsGridEmpty(grid, grid.VisibleRowCount = 0)
		End Sub

		Private Sub CheckBox_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			grid.SetValue(ThemeManager.ThemeNameProperty, "Azure")
		End Sub

		Private Sub CheckBox_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			grid.ClearValue(ThemeManager.ThemeNameProperty)
		End Sub

		Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Window1.SetIsGridEmpty(grid, (Not Window1.GetIsGridEmpty(grid)))
		End Sub
	End Class

	Public Class TestData
        Private _text As String
        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property

        Private _number As Integer
        Public Property Number() As Integer
            Get
                Return _number
            End Get
            Set(ByVal value As Integer)
                _number = value
            End Set
        End Property

        Private _group As Integer
        Public Property Group() As Integer
            Get
                Return _group
            End Get
            Set(ByVal value As Integer)
                _group = value
            End Set
        End Property

    End Class
End Namespace
