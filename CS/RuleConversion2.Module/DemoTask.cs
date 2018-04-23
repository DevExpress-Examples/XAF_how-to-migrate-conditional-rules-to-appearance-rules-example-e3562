using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.ConditionalEditorState;

namespace RuleConversion2.Module {
    [DefaultClassOptions]
    [EditorStateRuleAttribute("TestRule4", "StringProperty", EditorState.Hidden, "DisableProperties = 'True'", ViewType.Any)]
    public class DemoTask : BaseObject {
        private int integerProperty;
        public int IntegerProperty {
            get {
                return integerProperty;
            }
            set {
                SetPropertyValue("IntegerProperty", ref integerProperty, value);
            }
        }
        private string stringProperty;
        public string StringProperty {
            get {
                return stringProperty;
            }
            set {
                SetPropertyValue("StringProperty", ref stringProperty, value);
            }
        }
        private bool disableProperties;
        [ImmediatePostData]
        public bool DisableProperties {
            get {
                return disableProperties;
            }
            set {
                SetPropertyValue("DisableProperties", ref disableProperties, value);
            }
        }
        public DemoTask(Session session)
            : base(session) {
        }
    }
}
