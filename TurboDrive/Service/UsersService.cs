using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using TurboDrive.Classes;

namespace TurboDrive.Service
{
    public static class UsersService
    {
        // 📌 Felhasználók lekérdezése
        public static async Task<List<User>> getUsers(HttpClient client)
        {
            try
            {
                Console.WriteLine("▶ Felhasználók lekérdezése folyamatban...");
                var response = await client.GetAsync($"api/User/{MainWindow.loggedUser.token}");

                Console.WriteLine($"ℹ️ Válasz státusz: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var lista = await response.Content.ReadFromJsonAsync<List<User>>();
                    Console.WriteLine($"✅ Sikeres lekérdezés. Talált felhasználók száma: {lista?.Count ?? 0}");
                    return lista ?? new List<User>();
                }
                else
                {
                    Console.WriteLine($"❌ Sikertelen lekérdezés: {response.ReasonPhrase}");
                    MessageBox.Show($"Hiba a felhasználók lekérdezése során: {response.ReasonPhrase}");
                    return new List<User>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hiba a getUsers során: {ex.Message}");
                MessageBox.Show($"Hiba a felhasználók lekérdezése során: {ex.Message}");
                return new List<User>();
            }
        }

        // 📌 Felhasználó hozzáadása
        public static async Task<bool> AddUser(HttpClient client, User user)
        {
            try
            {
                var response = await client.PostAsJsonAsync($"api/User/{MainWindow.loggedUser.token}", user);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Hiba történt: {response.ReasonPhrase} - {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}");
                return false;
            }
        }





        // 📌 Felhasználó frissítése (Módosítás)
        public static async Task<bool> UpdateUser(HttpClient client, User user)
        {
            try
            {
                Console.WriteLine($"▶ Felhasználó frissítése folyamatban (ID: {user.Id})...");
                var response = await client.PutAsJsonAsync($"api/User/{user.Id}?token={MainWindow.loggedUser.token}", user);

                Console.WriteLine($"ℹ️ Válasz státusz: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Felhasználó sikeresen frissítve.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"❌ Sikertelen frissítés: {response.ReasonPhrase}");
                    MessageBox.Show($"Felhasználó frissítése sikertelen: {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hiba a UpdateUser során: {ex.Message}");
                MessageBox.Show($"Hiba a felhasználó frissítése során: {ex.Message}");
                return false;
            }
        }
        public static async Task<bool> SaveUser(HttpClient client, User user)
        {
            try
            {
                var response = await client.PutAsJsonAsync($"api/User/{MainWindow.loggedUser.token}", user);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                MessageBox.Show("Hiba a mentés során.");
                return false;
            }
        }

        // 📌 Felhasználó törlése
        public static async Task<bool> DeleteUser(HttpClient client, int userId)
        {
            try
            {
                Console.WriteLine($"▶ Felhasználó törlése folyamatban (ID: {userId})...");
                var response = await client.DeleteAsync($"api/User/{MainWindow.loggedUser.token},{userId}");

                Console.WriteLine($"ℹ️ Válasz státusz: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Felhasználó sikeresen törölve.");
                    return true;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Sikertelen törlés: {response.ReasonPhrase} - {errorMessage}");
                    MessageBox.Show($"Felhasználó törlése sikertelen: {response.ReasonPhrase}\n{errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hiba a DeleteUser során: {ex.InnerException?.Message ?? ex.Message}");
                MessageBox.Show($"Hiba a felhasználó törlése során: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }
        }
    }
}
