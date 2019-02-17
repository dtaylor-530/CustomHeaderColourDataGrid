﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using static FancyGrid.FilterMethods;

namespace FancyGrid
{
    /// <summary>
    /// A grid that makes inline filtering possible.
    /// </summary>
    public class FilteringDataGrid : System.Windows.Controls.DataGrid
    {



        //public string[] FilterTypes
        //{
        //    get { return (string[])GetValue(FilterTypesProperty); }
        //    set { SetValue(FilterTypesProperty, value); }
        //}


        public static readonly DependencyProperty FilterTypesProperty = DependencyProperty.Register("FilterTypes", typeof(char[]), typeof(FilteringDataGrid), new PropertyMetadata(new[] {' ', '~', '=', '<', '>', '!' }));


        public Dictionary<string, Filterer> ColumnFilterModes
        {
            get { return (Dictionary<string, Filterer>)GetValue(ColumnFilterModesProperty); }
            set { SetValue(ColumnFilterModesProperty, value); }
        }


        public static readonly DependencyProperty ColumnFilterModesProperty = DependencyProperty.Register("ColumnFilterModes", typeof(Dictionary<string, Filterer>), typeof(FilteringDataGrid), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
         
            base.OnApplyTemplate();
        }
        /// <summary>
        /// This dictionary will have a list of all applied filters
        /// </summary>
        private Dictionary<string, string> columnFilters;

        /// <summary>
        /// This dictionary will have a list of all applied filters
        /// </summary>
        private Dictionary<string, string> columnSymbols = new Dictionary<string, string>();
        /// <summary>
        /// This dictionary will map a column to the filter behavior
        /// </summary>
        private Dictionary<string, Filterer> columnFilterModes;

        /// <summary>
        /// Cache with properties for better performance
        /// </summary>
        private Dictionary<string, PropertyInfo> propertyCache;

        /// <summary>
        /// Case sensitive filtering
        /// </summary>
        public static DependencyProperty IsFilteringCaseSensitiveProperty =
             DependencyProperty.Register("IsFilteringCaseSensitive", typeof(bool), typeof(FilteringDataGrid), new PropertyMetadata(true));

        /// <summary>
        /// Case sensitive filtering
        /// </summary>
        public bool IsFilteringCaseSensitive
        {
            get { return (bool)(GetValue(IsFilteringCaseSensitiveProperty)); }
            set { SetValue(IsFilteringCaseSensitiveProperty, value); }
        }

