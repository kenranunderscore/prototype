import textprocessing
import textanalysis
import metrics


class Prototype(object):
    @property
    def text(self):
        return self._text

    @text.setter
    def text(self, value):
        self._full_text = value
        self._text = value

    def load_file(self, path):
        self.text = textprocessing.load_file(path)

    def type_letter(self, letter):
        if len(self.text) > 0 and self.text[0] != letter:
            return False
        self._text = self._text[1:]
        if not self.text:
            pass
        return True

    def wpm(self):
        word_count = textanalysis.count_words(self._full_text)
        wpm = metrics.wpm(word_count, 60000)  # TODO
        return wpm
