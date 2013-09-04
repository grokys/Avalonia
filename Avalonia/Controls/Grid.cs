// -----------------------------------------------------------------------
// <copyright file="Grid.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Media;

    public class Grid : Panel
    {
        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.RegisterAttached(
                "Column",
                typeof(int),
                typeof(Grid));

        public static readonly DependencyProperty ColumnSpanProperty =
            DependencyProperty.RegisterAttached(
                "ColumnSpan",
                typeof(int),
                typeof(Grid),
                new PropertyMetadata(1));

        public static readonly DependencyProperty IsSharedSizeScopeProperty =
            DependencyProperty.RegisterAttached(
                "IsSharedSizeScope",
                typeof(bool),
                typeof(Grid));

        public static readonly DependencyProperty RowProperty =
            DependencyProperty.RegisterAttached(
                "Row",
                typeof(int),
                typeof(Grid));

        public static readonly DependencyProperty RowSpanProperty =
            DependencyProperty.RegisterAttached(
                "RowSpan",
                typeof(int),
                typeof(Grid),
                new PropertyMetadata(1));

        private Segment[,] row_matrix;
        private Segment[,] col_matrix;

        public Grid()
        {
            this.ColumnDefinitions = new ColumnDefinitionCollection();
            this.RowDefinitions = new RowDefinitionCollection();
        }

        public ColumnDefinitionCollection ColumnDefinitions
        {
            get;
            private set;
        }

        public RowDefinitionCollection RowDefinitions
        {
            get;
            private set;
        }

        public static int GetColumn(UIElement element)
        {
            return (int)element.GetValue(ColumnProperty);
        }

        public static int GetColumnSpan(UIElement element)
        {
            return (int)element.GetValue(ColumnSpanProperty);
        }

        public static int GetRow(UIElement element)
        {
            return (int)element.GetValue(RowProperty);
        }

        public static int GetRowSpan(UIElement element)
        {
            return (int)element.GetValue(RowSpanProperty);
        }

        public static void SetColumn(UIElement element, int value)
        {
            element.SetValue(ColumnProperty, value);
        }

        public static void SetColumnSpan(UIElement element, int value)
        {
            element.SetValue(ColumnSpanProperty, value);
        }

        public static void SetRow(UIElement element, int value)
        {
            element.SetValue(RowProperty, value);
        }

        public static void SetRowSpan(UIElement element, int value)
        {
            element.SetValue(RowSpanProperty, value);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size totalSize = constraint;
            int col_count = this.ColumnDefinitions.Count;
            int row_count = this.RowDefinitions.Count;
            Size total_stars = new Size(0, 0);

            bool empty_rows = row_count == 0;
            bool empty_cols = col_count == 0;
            bool hasChildren = this.InternalChildren.Count > 0;

            if (empty_rows) row_count = 1;
            if (empty_cols) col_count = 1;

            this.CreateMatrices(row_count, col_count);

            if (empty_rows)
            {
                row_matrix[0, 0] = new Segment(0, 0, double.PositiveInfinity, GridUnitType.Star);
                row_matrix[0, 0].stars = 1.0;
                total_stars.Height += 1.0;
            }
            else
            {
                for (int i = 0; i < row_count; i++)
                {
                    RowDefinition rowdef = this.RowDefinitions[i];
                    GridLength height = rowdef.Height;

                    rowdef.ActualHeight = double.PositiveInfinity;
                    row_matrix[i, i] = new Segment(0, rowdef.MinHeight, rowdef.MaxHeight, height.GridUnitType);

                    if (height.GridUnitType == GridUnitType.Pixel)
                    {
                        row_matrix[i, i].offered_size = Clamp(height.Value, row_matrix[i, i].min, row_matrix[i, i].max);
                        row_matrix[i, i].desired_size = row_matrix[i, i].offered_size;
                        rowdef.ActualHeight = row_matrix[i, i].offered_size;
                    }
                    else if (height.GridUnitType == GridUnitType.Star)
                    {
                        row_matrix[i, i].stars = height.Value;
                        total_stars.Height += height.Value;
                    }
                    else if (height.GridUnitType == GridUnitType.Auto)
                    {
                        row_matrix[i, i].offered_size = Clamp(0, row_matrix[i, i].min, row_matrix[i, i].max);
                        row_matrix[i, i].desired_size = row_matrix[i, i].offered_size;
                    }
                }
            }

            if (empty_cols)
            {
                col_matrix[0, 0] = new Segment(0, 0, double.PositiveInfinity, GridUnitType.Star);
                col_matrix[0, 0].stars = 1.0;
                total_stars.Width += 1.0;
            }
            else
            {
                for (int i = 0; i < col_count; i++)
                {
                    ColumnDefinition coldef = this.ColumnDefinitions[i];
                    GridLength width = coldef.Width;

                    coldef.ActualWidth = double.PositiveInfinity;
                    col_matrix[i, i] = new Segment(0, coldef.MinWidth, coldef.MaxWidth, width.GridUnitType);

                    if (width.GridUnitType == GridUnitType.Pixel)
                    {
                        col_matrix[i, i].offered_size = Clamp(width.Value, col_matrix[i, i].min, col_matrix[i, i].max);
                        col_matrix[i, i].desired_size = col_matrix[i, i].offered_size;
                        coldef.ActualWidth = col_matrix[i, i].offered_size;
                    }
                    else if (width.GridUnitType == GridUnitType.Star)
                    {
                        col_matrix[i, i].stars = width.Value;
                        total_stars.Width += width.Value;
                    }
                    else if (width.GridUnitType == GridUnitType.Auto)
                    {
                        col_matrix[i, i].offered_size = Clamp(0, col_matrix[i, i].min, col_matrix[i, i].max);
                        col_matrix[i, i].desired_size = col_matrix[i, i].offered_size;
                    }
                }
            }

            List<GridNode> sizes = new List<GridNode>();
            GridNode node;
            GridNode separator = new GridNode(null, 0, 0, 0);
            int separatorIndex;

            sizes.Add(separator);

            // Pre-process the grid children so that we know what types of elements we have so
            // we can apply our special measuring rules.
            GridWalker grid_walker = new GridWalker(this, row_matrix, col_matrix);

            for (int i = 0; i < 6; i++)
            {
                // These bools tell us which grid element type we should be measuring. i.e.
                // 'star/auto' means we should measure elements with a star row and auto col
                bool auto_auto = i == 0;
                bool star_auto = i == 1;
                bool auto_star = i == 2;
                bool star_auto_again = i == 3;
                bool non_star = i == 4;
                bool remaining_star = i == 5;

                if (hasChildren)
                {
                    this.ExpandStarCols(totalSize);
                    this.ExpandStarRows(totalSize);
                }

                foreach (UIElement child in VisualTreeHelper.GetChildren(this))
                {
                    int col, row;
                    int colspan, rowspan;
                    Size child_size = new Size(0, 0);
                    bool star_col = false;
                    bool star_row = false;
                    bool auto_col = false;
                    bool auto_row = false;

                    col = Math.Min(GetColumn(child), col_count - 1);
                    row = Math.Min(GetRow(child), row_count - 1);
                    colspan = Math.Min(GetColumnSpan(child), col_count - col);
                    rowspan = Math.Min(GetRowSpan(child), row_count - row);

                    for (int r = row; r < row + rowspan; r++)
                    {
                        star_row |= row_matrix[r, r].type == GridUnitType.Star;
                        auto_row |= row_matrix[r, r].type == GridUnitType.Auto;
                    }

                    for (int c = col; c < col + colspan; c++)
                    {
                        star_col |= col_matrix[c, c].type == GridUnitType.Star;
                        auto_col |= col_matrix[c, c].type == GridUnitType.Auto;
                    }

                    // This series of if statements checks whether or not we should measure
                    // the current element and also if we need to override the sizes
                    // passed to the Measure call. 

                    // If the element has Auto rows and Auto columns and does not span Star
                    // rows/cols it should only be measured in the auto_auto phase.
                    // There are similar rules governing auto/star and star/auto elements.
                    // NOTE: star/auto elements are measured twice. The first time with
                    // an override for height, the second time without it.
                    if (auto_row && auto_col && !star_row && !star_col)
                    {
                        if (!auto_auto)
                        {
                            continue;
                        }

                        child_size.Width = double.PositiveInfinity;
                        child_size.Height = double.PositiveInfinity;
                    }
                    else if (star_row && auto_col && !star_col)
                    {
                        if (!(star_auto || star_auto_again))
                        {
                            continue;
                        }

                        if (star_auto && grid_walker.HasAutoStar)
                        {
                            child_size.Height = double.PositiveInfinity;
                        }

                        child_size.Width = double.PositiveInfinity;
                    }
                    else if (auto_row && star_col && !star_row)
                    {
                        if (!auto_star)
                        {
                            continue;
                        }

                        child_size.Height = double.PositiveInfinity;
                    }
                    else if ((auto_row || auto_col) && !(star_row || star_col))
                    {
                        if (!non_star)
                        {
                            continue;
                        }

                        if (auto_row)
                        {
                            child_size.Height = double.PositiveInfinity;
                        }

                        if (auto_col)
                        {
                            child_size.Width = double.PositiveInfinity;
                        }
                    }
                    else if (!(star_row || star_col))
                    {
                        if (!non_star)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!remaining_star)
                        {
                            continue;
                        }
                    }

                    for (int r = row; r < row + rowspan; r++)
                    {
                        child_size.Height += row_matrix[r, r].offered_size;
                    }

                    for (int c = col; c < col + colspan; c++)
                    {
                        child_size.Width += col_matrix[c, c].offered_size;
                    }

                    child.Measure(child_size);
                    Size desired = child.DesiredSize;

                    // Elements distribute their height based on two rules:
                    // 1) Elements with rowspan/colspan == 1 distribute their height first
                    // 2) Everything else distributes in a LIFO manner.
                    // As such, add all UIElements with rowspan/colspan == 1 after the separator in
                    // the list and everything else before it. Then to process, just keep popping
                    // elements off the end of the list.
                    if (!star_auto)
                    {
                        node = new GridNode(row_matrix, row + rowspan - 1, row, desired.Height);
                        separatorIndex = sizes.IndexOf(separator);
                        sizes.Insert(node.row == node.col ? separatorIndex + 1 : separatorIndex, node);
                    }

                    node = new GridNode(col_matrix, col + colspan - 1, col, desired.Width);

                    separatorIndex = sizes.IndexOf(separator);
                    sizes.Insert(node.row == node.col ? separatorIndex + 1 : separatorIndex, node);
                }

                sizes.Remove(separator);

                while (sizes.Count > 0)
                {
                    node = sizes.Last();
                    node.matrix[node.row, node.col].desired_size = Math.Max(node.matrix[node.row, node.col].desired_size, node.size);
                    this.AllocateDesiredSize(row_count, col_count);
                    sizes.Remove(node);
                }

                sizes.Add(separator);
            }

            // Once we have measured and distributed all sizes, we have to store
            // the results. Every time we want to expand the rows/cols, this will
            // be used as the baseline.
            this.SaveMeasureResults();

            sizes.Remove(separator);

            Size grid_size = new Size(0, 0);
            
            for (int c = 0; c < col_count; c++)
            {
                grid_size.Width += col_matrix[c, c].desired_size;
            }

            for (int r = 0; r < row_count; r++)
            {
                grid_size.Height += row_matrix[r, r].desired_size;
            }

            return grid_size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int col_count = this.ColumnDefinitions.Count;
            int row_count = this.RowDefinitions.Count;

            this.RestoreMeasureResults ();

            Size total_consumed = new Size(0, 0);

            for (int c = 0; c < col_matrix.GetUpperBound(0) + 1; c++) 
            {
                col_matrix[c, c].offered_size = col_matrix[c, c].desired_size;
                total_consumed.Width += col_matrix[c, c].offered_size;
            } 
            
            for (int r = 0; r < row_matrix.GetUpperBound(0) + 1; r++) 
            {
                row_matrix[r, r].offered_size = row_matrix[r, r].desired_size;
                total_consumed.Height += row_matrix[r, r].offered_size;
            }

            if (total_consumed.Width != finalSize.Width)
            {
                this.ExpandStarCols(finalSize);
            }

            if (total_consumed.Height != finalSize.Height)
            {
                this.ExpandStarRows(finalSize);
            }

            for (int c = 0; c < col_count; c++)
            {
                this.ColumnDefinitions[c].ActualWidth = col_matrix[c, c].offered_size;
            }

            for (int r = 0; r < row_count; r++)
            {
                this.RowDefinitions[r].ActualHeight = row_matrix [r, r].offered_size;
            }

            foreach (UIElement child in VisualTreeHelper.GetChildren(this))
            {
                int col = Math.Min(GetColumn (child), col_matrix.GetUpperBound(0));
                int row = Math.Min(GetRow (child), row_matrix.GetUpperBound(0));
                int colspan = Math.Min(GetColumnSpan (child), col_matrix.GetUpperBound(0));
                int rowspan = Math.Min(GetRowSpan (child), row_matrix.GetUpperBound(0));

                Rect child_final = new Rect(0, 0, 0, 0);

                for (int c = 0; c < col; c++)
                {
                    child_final.X += col_matrix[c, c].offered_size;
                }

                for (int c = col; c < col + colspan; c++)
                {
                    child_final.Width += col_matrix[c, c].offered_size;
                }

                for (int r = 0; r < row; r++)
                {
                    child_final.Y += row_matrix[r, r].offered_size;
                }

                for (int r = row; r < row + rowspan; r++)
                {
                    child_final.Height += row_matrix[r, r].offered_size;
                }

                child.Arrange(child_final);
            }

            return finalSize;
        }

        private static double Clamp(double val, double min, double max)
        {
            if (val < min)
            {
                return min;
            }
            else if (val > max)
            {
                return max;
            }
            else
            {
                return val;
            }
        }

        private void CreateMatrices(int row_count, int col_count)
        {
            if (row_matrix == null || col_matrix == null || 
                row_matrix.GetUpperBound(0) != row_count - 1 || 
                col_matrix.GetUpperBound(0) != col_count - 1)
            {
                this.row_matrix = new Segment[row_count, row_count];
                this.col_matrix = new Segment[col_count, col_count];
            }
        }

        private void ExpandStarCols(Size availableSize)
        {
            int columns_count = this.ColumnDefinitions.Count;

            for (int i = 0; i < col_matrix.GetUpperBound(0) + 1; i++) 
            {
                if (col_matrix[i, i].type == GridUnitType.Star)
                {
                    col_matrix[i, i].offered_size = 0;
                }
                else
                {
                    availableSize.Width = Math.Max(availableSize.Width - col_matrix[i, i].offered_size, 0);
                }
            }

            double width = availableSize.Width;
            this.AssignSize(col_matrix, 0, col_matrix.GetUpperBound(0), ref width, GridUnitType.Star, false);
            availableSize.Width = Math.Max(0, width);

            if (columns_count > 0) 
            {
                for (int i = 0; i < col_matrix.GetUpperBound(0) + 1; i++)
                {
                    if (col_matrix[i, i].type == GridUnitType.Star)
                    {
                        this.ColumnDefinitions[i].ActualWidth = col_matrix[i, i].offered_size;
                    }
                }
            }
        }

        private void ExpandStarRows(Size availableSize)
        {
            int row_count = this.RowDefinitions.Count;

            // When expanding star rows, we need to zero out their height before
            // calling AssignSize. AssignSize takes care of distributing the 
            // available size when there are Mins and Maxs applied.
            for (int i = 0; i < row_matrix.GetUpperBound(0) + 1; i++) 
            {
                if (row_matrix[i, i].type == GridUnitType.Star)
                {
                    row_matrix[i, i].offered_size = 0.0;
                }
                else
                {
                    availableSize.Height = Math.Max(availableSize.Height - row_matrix[i, i].offered_size, 0);
                }
            }

            double height = availableSize.Height;
            this.AssignSize (row_matrix, 0, row_matrix.GetUpperBound(0), ref height, GridUnitType.Star, false);
            availableSize.Height = height;

            if (row_count > 0) 
            {
                for (int i = 0; i < row_matrix.GetUpperBound(0) + 1; i++)
                {
                    if (row_matrix[i, i].type == GridUnitType.Star)
                    {
                        this.RowDefinitions[i].ActualHeight = row_matrix[i, i].offered_size;
                    }
                }
            }
        }

        private void AssignSize(
            Segment[,] matrix, 
            int start, 
            int end, 
            ref double size, 
            GridUnitType type, 
            bool desired_size)
        {
            double count = 0;
            bool assigned;

            // Count how many segments are of the correct type. If we're measuring Star rows/cols
            // we need to count the number of stars instead.
            for (int i = start; i <= end; i++) 
            {
                double segment_size = desired_size ? matrix[i, i].desired_size : matrix[i, i].offered_size;
                if (segment_size < matrix[i, i].max)
                {
                    count += type == GridUnitType.Star ? matrix[i, i].stars : 1;
                }
            }

            do 
            {
                double contribution = size / count;

                assigned = false;

                for (int i = start; i <= end; i++) 
                {
                    double segment_size = desired_size ? matrix [i, i].desired_size : matrix [i, i].offered_size;

                    if (!(matrix[i, i].type == type && segment_size < matrix[i, i].max))
                    {
                        continue;
                    }

                    double newsize = segment_size;
                    newsize += contribution * (type == GridUnitType.Star ? matrix[i, i].stars : 1);
                    newsize = Math.Min(newsize, matrix[i, i].max);
                    assigned |= newsize > segment_size;
                    size -= newsize - segment_size;

                    if (desired_size)
                    {
                        matrix[i, i].desired_size = newsize;
                    }
                    else
                    {
                        matrix[i, i].offered_size = newsize;
                    }
                }
            } 
            while (assigned);
        }

        private void AllocateDesiredSize(int row_count, int col_count)
        {
            // First allocate the heights of the RowDefinitions, then allocate
            // the widths of the ColumnDefinitions.
            for (int i = 0; i < 2; i ++) 
            {
                Segment[,] matrix = i == 0 ? row_matrix : col_matrix;
                int count = i == 0 ? row_count : col_count;

                for (int row = count - 1; row >= 0; row--) 
                {
                    for (int col = row; col >= 0; col--) 
                    {
                        bool spans_star = false;
                        for (int j = row; j >= col; j--)
                        {
                            spans_star |= matrix[j, j].type == GridUnitType.Star;
                        }

                        // This is the amount of pixels which must be available between the grid rows
                        // at index 'col' and 'row'. i.e. if 'row' == 0 and 'col' == 2, there must
                        // be at least 'matrix [row][col].size' pixels of height allocated between
                        // all the rows in the range col -> row.
                        double current = matrix[row, col].desired_size;

                        // Count how many pixels have already been allocated between the grid rows
                        // in the range col -> row. The amount of pixels allocated to each grid row/column
                        // is found on the diagonal of the matrix.
                        double total_allocated = 0;
                        
                        for (int k = row; k >= col; k--)
                        {
                            total_allocated += matrix[k, k].desired_size;
                        }

                        // If the size requirement has not been met, allocate the additional required
                        // size between 'pixel' rows, then 'star' rows, finally 'auto' rows, until all
                        // height has been assigned.
                        if (total_allocated < current) 
                        {
                            double additional = current - total_allocated;

                            if (spans_star) 
                            {
                                this.AssignSize(matrix, col, row, ref additional, GridUnitType.Star, true);
                            } 
                            else 
                            {
                                this.AssignSize(matrix, col, row, ref additional, GridUnitType.Pixel, true);
                                this.AssignSize (matrix, col, row, ref additional, GridUnitType.Auto, true);
                            }
                        }
                    }
                }
            }

            for (int r = 0; r < row_matrix.GetUpperBound(0) + 1; r++)
            {
                row_matrix[r, r].offered_size = row_matrix[r, r].desired_size;
            }

            for (int c = 0; c < col_matrix.GetUpperBound(0) + 1; c++)
            {
                col_matrix[c, c].offered_size = col_matrix[c, c].desired_size;
            }
        }

        private void SaveMeasureResults()
        {
            for (int i = 0; i < row_matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < row_matrix.GetUpperBound(0) + 1; j++)
                {
                    row_matrix[i, j].original_size = row_matrix[i, j].offered_size;
                }
            }

            for (int i = 0; i < col_matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j < col_matrix.GetUpperBound(0); j++)
                {
                    col_matrix[i, j].original_size = col_matrix[i, j].offered_size;
                }
            }
        }

        private void RestoreMeasureResults()
        {
            for (int i = 0; i < row_matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < row_matrix.GetUpperBound(0) + 1; j++)
                {
                    row_matrix[i, j].offered_size = row_matrix[i, j].original_size;
                }
            }

            for (int i = 0; i < col_matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < col_matrix.GetUpperBound(0) + 1; j++)
                {
                    col_matrix[i, j].offered_size = col_matrix[i, j].original_size;
                }
            }
        }

        private struct Segment
        {
            public double original_size;
            public double max;
            public double min;
            public double desired_size;
            public double offered_size;
            public double stars;
            public GridUnitType type;

            public Segment (double offered_size, double min, double max, GridUnitType type)
            {
                this.original_size = 0;
                this.min = min;
                this.max = max;
                this.desired_size = 0;
                this.offered_size = offered_size;
                this.stars = 0;
                this.type = type;
            }

            public void Init (double offered_size, double min, double max, GridUnitType type)
            {
                this.offered_size = offered_size;
                this.min = min;
                this.max = max;
                this.type = type;
            }
        }

        private struct GridNode
        {
            public int row;
            public int col;
            public double size;
            public Segment[,] matrix;

            public GridNode(Segment[,] matrix, int row, int col, double size)
            {
                this.matrix = matrix;
                this.row = row;
                this.col = col;
                this.size = size;
            }
        }

        private class GridWalker 
        {
            public GridWalker (Grid grid, Segment[,] row_matrix, Segment[,] col_matrix)
            {
                foreach (UIElement child in VisualTreeHelper.GetChildren(grid))
                {
                    bool star_col = false;
                    bool star_row = false;
                    bool auto_col = false;
                    bool auto_row = false;

                    int col = Math.Min(Grid.GetColumn(child), col_matrix.GetUpperBound(0));
                    int row = Math.Min(Grid.GetRow(child), row_matrix.GetUpperBound(0));
                    int colspan = Math.Min(Grid.GetColumnSpan(child), col_matrix.GetUpperBound(0));
                    int rowspan = Math.Min(Grid.GetRowSpan(child), row_matrix.GetUpperBound(0));

                    for (int r = row; r < row + rowspan; r++) 
                    {
                        star_row |= row_matrix[r, r].type == GridUnitType.Star;
                        auto_row |= row_matrix[r, r].type == GridUnitType.Auto;
                    }

                    for (int c = col; c < col + colspan; c++) 
                    {
                        star_col |= col_matrix[c, c].type == GridUnitType.Star;
                        auto_col |= col_matrix[c, c].type == GridUnitType.Auto;
                    }

                    this.HasAutoAuto |= auto_row && auto_col && !star_row && !star_col;
                    this.HasStarAuto |= star_row && auto_col;
                    this.HasAutoStar |= auto_row && star_col;
                }
            }

            public bool HasAutoAuto { get; private set; }
            public bool HasStarAuto { get; private set; }
            public bool HasAutoStar { get; private set; }
        };
    }
}
