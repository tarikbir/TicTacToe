using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TicTacProject
{
    class Program
    {
        static void otomatikKayit(int boyut, oyunTahtasi tahta, oyuncu[] oyuncular, int oyuncuSayisi, int sira, StreamWriter sw)
        {
            /*  boyut
                 *  [matris]
                 *  [matris]
                 *  [matris]
                 *  oyuncu1 karakter
                 *  oyuncu1 insanMi
                 *  oyuncu1 oyuncuAdi
                 *  oyuncu2 karakter
                 *  oyuncu2 insanMi
                 *  oyuncu2 oyuncuAdi
                 *  sira
                 */
            sw.WriteLine(boyut);
            char[][] oyunT = tahta.oyunTahtasiniAl();
            for (int i=0; i<boyut; i++)
            {
                for (int j=0; j<boyut; j++)
                    sw.Write(oyunT[i][j]);
                sw.WriteLine();
            }
            for (int i=0; i<oyuncuSayisi; i++)
            {
                sw.WriteLine(oyuncular[i].karakteriAl());
                sw.WriteLine(oyuncular[i].oyuncuTurunuAl());
                sw.WriteLine(oyuncular[i].kullaniciAdiniAl());
            }
            sw.WriteLine(sira);
        }

        static void Main(string[] args)
        {
            int n = 3, sira=0, secim=0, oyuncuSayisi = 2;
            oyunTahtasi tahta;
            oyuncu[] oyuncular = new oyuncu[oyuncuSayisi];
            var saveFile = File.Open("save.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var saveFileW = new StreamWriter(saveFile);
            var saveFileR = new StreamReader(saveFile);

            // MENU
            Console.Write("::Tic Tac Toe::\n1.Yeni Oyun\n2.Kayitli Oyunu Ac\n3.Cikis\n\nSecim: ");
            while (!int.TryParse(Console.ReadLine(), out secim) || secim < 1 || secim > 3)
                Console.WriteLine("Yanlis secim yaptiniz! 1-3 arasinda secim yapiniz!");

            if (secim == 1) //YENI OYUN
            {
                Console.Write("Tic Tac Toe icin n boyutunu giriniz: ");
                while (!int.TryParse(Console.ReadLine(), out n) || n > 7 || n < 3)
                    Console.WriteLine("Lutfen mantikli bir boyut giriniz (3-7 araliginda).");
                Console.WriteLine("Girilen deger: {0}", n);
                tahta = new oyunTahtasi(n);
                for (int i = 0; i < oyuncuSayisi; i++) //Oyuncu atamasi
                {
                    char karakter;
                    bool insanMi;
                    string oyuncuAdi;
                    Console.WriteLine("Oyuncu {0} insan mi? true/false ", i + 1);
                    while (!bool.TryParse(Console.ReadLine(), out insanMi))
                        Console.WriteLine("Lutfen degeri adam gibi giriniz: ");
                    Console.WriteLine("Oyuncu {0} karakteri?", i + 1);
                    while (!char.TryParse(Console.ReadLine(), out karakter))
                        Console.WriteLine("Lutfen karaktersizlik yapmayiniz: ");
                    Console.WriteLine("Oyuncu {0} adi nedir?", i + 1);
                    oyuncuAdi = Console.ReadLine();

                    oyuncular[i] = new oyuncu(insanMi, karakter, oyuncuAdi);
                }
            }
            else if (secim == 2) //KAYITLI OYUNU AC
            {
                /*  boyut
                 *  [matris]
                 *  [matris]
                 *  [matris]
                 *  oyuncu1 karakter
                 *  oyuncu1 insanMi
                 *  oyuncu1 oyuncuAdi
                 *  oyuncu2 karakter
                 *  oyuncu2 insanMi
                 *  oyuncu2 oyuncuAdi
                 *  sira
                 */
                int boy = int.Parse(saveFileR.ReadLine());
                for (int i = 0; i < boy; i++)
                {
                    string line = saveFileR.ReadLine();
                    for (int j = 0; j < boy; j++)
                    {

                    }
                }
                tahta = new oyunTahtasi(n);
            }
            else
            {
                tahta = new oyunTahtasi(n);
                Environment.Exit(0);
            }
            Console.Clear();

            while (true) //Gameplay
            {
                Console.WriteLine("Tahta: ");
                string hamle;
                tahta.oyunTahtasiniYazdir();
                hamle = oyuncular[sira].oyuncununHamlesiniAl(n);

                if (!tahta.hamleyiYaz(hamle, oyuncular[sira]))
                {
                    Console.Clear();
                    continue;
                }

                if (tahta.beraberlikKontrol())
                {
                    Console.Clear();
                    Console.Write("Oyun berabere bitti!");
                    tahta.oyunTahtasiniYazdir();
                    break;
                }
                else if (tahta.kazanan(oyuncular[sira]))
                {
                    Console.Clear();
                    Console.Write("Oyunu {0} kazandi!", oyuncular[sira].kullaniciAdiniAl());
                    tahta.oyunTahtasiniYazdir();
                    break;
                }

                
                sira = (sira + 1) % 2;
                Console.Clear();
                otomatikKayit(n, tahta, oyuncular, oyuncuSayisi, sira, saveFileW);
            }

            Console.WriteLine(" Kapatmak icin bir tusa basiniz...");
            Console.ReadKey();

        }

    }
}
