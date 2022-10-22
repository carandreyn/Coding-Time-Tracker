using System;
using System.Globalization;
using System.Text;

namespace Coding_Tracker
{
    internal class GetUserInput
    {
        CodingController codingController = new();
        internal void MainMenu()
        {
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application");
                Console.WriteLine("Type 1 to View All Records");
                Console.WriteLine("Type 2 to Insert a Record");
                Console.WriteLine("Type 3 to Delete a Record");
                Console.WriteLine("Type 4 to Update a Record");
                Console.WriteLine("----------------------------");
                Console.WriteLine();

                try
                {
                    int input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 0:
                            Console.WriteLine("\nGoodbye!\n");
                            closeApp = true;
                            Environment.Exit(0);
                            break;
                        case 1:
                            codingController.Get();
                            break;
                        case 2:
                            ProcessAdd();
                            break;
                        case 3:
                            ProcessDelete();
                            break;
                        case 4:
                            ProcessUpdate();
                            break;
                        default:
                            Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                }
            }
        }

        private void ProcessAdd()
        {
            var date = GetDateInput();
            var duration = GetDurationInput();

            Coding coding = new();

            coding.Date = date;
            coding.Duration = duration;

            codingController.Post(coding);
        }

        private string GetDateInput()
        {
            Console.WriteLine("\nPlease insert the date: (Format mm-dd-yy). Type 0 to return to main menu.\n");
            string dateInput = Console.ReadLine();

            if (dateInput == "0") MainMenu();

            while (!DateTime.TryParseExact(dateInput, "MM-dd-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\nNot a valid date. Please insert the date with the format: mm-dd-yy.\n");
                dateInput = Console.ReadLine();
            }
            return dateInput;
        }
        private string GetDurationInput()
        {
            Console.WriteLine("\nPlease insert the date: (Format: hh:mm). Type 0 to return to main menu.\n");
            string durationInput = Console.ReadLine(); 

            if (durationInput == "0") MainMenu();

            while (!TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("\nNot a valid time. Please insert the duration: (Format hh:mm) or type 0 to return to main menu\n");
                durationInput = Console.ReadLine();

                if (durationInput == "0") MainMenu();
            }

            var parsedDuration = TimeSpan.Parse(durationInput);

            long date = parsedDuration.Ticks;
            if (date < 0)
            {
                Console.WriteLine("\nA negative time is not allowed.\n");
                GetDurationInput();
            }

            return durationInput;
        }

        private void ProcessDelete()
        {
            codingController.Get();

            Console.WriteLine("Please add ID of the record you want to delete. (or 0 to return to main menu).");

            string commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("Type a valid ID. (or 0 to return to main menu).");
                commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.ID == 0)
            {
                Console.WriteLine($"\nRecord with with id {id} doesn't exist.\n");
                Console.WriteLine("Please add ID of the record you want to delete. (or 0 to return to main menu).");
                commandInput = Console.ReadLine();
                id = Int32.Parse(commandInput);

                if (id == 0) MainMenu();

                coding = codingController.GetById(id);
            }
            codingController.Delete(id);
        }

        private void ProcessUpdate()
        {
            codingController.Get();

            Console.WriteLine("Please add ID of the record you want to update. (or 0 to return to main menu).");
            string commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("Type a valid ID. (or 0 to return to main menu).");
                commandInput = Console.ReadLine();  
            }
            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.ID == 0)
            {
                Console.WriteLine($"\nRecord with with id {id} doesn't exist.\n");
                ProcessUpdate();
            }

            var updateInput = "";
            bool updating = true;

            while (updating == true)
            {
                Console.WriteLine($"\nType 'd' for Date \n");
                Console.WriteLine($"\nType 't' for Duration \n");
                Console.WriteLine($"\nType 's' to save update \n");
                Console.WriteLine($"\nType '0' to go abck to main menu \n");

                updateInput = Console.ReadLine();

                switch (updateInput)
                {
                    case "d":
                        coding.Date = GetDateInput();
                        break;
                    case "t":
                        coding.Duration = GetDurationInput();
                        break;
                    case "s":
                        updating = false;
                        break;
                    case "0":
                        MainMenu();
                        updating = false;
                        break;
                    default:
                        Console.WriteLine($"\nType '0' to go abck to main menu \n");
                        break;
                }
            }
            codingController.Update(coding);
        }
    }
}