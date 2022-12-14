using System;
using System.Diagnostics;

namespace ReadExcelFileFramework.Utilities
{
    class ConsoleWatch
    {
        Stopwatch diffWatch;
        Stopwatch stampWatch;
        public string ProgramId { get; set; }

        public ConsoleWatch(string programId)
        {
            diffWatch = new Stopwatch();
            stampWatch = new Stopwatch();
            this.ProgramId = programId;
        }

        public void StartAll()
        {
            this.diffWatch.Start();
            this.stampWatch.Start();
        }

        public void StopAll()
        {
            this.diffWatch.Stop();
            this.stampWatch.Stop();
        }

        public void Timestamp(string message)
        {
            TimeSpan ts = stampWatch.Elapsed;
            var elapsedTime = String.Format("o [{0:00}:{1:00}:{2:00}.{3:00}] [{4}] {5}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10, this.ProgramId, message);
            Console.WriteLine(elapsedTime);
        }

        public void Diff(string message)
        {
            diffWatch.Stop();
            TimeSpan ts = diffWatch.Elapsed;
            var elapsedTime = String.Format("- [{0:00}:{1:00}:{2:00}.{3:00}] [{4}] {5}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10, this.ProgramId, message);
            Console.WriteLine(elapsedTime);
            diffWatch.Restart();
        }

        public void All(string message)
        {
            this.Timestamp(message);
            this.Diff(message);
        }
    }
}
