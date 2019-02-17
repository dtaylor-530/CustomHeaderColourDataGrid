using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;


using Microsoft.Xaml.Behaviors;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using System.Windows.Media;

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
            get { return (ListBox)GetValue(ListBoxProperty); }
            set { SetValue(ListBoxProperty, value); }
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

        public static readonly DependencyProperty ListBoxProperty = DependencyProperty.Register("ListBox", typeof(ListBox), typeof(ControlMatchCommandBehavior), new FrameworkPropertyMetadata(ListBoxChanged, coerce));

        private static object coerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        private static void ListBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // (e.NewValue as ListBox).SelectionChanged += (a, b) => ((d as ControlMatchCommandBehavior).Command as ICommand)?.Execute(d);

            (d as ControlMatchCommandBehavior).ListBoxChanged();
        }


        private void ListBoxChanged()
        {
            DataGridColumn xx = null;
            (ListBox).SelectionChanged += (a, b) =>
            {
                var dgch = AssociatedObject.FindParent<DataGridColumnHeader>();
                if (dgch.DisplayIndex > -1)
                {
                    var lbdc = ListBox.FindParent<ContentPresenter>().DataContext;
                    var c2dc = ContentTwo.DataContext;

                    if (c2dc == lbdc)
                    {
                        (this as ControlMatchCommandBehavior).Command?.Execute(this);

                        b.Handled = true;
                    }
                }
            };


        }

        public static readonly DependencyProperty ContentTwoProperty = DependencyProperty.Register("ContentTwo", typeof(Control), typeof(ControlMatchCommandBehavior), new FrameworkPropertyMetadata());

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ControlMatchCommandBehavior));

 

        protected override void OnAttached()
        {

        }

     
    }



}
