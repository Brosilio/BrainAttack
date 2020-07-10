using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
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

		public unsafe void Think()
		{
			StringBuilder sb = new StringBuilder();

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
					case PER: sb.Append((char)ram[dat]); break;
					case COM: ram[dat] = (byte)Console.Read(); break;
				}

			Console.WriteLine(sb);
		}

		public unsafe void AdvancedThink()
		{
			AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("FUCK"), AssemblyBuilderAccess.RunAndCollect);
			ModuleBuilder mb = ab.DefineDynamicModule("FUCK");
			TypeBuilder tb = mb.DefineType("CUCK");
			MethodBuilder meth = tb.DefineMethod("SLUT", MethodAttributes.Public | MethodAttributes.Static);
			ILGenerator il = meth.GetILGenerator();

			Type _type = tb.CreateType();

			ThinkHard(ref il);

			tb.GetMethod("SLUT").Invoke(null, null);
		}

		private void ThinkHard(ref ILGenerator gen)
		{
			MethodInfo SbAppendMi = typeof(StringBuilder).GetMethod("Append");

			// declare memory pointer and init to zero
			gen.DeclareLocal(typeof(int));
			gen.Emit(OpCodes.Ldc_I4_0);
			gen.Emit(OpCodes.Stloc_0);

			// declare RAM and allocate array
			gen.DeclareLocal(typeof(byte[]));
			gen.Emit(OpCodes.Ldc_I4, DEFAULT_RAM_SIZE);
			gen.Emit(OpCodes.Newarr, typeof(byte));
			gen.Emit(OpCodes.Stloc_1);

			gen.DeclareLocal(typeof(StringBuilder));
			gen.Emit(OpCodes.Newobj, typeof(StringBuilder));
			gen.Emit(OpCodes.Stloc_2);

			int len = prog.Length;
			for (int i = 0; i < len; i++)
			{
				byte inst = prog[i];
				int arg = args[i];

				switch (inst)
				{
					case RBK: if (ram[dat] != 0) ins = args[ins]; break;
					case SUB:
						gen.Emit(OpCodes.Ldloc_1);
						gen.Emit(OpCodes.Ldloc_0);
						gen.Emit(OpCodes.Ldloc_1);
						gen.Emit(OpCodes.Ldloc_0);
						gen.Emit(OpCodes.Ldelem_U1);
						gen.Emit(OpCodes.Ldc_I4, arg);
						gen.Emit(OpCodes.Sub);
						gen.Emit(OpCodes.Conv_U1);
						gen.Emit(OpCodes.Stelem_I1);
						break;

					case ADD:
						gen.Emit(OpCodes.Ldloc_1);
						gen.Emit(OpCodes.Ldloc_0);
						gen.Emit(OpCodes.Ldloc_1);
						gen.Emit(OpCodes.Ldloc_0);
						gen.Emit(OpCodes.Ldelem_U1);
						gen.Emit(OpCodes.Ldc_I4, arg);
						gen.Emit(OpCodes.Add);
						gen.Emit(OpCodes.Conv_U1);
						gen.Emit(OpCodes.Stelem_I1);
						break;

					case LBK: if (ram[dat] == 0x00) ins = args[ins]; break;

					case MVL:
						gen.Emit(OpCodes.Ldloc_0);
						gen.Emit(OpCodes.Ldc_I4, arg);
						gen.Emit(OpCodes.Sub);
						gen.Emit(OpCodes.Stloc_0);
						break;

					case MVR:
						gen.Emit(OpCodes.Ldloc_0);
						gen.Emit(OpCodes.Ldc_I4, arg);
						gen.Emit(OpCodes.Add);
						gen.Emit(OpCodes.Stloc_0);
						break;

					case PER:
						gen.Emit(OpCodes.Ldloc_2);
						gen.EmitCall(OpCodes.Call, SbAppendMi, null);
						break;
					case COM: ram[dat] = (byte)Console.Read(); break;
				}
			}

			gen.Emit(OpCodes.Ret);
		}
	}
}
