{ pkgs ? import <nixpkgs> {} }:

let
  prototype = import ./default.nix { inherit pkgs; };
in
with pkgs;
prototype.env.overrideAttrs (oldAttrs: rec {
  buildInputs = oldAttrs.buildInputs ++ [
    # Haskell dev tools
    haskellPackages.cabal-install
    haskellPackages.ghc
    haskellPackages.ghcide
    haskellPackages.ormolu
    haskellPackages.hlint
  ];
})
