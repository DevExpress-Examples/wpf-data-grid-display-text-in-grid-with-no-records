using System;
using System.Windows;
using System.Collections.Generic;


namespace DXGridTest {
    public partial class Window1 : Window {

        public static readonly DependencyProperty IsGridEmptyProperty = DependencyProperty.RegisterAttached("IsGridEmpty", typeof(bool), typeof(Window1), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetIsGridEmpty(DependencyObject dependencyObject) {
            return (bool)dependencyObject.GetValue(IsGridEmptyProperty);
        }
        public static void SetIsGridEmpty(DependencyObject dependencyObject, bool value) {
            dependencyObject.SetValue(IsGridEmptyProperty, value);
        }

        List<TestData> dataSource;

        public Window1() {
            InitializeComponent();

            Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e) {

            dataSource = new List<TestData>();
            for (int i = 0; i < 10; i++)
                dataSource.Add(new TestData() { Text = "Row" + i, Number = i, Group = i % 5 });
            grid.DataSource = dataSource;

            grid.LayoutUpdated += new EventHandler(grid_LayoutUpdated);
        }

        void grid_LayoutUpdated(object sender, EventArgs e) {
            Window1.SetIsGridEmpty(grid, grid.VisibleRowCount == 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            grid.DataSource = grid.DataSource == null ? dataSource : null;
        }
    }

    public class TestData {
        public string Text { get; set; }
        public int Number { get; set; }
        public int Group { get; set; }
    }
}
