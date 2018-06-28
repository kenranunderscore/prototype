from src import textanalysis


def test_single_word_count():
    count = textanalysis.count_words('foobar')
    assert count == 1


def test_count_of_multiple_words():
    count = textanalysis.count_words('these are some more words')
    assert count == 5


def test_word_count_with_special_chars():
    count = textanalysis.count_words('foo.bar and more;stuff! baz')
    assert count == 4
