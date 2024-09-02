using AssessmentMaui.EmployeeControls;
using AssessmentMaui.Model;
using AssessmentMaui.Services;
using AssessmentMaui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace AssessmentMaui.ViewModels;
public partial class LoginPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    public static string Token;

    private readonly IAssessmentRepository assessment = new AssessmentService();
    public LoginPageViewModel(IAssessmentRepository assessment)
    {
        this.assessment = assessment;
    }

    [RelayCommand]
    public async void SignIn()
    {
        //to check if internet connection is active
        //if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)


        try
        {
            if(!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                //calling api                                                                                                                                   
                Employee employee = await assessment.Login(new Employee
                {
                    Email = Email , Password = Password
                });
                if (employee != null)
                {
                    if (Preferences.ContainsKey(nameof(App.employee)))
                    {
                        Preferences.Remove(nameof(App.employee));
                    }

                   // await SecureStorage.SetAsync(nameof(App.Token), employee.auth);

                    string empDetails = JsonConvert.SerializeObject(employee);
                    Preferences.Set(nameof(App.employee), empDetails);
                    App.employee = employee;
                    Shell.Current.FlyoutHeader = new FlyoutHeaderControl();
                    await Shell.Current.GoToAsync(nameof(HomePage));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Email or Password is incorrect", "Ok");
                    return;
                } 

            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "All fields required", "Ok");
                return;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return;
        }
    }
   
    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync("SignUpPage");
    }


}

