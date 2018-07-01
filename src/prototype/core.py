import ctypes
import sdl2
import sdl2.sdlimage
import sys
from prototype.menus import MainMenu, OptionsMenu, FileChoiceMenu
from prototype.scenetype import SceneType
from prototype.gamescene import GameScene
from prototype.prototype import Prototype


class Core(object):
    def __init__(self, options, text_renderer):
        self._options = options
        self._text_renderer = text_renderer
        self._prototype = Prototype()
        self._game_scene = GameScene(self._prototype, self._text_renderer, self._options)

        if (sdl2.SDL_Init(sdl2.SDL_INIT_VIDEO) < 0
                or sdl2.sdlimage.IMG_Init(sdl2.sdlimage.IMG_INIT_PNG) < 0):
            error = sdl2.SDL_GetError().decode('utf-8')
            print('Failed to initialize SDL or any of its parts: ' + error)
            sys.exit(-1)

        self._window = sdl2.SDL_CreateWindow(
            b'prototype',
            sdl2.SDL_WINDOWPOS_CENTERED,
            sdl2.SDL_WINDOWPOS_CENTERED,
            self._options.screen_width,
            self._options.screen_height,
            sdl2.SDL_WINDOW_SHOWN)
        self._renderer = sdl2.SDL_CreateRenderer(self._window, 0, sdl2.SDL_RENDERER_ACCELERATED)
        sdl2.SDL_StartTextInput()

    def run(self):
        self._text_renderer.initialize(self._renderer)
        self._scene = MainMenu(self._text_renderer, self._options)

        isRunning = True
        event = sdl2.SDL_Event()
        while (isRunning):
            if (sdl2.SDL_WaitEvent(ctypes.byref(event)) != 0):
                if (event.type == sdl2.SDL_QUIT):
                    isRunning = False
                    break

            sdl2.SDL_SetRenderDrawColor(self._renderer, 0, 0, 0, 0xff)
            sdl2.SDL_RenderClear(self._renderer)
            target_scene = self._scene.handle_event(event)
            if not self._adjust_target_scene(target_scene):
                isRunning = False
                break
            self._scene.render()
            sdl2.SDL_RenderPresent(self._renderer)

    def _adjust_target_scene(self, target_scene):
        if target_scene == SceneType.QUIT:
            return False
        elif target_scene == SceneType.MAIN_MENU:
            self._scene = MainMenu(self._text_renderer, self._options)
        elif target_scene == SceneType.OPTIONS:
            self._scene = OptionsMenu(self._text_renderer, self._options)
        elif target_scene == SceneType.GAME:
            self._scene = self._game_scene
        elif target_scene == SceneType.FILE_CHOICE:
            self._scene = FileChoiceMenu(self._text_renderer, self._options, self._prototype)
        return True

    def cleanup(self):
        sdl2.SDL_DestroyRenderer(self._renderer)
        sdl2.SDL_DestroyWindow(self._window)
        sdl2.SDL_Quit()
