﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomHeaderColourDataGrid
{



    public class EllipseViewModelBase : DependencyObject //: INotifyPropertyChanged
    {

        public object Fill { get; set; }

    }

    public class EllipseViewModel : EllipseViewModelBase //: INotifyPropertyChanged
    {
        static int number = 1;
        public static int Number { get => number; set { number = value; RaisePropertyChanged(); } }
        public static event EventHandler NumberChanged;

        private static void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            NumberChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        public EllipseViewModel()
        {
            Fill = Brushes.Yellow;
        }


    }

    public class EllipseViewModel2 : EllipseViewModelBase //: INotifyPropertyChanged
    {
        static int number = 1;
        public static int Number { get => number; set { number = value; RaisePropertyChanged(); } }
        public static event EventHandler NumberChanged;

        public EllipseViewModel2()
        {
            Fill = Brushes.AliceBlue;
        }

        private static void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            NumberChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

    }



    public class EllipseViewModel3 : EllipseViewModelBase //: INotifyPropertyChanged
    {
        static int number = 2;
        public static int Number { get => number; set { number = value; RaisePropertyChanged(); } }
        public static event EventHandler NumberChanged;

        private static void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            NumberChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        public EllipseViewModel3()
        {
            Fill = Brushes.YellowGreen;
        }

    }


}


