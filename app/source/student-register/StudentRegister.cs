namespace ConsoleRegisterStudent
{
    class StudentRegister
    {
        // maximum of 3 possible courses to register for
        int[] registeredCourses = new int[3];
        int registeredCredits = 0;

        enum RegistrationResults
        {
            InvalidChoice,
            AlreadyRegistered,
            RegisterSuccess,
        }

        public void Run()
        {
            // loop until user chooses to quit or registers for 3 classes
            bool continueRegistration = true;
            while (continueRegistration)
            {
                PromptForRegistration();

                string input = Console.ReadLine();

                // filter out non-integer input
                if (!int.TryParse(input, out int choice))
                {
                    continue;
                }
                
                // filter by registration result and provide feedback
                switch (ValidateRegistrationChoice(choice))
                {
                    case RegistrationResults.InvalidChoice:
                        Console.WriteLine("Selection of '{0}' is not a recognized course.", input);
                        break;

                    case RegistrationResults.AlreadyRegistered:
                        Console.WriteLine("Course {0} has already been registered for.", Database.GetCourseInfo(choice));
                        break;

                    case RegistrationResults.RegisterSuccess:
                        Console.WriteLine("Registration confirmed for {0}.", Database.GetCourseInfo(choice));
                        registeredCredits += 3;
                        
                        // 'fill in' the course assignment slots as courses are registered
                        for (int i = 0; i < registeredCourses.Length; i++)
                        {
                            if (registeredCourses[i] == 0)
                            {
                                registeredCourses[i] = choice;
                                break;
                            }
                        }
                        break;
                }

                // provide the user with a list of registrations at each iteration
                DisplayRegisteredCourses();

                // continue registering or end registration
                if (registeredCredits == 9)
                {
                    Console.WriteLine("\nYou have registered for the maximum of 9 credit hours. ");
                    break;
                }
                else continueRegistration = YesOrNoPrompt("Continue with registration? (Y/N): ");
                
                // final exit prompt for if the user tries to quit before registering for all classes
                if (!continueRegistration)
                {
                    continueRegistration = YesOrNoPrompt("Course registration has not been completed. Return to registration? (Y/N): ");
                }
            }
            
            // final message before exit
            Console.WriteLine("\nThank you for using the student self-registration system. Have a nice day!");
            Console.ReadKey();
        }

        bool YesOrNoPrompt(string promptText)
        {
            // end line when first entering the prompt
            Console.Write("\n");

            // loop until the user gives y or n
            string continueChoice;
            while (true)
            {
                Console.Write($"{promptText}");
                continueChoice = Console.ReadLine().ToUpper();
                if (continueChoice == "N")
                    return false;
                else if (continueChoice == "Y")
                    return true;
                else continue;
            }
        }

        void PromptForRegistration()
        {
            // show available courses and prompt user for a course to register; populate using data from ChoiceToCourse
            Console.Clear();
            Console.WriteLine("Courses available for registration:");

            Console.WriteLine(Database.GetAllCourses());

            Console.Write("\nPlease enter the number of the course you wish to register for: ");
        }

        RegistrationResults ValidateRegistrationChoice(int choice)
        {
            if (choice < 0 || choice >= Database.CourseCount)
            {
                // reject choice if course number is invalid
                return RegistrationResults.InvalidChoice;
            }
            else
            {
                // reject choice if course already registered
                for (int i = 0; i < registeredCourses.Length; i++)
                {
                    if (registeredCourses[i] == choice)
                    {
                        return RegistrationResults.AlreadyRegistered;
                    }
                }
            }

            return RegistrationResults.RegisterSuccess;
        }

        void DisplayRegisteredCourses()
        {
            Console.Write("\nCurrent registrations: ");
            Console.WriteLine("\n");

            if (registeredCourses[0] == 0)
            {
                Console.WriteLine("<None>");
                return;
            }

            for (int i = 0; i < registeredCourses.Length; i++)
            {
                if (registeredCourses[i] == 0) continue;

                Console.WriteLine("{0}", Database.GetCourseInfo(registeredCourses[i]));
            }
        }
    }
}