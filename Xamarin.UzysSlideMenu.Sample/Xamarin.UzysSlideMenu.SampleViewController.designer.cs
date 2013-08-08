// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Xamarin.UzysSlideMenu.Sample
{
	[Register ("Xamarin_UzysSlideMenu_SampleViewController")]
	partial class Xamarin_UzysSlideMenu_SampleViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton MainButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView ScrollView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (MainButton != null) {
				MainButton.Dispose ();
				MainButton = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}
		}
	}
}
