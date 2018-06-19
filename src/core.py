import ctypes
from sdl2 import *
from sdl2.sdlimage import *
from menus import MainMenu, OptionsMenu, FileChoiceMenu
from scene_type import SceneType
from game_scene import GameScene
from prototype import Prototype

class Core(object):
    def __init__(self, options, text_renderer):
        self._options = options
        self._text_renderer = text_renderer
        self._prototype = Prototype()
        self._game_scene = GameScene(self._prototype, self._text_renderer, self._options)

        if (SDL_Init(SDL_INIT_VIDEO) < 0 or IMG_Init(IMG_INIT_PNG) < 0):
            print('Failed to initialize SDL or any of its parts: ' + SDL_GetError())
            exit(-1)
            
        self._window = SDL_CreateWindow(
            b'prototype',
            SDL_WINDOWPOS_CENTERED,
            SDL_WINDOWPOS_CENTERED,
            self._options.screen_width,
            self._options.screen_height,
            SDL_WINDOW_SHOWN)
        self._renderer = SDL_CreateRenderer(self._window, 0, SDL_RENDERER_ACCELERATED)
        SDL_StartTextInput()
    
    def run(self):
        self._text_renderer.initialize(self._renderer)
        self._scene = MainMenu(self._text_renderer, self._options)

        isRunning = True
        event = SDL_Event()
        while (isRunning):
            if (SDL_WaitEvent(ctypes.byref(event)) != 0):
                if (event.type == SDL_QUIT):
                    isRunning = False
                    break

            SDL_SetRenderDrawColor(self._renderer, 0, 0, 0, 0xff)
            SDL_RenderClear(self._renderer)
            target_scene = self._scene.handle_event(event)
            if not self._adjust_target_scene(target_scene):
                isRunning = False
                break
            self._scene.render()
            SDL_RenderPresent(self._renderer)
    
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
        SDL_DestroyRenderer(self._renderer)
        SDL_DestroyWindow(self._window)
        SDL_Quit()