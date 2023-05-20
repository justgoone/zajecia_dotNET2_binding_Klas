using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.IO;

namespace NET_PR2_1_z2
{
    /// <summary>
    /// Logika interakcji dla klasy ListaFilmów.xaml
    /// </summary>
    public partial class ListaFilmów : Window
    {
        public ObservableCollection<Film> Filmy { get; } = new();

        ListBox lista;
        public ListaFilmów()
        {
            DataContext = this;
            InitializeComponent();
            lista = (ListBox)FindName("Lista");
        }

        private void Edytuj(object sender, RoutedEventArgs e)
        {
            Film wybrana = (Film)lista.SelectedItem;
            if (wybrana != null)
            {
                new WidokFilmu(
                    (Film)lista.SelectedItem
                    )
                    .Show();
            }
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Film nowa = new Film();
            Filmy.Add(nowa);
            new WidokFilmu(nowa)
                .Show();
        }

        private void Usuń(object sender, RoutedEventArgs e)
        {
            Film wybrana = (Film)lista.SelectedItem;
            Filmy.Remove(wybrana);
        }

        private void Importuj(object sender, RoutedEventArgs e)
        {
            string path = $@"./filmlist.json";
            try
            {
                string plikJSON = File.ReadAllText(path);

                Film[] nowy = JsonSerializer.Deserialize<Film[]>(plikJSON);
                foreach (Film jedenFilm in nowy)
                {
                    Filmy.Add(jedenFilm);
                }
            }
            catch (FileNotFoundException)
            {
                string directoryCurrent = Directory.GetCurrentDirectory();
                string messageBoxText = ("Nie znaleziono pliku do importu. Upewnij się że poprawny plik znajduje się w '" + directoryCurrent + "/' oraz nazywa się filmlist.json. W przypadku dalszych błędów możliwe iż występuje problem z danymi w pliku");
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, "Error", button, icon);
            }
        }

        private void Eksportuj(object sender, RoutedEventArgs e)
        {

            var filmyDoPliku = JsonSerializer.Serialize(Filmy.ToList(),
                new JsonSerializerOptions
                {
                    WriteIndented = false,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            string path = $@"./filmlist.json";
        
            File.Create(path).Dispose();
            File.WriteAllText(path, filmyDoPliku);
        }
    }
}
