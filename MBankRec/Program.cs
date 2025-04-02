
using System.Security.Cryptography.X509Certificates;
using Logic;

class Program
{
    public static string sciezkaPliku = string.Empty;



    static void Main(string[] args)
    {

#if DEBUG

        sciezkaPliku = $@"D:\Projects\MBankRec\flatFile.txt";

#endif


        var wielkorzadca = new Logic.FileHelper();
        var daneZPliku = wielkorzadca.PobnierzDaneZPliku(sciezkaPliku);




    }


}