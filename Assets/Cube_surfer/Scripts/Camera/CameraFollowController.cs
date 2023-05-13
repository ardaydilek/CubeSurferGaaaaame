using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Transform heroTransform;

    private Vector3 newPosition;
    private Vector3 offset;

    [SerializeField] private float lerpValue;
    Vector3 targetPosition;

    void Start()
    {
       offset = transform.position - heroTransform.position;
    }

    void LateUpdate()
    {
        SetCameraSmoothFollow();
    }

    private void SetCameraSmoothFollow()
    {
        targetPosition = heroTransform.position + heroTransform.up * 13f - heroTransform.forward * 8f;
        // Lerp the position smoothly towards the target position
        newPosition = Vector3.Lerp(transform.position, targetPosition, lerpValue * Time.deltaTime);
        transform.position = newPosition;

        // Get the direction from the camera to the hero
        Vector3 direction = heroTransform.position - transform.position;
        direction.y = 0f;

        // Rotate the camera towards the hero with a smooth damping effect
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        // Get the current Euler angles of the camera rotation
        Vector3 currentEulerAngles = transform.rotation.eulerAngles;
        // Set the x and z rotation values of the target rotation to be the same as the current rotation
        targetRotation.eulerAngles = new Vector3(currentEulerAngles.x, targetRotation.eulerAngles.y, currentEulerAngles.z);
        // Rotate the camera towards the target rotation with a smooth damping effect
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 4f * Time.deltaTime);
    }

    public void finishCameraMovement()
    {
        Debug.Log("finishcamera");
        Vector3 finishPosition = new Vector3(targetPosition.x, targetPosition.y+25, targetPosition.z);
        targetPosition = finishPosition;
    }
}



