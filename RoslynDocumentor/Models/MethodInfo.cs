﻿using System.Collections.Generic;

namespace RoslynDocumentor.Models {

	public class MethodInfo : IMemberInfo {

		public MethodInfo() {
			Parameters = new List<Parameter>();
			Location = new Location();
		}

		public string Name { get; set; }
		public bool IsStatic { get; set; }
		public string Description { get; set; }
		public Location Location { get; set; }



		public string TypeName { get; set; }
		public Location TypeLocationInfo { get; set; }
		public ICollection<Parameter> Parameters { get; set; }

		public class Parameter {

			public Parameter() {
				TypeLocationInfo = new Location();
			}
			public bool IsGeneric { get; set; }
			public string Name { get; set; }
			public string TypeName { get; set; }
			public Location TypeLocationInfo { get; set; }
			public string DefaultValue { get; set; }
		}

	}

}
