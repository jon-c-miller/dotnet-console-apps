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

        int maxCredits => creditPerCourse * registeredCourses.Length;

        public void Prompt()
        {
            // show available courses and prompt user for a course to register; populate using data from ChoiceToCourse
            Console.Clear();
            ConsoleExtensions.PrintToConsole("-- Course List --");
            ConsoleExtensions.PrintToConsole(CourseDatabase.GetAllCourses(registeredCourses), false, true);
            ConsoleExtensions.PrintToConsole($"Please enter a course id (1 - {CourseDatabase.CourseCount}): ", true);
        }

        public void HandleSelection(int choice)
        {
            // filter by registration result and provide feedback
            switch (ValidateRegistrationChoice(choice))
            {
                case RegistrationResults.InvalidChoice:
                    ConsoleExtensions.PrintToConsole($"'{choice}' is not a valid course id. ", true);
                    break;

                case RegistrationResults.AlreadyRegistered:
                    bool unregister = ConsoleExtensions.YesOrNoPrompt($"Course {CourseDatabase.GetCourseInfo(choice)} has already been registered for. Unregister? (Y/N): ");
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

                case RegistrationResults.CanRegister:
                    if (registeredCredits < maxCredits)
                    {
                        bool confirm = ConsoleExtensions.YesOrNoPrompt($"Register for {CourseDatabase.GetCourseInfo(choice)}? (Y/N): ", true);
                        if (confirm)
                        {
                            ConsoleExtensions.PrintToConsole($"Course registered. ", true);
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
                        }
                    }
                    else ConsoleExtensions.PrintToConsole("The maximum number of courses has been registered for. Please unregister for a course first. ", true);
                    break;
            }
        }

        public void DisplayRegisteredCourses()
        {
            ConsoleExtensions.PrintToConsole("Current Registrations: ", true);

            if (registeredCourses[0] == 0)
            {
                ConsoleExtensions.PrintToConsole("<None>", false, true);
                return;
            }

            // display all registered courses on the same line separated by commas
            int displayedCourses = 0;
            for (int i = 0; i < registeredCourses.Length; i++)
            {
                if (registeredCourses[i] == 0) continue;

                displayedCourses++;
                if (displayedCourses > 1)
                    ConsoleExtensions.PrintToConsole(", ");
                
                ConsoleExtensions.PrintToConsole($"{CourseDatabase.GetCourseInfo(registeredCourses[i])}");
            }
            ConsoleExtensions.PrintToConsole("", true);
        }

        public bool TryContinueRegistration()
        {
            // continue registering or end registration
            bool continueRegistering = true;
            if (registeredCredits == maxCredits)
            {
                // confirm finalizing the selected courses
                bool finalize = false;
                ConsoleExtensions.PrintToConsole($"You have registered for the maximum of {maxCredits} credit hours. ", true);
                finalize = ConsoleExtensions.YesOrNoPrompt("Finalize course selections? (Y/N): ", true);

                // give user a chance to reset the course registrations
                if (!finalize)
                {
                    bool reset = ConsoleExtensions.YesOrNoPrompt("Reset course registrations? (Y/N): ", true);
                    if (reset)
                    {
                        registeredCredits = 0;
                        for (int i = 0; i < registeredCourses.Length; i++)
                            registeredCourses[i] = 0;

                        ConsoleExtensions.PrintToConsole("Course registrations reset. Press any key to continue...");
                        Console.ReadKey();
                    }
                    return true;
                }
                else return false;
            }
            else continueRegistering = ConsoleExtensions.YesOrNoPrompt("Continue with registration? (Y/N): ", true);

            // final exit prompt for if the user tries to quit before registering for all classes
            if (!continueRegistering)
            {
                return ConsoleExtensions.YesOrNoPrompt("Course registration has not been completed. Return to registration? (Y/N): ");
            }
            return true;
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

            return RegistrationResults.CanRegister;
        }
    }

    enum RegistrationResults
    {
        InvalidChoice,
        AlreadyRegistered,
        CanRegister,
        Null,
    }
}