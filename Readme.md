<!-- default file list -->
*Files to look at*:

* [Window1.xaml](./CS/DXGridTest/Window1.xaml) (VB: [Window1.xaml.vb](./VB/DXGridTest/Window1.xaml.vb))
* [Window1.xaml.cs](./CS/DXGridTest/Window1.xaml.cs) (VB: [Window1.xaml.vb](./VB/DXGridTest/Window1.xaml.vb))
<!-- default file list end -->
# How to display a line when the DXGrid doesn't include any record 


<p>This sample shows how to display a message in the grid if there are no visible rows.</p>


<h3>Description</h3>

<p>Create the additional attached inherited property IsGridEmpty by the Boolean type. Subscribe to the LayoutUpdated event, and in the LayoutUpdated event handler, customize this property. In the grid, override the style for the GridItemsContainer.</p>

<br/>


