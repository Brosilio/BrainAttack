namespace BrainAttack
{
	public static class BfInstruction
	{
		public const byte LBK = (byte)'[';
		public const byte RBK = (byte)']';
		public const byte MVR = (byte)'<';
		public const byte MVL = (byte)'>';
		public const byte ADD = (byte)'+';
		public const byte SUB = (byte)'-';
		public const byte PER = (byte)'.';
		public const byte COM = (byte)',';

		public const byte EXT_ZRO = (byte)'#';
		public const byte EXT_CPY = 0xFD;
	}
}
