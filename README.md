Final Project Demonstration Link : https://youtu.be/6zXWBLdGgMA

[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/ZUtYscbQ)

# Arcane Maze Game Design

## 1. Game Overview

**Project Title:** Arcane Maze  

**Concept:** Arcane Maze is a 3D action-adventure game set in a mysterious labyrinth where the player battles elemental monsters. Each defeated monster drops elemental energy that can be collected and used as ammunition for ranged attacks. The player must navigate the labyrinth, defeat enemies, and strategically manage their elemental energy bars to progress through increasingly challenging stages.

**Setting:** The player finds themselves trapped in an ancient, shifting maze filled with monsters that embody the elements of fire, water, earth, air, and lightning. To survive and escape, the player must master the energy left behind by these monsters to fuel their elemental powers.

**Main Features:**
- **Elemental Energy System:** Defeating monsters of different elements grants the player energy that fuels their ranged attacks.
- **Nature Spirits:** Each element is guarded by nature spirits that provide energy replenishment. For fire, water, earth, and air, players can interact peacefully to collect energy. However, for lightning, players must defeat the moving nature spirit to gain a full energy bar.
- **Strategic Resource Management:** The player has four elemental energy bars (fire, water, earth, air) and one ultimate energy bar (lightning), each influencing combat tactics.
- **Dynamic Mazes:** The labyrinth shifts after every stage, introducing new puzzles, enemies, and obstacles.
- **Procedural Power-Ups:** New abilities and elemental powers are unlocked after completing each stage.

## 2. Gameplay Mechanics

**Player Character:**
- **Role:** The player is an elemental manipulator trapped in the maze, capable of wielding the elemental powers of fire, water, earth, air, and lightning.
- **Abilities:**
  - **Elemental Shot:** Ranged attacks fueled by elemental energy (fire, water, earth, air) collected from defeated enemies.
  - **Ultimate Ability:** Lightning shot, a powerful but rare ability that can only be activated when the lightning energy bar is full.

**Energy System:**
- **Fire, Water, Earth, Air:** Each element has its own energy bar. When a player defeats an elemental monster, it drops a corresponding energy orb. By collecting these orbs, the player replenishes their energy for that element.
- **Nature Spirits:**
  - **Fire, Water, Earth, Air:** Each element has a nature spirit that appears in the maze. Players can interact with these spirits to absorb energy and replenish their elemental energy bars peacefully.
  - **Lightning:** The nature spirit for lightning is elusive and moves throughout the maze. Players must track and defeat this spirit to gain a full charge for their lightning energy bar.

**Recharge Mechanics:**
- **Nature Spirits:** Each stage features nature spirits for each element. Players can approach and interact with these spirits to collect energy for fire, water, earth, and air. For lightning, players must defeat the moving spirit.

**Enemies:**
- **Elemental Monsters:** Each enemy has elemental affinities (fire, water, earth, air) and drops corresponding energy upon defeat.
- **Lightning Monsters:** Rare enemies with lightning powers that drop lightning energy, needed to charge the ultimate ability.

## 3. Interactive Graphics Techniques

### 3.1 Shaders
**Elemental Effects:**
- **Fire, Water, Earth, Air:** Custom shaders are used to create distinct visual effects for each element.
  - **Fire:** Bright, glowing flames with flickering particle trails.
  - **Water:** Reflective and refractive surfaces with ripple effects.
  - **Earth:** Rough textures and dust particles for a grounded feel.
  - **Air:** Soft, transparent motion trails for fast, fluid movement.
- **Lightning:** A special shader creates bright, flashing bolts of energy with high contrast and bloom for the ultimate ability.

### 3.2 Geometry
**Maze Design:**
- **Procedural Geometry:** The labyrinth’s walls, floors, and obstacles are procedurally generated to create unique layouts for each stage.
- **Dynamic Obstacles:** Certain parts of the maze may shift or change as the player interacts with elemental energy, requiring creative use of abilities to progress.

### 3.3 Lighting
**Elemental Lighting:**
- **Dynamic Lighting:** The player’s elemental attacks emit light, casting dynamic shadows that change based on the type of attack (e.g., fire casts a warm, flickering glow, while lightning creates intense flashes).
- **Environment-Specific Lighting:** Each stage has lighting conditions that reflect its elemental theme (e.g., dark, shadowy mazes for earth, bright, airy environments for wind).

### 3.4 Animation
**Player and Monster Interactions:**
- **Energy Absorption:** When the player collects elemental orbs or interacts with nature spirits, an animated energy flow visualizes the transfer of power to the player’s elemental bars.
- **Elemental Attacks:** Projectiles and abilities are animated with particle systems to reflect the unique nature of each element.

## 4. Conclusion
Arcane Maze combines fast-paced elemental combat with strategic energy management, all set in a procedurally generated labyrinth. By utilizing interactive graphics techniques such as shaders, dynamic lighting, and procedural geometry, the game creates an immersive experience that challenges players both visually and tactically.
