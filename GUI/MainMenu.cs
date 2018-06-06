﻿namespace prototype.GUI
{
    using System.Collections.Generic;
    using System.Linq;
    using static SDL2.SDL;

    internal class MainMenu
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
            var totalMenuHeight = 5 * Defaults.LetterHeight;
            var x = options.ScreenWidth / 2 - maxMenuItemLength * Defaults.LetterWidth;
            var centerY = options.ScreenHeight / 2;
            var y = centerY - totalMenuHeight / 2;

            menuItems_ = new List<MenuItem>
            {
                new MenuItem(
                    textRenderer,
                    "Play",
                    new SDL_Rect { x = x, y = y, w = 4 * Defaults.LetterWidth, h = Defaults.LetterHeight },
                    TargetScene.Game),
                new MenuItem(
                    textRenderer,
                    "Options",
                    new SDL_Rect { x = x, y = y + 2 * Defaults.LetterHeight, w = 7 * Defaults.LetterWidth, h = Defaults.LetterHeight },
                    TargetScene.Options),
                new MenuItem(
                    textRenderer,
                    "Quit",
                    new SDL_Rect { x = x, y = y + 4 * Defaults.LetterHeight, w = 4 * Defaults.LetterWidth, h = Defaults.LetterHeight },
                    TargetScene.Quit),
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
