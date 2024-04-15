namespace ConsoleRegisterStudent
{
    public class Registration
    {
        // maximum of 3 possible courses to register for
        int[] registeredCourses = new int[3];
        int registeredCredits;

        public readonly int MaxCredits = 9;
        public int CurrentCredits => registeredCredits;
        public int[] RegisteredCourses => registeredCourses;

        public void Prompt()
        {
            // show available courses and prompt user for a course to register; populate using data from ChoiceToCourse
            Console.Clear();
            Console.WriteLine("Courses available for registration:");

            Console.WriteLine(CourseDatabase.GetAllCourses(registeredCourses));

            Console.Write("\nPlease enter the number of the course you wish to register for: ");
        }

        public void ValidateSelection(int choice)
        {
            // filter by registration result and provide feedback
            switch (ValidateRegistrationChoice(choice))
            {
                case RegistrationResults.InvalidChoice:
                    Console.WriteLine("Selection of '{0}' is not a recognized course.", choice);
                    break;

                case RegistrationResults.AlreadyRegistered:
                    Console.WriteLine("Course {0} has already been registered for.", CourseDatabase.GetCourseInfo(choice));
                    break;

                case RegistrationResults.RegisterSuccess:
                    Console.WriteLine("Registration confirmed for {0}.", CourseDatabase.GetCourseInfo(choice));
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
        }

        public void DisplayRegistered()
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

                Console.WriteLine("{0}", CourseDatabase.GetCourseInfo(registeredCourses[i]));
            }
        }

        RegistrationResults ValidateRegistrationChoice(int choice)
        {
            if (choice < 0 || choice > CourseDatabase.CourseCount)
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