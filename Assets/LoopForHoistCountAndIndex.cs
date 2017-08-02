public sealed class LoopForHoistCountAndIndex : BaseTest {
	public override void Test () {
		int count = TestList.Count;
		int i;
		
		for (i = 0; i < count; i++) {
			int x = TestList[i].Value;
		}
	}
}
