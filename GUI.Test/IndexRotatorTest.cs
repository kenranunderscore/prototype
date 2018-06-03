namespace prototype.GUI.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class IndexRotatorTest
    {
        [Test]
        public void Zero_increment_keeps_index_unchanged()
        {
            var nextIndex = IndexRotator.NextIndex(3, 0, 5);
            Assert.That(nextIndex, Is.EqualTo(3));
        }

        [Test]
        public void Index_increases_when_sum_is_less_than_or_equal_to_count()
        {
            var nextIndex = IndexRotator.NextIndex(3, 2, 6);
            Assert.That(nextIndex, Is.EqualTo(5));
        }

        [Test]
        public void Index_is_reset_when_overstepping_count()
        {
            var nextIndex = IndexRotator.NextIndex(1, 1, 2);
            Assert.That(nextIndex, Is.EqualTo(0));
        }

        [Test]
        public void Negative_increment_leads_to_decreasing_index_when_not_below_zero()
        {
            var nextIndex = IndexRotator.NextIndex(1, -1, 2);
            Assert.That(nextIndex, Is.EqualTo(0));
        }

        [Test]
        public void Negative_increments_rotate()
        {
            var nextIndex = IndexRotator.NextIndex(1, -5, 3);
            Assert.That(nextIndex, Is.EqualTo(2));
        }
    }
}
