using System;
using System.Collections.Generic;
using System.Text;

namespace PEA1
{
    class ResultList
    {
        //miasta do odwiedzenia
        public int[] to_visit = new int[] { };
        //macierz przedstawiająca aktualny stan dla węzła
        public int[,] numArray = new int[,] { };
        //droga pokonana
        public string road = "0";
        //rodzic dla aktualnego wezla
        public int parent = 0;
        //aktualne miasto
        public int current_city = 0;
        //droga jaka aktualnie juz pokonano 
        public int minimal_road = 0;
        //ilosc miast
        public int city = 0;

        public int max = 9999999;
        public ResultList(int[,] numArray, int city, int[] to_visit, int minimal_road, int current_city,int parent,string road,int max)
        {
            this.numArray = numArray;
            this.city = city;
            this.to_visit = to_visit;
            this.minimal_road = minimal_road;
            this.current_city = current_city;
            this.parent = parent;
            this.road = road;
            this.max = max;
            
        }
        public ResultList() { 
        }

        public void set_parent(int parent) {
            this.parent = parent;
        }


    }
}
