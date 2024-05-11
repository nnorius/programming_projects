BEAR SURVIVAL\
\
**Controls**\
Toggle Dev Mode – F1\
Toggle Exit Game Menu – ESC\
Toggle Map - TAB\
Movement – WASD\
Jump – Space\
Run – Shift (held down while moving)\
Eat / Drink / Pickup – Left click\
Consume Magic Mushrooms – 1\
Consume Health Mushrooms – 2\
Consume Energy Mushrooms – 3\
\
**Game**\
You spawn on an island of bears surrounded by an ocean. Your goal is to get to the north west end of the island to a finish point. Anytime health, hunger, or thirst reaches 0 the player is respawned back at a random point. 
-	Anytime the player enters the ocean they steadily lose health until they leave the ocean 
 
**Finish**
-	There is an arch before the finish point that you cannot pass through unless you have collected 5 of each of the 3 rocks the bears drop
-	The finish point is marked by a particle effect that once you walk through takes you back to the main menu
  
**Player** 
-	At start of game spawns to randomly selected position


![Bear-Survival 5_11_2024 5_45_19 PM](https://github.com/nnorius/programming_projects/assets/128853412/bb948d09-7644-498d-9012-e2ba0b020f2b)

Map
- There is a mini-map in the right top corner of the player overlay
- The player can also toggle a large map of the entire island\

![Bear-Survival 5_11_2024 5_48_02 PM](https://github.com/nnorius/programming_projects/assets/128853412/6b8f6c6f-b47a-4670-b94d-6b0e5799f840)

  
Status Bar

-	Health
    - Health does not recharge and if it reaches 0 player randomly respawns

-	Hunger
    -	Depletes over time
    -	Food symbol appears on screen when hovering over edible mushrooms
    -	Left click eats plant (destroys gameobject) and replenishes health

-	Thirst
    -	Depletes over time
    -	Can only drink from fresh water (not ocean)
    -	A waterdrop symbol will appear when hovering over fresh water
    -	Left click to drink and replenish thirst

-	Exhaustion
    -	Depletes while running
    -	Once zero can’t run
    -	Starts replenishing after 5 second cooldown

**Brown Bear**\
![image](https://github.com/nnorius/programming_projects/assets/128853412/381788c7-73d5-48c7-86a4-6893217a1b6f)

-	Spawns pink rocks when attacking\
  ![image](https://github.com/nnorius/programming_projects/assets/128853412/ba4f0faa-b2ad-4e37-8616-c28c212b8195)

  
Idle State

-	Idles in one position
-	If sees player move to Hostile State
  
Natural State

-	Chooses randomly between three actions: 
    -	Wander: Choose a random point on Navmesh within radius and go to it
    -	Find water: Randomly choose water from an array of fresh water sources and go to it. Once sense water on ground stop and drink.
    -	Idle: idle in place for certain amount of seconds
-	Higher probability of choosing wander than find water or idle
-	If see player at any point move to hostile state
  
Hostile State

-	Charges player
-	Once player in range select one random attack: Swipe Left, Swipe Right, or Bite
-	10 damage if player hit
-	After attack bear circles away and then charges player again
-	If it cannot see player it will go to the last place it saw the player, then give up and move to Natural State if it still doesn’t see player

**Black Bear**\
![image](https://github.com/nnorius/programming_projects/assets/128853412/f6df156e-4d89-4cc9-ba9c-6c946c5ea144)

-	Can hear player if they run within 100 units 
-	Spawns red rocks when attack is triggered\
![image](https://github.com/nnorius/programming_projects/assets/128853412/354c94bf-4ac1-4fe1-abcc-7716c8b5b10f)

  
Idle State

-	Idles in one position
-	If sees player move to Hostile State
  
Hostile State

-	Will charge straight for player
-	Once player in range has lunge attack where it rears back and then pounds both front feet into the ground in front
-	20 damage if hits player. The player has 0.3 seconds to move out of box collider after bear attack is triggered or will get hit.
-	After lunging the bear roars and then stands still for a few seconds to cooldown
-	It will charge for the player position after attack cooldown but if it doesn’t see the player by the time it gets there will move to natural state
  
Natural State

-	Chooses randomly between three actions: 
    -	Wander: Choose a random point on Navmesh within radius and go to it
    -	Find water: Randomly choose water from an array of fresh water sources and go to it. Once sense water on ground stop and drink.
    -	Idle: idle in place for certain amount of seconds
-	Higher probability of choosing wander than find water or idle
-	If see player at any point move to hostile state
-	If hear player run at any point charges towards position where they heard the player and move to hostile state
  
**White Bear**\
![image](https://github.com/nnorius/programming_projects/assets/128853412/d6b04ee3-d4bd-44f2-8af7-99773784c073)

-	Spawns a blue rock when goes to sleep, so need to get the bear to move to retrieve it\
  ![image](https://github.com/nnorius/programming_projects/assets/128853412/3c0d4374-352e-4166-8f43-e9ca50a77617)

-	This bear has keen senses and will sense the player within a certain radius even if they are behind an object
-	The sense radius is bigger when the bear is not sleeping
-	This bear is slow and never runs
  
Sleep State

-	Bear always starts out sleeping
-	It will sense the player within a certain radius and wake up moving to Hostile State
  
Hostile State

-	The bear’s sense radius expands
-	The bear will follow the player at a walk if within sense radius even behind objects
-	Every few seconds it will shoot a light beam forward
-	If the beam hits the player it freezes the player for 5 seconds
-	If the bear gets close enough to touch the player the player is respawned at a random point. Stats are not affected.
-	If the bear cannot sense the player it will move to Natural State
  
Natural State

-	The bear will follow a behavior sequence of three actions
    1. Wander to one random position
    2. Idle for 10 seconds
    3. Fall back to sleep (Move to Sleep State, sense radius contracts)
-	If at any time it senses the player again during this sequence it will move back to Hostile State
  
**Flora**\
MUSHROOMS\
 Food – replenish from hunger\
 ![image](https://github.com/nnorius/programming_projects/assets/128853412/091e9eab-2d65-431f-b941-e977f5977438)

Inventory mushrooms – often located at base of Bonsai Trees 
- Number in inventory shows up in top left of player screen
  
Magic – Hit 1 to consume. Makes you undetectable and invisible to bears for 6 seconds
-	Screen has a blue tint while they are active\
 ![image](https://github.com/nnorius/programming_projects/assets/128853412/c8ea6b48-ce9e-4c72-a0c1-e5d34366a223)
  
Replenish Health – Hit 2 to consume\
 ![image](https://github.com/nnorius/programming_projects/assets/128853412/ef1a5316-e74f-430e-a4cc-0b45c9157714)

Restore full energy – Hit 3 to consume\
 ![image](https://github.com/nnorius/programming_projects/assets/128853412/79f6bf24-ae06-4ed6-baf8-1a9bb4666af6)

TREES\
Bonsai – look for inventory mushrooms at base\
 ![image](https://github.com/nnorius/programming_projects/assets/128853412/ef92d744-45d1-4cbc-a491-975a381ad9d5)


Oak – only grows near water\
 ![image](https://github.com/nnorius/programming_projects/assets/128853412/58aae5e7-7c41-4e98-b975-87b2a76b1e2c)



