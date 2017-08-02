public sealed class AllocateObjectToClass : BaseTest {
	private static TestObject _testObject;
	
	public override void Test () {
		for (int i = 0; i < TestAllocationCount; i++) {
			_testObject = new TestObject(i);
		}
	}

	public override void CleanUp () {
		_testObject = null;
	}
}
