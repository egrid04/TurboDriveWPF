using System;
using System.Windows;
using TurboDrive.Classes;
using TurboDrive.ViewModels;

namespace TurboDrive.Windows
{
    public partial class UjFelhasznalo : Window
    {
        private readonly UserViewModel _viewModel;

        public UjFelhasznalo(UserViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private async void Hozzaadas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ellenőrizzük, hogy a jogosultság és az aktív státusz szám
                if (!int.TryParse(jogosultsag.Text, out int jogosultsagValue))
                {
                    MessageBox.Show("A jogosultságnak számnak kell lennie!");
                    return;
                }

                if (!int.TryParse(aktiv.Text, out int aktivValue))
                {
                    MessageBox.Show("Az aktív státusznak számnak kell lennie!");
                    return;
                }

                // Új felhasználó létrehozása az űrlap adataiból
                User newUser = new User
                {
                    FelhasznaloNev = felhasznaloNev.Text,
                    TeljesNev = teljesNev.Text,
                    Email = email.Text,
                    Jogosultsag = jogosultsagValue,
                    Aktiv = aktivValue,
                    Salt = "saltValue", // Ha van salt, akkor itt add meg
                    Hash = "hashValue", // Ha van hash, akkor itt add meg
                    RegisztracioDatuma = DateTime.Now, // Ha szükséges, akkor adj hozzá dátumot
                    FenykepUtvonal = "fenykepUtvonalValue", // Ha van fénykép, akkor itt add meg
                };




                // Az AddUserAsync metódus hívása a newUser paraméterrel
                bool isUserAdded = await _viewModel.AddUserAsync(newUser);

                if (isUserAdded)
                {
                    MessageBox.Show("Új felhasználó sikeresen hozzáadva!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a felhasználó felvétele során.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }

    }
}
