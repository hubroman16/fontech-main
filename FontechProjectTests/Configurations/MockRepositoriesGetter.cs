using FontechProject.Domain.Entity;
using FontechProject.Domain.Interfaces.Repositories;
using Moq;

namespace FontechProjectTests.Configurations;

public static class MockRepositoriesGetter
{
    public static Mock<IBaseRepository<Report>> GetMockReportRepository()
    {
        var mock = new Mock<IBaseRepository<Report>>();

        //var reports = GetReports().BuildMockDbSet();
        //mock.Setup(u => u.GetAll()).Returns(() => reports.Object);
        return mock;
    }

    public static Mock<IBaseRepository<User>> GetMockUserRepository()
    {
        var mock = new Mock<IBaseRepository<User>>();

        //var users = GetUsers().BuildMockDbSet();
        //mock.Setup(u => u.GetAll()).Returns(() => users.Object);
        return mock;
    }

}