using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace WallBuilder.Systems
{
    public static class WallFillSystem
    {
        public static int GetWallType(Player player)
        {
            bool isUnderground = player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight || player.ZoneUnderworldHeight;

            if (player.ZoneDungeon) return WallID.BlueDungeonUnsafe;

            if (player.ZoneJungle) return isUnderground ? WallID.JungleUnsafe : WallID.LivingWood;
            
            if (player.ZoneSnow) return isUnderground ? WallID.SnowWallUnsafe : WallID.BorealWood;
            if (player.ZoneDesert) return isUnderground ? WallID.Sandstone : WallID.PalmWood;
            if (player.ZoneCorrupt) return isUnderground ? WallID.EbonstoneUnsafe : WallID.Ebonwood;
            if (player.ZoneCrimson) return isUnderground ? WallID.CrimstoneUnsafe : WallID.Shadewood;
            if (player.ZoneHallow) return isUnderground ? WallID.HallowUnsafe1 : WallID.Pearlwood;
            if (player.ZoneGlowshroom) return isUnderground ? WallID.MushroomUnsafe : WallID.Mushroom;
            if (player.ZoneUnderworldHeight) return WallID.ObsidianBrickUnsafe;

            if (isUnderground)
            {
                if(player.ZoneRockLayerHeight) return WallID.CaveUnsafe;
                if(player.ZoneDirtLayerHeight) return WallID.DirtUnsafe;
            }

            return WallID.Wood;
        }

        public static void FillArea(Player player, Point center, int radius)
        {
            int wallType = GetWallType(player);

            int minX = center.X - radius;
            int maxX = center.X + radius;
            int minY = center.Y - radius;
            int maxY = center.Y + radius;

            Queue<Point> queue = new Queue<Point>();
            HashSet<Point> visited = new HashSet<Point>();

            queue.Enqueue(center);
            visited.Add(center);

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                int x = current.X;
                int y = current.Y;

                if (!WorldGen.InWorld(x, y)) continue;

                Tile tile = Main.tile[x, y];
                if (tile == null) continue;

                if (tile.WallType == 0)
                {
                    tile.WallType = (ushort)wallType;
                    WorldGen.SquareWallFrame(x, y);
                }

                if (tile.HasTile && (Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType] || tile.TileType == TileID.ClosedDoor))
                {
                    continue; 
                }

                Point[] neighbors = new Point[]
                {
                    new Point(x - 1, y),
                    new Point(x + 1, y),
                    new Point(x, y - 1),
                    new Point(x, y + 1)
                };

                foreach (Point next in neighbors)
                {
                    if (next.X < minX || next.X > maxX || next.Y < minY || next.Y > maxY) 
                        continue;

                    if (!visited.Contains(next))
                    {
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                }
            }
        }
    }
}