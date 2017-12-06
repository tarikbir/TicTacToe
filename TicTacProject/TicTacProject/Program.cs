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
            n = Convert.ToInt32(Console.ReadLine());
            //TODO: Kontrol ekle.
            Console.WriteLine("Girilen deger: {0}", n);
            oyunTahtasi tahta = new oyunTahtasi(n);
            //TODO: Tahtayi ayarla.

            oyuncu[] oyuncular = new oyuncu[2];
            oyuncular[0] = new oyuncu();
            oyuncular[1] = new oyuncu(false);
            //TODO: Oyunculari ata.
            
            while (true) //Gameplay
            {
                string hamle;
                tahta.oyunTahtasiniYazdir();
                hamle = oyuncular[sira].oyuncununHamlesiniAl();
                if (tahta.hamleyiYaz(hamle,oyuncular[sira]))
                {
                    sira = (sira + 1) % 2;
                }
                
                //Console.Clear();
            }

        }

    }
}
