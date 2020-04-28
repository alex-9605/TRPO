namespace Paint.App.Dto
{
    public class LineDto : ShapeDto
    {
        public PointDto Start { get; set; }

        public PointDto End { get; set; }
    }
}
