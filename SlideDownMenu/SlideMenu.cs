using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using System.Linq;

namespace SlideDownMenu
{
	public class SlideMenu : UIView
	{
		private UIView backgroundView;

		public UIView BackgroundView {
			get {
				return backgroundView;
			}
			set {
				backgroundView = value;
			}
		}

		private List<MenuItemView> itemViews;

		private List<MenuItem> items;

		public List<MenuItem> Items {
			get {
				return items;
			}
			set {
				items = value;
			}
		}

		MenuStateEnum menuState;
		public MenuStateEnum MenuState {
			get {
				return menuState;
			}
			set {
				menuState = value;
			}
		}

		public SlideMenu (List<MenuItem> items, PointF origin)
		{
			this.items = items;
			this.UserInteractionEnabled = true;
			this.itemViews = new List<MenuItemView>();
			this.MenuState = MenuStateEnum.IconMenu;
			this.SetupLayout(origin);
			this.ShowIconMenu (false);
		}

		private void SetupLayout(PointF origin)
		{
			var itemView = (MenuItemView)Runtime.GetNSObject(NSBundle.MainBundle.LoadNib ("MenuItemView", this, null).ValueAt(0));
			var menuHeight = itemView.Bounds.Height * this.Items.Count;
			float menuWidth = itemView.Bounds.Width;

			this.Frame = new RectangleF (origin.X, origin.Y, menuWidth, menuHeight);

			int index = 0;

			this.Items.ForEach ((mItem) => {
				var childItemView = (MenuItemView)Runtime.GetNSObject(NSBundle.MainBundle.LoadNib ("MenuItemView", this, null).ValueAt(0));
				childItemView.Frame = new RectangleF (0, 0, itemView.Bounds.Width, itemView.Bounds.Height);
				childItemView.Item = mItem;
				childItemView.TargetFrame = new RectangleF(0, childItemView.Bounds.Height * index, childItemView.Bounds.Width, childItemView.Bounds.Height);
				childItemView.BackgroundViewProxy.Alpha = 0; 
				childItemView.LabelProxy.Alpha = 0;
				childItemView.Alpha = 0;
				childItemView.UserInteractionEnabled = true;
				childItemView.Tag = childItemView.Item.Tag;
				childItemView.MenuItemDidAction = this.MenuItemDidAction;
				this.AddSubview(childItemView);
				this.SendSubviewToBack(childItemView);
				this.itemViews.Add(childItemView);
				index++;
			});
		}

		private void ShowIconMenu(bool animated)
		{
			if (animated) {
				UIView.Animate(0.3, 0, UIViewAnimationOptions.BeginFromCurrentState|UIViewAnimationOptions.TransitionCrossDissolve|UIViewAnimationOptions.CurveEaseOut|UIViewAnimationOptions.AllowAnimatedContent, () => {
					this.IterateAndShowIcons();
				}, () => {
					this.MenuState = MenuStateEnum.IconMenu;
				}); 
			} else {
				this.IterateAndShowIcons();
				this.MenuState = MenuStateEnum.IconMenu;
			}

		}

		private void IterateAndShowIcons()
		{
			for (int i = 0; i < this.itemViews.Count; i++) {
				var itemView = this.itemViews[i];
				itemView.Frame = new RectangleF(0,0,itemView.Bounds.Width, itemView.Bounds.Height);
				if(i == 0)
				{
					itemView.Alpha = 1;
					itemView.LabelProxy.Alpha = 0;
					itemView.BackgroundViewProxy.Alpha = 0;
					itemView.SeperatorViewProxy.Alpha = 0;
					itemView.ImageViewProxy.Alpha = 1;
				} else {
					itemView.LabelProxy.Alpha = 0;
					itemView.BackgroundViewProxy.Alpha = 0.7f;
					itemView.SeperatorViewProxy.Alpha = 1;
					itemView.ImageViewProxy.Alpha = 1;
					itemView.Alpha = 0;
				}
			}

			Console.WriteLine ("Frame {0} Bounds {1}", this.Frame, this.Bounds);
		}


