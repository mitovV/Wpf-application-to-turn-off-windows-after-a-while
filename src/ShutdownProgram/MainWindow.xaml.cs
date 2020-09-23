namespace ShutdownProgram
{
    using System.Diagnostics;
    using System.Windows;

    public partial class MainWindow : Window
    {
        private const string ProgramName = "cmd.exe";
        private const string ShutdownCmd = "/c shutdown /s /t {0}";
        private const string AbortCmd = "/c shutdown /a";
        private const string EmptyStr = "Няма";
        private const string HalfHourStr = "30 минути";
        private const string HourStr = "1 час";
        private const string TwoHoursStr = "2 часа";
        private const int HalfHour = 30;
        private const int Hour = 1;
        private const int TwoHours = 2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Shutdown(object sender, RoutedEventArgs e)
        {
            var chkBoxValue = ChkBox.Text;

            switch (chkBoxValue)
            {
                case EmptyStr:
                    if (!int.TryParse(Hours.Text, out int hours))
                    {
                        hours = 0;
                    }

                    if (!int.TryParse(Minutes.Text, out int minutes))
                    {
                        minutes = 0;
                    }

                    this.ProcessCmd(hours, minutes, "Shutdown");
                    break;
                case HalfHourStr:
                    this.ProcessCmd(default, HalfHour, "Shutdown");
                    break;
                case HourStr:
                    this.ProcessCmd(Hour, default, "Shutdown");
                    break;
                case TwoHoursStr:
                    this.ProcessCmd(TwoHours, default, "Shutdown");
                    break;
            }
        }

        private void Abort(object sender, RoutedEventArgs e)
        {
            this.ProcessCmd(default, default, "Abort");
        }

        private void ProcessCmd(int hour, int minutes, string command)
        {
            using Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            var cmd = string.Empty;

            if (command == "Abort")
            {
                cmd = AbortCmd;
            }
            else
            {
                var time = (hour * 3600) + (minutes * 60);
                cmd = string.Format(ShutdownCmd, time);
            }

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = ProgramName;
            startInfo.Arguments = cmd;
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
