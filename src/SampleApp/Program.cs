
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace SampleApp {

    class Program {

        static void Main(string[] args) {
            var dir = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var assemblyPath = Path.Combine(dir, args[0]);

            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);

            var dependencyContext = DependencyContext.Load(assembly);
            
            var assemblyResolver = new CompositeCompilationAssemblyResolver(
                new ICompilationAssemblyResolver[] {
                    new AppBaseCompilationAssemblyResolver(Path.GetDirectoryName(assemblyPath)),
                    new ReferenceAssemblyPathResolver(),
                    new PackageCompilationAssemblyResolver()
            });

            var name = new AssemblyName("Ninject, Version=3.3.0.0, Culture=neutral, PublicKeyToken=c7192dc5380945e7");
            var runtimeLibrary = dependencyContext.RuntimeLibraries.First(
                (rl) => string.Equals(rl.Name, name.Name, StringComparison.OrdinalIgnoreCase));
            
            var compilationLibrary = new CompilationLibrary(
                runtimeLibrary.Type,
                runtimeLibrary.Name,
                runtimeLibrary.Version,
                runtimeLibrary.Hash,
                runtimeLibrary.RuntimeAssemblyGroups.SelectMany(g => g.AssetPaths),
                runtimeLibrary.Dependencies,
                runtimeLibrary.Serviceable
            );
            
            var assemblies = new List<string>();
            assemblyResolver.TryResolveAssemblyPaths(compilationLibrary, assemblies);
            Console.Write(assemblies.Count);  // 0 / zero when executed from published context
        }
    }
}
