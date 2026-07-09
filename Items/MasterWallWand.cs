using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WallBuilder.Systems;

namespace WallBuilder.Items
{
    public class MasterWallWand : ModItem
    {
        public override string Texture => "WallBuilder/Items/Textures/MasterWallWand";

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = true;
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;

            Item.consumable = true;
            Item.maxStack = 9999;
        }

        public override bool? UseItem(Player player)
        {
            Point tilePos = Main.MouseWorld.ToTileCoordinates();
            WallFillSystem.FillArea(player, tilePos, 30); // ~60x60
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 150)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}