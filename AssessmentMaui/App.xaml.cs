using AssessmentMaui.Model;
using System.Security.Cryptography.X509Certificates;

namespace AssessmentMaui
{
    public partial class App : Application
    {
        public static Employee employee;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static string Token { get; internal set; }
    }
}