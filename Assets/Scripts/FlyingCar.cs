using System.Collections;
using UnityEngine;

public class FlyingCar : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 ascensionPosition;
    private Vector3 cowLocation;
    private CowAscension cowAscension;
    public GameObject cow;
    public GameObject greenLight;
    public GameObject target;
    bool reachedCow;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        cow = GameObject.Find("FloatingCow");
        if (cow)
        {
            cowLocation = cow.transform.position;
        }
        rb = GetComponent<Rigidbody>();
        speed = 0.5f;
        reachedCow = false;
        cowLocation.y += 40;
        ascensionPosition = cowLocation;
        cowAscension = gameObject.AddComponent<CowAscension>();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(10);
        cowAscension.BeginCowAscension(cow);
    }

    void CowPickUp()
    {
        if (transform.position == ascensionPosition)
        {
            reachedCow = true;
        }
        else if (!reachedCow)
        {
            transform.position = Vector3.MoveTowards(transform.position, ascensionPosition, speed);
        }
        if (cow.activeSelf)
        {
            StartCoroutine(Wait());
        }
        else
        {
            transform.RotateAround(target.transform.position, Vector3.up, 30 * Time.deltaTime);
        }
    }

    void Update()
    {
        CowPickUp();
    }
}