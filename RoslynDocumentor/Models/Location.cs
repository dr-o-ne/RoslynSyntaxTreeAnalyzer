namespace RoslynDocumentor.Models {

	public struct Location {

		/// <summary>
		///     Relative source file path within solution
		/// </summary>
		private string SourceFile { get; set; }

		/// <summary>
		///     1-based line number of location
		/// </summary>
		private int LineNumber { get; set; }

	}

}
