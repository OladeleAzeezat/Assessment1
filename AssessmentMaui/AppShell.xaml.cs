using AssessmentMaui.ViewModels;
using AssessmentMaui.Views;

namespace AssessmentMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(Item), typeof(Item));
            Routing.RegisterRoute(nameof(Views.Contact), typeof(Views.Contact));

        }

    }
}