from prototype.indexrotation import rotate_index


def test_zero_increment_keeps_index_unchanged():
    next_index = rotate_index(3, 0, 5)
    assert next_index == 3


def test_index_increases_when_sum_below_count():
    next_index = rotate_index(3, 2, 6)
    assert next_index == 5


def test_index_resets_when_overstepping_count():
    next_index = rotate_index(1, 1, 2)
    assert next_index == 0


def test_negative_increment_decreases_index():
    next_index = rotate_index(1, -1, 2)
    assert next_index == 0


def test_negative_increments_rotate():
    next_index = rotate_index(1, -5, 3)
    assert next_index == 2
