﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomHeaderColourDataGrid
{
    public class RestrictedEllipseItem:ListBoxItem
    {
        static RestrictedEllipseItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RestrictedEllipseItem), new FrameworkPropertyMetadata(typeof(RestrictedEllipseItem)));
        }

        public override void OnApplyTemplate()
        {
            RestrictedEllipse = this.GetTemplateChild("RestrictedEllipse") as RestrictedEllipse;
        }
 

        public RestrictedEllipseItem()
        {
            //Uri resourceLocater = new Uri("/CustomHeaderColourDataGrid;component/Themes/RestrictedEllipse.xaml", System.UriKind.Relative);
            //ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            //Style = resourceDictionary["rei"] as Style;

        }
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(RestrictedEllipseItem));


        public RestrictedEllipse RestrictedEllipse { get; private set; }

        public object MyProperty
        {
            get { return (object)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }


        public static readonly DependencyProperty MyPropertyProperty = DependencyProperty.Register("MyProperty", typeof(object), typeof(RestrictedEllipseItem),new PropertyMetadata(null,chng));

        private static void chng(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
       
        }
    }
}
