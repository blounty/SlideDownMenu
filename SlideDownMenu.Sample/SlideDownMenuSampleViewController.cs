using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace SlideDownMenu.Sample
{
	public partial class SlideDownMenuSampleViewController : UIViewController
	{

		private SlideMenu slideMenu;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SlideDownMenuSampleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		#region View lifecycle
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			var frame = UIScreen.MainScreen.ApplicationFrame;
			this.View.Frame = frame;
			this.ScrollView.Frame = this.View.Bounds;
			this.ScrollView.ContentSize = new SizeF (this.View.Frame.Width, this.View.Bounds.Height);
			this.ScrollView.Scrolled += ScrollViewDidScroll;

			var item0 = new MenuItem ("Slide Menu", UIImage.FromBundle ("Images/a0.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				this.MoveButtonToXY(10,200);
				this.ChangeButtonBackground(0);
			});

			var item1 = new MenuItem ("Favourite", UIImage.FromBundle ("Images/a1.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				this.MoveButtonToXY(10,150);
				this.ChangeButtonBackground(1);
			});

			var item2 = new MenuItem ("Search", UIImage.FromBundle ("Images/a2.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				this.MoveButtonToXY(10,250);
				this.ChangeButtonBackground(2);
			});

			item0.Tag = 0;
			item1.Tag = 1;
			item2.Tag = 2;

			this.slideMenu = new SlideMenu (new List<MenuItem> { item0, item1, item2 }, new PointF(0f,100f));

			this.View.AddSubview (this.slideMenu);

			this.MainButton.TouchUpInside += MainButtonPressed;
		}

		#endregion

		void MainButtonPressed (object sender, EventArgs e)
		{
			this.slideMenu.ToggleMenu ();
		}

		private void ScrollViewDidScroll (object sender, EventArgs e)
		{
			this.slideMenu.OpenIconMenu ();
		}

		private void MoveButtonToXY(float x, float y)
		{
			UIView.Animate(0.2, () => {
				this.MainButton.Frame = new RectangleF(x, y, this.MainButton.Bounds.Width, this.MainButton.Bounds.Height);
			});
		}

		private void ChangeButtonBackground(int buttonNumber)
		{
			UIView.Animate(0.2, () => {
				this.MainButton.SetBackgroundImage(UIImage.FromBundle (string.Format("Images/a{0}.png", buttonNumber)), UIControlState.Normal);
			});
		}
	}
}

