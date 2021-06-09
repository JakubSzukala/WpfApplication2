using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApplication2.ViewModels
{
    public class ColorSetting : INotifyPropertyChanged
    {

        private string previewColor;

        public string MyProperty
        {
            get { return previewColor; }
            set { previewColor = value; }
        }


        private int _inputR;
        public  int inputR
        {
            get 
            {
                return _inputR;
            }
            set 
            {
                Debug.WriteLine("Value of red stored");
                _inputR = value; 
            }
        }


        private int _inputG;
        public int inputG
        {
            get 
            {
                return _inputG; 
            }
            set 
            {
                Debug.WriteLine("Value of green stored");
                _inputG = value; 
            }
        }


        private int _inputB;
        public int inputB
        {
            get 
            {
                return _inputB; 
            }
            set
            {
                Debug.WriteLine("Value of blue stored");
                _inputB = value; 
            }
        }


        public ColorSetting()
        {

        }
        
        public void rgbToHex()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
