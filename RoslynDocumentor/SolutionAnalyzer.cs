using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using RoslynDocumentor.Models;
using static RoslynDocumentor.Models.MethodInfo;
using MethodInfo = RoslynDocumentor.Models.MethodInfo;

namespace RoslynDocumentor {

	public sealed class SolutionAnalyzer {

		private readonly DocumentSyntaxAnalyzer m_syntaxAnalyzer = new DocumentSyntaxAnalyzer();
		private readonly DocumentSemanticAnalyzer m_semanticAnalyzer = new DocumentSemanticAnalyzer();


		public async Task<IEnumerable<ClassInfo>> Analyze( Solution solution ) {

			var result = new List<ClassInfo>();

			foreach( Document doc in solution.Projects.SelectMany( p => p.Documents ) ) {

				// Syntax Info
				SyntaxTree tree = await doc.GetSyntaxTreeAsync();
				var classInfos = m_syntaxAnalyzer.Analyze( tree, doc.FilePath );

				// Semantic Info
				SemanticModel model = await doc.GetSemanticModelAsync();
				foreach( var classInfo in classInfos ) {
					ISymbol symbol = model.GetDeclaredSymbol( classInfo.ClassSyntaxNode );
					classInfo.FullName = ToFullName( symbol.ContainingNamespace.ToString(), symbol.Name );
					classInfo.IsStatic = symbol.IsStatic;

					foreach( var methodInfo in classInfo.Methods ) {
						AnalyzeMethod( model, methodInfo );
					}

				}

				result.AddRange( classInfos );

			}

			return result;
		}

		private static void AnalyzeMethod( SemanticModel model, MethodInfo methodInfo ) {

			var methodSymbol = (IMethodSymbol)model.GetDeclaredSymbol( methodInfo.Node );

			methodInfo.IsStatic = methodSymbol.IsStatic;
			methodInfo.TypeName = methodSymbol.ReturnType.Name;
			methodInfo.FullTypeName = ToFullName( methodSymbol.ContainingNamespace.ToString(), methodSymbol.Name );

			foreach( IParameterSymbol symbol in methodSymbol.Parameters ) {
				methodInfo.Parameters.Add( AnalyzeParameter( symbol ) );
			}

		}

		private static Parameter AnalyzeParameter( IParameterSymbol symbol ) {

			var parameterInfo = new Parameter();

			parameterInfo.Name = symbol.Name;
			parameterInfo.TypeName = symbol.Type.Name;
			parameterInfo.FullTypeName = symbol.Type.ContainingNamespace + "." + symbol.Type.Name;
			if( symbol.HasExplicitDefaultValue )
				parameterInfo.DefaultValue = symbol.ExplicitDefaultValue.ToString();

			return parameterInfo;

		}

		private static string ToFullName( string containingNamespace, string typeName ) => containingNamespace + "." + typeName;

	}

}
