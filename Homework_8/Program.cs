using System.Diagnostics;
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

            var userChoices = new UserChoices();

            do
            {
                Console.WriteLine("1 - Таймер | 2 - Секундомір | 's' - Для зупинки");
                checkUserChoices = !Int32.TryParse(Console.ReadLine(), out userModeChoices);
            }
            while (checkUserChoices);

            do
            {
                Console.WriteLine("Введіть кількість секунд: ");
                checkUserChoices = !Int32.TryParse(Console.ReadLine(), out userSecondChoices);
            }
            while (checkUserChoices);


            userChoices.secondsCount = userSecondChoices;
            userChoices.timeMode = userModeChoices;
            timeManager.StartTimeManager(userChoices);
        }

        

        internal static void OnSecondsUpdate(int seconds)
        {
            Console.WriteLine(seconds);
        }
    }


    public class UserChoices
    {
        public int secondsCount;
        public int timeMode;
        public bool check;
    }

    internal class TimerManager
    {


        public event SecondsUpdate OnSecondsUpdate;

        public void StartTimeManager(UserChoices userChoices)
        {
            Console.Clear();



            if(userChoices.timeMode == 1)
            {
                StopManager(userChoices);

                Console.WriteLine(0);
                for (int i = 1; i <= userChoices.secondsCount; i++)
                {
                    Thread.Sleep(1000);
                    OnSecondsUpdate?.Invoke(i);

                    if (userChoices.check)
                    {
                        break;
                    }
                }

                Console.WriteLine();
                Program.Start();
            }
            else if (userChoices.timeMode == 2)
            {
                StopManager(userChoices);

                Console.WriteLine(userChoices.secondsCount);
                for (int i = userChoices.secondsCount - 1; i >= 0; i--)
                {
                    Thread.Sleep(1000);
                    OnSecondsUpdate?.Invoke(i);

                    if (userChoices.check)
                    {
                        break;
                    }
                }

                Console.WriteLine();
                Program.Start();
            }

        }

        public static void StopManager(UserChoices userChoices)
        {
            var check = false;

            Task.Run(async () =>
            {
                var tmp = Console.ReadKey();

                if (tmp.KeyChar == 's')
                {
                    userChoices.check = true;
                    Console.Clear();
                }
            });
        }
    }
}