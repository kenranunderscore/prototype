import setuptools


REQUIRED_PACKAGES = ['pysdl2', 'pytest', 'pytest_mock']


setuptools.setup(
    name='prototype',
    version='0.1',
    description='A typing program',
    author='kenranunderscore',
    url='https://github.com/kenranunderscore/prototype',
    packages=setuptools.find_packages('src'),
    package_dir={'': 'src'},
    install_requires=REQUIRED_PACKAGES,
    include_package_data=True
)
