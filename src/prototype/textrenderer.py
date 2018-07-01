from prototype import texture
from prototype import letterclips
from sdl2 import SDL_Color


class TextRenderer(object):
    def __init__(self, options):
        self._options = options

    def initialize(self, renderer):
        self._letters = texture.load_texture(
            b"../resources/letters.png", # TODO make configurable
            renderer,
            SDL_Color(0xff, 0, 0xdc))

    def render(self, text, target_area, color=None):
        if color:
            self._letters.set_color_mod(color)
        for i, c in enumerate(text):
            self._letters.render(
                target_area.x + i * self._options.scaled_letter_width,
                target_area.y,
                letterclips.get_clip(c),
                self._options.scale)
