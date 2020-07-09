using System.Collections.Generic;
using static BrainAttack.BfInstruction;

namespace BrainAttack
{
	public static class Optimizer
	{
		public static int RLE(ref byte[] input, ref byte[] output, ref int[] counts)
		{
			int cnt = 1;

			List<byte> outputList = new List<byte>();
			List<int> countList = new List<int>();

			for (int i = 1; i < input.Length; i++)
			{
				byte c = input[i - 1];
				switch (c)
				{
					case ADD:
					case SUB:
					case MVR:
					case MVL:
						if (input[i] == c)
						{
							cnt++;
							continue;
						}
						else
						{
							outputList.Add(c);
							countList.Add(cnt);
							cnt = 1;
						}
						continue;

					case LBK:
					case RBK:
					case PER:
					case COM:
					case EXT_ZRO:
						outputList.Add(c);
						countList.Add(-1);
						continue;
				}
			}

			output = outputList.ToArray();
			counts = countList.ToArray();

			return output.Length;
		}

		public static void MatchLoops(ref byte[] prog, ref int[] args)
		{
			bool matched;
			do
			{
				matched = MatchNextBracketPair(ref prog, ref args);
			} while (matched);
		}

		private static bool MatchNextBracketPair(ref byte[] prog, ref int[] args)
		{
			int depth = 0, firstIndex = 0, lastIndex = 0;
			bool foundFirst = false, foundLast = false;

			for (int i = 0; i < prog.Length; i++)
			{
				switch (prog[i])
				{
					case LBK:
						if (args[i] != -1)
							continue;

						depth++;

						if (!foundFirst)
						{
							foundFirst = true;
							firstIndex = i;
						}
						break;

					case RBK:
						if (args[i] != -1)
							continue;

						depth--;

						if (depth == 0)
						{
							foundLast = true;
							lastIndex = i;
						}
						break;
				}

				if (foundFirst && foundLast)
				{
					args[firstIndex] = lastIndex;
					args[lastIndex] = firstIndex;
					break;
				}
			}

			return foundFirst && foundLast;
		}
	}
}
