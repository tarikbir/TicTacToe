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
            string savePath = "save.txt";
            oyunTahtasi tahta;
            oyuncu[] oyuncular = new oyuncu[oyuncuSayisi];

            // MENU
            Console.Write("::Tic Tac Toe::\n1.Yeni Oyun\n2.Kayitli Oyunu Ac\n3.Cikis\n\nSecim: ");
            while (!int.TryParse(Console.ReadLine(), out secim) || secim < 1 || secim > 3)
                Console.WriteLine("Yanlis secim yaptiniz! 1-3 arasinda secim yapiniz!");

            if (secim == 1) //YENI OYUN
            {
                Console.Write("Tic Tac Toe icin n boyutunu giriniz: ");
                while (!int.TryParse(Console.ReadLine(), out n) || n != 7 || n != 5 || n != 3)
                    Console.WriteLine("Lutfen mantikli bir boyut giriniz (3, 5 veya 7).");
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
                    while (!char.TryParse(Console.ReadLine(), out karakter) || karakter == ' ')
                        Console.WriteLine("Lutfen karaktersizlik yapmayiniz: ");
                    Console.WriteLine("Oyuncu {0} adi nedir?", i + 1);
                    oyuncuAdi = Console.ReadLine();

                    oyuncular[i] = new oyuncu(insanMi, karakter, oyuncuAdi);
                }
            }
            else if (secim == 2) //KAYITLI OYUNU AC
            {
                if (!File.Exists(savePath))
                {
                    Console.WriteLine("No save file found. Press any key to exit.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                using (StreamReader sr = new StreamReader(savePath))
                {
                    n = int.Parse(sr.ReadLine());
                    Console.WriteLine("Boyut alindi ({0}).", n);
                    char[][] geciciTahta = new char[n][];
                    for (int i = 0; i < n; i++)
                    {
                        geciciTahta[i] = sr.ReadLine().ToCharArray();
                        Console.WriteLine(geciciTahta[i]);
                    }
                    Console.WriteLine("Oyun tahtasi alindi.");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (geciciTahta[i][j] == ' ')
                                Console.Write("_ ");
                            else
                                Console.Write("{0} ", geciciTahta[i][j]);
                        }
                        Console.Write('\n');
                    }
                    tahta = new oyunTahtasi(geciciTahta, n);
                    tahta.oyunTahtasiniYazdir();
                    for (int i = 0; i < oyuncuSayisi; i++)
                    {
                        char karakter = char.Parse(sr.ReadLine());
                        bool insanMi = bool.Parse(sr.ReadLine());
                        string oyuncuAdi = sr.ReadLine();
                        oyuncular[i] = new oyuncu(insanMi, karakter, oyuncuAdi);
                        Console.WriteLine("Oyuncu {0} alindi.", oyuncuAdi);
                    }
                    sira = int.Parse(sr.ReadLine());
                    Console.WriteLine("Sira alindi ({0}).", sira);
                }
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
                using (StreamWriter sw = new StreamWriter(savePath, false))
                {
                    otomatikKayit(n, tahta, oyuncular, oyuncuSayisi, sira, sw);
                }
            }
            Console.WriteLine(" Kapatmak icin bir tusa basiniz...");
            Console.ReadKey();

        }

    }
}
