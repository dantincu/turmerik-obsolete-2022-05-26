using System;
using System.Collections.Generic;
using System.Text;

namespace FsUtils.Core.Program
{
    public abstract class ProgramArgsBase
    {
        public bool IsInteractiveShell { get; set; }
        public string CurrentDirPath { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
