using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0, sira=0;
            Console.Write("Tic Tac Toe icin n boyutunu giriniz: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n>7 || n<3)
                Console.WriteLine("Lutfen mantikli bir boyut giriniz (3-7 araliginda).");
            Console.WriteLine("Girilen deger: {0}", n);
            oyunTahtasi tahta = new oyunTahtasi(n);
            //TODO: Tahtayi ayarla.

            oyuncu[] oyuncular = new oyuncu[2];
            for(int i=0;i<2;i++) //Oyuncu atamasi
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
            
            while (true) //Gameplay
            {
                string hamle;
                tahta.oyunTahtasiniYazdir();
                hamle = oyuncular[sira].oyuncununHamlesiniAl(n);
                if (tahta.hamleyiYaz(hamle,oyuncular[sira]))
                {
                    sira = (sira + 1) % 2;
                }
                
                //Console.Clear();
            }

        }

    }
}
