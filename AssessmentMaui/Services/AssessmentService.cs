using AssessmentMaui.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AssessmentMaui.Services;
public class AssessmentService : IAssessmentRepository
{
//    public async Task<Employee> SignIn(string email, string password)
//    {
//        try
//        {
//            var client = new HttpClient();
//            string localhostUrl = "https://localhost:44383/api/Login?email=" + email + "&password=" + password;
//            client.BaseAddress = new Uri(localhostUrl);
//            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
//            if (response.IsSuccessStatusCode)
//            {
//                Employee employee = await response.Content.ReadFromJsonAsync<Employee>();
//                return await Task.FromResult(employee);
//            }
//            return null;
//        }
//        catch (Exception ex)
//        {
//            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
//            return null;
//        }
//    }

    public async Task<Employee> Login(Employee emp)
    {
        try
        {
            var client = new HttpClient();

            string signinRequeststr = JsonConvert.SerializeObject(emp);

            //var response = await client.PostAsync("https://localhost:44383/api/Login?email=" + emp.Email + "&password=" + emp.Password,
              //      new StringContent(signinRequeststr, Encoding.UTF8, "application/json"));
            
            string localhostUrl = "https://localhost:44383/api/Login?email=" + emp.Email + "&password=" + emp.Password;
            client.BaseAddress = new Uri(localhostUrl);
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, new StringContent(signinRequeststr, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                Employee employee = await response.Content.ReadFromJsonAsync<Employee>();
                return await Task.FromResult(employee);
            }
            return null;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task SignUp(Employee employee)
    {
        var client = new HttpClient();
        string localhostUrl = "https://localhost:44383/api/AddEmployees";
    }
}

