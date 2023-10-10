namespace Lab_5;

using System.Globalization;
using System.Text.Json;



class Program
{
    public static async Task Main(string[] args)
    {


        bool continueApp = true;



        //Create user menu
        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Hey, Traveler!");
        Console.ResetColor();
        Console.WriteLine("What do they call you in the world of Teyvat?\n");
        Console.WriteLine("[ENTER NAME]");
        string travelerName = Console.ReadLine();
        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

        while (continueApp)
        {
            try
            {


                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Welcome to GIGL: Genshin Impact Game Library, {travelerName}!\n");
                Console.ResetColor();

                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("GENSHIN IMPACT GAME LIBRARY");
                Console.ResetColor();
                Console.WriteLine("As you embark on your journey through the Genshin Impact Game Library, which path do you wish to tread, Traveler?\n");
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

                Console.WriteLine("{1}. Gather All Characters as the Seven Gather Elements.\n");
                Console.WriteLine("{2}. Search the Archives for Knowledge\n");
                Console.WriteLine("{3}. Make A Wish on Today's Banner.\n");
                Console.WriteLine("{4}. Search for an Element\n");
                Console.WriteLine("{5}. Exit GIGL\n");


                Console.WriteLine("[ENTER OPTION CHOICE]\n");

                int choice = Convert.ToInt32(Console.ReadLine());



                switch (choice)
                {

                    //display all characters 
                    case 1:
                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

                        await CharacterCall();

                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");


                        break;

                    //search for something : display everything to search for EXCEPT NATIONS AND CHARACTERS...then filter results and display them 
                    case 2:
                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

                        await ArchiveCall();

                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

                        break;

                    //Wish on a Banner (Today's banner is Kazuha, Zhongli, Eula} Make a random character generator 
                    case 3:
                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

                        await BannerCall();

                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");
                        break;

                    //explore the nations of Teyvat
                    case 4:
                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

                        await ElementCall();

                        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");

                        break;

                    //exit the program 
                    case 5:

                        continueApp = false;
                        string message = "May the winds guide your path, adventurer.";
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
                        Console.WriteLine(message);
                        Console.ReadLine();
                        Console.ResetColor();

                        break;

                    //error message
                    default:
                        Console.WriteLine("Hm, it seems your request got lost in the winds of Teyvat. Please select a valid option.\n");
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again.");
            }
        }
    }

    //Character Call Method
    public static async Task CharacterCall()
    {
        //send request to https://api.genshin.dev/characters
        //create client

        var client = new HttpClient();

        //receive a response and store in a variable
        //use await when working with async method/resource

        HttpResponseMessage response = await client.GetAsync("https://api.genshin.dev/characters?lang=en");

        //store body of the response in a variable as json!
        string json = await response.Content.ReadAsStringAsync();

        //deserialize = .Net object from json
        //serialize = json from .Net object

        //capitalization of properties doesn't matter
        //match the json keys


        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        //deserialize json array
        List<string> genshinCharacters = JsonSerializer.Deserialize<List<string>>(json);



        foreach (var characterName in genshinCharacters)
        {
            //create the api url for each character
            string characterUrl = $"https://api.genshin.dev/characters/{characterName}?lang=en";

            //create request to fetch each character details
            HttpResponseMessage characterResponse = await client.GetAsync(characterUrl);

            if (characterResponse.IsSuccessStatusCode)
            {
                //we store the info details as JSON
                string characterJson = await characterResponse.Content.ReadAsStringAsync();

                //next we deserialize the details in a Character Object
                Character aCharacter = JsonSerializer.Deserialize<Character>(characterJson, options);


                //capitalize character names
                string capitalizedName = characterName.First().ToString().ToUpper() + characterName.Substring(1);



                //Display all character details
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Character: {capitalizedName}");
                Console.ResetColor();
                Console.WriteLine($"Vision: {aCharacter.Vision}");
                Console.WriteLine($"Weapon: {aCharacter.Weapon}");
                Console.WriteLine($"Rarity: {aCharacter.Rarity}");
                Console.WriteLine($"Description: {aCharacter.Description}\n");

            }

            else
            {
                Console.WriteLine("The information was lost in the wind, Traveler.");
            }
        }



    }

    //Element Call method
    public static async Task ElementCall()
    {
        //create client
        var client = new HttpClient();

        //receive response and store in variable
        HttpResponseMessage response = await client.GetAsync("https://api.genshin.dev/elements");

        //match the json keys
        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        //store body of the response in a variable as json!
        string json = await response.Content.ReadAsStringAsync();

        //deserialize json array
        List<string> elements = JsonSerializer.Deserialize<List<string>>(json, options);

        foreach (var element in elements)
        {
            //display all elements FIRST!
            string capitalizedElement = element.First().ToString().ToUpper() + element.Substring(1);
            Console.WriteLine($"Element: {capitalizedElement}\n");
        }


        try
        {
            //ask the user
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("Which elemental power intrigues you the most?\n");
          
            Console.WriteLine("[TYPE YOUR ELEMENT CHOICE]\n");

            string elementChoice = Console.ReadLine();

            if (elementChoice != null)
            {
                string elementURL = $"https://api.genshin.dev/elements/{elementChoice}";
                HttpResponseMessage elementResponse = await client.GetAsync(elementURL);

                if (elementResponse.IsSuccessStatusCode)
                {
                    //we store the info details as JSON
                    string elementJson = await elementResponse.Content.ReadAsStringAsync();

                    //next we deserialize the details in a Element Object
                    Element anElement = JsonSerializer.Deserialize<Element>(elementJson, options);


                    //Display all element details
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nName:{anElement.Name}");
                    Console.ResetColor();
                    Console.WriteLine($"Key: {anElement.Key}");
                    Console.WriteLine($"Reactions: \n");

                    foreach (var reaction in anElement.Reactions)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"-Name: {reaction.Name}");
                        Console.WriteLine($"-Elements: {reaction.Element}");
                        Console.WriteLine($"-Description: {reaction.Description}\n");
                        Console.ResetColor();
                    }

                }

                else
                {
                    Console.WriteLine("It seems there is an error with your input.");
                }


            }

        }

