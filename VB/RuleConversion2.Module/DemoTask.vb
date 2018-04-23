Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.ConditionalEditorState

Namespace RuleConversion2.Module
	<DefaultClassOptions, EditorStateRuleAttribute("TestRule4", "StringProperty", EditorState.Hidden, "DisableProperties = 'True'", ViewType.Any)> _
	Public Class DemoTask
		Inherits BaseObject
'INSTANT VB NOTE: The variable integerProperty was renamed since Visual Basic does not allow class members with the same name:
		Private integerProperty_Renamed As Integer
		Public Property IntegerProperty() As Integer
			Get
				Return integerProperty_Renamed
			End Get
			Set(ByVal value As Integer)
				SetPropertyValue("IntegerProperty", integerProperty_Renamed, value)
			End Set
		End Property
'INSTANT VB NOTE: The variable stringProperty was renamed since Visual Basic does not allow class members with the same name:
		Private stringProperty_Renamed As String
		Public Property StringProperty() As String
			Get
				Return stringProperty_Renamed
			End Get
			Set(ByVal value As String)
				SetPropertyValue("StringProperty", stringProperty_Renamed, value)
			End Set
		End Property
'INSTANT VB NOTE: The variable disableProperties was renamed since Visual Basic does not allow class members with the same name:
		Private disableProperties_Renamed As Boolean
		<ImmediatePostData> _
		Public Property DisableProperties() As Boolean
			Get
				Return disableProperties_Renamed
			End Get
			Set(ByVal value As Boolean)
				SetPropertyValue("DisableProperties", disableProperties_Renamed, value)
			End Set
		End Property
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
	End Class
End Namespace
