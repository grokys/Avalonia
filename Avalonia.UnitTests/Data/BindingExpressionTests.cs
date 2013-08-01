using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Avalonia.UnitTests.Data
{
    [TestClass]
    public class BindingExpressionTests
    {
        [TestMethod]
        public void AttachPropertyChangedHandler_Should_Be_Called_On_Chain()
        {
            var source = new Mock<IObservableDependencyObject>();
            var foo = new Mock<IObservableDependencyObject>();
            var bar = new Mock<IObservableDependencyObject>();

            Mock<IPropertyPathParser> mockPathParser = new Mock<IPropertyPathParser>();
            mockPathParser.Setup(x => x.Parse(source.Object, "Foo.Bar")).Returns(new[]
            {
                new PropertyPathToken(source.Object, "Foo"),
                new PropertyPathToken(foo.Object, "Bar"),
                new PropertyPathToken(bar.Object, null),
            });

            Mock<DependencyObject> mockTarget = new Mock<DependencyObject>();

            Binding binding = new Binding
            {
                Path = new PropertyPath("Foo.Bar"),
                Source = source.Object,
            };

            BindingExpression target = new BindingExpression(
                mockPathParser.Object,
                mockTarget.Object,
                Control.BackgroundProperty,
                binding);

            target.GetValue();

            source.Verify(x => x.AttachPropertyChangedHandler("Foo", It.IsAny<DependencyPropertyChangedEventHandler>()));
            foo.Verify(x => x.AttachPropertyChangedHandler("Bar", It.IsAny<DependencyPropertyChangedEventHandler>()));
            bar.Verify(x => x.AttachPropertyChangedHandler("Bar", It.IsAny<DependencyPropertyChangedEventHandler>()), Times.Never());
        }
    }
}
