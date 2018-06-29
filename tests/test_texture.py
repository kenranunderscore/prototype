import pytest
from sdl2 import SDL_Color
from src import texture


@pytest.fixture
def surface(mocker):
    return mocker.patch('src.texture.sdl2.SDL_Surface')


@pytest.fixture
def free_surface(mocker):
    return mocker.patch('src.texture.sdl2.SDL_FreeSurface')


@pytest.fixture
def create_texture(mocker):
    return mocker.patch('src.texture.sdl2.SDL_CreateTextureFromSurface')


@pytest.fixture
def img_load(mocker, surface, free_surface, create_texture):
    return mocker.patch('src.texture.sdl2.sdlimage.IMG_Load', return_value=surface)


def test_load_texture_loads_correct_image(img_load):
    texture.load_texture(b'foo', None)
    img_load.assert_called_with(b'foo')


def test_color_key_values_are_passed_to_sdl(mocker, img_load):
    sdl_set_color_key = mocker.patch('src.texture.sdl2.SDL_SetColorKey')
    sdl_maprgb = mocker.patch('src.texture.sdl2.SDL_MapRGB')
    texture.load_texture(None, None, SDL_Color(0x12, 0xc8, 0x7e))
    sdl_set_color_key.assert_called()
    sdl_maprgb.assert_called_with(mocker.ANY, 0x12, 0xc8, 0x7e)


def test_load_texture_frees_loaded_surface(img_load, surface, free_surface):
    texture.load_texture(None, None)
    free_surface.assert_called_with(surface)


def test_texture_created_from_surface(mocker, surface, create_texture, img_load):
    texture.load_texture(None, None)
    create_texture.assert_called_with(mocker.ANY, surface)
