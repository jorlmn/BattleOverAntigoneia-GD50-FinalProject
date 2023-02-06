# (EN/US) Battle Over Antigoneia - CS50's Introduction to Game Development Final Project - By Jorel Lemes
#### Video Demo:  <https://youtu.be/GOVWQYQFtyQ>

Battle Over Antigoneia is a Unity-3D space battle video game, inspired by the Battle Over Coruscant as depicted in Star Wars Episode III: Revenge of the Sith.

In this initial implementation of the game, the player takes control of one warship and must push through the enemy blockade to reach the Allied space station, thus winning the match. If the player ship dies, the match is over.

## Why?
The space battle theme was chosen in order to minimize the need for 3D models and lower the difficulty in creating interesting levels and scenarios. By choosing this theme, an immersive level can be halfway into creation by just choosing a great skybox. Thus, this allowed me to focus on the programming side of things and to create relatively complex Weapon, Ship and Health systems, which is the core of the game, as explained further below.

## Controls
By default, the player uses WASD keys to move their ship, and right mouse click to aim and left click to fire. To go faster, they can press the SHIFT key for turbo, and C key to go slower. Mouse scrollwheel changes the camera zoom, and holding the middle mouse button and moving the mouse will pivot the camera. The Minus key is used to reset the camera position. Lastly, the Escape key opens the menu. On the Controls panel, the player can change most of these keybindings.

## Settings
The player can change the UI, Effects and Music volumes independently from one another. The player can also change the difficulty level, which impacts the AI damage multiplier, firing delay and accuracy. On Very Hard, the AI will fire their weapons quicker, more accurately, and doing more damage.

## Main Systems
### \MenuAndSettings
#### State Manager
Controls the Game State and Player Aim state. Once the state change method is evoked, the current state's Exit method is called, followed by the new state's Entry method, which allows for a flexible change in state and the addition of new states without having to change others. The Game State controls which player inputs are enabled, the game timescale (and pause), the opening and closing of menus and the game over screen, and more.

#### Input Manager, Keybindings and Input Remapping
Input Manager centers all player inputs into one script, triggering methods related to player inputs on other scripts and/or being referenced by other scripts that need to check for input. Keybindings holds the keys itself that relate to a particular input, and are referenced by the Input Manager. Input Remapping is the script attached to the Controls UI Panel, used to remap the keys in the Keybinding script.

#### GameSettings and AudioSettings
Centers all general game and audio settings, such as difficulty and music levels. As with the Input Remapping script above, they reference UI elements in their respective panels that allow the player to see their settings, reset or change them.

#### \MenuAndSettings\MatchInit
MatchSettings and the Play UI Panel set up the match, passing information from the Title Scene (such as which warship the player chose) to the Play Scene. This information is then referenced by the MatchStart script, where the player ship is spawned and referenced by the AIs as the enemy object. GameEndManager controls the game's end, triggering player death and player victory when suitable. VictoryTrigger is the script that checks if the player neared the Space Station, the current goal of the game.

### \Camera
The topdown camera is composed of three objects in the same hierarchy: 1) The main object (or grandParent), which is empty, rotates sideways on player input, and is fixed on the target (player) at every Update; 2) The second object (or parent - and also empty) which handles the zoom, its altitude above the grandparent changing on player input, simulating zoom in and out; and 3) The camera, a children of the 2nd object.

When the player aims, the secondary object moves towards the aim position, thus pushing the camera. When the player stops aiming, it smoothly returns to the same X and Z position as the main object (which sat fixed on the player ship) with a Coroutine, thus resetting the camera back to the player's location.

With this setup, one can have a flexible camera with zoom in and out, pivotting, and changes in several rotation axis and position, all the while keeping references to the target and smoothly changing these values on player input.

This topdown camera was inspired by the videogame Foxhole.

### \Weapons
Weapon class handles the shooting of projectiles and the rotation and aiming of the weapon (i.e, a Turret). Its children classes add particular features to it (like firing multiple projectiles at once, one from each cannon in a turret - or firing a laser beam) and implement the actual shooting method. WeaponExtensions hold all Weapon class extension methods, used by the Player and AI weapon scripts.

On shooting, the weapon passes the projectile data to the projectile object. The projectile then checks for collisions. Once the projectile hits something with a Health component, it invokes the TakeDamage function with the appropriate values. This function returns true if the impacted object was not shielded, and false if it was, thus allowing the projectile to spawn, on the exact impact position, the correct particle effect: an explosion or a shield-hit effect.

