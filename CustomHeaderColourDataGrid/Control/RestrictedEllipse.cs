using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CustomHeaderColourDataGrid
{
    public class RestrictedEllipse : Control
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ellipse = this.GetTemplateChild("Ellipse") as Ellipse;
            ellipse.Fill = (Brush)FillProperty;
        }

        static RestrictedEllipse()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RestrictedEllipse), new FrameworkPropertyMetadata(typeof(RestrictedEllipse)));
        }

        static int num = 10;

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(RestrictedEllipse), new PropertyMetadata(num, new PropertyChangedCallback(dd), new CoerceValueCallback(dfs)), new ValidateValueCallback(IsValidReading));

        private static void dd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public RestrictedEllipse()
        {

        }


        private static object dfs(DependencyObject d, object baseValue)
        {
            return (int)baseValue;
        }

        private static bool IsValidReading(object value)
        {
            return (int)value >= 0;
            throw new NotImplementedException();
        }



        private static object sddsf(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        private static void ds(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as RestrictedEllipse).oldValue =(Brush)e.OldValue;
        }



        public object FillProperty
        {
            get { return (object)GetValue(FillPropertyProperty); }
            set { SetValue(FillPropertyProperty, value); }
        }


        public static readonly DependencyProperty FillPropertyProperty =            DependencyProperty.Register("FillProperty", typeof(object), typeof(RestrictedEllipse), new PropertyMetadata(Brushes.Beige, new PropertyChangedCallback(ds)));




        public event EventHandler<FillChangedEventArgs> FillChanged;

        public Brush GetFill(Brush value)
        {
           FillChanged?.Invoke(this, new FillChangedEventArgs {DataContext=DataContext,OldValue=value});
            return (Brush)FillProperty;
        }


        private Ellipse ellipse;
        private Brush oldValue;
    }


}
