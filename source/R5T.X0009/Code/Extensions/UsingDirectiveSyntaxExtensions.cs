using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0129;


namespace System
{
    public static class UsingDirectiveSyntaxExtensions
    {
        public static IEnumerable<NameAlias> GetMissingNameAliases(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            IEnumerable<NameAlias> nameAliases)
        {
            var currentNameAliases = usingDirectives.GetUsingNameAliases();

            var output = nameAliases.Except(currentNameAliases);
            return output;
        }

        public static NameAlias GetNameAlias(this UsingDirectiveSyntax usingDirective)
        {
            var destinationName = usingDirective.GetDestinationName();
            var sourceNameExpression = usingDirective.GetSourceNameExpression();

            var output = new NameAlias(
                destinationName,
                sourceNameExpression);

            return output;
        }

        public static IEnumerable<NameAlias> GetUsingNameAliases(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNameAliasDirectiveSyntaxes()
                .Select(x => x.GetNameAlias())
                ;

            return output;
        }
    }
}
