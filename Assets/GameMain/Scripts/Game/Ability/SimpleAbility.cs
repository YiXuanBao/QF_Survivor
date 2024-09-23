using UnityEngine;
using QFramework;

namespace Survivor
{
	public partial class SimpleAbility : ViewController
	{
		private float mCurrentSeconds = 0f;

		void Start()
		{
			// Code Here
		}

		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		void Update()
		{
			mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= 1.5f)
			{
				mCurrentSeconds = 0f;
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

				foreach (var enemy in enemies)
				{
					var distance = (Player.Default.transform.position - enemy.transform.position).magnitude;
					if (distance < 5f)
					{
						var enemyRefCache = enemy;
						enemyRefCache.Sprite.color = Color.red;
						ActionKit.Delay(0.3f, () =>
						{
							enemyRefCache.HP--;
							enemyRefCache.Sprite.color = Color.white;
						}).StartGlobal();
					}
				}
			}
		}
	}
}
