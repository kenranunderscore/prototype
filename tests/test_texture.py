from sdl2 import SDL_Color
from src import texture


def test_load_texture_loads_correct_image(mocker):
    surface = mocker.patch('src.texture.sdl2.SDL_Surface')
    img_load = mocker.patch('src.texture.sdl2.sdlimage.IMG_Load', return_value=surface)
    mocker.patch('src.texture.sdl2.SDL_FreeSurface')
    mocker.patch('src.texture.sdl2.SDL_CreateTextureFromSurface')
    path = b'foo'
    texture.load_texture(path, None)
    img_load.assert_called_with(path)


def test_color_key_values_are_passed_to_sdl(mocker):
    surface = mocker.patch('src.texture.sdl2.SDL_Surface')
    mocker.patch('src.texture.sdl2.sdlimage.IMG_Load', return_value=surface)
    mocker.patch('src.texture.sdl2.SDL_FreeSurface')
    mocker.patch('src.texture.sdl2.SDL_CreateTextureFromSurface')
    sdl_set_color_key = mocker.patch('src.texture.sdl2.SDL_SetColorKey')
    sdl_maprgb = mocker.patch('src.texture.sdl2.SDL_MapRGB')
    texture.load_texture(None, None, SDL_Color(0x12, 0xc8, 0x7e))
    sdl_set_color_key.assert_called()
    sdl_maprgb.assert_called_with(mocker.ANY, 0x12, 0xc8, 0x7e)
