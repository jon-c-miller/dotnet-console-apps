namespace ConsoleRegisterStudent
{
    class StudentRegister
    {
        Registration registration;

        public void Run()
        {
            registration = new();

            // loop until user chooses to quit or registers for 3 classes
            bool continueRegistration = true;
            while (continueRegistration)
            {
                registration.Prompt();

                string input = Console.ReadLine();

                // filter out non-integer input
                if (!int.TryParse(input, out int choice)) continue;

                registration.HandleSelection(choice);
                
                // provide the user with a list of registrations at each iteration
                registration.DisplayRegistered();

                // continue registering or end registration
                if (registration.CurrentCredits == registration.MaxCredits)
                {
                    ConsoleExtensions.PrintToConsole($"You have registered for the maximum of {registration.MaxCredits} credit hours. ", true);
                    break;
                }
                else continueRegistration = ConsoleExtensions.YesOrNoPrompt("Continue with registration? (Y/N): ", true);
                
                // final exit prompt for if the user tries to quit before registering for all classes
                if (!continueRegistration)
                {
                    continueRegistration = ConsoleExtensions.YesOrNoPrompt("Course registration has not been completed. Return to registration? (Y/N): ");
                }
            }
            
            // final message before exit
            ConsoleExtensions.PrintToConsole("Thank you for using the student self-registration system. Have a nice day!", true);
            ConsoleExtensions.PrintToConsole("Press any key to quit...", true);
            Console.ReadKey();
        }
    }
}