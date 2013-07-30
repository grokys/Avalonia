namespace Avalonia.Direct2D1
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using Avalonia.Direct2D1.Interop;
    using Avalonia.Input;
    using Avalonia.Media;
    using SharpDX.Direct2D1;
    using SharpDX.DXGI;

    public class HwndSource : AvaloniaPresentationSource, IDisposable
    {
        private string className;
        private WindowRenderTarget renderTarget;

        [AvaloniaSpecific]
        public HwndSource()
        {
            HwndSourceParameters parameters = new HwndSourceParameters
            {
                PositionX = UnmanagedMethods.CW_USEDEFAULT,
                PositionY = UnmanagedMethods.CW_USEDEFAULT,
                Width = UnmanagedMethods.CW_USEDEFAULT,
                Height = UnmanagedMethods.CW_USEDEFAULT,
                WindowStyle = (int)UnmanagedMethods.WindowStyles.WS_OVERLAPPEDWINDOW
            };

            this.Initialize(parameters);
        }

        public HwndSource(HwndSourceParameters parameters)
        {
            this.Initialize(parameters);
        }

        public HwndSource(int classStyle, int style, int exStyle, int x, int y, string name, IntPtr parent)
            : this(new HwndSourceParameters
            {
                WindowClassStyle = classStyle,
                WindowStyle = style,
                ExtendedWindowStyle = exStyle,
                PositionX = x,
                PositionY = y,
                WindowName = name,
                ParentWindow = parent,
            })
        {
        }

        public HwndSource(int classStyle, int style, int exStyle, int x, int y, int width, int height, string name, IntPtr parent)
            : this(new HwndSourceParameters
            {
                WindowClassStyle = classStyle,
                WindowStyle = style,
                ExtendedWindowStyle = exStyle,
                PositionX = x,
                PositionY = y,
                Width = width,
                Height = height,
                WindowName = name,
                ParentWindow = parent,
            })
        {

        }

        public HwndSource(int classStyle, int style, int exStyle, int x, int y, int width, int height, string name, IntPtr parent, bool adjustSizingForNonClientArea)
            : this(new HwndSourceParameters
            {
                WindowClassStyle = classStyle,
                WindowStyle = style,
                ExtendedWindowStyle = exStyle,
                PositionX = x,
                PositionY = y,
                Width = width,
                Height = height,
                WindowName = name,
                ParentWindow = parent,
            })
        {
        }

        public IntPtr Handle { get; private set; }
        public override Visual RootVisual { get; set; }

        [AvaloniaSpecific]
        public override Rect BoundingRect
        {
            get
            {
                UnmanagedMethods.RECT rect;
                UnmanagedMethods.GetWindowRect(this.Handle, out rect);
                return new Rect(new Point(rect.left, rect.top), new Point(rect.right, rect.bottom));
            }

            set
            {
                UnmanagedMethods.SetWindowPos(
                    this.Handle,
                    IntPtr.Zero,
                    (int)value.X,
                    (int)value.Y,
                    (int)value.Width,
                    (int)value.Height,
                    UnmanagedMethods.SetWindowPosFlags.SWP_RESIZE);
            }
        }

        [AvaloniaSpecific]
        public override Size ClientSize
        {
            get
            {
                UnmanagedMethods.RECT rect;
                UnmanagedMethods.GetClientRect(this.Handle, out rect);
                return new Size(rect.right, rect.bottom);
            }
        }

        [AvaloniaSpecific]
        public override DrawingContext CreateDrawingContext()
        {
            return new Direct2D1DrawingContext(this.renderTarget);
        }

        public void Dispose()
        {
            if (this.Handle != IntPtr.Zero)
            {
                UnmanagedMethods.DestroyWindow(this.Handle);
                UnmanagedMethods.UnregisterClass(this.className, IntPtr.Zero);
                this.Handle = IntPtr.Zero;
            }
        }

        [AvaloniaSpecific]
        public override void Show()
        {
            this.CreateRenderTarget();
            UnmanagedMethods.ShowWindow(this.Handle, 4);
        }

        private void Initialize(HwndSourceParameters parameters)
        {
            this.className = "Avalonia.HwndSource|" + Guid.NewGuid().ToString();

            UnmanagedMethods.WNDCLASSEX wndClassEx = new UnmanagedMethods.WNDCLASSEX
            {
                cbSize = Marshal.SizeOf(typeof(UnmanagedMethods.WNDCLASSEX)),
                style = parameters.WindowClassStyle,
                lpfnWndProc = this.WndProc,
                hInstance = Marshal.GetHINSTANCE(this.GetType().Module),
                lpszClassName = className,
            };

            ushort atom = UnmanagedMethods.RegisterClassEx(ref wndClassEx);

            if (atom == 0)
            {
                throw new Win32Exception();
            }

            this.Handle = UnmanagedMethods.CreateWindowEx(
                parameters.ExtendedWindowStyle,
                atom,
                parameters.WindowName,
                parameters.WindowStyle,
                parameters.PositionX,
                parameters.PositionY,
                parameters.Width,
                parameters.Height,
                parameters.ParentWindow,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);

            if (this.Handle == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
        }

        private void CreateRenderTarget()
        {
            Size clientSize = this.ClientSize;

            this.renderTarget = new WindowRenderTarget(
                new SharpDX.Direct2D1.Factory(),
                new RenderTargetProperties
                {
                    PixelFormat = new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)                    
                },
                new HwndRenderTargetProperties
                {
                    Hwnd = this.Handle,
                    PixelSize = new SharpDX.DrawingSize((int)clientSize.Width, (int)clientSize.Height),
                });
        }

        private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch ((UnmanagedMethods.WindowsMessage)msg)
            {
                case UnmanagedMethods.WindowsMessage.WM_DESTROY:
                    this.OnClosed();
                    break;

                case UnmanagedMethods.WindowsMessage.WM_LBUTTONDOWN:
                    this.OnMouseButtonDown(new MouseButtonEventArgs());
                    break;

                case UnmanagedMethods.WindowsMessage.WM_SIZE:
                    if (this.renderTarget != null)
                    {
                        this.renderTarget.Resize(new SharpDX.DrawingSize((int)lParam & 0xffff, (int)lParam >> 16));
                    }

                    this.OnResized();
                    return IntPtr.Zero;
            }

            return UnmanagedMethods.DefWindowProc(hWnd, msg, wParam, lParam);
        }
    }
}
