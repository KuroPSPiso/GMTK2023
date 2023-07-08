# [BULLETHELL] Gradius, Strikers1945 Series like

The player must reach the end of the stage to win. The player can chose between different difficulties. The AI is set in 3 difficulties which changes the frequency of attacks. Certain types of AI can drop powerups. Bombs can be used to wipe the field. At the end a boss stage is presented.

## AI

- simple plane
 * moves
 * pellets
 * 1 in 5 powerup
 * 4 max
 * onscreen 2sec
- heavy plane
 * moves
 * spread pellets
 * 3 max
 * onscreen 5 sec
- tank
 * move to pos and stick stationary
 * onscreen till scrolls off
 * disable fire halfway on screen
 * turrets rotate towards player
- turret
 * stationary placement

## player
- can move Y (X would be useful for pickup of power ups but no time to train model)
- has bombs (2 on start)
- once explodes can pickup one or multiple dropped bonusses
- shoots pellets or streams
 * streams can be heat seeking
 * pellets always shoot forward


 ## min viable

- 1 enemy type
- 1 stage
- char select
- 2 player types (raystorm like option then 1 straight pellet, 1 laser auto)
- AI controls

## bonus
- AI on training model