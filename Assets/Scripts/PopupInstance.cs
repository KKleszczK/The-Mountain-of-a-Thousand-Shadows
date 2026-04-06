using UnityEngine;
using UnityEngine.UI;
//Old veersion Codee
public class PopupInstance : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeDuration = 1f;
    public float rotationAngle = 15f;

    private float lifetime;
    private CanvasGroup canvasGroup;
    private Vector3 moveDirection;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        lifetime = fadeDuration;
        moveDirection = Vector3.up + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        transform.Rotate(0, 0, Random.Range(-rotationAngle, rotationAngle));
    }

    void Update()
    {
        transform.position += moveDirection * floatSpeed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = Mathf.Clamp01(lifetime / fadeDuration);
        }

        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}