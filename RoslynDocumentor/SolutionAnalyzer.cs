using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynDocumentor.Models;

namespace RoslynDocumentor {

	public sealed class SolutionAnalyzer {

		private readonly DocumentSyntaxAnalyzer m_syntaxAnalyzer = new DocumentSyntaxAnalyzer();
		private readonly DocumentSemanticAnalyzer m_semanticAnalyzer = new DocumentSemanticAnalyzer();


		public async Task<IEnumerable<ClassInfo>> Analyze( Solution solution ) {

			foreach( var project in solution.Projects ) {
			foreach( var doc in project.Documents ) {

					SyntaxTree tree = await doc.GetSyntaxTreeAsync();

					var data = m_syntaxAnalyzer.Analyze( tree );

					SemanticModel model = await doc.GetSemanticModelAsync();


					foreach( ClassInfo classInfo in data.ClassInfos ) {



						var classes = doc.GetSyntaxRootAsync().Result.DescendantNodes().OfType<ClassDeclarationSyntax>();
						foreach( var cl in classes ) {

							var ttt = model.GetDeclaredSymbol( cl );



							//var ttt = model.GetTypeInfo( cl ).Type.ContainingNamespace;
						}


						var t1= model.GetTypeInfo( classInfo.ClassSyntaxNode );
						var t = model.Compilation.GetDeclarationDiagnostics();

						//Compilation.GetTypeByMetadataName()

						foreach( var methodInfo in classInfo.Methods ) {
							foreach( var parameterInfo in methodInfo.Parameters ) {

								var pInfo = model.GetSymbolInfo( parameterInfo.TypeSyntax );

								var namespaceName = pInfo.Symbol.ContainingNamespace.Name;
								var typeName = pInfo.Symbol.Name;

								var fullName = namespaceName + "." + typeName;


							}
						}


					}	

					m_semanticAnalyzer.Analyze( model, data );

				}
			}

			return new List<ClassInfo>();
		}

	}

}
