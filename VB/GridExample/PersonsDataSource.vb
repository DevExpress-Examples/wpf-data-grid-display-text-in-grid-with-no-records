Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands

Namespace GridExample

    Public Class PersonsViewModel

        Private personsField As ObservableCollection(Of Person)

        Public Sub New()
            Dim Names As List(Of String) = New List(Of String) From {"Alex", "Alice", "Tony", "Den", "Andrew", "John", "Donald", "Brian", "Effy", "Lisa", "Matthew"}
            personsField = New ObservableCollection(Of Person)()
            For i As Integer = 0 To 10 - 1
                personsField.Add(New Person(Names(i), "Last name " & i, 21 + i, i Mod 2 = 0, 170 + i, 75 + i))
            Next

            personsField(5).Age = 22
            personsField(8).Age = 50
        End Sub

        Public ReadOnly Property Persons As ObservableCollection(Of Person)
            Get
                Return personsField
            End Get
        End Property

        Public ReadOnly Property ClearPersons As ICommand
            Get
                Return New DelegateCommand(Of Object)(Sub(p) Persons.Clear())
            End Get
        End Property
    End Class

    Public Class Person
        Implements INotifyPropertyChanged, IDataErrorInfo

        Const lastNamePropertyName As String = "LastName"

        Const firstNamePropertyName As String = "FirstName"

        Const agePropertyName As String = "Age"

        Const heightPropertyName As String = "IsMarried"

        Const isMarriedPropertyName As String = "Height"

        Const weightPtropertyName As String = "Weight"

        Private firstNameField As String

        Private lastNameField As String

        Private ageField As Integer

        Private isMarriedField As Boolean

        Private heightField As Integer

        Private weightField As Integer

        Private Validator As PersonPropertiesValidator = New PersonPropertiesValidator()

        Public Sub New()
        End Sub

        Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal age As Integer, ByVal isMarried As Boolean, ByVal height As Integer, ByVal weight As Integer)
            Me.FirstName = firstName
            Me.LastName = lastName
            Me.Age = age
            Me.IsMarried = isMarried
            Me.Weight = weight
            Me.Height = height
        End Sub

        Public Property FirstName As String
            Get
                Return firstNameField
            End Get

            Set(ByVal value As String)
                Validator.IsNameValid(value, firstNamePropertyName)
                firstNameField = value
                RaisePropertyChanged(firstNamePropertyName)
            End Set
        End Property

        Public Property LastName As String
            Get
                Return lastNameField
            End Get

            Set(ByVal value As String)
                Validator.IsNameValid(value, lastNamePropertyName)
                lastNameField = value
                RaisePropertyChanged(lastNamePropertyName)
            End Set
        End Property

        Public Property Age As Integer
            Get
                Return ageField
            End Get

            Set(ByVal value As Integer)
                Validator.IsAgeValid(value, agePropertyName)
                ageField = value
                RaisePropertyChanged(agePropertyName)
            End Set
        End Property

        Public Property IsMarried As Boolean
            Get
                Return isMarriedField
            End Get

            Set(ByVal value As Boolean)
                isMarriedField = value
                RaisePropertyChanged(isMarriedPropertyName)
            End Set
        End Property

        Public Property Height As Integer
            Get
                Return heightField
            End Get

            Set(ByVal value As Integer)
                heightField = value
                RaisePropertyChanged(heightPropertyName)
            End Set
        End Property

        Public Property Weight As Integer
            Get
                Return weightField
            End Get

            Set(ByVal value As Integer)
                weightField = value
                RaisePropertyChanged(weightPtropertyName)
            End Set
        End Property

'#Region "INotifyPropertyChanged members"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

'#End Region
'#Region "IDataErrorInfo members"
        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.[Error]
            Get
                Return Validator.DataError
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
            Get
                If Validator.DataErrors.ContainsKey(columnName) Then
                    Return Validator.DataErrors(columnName)
                Else
                    Return Nothing
                End If
            End Get
        End Property
'#End Region
    End Class

    Public Class PersonPropertiesValidator

        Private dataErrorField As String = ""

        Private dataErrorsField As Dictionary(Of String, String) = New Dictionary(Of String, String)()

        Public Function IsNameValid(ByVal value As String, ByVal propertyName As String) As Boolean
            If String.IsNullOrEmpty(value) Then
                dataErrorsField(propertyName) = "Full name is required."
                Return False
            Else
                ClearPropertyErrors(propertyName)
                Return True
            End If
        End Function

        Public Function IsAgeValid(ByVal value As Integer, ByVal propertyName As String) As Boolean
            If value <= 0 Then
                dataErrorsField(propertyName) = "Age validation failed."
                Return False
            Else
                ClearPropertyErrors(propertyName)
                Return True
            End If
        End Function

        Public ReadOnly Property DataError As String
            Get
                Return dataErrorField
            End Get
        End Property

        Public ReadOnly Property DataErrors As Dictionary(Of String, String)
            Get
                Return dataErrorsField
            End Get
        End Property

        Private Sub ClearPropertyErrors(ByVal propertyName As String)
            If dataErrorsField.ContainsKey(propertyName) Then dataErrorsField.Remove(propertyName)
        End Sub
    End Class
End Namespace
