namespace Avalonia.UnitTests.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WpfInputManagerTests
    {
        [TestMethod]
        public void Event_Should_Be_Routed_To_Device_Target()
        {
            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;
            InputManager.Current.ProcessInput(e);

            Assert.IsTrue(element.RawEventRaised);
        }

        [TestMethod]
        public void Event_Should_Not_Be_Routed_If_Cancelled()
        {
            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                ev.Cancel();
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsFalse(element.RawEventRaised);
        }

        [TestMethod]
        public void PreProcessInput_Should_Be_Called()
        {
            bool called = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                called = true;
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void PreNotifyInput_Should_Be_Called()
        {
            bool notified = false;

            NotifyInputEventHandler preNotify = (sender, ev) =>
            {
                notified = true;
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreNotifyInput += preNotify;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreNotifyInput -= preNotify;

            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void PreNotifyInput_Should_Not_Be_Called_If_Cancelled()
        {
            bool notified = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                ev.Cancel();
            };

            NotifyInputEventHandler preNotify = (sender, ev) =>
            {
                notified = true;
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.PreNotifyInput += preNotify;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreNotifyInput -= preNotify;
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsFalse(notified);
        }

        [TestMethod]
        public void PostNotifyInput_Should_Be_Called()
        {
            bool notified = false;

            NotifyInputEventHandler postNotify = (sender, ev) =>
            {
                notified = true;
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PostNotifyInput += postNotify;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PostNotifyInput -= postNotify;

            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void PostNotifyInput_Should_Not_Be_Called_If_Cancelled()
        {
            bool notified = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                ev.Cancel();
            };

            NotifyInputEventHandler postNotify = (sender, ev) =>
            {
                notified = true;
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.PostNotifyInput += postNotify;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PostNotifyInput -= postNotify;
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsFalse(notified);
        }

        [TestMethod]
        public void PostProcessInput_Should_Be_Called()
        {
            bool notified = false;

            ProcessInputEventHandler postProcess = (sender, ev) =>
            {
                notified = true;
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PostProcessInput += postProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PostProcessInput -= postProcess;

            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void PostProcessInput_Should_Not_Be_Called_If_Cancelled()
        {
            bool notified = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                ev.Cancel();
            };

            ProcessInputEventHandler postProcess = (sender, ev) =>
            {
                notified = true;
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.PostProcessInput += postProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PostProcessInput += postProcess;
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsFalse(notified);
        }

        [TestMethod]
        public void Raw_And_Mutated_Events_Should_Be_Raised()
        {
            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsTrue(element.RawEventRaised);
            Assert.IsTrue(element.MutatedEventRaised);
        }

        [TestMethod]
        public void Mutated_Event_Should_Still_Be_Raised_When_Raw_Event_Cancelled()
        {
            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                    ev.Cancel();
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsFalse(element.RawEventRaised);
            Assert.IsTrue(element.MutatedEventRaised);
        }

        [TestMethod]
        public void PreProcess_Event_Should_Be_Called_With_Mutated_Event()
        {
            bool calledWithMutated = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                    ev.Cancel();
                }
                else if (ev.StagingItem.Input.RoutedEvent == InputElement.MutatedEvent)
                {
                    calledWithMutated = true;
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsTrue(calledWithMutated);
        }

        [TestMethod]
        public void PreNotify_Event_Should_Be_Called_With_Mutated_Event()
        {
            bool calledWithMutated = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                    ev.Cancel();
                }
            };

            NotifyInputEventHandler preNotify = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.MutatedEvent)
                {
                    calledWithMutated = true;
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.PreNotifyInput += preNotify;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreNotifyInput -= preNotify;
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsTrue(calledWithMutated);
        }

        [TestMethod]
        public void PostNotify_Event_Should_Be_Called_With_Mutated_Event()
        {
            bool calledWithMutated = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                    ev.Cancel();
                }
            };

            NotifyInputEventHandler postNotify = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.MutatedEvent)
                {
                    calledWithMutated = true;
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.PostNotifyInput += postNotify;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PostNotifyInput += postNotify;
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsTrue(calledWithMutated);
        }

        [TestMethod]
        public void PostProcess_Event_Should_Be_Called_With_Mutated_Event()
        {
            bool calledWithMutated = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                    ev.Cancel();
                }
            };

            ProcessInputEventHandler postProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.MutatedEvent)
                {
                    calledWithMutated = true;
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.PostProcessInput += postProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PostProcessInput += postProcess;
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsTrue(calledWithMutated);
        }

        [TestMethod]
        public void ProcessInput_Should_Return_False_If_No_Events_Handled()
        {
            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            bool result = InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProcessInput_Should_Return_True_If_One_Event_Handled()
        {
            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    StagingAreaInputItem stagingItem = CreateStagingItem(mutated);
                    ev.PushInput(stagingItem);
                }
            };

            InputElement element = new InputElement();
            element.SetMutatedEventHandled = true;

            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            bool result = InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            // This goes against the documentation which says should return true if all events handled.
            Assert.IsTrue(element.RawEventRaised);
            Assert.IsTrue(element.MutatedEventRaised);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Mutated_Event_Should_Be_Raised_With_Nested_ProcessInput()
        {
            bool calledWithMutated = false;

            PreProcessInputEventHandler preProcess = (sender, ev) =>
            {
                if (ev.StagingItem.Input.RoutedEvent == InputElement.RawEvent)
                {
                    TestEventArgs mutated = new TestEventArgs(ev.StagingItem.Input.Device.Target);
                    mutated.RoutedEvent = InputElement.MutatedEvent;
                    InputManager.Current.ProcessInput(mutated);
                }
                else if (ev.StagingItem.Input.RoutedEvent == InputElement.MutatedEvent)
                {
                    calledWithMutated = true;
                }
            };

            InputElement element = new InputElement();
            TestEventArgs e = new TestEventArgs(element);
            e.RoutedEvent = InputElement.RawEvent;

            InputManager.Current.PreProcessInput += preProcess;
            InputManager.Current.ProcessInput(e);
            InputManager.Current.PreProcessInput -= preProcess;

            Assert.IsTrue(element.RawEventRaised);
            Assert.IsTrue(element.MutatedEventRaised);
            Assert.IsTrue(calledWithMutated);
        }

        private static StagingAreaInputItem CreateStagingItem(TestEventArgs e)
        {
            ConstructorInfo c = typeof(StagingAreaInputItem).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[] { typeof(bool) },
                null);
            StagingAreaInputItem result = (StagingAreaInputItem)c.Invoke(new object[] { false });
            FieldInfo f = typeof(StagingAreaInputItem).GetField("_input", BindingFlags.NonPublic | BindingFlags.Instance);
            f.SetValue(result, e);
            return result;
        }

        private class InputElement : UIElement
        {
            public static readonly RoutedEvent RawEvent = EventManager.RegisterRoutedEvent(
                "Raw",
                RoutingStrategy.Direct,
                typeof(TestEventHandler),
                typeof(InputElement));

            public static readonly RoutedEvent MutatedEvent = EventManager.RegisterRoutedEvent(
                "Mutated",
                RoutingStrategy.Direct,
                typeof(TestEventHandler),
                typeof(InputElement));

            public InputElement()
            {
                this.AddHandler(RawEvent, (TestEventHandler)OnRawEvent);
                this.AddHandler(MutatedEvent, (TestEventHandler)OnMutatedEvent);
            }

            public bool RawEventRaised { get; private set; }

            public bool MutatedEventRaised { get; private set; }

            public bool SetRawEventHandled { get; set; }

            public bool SetMutatedEventHandled { get; set; }

            private void OnRawEvent(object sender, InputEventArgs e)
            {
                this.RawEventRaised = true;
                e.Handled = this.SetRawEventHandled;
            }

            private void OnMutatedEvent(object sender, InputEventArgs e)
            {
                this.MutatedEventRaised = true;
                e.Handled = SetMutatedEventHandled;
            }
        }

        private class TestInputDevice : InputDevice
        {
            private IInputElement target;

            public TestInputDevice(IInputElement target)
            {
                this.target = target;
            }

            public override PresentationSource ActiveSource
            {
                get { throw new NotImplementedException(); }
            }

            public override IInputElement Target
            {
                get { return this.target; }
            }
        }

        private class TestEventArgs : InputEventArgs
        {
            public TestEventArgs(IInputElement input)
                : base(new TestInputDevice(input), 0)
            {
            }

            protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
            {
                TestEventHandler handler = (TestEventHandler)genericHandler;
                handler(genericTarget, this);
            }
        }

        private delegate void TestEventHandler(object sender, TestEventArgs e);
    }
}
