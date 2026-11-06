using MedicalCard;
using Xunit;

public class MedicalRecordAccessPolicyTests
{
    [Fact]
    public void AssignedDoctorReadIsAudited()
    {
        var reads = 0;
        new MedicalRecordAccessPolicy().Read("doctor", true, () => reads++);
        Assert.Equal(1, reads);
    }

    [Theory]
    [InlineData("doctor", false)]
    [InlineData("patient", true)]
    [InlineData("administrator", true)]
    public void UnauthorizedReadIsDenied(string role, bool assigned)
    {
        var reads = 0;
        Assert.Throws<UnauthorizedAccessException>(() =>
            new MedicalRecordAccessPolicy().Read(role, assigned, () => reads++));
        Assert.Equal(0, reads);
    }

    [Fact]
    public void AuditFailureStopsRead()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new MedicalRecordAccessPolicy().Read("doctor", true,
                () => throw new InvalidOperationException("Audit unavailable")));
    }
}
