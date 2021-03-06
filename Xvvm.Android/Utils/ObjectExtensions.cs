﻿namespace Xvvm.Android.Utils
{
    public static class ObjectExtensions
	{
		public static Java.Lang.Object ToJavaObj<T>(this T obj)
		{
			if (obj == null) 
			{
				return null;
			}
			return new JavaObject<T>(obj);
		}

		private class JavaObject<T> : Java.Lang.Object
		{
			public readonly T Value;

			public JavaObject(T value)
			{
				Value = value;
			}

			public override bool Equals(Java.Lang.Object that)
			{
				if (!(that is JavaObject<T>))
				{
					return false;
				}

				return Value.Equals((that as JavaObject<T>).Value);
			}

			public override int GetHashCode()
			{
				return Value.GetHashCode();
			}

			public override string ToString()
			{
				return Value.ToString();
			}
		}
	}
}
