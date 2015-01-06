using System;
using UIKit;
using CoreGraphics;

namespace SlideDownMenu
{
	public partial class MenuItemView : UIView
	{
		MenuItem item;
		public MenuItem Item {
			get {
				return item;
			}
			set {
				item = value;
				this.UpdateView ();
			}
		}

		public CGRect TargetFrame {
			get;
			set;
		}

		public MenuItemView (IntPtr handle)
			:base(handle)
		{

		}

		public Action<MenuItemView> MenuItemDidAction {
			get;
			set;
		}

		public UIView BackgroundViewProxy {
			get { return this.BackgroundView; }
		}

		public UIView LabelProxy {
			get { return this.Label; }
		}

		public UIView SeperatorViewProxy {
			get { return this.SeperatorView; }
		}

		public UIView ImageViewProxy {
			get { return this.ImageView; }
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			var tapRecognizer = new UITapGestureRecognizer (this.TapGestureRecognized);
			tapRecognizer.NumberOfTapsRequired = 1;
			this.ImageView.UserInteractionEnabled = true;
			this.AddGestureRecognizer (tapRecognizer);
		}

		private void TapGestureRecognized(UITapGestureRecognizer sender){
			if (sender.State == UIGestureRecognizerState.Ended) {
				Console.WriteLine ("Tapped");
				if (Item.Action != null) {
					if (this.MenuItemDidAction != null) {
						this.MenuItemDidAction (this);
					}
					this.Item.Action (this.Item);
				}
			}
		}

		private void UpdateView()
		{
			this.ImageView.Image = this.Item.Image;
			this.Label.Text = this.Item.Title;
		}
	}
}

