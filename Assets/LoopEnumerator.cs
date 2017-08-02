using System.Collections.Generic;

public sealed class LoopEnumerator : BaseTest {
	public override void Test () {
		using (IEnumerator<TestStruct> enumerator = TestList.GetEnumerator()) {
			while (enumerator.MoveNext()) {
				int x = enumerator.Current.Value;
			}
		}
	}
}
