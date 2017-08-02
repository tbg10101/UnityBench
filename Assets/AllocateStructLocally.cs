public sealed class AllocateStructLocally : BaseTest {
	public override void Test () {
		TestStruct testStruct;

		for (int i = 0; i < TestAllocationCount; i++) {
			testStruct = new TestStruct(i);
		}
	}
}
