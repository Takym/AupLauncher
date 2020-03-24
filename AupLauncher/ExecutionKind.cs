using System;
using AupLauncher.Properties;

namespace AupLauncher
{
	public enum ExecutionKind : int
	{
		InvalidValue     = -2,
		ShowError        = -1,
		Nothing          =  0,
		AviUtl           =  1,
		Audacity         =  2,
		RunCustomProgram =  3,
	}

	public static class ExecutionKindExtension
	{
		private const int MIN_VALUE = ((int)(ExecutionKind.InvalidValue));
		private const int MAX_VALUE = ((int)(ExecutionKind.RunCustomProgram));

		public static ExecutionKind ToExecutionKind(this object obj)
		{
			switch (obj) {
			case int n:
				if (MIN_VALUE <= n && n <= MAX_VALUE) {
					return ((ExecutionKind)(n));
				} else {
					return ExecutionKind.InvalidValue;
				}
			case long n:
				if (MIN_VALUE <= n && n <= MAX_VALUE) {
					return ((ExecutionKind)(n));
				} else {
					return ExecutionKind.InvalidValue;
				}
			case byte[] b:
				long v;
				if (b.Length == 1) {
					v = b[0];
				} else if (b.Length == 2) {
					v = BitConverter.ToInt16(b, 0);
				} else if (b.Length == 4) {
					v = BitConverter.ToInt32(b, 0);
				} else if (b.Length == 8) {
					v = BitConverter.ToInt64(b, 0);
				} else {
					v = -2; // ExecutionKind.InvalidValue
				}
				if (MIN_VALUE <= v && v <= MAX_VALUE) {
					return ((ExecutionKind)(v));
				} else {
					return ExecutionKind.InvalidValue;
				}
			case string s:
				if (Enum.TryParse(s, out ExecutionKind result1)) {
					return result1;
				} else {
					return ExecutionKind.InvalidValue;
				}
			case string[] s:
				if (s.Length == 1 && Enum.TryParse(s[0], out ExecutionKind result2)) {
					return result2;
				} else {
					return ExecutionKind.InvalidValue;
				}
			default:
				return ExecutionKind.InvalidValue;
			}
		}

		public static LocalizedExecutionKind Localized(this ExecutionKind value)
		{
			return new LocalizedExecutionKind(value);
		}
	}

	public struct LocalizedExecutionKind
	{
		public ExecutionKind Value { get; }

		public LocalizedExecutionKind(ExecutionKind value)
		{
			this.Value = value;
		}

		public override bool Equals(object obj)
		{
			if (obj is null) {
				return false;
			} else if (obj is LocalizedExecutionKind lek) {
				return this.Value == lek.Value;
			} else if (obj is ExecutionKind ek) {
				return this.Value == ek;
			} else {
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public override string ToString()
		{
			return Resources.ResourceManager.GetString("EK_" + this.Value.ToString());
		}
	}
}
