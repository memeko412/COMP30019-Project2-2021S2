using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Spawn : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    LivingObject player;
    Transform playerTransform;
    MapGeneration map;
    Wave currentWave;
    int currentWaveIndex =0;
    float nextEnemySpawn;
    private List<Enemy> currentEnemys = new List<Enemy>();

    public UnityEvent OnNextWave = new UnityEvent();
    public UnityEvent OnPlayerWon;
    public UnityAction NextWaveDelay;

    bool can_spawn =false;
    bool doorOpened = false;
    int whichDoorOpened = 0;
    void Start()
    {
        map = FindObjectOfType<MapGeneration> ();
        player = FindObjectOfType<Player> ();
        playerTransform = player.transform;
        SetInitialPosition(); 
        StartCoroutine(DelayNextWave(FirstWave));
        FindObjectOfType<AudioManager>().PlayBGM();
    }

    void Update()
    {
        if (currentWave != null)
        {
            //spawn next enemy in current wave
            if (currentWave.allEnemys.Count > 0 && Time.time > nextEnemySpawn && can_spawn)
            {
                SpawnOneEnemy();
            }
            //call next wave
            if (currentEnemys.Count <= 0 && currentWave.allEnemys.Count <= 0 && currentWaveIndex + 1 < waves.Count && doorOpened)   
            {
                can_spawn = false;
                OnNextWave.Invoke();
                currentWave = waves[++currentWaveIndex];
                doorOpened = false;
                currentWave.SetEnemy();
                StartCoroutine(DelayNextWave(NextWave));
            }
            if (currentEnemys.Count <= 0 && currentWave.allEnemys.Count <= 0 && currentWaveIndex + 1 == waves.Count) {
                this.OnPlayerWon.Invoke();
            }
        }
        
    }

    private void NextWave()
    {
        FindObjectOfType<AudioManager>().PlayBGM();
        can_spawn = true;
        if (whichDoorOpened == 1) {
            SetRightPosition();
        }
        else if (whichDoorOpened == 2) {
            SetLeftPosition();
        }
        else if (whichDoorOpened == 3) {
            SetBottomPosition();
        }
        else if (whichDoorOpened == 4) {
            SetTopPosition();
        }

    }
    public void RemoveDiedEnemy(Enemy enemy)
    {
        if(currentEnemys.Contains(enemy))
        currentEnemys.Remove(enemy);
    }
    public Vector3 GetRandomPosition()
    {
        Vector3 newPos = UnityEngine.Random.insideUnitSphere * 100;
        int posX = UnityEngine.Random.Range(0, 101);
        int posZ = UnityEngine.Random.Range(0, 101);

        newPos += this.transform.position; //new Vector3(posX, 0, posZ);
        NavMeshHit hit;
       if(!NavMesh.SamplePosition(newPos, out hit, 100, 1))
        {
            GetRandomPosition();
        }
        return hit.position;
    }
    public void SpawnOneEnemy()
    {
        Enemy newEenemy = Instantiate(currentWave.allEnemys.Dequeue());
        currentEnemys.Add(newEenemy);
        newEenemy.onEnemyDie.AddListener(RemoveDiedEnemy);
        newEenemy.transform.position = GetRandomPosition();
        nextEnemySpawn = currentWave.waitTime+Time.time;
        doorOpened = false;
    }
    IEnumerator DelayNextWave(UnityAction todoAction)
    {
        yield return new WaitForSeconds(1f);
        todoAction.Invoke();
    }
    public void FirstWave()
    {
        currentWave = waves[currentWaveIndex];
        currentWave.SetEnemy();
        SpawnOneEnemy();
        can_spawn = true;
    }

    public bool GetDoorIsOpen() {
		return doorOpened;
	}
	public void SetDoorIsOpen(bool isOpen) {
		this.doorOpened = isOpen;
	}

	public int GetCurrentWaveNumber(){
		return currentWaveIndex;
	} 

	public void SetCurrentWaveNumber(int waveIndex) {
		this.currentWaveIndex = waveIndex;
	}

	public void SetWhichDoorIsOpen(int doorNum) {
		this.whichDoorOpened = doorNum;
	}

	public Wave GetCurrentWave() {
		return currentWave;
	}
    void SetRightPosition() {
		playerTransform.position = new Vector3(map.xCoordinate/2-1.2f,0,0);
	}

	void SetInitialPosition() {
		playerTransform.position = new Vector3(0,2,0);
	}

	void SetLeftPosition() {
		playerTransform.position = new Vector3(-1.0f*map.xCoordinate/2,0,0);
	}

    void SetTopPosition() {
		playerTransform.position = new Vector3(0,0,map.yCoordinate/2-1);
	}

    void SetBottomPosition() {
		playerTransform.position = new Vector3(0,0,-1.0f*map.yCoordinate/2);
	}


}
[System.Serializable]
public class Wave
{
    public Queue<Enemy> allEnemys = new Queue<Enemy>();
    public List<enemySetUp> enemys = new List<enemySetUp>();
    public float waitTime;

    public void SetEnemy()
    {
        foreach(enemySetUp enemySetUp in enemys)
        {
            for (int i = 0; i < enemySetUp.enemyNumber; i++)
            {
                allEnemys.Enqueue(enemySetUp.enemyPrefab);
            }
        }
        allEnemys = new Queue<Enemy>(Shuffle<Enemy>.ShuffleTheList(allEnemys.ToArray()));
    }
}
[System.Serializable]
public struct enemySetUp
{
    public Enemy enemyPrefab;
    public int enemyNumber;
}