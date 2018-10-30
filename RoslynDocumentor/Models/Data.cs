using System.Collections.Generic;

namespace RoslynDocumentor.Models {

	public sealed class Data {

		public Dictionary<string, ClassInfo> FullName2ClassInfoMap = new Dictionary<string, ClassInfo>();

		public Data( List<ClassInfo> classInfos ) {
			ClassInfos = classInfos;
		}

		public List<ClassInfo> ClassInfos;

		public void BuildMap() {

		}

	}

}
