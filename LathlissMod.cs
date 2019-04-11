using LathlissMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace LathlissMod
{

	class LathlissMod : Mod
	{
		internal static LathlissMod Instance;
		internal UserInterface HatcheryUserInterface;

		public LathlissMod() {
			Instance = this;
		}

		public override void Load() {
			HatcheryUserInterface = new UserInterface();
		}

		public override void UpdateUI(GameTime gameTime) {
			if(HatcheryUserInterface != null) HatcheryUserInterface.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
			if (inventoryIndex != -1) {
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Example Person UI",
					delegate {
						// If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
						HatcheryUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI) {
			LathlissModMessageType msgType = (LathlissModMessageType)reader.ReadByte();
			switch (msgType) {
				case LathlissModMessageType.LathlissPlayerSyncPlayer:
					byte playernumber = reader.ReadByte();
					LathlissPlayer lathlissPlayer = Main.player[playernumber].GetModPlayer<LathlissPlayer>();
					lathlissPlayer.chest = reader.ReadInt32();
					// SyncPlayer will be called automatically, so there is no need to forward this data to other clients.
					break;
			}
		}

		internal enum LathlissModMessageType : byte {
			LathlissPlayerSyncPlayer
		}
	}
}
