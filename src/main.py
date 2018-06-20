from core import Core
from options import Options
from text_renderer import TextRenderer


def main():
    options = Options(1600, 900)
    core = Core(options, TextRenderer(options))
    core.run()
    core.cleanup()


main()
