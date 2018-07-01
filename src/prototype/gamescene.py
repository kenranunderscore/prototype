import sdl2
from prototype.scenetype import SceneType
from prototype.colorscheme import ERROR_COLOR, DEFAULT_COLOR


class GameScene(object):
    def __init__(self, prototype, text_renderer, options):
        self._prototype = prototype
        self._prototype.text = "Some foo bar text"
        self._text_renderer = text_renderer
        self._options = options

    def handle_event(self, event):
        if event.type == sdl2.SDL_KEYDOWN and event.key.keysym.sym == sdl2.SDLK_ESCAPE:
            return SceneType.MAIN_MENU
        if event.type == sdl2.SDL_TEXTINPUT:
            char = event.text.text.decode('utf-8')
            type_result = self._prototype.type_letter(char)
            self._color = DEFAULT_COLOR if type_result else ERROR_COLOR
            if not self._prototype.text:
                print(self._prototype.wpm())
                return SceneType.MAIN_MENU
        return SceneType.UNCHANGED

    def render(self):
        area = self._calculate_render_area()
        self._text_renderer.render(self._prototype.text, area, ERROR_COLOR)

    def _calculate_render_area(self):
        return sdl2.SDL_Rect(
            int(0.3 * self._options.screen_width),
            int(0.5 * self._options.screen_height - 0.5 * self._options.scaled_letter_height),
            int(0.5 * self._options.screen_width),
        )
