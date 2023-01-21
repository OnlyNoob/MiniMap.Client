using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Data;
using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;

namespace MiniMap_Client
{
    class Track
    {
        //Drawing Line and return it
        public static Line DrawTLine(Point start, Point end, Point offset, Brush color, string tipmsg)
        {
            //Apply Offset
            start.Offset(offset.X, offset.Y);
            end.Offset(offset.X, offset.Y);
            //Color
            color = color ?? Brushes.Green;
            //Draw
            Line TrackLine = new Line();
            TrackLine.Stroke = color;
            TrackLine.X1 = start.X;
            TrackLine.X2 = end.X;
            TrackLine.Y1 = start.Y;
            TrackLine.Y2 = end.Y;
            TrackLine.HorizontalAlignment = HorizontalAlignment.Left;
            TrackLine.VerticalAlignment = VerticalAlignment.Top;
            TrackLine.StrokeThickness = 5;
            TrackLine.StrokeStartLineCap = PenLineCap.Triangle;
            TrackLine.StrokeEndLineCap = PenLineCap.Triangle;
            //ToolTip
            ToolTip tip = new ToolTip();
            tip.Content = tipmsg;
            TrackLine.ToolTip = tip; 
            //Add
            Globals.TrackDrawPlateGlobal.Children.Add(TrackLine);

            return TrackLine;
        }

        public static Label DrawSLabel(Point start, Point offset, string msg)
        {
            //Apply Offset
            start.Offset(offset.X, offset.Y);

            //Draw
            Label StationLabel = new Label();
            StationLabel.Content = msg;
            StationLabel.HorizontalAlignment = HorizontalAlignment.Left;
            StationLabel.VerticalAlignment = VerticalAlignment.Top;
            StationLabel.Margin = new Thickness(start.X, start.Y, 0, 0);
            StationLabel.IsHitTestVisible = false;
            //Add
            Globals.TrackDrawPlateGlobal.Children.Add(StationLabel);

            return StationLabel;
        }

        public static void Init(string map)
        {
            switch (map)
            {
                case "gm_mus_orange_metro_h":
                    //LoadTrackData
                    for (int LineID = 0; LineID < TrackData.track_gm_mus_orange_metro_h.TrackDataLines.GetLength(0); LineID++)
                    {
                        TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 3] = DrawTLine((Point)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 0], (Point)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 1], (Point)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 2], (Brush)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 5], (string)TrackData.track_gm_mus_orange_metro_h.TrackDataLines[LineID, 4]);
                    }
                    //LoadStationData
                    for (int LineID = 0; LineID < TrackData.track_gm_mus_orange_metro_h.StationDataLines.GetLength(0); LineID++)
                    {
                        TrackData.track_gm_mus_orange_metro_h.StationDataLines[LineID, 2] = DrawSLabel((Point)TrackData.track_gm_mus_orange_metro_h.StationDataLines[LineID, 0], (Point)TrackData.track_gm_mus_orange_metro_h.StationDataLines[LineID, 1], (string)TrackData.track_gm_mus_orange_metro_h.StationDataLines[LineID, 3]);
                    }
                    //Set DrawPlate size
                    Globals.TrackDrawPlateGlobal.Width = 1300;
                    Globals.TrackDrawPlateGlobal.Height = 350;
                    break;
            }
        }
    }
}
