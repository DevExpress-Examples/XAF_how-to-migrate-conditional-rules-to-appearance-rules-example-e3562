Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.ConditionalFormatting
Imports DevExpress.ExpressApp.ConditionalEditorState

Namespace RuleConversion2.Module
	Partial Public Class RuleConversion2Module
		Public Overrides Sub ExtendModelInterfaces(ByVal extenders As ModelInterfaceExtenders)
			MyBase.ExtendModelInterfaces(extenders)
			extenders.Add(Of IModelColumn, IModelConditionalFormatting)()
			extenders.Add(Of IModelListView, IModelConditionalFormatting)()
			extenders.Add(Of IModelApplication, IModelApplicationConditionalEditorStates)()
		End Sub
		Public Overrides Sub AddModelNodeUpdaters(ByVal updaterRegistrator As IModelNodeUpdaterRegistrator)
			MyBase.AddModelNodeUpdaters(updaterRegistrator)
			updaterRegistrator.AddUpdater(Of IModelColumn)(New ConditionalFormattingColumnConverter())
			updaterRegistrator.AddUpdater(Of IModelListView)(New ConditionalFormattingListViewConverter())
			updaterRegistrator.AddUpdater(Of IModelConditionalEditorState)(New ConditionalEditorStateConverter())
		End Sub
		Public Overrides Sub AddGeneratorUpdaters(ByVal updaters As ModelNodesGeneratorUpdaters)
			MyBase.AddGeneratorUpdaters(updaters)
			updaters.Add(New ConditionalAppearanceNodesGeneratorUpdater())
		End Sub
	End Class
End Namespace
