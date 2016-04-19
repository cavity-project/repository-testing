using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Testing.Repository.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Repository.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Repository Testing Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Repository Testing Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]