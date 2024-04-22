using System.Collections;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    //Bi?n ki?m tra có ?ang chém không
    private bool slicing;
    //Bi?n h??ng chém 
    public Vector3 direction { get; private set; }
    //Bi?n l?u v?n t?c di chuy?n dao th?p nh?t
    public float minSliceVelocity = 0.01f;
    public float sliceForce = 5f;

    private void Awake()
    {
        //L?y Các ??i t??ng c?n thi?t
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }
    //Hàm g?i khi Script enable
    private void OnEnable()
    {
        StopSlicing();
    }
    //Hàm g?i khi Script disable
    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        //L?y s? ki?n ghi nh?n chu?t trái xu?ng ta s? g?i hàm StartSclicing()
        if (Input.GetMouseButtonDown(0)){
            StartSlicing();
        }
        //L?y s? ki?n ghi th? chu?t trái ra s? g?i hàm StopSlicing()
        else if (Input.GetMouseButtonUp(0)){
            StopSlicing();
        }
        //Ho?c khi ta ?è chu?t trái xu?ng thì bi?n slicing s? ?úng và g?i hàm ContinueSlicing();
        else if (slicing){
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        //Ta l?y v? trí c?a con chu?t trên màn hình mainCamera và ch?nh chi?u z là 0
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        //Thay ??i v? trí c?a blade hi?n là v? trí ?ã l?y
        transform.position = newPosition;
        //Ch?nh Collider, trail c?a Blade enable và bi?n slicing thành true
        slicing = true;
        bladeCollider.enabled = true;

        bladeTrail.enabled = true;
        //Clear ?? cho ghi ?n v? trí m?i s? không t?o 1 v?t dài
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        //D?ng chém thì ??i bi?n slicing thành false và các bi?n collider và trail disable
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        //Ta l?y v? trí c?a con chu?t trên màn hình mainCamera và ch?nh chi?u z là 0
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        //Tính Vector h??ng direction
        direction = newPosition - transform.position;
        //Tính t?c ?? blade trong kho?ng th?i gian chém v?i ?? l?n h??ng chia cho th?i gian trôi qua
        float velocity = direction.magnitude / Time.deltaTime;
        //Khi t?c ?? th?p h?n bi?n minSliceVelocity thì collider s? t?t(Ví d? dao ??ng yên 1 ch?)
        bladeCollider.enabled = velocity > minSliceVelocity;
        //Thay ??i v? trí hi?n t?i thành v? trí m?i
        transform.position = newPosition;
    }
}
