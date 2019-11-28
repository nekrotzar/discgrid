# discgrid
A basic game with disc throwing mechanic to experiement with Unity's global lighting.

## Setup

- Developed and tested on **Unity 2019.2.11f1**


## Overview

This project was developed to explore Unity's rendering pipeline and global illumination. To this end, we developed a game with a basic level inspired by the Tron disc wars.

The goal of the game is to reach the white platform and destroy the maximum amouny of enemies to get the highest score. Using a first-person shooter approach, the player can aim and throw a disc to damage enemy turrets. The game is over when the player health is depleted or fall into the "abyss".

### HUD and Menus
|![](https://github.com/nekrotzar/discgrid/blob/master/reference/images/mainmenu.png)|![](https://github.com/nekrotzar/discgrid/blob/master/reference/images/submenu.png)|![](https://github.com/nekrotzar/discgrid/blob/master/reference/images/hud.png)|
|----|---|---|
| Main menu|Controls sub-menu B| Head-up display |


### Scene and Lighting
The scene is composed by directional, spot and emissive lights. The indoor section of our level was built with static geometry to whom were baked lightmaps from the emissive light. Post processing effects such as gloom, motion blur and color grading were also used. In addition to this, we tried different effects based on shaders and particle systems.

|![](https://github.com/nekrotzar/discgrid/blob/master/reference/images/gameplay.gif)|
|----|



## Authors
---
- [Luís Fonseca](https://github.com/nekrotzar)
- [Ricardo Fonseca](https://github.com/rpsfonseca)



