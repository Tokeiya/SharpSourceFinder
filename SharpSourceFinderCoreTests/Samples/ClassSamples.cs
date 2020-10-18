using System;
using System.Collections.Generic;
using System.Text;

namespace NameSpace
{
	public class Public
	{
		public public (int value){}
		public int Value { get; set; }

		public int Method()
		{
		}

	}

	internal class Internal
	{

	}

	public unsafe class Unsafe
	{

	}

	public partial class Partial
	{

	}

	public abstract class Abstract
	{

	}

	public sealed class Sealed
	{

	}

	public unsafe sealed partial class UnsafePartialSealed
	{

	}

	public class Envelope
	{
		private class Private
		{
			
		}

		protected internal class ProtectedInternal
		{ }

		private protected class PrivateProtected
		{ }

		protected class Protected
		{
			
		}
	}
}