        catch (Exception e)
        {
            Console.WriteLine("Something went wrong, Traveler.");
        }




    }

    //Banner Call Method

    public static async Task BannerCall()
    {
        
        var client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync("https://api.genshin.dev/characters?lang=en");

        //store body of the response in a variable as json!
        string json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        //deserialize json array
        List<string> genshinCharacters = JsonSerializer.Deserialize<List<string>>(json);

        List<Character> bannerCharacters = new List<Character>
        {
             new Character { Name = "Kazuha"},
             new Character { Name = "Venti"},
             new Character { Name = "Eula"}
        };

        List<Character> allCharacters = genshinCharacters
        .Select(name => new Character { Name = name })
        .Concat(bannerCharacters)
        .ToList();


        Console.WriteLine("Welcome to the Wishing Banner!");
        Console.WriteLine("Press [ENTER] to Make a Wish");
        Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Character pulledCharacter = PullRandomCharacter(allCharacters);
        string name = pulledCharacter.Name; 

        string capName = name.First().ToString().ToUpper() + name.Substring(1);
        Console.WriteLine($"You pulled . . . . {capName}!");
        Console.ResetColor();

        static Character PullRandomCharacter(List<Character> characters)
        {
            Random random = new Random();
            int randomIndex = random.Next(characters.Count);
            return characters[randomIndex];
        }
    }

    //Archive Search Method

    public static async Task ArchiveCall()
    {
       try
        {
            var client = new HttpClient();
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            Console.WriteLine("Archives Menu:");
            Console.WriteLine("* Artifacts");
            Console.WriteLine("* Bosses");
            Console.WriteLine("* Characters");
            Console.WriteLine("* Consumables");
            Console.WriteLine("* Domains");
            Console.WriteLine("* Elements");
            Console.WriteLine("* Enemies");
            Console.WriteLine("* Materials");
            Console.WriteLine("* Nations");
            Console.WriteLine("* Weapons\n");
            Console.WriteLine("What are you seeking to find in the archives?");

            Console.WriteLine("[TYPE YOUR KEYWORD]\n");
            string keyword = Console.ReadLine();




            if (keyword != null)
            {
                string archiveURL = $"https://api.genshin.dev/{keyword}";
                HttpResponseMessage archiveResponse = await client.GetAsync(archiveURL);
         

                if (archiveResponse.IsSuccessStatusCode)
                {
                    //we store the info details as JSON
                    string archiveJson = await archiveResponse.Content.ReadAsStringAsync();

                    //next we deserialize the details in a Archive Object
                    List<string> archiveFeatures = JsonSerializer.Deserialize<List<string>>(archiveJson);
                   

                    //Display all archive details
                    foreach (var item in archiveFeatures)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        string capitalizedItem = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item);
                        Console.WriteLine($"* {capitalizedItem}\n");
                        Console.ResetColor();
                    }



                }

                else
                {
                    Console.WriteLine("There was an error retrieving the knowledge.");
                }

            }
        }

        catch(Exception e)
        {
            Console.WriteLine("Something went wrong, Traveler.");
        }
        




        

    }

}

