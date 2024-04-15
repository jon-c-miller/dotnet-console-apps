namespace ConsoleRegisterStudent
{
    public class Registration
    {
        // initialize course selection collection
        public Registration() => registeredCourses = new int[maxCourses];

        int[] registeredCourses;    // stores student selection for courses
        int registeredCredits;      // tracks currently registered credits
        int creditPerCourse = 3;    // determines credit given per course
        int maxCourses = 3;         // limits maximum amount of courses a student can register for

        public int MaxCredits => creditPerCourse * registeredCourses.Length;
        public int CurrentCredits => registeredCredits;

        public void Prompt()
        {
            // show available courses and prompt user for a course to register; populate using data from ChoiceToCourse
            Console.Clear();
            PrintLine("Courses available for registration:");
            PrintLine(CourseDatabase.GetAllCourses(registeredCourses), false, true);
            PrintLine($"Please enter the id of the course you wish to register for (1 - {CourseDatabase.CourseCount}): ", true);
        }

        public void HandleSelection(int choice)
        {
            // filter by registration result and provide feedback
            switch (ValidateRegistrationChoice(choice))
            {
                case RegistrationResults.InvalidChoice:
                    PrintLine($"'{choice}' is not a valid course id. ", true);
                    break;

                case RegistrationResults.AlreadyRegistered:
                    bool unregister = YesOrNoPrompt($"Course {CourseDatabase.GetCourseInfo(choice)} has already been registered for. Unregister? (Y/N): ");
                    if (unregister)
                    {
                        // find the registered course in the collection and set it back to 0
                        for (int i = 0; i < registeredCourses.Length; i++)
                        {
                            if (registeredCourses[i] == choice)
                            {
                                registeredCourses[i] = 0;
                                registeredCredits -= creditPerCourse;
                                break;
                            }
                        }
                    }
                    break;

                case RegistrationResults.RegisterSuccess:
                    PrintLine($"Registered for {CourseDatabase.GetCourseInfo(choice)}. ", true);
                    registeredCredits += creditPerCourse;
                    
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
        }

        public void DisplayRegistered()
        {
            PrintLine("Current Registrations: ");

            if (registeredCourses[0] == 0)
            {
                PrintLine("<None>", false, true);
                return;
            }

            // display all registered courses on the same line separated by commas
            int displayedCourses = 0;
            for (int i = 0; i < registeredCourses.Length; i++)
            {
                if (registeredCourses[i] == 0) continue;

                displayedCourses++;
                if (displayedCourses > 1)
                    Console.Write(", ");
                
                Console.Write("{0}", CourseDatabase.GetCourseInfo(registeredCourses[i]));
            }
            PrintLine("", true);
        }

        public bool YesOrNoPrompt(string promptText, bool endlineBefore = false)
        {
            // end line when first entering the prompt
            if (endlineBefore)
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

        void PrintLine(string message, bool endlineBefore = false, bool endlineAfter = false)
        {
            // print the given message with optional line spacing before and after
            if (endlineBefore)
                Console.Write("\n");

            Console.Write(message);

            if (endlineAfter)
                Console.Write("\n");
        }

        RegistrationResults ValidateRegistrationChoice(int choice)
        {
            if (choice < 1 || choice > CourseDatabase.CourseCount)
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
                        return RegistrationResults.AlreadyRegistered;
                }
            }

            return RegistrationResults.RegisterSuccess;
        }
    }

    enum RegistrationResults
    {
        InvalidChoice,
        AlreadyRegistered,
        RegisterSuccess,
    }
}