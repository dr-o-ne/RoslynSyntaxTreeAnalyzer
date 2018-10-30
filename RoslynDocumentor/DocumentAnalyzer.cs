using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynDocumentor.Models;

namespace RoslynDocumentor {

	public sealed class DocumentAnalyzer {

		public IEnumerable<ClassInfo> Analyze( SyntaxTree tree ) {

			var result = new List<ClassInfo>();

			var classNodes = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where( c => IsPublic( c.Modifiers ) ).ToList();
			foreach( var classNode in classNodes ) {
				result.Add( Analyze( classNode ) );
			}

			return result;

		}

		public ClassInfo Analyze( ClassDeclarationSyntax classNode ) {

			var result = new ClassInfo();

			result.Name = classNode.Identifier.Text;
			result.IsStatic = IsStatic( classNode.Modifiers );
			result.Description = GetSummary( classNode );
			result.Location.LineNumber = GetLocation( classNode );

			return result;
		}

		private static string GetSummary( CSharpSyntaxNode classNode ) {

			DocumentationCommentTriviaSyntax xmlTrivia = classNode.GetLeadingTrivia()
				.Select( i => i.GetStructure() )
				.OfType<DocumentationCommentTriviaSyntax>()
				.FirstOrDefault();

			bool? isSummary = xmlTrivia?.ChildNodes()
				.OfType<XmlElementSyntax>()
				.Any( i => i.StartTag.Name.ToString().Equals( "summary" ) );

			return xmlTrivia?.ToString();

		}

		private static int GetLocation( BaseTypeDeclarationSyntax classNode ) => classNode.Identifier.GetLocation().GetMappedLineSpan().StartLinePosition.Line + 1;

		private static bool IsStatic( SyntaxTokenList modifiers ) => HasModifier( modifiers, "static" );

		private static bool IsPublic( SyntaxTokenList modifiers ) => HasModifier( modifiers, "public" );

		private static bool HasModifier( SyntaxTokenList modifiers, string modifier ) => modifiers.Any( m => m.Value.Equals( modifier ) );

	}

}
