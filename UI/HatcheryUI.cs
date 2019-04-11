using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace LathlissMod.UI
{
    internal class HatcheryUI : UIState {
		private VanillaItemSlotWrapper _fishSlot =
            new VanillaItemSlotWrapper(ItemSlot.Context.ChestItem, 0.85f) {
				Left = { Pixels = 50 },
				Top = { Pixels = 270 },
				ValidItemFunc = item => item.IsAir || !item.IsAir 
			};
		private VanillaItemSlotWrapper _feedSlot =
            new VanillaItemSlotWrapper(ItemSlot.Context.ChestItem, 0.85f) {
				Left = { Pixels = 100 },
				Top = { Pixels = 270 },
				ValidItemFunc = item => item.IsAir || !item.IsAir,
			};
        private int linkedChest = -1;

        public override void OnInitialize() {
            ItemSlot.Options.DisableLeftShiftTrashCan = true;
            Append(_fishSlot);
            Append(_feedSlot);
        }

        public int getChest() {
            return linkedChest;
        }

        public void setChest(int i, int j) {
            linkedChest = Chest.FindChest(i, j);
            _fishSlot.Item = Main.chest[linkedChest].item[0].Clone();
            //_fishSlot.Item.stack = Main.chest[linkedChest].item[0].stack;
            _feedSlot.Item = Main.chest[linkedChest].item[1].Clone();
            //_feedSlot.Item.stack = Main.chest[linkedChest].item[1].stack;
        }

        public override void Update(GameTime gameTime) {
			base.Update(gameTime);
            Main.chest[linkedChest].item[0] = _fishSlot.Item.Clone();
            //Main.chest[linkedChest].item[0].stack = _fishSlot.Item.stack;
            Main.chest[linkedChest].item[1] = _feedSlot.Item.Clone();
            //Main.chest[linkedChest].item[1].stack = _feedSlot.Item.stack;

            if(!Main.playerInventory) LathlissMod.Instance.HatcheryUserInterface.SetState(null);
        }

         public static bool TryPlacingInChest(Item I, bool justCheck) {
            bool flag1 = false;
            Player player = Main.player[Main.myPlayer];
            Item[] objArray = player.bank.item;
            int chest = ((HatcheryUI) LathlissMod.Instance.HatcheryUserInterface.CurrentState).getChest();
            objArray = Main.chest[chest].item;
            flag1 = Main.netMode == 1;
            bool flag2 = false;
            if (I.maxStack > 1)
            {
                for (int index = 0; index < 2; ++index)
                {
                if (objArray[index].stack < objArray[index].maxStack && I.IsTheSameAs(objArray[index]))
                {
                    int num = I.stack;
                    if (I.stack + objArray[index].stack > objArray[index].maxStack)
                    num = objArray[index].maxStack - objArray[index].stack;
                    if (justCheck)
                    {
                    flag2 = flag2 || num > 0;
                    break;
                    }
                    I.stack -= num;
                    objArray[index].stack += num;
                    Main.PlaySound(7, -1, -1, 1, 1f, 0.0f);
                    if (I.stack <= 0)
                    {
                    I.SetDefaults(0, false);
                    if (flag1)
                    {
                        NetMessage.SendData(32, -1, -1, (NetworkText) null, chest, (float) index, 0.0f, 0.0f, 0, 0, 0);
                        break;
                    }
                    break;
                    }
                    if (objArray[index].type == 0)
                    {
                    objArray[index] = I.Clone();
                    I.SetDefaults(0, false);
                    }
                    if (flag1)
                    NetMessage.SendData(32, -1, -1, (NetworkText) null, chest, (float) index, 0.0f, 0.0f, 0, 0, 0);
                }
                }
            }
            if (I.stack > 0)
            {
                for (int index = 0; index < 2; ++index)
                {
                if (objArray[index].stack == 0)
                {
                    if (justCheck)
                    {
                    flag2 = true;
                    break;
                    }
                    Main.PlaySound(7, -1, -1, 1, 1f, 0.0f);
                    objArray[index] = I.Clone();
                    I.SetDefaults(0, false);
                    if (flag1)
                    {
                    NetMessage.SendData(32, -1, -1, (NetworkText) null, chest, (float) index, 0.0f, 0.0f, 0, 0, 0);
                    break;
                    }
                    break;
                }
                }
            }
            return flag2;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
            Main.craftingHide = true;

			//const int slotX = 50;
			//const int slotY = 270;
        }

    }
}