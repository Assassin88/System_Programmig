using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessInformation
{
    class ProcInfo
    {
        private IList<Process> _proc = Process.GetProcesses(".").ToList();

        public void UpdateProc()
        {
            _proc = null;
            _proc = Process.GetProcesses(".").ToList();
        }

        public void ShowProcesses()
        {
            foreach (var item in _proc.Select(p => p).OrderBy(p => p.Id))
            {
                Console.WriteLine("Id: " + item.Id + ", Name: " + item.ProcessName);
            }
        }

        public void GetProcessById(int id)
        {
            try
            {
                var proc = Process.GetProcessById(id);
                Console.WriteLine("Id: {0}, Name: {1}", proc.Id, proc.ProcessName);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Process GetProcessByName(string name)
        {
            try
            {
                var proc = Process.GetProcessesByName(name)[0];
                return proc;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void StartProc(string name)
        {
            try
            {
                Process.Start(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void KillProc(string name)
        {
            try
            {
                var proc = Process.GetProcessesByName(name)[0];
                proc.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void ShowThreadsInProcess(string name)
        {
            try
            {
                var proc = Process.GetProcessesByName(name)[0];
                ProcessThreadCollection collection = proc.Threads;
                foreach (ProcessThread item in collection)
                {
                    Console.WriteLine("Count threads in current process: {0}", collection.Count);
                    Console.WriteLine();
                    Console.WriteLine("Id: {0}; Name: {1}; Priority: {2}",
                        item.Id, item.StartAddress, item.PriorityLevel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowModulesInProcess(string name)
        {
            try
            {
                var proc = Process.GetProcessesByName(name)[0];
                ProcessModuleCollection mod = proc.Modules;
                foreach (ProcessModule item in mod)
                {
                    Console.WriteLine("Name: {0}, memorySize: {1}\n",
                        item.ModuleName, item.ModuleMemorySize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
