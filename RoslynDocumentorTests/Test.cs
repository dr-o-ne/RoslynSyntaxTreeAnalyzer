namespace CodilityLessons {

	/// <summary>
	/// My Solution
	/// </summary>
	public sealed class Nesting<T1> {

		/// <summary>
		/// Special case of Brackets problem, but for this case no need to create a stack. Just need to check ""stack"" size 
		/// </summary>
		public int solution1( string S ) {
			return -1;
		}

		public int[] solution2( string S ) => new[] {1};

		public static bool solution3<T2>( T1 t1, T2 t2 ) {
			return true;
		}


	}

}