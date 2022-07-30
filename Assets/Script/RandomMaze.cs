using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomMaze
{
    public bool[,] Maze { get; set; }
    public RandomMaze(int x, int y)
    {
        Maze = new bool[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Maze[i, j] = false;
            }
        }
        
        /*for (int i = 0; i < 3; i++) 
        {
            Maze[(int)(x/2+1),i] = true;
            Maze[(int)(x/2),i] = true;
            Maze[(int)(x/2-1),i] = true;
            Maze[(int)(x/2+1),y-1-i] = true;
            Maze[(int)(x/2),y-1-i] = true;
            Maze[(int)(x/2-1),y-1-i] = true;
            Maze[i,(int)(y/2+1)] = true;
            Maze[i,(int)(y/2)] = true;
            Maze[i,(int)(y/2-1)] = true;
            Maze[(int)(x-1-i),(int)(y/2+1)] = true;
            Maze[(int)(x-1-i),(int)(y/2)] = true;
            Maze[(int)(x-1-i),(int)(y/2-1)] = true;
        } */
        
    }

    public void BarrierMap(int x, int y)
    {
        if(Maze[x,y] == false) {
            int tempvalue = 0;
            if(x > 0 && x < (Maze.GetLength(0) - 1) && y > 0 && y < (Maze.GetLength(1) - 1)) {
                tempvalue += Convert.ToInt32(Maze[x+1,y]); 
                tempvalue += Convert.ToInt32(Maze[x-1,y]); 
                tempvalue += Convert.ToInt32(Maze[x,y+1]);
                tempvalue += Convert.ToInt32(Maze[x,y-1]); 
            }
            if( tempvalue <= 1 ) {
                Maze[x,y] = true;
                int[] direction = new int[4] {0,1,2,3};
                for (int i = 4; i>0; --i) {
                    int r = (UnityEngine.Random.Range(0,1000))%4;
                    int tempDirect = direction[r];
                    direction[r] = direction[i-1];
                    direction[i-1] = tempDirect;
                    switch (direction[i - 1]) {
                        case 0:
                            if(x > 0 && x < (Maze.GetLength(0) - 1) && y > 0 && y < (Maze.GetLength(1) - 1)) BarrierMap(x-1,y);
                            break;
                        case 1:
                            if(x > 0 && x < (Maze.GetLength(0) - 1) && y > 0 && y < (Maze.GetLength(1) - 1)) BarrierMap(x+1,y);
                            break;
                        case 2:
                            if(x > 0 && x < (Maze.GetLength(0) - 1) && y > 0 && y < (Maze.GetLength(1) - 1)) BarrierMap(x,y-1);
                            break;
                        case 3:
                            if(x > 0 && x < (Maze.GetLength(0) - 1) && y > 0 && y < (Maze.GetLength(1) - 1)) BarrierMap(x,y+1);
                            break;
                        default:
                            break;
                        
                    }
                }
            }
        }
    }


}
