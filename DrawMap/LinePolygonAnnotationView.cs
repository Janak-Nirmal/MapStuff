//
//  LinePolygonAnnotationView.m
//  testMapp
//
//  Created by Craig on 8/18/09.
//  Copyright Craig Spitzkoff 2009. All rights reserved.
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using MonoTouch.CoreGraphics;

namespace MapStuff.DrawMap
{
	public class LinePolygonAnnotationView : MKAnnotationView
	{
		LinePolygonAnnotationInternalView _internalView;

		MKMapView _mapView;
		public MKMapView MapView
		{
			get {return _mapView;}
			set 
			{
				_mapView = value;
				RegionChanged();
			}	
		}

		public LinePolygonAnnotationView (RectangleF rect) : base ()
		{
			base.Frame = rect;
			BackgroundColor = UIColor.Clear;
			ClipsToBounds = false;
			_internalView = new LinePolygonAnnotationInternalView();
			_internalView.MainView = this;

			AddSubview(_internalView);
		}

		public void RegionChanged ()
		{
			Debug.WriteLine("RegionChanged");

			PointF origin = new PointF(0,0);
			origin = _mapView.ConvertPointToView (origin, this);

			_internalView.Frame = new RectangleF(origin.X, origin.Y, _mapView.Frame.Size.Width, _mapView.Frame.Size.Height);
			_internalView.SetNeedsDisplay();
		}

		
		public class LinePolygonAnnotationInternalView : UIView
		{
			public LinePolygonAnnotationView MainView;

			public LinePolygonAnnotationInternalView () : base ()
			{
				BackgroundColor = UIColor.Clear;
				ClipsToBounds = false;
			}

			public override void Draw (RectangleF rect)
			{
				GeometryAnnotation myAnnotation = MainView.Annotation as GeometryAnnotation;
				
				// only draw our lines if we're not in the middle of a transition and we 
				// acutally have some points to draw. 	
				if (!this.Hidden && myAnnotation.Points != null && myAnnotation.Points.Count > 0)
				{
					CGContext context = UIGraphics.GetCurrentContext();
					if (myAnnotation.LineColor != null)
						myAnnotation.LineColor = UIColor.White;

					context.SetStrokeColorWithColor(myAnnotation.LineColor.CGColor);

					if (myAnnotation.Type == GeometryType.Polygon)
					{
						context.SetRGBFillColor (0.0f,0.0f,1.0f,1.0f);
					}

					// Draw them with a 2.0 stroke width so they are a bit more visible
					context.SetLineWidth(2.0f);

					for (int idx = 0; idx < myAnnotation.Points.Count; idx++)
					{
						CLLocation location = myAnnotation.Points[idx];
						PointF point = MainView.MapView.ConvertCoordinate(location.Coordinate, this);

						//Debug.WriteLine("Point: {0}, {1}", point.X, point.Y);

						if (idx == 0)
						{
							context.MoveTo(point.X, point.Y);
						}
						else
						{
							context.AddLineToPoint(point.X, point.Y);
						}
					}
					if (myAnnotation.Type == GeometryType.Line)
					{
						context.StrokePath ();		
					}
					else if (myAnnotation.Type == GeometryType.Polygon)
					{
						context.ClosePath();
						context.DrawPath(CGPathDrawingMode.FillStroke);	
					}

					// debug. Draw the line around our view. 
					/*
					CGContextMoveToPoint(context, 0, 0);
					CGContextAddLineToPoint(context, 0, self.frame.size.height);
					CGContextAddLineToPoint(context, self.frame.size.width, self.frame.size.height);
					CGContextAddLineToPoint(context, self.frame.size.width, 0);
					CGContextAddLineToPoint(context, 0, 0);
					CGContextStrokePath(context);
					*/
				}
			}
		}	
	}
}

