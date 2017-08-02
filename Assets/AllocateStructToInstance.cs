public sealed class AllocateStructToInstance : BaseTest {
	private TestStruct _testStruct;
	
	public override void Test () {
		for (int i = 0; i < TestAllocationCount; i++) {
			_testStruct = new TestStruct(i);
		}
	}
}
