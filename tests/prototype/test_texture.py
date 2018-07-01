import pytest
import sdl2
from prototype import texture
from prototype.texture import Texture


@pytest.fixture
def surface(mocker):
    return mocker.patch('prototype.texture.sdl2.SDL_Surface')


@pytest.fixture
def sdl(mocker, surface):
    sdl = mocker.patch('prototype.texture.sdl2')
    sdl.sdlimage.IMG_Load.return_value = surface
    return sdl 


def test_load_texture_loads_correct_image(sdl):
    texture.load_texture(b'foo', None)
    sdl.sdlimage.IMG_Load.assert_called_with(b'foo')


def test_color_key_values_are_passed_to_sdl(mocker, sdl):
    texture.load_texture(None, None, sdl2.SDL_Color(0x12, 0xc8, 0x7e))
    sdl.SDL_SetColorKey.assert_called()
    sdl.SDL_MapRGB.assert_called_with(mocker.ANY, 0x12, 0xc8, 0x7e)


def test_load_texture_frees_loaded_surface(sdl, surface):
    texture.load_texture(b'abc', None)
    sdl.SDL_FreeSurface.assert_called_with(surface)


def test_texture_created_from_surface(mocker, sdl, surface):
    texture.load_texture(None, None)
    sdl.SDL_CreateTextureFromSurface.assert_called_with(mocker.ANY, surface)

def test_free_destroys_texture(sdl):
    texture = Texture('tex', None, 4, 4)
    texture.free()
    sdl.SDL_DestroyTexture.assert_called_with('tex')
