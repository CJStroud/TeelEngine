using System.Diagnostics;
using System.Dynamic;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Point = Microsoft.Xna.Framework.Point;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TeelEngine.GUI
{
    public abstract class BaseGui
    {
        #region Properties

        public Point Location
        {
            get
            {
                if (ParentContainer != null && !ForcePixelValues)
                    return ApplyAnchorOffset(new Point(
                        (int)(ParentContainer.Width * _relativeLocation.X) + ParentContainer.Location.X, 
                        (int)(ParentContainer.Height * _relativeLocation.Y) + ParentContainer.Location.Y));

                return ApplyAnchorOffset(_pixelLocation);
            }
        }

        public int Height
        {
            get
            {
                if (ParentContainer != null && !ForcePixelValues)
                    // Checking if the scaled height is not bigger than a set max height(_pixelHeight)
                    // Also checks to see if a max height has been set and if not returns the relative height
                    return (ParentContainer.Height*RelativeHeight < _pixelHeight || _pixelHeight == -1)
                        ? (int)(ParentContainer.Height*RelativeHeight)
                        : _pixelHeight;

                return _pixelHeight;
            }
        }

        public int Width
        {
            get
            {
                if (ParentContainer != null && !ForcePixelValues)
                    // Checking if the scaled width is not bigger than a set max width(_pixelWidth)
                    // Also checks to see if a max width has been set and if not returns the relative width
                    return (ParentContainer.Width * RelativeWidth < _pixelWidth || _pixelWidth == -1)
                        ? (int)(ParentContainer.Width * RelativeWidth)
                        : _pixelWidth;

                return _pixelWidth;
            }
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(Location.X, Location.Y, Width, Height);
            }
        }

        
        public Color BackColour { get; set; }

        public BaseGuiContainer ParentContainer { get; set; }

        public bool ForcePixelValues { get; set; }

        public float RelativeWidth { get; private set; }

        public float RelativeHeight { get; private set; }

        public int PixelWidth
        {
            get { return _pixelWidth; }
        }

        public int PixelHeight
        {
            get { return _pixelHeight;}
        }

        public Anchor Anchor { get; set; }

        public bool Visible { get; set; }

        public int Bottom
        {
            get
            {
                if(ParentContainer != null)
                    return (int)(ParentContainer.Height * _relativeLocation.Y) + Height;
                return Location.Y + Height;
            }
        }

        public int Right
        {
            get
            {
                if(ParentContainer != null)
                    return (int)(ParentContainer.Width * _relativeLocation.X) + Width;
                return Location.X + Width;
            }
        }

        public float Opacity
        {
            get { return _opacity; }
            set
            {
                if (value > 1F)
                {
                    _opacity = 1F;
                }
                else if (value < 0)
                {
                    _opacity = 0F;
                }
                else
                {
                    _opacity = value;
                }


            }
        }

        #endregion

        #region Private Globals

        private Texture2D _texture;

        private Vector2 _relativeLocation;

        private Point _pixelLocation;

        private int _pixelWidth = -1;

        private int _pixelHeight = -1;

        private float _opacity = 1F;

        #endregion

        #region Constructors

        public BaseGui(Texture2D texture, Point location, int width, int height, bool visible = true)
        {
            _texture = texture;
            _pixelLocation = location;
            _pixelWidth = width;
            _pixelHeight = height;
            BackColour = new Color(0,0,0,0);
            Anchor = Anchor.TopLeft;
            Visible = visible;
            Opacity = 1F;
        }

        public BaseGui(Point location, int width, int height) : this (null, location, width, height){}

        public BaseGui(Texture2D texture, Vector2 location, float width, float height, bool visible = true)
        {
            _texture = texture;
            _relativeLocation = location;
            RelativeWidth = width;
            RelativeHeight = height;
            BackColour = new Color(0, 0, 0, 0);
            Anchor = Anchor.TopLeft;
            Visible = visible;
            Opacity = 1F;
        }

        public BaseGui(Vector2 location, float width, float height) : this(null, location, width, height){}

        public BaseGui(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : this(texture, location, width, height)
        {
            _pixelWidth = maxWidth;
            _pixelHeight = maxHeight;
        }

        public BaseGui(Vector2 location, float width, float height, int maxWidth, int maxHeight)
            : this(null, location, width, height)
        {
            _pixelWidth = maxWidth;
            _pixelHeight = maxHeight;
        }

        #endregion

        #region Override Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible) return;

            if (BackColour.A > 0)
            {
                spriteBatch.Draw(CreateTextureFromColor(spriteBatch.GraphicsDevice, BackColour), BoundingRectangle,
                    Color.White * Opacity);
            }

            if (_texture == null) return;

            spriteBatch.Draw(_texture, BoundingRectangle, Color.White * Opacity);
        }

        public virtual void Update()
        {
            
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a 1x1 Texture2D of the colour passed.
        /// </summary>
        /// <param name="graphicsDevice">Used to create the Texture2D that is returned</param>
        /// <param name="colour">The disered colour of the texture</param>
        /// <returns>Returns a 1x1 Texture2D of the passed colour</returns>
        public Texture2D CreateTextureFromColor(GraphicsDevice graphicsDevice, Color colour)
        {
            var colourTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            colourTexture.SetData<Color>(new Color[] { colour });
            return colourTexture;
        }

        #region Sizing Methods

        public void SetHeight(int height)
        {
            _pixelHeight = height;
        }

        public void SetWidth(int width)
        {
            _pixelWidth = width;
        }

        public void SetSize(int width, int height)
        {
            SetWidth(width);
            SetHeight(height);
        }

        public void SetHeight(float height)
        {
            RelativeHeight = height;
        }

        public void SetWidth(float width)
        {
            RelativeWidth = width;
        }

        public void SetSize(float width, float height)
        {
            SetWidth(width);
            SetHeight(height);
        }

        #endregion

        public Point ApplyAnchorOffset(Point location)
        {
            int width = (ParentContainer != null) ? ParentContainer.Width - Right : Width;
            int height = (ParentContainer != null) ? ParentContainer.Height - Bottom : Height;

            switch (Anchor)
            {
                default:
                    return location;
                case Anchor.TopMiddle:
                    return new Point(location.X + width / 2, location.Y);
                case Anchor.TopRight:
                    return new Point(location.X + width, location.Y);
                case Anchor.MiddleLeft:
                    return new Point(location.X, location.Y + height / 2);
                case Anchor.Middle:
                    return new Point(location.X + width / 2, location.Y + height / 2);
                case Anchor.MiddleRight:
                    return new Point(location.X + width, location.Y + height / 2);
                case Anchor.BottomLeft:
                    return new Point(location.X, location.Y + height);
                case Anchor.BottomMiddle:
                    return new Point(location.X + width / 2, location.Y + height);
                case Anchor.BottomRight:
                    return new Point(location.X + width, location.Y + height);
            }
        }

        #endregion
    }
}
