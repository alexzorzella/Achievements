using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static List<Achievement> achievements;

    public int integer;
    public float floating_point;

    private void Start()
    {
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        if (achievements != null)
            return;

        achievements = new List<Achievement>();
        achievements.Add(new Achievement("Step By Step", "Set your integer to 110 or above.", (object o) => integer >= 100));
        achievements.Add(new Achievement("Not So Precise", "Set your floating point to 230.5 or above.", (object o) => floating_point >= 230.5));
    }

    private void Update()
    {
        CheckAchievementCompletion();
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateCompletion();
        }
    }
}

public class Achievement
{
    public Achievement(string title, string description, Predicate<object> requirement)
    {
        this.title = title;
        this.description = description;
        this.requirement = requirement;
    }

    public string title;
    public string description;
    public Predicate<object> requirement;

    public bool achieved;

    public void UpdateCompletion()
    {
        if (achieved)
            return;

        if (RequirementsMet())
        {
            Debug.Log($"{title}: {description}");
            achieved = true;
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}