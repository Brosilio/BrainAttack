using System;
using System.Diagnostics;
using System.IO;

namespace BrainAttack
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("LOW-ORBIT TRANSIENT ISCHEMIC ATTACK INITIALIZING...");
			if(args.Length != 1)
			{
				Console.Write("INTERACTIVE MODE - ENTER INPUT FILENAME: ");
				string s = Console.ReadLine();
				if(string.IsNullOrWhiteSpace(s))
				{
					Console.WriteLine("!! NO TARGET PROVIDED !!");
					Console.WriteLine("Usage: brainattack [file.bf]");
					return;
				}

				args = new[] { s };
			}
			Console.WriteLine($"target acquired: {args[0]}");
			

			byte[] rom = File.ReadAllBytes(args[0]);
			Stopwatch sw = new Stopwatch();
			Brain brain = new Brain(ref rom);
			sw.Start();
			brain.Fuck();
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);
		}
	}
}
