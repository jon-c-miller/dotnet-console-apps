namespace ConsoleRegisterStudent
{
    class StudentRegister
    {
        // maximum of 3 possible courses to register for
        int[] registeredCourses = new int[3];

        public void Run()
        {
            int    totalCredit = 0;

            //loop until user chooses to quit or registers for 3 classes
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
                
                //pass the variables for the user choice and user selections over for comparison and validation
                switch (choice)
                {
                    //invalid selection
                    case -1:
                        Console.WriteLine("Your entered selection of '{0}' is not a recognized course.", choice);
                        break;
                    //duplicate selection
                    case -2:
                        Console.WriteLine("You have already registered for course {0}.", ChoiceToCourse(choice));
                        break;
                    //registration confirmation
                    case 0:
                        Console.WriteLine("Registration confirmed for course {0}.", ChoiceToCourse(choice));
                        totalCredit += 3;

                        // 'fill in' the course assignments as courses are registered
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

                //break out of the loop once 3 classes have been registered
                if (totalCredit == 9)
                {
                    Console.WriteLine("\nYou have registered for the maximum of 9 credit hours. ");
                    break;
                }
                else continueRegistration = YesOrNoPrompt("Continue with registration? (Y/N): ");
                
                //final exit prompt for if the user hits N before registering for all classes
                if (!continueRegistration)
                {
                    continueRegistration = YesOrNoPrompt("You have not yet completed course registration. Continue? (Y/N): ");
                }
            }
            
            //final message before exit
            Console.WriteLine("\nThank you for registering with us.");
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

        // show available courses and prompt user for a course to register; populate using data from ChoiceToCourse
        void PromptForRegistration()
        {
            Console.Clear();
            Console.WriteLine("Courses available for registration: \n\n[1] {0} \n[2] {1} \n[3] {2} \n[4] {3} \n[5] {4} \n[6] {5} \n[7] {6}",
                "" + ChoiceToCourse(1), ChoiceToCourse(2), ChoiceToCourse(3),
                "" + ChoiceToCourse(4), ChoiceToCourse(5), ChoiceToCourse(6),
                "" + ChoiceToCourse(7));

            Console.Write("\nPlease enter the number of the course you wish to register for: ");
        }

        //validation for user input before assigning courses
        int ValidateChoice(int choice, int firstChoice, int secondChoice, int thirdChoice, int totalCredit)
        {
            //reject user input as an invalid number
            if (choice < 1 || choice > 7)
                return -1;
            //compare the current choice against each of the registered courses
            else if (choice == firstChoice || choice == secondChoice || choice == thirdChoice)
                return -2;
            return 0;
        }

        void DisplayRegisteredCourses()
        {
            if (registeredCourses[0] == 0)
            {
                Console.WriteLine("\nThere are no current registrations.");
            }
            else
            {
                Console.Write("\nCurrent registrations: ");

                if (registeredCourses[1] == 0)
                {

                }
                else if (registeredCourses[2] == 0)
                {

                }
                else
                {
                    
                }
            }
        }

        //hold course name information for referencing
        string ChoiceToCourse(int choice)
        {
            string course = "";
            switch (choice)
            {
                case 1:
                    course = "IT 145";
                    break;
                case 2:
                    course = "IT 200";
                    break;
                case 3:
                    course = "IT 201";
                    break;
                case 4:
                    course = "IT 270";
                    break;
                case 5:
                    course = "IT 315";
                    break;
                case 6:
                    course = "IT 328";
                    break;
                case 7:
                    course = "IT 330";
                    break;
                default:
                    break;
            }
            return course;
        }
    }
}