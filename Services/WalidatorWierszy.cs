using Models;
using System.Reflection;

namespace Services
{
    public class WalidatorWierszy: IWalidatorWierszy
    {

        /// <summary>
        /// Określa w jakiej kolejności dane są przechowywane w pliku oraz wykonuje walidację.
        /// Przykłądowy nagłowek: LP,kwota,rachunekNadawcy,rachunekOdbiorcy,”opis”
        /// </summary>
        /// <param name="wiersz"></param>
        public (bool powodzenie, List<string> naglowki) InterpretujNaglowki(string wiersz, char separator = ',')
        {
            var podzielone = wiersz.Split(separator);

            if (podzielone.Length == 0) { throw new ArgumentException($@"Nie udało się rozdzielić danych w pliku. Plik jest pusty, lub wskazany separator jest inny."); } //todo:mpatela: napisać metodę samoczynnie okreslającą najbardziej potencjalny separator.
            var interpretacja = PobierzAktualneNazwyPol(podzielone.ToList());

            if (interpretacja.Item1 == false) { return (false, null); }
            else return interpretacja;

        }


        /// <summary>
        /// Waliduje i konwertuje dane wejsciowe do klasy Przelew.
        /// </summary>
        /// <param name="wiersz"></param>
        /// <param name="naglowki"></param>
        /// <returns></returns>
        public (bool powodzenie, Przelew Przelew) InterpretujWiersz(string wiersz, List<string> naglowki, char separator = ',')
        {
            var rozszyte = wiersz.Split(separator).ToList();
            rozszyte.Except(new List<string>() { "ID", "DodanyDnia", "TrybDodania", "PlikPochodzenia" });

            if (rozszyte.Count == 0) { return (false, null); };
            if (rozszyte.ToList().Count != naglowki.Count) { return (false, null); }; //todo: znowu kwestia pol wymaganych i opcjonalnych.

            var przelew = new Przelew();
            var licznik = naglowki.Count;
            var bledy = false;

            for (int i = 0; i < licznik; i++)
            {
                if (bledy)
                {
                    break;
                }

                //todo:mpatela:to można będzie lepiej zrobić, może jakąś refleksją póki co bruteforcowe przypisywanie wartości
                //todo:mpatela:może jakiś intersect na toupperach. jak będzie czas to się pokombinuje
                switch (naglowki[i])
                {
                    case "LP":
                        {
                            var liczba = 0.0f;
                            if (!float.TryParse(rozszyte[i], out liczba)) { bledy = true; }
                            przelew.LP = liczba;
                            break;
                        }
                    case "kwota":
                        {
                            var liczba = 0;
                            if (!Int32.TryParse(rozszyte[i], out liczba)) { bledy = true; }
                            przelew.Kwota = liczba;
                            break;
                        }
                    case "rachunekNadawcy":
                        {
                            przelew.RachunekNadawcy = rozszyte[i];
                            break;
                        }
                    case "rachunekOdbiorcy":
                        {
                            przelew.RachunekOdbiorcy = rozszyte[i];
                            break;
                        }
                    case "opis":
                        {
                            przelew.Opis = rozszyte[i];
                            break;
                        }
                }
            }
            //todo:mpatela:logowanie na bledach
            if (bledy) { return (false, null); }
            return (true, przelew);
        }


        /// <summary>
        /// Porównuje, czy pola w pliku są zgodne z tymi z implementacji klasy.
        /// </summary>
        /// <param name="polaZPliku"></param>
        private (bool powodzenie, List<string>) PobierzAktualneNazwyPol(List<string> polaZPliku)
        {
            var polaKlasy = typeof(Przelew)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Select(p => p.Name.ToUpper())
           .Except(new List<string>() { "ID", "DODANYDNIA", "TRYBDODANIA", "PLIKPOCHODZENIA" })
           .ToList();

            //todo: mpatela podwalidatory wywalic do osobnych metod

            //todo:mpatela: ilosć pól w pliku może być inna. dodatkowa walidacja czy wszystkie wymagane (atrybuty klasy) są uwzględnione w pliku.
            if (polaKlasy.Count != polaZPliku.Count) { return (false, null); }

            //porownanie nazw z pliku i klasy
            //todo:mpatela: możliwe, że mogą sięróżnić, ale wtedy będzie potrzebny customowy walidator, a nie ogólny jak ten
            var polaZPlikuWielkie = new List<string>();
            var licznik = polaZPliku.Count;
            for (int i = 0; i < licznik; i++)
            {
                polaZPlikuWielkie.Add(polaZPliku[i]
                    .ToUpper());
            }

            var elementyWspolne = polaKlasy.Intersect(polaZPlikuWielkie);

            if (elementyWspolne.ToList().Count != polaZPliku.Count) { return (false, null); }


            return (true, polaZPliku);
        }

    }
}
