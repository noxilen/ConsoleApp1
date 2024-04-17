using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM02_520_Bekbotov_NUMB23
{
    class TransportProblemSolver
    {
        static void Main()
        {
            try
            {
                Console.Write("Введите количество поставщиков: ");
                int m = int.Parse(Console.ReadLine());
                Console.Write("Введите количество потребителей: ");
                int n = int.Parse(Console.ReadLine());

                int[] supply = new int[m];
                int[] demand = new int[n];

                for (int i = 0; i < m; i++)
                {
                    Console.Write("Введите объем поставки от поставщика {0}: ", i + 1);
                    supply[i] = int.Parse(Console.ReadLine());
                }

                for (int j = 0; j < n; j++)
                {
                    Console.Write("Введите объем потребности у потребителя {0}: ", j + 1);
                    demand[j] = int.Parse(Console.ReadLine());
                }

                int[,] cost = new int[m, n];

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("Введите транспортную затрату для поставщика {0} и потребителя {1}: ", i + 1, j + 1);
                        cost[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                int[,] plan = SolveNorthWestCorner(supply, demand, cost);
                int totalCost = CalculateTotalCost(plan, cost);
                PrintMatrix(plan);
                Console.WriteLine("Общие затраты на перевозку грузов: {0}", totalCost);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        // Решение транспортной задачи методом северо-западного угла
        static int[,] SolveNorthWestCorner(int[] supply, int[] demand, int[,] cost)
        {
            int m = supply.Length;
            int n = demand.Length;
            int[,] plan = new int[m, n];

            int i = 0;
            int j = 0;

            while (i < m && j < n)
            {
                int quantity = Math.Min(supply[i], demand[j]);

                plan[i, j] = quantity;
                supply[i] -= quantity;
                demand[j] -= quantity;

                if (supply[i] == 0)
                {
                    i++;
                }
                if (demand[j] == 0)
                {
                    j++;
                }
            }

            return plan;
        }

        static int CalculateTotalCost(int[,] plan, int[,] cost)
        {
            int m = plan.GetLength(0);
            int n = plan.GetLength(1);
            int totalCost = 0;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    totalCost += plan[i, j] * cost[i, j];
                }
            }

            return totalCost;
        }

        // Вывод матрицы на экран
        static void PrintMatrix(int[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0}\t", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}