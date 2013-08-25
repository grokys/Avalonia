// -----------------------------------------------------------------------
// <copyright file="TextBlock.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Avalonia.Media;

    public class TextElement : DependencyObject
    {
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.RegisterAttached(
                "FontFamily",
                typeof(FontFamily),
                typeof(TextElement),
                new FrameworkPropertyMetadata(
                    new FontFamily("Segoe UI"),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.RegisterAttached(
                "FontSize",
                typeof(double),
                typeof(TextElement),
                new FrameworkPropertyMetadata(
                    12.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontStretchProperty =
            DependencyProperty.RegisterAttached(
                "FontStretch",
                typeof(FontStretch),
                typeof(TextElement),
                new FrameworkPropertyMetadata(
                    new FontStretch(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontStyleProperty =
            DependencyProperty.RegisterAttached(
                "FontStyle",
                typeof(FontStyle),
                typeof(TextElement),
                new FrameworkPropertyMetadata(
                    FontStyles.Normal,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FontWeightProperty =
            DependencyProperty.RegisterAttached(
                "FontWeight",
                typeof(FontWeight),
                typeof(TextElement),
                new FrameworkPropertyMetadata(
                    FontWeights.Normal,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
    }
}
