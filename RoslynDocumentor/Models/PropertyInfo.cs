﻿namespace RoslynDocumentor.Models {

	public class PropertyInfo : IMemberInfo {

		public bool CanWrite { get; set; }
		public string Name { get; set; }
		public string TypeName { get; set; }
		public string FullTypeName { get; set; }
		public Location TypeLocationInfo { get; set; }
		public bool IsStatic { get; set; }
		public string Description { get; set; }
		public Location Location { get; set; }

	}

}
