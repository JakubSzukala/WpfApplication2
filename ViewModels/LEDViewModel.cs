using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using WpfApplication2.Models;

namespace WpfApplication2.ViewModels
{
    public class LEDViewModel
    {
        LedMatrix leds;

        int Xdim;
        int Ydim;
        public ObservableCollection<LedButtonCommand> ledButtons { get; set; }

        //temporary for tests, later scale
        public LedButtonCommand mBtn { set; get; }
        public LedButtonCommand a;
        public LedButtonCommand b;

        public LEDViewModel()
        {
            Xdim = 8;
            Ydim = 8;
            
            leds = new LedMatrix(Xdim, Ydim);

            // generate dynamically created led matrix 
            // this observable collection is binded with data template in xaml
            ledButtons = new ObservableCollection<LedButtonCommand>();
            for(int i = 0; i < Xdim; i++)
            {
                for(int k = 0; k < Ydim; k++)
                {
                    ledButtons.Add(new LedButtonCommand(BtnOnClick, i, k));
                }
            }

        }

        public void BtnOnClick()
        {
            Debug.WriteLine("Clicked");
        }


    }
}
