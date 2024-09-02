using AssessmentMaui.ViewModels;


namespace AssessmentMaui;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpPageViewModel vm)
	{
		InitializeComponent();
		BindingContext =vm;

    }
//	private async void SignIn(object sender, EventArgs e)
//	{
//		await Shell.Current.GoToAsync("//LoginPage");
//	}
}