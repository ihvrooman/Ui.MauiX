
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// A view used to show the information related to a completed workflow step.
    /// <para>Note: This control should only be used within the <see cref="DynamicWorkflow"/> control.</para>
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedWorkflowStepView : ContentView
    {
        public CompletedWorkflowStepView()
        {
            InitializeComponent();
        }
    }
}