using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynDocumentor;
using Xunit;

namespace RoslynDocumentorTests {

	public sealed class DocumentAnalyzerTests {

		private readonly DocumentSyntaxAnalyzer m_sut = new DocumentSyntaxAnalyzer();

		[Fact]
		public void EmptyStaticClass_Test() {

			SyntaxTree tree = CSharpSyntaxTree.ParseText( CodeSamples.EmptyStaticClass );
			var result = m_sut.Analyze( tree, "TestPath" );

			Assert.Single( result );
			var classInfo = result[0];
			Assert.True( classInfo.IsStatic );
			Assert.Equal( "StaticClass", classInfo.Name );
			Assert.Null( classInfo.Description );
			Assert.Equal( 2, classInfo.Location.LineNumber );
			Assert.Empty( classInfo.Methods );
			Assert.Empty( classInfo.Properties );
			Assert.Equal( "TestPath", classInfo.Location.SourceFile );

		}

		[Fact]
		public void Sample1_Test() {

			SyntaxTree tree = CSharpSyntaxTree.ParseText( CodeSamples.Sample1 );
			var result = m_sut.Analyze( tree, "TestPath" );

			// class
			Assert.Single( result );
			var classInfo = result[0];
			Assert.False( classInfo.IsStatic );
			Assert.Equal( "Nesting", classInfo.Name );
			Assert.Equal( " <summary>\r\n\t/// My Solution\r\n\t/// </summary>\r\n", classInfo.Description );
			Assert.Equal( 8, classInfo.Location.LineNumber );
			Assert.Equal( "TestPath", classInfo.Location.SourceFile );

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
			Assert.Equal( 34, method4.Location.LineNumber );

			// properties

			Assert.Equal( 2, classInfo.Properties.Count );
			var property1 = classInfo.Properties.First();
			var property2 = classInfo.Properties.Skip( 1 ).First();

			Assert.Equal( "AutoProperty", property1.Name );
			Assert.Null( property1.Description );
			Assert.True( property1.IsStatic );
			Assert.Equal( 24, property1.Location.LineNumber );

			Assert.Equal( "ReadOnlyProperty", property2.Name );
			Assert.NotNull( property2.Description );
			Assert.False( property2.IsStatic );
			Assert.Equal( 29, property2.Location.LineNumber );


		}

	}

}
