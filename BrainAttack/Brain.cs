using static BrainAttack.BfInstruction;

namespace BrainAttack
{
	public unsafe class Brain
	{
		public const int DEFAULT_RAM_SIZE = 32768;

		public byte[] rom, ram, prog;
		public int[] args;
		public int ins, dat;

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
			for (int ins = 0; ins < len; ins++)
				switch (prog[ins])
				{
					case RBK: if (ram[dat] != 0) ins = args[ins]; break;
					case SUB: ram[dat] -= (byte)args[ins]; break;
					case ADD: ram[dat] += (byte)args[ins]; break;
					case LBK: if (ram[dat] == 0x00) ins = args[ins]; break;
					case MVL: dat += args[ins]; break;
					case MVR: dat -= args[ins]; break;
				}
		}
	}
}
