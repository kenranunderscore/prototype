from prototype.core import Core
from prototype.options import Options
from prototype.textrenderer import TextRenderer


def main():
    options = Options(1600, 900)
    c = Core(options, TextRenderer(options))
    c.run()
    c.cleanup()


if __name__ == '__main__':
    main()
