using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingPopup settingsPopup;
    [SerializeField] private AudioSettinsPopup audioPopup;
    private int _score;

    void Awake()
    {
      // tells messenger to add this object as listener. Specify the message it listens to and the function it does in response
      Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    void OnDestroy()
    {
      Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();
        settingsPopup.Close();
        audioPopup.Close();
    }

    private void OnEnemyHit()
    {
      _score += 1;
      scoreLabel.text = _score.ToString();
    }

    public void OnOpenSettings()
    {
        settingsPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }

    void Update()
    {
      if(Input.GetKeyDown(KeyCode.M))
      {
        bool isShowing = audioPopup.gameObject.activeSelf;
        if(isShowing)
        {
          audioPopup.Close();
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
        } else {
          audioPopup.Open();
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
        }
      }
    }
}
