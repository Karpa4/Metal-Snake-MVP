using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services.Pause;

public class EnemySpawner : MonoBehaviour, IPause
{
    [SerializeField] private float minStartTimeValue = 20f;
    [SerializeField] private float maxStartTimeValue = 30f;
    [SerializeField] private float minTimeValue = 15f;
    [SerializeField] private float maxTimeValue = 25f;
    [SerializeField] private float greedRate = 0.9f;
    [SerializeField] private float timeToHordeAttack = 80f;
    [SerializeField] private CarriageManager carriageManager;
    [SerializeField] private Transform mainPlayer;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource source;

    [SerializeField] private CarDriverAI bearPrefab;
    [SerializeField] private CarDriverAI porcupinePrefab;
    [SerializeField] private CarDriverAI hyenaPrefab;

    [SerializeField] private Collider2D potentialSpawnZone;


    private Vector3 defaultDistanceToPlayer;
    private PauseManager pauseManager;
    private List<CarDriverAI> enemies; 
    private LayerMask obstacleMask;
    private LayerMask enemyMask;

    private float pauseTime = 0;
    private float hordeRate;
    private float nextAttackTime;
    private float gameSessionTime;
    private bool isPause = false;

    private void Awake()
    {
        obstacleMask = LayerMask.GetMask("Obstacle");
        enemyMask = LayerMask.GetMask("Enemy");
    }

    /// <summary>
    /// Первая волна противников
    /// </summary>
    private void FirstEnemySpawn()
    {
        nextAttackTime = Random.Range(minStartTimeValue, maxStartTimeValue);
        StartCoroutine(EnemySpawnTimer());
    }

    /// <summary>
    /// Запуск таймера волн противников
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemySpawnTimer()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(nextAttackTime);
            if (isPause || pauseTime > 0)
            {
                nextAttackTime = pauseTime;
                pauseTime = 0;
            }
            else
            {

                SpawnEnemies();
                nextAttackTime = Random.Range(minTimeValue, maxTimeValue) * Mathf.Pow(greedRate, carriageManager.GetCarriagesCount() + hordeRate);
                CheckHordeRate();
                Debug.Log("next wave in" + nextAttackTime);
            }
        }
    }

    /// <summary>
    /// Система выбора противников
    /// </summary>
    /// <returns></returns>
    private List<CarDriverAI> EnemyChooseSystem()
    {
        List<CarDriverAI> currentEnemies = new List<CarDriverAI>();
        int r = Random.Range(1, 7); 

        switch (r) 
        {
            case 1:
                currentEnemies.Add(bearPrefab);
                break;
            case 2:
                currentEnemies.Add(bearPrefab);
                currentEnemies.Add(porcupinePrefab);
                break;
            case 3:
                currentEnemies.Add(hyenaPrefab);
                currentEnemies.Add(porcupinePrefab);
                break;
            case 4:
                currentEnemies.Add(hyenaPrefab);
                currentEnemies.Add(hyenaPrefab);
                currentEnemies.Add(hyenaPrefab);
                break;
            case 5:
                currentEnemies.Add(hyenaPrefab);
                currentEnemies.Add(hyenaPrefab);
                break;
            default:
                currentEnemies.Add(porcupinePrefab);
                currentEnemies.Add(porcupinePrefab);
                break;

        }

        return currentEnemies;
    }

    /// <summary>
    /// Проверка места спавна противника
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool CheckSpawnPosition(Vector2 position)
    {
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(position, 3f, obstacleMask);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(position, 2f, enemyMask);
        if (obstacles.Length + enemies.Length == 0)
            return true;

        else
            return false;
    }

    /// <summary>
    /// Генерация противников
    /// </summary>
    /// <param name="enemies"></param>
    private void SpawnEnemies()
    {
        Vector3 distance;
        enemies = EnemyChooseSystem();
        defaultDistanceToPlayer = mainPlayer.position + new Vector3(0, -15f, 0); 
        CarDriverAI newEnemy;

        for (int i = 0; i < enemies.Count; i++)
        {

            if (!CheckSpawnPosition(defaultDistanceToPlayer))
            {
                distance = CalculatePointToSpawn();
            }

            else
            {
                distance = defaultDistanceToPlayer;
            }

            if (distance != Vector3.zero)
            {
                newEnemy = Instantiate(enemies[i], distance, Quaternion.identity);
                newEnemy.SetTargetTransform(mainPlayer);
                newEnemy.GetComponent<CarDriver>().SetMaxSpeed();
            }

            else
            {
                Debug.Log("Enemies can't be spawn");
            }
        }

        audioManager.PlaySound(AudioManager.Sound.Honk, source); // Звук гудка
    }

    /// <summary>
    /// Увеличение коэффициента орды при выполнении условия
    /// </summary>
    private void CheckHordeRate()
        {
            if (gameSessionTime > timeToHordeAttack)
                hordeRate++;
        }

        void Start()
        {
            pauseManager = RefContainer.Instance.MainPauseManager;
            pauseManager.Register(this);
            FirstEnemySpawn();
        }

        private void Update()
        {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnEnemies();

            if (isPause)
            {
                pauseTime += Time.deltaTime;
            }
            else
            {
                gameSessionTime += Time.deltaTime;
            }

        }

        public void PauseOn()
        {
            isPause = true;
        }

        public void PauseOff()
        {
            isPause = false;
        }
    
   
    private Vector3 CalculatePointToSpawn() 
    {
        Vector3 resultPosition = Vector3.zero;
        Vector3 positionToCheck;
     
        float max_x = potentialSpawnZone.bounds.max.x;
        float min_x = potentialSpawnZone.bounds.min.x;
        float max_y = potentialSpawnZone.bounds.max.y;
        float min_y = potentialSpawnZone.bounds.min.y;

        for (float y = min_y; y < max_y; y++)
        {
            for (float x = max_x; x > min_x; x--)
            {
                positionToCheck = new Vector3(x, y);
                if (CheckSpawnPosition(positionToCheck) == true)
                {
                    resultPosition = positionToCheck;
                    y = max_y;
                    break;
                }    
            }
        }
        return resultPosition;
    }



}
