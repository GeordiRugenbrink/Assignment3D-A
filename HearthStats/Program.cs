using System;
using System.Linq;

namespace HearthStats
{
    class Program
    {
        static States[,] fsm_robber = new States[4, 6];
        static States currentState = States.ROBBING_BANK;
        static Events currentEvent = Events.GET_RICH;

        enum States{
            ROBBING_BANK,
            HAVING_GOOD_TIME,
            LAYING_LOW,
            FLEEING
        };

        enum Events {
            GET_RICH,
            SPOT_COP,
            GET_TIRED,
            FEEL_SAFE,
            GET_POOR,
            GREED
        }; 

        static void Main(string[] args)
        {
            SetPossibleStateCombinations();
            string command = "";
            //This keeps the program running
            while (true)
            {
                //the input of the user
                command = Console.ReadLine();
                //Parses the command of the user to output from the robber
                ParseCommand(command);
                //Updates the current state of the robber
                UpdateRobberState();
            }
        }
        /// <summary>
        /// Changes the current state of the robber
        /// </summary>
        static void UpdateRobberState()
        {
            currentState = fsm_robber[(int)currentState, (int)currentEvent];
            Console.WriteLine(currentState.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">The command the user gives as input</param>
        static void ParseCommand(string command)
        {
            //Create a temporary command which is in lower case with no empty spaces for error prevention
            string tempCommand = command.ToLower().Replace(" ", string.Empty);
            //Create an empty line for formatting
            string outputLine = "\n";

            switch (tempCommand)
            {
                default:
                    outputLine += "Robber: 'I don't understand this command.";
                    break;
                case "getrich":
                    currentEvent = Events.GET_RICH;
                    break;
                case "spotcop":
                    currentEvent = Events.SPOT_COP;
                    break;
                case "gettired":
                    currentEvent = Events.GET_TIRED;
                    break;
                case "feelsafe":
                    currentEvent = Events.FEEL_SAFE;
                    break;
                case "getpoor":
                    currentEvent = Events.GET_POOR;
                    break;
                case "greed":
                    currentEvent = Events.GREED;
                    break;
            }
            //outputs the line of the robber
            Console.WriteLine(outputLine + "\n");
        }

        static void SetPossibleStateCombinations()
        {
            //Having good time
            fsm_robber[(int)States.ROBBING_BANK, (int)Events.GET_RICH] = States.HAVING_GOOD_TIME;

            //Robbing bank
            fsm_robber[(int)States.HAVING_GOOD_TIME, (int)Events.GET_POOR] = States.ROBBING_BANK;
            fsm_robber[(int)States.HAVING_GOOD_TIME, (int)Events.GREED] = States.ROBBING_BANK;
            fsm_robber[(int)States.FLEEING, (int)Events.FEEL_SAFE] = States.ROBBING_BANK;
            fsm_robber[(int)States.LAYING_LOW, (int)Events.FEEL_SAFE] = States.ROBBING_BANK;

            //Fleeing
            fsm_robber[(int)States.HAVING_GOOD_TIME, (int)Events.SPOT_COP] = States.FLEEING;
            fsm_robber[(int)States.ROBBING_BANK, (int)Events.SPOT_COP] = States.FLEEING;
            
            //Laying low
            fsm_robber[(int)States.HAVING_GOOD_TIME, (int)Events.GET_TIRED] = States.LAYING_LOW;
            fsm_robber[(int)States.FLEEING, (int)Events.GET_TIRED] = States.LAYING_LOW;
        }
    }
}