using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Collider là khu v?c t?o ra các trái cây
    private Collider spawnArea;
    //List l?u danh sách các Prefabs Fruit
    public GameObject[] fruitPrefabs;
    //Game Object l?u Prefabs Bomb
    public GameObject bombPrefab;
    //T? l? qu? Bomb sinh ra trong kho?ng 0 ??n 1.
    [Range(0f, 1f)]
    public float bombChance = 0.05f;
    //Kho?ng th?i gian nhanh và ch?m nh?t ?? sinh ra Fruit
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    //Kho?ng góc ?? thay ??i ?? quay tr?c z c?a Fruit khi sinh ra
    public float minAngle = -15f;
    public float maxAngle = 15f;
    //L?c b?n lên th?p và l?n nh?t c?a m?i Fruit khi sinh ra
    public float minForce = 18f;
    public float maxForce = 22f;
    //Th?i gian t?i ?a c?a 1 Fruit t?n t?i
    public float maxLifeTime = 5f;

    private void Awake()
    {
        //L?y Collider c?a Game Object
        spawnArea = GetComponent<Collider>();
    }
    //Khi Spawner.cs Enable s? liên t?c g?i hàm Spawn() theo kho?ng th?i gian nh?t ??nh
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    //Khi Spawner.cs disable s? d?ng h?t hàm Coroutines ?ang ch?y
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        //??i 2 giây tr??c khi b?t ??u Spawn
        yield return new WaitForSeconds(2f);
        
        while (enabled)
        {
            //L?y ng?u nhiên 1 Prefab trong list Prefabs Fruit ?ã truy?n vào
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            //Random t? l? ?? sinh Bomb
            if(Random.value < bombChance)
            {
                prefab = bombPrefab;
            }
            //L?y v? trí ng?u nhiên n?m trên khu v?c sinh ra Fruit
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            //L?y ?? quay ng?u nhiên b?ng tr?c z c?a Fruit khi sinh ra
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));
            //T?o 1 b?n sao Game Object Fruit trên Scene, v?i prefab, v? trí, ?? quay ?ã có
            GameObject fruit = Instantiate(prefab, position, rotation);
            //Xoá b?n sao Fruit ?ã t?o trong kho?ng th?i gian maxLifeTime
            Destroy(fruit, maxLifeTime);
            //L?y ng?u nhiên l?c b?n lên c?a Fruit khi sinh ra
            float force = Random.Range(minForce, maxForce);
            //L?y RigidBody c?a fruit và thêm l?c tác ??ng b?ng AddForce
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
            //??i kho?ng th?i gian ?? ti?p t?c sinh ra m?t Fruit ho?c Bomb khác
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
