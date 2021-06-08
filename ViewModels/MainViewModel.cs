namespace WpfApplication2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Timers;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using WpfApplication2.ViewModels;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Diagnostics;
    using Models;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json.Linq;

    public class MainViewModel:INotifyPropertyChanged
    {
        private Config mConfig;
        // fields
        private Random rnd;
        private double lastTimeStamp;
        private static System.Timers.Timer mTimer;
        private double _TempSampleTime;
        private double _SampleTime;
        private ServerIoT Serv;
        // properties
        public PlotModel DataPlotModel { get; set; }
        public mButtonCommand getDataButton { get; set; }
        public mButtonCommand stopDataButton{ get; set; }
        public mButtonCommand applyChangesButton{ get; set; }

        // property that will take a value from a text box and store it until user presses apply button
        public double TempSampleTime
        {
            get
            { 
                return _TempSampleTime; 
            }
            set 
            { 
                _TempSampleTime = value;
                NotifyPropertyChanged();
            }
        }

        // property used mostly for preview in textblock in view
        public double SampleTime
        {
            get
            {
                return _SampleTime;
            }
            set
            {
                _SampleTime = value;
                NotifyPropertyChanged();
            }
        }


        public MainViewModel()
        {
            mConfig = new Config();
            SampleTime = mConfig.sampleTime;

            Serv = new ServerIoT(mConfig.ip);
            
            rnd = new Random();
            lastTimeStamp = 0.0;
            DataPlotModel = new PlotModel { Title = "Temperature Data" };
            initGraph();

            // assign a onclick function to a button with command 
            getDataButton = new mButtonCommand(StartTimer);
            stopDataButton = new mButtonCommand(StopTimer);
            applyChangesButton = new mButtonCommand(UpdateConfig);
        }

        public event PropertyChangedEventHandler PropertyChanged;   

        public double getRandomData(int x)
        {
            return rnd.Next(0, x);
        }

        // buttons onclick methods
        public void StartTimer()
        {
            mTimer = new System.Timers.Timer(SampleTime*1000.0);
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            // using await keyword allows to start a task in non blocking mode 
            // AddDataAsync returns a task that represents a single operation on a thread usually in async mode
            mTimer.Elapsed += async (object sender, ElapsedEventArgs e) => await AddDataAsync();
            mTimer.Start();
        }

        public void StopTimer()
        {
            if(mTimer != null)
            {
                mTimer.Enabled = false;
                mTimer = null;
            }
        }

        private void UpdateConfig()
        {
            // update sample time and set it in the model
            if(SampleTime != TempSampleTime)
            {
                SampleTime = TempSampleTime;
                mConfig = new Config(SampleTime);
                TempSampleTime = 0.0;
                DataPlotModel.ResetAllAxes();
            }
        }

        private async Task AddDataAsync()
        {
            // parse json obj from string that we got from request
            dynamic jobj = JObject.Parse(await Serv.makeHttpRequest());

            // update a series of temperature and humidity stored in json object
            LineSeries ls_temperature = DataPlotModel.Series[0] as LineSeries;
            ls_temperature.Points.Add(new DataPoint(lastTimeStamp, (double)jobj.Temperature));
            LineSeries ls_pressure = DataPlotModel.Series[1] as LineSeries;
            ls_pressure.Points.Add(new DataPoint(lastTimeStamp, (double)jobj.Pressure / 100.0));
            lastTimeStamp +=SampleTime;

            // move a plot if necessary
            if(ls_temperature.Points.Count > mConfig.maxPoints)
            {
                ls_temperature.Points.RemoveAt(0);
                ls_pressure.Points.RemoveAt(0);
                DataPlotModel.Axes[0].Minimum = lastTimeStamp - mConfig.maxPoints*SampleTime;
                DataPlotModel.Axes[0].Maximum = lastTimeStamp + SampleTime;
            }

            // refresh a plot 
            DataPlotModel.InvalidatePlot(true);
            
        }

        public void initGraph()
        {
            DataPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = mConfig.maxPoints,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            DataPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 10,
                Key = "Vertical",
                Unit = "*C",
                Title = "Temperature"
            });
            DataPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Right,
                Minimum = 0,
                Maximum = 1000,
                Key = "Vertical",
                Unit = "hPa",
                Title = "Pressure",
                
            });

            DataPlotModel.Series.Add(new LineSeries() { Title = "temperature data series", Color = OxyColor.Parse("#FF000000") });
            DataPlotModel.Series.Add(new LineSeries() { Title = "Pressure data series", Color = OxyColor.Parse("#AAAA0000") });
        }

        // caller member name returns name of a function that called this function 
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}