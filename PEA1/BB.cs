using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA1
{
    class BB
    {


        //static int find_close_town(int[,] numArray, int city, int[] to_visit, int parent, int minimal_over)
        static ResultList find_minimal_road(ResultList result, SimplePriorityQueue<ResultList> resultLists)
        {
            
            //klon macierzy ktora przyda sie do badania kolejnych wezlow poprzez przywrocenia starej wersji
            int[,] copyArray = (int[,])result.numArray.Clone();
            //ustawio jako -1 caly wiersz dla rodzica
            for (int j = 0; j < result.city; j++)
            {
                result.numArray[result.parent, j] = -1;
            }
            int town = -1;

            //klon macierzy na ktorym zostana wykonane dzialania konieczne do wyznaczenia minimalnej drogi
            int[,] clonedArray = (int[,])result.numArray.Clone();
            //klon tablicy ktory zostanie wykorzystany do przekazania zmienionej tablicy to_visit dla aktualnego wezla a nastepnie przywrocony do starej wersji do badania innych wierzcholkow
            int[] copy_to_visit = (int[])result.to_visit.Clone();

            int numIndex = 0;


            //badanie potomkow i dodanie ich do kolejki prioritetowej
            for (int d = 0; d < result.to_visit.Length; d++)
            {
                int range_to_town = result.minimal_road;
                town = result.to_visit[d];
                range_to_town += copyArray[result.parent, town];
                clonedArray[town, 0] = -1;          //to jest powrot
                
                
                    for (int i = 0; i < result.city; i++)
                    {
                        clonedArray[i, town] = -1;
                    }
                
                range_to_town += reduce(clonedArray, result.city);
                numIndex = Array.IndexOf(copy_to_visit, town);
                copy_to_visit = copy_to_visit.Where((val, idx) => idx != numIndex).ToArray();
                
                if(range_to_town <= result.max)
                {
                    resultLists.Enqueue(new ResultList(clonedArray, result.city, copy_to_visit, range_to_town, town, result.parent, (result.road + town.ToString()) + "-",result.max), range_to_town);

                }
                copy_to_visit = result.to_visit;
                
                clonedArray = (int[,])result.numArray.Clone();
            }


            resultLists.First.set_parent(resultLists.First.current_city);
            //zwrocenie miasta o najmniejszej odleglosci
            return resultLists.Dequeue();
            

        }

        static int reduce(int[,] numArray, int city)
        {
            
            int minimal_overall_road = 0;
            int minimal_road = 0;
            int min_is = -2;
            //szukanie wartosci minimalnej w wierszu
            for (int i = 0; i < city; i++)
            {
                for (int j = 0; j < city; j++) 
                {
                    if (numArray[i, j] != -1)
                    {
                        if (min_is == -2)
                        {
                            min_is = j;
                            minimal_road = numArray[i, j];
                        }
                        else if (numArray[i, j] < numArray[i, min_is])
                        {
                            min_is = j;
                            minimal_road = numArray[i, j];
                        }

                    }

                }
                minimal_overall_road += minimal_road;
                //odejmowanie od wiersza minimalnej wartosci w przypadku gdy wartosc wynosi 0 nic sie nie dzieje
                if (minimal_road != 0)
                {
                    for (int c = 0; c < city; c++) 
                    {
                        if (numArray[i, c] != -1)
                        {
                            numArray[i, c] -= minimal_road;
                        }
                    }
                }

                minimal_road = 0;


                min_is = -2;
            }



            minimal_road = 0;
            min_is = -2;
            //szukanie wartosci minimalnej w kolumnie
            for (int j = 0; j < city; j++)
            {
                for (int i = 0; i < city; i++)
                {
                    if (numArray[i, j] != -1)
                    {
                        if (min_is == -2)
                        {
                            min_is = i;
                            minimal_road = numArray[i, j];
                        }
                        else if (numArray[i, j] < numArray[min_is, j])
                        {
                            min_is = i;
                            minimal_road = numArray[i, j];
                        }
                    }
                }
                minimal_overall_road += minimal_road;
                //odejmowanie od kolumny minimalnej wartosci w przypadku gdy wartosc wynosi 0 nic sie nie dzieje
                if (minimal_road != 0)
                {
                    for (int c = 0; c < city; c++) 
                    {
                        if (numArray[c, j] != -1)
                        {
                            numArray[c, j] -= minimal_road;
                        }
                    }
                }
                minimal_road = 0;


                min_is = -2;


            }


            return minimal_overall_road;
        }


        static int fin_max(ResultList result)
        {
            
            //klon macierzy ktora przyda sie do badania kolejnych wezlow poprzez przywrocenia starej wersji
            int[,] copyArray = (int[,])result.numArray.Clone();
            //ustawio jako -1 caly wiersz dla rodzica
            for (int j = 0; j < result.city; j++)
            {
                result.numArray[result.parent, j] = -1;
            }
            int town = -1;

            //klon macierzy na ktorym zostana wykonane dzialania konieczne do wyznaczenia minimalnej drogi
            int[,] clonedArray = (int[,])result.numArray.Clone();
            //klon tablicy ktory zostanie wykorzystany do przekazania zmienionej tablicy to_visit dla aktualnego wezla a nastepnie przywrocony do starej wersji do badania innych wierzcholkow
            int[] copy_to_visit = (int[])result.to_visit.Clone();

            int numIndex = 0;
            int[,] closestArray = (int[,])result.numArray.Clone();
            int minimall_overall = 99999999;
            int road = 0;
            
            for (int d = 0; d < result.to_visit.Length; d++)
            {
                int range_to_town = result.minimal_road;
                town = result.to_visit[d];
                range_to_town += copyArray[result.parent, town];
                clonedArray[town, 0] = -1;          //to jest powrot


                for (int i = 0; i < result.city; i++)
                {
                    clonedArray[i, town] = -1;
                }

                range_to_town += reduce(clonedArray, result.city);
                if(range_to_town<minimall_overall)
                {
                    minimall_overall = range_to_town;
                    closestArray = (int[,])clonedArray.Clone();
                    road = town;
                    numIndex = town;
                }
               

                
                copy_to_visit = result.to_visit;

                clonedArray = (int[,])result.numArray.Clone();
            }
            
            numIndex = Array.IndexOf(copy_to_visit, numIndex);
            copy_to_visit = copy_to_visit.Where((val, idx) => idx != numIndex).ToArray();

            if (result.to_visit.Length != 0) {
                result.minimal_road = minimall_overall;
                result.numArray = closestArray;
                result.road += road.ToString();
                result.parent = road;
                result.to_visit = copy_to_visit;
                fin_max(result);
            }
            
            return result.minimal_road;


        }
        public  void bandb(ResultList nr, SimplePriorityQueue<ResultList> resultLists )
        {
            int[,] clonedArray = (int[,])nr.numArray.Clone();
            //proba optymalizacji poprzez szybkie wyszukanie maximum
            ResultList test = new ResultList(nr.numArray,nr.city,nr.to_visit,nr.minimal_road,nr.current_city,nr.parent,nr.road,0);
            int max = fin_max(test);
            ResultList minimal = new ResultList(clonedArray, nr.city, nr.to_visit, nr.minimal_road, nr.current_city, nr.parent, nr.road,9999999);
            minimal.max = max;
            //petla w której szukamy najkrótszej drogi 
            while (true)
            {

                minimal = find_minimal_road(minimal, resultLists);
                if (minimal.to_visit.Length == 0)
                {
                    
                   Console.WriteLine("Droga pokonana: "+"0-" + minimal.road + "0");
                   Console.WriteLine("Minimalny koszt: "+minimal.minimal_road);
                    return;
                }
            }
            
            
        }

    }
}
