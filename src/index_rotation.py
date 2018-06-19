def rotate_index(current, increment, count):
    next_relative_index = (current + increment) % count
    next_index = next_relative_index if next_relative_index >= 0 else next_relative_index + count
    return next_index