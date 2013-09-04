// -----------------------------------------------------------------------
// <copyright file="RowDefinition.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    public class RowDefinition : DefinitionBase
    {
        public static readonly DependencyProperty MaxHeightProperty =
            DependencyProperty.Register(
                "MaxHeight",
                typeof(double),
                typeof(ColumnDefinition),
                new PropertyMetadata(double.PositiveInfinity));

        public static readonly DependencyProperty MinHeightProperty =
            DependencyProperty.Register(
                "MinHeight",
                typeof(double),
                typeof(ColumnDefinition),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register(
                "Height",
                typeof(GridLength),
                typeof(ColumnDefinition),
                new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

        public double ActualHeight
        {
            get;
            internal set;
        }

        ////[TypeConverter(typeof(LengthConverter))]
        public double MaxHeight
        {
            get { return (double)this.GetValue(MaxHeightProperty); }
            set { this.SetValue(MaxHeightProperty, value); }
        }

        ////[TypeConverter(typeof(LengthConverter))]
        public double MinHeight
        {
            get { return (double)this.GetValue(MinHeightProperty); }
            set { this.SetValue(MinHeightProperty, value); }
        }

        public GridLength Height
        {
            get { return (GridLength)this.GetValue(HeightProperty); }
            set { this.SetValue(HeightProperty, value); }
        }
    }
}