Projectiles are not instantiated every time a weapon fires or destroyed when they hit something. Instead, at the start of the game a number of projectiles of each featured type (i.e, those ScriptableObjects present in the SOReference Script lists) are instantiated and set inactive by the ProjectilePool script. Once a weapon fires, it grabs an inactive projectile from that pool of the right ID, and activates it in the weapon firing position and handles its physics movement. Once a projectile hits something, it deactivates and thus returns to the pool. If all projectiles in the pool are active, the ProjectilePool script spawns a new one, adds it to the pool, and returns it immediately to the Weapon script that last checked for an inactive projectile. These projectiles and the ProjectilePool are used by all weapons with the exception of the BeamWeapon class, as it simply uses a LineRenderer and triggers damage per second on the object being impacted by the Raycast that set the target point of that LineRenderer.

Lastly, the particle effects that play when a projectile hits something are also grabbed from an object pool much alike the ProjectilePool: The ParticlePool.

### \Entities
#### \Entities\ShipSystems
The scripts attached to the warships, both player and AI. ShipSystemManager centers all the references to other ship components and weapons. Once a ship's Hull Health reaches 0, it triggers the OnDeath event on that script, spawning a Wreckage (an object with the same model as the alive one, but without any scripts, with the exception of the ShipWreckage script) on the same position and rotation as the destroyed ship, and deactivating it.

Every Weapon in a ship has, alongside a Weapon component, a WeaponHealth component and a collider. If a projectile hits that collider, it calls the TakeDamage function. The WeaponHealth component, instead of inflicting the damage on the health of the weapon at once, it instead calls the TakeDamage function on the HullHealth, the base health of the ship, to check if the ship is shielded or not. If it was shielded, then it damages the ship's shields only. Otherwise, it damages the weapon's health. Once it reaches 0, the Weapon's active bool variable turns to false, thus disabling the weapon from shooting and rotating, and spawns a fire on its position.

The same logic applies to the ShipEngine and its EngineHealth counterpart, with the addition that every time it is damaged, its velocities are proportionally diminished and its glowing engine effects are proportionally disabled, and once its health reaches 0, it disables the Turbo feature.

CriticalPartsManager holds a dictionary with the main hardpoints of a ship, including engine, all of its weapons, and several empty objects across its hull. This script is used by the AIWeaponsManager, so the AI's weapons can shoot across the length of the player ship and on several different locations, as well as prioritize particular systems, like firing at the Engine specifically to disable it, or the player's weapons.

#### \Entities\Player
The scripts attached to the player warships. PlayerMovement handles the movement of the ship based on player input and the ship velocities from the ShipEngine script. PlayerTarget simply gets the target point with a Raycast from the camera to the 3D position where the mouse is. When aiming, the player weapons will try to rotate and shoot towards that point, something which is done by the PlayerWeaponsManager, which grabs the weapon object references from the ShipSystemManager.

#### \Entities\AI
AI is not yet fully implemented. As it stands, AI firing at the player's ship is the only finished feature. Their weapons will attempt to rotate and fire at the nearest transform in the Player's CriticalPartsManager list of transforms of a specified hardpoint, such as Hull or Weapons. Every four seconds, these weapons will try to find a better target. Additionally, the AI's damage, firing delay and accuracy while shooting are dynamically grabbed from the chosen difficulty preset, which are ScriptableObjects that hold these values, such as Easy_AI, Medium_AI and so on.

## Credits
- Game design, programming, particle effects, UI animations, documentation, and everything else not mentioned below - Jorel Lemes
- Battleship, Freighter, Scout gunship, Light Turret, and Heavy Tri Turret models - Marcus Brandão
- Raider gunship, Space Station, and Beam Turret models - Rafael Tavares
- Skybox - SpaceSkies Free by Pulsar Bytes, Unity Asset Store
- Font: Ethnocentric font - Typodermic Fonts
- Title Image - Yuri_B
https://pixabay.com/illustrations/space-galaxy-planet-universe-1569133/

## Sounds
(most were used trimmed and with pitch changes)

- Title song - onderwish
https://pixabay.com/sound-effects/sci-fi-survival-dreamscape-6319/
- UI Panel switch - alexo400
https://pixabay.com/sound-effects/sci-fi-door-6451/
- UI Button click - peepholecircus
https://pixabay.com/sound-effects/sci-fi-door-14782/
- Light Weapon fire - C3Sabertooth
https://pixabay.com/sound-effects/blaster-multiple-14893/
- Other Weapon fire - Kreastricon62
https://pixabay.com/sound-effects/heavy-mounted-assault-plasma-gun-37856/
- Explosions - FlashTrauma
https://pixabay.com/sound-effects/explosion-6055/
- Ship engines - SamsterBirdies
https://pixabay.com/sound-effects/jet-engine-startup-14537/

## Planned features:
- Procedurally generated levels
- AI navmesh-based movement
- Ship Power and Crew systems
- New ships such as cruisers and carriers
- New weapons such as torpedoes, missiles and bombers
- Faction system with AI allies and enemies that fight each other
- New gamemodes such as escort, capture the flag, annihilation
