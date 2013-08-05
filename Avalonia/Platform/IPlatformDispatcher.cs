// -----------------------------------------------------------------------
// <copyright file="IPlatformDispatcher.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [AvaloniaSpecific]
    public interface IPlatformDispatcher
    {
        void ProcessMessage();

        void SendMessage();
    }
}
