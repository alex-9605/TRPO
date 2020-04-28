using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Paint.App.ChangeManager;
using Paint.App.Dto;
using Paint.App.Dto.ChangeInfoDto;
using Paint.Object;

using Graphics = System.Drawing.Graphics;

namespace Paint.App
{
    public class AutomapperConfig
    {
        private static Mapper mapper;

        public static Mapper Mapper
        {
            get { return mapper == null ? (mapper = new Mapper(new MapperConfiguration(Configure))) : mapper; }
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<LineType, LineTypeDto>();
            cfg.CreateMap<Point, PointDto>();
            cfg.CreateMap<Circle, CircleDto>();
            cfg.CreateMap<Ellipse, EllipseDto>();
            cfg.CreateMap<LineType, LineTypeDto>();
            cfg.CreateMap<Line, LineDto>();
            cfg.CreateMap<Polyline, PolylineDto>();
            cfg.CreateMap<Polygon, PolygonDto>();
            cfg.CreateMap<Shape, ShapeDto>()
                .Include<Line, LineDto>()
                .Include<Circle, CircleDto>()
                .Include<Ellipse, EllipseDto>()
                .Include<Polyline, PolylineDto>()
                .Include<Polygon, PolygonDto>();

            cfg.CreateMap<ChangeInfo, BaseChangeInfoDto>();
            cfg.CreateMap<AddShapeInfo, AddShapeChangeInfoDto>();
            cfg.CreateMap<CopyShapeChangeInfo, CopyChangeInfoDto>();
            cfg.CreateMap<CutShapeInfo, CutChangeInfoDto>();
            cfg.CreateMap<DeleteChangeInfo, DeleteShapeChangeInfoDto>();
            cfg.CreateMap<ShapeChangeInfo, ShapeChangeInfoDto>();
            cfg.CreateMap<ChangeInfo, BaseChangeInfoDto>()
                .Include<AddShapeInfo, AddShapeChangeInfoDto>()
                .Include<CopyShapeChangeInfo, CopyChangeInfoDto>()
                .Include<CutShapeInfo, CutChangeInfoDto>()
                .Include<DeleteChangeInfo, DeleteShapeChangeInfoDto>()
                .Include<ShapeChangeInfo, ShapeChangeInfoDto>();


            cfg.CreateMap<PointDto, Point>()
                .ConstructUsing(delegate(PointDto dto, ResolutionContext context)
                {
                    var graphic = (Graphics)context.Items["graphic"];
                    return new Point(dto.Id, graphic, dto.X, dto.Y);
                });
            cfg.CreateMap<CircleDto, IShape>()
                .ConstructUsing(delegate(CircleDto dto, ResolutionContext context)
                {
                    var graphic = (Graphics)context.Items["graphic"];

                    var left = context.Mapper.Map<Point>(dto.Position);
                    var lineType = context.Mapper.Map<LineType>(dto.LineType);
                    var rectWidth = dto.RectWidth;
                    var width = dto.Width;
                    var color = dto.Color;
                    var fillColor = dto.FillColor;

                    return new Circle(dto.Id, graphic, left, rectWidth, width, color, fillColor, lineType);
                });

            cfg.CreateMap<LineDto, IShape>()
                .ConstructUsing(delegate(LineDto dto, ResolutionContext context)
                {
                    var graphic = (Graphics)context.Items["graphic"];

                    var lineType = context.Mapper.Map<LineType>(dto.LineType);
                    var width = dto.Width;
                    var color = dto.Color;
                    var fillColor = dto.FillColor;
                    var start = context.Mapper.Map<Point>(dto.Start);
                    var end = context.Mapper.Map<Point>(dto.End);

                    return new Line(dto.Id, graphic, start, end, width, color, fillColor, lineType);
                });

            cfg.CreateMap<EllipseDto, IShape>()
                .ConstructUsing(delegate (EllipseDto dto, ResolutionContext context)
                {
                    var graphic = (Graphics)context.Items["graphic"];

                    var lineType = context.Mapper.Map<LineType>(dto.LineType);
                    var width = dto.Width;
                    var color = dto.Color;
                    var fillColor = dto.FillColor;

                    var position = context.Mapper.Map<Point>(dto.Position);

                    return new Ellipse(dto.Id, graphic, position, dto.RectWidth, dto.RectHeight, width, color, fillColor, lineType);
                });

            cfg.CreateMap<PolylineDto, IShape>()
                .ConstructUsing(delegate (PolylineDto dto, ResolutionContext context)
                {
                    var graphic = (Graphics)context.Items["graphic"];

                    var lineType = context.Mapper.Map<LineType>(dto.LineType);
                    var width = dto.Width;
                    var color = dto.Color;
                    var fillColor = dto.FillColor;

                    var point = context.Mapper.Map<Point[]>(dto.Points);

                    return new Polyline(dto.Id, graphic, point.ToList(), width, color, fillColor, lineType);
                });

            cfg.CreateMap<PolygonDto, IShape>()
                .ConstructUsing(delegate (PolygonDto dto, ResolutionContext context)
                {
                    var graphic = (Graphics)context.Items["graphic"];

                    var lineType = context.Mapper.Map<LineType>(dto.LineType);
                    var width = dto.Width;
                    var color = dto.Color;
                    var fillColor = dto.FillColor;

                    var point = context.Mapper.Map<Point[]>(dto.Points);

                    return new Polygon(dto.Id, graphic, point.ToList(), width, color, fillColor, lineType);
                });

            cfg.CreateMap<AddShapeChangeInfoDto, ChangeInfo>()
                .ConstructUsing(delegate (AddShapeChangeInfoDto dto, ResolutionContext context)
                {
                    var commonList = (List<IShape>)context.Items["commonList"];
                    var shape = context.Mapper.Map<IShape>(dto.Shape);

                    return new AddShapeInfo(shape, commonList);
                });

            cfg.CreateMap<CopyChangeInfoDto, ChangeInfo>()
                .ConstructUsing(delegate (CopyChangeInfoDto dto, ResolutionContext context)
                {
                    var commonList = (List<IShape>)context.Items["commonList"];
                    var shape = context.Mapper.Map<IShape>(dto.Shape);

                    return new CopyShapeChangeInfo(shape, commonList);
                });



            cfg.CreateMap<CutChangeInfoDto, ChangeInfo>()
                .ConstructUsing(delegate (CutChangeInfoDto dto, ResolutionContext context)
                {
                    var commonList = (List<IShape>)context.Items["commonList"];
                    var shape = context.Mapper.Map<IShape>(dto.Shape);
                    var oldShape = context.Mapper.Map<IShape>(dto.OldShape);

                    return new CutShapeInfo(oldShape, shape, commonList, dto.OldIndex, dto.NewIndex);
                });

            cfg.CreateMap<DeleteShapeChangeInfoDto, ChangeInfo>()
                .ConstructUsing(delegate (DeleteShapeChangeInfoDto dto, ResolutionContext context)
                {
                    var commonList = (List<IShape>)context.Items["commonList"];
                    var shape = context.Mapper.Map<IShape>(dto.Shape);

                    return new DeleteChangeInfo(shape, commonList);
                });

            cfg.CreateMap<ShapeChangeInfoDto, ChangeInfo>()
                .ConstructUsing(delegate (ShapeChangeInfoDto dto, ResolutionContext context)
                {
                    var commonList = (List<IShape>)context.Items["commonList"];
                    var shape = context.Mapper.Map<IShape>(dto.Shape);
                    var markerPoint = context.Mapper.Map<Point>(dto.MarkerPoint);
                    var point = context.Mapper.Map<Point>(dto.Point);

                    return new ShapeChangeInfo(shape, commonList, markerPoint, point);
                });
        }
    }
}
