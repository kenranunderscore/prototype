from prototype.textcropping import crop


def test_short_text_is_not_cropped():
    text = 'Foo Bar'
    cropped = crop(text, 20, 2)
    assert cropped == text


def test_long_text_is_cropped():
    text = 'This is a long text.'
    cropped = crop(text, 10, 1)
    assert cropped == 'This is a '


def test_exactly_fitting_text_is_not_cropped():
    text = 'Blub'
    cropped = crop(text, 10, 1)
    assert cropped == text
