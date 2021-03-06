﻿using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace CustomHeaderColourDataGrid
{
    //Transfer Colour Command
    public class TransferColourCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            ControlMatchCommandBehavior behavior = parameter as ControlMatchCommandBehavior;

            var rei = behavior.ListBox.ItemContainerGenerator.ContainerFromItem(behavior.ListBox.SelectedItem) as RestrictedEllipseItem;

            (behavior.ContentTwo).Background = rei?.RestrictedEllipse.GetFill((behavior.ContentTwo).Background);

        }
    }
}
