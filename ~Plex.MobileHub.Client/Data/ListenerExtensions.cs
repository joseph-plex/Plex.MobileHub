using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace MobileHubClient.Data
{
    public static class ListenerExtensions
    {
        public static IEnumerable<string> LsnrCtlGetStatus(this Listener listener)
        {
            string file = Path.GetTempFileName();
            ProcessStartInfo Psi = new ProcessStartInfo();
            Psi.FileName = "cmd.exe";
            Psi.Arguments = @"/C * status >> ".Replace("*", listener.Executable) + file;
            Psi.WindowStyle = ProcessWindowStyle.Hidden;

            using (var Process = new Process() { StartInfo = Psi })
            {
                Process.Start();
                Process.WaitForExit();
            }

            var output = File.ReadAllLines(file);
            File.Delete(file);
            return output;
        }

        public static IEnumerable<string> ExtractEndPointSummary(this Listener listener)
        {
            return Listener.ExtractEndPointSummary(listener.LsnrCtlGetStatus());
        }

        public static IEnumerable<string> ExtractServiceNames(this Listener listener)
        {
            return Listener.ExtractServiceNames(listener.LsnrCtlGetStatus());
        }

    }
}
