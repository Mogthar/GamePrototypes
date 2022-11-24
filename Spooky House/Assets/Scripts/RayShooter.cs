using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip hitWallSound;
    [SerializeField] private AudioClip hitEnemySound;

    private Camera _camera;
    public int crosshairSize = 12;
    // public GUIStyle crosshairStyle;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    void OnGUI()
    {
        float posX = _camera.pixelWidth/2 - crosshairSize/2;
        float posY = _camera.pixelHeight/2 - crosshairSize/2;
        GUI.Label(new Rect(posX, posY, crosshairSize, crosshairSize), "*");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
              GameObject hitObject = hit.transform.gameObject;
              ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
              if(target != null)
              {
                target.ReactToHit();
                soundSource.PlayOneShot(hitEnemySound);
                Messenger.Broadcast(GameEvent.ENEMY_HIT);
              }
              else
              {
                StartCoroutine(SphereIndicator(hit.point));
                soundSource.PlayOneShot(hitWallSound);
              }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
      GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      sphere.transform.position = pos;

      yield return new WaitForSeconds(1);

      Destroy(sphere);
    }
}
