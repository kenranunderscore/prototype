def load_file(path):
    # TODO handle missing file
    with open(path, 'r') as f:
        content = f.read()
    return _process_text(content)


def _process_text(text):
    # TODO handle empty text
    # TODO refactor -> make code pythonic
    processed = []
    skipped = False
    index = 0
    for c in text:
        if c.isspace() and not skipped:
            if index > 0:
                processed.append(' ')
                index += 1
            skipped = True
        else:
            skipped = False
            processed.append(c)
            index += 1
    return ''.join(processed)
