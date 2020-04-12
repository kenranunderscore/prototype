{ mkDerivation, base, brick, stdenv }:
mkDerivation {
  pname = "prototype";
  version = "0.1.0.0";
  src = ./.;
  isLibrary = true;
  isExecutable = true;
  libraryHaskellDepends = [ base brick ];
  executableHaskellDepends = [ base ];
  testHaskellDepends = [ base ];
  homepage = "https://github.com/kenranunderscore/prototype";
  license = stdenv.lib.licenses.bsd3;
}
