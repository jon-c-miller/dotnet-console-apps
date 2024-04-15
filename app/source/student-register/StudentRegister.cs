namespace ConsoleRegisterStudent
{
    class StudentRegister
    {
        Registration registration;

        public void Run()
        {
            registration = new();

            // loop until user chooses to quit or registers for maximum amount of courses
            bool continueRegistration = true;
            while (continueRegistration)
            {
                registration.Prompt();

                string input = Console.ReadLine();

                // filter out non-integer input
                if (!int.TryParse(input, out int choice)) continue;

                registration.HandleSelection(choice);

                registration.DisplayRegisteredCourses();

                continueRegistration = registration.TryContinueRegistration();
            }
            
            // final message before exit
            ConsoleExtensions.PrintToConsole("Thank you for using the student self-registration system. Have a nice day!", true);
            ConsoleExtensions.PrintToConsole("Press any key to quit...", true);
            Console.ReadKey();
        }
    }
}