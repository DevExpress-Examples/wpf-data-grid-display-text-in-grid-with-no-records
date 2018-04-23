using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core.Native;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Data;
using DevExpress.Xpf.Core;

namespace GridExample {
    public class EmptyGridHelper {

        public static readonly DependencyProperty HasVisibleRowsProperty =
            DependencyProperty.RegisterAttached("HasVisibleRows", typeof(bool), typeof(EmptyGridHelper), new PropertyMetadata(false));

        public static readonly DependencyProperty EmptyGridTemplateProperty =
            DependencyProperty.RegisterAttached("EmptyGridTemplate", typeof(DataTemplate), typeof(EmptyGridHelper), new UIPropertyMetadata(null, new PropertyChangedCallback(EmptyGridTemplateChanged)));

        static void EmptyGridTemplateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            TableView view = sender as TableView;
            DataTemplate emptyGridTemplate = (DataTemplate)e.NewValue;
            if (view == null || emptyGridTemplate == null) return;

            view.Grid.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Grid_PropertyChanged);
            view.Grid.ItemsSourceChanged += new ItemsSourceChangedEventHandler(Grid_ItemsSourceChanged);

            view.Loaded += new RoutedEventHandler(view_Loaded);
        }

        static void view_Loaded(object sender, RoutedEventArgs e) {
            TableView view = sender as TableView;

            DataTemplate emptyGridTemplate = (DataTemplate)view.GetValue(EmptyGridHelper.EmptyGridTemplateProperty);
            if (view == null || emptyGridTemplate == null) return;

            AddEptyGridContentPresenter(view, emptyGridTemplate);
        }

        static void AddEptyGridContentPresenter(TableView view, DataTemplate emptyGridTemplate) {
            ContentPresenter emptyGridContentresenter = new ContentPresenter();
            emptyGridContentresenter.ContentTemplate = emptyGridTemplate;

            emptyGridContentresenter.HorizontalAlignment = HorizontalAlignment.Stretch;
            emptyGridContentresenter.VerticalAlignment = VerticalAlignment.Stretch;

            Binding emptyGridContentVisibilityBinding = new Binding("(local:EmptyGridHelper.HasVisibleRows)") {
                Source = view,
                Converter = new BoolToVisibilityInverseConverter() { }
            };
            emptyGridContentresenter.SetBinding(ContentPresenter.VisibilityProperty, emptyGridContentVisibilityBinding);

            Grid container = (Grid)LayoutHelper.FindElementByName(view, "rowPresenterGrid");

            container.Children.Add(emptyGridContentresenter);
        }


        static void Grid_ItemsSourceChanged(object sender, ItemsSourceChangedEventArgs e) {
            if (e.NewDataSource == null) {
                ((GridControl)sender).View.SetValue(EmptyGridHelper.HasVisibleRowsProperty, false);
            }
        }

        static void Grid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "VisibleRowCount") {
                GridControl grid = (GridControl)sender;
                grid.View.SetValue(EmptyGridHelper.HasVisibleRowsProperty, grid.VisibleRowCount != 0);
            }
        }

        #region CLRs
        public static bool GetHasVisibleRows(DependencyObject obj) {
            return (bool)obj.GetValue(HasVisibleRowsProperty);
        }

        public static void SetHasVisibleRows(DependencyObject obj, bool value) {
            obj.SetValue(HasVisibleRowsProperty, value);
        }

        public static DataTemplate GetEmptyGridTemplate(DependencyObject obj) {
            return (DataTemplate)obj.GetValue(EmptyGridTemplateProperty);
        }

        public static void SetEmptyGridTemplate(DependencyObject obj, DataTemplate value) {
            obj.SetValue(EmptyGridTemplateProperty, value);
        }
        #endregion  

    }
}
