using UnityEngine;

public class Start : MonoBehaviour
{
  [SerializeField] TMPro.TextMeshProUGUI startText;

  void Awake()
  {
  }
  
  public void Clicked()
  {
    if (Game_Manager.Instance.startGame)
    {
      Game_Manager.Instance.startGame = false;
      startText.text = "start";
    }
    else
    {
      Game_Manager.Instance.startGame = true;
      startText.text = "Stop";
    }
  }
}
