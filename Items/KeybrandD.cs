using Terraria.ID;
using Terraria.ModLoader;

namespace LathlissMod.Items
{
	public class KeybrandD : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Keybrand D");
			Tooltip.SetDefault("A keybrand forged in darkness");
		}
		public override void SetDefaults()
		{
			item.damage = 130;
			item.melee = true;
			item.width = 46;
			item.height = 50;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 65400;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Keybrand);
			recipe.AddIngredient(ItemID.GoldBar, 12);
			recipe.AddIngredient(ItemID.BeetleHusk, 4);
			recipe.AddIngredient(ItemID.SoulofNight, 16);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
