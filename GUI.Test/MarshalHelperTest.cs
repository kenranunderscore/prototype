namespace prototype.GUI.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class MarshalHelperTest
    {
        [Test]
        public unsafe void Utf8_string_is_read_correctly_from_byte_pointer()
        {
            var s = new TestStruct();
            s.foo[0] = (byte)'F';
            s.foo[1] = (byte)'o';
            var utf8 = MarshalHelper.GetUtf8String(s.foo);
            Assert.That(utf8, Is.EqualTo("Fo"));
        }

        private unsafe struct TestStruct
        {
            public fixed byte foo[3];
        }
    }
}
