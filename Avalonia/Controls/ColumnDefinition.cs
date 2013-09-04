// -----------------------------------------------------------------------
// <copyright file="ColumnDefinition.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    public class ColumnDefinition : DefinitionBase
    {
        public static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.Register(
                "MaxWidth",
                typeof(double),
                typeof(ColumnDefinition),
                new PropertyMetadata(double.PositiveInfinity));

        public static readonly DependencyProperty MinWidthProperty =
            DependencyProperty.Register(
                "MinWidth",
                typeof(double),
                typeof(ColumnDefinition),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register(
                "Width",
                typeof(GridLength),
                typeof(ColumnDefinition),
                new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

        public double ActualWidth
        {
            get;
            internal set;
        }

        ////[TypeConverter(typeof(LengthConverter))]
        public double MaxWidth
        {
            get { return (double)this.GetValue(MaxWidthProperty); }
            set { this.SetValue(MaxWidthProperty, value); }
        }

        ////[TypeConverter(typeof(LengthConverter))]
        public double MinWidth
        {
            get { return (double)this.GetValue(MinWidthProperty); }
            set { this.SetValue(MinWidthProperty, value); }
        }

        public GridLength Width
        {
            get { return (GridLength)this.GetValue(WidthProperty); }
            set { this.SetValue(WidthProperty, value); }
        }
    }
}
