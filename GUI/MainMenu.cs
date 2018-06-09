namespace prototype.GUI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime;
    using static SDL2.SDL;

    internal class MainMenu : IScene
    {
        private IList<MenuItem> menuItems_;

        public MainMenu(TextRenderer textRenderer, Options options)
        {
            CreateMenuItems(textRenderer, options);
        }

        private void CreateMenuItems(TextRenderer textRenderer, Options options)
        {
            //TODO refactor
            var maxMenuItemLength = 7;
            var totalMenuHeight = 5 * options.ScaledLetterHeight;
            var x = options.ScreenWidth / 2 - maxMenuItemLength * options.ScaledLetterWidth / 2;
            var centerY = options.ScreenHeight / 2;
            var y = centerY - totalMenuHeight / 2;

            //TODO calculate areas (to react to scale changes) instead of setting only once
            menuItems_ = new List<MenuItem>
            {
                new MenuItem(
                    textRenderer,
                    "Play",
                    new SDL_Rect { x = x + (int)(1.5 * options.ScaledLetterWidth), y = y, w = 4 * options.ScaledLetterWidth, h = options.ScaledLetterHeight },
                    TargetSceneType.Game),
                new MenuItem(
                    textRenderer,
                    "Options",
                    new SDL_Rect { x = x, y = y + 2 * options.ScaledLetterHeight, w = 7 * options.ScaledLetterWidth, h = options.ScaledLetterHeight },
                    TargetSceneType.Options),
                new MenuItem(
                    textRenderer,
                    "Quit",
                    new SDL_Rect { x = x + (int)(1.5 * options.ScaledLetterWidth), y = y + 4 * options.ScaledLetterHeight, w = 4 * options.ScaledLetterWidth, h = options.ScaledLetterHeight },
                    TargetSceneType.Quit),
            };
            menuItems_.First().IsActive = true;
        }

        public void Render()
        {
            foreach (var menuItem in menuItems_)
            {
                menuItem.Render();
            }
        }

        public TargetSceneType HandleEvent(SDL_Event e)
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
                    return ActiveMenuItem.TargetScene;
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

        private MenuItem ActiveMenuItem => menuItems_.Single(x => x.IsActive);

        private void ActivateNextMenuItem(int increment)
        {
            var activeItem = ActiveMenuItem;
            var activeIndex = menuItems_.IndexOf(activeItem);
            activeItem.IsActive = false;
            var nextIndex = IndexRotator.NextIndex(activeIndex, increment, menuItems_.Count);
            menuItems_[nextIndex].IsActive = true;
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
