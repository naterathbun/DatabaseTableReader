using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTableReader
{
    public class MenuCLI
    {
        public void OpenMenu()
        {
            while (true)
            {
                string optionPicked = Console.ReadLine();

                Dictionary<string, Action> commands = new Dictionary<string, Action>()
                {
                    {"0", Option1 },
                    {"1", Option2 },
                    {"2", Option3 },
                    {"3", Option4 }
                };

                if (commands.ContainsKey(optionPicked))
                {
                    commands[optionPicked].Invoke();
                }
                else
                {
                    Console.WriteLine("Option Unavailable: Press Enter to Choose Again.");
                    Console.ReadLine();
                }
            }
        }

        public void Option1()
        {

        }

        public void Option2()
        {

        }

        public void Option3()
        {

        }

        public void Option4()
        {

        }


    }
}
