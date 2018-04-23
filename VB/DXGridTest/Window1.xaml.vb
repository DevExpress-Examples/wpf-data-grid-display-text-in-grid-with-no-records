Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Collections.Generic


Namespace DXGridTest
	Partial Public Class Window1
		Inherits Window

		Public Shared ReadOnly IsGridEmptyProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsGridEmpty", GetType(Boolean), GetType(Window1), New FrameworkPropertyMetadata(False, FrameworkPropertyMetadataOptions.Inherits))

		Public Shared Function GetIsGridEmpty(ByVal dependencyObject As DependencyObject) As Boolean
			Return CBool(dependencyObject.GetValue(IsGridEmptyProperty))
		End Function
		Public Shared Sub SetIsGridEmpty(ByVal dependencyObject As DependencyObject, ByVal value As Boolean)
			dependencyObject.SetValue(IsGridEmptyProperty, value)
		End Sub

		Private dataSource As List(Of TestData)

		Public Sub New()
			InitializeComponent()

			AddHandler Loaded, AddressOf Window1_Loaded
		End Sub

		Private Sub Window1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

			dataSource = New List(Of TestData)()
			For i As Integer = 0 To 9
				dataSource.Add(New TestData() With {.Text = "Row" & i, .Number = i, .Group = i Mod 5})
			Next i
			grid.DataSource = dataSource

			AddHandler grid.LayoutUpdated, AddressOf grid_LayoutUpdated
		End Sub

		Private Sub grid_LayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
			Window1.SetIsGridEmpty(grid, grid.VisibleRowCount = 0)
		End Sub

		Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			grid.DataSource = If(grid.DataSource Is Nothing, dataSource, Nothing)
		End Sub
	End Class

	Public Class TestData
		Private privateText As String
		Public Property Text() As String
			Get
				Return privateText
			End Get
			Set(ByVal value As String)
				privateText = value
			End Set
		End Property
		Private privateNumber As Integer
		Public Property Number() As Integer
			Get
				Return privateNumber
			End Get
			Set(ByVal value As Integer)
				privateNumber = value
			End Set
		End Property
		Private privateGroup As Integer
		Public Property Group() As Integer
			Get
				Return privateGroup
			End Get
			Set(ByVal value As Integer)
				privateGroup = value
			End Set
		End Property
	End Class
End Namespace
