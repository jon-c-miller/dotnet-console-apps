using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Developer Note: In the case of course changes, updating ChoiceToCourse() data is all that is necessary. Changes will be reflected throughout. */

namespace ConsoleRegisterStudent
{
    class Program
    {
        static void Main(string[] args)
        {
            (new Program()).run();
        }

        void run()
        {
            int    choice;
            int    firstChoice = 0, secondChoice = 0, thirdChoice = 0;
            int    totalCredit = 0;
            string yesOrNo     = "";

            //loop until user chooses to quit or registers for 3 classes
            do
            {
                WritePrompt();
                //convert the user choice from string to int
                choice = Convert.ToInt32(Console.ReadLine());
                
                //pass the variables for the user choice and user selections over for comparison and validation
                switch (ValidateChoice(choice, firstChoice, secondChoice, thirdChoice, totalCredit))
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
                        
                        //'fill in' the course assignments from the bottom up by checking until an 'empty' one is found; break out of switch
                        if (firstChoice == 0)
                            firstChoice = choice;
                        else if (secondChoice == 0)
                            secondChoice = choice;
                        else if (thirdChoice == 0)
                            thirdChoice = choice;
                        break;
                }
                //provide the user with a list of registrations at each iteration
                WriteCurrentRegistration(firstChoice, secondChoice, thirdChoice);
                
                //break out of the loop once 3 classes have been registered
                if (totalCredit == 9)
                {
                    Console.WriteLine("\nYou have registered for the maximum of 9 credit hours. ");
                    break;
                }
                //prompt user for continue
                else
                {
                    Console.Write("\nContinue with registration? (Y/N): ");
                    yesOrNo = (Console.ReadLine()).ToUpper();
                }
                
                //final exit prompt for if the user hits N before registering for all classes
                if (yesOrNo == "N")
                {
                    Console.Write("You have not yet completed course registration. Continue? (Y/N): ");
                    yesOrNo = (Console.ReadLine()).ToUpper();
                }
            } while (yesOrNo == "Y");
            
            //final message before exit
            Console.WriteLine("\nThank you for registering with us.");
            Console.ReadKey();
        }

        //show available courses and prompt user for a course to register; populate using data from ChoiceToCourse
        void WritePrompt()
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

        //display registered courses to the user if active
        void WriteCurrentRegistration(int firstChoice, int secondChoice, int thirdChoice)
        {
            if (firstChoice == 0)
            {
                Console.WriteLine("\nThere are no current registrations.");
            }
            else { Console.Write("\nCurrent registrations: "); }

            if (secondChoice == 0)
            {
                Console.WriteLine("\n{0}", ChoiceToCourse(firstChoice));
            }
            else if (thirdChoice == 0)
                Console.WriteLine("\n{0}\n{1}", ChoiceToCourse(firstChoice), ChoiceToCourse(secondChoice));
            else
            {
                Console.WriteLine("\n{0}\n{1}\n{2}", ChoiceToCourse(firstChoice), ChoiceToCourse(secondChoice), ChoiceToCourse(thirdChoice));
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