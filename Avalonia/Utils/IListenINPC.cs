// -----------------------------------------------------------------------
// <copyright file="IListenINPC.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Utils
{
    using System.ComponentModel;

    internal interface IListenINPC
    {
        void OnPropertyChanged(object o, PropertyChangedEventArgs e);
    }
}
