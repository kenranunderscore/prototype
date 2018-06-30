from src.core import Core
from src.options import Options
from src.textrenderer import TextRenderer


def main():
    options = Options(1600, 900)
    core = Core(options, TextRenderer(options))
    core.run()
    core.cleanup()


main()
