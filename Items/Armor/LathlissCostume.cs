using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LathlissMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class LathlissHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lathliss Head");
			Tooltip.SetDefault("Great for impersonating devs!");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			//item.value = 10000;
			item.rare = 9;
			item.vanity = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			//recipe.AddRecipe();
		}
    }

}