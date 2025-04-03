using System.Windows;
using TurboDrive.ViewModels;

namespace TurboDrive.Windows
{
    public partial class Modositas : Window
    {
        private UserViewModel _viewModel;

        public Modositas(UserViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private async void Mentes_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedUser != null)
            {
                await _viewModel.SaveUserAsync(_viewModel.SelectedUser);
                MessageBox.Show("Felhasználó módosítva!");
                Close();
            }
        }
    }
}
