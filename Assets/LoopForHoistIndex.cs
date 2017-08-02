public sealed class LoopForHoistIndex : BaseTest {
	public override void Test () {
		int i;
		
		for (i = 0; i < TestList.Count; i++) {
			int x = TestList[i].Value;
		}
	}
}
