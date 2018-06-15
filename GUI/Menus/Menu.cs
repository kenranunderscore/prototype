namespace prototype.GUI.Menus
{
    using System.Collections.Generic;
    using System.Linq;
    using static SDL2.SDL;

    internal abstract class Menu : IScene
    {
        protected TextRenderer TextRenderer { get; }
        protected Options Options { get; }
        protected List<MenuItem> MenuItems { get; } = new List<MenuItem>();
        protected MenuItem ActiveMenuItem => MenuItems.Single(x => x.IsActive);

        protected Menu(TextRenderer textRenderer, Options options)
        {
            TextRenderer = textRenderer;
            Options = options;
        }

        protected void CalculateAreas()
        {
            var maxMenuItemLength = MenuItems.Max(x => x.Caption.Length);
            var left = Options.ScreenWidth / 2 - maxMenuItemLength * Options.ScaledLetterWidth / 2;
            var menuHeight = (MenuItems.Count * 2 - 1) * Options.ScaledLetterHeight;
            var top = Options.ScreenHeight / 2 - menuHeight / 2;
            for (var i = 0; i < MenuItems.Count; i++)
            {
                var caption = MenuItems[i].Caption;
                var length = caption.Length;
                var offset = (int)((maxMenuItemLength - length) / 2d * Options.ScaledLetterWidth);
                var area = new SDL_Rect
                {
                    x = left + offset,
                    y = top + 2 * i * Options.ScaledLetterHeight,
                    w = length * Options.ScaledLetterWidth,
                    h = Options.ScaledLetterHeight
                };

                MenuItems[i].Area = area;
            }
        }

        public virtual TargetSceneType HandleEvent(SDL_Event e)
        {
            if (e.type == SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                var target = MenuItems.SingleOrDefault(m => m.Area.Contains(e.button.x, e.button.y));
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

        protected TargetSceneType HandleKeyDown(SDL_KeyboardEvent e)
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

        protected virtual void ActivateNextMenuItem(int increment)
        {
            var activeIndex = MenuItems.IndexOf(ActiveMenuItem);
            ActiveMenuItem.IsActive = false;
            var nextIndex = IndexRotator.NextIndex(activeIndex, increment, MenuItems.Count);
            MenuItems[nextIndex].IsActive = true;
        }

        public virtual void Render()
        {
            SDL_GetMouseState(out var x, out var y);
            foreach (var menuItem in MenuItems)
            {
                var color = MenuItemColor(menuItem, x, y);
                TextRenderer.Render(menuItem.Caption, menuItem.Area, color);
            }
        }

        protected virtual SDL_Color MenuItemColor(MenuItem menuItem, int x, int y)
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
    }
}
