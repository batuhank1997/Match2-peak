using Game.Core.Enums;
using UnityEngine;

namespace Game.Core.LevelBase
{
	public abstract class LevelData
	{		
		public abstract ItemType GetNextFillItemType();
		public abstract void Initialize();

		public ItemType[,] GridData { get; protected set; }

		private static readonly ItemType[] DefaultCubeArray = new[]
		{
			ItemType.GreenCube,
			ItemType.YellowCube,
			ItemType.BlueCube,
			ItemType.RedCube
		};

		private static readonly ItemType DefaultBalloonItem = ItemType.Balloon;

		private static readonly ItemType[] ColoredBalloonArray = new[]
		{
			ItemType.BlueBalloon,
			ItemType.GreenBalloon,
			ItemType.YellowBalloon,
			ItemType.RedBalloon,
		};

		protected static ItemType GetRandomCubeItemType()
		{
			return GetRandomItemTypeFromArray(DefaultCubeArray);
		}
		
		protected static ItemType GetDefaultBalloonItemType()
		{
			return (DefaultBalloonItem);
		}
		
		protected static ItemType GetRandomBalloonItemType()
		{
			return GetRandomItemTypeFromArray(ColoredBalloonArray);
		}

		protected static ItemType GetRandomItemTypeFromArray(ItemType[] itemTypeArray)
		{
			return itemTypeArray[Random.Range(0, itemTypeArray.Length)];
		}
	}
}
