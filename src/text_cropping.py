def crop(text, target_area_with, letter_width):
    max_number_of_letters = target_area_with / letter_width;
    return text if max_number_of_letters >= len(text) else text[:max_number_of_letters]