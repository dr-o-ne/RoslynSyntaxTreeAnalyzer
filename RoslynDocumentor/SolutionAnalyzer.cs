using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using RoslynDocumentor.Models;

namespace RoslynDocumentor {

	public sealed class SolutionAnalyzer {

		private readonly DocumentAnalyzer m_documentAnalyzer = new DocumentAnalyzer();

		public async Task<IEnumerable<ClassInfo>> Analyze( Solution solution ) {

			foreach( var project in solution.Projects ) {
			foreach( var doc in project.Documents ) {
					SyntaxTree tree = await doc.GetSyntaxTreeAsync();

					m_documentAnalyzer.Analyze( tree );

					//var classes = GetClassInfo( tree, doc.FilePath );
				}
			}

			return new List<ClassInfo>();
		}

	}

}
