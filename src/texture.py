from sdl2 import *
from sdl2.sdlimage import IMG_Load

class Texture(object):
    def __init__(self, texture, renderer, width, height):
        self._texture = texture
        self._renderer = renderer
        self._width = width
        self._height = height
    
    def render(self, x, y, clip, scale = 1.0):
        w = self._width if clip is None else clip.w
        h = self._height if clip is None else clip.h
        renderArea = SDL_Rect(x, y, self._apply_scaling(w, scale), self._apply_scaling(h, scale))
        SDL_RenderCopy(self._renderer, self._texture, clip, renderArea)

    def free(self):
        SDL_DestroyTexture(self._texture)
    
    def set_color_mod(self, color):
        SDL_SetTextureColorMod(self._texture, color.r, color.g, color.b)
    
    def _apply_scaling(self, length, scale):
        return int(round(scale * length))


def loadTexture(path, renderer, color_key=None):
    surface = IMG_Load(path)
    if color_key is not None:
        SDL_SetColorKey(
            surface,
            1,
            SDL_MapRGB(surface.contents.format, color_key.r, color_key.g, color_key.b))
    
    sdl_texture = SDL_CreateTextureFromSurface(renderer, surface)
    texture = Texture(sdl_texture, renderer, surface.contents.w, surface.contents.h)
    SDL_FreeSurface(surface)
    return texture