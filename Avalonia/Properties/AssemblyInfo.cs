// -----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Avalonia")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Avalonia")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("6acfae5c-58d1-4cb5-a69c-b8f7aa43b5f8")]

// Version information for an assembly consists of the following four values:
//
// Major Version
// Minor Version 
// Build Number
// Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: XmlnsDefinition("https://github.com/grokys/Avalonia", "Avalonia")]
[assembly: XmlnsDefinition("https://github.com/grokys/Avalonia", "Avalonia.Controls")]
[assembly: XmlnsDefinition("https://github.com/grokys/Avalonia", "Avalonia.Controls.Primitives")]
[assembly: XmlnsDefinition("https://github.com/grokys/Avalonia", "Avalonia.Data")]
[assembly: XmlnsDefinition("https://github.com/grokys/Avalonia", "Avalonia.Shapes")]

[assembly: XmlnsCompatibleWith("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "https://github.com/grokys/Avalonia")]

[assembly: InternalsVisibleTo("Avalonia.UnitTests")]