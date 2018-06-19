import ctypes
import color_scheme
import index_rotation
from sdl2 import *
from scene_type import SceneType

def contains(self, x, y):
    return x >= self.x and y >= self.y and x <= self.x + self.w and y <= self.y + self.h

SDL_Rect.contains = contains

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
            color = color_scheme.ACTIVE_COLOR
        elif item.active:
            color = color_scheme.MOUSE_OVER_COLOR
        else:
            color = color_scheme.DEFAULT_COLOR
        return color

    def render(self):
        x, y = ctypes.c_int(0), ctypes.c_int(0)
        SDL_GetMouseState(ctypes.byref(x), ctypes.byref(y))
        for item in self._menu_items:
            color = self._menu_item_color(item, x.value, y.value)
            self._text_renderer.render(item.caption, item.area, color)
    
    def handle_event(self, event):
        if event.type == SDL_MOUSEBUTTONUP:
            x = event.button.x
            y = event.button.y
            for item in self._menu_items:
                if item.area.contains(x, y):
                    return item.target_scene
        if event.type == SDL_KEYDOWN:
            return self._handle_key_down(event.key)
        return SceneType.UNCHANGED

    def _activate_next_menu_item(self, increment):
        active_index = self._menu_items.index(self._active_menu_item)
        self._active_menu_item.active = False
        next_index = index_rotation.rotate_index(active_index, increment, len(self._menu_items))
        self._menu_items[next_index].active = True
    
    def _handle_key_down(self, keyboard_event):
        increment = 0
        sym = keyboard_event.keysym.sym
        if sym == SDLK_KP_ENTER or sym == SDLK_RETURN:
            return self._active_menu_item.target_scene
        elif sym == SDLK_UP:
            increment = -1
        elif sym == SDLK_DOWN:
            increment = 1
        if increment != 0:
            self._activate_next_menu_item(increment)
        return SceneType.UNCHANGED

    def _calculate_areas(self):
        max_caption_width = max([len(x.caption) for x in self._menu_items])
        left = self._options.screen_width // 2 - max_caption_width * self._options.scaled_letter_width // 2
        menu_height = self._options.scaled_letter_height * (2 * len(self._menu_items) - 1)
        top = (self._options.screen_height - menu_height) // 2

        for i, item in enumerate(self._menu_items):
            caption = item.caption
            length = len(caption)
            offset = (max_caption_width - length) * self._options.scaled_letter_width // 2
            area = SDL_Rect(
                left + offset,
                top + 2 * i * self._options.scaled_letter_height,
                length * self._options.scaled_letter_width,
                self._options.scaled_letter_height
            )
            item.area = area

class MainMenu(Menu):
    def __init__(self, text_renderer, options):
        super(MainMenu, self).__init__(text_renderer, options)
        self._menu_items.append(MenuItem('Play', SceneType.GAME, True))
        self._menu_items.append(MenuItem('Options', SceneType.OPTIONS))
        self._menu_items.append(MenuItem('Quit', SceneType.QUIT))
        self._calculate_areas()

class OptionsMenu(Menu):
    def __init__(self, text_renderer, options):
        super(OptionsMenu, self).__init__(text_renderer, options)
        self._menu_items.append(MenuItem('Back', SceneType.MAIN_MENU, True))
        self._calculate_areas()