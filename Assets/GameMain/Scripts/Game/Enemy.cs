using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Enemy : ViewController
	{
		public int HP = 3;
		public float MovementSpeed = 2f;
		void Start()
		{
			// Code Here
		}

		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		void Update()
		{
			if (Player.Default != null)
			{
				var direction = (Player.Default.transform.position - transform.position).normalized;
				transform.Translate(direction * MovementSpeed * Time.deltaTime);
			}

			if (HP <= 0)
			{
				this.DestroyGameObjGracefully();
				UIKit.OpenPanel<UIGamePassPanel>();
			}
		}
	}
}
