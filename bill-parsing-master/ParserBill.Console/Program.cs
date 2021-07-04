using ParserBill.Console.Data;
using ParserBill.Console.Models;
using ParserBill.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill.Console
{
    public enum Period
    {
        Full,
        TwoDay,
        Custom
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var settings = GetSettings(args);
                //Settings settings = new Settings();
                //settings.StartDate = new DateTime(2019, 3, 7);
                //settings.EndDate = new DateTime(2019, 3, 10).AddSeconds(-1);
                System.Console.WriteLine($"Начинаю парсинг чеков за период " +
                    $"{settings.StartDate.ToShortDateString()} - {settings.EndDate.ToShortDateString()}");
                ParserManager parserManager = new ParserManager(settings.StartDate, settings.EndDate);
                var items = parserManager.GetBillItems();
                //Print(items);
                AddDb(items, settings);
                System.Console.WriteLine("Готово");
            }
            catch (Exception)
            {
                System.Console.WriteLine("Неверный формат аргументов");
            }

            System.Console.ReadKey();
        }

        private static Settings GetSettings(string[] args)
        {
            Settings settings = null;
            if (args.Length == 0)
            {
                settings = GetSettingsPeriodTwoDay();
            }
            else
            {
                Period period = GetPeriod(args);
                switch (period)
                {
                    case Period.Full:
                        settings = GetSettingsPeriodFull();
                        break;
                    case Period.TwoDay:
                        settings = GetSettingsPeriodTwoDay();
                        break;
                    case Period.Custom:
                        settings = GetSettingPeriodCustom(args);
                        break;
                    default:
                        break;
                }
            }
            return settings;
        }

        private static Settings GetSettingPeriodCustom(string[]args)
        {
            Settings settings = new Settings();
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            if (args.Length == 1)
            {
                endDate = DateTime.Today.AddDays(-1);
                DateTime.TryParseExact(args[0], "d.M.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            }
            else
            {
                DateTime.TryParseExact(args[0], "d.M.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                DateTime.TryParseExact(args[1], "d.M.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            }
            if (startDate>endDate)
            {
                RevertDate(ref startDate, ref endDate);
            }
            if (endDate > DateTime.Today.AddSeconds(-1) && startDate.Year < 2019)
            {
                throw new ArgumentException();
            }

            settings.StartDate = startDate;
            settings.EndDate = endDate.AddDays(1).AddSeconds(-1);

            return settings;

        }

        private static void RevertDate(ref DateTime startDate, ref DateTime endDate)
        {
            DateTime tempDate = startDate;
            startDate = endDate;
            endDate = tempDate;
        }

        private static Settings GetSettingsPeriodFull()
        {
            var setting = new Settings();
            setting.EndDate = DateTime.Today.AddSeconds(-1);
            setting.StartDate = new DateTime(2019, 1, 1);
            return setting;
        }

        private static Period GetPeriod(string[] args)
        {
            if (args[0].ToUpper() == "-F" )
            {
                return Period.Full;
            }
            if (IsDate(args[0]))
            {
                return Period.Custom;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static bool IsDate(string v)
        {
            try
            {
                DateTime dateTime = DateTime.Parse(v);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static Settings GetSettingsPeriodTwoDay()
        {
            var settings = new Settings();
            settings.EndDate = DateTime.Today.AddSeconds(-1);
            settings.StartDate = settings.EndDate.AddDays(-2);
            return settings;
        }

        private static void AddDb(List<BillItem> items, Settings settings)
        {
            BillRepository repository = new BillRepository();
            System.Console.WriteLine($"Удаляю старые данные из БД");
            repository.RemoveBills(settings.StartDate, settings.EndDate);
            List<Bill> currentBills = repository.GetBills();
            foreach (var bill in currentBills)
            {
                System.Console.WriteLine(bill.BillDate.ToShortDateString());
            }
            System.Console.WriteLine("Добавляю новые данные в БД");
            repository.AddBills(items);
        }

        private static void Print(List<ParserBill.Models.BillItem> items)
        {
            int i = 1;
            foreach (var item in items)
            {
                System.Console.WriteLine($"{i++}--{item}");
            }
        }
    }
}
