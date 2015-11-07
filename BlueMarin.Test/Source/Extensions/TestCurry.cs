using NUnit.Framework;
using BlueMarin;
using System;

namespace BlueMarin.Test
{
	[TestFixture ()]
	public class TestCurry
	{
		[Test]
		public void TestCurryAdd ()
		{
			Func<int, int, int> add = (a, b) => a + b;
			var add3 = add.Curry () (3);
			var res = add3 (4);
			Assert.AreEqual (7, res);
		}

		[Test]
		public void TestCurryMultiType ()
		{
			Func<bool, int, string> combineBoolAndInt = (boolz, intz) => string.Concat (boolz, intz);
			var combineInt = combineBoolAndInt.Curry () (false);
			var res = combineInt (666);
			Assert.AreEqual ("False666", res);
		}
	}
}

