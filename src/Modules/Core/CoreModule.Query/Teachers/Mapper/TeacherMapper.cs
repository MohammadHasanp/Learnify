using CoreModule.Query._Data.Entities;
using CoreModule.Query.DTOs;
using CoreModule.Query.Teachers.DTOs;

namespace CoreModule.Query.Teachers.Mapper;

static class TeacherMapper
{
    public static TeacherDto Map(this TeacherQueryModel model)
    {
        return new TeacherDto()
        {
            CreationDate = model.CreationDate,
            CvFileName = model.CvFileName,
            TeacherStatus = model.TeacherStatus,
            IsDelete = model.IsDelete,
            Id = model.Id,
            UserName = model.UserName,
            User = new CoreModuleUserDto()
            {
                Avatar = model.Users.Avatar,
                Id = model.Users.Id,
                IsDelete = model.Users.IsDelete,
                CreationDate = model.Users.CreationDate,
                Email = model.Users.Email,
                Family = model.Users.Family,
                Mobile = model.Users.Mobile,
                Name = model.Users.Name
            }
        };
    }
}
