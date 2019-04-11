using Terraria.ID;
using Terraria.ModLoader;

namespace LathlissMod.Items.Placeable
{
	public class HatcheryItem : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("A fish hatchery\nUsed to multiply fish");
		}

		public override void SetDefaults() {
			item.SetNameOverride("Hatchery");
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 500;
			item.createTile = mod.TileType("HatcheryTile");
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			/* recipe.AddIngredient(ItemID.StoneBlock, 26);
			recipe.AddIngredient(ItemID.Wood, 16);
			recipe.AddIngredient(ItemID.WaterBucket, 1);
			recipe.AddTile(TileID.HeavyWorkBench);*/
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}