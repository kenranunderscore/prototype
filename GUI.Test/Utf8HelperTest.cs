namespace prototype.GUI.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class Utf8HelperTest
    {
        [Test]
        public unsafe void Utf8_string_is_read_correctly_from_byte_pointer()
        {
            var bytes = new byte[4] { (byte)'F', (byte)'o', 0, 0 };
            fixed (byte* ptr = bytes)
            {
                var utf8 = Utf8Helper.GetUtf8String(ptr);
                Assert.That(utf8, Is.EqualTo("Fo"));
            }
        }
    }
}
