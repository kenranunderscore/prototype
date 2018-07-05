import pytest
import sdl2
from prototype import colorscheme
from prototype.menus import MenuItem, MainMenu
from prototype.scenetype import SceneType
from prototype.options import Options


@pytest.fixture
def area():
    return sdl2.SDL_Rect(10, 20, 10, 5)


def test_contains_too_far_left(area):
    assert not area.contains(9, 12)


def test_contains_too_far_right(area):
    assert not area.contains(22, 12)


def test_contains_too_far_top(area):
    assert not area.contains(15, 5)


def test_contains_too_far_bottom(area):
    assert not area.contains(14, 28)


def test_contains_point_inside(area):
    assert area.contains(20, 20)


def test_menu_item_default_scene():
    item = MenuItem('cap')
    assert item.target_scene == SceneType.UNCHANGED


def test_menu_item_is_inactive_by_default():
    item = MenuItem('cap')
    assert not item.active


def test_menu_item_caption():
    item = MenuItem('foo bar baz')
    assert item.caption == 'foo bar baz'


def test_menu_item_active_state():
    item = MenuItem('foo', active=True)
    assert item.active


def test_menu_item_target_scene():
    item = MenuItem('bar', target_scene=SceneType.FILE_CHOICE)
    assert item.target_scene == SceneType.FILE_CHOICE


@pytest.fixture
def main_menu():
    return MainMenu(None, Options(100, 100))


@pytest.fixture
def event_return(mocker):
    event = mocker.patch('sdl2.SDL_Event', autospec=True)
    event.type = sdl2.SDL_KEYDOWN
    event.key.keysym.sym = sdl2.SDLK_RETURN
    return event


@pytest.fixture
def event_down(mocker):
    event = mocker.patch('sdl2.SDL_Event', autospec=True)
    event.type = sdl2.SDL_KEYDOWN
    event.key.keysym.sym = sdl2.SDLK_DOWN
    return event


@pytest.fixture
def event_up(mocker):
    event = mocker.patch('sdl2.SDL_Event', autospec=True)
    event.type = sdl2.SDL_KEYDOWN
    event.key.keysym.sym = sdl2.SDLK_UP
    return event


def test_main_menu_play_is_active(main_menu, event_return):
    target_scene = main_menu.handle_event(event_return)
    assert target_scene == SceneType.FILE_CHOICE


def test_main_menu_down_activates_options(main_menu, event_down, event_return):
    main_menu.handle_event(event_down)
    target_scene = main_menu.handle_event(event_return)
    assert target_scene == SceneType.OPTIONS


def test_main_menu_up_quits(main_menu, event_up, event_return):
    main_menu.handle_event(event_up)
    target_scene = main_menu.handle_event(event_return)
    assert target_scene == SceneType.QUIT


def test_main_menu_down_thrice_is_back_at_top(main_menu, event_down, event_return):
    main_menu.handle_event(event_down)
    main_menu.handle_event(event_down)
    main_menu.handle_event(event_down)
    target_scene = main_menu.handle_event(event_return)
    assert target_scene == SceneType.FILE_CHOICE


def test_mouse_up_returns_correct_scene(mocker, main_menu):
    event = mocker.patch('sdl2.SDL_Event', autospec=True)
    event.type = sdl2.SDL_MOUSEBUTTONUP
    event.button.x, event.button.y = 50, 50
    target_scene = main_menu.handle_event(event)
    assert target_scene == SceneType.OPTIONS


def test_active_item_color(mocker):
    mocker.patch('prototype.menus.sdl2')
    text_renderer = mocker.patch('prototype.textrenderer.TextRenderer')
    menu = MainMenu(text_renderer, Options(100, 100))
    menu.render()
    call = mocker.call('Play', mocker.ANY, colorscheme.ACTIVE_COLOR)
    text_renderer.render.assert_has_calls([call])
