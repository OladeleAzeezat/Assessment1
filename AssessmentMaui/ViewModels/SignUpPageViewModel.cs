using AssessmentMaui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentMaui.ViewModels;

public partial class SignUpPageViewModel : ObservableObject
{
    readonly IAssessmentRepository assessment = new AssessmentService();
    public async void SignUp()
    {

        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}

