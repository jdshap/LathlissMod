using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace LathlissMod.Tiles
{
	public class HatcheryTile : ModTile
	{
		public override void SetDefaults() {
			Main.tileContainer[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = false;
			Main.tileNoAttach[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.HookCheck = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new[] { 127 };
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleWrapLimit = 2; //not really necessary but allows me to add more subtypes of chairs below the example chair texture
			TileObjectData.newTile.StyleMultiplier = 2; //same as above
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight; //allows me to place example chairs facing the same way as the player
			TileObjectData.addAlternate(0); //facing right will use the second texture style
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Hatchery");
			AddMapEntry(new Color(150, 150, 150), name);
			dustType = mod.DustType("Sparkle");
			disableSmartCursor = true;
			chest = "Hatchery";
			adjTiles = new int[] { TileID.Containers };
		}

		private void openUI(int chestX, int chestY) {
            UI.HatcheryUI ui = new UI.HatcheryUI();
			ui.setChest(chestX, chestY);
			LathlissMod.Instance.HatcheryUserInterface.SetState(ui);
		}

		public override bool HasSmartInteract() {
			return true;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 0;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			if (frameX != 0) {
				i -= frameX / 16;
			}
			if (frameY != 0) {
				j -= frameY / 16;
			}
			Item.NewItem(i * 16, j * 16, 32, 0, mod.ItemType("HatcheryItem"));
			Chest.DestroyChest(i, j);
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged) {
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX != 0) {
				left -= tile.frameX / 16;
			}
			if (tile.frameY != 0) {
				top -= tile.frameY / 16;
			}
			int chest = Chest.FindChest(left, top);
			return Main.chest[chest].item[0].stack.Equals(0) && Main.chest[chest].item[1].stack.Equals(0);
		}

		public override void MouseOver(int i, int j) {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("HatcheryItem");
		}

		public override void RightClick(int i, int j) {
			Player player = Main.LocalPlayer;
			var lathlissPlayer = Main.LocalPlayer.GetModPlayer<LathlissPlayer>();
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX != 0) {
				left -= tile.frameX / 16;
			}
			if (tile.frameY != 0) {
				top -= tile.frameY / 16;
			}
			if (player.sign >= 0) {
				Main.PlaySound(SoundID.MenuClose);
				player.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
			}
			if (Main.editChest) {
				Main.PlaySound(SoundID.MenuTick);
				Main.editChest = false;
				Main.npcChatText = "";
			}
			if (player.editedChestName) {
				NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
				player.editedChestName = false;
			}
			int chest = Chest.FindChest(left, top);
			if (chest >= 0) {
				Main.stackSplit = 600;
				if (LathlissMod.Instance.HatcheryUserInterface.CurrentState != null &&  lathlissPlayer.chest == chest) {
					LathlissMod.Instance.HatcheryUserInterface.SetState(null);
					Main.PlaySound(SoundID.MenuClose);
				}
				else {
					Main.playerInventory = true;
					openUI(left, top);
					lathlissPlayer.chest = Chest.FindChest(left, top);
					Main.PlaySound(SoundID.MenuOpen);
				}
			}
		}
	}
}