using System;
using MonoTouch.UIKit;

namespace SlideDownMenu
{
	public class MenuItem
	{
		private string title;
		public string Title {
			get {
				return title;
			}
			set {
				title = value;
			}
		}

		private UIImage image;
		public UIImage Image {
			get {
				return image;
			}
			set {
				image = value;
			}
		}

		private Action<MenuItem> action;
		public Action<MenuItem> Action {
			get {
				return action;
			}
			set {
				action = value;
			}
		}

		int tag;
		public int Tag {
			get {
				return tag;
			}
			set {
				tag = value;
			}
		}
		
		public MenuItem (string title, UIImage image, Action<MenuItem> action)
		{
			this.action = action;
			this.image = image;
			this.title = title;
		}

		public override string ToString ()
		{
			return string.Format ("Title:{0} Tag:{1}", this.title, this.tag);
		}
	}
}

