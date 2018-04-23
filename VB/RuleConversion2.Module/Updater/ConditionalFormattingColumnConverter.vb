Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.ConditionalFormatting
Imports DevExpress.ExpressApp.ConditionalAppearance

Namespace RuleConversion2.Module
	Public Class ConditionalFormattingColumnConverter
		Inherits ConditionalFormattingConverterBase
		Implements IModelNodeUpdater(Of IModelColumn)
		Public Sub UpdateNode(ByVal targetnode As IModelColumn, ByVal application As IModelApplication) Implements IModelNodeUpdater(Of IModelColumn).UpdateNode
			Dim view As IModelObjectView = TryCast(FindView(application, targetnode.ParentView.Id), IModelObjectView)
			Convert(CType(targetnode, IModelConditionalFormatting), application, view)
		End Sub
	End Class

	Public Class ConditionalFormattingListViewConverter
		Inherits ConditionalFormattingConverterBase
		Implements IModelNodeUpdater(Of IModelListView)
		Public Sub UpdateNode(ByVal targetnode As IModelListView, ByVal application As IModelApplication) Implements IModelNodeUpdater(Of IModelListView).UpdateNode
			Dim view As IModelObjectView = TryCast(FindView(application, targetnode.Id), IModelObjectView)
			Convert(CType(targetnode, IModelConditionalFormatting), application, view)
		End Sub
	End Class

	Public MustInherit Class ConditionalFormattingConverterBase
		Protected Function FindView(ByVal application As IModelApplication, ByVal viewId As String) As IModelView
			Dim app As ModelNode = ModelNodeHiddenMethods.GetGeneratorLayer(CType(application, ModelNode))
			Return TryCast(app.GetNodeInThisLayer("Views").GetNodeInThisLayer(viewId), IModelView)
		End Function
		Protected Sub Convert(ByVal targetnode As IModelConditionalFormatting, ByVal application As IModelApplication, ByVal view As IModelObjectView)
			Dim ruleList As IModelConditionalFormatting = CType(targetnode, IModelConditionalFormatting)
			If ruleList.ConditionalFormatting Is Nothing Then
				Return
			End If
			For Each node As IModelConditionalFormattingRule In ruleList.ConditionalFormatting
				If (CType(node, ModelNode)).IsNewNode AndAlso view.ModelClass IsNot Nothing Then
					Dim modelClass As IModelClass = ConditionalEditorStateConverter.GetModelClassNode((CType(node, ModelNode)).Application, view.ModelClass.Name)
					If TypeOf modelClass Is IModelConditionalAppearance Then
						Dim appearance As IModelConditionalAppearance = TryCast(modelClass, IModelConditionalAppearance)
						Dim targetRule As IModelAppearanceRule = appearance.AppearanceRules.AddNode(Of IModelAppearanceRule)(view.Id & node.Id)
						targetRule.Criteria = node.Criteria
						targetRule.Priority = node.Priority
						targetRule.AppearanceItemType = AppearanceItemType.ViewItem.ToString()
						targetRule.Context = "ListView"
						Dim targetItems As String = ""
						If TypeOf targetnode Is IModelListView Then
							targetItems = "*"
						ElseIf TypeOf targetnode Is IModelColumn Then
							targetItems = (CType(targetnode, IModelColumn)).Id
						End If
						targetRule.TargetItems = targetItems
						For Each target As IModelConditionalFormattingTarget In node
							If target.Name = "Foreground" Then
								targetRule.FontColor = CType(target.Color, Color)
							ElseIf target.Name = "Background" Then
								targetRule.BackColor = CType(target.Color, Color)
							End If
						Next target
					End If
					CType(node, IModelNode).Remove()
				End If
			Next node
		End Sub
	End Class
End Namespace
