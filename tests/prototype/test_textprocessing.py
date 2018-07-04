import pytest
from prototype import textprocessing


@pytest.fixture
def fake_file(mocker):
    data = "This is a file\n! A     long whitespace\r\nriddled with \t special characters."
    mocker.patch('builtins.open', new_callable=mocker.mock_open, read_data=data)


def test_processed_text(fake_file):
    processed = textprocessing.load_file('foopath')
    assert processed == "This is a file ! A long whitespace riddled with special characters."
