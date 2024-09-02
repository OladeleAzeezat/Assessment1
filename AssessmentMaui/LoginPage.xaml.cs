using AssessmentMaui.ViewModels;

namespace AssessmentMaui;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private async void SignUp(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//SignUp");
	}
}