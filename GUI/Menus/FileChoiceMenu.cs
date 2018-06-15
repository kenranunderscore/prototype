namespace prototype.GUI.Menus
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using static SDL2.SDL;

    internal class FileChoiceMenu : Menu
    {
        private const int NumberOfVisibleMenuItems = 5;
        private readonly List<MenuItem> allItems_ = new List<MenuItem>();
        private int activeIndex_;

        public FileChoiceMenu(TextRenderer textRenderer, Options options)
            : base(textRenderer, options)
        {
            var files = Directory.EnumerateFiles("TextSources", "*.txt", SearchOption.TopDirectoryOnly);
            allItems_.AddRange(files.Select(f => new MenuItem(Path.GetFileNameWithoutExtension(f))));
            activeIndex_ = allItems_.Count / 2;
            allItems_[activeIndex_].IsActive = true;
            AdjustVisibleMenuItems(0);
        }

        public override TargetSceneType HandleEvent(SDL_Event e)
        {
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                var target = MenuItems.SingleOrDefault(m => m.Area.Contains(e.button.x, e.button.y));
                if (target != null)
                {
                    //TODO Load file
                    return target.TargetSceneType;
                }
            }

            if (e.type == SDL_EventType.SDL_KEYDOWN)
            {
                return HandleKeyDown(e.key);
            }

            return TargetSceneType.Unchanged;
        }

        public override void Render()
        {
            base.Render();

            if (activeIndex_ - NumberOfVisibleMenuItems / 2 > 0)
            {
                RenderUpArrow();
            }

            if (activeIndex_ + NumberOfVisibleMenuItems / 2 < allItems_.Count - 1)
            {
                RenderDownArrow();
            }
        }

        private void RenderUpArrow()
        {
            var x = Options.ScreenWidth / 2 - Options.ScaledLetterWidth / 2;
            var y = MenuItems.First().Area.y - 4 * Options.ScaledLetterHeight;
            TextRenderer.Render("↑", new SDL_Rect { x = x, y = y }, ColorScheme.Default);
        }

        private void RenderDownArrow()
        {
            var x = Options.ScreenWidth / 2 - Options.ScaledLetterWidth / 2;
            var y = MenuItems.Last().Area.y + 4 * Options.ScaledLetterHeight;
            TextRenderer.Render("↓", new SDL_Rect { x = x, y = y }, ColorScheme.Default);
        }

        protected override void ActivateNextMenuItem(int increment)
        {
            AdjustVisibleMenuItems(increment);
            var nextRelativeIndex = MenuItems.IndexOf(ActiveMenuItem) + increment;
            if (nextRelativeIndex < MenuItems.Count && nextRelativeIndex >= 0)
            {
                ActiveMenuItem.IsActive = false;
                MenuItems[nextRelativeIndex].IsActive = true;
                activeIndex_ += increment;
            }
        }

        private void AdjustVisibleMenuItems(int increment)
        {
            MenuItems.Clear();
            var nextAbsoluteIndex = activeIndex_ + increment - NumberOfVisibleMenuItems / 2;
            var maxIndex = allItems_.Count - NumberOfVisibleMenuItems;
            var startIndex = Math.Max(Math.Min(nextAbsoluteIndex, maxIndex), 0);
            MenuItems.AddRange(allItems_.Skip(startIndex).Take(NumberOfVisibleMenuItems));
            CalculateAreas();
        }
    }
}
