import sdl2
from sdl2 import sdlimage


class Texture(object):
    def __init__(self, texture, renderer, width, height):
        self._texture = texture
        self._renderer = renderer
        self._width = width
        self._height = height

    def render(self, x, y, clip, scale=1.0):
        w = self._width if clip is None else clip.w
        h = self._height if clip is None else clip.h
        renderArea = sdl2.SDL_Rect(x, y, self._apply_scaling(w, scale), self._apply_scaling(h, scale))
        sdl2.SDL_RenderCopy(self._renderer, self._texture, clip, renderArea)

    def free(self):
        sdl2.SDL_DestroyTexture(self._texture)

    def set_color_mod(self, color):
        sdl2.SDL_SetTextureColorMod(self._texture, color.r, color.g, color.b)

    def _apply_scaling(self, length, scale):
        return int(round(scale * length))


def load_texture(path, renderer, color_key=None):
    surface = sdlimage.IMG_Load(path)
    if color_key:
        sdl2.SDL_SetColorKey(
            surface,
            1,
            sdl2.SDL_MapRGB(surface.contents.format, color_key.r, color_key.g, color_key.b)
        )

    sdl_texture = sdl2.SDL_CreateTextureFromSurface(renderer, surface)
    texture = Texture(sdl_texture, renderer, surface.contents.w, surface.contents.h)
    sdl2.SDL_FreeSurface(surface)
    return texture
