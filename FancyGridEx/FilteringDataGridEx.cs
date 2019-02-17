using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancyGridEx
{
    public class FilteringDataGridEx : FancyGrid.FilteringDataGrid
    {
        static FilteringDataGridEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilteringDataGridEx), new FrameworkPropertyMetadata(typeof(FilteringDataGridEx)));
        }

        public object DropDownContent
        {
            get { return (object)GetValue(DropDownContentProperty); }
            set { SetValue(DropDownContentProperty, value); }
        }

        public static readonly DependencyProperty DropDownContentProperty = DependencyProperty.Register("DropDownContent", typeof(object), typeof(FilteringDataGridEx), new PropertyMetadata(null, new PropertyChangedCallback(dd)));

        private static void dd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

    }
}
