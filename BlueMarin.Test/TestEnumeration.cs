using NUnit.Framework;
using System;
using BlueMarin;
using System.Diagnostics;

namespace BlueMarin.Test
{
	[TestFixture ()]
	public class TestEnumeration
	{
		
		
		[Test]
		public void TestNotEqual ()
		{
			Assert.AreNotEqual (TestEnum.ValueOne, TestEnum.ValueTwo);
			Assert.AreNotEqual (TestEnum.ValueOne.Value, TestEnum.ValueTwo.Value);
			Assert.AreNotSame (TestEnum.ValueOne.Value, TestEnum.ValueTwo.Value);

			Assert.AreEqual (TestEnum.ValueOne.Value, 0);
			Assert.AreEqual (TestEnum.ValueTwo.Value, 1);
		}

		[Test]
		public void TestFromName ()
		{
			Assert.NotNull (Enumeration.FromDisplayName<TestEnum> ("ValueTwo"));
		}

		[Test]
		public void TestToString ()
		{
			Assert.AreEqual ("ValueTwo", TestEnum.ValueTwo.Name);
		}

		[Test]
		public void TestEqualsWithDynamic ()
		{
			var dyn = Enumeration.FromDisplayName<TestEnum> ("ValueTwo");
			Assert.IsTrue (TestEnum.ValueTwo.Equals (dyn));
			Assert.AreEqual (TestEnum.ValueTwo, dyn);
		}

		[Test]
		public void Test ()
		{
			Debug.WriteLine ("SubTestEnum.ValueThree = " + SubTestEnum.ValueThree.Value);
			Assert.AreNotEqual (TestEnum.ValueTwo, SubTestEnum.ValueThree);
		}
	}


	class TestEnum : Enumeration
	{
		public static TestEnum ValueOne = new TestEnum();
		public static TestEnum ValueTwo = new TestEnum();
	}

	class SubTestEnum : TestEnum
	{
		public static SubTestEnum ValueThree = new SubTestEnum();
	}
}

