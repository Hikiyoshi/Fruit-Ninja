using UnityEngine;

public class Bomb : MonoBehaviour
{
    //Hàm kiểm tra bom có bị chém hay không
    private void OnTriggerEnter(Collider other)
    {
        //Kiểm tra collider khi dụng vào bom có phải blade hay không
        if (other.CompareTag("Player"))
        {
            //Gọi hàm Explode trong GameManager
            FindObjectOfType<GameManager>().Explode();
        }
    }
}
