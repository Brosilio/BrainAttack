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
			if (args.Length != 1)
			{
				Console.WriteLine("Usage: brainattack [file.bf]");
				return;
			}
			Console.WriteLine($"->> TARGET: {args[0]}");

			byte[] rom = File.ReadAllBytes(args[0]);
			Stopwatch sw = new Stopwatch();

			sw.Start();
			new Brain(ref rom).AdvancedThink();
			sw.Stop();
			Console.WriteLine($"Time: {sw.Elapsed.TotalMilliseconds}");
		}
	}
}
