namespace demoSqlite
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly LocalDBServices _dbServices;
        private int _editEmployeeId;

        public MainPage(LocalDBServices dbServices)
        {
            InitializeComponent();
            _dbServices = dbServices;

            Task.Run(async () => lstView.ItemsSource = await _dbServices.GetEmployees()); // dont need to add 'async'
        }

        private async void saveBtn_Clicked(object sender, EventArgs e)
        {
            if (_editEmployeeId == 0)
            {
                // Add Employee
                await _dbServices.Create(new Employee
                {
                    Name = nameEntry.Text,
                    Email = emailEntry.Text,
                });

            } else {
                // Edit Employee
                await _dbServices.Update(new Employee
                {
                    Id = _editEmployeeId,
                    Name = nameEntry.Text,
                    Email = emailEntry.Text,
                });
            }

            _editEmployeeId = 0;
            nameEntry.Text = string.Empty;
            emailEntry.Text = string.Empty;
            lstView.ItemsSource = await _dbServices.GetEmployees();
        }

        private async void lstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var employee = (Employee)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    _editEmployeeId = employee.Id;
                    nameEntry.Text = employee.Name;
                    emailEntry.Text = employee.Email;
                    break;
                case "Delete":
                    await _dbServices.Delete(employee);
                    lstView.ItemsSource = await _dbServices.GetEmployees();
                    break;

            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

        }
    }

}
