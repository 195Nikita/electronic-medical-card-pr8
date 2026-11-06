namespace MedicalCard;

public sealed class MedicalRecordAccessPolicy
{
    // Учебная проверка: claims и назначения в рабочей системе должны
    // поступать из доверенных серверных источников, а не из запроса клиента.
    public void Read(string role, bool activeAssignment, Action auditRead)
    {
        if (role != "doctor" || !activeAssignment)
            throw new UnauthorizedAccessException("Access denied");
        // При недоступности аудита чтение не считается завершённым.
        auditRead();
    }
}
