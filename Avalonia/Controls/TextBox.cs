// -----------------------------------------------------------------------
// <copyright file="TextBox.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using Avalonia.Controls.Primitives;
    using Avalonia.Input;
    using Avalonia.Media;

    public class TextBox : TextBoxBase
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBox),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                    TextChanged));

        private TextBoxView textBoxView;

        private int caretIndex;

        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }

        public int CaretIndex 
        { 
            get
            {
                return this.caretIndex;
            }
            
            set
            {
                value = Math.Min(Math.Max(value, 0), this.Text.Length);

                if (this.caretIndex != value)
                {
                    this.caretIndex = value;
                    this.textBoxView.InvalidateVisual();
                }
            }
        }

        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        protected internal override IEnumerator LogicalChildren
        {
            get { return new[] { this.Text }.GetEnumerator(); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CreateTextBoxView();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            string text = this.Text;

            switch (e.Key)
            {
                case Key.Left:
                    --this.CaretIndex;
                    break;
                
                case Key.Right:
                    ++this.CaretIndex;
                    break;

                case Key.Back:
                    if (this.caretIndex > 0)
                    {                        
                        this.Text = text.Substring(0, this.caretIndex - 1) + text.Substring(this.caretIndex);
                        --this.CaretIndex;
                    }

                    break;

                case Key.Delete:
                    if (this.caretIndex < text.Length)
                    {
                        this.Text = text.Substring(0, this.caretIndex) + text.Substring(this.caretIndex + 1);
                    }

                    break;
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            string text = this.Text;
            this.Text = text.Substring(0, this.caretIndex) + e.Text + text.Substring(this.caretIndex);
            ++this.CaretIndex;
            e.Handled = true;
        }

        private static void TextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.textBoxView != null)
            {
                textBox.textBoxView.InvalidateText();
            }
        }

        private void CreateTextBoxView()
        {
            // TODO: This should be a ScrollViewer but that's not implemented yet...
            Decorator contentHost = this.GetTemplateChild("PART_ContentHost") as Decorator;

            if (contentHost != null)
            {
                this.textBoxView = new TextBoxView(this);
                contentHost.Child = this.textBoxView;
            }
        }
    }
}
