import constants

class Options(object):
    def __init__(self, screen_width, screen_height, scale=2.0):
        self.screen_width = screen_width
        self.screen_height = screen_height
        self.scale = scale
    
    @property
    def scaled_letter_width(self):
        return int(round(self.scale * constants.DEFAULT_LETTER_WIDTH))
    
    @property
    def scaled_letter_height(self):
        return int(round(self.scale * constants.DEFAULT_LETTER_HEIGHT))