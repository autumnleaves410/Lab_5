using System;
namespace Lab_5
{
	public class Element
	{
		public string Name { get; set; }
		public string Key { get; set; }
		public List<Reaction> Reactions{ get; set; }

		public Element()
		{
			

		}

		public Element(string name, string key, List<Reaction> reactions)
		{
			Name = name;
			Key = key;
			Reactions = reactions;
		}
	}

	public class Reaction
	{
		public string Name { get; set; }
		public List<string> Element { get; set; }
		public string Description { get; set; }


		public Reaction()
		{

		}

		public Reaction(string name, List<string> element, string desc)
		{
			Name = name;
			Element = element;
			Description = desc;

		}
	}



}

