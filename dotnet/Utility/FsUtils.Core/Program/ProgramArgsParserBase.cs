using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Helpers;

namespace FsUtils.Core.Program
{
    public abstract class ProgramArgsParserBase<TArgs>
        where TArgs : ProgramArgsBase, new()
    {
        public TArgs Parse(string[] args)
        {
            var parsedArgs = new TArgs
            {
                CurrentDirPath = args[0]
            };

            List<string> rawOpts = new List<string>();

            foreach (var arg in args)
            {
                if (arg.FirstOrDefault() == '/')
                {
                    AddSwitch(parsedArgs, arg.Substring(1).ToUpper());
                }
                else
                {
                    rawOpts.Add(arg);
                }
            }

            throw new NotImplementedException();
        }

        private void AddSwitch(
            TArgs parsedArgs, string switchStr)
        {
            (string swName, string swValue) = switchStr.SubStr(
                (str, len) => str.IndexOf('='));

            switch (swName)
            {
                case "I":
                    parsedArgs.IsInteractiveShell = true;
                    break;
                case "TS":
                    
                default:
                    throw new ArgumentException(nameof(switchStr));
            }
        }
    }
}
