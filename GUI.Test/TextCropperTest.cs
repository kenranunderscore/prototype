namespace prototype.GUI.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class TextCropperTest
    {
        [Test]
        public void Short_text_is_not_cropped()
        {
            var cropper = new TextCropper();
            var text = "Foo Bar";
            var croppedText = cropper.Crop(text, 20, 2);
            Assert.That(croppedText, Is.EqualTo(text));
        }

        [Test]
        public void Long_text_is_cropped()
        {
            var cropper = new TextCropper();
            var text = "This is a long text.";
            var croppedText = cropper.Crop(text, 10, 1);
            Assert.That(croppedText, Is.EqualTo("This is a "));
        }

        [Test]
        public void Text_that_fits_exactly_is_not_cropped()
        {
            var cropper = new TextCropper();
            var text = "Blub";
            var croppedText = cropper.Crop(text, 4, 1);
            Assert.That(croppedText, Is.EqualTo(text));
        }
    }
}
