using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynDocumentor;
using RoslynDocumentor.Models;
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
			Assert.Null( classInfo.Description );
			Assert.Equal( 2, classInfo.Location.LineNumber );
			Assert.Empty( classInfo.Methods );
			Assert.Empty( classInfo.Properties );

		}


		[Fact]
		public void Sample1_Test() {

			SyntaxTree tree = CSharpSyntaxTree.ParseText( CodeSamples.Sample1 );
			var result = m_sut.Analyze( tree ).ToList();

			// class
			Assert.Single( result );
			var classInfo = result[0];
			Assert.False( classInfo.IsStatic );
			Assert.Equal( "Nesting", classInfo.Name );
			Assert.Equal( " <summary>\r\n\t/// My Solution\r\n\t/// </summary>\r\n", classInfo.Description );
			Assert.Equal( 8, classInfo.Location.LineNumber );

			// methods
			Assert.Equal( 4, classInfo.Methods.Count );
			var method1 = classInfo.Methods.First();
			var method2 = classInfo.Methods.Skip( 1 ).First();
			var method3 = classInfo.Methods.Skip( 2 ).First();
			var method4 = classInfo.Methods.Skip( 3 ).First();

			Assert.Equal( "solution1", method1.Name );
			Assert.False( method1.IsStatic );
			Assert.NotNull( method1.Description );
			Assert.Equal( 13, method1.Location.LineNumber );


			Assert.Equal( "solution2", method2.Name );
			Assert.False( method2.IsStatic );
			Assert.Null( method2.Description );
			Assert.Equal( 17, method2.Location.LineNumber );


			Assert.Equal( "solution3", method3.Name );
			Assert.True( method3.IsStatic );
			Assert.Null( method3.Description );
			Assert.Equal( 19, method3.Location.LineNumber );


			Assert.Equal( "Test", method4.Name );
			Assert.False( method4.IsStatic );
			Assert.Null( method4.Description );
			Assert.Equal( 27, method4.Location.LineNumber );


		}

	}

}
