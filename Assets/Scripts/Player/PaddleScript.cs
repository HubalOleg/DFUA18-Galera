using UnityEngine;

namespace ua.com.gdg.devfest
{
	public class PaddleScript : MonoBehaviour
	{
		[Range(0, 90)] [SerializeField] private float _offset;
		
		// Update is called once per frame
		void Update()
		{
			float x = transform.localEulerAngles.x;
			float y = transform.localEulerAngles.y;

			x = Mathf.Clamp(x, x - _offset, x + _offset);
			y = Mathf.Clamp(y, y - _offset, y + _offset);

			transform.localEulerAngles = new Vector3(x, y, transform.localEulerAngles.z);
		}
	}
}