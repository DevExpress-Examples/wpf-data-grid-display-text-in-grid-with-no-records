using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Wpf.Utils.Themes;
using DevExpress.Wpf.Grid;
using DevExpress.Wpf.Core;

namespace DXGridTest {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        public static readonly DependencyProperty IsGridEmptyProperty;
        static Window1() {
            IsGridEmptyProperty = DependencyProperty.RegisterAttached("IsGridEmpty", typeof(bool), typeof(Window1), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        }
        public static bool GetIsGridEmpty(DependencyObject dependencyObject) {
            return (bool)dependencyObject.GetValue(IsGridEmptyProperty);
        }
        public static void SetIsGridEmpty(DependencyObject dependencyObject, bool value) {
            dependencyObject.SetValue(IsGridEmptyProperty, value);
        }
        public Window1() {
            InitializeComponent();


            List<TestData> list = new List<TestData>();
            for(int i = 0; i < 100; i++)
                list.Add(new TestData() { Text = "Row" + i, Number = i, Group = i % 5 });
            grid.DataSource = list;

            grid.LayoutUpdated += new EventHandler(grid_LayoutUpdated);
        }

        void grid_LayoutUpdated(object sender, EventArgs e) {
            Window1.SetIsGridEmpty(grid, grid.VisibleRowCount == 0);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            grid.SetValue(ThemeManager.ThemeNameProperty, "Azure");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            grid.ClearValue(ThemeManager.ThemeNameProperty);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Window1.SetIsGridEmpty(grid, !Window1.GetIsGridEmpty(grid));
        }
    }

    public class TestData {
        public string Text { get; set; }
        public int Number { get; set; }
        public int Group { get; set; }
    }
}
