using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player
{
    public string Name;
    public int Level;
    public float Health;
    public float Strength;
    public float Agility;
    public float Stamina;
    public float Defense;
    public int SkillPoints;

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Health = 100f;
        Strength = 10f;
        Agility = 10f;
        Stamina = 50f;
        Defense = 10f;
        SkillPoints = 10; 
    }

    public void LevelUp()
    {
        Level++;
        SkillPoints += 3;
    }
    public void DisplayPlayerInfo()
    {
        Debug.Log($"Player Name: {Name}, Health: {Health}, level: {Level}");
    }

    public void TakeDamege(float damage)
    {
        Health -= damage;
        Debug.Log($"{Name} took {damage} damage. Remaining health: {Health}");

        if (Health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log($"{Name} has been defeated.");
    }
    public void UpgradeStat(string statName)
    {
        if (SkillPoints <= 0) return;

        switch (statName)
        {
            case "Strength":
                Strength += 1f;
                break;
            case "Agility":
                Agility += 1f;
                break;
            case "Stamina":
                Stamina += 1f;
                break;
            case "Defense":
                Defense += 1f;
                break;
        }
        SkillPoints--;
    }
}
