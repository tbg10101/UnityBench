public sealed class AllocateObjectLocally : BaseTest {
	public override void Test () {
		TestObject testObject;

		for (int i = 0; i < TestAllocationCount; i++) {
			testObject = new TestObject(i);
		}
	}
}