        static FilteringDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilteringDataGrid), new FrameworkPropertyMetadata(typeof(FilteringDataGrid)));
        }

        /// <summary>
        /// Register for all text changed events
        /// </summary>
        public FilteringDataGrid()
        {


            // Initialize lists
            columnFilters = new Dictionary<string, string>();
            columnFilterModes = new Dictionary<string, Filterer>();
            propertyCache = new Dictionary<string, PropertyInfo>();
       
            // Enable Filtering
            AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(OnTextChanged), true);
            AddHandler(ComboBox.SelectionChangedEvent, new SelectionChangedEventHandler(OnSelectionChanged2), true);
            // Enable Multisort
            Sorting += FilteringDataGrid_Sorting;

            // Clear the cache if we bind a new collection
            DataContextChanged += new DependencyPropertyChangedEventHandler(FilteringDataGrid_DataContextChanged);

            //Set up context menus
            ContextMenuOpening += FilteringDataGrid_ContextMenuOpening;

            AutoGeneratingColumn += FilteringDataGrid_AutoGeneratingColumn;


        }

        private void FilteringDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!e.Column.CanUserSort)
            {
                Type type = e.PropertyType;
                if (type.IsGenericType && type.IsValueType && typeof(IComparable).IsAssignableFrom(type.GetGenericArguments()[0]))
                {
                    // allow nullable primitives to be sorted
                    e.Column.CanUserSort = true;
                }
            }
        }


        void FilteringDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender != this)
                return;

            if (ContextMenu == null)
                ContextMenu = new ContextMenu();
            ContextMenu.Items.Clear();
            ContextMenu.Items.Add(new MenuItem() { Header = "Include items like this " + CurrentCell.Item, Command = new FunctionRunnerCommand(FilterToSelected) });
            ContextMenu.Items.Add(new MenuItem() { Header = "Exclude items like this  " + CurrentCell.Item, Command = new FunctionRunnerCommand(FilterToNotSelected) });
            ContextMenu.Items.Add(new MenuItem() { Header = "Clear all filters", Command = new FunctionRunnerCommand(ClearFilters) });
            ContextMenu.Items.Add(new Separator());
            if (ExtraContextMenuItems != null)
                foreach (var item in ExtraContextMenuItems)
                    ContextMenu.Items.Add(ExtraContextMenuItems);

        }

        private void FilterToSelected(object p)
        {
            var tbs = Helpers.AllChildren<TextBox>(this);
            TextBox tb = null;
            foreach (var item in tbs)
            {
                DataGridColumnHeader header = TryFindParent<DataGridColumnHeader>(item);
                if (header.Column != null && header.Column == CurrentColumn)
                {
                    tb = item;
                    break;
                }
            }
            var text = (CurrentColumn.GetCellContent(CurrentCell.Item) as TextBlock).Text; //Kludge
            tb.Text = text;
        }
        private void FilterToNotSelected(object p)
        {
            var tbs = Helpers.AllChildren<TextBox>(this);
            TextBox tb = null;
            foreach (var item in tbs)
            {
                DataGridColumnHeader header = TryFindParent<DataGridColumnHeader>(item);
                if (header.Column != null && header.Column == CurrentColumn)
                {
                    tb = item;
                    break;
                }
            }
            var text = (CurrentColumn.GetCellContent(CurrentCell.Item) as TextBlock).Text; //Kludge
            tb.Text = "!" + text;
        }

        void ClearFilters(object p)
        {
            foreach (var item in Helpers.AllChildren<TextBox>(this))
                item.Clear();

            columnFilters.Clear();
            columnFilterModes.Clear();
            ApplyFilters();
        }


        void FilteringDataGrid_Sorting(object sender, System.Windows.Controls.DataGridSortingEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(Items);

            foreach (var column in Columns)
            {

                var sd = Helpers.FindSortDescription(view.SortDescriptions, column.SortMemberPath);
                if (sd.HasValue)
                    column.SortDirection = sd.Value.Direction;
            }

            if (e.Column.SortDirection.HasValue)
            {

                view.SortDescriptions.Remove(view.SortDescriptions.FirstOrDefault(sd => sd.PropertyName == e.Column.Header.ToString()));
                switch (e.Column.SortDirection.Value)
                {
                    case ListSortDirection.Ascending:
                        e.Column.SortDirection = ListSortDirection.Descending;
                        view.SortDescriptions.Add(new SortDescription(e.Column.Header.ToString(), ListSortDirection.Descending));
                        break;
                    case ListSortDirection.Descending:
                        e.Column.SortDirection = null;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                e.Column.SortDirection = ListSortDirection.Ascending;
                view.SortDescriptions.Add(new SortDescription(e.Column.Header.ToString(), ListSortDirection.Ascending));
            }
            e.Handled = true; ;
        }

        private void FilteringDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            propertyCache.Clear();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Get the textbox
            TextBox filterTextBox = e.OriginalSource as TextBox;

            // Get the header of the textbox
            DataGridColumnHeader header = TryFindParent<DataGridColumnHeader>(filterTextBox);
            if (header != null)
            {
                UpdateFilterValue(filterTextBox, header);
                ApplyFilters();
            }
        }

        private void OnSelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            // Get the textbox
            ComboBox combobox = e.OriginalSource as ComboBox;
            if (combobox != null)
            {
                // Get the header of the textbox
                DataGridColumnHeader header = TryFindParent<DataGridColumnHeader>(combobox);
                string columnBinding = header.DataContext != null ? header.DataContext.ToString() : "";
                if (header != null && !string.IsNullOrEmpty(combobox.Text))
                {
                    columnFilterModes[columnBinding] = UpdateFilterer(e.AddedItems.Cast<Char>().First().ToString(), header);
                    this.Dispatcher.InvokeAsync(() => ColumnFilterModes = null, System.Windows.Threading.DispatcherPriority.Normal);
                    this.Dispatcher.InvokeAsync(() => ColumnFilterModes = columnFilterModes, System.Windows.Threading.DispatcherPriority.Background);
                    ApplyFilters();
                }
            }
        }

        private void UpdateFilterValue(TextBox textBox, DataGridColumnHeader header)
        {
            // Try to get the property bound to the column.
            // This should be stored as datacontext.
            string columnBinding = header.DataContext != null ? header.DataContext.ToString() : "";
            if(!columnSymbols.Any())
                columnSymbols = Columns.ToDictionary(_ => _.Header.ToString(), _ => " ");
            if (!String.IsNullOrEmpty(columnBinding))
            {
                columnFilters[columnBinding] = textBox.Text;
                columnFilterModes[columnBinding] = UpdateFilterer(columnSymbols[columnBinding], header);
                this.Dispatcher.InvokeAsync(() => ColumnFilterModes = null, System.Windows.Threading.DispatcherPriority.Normal);
                this.Dispatcher.InvokeAsync(() => ColumnFilterModes = columnFilterModes, System.Windows.Threading.DispatcherPriority.Background);
                ApplyFilters();
            }
        }


        private Filterer UpdateFilterer(string text, DataGridColumnHeader header)
        {
            // Try to get the property bound to the column.
            // This should be stored as datacontext.
            string columnBinding = header.DataContext != null ? header.DataContext.ToString() : "";

            if (!String.IsNullOrEmpty(columnBinding))
            {

                columnSymbols[columnBinding] = text;

                if (columnFilters.ContainsKey(columnBinding))
                    switch (text)
                    {
                        case "=":
                            return new IsFilterer(columnFilters[columnBinding], IsFilteringCaseSensitive);
                        case "!":
                            return new IsNotFilterer(columnFilters[columnBinding], IsFilteringCaseSensitive);
                        case "~":
                            return new NotContainsFilterer(columnFilters[columnBinding], IsFilteringCaseSensitive);
                        case "<":
                            return new LessThanFilterer(columnFilters[columnBinding]);
                        case ">":
                            return new GreaterThanFilterer(columnFilters[columnBinding]);
                        case "\"\"":
                            return new BlankFilterer();
                        case @"*":
                            return new NotBlankFilterer();
                        default:
                            return new ContainsFilterer(columnFilters[columnBinding], IsFilteringCaseSensitive);
                    }
                else
                    switch (text)
                    {

                        case "\"\"":
                            return new BlankFilterer();
                        case @"*":
                            return new NotBlankFilterer();
                        default:
                            return null;
                    }
            }
            return null;
        }


        private void ApplyFilters()
        {
            // Get the view
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
            view.Filter = Filter;
        }

        private bool Filter(object item)
        {

            // Loop filters
            foreach (KeyValuePair<string, string> filter in columnFilters)
            {
                object property = GetPropertyValue(item, filter.Key);
                if (property != null && !string.IsNullOrEmpty(filter.Value))
                {
                    if (columnFilterModes.ContainsKey(filter.Key))
                    {
                        if (!columnFilterModes[filter.Key].Filter(property))
                            return false;
                    }
                    else
                        return new ContainsFilterer(filter.Value, IsFilteringCaseSensitive).Filter(property);
                }
            }

            return true;
        }


        /// <summary>
        /// Get the value of a property
        /// </summary>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private object GetPropertyValue(object item, string property)
        {
            // No value
            object value = null;

            // Get property  from cache
            PropertyInfo pi = null;
            if (propertyCache.ContainsKey(property))
                pi = propertyCache[property];
            else
            {
                pi = item.GetType().GetProperty(property);
                propertyCache.Add(property, pi);
            }

            // If we have a valid property, get the value
            if (pi != null)
                value = pi.GetValue(item, null);

            // Done
            return value;
        }

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null reference is being returned.</returns>
        public static T TryFindParent<T>(DependencyObject child)
          where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Do note, that for content element,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise null.</returns>
        public static DependencyObject GetParentObject(DependencyObject child)
        {
            if (child == null) return null;
            ContentElement contentElement = child as ContentElement;

            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            // If it's not a ContentElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        public IEnumerable<MenuItem> ExtraContextMenuItems { get; set; }
    }
}
