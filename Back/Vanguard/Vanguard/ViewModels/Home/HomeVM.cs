using Vanguard.Models;
using Vanguard.ViewModels.Blog;

namespace Vanguard.ViewModels.Home;

public class HomeVM
{
    public TrendyProductVM? TrendyVM { get; set; }
    public List<BlogAllVM>? LastBlogs { get; set; }
    public List<HomeSlider> Sliders { get; set; }=new List<HomeSlider>();
}
