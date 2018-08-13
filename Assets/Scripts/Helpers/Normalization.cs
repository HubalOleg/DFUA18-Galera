using UnityEngine;

namespace ua.com.gdg.devfest
{
	public class Normalization : MonoBehaviour
	{

		public static float NormalizeToRange(float value, float left, float right)
		{
			return value * (right - left) + left;
		}

		public static float NormalizeTo_0_1(float left, float right, float current)
		{
			if (right > left)
			{
				//Usual normalization
				return (current - left) / (right - left);
			}

			// if right end of range has lesser value compute it like usual normalization and
			// invert it (1 - normalization)
			return 1 - (current - right) / (left - right);
		}
	}
}