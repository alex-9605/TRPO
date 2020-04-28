namespace Paint.App.Dto
{
    public class EllipseDto : ShapeDto
    {
        public PointDto Position { get; set; }

        public int RectWidth { get; set; }

        public int RectHeight { get; set; }
    }
}
