public sealed class LoopFor : BaseTest {
	public override void Test () {
		for (int i = 0; i < TestList.Count; i++) {
			int x = TestList[i].Value;
		}
	}
}
