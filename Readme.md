<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128649877/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1786)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WPF Data Grid - How to Display Text in a Grid With No Records

This example demonstrates how to display a text message in the [GridControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl) if the grid contains no records:

1. Set the [DataViewBase.ShowEmptyText](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataViewBase.ShowEmptyText) property to `true`.
2. Use the [DataViewBase.RuntimeLocalizationStrings](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataViewBase.RuntimeLocalizationStrings) property to change the default text message ("No Records").

![image](https://github.com/AndreySlabov/how-to-display-a-line-when-the-dxgrid-doesnt-include-any-record-e1786/assets/65009440/a5ca8e92-1295-4e18-973f-b36a2f9b813f)

## Files to Review

* [MainWindow.xaml](./CS/GridExample/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/GridExample/MainWindow.xaml))

## Documentation

* [DataViewBase.ShowEmptyText](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataViewBase.ShowEmptyText)
