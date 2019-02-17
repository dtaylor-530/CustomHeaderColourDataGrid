using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomHeaderColourDataGrid
{
    public class DataGridEx:DataGrid
    {
        static DataGridEx()
        {
           // DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGridEx), new FrameworkPropertyMetadata(typeof(DataGridEx)));
        }

        public object DropDownContent
        {
            get { return (object)GetValue(DropDownContentProperty); }
            set { SetValue(DropDownContentProperty, value); }
        }

        public static readonly DependencyProperty DropDownContentProperty = DependencyProperty.Register("DropDownContent", typeof(object), typeof(DataGridEx), new PropertyMetadata(null,new PropertyChangedCallback(dd)));

        public DataGridEx()
        {
            Uri resourceLocater = new Uri("/CustomHeaderColourDataGrid;component/Themes/SplitButtonColumnHeaderStyle.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            ColumnHeaderStyle = resourceDictionary["SplitButtonColumnHeaderStyle"] as Style;
          
        }


        private static void dd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
