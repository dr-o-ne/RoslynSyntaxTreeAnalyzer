namespace RoslynDocumentorTests {

	internal static class CodeSamples {

		public static string EmptyStaticClass = @"namespace RoslynDocumentorTests {
	public static class StaticClass {
	}
}";

		public static string Sample1 = @"using Xunit;

namespace CodilityLessons {

	/// <summary>
	/// My Solution
	/// </summary>
	public sealed class Nesting {

		/// <summary>
		/// Special case of Brackets problem, but for this case no need to create a stack. Just need to check ""stack"" size 
		/// </summary>
		public int solution1( string S ) {
			return -1;
		}

		private int solution2( string S ) => return 1;

		internal bool solution3( int S ) 
		{ 
			return true;
		}

		[Theory]
		[InlineData( 0, ""())"" )]
		[InlineData( 1, ""(()(())())"" )]
		public void Test( int expected, string input ) => Assert.Equal( expected, solution( input ) );

	}

}";

		public static string Classes = @"	
using System;

namespace RoslynDocumentorTests {


	public sealed class EmptySealedClass {
	}

	[Serializable]
	public class EmptyClassBase {
	}

	[Obsolete]
	[Serializable]
	public class EmptyClassChild : EmptyClassBase {
	}

	public sealed class EmptySealedClassChild : EmptyClassBase {
	}

	public static class StaticClass {
	}

}";


	}
}