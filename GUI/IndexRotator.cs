namespace prototype.GUI
{
    public static class IndexRotator
    {
        public static int NextIndex(int currentIndex, int increment, int count)
        {
            var nextRelativeIndex = (currentIndex + increment) % count;
            return nextRelativeIndex >= 0
                ? nextRelativeIndex
                : nextRelativeIndex + count;
        }
    }
}
