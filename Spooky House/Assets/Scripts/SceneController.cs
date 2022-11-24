using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    // Start is called before the first frame update

    private float sliderValue;
    void Awake()
    {
      Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void Start()
    {
      sliderValue = PlayerPrefs.GetFloat("speed", 1);
    }

    void OnDestroy()
    {
      Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemy == null)
        {
          _enemy = Instantiate(enemyPrefab) as GameObject;
          _enemy.transform.position = new Vector3(0,1,0);
          float angle = Random.Range(0, 360);
          _enemy.transform.Rotate(0,angle,0);
          _enemy.GetComponent<WanderingAI>().OnSpeedChanged(sliderValue);
        }
    }

    private void OnSpeedChanged(float value)
    {
      sliderValue =  value;
    }
}
