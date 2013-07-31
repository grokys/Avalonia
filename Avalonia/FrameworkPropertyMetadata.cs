// -----------------------------------------------------------------------
// <copyright file="FrameworkPropertyMetadata.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using Avalonia.Data;

    [Flags]
    public enum FrameworkPropertyMetadataOptions
    {
        None = 0,
        AffectsMeasure = 0x01,
        AffectsArrange = 0x02,
        AffectsParentMeasure = 0x04,
        AffectsParentArrange = 0x08,
        AffectsRender = 0x10,
        Inherits = 0x11,
        OverridesInheritanceBehavior = 0x12,
        NotDataBindable = 0x14,
        BindsTwoWayByDefault = 0x18,
        Journal = 0x400,
        SubPropertiesDoNotAffectRender = 0x800,
    }

    public class FrameworkPropertyMetadata : UIPropertyMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(object defaultValue)
                    : base(defaultValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(PropertyChangedCallback propertyChangedCallback)
                    : base(propertyChangedCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    object defaultValue,
                    FrameworkPropertyMetadataOptions flags)
                    : base(defaultValue)
        {
            this.LoadFlags(flags);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    object defaultValue,
                    PropertyChangedCallback propertyChangedCallback)
                    : base(defaultValue, propertyChangedCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    PropertyChangedCallback propertyChangedCallback,
                    CoerceValueCallback coerceValueCallback)
                    : base(propertyChangedCallback)
        {
            this.CoerceValueCallback = coerceValueCallback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    object defaultValue,
                    FrameworkPropertyMetadataOptions flags,
                    PropertyChangedCallback propertyChangedCallback)
                    : base(defaultValue, propertyChangedCallback)
        {
            this.LoadFlags(flags);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    object defaultValue,
                    PropertyChangedCallback propertyChangedCallback,
                    CoerceValueCallback coerceValueCallback)
                    : base(defaultValue, propertyChangedCallback, coerceValueCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    object defaultValue,
                    FrameworkPropertyMetadataOptions flags,
                    PropertyChangedCallback propertyChangedCallback,
                    CoerceValueCallback coerceValueCallback)
                    : base(defaultValue, propertyChangedCallback, coerceValueCallback)
        {
            this.LoadFlags(flags);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    object defaultValue,
                    FrameworkPropertyMetadataOptions flags,
                    PropertyChangedCallback propertyChangedCallback,
                    CoerceValueCallback coerceValueCallback,
                    bool isAnimationProhibited)
                    : base(defaultValue, propertyChangedCallback, coerceValueCallback, isAnimationProhibited)
        {
            this.LoadFlags(flags);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkPropertyMetadata"/> class.
        /// </summary>
        public FrameworkPropertyMetadata(
                    object defaultValue,
                    FrameworkPropertyMetadataOptions flags,
                    PropertyChangedCallback propertyChangedCallback,
                    CoerceValueCallback coerceValueCallback,
                    bool isAnimationProhibited,
                    UpdateSourceTrigger defaultUpdateSourceTrigger)
                    : base(defaultValue, propertyChangedCallback, coerceValueCallback, isAnimationProhibited)
        {
            this.DefaultUpdateSourceTrigger = defaultUpdateSourceTrigger;
            this.LoadFlags(flags);
        }

        public bool AffectsArrange { get; set; }

        public bool AffectsMeasure { get; set; }

        public bool AffectsParentArrange { get; set; }

        public bool AffectsParentMeasure { get; set; }

        public bool AffectsRender { get; set; }

        public bool BindsTwoWayByDefault { get; set; }

        public UpdateSourceTrigger DefaultUpdateSourceTrigger { get; set; }

        public bool Inherits { get; set; }

        public bool IsNotDataBindable { get; set; }

        public bool Journal { get; set; }

        public bool OverridesInheritanceBehavior { get; set; }

        public bool SubPropertiesDoNotAffectRender { get; set; }

        private void LoadFlags(FrameworkPropertyMetadataOptions flags)
        {
            this.AffectsArrange = (flags & FrameworkPropertyMetadataOptions.AffectsArrange) != 0;
            this.AffectsMeasure = (flags & FrameworkPropertyMetadataOptions.AffectsMeasure) != 0;
            this.AffectsParentArrange = (flags & FrameworkPropertyMetadataOptions.AffectsParentArrange) != 0;
            this.AffectsParentMeasure = (flags & FrameworkPropertyMetadataOptions.AffectsParentMeasure) != 0;
            this.AffectsRender = (flags & FrameworkPropertyMetadataOptions.AffectsRender) != 0;
            this.BindsTwoWayByDefault = (flags & FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) != 0;
            this.Inherits = (flags & FrameworkPropertyMetadataOptions.Inherits) != 0;
            this.IsNotDataBindable = (flags & FrameworkPropertyMetadataOptions.NotDataBindable) != 0;
            this.Journal = (flags & FrameworkPropertyMetadataOptions.Journal) != 0;
            this.OverridesInheritanceBehavior = (flags & FrameworkPropertyMetadataOptions.OverridesInheritanceBehavior) != 0;
            this.SubPropertiesDoNotAffectRender = (flags & FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender) != 0;
        }
    }
}