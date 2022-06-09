using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0129;

using Instances = R5T.X0009.Instances;


namespace System
{
    public static class CompilationUnitSyntaxExtensions
    {
        public static CompilationUnitSyntax AddUsings_NonIdempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var usingDirectives = namespaceNames
                .Select(xNamespaceName => Instances.SyntaxGenerator_InitialSimple.Using(xNamespaceName)
                    .NormalizeWhitespace())
                .ToArray();

            var output = compilationUnit.AddUsings(usingDirectives);
            return output;
        }

        public static CompilationUnitSyntax AddUsings_Idempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var missingUsingDirectives = compilationUnit.GetMissingUsingNamespaceNames(namespaceNames);

            var output = compilationUnit.AddUsings_NonIdempotent(missingUsingDirectives);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="AddUsings_Idempotent(CompilationUnitSyntax, IEnumerable{string})"/> as the default.
        /// </summary>
        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var output = compilationUnit.AddUsings_Idempotent(namespaceNames);
            return output;
        }

        public static CompilationUnitSyntax AddUsings_NonIdempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var usingDirectives = nameAliases
                .Select(xNameAlias => Instances.SyntaxGenerator_InitialSimple.Using(
                    xNameAlias.DestinationName,
                    xNameAlias.SourceName))
                .ToArray();

            var output = compilationUnit.AddUsings(usingDirectives);
            return output;
        }

        public static CompilationUnitSyntax AddUsings_Idempotent(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var missingNameAliases = compilationUnit.GetMissingUsingNameAliases(nameAliases);

            var output = compilationUnit.AddUsings_NonIdempotent(missingNameAliases);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="AddUsings_Idempotent(CompilationUnitSyntax, IEnumerable{NameAlias})"/> as the default.
        /// </summary>
        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var output = compilationUnit.AddUsings_Idempotent(nameAliases);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            params NameAlias[] nameAliases)
        {
            var output = compilationUnit.AddUsings(nameAliases.AsEnumerable());
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            IEnumerable<(string DestinationName, string SourceNameExpression)> nameAliasValues)
        {
            var nameAliases = nameAliasValues
                .Select(xTuple => NameAlias.From(
                    xTuple.DestinationName,
                    xTuple.SourceNameExpression))
                ;

            var output = compilationUnit.AddUsings(nameAliases);
            return output;
        }

        public static CompilationUnitSyntax AddUsings(this CompilationUnitSyntax compilationUnit,
            params (string DestinationName, string SourceNameExpression)[] nameAliasValues)
        {
            var output = compilationUnit.AddUsings(nameAliasValues.AsEnumerable());
            return output;
        }

        public static IEnumerable<NameAlias> GetMissingUsingNameAliases(this CompilationUnitSyntax compilationUnit,
            IEnumerable<NameAlias> nameAliases)
        {
            var output = compilationUnit.GetUsingNameAliasDirectiveSyntaxes().GetMissingNameAliases(nameAliases);
            return output;
        }
    }
}
