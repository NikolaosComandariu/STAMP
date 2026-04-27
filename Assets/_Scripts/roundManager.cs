using UnityEngine;
using TMPro;

public class roundManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI roundText;

    public void UpdateRound(int round)
    {
        roundText.text = "Round: " + round.ToString();
    }
}