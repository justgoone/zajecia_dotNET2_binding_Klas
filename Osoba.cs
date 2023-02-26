using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace zajecia2
{
    public class Osoba : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private static Dictionary<string, ICollection<string>> powiązaneWłaściwości = new()
        {
            ["Imię"] = new string[] { "ImięNazwisko", "FormatWitaj"},
            ["Nazwisko"] = new string[] { "ImięNazwisko", "FormatWitaj" },
        };
        private void NotyfikujZmianę([CallerMemberName] string? nazwaWłaściwości = null)
        {
            PropertyChanged?.Invoke(
                this, 
                new(nazwaWłaściwości)
                );
            foreach(string powiązanaWłaściwość in powiązaneWłaściwości[nazwaWłaściwości])
                PropertyChanged?.Invoke(
                    this,
                    new(powiązanaWłaściwość)
                    );
        }

        private string imię;
        private string nazwisko;

        public string Imię
        {
            get => imię; 
            
            set
            {
                imię = value;
                NotyfikujZmianę();
            }
        }
        public string Nazwisko
        {
            get => nazwisko; 
            
            set
            {
                nazwisko = value;
                NotyfikujZmianę();
            }
        }
        public string ImięNazwisko => $"{imię} {nazwisko}";
        public string FormatWitaj => $"Witaj {ImięNazwisko}!";

    }
}
