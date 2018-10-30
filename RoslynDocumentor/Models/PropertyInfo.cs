namespace RoslynDocumentor.Models {

	public class PropertyInfo : IMemberInfo {

		public PropertyInfo() {
			Location = new Location();
			TypeLocation = new Location();
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public Location Location { get; set; }
		public bool IsStatic { get; set; }

		public bool CanWrite { get; set; }
		public string TypeName { get; set; }
		public string FullTypeName { get; set; }
		public Location TypeLocation { get; set; }

	}

}
