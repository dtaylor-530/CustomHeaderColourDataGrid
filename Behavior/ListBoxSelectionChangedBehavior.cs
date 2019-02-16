using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;


using Microsoft.Xaml.Behaviors;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;

namespace CustomHeaderColourDataGrid
{
    // If two controls match by specified equaility comparison then command is executed
    public class ControlMatchCommandBehavior : Behavior<DropDownButton>
    {
        public Control ContentTwo
        {
            get { return (Control)GetValue(ContentTwoProperty); }
            set { SetValue(ContentTwoProperty, value); }
        }


        public ListBox ListBox
        {
            get { return (ListBox)GetValue(ContentOneProperty); }
            set { SetValue(ContentOneProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ControlMatchCommandBehavior()
        {

        }

        public static readonly DependencyProperty ContentOneProperty = DependencyProperty.Register("ListBox", typeof(ListBox), typeof(ControlMatchCommandBehavior),                                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty ContentTwoProperty = DependencyProperty.Register("ContentTwo", typeof(Control), typeof(ControlMatchCommandBehavior),                                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ControlMatchCommandBehavior));


        protected override void OnAttached()
        {
            (ListBox).SelectionChanged += (a,b) => (Command as ICommand).Execute(this);
        }

    }



}
