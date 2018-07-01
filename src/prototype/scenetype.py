from enum import Enum


class SceneType(Enum):
    UNCHANGED = 0
    GAME = 1
    MAIN_MENU = 2
    OPTIONS = 3
    FILE_CHOICE = 4
    QUIT = 5
