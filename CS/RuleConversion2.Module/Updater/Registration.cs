using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.ConditionalFormatting;
using DevExpress.ExpressApp.ConditionalEditorState;

namespace RuleConversion2.Module {
    partial class RuleConversion2Module {
        public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders) {
            base.ExtendModelInterfaces(extenders);
            extenders.Add<IModelColumn, IModelConditionalFormatting>();
            extenders.Add<IModelListView, IModelConditionalFormatting>();
            extenders.Add<IModelApplication, IModelApplicationConditionalEditorStates>();
        }
        public override void AddModelNodeUpdaters(IModelNodeUpdaterRegistrator updaterRegistrator) {
            base.AddModelNodeUpdaters(updaterRegistrator);
            updaterRegistrator.AddUpdater<IModelColumn>(new ConditionalFormattingColumnConverter());
            updaterRegistrator.AddUpdater<IModelListView>(new ConditionalFormattingListViewConverter());
            updaterRegistrator.AddUpdater<IModelConditionalEditorState>(new ConditionalEditorStateConverter());
        }
        public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters) {
            base.AddGeneratorUpdaters(updaters);
            updaters.Add(new ConditionalAppearanceNodesGeneratorUpdater());
        }
    }
}
