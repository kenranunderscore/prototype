import pytest
from src import core


def test_program_exists_when_sdl_fails_to_load(mocker):
    mocker.patch('src.core.sdl2.SDL_Init', return_value=-1)
    mocker.patch('src.core.sdl2.SDL_GetError')
    with pytest.raises(SystemExit):
        c = core.Core(None, None)

def test_program_exists_when_img_fails_to_load(mocker):
    mocker.patch('src.core.sdl2.SDL_Init', return_value=0)
    mocker.patch('src.core.sdl2.sdlimage.IMG_Init', return_value=-1)
    mocker.patch('src.core.sdl2.SDL_GetError')
    with pytest.raises(SystemExit):
        c = core.Core(None, None)
