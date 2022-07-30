using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGeneration : MonoBehaviour
{
    [Header("====Map Setting====")]
    public float xCoordinate;
    public float yCoordinate;
    [Range(0,1)]
    public float outlineRatio;
    public GameObject[,] floorPositions;
    [Header("====Barrier Setting====")]
    public Color initialColor;
    public Color endColor;

    public GameObject barrierPrefb;
    public GameObject floorGameObject;

    GameObject currentBarriers;
    public GameObject[] doorGameObjects;
	public GameObject[] doorTriggers;
    GameObject doorTop;
    GameObject doorBottom;
    GameObject doorLeft;
    GameObject doorRight;
    GameObject triggerTop;
    GameObject triggerBottom;
    GameObject triggerLeft;
    GameObject triggerRight;
    Spawn spawn;
    void Awake()
    {
        floorPositions = new GameObject[(int)yCoordinate, (int)xCoordinate];
		spawn = FindObjectOfType<Spawn> ();
    }

    void Start()
    {
        ChangeMap();
        ChangeBarrier();
    }

    public void ChangeMap()
    {
        for (int i = 0; i < xCoordinate; i++)
        {
            for (int j = 0; j < yCoordinate; j++)
            {
                Vector3 floorPosition = new Vector3(-xCoordinate / 2 + 0.5f + i, 0, -yCoordinate/2+0.5f+j);
                GameObject generatedFloor = Instantiate(floorGameObject);
                generatedFloor.transform.SetParent(this.transform.GetChild(0).transform);
                generatedFloor.transform.localPosition = floorPosition;
                generatedFloor.transform.localScale = new Vector3(1 - outlineRatio, 1 - outlineRatio, 1 - outlineRatio);
                generatedFloor.GetComponent<Floor>().FloorPos = floorPosition;
                floorPositions[(int)Mathf.Abs(-(int)Math.Round(-floorPosition.z) + (int)(yCoordinate/2)), -(int)Math.Round(-floorPosition.x) + (int)(xCoordinate/2)] = generatedFloor;
            }
        }
    }
    public void ChangeBarrier()
    {
        Vector3 bottomPositionTrigger = new Vector3(0,0,-yCoordinate/2-1.58f);
		Vector3 rightPositionTrigger = new Vector3(xCoordinate/2+1.5f,0,0);
		Vector3 topPositionTrigger = new Vector3(0,0,yCoordinate/2+1.5f);
		Vector3 leftPositionTrigger = new Vector3(-xCoordinate/2-1.5f,0,0);

        triggerBottom = Instantiate(doorTriggers[0],bottomPositionTrigger,Quaternion.identity);
		triggerTop = Instantiate(doorTriggers[1],topPositionTrigger,Quaternion.identity);
		triggerLeft = Instantiate(doorTriggers[2],leftPositionTrigger,Quaternion.identity);
		triggerRight = Instantiate(doorTriggers[3],rightPositionTrigger,Quaternion.identity);

        Vector3 bottomPosition = new Vector3(0,0,-yCoordinate/2-1);
		Vector3 rightPosition = new Vector3(xCoordinate/2,0,0);
		Vector3 topPosition = new Vector3(0,0,yCoordinate/2);
		Vector3 leftPosition = new Vector3(-xCoordinate/2-1,0,0);

		doorBottom = Instantiate(doorGameObjects[0],bottomPosition,Quaternion.identity); //bottom
		doorTop =  Instantiate(doorGameObjects[1],topPosition,Quaternion.identity); //top
		doorLeft = Instantiate(doorGameObjects[2],leftPosition,Quaternion.Euler (Vector3.up * 90));//left
		doorRight = Instantiate(doorGameObjects[3],rightPosition,Quaternion.Euler (Vector3.up * 90)); //right

        RandomMaze newMaze = new RandomMaze((int)yCoordinate, (int)xCoordinate);
        newMaze.Maze[(int)(yCoordinate / 2), (int)(xCoordinate/ 2 )] = true;
        newMaze.BarrierMap((int)(yCoordinate / 2),1);
        newMaze.BarrierMap((int)(yCoordinate / 2),(int)(xCoordinate-2 ));
        newMaze.BarrierMap((int)(1),(int)(xCoordinate/ 2 ));
        newMaze.BarrierMap((int)(yCoordinate -2),(int)(xCoordinate/ 2 ));
        newMaze.Maze[(int)(yCoordinate / 2), (int)(xCoordinate/ 2 )] = false;
        newMaze.BarrierMap((int)(yCoordinate / 2),(int)(xCoordinate/ 2 ));
        newMaze.BarrierMap((int)(yCoordinate / 4),(int)(xCoordinate/ 4 * 3));
        newMaze.BarrierMap((int)(yCoordinate / 4 * 3),(int)(xCoordinate/ 4 * 3 ));
        newMaze.BarrierMap((int)(yCoordinate / 4),(int)(xCoordinate/ 4 ));
        newMaze.BarrierMap((int)(yCoordinate / 4 * 3),(int)(xCoordinate/ 4 ));

    
        for (int i = 0; i < 3; i++) 
        {
            newMaze.Maze[(int)(yCoordinate /2 -1 + i), (int)(xCoordinate/ 2 + 1)] = true;
            newMaze.Maze[(int)(yCoordinate /2 -1 + i) , (int)(xCoordinate/ 2 )] = true;
            newMaze.Maze[(int)(yCoordinate /2 -1 + i), (int)(xCoordinate/ 2 - 1)] = true;

            newMaze.Maze[(int)(yCoordinate / 2+1),i] = true;
            newMaze.Maze[(int)(yCoordinate / 2),i] = true;
            newMaze.Maze[(int)(yCoordinate / 2-1),i] = true;
            newMaze.Maze[(int)(yCoordinate / 2+1),(int)(xCoordinate-1-i)] = true;
            newMaze.Maze[(int)(yCoordinate / 2),(int)(xCoordinate-1-i)] = true;
            newMaze.Maze[(int)(yCoordinate / 2-1),(int)(xCoordinate-1-i)] = true;
            newMaze.Maze[i,(int)(xCoordinate/2+1)] = true;
            newMaze.Maze[i,(int)(xCoordinate/2)] = true;
            newMaze.Maze[i,(int)(xCoordinate/2-1)] = true;
            newMaze.Maze[(int)(yCoordinate-1-i),(int)(xCoordinate/2+1)] = true;
            newMaze.Maze[(int)(yCoordinate-1-i),(int)(xCoordinate/2)] = true;
            newMaze.Maze[(int)(yCoordinate-1-i),(int)(xCoordinate/2-1)] = true;
        }

        
        GameObject parentBarrier =  Instantiate(barrierPrefb, this.transform);
        parentBarrier.transform.localPosition -= new Vector3(0, 0.8f, 0);
        currentBarriers = parentBarrier;

        for (int i = 0; i < yCoordinate; i++)
        {
            for (int j = 0; j < xCoordinate; j++)
            {
                if(newMaze.Maze[i,j]==false)
                {
                    GameObject generatedBarrier = Instantiate(barrierPrefb);
                    generatedBarrier.transform.SetParent(parentBarrier.transform);
                    float randomHeight = (float)UnityEngine.Random.Range(0, 5) / 10;
                    Vector3 barrierPosition = floorPositions[i, j].GetComponent<Floor>().FloorPos;
                    generatedBarrier.transform.localPosition = barrierPosition;
                    generatedBarrier.transform.localScale = new Vector3(1, 1, 1);
                    generatedBarrier.GetComponent<Barrier>().SetBarrier(randomHeight,1);
                    float colorRatio;
                    colorRatio =j/xCoordinate;
                    MeshRenderer barrierRenderer = generatedBarrier.GetComponent<MeshRenderer>();
                    barrierRenderer.material.color = Color.Lerp(initialColor, endColor, colorRatio);
                }
            }
        }

        GenerateHorizontalWall(-yCoordinate/2-1, parentBarrier.transform);//bottom
        GenerateHorizontalWall(yCoordinate/2+0.5f, parentBarrier.transform); //top
        GenerateVerticalWall(-xCoordinate/2-0.7f,parentBarrier.transform);
        GenerateVerticalWall(xCoordinate/2+0.2f,parentBarrier.transform);
		parentBarrier.transform.position = new Vector3(0, 0.8f, 0);

    }

    void GenerateHorizontalWall(float zPosition, Transform parentPosition) {
        for (int i = (int)-xCoordinate/2;i<xCoordinate/2;i++) {
            if (!(i<2&&i>=-2)) {
                GameObject generatedWall = Instantiate(barrierPrefb);
                generatedWall.transform.SetParent(parentPosition);
                float wallHeight = -0.5f;
                Vector3 wallPosition = new Vector3(i,0,zPosition);
                generatedWall.transform.localPosition = wallPosition;
                generatedWall.transform.localScale = new Vector3(1,1,1.4f);
                generatedWall.GetComponent<Barrier>().SetBarrier(wallHeight,2);
                float colorRatio;
                colorRatio =i/xCoordinate;
                MeshRenderer wallRenderer = generatedWall.GetComponent<MeshRenderer>();
                wallRenderer.material.color = Color.Lerp(initialColor, endColor, colorRatio);
           }   
        }
    }

    void GenerateVerticalWall(float xPosition,Transform parentPosition) {

        for (int i = (int)-yCoordinate/2;i<yCoordinate/2;i++) {
            if (!(i<3&&i>-2)) {
                GameObject generatedWall = Instantiate(barrierPrefb);
                generatedWall.transform.SetParent(parentPosition);
                float wallHeight = -0.5f;
                Vector3 wallPosition = new Vector3(xPosition,0,i);
                generatedWall.transform.localPosition = wallPosition;
                generatedWall.transform.localScale = new Vector3(1,1,1f);
                generatedWall.GetComponent<Barrier>().SetBarrier(wallHeight,2);
                float colorRatio;
                colorRatio =i/xCoordinate;
                MeshRenderer wallRenderer = generatedWall.GetComponent<MeshRenderer>();
                wallRenderer.material.color = Color.Lerp(initialColor, endColor, colorRatio);
           }   
        }

    }
    public void newBarrier()
    {
        StartCoroutine(generateNewBarrier());
    }
    IEnumerator generateNewBarrier()
    {
        currentBarriers.transform.position = new Vector3(0, 0.8f, 0);
        yield return new WaitForSeconds(1f);
        Destroy(currentBarriers.gameObject);
        Destroy(doorBottom.gameObject);
        Destroy(doorTop.gameObject);
        Destroy(doorLeft.gameObject);
        Destroy(doorRight.gameObject);
        Destroy(triggerBottom);
        Destroy(triggerLeft);
        Destroy(triggerRight);
        Destroy(triggerTop);
        if (spawn.GetCurrentWaveNumber()+1!=spawn.waves.Count) {
            ChangeBarrier();
        }
    }

}


