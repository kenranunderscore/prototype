import pytest
from main import main


@pytest.fixture
def core(mocker):
    core = mocker.patch('main.Core')
    core.return_value = core
    return core


def test_main_cleans_up(core):
    main()
    core.cleanup.assert_called()


def test_main_runs_the_app(core):
    main()
    core.run.assert_called()
