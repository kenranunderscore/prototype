def rotate_index(current, increment, count):
    next_rel_index = (current + increment) % count
    if next_rel_index < 0:
        next_rel_index += count
    return next_rel_index
