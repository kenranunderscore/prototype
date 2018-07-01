import pytest
from prototype import core


@pytest.fixture
def sdl(mocker):
    return mocker.patch('prototype.core.sdl2')


def test_program_exits_when_sdl_fails_to_load(mocker, sdl):
    sdl.SDL_Init.return_value = -1
    with pytest.raises(SystemExit):
        c = core.Core(None, None)


def test_program_exits_when_img_fails_to_load(mocker, sdl):
    sdl.SDL_Init.return_value = 1
    sdl.sdlimage.IMG_Init.return_value = -1
    with pytest.raises(SystemExit):
        c = core.Core(None, None)
