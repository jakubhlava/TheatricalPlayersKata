using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public interface PrintingInterface
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays);
}