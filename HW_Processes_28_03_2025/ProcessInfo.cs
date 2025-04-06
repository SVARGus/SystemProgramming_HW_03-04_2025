using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Processes_28_03_2025
{
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Process Process { get; set; }
        public ProcessInfo(Process process)
        {
            Process = process;
            Id = process.Id;
            Name = process.ProcessName;
        }
    }
}
