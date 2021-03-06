// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License.

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Markup;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Generic;

namespace InteractiveDataDisplay.WPF
{
    /// <summary>
    /// Ready to use figure with left and bottom axes, grid, legend, mouse and keyboard navigation
    /// </summary>
    [ContentProperty("Content")]
    [Description("Ready to use figure")]
    public class Chart : ContentControl
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Chart"/> class.
        /// </summary>
        public Chart()
        {
            DefaultStyleKey = typeof(Chart);
            Background = new SolidColorBrush(Colors.White);
            Foreground = new SolidColorBrush(Colors.Black);
            LegendContent = new LegendItemsPanel();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PlotAxis plotAxis = base.GetTemplateChild("PART_horizontalAxis") as PlotAxis;
            PlotAxis plotAxis2 = base.GetTemplateChild("PART_verticalAxis") as PlotAxis;
            AxisGrid axisGrid = base.GetTemplateChild("PART_axisGrid") as AxisGrid;
            if (plotAxis == null || plotAxis2 == null || axisGrid == null)
            {
                return;
            }
            BindingOperations.SetBinding(axisGrid, AxisGrid.HorizontalTicksProperty, new Binding("Ticks")
            {
                Source = plotAxis
            });
            BindingOperations.SetBinding(axisGrid, AxisGrid.VerticalTicksProperty, new Binding("Ticks")
            {
                Source = plotAxis2
            });
        }
        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Focused", false);
            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Unfocused", false);
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Gets or sets legend content
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public object LegendContent
        {
            get { return (object)GetValue(LegendContentProperty); }
            set { SetValue(LegendContentProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="LegendContent"/> dependency property
        /// </summary>
        public static readonly DependencyProperty LegendContentProperty =
            DependencyProperty.Register("LegendContent", typeof(object), typeof(Chart), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets visibility of LegendControl
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public Visibility LegendVisibility
        {
            get { return (Visibility)GetValue(LegendVisibilityProperty); }
            set { SetValue(LegendVisibilityProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="LegendVisibility"/> dependency property
        /// </summary>
        public static readonly DependencyProperty LegendVisibilityProperty =
            DependencyProperty.Register("LegendVisibility", typeof(Visibility), typeof(Chart), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Gets or sets auto fit mode. <see cref="IsAutoFitEnabled"/> property of all
        /// plots in compositions are updated instantly to have same value.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public bool IsAutoFitEnabled
        {
            get { return (bool)GetValue(IsAutoFitEnabledProperty); }
            set { SetValue(IsAutoFitEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="IsAutoFitEnabled"/> dependency property
        /// </summary>
        public static readonly DependencyProperty IsAutoFitEnabledProperty =
            DependencyProperty.Register("IsAutoFitEnabled", typeof(bool), typeof(Chart), new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets desired plot width in plot coordinates. Settings this property
        /// turns off auto fit mode.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double PlotWidth
        {
            get { return (double)GetValue(PlotWidthProperty); }
            set { SetValue(PlotWidthProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="PlotWidth"/> dependency property
        /// </summary>
        public static readonly DependencyProperty PlotWidthProperty =
            DependencyProperty.Register("PlotWidth", typeof(double), typeof(Chart), new PropertyMetadata(1.0));

        /// <summary>
        /// Gets or sets desired plot height in plot coordinates. Settings this property
        /// turns off auto fit mode.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double PlotHeight
        {
            get { return (double)GetValue(PlotHeightProperty); }
            set { SetValue(PlotHeightProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="PlotHeight"/> dependency property
        /// </summary>
        public static readonly DependencyProperty PlotHeightProperty =
            DependencyProperty.Register("PlotHeight", typeof(double), typeof(Chart), new PropertyMetadata(1.0));

        /// <summary>
        /// Gets or sets desired minimal visible horizontal coordinate in plot coordinates. Settings this property
        /// turns off auto fit mode.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double PlotOriginX
        {
            get { return (double)GetValue(PlotOriginXProperty); }
            set { SetValue(PlotOriginXProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="PlotOriginX"/> dependency property
        /// </summary>
        public static readonly DependencyProperty PlotOriginXProperty =
            DependencyProperty.Register("PlotOriginX", typeof(double), typeof(Chart), new PropertyMetadata(0.0));

        /// <summary>
        /// Gets or sets desired minimal visible vertical coordinate in plot coordinates. Settings this property
        /// turns off auto fit mode.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double PlotOriginY
        {
            get { return (double)GetValue(PlotOriginYProperty); }
            set { SetValue(PlotOriginYProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="PlotOriginY"/> dependency property
        /// </summary>
        public static readonly DependencyProperty PlotOriginYProperty =
            DependencyProperty.Register("PlotOriginY", typeof(double), typeof(Chart), new PropertyMetadata(0.0));

        /// <summary>
        /// Desired ratio of horizontal scale to vertical scale. Values less than or equal to zero 
        /// represent unspecified aspect ratio.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double AspectRatio
        {
            get { return (double)GetValue(AspectRatioProperty); }
            set { SetValue(AspectRatioProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="AspectRatio"/> dependency property
        /// </summary>
        public static readonly DependencyProperty AspectRatioProperty =
            DependencyProperty.Register("AspectRatio", typeof(double), typeof(Chart), new PropertyMetadata(0.0));

        /// <summary>Gets or sets chart title. Chart title is an object that is shown centered above plot area.
        /// Chart title is used inside <see cref="System.Windows.Controls.ContentControl"/> and can be a 
        /// <see cref="System.Windows.UIElement"/>.</summary>
        [Category("InteractiveDataDisplay")]
        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>Identifies <see cref="Title"/> dependency property</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(Chart), new PropertyMetadata(null));

        /// <summary>Gets or sets bottom axis title. Bottom axis title is an object that is centered under plot area.
        /// Bottom axis title is used inside <see cref="System.Windows.Controls.ContentControl"/> and can be a 
        /// <see cref="System.Windows.UIElement"/>.</summary>
        [Category("InteractiveDataDisplay")]
        public object BottomTitle
        {
            get { return (object)GetValue(BottomTitleProperty); }
            set { SetValue(BottomTitleProperty, value); }
        }

        /// <summary>Identifies <see cref="BottomTitle"/> dependency property</summary>
        public static readonly DependencyProperty BottomTitleProperty =
            DependencyProperty.Register("BottomTitle", typeof(object), typeof(Chart), new PropertyMetadata(null));

        /// <summary>Gets or sets right axis title. Right axis title is an object that is vertically centered 
        /// and located to the right from plot area.
        /// Right axis title is used inside <see cref="System.Windows.Controls.ContentControl"/> and can be a 
        /// <see cref="System.Windows.UIElement"/>.</summary>
        [Category("InteractiveDataDisplay")]
        public object RightTitle
        {
            get { return (object)GetValue(RightTitleProperty); }
            set { SetValue(RightTitleProperty, value); }
        }

        /// <summary>Identifies <see cref="RightTitle"/> dependency property</summary>
        public static readonly DependencyProperty RightTitleProperty =
            DependencyProperty.Register("RightTitle", typeof(object), typeof(Chart), new PropertyMetadata(null));

        /// <summary>Gets or sets left axis title. Left axis title is an object that is vertically centered 
        /// and located to the left from plot area.
        /// Left axis title is used inside <see cref="System.Windows.Controls.ContentControl"/> and can be a 
        /// <see cref="System.Windows.UIElement"/>.</summary>
        [Category("InteractiveDataDisplay")]
        public object LeftTitle
        {
            get { return (object)GetValue(LeftTitleProperty); }
            set { SetValue(LeftTitleProperty, value); }
        }

        /// <summary>Identifies <see cref="LeftTitle"/> dependency property</summary>
        public static readonly DependencyProperty LeftTitleProperty =
            DependencyProperty.Register("LeftTitle", typeof(object), typeof(Chart), new PropertyMetadata(null));

        /// <summary>Gets or sets thickness of border surrounding Chart center. This value also is taken into 
        /// account when computing screen padding</summary>
        [Category("InteractiveDataDisplay")]
        public new Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        /// <summary>Identifies <see cref="BorderThickness"/> property</summary>
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(Chart), new PropertyMetadata(new Thickness(1)));

        /// <summary>Gets or sets vertical navigation status. True means that user can navigate along Y axis</summary>
        [Category("InteractiveDataDisplay")]
        public bool IsVerticalNavigationEnabled
        {
            get { return (bool)GetValue(IsVerticalNavigationEnabledProperty); }
            set { SetValue(IsVerticalNavigationEnabledProperty, value); }
        }

        /// <summary>Identifies <see cref="IsVerticalNavigationEnabled"/> property</summary>
        public static readonly DependencyProperty IsVerticalNavigationEnabledProperty =
            DependencyProperty.Register("IsVerticalNavigationEnabled", typeof(bool), typeof(Chart), new PropertyMetadata(true));

        /// <summary>Gets or sets horizontal navigation status. True means that user can navigate along X axis</summary>
        [Category("InteractiveDataDisplay")]
        public bool IsHorizontalNavigationEnabled
        {
            get { return (bool)GetValue(IsHorizontalNavigationEnabledProperty); }
            set { SetValue(IsHorizontalNavigationEnabledProperty, value); }
        }

        /// <summary>Identifies <see cref="IsHorizontalNavigationEnabled"/> property</summary>
        public static readonly DependencyProperty IsHorizontalNavigationEnabledProperty =
            DependencyProperty.Register("IsHorizontalNavigationEnabled", typeof(bool), typeof(Chart), new PropertyMetadata(true));

        /// <summary>
        /// Identifies <see cref="IsXAxisReversed"/> dependency property
        /// </summary>
        public static readonly DependencyProperty IsXAxisReversedProperty =
            DependencyProperty.Register("IsXAxisReversed", typeof(bool), typeof(Chart), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a flag indicating whether the x-axis is reversed or not.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public bool IsXAxisReversed
        {
            get { return (bool)GetValue(IsXAxisReversedProperty); }
            set { SetValue(IsXAxisReversedProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="IsYAxisReversed"/> dependency property
        /// </summary>
        public static readonly DependencyProperty IsYAxisReversedProperty =
            DependencyProperty.Register("IsYAxisReversed", typeof(bool), typeof(Chart), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a flag indicating whether the y-axis is reversed or not.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public bool IsYAxisReversed
        {
            get { return (bool)GetValue(IsYAxisReversedProperty); }
            set { SetValue(IsYAxisReversedProperty, value); }
        }

        /// <summary>
        /// Gets or sets <see cref="TicksProvider"/> for X axis.
        /// </summary>
        /// <remarks>
        /// The default provider is <see cref="TicksProvider"/>
        /// TicksProvider is used 
        /// </remarks>
        [Description("Ticks provider for the Ticks")]
        [Category("InteractiveDataDisplay")]
        public TicksProvider XAxisTicksProvider
        {
            get { return (TicksProvider)GetValue(XAxisTicksProviderProperty); }
            set { SetValue(XAxisTicksProviderProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="TicksProvider"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty XAxisTicksProviderProperty =
            DependencyProperty.Register("XAxisTicksProvider", typeof(TicksProvider), typeof(Chart), new PropertyMetadata(new TicksProvider()));

        /// <summary>
        /// Gets or sets <see cref="TicksProvider"/> for Y axis.
        /// </summary>
        /// <remarks>
        /// The default provider is <see cref="TicksProvider"/>
        /// TicksProvider is used 
        /// </remarks>
        [Description("Ticks provider for the Ticks")]
        [Category("InteractiveDataDisplay")]
        public TicksProvider YAxisTicksProvider
        {
            get { return (TicksProvider)GetValue(YAxisTicksProviderProperty); }
            set { SetValue(YAxisTicksProviderProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="TicksProvider"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty YAxisTicksProviderProperty =
            DependencyProperty.Register("YAxisTicksProvider", typeof(TicksProvider), typeof(Chart), new PropertyMetadata(new TicksProvider()));

        /// <summary>
        /// Gets or sets <see cref="LabelProvider"/> for X axis.
        /// </summary>
        /// <remarks>
        /// The default provider is <see cref="LabelProvider"/>
        /// LabelProvider is used 
        /// </remarks>
        [Description("Label provider for the Ticks")]
        [Category("InteractiveDataDisplay")]
        public ILabelProvider XAxisLabelProvider
        {
            get { return (ILabelProvider)GetValue(XAxisLabelProviderProperty); }
            set { SetValue(XAxisLabelProviderProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="TicksProvider"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty XAxisLabelProviderProperty =
            DependencyProperty.Register("XAxisLabelProvider", typeof(ILabelProvider), typeof(Chart), new PropertyMetadata(new LabelProvider()));

        /// <summary>
        /// Gets or sets <see cref="LabelProvider"/> for Y axis.
        /// </summary>
        /// <remarks>
        /// The default provider is <see cref="LabelProvider"/>
        /// LabelProvider is used 
        /// </remarks>
        [Description("Label provider for the Ticks")]
        [Category("InteractiveDataDisplay")]
        public ILabelProvider YAxisLabelProvider
        {
            get { return (ILabelProvider)GetValue(YAxisLabelProviderProperty); }
            set { SetValue(YAxisLabelProviderProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="TicksProvider"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty YAxisLabelProviderProperty =
            DependencyProperty.Register("YAxisLabelProvider", typeof(ILabelProvider), typeof(Chart), new PropertyMetadata(new LabelProvider()));

        /// <summary>
        /// Gets or sets a set of double values displayed as axis ticks.
        /// </summary>
        [Browsable(false)]
        public IEnumerable<double> XAxisTicks
        {
            get { return (IEnumerable<double>)GetValue(XAxisTicksProperty); }
            set { SetValue(XAxisTicksProperty, value); }
        }

        /// <summary>Identify <see cref="Ticks"/> property</summary>
        public static readonly DependencyProperty XAxisTicksProperty =
            DependencyProperty.Register("XAxisTicks", typeof(IEnumerable<double>), typeof(Chart), new PropertyMetadata(new double[0]));

        /// <summary>
        /// Gets or sets a set of double values displayed as axis ticks.
        /// </summary>
        [Browsable(false)]
        public IEnumerable<double> YAxisTicks
        {
            get { return (IEnumerable<double>)GetValue(YAxisTicksProperty); }
            set { SetValue(YAxisTicksProperty, value); }
        }

        /// <summary>Identify <see cref="Ticks"/> property</summary>
        public static readonly DependencyProperty YAxisTicksProperty =
            DependencyProperty.Register("YAxisTicks", typeof(IEnumerable<double>), typeof(Chart), new PropertyMetadata(new double[0]));

        /// <summary>
        /// This is the X offset of the axis from the top left corner of the chart
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double XOrigin
        {
            get { return (double)GetValue(XOriginProperty); }
            set { SetValue(XOriginProperty, value); }
        }

        /// <summary>Identifies <see cref="XOrigin"/> dependency property</summary>
        public static readonly DependencyProperty XOriginProperty =
            DependencyProperty.Register("XOrigin", typeof(double), typeof(Chart), new PropertyMetadata(0.0));

        /// <summary>
        /// This is the Y offset of the axis from the top left corner of the chart. 
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double YOrigin
        {
            get { return (double)GetValue(YOriginProperty); }
            set { SetValue(YOriginProperty, value); }
        }

        /// <summary>Identifies <see cref="YOrigin"/> dependency property</summary>
        public static readonly DependencyProperty YOriginProperty =
            DependencyProperty.Register("YOrigin", typeof(double), typeof(Chart), new PropertyMetadata(0.0));

        /// <summary>
        /// This is the offset of the left axis from the left edge of the chart. This allows us to move the left
        /// axis in the chart.
        /// </summary>
        [Category("InteractiveDataDisplay")]
        public double LeftAxisOffset
        {
            get { return (double)GetValue(LeftAxisOffsetProperty); }
            set { SetValue(LeftAxisOffsetProperty, value); }
        }

        /// <summary>Identifies <see cref="XOrigin"/> dependency property</summary>
        public static readonly DependencyProperty LeftAxisOffsetProperty =
            DependencyProperty.Register("LeftAxisOffset", typeof(double), typeof(Chart), new PropertyMetadata(0.0));

    }
}
