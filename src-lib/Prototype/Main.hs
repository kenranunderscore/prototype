module Prototype.Main
  ( main
  )
where

import qualified Brick
import qualified Brick.Widgets.Center as Center

ui :: Brick.Widget ()
ui =
  Center.center (Brick.str "Welcome to prototype")

main :: IO ()
main =
  Brick.simpleMain ui
