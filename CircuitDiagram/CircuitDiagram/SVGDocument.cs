﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Media;
using System.Windows;

namespace SVGLibrary
{
    public class SVGDocument
    {
        XmlWriter m_writer;
        MemoryStream m_stream;

        public SVGDocument()
        {
            m_stream = new MemoryStream();
            m_writer = XmlWriter.Create(m_stream);
            m_writer.WriteStartDocument();
            m_writer.WriteDocType("svg", "-//W3C//DTD SVG 1.1//EN", "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd", null);
            m_writer.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            m_writer.WriteAttributeString("version", "1.1");
            m_writer.WriteAttributeString("width", "100%");
            m_writer.WriteAttributeString("height", "100%");
        }

        public void DrawLine(Color color, double thickness, Point point0, Point point1)
        {
            m_writer.WriteStartElement("line");
            m_writer.WriteAttributeString("x1", point0.X.ToString());
            m_writer.WriteAttributeString("y1", point0.Y.ToString());
            m_writer.WriteAttributeString("x2", point1.X.ToString());
            m_writer.WriteAttributeString("y2", point1.Y.ToString());
            m_writer.WriteAttributeString("style", "stroke:rgb(0,0,0);stroke-width:" + thickness.ToString());
            m_writer.WriteEndElement();
        }

        public void DrawEllipse(Color fillColor, Color strokeColor, double strokeThickness, Point centre, double radiusX, double radiusY)
        {
            string fillOpacity = ((float)fillColor.A / 255f).ToString();

            m_writer.WriteStartElement("ellipse");
            m_writer.WriteAttributeString("cx", centre.X.ToString());
            m_writer.WriteAttributeString("cy", centre.Y.ToString());
            m_writer.WriteAttributeString("rx", radiusX.ToString());
            m_writer.WriteAttributeString("ry", radiusY.ToString());
            m_writer.WriteAttributeString("style", "fill-opacity:" + fillOpacity + ";fill:rgb(0,0,0);stroke:rgb(0,0,0);stroke-width:" + strokeThickness.ToString());
            m_writer.WriteEndElement();
        }

        public void DrawRectangle(Color fillColor, Color strokeColor, double strokeThickness, Rect rectangle)
        {
            m_writer.WriteStartElement("rect");
            m_writer.WriteAttributeString("x", rectangle.X.ToString());
            m_writer.WriteAttributeString("y", rectangle.Y.ToString());
            m_writer.WriteAttributeString("width", rectangle.Width.ToString());
            m_writer.WriteAttributeString("height", rectangle.Height.ToString());
            m_writer.WriteAttributeString("style", "fill-opacity:0;stroke:rgb(0,0,0);stroke-width:" + strokeThickness.ToString());
            m_writer.WriteEndElement();
        }

        public void DrawText(string text, string font, double emSize, Color foreground, Point origin)
        {
            m_writer.WriteStartElement("text");
            m_writer.WriteAttributeString("x", origin.X.ToString());
            m_writer.WriteAttributeString("y", origin.Y.ToString());
            m_writer.WriteAttributeString("style", "font-family:" + font + ";font-size:" + emSize.ToString());
            m_writer.WriteString(text);
            m_writer.WriteEndElement();
        }

        public void DrawPath(Color fillColor, Color strokeColor, double strokeThickness, string data)
        {
            string fillOpacity = ((float)fillColor.A / 255f).ToString();

            m_writer.WriteStartElement("path");
            m_writer.WriteAttributeString("d", data);
            m_writer.WriteAttributeString("style", "fill-opacity:" + fillOpacity + ";fill:rgb(0,0,0);stroke:rgb(0,0,0);stroke-width:" + strokeThickness.ToString());
            m_writer.WriteEndElement();
        }

        public void Save(string path)
        {
            m_writer.WriteEndElement();
            m_writer.WriteEndDocument();
            m_writer.Flush();
            byte[] data = m_stream.ToArray();
            File.WriteAllBytes(path, data);
            m_stream.Close();
        }
    }
}
