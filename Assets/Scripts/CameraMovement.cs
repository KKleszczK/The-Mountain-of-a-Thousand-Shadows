using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Camera camera1;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Transform image1;
    [SerializeField] private float Smoothnes = 0.2f;
    [SerializeField] private float zoomSpeed = 10f;

    private float CameraVelocity = 0f;

    private float Center;
    private float CameraStartPos;

    private float MaxDistance = 28.9f;
    private float MinDistance = 14f;
    private float UnderMinDistance = 1.6f;

    private float CameraStartSize = 5f;
    private float CameraMaxSize = 10.5f;


    private float minCameraUp = 0f;
    private float maxCameraUp = 2.5f;

    private float cameraMaxDistanceFromStartPos = 9.75f;

    public bool overrideFollow = false;


    float backgroundScaleMultiplier = 0.51f;

    void Start()
    {
        CameraStartPos = camera1.transform.position.x;
    }


    
    void LateUpdate()
    {
        if (overrideFollow)
            return;

        float distance = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);
        if (distance > MinDistance)
        {
            float t = Mathf.InverseLerp(MinDistance, MaxDistance, distance);
            float targetZoom = Mathf.Lerp(CameraStartSize, CameraMaxSize, t);

            float tmp = Mathf.Lerp(minCameraUp, maxCameraUp, t);

            Vector3 c = camera1.transform.position;
            c.y = tmp;
            camera1.transform.position = c;

            camera1.orthographicSize = Mathf.Lerp(camera1.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        }
        else if (distance > UnderMinDistance)
        {
            camera1.orthographicSize = Mathf.Lerp(camera1.orthographicSize, CameraStartSize, Time.deltaTime * zoomSpeed);
        }

        float camSizeNormalized = (camera1.orthographicSize - CameraStartSize) / (CameraMaxSize - CameraStartSize);
        float targetScale = 1f + camSizeNormalized * backgroundScaleMultiplier;
        targetScale = Mathf.Max(targetScale, 1f);
        image1.localScale = new Vector3(targetScale, targetScale, 1f);

        Center = (player1.transform.position.x + player2.transform.position.x) / 2f;
        float pos = camera1.transform.position.x;
        pos = Mathf.SmoothDamp(pos, Center, ref CameraVelocity, Smoothnes);

            pos = Mathf.Clamp(pos, CameraStartPos - cameraMaxDistanceFromStartPos, CameraStartPos + cameraMaxDistanceFromStartPos);
            
        
        
        Vector3 finalPos = camera1.transform.position;
        Vector3 finalImagePos = image1.transform.position;
        finalPos.x = pos;
        camera1.transform.position = finalPos;
        finalImagePos.x = finalPos.x;
        image1.transform.position = finalImagePos;
    }
}
