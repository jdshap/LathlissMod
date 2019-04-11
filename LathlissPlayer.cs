using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LathlissMod {
    public class LathlissPlayer : ModPlayer {
        public int chest;

        public override void clientClone(ModPlayer clientClone) {
			LathlissPlayer clone = clientClone as LathlissPlayer;
			// Here we would make a backup clone of values that are only correct on the local players Player instance.
			// Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
			ModPacket packet = mod.GetPacket();
            packet.Write((byte)LathlissMod.LathlissModMessageType.LathlissPlayerSyncPlayer);
			packet.Write((byte)player.whoAmI);
            packet.Write((int) chest);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer) {
			// Here we would sync something like an RPG stat whenever the player changes it.
			LathlissPlayer clone = clientPlayer as LathlissPlayer;
		}
    }
}