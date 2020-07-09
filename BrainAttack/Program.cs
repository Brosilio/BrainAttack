using System;
using System.IO;

namespace BrainAttack
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("LOW-ORBIT TRANSIENT ISCHEMIC ATTACK INITIALIZING...");
			if (args.Length != 1)
			{
				Console.WriteLine("Usage: brainattack [file.bf]");
				return;
			}
			Console.WriteLine($"->> TARGET: {args[0]}");

			byte[] rom = File.ReadAllBytes(args[0]);
			new Brain(ref rom).Fuck();
		}
	}
}
