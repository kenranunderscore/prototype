import pytest
from prototype.prototype import Prototype


@pytest.fixture
def proto(mocker):
    mocker.patch('prototype.prototype.textprocessing.load_file', return_value='Some data')
    prototype = Prototype()
    prototype.load_file('My path')
    return prototype


def test_loaded_file_becomes_text(proto):
    assert proto.text == 'Some data'


def test_typing_wrong_letter_does_not_change_text(proto):
    proto.type_letter('x')
    assert proto.text == 'Some data'


def test_typing_wrong_letter_returns_failure(proto):
    typing_result = proto.type_letter('x')
    assert not typing_result


def test_typing_correct_letter_returns_success(proto):
    typing_result = proto.type_letter('S')
    assert typing_result


def test_typing_correct_letter_shortens_text(proto):
    proto.type_letter('S')
    assert proto.text == 'ome data'
