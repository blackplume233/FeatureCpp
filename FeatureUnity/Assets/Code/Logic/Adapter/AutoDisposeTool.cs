namespace Code.Logic.Adapter
{
	public class AutoDisposePool
	{
		void Test()
		{
			using ObjectHandle<object> handle = new ObjectHandle<object>();
			
		}
	}


	public struct ObjectHandle<T> :System.IDisposable
	{
		public void Dispose()
		{
			
		}
	}
}