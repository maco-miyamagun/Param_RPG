using UnityEngine;

[RequireComponent(typeof(StatusCtrl))]
public class PlayerLevelCtrl : MonoBehaviour
{
    StatusCtrl statusCtrl;

    [SerializeField]
    int level = 1;

    [SerializeField]
    int experience = 0;

    void Awake()
    {
        statusCtrl = GetComponent<StatusCtrl>();
        if (statusCtrl != null)
        {
            statusCtrl.SetStatusFromJson();
            statusCtrl.SetStatusForLevel(level);
        }
    }

    public int Level
    {
        get { return level; }
        set
        {
            if (value > 0)
            {
                level = value;
                if (statusCtrl != null)
                {
                    statusCtrl.SetStatusForLevel(level);
                }
            }
        }
    }

    public int Experience
    {
        get { return experience; }
        set
        {
            if (value >= 0)
            {
                experience = value;
                CheckLevelUp();
            }
        }
    }

    private void CheckLevelUp()
    {
        int experienceThreshold = GetExperienceForNextLevel(level);
        if (experience >= experienceThreshold)
        {
            level++;
            experience -= experienceThreshold; // 経験値をリセット
            Debug.Log("Level Up! Current Level: " + level);
            if (statusCtrl != null)
            {
                statusCtrl.SetStatusForLevel(level);
            }
        }
    }

    private int GetExperienceForNextLevel(int current_level)
    {
        return current_level * 100; // 次のレベルに必要な経験値の例
    }
}
