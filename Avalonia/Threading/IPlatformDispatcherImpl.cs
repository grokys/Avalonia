// -----------------------------------------------------------------------
// <copyright file="IPlatformDispatcherImpl.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AvaloniaSpecific]
    public interface IPlatformDispatcherImpl
    {
        void ProcessMessage();

        void SendMessage();
    }
}
