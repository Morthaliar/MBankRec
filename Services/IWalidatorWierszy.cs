using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IWalidatorWierszy
    {
        (bool powodzenie, List<string> naglowki) InterpretujNaglowki(string wiersz, char separator = ',');
        (bool powodzenie, Przelew Przelew) InterpretujWiersz(string wiersz, List<string> naglowki, char separator = ',');
    }
}
