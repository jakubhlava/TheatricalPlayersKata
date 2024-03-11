using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class InvoiceInfo
{
    public string Customer { get; set; }
    public IEnumerable<PlayInfo> PlayInfos { get; set; }
    
    public int VolumeCredits { get; set; }
    public CultureInfo CultureInfo { get; set; }
}