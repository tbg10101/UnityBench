public sealed class LoopForeach : BaseTest {
	public override void Test () {
		foreach (TestStruct s in TestList) {
			int x = s.Value;
		}
	}
}
