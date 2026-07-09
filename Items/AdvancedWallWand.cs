using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WallBuilder.Systems;

namespace WallBuilder.Items
{
    public class AdvancedWallWand : ModItem
    {
        public override string Texture => "WallBuilder/Items/Textures/AdvancedWallWand";

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = true;
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;

            Item.consumable = true;
            Item.maxStack = 9999;
        }

        public override bool? UseItem(Player player)
        {
            Point tilePos = Main.MouseWorld.ToTileCoordinates();
            WallFillSystem.FillArea(player, tilePos, 15); // ~30x30
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 100)
                .AddIngredient(ItemID.FallenStar, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}