using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Collider l� khu v?c t?o ra c�c tr�i c�y
    private Collider spawnArea;
    //List l?u danh s�ch c�c Prefabs Fruit
    public GameObject[] fruitPrefabs;
    //Game Object l?u Prefabs Bomb
    public GameObject bombPrefab;
    //T? l? qu? Bomb sinh ra trong kho?ng 0 ??n 1.
    [Range(0f, 1f)]
    public float bombChance = 0.05f;
    //Kho?ng th?i gian nhanh v� ch?m nh?t ?? sinh ra Fruit
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    //Kho?ng g�c ?? thay ??i ?? quay tr?c z c?a Fruit khi sinh ra
    public float minAngle = -15f;
    public float maxAngle = 15f;
    //L?c b?n l�n th?p v� l?n nh?t c?a m?i Fruit khi sinh ra
    public float minForce = 18f;
    public float maxForce = 22f;
    //Th?i gian t?i ?a c?a 1 Fruit t?n t?i
    public float maxLifeTime = 5f;

    private void Awake()
    {
        //L?y Collider c?a Game Object
        spawnArea = GetComponent<Collider>();
    }
    //Khi Spawner.cs Enable s? li�n t?c g?i h�m Spawn() theo kho?ng th?i gian nh?t ??nh
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    //Khi Spawner.cs disable s? d?ng h?t h�m Coroutines ?ang ch?y
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        //??i 2 gi�y tr??c khi b?t ??u Spawn
        yield return new WaitForSeconds(2f);
        
        while (enabled)
        {
            //L?y ng?u nhi�n 1 Prefab trong list Prefabs Fruit ?� truy?n v�o
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            //Random t? l? ?? sinh Bomb
            if(Random.value < bombChance)
            {
                prefab = bombPrefab;
            }
            //L?y v? tr� ng?u nhi�n n?m tr�n khu v?c sinh ra Fruit
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            //L?y ?? quay ng?u nhi�n b?ng tr?c z c?a Fruit khi sinh ra
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));
            //T?o 1 b?n sao Game Object Fruit tr�n Scene, v?i prefab, v? tr�, ?? quay ?� c�
            GameObject fruit = Instantiate(prefab, position, rotation);
            //Xo� b?n sao Fruit ?� t?o trong kho?ng th?i gian maxLifeTime
            Destroy(fruit, maxLifeTime);
            //L?y ng?u nhi�n l?c b?n l�n c?a Fruit khi sinh ra
            float force = Random.Range(minForce, maxForce);
            //L?y RigidBody c?a fruit v� th�m l?c t�c ??ng b?ng AddForce
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
            //??i kho?ng th?i gian ?? ti?p t?c sinh ra m?t Fruit ho?c Bomb kh�c
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
