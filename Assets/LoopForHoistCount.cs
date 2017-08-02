public sealed class LoopForHoistCount : BaseTest {
	public override void Test () {
		int count = TestList.Count;
		
		for (int i = 0; i < count; i++) {
			int x = TestList[i].Value;
		}
	}
}
