using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TurboDrive.Classes;
using TurboDrive.Service;


namespace TurboDrive.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; } = new();
        private User _selectedUser;
        private User _ujFelhasznalo = new User();
        public User UjFelhasznalo
        {
            get => _ujFelhasznalo;
            set
            {
                _ujFelhasznalo = value;
                OnPropertyChanged(nameof(UjFelhasznalo));
            }
        }
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task LoadUsersAsync()
        {
            var users = await UsersService.getUsers(MainWindow.client);
            if (users != null)
            {
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
        }

        public async Task SaveUserAsync(User user)
        {
            bool success = await UsersService.SaveUser(MainWindow.client, user);
            if (success) MessageBox.Show("Felhasználó mentve!");
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                MessageBox.Show("A felhasználó nem található a listában!");
                return;
            }

            MessageBox.Show($"Törlendő ID: {userId}");

            bool success = await UsersService.DeleteUser(MainWindow.client, userId);
            if (success)
            {
                MessageBox.Show("Felhasználó törölve!");
                await LoadUsersAsync();
            }
            else
            {
                MessageBox.Show("A felhasználó nem található az adatbázisban!");
            }
        }



        public async Task UpdateUserAsync(User user)
        {
            bool success = await UsersService.UpdateUser(MainWindow.client, user);
            if (success)
            {
                MessageBox.Show("Felhasználó sikeresen módosítva!");
                await LoadUsersAsync(); // Frissítjük a listát
            }
            else
            {
                MessageBox.Show("Hiba a módosítás során.");
            }
        }
        public async Task<bool> AddUserAsync(User user)
        {
            bool success = await UsersService.AddUser(MainWindow.client, user);
            if (success)
            {
                MessageBox.Show("Felhasználó sikeresen hozzáadva!");
                await LoadUsersAsync(); // Frissítjük a listát
            }
            else
            {
                MessageBox.Show("Hiba a felhasználó hozzáadása során.");
            }
            return success;
        }






        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
