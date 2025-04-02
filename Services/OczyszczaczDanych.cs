using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OczyszczaczDanych : IOczyszczaczDanych
    {
        /// <summary>
        /// Usuwa wszystkie niepotrzebne znaki
        /// </summary>
        /// <param name="linia"></param>
        /// <returns></returns>
        public string UsunZbedneZnaki(string dane)
        {
            return dane.Replace("”", "").Trim();
        }
    }

}
