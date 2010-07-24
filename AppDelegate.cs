/*
LICENSE FOR ICONS IN TABBAR
---------------------------
Created by Joseph Wain (see http://penandthink.com) at and probably downloaded from http://glyphish.com

This work is licensed under the Creative Commons Attribution 3.0 United States License. To view a copy of this license, visit http://creativecommons.org/licenses/by/3.0/us/ or send a letter to Creative Commons, 171 Second Street, Suite 300, San Francisco, California, 94105, USA.

You are free to share it and to remix it remix under the following conditions:

* You must attribute the work in the manner specified by the author or licensor (but not in any way that suggests that they endorse you or your use of the work).
* For any reuse or distribution, you must make clear to others the license terms of this work. The best way to do this is with a link to http://creativecommons.org/licenses/by/3.0/us/
* Any of the above conditions can be waived if you get permission from the copyright holder (send me an email!).
* Apart from the remix rights granted under this license, nothing in this license impairs or restricts the author's moral rights.

ATTRIBUTION -- a note reading "icons by Joseph Wain / glyphish.com" or similar, plus a link back to glyphish.com from your app's website, is the preferred form of attribution. Also acceptable would be, like, a link from within your iPhone application, or from the iTunes store page, but those aren't as useful to other people. If none of these work for you, please contact hello@glyphish.com and we can work something out.

USE WITHOUT ATTRIBUTION -- If attribution is not possible, workable or desirable for your application, contact hello@glyphish.com for commercial non-attributed licensing terms.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MapStuff
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
        UIWindow window;

		UITabBarController tabBarController;

		MapStuff.MapLineSharp.MapLinesViewController viewController1;
		MapStuff.DrawMap.DrawGeometryMapViewController viewController2;

		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);			

			tabBarController = new UITabBarController();

			viewController1 = new MapStuff.MapLineSharp.MapLinesViewController();
			viewController2 = new MapStuff.DrawMap.DrawGeometryMapViewController();

			viewController1.TabBarItem = new UITabBarItem ("Show", UIImage.FromFile("MapLineSharp/103-map.png"), 0);
			viewController2.TabBarItem = new UITabBarItem ("Draw", UIImage.FromFile("DrawMap/72-pin.png"), 0);
			// TODO: add iOS4 examples with MKOverlay

			tabBarController.ViewControllers = new UIViewController[] {viewController1, viewController2};

			window.AddSubview (tabBarController.View);
			window.MakeKeyAndVisible ();
			return true;
		}
	}
}

