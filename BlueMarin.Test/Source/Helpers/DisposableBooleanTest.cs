using System;
using NUnit.Framework;

namespace BlueMarin.Test
{
	[TestFixture ()]
	public class DisposableBooleanTest
	{
		bool TestGetSet { get; set; }
		bool TestSet { 
			set{
				Console.WriteLine (value);
			}
		}
		bool TestGet { get; }

		[Test]
		public void TestGetterSetter ()
		{
			using (DisposableBoolean db = new DisposableBoolean (b => TestGetSet = b)) {
				Assert.IsTrue (TestGetSet);
			}
			Assert.IsFalse (TestGetSet);

			//Assert.IsNull (db);
		}


		[Test]
		public void TestSetter ()
		{
			using (DisposableBoolean db = new DisposableBoolean (b => TestSet = b)) {
			}

			//Assert.IsNull (db);
		}
	}
}

