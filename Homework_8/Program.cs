using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Homework_8
{

    public delegate void SecondsUpdate(int seconds);



    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Start();
        }

        public static void Start()
        {
            var checkUserChoices = true;
            var userModeChoices = 10;
            var userSecondChoices = 10;

            var timeManager = new TimerManager();
            timeManager.OnSecondsUpdate += OnSecondsUpdate;

            do
            {
                Console.WriteLine("1 - Таймер | 2 - Секундомір");
                checkUserChoices = !Int32.TryParse(Console.ReadLine(), out userModeChoices);
            }
            while (checkUserChoices);

            do
            {
                Console.WriteLine("Введіть кількість секунд: ");
                checkUserChoices = !Int32.TryParse(Console.ReadLine(), out userSecondChoices);
            }
            while (checkUserChoices);


            timeManager.secondsCount = userSecondChoices;
            timeManager.timeMode = userModeChoices;
            timeManager.StartTimeManager();
        }

        internal static void OnSecondsUpdate(int seconds)
        {
            Console.WriteLine(seconds);
        }
    }

    internal class TimerManager
    {
        public int secondsCount;
        public int timeMode;

        public event SecondsUpdate OnSecondsUpdate;

        public void StartTimeManager()
        {
            //Task.Run(async () =>
            //{
            //    for (int i = 0; i <= secondsCount; i++)
            //    {
            //        await Task.Delay(1000);
            //        OnSecondsUpdate?.Invoke(i);
            //    }
            //});

            Console.Clear();

            if(timeMode == 1)
            {
                Console.WriteLine(0);
                for (int i = 1; i <= secondsCount; i++)
                {
                    Thread.Sleep(1000);
                    OnSecondsUpdate?.Invoke(i);
                }

                Console.WriteLine();
                Program.Start();
            }
            else if (timeMode == 2)
            {
                Console.WriteLine(secondsCount);
                for (int i = secondsCount - 1; i >= 0; i--)
                {
                    Thread.Sleep(1000);
                    OnSecondsUpdate?.Invoke(i);
                }

                Console.WriteLine();
                Program.Start();
            }

        }
    }
}