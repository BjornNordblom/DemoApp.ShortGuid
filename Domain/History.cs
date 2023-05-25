using System.ComponentModel.DataAnnotations;
using DemoApp.ValueTypes;

public abstract class History
{
    public int Subject { get; set; }
    public string Description { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}

public class DebtorHistory : History
{
    [Key]
    public DebtorId DebtorId { get; set; } = default!;
    public Debtor Debtor { get; set; } = default!;
}

public class ClaimHistory : History
{
    [Key]
    public ClaimId ClaimId { get; set; } = default!;
    public Claim Claim { get; set; } = default!;
}
