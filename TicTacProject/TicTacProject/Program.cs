using System;
using System.Collections.Generic;
using System.IO;

namespace TicTacProject
{
    class Program
    {
        static void otomatikKayit(int boyut, oyunTahtasi tahta, oyuncu[] oyuncular, int oyuncuSayisi, int sira, StreamWriter sw)
        {
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
                int oyunTuru;
                Console.Write("\nTic Tac Toe icin n boyutunu giriniz: ");
                while (!int.TryParse(Console.ReadLine(), out n) || (n != 7 && n != 5 && n != 3))
                    Console.WriteLine("Lutfen mantikli bir boyut giriniz (3, 5 veya 7).");
                tahta = new oyunTahtasi(n);
                Console.Write("\nOyun turunu seciniz:\n1.Insan vs. Insan\n2.Insan vs. PC\n3.PC vs. PC\n\nSecim: ");
                while (!int.TryParse(Console.ReadLine(), out oyunTuru) || oyunTuru < 1 || oyunTuru > 3)
                    Console.WriteLine("Yanlis secim yaptiniz! 1-3 arasinda secim yapiniz!");
                if (oyunTuru == 1) //Insan vs. Insan
                {
                    for (int i = 0; i < oyuncuSayisi; i++)
                    {
                        char karakter;
                        string oyuncuAdi;
                        Console.WriteLine("Oyuncu {0} karakteri?", i + 1);
                        while (!char.TryParse(Console.ReadLine(), out karakter) || karakter == ' ')
                            Console.WriteLine("Lutfen bosluk haricinde bir karakter giriniz: ");
                        Console.WriteLine("Oyuncu {0} adi nedir?", i + 1);
                        oyuncuAdi = Console.ReadLine();
                        if (oyuncuAdi == "")
                            oyuncular[i] = new oyuncu(true, karakter);
                        else
                            oyuncular[i] = new oyuncu(true, karakter, oyuncuAdi);
                    }
                }
                else if (oyunTuru == 2) //Insan vs. PC
                {
                    char karakter;
                    string oyuncuAdi;
                    Console.WriteLine("Oyuncu karakteri?");
                    while (!char.TryParse(Console.ReadLine(), out karakter) || karakter == ' ')
                        Console.WriteLine("Lutfen bosluk haricinde bir karakter giriniz: ");
                    Console.WriteLine("Oyuncu adi nedir?");
                    oyuncuAdi = Console.ReadLine();
                    if (oyuncuAdi == "")
                        oyuncular[0] = new oyuncu(true, karakter);
                    else
                        oyuncular[0] = new oyuncu(true, karakter, oyuncuAdi);
                    oyuncular[1] = new oyuncu(false);
                }
                else //PC vs. PC
                {
                    oyuncular[0] = new oyuncu(false, 'X', "PC 1");
                    oyuncular[1] = new oyuncu(false, 'O', "PC 2");
                }
            }
            else if (secim == 2) //KAYITLI OYUNU AC
            {
                if (!File.Exists(savePath))
                {
                    Console.WriteLine("Kayitli dosya bulunamadi. Cikis icin bir tusa basiniz.");
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
                    tahta = new oyunTahtasi(geciciTahta, n);
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
            while (true) //Oyun ana dongusu
            {
                Console.WriteLine("Tahta: ");
                tahta.oyunTahtasiniYazdir();
                string hamle = oyuncular[sira].oyuncununHamlesiniAl(n);

                if (!tahta.hamleyiYaz(hamle, oyuncular[sira]))
                {
                    Console.Clear();
                    continue;
                }

                if (tahta.kazanan(oyuncular[sira]))
                {
                    Console.Clear();
                    Console.Write("Oyunu {0} kazandi!\n", oyuncular[sira].kullaniciAdiniAl());
                    tahta.oyunTahtasiniYazdir();
                    break;
                }
                else if (tahta.beraberlikKontrol())
                {
                    Console.Clear();
                    Console.Write("Oyun berabere bitti!\n");
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
            Console.WriteLine("Kapatmak icin bir tusa basiniz...");
            Console.ReadKey();
        }
    }
}
