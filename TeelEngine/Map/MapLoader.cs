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
                        var entityLayer = new EntityLayer();
                        foreach (var entityLayerDescendant in layerDescendant.Descendants("Entity"))
                        {
                            int id = Convert.ToInt32(entityLayerDescendant.Attribute("id"));
                            foreach (var entityDescendant in entityLayerDescendant.Descendants())
                            {
                                IEntity entity = EntityLayer.EntityList[id];
                                string[] pieces = entityDescendant.Value.Split(',');
                                float x = Convert.ToSingle(pieces[0]);
                                float y = Convert.ToSingle(pieces[1]);
                                entity.Location = new Vector2(x, y);
                                entityLayer.Entities.Add(entity);
                            }
                        }
                        layerController.Add(entityLayer);
                        break;
                    case "Terrain":
                        var terrainLayer = new TerrainLayer();
                        foreach (var terrainDescendant in layerDescendant.Descendants("Tile"))
                        {
                            int id = Convert.ToInt32(terrainDescendant.Attribute("id"));
                            foreach (var tileDescendant in terrainDescendant.Descendants())
                            {
                                ITile tile = TerrainLayer.TileList[id];
                                string[] pieces = tileDescendant.Value.Split(',');
                                float x = Convert.ToSingle(pieces[0]);
                                float y = Convert.ToSingle(pieces[1]);
                                tile.Location = new Vector2(x, y);
                                terrainLayer.Tiles.Add(tile);
                            }
                        }
                        layerController.Add(terrainLayer);
                        break;
                }
            }

            return layerController;
        }
    }
}
