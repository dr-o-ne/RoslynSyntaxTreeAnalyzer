using System.Collections.Generic;

namespace RoslynDocumentor.Models {

	public sealed class Data {

		public Data( List<ClassInfo> classInfos ) {
			ClassInfos = classInfos;
		}

		public List<ClassInfo> ClassInfos;

	}

}
