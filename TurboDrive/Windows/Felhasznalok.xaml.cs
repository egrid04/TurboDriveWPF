using System.Windows;
using TurboDrive.ViewModels;

namespace TurboDrive.Windows
{
    public partial class Felhasznalok : Window
    {
        private UserViewModel viewModel;

        public Felhasznalok()
        {
            InitializeComponent();
            viewModel = (UserViewModel)DataContext;
            Loaded += async (_, _) => await viewModel.LoadUsersAsync();
        }

        public async void Adatokbetoltese(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadUsersAsync();
        }

        private async void Torles(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedUser != null)
            {
                if (MessageBox.Show("Biztosan törlöd a kiválasztott felhasználót?", "Megerősítés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await viewModel.DeleteUserAsync(viewModel.SelectedUser.Id);
                }
            }
            else
            {
                MessageBox.Show("Válassz ki egy felhasználót a törléshez.");
            }
        }

        private void Modositas(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedUser != null)
            {
                var modositasAblak = new Modositas(viewModel);
                modositasAblak.ShowDialog();
            }
            else
            {
                MessageBox.Show("Válassz ki egy felhasználót a módosításhoz.");
            }
        }
        
        private void UjFelhasznalo_Click(object sender, RoutedEventArgs e)
        {
            var ujFelhasznaloAblak = new UjFelhasznalo(viewModel);
            ujFelhasznaloAblak.ShowDialog();
        }


    }
}
