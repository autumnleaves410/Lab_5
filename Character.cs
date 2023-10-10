using System;
namespace Lab_5;
using Newtonsoft.Json;

public class Character
{
	//all i need from API is the name, vision, weapon, nation, rarity, and description
		
	public string Name { get; set; }	
	public string Vision { get; set; }
	public string Weapon { get; set; }
	public string Nation { get; set; }
	public int Rarity { get; set; }
	public string Description { get; set; } 


	public Character()
	{
			
	}

	public Character(string name, string vision, string weapon, string nation, int rarity, string desc)
	{
		Name = name;
		Vision = vision;
		Weapon = weapon;
		Nation = nation;
		Rarity = rarity;
		Description = desc;
	}

    public override string ToString()
    {
		string characterString = "";
		characterString += $"Name: {Name}\n";
		

		return characterString;
    }
}




