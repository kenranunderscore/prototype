namespace prototype.GUI
{
    using System.Collections.Generic;
    using System.Linq;
    using static SDL2.SDL;

    internal class MainMenu : IScene
    {
        private readonly TextRenderer textRenderer_;
        private readonly Options options_;

        private readonly IList<MenuItem> menuItems_ = new List<MenuItem>
        {
            new MenuItem("Play") { TargetSceneType = TargetSceneType.Game, IsActive = true },
            new MenuItem("Options") { TargetSceneType = TargetSceneType.Options },
            new MenuItem("Quit") { TargetSceneType = TargetSceneType.Quit }
        };

        private MenuItem ActiveMenuItem => menuItems_.Single(x => x.IsActive);

        public MainMenu(TextRenderer textRenderer, Options options)
        {
            textRenderer_ = textRenderer;
            options_ = options;
            CalculateAreas();
        }

        private void CalculateAreas()
        {
            var maxMenuItemLength = menuItems_.Max(x => x.Caption.Length);
            var left = options_.ScreenWidth / 2 - maxMenuItemLength * options_.ScaledLetterWidth / 2;
            var top = options_.ScreenHeight / 2;
            for (var i = 0; i < menuItems_.Count; i++)
            {
                var caption = menuItems_[i].Caption;
                var length = caption.Length;
                var offset = (int)((maxMenuItemLength - length) / 2d * options_.ScaledLetterWidth);
                var area = new SDL_Rect
                {
                    x = left + offset,
                    y = top + 2 * i * options_.ScaledLetterHeight,
                    w = length * options_.ScaledLetterWidth,
                    h = options_.ScaledLetterHeight
                };

                menuItems_[i].Area = area;
            }
        }

        public void Render()
        {
            SDL_GetMouseState(out var x, out var y);
            foreach (var menuItem in menuItems_)
            {
                var color = MenuItemColor(menuItem, x, y);
                textRenderer_.Render(menuItem.Caption, menuItem.Area, color);
            }
        }

        private static SDL_Color MenuItemColor(MenuItem menuItem, int x, int y)
        {
            if (menuItem.Area.Contains(x, y))
            {
                return ColorScheme.ActiveItem;
            }

            if (menuItem.IsActive)
            {
                return ColorScheme.MouseOverItem;
            }

            return ColorScheme.Default;
        }

        public TargetSceneType HandleEvent(SDL_Event e)
        {
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                var target = menuItems_.SingleOrDefault(m => m.Area.Contains(e.button.x, e.button.y));
                if (target != null)
                {
                    return target.TargetSceneType;
                }
            }

            if (e.type == SDL_EventType.SDL_KEYDOWN)
            {
                return HandleKeyDown(e.key);
            }

            return TargetSceneType.Unchanged;
        }

        private TargetSceneType HandleKeyDown(SDL_KeyboardEvent e)
        {
            var increment = 0;
            switch (e.keysym.sym)
            {
                case SDL_Keycode.SDLK_KP_ENTER:
                case SDL_Keycode.SDLK_RETURN:
                    return ActiveMenuItem.TargetSceneType;
                case SDL_Keycode.SDLK_UP:
                    increment = -1;
                    break;
                case SDL_Keycode.SDLK_DOWN:
                    increment = 1;
                    break;
            }

            if (increment != 0)
            {
                ActivateNextMenuItem(increment);
            }

            return TargetSceneType.Unchanged;
        }

        private void ActivateNextMenuItem(int increment)
        {
            var activeItem = ActiveMenuItem;
            var activeIndex = menuItems_.IndexOf(activeItem);
            activeItem.IsActive = false;
            var nextIndex = IndexRotator.NextIndex(activeIndex, increment, menuItems_.Count);
            menuItems_[nextIndex].IsActive = true;
        }
    }
}
