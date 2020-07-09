using System;
using System.Collections.Generic;
using System.Text;
using static BrainAttack.BfInstruction;

namespace BrainAttack
{
	public unsafe class Brain
	{
		public const int DEFAULT_RAM_SIZE = 32768;

		public byte[] rom;
		public byte[] ram;
		
		public byte[] prog;
		public int[] args;

		public int ins;
		public int dat;

		public Brain(ref byte[] rom, int ram = DEFAULT_RAM_SIZE)
		{
			this.rom = rom;
			this.ram = new byte[ram];
			Optimize();
		}

		public unsafe void Optimize()
		{
			Optimizer.RLE(ref rom, ref prog, ref args);
			Optimizer.MatchLoops(ref prog, ref args);
		}

		public unsafe void Fuck()
		{
			int len = prog.Length;

			while(ins < len)
			{
				switch(prog[ins])
				{
					case MVL:
						dat += args[ins];
						break;

					case MVR:
						dat -= args[ins];
						break;

					case ADD:
						ram[dat] += (byte)args[ins];
						break;

					case SUB:
						ram[dat] -= (byte)args[ins];
						break;

					case LBK:
						if(ram[dat] == 0x00)
							ins = args[ins];
						break;

					case RBK:
						if (ram[dat] != 0)
							ins = args[ins];
						break;

					case PER:
						//Console.Write((char)ram[dat]);
						break;

					case COM:
						ram[dat] = (byte)Console.Read();
						break;
				}

				++ins;
			}
		}

		public void StrokeOut()
		{
			ins = 0;
			dat = 0;
		}
	}
}

