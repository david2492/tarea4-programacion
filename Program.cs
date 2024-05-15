using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int[] numbers = GetNumbersFromUser();
            SortingAlgorithms sorting = new SortingAlgorithms(numbers);

            int[] sortedBubble = sorting.BubbleSort();
            int[] sortedSelection = sorting.SelectionSort();
            int[] sortedInsertion = sorting.InsertionSort();
            int[] sortedShell = sorting.ShellSort();

            Console.WriteLine("Sorted with Bubble Sort: " + string.Join(", ", sortedBubble));
            Console.WriteLine("Sorted with Selection Sort: " + string.Join(", ", sortedSelection));
            Console.WriteLine("Sorted with Insertion Sort: " + string.Join(", ", sortedInsertion));
            Console.WriteLine("Sorted with Shell Sort: " + string.Join(", ", sortedShell));

            if (!Directory.Exists("outputs"))
            {
                Directory.CreateDirectory("outputs");
            }

            WriteToFile("outputs/bubble_sort.txt", sortedBubble);
            WriteToFile("outputs/selection_sort.txt", sortedSelection);
            WriteToFile("outputs/insertion_sort.txt", sortedInsertion);
            WriteToFile("outputs/shell_sort.txt", sortedShell);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static int[] GetNumbersFromUser()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter 10 different numbers separated by spaces: ");
                string userInput = Console.ReadLine();
                string[] inputs = userInput.Split(' ');
                if (inputs.Length != 10)
                {
                    throw new ArgumentException("You must enter exactly 10 numbers.");
                }

                int[] numbers = Array.ConvertAll(inputs, int.Parse);
                if (numbers.Length != 10 || numbers.Length != numbers.Distinct().Count())
                {
                    throw new ArgumentException("You must enter 10 different numbers.");
                }

                return numbers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message + " Please try again.");
            }
        }
    }

    static void WriteToFile(string filename, int[] data)
    {
        File.WriteAllText(filename, string.Join(", ", data));
    }
}

class SortingAlgorithms
{
    private int[] numbers;

    public SortingAlgorithms(int[] numbers)
    {
        this.numbers = (int[])numbers.Clone();
    }

    public int[] BubbleSort()
    {
        int[] arr = (int[])numbers.Clone();
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        return arr;
    }

    public int[] SelectionSort()
    {
        int[] arr = (int[])numbers.Clone();
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIdx])
                {
                    minIdx = j;
                }
            }
            int temp = arr[minIdx];
            arr[minIdx] = arr[i];
            arr[i] = temp;
        }
        return arr;
    }

    public int[] InsertionSort()
    {
        int[] arr = (int[])numbers.Clone();
        int n = arr.Length;
        for (int i = 1; i < n; ++i)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
        return arr;
    }

    public int[] ShellSort()
    {
        int[] arr = (int[])numbers.Clone();
        int n = arr.Length;

        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i += 1)
            {
                int temp = arr[i];

                int j;
                for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                {
                    arr[j] = arr[j - gap];
                }

                arr[j] = temp;
            }
        }
        return arr;
    }
}