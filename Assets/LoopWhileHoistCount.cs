public sealed class LoopWhileHoistCount : BaseTest {
	public override void Test () {
		int count = TestList.Count;
		int i = 0;

		while (i < count) {
			int x = TestList[i].Value;
			i++;
		}
	}
}
