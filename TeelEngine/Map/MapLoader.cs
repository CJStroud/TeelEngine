using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public static class MapLoader
    {
        /// <summary>
        /// INCOMPLETE: This is a method for testing that will return a hardcoded layercontroller
        /// </summary>
        /// <returns></returns>
        public static LayerController Load()
        {
            // incomplete
            var layerController = new LayerController();
            var terrainLayer = new TerrainLayer();
            ITile tile = CreateTile(1, new Vector2(1,1));
            terrainLayer.Add(tile);


            var entityLayer = new EntityLayer();
            IEntity entity = CreateEntity(1, new Vector2(2, 2));


            return layerController;
        }

        public static LayerController Load(string filePath)
        {
            var layerController = new LayerController();
            int mapWidth = -1;
            int mapHeight = -1;
            XDocument doc = XDocument.Load(filePath);

            
            var xElement = doc.Element("Tile-Size");
            if (xElement != null) Globals.TileSize = Convert.ToInt32(xElement.Value);



            foreach (var layerDescendant in doc.Descendants("Layer"))
            {
                switch (layerDescendant.Attribute("Id").Value)
                {
                    case "Entity":
                        EntityLayer entityLayer = CreateEntityLayer(layerDescendant);
                        layerController.Add(entityLayer);
                        break;
                    case "Terrain":
                        TerrainLayer terrainLayer = CreateTerrainLayer(layerDescendant);
                        layerController.Add(terrainLayer);
                        break;
                }
            }

            return layerController;
        }

        private static TerrainLayer CreateTerrainLayer(XElement layerDescendant)
        {
            var terrainLayer = new TerrainLayer();
            foreach (var terrainDescendant in layerDescendant.Descendants("Tile"))
            {
                int id = Convert.ToInt32(terrainDescendant.Attribute("id"));
                foreach (var tileDescendant in terrainDescendant.Descendants())
                {
                    Vector2 location = ExtractLocation(tileDescendant);
                    var tile = CreateTile(id, location);
                    terrainLayer.Add(tile);
                }
            }
            return terrainLayer;
        }

        private static EntityLayer CreateEntityLayer(XElement layerDescendant)
        {
            var entityLayer = new EntityLayer();
            foreach (var entityLayerDescendant in layerDescendant.Descendants("Entity"))
            {
                int id = Convert.ToInt32(entityLayerDescendant.Attribute("id"));
                foreach (var entityDescendant in entityLayerDescendant.Descendants())
                {
                    Vector2 location = ExtractLocation(entityDescendant);
                    var entity = CreateEntity(id, location);
                    entityLayer.Add(entity);
                }
            }
            return entityLayer;
        }

        private static ITile CreateTile(int id, Vector2 location)
        {
            ITile tile = TerrainLayer.TileList[id];
            tile.Location = location;
            return tile;
        }

        private static IEntity CreateEntity(int id, Vector2 location)
        {
            IEntity entity = EntityLayer.EntityList[id];
            entity.Location = location;
            return entity;
        }

        private static Vector2 ExtractLocation(XElement descendant)
        {
            string[] point = descendant.Value.Split(',');
            return new Vector2(Convert.ToSingle(point[0]), Convert.ToSingle(point[1]));
        }
    }
}
