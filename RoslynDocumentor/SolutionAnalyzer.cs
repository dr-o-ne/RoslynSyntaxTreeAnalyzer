using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
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

					m_semanticAnalyzer.Analyze( model, data );

				}
			}

			return new List<ClassInfo>();
		}

	}

}
