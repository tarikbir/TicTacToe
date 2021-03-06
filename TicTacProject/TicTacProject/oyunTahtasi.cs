﻿using System;
using System.Collections.Generic;

namespace TicTacProject
{
    class oyunTahtasi
    {
        private char[][] oynTahtasi = new char[7][];
        private int boyut=3;

        public oyunTahtasi()
        {
            for (int i = 0; i < boyut; i++)
            {
                this.oynTahtasi[i] = new char[boyut];
            }
        }

        public oyunTahtasi(int boyut)
        {
            this.boyut = boyut;
            for (int i = 0; i < boyut; i++)
            {
                this.oynTahtasi[i] = new char[boyut];
            }
        }

        public oyunTahtasi(char[][] oynTahtasi, int boyut)
        {
            this.boyut = boyut;
            this.oynTahtasi = new char[boyut][];
            this.oynTahtasi = oynTahtasi;
        }

        public char[][] oyunTahtasiniAl()
        {
            return oynTahtasi;
        }

        public void oyunTahtasiniYazdir()
        {
            Console.Write(" ");
            for (int i = 0; i < boyut; i++) Console.Write(" {0}", i);
            Console.WriteLine();
            for (int i=0; i<boyut; i++)
            {
                Console.Write("{0} ",i);
                for (int j=0; j<boyut; j++)
                {
                    if (oynTahtasi[i][j] == '\0')
                        Console.Write("_ ");
                    else
                        Console.Write("{0} ", oynTahtasi[i][j]);
                }
                Console.WriteLine();
            }
        }

        public int[] str2cord(string koordinat)
        {
            int[] kord = new int[2];
            kord[0] = Convert.ToInt32(koordinat.Substring(0,1));
            kord[1] = Convert.ToInt32(koordinat.Substring(1,1));
            return kord;
        }

        public bool hamleyiYaz(string koordinat, oyuncu oyuncu)
        {
            int[] kord = str2cord(koordinat);
            if (oynTahtasi[kord[0]][kord[1]] == '\0')
            {
                oynTahtasi[kord[0]][kord[1]] = oyuncu.karakteriAl();
                return true;
            }
            else
                return false;
        }

        public bool kazanan(oyuncu oyuncu)
        {
            int kazanmaSayisi = 3;
            if (boyut > 3)
            {
                kazanmaSayisi = 4;
            }
            char karakter = oyuncu.karakteriAl();
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    if (oynTahtasi[i][j] == karakter )
                    {
                        int sayacH = 1;
                        int sayacD = 1;
                        int sayacV = 1;
                        for (int b = 1; b < kazanmaSayisi; b++)
                        {
                            if (j + b < boyut)
                            {
                                if (oynTahtasi[i][j + b] == karakter) sayacH++;
                            }

                            if (i + b < boyut)
                            {
                                if (oynTahtasi[i + b][j] == karakter) sayacV++;
                            }

                            if (i + b < boyut && j + b < boyut)
                            {
                                if (oynTahtasi[i + b][j + b] == karakter) sayacD++;
                            }
                        }
                        if (sayacD >= kazanmaSayisi || sayacH >= kazanmaSayisi || sayacV >= kazanmaSayisi)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool beraberlikKontrol()
        {
            for (int i=0;i<boyut;i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    if (oynTahtasi[i][j] == '\0')
                        return false;
                }
            }
            return true;
        }




    }
}
