def load_file(path):
    # TODO handle missing file
    with open(path, 'r') as f:
        content = f.read()
    return _process_text(content)


def _process_text(text):
    # TODO handle empty text
    processed = []
    skipped = False
    for c in text:
        if c.isspace():
            if not skipped:
                processed.append(' ')
                skipped = True
        else:
            skipped = False
            processed.append(c)
    return ''.join(processed)
