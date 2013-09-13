// -----------------------------------------------------------------------
// <copyright file="ToggleButton.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System.ComponentModel;
    using Avalonia.Input;

    public class ToggleButton : ButtonBase
    {
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                "IsChecked",
                typeof(bool?),
                typeof(ToggleButton),
                new FrameworkPropertyMetadata(
                    false, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool? IsChecked 
        {
            get { return (bool?)this.GetValue(IsCheckedProperty); }
            set { this.SetValue(IsCheckedProperty, value); }
        }

        protected override void OnClick()
        {
            bool? isChecked = this.IsChecked;
            this.IsChecked = (isChecked.HasValue) ? !isChecked : false;
            base.OnClick();
        }
    }
}
