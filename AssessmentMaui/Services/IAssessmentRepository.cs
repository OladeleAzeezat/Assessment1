using AssessmentMaui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentMaui.Services;
public interface IAssessmentRepository
{
    Task<Employee> Login(Employee emp);
    //Task<Employee> SignIn(string email, string password);
    
    //void Register(Employee employee);
}

