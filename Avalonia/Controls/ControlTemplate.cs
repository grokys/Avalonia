// -----------------------------------------------------------------------
// <copyright file="ControlTemplate.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Windows.Markup;
    using Avalonia.Media;

    public class ControlTemplate : FrameworkTemplate
    {
        [AmbientAttribute]
        public Type TargetType { get; set; }
    }
}
