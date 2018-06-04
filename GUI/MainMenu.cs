namespace prototype.GUI
{
    using System.Collections.Generic;
    using System.Linq;
    using static SDL2.SDL;

    internal class MainMenu
    {
        private IList<MenuItem> menuItems_;

        private readonly IReadOnlyCollection<string> menuItemCaptions_ = new List<string>
        {
            "Play",
            "Options",
            "Quit"
        };

        public MainMenu(TextRenderer textRenderer, Options options)
        {
            CreateMenuItems(textRenderer, options);
        }

        private void CreateMenuItems(TextRenderer textRenderer, Options options)
        {
            //TODO refactor
            var maxMenuItemWidth = menuItemCaptions_.Max(c => c.Length) * Defaults.LetterWidth;
            var x = options.ScreenWidth / 2 - maxMenuItemWidth / Defaults.LetterWidth;
            var centerY = options.ScreenHeight / 2;
            var menuItemHeight = Defaults.LetterHeight;
            var totalMenuHeight = (2 * menuItemCaptions_.Count - 1) * menuItemHeight;
            var y = centerY - totalMenuHeight / 2;
            menuItems_ = menuItemCaptions_.Select((caption, i) => new MenuItem(
                    textRenderer,
                    caption,
                    new SDL_Rect
                    {
                        x = x,
                        y = y + 2 * i * menuItemHeight,
                        w = caption.Length * Defaults.LetterWidth,
                        h = Defaults.LetterHeight
                    },
                    TargetScene.Unchanged))
                .ToList();
            menuItems_.First().IsActive = true;
        }

        public void Render()
        {
            foreach (var menuItem in menuItems_)
            {
                menuItem.Render();
            }
        }

        public TargetScene HandleEvent(SDL_Event e)
        {
            if (e.type == SDL_EventType.SDL_MOUSEMOTION)
            {
                HandleMouseMotion(e.motion);
            }

            if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                var targetItem = menuItems_.FirstOrDefault(x => x.Area.Contains(e.button.x, e.button.y));
                if (targetItem != null)
                {
                    return targetItem.TargetScene;
                }
            }

            if (e.type == SDL_EventType.SDL_KEYDOWN)
            {
                HandleKeyDown(e.key);
            }

            return TargetScene.Unchanged;
        }

        private void HandleKeyDown(SDL_KeyboardEvent e)
        {
            var increment = 0;
            switch (e.keysym.sym)
            {
                case SDL_Keycode.SDLK_UP:
                    increment = -1;
                    break;
                case SDL_Keycode.SDLK_DOWN:
                    increment = 1;
                    break;
            }

            if (increment != 0)
            {
                var activeItem = menuItems_.Single(x => x.IsActive);
                var activeIndex = menuItems_.IndexOf(activeItem);
                activeItem.IsActive = false;
                var nextIndex = IndexRotator.NextIndex(activeIndex, increment, menuItems_.Count);
                menuItems_[nextIndex].IsActive = true;
            }
        }

        private void HandleMouseMotion(SDL_MouseMotionEvent e)
        {
            foreach (var menuItem in menuItems_)
            {
                menuItem.HandleMouseMotion(e);
            }
        }
    }
}
