Imports System.Reflection
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.ConditionalEditorState
Imports DevExpress.ExpressApp.ConditionalEditorState.Core

Namespace RuleConversion2.Module
	Public Class ConditionalAppearanceNodesGeneratorUpdater
		Inherits ModelNodesGeneratorUpdater(Of AppearanceRulesModelNodesGenerator)
		Public Overrides Sub UpdateNode(ByVal node As ModelNode)
			Dim visibility As ViewItemVisibility = ViewItemVisibility.Hide
			If TypeOf node.Application Is IModelApplicationConditionalEditorStates Then
				If (CType(node.Application, IModelApplicationConditionalEditorStates)).ConditionalEditorState.HideEditorMode = HideEditorMode.ShowEmptySpace Then
					visibility = ViewItemVisibility.ShowEmptySpace
				End If
			End If
			Dim modelClass As IModelClass = TryCast(node.Parent, IModelClass)
			CreateAppearanceRuleNodes(EditorStateRuleManager.FindAttributes(modelClass.TypeInfo), modelClass, String.Empty, visibility)
			For Each methodInfo As MethodInfo In modelClass.TypeInfo.Type.GetMethods(EditorStateRuleManager.MethodRuleBindingFlags)
				CreateAppearanceRuleNodes(EditorStateRuleManager.FindAttributes(methodInfo), modelClass, methodInfo.Name, visibility)
			Next methodInfo
		End Sub
		Public Shared Sub CreateAppearanceRuleNodes(ByVal attributes As IEnumerable(Of EditorStateRuleAttribute), ByVal modelClass As IModelClass, ByVal methodName As String, ByVal visibility As ViewItemVisibility)
			For Each attribute As EditorStateRuleAttribute In attributes
				ConditionalEditorStateConverter.CreateAppearanceRuleNode(modelClass, attribute.Id, attribute.Properties, attribute.EditorState, attribute.Criteria, attribute.ViewType, methodName, Nothing, visibility)
			Next attribute
		End Sub
	End Class
End Namespace
