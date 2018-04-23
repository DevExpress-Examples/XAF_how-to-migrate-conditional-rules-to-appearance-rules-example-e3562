using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.ConditionalEditorState;

namespace RuleConversion2.Module {
    public class ConditionalEditorStateConverter : IModelNodeUpdater<IModelConditionalEditorState> {
        // If a ConditionalEditorState rule contains this string in the Id then the AppearanceItemType of a resulting ConditionalAppearance rule will be "LayoutItem".
        public const string LayoutItemRuleTargetItemTypeMark = "LayoutItem";
        private const string MethodNameMemberPresentErrorsMessage = "Cannot create a rule from the attribute because the MethodName member cannot be used in the Conditional Appearance module exactly as it was in ConditionalEditorState module.\r\nRefer to the documentation to see how to change it manually.";

        public void UpdateNode(IModelConditionalEditorState node, IModelApplication application) {
            foreach (IModelEditorStateRule rule in node.Rules) {
                if (((ModelNode)rule).IsNewNode) {
                    IModelClass modelClass = rule.ModelClass;
                    if (modelClass == null) {
                        modelClass = GetModelClassNode(((ModelNode)rule).Application, ((ModelNode)rule).GetValue("ModelClass_ID").ToString());
                    }
                    if (modelClass != null) {
                        ViewItemVisibility visibility = ViewItemVisibility.Hide;
                        if (node.HideEditorMode == HideEditorMode.ShowEmptySpace) {
                            visibility = ViewItemVisibility.ShowEmptySpace;
                        }
                        CreateAppearanceRuleNode(modelClass, rule.Id, rule.Properties, rule.EditorState, rule.Criteria, rule.ViewType, rule.MethodName, rule.Index, visibility);
                        rule.Remove();
                    }
                }
            }
        }

        public static IModelClass GetModelClassNode(IModelApplication application, string className) {
            IModelClass result = null;
            if (!string.IsNullOrEmpty(className)) {
                if (application.BOModel == null) {
                    application.AddNode<IModelBOModel>("BOModel");
                }
                result = application.BOModel[className];
                if (result == null) {
                    result = application.BOModel.AddNode<IModelClass>(className);
                }
            }
            return result;
        }
        public static void CreateAppearanceRuleNode(IModelClass modelClass, string id, string properties, EditorState editorState, string criteria, ViewType viewType, string methodName, int? index, ViewItemVisibility visibility) {
            if (modelClass is IModelConditionalAppearance) {
                IModelConditionalAppearance appearance = modelClass as IModelConditionalAppearance;
                IModelAppearanceRule targetRule = appearance.AppearanceRules.AddNode<IModelAppearanceRule>(id);
                targetRule.TargetItems = properties;
                switch (editorState) {
                    case EditorState.Default:
                        if (!String.IsNullOrEmpty(methodName)) {
                            throw new ArgumentException(MethodNameMemberPresentErrorsMessage + "\r\nIn class: " + modelClass.Name + " Attribute.Id=" + id);
                        }
                        break;
                    case EditorState.Disabled:
                        targetRule.Enabled = false;
                        break;
                    case EditorState.Hidden:
                        targetRule.Visibility = visibility;
                        break;
                }
                targetRule.Criteria = criteria;
                targetRule.Method = methodName;
                targetRule.Context = viewType.ToString();
                targetRule.Index = index;
                if (id.Contains(LayoutItemRuleTargetItemTypeMark)) {
                    targetRule.AppearanceItemType = AppearanceItemType.LayoutItem.ToString();
                }
                else {
                    targetRule.AppearanceItemType = AppearanceItemType.ViewItem.ToString();
                }
            }
        }
    }
}
