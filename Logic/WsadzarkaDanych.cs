using Microsoft.Extensions.DependencyInjection;
using Models;
using Services;
using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Logic
{
    public class WsadzarkaDanych
    {
        private ServiceProvider _services;
        private readonly IOczyszczaczDanych _oczyszczaczDanych;
        private readonly IWalidatorWierszy _walidatorWierszy;
        //  private readonly ILogger _logger;
        public WsadzarkaDanych()
        {

            _services = new ServiceCollection()
            .AddTransient<IOczyszczaczDanych, OczyszczaczDanych>()
            .AddTransient<IWalidatorWierszy, WalidatorWierszy>()
            // .AddLogging()            
            .BuildServiceProvider();

            _oczyszczaczDanych = _services.GetService<IOczyszczaczDanych>();
            _walidatorWierszy = _services.GetService<IWalidatorWierszy>();
            //  _logger = _services.GetService<ILogger>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sciezkaPliku"></param>
        /// <param name="separator">jaki znak rozdziela poszczególne partie danych</param>
        public (List<Przelew> przeprocesowaneDane, int otrzymanychDanych) PobnierzDaneZPliku(string sciezkaPliku, char separator = ',')
        {
            var naglowki = new List<string>();
            var obiekty = new List<Przelew>();
            var plik = new FileInfo(sciezkaPliku);


            var licznik = 0;
            using (StreamReader sr = new StreamReader(sciezkaPliku))
            {
                try
                {
                    while (!sr.EndOfStream)
                    {
                        var aktualnaLinia = _oczyszczaczDanych.UsunZbedneZnaki(sr.ReadLine());

                        if (!string.IsNullOrEmpty(aktualnaLinia))
                        {
                            if (licznik == 0)
                            {
                                var interpretacjaNaglowkow = _walidatorWierszy.InterpretujNaglowki(aktualnaLinia, separator);
                                if (interpretacjaNaglowkow.powodzenie == false) { break; }
                                naglowki = interpretacjaNaglowkow.naglowki;
                            }
                            else
                            {
                                var interpretacjaWiersza = _walidatorWierszy.InterpretujWiersz(aktualnaLinia, naglowki, separator);

                                if (interpretacjaWiersza.powodzenie == true)
                                {
                                    interpretacjaWiersza.Przelew.TrybDodania = TrybDodania.ZPliku;
                                    interpretacjaWiersza.Przelew.DodanyDnia = DateTime.Now;
                                    interpretacjaWiersza.Przelew.PlikPochodzenia = plik.Name;
                                    obiekty.Add(interpretacjaWiersza.Przelew);
                                }
                            }
                            //todo: mpatela tu jeszcze logować ile się powiodło, a ile nie
                            licznik++;
                        }
                    }
                }
                finally { sr.Close(); }
            }
            licznik--;
            return (obiekty, licznik);
        }

    }
}
