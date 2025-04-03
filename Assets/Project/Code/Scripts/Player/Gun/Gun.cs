using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int maxMisiles = 2;
    public int misiles = 2;

    private Transform aimTransform;

    [SerializeField] private Transform shootPoint;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        Aiming();
        ShootingBullets();
        if (misiles >= 1)
        {
            ShootingMisiles();
        }
    }


    private void Aiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void ShootingBullets()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = BulletPool.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.SetActive(true);
            }
        }
    }

    private void ShootingMisiles()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject misile = MisilePool.instance.GetPooledObject();
            if (misile != null)
            {
                misile.transform.position = shootPoint.position;
                misile.transform.rotation = shootPoint.rotation;
                misile.SetActive(true);
                misiles -= 1;
            }
        }
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWZ()
    {
        return GetMouseWorldPositionWZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

}
