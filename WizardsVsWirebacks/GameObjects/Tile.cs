using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WizardsVsWirebacks.GameObjects
{
    internal class Tile
    {
        public Texture2D texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public int levelID 
        { get { return levelID; } 
            set {levelID = value; } }

        public bool captureState
        {
            get { return captureState; }
            set { captureState = value; }
        }

        public Vector2 tilePosition
        {
            get => tilePosition;
            set { tilePosition = value; }
        }
        
        public int terrainType 
        {   get { return terrainType; }
            set { terrainType = value; }
     
        }
        

        public Tile(Texture2D Texture, int LevelID, bool CaptureState, Vector2 TilePostion, int TerrainType)
        {
            Texture = texture;
            LevelID = levelID;
            CaptureState = captureState;
            TilePostion = tilePosition;
            TerrainType = terrainType;
        }




    }







}