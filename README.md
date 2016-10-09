# 3D-Tower-Defense
3D Tower Defense Game for Android

# About the Game
Defend your base from enemy spheres. Place down towers in strategic positions, allowing for many different strategys to complete the level. Each level will have 20 rounds, each round becoming increasingly harder as the player progresses. 

# Initial Game Characters
    - Towers
        Flame: Does continues damage to an area of enemies (in a Cone)
        Ice: Freezes an area of enemeis (in a Cone)
        Rocket: Shoots rockets periodically at enemies dealing splash damage (in a circle), dealing less damage the farther the enemy is from the impact zone
        Turret: Shoots bullets at a single enemy
    
    - Enemies (Sorted By Color)
        White: Normal enemy, affected by every tower
        Red: Isn't affected by fire towers
        Blue: Isn't affected by ice towers
        Grey: Reinforced, takes more damage than others
        Black: Boss, takes a lot of damage and cannot be slowed

# Levels
    - Start with 100 credits
    - Rounds
        1-4: Only White enemies
        5: Introduce Red enemies
        6-9: Red and White Enemies
        10: Introduce Blue Enemies
        11-14: White, Red, & Blue enemies
        15: Introduce Grey enemies
        15-19: White Red, Blue, & Grey Enemies
        20: All other enemies come in, and Boss (Black) comes in at the very end
    - Each Round there is an increasing amount of money earned (100, 125, 150, etc.)

# How Enemies are Spawned
Calculate the Maximum number of enemies based on move speed, spawn rate, and distance of the level
Instantiate the enemies, & Pool based on type
Move the enemies to spawner when needed to spawn, and move back to pool position if killed or makes it to the end-

# Saving
Save player data 
    - Name
    - Levels Unlocked
    
If the player wants to save the current game save -
    - Level Type
    - Tower Positions (If there are towers)
    - Enemy Positions (If there are active enemies)
    - Round
    - Spawn Rate
    - Current Money
    - Current Health

# Future Updates 
    - More Enemies
    - More Towers
    - Customizer (Create custom Towers, and levels)


        