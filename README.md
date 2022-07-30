[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-f059dc9a6f8d3a56e377f745f24479a46679e63a5d9fe6f495e02850cd0d8118.svg)](https://classroom.github.com/online_ide?assignment_repo_id=450373&assignment_repo_type=GroupAssignmentRepo)


**The University of Melbourne**
# COMP30019 – Graphics and Interaction

Final Electronic Submission (project): **4pm, November 1**

Do not forget **One member** of your group must submit a text file to the LMS (Canvas) by the due date which includes the commit ID of your final submission.

You can add a link to your Gameplay Video here but you must have already submit it by **4pm, October 17**

# Project-2 README

You must modify this `README.md` that describes your application, specifically what it does, how to use it, and how you evaluated and improved it.

Remember that _"this document"_ should be `well written` and formatted **appropriately**. This is just an example of different formating tools available for you. For help with the format you can find a guide [here](https://docs.github.com/en/github/writing-on-github).


**Get ready to complete all the tasks:**

- [x] Read the handout for Project-2 carefully.

- [x] Brief explanation of the game.

- [x] How to use it (especially the user interface aspects).

- [x] How you designed objects and entities.

- [ ] How you handled the graphics pipeline and camera motion.

- [x] The procedural generation technique and/or algorithm used, including a high level description of the implementation details.

- [ ] Descriptions of how the custom shaders work (and which two should be marked).

- [ ] A description of the particle system you wish to be marked and how to locate it in your Unity project.

- [x] Description of the querying and observational methods used, including a description of the participants (how many, demographics), description of the methodology (which techniques did you use, what did you have participants do, how did you record the data), and feedback gathered.

- [x] Document the changes made to your game based on the information collected during the evaluation.

- [x] References and external resources that you used.

- [ ] A description of the contributions made by each member of the group.

## Table of contents
* [Team Members](#team-members)
* [Explanation of the game](#explanation-of-the-game)
* [How to use it](#how-to-use)
* [Technologies](#technologies)
* [Objects and entities](#objects-and-entities)
* [Procedural generation](#procedural-generation)
* [Query and observations](#queries)
* [Changes made based off feedback](#changes)
* [Using Images](#using-images)
* [Code Snipets ](#code-snippets)

## Team Members

| Name | Task | State |
| :---         |     :---:      |          ---: |
| Rui Guo | health && status     |  Finished |
|         | Random item drops    |  Finished |
|         | Block destruction    |  Finished |
|            | Barrier destruction Particle Effect|  Finished |
|            | Grenade Explosion Particle Effect|  Finished |
|            | Random Map generating && enemy wave generating|  Finished |
|         | Project Video        |  Finished |
|         | UI Assets        |  Finished |
|         | Play testing        |  Finished |
|         | Query and observation        |  Finished |
| Student Name 2| Shader      |  Testing |
| Ziqiang Li | player control && weapon shooting|  Finished |
|         | Block destruction    |  Finished |
|            | Random Map generating && enemy wave generating|  Finished |
|            | Particle Effect|  Finished |
|            | Grenade Throwing| Finished |
|            | Game Over Scene && Score Board | Finished |
| Yuanbo Xu    | heads-up display | Finished |
| 	       | Menu | Finished |
|              | Enemy and bosses | Finished |
|              | pause page| Finished |
|              | audio and sound effect| Finished |
|              | Transition | Finished |

## Explanation of the game
Our game is a third person top down view rougue like RPG. The player will be spawned into a randomly generated dungeon where monsters will try to hunt down the player, and the only way to survive is to traverse through the maze while defeating different monsters that will try to hunt you down. The player will be able to obtain more health, shields, bomb, and invunerable buffs from defeating monsters or destroying obstacles. As the player progress further into the game, more challenging monsters will spawn, for example, there will be monsters that shoots poison bullets at the player that ignores any shields the player has, monsters that can teleport to where the player is, and so on. 

## How to use it
The game has a very simple interface. The start menu has four options: Play, Settings, Credit and Quit. The Play button will start the game, Settings will show the settings page, Credit will show the credits of the game, and quit will exit the game. During the game, the player's combat informations are shown on the top left corner, this includes health, shield, ammo and bomb. On the top right corner, the score of the current run is shown. Hitting esc will stop the game, and a stop menu will be shown, which has three buttons, continue which will resume the game; Settings which will show the settings page; and quit will exit to start menu. The game uses a very intuitive keybinds: Shooting with left click, right click throws bombs, and movements are WASD. 

## Technologies
Project is created with:
* Unity 2021.1.13f1
* Ipsum version: 2.33
* Ament library version: 999

## Objects and entities
The game is designed around several important objects and entities. LivingObjects, Barriers, Maps and spawn. LivingObjects is the parent of all moving entities, this includes the player, and all enemies. All LivingObjects have a health controller attacted to them, which handles all health ahd armor related aspects of those entities. The player also has a statuscontroller, which allows the player to gain buffs and debuffs. Players and some enemies also has a GunController or GrenadeController, which allows the objects that has them to shoot projectiles. Enemies also have a dropcontroller, which allows random drop of items which would modify the player's stats upon defeating said enemies.

The map house all of the information regarding the visual layout of the level. It generates a random maze and populates the room according to that maze layout with barriers. Barriers also carry a health and drop controller. This allows blocks to be destructable and also able to drop items like enemies do.  

Spawn represents enemy waves. When player enters a room, the spawns decides which enemies to spawn in the room.

## Procedural generation
The game uses a procedurally generated maze as the main playground to host the player character and enemies. The maze generation uses depth first search. It first fills the level with walls, pick where the path needs to end up at by turning it into a path tile, and start at a given point, dig through all four directions, and recursively dig through until it digs to a tile that's not a wall. In that case, it would either be at the tile that was designated as the destination, or it would find itself pathing back to where it dug, which in that case the algorithm will halt. This precedure is then conducted on all four possible exits of the level, and to increase complexity of the maze, the algorithm is also called on the centre of the four quadrants of the room. 


## Query and observations
We conducted querying and observations through an online chat open question interview with a number of participants of the age group 19-28, most of them being gamers and a few not so familiar with gaming, or rogue like RPGs. Out of the total of 14 participants, 9 are based in Australia, with 5 based in China. The players are presented with the game with no verbal instructions, and would go through a cooperative evaluation of the game where the players would play through the game while being able to ask us questions and give feedback about the game which we would record down. 10 players complained upon launching the game, about the menu music being way to loud, and needing to turn down the music right away. Most players were able to get the hang of the controls almost immediately, with one player who doesn't play games a lot being confused as to wether she needed to use the mouse to move the character. 4 players commented about the visual instructions shown at the beginning of the game being very trivial. All players were able to get through the first level and very quickly learn the basic enemy movements and attack patterns, as well as traversing and destroying the maze. However 2 players were confused about the drop system of the game, as there are no visual indication of item drops as they are only reflected on the status bar. 6 players commented that the sudden music change upon entering a room feels very awkward. After having the players play the game for about 10 minutes, we asked them a few questions regarding the game. Every participant had different questions as we were able to observe their gameplay and answer their questions while they play. But some of the notable questions being the following: A player that's not familiar with rogue like RPGs questioned the motive of the game, and why there are seemingly no ending to the game. Players were asked about the difficulty of the game, and all but 2 players felt the early level were easy, the game gets much harder when long range enemies are spawned. of the 14 participants, only 5 players managed to get to and beat a level containing a boss. 2 of them felt the bosses were very easy, that the game needed to raise its difficulty, whereas the other 3 said the bosses are difficult and the difficuly could be tuned down a little. 

## Changes made based off feedback
Upon receiving the feedback, we conducted an group meeting to decide which feedbacks are of high importance to the gameplay experience. The first change we made was to the start menu music, by tuning the volume down and also changing the db range in the volume settings. We decided to make no changes to the visual control instructions as despite being very trivial, there clearly are players that aren't as familiar with gaming that could get hang up on it. Based off player's response about the game's difficulty, we deicded to maintain the current difficulty bottom line while increasing the difficulty cap in the late game. we moved bosses to later levels, and decreased enemy that would spawn along with long ranged enemies in the early game to allow players to maintain their character's health and getting more chances to familiar themselves with these enemies. 

## Using Images

You can use images/gif by adding them to a folder in your repo:

<p align="center">
  <img src="Gifs/Q1-1.gif"  width="300" >
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

## Code Snippets 

You can include a code snippet here, but make sure to explain it! 
Do not just copy all your code, only explain the important parts.

```c#
public class firstPersonController : MonoBehaviour
{
    //This function run once when Unity is in Play
     void Start ()
    {
      standMotion();
    }
}
```
## References

Finds the nearest point based on the NavMesh within a specified range, used in Spawn script:  

https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html

detecting hitting object using ray cast, learned from the tutorial on YouTube: 

https://www.youtube.com/watch?v=0jTPKz3ga4w

https://www.youtube.com/watch?v=cMp3kTyDmpw

Also documentation about ray casting: 

https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Physics.Raycast.html

Shuffle the list for enemy spawn, based on code from 

https://www.codegrepper.com/code-examples/csharp/shuffle+array+c%23

documentation on range impact and inside collision:

https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html





