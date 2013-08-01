// -----------------------------------------------------------------------
// <copyright file="DispatcherProcessingDisabled.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Threading
{
    using System;

    public struct DispatcherProcessingDisabled : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatcherProcessingDisabled"/> struct.
        /// </summary>
        internal DispatcherProcessingDisabled(int foo)
        {
        }

        public static bool operator !=(DispatcherProcessingDisabled left, DispatcherProcessingDisabled right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(DispatcherProcessingDisabled left, DispatcherProcessingDisabled right)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}