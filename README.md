# Integrated-Project-One
 
To play the current build, click [here](https://CynicalMouse.github.io/Integrated-Project-One/Builds/18-04-2022(WebGL)/index.html) (Last updated 18-04-2022)
  SOME STUFF DOESNT WORK IN WEB VERSION, DOWNLOAD THE .EXE BUILD IN THE BUILDS FOLDER
## Controls
- WASD/Arrow Keys to move
- Space to jump
- E to pick up and throw
  
## New Features  
- Pick of object
  - Press E to pick up and throw an object, currently the box with a hand icon
  - Does damage to enemy
    - Enemy HP configurable in editor
  - Note: if it doenst look like its throwing it is because it is colliding with the area around you, move to a more open section
- Enemy Projectile
  - Enemy which spawns projectile
  - Projectiles move towards player's location when it spawns
  - Projectile destroyed upon collision with anything
  - Speed of and time between projectiles configurable in editor

## Changes
- Removed player collision with throwable object
  
## Curent Features  
- Pause Menu
- Basic Movement  
- Basic Player following camera  
- Variable Jump Height 
  - hold space to go higher  
- Multi-Jump 
  - Double, Triple... config in editor  
- Moving Platforms 
  - Vertical and Horizontal 
    - Diagonal probably works but not tested  
- Platforms that can be jumped through/ fallen through  
- Death and Respawn  
  - Run into the block with a cross in it to die
- Basic Player following camera  
  - Added Cinemachine Camera
- Platforms that can be jumped through/ fallen through (press down to do so)

## Bugs
- If standing on the vertical moving block you can get squished 
- throw is easily hindered by surrounding terrain, bigger area or smaller block needed
- animation gets a bit wonky near edge
  - tried to fix by changing ground check from circle to rectangle but for somereason it broke everything so it remains a circle for now
