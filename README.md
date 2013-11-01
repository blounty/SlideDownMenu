SlideDownMenu
=====================

Xamarin Port Of [UzysSlideMenu](https://github.com/uzysjung/UzysSlideMenu/) - All C# NO Bindings  

Slide Down Menu

![Screenshot](https://github.com/uzysjung/UzysSlideMenu/raw/master/UzysSlideMenu.gif)

**SlideMenu features:**

* It's very simple structure.
* Very Easy to customize menu view , you can use interface builder.  

## Installation
Add the project to your solution in Xamarin Studio / Visual Studio

## Usage

Add a using

``` csharp
using SlideDownMenu
```

### Initialize
####1. make MenuItem

``` csharp
var item0 = new MenuItem ("Slide Menu", UIImage.FromBundle ("Images/a0.png"), (menuItem) => {
  			Console.WriteLine("Item: {0}", menuItem);
			});
item0.tag = 0;
```
####2. make SlideMenu
``` csharp
var slideMenu = new SlideMenu (new List<MenuItem> { item0, item1, item2 });
this.View.AddSubview (this.slideMenu);
```

## License

 - See [LICENSE](https://github.com/blounty/Xamarin.UzysSlideMenu/blob/master/LICENSE).
