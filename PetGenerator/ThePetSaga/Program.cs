using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("The Pet Saga Game Initialized.");
        StartGame();
    }

    private static void StartGame()
    {
        Console.WriteLine("Enter Player 1 name: ");
        var player1 = new Player(Console.ReadLine(), 25);

        Console.WriteLine("Enter Player 2 name: ");
        var player2 = new Player(Console.ReadLine(), 30);

        ChoosePets(player1);
        ChoosePets(player2);

        for (int i = 0; i < 3; i++) // 3 rounds
        {
            Pet player1Champion = ChooseChampion(player1, i + 1);
            Pet player2Champion = ChooseChampion(player2, i + 1);
            StartRound(player1, player1Champion, player2, player2Champion, i + 1);
        }
    }

    private static void ChoosePets(Player player)
    {
        Console.WriteLine($"{player.PlayerName}, choose your 3 pets:");
        var allPets = PetDictionaryManager.GetAllPets().ToList();

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Choose pet number {i + 1}:");
            int count = 1;

            foreach (var pet in allPets)
            {
                Console.WriteLine($"{count}: {pet.Key} - Health {pet.Value.Health}");
                count++;
            }

            int chosenPetIndex = GetPlayerChoice(allPets.Count) - 1;
            player.AddPet(allPets[chosenPetIndex].Value);
            allPets.RemoveAt(chosenPetIndex);
        }
    }

    private static Pet ChooseChampion(Player player, int roundNumber)
    {
        Console.WriteLine($"{player.PlayerName}, choose your champion for Round {roundNumber}:");
        for (int i = 0; i < player.Pets.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {player.Pets[i].Name} - Health {player.Pets[i].Health}");
        }

        int chosenIndex = GetPlayerChoice(player.Pets.Count) - 1;
        return player.Pets[chosenIndex];
    }

    private static int GetPlayerChoice(int maxChoice)
    {
        int choice;
        do
        {
            Console.WriteLine($"Enter a number between 1 and {maxChoice}:");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > maxChoice);

        return choice;
    }

    private static void StartRound(Player player1, Pet player1Champion, Player player2, Pet player2Champion, int roundNumber)
    {
        Console.WriteLine($"Round {roundNumber} begins: {player1.PlayerName}'s {player1Champion.Name} vs {player2.PlayerName}'s {player2Champion.Name}");
        
    }
}

public class Pet
{
    public string Name { get; set; }
    public int Health { get; set; }
    public List<Ability> Abilities { get; set; }

    public Pet(string name, int health)
    {
        Name = name;
        Health = health;
        Abilities = new List<Ability>();
    }

    public void AddAbility(Ability ability)
    {
        Abilities.Add(ability);
    }
}

public enum AbilityType
{
    Offensive,
    Defensive,
}

public class Ability
{
    public string Name { get; set; }
    public AbilityType Type { get; set; }
    public int Power { get; set; }

    public Ability(string name, AbilityType type, int power)
    {
        Name = name;
        Type = type;
        Power = power;
    }
}

public class Player
{
    public string PlayerName { get; set; }
    public int PlayerAge { get; set; }
    public List<Pet> Pets { get; private set; }

    public Player(string playerName, int playerAge)
    {
        PlayerName = playerName;
        PlayerAge = playerAge;
        Pets = new List<Pet>();
    }

    public bool AddPet(Pet pet)
    {
        if (Pets.Count < 3)
        {
            Pets.Add(pet);
            return true;
        }
        else
        {
            return false;
        }
    }
}

public static class PetDictionaryManager
{
    private static Dictionary<string, Pet> AvailablePets = new Dictionary<string, Pet>();

    static PetDictionaryManager()
{
    AvailablePets.Add("Dragon", new Pet("Dragon", 100));
    AvailablePets["Dragon"].AddAbility(new Ability("Fire Breath", AbilityType.Offensive, 20));
    AvailablePets["Dragon"].AddAbility(new Ability("Scale Armor", AbilityType.Defensive, 15));

    AvailablePets.Add("Knight", new Pet("Knight", 85));
    AvailablePets["Knight"].AddAbility(new Ability("Sword Slash", AbilityType.Offensive, 15));
    AvailablePets["Knight"].AddAbility(new Ability("Shield Block", AbilityType.Defensive, 20));

    AvailablePets.Add("Phoenix", new Pet("Phoenix", 90));
    AvailablePets["Phoenix"].AddAbility(new Ability("Blaze", AbilityType.Offensive, 25));
    AvailablePets["Phoenix"].AddAbility(new Ability("Rebirth", AbilityType.Defensive, 5));

    AvailablePets.Add("Troll", new Pet("Troll", 120));
    AvailablePets["Troll"].AddAbility(new Ability("Club Smash", AbilityType.Offensive, 18));
    AvailablePets["Troll"].AddAbility(new Ability("Regenerate", AbilityType.Defensive, 10));

    AvailablePets.Add("Wizard", new Pet("Wizard", 70));
    AvailablePets["Wizard"].AddAbility(new Ability("Arcane Bolt", AbilityType.Offensive, 22));
    AvailablePets["Wizard"].AddAbility(new Ability("Magic Shield", AbilityType.Defensive, 12));

    AvailablePets.Add("Elf", new Pet("Elf", 75));
    AvailablePets["Elf"].AddAbility(new Ability("Bow Shot", AbilityType.Offensive, 17));
    AvailablePets["Elf"].AddAbility(new Ability("Dodge", AbilityType.Defensive, 15));

    AvailablePets.Add("Vampire", new Pet("Vampire", 80));
    AvailablePets["Vampire"].AddAbility(new Ability("Life Drain", AbilityType.Offensive, 20));
    AvailablePets["Vampire"].AddAbility(new Ability("Mist Form", AbilityType.Defensive, 8));

    AvailablePets.Add("Golem", new Pet("Golem", 150));
    AvailablePets["Golem"].AddAbility(new Ability("Rock Throw", AbilityType.Offensive, 14));
    AvailablePets["Golem"].AddAbility(new Ability("Stony Skin", AbilityType.Defensive, 25));

    AvailablePets.Add("Necromancer", new Pet("Necromancer", 65));
    AvailablePets["Necromancer"].AddAbility(new Ability("Dark Magic", AbilityType.Offensive, 23));
    AvailablePets["Necromancer"].AddAbility(new Ability("Bone Barrier", AbilityType.Defensive, 10));

    AvailablePets.Add("Mermaid", new Pet("Mermaid", 80));
    AvailablePets["Mermaid"].AddAbility(new Ability("Water Blast", AbilityType.Offensive, 16));
    AvailablePets["Mermaid"].AddAbility(new Ability("Heal", AbilityType.Defensive, 18));

    AvailablePets.Add("Witch", new Pet("Witch", 90));
    AvailablePets["Witch"].AddAbility(new Ability("Curse", AbilityType.Offensive, 18));
    AvailablePets["Witch"].AddAbility(new Ability("Heal Spell", AbilityType.Defensive, 15));

    AvailablePets.Add("Werewolf", new Pet("Werewolf", 95));
    AvailablePets["Werewolf"].AddAbility(new Ability("Claw Attack", AbilityType.Offensive, 22));
    AvailablePets["Werewolf"].AddAbility(new Ability("Howl", AbilityType.Defensive, 12));
}


    public static Pet? GetPet(string petName)
    {
        AvailablePets.TryGetValue(petName, out Pet? pet);
        return pet;
    }

    public static Dictionary<string, Pet> GetAllPets()
    {
        return AvailablePets;
    }
}