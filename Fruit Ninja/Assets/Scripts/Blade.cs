using System.Collections;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    //Bi?n ki?m tra c� ?ang ch�m kh�ng
    private bool slicing;
    //Bi?n h??ng ch�m 
    public Vector3 direction { get; private set; }
    //Bi?n l?u v?n t?c di chuy?n dao th?p nh?t
    public float minSliceVelocity = 0.01f;
    public float sliceForce = 5f;

    private void Awake()
    {
        //L?y C�c ??i t??ng c?n thi?t
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }
    //H�m g?i khi Script enable
    private void OnEnable()
    {
        StopSlicing();
    }
    //H�m g?i khi Script disable
    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        //L?y s? ki?n ghi nh?n chu?t tr�i xu?ng ta s? g?i h�m StartSclicing()
        if (Input.GetMouseButtonDown(0)){
            StartSlicing();
        }
        //L?y s? ki?n ghi th? chu?t tr�i ra s? g?i h�m StopSlicing()
        else if (Input.GetMouseButtonUp(0)){
            StopSlicing();
        }
        //Ho?c khi ta ?� chu?t tr�i xu?ng th� bi?n slicing s? ?�ng v� g?i h�m ContinueSlicing();
        else if (slicing){
            ContinueSlicing();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.QuitGame();
        }
    }

    private void StartSlicing()
    {
        //Ta l?y v? tr� c?a con chu?t tr�n m�n h�nh mainCamera v� ch?nh chi?u z l� 0
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        //Thay ??i v? tr� c?a blade hi?n l� v? tr� ?� l?y
        transform.position = newPosition;
        //Ch?nh Collider, trail c?a Blade enable v� bi?n slicing th�nh true
        slicing = true;
        bladeCollider.enabled = true;

        bladeTrail.enabled = true;
        //Clear ?? cho ghi ?n v? tr� m?i s? kh�ng t?o 1 v?t d�i
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        //D?ng ch�m th� ??i bi?n slicing th�nh false v� c�c bi?n collider v� trail disable
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        //Ta l?y v? tr� c?a con chu?t tr�n m�n h�nh mainCamera v� ch?nh chi?u z l� 0
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        //T�nh Vector h??ng direction
        direction = newPosition - transform.position;
        //T�nh t?c ?? blade trong kho?ng th?i gian ch�m v?i ?? l?n h??ng chia cho th?i gian tr�i qua
        float velocity = direction.magnitude / Time.deltaTime;
        //Khi t?c ?? th?p h?n bi?n minSliceVelocity th� collider s? t?t(V� d? dao ??ng y�n 1 ch?)
        bladeCollider.enabled = velocity > minSliceVelocity;
        //Thay ??i v? tr� hi?n t?i th�nh v? tr� m?i
        transform.position = newPosition;
    }
}
