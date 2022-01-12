using Priority_Queue;
using System.IO;
using System;
using System.Diagnostics;
using System.Threading;


namespace PEA1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Wybierz macierz z pliku");
            Console.WriteLine("2. Losowa macierz");
            Console.Write("\r\nWybor: ");
            string choice = Console.ReadLine();
           
                switch (choice)
                {
                    case "1":
                    {
                        
                                int[,] numArray = new int[,] { };
                                int city = 0;
                                Console.Clear();
                                Console.WriteLine("Podaj nazwe pliku");
                                Console.Write("\r\nNazwa: ");
                                string filename = Console.ReadLine();
                                using (StreamReader file = new StreamReader(filename + ".txt"))
                                {

                                    string ln;
                                    city = Int32.Parse(file.ReadLine());
                                    int ft = 0; int st = 0;
                                    numArray = new int[city, city];
                                    while ((ln = file.ReadLine()) != null)
                                    {
                                        char[] separators = new char[] { ' ', '.' };
                                        string[] subs = ln.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                                        foreach (var single_number in subs)
                                        {

                                            numArray[ft, st] = Int32.Parse(single_number);
                                            st++;
                                        }
                                        st = 0;
                                        ft++;
                                    }
                                    //dla miast tych samych wpisujemy droge -1
                                    for (int i = 0; i < city; i++)
                                    {
                                        numArray[i, i] = -1;
                                    }
                                    file.Close();
                                }

                                //dynamiczna alokacja tablicy przechowywujaca miasta do odwiedzenia
                                int[] to_visit = new int[] { };
                                to_visit = new int[city - 1];
                                for (int i = 0; i < city - 1; i++)
                                {
                                    to_visit[i] = i + 1;
                                }
                                int minimal_over = 0;



                                //kolejka prioritetowa 
                                SimplePriorityQueue<ResultList> resultList = new SimplePriorityQueue<ResultList>();


                                BB bb = new BB();


                                Stopwatch stopWatch = new Stopwatch();
                                stopWatch.Start();
                                minimal_over = reduce(numArray, city);
                                ResultList newresult = new ResultList(numArray, city, to_visit, minimal_over, 0, 0, "", 0);
                                bb.bandb(newresult, resultList);
                                stopWatch.Stop();
                                Console.WriteLine("Czas: " + stopWatch.Elapsed.TotalMilliseconds + "[ms]");
                                resultList.Clear();
                                Console.WriteLine("-------------------------------------------");
                                Console.WriteLine("Jesli chcesz kontynuwać eksperyment wciśnij 1");
                                string check = Console.ReadLine();
                                
                                if (Int32.Parse(check) == 1) {
                                goto case "1";
                                 }
                                
                        
                            
                        break;
                    }
                    case "2":
                    {
                        Console.Clear();
                        Console.WriteLine("Podaj ilosc miast");
                        Console.Write("\r\nIlosc: ");
                        int city = Int32.Parse(Console.ReadLine());


                        //dynamiczna alokacja tablicy przechowywujaca miasta do odwiedzenia
                        int[] to_visit = new int[] { };
                        to_visit = new int[city - 1];
                        for (int i = 0; i < city - 1; i++)
                        {
                            to_visit[i] = i + 1;
                        }
                        int minimal_over = 0;

                        //kolejka prioritetowa 
                        SimplePriorityQueue<ResultList> resultList = new SimplePriorityQueue<ResultList>();

                        int[,] numArray = new int[,] { };
                        numArray = new int[city, city];
                        numArray = randommatrix(city);


                        BB bb = new BB();
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        int amount = 100;
                        ResultList newresult;
                        for (int i = 0; i < amount; i++)
                        {
                            numArray = randommatrix(city);
                            stopWatch.Start();
                            minimal_over = reduce(numArray, city);
                            newresult = new ResultList(numArray, city, to_visit, minimal_over, 0, 0, "", 0);
                            bb.bandb(newresult, resultList);
                            stopWatch.Stop();

                            Console.WriteLine("Czas: "+stopWatch.Elapsed.TotalMilliseconds+"[ms]");

                            //Console.WriteLine(stopWatch.Elapsed.TotalMilliseconds * 1000000);
                            stopWatch.Reset();
                            resultList.Clear();

                        }
                        Console.WriteLine("Aby wyjsc nalezy wcisnac dowolny klawisz");
                        Console.ReadKey();
                        break;
                    }
                    case "3":
                        
                    break;

                    default:

                        break;
                
                }








        }

        static int[,] randommatrix(int city) {
            int[,] numArray = new int[,] { };
            numArray = new int[city, city];
            // przypisujemy losowe wartosci dla macierzy
            Random rnd = new Random();
            for (int i = 0; i < numArray.GetLength(0); i++)
            {
                for (int j = 0; j < numArray.GetLength(1); j++)
                {
                    numArray[i, j] = rnd.Next(1, 88);
                }
            }
            //dla miast tych samych wpisujemy droge -1
            for (int i = 0; i < city; i++)
            {
                numArray[i, i] = -1;
            }

            return numArray;
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


    }
}


