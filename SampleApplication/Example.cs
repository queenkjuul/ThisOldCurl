using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApplication
{
    public interface IExample
    {
        bool PrintAndPause();
        void Run();
        bool Demo();
        string Description { get; }
    }

    public abstract class Example : IExample
    {
        protected string name;
        protected string description;
        public bool PromptContinue() { return PromptContinue(null); }
        public bool PromptContinue(string prompt)
        {
            Console.WriteLine(prompt ?? "Continue? [y/N]");
            ConsoleKeyInfo keyinfo = Console.ReadKey();
            if (keyinfo.Key != ConsoleKey.Y)
                return false;
            return true;
        }
        public bool PrintAndPause()
        {
            Console.Clear();
            Console.WriteLine(this.name);
            Console.WriteLine(this.Description);
            Console.WriteLine();
            return PromptContinue("Run this demo? [y/N]");   
        }
        public abstract void Run();
        public bool Demo()
        {
            if (PrintAndPause())
            {
                Run();
                Console.WriteLine();
                Console.WriteLine("Demo Complete: " + this.name);
                Console.WriteLine();
                if (PromptContinue())
                    return true;
                else
                    Environment.Exit(0);
            }
            return true;
        }
        public string Description
        {
            get { return this.description; }
        }
        public string Name
        {
            get { return this.name; }
        }
    }
}