		private void ShowFullMenu (bool animated)
		{
			if (animated) {
				UIView.Animate(0.3, 0, UIViewAnimationOptions.BeginFromCurrentState|UIViewAnimationOptions.TransitionCrossDissolve|UIViewAnimationOptions.CurveEaseOut|UIViewAnimationOptions.AllowAnimatedContent, () => {
					for (int i = 0; i < this.itemViews.Count; i++) {
						var itemView = this.itemViews[i];
						itemView.TargetFrame = new RectangleF (0, itemView.Bounds.Size.Height * i, itemView.Bounds.Size.Width, itemView.Bounds.Size.Height); 
						itemView.Alpha = 0.1f;
					}
				}, () => {
					UIView.Animate(0.3, 0, UIViewAnimationOptions.BeginFromCurrentState|UIViewAnimationOptions.TransitionCrossDissolve|UIViewAnimationOptions.CurveEaseOut|UIViewAnimationOptions.AllowAnimatedContent, () => {
						this.IterateAndShowFullMenu();
					}, () => {
						this.MenuState = MenuStateEnum.FullMenu;
					}); 
				}); 
			} else {
				this.IterateAndShowFullMenu();
				this.MenuState = MenuStateEnum.FullMenu;
			}

		}

		private void IterateAndShowFullMenu ()
		{
			for (int i = 0; i < this.itemViews.Count; i++) {
				var itemView = this.itemViews[i];

				itemView.Frame = itemView.TargetFrame;

				itemView.Alpha = 1;
				itemView.LabelProxy.Alpha = 1;
				itemView.BackgroundViewProxy.Alpha = 0.7f;
				itemView.SeperatorViewProxy.Alpha = 1;
			}
		}


		private void ShowMainMenu (bool animated)
		{
			if (animated) {
				UIView.Animate(0.3, 0, UIViewAnimationOptions.BeginFromCurrentState|UIViewAnimationOptions.TransitionCrossDissolve|UIViewAnimationOptions.CurveEaseOut|UIViewAnimationOptions.AllowAnimatedContent, () => {
					this.IterateAndShowMainMenu();
				}, () => {
					this.MenuState = MenuStateEnum.MainMenu;
				}); 
			} else {
				this.IterateAndShowMainMenu();
				this.MenuState = MenuStateEnum.MainMenu;
			}
		}

		private void IterateAndShowMainMenu()
		{
			for (int i = 0; i < this.itemViews.Count; i++) {
				var itemView = this.itemViews[i];
				itemView.Frame = new RectangleF(0,0,itemView.Bounds.Width, itemView.Bounds.Height);
				if(i == 0)
				{
					itemView.Alpha = 1;
					itemView.LabelProxy.Alpha = 1;
					itemView.BackgroundViewProxy.Alpha = 0.3f;
					itemView.SeperatorViewProxy.Alpha = 1;
					itemView.ImageViewProxy.Alpha = 1;
				} else {
					itemView.Alpha = 0;
					itemView.LabelProxy.Alpha = 1;
					itemView.BackgroundViewProxy.Alpha = 0.3f;
					itemView.SeperatorViewProxy.Alpha = 0;
					itemView.ImageViewProxy.Alpha = 1;
				}
			}
		}

		public void ToggleMenu()
		{
			switch (this.MenuState) {
			case MenuStateEnum.IconMenu:
				this.ShowFullMenu (true);
			case MenuStateEnum.MainMenu:
				this.ShowFullMenu (true);
			case MenuStateEnum.FullMenu:
				this.ShowMainMenu (true);
			default:
				break;
			}
		}

		public void OpenIconMenu()
		{
			if (this.MenuState != MenuStateEnum.IconMenu) {
				this.ShowIconMenu (true);
			}
		}

		public RectangleF GetMainIconFrame(UIView view)
		{
			var itemView = this.itemViews [0];
			return this.ConvertRectToView (itemView.Frame, view);
		}

		private void MenuItemDidAction(MenuItemView itemView)
		{
			this.itemViews.Remove (itemView);
			this.itemViews.Insert (0, itemView);
			this.ToggleMenu ();
		}

		public override UIView HitTest (PointF point, UIEvent uievent)
		{
			if (this.MenuState == MenuStateEnum.FullMenu) {
				var view = this.itemViews [0];
				if (this.Bounds.Contains (point)) {
					return base.HitTest (point, uievent);
				} else {
					return null;
				}
			} else if (this.MenuState == MenuStateEnum.IconMenu) {
				var view = this.itemViews [0];
				if (view.ImageViewProxy.Frame.Contains (point)) {
					return base.HitTest (point, uievent);
				} else {
					return null;
				}
			} else if (this.MenuState == MenuStateEnum.MainMenu) {
				var view = this.itemViews [0];
				if (view.Frame.Contains (point)) {
					return base.HitTest (point, uievent);
				} else {
					return null;
				}
			}
			return null;
		}


	}
}

