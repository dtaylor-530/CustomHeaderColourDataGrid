using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace CustomHeaderColourDataGrid
{
    public class ExtendendHeadersBehavior : Behavior<DataGrid>
    {





        public ObservableCollection<HeaderBrush> ColumnBrushes
        {
            get { return (ObservableCollection<HeaderBrush>)GetValue(ColumnBrushesProperty); }
            set { SetValue(ColumnBrushesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnBrushes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnBrushesProperty =
            DependencyProperty.Register("ColumnBrushes", typeof(ObservableCollection<HeaderBrush>), typeof(ExtendendHeadersBehavior), new PropertyMetadata(new ObservableCollection<HeaderBrush>()));


        public ExtendendHeadersBehavior()
        {
            //ColumnBrushes = columnBrushes;
        }


        //static ObservableCollection<HeaderBrush> columnBrushes = new ObservableCollection<HeaderBrush>();




        protected override void OnAttached()
        {
            AssociatedObject.AutoGeneratingColumn += AssociatedObject_AutoGeneratingColumn;

            AssociatedObject.Loaded += AssociatedObject_Initialized;

        }

        private void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            var headers = AssociatedObject.Columns.Select(_ => AssociatedObject.GetColumnHeader(_));

            foreach (var head in headers)
            {
                head.GetChildOfType<DropDownButton>().MouseEnter += Head_Click;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.AutoGeneratingColumn -= AssociatedObject_AutoGeneratingColumn;
            AssociatedObject.Initialized -= AssociatedObject_Initialized;
        }


        private void AssociatedObject_MouseLeftButtonUp()
        {
            var headers = AssociatedObject.Columns.Select(_ => AssociatedObject.GetColumnHeader(_)).ToDictionary(_ => (string)_.Content, _ => (_).Background);

                var yu = headers.Except(ColumnBrushes.ToDictionary(_ => _.Header, _ => _.Brush), new KeyEqualityComparer());
                var yu2 = headers.Intersect(ColumnBrushes.ToDictionary(_ => _.Header, _ => _.Brush), new KeyEqualityComparer());
  

                foreach (var yy in yu2)
                {
                this.Dispatcher.InvokeAsync(() =>
                    ColumnBrushes.Single(_ => _.Header == yy.Key).Brush = headers[yy.Key], System.Windows.Threading.DispatcherPriority.Background);
                }
                foreach (var yy in yu)
                {
                    ColumnBrushes.Add(new HeaderBrush { Header = yy.Key, Brush = yy.Value });
                }
            
        }

        private void Head_Click(object sender, RoutedEventArgs e)
        {
            AssociatedObject_MouseLeftButtonUp();
        }

        private void AssociatedObject_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //if (e.PropertyDescriptor is PropertyDescriptor desc)
            //{
            //    string header = desc.Attributes.OfType<DescriptionAttribute>()
            //        .FirstOrDefault()?.Description;

            //    var xx = new Xceed.Wpf.Toolkit.SplitButton();
            //    //e.Column.Header.
            //    xx.DropDownContent = DropDownContent;
            //    //if (!string.IsNullOrEmpty(header))
            //    //{
            //    e.Column.Header = xx;
            //    //}
            //}
        }
    }


    public class KeyEqualityComparer : IEqualityComparer<KeyValuePair<string, Brush>>
    {
        public bool Equals(KeyValuePair<string, Brush> x, KeyValuePair<string, Brush> y)
        {
            return x.Key == y.Key;
        }

        public int GetHashCode(KeyValuePair<string, Brush> obj)
        {
            return obj.Key.Length;
        }
    }

    public static class DatagridHelper
    {
        public static DataGridColumnHeader GetColumnHeader(this DataGrid dataGrid, DataGridColumn column)
        {
            // dataGrid is the name of your DataGrid. In this case Name="dataGrid"
            List<DataGridColumnHeader> columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(dataGrid);
            foreach (DataGridColumnHeader columnHeader in columnHeaders)
            {
                if (columnHeader.Column == column)
                {
                    return columnHeader;
                }
            }
            return null;
        }

        public static List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        public static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }
    }

    public class HeaderBrush : INotifyPropertyChanged
    {

        public string Header { get; set; }


        private Brush brush;

        public event PropertyChangedEventHandler PropertyChanged;

        public Brush Brush { get => brush; set { brush = value; RaisePropertyChanged(); } }


        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
