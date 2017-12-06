using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacProject
{
    class oyunTahtasi
    {
        public char[][] oynTahtasi = new char [7][];
        private int boyut=3;

        public oyunTahtasi()
        {

        }

        public oyunTahtasi(int boyut)
        {
            this.boyut = boyut;
            for (int i=0; i<boyut; i++)
            {
                oynTahtasi[i] = new char[boyut];
            }
        }

        public oyunTahtasi(char[][] oynTahtasi, int boyut)
        {
            this.oynTahtasi.Equals(oynTahtasi);
            this.boyut = boyut;
            //Start here
        }

        public char[][] oyunTahtasiniAl()
        {
            return oynTahtasi;
        }

        public void oyunTahtasiniYazdir()
        {
            for (int i=0; i<boyut; i++)
            {
                for (int j=0; j<boyut; j++)
                {
                    if (oynTahtasi[i][j] != 0)
                        Console.Write("{0} ", oynTahtasi[i][j]);
                    else
                        Console.Write("_ ");
                }
                Console.WriteLine();
            }
        }

        public int[] str2cord(string koordinat)
        {
            int[] kord = new int[2];
            kord[0] = Convert.ToInt32(koordinat.Substring(0,1));
            kord[1] = Convert.ToInt32(koordinat.Substring(1,1));
            Console.WriteLine("({0},{1})", kord[0], kord[1]);
            return kord;
        }

        public bool hamleyiYaz(string koordinat, oyuncu oyuncu)
        {
            int[] kord = str2cord(koordinat);
            if (oynTahtasi[kord[0]][kord[1]] == 0)
            {
                oynTahtasi[kord[0]][kord[1]] = oyuncu.karakteriAl();
                return true;
            }
            else
                return false;
        }

        public bool kazanan(char oyuncu)
        {
            return true;
        }

        public bool beraberlikKontrol()
        {
            for (int i=0;i<boyut;i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    if (oynTahtasi[i][j] == 0)
                        return false;
                }
            }
            return true;
        }




    }
}
