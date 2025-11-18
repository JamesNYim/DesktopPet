using UnityEngine;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI xpText;

    public void UpdateStats(PetStats stats)
    {
        xpText.text = $"xp: {stats.xp}";
        hungerText.text = $"Hunger: {stats.hunger}";
        happinessText.text = $"Happiness: {stats.happiness}";
        energyText.text = $"Energy: {stats.energy}";
    }
}