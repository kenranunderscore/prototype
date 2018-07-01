import pytest
from sdl2 import SDL_Rect
from prototype.textrenderer import TextRenderer
from prototype.options import Options


@pytest.fixture
def texture(mocker):
    texture = mocker.patch('prototype.texture.Texture')
    mocker.patch('prototype.texture.load_texture', return_value=texture)
    return texture


@pytest.fixture
def clips(mocker):
    return mocker.patch('prototype.textrenderer.letterclips')


@pytest.fixture
def renderer(texture):
    renderer = TextRenderer(Options(200, 100))
    renderer.initialize(None)
    return renderer


def test_all_characters_are_rendered(renderer, texture):
    renderer.render('Foo', SDL_Rect(1, 2, 3, 4))
    assert texture.render.call_count == 3


def test_correct_clips_are_requested(mocker, renderer, clips):
    renderer.render('Foo !', SDL_Rect(1, 2, 3, 4))
    calls = [
        mocker.call('F'),
        mocker.call('o'),
        mocker.call('o'),
        mocker.call(' '),
        mocker.call('!')
    ]
    clips.get_clip.assert_has_calls(calls)


def test_color_mod_is_set_when_needed(renderer, texture):
    renderer.render('bar', SDL_Rect(4, 3, 2, 1), 'color')
    texture.set_color_mod.assert_called_with('color')
