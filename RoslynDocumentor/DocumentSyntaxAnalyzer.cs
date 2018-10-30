using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynDocumentor.Models;
using static RoslynDocumentor.Models.MethodInfo;

namespace RoslynDocumentor {

	public sealed class DocumentSyntaxAnalyzer {

		public Data Analyze( SyntaxTree tree ) {

			var result = new List<ClassInfo>();

			var classNodes = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where( c => IsPublic( c.Modifiers ) ).ToList();
			foreach( var classNode in classNodes ) {
				result.Add( Analyze( classNode ) );
			}

			return new Data( result );

		}

		public ClassInfo Analyze( ClassDeclarationSyntax classNode ) {


			var identifier = GetIdentifier( classNode );


			var result = new ClassInfo();

			result.Name = GetName( identifier );
			result.IsStatic = IsStatic( classNode.Modifiers );
			result.Description = GetSummary( classNode );
			result.Location.LineNumber = GetLineNumber( identifier );

			var methods = classNode.DescendantNodes().OfType<MethodDeclarationSyntax>().Where( m => IsPublic( m.Modifiers ) ).ToList();
			foreach( var method in methods ) {
				result.Methods.Add( AnalyzeMethod( method ) );
			}

			return result;
		}

		private static MethodInfo AnalyzeMethod( MethodDeclarationSyntax methodNode ) {

			var identifier = GetIdentifier( methodNode );

			var result = new MethodInfo();

			result.Name = GetName( identifier );
			result.IsStatic = IsStatic( methodNode.Modifiers );
			result.Description = GetSummary( methodNode );
			result.Location.LineNumber = GetLineNumber( identifier );




			var parameters = methodNode.ParameterList.Parameters.ToList();
			foreach( var parameter in parameters ) {
				result.Parameters.Add( AnalyzeParameter( parameter ) );
			}

			return result;

		}

		private static Parameter AnalyzeParameter( ParameterSyntax parameterNode ) {

			var identifier = GetIdentifier( parameterNode );
			var result = new Parameter();

			result.Name = GetName( identifier );
			result.TypeName = parameterNode.Type.ToString();



			var tt = parameterNode.Type.GetType();

			var ssss = tt + "   " +  result.Name;

			//var res = tt is GenericNameSyntax;

			return result;
		}

		private static string GetSummary( CSharpSyntaxNode classNode ) {

			DocumentationCommentTriviaSyntax xmlTrivia = classNode.GetLeadingTrivia()
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

		private static SyntaxToken GetIdentifier( MethodDeclarationSyntax node ) => node.Identifier;

		private static SyntaxToken GetIdentifier( ParameterSyntax node ) => node.Identifier;

		private static string GetName( SyntaxToken syntaxToken ) => syntaxToken.Text;

		private static int GetLineNumber( SyntaxToken syntaxToken ) => syntaxToken.GetLocation().GetMappedLineSpan().StartLinePosition.Line + 1;

		private static bool IsStatic( SyntaxTokenList modifiers ) => HasModifier( modifiers, "static" );

		private static bool IsPublic( SyntaxTokenList modifiers ) => HasModifier( modifiers, "public" );

		private static bool HasModifier( SyntaxTokenList modifiers, string modifier ) => modifiers.Any( m => m.Value.Equals( modifier ) );

	}

}
