using UnityEngine;

public class CowAscension : MonoBehaviour
{
    public Vector3 targetHeight;
    public Vector3 cowLocation;
    private Vector3 targetScale;
    private float shrinkSpeed = 0;
    private float rotateSpeedY;
    private float rotateSpeedX;
    private float rotateSpeedZ;
    private float movementSpeed;
    private readonly float shrinkDuration = 5000f;
    private readonly float rotationDurationY = 20f;
    private readonly float rotationDurationX = 30f;
    private readonly float rotationDurationZ = 30f;

    void Start()
    {
        targetHeight = new Vector3(300f, 72f, 330f);
        targetScale = Vector3.one * .1f;
        cowLocation = transform.position;
    }

    public void BeginCowAscension(GameObject gObj)
    {
        if (gObj.transform.position.y != targetHeight.y)
        {
            // Set the shrink speed of cow and shrink cow
            shrinkSpeed += Time.deltaTime / shrinkDuration;
            Vector3 newScale = Vector3.Lerp(gObj.transform.localScale, targetScale, shrinkSpeed);
            gObj.transform.localScale = newScale;

            // Set rotation speeds and start rotating cow
            rotateSpeedX = Time.deltaTime * rotationDurationX;
            rotateSpeedY = Time.deltaTime * rotationDurationY;
            rotateSpeedZ = Time.deltaTime * rotationDurationZ;
            gObj.transform.Rotate(rotateSpeedX, rotateSpeedY, rotateSpeedZ);

            // Set movement speed and make cow move towards Spaceship
            movementSpeed = 0.025f;
            gObj.transform.position = Vector3.MoveTowards(gObj.transform.position, targetHeight, movementSpeed);
        }
        else
        {
            gObj.SetActive(false);
        }
    }
}
