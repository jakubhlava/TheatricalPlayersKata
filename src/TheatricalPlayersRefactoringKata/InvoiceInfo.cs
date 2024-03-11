using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class InvoiceInfo
{
    public IEnumerable<PlayInfo> PlayInfos { get; set; }
    
    public int VolumeCredits { get; set; }
}