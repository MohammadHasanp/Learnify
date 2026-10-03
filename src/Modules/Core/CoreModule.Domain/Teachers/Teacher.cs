using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Domain.Teachers.Service;

namespace CoreModule.Domain.Teachers;

public class Teacher : AggregateRoot
{
    private Teacher() { }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = null!;
    public string CvFileName { get; private set; } = null!;
    public TeacherStatus TeacherStatus { get; private set; }

    public Teacher(Guid userId, string userName, string cvFileName, ITeacherService service)
    {
        Guard(userName, cvFileName, service);
        UserId = userId;
        UserName = userName;
        CvFileName = cvFileName;
        TeacherStatus = TeacherStatus.Pending;
    }


    public void ToggleStatus()
    {
        if (TeacherStatus == TeacherStatus.Active)
            TeacherStatus = TeacherStatus.Inactive;

        else if (TeacherStatus == TeacherStatus.Inactive)
            TeacherStatus = TeacherStatus.Active;
    }

    public void AcceptRequest()
    {
        if (TeacherStatus == TeacherStatus.Pending)
            TeacherStatus = TeacherStatus.Active;
    }

    void Guard(string userName, string cvFileName, ITeacherService service)
    {
        NullOrEmptyDomainDataException.CheckString((cvFileName, nameof(cvFileName)), (userName, nameof(userName)));

        if (UserName != userName)
            if (service.IsExistsUserName(userName))
                throw new InvalidDomainDataException("Username Is Exists");

        if (userName.IsUniCode())
            throw new InvalidDomainDataException("Username Invalid");
    }
}
