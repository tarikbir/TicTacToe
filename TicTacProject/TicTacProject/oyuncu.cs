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

        public oyuncu(bool insanmiKontrolu, char kr, string ka)
        {
            karakter = kr;
            insanMi = insanmiKontrolu;
            kullaniciAdi = ka;
        }

        public char karakteriAl()
        {
            return karakter;
        }

        public bool oyuncuTurunuAl()
        {
            return insanMi;
        }

        public string oyuncununHamlesiniAl(int boyut)
        {
            if (insanMi)
                return insanOyuncuHamlesiniKontrol(boyut);
            else
                return bilgisayarHamlesiUret(boyut);
        }

        public string insanOyuncuHamlesiniKontrol(int boyut)
        {
            int hamle;
            string hamleS;
            Console.WriteLine("Oyuncu {0} icin hamle giriniz: ", kullaniciAdi);
            //TODO: Boyut kontrolu ekle
            do
            {
                hamleS = Console.ReadLine();
                if (!int.TryParse(hamleS, out hamle))
                {
                    Console.WriteLine("Lutfen dogru bir hamle giriniz: ");
                    continue;
                }
                else
                {
                    hamleS = hamleS.Substring(0, 2);
                    break;
                }
            } while (true);

            return hamleS;
        }

        public string bilgisayarHamlesiUret(int boyut)
        {
            return "xx";
        }

    }
}
