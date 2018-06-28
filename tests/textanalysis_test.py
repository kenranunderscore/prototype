from src import textanalysis


def test_single_word_count():
    count = textanalysis.count_words('foobar')
    assert count == 1
