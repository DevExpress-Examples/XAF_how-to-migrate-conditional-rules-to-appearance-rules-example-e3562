using System.Drawing;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.ConditionalFormatting;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace RuleConversion2.Module {
    public class ConditionalFormattingColumnConverter : ConditionalFormattingConverterBase, IModelNodeUpdater<IModelColumn> {
        public void UpdateNode(IModelColumn targetnode, IModelApplication application) {
            IModelObjectView view = FindView(application, targetnode.ParentView.Id) as IModelObjectView;
            Convert((IModelConditionalFormatting)targetnode, application, view);
        }
    }
    
    public class ConditionalFormattingListViewConverter : ConditionalFormattingConverterBase, IModelNodeUpdater<IModelListView> {
        public void UpdateNode(IModelListView targetnode, IModelApplication application) {
            IModelObjectView view = FindView(application, targetnode.Id) as IModelObjectView;
            Convert((IModelConditionalFormatting)targetnode, application, view);
        }
    }

    public abstract class ConditionalFormattingConverterBase {
        protected IModelView FindView(IModelApplication application, string viewId) {
            ModelNode app = ModelNodeHiddenMethods.GetGeneratorLayer((ModelNode)application);
            return app.GetNodeInThisLayer("Views").GetNodeInThisLayer(viewId) as IModelView;
        }
        protected void Convert(IModelConditionalFormatting targetnode, IModelApplication application, IModelObjectView view) {
            IModelConditionalFormatting ruleList = (IModelConditionalFormatting)targetnode;
            if (ruleList.ConditionalFormatting == null) {
                return;
            }
            foreach (IModelConditionalFormattingRule node in ruleList.ConditionalFormatting) {
                if (((ModelNode)node).IsNewNode && view.ModelClass != null) {
                    IModelClass modelClass = ConditionalEditorStateConverter.GetModelClassNode(((ModelNode)node).Application, view.ModelClass.Name);
                    if (modelClass is IModelConditionalAppearance) {
                        IModelConditionalAppearance appearance = modelClass as IModelConditionalAppearance;
                        IModelAppearanceRule targetRule = appearance.AppearanceRules.AddNode<IModelAppearanceRule>(view.Id + node.Id);
                        targetRule.Criteria = node.Criteria;
                        targetRule.Priority = node.Priority;
                        targetRule.AppearanceItemType = AppearanceItemType.ViewItem.ToString();
                        targetRule.Context = "ListView";
                        string targetItems = "";
                        if (targetnode is IModelListView) {
                            targetItems = "*";
                        }
                        else if (targetnode is IModelColumn) {
                            targetItems = ((IModelColumn)targetnode).Id;
                        }
                        targetRule.TargetItems = targetItems;
                        foreach (IModelConditionalFormattingTarget target in node) {
                            if (target.Name == "Foreground") {
                                targetRule.FontColor = (Color)target.Color;
                            }
                            else if (target.Name == "Background") {
                                targetRule.BackColor = (Color)target.Color;
                            }
                        }
                    }
                    node.Remove();
                }
            }
        }
    }
}
