using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform;

namespace Avalonia.Direct2D1
{
    public class Direct2DPlatformFactory : PlatformFactory
    {
        public Direct2DPlatformFactory()
        {
            this.Dispatcher = new WindowMessageDispatcher();
        }

        public override IPlatformDispatcher Dispatcher { get; protected set; }

        public override PlatformPresentationSource CreatePresentationSource()
        {
            return new HwndSource();
        }
    }
}
