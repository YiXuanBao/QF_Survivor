using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class Player : ViewController
	{
		public static Player Default { get; private set; }
		public float MovementSpeed = 5f;

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
			Default = this;
		}
		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		void OnDestroy()
		{
			Default = null; ;
		}

		void Start()
		{
			HurtBox.OnTriggerEnter2DEvent(colider2D =>
			{
				this.DestroyGameObjGracefully();
				UIKit.OpenPanel<UIGameOverPanel>();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		void Update()
		{
			var h = Input.GetAxis("Horizontal");
			var v = Input.GetAxis("Vertical");

			var direction = new Vector2(h, v).normalized;

			SelfRigidbody2D.velocity = direction * MovementSpeed;
		}
	}
}
