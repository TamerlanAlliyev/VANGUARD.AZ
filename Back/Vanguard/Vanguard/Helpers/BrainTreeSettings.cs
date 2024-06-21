using System.Security.Cryptography.X509Certificates;

namespace Vanguard.Helpers;

public class BrainTreeSettings
{
    public string Environment { get; set; } = null!;
    public string MerchantId {  get; set; }
    public string PublicKey {  get; set; }
    public string PrivateKey {  get; set; }
}


