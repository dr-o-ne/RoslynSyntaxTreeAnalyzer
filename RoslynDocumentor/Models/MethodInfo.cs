using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynDocumentor.Models {

	public class MethodInfo : IMemberInfo {

		public MethodInfo() {
			Parameters = new List<Parameter>();
			TypeLocation = new Location();
			Location = new Location();
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsStatic { get; set; }
		public Location Location { get; set; }

		// return type
		public Location TypeLocation { get; set; }
		public string TypeName { get; set; }
		public string FullTypeName { get; set; }							//<---

		public ICollection<Parameter> Parameters { get; set; }

		public class Parameter {

			public Parameter() {
				TypeLocationInfo = new Location();
			}

			public string Name { get; set; }
			public string TypeName { get; set; }

			public bool IsGeneric { get; set; }
			public Location TypeLocationInfo { get; set; }
			public string DefaultValue { get; set; }

			public TypeSyntax TypeSyntax { get; set; }						//<--- TODO: mb not needed
		}

	}

}
