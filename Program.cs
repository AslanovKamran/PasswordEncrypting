using HashingAndSalting.Models;
using System.Security.Cryptography;
using System.Text;

namespace HashingAndSalting
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Start();
		}
		static void Start()
		{
			Console.Clear();
			Console.WriteLine("[R] Register [L] Log in");
			var userInput = Console.ReadLine()?.ToUpper()[0];
			while (true)
			{
				switch (userInput)
				{
					case 'R': { Register(); } break;
					case 'L': { Login(); } break;
					default: { Console.WriteLine("Wrong"); } break;
				}
			}

		}

		static void Register() 
		{
			Console.Clear();
			Console.WriteLine("\n================Register================\n");
			Console.Write("User Name: ");
			var name = Console.ReadLine();
			Console.Write("\nUser Pswr: ");
			var password = Console.ReadLine();

			var salt = DateTime.Now.ToString();
			var hashed = HashPassword($"{password!}{salt}");


			using var dbContext = new AppDbContext();
			dbContext?.Users?.Add(new User { Name = name!, Password = hashed!, Salt = salt });
			dbContext?.SaveChanges();

			while (true)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine("Registration Completed");
				Console.ResetColor();

				Console.WriteLine("Press [B] to go back");
				if (Console.ReadKey().Key == ConsoleKey.B) 
				{
					Start();
					
				}
			}
		}

		static void Login() 
		{
			Console.Clear();
			Console.WriteLine("\n================Login================\n");
			Console.Write("User Name: ");
			var name = Console.ReadLine();
			Console.Write("\nUser Pswr: ");
			var password = Console.ReadLine();

			using var dbContext = new AppDbContext();
			var userFound = dbContext?.Users?.Any(x => x.Name == name);
			if ((userFound != null) && userFound == true)
			{
				var loggedInUser = dbContext?.Users?.FirstOrDefault(x => x.Name == name);

				if (HashPassword($"{password}{loggedInUser!.Salt}") == loggedInUser.Password) 
				{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.WriteLine("Access Granted");
					Console.ResetColor();
					Console.ReadLine();
				}
				else
				{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine("Access Denied");
					Console.ResetColor();
					Console.ReadLine();
				}
			}
		}

		static string HashPassword(string password)
		{
			SHA256 hash = SHA256.Create();
			var hashedPasswordBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
			return Convert.ToBase64String(hashedPasswordBytes);
		}


	}
}