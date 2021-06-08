using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApplication2.Models
{
    public class Config
    {
        private double _sampleTime;
        private string _ip;
        public readonly int maxPoints = 10;

        public double sampleTime
        {
            get { return _sampleTime; }
            set { _sampleTime = value; }
        }
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }


        public Config()
        {
            sampleTime = 1.0;
            ip = "http://localhost/sense_hat_termomethers_mock.php";
        }

        public Config(double st)
        {
            sampleTime = st;
            ip = "http://localhost/sense_hat_termomethers_mock.php";
        }
    }
}
