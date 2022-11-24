using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private float revealWaitTime = 0.5f;
    [SerializeField] private TextMesh scoreLabel;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int score = 0;

    public bool canReveal
    {
      get {return _secondRevealed == null;}
    }
    // Start is called before the first frame update
    void Start()
    {
      Vector3 startPos = originalCard.transform.position;
      int[] numbers = CreateIDArray();
      numbers = ShuffleArray(numbers);
      for(int i = 0; i < gridCols; i++)
      {
        for(int j = 0; j < gridRows; j++)
        {
          MemoryCard card;
          if(i==0 && j==0)
          {
            card = originalCard;
          }
          else
          {
            card = Instantiate(originalCard) as MemoryCard;
          }
          int index = i + j * gridCols;
          int id = numbers[index];
          card.SetCard(id, images[id]);
          float posX = startPos.x + i * offsetX;
          float posY = startPos.y - j * offsetY;
          card.transform.position = new Vector3(posX, posY, startPos.z);
        }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int[] CreateIDArray()
    {
      int[] idArray = new int[2 * images.Length];
      for(int i = 0; i < images.Length; i++)
      {
        idArray[2*i] = i;
        idArray[2*i+1] =i;
      }
      return idArray;
    }

    private int[] ShuffleArray(int[] array)
    {
        int[] newArray = array.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
          int temp = newArray[i];
          int randIndex = Random.Range(i, images.Length);
          newArray[i] = newArray[randIndex];
          newArray[randIndex] = temp;
        }
        return newArray;
    }

    public void CardRevealed(MemoryCard card)
    {
      if(_firstRevealed == null)
      {
        _firstRevealed = card;
      }
      else
      {
        _secondRevealed = card;
        StartCoroutine(CheckMatch());
      }
    }

    private IEnumerator CheckMatch()
    {
      if(_firstRevealed.id == _secondRevealed.id)
      {
        score++;
        scoreLabel.text = "Score: " + score;
      }
      else
      {
        yield return new WaitForSeconds(revealWaitTime);
        _firstRevealed.Unreveal();
        _secondRevealed.Unreveal();
      }

      _firstRevealed = null;
      _secondRevealed = null;
    }

    public void Restart()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
