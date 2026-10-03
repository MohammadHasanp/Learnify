using Common.Infrastructure;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Infrastructure.Persistent._Context;

namespace CoreModule.Infrastructure.Persistent.Courses;

public class CourseRepository(CoreModuleEfContext context) : BaseRepository<Course, CoreModuleEfContext>(context), ICourseRepository;
