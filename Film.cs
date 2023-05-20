using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NET_PR2_1_z2
{
    public class Film : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private static Dictionary<string,ICollection<string>> powiązaneWłaściwości
            = new()
            {
                ["Tytuł"] = new string[] { "SkrótSzczegółów" },
                ["Reżyser"] = new string[] { "SkrótSzczegółów" }
            };
        private void NotyfikujZmianę(
            [CallerMemberName] string? nazwaWłaściwości = null,
            HashSet<string> jużZałatwione = null
            )
        {
            if (jużZałatwione == null)
                jużZałatwione = new();
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nazwaWłaściwości)
                );
            jużZałatwione.Add(nazwaWłaściwości);
            if(powiązaneWłaściwości.ContainsKey(nazwaWłaściwości))
                foreach(
                    string powiązanaWłaściwość
                    in
                    powiązaneWłaściwości[nazwaWłaściwości]
                    )
                    if(jużZałatwione.Contains(powiązanaWłaściwość) == false)
                        NotyfikujZmianę(powiązanaWłaściwość, jużZałatwione);
        }

        private string
            tytuł,
            reżyser,
            studio
            ;
        private TimeSpan
            czasTrwania
            ;
        private DateTime
            dataWydania = DateTime.Today
            ;
        private int
            wybranyNośnik
            ;
        public string Tytuł {
            get => tytuł;
            set
            {
                tytuł = value;
                NotyfikujZmianę();
            }
        }
        public string Reżyser {
            get => reżyser;
            set
            {
                reżyser = value;
                NotyfikujZmianę();
            }
        }

        public string Studio
        {
            get => studio;
            set
            {
                studio = value;
                NotyfikujZmianę();
            }
        }

        public DateTime? DataWydania {
            get => dataWydania;
            set
            {
                dataWydania = (DateTime)value;
                NotyfikujZmianę();
            }
        }

        public int WybranyNośnik {
            get => wybranyNośnik;
            set
            {
                wybranyNośnik = value;
                NotyfikujZmianę();
            }
        }

        public TimeSpan? CzasTrwania {
            get => czasTrwania;
            set
            {
                czasTrwania = (TimeSpan)value;
                NotyfikujZmianę();
            }
        }


        public string SkrótSzczegółów => $"'{Tytuł}' reż.: {Reżyser}";
    }
}
