using AssessmentMaui.Model;

namespace AssessmentMaui.EmployeeControls;

public partial class FlyoutHeaderControl : ContentView
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();
		if(App.employee != null)
		{
			lblText.Text = "Login as: " + App.employee.Name; 
			lblEmail.Text = App.employee.Email; ;
		}
	}
}