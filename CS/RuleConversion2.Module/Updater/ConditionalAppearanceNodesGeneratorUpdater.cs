using System;
using System.Reflection;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.ConditionalEditorState;
using DevExpress.ExpressApp.ConditionalEditorState.Core;

namespace RuleConversion2.Module {
    public class ConditionalAppearanceNodesGeneratorUpdater : ModelNodesGeneratorUpdater<AppearanceRulesModelNodesGenerator> {
        public override void UpdateNode(ModelNode node) {
            ViewItemVisibility visibility = ViewItemVisibility.Hide;
            if (node.Application is IModelApplicationConditionalEditorStates) {
                if (((IModelApplicationConditionalEditorStates)node.Application).ConditionalEditorState.HideEditorMode == HideEditorMode.ShowEmptySpace) {
                    visibility = ViewItemVisibility.ShowEmptySpace;
                }
            }
            IModelClass modelClass = node.Parent as IModelClass;
            CreateAppearanceRuleNodes(EditorStateRuleManager.FindAttributes(modelClass.TypeInfo), modelClass, String.Empty, visibility);
            foreach (MethodInfo methodInfo in modelClass.TypeInfo.Type.GetMethods(EditorStateRuleManager.MethodRuleBindingFlags)) {
                CreateAppearanceRuleNodes(EditorStateRuleManager.FindAttributes(methodInfo), modelClass, methodInfo.Name, visibility);
            }
        }
        public static void CreateAppearanceRuleNodes(IEnumerable<EditorStateRuleAttribute> attributes, IModelClass modelClass, string methodName, ViewItemVisibility visibility) {
            foreach (EditorStateRuleAttribute attribute in attributes) {
                ConditionalEditorStateConverter.CreateAppearanceRuleNode(modelClass, attribute.Id, attribute.Properties, attribute.EditorState, attribute.Criteria, attribute.ViewType, methodName, default(int?), visibility);
            }
        }
    }
}
