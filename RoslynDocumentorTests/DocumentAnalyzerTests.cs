using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynDocumentor;
using Xunit;

namespace RoslynDocumentorTests {

	public sealed class DocumentAnalyzerTests {

		private readonly DocumentAnalyzer m_sut = new DocumentAnalyzer();

		[Fact]
		public void EmptyStaticClass_Test() {

			SyntaxTree tree = CSharpSyntaxTree.ParseText( CodeSamples.EmptyStaticClass );
			var result = m_sut.Analyze( tree ).ToList();

			Assert.Single( result );
			var classInfo = result[0];
			Assert.True( classInfo.IsStatic );
			Assert.Equal( "StaticClass", classInfo.Name );
			Assert.Equal( null, classInfo.Description );
		}


		[Fact]
		public void Sample1_Test() {

			SyntaxTree tree = CSharpSyntaxTree.ParseText( CodeSamples.Sample1 );
			var result = m_sut.Analyze( tree ).ToList();

			Assert.Single( result );
			var classInfo = result[0];
			Assert.False( classInfo.IsStatic );
			Assert.Equal( "Nesting", classInfo.Name );
			Assert.Equal( " <summary>\r\n\t/// My Solution\r\n\t/// </summary>\r\n", classInfo.Description );

		}

	}

}
