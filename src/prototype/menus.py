import ctypes
import os
import sdl2
from prototype import colorscheme
from prototype import indexrotation
from prototype.scenetype import SceneType


def contains(self, x, y):
    return x >= self.x and y >= self.y and x <= self.x + self.w and y <= self.y + self.h


sdl2.SDL_Rect.contains = contains


class MenuItem(object):
    def __init__(self, caption, target_scene=SceneType.UNCHANGED, active=False):
        self.caption = caption
        self.active = active
        self.target_scene = target_scene


class Menu(object):
    def __init__(self, text_renderer, options):
        self._text_renderer = text_renderer
        self._options = options
        self._menu_items = []

    @property
    def _active_menu_item(self):
        return next(x for x in self._menu_items if x.active)

    def _menu_item_color(self, item, x, y):
        if item.area.contains(x, y):
            color = colorscheme.ACTIVE_COLOR
        elif item.active:
            color = colorscheme.MOUSE_OVER_COLOR
        else:
            color = colorscheme.DEFAULT_COLOR
        return color

    def render(self):
        x, y = ctypes.c_int(0), ctypes.c_int(0)
        sdl2.SDL_GetMouseState(ctypes.byref(x), ctypes.byref(y))
        for item in self._menu_items:
            color = self._menu_item_color(item, x.value, y.value)
            self._text_renderer.render(item.caption, item.area, color)

    def handle_event(self, event):
        if event.type == sdl2.SDL_MOUSEBUTTONUP:
            x = event.button.x
            y = event.button.y
            for item in self._menu_items:
                if item.area.contains(x, y):
                    return item.target_scene
        if event.type == sdl2.SDL_KEYDOWN:
            return self._handle_key_down(event.key)
        return SceneType.UNCHANGED

    def _activate_next_menu_item(self, increment):
        active_index = self._menu_items.index(self._active_menu_item)
        self._active_menu_item.active = False
        next_index = indexrotation.rotate_index(active_index, increment, len(self._menu_items))
        self._menu_items[next_index].active = True

    def _handle_key_down(self, keyboard_event):
        increment = 0
        sym = keyboard_event.keysym.sym
        if sym == sdl2.SDLK_KP_ENTER or sym == sdl2.SDLK_RETURN:
            return self._active_menu_item.target_scene
        elif sym == sdl2.SDLK_UP:
            increment = -1
        elif sym == sdl2.SDLK_DOWN:
            increment = 1
        if increment != 0:
            self._activate_next_menu_item(increment)
        return SceneType.UNCHANGED

    def _calculate_areas(self):
        max_caption_width = max([len(x.caption) for x in self._menu_items])
        scaled_w = self._options.scaled_letter_width
        scaled_h = self._options.scaled_letter_height
        left = self._options.screen_width // 2 - max_caption_width * scaled_w // 2
        menu_height = scaled_h * (2 * len(self._menu_items) - 1)
        top = (self._options.screen_height - menu_height) // 2

        for i, item in enumerate(self._menu_items):
            caption = item.caption
            length = len(caption)
            offset = (max_caption_width - length) * scaled_w // 2
            area = sdl2.SDL_Rect(
                left + offset,
                top + 2 * i * scaled_h,
                length * scaled_w,
                scaled_h)
            item.area = area


class MainMenu(Menu):
    def __init__(self, text_renderer, options):
        super(MainMenu, self).__init__(text_renderer, options)
        self._menu_items.append(MenuItem('Play', SceneType.FILE_CHOICE, True))
        self._menu_items.append(MenuItem('Options', SceneType.OPTIONS))
        self._menu_items.append(MenuItem('Quit', SceneType.QUIT))
        self._calculate_areas()


class OptionsMenu(Menu):
    def __init__(self, text_renderer, options):
        super(OptionsMenu, self).__init__(text_renderer, options)
        self._menu_items.append(MenuItem('Back', SceneType.MAIN_MENU, True))
        self._calculate_areas()


class FileChoiceMenu(Menu):
    NUMBER_OF_VISIBLE_ITEMS = 3

    def __init__(self, text_renderer, options, prototype):
        super(FileChoiceMenu, self).__init__(text_renderer, options)
        self._prototype = prototype
        path = '../text_sources'
        files = [f for f in os.listdir(path) if os.path.isfile(os.path.join(path, f))]
        self._paths = dict((f, os.path.join(path, f)) for f in files)
        self._items = [MenuItem(f, SceneType.GAME) for f in files]
        self._index = len(self._items) // 2
        self._items[self._index].active = True
        self._adjust_visible_items(0)

    def handle_event(self, event):
        if event.type == sdl2.SDL_MOUSEBUTTONUP:
            x = event.button.x
            y = event.button.y
            for item in self._menu_items:
                if item.area.contains(x, y):
                    return item.target_scene
        elif event.type == sdl2.SDL_KEYDOWN:
            target_scene = self._handle_key_down(event.key)
            if target_scene == SceneType.GAME:
                path = self._paths[self._active_menu_item.caption]
                self._prototype.load_file(path)
                return target_scene
        return SceneType.UNCHANGED

    def render(self):
        super(FileChoiceMenu, self).render()
        if self._index - self.NUMBER_OF_VISIBLE_ITEMS // 2 > 0:
            self._render_up_arrow()
        if self._index + self.NUMBER_OF_VISIBLE_ITEMS // 2 < len(self._items) - 1:
            self._render_down_arrow()

    def _render_up_arrow(self):
        x = self._options.screen_width // 2 - self._options.scaled_letter_width // 2
        y = self._menu_items[0].area.y - 4 * self._options.scaled_letter_height
        self._text_renderer.render('↑', sdl2.SDL_Rect(x, y), colorscheme.DEFAULT_COLOR)

    def _render_down_arrow(self):
        x = self._options.screen_width // 2 - self._options.scaled_letter_width // 2
        last_area = self._menu_items[-1].area
        y = last_area.y + 4 * self._options.scaled_letter_height
        self._text_renderer.render(
            '↓',
            sdl2.SDL_Rect(x, y),
            colorscheme.DEFAULT_COLOR)

    def _adjust_visible_items(self, increment):
        next_abs_index = self._index + increment - self.NUMBER_OF_VISIBLE_ITEMS // 2
        max_index = len(self._items) - self.NUMBER_OF_VISIBLE_ITEMS
        start_index = max(min(next_abs_index, max_index), 0)
        end_index = start_index + self.NUMBER_OF_VISIBLE_ITEMS
        self._menu_items = self._items[start_index:end_index]
        self._calculate_areas()

    def _activate_next_menu_item(self, increment):
        self._adjust_visible_items(increment)
        next_relative_index = self._menu_items.index(self._active_menu_item) + increment
        if next_relative_index < len(self._menu_items) and next_relative_index >= 0:
            self._active_menu_item.active = False
            self._menu_items[next_relative_index].active = True
            self._index += increment
