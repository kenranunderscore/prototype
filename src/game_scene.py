from scene_type import SceneType
from color_scheme import ERROR_COLOR, DEFAULT_COLOR
from sdl2 import *

class GameScene(object):
    def __init__(self, text_renderer, options):
        self._text_renderer = text_renderer
        self._options = options

    def handle_event(self, event):
        if event.type == SDL_KEYDOWN and event.key.keysym.sym == SDLK_ESCAPE:
            return SceneType.MAIN_MENU
        if event.type == SDL_TEXTINPUT:
            char = event.text.text
            print(char)
            print(type(char))
            return SceneType.MAIN_MENU
        return SceneType.UNCHANGED
    
    def render(self):
        area = self._calculate_render_area()
        self._text_renderer.render("Hello guys, this is some foo bar baz", area, ERROR_COLOR)
    
    def _calculate_render_area(self):
        return SDL_Rect(
            int(0.3 * self._options.screen_width),
            int(0.5 * self._options.screen_height - 0.5 * self._options.scaled_letter_height),
            int(0.5 * self._options.screen_width),
        )