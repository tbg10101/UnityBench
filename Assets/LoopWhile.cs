public sealed class LoopWhile : BaseTest {
	public override void Test () {
		int i = 0;

		while (i < TestList.Count) {
			int x = TestList[i].Value;
			i++;
		}
	}
}
