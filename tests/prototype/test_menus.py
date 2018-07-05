import pytest
from sdl2 import SDL_Rect
from prototype.menus import MenuItem
from prototype.scenetype import SceneType


@pytest.fixture
def area():
    return SDL_Rect(10, 20, 10, 5)


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


