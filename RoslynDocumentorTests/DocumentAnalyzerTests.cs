using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynDocumentor;
using Xunit;

namespace RoslynDocumentorTests {

	public sealed class DocumentAnalyzerTests {

		private readonly DocumentAnalyzer m_sut = new DocumentAnalyzer();

		[Fact]
		public void Test() {
			SyntaxTree tree = CSharpSyntaxTree.ParseText( CodeSamples.Sample1 );
			m_sut.Analyze( tree );
		}

	}

}
