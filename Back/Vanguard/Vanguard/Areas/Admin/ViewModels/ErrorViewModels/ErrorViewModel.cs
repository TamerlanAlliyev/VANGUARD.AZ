namespace Vanguard.Areas.Admin.ViewModels.ErrorsViewModel;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
