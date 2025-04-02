using Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Services
{
    public class WalidatorWierszy : IWalidatorWierszy
    {

        /// <summary>
        /// Określa w jakiej kolejności dane są przechowywane w pliku oraz wykonuje walidację.
        /// Przykłądowy nagłowek: LP,kwota,rachunekNadawcy,rachunekOdbiorcy,”opis”
        /// </summary>
        /// <param name="wiersz"></param>
        public (bool powodzenie, List<string> naglowki) InterpretujNaglowki(string wiersz, char separator = ',')
        {

            //todo: odgadnąć separator

            var podzielone = wiersz.Split(separator);
            if (podzielone.Length == 0) { return (false, null); }

            else return (true,podzielone.ToList());
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

                switch (naglowki[i])
                {
                    case "LP":
                        {
                            if (string.IsNullOrEmpty(rozszyte[i]))
                            {
                                if (CzyPoleWYmagane("LP"))
                                {
                                    bledy = true; break;
                                }
                            }

                            var liczba = 0.0f;
                            if (!float.TryParse(rozszyte[i], out liczba)) { bledy = true; }
                            przelew.LP = liczba;
                            break;
                        }
                    case "kwota":
                        {
                            if (string.IsNullOrEmpty(rozszyte[i]) || string.IsNullOrWhiteSpace(rozszyte[i]))
                            {
                                if (CzyPoleWYmagane("Kwota"))
                                {
                                    bledy = true; break;
                                }
                            }
                            var liczba = 0;
                            if (!Int32.TryParse(rozszyte[i], out liczba)) { bledy = true; }
                            przelew.Kwota = liczba;
                            break;
                        }
                    case "rachunekNadawcy":
                        {
                            if (string.IsNullOrEmpty(rozszyte[i]) || string.IsNullOrWhiteSpace(rozszyte[i]))
                            {
                                if (CzyPoleWYmagane("RachunekNadawcy"))
                                {
                                    bledy = true; break;
                                }
                            }
                            przelew.RachunekNadawcy = rozszyte[i];
                            break;
                        }
                    case "rachunekOdbiorcy":
                        {
                            
                            if (string.IsNullOrEmpty(rozszyte[i]) || string.IsNullOrWhiteSpace(rozszyte[i]))
                            {
                                if (CzyPoleWYmagane("RachunekOdbiorcy"))
                                {
                                    bledy = true; break;
                                }
                            }
                            przelew.RachunekOdbiorcy = rozszyte[i];
                            break;
                        }
                    case "opis":
                        {
                            if (string.IsNullOrEmpty(rozszyte[i]) || string.IsNullOrWhiteSpace(rozszyte[i]))
                            {
                                if (CzyPoleWYmagane("Opis"))
                                {
                                    bledy = true; break;
                                }
                            }
                            przelew.Opis = rozszyte[i];
                            break;
                        }
                }
            }
            //todo:mpatela:logowanie na bledach
            if (bledy) { return (false, null); }
            return (true, przelew);
        }

        private bool CzyPoleWYmagane(string pole)
        {
            var atrybut =  typeof(Przelew).GetProperties().Where(w => w.Name == pole).FirstOrDefault().GetCustomAttribute(typeof(RequiredAttribute), false);

            return atrybut is not null;
        }



    }
}
