using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentMaui.Model;
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string Token 
    {
        get => $"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkF6ZWV6YXRAZ21haWwuY29tIiwibmJmIjoxNzAyNDYyMzE0LCJleHAiOjE3MDI0NjI2MTQsImlhdCI6MTcwMjQ2MjMxNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzODMiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDM4MyJ9.47cjBnBqTVj1rHMMrdnim6C5sIFN4dYnl2_MNEnrXRw";
    }
}

