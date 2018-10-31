using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynDocumentor.Models;

namespace RoslynDocumentor {

	public sealed class DocumentSyntaxAnalyzer {

		public List<ClassInfo> Analyze( SyntaxTree tree, string filePath ) {

			var result = new List<ClassInfo>();

			var classNodes = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where( c => IsPublic( c.Modifiers ) ).ToList();
			foreach( var classNode in classNodes ) {
				result.Add( Analyze( classNode, filePath ) );
			}

			return result;

		}

		private static ClassInfo Analyze( ClassDeclarationSyntax node, string filePath ) {

			var identifier = GetIdentifier( node );

			var result = new ClassInfo();

			result.Name = GetName( identifier );
			result.Description = GetSummary( node );
			result.Location.LineNumber = GetLineNumber( identifier );
			result.Location.SourceFile = filePath;

			var methods = node.DescendantNodes().OfType<MethodDeclarationSyntax>().Where( m => IsPublic( m.Modifiers ) ).ToList();
			foreach( var method in methods ) {
				result.Methods.Add( AnalyzeMethod( method, filePath ) );
			}

			var properties = node.DescendantNodes().OfType<PropertyDeclarationSyntax>().Where( m => IsPublic( m.Modifiers ) ).ToList();
			foreach( var property in properties ) {
				result.Properties.Add( AnalyzeProperty( property, filePath ) );
			}

			result.ClassSyntaxNode = node;

			return result;
		}

		private static PropertyInfo AnalyzeProperty( PropertyDeclarationSyntax node, string filePath ) {

			var identifier = GetIdentifier( node );

			var result = new PropertyInfo();

			result.Name = GetName( identifier );
			result.Description = GetSummary( node );
			result.Location.LineNumber = GetLineNumber( identifier );
			result.Location.SourceFile = filePath;

			return result;
		}

		private static MethodInfo AnalyzeMethod( MethodDeclarationSyntax node, string filePath ) {

			var identifier = GetIdentifier( node );

			var result = new MethodInfo();

			result.Name = GetName( identifier );
			result.Description = GetSummary( node );
			result.Location.LineNumber = GetLineNumber( identifier );
			result.Location.SourceFile = filePath;
			result.Node = node;

			return result;

		}

		private static string GetSummary( CSharpSyntaxNode node ) {

			DocumentationCommentTriviaSyntax xmlTrivia = node.GetLeadingTrivia()
				.Select( i => i.GetStructure() )
				.OfType<DocumentationCommentTriviaSyntax>()
				.FirstOrDefault();


			//TODO: fix
			bool? isSummary = xmlTrivia?.ChildNodes()
				.OfType<XmlElementSyntax>()
				.Any( i => i.StartTag.Name.ToString().Equals( "summary" ) );

			return isSummary == true ? xmlTrivia.ToString() : null;

		}

		private static SyntaxToken GetIdentifier( BaseTypeDeclarationSyntax node ) => node.Identifier;

		private static SyntaxToken GetIdentifier( PropertyDeclarationSyntax node ) => node.Identifier;

		private static SyntaxToken GetIdentifier( MethodDeclarationSyntax node ) => node.Identifier;

		private static string GetName( SyntaxToken syntaxToken ) => syntaxToken.Text;

		private static int GetLineNumber( SyntaxToken syntaxToken ) => syntaxToken.GetLocation().GetMappedLineSpan().StartLinePosition.Line + 1;

		private static bool IsPublic( SyntaxTokenList modifiers ) => modifiers.Any( m => m.Value.Equals( "public" ) );

	}

}