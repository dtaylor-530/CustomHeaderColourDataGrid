using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomHeaderColourDataGrid
{
    public class ExtendendHeadersBehavior : Behavior<DataGrid>
    {
        public object DropDownContent
        {
            get { return (object)GetValue(DropDownContentProperty); }
            set { SetValue(DropDownContentProperty, value); }
        }

        public static readonly DependencyProperty DropDownContentProperty = DependencyProperty.Register("DropDownContent", typeof(object), typeof(DataGridEx), new PropertyMetadata(null, new PropertyChangedCallback(dd)));

        private static void dd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
    
        }

        protected override void OnAttached()
        {
            AssociatedObject.AutoGeneratingColumn += AssociatedObject_AutoGeneratingColumn;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.AutoGeneratingColumn -= AssociatedObject_AutoGeneratingColumn;
        }

        private void AssociatedObject_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor desc)
            {
                string header = desc.Attributes.OfType<DescriptionAttribute>()
                    .FirstOrDefault()?.Description;

                var xx = new Xceed.Wpf.Toolkit.SplitButton();
                
                xx.DropDownContent = DropDownContent;
                //if (!string.IsNullOrEmpty(header))
                //{
                e.Column.Header = xx;
                //}
            }
        }
    }
}
