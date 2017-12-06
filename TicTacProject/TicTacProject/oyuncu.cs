using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacProject
{
    class oyuncu
    {
        private char karakter;
        private bool insanMi;
        private string kullaniciAdi = "oyuncu";

        public oyuncu()
        {
            karakter = 'X';
            insanMi = true;
        }

        public oyuncu(bool insanmiKontrolu)
        {
            insanMi = insanmiKontrolu;
            if (insanmiKontrolu)
                karakter = 'X';
            else
                karakter = 'O';
        }

        public oyuncu(bool insanmiKontrolu, char kr)
        {
            karakter = kr;
            insanMi = insanmiKontrolu;
        }

        public char karakteriAl()
        {
            return karakter;
        }

        public bool oyuncuTurunuAl()
        {
            return insanMi;
        }

        public string oyuncununHamlesiniAl()
        {
            string hamle;
            Console.WriteLine("Oyuncu {0} icin hamle giriniz: ",kullaniciAdi);
            hamle = Console.ReadLine();
            //TODO: Kontrol yap.
            return hamle;
        }

        public string insanOyuncuHamlesiniKontrol()
        {
            return "xx";
        }

        public string bilgisayarHamlesiUret()
        {
            return "xx";
        }

    }
}
