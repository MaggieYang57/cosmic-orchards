using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public bool callGodRay = false;
    public bool playedChime = false; //added so chime only plays once when star is falling
    public bool callShootingStar = true;
    public bool changeTargetLocation = false;
    public bool planted = false;
    public int numPlanted = 0;
    public TextMeshProUGUI seedText;

    // the target will already start at first position, only need to change 4 more times
    // first position: Vector3(-54.3199997,1.89999998,72.4700012)
    public Vector3[] nextLocation = new[] {
            new Vector3(50.5f, 1.28f, -93.0999985f),
            new Vector3(58.2999992f,-0.0125286579f,44.5999985f),
            new Vector3(-68.9000015f, 1.28f, -71f),
            new Vector3(0.1f, 1.28f, 18.1000004f)
            };
    public int locIndex = 0;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        updateSeedText();
    }

    private void updateSeedText()
    {
        seedText.text = "Seeds Planted: " + numPlanted +"/5";
    }
}