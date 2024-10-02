using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Battle_Arena
{
    //Hey sorry if you guys coded some of this project together and this doesn't match, I was absolutely knocked out in bed and didn't join the discord meeting.
    internal class Game
    {
        //Abstract class for a generic monster
        abstract class Monster
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public Monster(string name, int health)
            {
                Name = name;
                Health = health;
            }
            public abstract int Attack();
            public void TakeDamage(int damage)
            {
                Health -= damage;
                Console.WriteLine(Name + " took " + damage + " damage! Remaining health: " + Health + ".");
            }
        }
        //Inherited class for a goblin
        class Goblin : Monster
        {
            public Goblin() : base("Goblin", 30) { }
            public override int Attack()
            {
                int damage = 10;
                Console.WriteLine(Name + " attacks for " + damage + " damage!");
                return damage;
            }
        }
        //Inherited class for an orc
        class Orc : Monster
        {
            public Orc() : base("Orc", 50) { }
            public override int Attack()
            {
                int damage = 15;
                Console.WriteLine(Name + " attacks for " + damage + " damage!");
                return damage;
            }
        }
        //inherited class for a dragon
        class Dragon : Monster
        {
            public Dragon() : base("Dragon", 100) { }
            public override int Attack()
            {
                int damage = 20;
                Console.WriteLine(Name + " attacks for " + damage + " damage!");
                return damage;
            }
        }
        class Player
        {
            public string Name { get; set; }
            public int Health { get; set; }
            private const int MaxHealth = 100;
            public Player(string name)
            {
                Name = name;
                Health = MaxHealth;
            }

            public void TakeDamage(int damage)
            {
                Health -= damage;
                Console.WriteLine(Name + " took " + damage + " damage! Remaining health: " + Health);
            }
            public void Heal()
            {
                int healAmount = 30;
                Health += healAmount;
                //So the player doesn't go over their max health
                if(Health > MaxHealth)
                {
                    Health = MaxHealth;
                }
                Console.WriteLine(Name + " healed for " + healAmount + "! Current health: " + Health);
            }
            public bool isAlive()
            {
                return Health > 0;
            }
        }
        public void Run()
        {
            Console.WriteLine("Welcome to the Battle Arena!");
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            Player player = new Player(playerName);
            Monster[] monsters = new Monster[]
            {
                new Goblin(),
                new Orc(),
                new Dragon()
            };
            foreach(var monster in monsters)
            {
                Console.WriteLine("A wild " + monster.Name + " appears!");
                while(player.isAlive() && monster.Health > 0)
                {
                    Console.WriteLine("Choose an action: (1) Attack (2) Heal");
                    string input = Console.ReadLine();
                    if(input == "1")
                    {
                        //Player attacks monster
                        int playerDamage = 20;
                        monster.TakeDamage(playerDamage);
                        if(monster.Health <= 0)
                        {
                            Console.WriteLine("The " + monster.Name + " has been defeated!");
                            break;
                        }
                        //Monster counterattacks
                        int monsterDamage = monster.Attack();
                        player.TakeDamage(monsterDamage);
                        if (!player.isAlive())
                        {
                            Console.WriteLine(player.Name + " has been defeated! Game Over.");
                            return;
                        }
                    }
                    else if(input == "2")
                    {
                        //Healing
                        player.Heal();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input.");
                    }
                }
            }
            Console.WriteLine("You have defeated all of the monsters! Congratulations!");
        }
    }
}
