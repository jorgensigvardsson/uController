﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using uController;

namespace System.Reflection
{
    public class MetadataLoadContext
    {
        private readonly Dictionary<string, IAssemblySymbol> _assemblies = new(StringComparer.OrdinalIgnoreCase);
        private readonly Compilation _compilation;

        public MetadataLoadContext(Compilation compilation)
        {
            _compilation = compilation;
            
            var assemblies = compilation.References
                                        .OfType<PortableExecutableReference>()
                                        .ToDictionary(r => AssemblyName.GetAssemblyName(r.FilePath),
                                                      r => (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(r));

            foreach (var item in assemblies)
            {
                // REVIEW: We need to figure out full framework
                // _assemblies[item.Key.FullName] = item.Value;
                _assemblies[item.Key.Name] = item.Value;
            }

            CoreAssembly = new AssemblyWrapper(compilation.GetTypeByMetadataName("System.Object").ContainingAssembly, this);
            MainAssembly = new AssemblyWrapper(compilation.Assembly, this);
        }

        public Type Resolve<T>() => Resolve(typeof(T));

        public Type Resolve(Type type)
        {
            var asmName = type.Assembly.GetName().Name;

            IAssemblySymbol assemblySymbol;

            if (asmName == "System.Private.CoreLib" || asmName == "mscorlib" || asmName == "System.Runtime")
            {
                assemblySymbol = CoreAssembly.Symbol;
            }
            else
            {
                var typeForwardedFrom = type.GetCustomAttributeData(typeof(TypeForwardedFromAttribute));

                if (typeForwardedFrom != null)
                {
                    asmName = typeForwardedFrom.GetConstructorArgument<string>(0);
                }

                if (!_assemblies.TryGetValue(new AssemblyName(asmName).Name, out assemblySymbol))
                {
                    return null;
                }
            }

            if (type.IsArray)
            {
                var typeSymbol = assemblySymbol.GetTypeByMetadataName(type.GetElementType().FullName);
                if (typeSymbol == null)
                {
                    return null;
                }

                return _compilation.CreateArrayTypeSymbol(typeSymbol).AsType(this);
            }

            // Resolve the full name
            return assemblySymbol.GetTypeByMetadataName(type.FullName).AsType(this);
        }

        private AssemblyWrapper CoreAssembly { get; }
        public Assembly MainAssembly { get; }

        internal Assembly LoadFromAssemblyName(string fullName)
        {
            if (_assemblies.TryGetValue(new AssemblyName(fullName).Name, out var assembly))
            {
                return new AssemblyWrapper(assembly, this);
            }
            return null;
        }
    }
}
