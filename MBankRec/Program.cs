
using System.Security.Cryptography.X509Certificates;
using Logic;
using Repository;

class Program
{
    public static string sciezkaPliku = string.Empty;



    static void Main(string[] args)
    {

#if DEBUG

        sciezkaPliku = $@"D:\Projects\MBankRec\flatFile.txt";

#endif


        var wielkorzadca = new Logic.WsadzarkaDanych();
        var repo = new RepozytoriumPrzelewy();
        var daneZPliku = wielkorzadca.PobnierzDaneZPliku(sciezkaPliku);
        foreach (var przelew in daneZPliku.przeprocesowaneDane)
        {
            repo.DodajPrzelew(przelew);
           // repo.DodajPrzelewProcedura(przelew);
        }
        Console.WriteLine($@"Przeprocesowano {daneZPliku.przeprocesowaneDane.Count} z {daneZPliku.otrzymanychDanych} otrzymanych rekordów");

        Console.ReadKey();

    }


}