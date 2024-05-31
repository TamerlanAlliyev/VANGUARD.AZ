
using Vanguard.Models;

namespace Vanguard.ViewModels.Shop.AdditionVMs;

public class ReadyCategory
{
    public Category Category { get; set; } = null!;
    public bool IsSelected { get; set; } = false;
}
