using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; 
    private Enemy currentEnemy;
    [SerializeField]
    private SceneController sceneController;
    public GameObject spownPt;
    private void Start()
    {
        SpawnAndInitializeEnemy();
    }
     void Update()
    {
        if (currentEnemy.Health <=0) 
        {
            sceneController.LoadScene("City");
        }
    }
    private void SpawnAndInitializeEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab, spownPt.transform.position, Quaternion.identity);

        currentEnemy = enemyObj.GetComponent<Enemy>();

        if (currentEnemy == null)
        {
            currentEnemy = enemyObj.AddComponent<Enemy>();
        }

        string enemyName = "Enemy_" + Random.Range(1, 1000);

        currentEnemy.InitializeEnemy(enemyName);

        currentEnemy.DisplayEnemyInfo();
    }
}
