using Turmerik.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Turmerik.Core.Components;

namespace FsUtils.Core.Program
{
    public abstract class ProgramComponentBase<TArgs, TArgsParser>
        where TArgs : ProgramArgsBase, new()
        where TArgsParser : ProgramArgsParserBase<TArgs>, new()
    {
        protected readonly string TypeName;
        protected readonly ConsoleComponent Consl;

        public ProgramComponentBase(ConsoleComponent consoleComponent)
        {
            TypeName = this.GetType().GetTypeFullDisplayName();
            Consl = consoleComponent;
        }

        public void Run(string[] strArgs)
        {
            PrintStrArgs(strArgs);
            var args = ParseArgs(strArgs);

            PrintArgs(args);

            if (CanExecute(args))
            {
                TryRunCore(args);
            }
            else
            {
                Consl.Write("The program will not execute.", true);
            }

            AskForPromptBeforeExitProcessIfReq(args);
        }

        protected abstract void RunCore(TArgs args);

        private void TryRunCore(TArgs args)
        {
            DateTime startTime = DateTime.Now;

            try
            {
                Consl.Write($"{TextH.NwLns(2)}The program execution started.",
                    true, null, true, null, true, true);

                RunCore(args);
                TimeSpan timeSpan = DateTime.Now - startTime;

                Consl.Write($"{TextH.NwLns(2)}The program execution completed after {timeSpan.TotalSeconds} seconds.",
                    true, null, true, null, true, true);
            }
            catch (Exception exc)
            {
                TimeSpan timeSpan = DateTime.Now - startTime;

                Consl.WriteException(exc,
                    $"{TextH.NwLns(2)}The program execution crashed after {timeSpan.TotalSeconds} seconds due to an unhandled exception");
            }
        }

        private void AskForPromptBeforeExitProcessIfReq(TArgs args)
        {
            if (args.IsInteractiveShell)
            {
                Consl.Write("Press any key to exit the console.", true);
                Console.ReadKey();
            }
        }

        private bool CanExecute(TArgs args)
        {
            bool retVal = !args.IsInteractiveShell;

            if (!retVal)
            {
                Consl.Write(string.Join(TextH.NL, "Are you sure you want to go on with the program execution?",
                    "Press Y for confirmation or any other key to cancel:"), true);

                ConsoleKeyInfo key = Console.ReadKey();

                if (char.ToUpper(key.KeyChar) == 'Y')
                {
                    retVal = true;
                }
            }
            
            return retVal;
        }

        private void PrintArgs(TArgs args)
        {
            Consl.WriteJson(args,
                $"Args passed to program component {TypeName} have been parsed");
        }

        private TArgs ParseArgs(string[] strArgs)
        {
            var parser = new TArgsParser();
            var args = parser.Parse(strArgs);

            return args;
        }

        private void PrintStrArgs(string[] args)
        {
            Consl.WriteJson(args,
                $"Program component {TypeName} has been invoked with args");
        }
    }
}
