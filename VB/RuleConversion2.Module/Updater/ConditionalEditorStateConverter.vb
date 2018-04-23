Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.ConditionalEditorState

Namespace RuleConversion2.Module
	Public Class ConditionalEditorStateConverter
		Implements IModelNodeUpdater(Of IModelConditionalEditorState)
		' If a ConditionalEditorState rule contains this string in the Id then the AppearanceItemType of a resulting ConditionalAppearance rule will be "LayoutItem".
		Public Const LayoutItemRuleTargetItemTypeMark As String = "LayoutItem"
		Private Const MethodNameMemberPresentErrorsMessage As String = "Cannot create a rule from the attribute because the MethodName member cannot be used in the Conditional Appearance module exactly as it was in ConditionalEditorState module." & vbCrLf & "Refer to the documentation to see how to change it manually."

		Public Sub UpdateNode(ByVal node As IModelConditionalEditorState, ByVal application As IModelApplication) Implements IModelNodeUpdater(Of IModelConditionalEditorState).UpdateNode
			For Each rule As IModelEditorStateRule In node.Rules
				If (CType(rule, ModelNode)).IsNewNode Then
					Dim modelClass As IModelClass = rule.ModelClass
					If modelClass Is Nothing Then
						modelClass = GetModelClassNode((CType(rule, ModelNode)).Application, (CType(rule, ModelNode)).GetValue("ModelClass_ID").ToString())
					End If
					If modelClass IsNot Nothing Then
						Dim visibility As ViewItemVisibility = ViewItemVisibility.Hide
						If node.HideEditorMode = HideEditorMode.ShowEmptySpace Then
							visibility = ViewItemVisibility.ShowEmptySpace
						End If
						CreateAppearanceRuleNode(modelClass, rule.Id, rule.Properties, rule.EditorState, rule.Criteria, rule.ViewType, rule.MethodName, rule.Index, visibility)
						rule.Remove()
					End If
				End If
			Next rule
		End Sub

		Public Shared Function GetModelClassNode(ByVal application As IModelApplication, ByVal className As String) As IModelClass
			Dim result As IModelClass = Nothing
			If Not String.IsNullOrEmpty(className) Then
				If application.BOModel Is Nothing Then
					application.AddNode(Of IModelBOModel)("BOModel")
				End If
                Dim classes As IModelList(Of IModelClass) = CType(application.BOModel, IModelList(Of IModelClass))
				result = classes(className)
				If result Is Nothing Then
					result = application.BOModel.AddNode(Of IModelClass)(className)
				End If
			End If
			Return result
		End Function
		Public Shared Sub CreateAppearanceRuleNode(ByVal modelClass As IModelClass, ByVal id As String, ByVal properties As String, ByVal editorState As EditorState, ByVal criteria As String, ByVal viewType As ViewType, ByVal methodName As String, ByVal index As Nullable(Of Integer), ByVal visibility As ViewItemVisibility)
			If TypeOf modelClass Is IModelConditionalAppearance Then
				Dim appearance As IModelConditionalAppearance = TryCast(modelClass, IModelConditionalAppearance)
				Dim targetRule As IModelAppearanceRule = appearance.AppearanceRules.AddNode(Of IModelAppearanceRule)(id)
				targetRule.TargetItems = properties
				Select Case editorState
					Case EditorState.Default
						If Not String.IsNullOrEmpty(methodName) Then
							Throw New ArgumentException(MethodNameMemberPresentErrorsMessage & vbCrLf & "In class: " & modelClass.Name & " Attribute.Id=" & id)
						End If
					Case EditorState.Disabled
						targetRule.Enabled = False
					Case EditorState.Hidden
						targetRule.Visibility = visibility
				End Select
				targetRule.Criteria = criteria
				targetRule.Method = methodName
				targetRule.Context = viewType.ToString()
				targetRule.Index = index
				If id.Contains(LayoutItemRuleTargetItemTypeMark) Then
					targetRule.AppearanceItemType = AppearanceItemType.LayoutItem.ToString()
				Else
					targetRule.AppearanceItemType = AppearanceItemType.ViewItem.ToString()
				End If
			End If
		End Sub
	End Class
End Namespace
